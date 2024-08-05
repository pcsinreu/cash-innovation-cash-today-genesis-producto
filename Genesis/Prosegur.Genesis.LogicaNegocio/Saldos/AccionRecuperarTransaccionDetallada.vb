Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Global.Saldos

Public Class AccionRecuperarTransaccionDetallada

    ''' <summary>
    ''' Valida los datos obligatorios de lo objecto Peticion
    ''' </summary>
    ''' <param name="Peticion">ContractoServicio.RecuperarTransaccionDetallada.Peticion</param>
    ''' <param name="Respuesta">ContractoServicio.RecuperarTransaccionDetallada.Respuesta</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Function ValidarPeticion(Peticion As RecuperarTransaccionDetallada.Peticion, ByRef Respuesta As RecuperarTransaccionDetallada.Respuesta) As Boolean

        'Objeto que recebe as mensagens
        Dim Mensagens As New System.Text.StringBuilder

        'Verifica se o usuário foi informado
        If (Peticion.Usuario IsNot Nothing AndAlso (String.IsNullOrEmpty(Peticion.Usuario.Login) _
            OrElse String.IsNullOrEmpty(Peticion.Usuario.Clave))) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(Traduzir("10_msg_dados_usuario_naoinformado"))

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
                    Mensagens.AppendLine(Traduzir("10_msg_login_bloqueado"))
                End If
            Else
                ' adiciona texto de mensagem
                Mensagens.AppendLine(Traduzir("10_msg_login_invalido"))
            End If

        End If

        'Verifica se o Identificador da transação foi informado
        If (String.IsNullOrEmpty(Peticion.OidTransaccion)) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(String.Format(Traduzir("10_msg_dados_campo_naoinformado"), Traduzir("10_msg_campo_codigoplanta")))

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
    ''' Método principal responsável por obter dados das transações 
    ''' e chamar os metodos que converte para o objeto respuesta
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 13/07/2011 - Creado
    ''' </history>
    Public Function Ejecutar(Peticion As RecuperarTransaccionDetallada.Peticion) As RecuperarTransaccionDetallada.Respuesta

        Dim objRespuesta As New RecuperarTransaccionDetallada.Respuesta

        Try

            If ValidarPeticion(Peticion, objRespuesta) Then

                ' Verifica se já existe documento para a transação corrente...
                Dim objDocumento As New Negocio.Documento
                objDocumento.IdMovimentacionFondo = Peticion.OidTransaccion
                objDocumento.RealizarIdMovimentacionFondo()

                ' Preenche o retorno
                PreencherTransacion(objDocumento, objRespuesta.Transaccion)

            End If

            'Caso não ocorra exceção, retorna o objrespuesta com codigo 0 e mensagem erro vazio
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Preenche a Transación
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objTransacion">ContractoServicio.RecuperarTransaccionDetallada.Transaccion</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherTransacion(objDocumento As Negocio.Documento, ByRef objTransacion As RecuperarTransaccionDetallada.Transaccion)

        ' Se o documento não está nulo
        If objDocumento IsNot Nothing Then

            ' Busca o centro de processo
            objTransacion = New RecuperarTransaccionDetallada.Transaccion
            objTransacion.OidTransaccion = objDocumento.IdMovimentacionFondo
            objTransacion.FechaTransaccion = objDocumento.FechaGestion


            objTransacion.NombreDocumento = IIf(objDocumento.Formulario IsNot Nothing, objDocumento.Formulario.Descripcion, String.Empty)

            ' Preenche o número externo
            Dim objCampo As Negocio.Campo = ObterObjetoCampo(Enumeradores.eCampos.NumExterno.ToString, objDocumento.Formulario.Campos)
            If objCampo IsNot Nothing Then
                objTransacion.NumeroExterno = objCampo.Valor
            End If

            ' Preenche o cliente
            PreencherCliente(objDocumento, objTransacion.Cliente)

            ' Preenche a planta
            PreencherPlanta(objDocumento, objTransacion.Planta)

            ' Preenche o setor origem
            PreencherSector(objDocumento, objTransacion.SectorOrigen, Enumeradores.eCampos.CentroProcesoOrigen)

            ' Preenche o setor destino
            PreencherSector(objDocumento, objTransacion.SectorOrigen, Enumeradores.eCampos.CentroProcesoDestino)

            ' Preenche o canal origem
            PreencherCanal(objDocumento, objTransacion.CanalOrigen, Enumeradores.eCampos.Banco)

            ' Preenche o canal destino
            PreencherCanal(objDocumento, objTransacion.CanalDestino, Enumeradores.eCampos.BancoDeposito)

            ' Preenche campos extras
            PreencherCamposExtras(objDocumento, objTransacion.CamposExtras)

            ' Preenche as moedas
            PreencherMonedas(objDocumento, objTransacion.Monedas)

        End If

    End Sub

    ''' <summary>
    ''' Preenche a Planta
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objPlanta">ContractoServicio.RecuperarTransaccionDetallada.Planta</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherPlanta(objDocumento As Negocio.Documento, ByRef objPlanta As RecuperarTransaccionDetallada.Planta)

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

            objPlanta = New RecuperarTransaccionDetallada.Planta With { _
                        .Codigo = planta.IdPS, _
                        .Descripcion = planta.Descripcion _
                        }

        End If

    End Sub

    ''' <summary>
    ''' Preenche o Centro Proceso
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objSector">ContractoServicio.RecuperarTransaccionDetallada.Sector</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherSector(objDocumento As Negocio.Documento, ByRef objSector As RecuperarTransaccionDetallada.Sector, TipoCampo As Enumeradores.eCampos)

        ' Busca o campo
        Dim objcampo As Negocio.Campo = ObterObjetoCampo(TipoCampo.ToString, objDocumento.Formulario.Campos)

        If (objcampo IsNot Nothing) Then

            ' Busca o Centro de Processo
            Dim centroProceso As New Negocio.CentroProceso
            centroProceso.Id = objcampo.IdValor
            centroProceso.Realizar()

            objSector = New RecuperarTransaccionDetallada.Sector With { _
                        .Codigo = centroProceso.IdPS, _
                        .Descripcion = centroProceso.Descripcion _
                        }

        End If

    End Sub

    ''' <summary>
    ''' Preenche o Canal
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objCanal">ContractoServicio.RecuperarTransaccionDetallada.Canal</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherCanal(objDocumento As Negocio.Documento, ByRef objCanal As RecuperarTransaccionDetallada.Canal, TipoCampo As Enumeradores.eCampos)

        ' Busca o campo
        Dim objcampo As Negocio.Campo = ObterObjetoCampo(TipoCampo.ToString, objDocumento.Formulario.Campos)

        If (objcampo IsNot Nothing) Then

            ' Busca o Canal
            Dim canal As New Negocio.Banco
            canal.Id = objcampo.IdValor
            canal.Realizar()

            objCanal = New RecuperarTransaccionDetallada.Canal With { _
                        .Codigo = canal.IdPS, _
                        .Descripcion = canal.Descripcion _
                        }

        End If

    End Sub

    ''' <summary>
    ''' Preenche a Planta
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objCliente">ContractoServicio.RecuperarTransaccionDetallada.Cliente</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherCliente(objDocumento As Negocio.Documento, ByRef objCliente As RecuperarTransaccionDetallada.Cliente)

        ' Busca o campo
        Dim objcampo As Negocio.Campo = ObterObjetoCampo(Enumeradores.eCampos.ClienteOrigen.ToString, objDocumento.Formulario.Campos)

        If (objcampo IsNot Nothing) Then

            ' Busca o Cliente
            Dim cliente As New Negocio.Cliente
            cliente.Id = objcampo.IdValor
            cliente.Realizar()

            objCliente = New RecuperarTransaccionDetallada.Cliente
            objCliente.Codigo = cliente.IdPS
            objCliente.Descripcion = cliente.Descripcion

        End If

    End Sub

    ''' <summary>
    ''' Preenche a Planta
    ''' </summary>
    ''' <param name="objDocumento">Negocio.Documento</param>
    ''' <param name="objCamposExtras">ContractoServicio.GuardarDatosDocumento.CamposExtras</param>
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
    ''' <param name="objMonedas">ContractoServicio.RecuperarTransaccionDetallada.Monedas</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
    Private Sub PreencherMonedas(objDocumento As Negocio.Documento, ByRef objMonedas As RecuperarTransaccionDetallada.Monedas)

        ' Verifica se existe detalhes
        If (objDocumento.Detalles IsNot Nothing AndAlso objDocumento.Detalles.Count > 0) Then

            ' Intancia o objeto de moedas
            objMonedas = New RecuperarTransaccionDetallada.Monedas

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
    ''' <param name="objMonedas">RecuperarTransaccionDetallada.Monedas</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherMoneda(objDetalle As Negocio.Detalle, ByRef objMonedas As RecuperarTransaccionDetallada.Monedas)

        Dim objMoneda As RecuperarTransaccionDetallada.Moneda = Nothing

        ' Recupera dados da moeda
        objDetalle.Especie.Moneda.Realizar()

        ' Verifica se já existe essa Moneda
        If (objMonedas IsNot Nothing AndAlso objMonedas.Count > 0) Then

            objMoneda = objMonedas.Find(Function(s) s.Codigo = objDetalle.Especie.Moneda.IsoGenesis)

            If objMoneda IsNot Nothing Then

                objMoneda.Importe += objDetalle.Importe

                ' Preenche a moneda
                PreencherEspecie(objDetalle, objMoneda)

            End If

        End If

        ' Se a Moneda não foi preenchida
        If (objMoneda Is Nothing) Then

            objMoneda = New RecuperarTransaccionDetallada.Moneda
            objMoneda.Codigo = objDetalle.Especie.Moneda.IsoGenesis
            objMoneda.Descripcion = objDetalle.Especie.Moneda.Descripcion
            objMoneda.Importe = objDetalle.Importe
            objMonedas.Add(objMoneda)

            ' Preenche a moneda
            PreencherEspecie(objDetalle, objMoneda)

        End If

    End Sub

    ''' <summary>
    ''' Preenche a Especie
    ''' </summary>
    ''' <param name="objDetalle">Negocio.Detalle</param>
    ''' <param name="objMoneda">ContractoServicio.RecuperarTransaccionDetallada.Moneda</param>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
    Private Sub PreencherEspecie(objDetalle As Negocio.Detalle, ByRef objMoneda As RecuperarTransaccionDetallada.Moneda)

        Dim objEspecie As RecuperarTransaccionDetallada.Especie = Nothing

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

            objMoneda.Especies = New RecuperarTransaccionDetallada.Especies

        End If

        ' Se a Moneda não foi preenchida
        If (objEspecie Is Nothing) Then

            objEspecie = New RecuperarTransaccionDetallada.Especie
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
