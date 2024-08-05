Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration
Imports System.Data
Imports Prosegur.Global.Saldos
Imports Prosegur.Global.Saldos.ContractoServicio

Public Class AccionCrearMovimiento

#Region "[Crear Movimiento]"

    ''' <summary>
    ''' Valida los datos obrigatorios del objecto Peticion
    ''' </summary>
    ''' <param name="Peticion">CrearMovimiento.Peticion</param>
    ''' <param name="Respuesta">CrearMovimiento.Respuesta</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
    Private Function ValidarPeticion(Peticion As CrearMovimiento.Peticion, ByRef Respuesta As CrearMovimiento.Respuesta) As Boolean

        'Objeto que recebe as mensagens
        Dim Mensagens As New System.Text.StringBuilder

        'Verifica se o usuário foi informado
        If (Peticion.Usuario IsNot Nothing AndAlso (String.IsNullOrEmpty(Peticion.Usuario.Login) _
            OrElse String.IsNullOrEmpty(Peticion.Usuario.Clave))) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(Traduzir("07_msg_dados_usuario_naoinformado"))

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
                    Mensagens.AppendLine(Traduzir("07_msg_login_bloqueado"))
                End If
            Else
                ' adiciona texto de mensagem
                Mensagens.AppendLine(Traduzir("07_msg_login_invalido"))
            End If

        End If

        'Valida a ação do movimiento
        If (Peticion.Movimiento IsNot Nothing AndAlso String.IsNullOrEmpty(Peticion.Movimiento.Accion)) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(String.Format(Traduzir("07_msg_dados_campo_naoinformado"), Traduzir("07_msg_campo_accion")))

        End If

        'Valida o identificador do documento
        If (Peticion.Movimiento IsNot Nothing AndAlso String.IsNullOrEmpty(Peticion.Movimiento.CodigoTipoDocumento)) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(String.Format(Traduzir("07_msg_dados_campo_naoinformado"), Traduzir("07_msg_campo_codigotipodocumento")))

        Else

            Dim objFormulario As New Negocio.Formulario
            objFormulario.Id = Peticion.Movimiento.CodigoTipoDocumento
            objFormulario.Realizar()

            If String.IsNullOrEmpty(objFormulario.Descripcion) Then

                ' Adiciona texto de mensagem
                Mensagens.AppendLine(Traduzir("07_msg_tipodocumento_invalido"))

            End If

        End If

        'Verifica se o Código da Planta foi informado
        If (Peticion.Movimiento IsNot Nothing AndAlso String.IsNullOrEmpty(Peticion.Movimiento.CodigoPlanta)) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(String.Format(Traduzir("07_msg_dados_campo_naoinformado"), Traduzir("07_msg_campo_codigoplanta")))

        End If

        'Verifica se o Código do Cliente foi informado
        If (Peticion.Movimiento IsNot Nothing AndAlso String.IsNullOrEmpty(Peticion.Movimiento.CodigoClienteOrigen)) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(String.Format(Traduzir("07_msg_dados_campo_naoinformado"), Traduzir("07_msg_campo_codigocliente")))

        End If

        'Verifica se o Código de Centro de Processo Origem foi informado
        If (Peticion.Movimiento IsNot Nothing AndAlso String.IsNullOrEmpty(Peticion.Movimiento.CodigoSectorOrigen)) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(String.Format(Traduzir("07_msg_dados_campo_naoinformado"), Traduzir("07_msg_campo_codigocentroprocesoorigem")))

        End If

        'Verifica se o Código Canal Origem foi informado
        If (Peticion.Movimiento IsNot Nothing AndAlso String.IsNullOrEmpty(Peticion.Movimiento.CodigoCanalOrigen)) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(String.Format(Traduzir("07_msg_dados_campo_naoinformado"), Traduzir("07_msg_campo_codigocanalorigen")))

        End If

        'Verifica se o Desglose foi informado
        If (Peticion.Movimiento IsNot Nothing AndAlso Peticion.Movimiento.Desgloses Is Nothing OrElse Peticion.Movimiento.Desgloses.Count = 0) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(Traduzir("07_msg_campo_desglose"))

        End If

        'Verifica se o centro de processo origem e destino existem
        If (Peticion.Movimiento IsNot Nothing AndAlso Not String.IsNullOrEmpty(Peticion.Movimiento.CodigoSectorOrigen)) Then

            Dim CentrosProcesoDestinoPosibles As New Negocio.CentrosProceso
            Dim Formulario As New Negocio.Formulario
            Dim CentroProcesoOrigen As New Negocio.CentrosProceso
            CentroProcesoOrigen.IdPS = Peticion.Movimiento.CodigoPlanta & Peticion.Movimiento.CodigoSectorOrigen
            CentroProcesoOrigen.Realizar()

            If CentroProcesoOrigen.Count > 0 Then

                Dim idCaracteristica As Integer = Peticion.Movimiento.CodigoTipoDocumento

                If Not String.IsNullOrEmpty(Peticion.Movimiento.CodigoSectorDestino) AndAlso idCaracteristica <> 0 Then

                    Formulario.Id = idCaracteristica
                    Formulario.Realizar()
                    CentrosProcesoDestinoPosibles = New Negocio.CentrosProceso ' CentrosProceso
                    CentrosProcesoDestinoPosibles.CentroProcesoActual.Id = CentroProcesoOrigen.FirstOrDefault.Id
                    CentrosProcesoDestinoPosibles.DistinguirPorNivel = Formulario.DistinguirPorNivel
                    CentrosProcesoDestinoPosibles.Matrices = Formulario.Matrices
                    CentrosProcesoDestinoPosibles.Interplantas = Formulario.Interplantas
                    CentrosProcesoDestinoPosibles.TiposCentroProceso = Formulario.TiposCentroProcesoDestino
                    CentrosProcesoDestinoPosibles.Realizar()

                    Dim existe = From cp As Negocio.CentroProceso In CentrosProcesoDestinoPosibles _
                             Where cp.IdPS = Peticion.Movimiento.CodigoPlanta & Peticion.Movimiento.CodigoSectorDestino

                    If existe Is Nothing OrElse existe.Count = 0 Then
                        ' Adiciona texto de mensagem
                        Mensagens.AppendLine(Traduzir("07_srv_msg_centroprocessodestinonoencontrado"))
                    End If

                End If

            Else
                Mensagens.AppendLine(Traduzir("07_srv_msg_centroprocessoorigennoencontrado"))
            End If

        End If

        'Verifica se há mensagem de informação inválida
        If (Mensagens.Length > 0) Then

            'Preenche o objeto de resposta
            Respuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            Respuesta.MensajeError = Mensagens.ToString

            'Retorna falso
            Return False
        Else

            'Retorna verdadeiro
            Return True
        End If

    End Function

    ''' <summary>
    ''' Es la operación que crea el documento del movimiento en el sistema de Saldos de acuerdo con los 
    ''' parámetros enviados en el mensaje de entrada.
    ''' </summary>
    ''' <param name="Peticion">CrearMovimiento.Peticion</param>
    ''' <returns>CrearMovimiento.Respuesta</returns>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
    Public Function Ejecutar(Peticion As CrearMovimiento.Peticion) As CrearMovimiento.Respuesta

        ' Cria o objeto de resposta
        Dim Respuesta As New CrearMovimiento.Respuesta

        Try

            'Verifica os dados obrigatórios comum ao objeto peticion
            If (ValidarPeticion(Peticion, Respuesta)) Then

                ' Verifica se já existe documento para a transação corrente...
                Dim objDocumento As New Negocio.Documento
                objDocumento.IdMovimentacionFondo = Peticion.Movimiento.OidTransaccion
                objDocumento.RealizarIdMovimentacionFondo()

                ' Valida se o estado do documento permite alteração...
                If objDocumento.EstadoComprobante IsNot Nothing AndAlso Not String.IsNullOrEmpty(objDocumento.EstadoComprobante.Codigo) AndAlso
                    ((objDocumento.EstadoComprobante.Codigo.Equals(Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_IMPRESO) AndAlso Peticion.Movimiento.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearImprimir) OrElse
                     objDocumento.EstadoComprobante.Codigo.Equals(Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_ACEPTADO) OrElse
                     objDocumento.EstadoComprobante.Codigo.Equals(Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_RECHAZADO)) Then

                    Respuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                    Respuesta.MensajeError = If(objDocumento.EstadoComprobante.Codigo.Equals(Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_ACEPTADO), Traduzir("07_msg_documento_aceptado"),
                                             If(objDocumento.EstadoComprobante.Codigo.Equals(Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_IMPRESO), Traduzir("07_msg_documento_impreso"), Traduzir("07_msg_documento_rechazado")))
                    Return Respuesta

                End If

                'Obtem a lista de divisas e denominação para evitar acessar o banco várias vezes
                Dim dsDenominacionDivisas As DataSet = ObterListaIdMonedaEspecie(Peticion.Movimiento)

                'Cria objeto peticion GuardarDatosDocumento
                Dim GuardarDatosPeticion As New GuardarDatosDocumento.Peticion

                If objDocumento.EstadoComprobante IsNot Nothing AndAlso Not String.IsNullOrEmpty(objDocumento.EstadoComprobante.Codigo) AndAlso
                    objDocumento.EstadoComprobante.Codigo.Equals(Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_EN_PROCESO) Then
                    'Fornece o tipo da Ação
                    GuardarDatosPeticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Actualizar
                Else
                    'Fornece o tipo da Ação
                    GuardarDatosPeticion.Accion = If(Peticion.Movimiento.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearImprimir, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Crear, Peticion.Movimiento.Accion)
                End If

                'Adiciona informação do usuário
                GuardarDatosPeticion.Usuario.Login = Peticion.Usuario.Login
                GuardarDatosPeticion.Usuario.Clave = Peticion.Usuario.Clave

                'Verifica se é para 'Rechazar'
                If Peticion.Movimiento.Accion <> Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Rechazar Then

                    'Instancia el objeto de respuesta
                    Dim Resposta As GuardarDatosDocumento.Respuesta

                    'Cria objeto documento                    
                    GuardarDatosPeticion.Documento = MontarObjetoDocumento(Peticion.Movimiento, dsDenominacionDivisas, objDocumento.Id)

                    'Chama o método GuardarDatosDocumento
                    Dim AcaoGuardarDadosDocumeto As New AccionGuardarDatosDocumento

                    'Executa a ação de guardar os dados
                    Resposta = AcaoGuardarDadosDocumeto.Ejecutar(GuardarDatosPeticion)

                    ' Verifica se o documento foi criado
                    If Resposta.IdDocumento <> 0 AndAlso Peticion.Movimiento.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearImprimir Then

                        ' Define o identificador do documento
                        Dim documento As New Negocio.Documento()
                        documento.Id = Resposta.IdDocumento
                        documento.Realizar()

                        Dim cp As New Negocio.CentrosProceso
                        cp.IdPS = Peticion.Movimiento.CodigoPlanta & Peticion.Movimiento.CodigoSectorOrigen
                        cp.Realizar()
                        If cp.Count > 0 Then
                            documento.GMTVeranoAjuste = If(Not String.IsNullOrEmpty(cp(0).Planta.CodDelegacionGenesis), CType(Util.GetGMTVeranoAjuste(cp(0).Planta.CodDelegacionGenesis), Short?), Nothing)
                        End If


                        ' Imprime o documento
                        documento.Imprimir()

                    End If

                    'Verifica se houve erro ao guardar os dados
                    If (Resposta.CodigoError <> Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT) Then

                        'Informa o erro retornado
                        Respuesta.CodigoError = Resposta.CodigoError
                        Respuesta.MensajeError = Resposta.MensajeError

                    Else

                        'Informa sucesso da operação
                        Respuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                        Respuesta.MensajeError = String.Empty

                        ' Atribui o código de movimento a reposta
                        Respuesta.OidTransaccion = Peticion.Movimiento.OidTransaccion

                    End If

                Else

                    Dim cp As New Negocio.CentrosProceso
                    cp.IdPS = Peticion.Movimiento.CodigoPlanta & Peticion.Movimiento.CodigoSectorOrigen
                    cp.Realizar()
                    If cp.Count > 0 Then
                        objDocumento.GMTVeranoAjuste = If(Not String.IsNullOrEmpty(cp(0).Planta.CodDelegacionGenesis), CType(Util.GetGMTVeranoAjuste(cp(0).Planta.CodDelegacionGenesis), Short?), Nothing)
                    End If

                    ' Define o usuario de resolução
                    objDocumento.UsuarioResolutor.Id = Negocio.Usuario.RecuperarIdUsuario(Peticion.Usuario.Login)
                    ' Rechaza o documento
                    objDocumento.Rechazar()

                End If

            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Trata o erro inesperado corretamente
            If ex.Message = Traduzir("07_srv_msg_divisanoencontrada") Then
                Respuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            Else
                Respuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            End If
            Respuesta.MensajeError = ex.Message

        End Try

        Return Respuesta

    End Function

    Private Sub CrearDocumento(objDocumento As GuardarDatosDocumento.Documento)



    End Sub

    Private Sub RechazarDocumento()

    End Sub

    ''' <summary>
    ''' Cria e Preenche o objeto documento
    ''' </summary>
    ''' <param name="Movimiento">CrearMovimiento.Movimiento</param>
    ''' <returns>GuardarDatosDocumento.Documento</returns>
    ''' <param name="dsDenominacionDivisas">DataSet</param> 
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
    Private Function MontarObjetoDocumento(Movimiento As CrearMovimiento.Movimiento, _
                                           dsDenominacionDivisas As DataSet, _
                                           IdDocumento As Integer) _
                                           As GuardarDatosDocumento.Documento

        'Cria o objeto documento
        Dim Documento As New GuardarDatosDocumento.Documento

        'Adiciona informações do documento
        Dim planta As New Negocio.Planta With {.IdPS = Movimiento.CodigoPlanta.Trim()}
        planta.Realizar()

        Documento.FechaGestion = If(Movimiento.FechaTransaccion.HasValue, _
                                     Movimiento.FechaTransaccion.Value, _
                                     Util.GetDateTime(planta.CodDelegacionGenesis))


        'Documento.FechaGestion = Date.Now
        Documento.IdCaracteristica = Movimiento.CodigoTipoDocumento
        Documento.IdMovimentacionFondo = Movimiento.OidTransaccion

        'Se o documento estiver em processo informa o id do documento para atualiza - lo.
        If Not String.IsNullOrEmpty(IdDocumento) Then
            Documento.IdDocumento = IdDocumento
        End If

        'Adiciona Campos 
        Documento.Campos.Add(MontarObjetoCampo(Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.ClienteOrigen, Movimiento.CodigoClienteOrigen))
        Documento.Campos.Add(MontarObjetoCampo(Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.Banco, Movimiento.CodigoCanalOrigen))
        Documento.Campos.Add(MontarObjetoCampo(Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoOrigen, Movimiento.CodigoPlanta & Movimiento.CodigoSectorOrigen))

        If Not String.IsNullOrEmpty(Movimiento.CodigoClienteDestino) Then
            Documento.Campos.Add(MontarObjetoCampo(Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.ClienteDestino, Movimiento.CodigoClienteDestino))
        End If

        If Not String.IsNullOrEmpty(Movimiento.CodigoCanalDestino) Then
            Documento.Campos.Add(MontarObjetoCampo(Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.BancoDeposito, Movimiento.CodigoCanalDestino))
        End If

        If Not String.IsNullOrEmpty(Movimiento.CodigoSectorDestino) Then
            Documento.Campos.Add(MontarObjetoCampo(Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoDestino, Movimiento.CodigoPlanta & Movimiento.CodigoSectorDestino))
        End If

        If Not String.IsNullOrEmpty(Movimiento.NumeroExterno) Then
            Documento.Campos.Add(MontarObjetoCampo(Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.NumExterno, Movimiento.NumeroExterno))
        End If


        'Adiciona Detalhes (efietivo em salidas) existentes na coleção de Detalhes do objteto documento
        For Each objEspecie As CrearMovimiento.Desglose In Movimiento.Desgloses

            If objEspecie IsNot Nothing Then

                'Guarda os valores informados para o documento
                Dim Detalle As New GuardarDatosDocumento.Detalle

                'Obtem os identificadores das tabelas contidas no dataset
                Dim IdMoneda = From IdentificadorMoneda As DataRow In dsDenominacionDivisas.Tables(Negocio.Moneda.T_PD_MONEDA).Rows _
                               Where IdentificadorMoneda(Negocio.Moneda.C_ISOGENESIS) = objEspecie.CodigoMoneda _
                               Select CInt(IdentificadorMoneda(Negocio.Moneda.C_IDMONEDA))

                Dim IdEspecie = From IdentificadorEspecie As DataRow In dsDenominacionDivisas.Tables(Negocio.Especie.T_PD_ESPECIE).Rows _
                             Where IdentificadorEspecie(Negocio.Especie.C_IDGENESIS) = objEspecie.CodigoEspecie _
                             Select CInt(IdentificadorEspecie(Negocio.Especie.C_IDESPECIE))

                If (IdEspecie.Count = 0 OrElse IdMoneda.Count = 0) Then
                    'Informa um erro por não encontrar a moeda.
                    Throw New Excepcion.NegocioExcepcion(Traduzir("07_srv_msg_divisanoencontrada"))
                End If

                Detalle.IdEspecie = IdEspecie.First
                Detalle.IdMoneda = IdMoneda.First
                Detalle.Cantidad = objEspecie.Cantidad
                Detalle.Importe = objEspecie.Importe

                'Adiciona o novo Detalle à coleção
                Documento.Detalles.Add(Detalle)

            End If

        Next

        ' Adiciona os campos extras
        If Movimiento.CamposExtras IsNot Nothing AndAlso Movimiento.CamposExtras.Count > 0 Then
            ' Adiciona os campos extras
            Documento.CamposExtras = Movimiento.CamposExtras
        End If

        'Retorna o objeto preenchido
        Return Documento

    End Function

    ''' <summary>
    ''' Cria e Preenche o objeto campo
    ''' </summary>
    ''' <param name="TipoCampo">Enumeradores.eCampos</param>
    ''' <param name="Valor">String</param>
    ''' <returns>GuardarDatosDocumento.Campo</returns>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
    Private Function MontarObjetoCampo(TipoCampo As Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos, Valor As String) As GuardarDatosDocumento.Campo

        'Cria o objeto documento
        Dim Campo As New GuardarDatosDocumento.Campo

        'Adiciona informações do campo
        Campo.Nombre = TipoCampo
        Campo.Valor = Valor

        Return Campo

    End Function

    ''' <summary>
    ''' Gera duas tabelas contendo os códigos de IsoDivisa e Denominação e seus valores relacionando os campos 
    ''' Código CodigoMoneda (MiAgencia) com IdMoneda (Saldos) e CodigoEspecie (MiAgencia) com IdEspecie (Saldos)
    ''' </summary>
    ''' <param name="Movimiento">CrearMovimiento.Movimiento</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
    Private Function ObterListaIdMonedaEspecie(Movimiento As CrearMovimiento.Movimiento) As DataSet

        'DataSet que recebera as duas tabelas
        Dim ds As New DataSet

        'Tabelas de ids
        Dim dtMonedas As DataTable
        Dim dtEspecie As DataTable

        'Lista de IsoDivisa
        Dim IsoDivisas As String = String.Empty
        Dim CodigoDenominacion As String = String.Empty

        IsoDivisas = Join(Movimiento.Desgloses.Select(Function(e) String.Format("'{0}'", e.CodigoMoneda)).Distinct().ToArray, ",")

        CodigoDenominacion = Join(Movimiento.Desgloses.Select(Function(d) String.Format("'{0}'", d.CodigoEspecie)).ToArray, ",")

        If Not (String.IsNullOrEmpty(IsoDivisas)) Then
            'Busca a lista de IsoDivisas e IdMonedas
            dtMonedas = Negocio.Moneda.ListarIdMonedaIdps(IsoDivisas)
            dtMonedas.TableName = Negocio.Moneda.T_PD_MONEDA
            ds.Tables.Add(dtMonedas)
        End If

        If Not (String.IsNullOrEmpty(CodigoDenominacion)) Then
            'Busca a lista de CodigoDenominacion e IdEspecie
            dtEspecie = Negocio.Especie.ListarIdEspecieIdGenesis(CodigoDenominacion)
            dtEspecie.TableName = Negocio.Especie.T_PD_ESPECIE
            ds.Tables.Add(dtEspecie)
        End If

        Return ds

    End Function

#End Region

End Class