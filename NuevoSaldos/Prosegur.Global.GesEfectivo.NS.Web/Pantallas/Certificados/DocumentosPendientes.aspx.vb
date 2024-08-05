Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Web.Login

Public Class DocumentosPendientes
    Inherits Base


#Region "[PROPRIEDADES]"

    Private _identificadorSector As String = Nothing
    Public ReadOnly Property identificadorSector() As String
        Get
            If String.IsNullOrEmpty(_identificadorSector) Then
                _identificadorSector = Request.QueryString("identificadorSector")
            End If
            Return _identificadorSector
        End Get
    End Property

    Public Property Sectores As ObservableCollection(Of Clases.Sector)
        Get
            Return ucSectores.Sectores
        End Get
        Set(value As ObservableCollection(Of Clases.Sector))
            ucSectores.Sectores = value
        End Set
    End Property

    Public Property Plantas As ObservableCollection(Of Clases.Planta)
        Get
            Return ucSectores.Plantas
        End Get
        Set(value As ObservableCollection(Of Clases.Planta))
            ucSectores.Plantas = value
        End Set
    End Property

    Public Property Delegaciones As ObservableCollection(Of Clases.Delegacion)
        Get
            Return ucSectores.Delegaciones
        End Get
        Set(value As ObservableCollection(Of Clases.Delegacion))
            ucSectores.Delegaciones = value
        End Set
    End Property

    Public Property Clientes As ObservableCollection(Of Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl("~\Controles\ucCliente.ascx")
                _ucClientes.ID = Me.ID & "_ucClientes"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Private WithEvents _ucSectores As ucSector
    Public Property ucSectores() As ucSector
        Get
            If _ucSectores Is Nothing Then
                _ucSectores = LoadControl("~\Controles\ucSector.ascx")
                _ucSectores.ID = Me.ID & "_ucSectores"
                AddHandler _ucSectores.Erro, AddressOf ErroControles
                phSector.Controls.Add(_ucSectores)
            End If
            Return _ucSectores
        End Get
        Set(value As ucSector)
            _ucSectores = value
        End Set
    End Property

    Public Property ultimaConsulta() As Peticion(Of Clases.Transferencias.FiltroDocumentos)
        Get
            Return Session("ultimaConsultaDocumento")
        End Get
        Set(value As Peticion(Of Clases.Transferencias.FiltroDocumentos))
            Session("ultimaConsultaDocumento") = value
        End Set
    End Property

    Public Property RespuestaDados() As Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))
        Get
            Return ViewState("_RespuestaDados")
        End Get
        Set(value As Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento)))
            ViewState("_RespuestaDados") = value
        End Set
    End Property


    Private _Acciones As ucAcciones
    Public ReadOnly Property Acciones() As ucAcciones
        Get
            If _Acciones Is Nothing Then
                _Acciones = LoadControl("~\Controles\UcAcciones.ascx")
                _Acciones.ID = "Acciones"
                AddHandler _Acciones.Erro, AddressOf ErroControles
                phAcciones.Controls.Add(_Acciones)
            End If
            Return _Acciones
        End Get
    End Property
#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub TraduzirControles()
        ' Titulos FieldSet
        lblTitulo.Text = Traduzir("067_lblTituloFiltro")
        lblTitulo1.Text = Traduzir("067_lblTituloResultado")

        '' Campos Filtro
        Me.lblFechaCertDesde.Text = Traduzir("067_lblFechaCertDesde")
        Me.lblFechaCertHasta.Text = Traduzir("067_lblFechaCertHasta")
        Me.lblTipoDocumento.Text = Traduzir("067_lblTipoDocumento")
        Me.chkIncDocSinFechaCert.Text = Traduzir("067_chkIncDocSinFechaCert")
        Me.chkIncDocNoCert.Text = Traduzir("067_chkIncDocNoCert")

        lblSemRegistro.Text = Traduzir("lblSemRegistro")

        btnBuscar.Text = Traduzir("btnBuscar")

        Master.Titulo = Traduzir("067_Titulo")

    End Sub

    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
        Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFechaCertDesde.ClientID, "True")
        script &= String.Format("AbrirCalendario('{0}','{1}');", txtFechaCertHasta.ClientID, "True")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CERTIFICADOS_DOCUMENTOS_PENDIENTES_MODIFICAR
        MyBase.ValidarAcesso = True
        MyBase.ValidarPemissaoAD = True
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Try
            Me.ConfigurarControle_Sector()
            Me.ConfigurarControle_Cliente()
            Me.ConfiguraAcciones()

            If Not Me.IsPostBack Then

                Me.ucSectores.Focus()
                CarregaDropDownTipoDocumentos()

            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim mensagem As String = ValidarBusca()

            If Not String.IsNullOrEmpty(mensagem) Then
                MyBase.MostraMensagemErro(mensagem)
            Else
                Me.RespuestaDados = Nothing
                grvResultadoDocumentos.DataSource = Nothing
                grvResultadoDocumentos.PageIndex = 0
                ultimaConsulta = Nothing
                PopularGridView()
                grvResultadoDocumentos.DataBind()
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub grvResultadoDocumentos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grvResultadoDocumentos.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = Traduzir("067_grid_codigo_externo")
            e.Row.Cells(1).Text = Traduzir("067_grid_codigo_Comprovante")
            e.Row.Cells(2).Text = Traduzir("067_grid_formulario")
            e.Row.Cells(3).Text = Traduzir("067_grid_cliente")
            e.Row.Cells(4).Text = Traduzir("067_grid_subcliente")
            e.Row.Cells(5).Text = Traduzir("067_grid_subcanal")
            e.Row.Cells(6).Text = Traduzir("067_grid_sector_origem")
            e.Row.Cells(7).Text = Traduzir("067_grid_sector_destino")
            e.Row.Cells(8).Text = Traduzir("067_grid_fecha_hora_plan_cert")
            e.Row.Cells(9).Text = Traduzir("067_grid_excluir")
            e.Row.Cells(10).Text = Traduzir("067_grid_documento")
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            Dim objDocumento = DirectCast(e.Row.DataItem, Clases.Transferencias.DocumentoGrupoDocumento)

            Dim txtFechaPlanCert As TextBox = e.Row.FindControl("txtFechaPlanCert")
            txtFechaPlanCert.Enabled = Not objDocumento.EsGeneracionF22

            If txtFechaPlanCert IsNot Nothing AndAlso Not objDocumento.EsGeneracionF22 Then

                Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFechaPlanCert.ClientID, "True")
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA_CALENDAR_" + txtFechaPlanCert.ClientID, script, True)

            Else
                txtFechaPlanCert.Style.Add("margin-right", "23px")
            End If

            If objDocumento IsNot Nothing Then

                If objDocumento.FechaPlanCertificacion = DateTime.MinValue Then
                    txtFechaPlanCert.Text = ""
                End If

                Dim redirecionaDocumento As ImageButton = e.Row.FindControl("redirecionaDocumento")

                If redirecionaDocumento IsNot Nothing Then

                    Dim scriptPopup As String = Me.RetornaAbrirPopup("../Documento.aspx", "IdentificadorDocumento=" & objDocumento.Identificador + "&Modo=" + Enumeradores.Modo.Consulta.ToString())
                    redirecionaDocumento.OnClientClick = scriptPopup + " return false;"
                End If

            End If

        End If
    End Sub

    Private Sub grvResultadoDocumentos_PageIndexChanged(sender As Object, e As System.EventArgs) Handles grvResultadoDocumentos.PageIndexChanged
        ultimaConsulta = Nothing
    End Sub

    Private Sub Acciones_onAccionGuardar()
        Try
            Dim lstAlterados = RetornaAlterados()

            If lstAlterados IsNot Nothing AndAlso lstAlterados.Count > 0 Then

                For Each objDocumento In lstAlterados
                    If objDocumento.FechaPlanCertificacion <> DateTime.MinValue Then

                        If Not LogicaNegocio.GenesisSaldos.Certificacion.AccionValidaCertificacion.EsFechaHoraPlanCertificacionValidaPorCuentaSaldo(objDocumento.CuentaSaldoOrigen, objDocumento.CuentaSaldoDestino, objDocumento.FechaPlanCertificacion) Then
                            MyBase.MostraMensagemErro(Traduzir("067_validaFechaHoraCertPlan"))
                            Exit Sub
                        End If

                    End If
                Next

                LogicaNegocio.GenesisSaldos.MaestroDocumentos.ActualizarDocumentosPendientes(lstAlterados)

            End If

            Dim script As String = "alert('" + Traduzir("067_registro_salvo") + "');"
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "registro_salvo", script, True)

            Me.RespuestaDados = Nothing
            grvResultadoDocumentos.DataSource = Nothing
            grvResultadoDocumentos.PageIndex = 0
            ultimaConsulta = Nothing
            PopularGridView()
            grvResultadoDocumentos.DataBind()
            upnSearchResults.Update()

        Catch ex As Excepciones.ExcepcionLogica
            MyBase.MostraMensagemErro(ex.Message)
        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Message)
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionCancelar()
        Try

            If Master.Historico.Count > 1 Then
                Response.Redireccionar(Master.Historico(Master.Historico.Count - 2).Key)
            Else
                Response.Redireccionar(Constantes.NOME_PAGINA_MENU)
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Protected Sub txtFechaPlanCert_TextChanged(sender As Object, e As System.EventArgs)
        Dim txtFechaPlanCert As TextBox = sender
        Dim grdRow As GridViewRow = txtFechaPlanCert.DataItemContainer
        Dim identificador As Label = grdRow.FindControl("Identificador")
        Dim fechaPlanCert As Label = grdRow.FindControl("FechaPlanCert")
        If fechaPlanCert.Text <> txtFechaPlanCert.Text Then

            If Not String.IsNullOrEmpty(txtFechaPlanCert.Text) Then

                Dim objDocumento As Clases.Transferencias.DocumentoGrupoDocumento = Me.RespuestaDados.Retorno.Find(Function(a) a.Identificador = identificador.Text)

                If Not LogicaNegocio.GenesisSaldos.Certificacion.AccionValidaCertificacion.EsFechaHoraPlanCertificacionValidaPorCuentaSaldo(objDocumento.CuentaSaldoOrigen, objDocumento.CuentaSaldoDestino, txtFechaPlanCert.Text) Then
                    MyBase.MostraMensagemErro(Traduzir("067_validaFechaHoraCertPlan"))
                    Dim script As String = "DefineFechaPlanCert('" + txtFechaPlanCert.ClientID + "','" + fechaPlanCert.Text + "');"
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "txtFechaPlanCert_TextChanged", script, True)
                    Exit Sub
                End If

            End If

            Dim objCertificado As Clases.Certificado = LogicaNegocio.GenesisSaldos.MaestroDocumentos.ValidaCertificadoProvisional(identificador.Text, Base.InformacionUsuario.DelegacionSeleccionada)
            If objCertificado IsNot Nothing Then
                Dim tipo As String = String.Empty
                Select Case objCertificado.Estado
                    Case Enumeradores.EstadoCertificado.ProvisionalConCierre
                        tipo = Traduzir("067_ProvisionalCon")
                    Case Enumeradores.EstadoCertificado.ProvisionalSinCierre
                        tipo = Traduzir("067_ProvisionalSin")
                End Select

                Dim script As String = "ConfirmaFechaCertificacion('" & String.Format(Traduzir("067_documento_certificado"), tipo, objCertificado.FechaHoraCertificado.ToString("dd/MM/yyyy HH:mm:ss")) & "','" + txtFechaPlanCert.ClientID + "','" + fechaPlanCert.Text + "');"
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "txtFechaPlanCert_TextChanged", script, True)
            End If

        End If

    End Sub

    Protected Sub chkExcluir_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim chkExcluir As CheckBox = sender
        Dim grdRow As GridViewRow = chkExcluir.DataItemContainer
        Dim identificador As Label = grdRow.FindControl("Identificador")
        Dim noCertificar As CheckBox = grdRow.FindControl("NoCertificar")
        If noCertificar.Checked <> chkExcluir.Checked Then

            Dim objCertificado As Clases.Certificado = LogicaNegocio.GenesisSaldos.MaestroDocumentos.ValidaCertificadoProvisional(identificador.Text, Base.InformacionUsuario.DelegacionSeleccionada)
            If objCertificado IsNot Nothing Then
                Dim tipo As String = String.Empty
                Select Case objCertificado.Estado
                    Case Enumeradores.EstadoCertificado.ProvisionalConCierre
                        tipo = Traduzir("067_ProvisionalCon")
                    Case Enumeradores.EstadoCertificado.ProvisionalSinCierre
                        tipo = Traduzir("067_ProvisionalSin")
                End Select

                Dim script As String = "ConfirmaFechaCertificacion('" & String.Format(Traduzir("067_documento_certificado"), tipo, objCertificado.FechaHoraCertificado.ToString("dd/MM/yyyy HH:mm:ss")) & "','" + chkExcluir.ClientID + "','');"
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "chkExcluir_CheckedChanged", script, True)
            End If

        End If
    End Sub

#End Region

#Region "[METODOS]"

#Region "     Helpers     "

    Protected Sub ConfigurarControle_Sector()
        Me.ucSectores.ConsiderarPermissoes = True
        Me.ucSectores.SelecaoMultipla = True
        Me.ucSectores.SectorHabilitado = True
        Me.ucSectores.DelegacionHabilitado = False
        Me.ucSectores.PlantaHabilitado = False
        Me.ucSectores.SolamenteSectoresPadre = True
        Me.ucSectores.ucDelegacion.Visible = False
        Me.ucSectores.ucPlanta.Visible = False
        Delegaciones.Add(Base.InformacionUsuario.DelegacionSeleccionada)
        Plantas.Add(Base.InformacionUsuario.DelegacionSeleccionada.Plantas(0))

        If Delegaciones IsNot Nothing Then
            Me.ucSectores.Delegaciones = Delegaciones
        End If
        If Plantas IsNot Nothing Then
            Me.ucSectores.Plantas = Plantas
        End If
        If Sectores IsNot Nothing Then
            Me.ucSectores.Sectores = Sectores
        End If
    End Sub

    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.SubClienteHabilitado = False
        Me.ucClientes.PtoServicioHabilitado = False
        Me.ucClientes.ucSubCliente.Visible = False
        Me.ucClientes.ucPtoServicio.Visible = False
        Me.ucClientes.TotalizadorSaldo = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub

#End Region

#Region "     General     "

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Public Sub CarregaDropDownTipoDocumentos()
        Try
            Dim selectedValue As String = ddlTipoDocumento.SelectedValue
            ddlTipoDocumento.Items.Clear()
            ddlTipoDocumento.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))

            For Each item In LogicaNegocio.GenesisSaldos.MaestroDocumentos.RecuperarTipoDocumentoCertificacion()
                ddlTipoDocumento.Items.Add(New ListItem(item.Descripcion, item.Identificador))
            Next

            If ddlTipoDocumento.Items Is Nothing OrElse ddlTipoDocumento.Items.Count = 0 Then
                ddlTipoDocumento.Enabled = False
            ElseIf ddlTipoDocumento.Items.FindByValue(selectedValue) IsNot Nothing Then
                ddlTipoDocumento.SelectedValue = selectedValue
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Function MontaFiltro() As Clases.Transferencias.FiltroDocumentos

        Dim objFiltro As New Clases.Transferencias.FiltroDocumentos

        Try
            With objFiltro

                If Me.Clientes IsNot Nothing AndAlso Me.Clientes.Count > 0 Then

                    .Clientes = Me.Clientes.ToList()

                End If

                If Me.Sectores IsNot Nothing AndAlso Me.Sectores.Count > 0 Then

                    .Sectores = Me.Sectores.ToList()

                End If

                If txtFechaCertDesde.Text IsNot Nothing AndAlso txtFechaCertDesde.Text.Length = 19 AndAlso IsDate(txtFechaCertDesde.Text) Then

                    .FechaPlanCertificacionDesde = txtFechaCertDesde.Text

                End If

                If txtFechaCertHasta.Text IsNot Nothing AndAlso txtFechaCertHasta.Text.Length = 19 AndAlso IsDate(txtFechaCertHasta.Text) Then

                    .FechaPlanCertificacionHasta = txtFechaCertHasta.Text

                End If

                If Not String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue) Then

                    .IdentificadorTipoDocumento = ddlTipoDocumento.SelectedValue

                End If

                .BolIncluirDocSinFechaPlan = chkIncDocSinFechaCert.Checked

                .BolIncluirDocNoCertificar = chkIncDocNoCert.Checked

            End With

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

        Return objFiltro

    End Function

    Public Function ObtenerDocumentos(objFiltro As Clases.Transferencias.FiltroDocumentos) As Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))

        Dim objPeticion As New Peticion(Of Clases.Transferencias.FiltroDocumentos)
        objPeticion.ParametrosPaginacion = New Paginacion.ParametrosPeticionPaginacion()
        Dim objRespuesta As Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))

        If ultimaConsulta IsNot Nothing Then
            objPeticion = ultimaConsulta
        Else
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False
            objPeticion.Parametro = objFiltro

            ultimaConsulta = objPeticion
        End If

        objRespuesta = LogicaNegocio.GenesisSaldos.Documento.ObtenerDocumentosPendientes(objPeticion, Base.InformacionUsuario.DelegacionSeleccionada)

        Return objRespuesta
    End Function

    Private Sub PopularGridView()

        Try
            Dim respuesta As Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))

            If Me.RespuestaDados Is Nothing Then

                'Para não executar a busca ao entrar na página                
                Dim objFiltro As Clases.Transferencias.FiltroDocumentos = MontaFiltro()
                respuesta = ObtenerDocumentos(objFiltro)
                Me.RespuestaDados = respuesta

            End If

            If Me.RespuestaDados IsNot Nothing AndAlso Me.RespuestaDados.Retorno IsNot Nothing AndAlso Me.RespuestaDados.Retorno.Count > 0 Then
                dvEmptyData.Style.Item("display") = "none"
                grvResultadoDocumentos.DataSource = Me.RespuestaDados.Retorno
            Else
                dvEmptyData.Style.Item("display") = "block"
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Private Function ValidarBusca() As String
        Dim mensagem As String = String.Empty
        Dim sbMensagens As New StringBuilder

        If Me.Sectores.Count = 0 AndAlso Me.Clientes.Count = 0 Then
            sbMensagens.Append(String.Format(Traduzir("067_validar_cliente_sector"), mensagem))
        End If

        Return sbMensagens.ToString
    End Function

    Private Sub ConfiguraAcciones()

        AddHandler Acciones.onAccionGuardar, AddressOf Acciones_onAccionGuardar
        AddHandler Acciones.onAccionCancelar, AddressOf Acciones_onAccionCancelar

        Acciones.btnGuardarVisible = True
        Acciones.btnCancelarVisible = True

        'Seta o botão default do formulário para ao submetê-lo rodar o evento do botão guardar.
        Me.Form.DefaultButton = Me.Acciones.FindControl("btnGuardar").FindControl("btnBoton").UniqueID
    End Sub

    Private Function RetornaAlterados() As List(Of Clases.Transferencias.DocumentoGrupoDocumento)
        Dim lstAlterados As New List(Of Clases.Transferencias.DocumentoGrupoDocumento)

        For Each grdRow As GridViewRow In grvResultadoDocumentos.Rows

            Dim identificador As String = DirectCast(grdRow.FindControl("identificador"), Label).Text
            Dim chkExcluir As CheckBox = DirectCast(grdRow.FindControl("chkExcluir"), CheckBox)
            Dim txtFechaPlanCert As TextBox = DirectCast(grdRow.FindControl("txtFechaPlanCert"), TextBox)

            Dim objDocumento As Clases.Transferencias.DocumentoGrupoDocumento = Me.RespuestaDados.Retorno.Find(Function(a) a.Identificador = identificador).Clonar()

            If objDocumento IsNot Nothing Then
                If objDocumento.NoCertificar <> chkExcluir.Checked OrElse (Not String.IsNullOrEmpty(txtFechaPlanCert.Text) AndAlso Convert.ToDateTime(txtFechaPlanCert.Text) <> objDocumento.FechaPlanCertificacion) OrElse _
                    (String.IsNullOrEmpty(txtFechaPlanCert.Text) AndAlso objDocumento.FechaPlanCertificacion <> Date.MinValue) Then
                    With objDocumento
                        .NoCertificar = chkExcluir.Checked
                        .FechaPlanCertificacion = If(Not String.IsNullOrEmpty(txtFechaPlanCert.Text), Convert.ToDateTime(txtFechaPlanCert.Text), Nothing)
                        .FechaHoraModificacion = DateTime.UtcNow
                        .UsuarioModificacion = Parametros.Permisos.Usuario.Login
                    End With
                    lstAlterados.Add(objDocumento)
                End If
            End If

        Next

        Return lstAlterados

    End Function
#End Region

#End Region

End Class