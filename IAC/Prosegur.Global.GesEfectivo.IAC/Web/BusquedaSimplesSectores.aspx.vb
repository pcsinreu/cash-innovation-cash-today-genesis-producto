Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion

''' <summary>
''' PopUp de Sector
''' </summary>
''' <remarks></remarks>
''' <history>
''' [pgoncalves] 13/03/2013 Criado
''' </history>
Public Class BusquedaSimplesSectores
    Inherits Base

    ''' <summary>
    ''' Classes usadas apenas para transferir dados da entidade a ser consultada 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>[pgoncalves] 15/05/2013 - Criado</history>
    <Serializable()> _
    Public Class DadosPesquisa

        Private _OidPlanta As String
        Private _OidTipoSector As String
        Private _BolCentroProceso As Boolean

        Public Property OidPlanta As String
            Get
                Return _OidPlanta
            End Get
            Set(value As String)
                _OidPlanta = value
            End Set
        End Property

        Public Property OidTipoSector As String
            Get
                Return _OidTipoSector
            End Get
            Set(value As String)
                _OidTipoSector = value
            End Set
        End Property

        Public Property BolCentroProceso As Boolean
            Get
                Return _BolCentroProceso
            End Get
            Set(value As Boolean)
                _BolCentroProceso = value
            End Set
        End Property

    End Class

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        txtCodigo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricao.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img', event);")

        btnCancelar.FuncaoJavascript = "window.close();"

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigo.TabIndex = 1
        txtDescricao.TabIndex = 2
        btnBuscar.TabIndex = 3
        btnLimpar.TabIndex = 4
        btnAceptar.TabIndex = 10
        btnCancelar.TabIndex = 11

        Me.DefinirRetornoFoco(btnCancelar, txtCodigo)

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CLIENTES
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
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Consome os ojetos passados
                ConsomeEntidade()

                txtCodigo.Focus()

                ' se for para persistir os sectores, consome da sessão os selecionados anteriormente
                If PersisteSelecaoSectores Then
                    For Each Sector In Sectores
                        Me.hdnSelecionados.Value &= Sector.codSector & "#" & Sector.desSector & "|"
                    Next
                    If hdnSelecionados.Value.Length > 0 Then _
                        hdnSelecionados.Value = hdnSelecionados.Value.Substring(0, hdnSelecionados.Value.Length - 1)
                End If

                If Not String.IsNullOrEmpty(Request.QueryString("oidSetor")) Then
                    OidSetor = Request.QueryString("oidSetor")
                End If

            End If

            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("054_lblTituloBuscaSimples")
        lblCodigo.Text = Traduzir("054_lbl_BuscaSimplesCodigo")
        lblDescricao.Text = Traduzir("054_lbl_BuscaSimplesDescripcion")
        lblTituloSector.Text = Traduzir("054_lbl_criterioBusca")
        lblSubTitulosSector.Text = Traduzir("054_lbl_tiulo_Resultados")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Objeto CodigoAjenoSimples passado por session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewStateEntrada() As DadosPesquisa
        Get
            Return ViewState("DadosPesquisa")
        End Get
        Set(value As DadosPesquisa)
            ViewState("DadosPesquisa") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena os setores encontrados na busca.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Private Property Sectores() As ContractoServicio.Setor.GetSectores.SetorColeccion
        Get
            Return ViewState("Sectores")
        End Get
        Set(value As ContractoServicio.Setor.GetSectores.SetorColeccion)
            ViewState("Sectores") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena o sector selecionado no grid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Private Property SectorSelecionado() As ContractoServicio.Setor.GetSectores.Setor
        Get
            Return Session("SectorSelecionado")
        End Get
        Set(value As ContractoServicio.Setor.GetSectores.Setor)
            Session("SectorSelecionado") = value
        End Set
    End Property

    ''' <summary>
    ''' Determina se irá selecionar uma coleção de Sectores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[pgoncalves] 13/03/2013 - Criado</history>
    Private ReadOnly Property SelecionaColecaoSectores As Boolean
        Get
            Return If(String.IsNullOrEmpty(Request.QueryString("SelecionaColecaoSectores")), False, Request.QueryString("SelecionaColecaoSectores"))
        End Get
    End Property

    ''' <summary>
    ''' Determina se é necessário persistir com os Sectores já selecionadosS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[pgoncalves] 13/03/2013 - Criado</history>
    Private ReadOnly Property PersisteSelecaoSectores As Boolean
        Get
            Return If(String.IsNullOrEmpty(Request.QueryString("PersisteSelecaoSectores")), False, Request.QueryString("PersisteSelecaoSectores"))
        End Get
    End Property

    ''' <summary>
    ''' Armazena os códigos dos sectores selecionados no ViewState
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property VSSectoresSelecionados As List(Of String)
        Get
            Dim _id As String = "SectoresSelecionados"
            If ViewState(_id) Is Nothing Then ViewState(_id) = New List(Of String)
            Return ViewState(_id)
        End Get
        Set(value As List(Of String))
            ViewState("SectoresSelecionados") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena a coleção de Sectores selecioandos na sessão
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SectorSelecionados As ContractoServicio.Setor.GetSectores.SetorColeccion
        Get
            Return Session("SectorSelecionados")
        End Get
        Set(value As ContractoServicio.Setor.GetSectores.SetorColeccion)
            Session("SectorSelecionados") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena o OIDSECTOR da tela anterior
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OidSetor As String
        Get
            Return Session("OidSetor")
        End Get
        Set(value As String)
            Session("OidSetor") = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView.EPreencheDados

        Try

            Dim objDT As DataTable

            If Me.Sectores IsNot Nothing AndAlso Me.Sectores.Count > 0 Then

                pnlSemRegistro.Visible = False

                ' converter objeto para datatable
                objDT = ProsegurGridView.ConvertListToDataTable(Me.Sectores)

                If ProsegurGridView.SortCommand = String.Empty Then
                    objDT.DefaultView.Sort = " codSector asc"
                Else
                    objDT.DefaultView.Sort = ProsegurGridView.SortCommand
                End If

                ProsegurGridView.ControleDataSource = objDT
            Else

                'Limpa a consulta
                ProsegurGridView.DataSource = Nothing
                ProsegurGridView.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridView.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/03/2013 Criado
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
    ''' [pgoncalves] 13/03/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If CBool(e.Row.DataItem("bolCentroProceso")) Then
                    'CType(e.Row.Cells(2).FindControl("imgCprocesso"), Image).ImageUrl = "~/Imagenes/contain01.png"
                    e.Row.Cells(2).Text = Traduzir("019_lbl_centroProcessoAtivado")
                Else
                    'CType(e.Row.Cells(2).FindControl("imgCprocesso"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                    e.Row.Cells(2).Text = Traduzir("019_lbl_centroProcessoDesativado")
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
    ''' [pgoncalves] 13/03/2013 Criado
    ''' </history>
    Private Sub ProsegurGridViewProcesso_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("054_lbl_grd_codigo")
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 14
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 15
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("054_lbl_grd_descripcion")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 16
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 17
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("054_lbl_grd_centroproceso")

            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento acionado durante o evento RowDataBound do grid.
    ''' Permite adicionar um script na linha
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/03/2013 Adicionado para atender seleção de coleção de sectores
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.EOnClickRowClientScript

        Try
            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("bolCentroProceso").ToString.ToLower & ");"
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

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
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try

            ' verifica se é seleção de coleção
            If Not SelecionaColecaoSectores Then
                Dim strErro As New Text.StringBuilder(String.Empty)

                If ProsegurGridView.getValorLinhaSelecionada = String.Empty Then
                    strErro.Append(Traduzir("054_msg_selecione_Sector") & Aplicacao.Util.Utilidad.LineBreak)
                End If

                If strErro.Length > 0 Then
                    ControleErro.ShowError(strErro.ToString(), False)
                    Exit Sub
                End If

                ' salva cliente selecionado para sessão
                SalvarSectoresSelecionado()

                ' fechar janela
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ClienteOk", "window.returnValue=0;window.close();", True)
            Else

                Dim strErro As New Text.StringBuilder(String.Empty)

                If Not String.IsNullOrEmpty(hdnSelecionados.Value) Then

                    Dim clientesSelecionados() As String = hdnSelecionados.Value.Split("|")

                    If clientesSelecionados.Count > 0 Then
                        For Each item In clientesSelecionados : VSSectoresSelecionados.Add(item) : Next item
                    End If

                End If

                ' se não selecionou nenhum cliente
                If VSSectoresSelecionados.Count = 0 Then
                    SectorSelecionados = New ContractoServicio.Setor.GetSectores.SetorColeccion
                Else
                    SalvarSectoresSelecionados()
                End If

                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ClienteOk", "window.returnValue=0;window.close();", True)

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
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Try
            Acao = Aplicacao.Util.Utilidad.eAcao.Busca

            ' obtém os registros na base e preenche o grid
            PreencherGridSetores()

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
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            'Limpa a consulta
            ProsegurGridView.DataSource = Nothing
            ProsegurGridView.DataBind()

            pnlSemRegistro.Visible = False
            txtCodigo.Text = String.Empty
            txtDescricao.Text = String.Empty

            txtCodigo.Focus()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

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
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        Try

            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ClienteOk", "window.close();", True)

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
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
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                txtCodigo.Enabled = True
                txtDescricao.Enabled = True
                btnCancelar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles

        End Select

    End Sub

#End Region

#Region "[MÉTODOS]"

    Private Sub ConsomeEntidade()
        If Session("EntidadeDados") IsNot Nothing Then

            'Consome os subclientes passados
            ViewStateEntrada = CType(Session("EntidadeDados"), DadosPesquisa)

            'Remove da sessão
            Session("EntidadeDados") = Nothing

        End If
    End Sub

    ''' <summary>
    ''' Obtém os sectores
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Private Function GetComboSectores() As ContractoServicio.Setor.GetSectores.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.Setor.GetSectores.Peticion

        objPeticion.oidPlanta = ViewStateEntrada.OidPlanta
        objPeticion.bolCentroProceso = IIf(ViewStateEntrada.BolCentroProceso = True, True, Nothing)
        objPeticion.codSector = txtCodigo.Text
        objPeticion.desSector = txtDescricao.Text
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.bolActivo = True

        ' criar objeto proxy
        Dim objProxy As New ProxySector

        ' chamar servicio
        Return objProxy.getSectores(objPeticion)

    End Function

    ''' <summary>
    ''' Preenche o grid de sectores
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Private Sub PreencherGridSetores()

        Dim objRespuesta As ContractoServicio.Setor.GetSectores.Respuesta = GetComboSectores()

        If Not String.IsNullOrEmpty(OidSetor) Then
            objRespuesta.Setor.Remove((From item In objRespuesta.Setor
                                      Where item.oidSector = OidSetor
                                      Select item).FirstOrDefault)
        End If

        ' TRATAR RETORNO
        If Not ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        If objRespuesta.Setor IsNot Nothing AndAlso objRespuesta.Setor.Count > 0 Then

            Dim objDt As DataTable = ProsegurGridView.ConvertListToDataTable(objRespuesta.Setor)

            If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                objDt.DefaultView.Sort = " codSector ASC"
            Else
                objDt.DefaultView.Sort = ProsegurGridView.SortCommand
            End If
            ProsegurGridView.CarregaControle(objDt)
            pnlSemRegistro.Visible = False

            Sectores = objRespuesta.Setor
        Else

            'Limpa a consulta
            ProsegurGridView.DataSource = Nothing
            ProsegurGridView.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        End If

    End Sub

    ''' <summary>
    ''' Salva o objeto selecionado para sessão. Esta sessão poderá ser consumida 
    ''' por outras telas que chamam a tela de busca de sectores.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 13/03/2013 Criado
    ''' </history>
    Private Sub SalvarSectoresSelecionado()

        Dim objSector As New ContractoServicio.Setor.GetSectores.Setor

        Dim pesquisa = From c In Sectores _
                       Where c.codSector = ProsegurGridView.getValorLinhaSelecionada _
                       Select c.codSector, c.desSector, c.oidSector

        Dim en As IEnumerator = pesquisa.GetEnumerator()
        Dim objRetorno As Object

        If en.MoveNext Then

            objRetorno = en.Current
            objSector.codSector = pesquisa(0).codSector
            objSector.desSector = pesquisa(0).desSector
            objSector.oidSector = pesquisa(0).oidSector
        End If

        ' setar objeto sector para sessao
        SectorSelecionado = objSector

    End Sub

    ''' <summary>
    ''' Salva a coleção de sectores selecionados para a sessão que poderá ser usada nas outras telas que chamam essa
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/03/2013 Adicionado para atender seleção de coleção de sectores
    ''' </history>
    Private Sub SalvarSectoresSelecionados()

        Dim objSector As ContractoServicio.Setor.GetSectores.Setor
        Dim objColSector As New ContractoServicio.Setor.GetSectores.SetorColeccion

        ' obtém código e descrição dos sectores selecionados
        Dim pesquisa = From C In SectorSelecionados _
                       Join D As String In VSSectoresSelecionados _
                       On C.codSector Equals D _
                       Select C.codSector, C.desSector

        Dim en As IEnumerator = pesquisa.GetEnumerator
        Dim objRetorno As Object

        While en.MoveNext

            objRetorno = en.Current

            objSector = New ContractoServicio.Setor.GetSectores.Setor
            objSector.codSector = objRetorno.Codigo
            objSector.desSector = objRetorno.Descripcion

            objColSector.Add(objSector)

        End While

        For Each cli In VSSectoresSelecionados
            If cli.IndexOf("#") > 0 Then _
                objColSector.Add(New ContractoServicio.Setor.GetSectores.Setor With {.codSector = cli.Split("#")(0), .desSector = cli.Split("#")(1)})
        Next cli

        'seta o objeto para a sessão
        SectorSelecionados = objColSector

    End Sub

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

#End Region

End Class