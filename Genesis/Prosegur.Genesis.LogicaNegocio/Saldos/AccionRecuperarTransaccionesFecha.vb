Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Global.Saldos
Imports System.Data

Public Class AccionRecuperarTransaccionesFechasFecha

    Private _MuestraSaldoDesglosado As Nullable(Of Boolean) = True

    ''' <summary>
    ''' Valida los datos obligatorios de lo objecto Peticion
    ''' </summary>
    ''' <param name="Peticion">RecuperarTransaccionesFechas.Peticion</param>
    ''' <param name="Respuesta">RecuperarTransaccionesFechas.Respuesta</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
    Private Function ValidarPeticion(Peticion As RecuperarTransaccionesFechas.Peticion, ByRef Respuesta As RecuperarTransaccionesFechas.Respuesta) As Boolean

        'Objeto que recebe as mensagens
        Dim Mensagens As New System.Text.StringBuilder

        'Verifica se o usuário foi informado
        If (Peticion.Usuario IsNot Nothing AndAlso (String.IsNullOrEmpty(Peticion.Usuario.Login) _
            OrElse String.IsNullOrEmpty(Peticion.Usuario.Clave))) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(Traduzir("09_msg_dados_usuario_naoinformado"))

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
                    Mensagens.AppendLine(Traduzir("09_msg_login_bloqueado"))
                End If
            Else
                ' adiciona texto de mensagem
                Mensagens.AppendLine(Traduzir("09_msg_login_invalido"))
            End If

        End If

        'Verifica se a Fecha/Hora Desde foi informada
        If (String.IsNullOrEmpty(Peticion.FechaTransaccionDesde)) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(String.Format(Traduzir("09_msg_dados_campo_naoinformado"), Traduzir("09_msg_campo_fechahoradesde")))

        End If

        ' Verifica se a Fecha/Hora Hasta foi informada
        If (String.IsNullOrEmpty(Peticion.FechaTransaccionHasta)) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(String.Format(Traduzir("09_msg_dados_campo_naoinformado"), Traduzir("09_msg_campo_fechahorahasta")))

        End If

        ' Verifica se a Fecha/Hora Hasta é maior do que a Desde
        If (Not String.IsNullOrEmpty(Peticion.FechaTransaccionDesde) AndAlso Not String.IsNullOrEmpty(Peticion.FechaTransaccionHasta)) Then

            ' Verfica se a FechaHora Hasta é menor do que a Desde
            If Peticion.FechaTransaccionHasta < Peticion.FechaTransaccionDesde Then

                ' Adiciona texto de mensagem
                Mensagens.AppendLine(Traduzir("09_msg_dados_fecha_invalida"))

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
    ''' Gera a tabela contendo a Moneda Selecionada
    ''' Código CodigoMoneda (MiProsegur) com IdMoneda (Saldos)
    ''' </summary>
    ''' <param name="CodigoIsoDivisa">String</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
    Private Function ObterListaIdMoneda(CodigoIsoDivisa As String) As String

        Dim IdMoneda As String = String.Empty

        If Not (String.IsNullOrEmpty(CodigoIsoDivisa)) Then

            'Tabelas de ids
            Dim dtMonedas As DataTable

            'Lista de IsoDivisa
            Dim IsoDivisas As String = String.Format("'{0}'", CodigoIsoDivisa)

            'Busca a lista de IdMonedas
            dtMonedas = Negocio.Moneda.ListarIdMonedaIdps(IsoDivisas)

            ' Verifica se encontrou a Moneda
            If dtMonedas.Rows.Count > 0 Then

                ' Define o Id da Moneda
                IdMoneda = dtMonedas.Rows(0)(Negocio.Moneda.C_IDMONEDA)

            End If

        End If

        ' Retorna o Id da Moneda
        Return IdMoneda

    End Function

    ''' <summary>
    ''' Método principal responsável por obter dados das transações 
    ''' e chamar os metodos que converte para o objeto respuesta
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 16/07/2009 Criado
    ''' </history>
    Public Function Ejecutar(Peticion As RecuperarTransaccionesFechas.Peticion) As RecuperarTransaccionesFechas.Respuesta

        Dim objRespuesta As New RecuperarTransaccionesFechas.Respuesta

        Try

            ' Valida os dados da petição
            If ValidarPeticion(Peticion, objRespuesta) Then

                ' Recupera os valores dos filtros
                Dim filtros As New Hashtable

                ' Preenche parâmetros obrigatórios
                filtros.Add(Negocio.Constantes.CONST_FECHA_HORA_DESDE, Peticion.FechaTransaccionDesde)
                filtros.Add(Negocio.Constantes.CONST_FECHA_HORA_HASTA, Peticion.FechaTransaccionHasta)

                'Preenche a lista de Id Clientes
                If Not String.IsNullOrEmpty(Peticion.CodigoPlanta) Then

                    ' Busca o código da Planta
                    Dim objPlanta As New Negocio.Planta
                    objPlanta.IdPS = Peticion.CodigoPlanta
                    objPlanta.Realizar()

                    ' Preenche o código da planta
                    filtros.Add(Negocio.Constantes.CONST_COD_PLANTA, objPlanta.Id)

                End If

                ' Preenche parâmetros opcionais
                ' Se existe Código de Setor
                If Not String.IsNullOrEmpty(Peticion.CodigoSector) Then
                    ' Busca o código do Setor
                    Dim objCentrosProceso As New Negocio.CentrosProceso
                    objCentrosProceso.IdPS = Peticion.CodigoSector
                    objCentrosProceso.Realizar()
                    filtros.Add(Negocio.Constantes.CONST_COD_SECTOR, objCentrosProceso.First.Id)
                End If

                ' Preenche parâmetros opcionais
                ' Se existe Código do Cliente
                If Not String.IsNullOrEmpty(Peticion.CodigoCliente) Then
                    ' Busca o código do Cliente
                    Dim objCliente As New Negocio.Cliente
                    objCliente.IdPS = Peticion.CodigoCliente
                    objCliente.RealizarPorIdPs()
                    filtros.Add(Negocio.Constantes.CONST_COD_CLIENTE, objCliente.Id)
                End If

                ' Se existe Código de Canal
                If Not String.IsNullOrEmpty(Peticion.CodigoCanal) Then
                    ' Busca o código do Canal
                    Dim objCanales As New Negocio.Bancos
                    objCanales.IdPS = Peticion.CodigoCanal
                    objCanales.Realizar()
                    filtros.Add(Negocio.Constantes.CONST_COD_CANAL, objCanales.First.Id)
                End If

                ' Se existe Código de Moneda
                If Not String.IsNullOrEmpty(Peticion.CodigoMoneda) Then
                    ' Define o filtro da moneda
                    filtros.Add(Negocio.Constantes.CONST_COD_MONEDA, ObterListaIdMoneda(Peticion.CodigoMoneda))
                End If

                ' Define o filtro de SoloSaldoDisponible
                filtros.Add(Negocio.Constantes.CONST_SOLO_SALDO_DIPONIBLE, Peticion.SoloSaldoDisponible)

                ' Recupera os documentos
                Dim objDocumentos As New Negocio.Documentos
                'objDocumentos.RealizarTransacciones(filtros)

                '' Define se mostra saldos desglosado
                'If (Peticion.SaldoDesglosado IsNot Nothing) Then
                '    _MuestraSaldoDesglosado = Peticion.SaldoDesglosado
                'End If

                '' Preenche o objeto de resposta
                'PreencherRespuesta(objDocumentos, objRespuesta)
                objDocumentos.RealizarTransaccionesDT(filtros, objRespuesta, Peticion.SaldoDesglosado)

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
    ''' Preenche o objeto respuesta com os dados do objeto Documento
    ''' </summary>
    ''' <param name="objDocumentos">Negocio.Documentos</param>
    ''' <param name="objRespuesta">RecuperarTransaccionesFechas.Respuesta</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 13/07/2011 - Creado
    ''' </history>
    Private Sub PreencherRespuesta(objDocumentos As Negocio.Documentos, _
                                   ByRef objRespuesta As RecuperarTransaccionesFechas.Respuesta)

        ' Se existe documentos
        If (objDocumentos IsNot Nothing AndAlso objDocumentos.Count > 0) Then

            ' Instancia o objeto de transacciones
            objRespuesta.Transacciones = New RecuperarTransaccionesFechas.Transacciones

            ' Para cada documento
            For Each objDocumento As Negocio.Documento In objDocumentos

                PreencherTransacion(objDocumento, objRespuesta.Transacciones)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche a Transación
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objTransaciones">RecuperarTransaccionesFechas.Transacciones</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherTransacion(objDocumento As Negocio.Documento, ByRef objTransaciones As RecuperarTransaccionesFechas.Transacciones)

        ' Se o documento não está nulo
        If objDocumento IsNot Nothing Then

            ' Preenche a transação
            Dim objTransacion = New RecuperarTransaccionesFechas.Transaccion
            objTransacion.OidTransaccion = objDocumento.IdMovimentacionFondo
            objTransacion.FechaTransaccion = objDocumento.FechaGestion
            PreencherNumeroExterno(objDocumento, objTransacion.NumExterno, Enumeradores.eCampos.NumExterno)
            PreencherSector(objDocumento, objTransacion.SectorOrigen, Enumeradores.eCampos.CentroProcesoOrigen)
            objTransacion.NombreDocumento = IIf(objDocumento.Formulario IsNot Nothing, objDocumento.Formulario.Descripcion, String.Empty)
            objTransacion.Disponible = objDocumento.SaldoDisponible

            ' Preenche o cliente
            PreencherCliente(objDocumento, objTransacion.Cliente)

            ' Preenche a planta
            PreencherPlanta(objDocumento, objTransacion.Planta)

            ' Preenche o setor origem
            PreencherSector(objDocumento, objTransacion.SectorOrigen, Enumeradores.eCampos.CentroProcesoOrigen)

            ' Preenche o setor destino
            PreencherSector(objDocumento, objTransacion.SectorDestino, Enumeradores.eCampos.CentroProcesoDestino)

            ' Preenche o canal origem
            PreencherCanal(objDocumento, objTransacion.CanalOrigen, Enumeradores.eCampos.Banco)

            ' Preenche o canal destino
            PreencherCanal(objDocumento, objTransacion.CanalDestino, Enumeradores.eCampos.BancoDeposito)

            ' Preenche as moedas
            PreencherMonedas(objDocumento, objTransacion.Monedas)

            objTransaciones.Add(objTransacion)

        End If

    End Sub


    ''' <summary>
    ''' Preenche o Número Externo
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="NumeroExterno">String</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 12/11/2011 - Creado</history>
    Private Sub PreencherNumeroExterno(objDocumento As Negocio.Documento, ByRef NumeroExterno As String, TipoCampo As Enumeradores.eCampos)

        ' Busca o campo
        Dim objcampo As Negocio.Campo = ObterObjetoCampo(TipoCampo.ToString, objDocumento.Formulario.Campos)

        If (objcampo IsNot Nothing) Then

            ' Define o número externo
            NumeroExterno = objcampo.Valor

        End If

    End Sub

    ''' <summary>
    ''' Preenche a Planta
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objPlanta">RecuperarTransaccionesFechas.Planta</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherPlanta(objDocumento As Negocio.Documento, ByRef objPlanta As RecuperarTransaccionesFechas.Planta)

        ' Busca o campo
        Dim objcampo As Negocio.Campo = ObterObjetoCampo(Enumeradores.eCampos.CentroProcesoOrigen.ToString, objDocumento.Formulario.Campos)

        If (objcampo IsNot Nothing) Then

            ' Busca o Centro de Processo
            Dim centroProceso As New Negocio.CentroProceso
            centroProceso.Id = objcampo.IdValor
            centroProceso.Realizar()

            ' Busca a Planta
            Dim planta As New Negocio.Planta
            planta.Id = centroProceso.Planta.Id
            planta.Realizar()

            objPlanta = New RecuperarTransaccionesFechas.Planta With { _
                        .Codigo = planta.IdPS, _
                        .Descripcion = planta.Descripcion _
                        }

        End If

    End Sub

    ''' <summary>
    ''' Preenche o Centro Proceso
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objSector">RecuperarTransaccionesFechas.Sector</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherSector(objDocumento As Negocio.Documento, ByRef objSector As RecuperarTransaccionesFechas.Sector, TipoCampo As Enumeradores.eCampos)

        ' Busca o campo
        Dim objcampo As Negocio.Campo = ObterObjetoCampo(TipoCampo.ToString, objDocumento.Formulario.Campos)

        If (objcampo IsNot Nothing) Then

            ' Busca o Centro de Processo
            Dim centroProceso As New Negocio.CentroProceso
            centroProceso.Id = objcampo.IdValor
            centroProceso.Realizar()

            objSector = New RecuperarTransaccionesFechas.Sector With { _
                        .Codigo = centroProceso.IdPS, _
                        .Descripcion = centroProceso.Descripcion _
                        }

        End If

    End Sub

    ''' <summary>
    ''' Preenche o Canal
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objCanal">RecuperarTransaccionesFechas.Canal</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherCanal(objDocumento As Negocio.Documento, ByRef objCanal As RecuperarTransaccionesFechas.Canal, TipoCampo As Enumeradores.eCampos)

        ' Busca o campo
        Dim objcampo As Negocio.Campo = ObterObjetoCampo(TipoCampo.ToString, objDocumento.Formulario.Campos)

        If (objcampo IsNot Nothing) Then

            ' Busca o Canal
            Dim canal As New Negocio.Banco
            canal.Id = objcampo.IdValor
            canal.Realizar()

            objCanal = New RecuperarTransaccionesFechas.Canal With { _
                        .Codigo = canal.IdPS, _
                        .Descripcion = canal.Descripcion _
                        }

        End If

    End Sub

    ''' <summary>
    ''' Preenche a Planta
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objCliente">RecuperarTransaccionesFechas.Cliente</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherCliente(objDocumento As Negocio.Documento, ByRef objCliente As RecuperarTransaccionesFechas.Cliente)

        ' Busca o campo
        Dim objcampo As Negocio.Campo = ObterObjetoCampo(Enumeradores.eCampos.ClienteOrigen.ToString, objDocumento.Formulario.Campos)

        If (objcampo IsNot Nothing) Then

            ' Busca o Cliente
            Dim cliente As New Negocio.Cliente
            cliente.Id = objcampo.IdValor
            cliente.Realizar()

            objCliente = New RecuperarTransaccionesFechas.Cliente
            objCliente.Codigo = cliente.IdPS
            objCliente.Descripcion = cliente.Descripcion

        End If

    End Sub

    ''' <summary>
    ''' Preenche a Planta
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objCamposExtras">GuardarDatosDocumento.CamposExtras</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherCamposExtras(objDocumento As Negocio.Documento, ByRef objCamposExtras As GuardarDatosDocumento.CamposExtras)

        ' Verifica se existe campos extras
        If (objDocumento.Formulario.CamposExtra IsNot Nothing AndAlso objDocumento.Formulario.CamposExtra.Count > 0) Then

            ' Inicializa a coleção
            objCamposExtras = New GuardarDatosDocumento.CamposExtras

            ' Para cada campo extra, realiza a conversão
            For Each campoExtra As Negocio.CampoExtra In objDocumento.Formulario.CamposExtra

                objCamposExtras.Add(New GuardarDatosDocumento.CampoExtra With { _
                            .Nombre = campoExtra.Nombre, _
                            .Valor = campoExtra.Valor _
                            })
            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche as Monedas
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objMonedas">RecuperarTransaccionesFechas.Monedas</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
    Private Sub PreencherMonedas(objDocumento As Negocio.Documento, ByRef objMonedas As RecuperarTransaccionesFechas.Monedas)

        ' Verifica se existe detalhes
        If (objDocumento.Detalles IsNot Nothing AndAlso objDocumento.Detalles.Count > 0) Then

            ' Intancia o objeto de moedas
            objMonedas = New RecuperarTransaccionesFechas.Monedas

            ' Para cada detalle existe
            For Each detalle As Negocio.Detalle In objDocumento.Detalles

                ' Preenche a moeda
                PreencherMoneda(detalle, objMonedas)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche a Moneda
    ''' </summary>
    ''' <param name="objDetalle">Negocio.Detalle</param>
    ''' <param name="objMonedas">RecuperarTransaccionesFechas.Monedas</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherMoneda(objDetalle As Negocio.Detalle, ByRef objMonedas As RecuperarTransaccionesFechas.Monedas)

        Dim objMoneda As RecuperarTransaccionesFechas.Moneda = Nothing

        ' Recupera dados da moeda
        objDetalle.Especie.Moneda.Realizar()

        ' Verifica se já existe essa Moneda
        If (objMonedas IsNot Nothing AndAlso objMonedas.Count > 0) Then

            objMoneda = objMonedas.Find(Function(s) s.Codigo = objDetalle.Especie.Moneda.IsoGenesis)

            If objMoneda IsNot Nothing Then

                objMoneda.Importe += objDetalle.Importe

                ' Se Mostra saldo desglosado
                If _MuestraSaldoDesglosado Then

                    ' Preenche a moneda
                    PreencherEspecie(objDetalle, objMoneda)

                End If

            End If

        End If

        ' Se a Moneda não foi preenchida
        If (objMoneda Is Nothing) Then

            objMoneda = New RecuperarTransaccionesFechas.Moneda
            objMoneda.Codigo = objDetalle.Especie.Moneda.IsoGenesis
            objMoneda.Descripcion = objDetalle.Especie.Moneda.Descripcion
            objMoneda.Importe = objDetalle.Importe
            objMonedas.Add(objMoneda)

            ' Se Mostra saldo desglosado
            If _MuestraSaldoDesglosado Then

                ' Preenche a moneda
                PreencherEspecie(objDetalle, objMoneda)

            End If

        End If

    End Sub


    ''' <summary>
    ''' Preenche a Especie
    ''' </summary>
    ''' <param name="objDetalle">Negocio.Detalle</param>
    ''' <param name="objMoneda">ContractoServicio.RecuperarTransaccionDetallada.Moneda</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherEspecie(objDetalle As Negocio.Detalle, ByRef objMoneda As RecuperarTransaccionesFechas.Moneda)

        Dim objEspecie As RecuperarTransaccionesFechas.Especie = Nothing

        ' Recupera dados da especie
        objDetalle.Especie.Realizar()

        ' Verifica se já existe essa Moneda
        If (objMoneda.Especies IsNot Nothing AndAlso objMoneda.Especies.Count > 0) Then


            objEspecie = objMoneda.Especies.Find(Function(s) s.Codigo = objDetalle.Especie.IdGenesis)

            If objEspecie IsNot Nothing Then

                objEspecie.Cantidad += objDetalle.Cantidad
                objEspecie.Importe += objDetalle.Importe

            End If

        Else

            objMoneda.Especies = New RecuperarTransaccionesFechas.Especies

        End If

        ' Se a Moneda não foi preenchida
        If (objEspecie Is Nothing) Then

            objEspecie = New RecuperarTransaccionesFechas.Especie
            objEspecie.Codigo = objDetalle.Especie.IdGenesis
            objEspecie.Descripcion = objDetalle.Especie.Descripcion
            objEspecie.Cantidad = objDetalle.Cantidad
            objEspecie.Importe = objDetalle.Importe

            objMoneda.Especies.Add(objEspecie)

        End If

    End Sub

    ''' <summary>
    ''' Obter o objeto campo
    ''' </summary>
    ''' <param name="TipoCampo">String</param>
    ''' <returns>Negocio.Campo</returns>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Function ObterObjetoCampo(TipoCampo As String, Campos As Negocio.Campos) As Negocio.Campo

        ' Retorna o campo
        Return (From c In Campos _
                Where c.Nombre = TipoCampo).FirstOrDefault

    End Function

End Class