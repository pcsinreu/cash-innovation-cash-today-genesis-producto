Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones.EnumExtension
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente
Imports Prosegur.Genesis.Mail

Public Class ucDatosBanc
    Inherits System.Web.UI.UserControl

    Public Property strBtnExecutar As String

    Public Property Cliente As Clases.Cliente
        Get
            If ViewState("Cliente") Is Nothing Then
                ViewState("Cliente") = New Clases.Cliente
            End If

            Return DirectCast(ViewState("Cliente"), Clases.Cliente)
        End Get

        Set(value As Clases.Cliente)
            ViewState("Cliente") = value
        End Set

    End Property
    Public Property SubCliente As Clases.SubCliente
        Get
            If ViewState("SubCliente") Is Nothing Then
                ViewState("SubCliente") = New Clases.SubCliente
            End If

            Return DirectCast(ViewState("SubCliente"), Clases.SubCliente)
        End Get

        Set(value As Clases.SubCliente)
            ViewState("ClienteCol") = value
        End Set

    End Property
    Public Property PuntoServicio As Clases.PuntoServicio
        Get
            If ViewState("PuntoServicio") Is Nothing Then
                ViewState("PuntoServicio") = New Clases.PuntoServicio
            End If

            Return DirectCast(ViewState("PuntoServicio"), Clases.PuntoServicio)
        End Get

        Set(value As Clases.PuntoServicio)
            ViewState("PuntoServicio") = value
        End Set

    End Property

    Public Property DatosBancarios() As List(Of Clases.DatoBancario)
        Get
            Return Session("ucDatosBanc_" + Me.Page.ToString() + "_" + Request.QueryString("oidCliente"))
        End Get
        Set(value As List(Of Clases.DatoBancario))
            Session("ucDatosBanc_" + Me.Page.ToString() + "_" + Request.QueryString("oidCliente")) = value
        End Set
    End Property

    Public Property DatosBancariosOriginal() As List(Of Clases.DatoBancario)
        Get
            Return Session("DatosBancariosOriginal_" + Me.Page.ToString() + "_" + Request.QueryString("oidCliente"))
        End Get
        Set(value As List(Of Clases.DatoBancario))
            Session("DatosBancariosOriginal_" + Me.Page.ToString() + "_" + Request.QueryString("oidCliente")) = value
        End Set
    End Property

    Private Property CantidadAprobaciones() As String

        Get
            Return ViewState("cantidadAprobaciones")
        End Get

        Set(value As String)
            ViewState("cantidadAprobaciones") = value
        End Set

    End Property
    Private Property LstCamposAprobacion() As List(Of String)

        Get
            Dim AUX As List(Of String) = ViewState("LstCamposAprobacion")
            If AUX Is Nothing Then
                ViewState("LstCamposAprobacion") = New List(Of String)
            End If
            Return ViewState("LstCamposAprobacion")
        End Get

        Set(value As List(Of String))
            ViewState("LstCamposAprobacion") = value
        End Set

    End Property
    Private Property AprobacionAlta() As String

        Get
            Return ViewState("aprobacionAlta")
        End Get

        Set(value As String)
            ViewState("aprobacionAlta") = value
        End Set

    End Property

    Private Master As IMaster

    Public Event DadosCarregados As System.EventHandler

    Public Property EsPopup As Boolean
        Get
            Return ViewState("EsPopup")
        End Get

        Set(value As Boolean)
            ViewState("EsPopup") = value
        End Set
    End Property

#Region "[METODOS]"

    Public Sub CarregaDados()

        Dim objRespuestaRecuperarDatosBancarios = RecuperarDatosBancarios(Cliente.Identificador, SubCliente.Identificador, PuntoServicio.Identificador)

        If Not Master.ControleErro.VerificaErro(objRespuestaRecuperarDatosBancarios.CodigoError, objRespuestaRecuperarDatosBancarios.NombreServidorBD, objRespuestaRecuperarDatosBancarios.MensajeError) Then
            Exit Sub
        End If

        ' TODO: PGP-916
        DatosBancariosOriginal = objRespuestaRecuperarDatosBancarios.DatosBancarios
        DatosBancarios = DatosBancariosOriginal.Clonar()
        ' Anadir en Clases.DatoBancario property se foi cambiado

        'DatosBancarios = objRespuestaRecuperarDatosBancarios.DatosBancarios
        AtualizaGrid()

        RaiseEvent DadosCarregados(Me, Nothing)

    End Sub
    Private Sub CarregarParametroAprovacion()

        Dim camposAprobacion As String = String.Empty
        Dim lstCandidadAprovaciones = Prosegur.Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_CANTIDAD_APROVACIONES)

        If lstCandidadAprovaciones IsNot Nothing AndAlso lstCandidadAprovaciones.Count > 0 Then
            If Not lstCandidadAprovaciones.ElementAt(0).MultiValue AndAlso lstCandidadAprovaciones.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                CantidadAprobaciones = lstCandidadAprovaciones.ElementAt(0).Valores.ElementAt(0)
            Else
                If lstCandidadAprovaciones.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    CantidadAprobaciones = lstCandidadAprovaciones.ElementAt(0).Valores.ElementAt(0)
                End If
            End If
        End If

        Dim auxCamposAprobacion = Prosegur.Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_CAMPOS_APROVACIONES)

        If (Not String.IsNullOrWhiteSpace(CantidadAprobaciones)) AndAlso CantidadAprobaciones <> "0" AndAlso
            auxCamposAprobacion IsNot Nothing AndAlso auxCamposAprobacion.Count > 0 Then
            If Not auxCamposAprobacion.ElementAt(0).MultiValue AndAlso auxCamposAprobacion.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                camposAprobacion = auxCamposAprobacion.ElementAt(0).Valores.ElementAt(0)
            Else
                If auxCamposAprobacion.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    camposAprobacion = auxCamposAprobacion.ElementAt(0).Valores.ElementAt(0)
                End If
            End If
            If Not String.IsNullOrWhiteSpace(camposAprobacion) Then
                LstCamposAprobacion = Split(camposAprobacion, ";").Select(Function(x) x.Trim()).ToList()
            End If
        End If


        Dim AprobacionDatosBancariosAlta = Prosegur.Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_APROBACION_ALTA)
        If AprobacionDatosBancariosAlta IsNot Nothing AndAlso AprobacionDatosBancariosAlta.Count > 0 Then
            If Not AprobacionDatosBancariosAlta.ElementAt(0).MultiValue AndAlso AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                AprobacionAlta = AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0)
            Else
                If AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    AprobacionAlta = AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0)
                End If
            End If
        End If


    End Sub


    Public Function BuscarPeticion() As Contractos.Integracion.ConfigurarDatosBancarios.Peticion
        Dim enviarCorreo As Boolean = False

        'Busco los parametros para saber si debo enviar el correo en el caso de un alta
        Dim _altaRequiereAprobacion As String = String.Empty
        Dim AprobacionDatosBancariosAlta = Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_APROBACION_ALTA)
        If AprobacionDatosBancariosAlta IsNot Nothing AndAlso AprobacionDatosBancariosAlta.Count > 0 Then
            If Not AprobacionDatosBancariosAlta.ElementAt(0).MultiValue AndAlso AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                _altaRequiereAprobacion = AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0)
            Else
                If AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    _altaRequiereAprobacion = AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0)
                End If
            End If
        End If


        Dim peticion As Contractos.Integracion.ConfigurarDatosBancarios.Peticion = New Contractos.Integracion.ConfigurarDatosBancarios.Peticion

        If peticion.DatosBancarios Is Nothing Then peticion.DatosBancarios = New List(Of Contractos.Integracion.ConfigurarDatosBancarios.Entrada.DatoBancario)

        ' TODO: PGP-916

        ' baja: todo que esta en DatosBancariosOriginal y no esta DatosBancarios
        For Each datoBanc In DatosBancariosOriginal.Where(Function(x) DatosBancarios.FirstOrDefault(Function(y) y.Identificador = x.Identificador) Is Nothing)

            Dim obj As Contractos.Integracion.ConfigurarDatosBancarios.Entrada.DatoBancario = New Contractos.Integracion.ConfigurarDatosBancarios.Entrada.DatoBancario

            obj.Accion = Contractos.Integracion.Comon.Enumeradores.AccionABM.Baja
            obj.CodigoBanco = datoBanc.Banco.Codigo
            obj.CodigoAgencia = datoBanc.CodigoAgencia
            obj.NumeroCuenta = datoBanc.CodigoCuentaBancaria
            obj.CodigoDivisa = datoBanc.Divisa.CodigoISO
            obj.Identificador = datoBanc.Identificador
            obj.Comentario = If(String.IsNullOrWhiteSpace(datoBanc.Comentario), Nothing, datoBanc.Comentario)
            peticion.DatosBancarios.Add(obj)

            'En caso de baja siempre se requiere aprobación por lo tanto se envía el correo
            enviarCorreo = True
        Next

        ' alta: todo que esta en DatosBancarios y no esta DatosBancariosOriginal
        For Each datoBanc In DatosBancarios.Where(Function(x) DatosBancariosOriginal.FirstOrDefault(Function(y) y.Identificador = x.Identificador) Is Nothing)
            Dim obj As Contractos.Integracion.ConfigurarDatosBancarios.Entrada.DatoBancario = New Contractos.Integracion.ConfigurarDatosBancarios.Entrada.DatoBancario

            obj.Accion = Contractos.Integracion.Comon.Enumeradores.AccionABM.Alta

            obj.Identificador = datoBanc.Identificador

            obj.NumeroCuenta = If(String.IsNullOrWhiteSpace(datoBanc.CodigoCuentaBancaria), Nothing, datoBanc.CodigoCuentaBancaria)

            obj.NumeroDocumento = If(String.IsNullOrWhiteSpace(datoBanc.CodigoDocumento), Nothing, datoBanc.CodigoDocumento)

            obj.Observaciones = If(String.IsNullOrWhiteSpace(datoBanc.DescripcionObs), Nothing, datoBanc.DescripcionObs)

            obj.Titularidad = If(String.IsNullOrWhiteSpace(datoBanc.DescripcionTitularidad), Nothing, datoBanc.DescripcionTitularidad)

            obj.Tipo = If(String.IsNullOrWhiteSpace(datoBanc.CodigoTipoCuentaBancaria), Nothing, datoBanc.CodigoTipoCuentaBancaria)

            obj.CodigoAgencia = datoBanc.CodigoAgencia

            If datoBanc.bolDefecto Then
                obj.Patron = "1"
            Else
                obj.Patron = "0"
            End If

            If datoBanc.Divisa IsNot Nothing Then
                obj.CodigoDivisa = datoBanc.Divisa.CodigoISO
            End If

            If datoBanc.Banco IsNot Nothing Then
                obj.CodigoBanco = datoBanc.Banco.Codigo
            End If
            If datoBanc.Cliente IsNot Nothing Then
                obj.IdentificadorCliente = datoBanc.Cliente.Identificador
            End If

            If datoBanc.SubCliente IsNot Nothing Then
                obj.IdentificadorSubCliente = datoBanc.SubCliente.Identificador
            End If

            If datoBanc.PuntoServicio IsNot Nothing Then
                obj.IdentificadorPuntoDeServicio = datoBanc.PuntoServicio.Identificador
            End If

            obj.CampoAdicional1 = If(String.IsNullOrWhiteSpace(datoBanc.DescripcionAdicionalCampo1), Nothing, datoBanc.DescripcionAdicionalCampo1)
            obj.CampoAdicional2 = If(String.IsNullOrWhiteSpace(datoBanc.DescripcionAdicionalCampo2), Nothing, datoBanc.DescripcionAdicionalCampo2)
            obj.CampoAdicional3 = If(String.IsNullOrWhiteSpace(datoBanc.DescripcionAdicionalCampo3), Nothing, datoBanc.DescripcionAdicionalCampo3)
            obj.CampoAdicional4 = If(String.IsNullOrWhiteSpace(datoBanc.DescripcionAdicionalCampo4), Nothing, datoBanc.DescripcionAdicionalCampo4)
            obj.CampoAdicional5 = If(String.IsNullOrWhiteSpace(datoBanc.DescripcionAdicionalCampo5), Nothing, datoBanc.DescripcionAdicionalCampo5)
            obj.CampoAdicional6 = If(String.IsNullOrWhiteSpace(datoBanc.DescripcionAdicionalCampo6), Nothing, datoBanc.DescripcionAdicionalCampo6)
            obj.CampoAdicional7 = If(String.IsNullOrWhiteSpace(datoBanc.DescripcionAdicionalCampo7), Nothing, datoBanc.DescripcionAdicionalCampo7)
            obj.CampoAdicional8 = If(String.IsNullOrWhiteSpace(datoBanc.DescripcionAdicionalCampo8), Nothing, datoBanc.DescripcionAdicionalCampo8)
            obj.Comentario = If(String.IsNullOrWhiteSpace(datoBanc.Comentario), Nothing, datoBanc.Comentario)

            'En caso de alta, verificar si se requiere aprobación para enviar el correo
            If (Not String.IsNullOrWhiteSpace(_altaRequiereAprobacion) AndAlso _altaRequiereAprobacion = "1") Then
                enviarCorreo = True
            End If

            peticion.DatosBancarios.Add(obj)
        Next


        ' modificar: todo que esta en DatosBancariosOriginal y esta DatosBancarios y la property modificar tiene valores
        For Each datoBanc In DatosBancarios.Where(Function(x) DatosBancariosOriginal.FirstOrDefault(Function(y) y.Identificador = x.Identificador) IsNot Nothing)

            Dim datoBancOriginal = DatosBancariosOriginal.FirstOrDefault(Function(x) x.Identificador = datoBanc.Identificador)

            Dim Modificado As Boolean = False

            Dim obj As Contractos.Integracion.ConfigurarDatosBancarios.Entrada.DatoBancario = New Contractos.Integracion.ConfigurarDatosBancarios.Entrada.DatoBancario

            obj.Accion = Contractos.Integracion.Comon.Enumeradores.AccionABM.Modificar

            obj.Identificador = datoBanc.Identificador


            If datoBanc.Cliente IsNot Nothing Then
                obj.IdentificadorCliente = datoBanc.Cliente.Identificador
            End If

            If datoBanc.SubCliente IsNot Nothing Then
                obj.IdentificadorSubCliente = datoBanc.SubCliente.Identificador
            End If

            If datoBanc.PuntoServicio IsNot Nothing Then
                obj.IdentificadorPuntoDeServicio = datoBanc.PuntoServicio.Identificador
            End If


            If datoBanc.Banco.Identificador <> datoBancOriginal.Banco.Identificador Then
                obj.CodigoBanco = datoBanc.Banco.Codigo
                Modificado = True
            Else
                obj.CodigoBanco = datoBancOriginal.Banco.Codigo
            End If

            If datoBanc.CodigoDocumento <> datoBancOriginal.CodigoDocumento Then
                obj.NumeroDocumento = datoBanc.CodigoDocumento
                Modificado = True
            End If

            If datoBanc.DescripcionTitularidad <> datoBancOriginal.DescripcionTitularidad Then
                obj.Titularidad = datoBanc.DescripcionTitularidad
                Modificado = True
            End If

            If datoBanc.CodigoTipoCuentaBancaria <> datoBancOriginal.CodigoTipoCuentaBancaria Then
                obj.Tipo = datoBanc.CodigoTipoCuentaBancaria
                Modificado = True
            End If

            If datoBanc.DescripcionObs <> datoBancOriginal.DescripcionObs Then
                obj.Observaciones = datoBanc.DescripcionObs
                Modificado = True
            End If

            If datoBanc.CodigoCuentaBancaria <> datoBancOriginal.CodigoCuentaBancaria Then
                obj.NumeroCuenta = datoBanc.CodigoCuentaBancaria
                Modificado = True
            Else
                obj.NumeroCuenta = datoBancOriginal.CodigoCuentaBancaria
            End If

            If datoBanc.CodigoAgencia <> datoBancOriginal.CodigoAgencia Then
                obj.CodigoAgencia = datoBanc.CodigoAgencia
                Modificado = True
            Else
                obj.CodigoAgencia = datoBancOriginal.CodigoAgencia
            End If

            If datoBanc.DescripcionAdicionalCampo1 <> datoBancOriginal.DescripcionAdicionalCampo1 Then
                obj.CampoAdicional1 = datoBanc.DescripcionAdicionalCampo1
                Modificado = True
            End If

            If datoBanc.DescripcionAdicionalCampo2 <> datoBancOriginal.DescripcionAdicionalCampo2 Then
                obj.CampoAdicional2 = datoBanc.DescripcionAdicionalCampo2
                Modificado = True
            End If

            If datoBanc.DescripcionAdicionalCampo3 <> datoBancOriginal.DescripcionAdicionalCampo3 Then
                obj.CampoAdicional3 = datoBanc.DescripcionAdicionalCampo3
                Modificado = True
            End If

            If datoBanc.DescripcionAdicionalCampo4 <> datoBancOriginal.DescripcionAdicionalCampo4 Then
                obj.CampoAdicional4 = datoBanc.DescripcionAdicionalCampo4
                Modificado = True
            End If

            If datoBanc.DescripcionAdicionalCampo5 <> datoBancOriginal.DescripcionAdicionalCampo5 Then
                obj.CampoAdicional5 = datoBanc.DescripcionAdicionalCampo5
                Modificado = True
            End If

            If datoBanc.DescripcionAdicionalCampo6 <> datoBancOriginal.DescripcionAdicionalCampo6 Then
                obj.CampoAdicional6 = datoBanc.DescripcionAdicionalCampo6
                Modificado = True
            End If

            If datoBanc.DescripcionAdicionalCampo7 <> datoBancOriginal.DescripcionAdicionalCampo7 Then
                obj.CampoAdicional7 = datoBanc.DescripcionAdicionalCampo7
                Modificado = True
            End If

            If datoBanc.DescripcionAdicionalCampo8 <> datoBancOriginal.DescripcionAdicionalCampo8 Then
                obj.CampoAdicional8 = datoBanc.DescripcionAdicionalCampo8
                Modificado = True
            End If

            If datoBanc.bolDefecto <> datoBancOriginal.bolDefecto Then
                If datoBanc.bolDefecto Then
                    obj.Patron = "1"
                Else
                    obj.Patron = "0"
                End If
                If String.IsNullOrWhiteSpace(datoBanc.Comentario) Then
                    datoBanc.Comentario = Traduzir("086_lbl_grd_defecto")
                End If

                Modificado = True

            End If

            If datoBanc.Divisa.Identificador <> datoBancOriginal.Divisa.Identificador Then
                obj.CodigoDivisa = datoBanc.Divisa.CodigoISO
                Modificado = True
            Else
                obj.CodigoDivisa = datoBancOriginal.Divisa.CodigoISO
            End If



            If Modificado Then
                'En caso de modificación siempre se requiere aprobación por lo tanto se envía el correo
                enviarCorreo = True

                obj.Comentario = datoBanc.Comentario
                peticion.DatosBancarios.Add(obj)
            End If


        Next

        peticion.Cultura = If(CulturaSistema IsNot Nothing AndAlso
                                                                             Not String.IsNullOrEmpty(CulturaSistema.Name),
                                                                             CulturaSistema.Name,
                                                                             If(CulturaPadrao IsNot Nothing, CulturaPadrao.Name, Globalization.CultureInfo.CurrentCulture.ToString()))
        Dim codigo_usuario As String = Session("BaseLoginUsuario")
        peticion.Usuario = codigo_usuario.Clonar




        Dim objProxyDelegacion As New Comunicacion.ProxyWS.IAC.ProxyDelegacion
        Dim objPeticionDelegacion As New IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Peticion
        Dim objRespuestaDelegacion As IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta

        objPeticionDelegacion.CodigoDelegacione = Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Delegacion.Codigo

        objRespuestaDelegacion = objProxyDelegacion.GetDelegacioneDetail(objPeticionDelegacion)
        If objRespuestaDelegacion IsNot Nothing AndAlso objRespuestaDelegacion.Delegacion.Count > 0 Then

            peticion.CodigoPais = objRespuestaDelegacion.Delegacion(0).CodPais

        End If

        'Valido si debo enviar el correo notificando cambios pendientes
        If enviarCorreo Then

            'Busco los parametros para enviar correo
            Dim _asunto As String = String.Empty
            Dim parametroAux = Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "AprobacionDatosBancariosMailAsunto")
            If parametroAux IsNot Nothing AndAlso parametroAux.Count > 0 Then
                If Not parametroAux.ElementAt(0).MultiValue AndAlso parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    _asunto = parametroAux.ElementAt(0).Valores.ElementAt(0)
                Else
                    If parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                        _asunto = parametroAux.ElementAt(0).Valores.ElementAt(0)
                    End If
                End If
            End If
            Dim _cuerpo As String = String.Empty
            parametroAux = Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "AprobacionDatosBancariosMailCuerpo")
            If parametroAux IsNot Nothing AndAlso parametroAux.Count > 0 Then
                If Not parametroAux.ElementAt(0).MultiValue AndAlso parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    _cuerpo = parametroAux.ElementAt(0).Valores.ElementAt(0)
                Else
                    If parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                        _cuerpo = parametroAux.ElementAt(0).Valores.ElementAt(0)
                    End If
                End If
            End If

            Dim _destinatarios As String = String.Empty
            parametroAux = Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "AprobacionDatosBancariosMailListaDestinatarios")
            If parametroAux IsNot Nothing AndAlso parametroAux.Count > 0 Then
                If Not parametroAux.ElementAt(0).MultiValue AndAlso parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    _destinatarios = parametroAux.ElementAt(0).Valores.ElementAt(0)
                Else
                    If parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                        _destinatarios = parametroAux.ElementAt(0).Valores.ElementAt(0)
                    End If
                End If
            End If

            Try
                MailUtil.SendMail(_asunto, _cuerpo, _destinatarios, peticion.CodigoPais)
            Catch ex As Exception
                Master.ControleErro.TratarErroException(ex)
            End Try
        End If


        ' comparar datos de lista original y cambiada
        ' cargar peticion solo con las diferencias (alta, baja, modificacion)
        ' objecto de respuesta es la colleccion para procedure de configurar datos bancarios
        ' alta: todo que esta en DatosBancarios y no esta DatosBancariosOriginal
        ' baja: todo que esta en DatosBancariosOriginal y no esta DatosBancarios
        ' modificar: todo que esta en DatosBancariosOriginal y esta DatosBancarios y la property modificar tiene valores
        ' modificar: en caso de modificacion solo cargar campos modificados

        Return peticion

    End Function

    Public Sub AtualizaGrid()
        ValidarDefecto()
        DatosBancarios = DatosBancarios.OrderBy(Function(a) a.Divisa.Descripcion).ThenBy(Function(b) b.Banco.Descripcion).ThenBy(Function(c) c.CodigoTipoCuentaBancaria).ToList()
        grvDatosBanc.DataSource = DatosBancarios
        grvDatosBanc.DataBind()

    End Sub

    Public Shared Function RecuperarDatosBancarios(oidCliente As String, oidSubCliente As String, oidPuntoServicio As String) As ContractoServicio.DatoBancario.GetDatosBancarios.Respuesta

        Dim objProxyComon As New Comunicacion.ProxyDatoBancario
        Dim objPeticionComon As New ContractoServicio.DatoBancario.GetDatosBancarios.Peticion
        Dim objRespuestaComon As ContractoServicio.DatoBancario.GetDatosBancarios.Respuesta

        With objPeticionComon
            .IdentificadorCliente = oidCliente
            .IdentificadorSubCliente = oidSubCliente
            .IdentificadorPuntoServicio = oidPuntoServicio
        End With

        objRespuestaComon = objProxyComon.GetDatosBancarios(objPeticionComon)

        Return objRespuestaComon

    End Function

    Private Sub TraduzirControles()

        CodFuncionalidad = "UCDATOSBANCARIOS"
        CarregaDicinario()


        grvDatosBanc.Columns(1).HeaderText = RecuperarValorDic("lbl_aprobacion")
        grvDatosBanc.Columns(4).HeaderText = Traduzir("086_lbl_grd_defecto")
        grvDatosBanc.Columns(5).HeaderText = Traduzir("086_lbl_grd_divisa")
        grvDatosBanc.Columns(6).HeaderText = Traduzir("086_lbl_grd_banco")
        grvDatosBanc.Columns(7).HeaderText = Traduzir("086_lbl_grd_tipo")

        grvDatosBanc.Columns(8).HeaderText = RecuperarValorDic("lbl_agencia")

        grvDatosBanc.Columns(9).HeaderText = Traduzir("086_lbl_grd_nroCuenta")
        grvDatosBanc.Columns(10).HeaderText = Traduzir("086_lbl_grd_nroDocumento")
        grvDatosBanc.Columns(2).HeaderText = Traduzir("086_lbl_grd_cambiar")
        grvDatosBanc.Columns(3).HeaderText = Traduzir("086_lbl_grd_borrar")

    End Sub

    Public Sub Desabilitar()
        grvDatosBanc.Enabled = False
    End Sub

    Public Sub Habilitar()
        grvDatosBanc.Enabled = True
    End Sub

    Private Sub ValidarDefecto()
        If Me.DatosBancarios IsNot Nothing Then

            For Each div_cli In (From b In Me.DatosBancarios
                                 Group b By divisa = b.Divisa.Identificador, cliente = b.Banco.Identificador Into G = Group
                                 Select divisa, cliente).Distinct

                Dim lstDatosBancDivisa = Me.DatosBancarios.Where(Function(a) a.Divisa.Identificador = div_cli.divisa AndAlso a.Banco.Identificador = div_cli.cliente).ToList()
                'Valida que el BolDefecto del DatoBancario sea no nulo
                If lstDatosBancDivisa.Exists(Function(a) a.bolDefecto IsNot Nothing) Then
                    If Not lstDatosBancDivisa.Exists(Function(a) a.bolDefecto) Then lstDatosBancDivisa.First.bolDefecto = True
                End If
            Next
        End If
    End Sub

    Public Sub ConsomeDatoBancario()

        If BusquedaDatosBancariosPopup.Respuesta IsNot Nothing Then

            If ValidarCambiar(BusquedaDatosBancariosPopup.Respuesta, BusquedaDatosBancariosPopup.Peticion) Then

                If Me.DatosBancarios Is Nothing Then
                    Me.DatosBancarios = New List(Of Clases.DatoBancario)
                End If

                Dim objDatoBancario As Clases.DatoBancario = Nothing

                'Novo
                If String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Peticion.Identificador) Then
                    objDatoBancario = New Clases.DatoBancario
                    objDatoBancario.Identificador = Guid.NewGuid().ToString
                    objDatoBancario.Nuevo = True
                    Me.DatosBancarios.Add(objDatoBancario)
                Else
                    objDatoBancario = Me.DatosBancarios.Find(Function(a) a.Identificador = BusquedaDatosBancariosPopup.Peticion.Identificador)
                End If

                With objDatoBancario

                    If Me.Cliente IsNot Nothing Then
                        .Cliente = New Clases.Cliente
                        .Cliente.Identificador = Me.Cliente.Identificador
                    End If
                    If Me.SubCliente IsNot Nothing Then
                        .SubCliente = New Clases.SubCliente
                        .SubCliente.Identificador = Me.SubCliente.Identificador
                    End If
                    If Me.PuntoServicio IsNot Nothing Then
                        .PuntoServicio = New Clases.PuntoServicio
                        .PuntoServicio.Identificador = Me.PuntoServicio.Identificador
                    End If

                    If BusquedaDatosBancariosPopup.Respuesta.Banco IsNot Nothing Then

                        .Banco = New Clases.Cliente
                        .Banco.Identificador = BusquedaDatosBancariosPopup.Respuesta.Banco.OidCliente
                        .Banco.Codigo = BusquedaDatosBancariosPopup.Respuesta.Banco.Codigo
                        .Banco.Descripcion = BusquedaDatosBancariosPopup.Respuesta.Banco.Descripcion


                    End If

                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.NroDocumento) Then

                        .CodigoDocumento = BusquedaDatosBancariosPopup.Respuesta.NroDocumento
                    Else
                        .CodigoDocumento = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.Titularidad) Then

                        .DescripcionTitularidad = BusquedaDatosBancariosPopup.Respuesta.Titularidad
                    Else
                        .DescripcionTitularidad = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.Tipo) Then

                        .CodigoTipoCuentaBancaria = BusquedaDatosBancariosPopup.Respuesta.Tipo
                    Else
                        .CodigoTipoCuentaBancaria = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.Obs) Then

                        .DescripcionObs = BusquedaDatosBancariosPopup.Respuesta.Obs
                    Else
                        .DescripcionObs = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.NroCuenta) Then

                        .CodigoCuentaBancaria = BusquedaDatosBancariosPopup.Respuesta.NroCuenta
                    Else
                        .CodigoCuentaBancaria = String.Empty
                    End If

                    .bolDefecto = BusquedaDatosBancariosPopup.Respuesta.BolDefecto

                    If BusquedaDatosBancariosPopup.Respuesta.Divisa IsNot Nothing Then

                        .Divisa = New Clases.Divisa
                        .Divisa.Identificador = BusquedaDatosBancariosPopup.Respuesta.Divisa.Identificador
                        .Divisa.CodigoISO = BusquedaDatosBancariosPopup.Respuesta.Divisa.CodigoIso
                        .Divisa.Descripcion = BusquedaDatosBancariosPopup.Respuesta.Divisa.Descripcion

                    End If



                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.Agencia) Then

                        .CodigoAgencia = BusquedaDatosBancariosPopup.Respuesta.Agencia
                    Else
                        .CodigoAgencia = String.Empty
                    End If



                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.CampoAdicional1) Then

                        .DescripcionAdicionalCampo1 = BusquedaDatosBancariosPopup.Respuesta.CampoAdicional1
                    Else
                        .DescripcionAdicionalCampo1 = String.Empty
                    End If


                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.CampoAdicional2) Then

                        .DescripcionAdicionalCampo2 = BusquedaDatosBancariosPopup.Respuesta.CampoAdicional2
                    Else
                        .DescripcionAdicionalCampo2 = String.Empty
                    End If


                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.CampoAdicional3) Then

                        .DescripcionAdicionalCampo3 = BusquedaDatosBancariosPopup.Respuesta.CampoAdicional3
                    Else
                        .DescripcionAdicionalCampo3 = String.Empty
                    End If


                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.CampoAdicional4) Then

                        .DescripcionAdicionalCampo4 = BusquedaDatosBancariosPopup.Respuesta.CampoAdicional4
                    Else
                        .DescripcionAdicionalCampo4 = String.Empty
                    End If


                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.CampoAdicional5) Then

                        .DescripcionAdicionalCampo5 = BusquedaDatosBancariosPopup.Respuesta.CampoAdicional5
                    Else
                        .DescripcionAdicionalCampo5 = String.Empty
                    End If


                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.CampoAdicional6) Then

                        .DescripcionAdicionalCampo6 = BusquedaDatosBancariosPopup.Respuesta.CampoAdicional6
                    Else
                        .DescripcionAdicionalCampo6 = String.Empty
                    End If


                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.CampoAdicional7) Then

                        .DescripcionAdicionalCampo7 = BusquedaDatosBancariosPopup.Respuesta.CampoAdicional7
                    Else
                        .DescripcionAdicionalCampo7 = String.Empty
                    End If


                    If Not String.IsNullOrEmpty(BusquedaDatosBancariosPopup.Respuesta.CampoAdicional8) Then

                        .DescripcionAdicionalCampo8 = BusquedaDatosBancariosPopup.Respuesta.CampoAdicional8
                    Else
                        .DescripcionAdicionalCampo8 = String.Empty
                    End If
                    .Comentario = BusquedaDatosBancariosPopup.Respuesta.Comentario
                    .Pendiente = BusquedaDatosBancariosPopup.Respuesta.Pendiente
                End With

                Dim lstDatos = Me.DatosBancarios.Where(Function(a) (a.Divisa.Identificador = objDatoBancario.Divisa.Identificador) AndAlso (a.Banco.Identificador = objDatoBancario.Banco.Identificador))

                If objDatoBancario.bolDefecto Then

                    For Each datoBanc In lstDatos
                        If objDatoBancario.Identificador <> datoBanc.Identificador Then
                            datoBanc.bolDefecto = False
                        End If
                    Next

                End If

                AtualizaGrid()

            End If

            BusquedaDatosBancariosPopup.Peticion = Nothing
            BusquedaDatosBancariosPopup.Respuesta = Nothing

        End If

    End Sub

    Private Function ValidarCambiar(respuestaBusqueda As BusquedaDatosBancariosPopup.RespuestaBusqueda, peticionBusqueda As BusquedaDatosBancariosPopup.PeticionBusqueda) As Boolean

        Dim validacao As Boolean = True

        If respuestaBusqueda IsNot Nothing Then

            If respuestaBusqueda.Banco Is Nothing Then
                respuestaBusqueda.Banco = New ContractoServicio.Utilidad.GetComboClientes.Cliente
            End If

        End If

        If String.IsNullOrEmpty(respuestaBusqueda.Banco.OidCliente) Then
            respuestaBusqueda.Banco = Nothing
        End If

        Return validacao
    End Function

    Public Sub PopupComparativo(identificador As String)
        'Busco el dato bancario seleccionado
        Dim objDatoBancario As Clases.DatoBancario = Me.DatosBancarios.Find(Function(a) a.Identificador = identificador)
        Dim objDatoBancarioOriginal As Clases.DatoBancario = Me.DatosBancariosOriginal.Find(Function(a) a.Identificador = identificador)


        'Limpio las variables de sesion utilizadas
        Session("Dto_Banc_Com_Banco") = Nothing
        Session("Dto_Banc_Com_CodigoBancario") = Nothing
        Session("Dto_Banc_Com_NroDocumento") = Nothing
        Session("Dto_Banc_Com_Agencia") = Nothing
        Session("Dto_Banc_Com_Divisa") = Nothing
        Session("Dto_Banc_Com_Obs") = Nothing
        Session("Dto_Banc_Com_Titularidad") = Nothing
        Session("Dto_Banc_Com_Cuenta") = Nothing
        Session("Dto_Banc_Com_Tipo") = Nothing
        Session("Dto_Banc_Com_Defecto") = Nothing
        Session("Dto_Banc_Com_CampoAdicional1") = Nothing
        Session("Dto_Banc_Com_CampoAdicional2") = Nothing
        Session("Dto_Banc_Com_CampoAdicional3") = Nothing
        Session("Dto_Banc_Com_CampoAdicional4") = Nothing
        Session("Dto_Banc_Com_CampoAdicional5") = Nothing
        Session("Dto_Banc_Com_CampoAdicional6") = Nothing
        Session("Dto_Banc_Com_CampoAdicional7") = Nothing
        Session("Dto_Banc_Com_CampoAdicional8") = Nothing

        'Asigno las variables de sesion para usar en el popup comparativo
        'En caso de tratarse de un nuevo dato bancario
        If objDatoBancario IsNot Nothing Then
            If objDatoBancario.Banco IsNot Nothing Then
                Session("Dto_Banc_Com_Banco") = $"{objDatoBancario.Banco.Codigo} - {objDatoBancario.Banco.Descripcion}"


                Dim _Proxy As New Comunicacion.ProxyCliente
                Dim _Peticion As New GetClientesDetalle.Peticion
                Dim _Respuesta As GetClientesDetalle.Respuesta

                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Peticion.CodCliente = objDatoBancario.Banco.Codigo
                _Respuesta = _Proxy.GetClientesDetalle(_Peticion)
                If _Respuesta.Clientes IsNot Nothing AndAlso _Respuesta.Clientes.Count > 0 Then
                    Session("Dto_Banc_Com_CodigoBancario") = _Respuesta.Clientes(0).CodBancario
                End If

                'LUCAS Ver de donde sacar el codigo bancario en nuevo registro
                'Session("Dto_Banc_Com_CodigoBancario") = Nothing
            End If
            If objDatoBancario.Divisa IsNot Nothing Then
                Session("Dto_Banc_Com_Divisa") = $"{objDatoBancario.Divisa.CodigoISO} - {objDatoBancario.Divisa.Descripcion}"
            End If

            If Not String.IsNullOrWhiteSpace(objDatoBancario.CodigoDocumento) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.CodigoDocumento <> objDatoBancario.CodigoDocumento) Then
                Session("Dto_Banc_Com_NroDocumento") = objDatoBancario.CodigoDocumento
            End If

            If Not String.IsNullOrWhiteSpace(objDatoBancario.CodigoAgencia) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.CodigoAgencia <> objDatoBancario.CodigoAgencia) Then
                Session("Dto_Banc_Com_Agencia") = objDatoBancario.CodigoAgencia
            End If

            If Not String.IsNullOrWhiteSpace(objDatoBancario.DescripcionObs) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.DescripcionObs <> objDatoBancario.DescripcionObs) Then
                Session("Dto_Banc_Com_Obs") = objDatoBancario.DescripcionObs
            End If
            If Not String.IsNullOrWhiteSpace(objDatoBancario.DescripcionTitularidad) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.DescripcionTitularidad <> objDatoBancario.DescripcionTitularidad) Then
                Session("Dto_Banc_Com_Titularidad") = objDatoBancario.DescripcionTitularidad
            End If
            If Not String.IsNullOrWhiteSpace(objDatoBancario.CodigoCuentaBancaria) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.CodigoCuentaBancaria <> objDatoBancario.CodigoCuentaBancaria) Then
                Session("Dto_Banc_Com_Cuenta") = objDatoBancario.CodigoCuentaBancaria
            End If
            If Not String.IsNullOrWhiteSpace(objDatoBancario.CodigoTipoCuentaBancaria) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.CodigoTipoCuentaBancaria <> objDatoBancario.CodigoTipoCuentaBancaria) Then
                Session("Dto_Banc_Com_Tipo") = objDatoBancario.CodigoTipoCuentaBancaria
            End If
            Session("Dto_Banc_Com_Defecto") = objDatoBancario.bolDefecto
            If Not String.IsNullOrWhiteSpace(objDatoBancario.DescripcionAdicionalCampo1) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.DescripcionAdicionalCampo1 <> objDatoBancario.DescripcionAdicionalCampo1) Then
                Session("Dto_Banc_Com_CampoAdicional1") = objDatoBancario.DescripcionAdicionalCampo1
            End If
            If Not String.IsNullOrWhiteSpace(objDatoBancario.DescripcionAdicionalCampo2) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.DescripcionAdicionalCampo2 <> objDatoBancario.DescripcionAdicionalCampo2) Then
                Session("Dto_Banc_Com_CampoAdicional2") = objDatoBancario.DescripcionAdicionalCampo2
            End If
            If Not String.IsNullOrWhiteSpace(objDatoBancario.DescripcionAdicionalCampo3) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.DescripcionAdicionalCampo3 <> objDatoBancario.DescripcionAdicionalCampo3) Then
                Session("Dto_Banc_Com_CampoAdicional3") = objDatoBancario.DescripcionAdicionalCampo3
            End If
            If Not String.IsNullOrWhiteSpace(objDatoBancario.DescripcionAdicionalCampo4) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.DescripcionAdicionalCampo4 <> objDatoBancario.DescripcionAdicionalCampo4) Then
                Session("Dto_Banc_Com_CampoAdicional4") = objDatoBancario.DescripcionAdicionalCampo4
            End If
            If Not String.IsNullOrWhiteSpace(objDatoBancario.DescripcionAdicionalCampo5) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.DescripcionAdicionalCampo5 <> objDatoBancario.DescripcionAdicionalCampo5) Then
                Session("Dto_Banc_Com_CampoAdicional5") = objDatoBancario.DescripcionAdicionalCampo5
            End If
            If Not String.IsNullOrWhiteSpace(objDatoBancario.DescripcionAdicionalCampo6) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.DescripcionAdicionalCampo6 <> objDatoBancario.DescripcionAdicionalCampo6) Then
                Session("Dto_Banc_Com_CampoAdicional6") = objDatoBancario.DescripcionAdicionalCampo6
            End If
            If Not String.IsNullOrWhiteSpace(objDatoBancario.DescripcionAdicionalCampo7) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.DescripcionAdicionalCampo7 <> objDatoBancario.DescripcionAdicionalCampo7) Then
                Session("Dto_Banc_Com_CampoAdicional7") = objDatoBancario.DescripcionAdicionalCampo7
            End If
            If Not String.IsNullOrWhiteSpace(objDatoBancario.DescripcionAdicionalCampo8) OrElse
                (objDatoBancarioOriginal IsNot Nothing AndAlso objDatoBancarioOriginal.DescripcionAdicionalCampo8 <> objDatoBancario.DescripcionAdicionalCampo8) Then
                Session("Dto_Banc_Com_CampoAdicional8") = objDatoBancario.DescripcionAdicionalCampo8
            End If

        End If

        Dim url As String = "BusquedaDatosBancariosPopupComparativo.aspx?identificador=" & identificador
        Master.ExibirModal(url, Traduzir("087_lbl_titulo"), 400, 700, False, False, String.Empty)
    End Sub
    Public Sub Cambiar(rowIndex As Integer)

        Try

            Dim objPage As Base = DirectCast(Me.Page, Base)
            Session("Dto_Banc_BancoSelecionado") = Nothing
            Session("Dto_Banc_NroDocumento") = Nothing
            Session("Dto_Banc_Titularidad") = Nothing
            Session("Dto_Banc_Tipo") = Nothing
            Session("Dto_Banc_Observaciones") = Nothing
            Session("Dto_Banc_NroCuenta") = Nothing
            Session("Dto_Banc_BolDefecto") = Nothing
            Session("Dto_Banc_DivisaSelecionado") = Nothing

            Session("Dto_Banc_CodigoAgencia") = Nothing
            Session("Dto_Banc_DescAdicionalCampo1") = Nothing
            Session("Dto_Banc_DescAdicionalCampo2") = Nothing
            Session("Dto_Banc_DescAdicionalCampo3") = Nothing
            Session("Dto_Banc_DescAdicionalCampo4") = Nothing
            Session("Dto_Banc_DescAdicionalCampo5") = Nothing
            Session("Dto_Banc_DescAdicionalCampo6") = Nothing
            Session("Dto_Banc_DescAdicionalCampo7") = Nothing
            Session("Dto_Banc_DescAdicionalCampo8") = Nothing
            Session("Dto_Banc_Comentario") = Nothing
            Session("Dto_Banc_Pendiente") = Nothing
            Session("Dato_Identificador") = Nothing
            Dim nuevoDatoBancario = False
            If rowIndex >= 0 Then
                Dim grdRow = grvDatosBanc.Rows(rowIndex)
                Dim lblIdentificador As Label = grdRow.FindControl("lblIdentificador")
                Session("Dato_Identificador") = lblIdentificador
                Dim objDatoBancario As Clases.DatoBancario = Me.DatosBancarios.Find(Function(a) a.Identificador = lblIdentificador.Text)

                If objDatoBancario IsNot Nothing Then
                    BusquedaDatosBancariosPopup.Peticion = New BusquedaDatosBancariosPopup.PeticionBusqueda
                    BusquedaDatosBancariosPopup.Peticion.Identificador = objDatoBancario.Identificador

                    ' CLIENTE
                    Dim Cliente = New ContractoServicio.Utilidad.GetComboClientes.Cliente
                    Dim SubCliente = New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
                    Dim PuntoServicio = New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio

                    If objDatoBancario.Banco IsNot Nothing Then
                        Cliente.Codigo = objDatoBancario.Banco.Codigo
                        Cliente.Descripcion = objDatoBancario.Banco.Descripcion
                        Cliente.OidCliente = objDatoBancario.Banco.Identificador
                        Cliente.TotalizadorSaldo = True

                        Session("Dto_Banc_BancoSelecionado") = Cliente

                    Else
                        Session("Dto_Banc_BancoSelecionado") = Nothing
                    End If

                    Session("Dto_Banc_NroDocumento") = objDatoBancario.CodigoDocumento
                    Session("Dto_Banc_Titularidad") = objDatoBancario.DescripcionTitularidad
                    Session("Dto_Banc_Tipo") = objDatoBancario.CodigoTipoCuentaBancaria
                    Session("Dto_Banc_Observaciones") = objDatoBancario.DescripcionObs
                    Session("Dto_Banc_NroCuenta") = objDatoBancario.CodigoCuentaBancaria
                    Session("Dto_Banc_BolDefecto") = objDatoBancario.bolDefecto
                    Session("Dto_Banc_CodigoAgencia") = objDatoBancario.CodigoAgencia
                    Session("Dto_Banc_DescAdicionalCampo1") = objDatoBancario.DescripcionAdicionalCampo1
                    Session("Dto_Banc_DescAdicionalCampo2") = objDatoBancario.DescripcionAdicionalCampo2
                    Session("Dto_Banc_DescAdicionalCampo3") = objDatoBancario.DescripcionAdicionalCampo3
                    Session("Dto_Banc_DescAdicionalCampo4") = objDatoBancario.DescripcionAdicionalCampo4
                    Session("Dto_Banc_DescAdicionalCampo5") = objDatoBancario.DescripcionAdicionalCampo5
                    Session("Dto_Banc_DescAdicionalCampo6") = objDatoBancario.DescripcionAdicionalCampo6
                    Session("Dto_Banc_DescAdicionalCampo7") = objDatoBancario.DescripcionAdicionalCampo7
                    Session("Dto_Banc_DescAdicionalCampo8") = objDatoBancario.DescripcionAdicionalCampo8
                    Session("Dto_Banc_Pendiente") = objDatoBancario.Pendiente

                    If objDatoBancario.Divisa IsNot Nothing Then

                        Dim divisa As New ContractoServicio.Utilidad.GetComboDivisas.Divisa

                        divisa.Identificador = objDatoBancario.Divisa.Identificador
                        divisa.CodigoIso = objDatoBancario.Divisa.CodigoISO
                        divisa.Descripcion = objDatoBancario.Divisa.Descripcion

                        Session("Dto_Banc_DivisaSelecionado") = divisa
                    Else
                        Session("Dto_Banc_DivisaSelecionado") = Nothing
                    End If

                    'Permite comprobar si pasa por el proceso de aprobación
                    nuevoDatoBancario = objDatoBancario.Nuevo
                End If

            Else
                nuevoDatoBancario = True
                BusquedaDatosBancariosPopup.Peticion = New BusquedaDatosBancariosPopup.PeticionBusqueda

            End If

            Dim url As String = "BusquedaDatosBancariosPopup.aspx?acao=" & objPage.Acao & "&campoObrigatorio=False" & "&nuevoDatoBancario=" & nuevoDatoBancario

            If Me.EsPopup Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "script_popup_tot_saldo", "AbrirPopup('" & url & "', 'script_popup_tot_saldo', 500, 800);", True)
            Else
                Master.ExibirModal(url, Traduzir("087_lbl_titulo"), 500, 800, False, True, strBtnExecutar)
            End If
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Private Sub Borrar(identificador As String)
        Dim objPage As Base = DirectCast(Me.Page, Base)

        'Agregamos el identificador en una variable de sesion
        Session("Dto_Banc_Identificador") = identificador

        Dim objDatoBancario As Clases.DatoBancario = Me.DatosBancarios.Find(Function(a) a.Identificador = identificador)

        If objDatoBancario IsNot Nothing Then
            'Validamos si se trata de un dato bancario que no esta en la base
            'Y si el campo BOL_ACTIVO requiere aprobación
            If Not objDatoBancario.Nuevo AndAlso
               (Not String.IsNullOrWhiteSpace(CantidadAprobaciones) AndAlso CantidadAprobaciones <> "0" AndAlso
               (LstCamposAprobacion.Count = 0 OrElse LstCamposAprobacion.Contains("BOL_ACTIVO"))) Then

                'Mostrar Modal de comentario
                Dim url As String = "BusquedaDatosBancariosComentarioPopup.aspx"
                Master.ExibirModal(url, Traduzir("087_lbl_titulo"), 220, 400, False, False, btnAlertaSi.ClientID)
            Else
                Me.DatosBancarios.Remove(objDatoBancario)
                ValidarDefecto()
                AtualizaGrid()
            End If
        End If

    End Sub

    Private Sub btnAlertaSi_Click(sender As Object, e As System.EventArgs) Handles btnAlertaSi.Click
        If Session("Dto_Banc_Comentario") IsNot Nothing AndAlso Session("Dto_Banc_Comentario") <> "" Then

            'Recupero el identificador de la variable de sesion
            Dim identificador As String = Session("Dto_Banc_Identificador")

            'Agregar comentario a dato bancario original 
            Dim objDatoBancarioOriginal = Me.DatosBancariosOriginal.Find(Function(a) a.Identificador = identificador)
            If objDatoBancarioOriginal IsNot Nothing Then
                objDatoBancarioOriginal.Comentario = Session("Dto_Banc_Comentario")
            End If

            'Elimino el dato bancario de la grilla
            Dim objDatoBancario As Clases.DatoBancario = Me.DatosBancarios.Find(Function(a) a.Identificador = identificador)
            If objDatoBancario IsNot Nothing Then
                Me.DatosBancarios.Remove(objDatoBancario)
                ValidarDefecto()
                AtualizaGrid()
            End If

        End If
    End Sub
#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        DevExpress.Web.ASPxGridView.ASPxGridView.RegisterBaseScript(Page)

        Me.Master = TryCast(Me.Page.Master, Master.Master)
        If Me.Master Is Nothing Then
            Me.Master = DirectCast(Me.Page.Master, Master.MasterModal)
        End If
        If Not Me.IsPostBack Then
            TraduzirControles()
            CarregaDados()
            CarregarParametroAprovacion()
        End If

        ConsomeDatoBancario()
    End Sub

    Protected Sub grvDatosBanc_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvDatosBanc.RowCommand
        If e.CommandName = "Cambiar" Then
            Cambiar(e.CommandArgument)
        ElseIf e.CommandName = "Borrar" Then
            Borrar(e.CommandArgument)
        ElseIf e.CommandName = "PopupComparativo" Then
            PopupComparativo(e.CommandArgument)
        End If
    End Sub

    Protected Sub grvDatosBanc_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim objPage As Base = DirectCast(Me.Page, Base)
            Dim imbModificar As ImageButton = e.Row.FindControl("imbModificar")
            Dim imbBorrar As ImageButton = e.Row.FindControl("imbBorrar")
            Dim chkDefecto As CheckBox = e.Row.FindControl("chkDefecto")
            Dim lblTipo As Label = e.Row.FindControl("lblTipo")

            'chkDefecto.Enabled = Not chkDefecto.Checked

            If objPage.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                imbModificar.Enabled = False
                imbModificar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/grid/edit.png")
                imbModificar.Style.Add("cursor", "default")
                imbModificar.Attributes.Add("class", "imgbutton")
                imbBorrar.Enabled = False
                imbBorrar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/grid/borrar.png")
                imbBorrar.Style.Add("cursor", "default")
                imbBorrar.Attributes.Add("class", "imgbutton")

            End If

            'Adicionado para preencher o Enum ao mudar de página
            'GENPLATINT-1778 - Argentina - IAC: No está mostrando la descripción del tipo de la cuenta en el listado de cuentas bancárias
            If TypeOf e.Row.DataItem Is DataRowView Then
                Dim dtRow As DataRowView = DirectCast(e.Row.DataItem, DataRowView)
                If dtRow IsNot Nothing AndAlso lblTipo IsNot Nothing Then
                    lblTipo.Text = String.Empty

                    Dim CodigoTipoCuentaBancaria As String = dtRow("CodigoTipoCuentaBancaria").ToString()
                    If Not String.IsNullOrEmpty(CodigoTipoCuentaBancaria) Then
                        lblTipo.Text = CodigoTipoCuentaBancaria
                    End If
                End If
            End If
            'Adicionado para preencher o Enum ao mudar de página
            'GENPLATINT-1778 - Argentina - IAC: No está mostrando la descripción del tipo de la cuenta en el listado de cuentas bancárias

        End If
    End Sub

    Protected Sub chkDefecto_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim chkDefectoDefecto As CheckBox = DirectCast(sender, CheckBox)
        Dim grdRowDefecto As GridViewRow = DirectCast(chkDefectoDefecto.DataItemContainer, GridViewRow)
        Dim lblIdentificador As Label = Session("Dato_Identificador")
        If lblIdentificador IsNot Nothing Then

            Dim datoSeleccionado = Me.DatosBancarios.FirstOrDefault(Function(a) a.Identificador = lblIdentificador.Text)
            If datoSeleccionado.bolDefecto Then

                Dim lblIdDivisa As Label = grdRowDefecto.FindControl("lblIdDivisa")
                Dim lblBanco As Label = grdRowDefecto.FindControl("lblBanco")

                Dim lstDatoBancarioDivisa As List(Of Clases.DatoBancario) = Me.DatosBancarios.Where(Function(a) a.Divisa.Identificador = lblIdDivisa.Text AndAlso a.Banco.Descripcion = lblBanco.Text).ToList()
                For Each datoBanc In lstDatoBancarioDivisa

                    If datoBanc.Identificador <> lblIdentificador.Text Then
                        datoBanc.bolDefecto = False
                    End If

                Next
            Else
                Dim objDatoBancario As Clases.DatoBancario = Me.DatosBancarios.Find(Function(a) a.Identificador = lblIdentificador.Text)
                If objDatoBancario IsNot Nothing Then
                    objDatoBancario.bolDefecto = False
                End If
                ValidarDefecto()
            End If

        End If

        AtualizaGrid()
    End Sub

    Protected Sub grvDatosBanc_EPreencheDados(sender As Object, e As System.EventArgs) Handles grvDatosBanc.EPreencheDados
        Try
            Dim objDT As DataTable = grvDatosBanc.ConvertListToDataTable(DatosBancarios)
            grvDatosBanc.ControleDataSource = objDT

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try
    End Sub
#End Region


    Private Property dicionario() As Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
        Get
            If Session("dicionario") IsNot Nothing Then
                Return Session("dicionario")
            Else
                Session("dicionario") = New Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
            End If

            Return Session("dicionario")
        End Get
        Set(value As Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String)))
            Session("dicionario") = value
        End Set
    End Property
    Public Property CodFuncionalidad() As String
        Get
            Return ViewState("CodFuncionalidad")
        End Get
        Set(value As String)
            ViewState("CodFuncionalidad") = value
        End Set
    End Property
    Public ReadOnly Property CodFuncionalidadGenerica() As String
        Get
            Return "GENERICO"
        End Get
    End Property


    Public Sub CarregaDicinario()

        CarregaChavesDicinario(Me.CodFuncionalidadGenerica)
        CarregaChavesDicinario(Me.CodFuncionalidad)

    End Sub

    Private Sub CarregaChavesDicinario(CodigoFuncionalidad As String)
        If Not String.IsNullOrEmpty(CodigoFuncionalidad) Then
            If dicionario Is Nothing Then
                dicionario = New Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
            End If

            'Se já tiver carregado os dicionarios da funcionalidade nao carrega novamente
            If dicionario.ContainsKey(CodigoFuncionalidad) AndAlso dicionario(CodigoFuncionalidad).Values IsNot Nothing AndAlso dicionario(CodigoFuncionalidad).Values.Count > 0 Then
                Exit Sub
            End If

            Dim codigoCultura As String = If(CulturaSistema IsNot Nothing AndAlso
                                                                             Not String.IsNullOrEmpty(CulturaSistema.Name),
                                                                             CulturaSistema.Name,
                                                                             If(CulturaPadrao IsNot Nothing, CulturaPadrao.Name, Globalization.CultureInfo.CurrentCulture.ToString()))

            Dim peticion As New Prosegur.Genesis.ContractoServicio.Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Peticion
            With peticion
                .CodigoFuncionalidad = CodigoFuncionalidad
                .Cultura = codigoCultura
            End With
            Dim respuesta = Prosegur.Genesis.LogicaNegocio.Genesis.Diccionario.ObtenerValoresDicionario(peticion)

            If dicionario.ContainsKey(CodigoFuncionalidad) Then
                dicionario(CodigoFuncionalidad) = respuesta.Valores
            Else
                dicionario.Add(CodigoFuncionalidad, respuesta.Valores)
            End If
        End If
    End Sub

    Public Function RecuperarValorDic(chave) As String
        Try
            If dicionario IsNot Nothing AndAlso dicionario.Count > 0 Then

                If Not String.IsNullOrWhiteSpace(Me.CodFuncionalidad) AndAlso dicionario.ContainsKey(Me.CodFuncionalidad) Then
                    Dim chavesDic = dicionario(Me.CodFuncionalidad)
                    If chavesDic IsNot Nothing AndAlso chavesDic.ContainsKey(chave) Then
                        Return chavesDic(chave)
                    End If
                End If

                If (Not String.IsNullOrWhiteSpace(Me.CodFuncionalidadGenerica) AndAlso dicionario.ContainsKey(Me.CodFuncionalidadGenerica)) Then
                    Dim chavesDic = dicionario(Me.CodFuncionalidadGenerica)
                    If chavesDic IsNot Nothing AndAlso chavesDic.ContainsKey(chave) Then
                        Return chavesDic(chave)
                    End If
                End If

            End If
        Catch ex As Exception
        End Try

        Return chave
    End Function

    Public Function RecuperarChavesDic() As Comon.SerializableDictionary(Of String, String)
        Try
            If dicionario IsNot Nothing AndAlso dicionario.Count > 0 AndAlso (dicionario.ContainsKey(Me.CodFuncionalidad) OrElse dicionario.ContainsKey(Me.CodFuncionalidadGenerica)) Then
                If dicionario.ContainsKey(Me.CodFuncionalidad) Then
                    Return dicionario(Me.CodFuncionalidad)
                ElseIf dicionario.ContainsKey(Me.CodFuncionalidadGenerica) Then
                    Return dicionario(Me.CodFuncionalidadGenerica)
                End If
            End If
        Catch ex As Exception
        End Try

        Return Nothing
    End Function

End Class