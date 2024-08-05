Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Paginacion
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon

Public Class BusquedaCamposExtraModal
    Inherits Base

    <Serializable()>
    Public Class CamposExtras
        Public Property TerminosPatron As Clases.CamposExtrasDeIAC
        Public Property TerminosDinamicos As Clases.CamposExtrasDeIAC
    End Class

#Region "[PROPRIEDADES]"
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

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
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
        MyBase.CodFuncionalidad = "ABM_PLANIFICACION"
    End Sub

    Public Property CamposExtrasPatronesDatos() As Prosegur.Genesis.Comon.Clases.CamposExtrasDeIAC
        Get
            If Session("camposExtrasPatronesDatos") Is Nothing Then
                Session("camposExtrasPatronesDatos") = New Prosegur.Genesis.Comon.Clases.CamposExtrasDeIAC()
            End If
            Return Session("camposExtrasPatronesDatos")
        End Get
        Set(ByVal value As Prosegur.Genesis.Comon.Clases.CamposExtrasDeIAC)
            Session("camposExtrasPatronesDatos") = value
        End Set
    End Property

    Public Property CamposExtrasDinamicosDatos() As Clases.CamposExtrasDeIAC
        Get
            If Session("camposExtrasDinamicosDatos") Is Nothing Then
                Session("camposExtrasDinamicosDatos") = New Clases.CamposExtrasDeIAC()
            End If
            Return Session("camposExtrasDinamicosDatos")
        End Get
        Set(ByVal value As Clases.CamposExtrasDeIAC)
            Session("camposExtrasDinamicosDatos") = value
        End Set
    End Property

    Private Sub ConfiguraInfAdicionales()

        Dim unTermino As Clases.Termino
        Dim iac_patron As String = String.Empty
        Dim iac_dinamico As String = String.Empty

        InfAdicionalesPatron.Modo = Request.QueryString("acao")
        InfAdicionalesDinamico.Modo = Request.QueryString("acao")

        InfAdicionalesDinamico.Terminos = Nothing
        InfAdicionalesPatron.Terminos = Nothing

        If CamposExtrasPatronesDatos IsNot Nothing Then
            iac_patron = CamposExtrasPatronesDatos.CodigoIAC

            Dim terminos = Genesis.LogicaNegocio.Integracion.AccionGrupoTerminoIAC.RecuperarGrupoTerminosIACPorCodigo(iac_patron)
            If terminos IsNot Nothing Then
                InfAdicionalesPatron.Terminos = terminos.TerminosIAC
                If CamposExtrasPatronesDatos.CamposExtras IsNot Nothing Then
                    For Each elementoTermino In InfAdicionalesPatron.Terminos

                        unTermino = CamposExtrasPatronesDatos.CamposExtras.Where(Function(x) x.Codigo = elementoTermino.Codigo).FirstOrDefault

                        If unTermino IsNot Nothing Then
                            elementoTermino.Valor = unTermino.Valor
                        End If
                    Next
                End If
            End If
        End If


        If CamposExtrasDinamicosDatos IsNot Nothing Then
            If CamposExtrasDinamicosDatos.CodigoIAC IsNot Nothing Then
                iac_dinamico = CamposExtrasDinamicosDatos.CodigoIAC
            Else
                If ddlIAC.SelectedItem IsNot Nothing Then
                    iac_dinamico = ddlIAC.SelectedItem.Text
                End If
            End If

            Dim terminos = Genesis.LogicaNegocio.Integracion.AccionGrupoTerminoIAC.RecuperarGrupoTerminosIACPorCodigo(iac_dinamico)
            If terminos IsNot Nothing Then
                InfAdicionalesDinamico.Terminos = terminos.TerminosIAC
                If CamposExtrasDinamicosDatos.CamposExtras IsNot Nothing Then
                    For Each elementoTermino In InfAdicionalesDinamico.Terminos

                        unTermino = CamposExtrasDinamicosDatos.CamposExtras.Where(Function(x) x.Codigo = elementoTermino.Codigo).FirstOrDefault

                        If unTermino IsNot Nothing Then
                            elementoTermino.Valor = unTermino.Valor
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Public Property CodigoIACPatron As String
        Get
            If Session("codigoIACPatron") Is Nothing Then

                Session("codigoIACPatron") = String.Empty
            End If
            Return Session("codigoIACPatron")
        End Get
        Set(value As String)
            Session("codigoIACPatron") = value
        End Set
    End Property

    Protected Overrides Sub Inicializar()

        Try
            If Not Page.IsPostBack Then
                DevExpress.Web.ASPxGridView.ASPxGridView.RegisterBaseScript(Page)
                'La primera vez lo inicializamos = Nothing
                _InfAdicionalesDinamico = Nothing
                _InfAdicionalesPatron = Nothing

                CargarTerminosIAC()

            End If
            ConfiguraInfAdicionales()
        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Private Sub CargarTerminosIAC()
        Try
            If Not IsPostBack Then
                Dim listaDeIACs As List(Of Clases.GrupoTerminosIAC) = Genesis.LogicaNegocio.Integracion.AccionGrupoTerminoIAC.RecuperarIACs()
                Dim strIdentificador As String = String.Empty
                Dim codigoAFiltrar As String = String.Empty
                txtCodigoIACPatron.Text = String.Empty
                If CamposExtrasPatronesDatos IsNot Nothing AndAlso CamposExtrasPatronesDatos.CodigoIAC IsNot Nothing Then

                    codigoAFiltrar = CamposExtrasPatronesDatos.CodigoIAC

                    If listaDeIACs IsNot Nothing AndAlso listaDeIACs.Count > 0 Then
                        If (listaDeIACs.Where(Function(x) x.Codigo = codigoAFiltrar).FirstOrDefault) IsNot Nothing Then
                            txtCodigoIACPatron.Text = CamposExtrasPatronesDatos.CodigoIAC
                            If listaDeIACs IsNot Nothing AndAlso CamposExtrasDinamicosDatos.CodigoIAC IsNot Nothing Then
                                strIdentificador = listaDeIACs.FirstOrDefault(Function(x) x.Codigo = CamposExtrasDinamicosDatos.CodigoIAC).Identificador
                                ddlIAC.SelectedValue = strIdentificador
                            Else
                                ddlIAC.SelectedIndex = 0
                            End If

                        Else
                            txtCodigoIACPatron.Text = String.Empty
                            InfAdicionalesPatron.Terminos = Nothing
                        End If
                    End If

                Else
                    If CamposExtrasPatronesDatos.CodigoIAC Is Nothing AndAlso listaDeIACs IsNot Nothing AndAlso listaDeIACs.Count > 0 Then
                        CamposExtrasDinamicosDatos.CodigoIAC = listaDeIACs.Where(Function(x) x.Codigo <> codigoAFiltrar)(0).Codigo
                    End If

                End If

                ddlIAC.DataSource = listaDeIACs.Where(Function(x) x.Codigo <> codigoAFiltrar)
                ddlIAC.DataTextField = "Codigo"
                ddlIAC.DataValueField = "Identificador"
                ddlIAC.DataBind()

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
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

        Me.Page.Title = MyBase.RecuperarValorDic("mod_titulo_busca_campos_extra")

        'Botoes
        btnAceptar.Text = MyBase.RecuperarChavesDic("btn_grabar")
        btnAceptar.ToolTip = MyBase.RecuperarChavesDic("btn_grabar")
        btnCancelar.Text = MyBase.RecuperarValorDic("btn_cancelar")
        btnCancelar.ToolTip = MyBase.RecuperarValorDic("btn_cancelar")
        lblCamposDinamicos.Text = MyBase.RecuperarValorDic("lblCamposDinamicos")
        lblCamposPatron.Text = MyBase.RecuperarValorDic("lblCamposPatron")

    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"



    Private WithEvents _InfAdicionalesPatron As ucInfAdicionales
    Public ReadOnly Property InfAdicionalesPatron() As ucInfAdicionales
        Get
            If _InfAdicionalesPatron Is Nothing Then
                _InfAdicionalesPatron = LoadControl("~\Controles\Helpers\ucInfAdicionales.ascx")
                _InfAdicionalesPatron.ID = "InfAdicionalesPatron"
                AddHandler _InfAdicionalesPatron.Erro, AddressOf ErroControles
                phInfAdicionalesPatron.Controls.Add(_InfAdicionalesPatron)
            End If
            Return _InfAdicionalesPatron
        End Get
    End Property

    Private WithEvents _InfAdicionalesDinamico As ucInfAdicionales
    Public ReadOnly Property InfAdicionalesDinamico() As ucInfAdicionales
        Get
            If _InfAdicionalesDinamico Is Nothing Then
                _InfAdicionalesDinamico = LoadControl("~\Controles\Helpers\ucInfAdicionales.ascx")
                _InfAdicionalesDinamico.ID = "InfAdicionalesDinamico"
                AddHandler _InfAdicionalesDinamico.Erro, AddressOf ErroControles
                phInfAdicionalesDinamico.Controls.Add(_InfAdicionalesDinamico)
            End If
            Return _InfAdicionalesDinamico
        End Get
    End Property

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click

        Try

            Me.CerrarModal()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub




#End Region





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

            Case Aplicacao.Util.Utilidad.eAcao.Busca

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Inicial


        End Select

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

    Private Sub AtualizaDadosHelperCliente(observableCollection As ObservableCollection(Of Comon.Clases.Cliente), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCliente)
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

        For Each c In observableCollection
            If Not String.IsNullOrEmpty(c.Identificador) Then
                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = c.Identificador
                    .Codigo = c.Codigo
                    .Descricao = c.Descripcion
                End With
                dadosCliente.DatosRespuesta.Add(DadosExibir)
            End If
        Next

        pUserControl.ucCliente.RegistrosSelecionados = dadosCliente
        pUserControl.ucCliente.ExibirDados(True)
    End Sub

    Private Sub ddlIAC_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlIAC.SelectedIndexChanged
        Try
            'MostrarLoading(True)
            Dim oidIAC As String = ddlIAC.SelectedValue
            Dim codigoIAC As String = ddlIAC.SelectedItem.Text
            Dim unTermino As Clases.Termino

            InfAdicionalesDinamico.Modo = Comon.Enumeradores.Modo.Alta
            InfAdicionalesDinamico.Terminos = Nothing
            InfAdicionalesDinamico.Terminos = Genesis.LogicaNegocio.Integracion.AccionGrupoTerminoIAC.RecuperarGrupoTerminosIACPorCodigo(codigoIAC).TerminosIAC
            If CamposExtrasDinamicosDatos IsNot Nothing AndAlso CamposExtrasDinamicosDatos.CamposExtras IsNot Nothing AndAlso InfAdicionalesDinamico.Terminos IsNot Nothing Then
                For Each elementoTermino In InfAdicionalesDinamico.Terminos
                    unTermino = CamposExtrasDinamicosDatos.CamposExtras.Where(Function(x) x.Codigo = elementoTermino.Codigo).FirstOrDefault

                    If unTermino IsNot Nothing Then
                        elementoTermino.Valor = unTermino.Valor
                    End If
                Next
            End If
            If InfAdicionalesDinamico.Terminos IsNot Nothing AndAlso InfAdicionalesDinamico.Terminos.Count > 0 Then
                'InfAdicionalesDinamico..Item("display") = "block"
                InfAdicionalesDinamico.RecargarTerminos()
            End If



            upIAC.Update()
            'MostrarLoading(False)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click
        Try
            If InfAdicionalesPatron IsNot Nothing Then
                InfAdicionalesPatron.GuardarDatos()
                Dim objTermino As Clases.Termino

                If CamposExtrasPatronesDatos IsNot Nothing AndAlso CamposExtrasPatronesDatos.CamposExtras IsNot Nothing Then
                    For Each unControl In InfAdicionalesPatron.Terminos
                        objTermino = CamposExtrasPatronesDatos.CamposExtras.FirstOrDefault(Function(x) x.Codigo = unControl.Codigo)
                        If objTermino IsNot Nothing Then
                            objTermino.Valor = unControl.Valor
                        End If
                    Next
                End If
            End If

            If InfAdicionalesDinamico IsNot Nothing Then
                InfAdicionalesDinamico.GuardarDatos()
                Dim objTermino As Clases.Termino
                If CamposExtrasDinamicosDatos Is Nothing Then
                    CamposExtrasDinamicosDatos = New Clases.CamposExtrasDeIAC
                End If
                CamposExtrasDinamicosDatos.CamposExtras = New List(Of Clases.Termino)
                If ddlIAC.SelectedItem IsNot Nothing Then
                    For Each term In Genesis.LogicaNegocio.Integracion.AccionGrupoTerminoIAC.RecuperarGrupoTerminosIACPorCodigo(ddlIAC.SelectedItem.Text).TerminosIAC
                        CamposExtrasDinamicosDatos.CamposExtras.Add(term)
                    Next

                    CamposExtrasDinamicosDatos.IdentificadorIAC = ddlIAC.SelectedValue
                    CamposExtrasDinamicosDatos.CodigoIAC = ddlIAC.SelectedItem.Text
                    For Each unControl In InfAdicionalesDinamico.Terminos
                        objTermino = CamposExtrasDinamicosDatos.CamposExtras.FirstOrDefault(Function(x) x.Codigo = unControl.Codigo)
                        If objTermino IsNot Nothing Then
                            objTermino.Valor = unControl.Valor
                        End If
                    Next
                End If



            End If

            CerrarModal()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub CerrarModal()
        Dim jsScript As String = "window.parent.FecharModal(" & Chr(34) & "#" & divModal & Chr(34) & "," & Chr(34) & "#" & ifrModal & Chr(34) & ",null);"
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoCamposExtras", jsScript, True)
    End Sub

    'Public Sub MostrarLoading(mostrar As Boolean)

    '    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "ExibirLoading", "ExibirLoading(" & mostrar.ToString().ToLower() & ");", True)

    'End Sub
#End Region


End Class