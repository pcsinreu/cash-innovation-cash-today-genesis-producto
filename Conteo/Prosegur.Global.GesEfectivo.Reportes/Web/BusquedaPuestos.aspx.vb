Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.Comunicacion.ProxyWS.IAC

''' <summary>
''' PopUp de Puesto
''' </summary>
''' <remarks></remarks>
''' <history>
''' [kasantos] 25/09/2012 Criado
''' </history>
Partial Public Class BusquedaPuesto
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        txtCodigoPuesto.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img');")
        txtHostPuesto.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img');")

        btnCancelar.FuncaoJavascript = "window.close();"

        ' Registra o id do controle de erro
        scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "idControleErro", "var idControleErro = '" & Me.ControleErro.ClientID & "';", True)

    End Sub

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarEstadoPagina()

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Consulta

                Me.txtCodigoPuesto.Enabled = True
                Me.txtHostPuesto.Enabled = True
                Me.cblVigente.Enabled = True
                Me.btnBuscar.Visible = True
                Me.btnLimpar.Visible = True
                Me.btnAceptar.Visible = True
                Me.btnCancelar.Visible = True

        End Select

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        Me.txtDelegacion.TabIndex = 1
        Me.txtCodigoPuesto.TabIndex = 2
        Me.txtHostPuesto.TabIndex = 3
        Me.cblVigente.TabIndex = 4
        Me.btnBuscar.TabIndex = 5
        Me.btnLimpar.TabIndex = 6
        Me.btnAceptar.TabIndex = 10
        Me.btnCancelar.TabIndex = 11

        Me.DefinirRetornoFoco(btnCancelar, txtDelegacion)

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.CLIENTE
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                If Not String.IsNullOrEmpty(Request.QueryString("codDelegacion")) Then

                    ' setar valor no campo codigo
                    txtDelegacion.Text = Request.QueryString("codDelegacion")

                    Acao = Enumeradores.eAcao.Busca

                End If
            End If

            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("021_titulo_busqueda_puestos")
        lblDelegacion.Text = Traduzir("021_lbl_delegacion")
        lblCodigoPuesto.Text = Traduzir("021_lbl_codigopuesto")
        lblHostPuesto.Text = Traduzir("021_lbl_hostpuesto")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("021_lbl_subtituloscriteriosbusqueda")
        lblSubTitulosPuestos.Text = Traduzir("021_lbl_subtitulospuestos")
        lblSemRegistro.Text = Traduzir("021_lbl_sem_registro")
        cblVigente.Items(0).Text = Traduzir("021_chk_vigente")
        cblVigente.Items(1).Text = Traduzir("021_chk_no_vigente")
        cblVigente.Items(2).Text = Traduzir("021_chk_cualquiera")

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            Me.ConfigurarEstadoPagina()
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Armazena os Puestos encontrados na busca.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 25/09/2012 Criado
    ''' </history>
    Private Property Puestos() As IAC.ContractoServicio.Puesto.GetPuestos.PuestoColeccion
        Get
            If ViewState("VSPuesto") Is Nothing Then
                ViewState("VSPuesto") = New IAC.ContractoServicio.Puesto.GetPuestos.PuestoColeccion
            End If
            Return ViewState("VSPuesto")
        End Get
        Set(value As IAC.ContractoServicio.Puesto.GetPuestos.PuestoColeccion)
            ViewState("VSPuesto") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena o cliente selecionado no grid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 25/09/2012 Criado
    ''' </history>
    Private Property PuestoSelecionado() As IAC.ContractoServicio.Puesto.GetPuestos.Puesto
        Get
            Return Session("PuestoSelecionado")
        End Get
        Set(value As IAC.ContractoServicio.Puesto.GetPuestos.Puesto)
            Session("PuestoSelecionado") = value
        End Set
    End Property

    Property FiltroPuesto() As String
        Get
            Return ViewState("FiltroPuesto")
        End Get
        Set(value As String)
            ViewState("FiltroPuesto") = value
        End Set
    End Property

    Property FiltroAplicacion() As String
        Get
            Return ViewState("FiltroAplicacion")
        End Get
        Set(value As String)
            ViewState("FiltroAplicacion") = value
        End Set
    End Property

    Property FiltroHostPuesto() As String
        Get
            Return ViewState("FiltroHostPuesto")
        End Get
        Set(value As String)
            ViewState("FiltroHostPuesto") = value
        End Set
    End Property

    Property FiltroVigente() As Nullable(Of Boolean)
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As Nullable(Of Boolean))
            ViewState("FiltroVigente") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena se o campo de pesquisa e um campo obrigatorio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Property CampoObrigatorio() As Boolean
        Get
            If ViewState("CampoObrigatorio") Is Nothing Then
                ViewState("CampoObrigatorio") = New Boolean
            End If
            Return ViewState("CampoObrigatorio")
        End Get
        Set(value As Boolean)
            ViewState("CampoObrigatorio") = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridView.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView.EPager_SetCss
        Try

            'Configura o estilo dos controles presentes no pager
            CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
            CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
            CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

            CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
            CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
            CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 9

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                'Índice das celulas do GridView Configuradas
                '0 - CodigoPuesto
                '1 - CodigoHostPuesto
                '2 - vigente


                If CBool(e.Row.DataItem("PuestoVigente")) Then
                    CType(e.Row.Cells(2).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.gif"
                Else
                    CType(e.Row.Cells(2).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.gif"
                End If

            End If
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_lbl_codigopuesto")
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 5
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 6
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_lbl_hostpuesto")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 7
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 8
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_chk_no_vigente")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 9
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 10
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 22/12/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView.EPreencheDados

        If MyBase.Acao = Enumeradores.eAcao.Inicial Then
            Return
        End If

        Dim objDT As DataTable

        Dim objRespoustaPuestos As Object

        objRespoustaPuestos = getPuestos().Puestos

        objDT = TransformarContractoPuestoEmDataTable(objRespoustaPuestos)

        If ProsegurGridView.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " CodigoPuesto ASC "
        Else
            objDT.DefaultView.Sort = ProsegurGridView.SortCommand
        End If

        ProsegurGridView.ControleDataSource = objDT

    End Sub

#End Region

#Region "[EVENTOS BOTOES]"

    ''' <summary>
    ''' Evento clique do botão aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try

            Dim strErro As New Text.StringBuilder(String.Empty)

            If ProsegurGridView.getValorLinhaSelecionada = String.Empty AndAlso CampoObrigatorio = True Then
                strErro.Append(Traduzir("021_seleccione_un_puesto") & Constantes.LineBreak)
            End If

            If strErro.Length > 0 Then
                ControleErro.ShowError(strErro.ToString(), False)
                Exit Sub
            End If

            Dim Codigos() As String = ProsegurGridView.getValorLinhaSelecionada.Replace("$#", "|").Split("|")

            If Codigos.Length >= 2 AndAlso Not String.IsNullOrEmpty(Codigos(0)) Then
                ' salva puesto selecionado para sessão
                SalvarPuestoSelecionado(Codigos(0), Codigos(1))
            Else
                ' setar para nulo objeto Puesto da sessao
                PuestoSelecionado = Nothing
                Session("LimparPuestoSelecionado") = True

                ' fechar janela
                scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "PuestoOk", "window.returnValue=0;window.close();", True)
            End If
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Try

            'Threading.Thread.Sleep(2000)
            Acao = Enumeradores.eAcao.Busca

            'Filtros
            FiltroAplicacion = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CodAplicacionConteo")
            FiltroHostPuesto = txtHostPuesto.Text
            FiltroPuesto = txtCodigoPuesto.Text
            If cblVigente.SelectedValue = "1" Then
                FiltroVigente = True
            ElseIf cblVigente.SelectedValue = "0" Then
                FiltroVigente = False
            Else
                FiltroVigente = Nothing
            End If

            'Retorna os canais de acordo com o filtro aciam
            PreencherPuestos()

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão limpar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            'Limpa a consulta
            ProsegurGridView.CarregaControle(Nothing, True, True)

            pnlSemRegistro.Visible = False
            txtCodigoPuesto.Text = String.Empty
            txtHostPuesto.Text = String.Empty
            txtCodigoPuesto.Focus()

            'Estado Inicial
            Acao = Enumeradores.eAcao.Inicial

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        Try
            ' fechar janela
            scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "PuestoOk", "window.close();", True)

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "[MÉTODOS]"


    ''' <summary>
    ''' Faz a busca dos Postos com os parametros informados
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Public Function getPuestos() As IAC.ContractoServicio.Puesto.GetPuestos.Respuesta

        Dim objProxyPuesto As New ProxyPuesto
        Dim objPeticionPuesto As New IAC.ContractoServicio.Puesto.GetPuestos.Peticion
        Dim objRespuestaPuesto As IAC.ContractoServicio.Puesto.GetPuestos.Respuesta

        'Recebe os valores do filtro
        objPeticionPuesto.BolVigente = FiltroVigente
        objPeticionPuesto.CodigoAplicacion = FiltroAplicacion
        objPeticionPuesto.CodigoDelegacion = MyBase.DelegacionConectada.Keys(0)
        objPeticionPuesto.CodigoPuesto = FiltroPuesto
        objPeticionPuesto.HostPuesto = FiltroHostPuesto

        objRespuestaPuesto = objProxyPuesto.GetPuestos(objPeticionPuesto)

        Return objRespuestaPuesto


    End Function

    ''' <summary>
    ''' transforma o contrato do Posto em DataTable
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 09/09/11 Criado
    Public Function TransformarContractoPuestoEmDataTable(puestos As IAC.ContractoServicio.Puesto.GetPuestos.PuestoColeccion) As DataTable
        Dim dt As DataTable

        Dim grid As New Prosegur.Web.ProsegurGridView
        dt = grid.ConvertListToDataTable(puestos)
        'dt.Columns.Add("DescripcionAplicacion", GetType([String]))
        'dt.Columns.Add("CodigoAplicacion", GetType([String]))
        'For Each dr As DataRow In dt.Rows
        '    Dim aplicacion As IAC.ContractoServicio.Puesto.GetPuestos.Aplicacion = CType(dr("Aplicacion"), IAC.ContractoServicio.Puesto.GetPuestos.Aplicacion)
        '    dr("DescripcionAplicacion") = aplicacion.DescripcionAplicacion
        '    dr("CodigoAplicacion") = aplicacion.CodigoAplicacion
        'Next

        Return dt

    End Function

    ''' <summary>
    ''' Preenche do grid com a coleção de Postos
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Public Sub PreencherPuestos()

        Dim objRespostaPuesto As IAC.ContractoServicio.Puesto.GetPuestos.Respuesta

        'Busca os canais
        objRespostaPuesto = getPuestos()

        If Not ControleErro.VerificaErro(objRespostaPuesto.CodigoError, objRespostaPuesto.NombreServidorBD, objRespostaPuesto.MensajeError) Then

            Acao = Enumeradores.eAcao.NoAction
            Exit Sub

        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaPuesto.Puestos.Count > 0 Then

            Puestos = objRespostaPuesto.Puestos

            pnlSemRegistro.Visible = False

            Dim objDt As DataTable
            objDt = TransformarContractoPuestoEmDataTable(objRespostaPuesto.Puestos)

            If Acao = Enumeradores.eAcao.Busca Then

                objDt.DefaultView.Sort = " CodigoPuesto ASC "

            End If

            ProsegurGridView.CarregaControle(objDt)


        Else

            'Verifica se a ação de preencher foi acionada pela baixa de um canal("Atualizar o GridView" - Não exibe o painel informativo de "sem registros")
            'ou se foi aciona por outra ação (ex:botão buscar - exibe o painel informativo de "sem registros").

            'Limpa a consulta
            ProsegurGridView.DataSource = Nothing
            ProsegurGridView.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Constantes.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Enumeradores.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Salva o objeto selecionado para sessão. Esta sessão poderá ser consumida 
    ''' por outras telas que chamam a tela de busca de posto.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 25/09/2012 Criado
    ''' </history>
    Private Sub SalvarPuestoSelecionado(codigoPuesto As String,
                                        codigoHostPuesto As String)

        Dim objPuesto As New IAC.ContractoServicio.Puesto.GetPuestos.Puesto

        Dim pesquisa = Puestos.FirstOrDefault(Function(f) f.CodigoPuesto = codigoPuesto AndAlso f.CodigoHostPuesto = codigoHostPuesto)

        If pesquisa IsNot Nothing Then

            ' setar objeto cliente para sessao
            PuestoSelecionado = pesquisa

            ' fechar janela
            scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "PuestoOk", "window.returnValue=0;window.close();", True)
        End If

    End Sub

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>

    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Util.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

#End Region

End Class