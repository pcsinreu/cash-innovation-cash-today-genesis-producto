Imports System.Data
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Global.Saldos

Public Class AccionRecuperarSaldos

#Region "Atributos"

    Private _SoloSaldosDisponible As Nullable(Of Boolean)

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Valida los datos obligatorios de lo objecto Peticion
    ''' </summary>
    ''' <param name="Peticion">RecuperarSaldos.Peticion</param>
    ''' <param name="Respuesta">RecuperarSaldos.Respuesta</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
    Private Function ValidarPeticion(Peticion As RecuperarSaldos.Peticion, ByRef Respuesta As RecuperarSaldos.Respuesta) As Boolean

        'Objeto que recebe as mensagens
        Dim Mensagens As New System.Text.StringBuilder

        'Verifica se o usuário foi informado
        If (Peticion.Usuario IsNot Nothing AndAlso (String.IsNullOrEmpty(Peticion.Usuario.Login) _
            OrElse String.IsNullOrEmpty(Peticion.Usuario.Clave))) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(Traduzir("08_msg_dados_usuario_naoinformado"))

        End If

        ' verifica se os dados do usuário são válidos
        If Peticion.Usuario IsNot Nothing AndAlso (Peticion.Usuario IsNot Nothing AndAlso (Not String.IsNullOrEmpty(Peticion.Usuario.Login) AndAlso Not String.IsNullOrEmpty(Peticion.Usuario.Clave))) Then

            Dim objUsuario As New Negocio.Usuario
            objUsuario.Nombre = Peticion.Usuario.Login
            objUsuario.Clave = Peticion.Usuario.Clave
            objUsuario.Ingresar()

            If objUsuario.Id <> -1 Then
                If objUsuario.Bloqueado Then
                    ' adiciona texto de mensagem
                    Mensagens.AppendLine(Traduzir("08_msg_login_bloqueado"))
                End If
            Else
                ' adiciona texto de mensagem
                Mensagens.AppendLine(Traduzir("08_msg_login_invalido"))
            End If

        End If

        ' Verifica se a Fecha/Hora Hasta é maior do que a Desde
        If (Not String.IsNullOrEmpty(Peticion.FechaHoraSaldoDesde) AndAlso Not String.IsNullOrEmpty(Peticion.FechaHoraSaldoHasta)) Then

            ' Verfica se a FechaHora Hasta é menor do que a Desde
            If Peticion.FechaHoraSaldoHasta < Peticion.FechaHoraSaldoDesde Then

                ' Adiciona texto de mensagem
                Mensagens.AppendLine(Traduzir("08_msg_dados_fecha_invalida"))

            End If

        End If

        'Verifica se há mensagem de informação inválida
        If (Mensagens.Length > 0) Then

            'Preenche o objeto de resposta
            Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            Respuesta.MensajeError = Mensagens.ToString

            'Retorna falso
            Return False
        Else

            'Retorna verdadeiro
            Return True
        End If

    End Function

    ''' <summary>
    ''' Método principal responsável por obter dados de saldos 
    ''' e chamar os metodos que converte para o objeto respuesta
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 07/07/2011 - Creado
    ''' </history>
    Public Function Ejecutar(Peticion As RecuperarSaldos.Peticion) As RecuperarSaldos.Respuesta

        Dim objRespuesta As New RecuperarSaldos.Respuesta
        Dim objSaldos As Negocio.Saldos
        Dim objPlanta As Negocio.Planta
        Dim objCentrosProceso As Negocio.CentrosProceso
        Dim objBancos As Negocio.Bancos
        Dim objClientes As Negocio.Clientes
        Dim i As Integer = 0

        Me._SoloSaldosDisponible = Peticion.SoloSaldoDisponible

        Try

            ' Valida los datos de la peticion
            If ValidarPeticion(Peticion, objRespuesta) Then

                objSaldos = New Negocio.Saldos

                'Rellena los parametros básicos
                objSaldos.DiscriminarEspecies = Peticion.SaldoDesglosado
                objSaldos.SoloSaldoDisponible = If(Peticion.SoloSaldoDisponible Is Nothing, False, Peticion.SoloSaldoDisponible)

                ' Rellena la fecha
                If Peticion.FechaHoraSaldoHasta = DateTime.MinValue Then
                    objPlanta = New Negocio.Planta
                    objPlanta.IdPS = Peticion.CodigoPlanta
                    objPlanta.CentrosProcesoRealizarByIdsPlanta()
                    objSaldos.Fecha = Util.GetDateTime(objPlanta.CodDelegacionGenesis)

                    objSaldos.Actual = True

                Else

                    objSaldos.Actual = False
                    objSaldos.Fecha = Peticion.FechaHoraSaldoHasta

                End If

                'Busca os Ids Centro proceso do IdPS sector informado
                objCentrosProceso = New Negocio.CentrosProceso

                If (Not String.IsNullOrEmpty(Peticion.CodigoSector)) Then

                    'Busca el Id del Centro de Proceso
                    objCentrosProceso.IdPS = Peticion.CodigoPlanta & Peticion.CodigoSector ' IdPsPlanta + IdPsSector
                    objCentrosProceso.Realizar()

                ElseIf (Not String.IsNullOrEmpty(Peticion.CodigoPlanta)) Then

                    'Busca el Idps de los Centros de Procesos de la Planta
                    objPlanta = New Negocio.Planta
                    objPlanta.IdPS = Peticion.CodigoPlanta
                    objPlanta.CentrosProcesoRealizarByIdsPlanta()
                    objCentrosProceso = objPlanta.CentrosProceso

                Else

                    ' Busca todos os centros de proceso
                    objCentrosProceso.Interplantas = True
                    objCentrosProceso.Usuario.Id = 0
                    objCentrosProceso.Realizar()

                End If

                'Preenche a lista de Idps separados por '|'
                ' Verifica se existe centros de processos
                If objCentrosProceso IsNot Nothing AndAlso objCentrosProceso.Count > 0 Then

                    'Para cada centro de processo existente
                    For Each CentroProceso In objCentrosProceso

                        If CentroProceso.Id = 0 AndAlso Not String.IsNullOrEmpty(CentroProceso.IdPS) Then

                            'Busca el Id del Centro de Proceso
                            Dim oCentrosProceso = New Negocio.CentrosProceso
                            oCentrosProceso.IdPS = CentroProceso.IdPS
                            oCentrosProceso.Realizar()

                            'Preenche a lista com el Id
                            objSaldos.ListaIdCentroProceso &= oCentrosProceso.First.Id & "|"
                        Else

                            'Preenche a lista com el Id
                            objSaldos.ListaIdCentroProceso &= CentroProceso.Id & "|"

                        End If

                    Next

                    objSaldos.ListaIdCentroProceso = "|" & objSaldos.ListaIdCentroProceso

                End If

                'Preenche a lista de Id Monedas
                If Not String.IsNullOrEmpty(Peticion.CodigoMoneda) Then

                    Dim dsMoneda As DataSet = ObterListaIdMoneda(Peticion)

                    If (dsMoneda IsNot Nothing AndAlso dsMoneda.Tables.Count > 0) Then

                        'Obtem os identificadores das tabelas contidas no dataset
                        Dim IdMoneda = CStr((From IdentificadorMoneda As DataRow In dsMoneda.Tables(Negocio.Moneda.T_PD_MONEDA).Rows
                                             Select IdentificadorMoneda(Negocio.Moneda.C_IDMONEDA)).FirstOrDefault)

                        'Preenche a lista com el Id
                        objSaldos.ListaIdMoneda = "|" & IdMoneda & "|"

                    End If

                Else

                    ' Busca todas as monedas visibles
                    Dim objMonedas As New Negocio.Monedas
                    objMonedas.Realizar()

                    ' Para cada moneda existente
                    For Each m As Negocio.Moneda In objMonedas
                        'Preenche a lista com el Id
                        objSaldos.ListaIdMoneda &= m.Id & "|"
                    Next

                    objSaldos.ListaIdMoneda = "|" & objSaldos.ListaIdMoneda

                End If

                'Preenche a lista de Id Banco
                If Not String.IsNullOrEmpty(Peticion.CodigoCanal) Then

                    ' Busca el Id del Banco
                    objBancos = New Negocio.Bancos
                    objBancos.IdPS = Peticion.CodigoCanal
                    objBancos.Realizar()

                    'Se existe banco
                    If objBancos.Count > 0 Then

                        'Preenche a lista com el Id
                        objSaldos.ListaIdBanco = "|" & objBancos.First.Id & "|"

                    End If

                Else 'Se nao for enviado a lista de bancos a consulta considera todos bancos

                    objSaldos.TodosBancos = True
                    objSaldos.ListaIdBanco = Nothing

                End If

                'Preenche a lista de Id Clientes
                If Not String.IsNullOrEmpty(Peticion.CodigoCliente) Then

                    ' Busca el Id del Cliente
                    objClientes = New Negocio.Clientes
                    objClientes.IdPS = Peticion.CodigoCliente
                    objClientes.Realizar()

                    'Se existe cliente
                    If objClientes.Count > 0 Then

                        'Preenche a lista com el Id
                        objSaldos.ListaIdCliente = "|" & objClientes.First.Id & "|"

                    End If

                Else
                    objSaldos.TodosClientes = True
                    objSaldos.ListaIdCliente = Nothing
                End If

                ' Recupera o saldo
                objSaldos.Realizar(False, True)

                ' Preenche o retorno
                PreencherRespuesta(objRespuesta, Peticion, objSaldos)

                'Caso não ocorra exceção, retorna o objrespuesta com codigo 0 e mensagem erro vazio
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Preenche o objeto respuesta com os dados do objeto saldo
    ''' </summary>
    ''' <history>
    ''' [maoliveira] - 07/07/2011 - Creado
    ''' </history>
    Private Sub PreencherRespuesta(ByRef objRespuesta As RecuperarSaldos.Respuesta, _
                                   objPeticao As RecuperarSaldos.Peticion, _
                                   objSaldos As Negocio.Saldos)

        ' Recupera o saldo inicial
        Dim objSaldoInicial As New RecuperarSaldos.Saldo

        ' Recupera o saldo final
        Dim objSaldoFinal As New RecuperarSaldos.Saldo

        ' Para cada saldo existente
        For Each objSaldo As Negocio.Saldo In objSaldos

            ' Preenche o saldo
            PreencherSector(objSaldo, objSaldoFinal)

        Next

        ' Verifica se o objeto de resposta não está vazio
        If objRespuesta IsNot Nothing Then

            ' Verifica se a coleção de saldos não está vazia
            If objRespuesta.Saldo Is Nothing Then

                ' Cria uma nova lista
                objRespuesta.Saldo = New RecuperarSaldos.Saldo

            End If

            ' Verifica se as horas são diferentes
            If objPeticao.FechaHoraSaldoHasta <> objPeticao.FechaHoraSaldoDesde Then

                objSaldos.Fecha = objPeticao.FechaHoraSaldoDesde
                objSaldos.Actual = False
                objSaldos.ConSaldosCero = True
                objSaldos.Realizar(False, True)

                ' Para cada saldo existente
                For Each objSaldo As Negocio.Saldo In objSaldos

                    ' Preenche o saldo
                    PreencherSector(objSaldo, objSaldoInicial)

                Next

                AjustarSaldo(objSaldoInicial, objSaldoFinal)

            End If

            objRespuesta.Saldo = objSaldoFinal

        End If

    End Sub

    ''' <summary>
    ''' Ajusta o Saldo final (SaldoFinal - SaldoInicial)
    ''' </summary>
    ''' <param name="SaldoInicial">RecuperarSaldos.Saldo</param>
    ''' <param name="SaldoFinal">RecuperarSaldos.Saldo</param>
    ''' <remarks></remarks>
    Private Sub AjustarSaldo(SaldoInicial As RecuperarSaldos.Saldo, ByRef SaldoFinal As RecuperarSaldos.Saldo)

        ' Se existe Sectores 
        If SaldoFinal.Sectores IsNot Nothing Then

            ' Para cada setor existente no saldo final
            For Each sf As RecuperarSaldos.Sector In SaldoFinal.Sectores

                ' Recupera o setor no saldo inicial
                Dim sector As RecuperarSaldos.Sector = SaldoInicial.Sectores.Where(Function(si) si.Codigo = sf.Codigo).FirstOrDefault

                ' Ajusta o Saldo do Sector
                AjustarSaldoSector(sector, sf)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Ajusta o Saldo final do Sector (SaldoFinal - SaldoInicial)
    ''' </summary>
    ''' <param name="SaldoSectorInicial">RecuperarSaldos.Sector</param>
    ''' <param name="SaldoSectorFinal">RecuperarSaldos.Sector</param>
    ''' <remarks></remarks>
    Private Sub AjustarSaldoSector(SaldoSectorInicial As RecuperarSaldos.Sector, ByRef SaldoSectorFinal As RecuperarSaldos.Sector)

        ' Se existe Clientes 
        If SaldoSectorFinal.Clientes IsNot Nothing Then

            ' Para cada sector existente no saldo final
            For Each cf As RecuperarSaldos.Cliente In SaldoSectorFinal.Clientes

                ' Recupera o cliente no saldo inicial
                Dim cliente As RecuperarSaldos.Cliente = SaldoSectorInicial.Clientes.Where(Function(ci) ci.Codigo = cf.Codigo).FirstOrDefault

                ' Ajusta o Saldo do Cliente
                AjustarSaldoCliente(cliente, cf)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Ajusta o Saldo final do Cliente (SaldoFinal - SaldoInicial)
    ''' </summary>
    ''' <param name="SaldoClienteInicial">RecuperarSaldos.Cliente</param>
    ''' <param name="SaldoClienteFinal">RecuperarSaldos.Cliente</param>
    ''' <remarks></remarks>
    Private Sub AjustarSaldoCliente(SaldoClienteInicial As RecuperarSaldos.Cliente, ByRef SaldoClienteFinal As RecuperarSaldos.Cliente)

        ' Se existe Canales 
        If SaldoClienteFinal.Canales IsNot Nothing Then

            ' Para cada cliente existente no saldo final
            For Each cf As RecuperarSaldos.Canal In SaldoClienteFinal.Canales

                ' Recupera o canal no saldo inicial
                Dim canal As RecuperarSaldos.Canal = SaldoClienteInicial.Canales.Where(Function(ci) ci.Codigo = cf.Codigo).FirstOrDefault

                ' Ajusta o Saldo do Canal
                AjustarSaldoCanal(canal, cf)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Ajusta o Saldo final do Canal (SaldoFinal - SaldoInicial)
    ''' </summary>
    ''' <param name="SaldoCanalInicial">RecuperarSaldos.Canal</param>
    ''' <param name="SaldoCanalFinal">RecuperarSaldos.Canal</param>
    ''' <remarks></remarks>
    Private Sub AjustarSaldoCanal(SaldoCanalInicial As RecuperarSaldos.Canal, ByRef SaldoCanalFinal As RecuperarSaldos.Canal)

        ' Se existe Monedas 
        If SaldoCanalFinal.Monedas IsNot Nothing Then

            ' Para cada cliente existente no saldo final
            For Each mf As RecuperarSaldos.Moneda In SaldoCanalFinal.Monedas

                ' Recupera o cliente no saldo inicial
                Dim moneda As RecuperarSaldos.Moneda = SaldoCanalInicial.Monedas.Where(Function(mi) mi.Codigo = mf.Codigo).FirstOrDefault

                ' Subtrai o saldo inicial do saldo final 
                mf.Importe = mf.Importe - moneda.Importe

                ' Ajusta o Saldo da Moneda
                AjustarSaldoMoneda(moneda, mf)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Ajusta o Saldo final do Moneda (SaldoFinal - SaldoInicial)
    ''' </summary>
    ''' <param name="SaldoMonedaInicial">RecuperarSaldos.Moneda</param>
    ''' <param name="SaldoMonedaFinal">RecuperarSaldos.Moneda</param>
    ''' <remarks></remarks>
    Private Sub AjustarSaldoMoneda(SaldoMonedaInicial As RecuperarSaldos.Moneda, ByRef SaldoMonedaFinal As RecuperarSaldos.Moneda)

        ' Se existe especies
        If SaldoMonedaFinal.Especies IsNot Nothing Then

            ' Para cada cliente existente no saldo final
            For Each ef As RecuperarSaldos.Especie In SaldoMonedaFinal.Especies

                ' Recupera o cliente no saldo inicial
                Dim especie As RecuperarSaldos.Especie = SaldoMonedaInicial.Especies.Where(Function(ei) ei.Codigo = ef.Codigo).FirstOrDefault

                ' Subtrai o saldo inicial do saldo final 
                ef.Importe = ef.Importe - especie.Importe
                ef.Cantidad = ef.Cantidad - especie.Cantidad

            Next

        End If

    End Sub

    ''' <summary>
    ''' Gera duas tabelas contendo os códigos de IsoDivisa e Denominação e seus valores relacionando os campos 
    ''' Código CodigoMoneda (MiAgencia) com IdMoneda (Saldos)
    ''' </summary>
    ''' <param name="Peticao">RecuperarSaldos.Peticion</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
    Private Function ObterListaIdMoneda(Peticao As RecuperarSaldos.Peticion) As DataSet

        'DataSet que recebera as duas tabelas
        Dim ds As New DataSet

        'Tabelas de ids
        Dim dtMonedas As DataTable

        'Lista de IsoDivisa
        Dim IsoDivisas As String = String.Empty

        IsoDivisas = String.Format("'{0}'", Peticao.CodigoMoneda)

        If Not (String.IsNullOrEmpty(IsoDivisas)) Then

            'Busca a lista de IsoDivisas e IdMonedas
            dtMonedas = Negocio.Moneda.ListarIdMonedaIdps(IsoDivisas)
            dtMonedas.TableName = Negocio.Moneda.T_PD_MONEDA
            ds.Tables.Add(dtMonedas)

        End If

        Return ds

    End Function

    ''' <summary>
    ''' Preenche o Sector 
    ''' </summary>
    ''' <param name="objSaldo">Negocio.Saldo</param>
    ''' <param name="objSaldoRespuesta">RecuperarSaldos.Saldo</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
    Private Sub PreencherSector(objSaldo As Negocio.Saldo, objSaldoRespuesta As RecuperarSaldos.Saldo)

        Dim objSector As RecuperarSaldos.Sector = Nothing

        ' Busca o centro de processo
        Dim objCentroProceso As New Negocio.CentroProceso
        objCentroProceso.Id = objSaldo.IdCentroProceso
        objCentroProceso.Realizar()

        ' Verifica se já existe esse cento de processo
        If (objSaldoRespuesta.Sectores IsNot Nothing AndAlso objSaldoRespuesta.Sectores.Count > 0) Then

            objSector = objSaldoRespuesta.Sectores.Find(Function(s) s.Codigo = objCentroProceso.IdPS)

        Else

            objSaldoRespuesta.Sectores = New RecuperarSaldos.Sectores

        End If

        ' Se o centro de processo não foi preenchido
        If (objSector Is Nothing) Then

            objSector = New RecuperarSaldos.Sector
            objSector.Codigo = objCentroProceso.IdPS
            objSector.Descripcion = objCentroProceso.Descripcion

            objSaldoRespuesta.Sectores.Add(objSector)

        End If

        PreencherCliente(objSaldo, objSector)

    End Sub

    ''' <summary>
    ''' Preenche o Cliente 
    ''' </summary>
    ''' <param name="objSaldo">Negocio.Saldo</param>
    ''' <param name="objSector">RecuperarSaldos.Sector</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
    Private Sub PreencherCliente(objSaldo As Negocio.Saldo, objSector As RecuperarSaldos.Sector)

        Dim objCliente As RecuperarSaldos.Cliente = Nothing

        ' Busca o Cliente
        Dim cliente As New Negocio.Cliente
        cliente.Id = objSaldo.IdCliente
        cliente.Realizar()

        ' Verifica se já existe esse Cliente
        If (objSector.Clientes IsNot Nothing AndAlso objSector.Clientes.Count > 0) Then

            objCliente = objSector.Clientes.Find(Function(s) s.Codigo = cliente.IdPS)

        Else

            objSector.Clientes = New RecuperarSaldos.Clientes

        End If

        ' Se o Cliente não foi preenchido
        If (objCliente Is Nothing) Then

            objCliente = New RecuperarSaldos.Cliente
            objCliente.Codigo = cliente.IdPS
            objCliente.Descripcion = cliente.Descripcion

            objSector.Clientes.Add(objCliente)

        End If

        PreencherCanal(objSaldo, objCliente)

    End Sub

    ''' <summary>
    ''' Preenche o Canal 
    ''' </summary>
    ''' <param name="objSaldo">Negocio.Saldo</param>
    ''' <param name="objCliente">RecuperarSaldos.Cliente</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
    Private Sub PreencherCanal(objSaldo As Negocio.Saldo, objCliente As RecuperarSaldos.Cliente)

        Dim objCanal As RecuperarSaldos.Canal = Nothing

        ' Busca o Canal
        Dim objBanco As New Negocio.Banco
        objBanco.Id = objSaldo.IdBanco
        objBanco.Realizar()

        ' Verifica se já existe esse Canal
        If (objCliente.Canales IsNot Nothing AndAlso objCliente.Canales.Count > 0) Then

            objCanal = objCliente.Canales.Find(Function(s) s.Codigo = objBanco.IdPS)

        Else

            objCliente.Canales = New RecuperarSaldos.Canales

        End If

        ' Se o Canal não foi preenchido
        If (objCanal Is Nothing) Then

            objCanal = New RecuperarSaldos.Canal
            objCanal.Codigo = objBanco.IdPS
            objCanal.Descripcion = objBanco.Descripcion

            objCliente.Canales.Add(objCanal)

        End If

        PreencherMoneda(objSaldo, objCanal)

    End Sub

    ''' <summary>
    ''' Preenche a Moneda
    ''' </summary>
    ''' <param name="objSaldo">Negocio.Saldo</param>
    ''' <param name="objCanal">RecuperarSaldos.Canal</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
    Private Sub PreencherMoneda(objSaldo As Negocio.Saldo, objCanal As RecuperarSaldos.Canal)

        Dim objMoneda As RecuperarSaldos.Moneda = Nothing

        ' Verifica se é para recuperar saldo disponível não disponível ou ambos
        If _SoloSaldosDisponible Is Nothing OrElse _SoloSaldosDisponible = objSaldo.Disponible Then

            ' Busca a Moeda
            Dim moneda As New Negocio.Moneda
            moneda.Id = objSaldo.IdMoneda
            moneda.Realizar()

            ' Verifica se já existe essa Moneda
            If (objCanal.Monedas IsNot Nothing AndAlso objCanal.Monedas.Count > 0) Then

                objMoneda = objCanal.Monedas.Find(Function(s) s.Codigo = moneda.IsoGenesis)

                If objMoneda IsNot Nothing Then

                    objMoneda.Importe += objSaldo.Importe

                End If

            Else

                objCanal.Monedas = New RecuperarSaldos.Monedas

            End If

            ' Se a Moneda não foi preenchida
            If (objMoneda Is Nothing) Then

                objMoneda = New RecuperarSaldos.Moneda
                objMoneda.Codigo = moneda.IsoGenesis
                objMoneda.Descripcion = moneda.Descripcion
                objMoneda.Importe = objSaldo.Importe
                objMoneda.Disponible = objSaldo.Disponible

                objCanal.Monedas.Add(objMoneda)

            End If

            PreencherEspecies(objSaldo, objMoneda, objMoneda.Disponible)

        End If

    End Sub

    ''' <summary>
    ''' Preenche a Especie
    ''' </summary>
    ''' <param name="objSaldo">Negocio.Saldo</param>
    ''' <param name="objMoneda">RecuperarSaldos.Moneda</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
    Private Sub PreencherEspecies(objSaldo As Negocio.Saldo, objMoneda As RecuperarSaldos.Moneda, disponible As Boolean)

        Dim objEspecie As RecuperarSaldos.Especie = Nothing

        For Each objFarjo As Negocio.Fajo In objSaldo.Fajos

            ' Busca a Moeda
            Dim especie As New Negocio.Especie
            especie.Id = objFarjo.IdEspecie
            especie.Realizar()


            ' Verifica se já existe essa Moneda
            If (objMoneda.Especies IsNot Nothing AndAlso objMoneda.Especies.Count > 0) Then

                objEspecie = objMoneda.Especies.Find(Function(s) s.Codigo = especie.IdGenesis)

                If objEspecie IsNot Nothing Then

                    objEspecie.Cantidad += objFarjo.Cantidad
                    objEspecie.Importe += objFarjo.Importe

                End If

            Else

                objMoneda.Especies = New RecuperarSaldos.Especies

            End If

            ' Se a Moneda não foi preenchida
            If (objEspecie Is Nothing) Then

                objEspecie = New RecuperarSaldos.Especie
                objEspecie.Codigo = especie.IdGenesis
                objEspecie.Descripcion = especie.Descripcion
                objEspecie.Cantidad = objFarjo.Cantidad
                objEspecie.Importe = objFarjo.Importe

                objMoneda.Especies.Add(objEspecie)

            End If

        Next

    End Sub

#End Region

End Class
