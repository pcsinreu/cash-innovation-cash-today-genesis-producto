Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Drawing
Imports System.IO
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports System.Web.UI.WebControls
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Public Class ConfiguracionReporte
    Inherits Base
    Private _Acciones As ucAcciones
    Private _Modo As Enumeradores.Modo?
    

    Private _ConfigReporteGrabado As Clases.ConfiguracionReporte = Nothing


#Region "[OVERRIDES]"
    Protected Overrides Sub Inicializar()
        Try

            MyBase.Inicializar()

            If Not IsPostBack Then
                cargarTiposClientes()
                cargarTiposReporte()
                CargarClientes()
                CargarRenderizador()
            End If

            AjustarAcciones()

            If (Not Page.IsPostBack) Then
                CargarDados()
                CargarParametros()
            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CERTIFICADOS_REPORTE_CONFIGURAR
        MyBase.ValidarAcesso = True
        MyBase.ValidarPemissaoAD = True
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("068_lblTitulo_ConfigReporte")
        Me.lblTituloDadosGenerales.Text = Traduzir("068_lblTituloDadosGenerales")
        Me.lblTiposReporte.Text = Traduzir("068_lbl_Tipo_Reporte")
        Me.lblMascaraNombre.Text = Traduzir("068_lblMascaraNombre")
        Me.lblExtensaoArquivo.Text = Traduzir("068_lblExtensaoArquivo")
        Me.lblParametros.Text = Traduzir("068_lblParametros")
        Me.lblSeparadorArquivo.Text = Traduzir("068_lblSeparadorArquivo")
        Me.lblRenderizador.Text = Traduzir("068_lblRenderizador")
    End Sub

#End Region

#Region "[MÉTODOS]"

    Private Function ValidarCamposObligatorios() As Boolean

        Dim msg As New StringBuilder

        If String.IsNullOrEmpty(txtCodigo.Text) Then
            msg.AppendLine(Traduzir("068_codigo_reporte"))
        End If

        If String.IsNullOrEmpty(txtDescricao.Text) Then
            msg.AppendLine(Traduzir("068_descripcion_reporte"))
        End If

        If String.IsNullOrEmpty(txtDireccion.Text) Then
            msg.AppendLine(Traduzir("068_direccion_reporte"))
        End If

        If cklTiposClientes IsNot Nothing AndAlso (From tb As ListItem In cklTiposClientes.Items Where tb.Selected).Count = 0 AndAlso _
            (cklClientesTotalizadorSaldos IsNot Nothing AndAlso (From tb As ListItem In cklClientesTotalizadorSaldos.Items Where tb.Selected).Count = 0) AndAlso _
            (cklClientesTotalizadorSaldos2 IsNot Nothing AndAlso (From tb As ListItem In cklClientesTotalizadorSaldos2.Items Where tb.Selected).Count = 0) AndAlso _
            (cklClientesTotalizadorSaldos3 IsNot Nothing AndAlso (From tb As ListItem In cklClientesTotalizadorSaldos3.Items Where tb.Selected).Count = 0) Then
            msg.AppendLine(Traduzir("068_tipo_cliente_obligatorio"))
        End If

        If cklTiposReporte IsNot Nothing AndAlso (From tb As ListItem In cklTiposReporte.Items Where tb.Selected).Count = 0 Then
            msg.AppendLine(Traduzir("068_tipo_reporte_obligatorio"))
        End If

        If cklTiposReporte.SelectedValue <> Comon.Enumeradores.TipoReporte.Certificacion _
            AndAlso String.IsNullOrEmpty(txtMascaraNombre.Text) Then
            msg.AppendLine(Traduzir("068_txtMascaraNombre_obligatorio"))
        End If

        If cklTiposReporte.SelectedValue <> Comon.Enumeradores.TipoReporte.Certificacion _
            AndAlso ddlRenderizador.SelectedValue = Traduzir("gen_selecione") Then
            msg.AppendLine(Traduzir("068_ddlRenderizador_obligatorio"))
        End If

        If ddlRenderizador.SelectedValue = Enumeradores.TipoRenderizador.CSV.ToString() AndAlso String.IsNullOrEmpty(txtExtensaoArquivo.Text) Then
            msg.AppendLine(Traduzir("068_txtExtensaoArquivo_obligatorio"))
        End If

        If msg.Length > 0 Then
            MyBase.MostraMensagemErro(msg.ToString)
            Return False
        Else
            Return True
        End If

    End Function

#End Region


    Public Property ConfigReporteGrabado() As Clases.ConfiguracionReporte
        Get
            If _ConfigReporteGrabado Is Nothing Then
                _ConfigReporteGrabado = ViewState("_ConfigReporteGrabado")
            End If
            Return _ConfigReporteGrabado
        End Get
        Set(value As Clases.ConfiguracionReporte)
            _ConfigReporteGrabado = value
            ViewState("_ConfigReporteGrabado") = _ConfigReporteGrabado
        End Set
    End Property

    Public ReadOnly Property Modo() As Enumeradores.Modo
        Get
            If Not _Modo.HasValue Then
                _Modo = [Enum].Parse(GetType(Enumeradores.Modo), Request.QueryString("Modo"), True)
            End If
            Return _Modo.Value
        End Get
    End Property
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Public ReadOnly Property Acciones() As ucAcciones
        Get
            If _Acciones Is Nothing Then
                _Acciones = LoadControl("~\Controles\UcAcciones.ascx")
                _Acciones.ID = Me.ID & "_Acciones"
                AddHandler _Acciones.Erro, AddressOf ErroControles
                phAcciones.Controls.Add(_Acciones)
            End If
            Return _Acciones
        End Get
    End Property

    Private Sub PreencherIdentificacion(ByRef ConfigReporte As Clases.ConfiguracionReporte)
        If hidIdendificador.Value.Trim.Length > 0 AndAlso Modo = Enumeradores.Modo.Modificacion Then
            ConfigReporte.Identificador = ConfigReporteGrabado.Identificador
            ConfigReporte.UsuarioCreacion = ConfigReporteGrabado.UsuarioCreacion
        End If
    End Sub

    Protected Sub CargarDados()

        hidIdendificador.Value = Request.QueryString("IdentificadorFormulario")

        If (hidIdendificador IsNot Nothing AndAlso hidIdendificador.Value.Trim().Length > 0) Then
            hidIdendificador.Value = Request.QueryString("IdentificadorFormulario").Trim()
            ConfigReporteGrabado = LogicaNegocio.GenesisSaldos.FormulariosCertificados.ObtenerConfiguracionReporte(hidIdendificador.Value)

            If ConfigReporteGrabado IsNot Nothing Then
                'preenchendo tipocliente e cliente
                ConfigReporteGrabado.TiposClientes = New ObservableCollection(Of Clases.TipoCliente)
                ConfigReporteGrabado.TiposClientes = LogicaNegocio.GenesisSaldos.FormulariosCertificados.ObtenerTipoClientesReporte(hidIdendificador.Value)

                ConfigReporteGrabado.Clientes = New ObservableCollection(Of Clases.Cliente)
                ConfigReporteGrabado.Clientes = LogicaNegocio.GenesisSaldos.FormulariosCertificados.ObtenerClientesReporte(hidIdendificador.Value)
                CargarIdentificacion(ConfigReporteGrabado)

                If ConfigReporteGrabado.TipoReporte = Enumeradores.TipoReporte.Certificacion Then
                    dvNombreArchivo.Style.Add("display", "none")
                Else
                    dvNombreArchivo.Style.Remove("display")
                End If

                If ConfigReporteGrabado.CodigoRedenrizador <> Enumeradores.TipoRenderizador.CSV Then
                    dvSeparadorArquivo.Style.Add("display", "none")
                    txtExtensaoArquivo.Enabled = False
                Else
                    dvSeparadorArquivo.Style.Remove("display")
                    txtExtensaoArquivo.Enabled = True
                End If
            End If
        End If

    End Sub

    Protected Sub CargarIdentificacion(ConfigReporte As Comon.Clases.ConfiguracionReporte)

        txtCodigo.Text = ConfigReporte.Codigo
        txtDescricao.Text = ConfigReporte.Descripcion
        txtDireccion.Text = ConfigReporte.Direccion
        txtMascaraNombre.Text = ConfigReporte.MascaraNombre
        txtSeparadorArquivo.Text = ConfigReporte.DescripcionSeparador
        ddlRenderizador.SelectedValue = ConfigReporte.CodigoRedenrizador.ToString()
        If ConfigReporte.CodigoRedenrizador = Enumeradores.TipoRenderizador.CSV Then
            txtExtensaoArquivo.Text = ConfigReporte.DescripcionExtension
        ElseIf ConfigReporte.CodigoRedenrizador <> Enumeradores.TipoRenderizador.NoDefinido Then
            txtExtensaoArquivo.Text = ConfigReporte.CodigoRedenrizador.RecuperarValor()
        End If

        If ConfigReporte.TiposClientes IsNot Nothing AndAlso ConfigReporte.TiposClientes.Count > 0 Then
            If cklTiposClientes IsNot Nothing AndAlso cklTiposClientes.Items.Count > 0 Then
                For Each Item In cklTiposClientes.Items
                    Dim TipoCliente As Clases.TipoCliente
                    If ConfigReporte.TiposClientes.FindAll(Function(Tc) Tc.Identificador = Item.value).Count > 0 Then
                        TipoCliente = New Clases.TipoCliente With {.Identificador = ConfigReporte.TiposClientes.FindAll(Function(Tc) Tc.Identificador = Item.value).First.Identificador}
                        Item.Selected = True
                    End If
                Next
            End If
        End If

        If cklTiposReporte.Items.Count > 0 Then

            Dim Item As ListItem = (From i As ListItem In cklTiposReporte.Items Where i.Value = ConfigReporte.TipoReporte.RecuperarValor).FirstOrDefault
            Item.Selected = True

        End If

        If ConfigReporte.Clientes IsNot Nothing AndAlso ConfigReporte.Clientes.Count > 0 Then
            If cklClientesTotalizadorSaldos IsNot Nothing AndAlso cklClientesTotalizadorSaldos.Items.Count > 0 Then
                For Each Item In cklClientesTotalizadorSaldos.Items
                    Dim Cliente As Clases.Cliente
                    If ConfigReporte.Clientes.FindAll(Function(Tc) Tc.Identificador = Item.value).Count > 0 Then
                        Cliente = New Clases.Cliente With {.Identificador = ConfigReporte.Clientes.FindAll(Function(Tc) Tc.Identificador = Item.value).First.Identificador}
                        Item.Selected = True
                    End If
                Next
            End If
            If cklClientesTotalizadorSaldos2 IsNot Nothing AndAlso cklClientesTotalizadorSaldos2.Items.Count > 0 Then
                For Each Item In cklClientesTotalizadorSaldos2.Items
                    Dim Cliente As Clases.Cliente
                    If ConfigReporte.Clientes.FindAll(Function(Tc) Tc.Identificador = Item.value).Count > 0 Then
                        Cliente = New Clases.Cliente With {.Identificador = ConfigReporte.Clientes.FindAll(Function(Tc) Tc.Identificador = Item.value).First.Identificador}
                        Item.Selected = True
                    End If
                Next
            End If
            If cklClientesTotalizadorSaldos3 IsNot Nothing AndAlso cklClientesTotalizadorSaldos3.Items.Count > 0 Then
                For Each Item In cklClientesTotalizadorSaldos3.Items
                    Dim Cliente As Clases.Cliente
                    If ConfigReporte.Clientes.FindAll(Function(Tc) Tc.Identificador = Item.value).Count > 0 Then
                        Cliente = New Clases.Cliente With {.Identificador = ConfigReporte.Clientes.FindAll(Function(Tc) Tc.Identificador = Item.value).First.Identificador}
                        Item.Selected = True
                    End If
                Next
            End If
        End If

    End Sub
    Private Sub cargarTiposClientes()
        Try

            Dim accionTipoCliente As New IAC.LogicaNegocio.AccionTipoCliente
            Dim peticionTipoCliente As New IAC.ContractoServicio.TipoCliente.GetTiposClientes.Peticion
            peticionTipoCliente.ParametrosPaginacion.RealizarPaginacion = False

            Dim tipoCliente = accionTipoCliente.getTiposClientes(peticionTipoCliente).TipoCliente

            If tipoCliente IsNot Nothing AndAlso tipoCliente.Count > 0 Then

                cklTiposClientes.DataTextField = "desTipoCliente"
                cklTiposClientes.DataValueField = "oidTipoCliente"
                cklTiposClientes.DataSource = tipoCliente
                cklTiposClientes.DataBind()

            End If


        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub cargarTiposReporte()
        Try


            Dim objTiposReporte As New UI.WebControls.ListItemCollection

            objTiposReporte.Add(New ListItem With { _
                                .Value = Comon.Enumeradores.TipoReporte.Certificacion.GetHashCode, _
                                .Text = Traduzir("068_Tipo_Reporte_Certificado")})

            objTiposReporte.Add(New ListItem With { _
                               .Value = Comon.Enumeradores.TipoReporte.AbonoVisualizacion.GetHashCode, _
                               .Text = Traduzir("068_Tipo_Reporte_Abono_Vizualicacion")})

            objTiposReporte.Add(New ListItem With { _
                                .Value = Comon.Enumeradores.TipoReporte.AbonoExportacion.GetHashCode, _
                                .Text = Traduzir("068_Tipo_Reporte_Abono_Exportacion")})

            cklTiposReporte.DataTextField = "Text"
            cklTiposReporte.DataValueField = "Value"
            cklTiposReporte.DataSource = objTiposReporte
            cklTiposReporte.DataBind()

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub CargarClientes()
        Try
            Dim objPeticion As New IAC.ContractoServicio.Cliente.GetClientes.Peticion()
            Dim objRespuesta As IAC.ContractoServicio.Cliente.GetClientes.Respuesta
            Dim objProxy As New Comunicacion.ProxyCliente()

            objPeticion.BolTotalizadorSaldo = True
            objPeticion.BolVigente = True
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False
            objRespuesta = objProxy.GetClientes(objPeticion)

            If objRespuesta.CodigoError <> Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(objRespuesta.CodigoError, objRespuesta.MensajeError)
            End If

            If objRespuesta IsNot Nothing AndAlso objRespuesta.Clientes.Count > 0 Then
                Dim Registro As Integer = 0
                Dim NumMin As Integer = objRespuesta.Clientes.Count / 3

                cklClientesTotalizadorSaldos.Items.Clear()
                For Each C In objRespuesta.Clientes.OrderBy(Function(x) x.DesCliente)
                    Registro = Registro + 1
                    If Registro <= NumMin Then
                        cklClientesTotalizadorSaldos.Items.Add(New ListItem With {.Text = C.DesCliente, .Value = C.OidCliente})
                    ElseIf Registro > NumMin AndAlso Registro <= (NumMin * 2) Then
                        cklClientesTotalizadorSaldos2.Items.Add(New ListItem With {.Text = C.DesCliente, .Value = C.OidCliente})
                    ElseIf Registro > (NumMin * 2) Then
                        cklClientesTotalizadorSaldos3.Items.Add(New ListItem With {.Text = C.DesCliente, .Value = C.OidCliente})
                    End If
                Next

            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub CargarRenderizador()
        Try
            ddlRenderizador.Items.Clear()

            Dim lstRenderizador As String() = [Enum].GetNames(GetType(Enumeradores.TipoRenderizador))

            ddlRenderizador.Items.Add(Traduzir("gen_selecione"))
            For Each tipoRenderizador In lstRenderizador
                If tipoRenderizador <> Enumeradores.TipoRenderizador.NoDefinido.ToString() Then
                    ddlRenderizador.Items.Add(tipoRenderizador)
                End If
            Next
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub CargarParametros()
        Try
            cklParametros.Items.Clear()
            If Not String.IsNullOrEmpty(cklTiposReporte.SelectedValue) Then
                Dim lstParametros = LogicaNegocio.GenesisSaldos.ParametroReporte.RecuperarParametrosPorTipo(cklTiposReporte.SelectedValue)
                If lstParametros IsNot Nothing Then

                    For Each objParametro In lstParametros

                        cklParametros.Items.Add(New ListItem(objParametro.Descripcion, objParametro.Identificador))
                        If ConfigReporteGrabado IsNot Nothing AndAlso ConfigReporteGrabado.ParametrosReporte IsNot Nothing AndAlso ConfigReporteGrabado.ParametrosReporte.Any(Function(a) a.Identificador = objParametro.Identificador) Then
                            cklParametros.Items(cklParametros.Items.Count - 1).Selected = True
                        End If

                    Next
                End If
            End If
            
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub AjustarAcciones()

        AddHandler Acciones.onAccionCancelar, AddressOf Acciones_onAccionCancelar
        AddHandler Acciones.onAccionGuardar, AddressOf Acciones_onAccionGuardar
        Acciones.btnCancelarVisible = True
        Acciones.btnGuardarVisible = True

    End Sub
    Private Sub Acciones_onAccionCancelar()
        Try

            If Master.Historico.Count > 1 Then
                Response.Redireccionar(Master.Historico(Master.Historico.Count - 2).Key)
            Else
                Response.Redireccionar(Constantes.NOME_PAGINA_MENU)
            End If
            'End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub
    Private Sub Acciones_onAccionGuardar()
        Try
            If ValidarCamposObligatorios() Then

                Dim objPeticion As New Prosegur.Genesis.Comon.Clases.ConfiguracionReporte

                objPeticion.Codigo = txtCodigo.Text
                objPeticion.Descripcion = txtDescricao.Text
                objPeticion.Direccion = txtDireccion.Text
                objPeticion.UsuarioCreacion = Parametros.Permisos.Usuario.Login
                objPeticion.FechaHoraCreacion = DateTime.UtcNow
                Dim strItensSelecionados As String = String.Empty
                If cklTiposClientes IsNot Nothing AndAlso (From tb As ListItem In cklTiposClientes.Items Where tb.Selected).Count > 0 Then
                    strItensSelecionados = Join((From tb As ListItem In cklTiposClientes.Items Where tb.Selected Select tb.Value).ToArray(), ",")
                    objPeticion.TiposClientes = New ObservableCollection(Of Clases.TipoCliente)

                    For Each itemselecionado In cklTiposClientes.Items
                        If DirectCast(itemselecionado, ListItem).Selected Then
                            objPeticion.TiposClientes.Add(New Clases.TipoCliente With {.Identificador = DirectCast(itemselecionado, System.Web.UI.WebControls.ListItem).Value})
                        End If
                    Next
                End If

                If cklTiposReporte IsNot Nothing AndAlso (From tb As ListItem In cklTiposReporte.Items Where tb.Selected).Count > 0 Then
                    Dim objTipoReporteSeleccionado As ListItem = (From tb As ListItem In cklTiposReporte.Items Where tb.Selected).FirstOrDefault

                    objPeticion.TipoReporte = Extenciones.RecuperarEnum(Of Comon.Enumeradores.TipoReporte)(objTipoReporteSeleccionado.Value)
                End If

                objPeticion.Clientes = New ObservableCollection(Of Clases.Cliente)
                If cklClientesTotalizadorSaldos IsNot Nothing AndAlso (From tb As ListItem In cklClientesTotalizadorSaldos.Items Where tb.Selected).Count > 0 Then
                    For Each itemselecionado In cklClientesTotalizadorSaldos.Items
                        If DirectCast(itemselecionado, ListItem).Selected Then
                            objPeticion.Clientes.Add(New Clases.Cliente With {.Identificador = DirectCast(itemselecionado, System.Web.UI.WebControls.ListItem).Value})
                        End If
                    Next
                End If
                If cklClientesTotalizadorSaldos2 IsNot Nothing AndAlso (From tb As ListItem In cklClientesTotalizadorSaldos2.Items Where tb.Selected).Count > 0 Then
                    For Each itemselecionado In cklClientesTotalizadorSaldos2.Items
                        If DirectCast(itemselecionado, ListItem).Selected Then
                            objPeticion.Clientes.Add(New Clases.Cliente With {.Identificador = DirectCast(itemselecionado, System.Web.UI.WebControls.ListItem).Value})
                        End If
                    Next
                End If
                If cklClientesTotalizadorSaldos3 IsNot Nothing AndAlso (From tb As ListItem In cklClientesTotalizadorSaldos3.Items Where tb.Selected).Count > 0 Then
                    For Each itemselecionado In cklClientesTotalizadorSaldos3.Items
                        If DirectCast(itemselecionado, ListItem).Selected Then
                            objPeticion.Clientes.Add(New Clases.Cliente With {.Identificador = DirectCast(itemselecionado, System.Web.UI.WebControls.ListItem).Value})
                        End If
                    Next
                End If

                If objPeticion.TipoReporte <> Enumeradores.TipoReporte.Certificacion Then
                    objPeticion.CodigoRedenrizador = [Enum].Parse(GetType(Enumeradores.TipoRenderizador), ddlRenderizador.SelectedValue)
                    objPeticion.MascaraNombre = txtMascaraNombre.Text
                    If objPeticion.CodigoRedenrizador = Enumeradores.TipoRenderizador.CSV Then
                        objPeticion.DescripcionExtension = txtExtensaoArquivo.Text
                    End If
                    objPeticion.DescripcionSeparador = txtSeparadorArquivo.Text
                End If

                objPeticion.ParametrosReporte = New ObservableCollection(Of Clases.ParametroReporte)
                For Each itemselecionado As ListItem In cklParametros.Items
                    If itemselecionado.Selected Then
                        objPeticion.ParametrosReporte.Add(New Clases.ParametroReporte With {.Identificador = itemselecionado.Value})
                    End If
                Next

                If Modo = Enumeradores.Modo.Alta Then
                    LogicaNegocio.GenesisSaldos.FormulariosCertificados.InserirFormulariosCertificado(objPeticion)

                ElseIf Modo = Enumeradores.Modo.Modificacion Then
                    objPeticion.Identificador = hidIdendificador.Value

                    LogicaNegocio.GenesisSaldos.FormulariosCertificados.AlterarFormulariosCertificado(objPeticion)

                ElseIf Modo = Enumeradores.Modo.Baja Then
                    objPeticion.Identificador = hidIdendificador.Value
                    LogicaNegocio.GenesisSaldos.FormulariosCertificados.InserirFormulariosCertificado(objPeticion)

                End If

                Response.Redireccionar("FormulariosCertificados.aspx")

            End If

        Catch ex As Excepciones.ExcepcionLogica
            MyBase.MostraMensagemErro(ex.Message)
        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Message)
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub cklTiposReporte_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cklTiposReporte.SelectedIndexChanged
        Try

            CargarParametros()

        Catch ex As Excepciones.ExcepcionLogica
            MyBase.MostraMensagemErro(ex.Message)
        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Message)
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

End Class