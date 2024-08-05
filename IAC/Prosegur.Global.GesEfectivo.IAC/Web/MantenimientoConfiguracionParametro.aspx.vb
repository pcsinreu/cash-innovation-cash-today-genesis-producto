Imports Prosegur.Framework.Dicionario.Tradutor
Imports DevExpress.Web.ASPxTabControl
Imports DevExpress.Web.ASPxClasses
Imports Prosegur.Genesis

Partial Public Class MantenimientoConfiguracionParametro
    Inherits Base

#Region "[CONSTANTES]"

    Private Const PREFIXO_NAME_TAB_PAG As String = "tab"
    Private Const PREFIXO_ID_TABELA As String = "tb"
    Private Const PREFIXO_ID_LABEL_CODIGO As String = "lblCod"
    Private Const PREFIXO_ID_IMAGE_BUTTON As String = "img"
    Private Const PREFIXO_ID_LABEL_DESCRICAO As String = "lblDesc"
    Private Const PREFIXO_ID_TEXT_BOX As String = "txt"
    Private Const PREFIXO_ID_CHECK_BOX As String = "chk"
    Private Const PREFIXO_ID_DROP_DOWN_LIST As String = "ddl"
    Private Const PREFIXO_ID_COLOR_PICKER_EXTENDER As String = "cpe"
    Private Const PREFIXO_ID_REQUIRED_FIELD_VALIDATOR As String = "rfv"
    Private Const PREFIXO_ID_UPDATE_PANEL As String = "up"
    Private Const MAX_QTD_COLUNAS As Integer = 4
    Private Const MAX_TAM_CAMPO_TEXTO As Integer = 4000
    Private Const MAX_TAM_CAMPO_INTEIRO As Integer = 8
    Private Const MAX_TAM_CAMPO_COR As Integer = 6
    Private Const INDICE_COLUNA_CONTROLE As Integer = 3
    Private Const ESTILO_LINHA_PADRAO As String = "LinhaPadrao"
    Private Const ESTILO_LINHA_ALTERNADA As String = "LinhaAlternada"

#End Region

#Region "[ATRIBUTOS]"

    Private habilitarControles As Boolean = True
    Private primeiroControle As Control = Nothing
    Private tabIndex As Integer = 1

#End Region

#Region "[PROPRIEDADES]"

    Private Property ConfiguracaoParametros() As ContractoServicio.Parametro.GetParametrosValues.NivelColeccion
        Get
            If ViewState("ConfiguracaoParametros") Is Nothing Then
                ViewState("ConfiguracaoParametros") = New ContractoServicio.Parametro.GetParametrosValues.NivelColeccion
            End If

            Return ViewState("ConfiguracaoParametros")
        End Get
        Set(value As ContractoServicio.Parametro.GetParametrosValues.NivelColeccion)
            ViewState("ConfiguracaoParametros") = value
        End Set
    End Property

    Private Property ParametrosValores() As ContractoServicio.Parametro.SetParametrosValues.ParametroColeccion
        Get
            If ViewState("ParametrosValores") Is Nothing Then
                ViewState("ParametrosValores") = New ContractoServicio.Parametro.SetParametrosValues.ParametroColeccion
            End If

            Return ViewState("ParametrosValores")
        End Get
        Set(value As ContractoServicio.Parametro.SetParametrosValues.ParametroColeccion)
            ViewState("ParametrosValores") = value
        End Set
    End Property

    Private Property CodigoAplicacion() As String
        Get
            If ViewState("CodigoAplicacion") Is Nothing Then

                If Request.Params("CodigoAplicacion") IsNot Nothing Then
                    ViewState("CodigoAplicacion") = Request.Params("CodigoAplicacion")
                Else
                    ViewState("CodigoAplicacion") = String.Empty
                End If

            End If
            Return ViewState("CodigoAplicacion")
        End Get
        Set(value As String)
            ViewState("CodigoAplicacion") = value
        End Set
    End Property

    Private Property CodigoDelegacion() As String
        Get
            If ViewState("CodigoDelegacion") Is Nothing Then

                If DelegacionConectada IsNot Nothing AndAlso DelegacionConectada.Keys IsNot Nothing AndAlso DelegacionConectada.Keys.Count > 0 Then
                    ViewState("CodigoDelegacion") = DelegacionConectada.Keys(0)
                Else
                    ViewState("CodigoDelegacion") = String.Empty
                End If

            End If
            Return ViewState("CodigoDelegacion")
        End Get
        Set(value As String)
            ViewState("CodigoDelegacion") = value
        End Set
    End Property

    Private Property CodigoPuesto() As String
        Get
            If ViewState("CodigoPuesto") Is Nothing Then

                If Request.Params("CodigoPuesto") IsNot Nothing Then
                    ViewState("CodigoPuesto") = Request.Params("CodigoPuesto")
                Else
                    ViewState("CodigoPuesto") = String.Empty
                End If

            End If
            Return ViewState("CodigoPuesto")
        End Get
        Set(value As String)
            ViewState("CodigoPuesto") = value
        End Set
    End Property

    'Private Property Executou() As Boolean
    '    Get
    '        If ViewState("CodigoPuesto") Is Nothing Then

    '            If Request.Params("CodigoPuesto") IsNot Nothing Then
    '                ViewState("CodigoPuesto") = Request.Params("CodigoPuesto")
    '            Else
    '                ViewState("CodigoPuesto") = String.Empty
    '            End If

    '        End If
    '        Return ViewState("CodigoPuesto")
    '    End Get
    '    Set(value As Boolean)
    '        ViewState("CodigoPuesto") = value
    '    End Set
    'End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Faz a validação dos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 06/09/2011 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()
        'TODO: Laerccius - Avaliar se deve excluir
        'MyBase.AddControleValidarPermissao(Aplicacao.Util.Utilidad.eAcoesTela._MODIFICAR.ToString, btnAceptar)

    End Sub

    ''' <summary>
    ''' Adiciona o java script
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 06/09/2011 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim strBuider As New StringBuilder

        ' Script para mudar a cor de fundo do controle
        strBuider.Append("function colorChanged(sender)")
        strBuider.Append("{")
        strBuider.Append("  sender.get_element().style.backgroundColor = '#' + sender.get_selectedColor();")
        strBuider.Append("}")

        'Adiciona o script na página
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ColorChanged", strBuider.ToString, True)


    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' [maoliveira] 06/09/2011 Criado
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Definição de parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 06/09/2011 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CONFIGURACION_PARAMETRO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de acesso
        MyBase.ValidarAcesso = True

    End Sub

    ''' <summary>
    ''' Metodo inicial e chamado quando a pagina e inicializada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 06/09/2011 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            DevExpress.Web.ASPxTabControl.ASPxPageControl.RegisterBaseScript(Me.Page)

            ' Trata o foco
            TrataFoco()

        Catch ex As Exception

            Throw New InicializarException(ex.ToString)

        End Try

    End Sub

    ''' <summary>
    ''' Metodo pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 06/09/2011 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()

        Try
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            ControleBotoes()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Traduz os controles da pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 06/09/2011 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("027_titulo_configuracion_parametros")
        lblSubTitulosConfiguracionParametros.Text = Traduzir("027_subtitulos_configuracion_parametros")
        btnAceptar.Text = Traduzir("btnAceptar")
        btnAceptar.ToolTip = Traduzir("btnAceptar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração do controle de estado da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 06/09/2011 Criado
    ''' </history>
    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                'Controles
                btnAceptar.Visible = True
                btnCancelar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction

                'Controles
                btnCancelar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Inicial

                'Controles
                btnAceptar.Visible = True
                btnCancelar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
        End Select

    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Recupera os parâmetros e seus valores
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 08/09/2011 Criado
    ''' </history>
    Private Sub RecuperarParametros()

        ' Se a lista de parâmetros está vazia
        If ConfiguracaoParametros Is Nothing OrElse ConfiguracaoParametros.Count = 0 Then

            ' Define a variável da petição e preenche os filtros 
            Dim peticao As New ContractoServicio.Parametro.GetParametrosValues.Peticion
            peticao.CodigoAplicacion = CodigoAplicacion
            peticao.CodigoDelegacion = CodigoDelegacion
            peticao.CodigoPuesto = CodigoPuesto

            ' Define a variável de comunicação
            Dim acaoParametro As New Comunicacion.ProxyParametro

            ' Define a variavel de resposta e recupera os parâmetros e seus os valores
            Dim respuesta As ContractoServicio.Parametro.GetParametrosValues.Respuesta = acaoParametro.GetParametrosValues(peticao)

            ' Verifica se existe algum erro
            If Not Master.ControleErro.VerificaErro(respuesta.CodigoError, respuesta.NombreServidorBD, respuesta.MensajeError) Then
                ' Sai do metodo
                Exit Sub
            Else
                ' Preenche a lista de parametros
                ' Remove as Agrupações que o usuario não tem acesso 
                ConfiguracaoParametros = AplicarRegrasAcessoParametros(respuesta.Niveles)

                ' Valida se teve
                If ConfiguracaoParametros.Count = 0 AndAlso respuesta.Niveles.Count > 0 Then
                    Response.Redirect(String.Format("~/BusquedaPuestos.aspx?AcessoNegadoConfigurarParametro={0}", True), True)
                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Cria o controle de parametros
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 08/09/2011 Criado
    ''' </history>
    Private Sub CriarParametros()

        ' Se existe parâmetros
        If (ConfiguracaoParametros IsNot Nothing AndAlso ConfiguracaoParametros.Count > 0) Then


            ' Para cada nível existente
            For Each nivel As ContractoServicio.Parametro.GetParametrosValues.Nivel In ConfiguracaoParametros.OrderByDescending(Function(n) n.CodigoNivel)

                ' Cria o controle de Nivel
                CriarAbaNivel(nivel)

            Next
        End If
    End Sub

    ''' <summary>
    ''' Cria a aba de níveis
    ''' </summary>
    ''' <param name="nivel">ContractoServicio.Parametro.GetParametrosValues.Nivel</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 08/09/2011 Criado
    ''' </history>
    Private Sub CriarAbaNivel(nivel As ContractoServicio.Parametro.GetParametrosValues.Nivel)

        ' Verifica se o codigo do posto foi informado e se está no nivel de Pais ou Delegação
        If Not String.IsNullOrEmpty(CodigoPuesto) AndAlso (nivel.CodigoNivel = ContractoServicio.Parametro.TipoNivel.Pais OrElse nivel.CodigoNivel = ContractoServicio.Parametro.TipoNivel.Delegacion) Then
            ' Desativa os controles da do nível
            habilitarControles = False
        End If

        If nivel.Agrupaciones.Count = 0 Then
            Return
        End If

        ' Cria uma nova aba de nível
        Dim tabPageNivel As New TabPage
        tabPageNivel.Name = PREFIXO_NAME_TAB_PAG & nivel.CodigoNivel
        tabPageNivel.Text = nivel.DescripcionNivel

        ' Cria o controle de agrupación
        CriarControleAgrupacion(nivel.Agrupaciones, tabPageNivel)

        ' Adiciona o nivel ao PageControl
        tabConfiguracionParametro.TabPages.Add(tabPageNivel)

    End Sub

    ''' <summary>
    ''' Cria o conteudo das abas dos parametros
    ''' </summary>
    ''' <param name="Agrupaciones">List(Of ContractoServicio.Parametro.GetParametrosValues.Agrupacion)</param>
    ''' <param name="tabPageNivel">TabPage</param>
    ''' <history>
    ''' [maoliveira] 08/09/2011 Criado
    ''' </history>
    Private Sub CriarControleAgrupacion(Agrupaciones As List(Of ContractoServicio.Parametro.GetParametrosValues.Agrupacion), ByRef tabPageNivel As TabPage)

        ' Se existe agrupaciones
        If Agrupaciones IsNot Nothing AndAlso Agrupaciones.Count > 0 Then

            ' Cria uma nova Page Control para a Agrupação
            Dim pageControlAgrupacion As New ASPxPageControl

            ' Formata o controle de Agrupação
            pageControlAgrupacion.EnableHierarchyRecreation = True
            pageControlAgrupacion.EnableViewState = True
            pageControlAgrupacion.ActiveTabIndex = 0
            pageControlAgrupacion.TabStyle.VerticalAlign = VerticalAlign.Top
            pageControlAgrupacion.Width = New Unit(100, UnitType.Percentage)
            pageControlAgrupacion.CssFilePath = "~/Css/css.css"
            pageControlAgrupacion.EnableTabScrolling = True
            ' Para cada agrupação existente
            For Each agrupacion As ContractoServicio.Parametro.GetParametrosValues.Agrupacion In Agrupaciones
                ' Cria uma nova aba de agrupação
                CriarAbaAgrupacion(agrupacion, pageControlAgrupacion)
            Next

            ' Adiciona na aba do nível o seu conteúdo
            tabPageNivel.Controls.Add(pageControlAgrupacion)

        End If

    End Sub

    ''' <summary>
    ''' Cria a aba de agrupaciones
    ''' </summary>
    ''' <param name="agrupacion">ContractoServicio.Parametro.GetParametrosValues.Agrupacion</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 09/09/2011 Criado
    ''' </history>
    Private Sub CriarAbaAgrupacion(agrupacion As ContractoServicio.Parametro.GetParametrosValues.Agrupacion, ByRef pageControlAgrupacion As DevExpress.Web.ASPxTabControl.ASPxPageControl)

        ' Cria uma nova aba de agrupação
        Dim tabPage As New TabPage
        tabPage.Name = PREFIXO_NAME_TAB_PAG & Trim(agrupacion.DescripcionCorto)
        tabPage.Text = agrupacion.DescripcionCorto

        ' Cria o controle de parametros
        CriarTabelaParametros(agrupacion.Parametros, tabPage)

        ' Adiciona a agrupação ao PageControl
        pageControlAgrupacion.TabPages.Add(tabPage)

    End Sub

    ''' <summary>
    ''' Cria a tabela de parametros
    ''' </summary>
    ''' <param name="Parametros">ContractoServicio.Parametro.GetParametrosValues.Parametro</param>
    ''' <param name="tabPageAgrupacion">tabPageAgrupacion As TabPage</param>
    ''' <history>
    ''' [maoliveira] 08/09/2011 Criado
    ''' </history>
    Private Sub CriarTabelaParametros(Parametros As List(Of ContractoServicio.Parametro.GetParametrosValues.Parametro), ByRef tabPageAgrupacion As TabPage)

        If Parametros IsNot Nothing AndAlso Parametros.Count > 0 Then

            ' Cria uma nova tabela
            Dim Tabela As New Table
            Tabela.ID = PREFIXO_ID_TABELA & tabPageAgrupacion.Name
            Tabela.Width = New Unit(100, UnitType.Percentage)

            Dim estiloLinha As String = ESTILO_LINHA_ALTERNADA

            ' Para cada parametro existente
            For Each parametro As ContractoServicio.Parametro.GetParametrosValues.Parametro In Parametros

                ' Muda o estilo da linha
                estiloLinha = If(estiloLinha = ESTILO_LINHA_ALTERNADA, ESTILO_LINHA_PADRAO, ESTILO_LINHA_ALTERNADA)

                ' Cria uma linha
                Dim Linha As New TableRow
                Linha.ID = parametro.CodigoParametro & tabPageAgrupacion.Name
                Linha.CssClass = estiloLinha

                ' Cria uma nova celula
                Dim Coluna As New TableCell
                ' Cria o label que recebe o código do parâmetro
                Dim label As New Label
                Coluna.Width = New Unit(25, UnitType.Percentage)
                Coluna.Wrap = False
                label.ID = PREFIXO_ID_LABEL_CODIGO & parametro.CodigoParametro
                label.Text = parametro.CodigoParametro
                ' Adiciona o label a coluna
                Coluna.Controls.Add(label)
                ' Adiciona a coluna a linha
                Linha.Cells.Add(Coluna)

                ' Cria uma nova celula
                Coluna = New TableCell
                Coluna.Width = New Unit(5, UnitType.Percentage)
                Coluna.HorizontalAlign = HorizontalAlign.Center
                ' Cria o label que recebe a descrição larga do parâmetro
                Dim imagem = New Image
                imagem.ID = PREFIXO_ID_IMAGE_BUTTON & parametro.CodigoParametro
                imagem.ImageUrl = "~/Imagenes/help_parametros.png"
                imagem.ToolTip = parametro.DescripcionLarga

                ' Adiciona o label a coluna
                Coluna.Controls.Add(imagem)
                ' Adiciona a coluna a linha
                Linha.Cells.Add(Coluna)

                ' Cria uma nova celula
                Coluna = New TableCell
                Coluna.Width = New Unit(30, UnitType.Percentage)
                ' Cria o label que recebe a descrição larga do parâmetro
                label = New Label
                label.ID = PREFIXO_ID_LABEL_DESCRICAO & parametro.CodigoParametro
                label.Text = parametro.DescripcionCorto
                ' Adiciona o label a coluna
                Coluna.Controls.Add(label)
                ' Adiciona a coluna a linha
                Linha.Cells.Add(Coluna)

                ' Cria uma nova celula
                Coluna = New TableCell
                Coluna.Width = New Unit(40, UnitType.Percentage)

                ' Cria o controle de parâmetro
                CriarControleParametro(parametro, Coluna)

                ' Adiciona a coluna a linha
                Linha.Cells.Add(Coluna)

                ' Se o primeiro controle não foi definido
                If primeiroControle Is Nothing Then
                    ' Define o primeiro controle da tela
                    primeiroControle = Linha.Cells(INDICE_COLUNA_CONTROLE).Controls(0)
                End If

                ' Adiciona a linha a tabela
                Tabela.Rows.Add(Linha)
                ' Incrementa o TabIndex
                tabIndex = tabIndex + 1

            Next

            ' Adiciona a tabela de parâmetros na aba da agrupação
            tabPageAgrupacion.Controls.Add(Tabela)

        End If

    End Sub

    ''' <summary>
    ''' Cria o controle que recebe o valor do parâmetro
    ''' </summary>
    ''' <param name="parametro">ContractoServicio.Parametro.GetParametrosValues.Parametro</param>
    ''' <param name="coluna">TableCell</param>
    ''' <remarks></remarks>
    Private Sub CriarControleParametro(parametro As ContractoServicio.Parametro.GetParametrosValues.Parametro, ByRef coluna As TableCell)

        Dim controle As Control = Nothing

        ' Verifica o tipo do parâmetro
        Select Case parametro.NecTipoComponente

            ' Caso seja um TextBox
            Case ContractoServicio.Parametro.TipoComponente.TextBox

                ' Cria o TextBox que recebe o valor do parâmetro
                controle = New TextBox
                controle.ID = PREFIXO_ID_TEXT_BOX & parametro.CodigoParametro
                DirectCast(controle, TextBox).Enabled = habilitarControles
                DirectCast(controle, TextBox).Width = New Unit(400, UnitType.Pixel)
                DirectCast(controle, TextBox).TabIndex = tabIndex
                DirectCast(controle, TextBox).MaxLength = MAX_TAM_CAMPO_TEXTO
                DirectCast(controle, TextBox).Text = parametro.ValorParametro

                ' Se o parâmetro é númerico
                If parametro.NecTipoDato = ContractoServicio.Parametro.TipoDato.Numerico Then
                    ' Formata o campo para receber somente números
                    DirectCast(controle, TextBox).MaxLength = MAX_TAM_CAMPO_INTEIRO
                    DirectCast(controle, TextBox).Attributes.Add("onkeypress", "return ValorNumerico(event);")
                    DirectCast(controle, TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                End If

                ' Se controle estiver desabilitado então exibe ToolTip.
                If Not habilitarControles Then
                    DirectCast(controle, TextBox).ToolTip = parametro.ValorParametro
                End If

                ' Caso seja um CheckBox
            Case ContractoServicio.Parametro.TipoComponente.CheckBox

                ' Cria o CheckBox que recebe o valor do parâmetro
                controle = New CheckBox
                controle.ID = PREFIXO_ID_CHECK_BOX & parametro.CodigoParametro
                DirectCast(controle, CheckBox).Checked = If(String.IsNullOrEmpty(parametro.ValorParametro) OrElse parametro.ValorParametro = 0, False, True)
                DirectCast(controle, CheckBox).Enabled = habilitarControles
                DirectCast(controle, CheckBox).TabIndex = tabIndex

                ' Caso seja um Combobox
            Case ContractoServicio.Parametro.TipoComponente.Combobox

                ' Cria o DropDownList que recebe o valor do parâmetro
                controle = New DropDownList
                controle.ID = PREFIXO_ID_DROP_DOWN_LIST & parametro.CodigoParametro
                DirectCast(controle, DropDownList).Width = New Unit(400, UnitType.Pixel)
                DirectCast(controle, DropDownList).TabIndex = tabIndex

                ' Se o parâmetro é obrigatorio
                If Not parametro.BolObligatorio Then
                    ' Adiciona o parâmetro vazio, quando o campo não é obrigatório
                    DirectCast(controle, DropDownList).Items.Add(New ListItem With {.Text = String.Empty, .Value = String.Empty})
                End If

                ' Verifica se existe somente números na lista
                Dim somenteNumero As Boolean = Not parametro.ParametroOpciones.Exists(Function(po) Not IsNumeric(po.CodigoOpcion))

                ' Para cada opção existente
                For Each opcion As ContractoServicio.Parametro.GetParametrosValues.ParametroOpciones In parametro.ParametroOpciones _
                    .OrderBy(Function(po)
                                 ' Se existe somente números
                                 If somenteNumero Then
                                     Return Integer.Parse(po.CodigoOpcion)
                                 Else
                                     Return po.CodigoOpcion
                                 End If
                             End Function)

                    ' Cria um novo item para a opção
                    Dim item = New ListItem With {.Text = String.Format("{0} - {1}", opcion.CodigoOpcion, opcion.DescriptionOpcion), .Value = opcion.CodigoOpcion}

                    ' Verifica se o valor da opção é a mesma do valor do parâmetro 
                    If opcion.CodigoOpcion = parametro.ValorParametro Then
                        ' Define o item como selecionado
                        item.Selected = True

                        'Coloca ToolTip nos campos dropdownlist.
                        If Not habilitarControles Then
                            DirectCast(controle, DropDownList).ToolTip = item.Text
                        End If

                    End If

                    ' Adiciona o item no combo box
                    DirectCast(controle, DropDownList).Items.Add(item)

                Next

                DirectCast(controle, DropDownList).Enabled = habilitarControles

                ' Caso seja uma PaletaCores
            Case ContractoServicio.Parametro.TipoComponente.PaletaCores

                ' Recupara a cor
                Dim strCor As String = RecuperarCor(parametro.ValorParametro)

                ' Cria o TextBox que recebe o valor do parâmetro
                controle = New TextBox
                controle.ID = PREFIXO_ID_TEXT_BOX & parametro.CodigoParametro
                DirectCast(controle, TextBox).Width = New Unit(400, UnitType.Pixel)
                DirectCast(controle, TextBox).Enabled = habilitarControles
                DirectCast(controle, TextBox).TabIndex = tabIndex
                DirectCast(controle, TextBox).Text = strCor
                DirectCast(controle, TextBox).MaxLength = MAX_TAM_CAMPO_COR
                DirectCast(controle, TextBox).Attributes.Add("onKeyDown", "return false;")

                ' Se o valor do parâmetro está preenchido
                If Not String.IsNullOrEmpty(strCor) Then
                    DirectCast(controle, TextBox).BackColor = Drawing.ColorTranslator.FromHtml(String.Format("#{0}", strCor))
                End If

                ' Se os controle es habilidados
                If habilitarControles Then

                    ' Cria a Paleta de Cores para selecionar a cor
                    Dim clrBox = New AjaxControlToolkit.ColorPickerExtender
                    clrBox.ID = PREFIXO_ID_COLOR_PICKER_EXTENDER & parametro.CodigoParametro
                    clrBox.TargetControlID = PREFIXO_ID_TEXT_BOX & parametro.CodigoParametro
                    clrBox.OnClientColorSelectionChanged = "colorChanged"

                    ' Adiciona a Paleta de Cores a coluna
                    coluna.Controls.Add(clrBox)

                End If

        End Select

        ' Se o controle não é nulo
        If controle IsNot Nothing Then

            ' Se o parâmetro é obrigatorio
            If parametro.BolObligatorio AndAlso Not TypeOf controle Is CheckBox Then

                ' Controle de validação do controle
                Dim rfvControl As New RequiredFieldValidator
                rfvControl.ID = PREFIXO_ID_REQUIRED_FIELD_VALIDATOR & parametro.CodigoParametro
                rfvControl.ControlToValidate = controle.ID
                rfvControl.Text = "*"
                rfvControl.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), parametro.CodigoParametro)
                rfvControl.IsValid = Not Page.IsPostBack

                coluna.Controls.Add(controle)
                coluna.Controls.Add(rfvControl)

            Else

                coluna.Controls.Add(controle)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Recupera os valores dos parâmetros
    ''' </summary>
    ''' <history>
    ''' [maoliveira] 08/09/2011 Criado
    ''' </history>
    Private Function RecuperarValoresParametros() As String

        Dim msgErro As New StringBuilder

        ' Limpa os valores dos parâmetros
        ParametrosValores.Clear()

        ' Se existe parâmetros
        If ConfiguracaoParametros IsNot Nothing AndAlso ConfiguracaoParametros.Count > 0 Then

            ' Para cada nível existente
            For Each nivel As ContractoServicio.Parametro.GetParametrosValues.Nivel In ConfiguracaoParametros

                ' Recupera a aba do nível
                Dim tabNivel As TabPage = tabConfiguracionParametro.TabPages.FindByName(PREFIXO_NAME_TAB_PAG & nivel.CodigoNivel)

                ' Se existe agrupações
                If tabNivel IsNot Nothing AndAlso nivel.Agrupaciones IsNot Nothing AndAlso nivel.Agrupaciones.Count > 0 Then

                    ' Recupera a página de agrupações
                    Dim pageAgrupacion As ASPxPageControl = (From c As Control In tabNivel.Controls _
                                                                Where TypeOf c Is ASPxPageControl).FirstOrDefault

                    ' Para cada agrupação existente
                    For Each agrupacion As ContractoServicio.Parametro.GetParametrosValues.Agrupacion In nivel.Agrupaciones

                        ' Recupera a aba de agrupação
                        Dim tabAgrupacion As TabPage = pageAgrupacion.TabPages.FindByName(PREFIXO_NAME_TAB_PAG & agrupacion.DescripcionCorto)

                        ' Se existe parâmetros
                        If tabAgrupacion IsNot Nothing AndAlso agrupacion.Parametros IsNot Nothing AndAlso agrupacion.Parametros.Count > 0 Then

                            ' Recupera a tabela de parametros
                            Dim tabelaParametro As Table = (From c As Control In tabAgrupacion.Controls _
                                                                Where c.ID = PREFIXO_ID_TABELA & tabAgrupacion.Name).FirstOrDefault

                            ' Se existe dados na tabela de parâmetros
                            If tabelaParametro IsNot Nothing Then

                                ' Para cada parâmetro existente
                                For Each parametro As ContractoServicio.Parametro.GetParametrosValues.Parametro In agrupacion.Parametros
                                    Dim parametroLocal = parametro
                                    ' Recupera a linha do parâmetro
                                    Dim linhaParametro As TableRow = (From r As TableRow In tabelaParametro.Rows _
                                                                     Where r.ID = parametroLocal.CodigoParametro & tabAgrupacion.Name).FirstOrDefault

                                    ' Se existe a linha do parâmetro
                                    If linhaParametro IsNot Nothing AndAlso linhaParametro.Cells.Count = MAX_QTD_COLUNAS Then

                                        ' Verifica o tipo do parâmetro
                                        Select Case parametro.NecTipoComponente

                                            ' Caso seja um TextBox
                                            Case ContractoServicio.Parametro.TipoComponente.TextBox

                                                ' Recupera o textBox que recebe o valor do parâmetro
                                                Dim txtBox As TextBox = (From c As Control In linhaParametro.Cells(INDICE_COLUNA_CONTROLE).Controls _
                                                                        Where c.ID = PREFIXO_ID_TEXT_BOX & parametroLocal.CodigoParametro).FirstOrDefault

                                                ' Se encontrou o TextBox
                                                If txtBox IsNot Nothing Then

                                                    ' Define o valor do parâmetro
                                                    ParametrosValores.Add(New ContractoServicio.Parametro.SetParametrosValues.Parametro _
                                                                           With {.CodigoParametro = parametro.CodigoParametro, .ValorParametro = txtBox.Text})
                                                End If

                                                ' Caso seja um CheckBox
                                            Case ContractoServicio.Parametro.TipoComponente.CheckBox

                                                ' Recupera o CheckBox que recebe o valor do parâmetro
                                                Dim chkBox As CheckBox = (From c As Control In linhaParametro.Cells(INDICE_COLUNA_CONTROLE).Controls _
                                                                        Where c.ID = PREFIXO_ID_CHECK_BOX & parametroLocal.CodigoParametro).FirstOrDefault

                                                ' Se encontrou o CheckBox
                                                If chkBox IsNot Nothing Then

                                                    ' Define o valor do parâmetro
                                                    ParametrosValores.Add(New ContractoServicio.Parametro.SetParametrosValues.Parametro _
                                                                           With {.CodigoParametro = parametro.CodigoParametro, .ValorParametro = If(chkBox.Checked, 1, 0)})
                                                End If

                                                ' Caso seja um Combobox
                                            Case ContractoServicio.Parametro.TipoComponente.Combobox

                                                ' Recupera o DropDownList que recebe o valor do parâmetro
                                                Dim cmbBox As DropDownList = (From c As Control In linhaParametro.Cells(INDICE_COLUNA_CONTROLE).Controls _
                                                                        Where c.ID = PREFIXO_ID_DROP_DOWN_LIST & parametroLocal.CodigoParametro).FirstOrDefault

                                                ' Se encontrou o DropDownList
                                                If cmbBox IsNot Nothing Then

                                                    ' Define o valor do parâmetro
                                                    ParametrosValores.Add(New ContractoServicio.Parametro.SetParametrosValues.Parametro _
                                                                           With {.CodigoParametro = parametro.CodigoParametro, .ValorParametro = cmbBox.SelectedValue})
                                                End If

                                                ' Caso seja uma PaletaCores
                                            Case ContractoServicio.Parametro.TipoComponente.PaletaCores

                                                ' Cria o TextBox que recebe o valor do parâmetro
                                                Dim txtBox As TextBox = (From c As Control In linhaParametro.Cells(INDICE_COLUNA_CONTROLE).Controls _
                                                                        Where c.ID = PREFIXO_ID_TEXT_BOX & parametroLocal.CodigoParametro).FirstOrDefault

                                                ' Se encontrou o TextBox
                                                If txtBox IsNot Nothing Then

                                                    ' Valida se a cor é valida
                                                    Try
                                                        If Not String.IsNullOrEmpty(txtBox.Text) Then
                                                            Drawing.ColorTranslator.FromHtml(String.Format("#{0}", txtBox.Text))
                                                        End If
                                                    Catch ex As Exception
                                                        txtBox.Text = String.Empty
                                                    End Try

                                                    ' Define o valor do parâmetro
                                                    ParametrosValores.Add(New ContractoServicio.Parametro.SetParametrosValues.Parametro _
                                                                           With {.CodigoParametro = parametro.CodigoParametro, .ValorParametro = String.Format("#{0}", txtBox.Text)})
                                                End If

                                        End Select

                                        ' Cria o RequiredFieldValidator que recebe o valor do parâmetro
                                        Dim rfvControle As RequiredFieldValidator = (From c As Control In linhaParametro.Cells(INDICE_COLUNA_CONTROLE).Controls _
                                                                Where c.ID = PREFIXO_ID_REQUIRED_FIELD_VALIDATOR & parametroLocal.CodigoParametro).FirstOrDefault

                                        ' Verifica se o parâmetro foi preenchido
                                        If rfvControle IsNot Nothing Then

                                            ' Verifica se o campo foi preechido
                                            If String.IsNullOrEmpty(ParametrosValores.Last.ValorParametro) Then

                                                ' Adiciona a mensagem do erro
                                                msgErro.Append(rfvControle.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                                                ' Define que o campo não está valido
                                                rfvControle.IsValid = False

                                            Else
                                                ' Define que o campo está valido
                                                rfvControle.IsValid = True
                                            End If

                                        End If

                                    End If

                                Next

                            End If

                        End If

                    Next

                End If

            Next

        End If

        Return msgErro.ToString

    End Function

    ''' <summary>
    ''' Grava os valores dos parâmetros
    ''' </summary>
    ''' <history>
    ''' [maoliveira] 08/09/2011 Criado
    ''' </history>
    Private Sub GravarValoresParametros()

        ' Define a variável da petição e preenche os filtros 
        Dim peticao As New ContractoServicio.Parametro.SetParametrosValues.Peticion
        peticao.CodigoAplicacion = CodigoAplicacion
        peticao.CodigoDelegacion = CodigoDelegacion
        peticao.CodigoPuesto = CodigoPuesto
        peticao.CodigoUsuario = MyBase.LoginUsuario
        peticao.Parametros = ParametrosValores

        ' Define a variável de comunicação
        Dim acaoParametro As New Comunicacion.ProxyParametro

        ' Define a variavel de resposta e recupera os parâmetros e seus os valores
        Dim respuesta As ContractoServicio.Parametro.SetParametrosValues.Respuesta = acaoParametro.SetParametrosValues(peticao)

        ' Verifica se existe algum erro
        If Not Master.ControleErro.VerificaErro(respuesta.CodigoError, respuesta.NombreServidorBD, respuesta.MensajeError) Then
            ' Sai do metodo
            Exit Sub
        Else

            Dim url As String = "BusquedaPuestos.aspx"

            'Dim script As String = "alert('" & Traduzir("001_msg_grabado_suceso") & "'); RedirecionaPaginaNormal('" & url & "');"
            Dim script As String = "alert('" & Traduzir("001_msg_grabado_suceso") & "');"
            'Registro gravado com sucesso
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", script, True)

        End If

    End Sub

    ''' <summary>
    ''' Recupera a cor informada no parâmetro
    ''' </summary>
    ''' <param name="corDescricao">String</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperarCor(corDescricao As String) As String

        Dim cor As Drawing.Color
        Dim strCor As String = String.Empty

        Try

            If String.IsNullOrEmpty(corDescricao) Then

                'Verifica se a cor é valida
                Drawing.ColorTranslator.FromHtml("#FFFFFF")

            Else

                strCor = corDescricao.Replace("#", "")

                'Verifica se a cor é valida
                Drawing.ColorTranslator.FromHtml(String.Format("#{0}", strCor))

            End If

        Catch ex As Exception

            'Se não for valida ,tenta converter o nome para hexadecimal
            cor = Drawing.ColorTranslator.FromHtml(strCor)
            Dim r As String = If(cor.R <= 9, "0" & cor.R, Hex(cor.R))
            Dim g As String = If(cor.G <= 9, "0" & cor.G, Hex(cor.G))
            Dim b As String = If(cor.B <= 9, "0" & cor.B, Hex(cor.B))
            strCor = r & g & b

        End Try

        Return strCor

    End Function

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

#Region "[EVENTOS]"

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click

        Try

            Dim url As String = "BusquedaPuestos.aspx"

            'Registro gravado com sucesso
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_cancelar", "if (confirm('" & Traduzir("info_msg_sair_pagina") & _
                                                                                "')) RedirecionaPaginaNormal('" & url & "'); ", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try

            ' Recupera os valores dos parâmetros
            Dim erro As String = RecuperarValoresParametros()

            If String.IsNullOrEmpty(erro) Then

                ' Grava os valores dos parâmetros
                GravarValoresParametros()

            Else

                Master.ControleErro.ShowError(erro, False)

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

        Try

            ' Recupera os parametros
            RecuperarParametros()

            ' Configura a tela
            CriarParametros()

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region

    Private Function AplicarRegrasAcessoParametros(nivelColeccion As ContractoServicio.Parametro.GetParametrosValues.NivelColeccion) As ContractoServicio.Parametro.GetParametrosValues.NivelColeccion

        If nivelColeccion Is Nothing Then
            Return Nothing
        End If

        Dim lista As New ContractoServicio.Parametro.GetParametrosValues.NivelColeccion

        For Each itemNivel As ContractoServicio.Parametro.GetParametrosValues.Nivel In nivelColeccion

            Dim agrupacoes As List(Of ContractoServicio.Parametro.GetParametrosValues.Agrupacion) = itemNivel.Agrupaciones.Where(Function(a) ValidarPermissao(a.CodigoPermiso)).ToList()
            If ValidarPermissao(itemNivel.CodigoPermiso) AndAlso agrupacoes.Count = 0 Then
                lista.Add(itemNivel)
            ElseIf agrupacoes.Count > 0 Then

                Dim nivel As New ContractoServicio.Parametro.GetParametrosValues.Nivel
                nivel.Agrupaciones = agrupacoes
                nivel.CodigoNivel = itemNivel.CodigoNivel
                nivel.DescripcionNivel = itemNivel.DescripcionNivel
                nivel.CodigoPermiso = itemNivel.CodigoPermiso

                lista.Add(nivel)

            End If
        Next

        If MyBase.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.CONFIGURACION_PARAMETRO, Aplicacao.Util.Utilidad.eAcao.Modificacion) AndAlso lista.Count = 0 Then
            Return nivelColeccion
        End If

        Return lista

    End Function

    Private Function ValidarPermissao(permiso As String) As Object
        Return InformacionUsuario.Permisos.Contains(permiso)
    End Function

End Class