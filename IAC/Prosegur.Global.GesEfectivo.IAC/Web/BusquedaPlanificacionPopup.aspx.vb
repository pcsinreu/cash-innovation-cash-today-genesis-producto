Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Paginacion
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports DevExpress.Web.ASPxGridView

Public Class BusquedaPlanificacionPopup
    Inherits Base

    <Serializable()>
    Public Class Planificacion

        Public Property Planificacion As Comon.Clases.Planificacion

        Public Property Programacion As List(Of PlanXProgramacion)
        Public Property BancoSelecionados As ObservableCollection(Of Comon.Clases.Cliente)
        Public Property OidTipoPlanificacao As String
    End Class

    <Serializable()>
    Public Class PlanXProgramacion

        Public Property OidPlanificacion As String
        Public Property CodPlanificacion As String
        Public Property DesPlanificacion As String
        Public Property FyhVigenciaInicio As DateTime
        Public Property FyhVigenciaFin As DateTime
        Public Property OidBanco As String
        Public Property CodBanco As String
        Public Property DesBanco As String
        Public Property FyhLunes As String
        Public Property FyhMartes As String
        Public Property FyhMiercoles As String
        Public Property FyhJueves As String
        Public Property FyhViernes As String
        Public Property FyhSabado As String
        Public Property FyhDomingo As String
        Public Property PlanVigente As Boolean

    End Class

#Region "[PROPRIEDADES]"
    Public Property ViewStatePlanificacionEntrada() As Planificacion
        Get
            Return Session("PlanificacionEntrada")
        End Get
        Set(value As Planificacion)
            Session("PlanificacionEntrada") = value
        End Set
    End Property

    Public Property ViewStatePlanificacionOriginal() As Planificacion
        Get
            Return ViewState("PlanificacionOriginal")
        End Get
        Set(value As Planificacion)
            ViewState("PlanificacionOriginal") = value
        End Set
    End Property

#End Region

#Region "[Propriedades]"

    Private ReadOnly Property divModal() As String
        Get
            Return Request.QueryString("divModal").ToString()
        End Get
    End Property
    Private ReadOnly Property ifrModal() As String
        Get
            Return Request.QueryString("ifrModal").ToString()
        End Get
    End Property
    Private ReadOnly Property btnExecutar() As String
        Get
            Return Request.QueryString("btnExecutar").ToString()
        End Get
    End Property

#End Region

#Region "[HelpersCliente]"
    Public Property Clientes As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucBancos.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucBancos.Clientes = value
        End Set
    End Property

    Private WithEvents _ucBancos As ucCliente
    Public Property ucBancos() As ucCliente
        Get
            If _ucBancos Is Nothing Then
                _ucBancos = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucBancos.ID = Me.ID & "_ucBancos"
                AddHandler _ucBancos.Erro, AddressOf ErroControles
                phBanco.Controls.Add(_ucBancos)
            End If
            Return _ucBancos
        End Get
        Set(value As ucCliente)
            _ucBancos = value
        End Set
    End Property

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Private Sub ConfigurarControle_Banco()

        Me.ucBancos.SelecaoMultipla = False
        Me.ucBancos.ClienteHabilitado = True
        Me.ucBancos.ClienteObrigatorio = True
        Me.ucBancos.TipoBanco = True
        Me.ucBancos.ClienteTitulo = MyBase.RecuperarValorDic("lbl_banco")

        If Clientes IsNot Nothing Then
            Me.ucBancos.Clientes = Clientes
        End If

    End Sub

    Private Sub ucBancos_OnControleAtualizado() Handles _ucBancos.UpdatedControl
        Try
            If ucBancos.Clientes IsNot Nothing Then
                Clientes = ucBancos.Clientes
                BuscarDados(Nothing)
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()
        'DefinirRetornoFoco(btnGrabar, txtIdentificador)
    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        'Define como ação inical
        Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        'MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CODIGO_AJENO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
        'Pegando a ação
        MyBase.Acao = Request.QueryString("acao")
        MyBase.CodFuncionalidad = "ABM_MAE"
    End Sub

    Protected Overrides Sub Inicializar()

        Try

            ASPxGridView.RegisterBaseScript(Page)


            If Not Page.IsPostBack Then
                Clientes = Nothing

                'Consome os ojetos passados
                ConsomeObjeto()

                If (ViewStatePlanificacionEntrada Is Nothing) Then

                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception("err_passagem_parametro")

                End If
                Dim oidPlanificacion As String = Nothing
                If ViewStatePlanificacionEntrada.Planificacion IsNot Nothing Then
                    oidPlanificacion = ViewStatePlanificacionEntrada.Planificacion.Identificador
                End If

                BuscarDados(oidPlanificacion)

                SelecionaItemGrid()

            End If

            ConfigurarControle_Banco()

            ' trata o foco dos campos
            TrataFoco()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = MyBase.RecuperarValorDic("mod_planificacion_titulo")
        lblSubTitulo.Text = MyBase.RecuperarValorDic("mod_planificacion_subtitulo")
        lblNombre.Text = MyBase.RecuperarValorDic("lbl_nombre")
        btnSeleccionar.Text = MyBase.RecuperarValorDic("btnSeleccionar")
        btnSeleccionar.ToolTip = MyBase.RecuperarValorDic("btnSeleccionar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")

        'Grid
        GdvPlanificacion.Columns(1).HeaderText = MyBase.RecuperarValorDic("lbl_grd_nombre")
        GdvPlanificacion.Columns(2).HeaderText = MyBase.RecuperarValorDic("lbl_grd_banco")
        GdvPlanificacion.Columns(3).HeaderText = MyBase.RecuperarValorDic("lbl_grd_lunes")
        GdvPlanificacion.Columns(4).HeaderText = MyBase.RecuperarValorDic("lbl_grd_martes")
        GdvPlanificacion.Columns(5).HeaderText = MyBase.RecuperarValorDic("lbl_grd_miercoles")
        GdvPlanificacion.Columns(6).HeaderText = MyBase.RecuperarValorDic("lbl_grd_jueves")
        GdvPlanificacion.Columns(7).HeaderText = MyBase.RecuperarValorDic("lbl_grd_viernes")
        GdvPlanificacion.Columns(8).HeaderText = MyBase.RecuperarValorDic("lbl_grd_sabado")
        GdvPlanificacion.Columns(9).HeaderText = MyBase.RecuperarValorDic("lbl_grd_domingo")

    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

    Private Sub btnSeleccionar_Click(sender As Object, e As System.EventArgs) Handles btnSeleccionar.Click

        Try
            Dim objPlanificacion As Comon.Clases.Planificacion = Nothing
            For Each dtRow As GridViewRow In GdvPlanificacion.Rows
                Dim rdbSelecionado As RadioButton = dtRow.Cells(0).FindControl("rdbSelecionado")
                Dim lbloidPlanificacion As Label = dtRow.Cells(0).FindControl("lbloidPlanificacion")
                If rdbSelecionado IsNot Nothing AndAlso (rdbSelecionado.Checked OrElse (hdnSelecionado IsNot Nothing AndAlso hdnSelecionado.Value = rdbSelecionado.ClientID)) Then
                    Dim objPlanProg As PlanXProgramacion = ViewStatePlanificacionEntrada.Programacion.Find(Function(a) a.OidPlanificacion = lbloidPlanificacion.Text)
                    objPlanificacion = New Comon.Clases.Planificacion
                    With objPlanificacion
                        .Identificador = objPlanProg.OidPlanificacion
                        .Codigo = objPlanProg.CodPlanificacion
                        .Descripcion = objPlanProg.DesPlanificacion
                        .FechaHoraVigenciaInicio = objPlanProg.FyhVigenciaInicio
                        .FechaHoraVigenciaFin = objPlanProg.FyhVigenciaFin
                        .BolActivo = objPlanProg.PlanVigente
                        .Cliente = New Comon.Clases.Cliente
                        .Cliente.Identificador = objPlanProg.OidBanco
                        .Cliente.Codigo = objPlanProg.CodBanco
                        .Cliente.Descripcion = objPlanProg.DesBanco
                        .Canales = LogicaNegocio.AccionPlanificacion.GetCanales(objPlanProg.OidPlanificacion)
                    End With
                End If
            Next
            SeleccionarPlanificacion(objPlanificacion)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub SeleccionarPlanificacion(objPlanificacion As Comon.Clases.Planificacion)
        Me.ViewStatePlanificacionEntrada.Planificacion = objPlanificacion
        ViewStatePlanificacionEntrada.BancoSelecionados = ucBancos.Clientes
        ' aqui deve gravar na sessão
        Session("objBusquedaPlanificacion") = Me.ViewStatePlanificacionEntrada

        Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
        ' fechar janela
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "BusquedaPlanificacion", jsScript, True)

    End Sub

    Protected Sub ProsegurGridView_EPreencheDados(sender As Object, e As EventArgs) Handles GdvPlanificacion.EPreencheDados

        Dim objDT As DataTable

        objDT = GdvPlanificacion.ConvertListToDataTable(GetBusqueda(Nothing).Planificacion)

        If GdvPlanificacion.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " desPlanificacion ASC "
        Else
            objDT.DefaultView.Sort = GdvPlanificacion.SortCommand
        End If

        GdvPlanificacion.ControleDataSource = objDT

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click

        Try
            Dim objPlanificacion As Comon.Clases.Planificacion = Nothing
            For Each dtRow As GridViewRow In GdvPlanificacion.Rows
                Dim rdbSelecionado As RadioButton = dtRow.Cells(0).FindControl("rdbSelecionado")
                Dim lbloidPlanificacion As Label = dtRow.Cells(0).FindControl("lbloidPlanificacion")
                If rdbSelecionado IsNot Nothing AndAlso rdbSelecionado.Checked Then
                    Dim objPlanProg As PlanXProgramacion = ViewStatePlanificacionEntrada.Programacion.Find(Function(a) a.OidPlanificacion = lbloidPlanificacion.Text)
                    objPlanificacion = New Comon.Clases.Planificacion
                    With objPlanificacion
                        .Identificador = objPlanProg.OidPlanificacion
                        .Codigo = objPlanProg.CodPlanificacion
                        .Descripcion = objPlanProg.DesPlanificacion
                        .FechaHoraVigenciaInicio = objPlanProg.FyhVigenciaInicio
                        .FechaHoraVigenciaFin = objPlanProg.FyhVigenciaFin
                        .Cliente = New Comon.Clases.Cliente
                        .Cliente.Identificador = objPlanProg.OidBanco
                        .Cliente.Codigo = objPlanProg.CodBanco
                        .Cliente.Descripcion = objPlanProg.DesBanco
                    End With
                End If
            Next

            Me.ViewStatePlanificacionEntrada.Planificacion = objPlanificacion

            Dim jsScript As String = "window.parent.FecharModal(" & Chr(34) & "#" & divModal & Chr(34) & "," & Chr(34) & "#" & ifrModal & Chr(34) & ",null);"

            If ViewStatePlanificacionOriginal IsNot Nothing AndAlso ViewStatePlanificacionEntrada IsNot Nothing Then
                If Aplicacao.Util.Utilidad.HayModificaciones(ViewStatePlanificacionEntrada.Planificacion, ViewStatePlanificacionOriginal.Planificacion) Then
                    MyBase.ExibirMensagemSimNao(MyBase.RecuperarValorDic("MSG_INFO_CERRAR_PANTALLA_PLANIFICACION"), jsScript)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoCamposExtras", jsScript, True)
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoCamposExtras", jsScript, True)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

    Protected Sub txtDesPlanificacion_TextChanged(sender As Object, e As EventArgs) Handles txtDesPlanificacion.TextChanged
        BuscarDados(Nothing)
    End Sub


#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Controla o estado dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 Criado
    ''' </history>
    Public Sub ControleBotoes()

        MyBase.Acao = Request.QueryString("acao")

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta

            Case Aplicacao.Util.Utilidad.eAcao.Baja
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                setConsultar()

            Case Aplicacao.Util.Utilidad.eAcao.Busca

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion


            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Inicial


        End Select

    End Sub

    Private Sub setConsultar()

        btnSeleccionar.Enabled = False
        btnSeleccionar.Visible = False

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    Public Sub ConsomeObjeto()

        If Session("objBusquedaPlanificacion") IsNot Nothing Then

            ViewStatePlanificacionEntrada = CType(Session("objBusquedaPlanificacion"), Planificacion)
            ViewStatePlanificacionOriginal = CType(Session("objBusquedaPlanificacion"), Planificacion)
            Clientes = ViewStatePlanificacionEntrada.BancoSelecionados
            ViewStatePlanificacionEntrada.BancoSelecionados = Nothing
            'Remove da sessão
            Session("objBusquedaPlanificacion") = Nothing

        End If

    End Sub



    Private Sub BuscarDados(oidPlanificacion As String)
        Try
            ViewStatePlanificacionEntrada.BancoSelecionados = Nothing

            ' setar ação de busca
            Acao = Aplicacao.Util.Utilidad.eAcao.Busca

            Dim objRespuesta As New IAC.ContractoServicio.Planificacion.GetPlanificaciones.Respuesta

            objRespuesta = GetBusqueda(oidPlanificacion)

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                MyBase.MostraMensagemErro(objRespuesta.MensajeError, Nothing)
                Exit Sub
            End If


            ' define a ação de busca somente se houve retorno
            If objRespuesta.Planificacion IsNot Nothing AndAlso objRespuesta.Planificacion.Count > 0 Then
                If objRespuesta.Planificacion.Count > 1 Then

                    ViewStatePlanificacionEntrada.Programacion = New List(Of PlanXProgramacion)
                    For Each objPlanificacion In objRespuesta.Planificacion
                        Dim programacion As New PlanXProgramacion

                        programacion.OidPlanificacion = objPlanificacion.OidPlanificacion
                        programacion.CodPlanificacion = objPlanificacion.CodPlanificacion
                        programacion.DesPlanificacion = objPlanificacion.DesPlanificacion
                        programacion.OidBanco = objPlanificacion.OidBanco
                        programacion.CodBanco = objPlanificacion.CodBanco
                        programacion.DesBanco = objPlanificacion.DesBanco
                        programacion.FyhLunes = objPlanificacion.FyhLunes
                        programacion.FyhMartes = objPlanificacion.FyhMartes
                        programacion.FyhMiercoles = objPlanificacion.FyhMiercoles
                        programacion.FyhJueves = objPlanificacion.FyhJueves
                        programacion.FyhViernes = objPlanificacion.FyhViernes
                        programacion.FyhSabado = objPlanificacion.FyhSabado
                        programacion.FyhDomingo = objPlanificacion.FyhDomingo
                        programacion.PlanVigente = objPlanificacion.BolActivo

                        'Vigencia já convertida no GMT da delegação da planificação
                        programacion.FyhVigenciaInicio = objPlanificacion.FechaHoraVigenciaInicio
                        programacion.FyhVigenciaFin = objPlanificacion.FechaHoraVigenciaFin

                        ViewStatePlanificacionEntrada.Programacion.Add(programacion)
                    Next

                    pnlSemRegistro.Visible = False

                    ' converter objeto para datatable
                    Dim objDt As DataTable = GdvPlanificacion.ConvertListToDataTable(ViewStatePlanificacionEntrada.Programacion)

                    If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                        objDt.DefaultView.Sort = " desPlanificacion ASC"
                    ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                        If GdvPlanificacion.SortCommand.Equals(String.Empty) Then
                            objDt.DefaultView.Sort = " desPlanificacion ASC "
                        Else
                            objDt.DefaultView.Sort = GdvPlanificacion.SortCommand
                        End If

                    Else
                        objDt.DefaultView.Sort = GdvPlanificacion.SortCommand
                    End If

                    ' carregar controle
                    GdvPlanificacion.CarregaControle(objDt)

                Else
                    'Se retornar apenas um registro, o planejamento é selecionado automaticamente
                    Dim objPlanProg = objRespuesta.Planificacion.FirstOrDefault()
                    Dim planificacionComon = New Comon.Clases.Planificacion

                    With planificacionComon
                        .Identificador = objPlanProg.OidPlanificacion
                        .Codigo = objPlanProg.CodPlanificacion
                        .Descripcion = objPlanProg.DesPlanificacion
                        .FechaHoraVigenciaInicio = objPlanProg.FechaHoraVigenciaInicio
                        .FechaHoraVigenciaFin = objPlanProg.FechaHoraVigenciaFin
                        .Cliente = New Comon.Clases.Cliente
                        .Cliente.Identificador = objPlanProg.OidBanco
                        .Cliente.Codigo = objPlanProg.CodBanco
                        .Cliente.Descripcion = objPlanProg.DesBanco
                        .BolActivo = objPlanProg.BolActivo
                        .BolControlaFacturacion = objPlanProg.BolControlaFacturacion
                        .Canales = LogicaNegocio.AccionPlanificacion.GetCanales(objPlanProg.OidPlanificacion)
                    End With

                    SeleccionarPlanificacion(planificacionComon)

                End If
            Else

                'Limpa a consulta
                GdvPlanificacion.DataSource = Nothing
                GdvPlanificacion.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Function GetBusqueda(oidPlanificacion) As IAC.ContractoServicio.Planificacion.GetPlanificaciones.Respuesta
        Dim objAccionPlanificacion As New LogicaNegocio.AccionPlanificacion
        Dim objPeticion As New IAC.ContractoServicio.Planificacion.GetPlanificaciones.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Planificacion.GetPlanificaciones.Respuesta

        If Not String.IsNullOrEmpty(oidPlanificacion) Then
            objPeticion.OidPlanificacion = ViewStatePlanificacionOriginal.Planificacion.Identificador
        Else
            If ucBancos.Clientes IsNot Nothing AndAlso ucBancos.Clientes.Count > 0 Then
                objPeticion.OidBanco = ucBancos.Clientes.First.Identificador
            End If
            objPeticion.DesPlanificacion = txtDesPlanificacion.Text
        End If

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.Vigente = True

        objPeticion.OidTipoPlanificacion = ViewStatePlanificacionEntrada.OidTipoPlanificacao

        If Not String.IsNullOrEmpty(objPeticion.OidBanco) _
                OrElse Not String.IsNullOrEmpty(objPeticion.DesPlanificacion) _
                    OrElse Not String.IsNullOrEmpty(objPeticion.OidPlanificacion) Then
            objRespuesta = objAccionPlanificacion.GetPlanificaciones(objPeticion)
        End If

        Return objRespuesta
    End Function

    Private Sub SelecionaItemGrid()
        Try
            If GdvPlanificacion.Rows.Count > 0 AndAlso ViewStatePlanificacionEntrada IsNot Nothing _
                    AndAlso ViewStatePlanificacionEntrada.Planificacion IsNot Nothing _
                        AndAlso Not String.IsNullOrEmpty(ViewStatePlanificacionEntrada.Planificacion.Identificador) Then

                Dim objPlanificacion As Comon.Clases.Planificacion = Nothing
                For Each dtRow As GridViewRow In GdvPlanificacion.Rows
                    Dim rdbSelecionado As RadioButton = dtRow.Cells(0).FindControl("rdbSelecionado")
                    Dim lbloidPlanificacion As Label = dtRow.Cells(0).FindControl("lbloidPlanificacion")
                    If lbloidPlanificacion.Text = ViewStatePlanificacionEntrada.Planificacion.Identificador Then
                        rdbSelecionado.Checked = True
                    End If
                Next

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region


End Class