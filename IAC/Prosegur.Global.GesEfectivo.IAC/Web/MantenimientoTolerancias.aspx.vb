Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' PopUp de Clientes
''' </summary>
''' <remarks></remarks>
''' <history>
''' [octavio.piramo] 18/02/2009 Criado
''' </history>
Partial Public Class MantenimientoTolerancias
    Inherits Base

#Region "[VARIÁVEIS]"

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

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        btnAceptar.TabIndex = 2

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If

        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TOLERANCIAS
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
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            Master.HabilitarHistorico = False

            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            'Possíveis Ações passadas pela página BusquedaAgrupaciones:
            ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
            ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
            ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta

            If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                    MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                    MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta OrElse _
                    MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar) Then

                Acao = Aplicacao.Util.Utilidad.eAcao.Erro

                'Informa ao usuário que o parâmetro passado são inválidos
                Throw New Exception(Traduzir("err_passagem_parametro"))

            End If

            ' verificar o tipo declarado.
            If Request.QueryString("tipodeclarado") Is Nothing Then
                'Informa ao usuário que o parâmetro passado são inválidos
                Throw New Exception(Traduzir("err_passagem_parametro"))
            Else
                ' setar o tipo declarado
                TipoDeclarado = Convert.ToInt16(Request.QueryString("TipoDeclarado").Trim)
            End If

            ' preparar tela de acordo com o tipo declarado
            PrepararTela()

            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

            ' preenche o grid
            PreencherGrid()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        ' seta titulo da página
        Me.Page.Title = Traduzir("018_titulo_pagina")

        ' seta subtitulo da página
        Dim tipoSubTitulo As String = IIf(TipoDeclarado = eTiposDeclarado.Agrupacion, Traduzir("018_col_agrupaciones"), Traduzir("018_col_medios_pago"))
        lblSubTituloTolerancia.Text = Traduzir("018_subtitulo_pagina").SetFormat(tipoSubTitulo)
        btnAceptar.Text = Traduzir("btnAceptar")

        'Grid ProsegurGridViewMedioPago
        ProsegurGridViewMedioPago.Columns(0).HeaderText = Traduzir("018_col_divisa")
        ProsegurGridViewMedioPago.Columns(1).HeaderText = Traduzir("018_col_tipo_medio_pago")
        ProsegurGridViewMedioPago.Columns(2).HeaderText = Traduzir("018_col_medio_pago")
        ProsegurGridViewMedioPago.Columns(3).HeaderText = Traduzir("018_col_divisa")
        ProsegurGridViewMedioPago.Columns(4).HeaderText = Traduzir("018_col_tipo_medio_pago")
        ProsegurGridViewMedioPago.Columns(5).HeaderText = Traduzir("018_col_medio_pago")
        ProsegurGridViewMedioPago.Columns(6).HeaderText = Traduzir("018_col_toleranciamin")
        ProsegurGridViewMedioPago.Columns(7).HeaderText = Traduzir("018_col_toleranciamax")
        ProsegurGridViewMedioPago.Columns(8).HeaderText = Traduzir("018_col_toleranciamin")
        ProsegurGridViewMedioPago.Columns(9).HeaderText = Traduzir("018_col_toleranciamax")
        ProsegurGridViewMedioPago.Columns(10).HeaderText = Traduzir("018_col_toleranciamin")
        ProsegurGridViewMedioPago.Columns(11).HeaderText = Traduzir("018_col_toleranciamax")

        'Grid ProsegurGridViewAgrupacion
        ProsegurGridViewAgrupacion.Columns(0).HeaderText = Traduzir("018_col_agrupacion")
        ProsegurGridViewAgrupacion.Columns(1).HeaderText = Traduzir("018_col_agrupacion")
        ProsegurGridViewAgrupacion.Columns(2).HeaderText = Traduzir("018_col_toleranciamin")
        ProsegurGridViewAgrupacion.Columns(3).HeaderText = Traduzir("018_col_toleranciamax")
        ProsegurGridViewAgrupacion.Columns(4).HeaderText = Traduzir("018_col_toleranciamin")
        ProsegurGridViewAgrupacion.Columns(5).HeaderText = Traduzir("018_col_toleranciamax")
        ProsegurGridViewAgrupacion.Columns(6).HeaderText = Traduzir("018_col_toleranciamin")
        ProsegurGridViewAgrupacion.Columns(7).HeaderText = Traduzir("018_col_toleranciamax")

    End Sub

#End Region

#Region "[ENUMERADORES]"

    ''' <summary>
    ''' Enumerado que contém os tipos de declarados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Enum eTiposDeclarado
        MedioPago = 1
        Agrupacion = 2
    End Enum

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Mantém a informação se está editando tolerancias de uma agrupação ou de um médio pago.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Property TipoDeclarado() As eTiposDeclarado
        Get
            Return ViewState("TipoDeclarado")
        End Get
        Set(value As eTiposDeclarado)
            ViewState("TipoDeclarado") = value
        End Set
    End Property

    ''' <summary>
    ''' Contém a coleção de tolerancias das agrupações.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Property ToleranciaAgrupacion() As PantallaProceso.ToleranciaAgrupacionColeccion
        Get
            If Session("ToleranciaAgrupacion") Is Nothing Then
                Session("ToleranciaAgrupacion") = New PantallaProceso.ToleranciaAgrupacionColeccion
            End If
            Return Session("ToleranciaAgrupacion")
        End Get
        Set(value As PantallaProceso.ToleranciaAgrupacionColeccion)
            Session("ToleranciaAgrupacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Contém a coleção de tolerancias dos médios pago.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Property ToleranciaMedioPago() As PantallaProceso.ToleranciaMedioPagoColeccion
        Get
            If Session("ToleranciaMedioPago") Is Nothing Then
                Session("ToleranciaMedioPago") = New PantallaProceso.ToleranciaMedioPagoColeccion
            End If
            Return Session("ToleranciaMedioPago")
        End Get
        Set(value As PantallaProceso.ToleranciaMedioPagoColeccion)
            Session("ToleranciaMedioPago") = value
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
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridViewAgrupacion.RowStyle.Height = 20
        ProsegurGridViewMedioPago.RowStyle.Height = 20
    End Sub

#Region "[GRID AGRUPACION]"

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewAgrupacion_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridViewAgrupacion.EPreencheDados

        Try

            If ToleranciaAgrupacion.Count > 0 Then

                pnlSemRegistro.Visible = False

                ' converter objeto para datatable
                Dim objDT As DataTable = ProsegurGridViewAgrupacion.ConvertListToDataTable(ToleranciaAgrupacion)

                If ProsegurGridViewAgrupacion.SortCommand = String.Empty Then
                    objDT.DefaultView.Sort = " codigo asc"
                Else
                    objDT.DefaultView.Sort = ProsegurGridViewAgrupacion.SortCommand
                End If

                ProsegurGridViewAgrupacion.ControleDataSource = objDT

            Else

                'Limpa a consulta
                ProsegurGridViewAgrupacion.DataSource = Nothing
                ProsegurGridViewAgrupacion.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewAgrupacion_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridViewAgrupacion.EPager_SetCss
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

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridViewAgrupacion_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewAgrupacion.RowCreated

        Try


            If e.Row.RowType = DataControlRowType.Header Then

                ' criar cabeçalho de agrupacion
                CriarCabecalhoAgrupacion()


            ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                ' se for do tipo consulta, deve desabilitar os campos texto
                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                    CType(e.Row.Cells(2).Controls(1), TextBox).Enabled = False
                    CType(e.Row.Cells(3).Controls(1), TextBox).Enabled = False
                    CType(e.Row.Cells(4).Controls(1), TextBox).Enabled = False
                    CType(e.Row.Cells(5).Controls(1), TextBox).Enabled = False
                    CType(e.Row.Cells(6).Controls(1), TextBox).Enabled = False
                    CType(e.Row.Cells(7).Controls(1), TextBox).Enabled = False

                Else
                    'Alteração para uma nova mascara monetaria
                    CType(e.Row.Cells(2).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(3).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(4).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(5).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(7).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))


                    'CType(e.Row.Cells(2).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    'CType(e.Row.Cells(3).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    'CType(e.Row.Cells(4).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    'CType(e.Row.Cells(5).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    'CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    'CType(e.Row.Cells(7).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))

                    CType(e.Row.Cells(2).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(3).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(4).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(5).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(7).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))

                    CType(e.Row.Cells(2).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")
                    CType(e.Row.Cells(3).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")
                    CType(e.Row.Cells(4).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")
                    CType(e.Row.Cells(5).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")
                    CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")
                    CType(e.Row.Cells(7).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")

                    CType(e.Row.Cells(2).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                    CType(e.Row.Cells(3).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                    CType(e.Row.Cells(4).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                    CType(e.Row.Cells(5).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                    CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                    CType(e.Row.Cells(7).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[GRID MÉDIO PAGO]"

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewMedioPago_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridViewMedioPago.EPreencheDados

        Try

            If ToleranciaMedioPago.Count > 0 Then

                pnlSemRegistro.Visible = False

                ' converter objeto para datatable
                Dim objDT As DataTable = ProsegurGridViewAgrupacion.ConvertListToDataTable(ToleranciaMedioPago)

                If ProsegurGridViewAgrupacion.SortCommand = String.Empty Then
                    objDT.DefaultView.Sort = " codigo asc"
                Else
                    objDT.DefaultView.Sort = ProsegurGridViewAgrupacion.SortCommand
                End If

                ProsegurGridViewMedioPago.ControleDataSource = objDT

            Else

                'Limpa a consulta
                ProsegurGridViewMedioPago.DataSource = Nothing
                ProsegurGridViewMedioPago.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewMedioPago_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridViewMedioPago.EPager_SetCss
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

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridViewMedioPago_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewMedioPago.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then

                ' criar cabeçalho de médio pago
                CriarCabecalhoMedioPago()

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                ' se for do tipo consulta, deve desabilitar os campos texto
                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                    CType(e.Row.Cells(6).Controls(1), TextBox).Enabled = False
                    CType(e.Row.Cells(7).Controls(1), TextBox).Enabled = False
                    CType(e.Row.Cells(8).Controls(1), TextBox).Enabled = False
                    CType(e.Row.Cells(9).Controls(1), TextBox).Enabled = False
                    CType(e.Row.Cells(10).Controls(1), TextBox).Enabled = False
                    CType(e.Row.Cells(11).Controls(1), TextBox).Enabled = False

                Else

                    'CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    'CType(e.Row.Cells(7).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    'CType(e.Row.Cells(8).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    'CType(e.Row.Cells(9).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    'CType(e.Row.Cells(10).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    'CType(e.Row.Cells(11).Controls(1), TextBox).Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))

                    CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(7).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(8).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(9).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(10).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(11).Controls(1), TextBox).Attributes.Add("onKeyUp", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))


                    CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(7).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(8).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(9).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(10).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
                    CType(e.Row.Cells(11).Controls(1), TextBox).Attributes.Add("onblur", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))

                    CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")
                    CType(e.Row.Cells(7).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")
                    CType(e.Row.Cells(8).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")
                    CType(e.Row.Cells(9).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")
                    CType(e.Row.Cells(10).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")
                    CType(e.Row.Cells(11).Controls(1), TextBox).Attributes.Add("onKeyPress", "return bloqueialetras(event,this);")

                    CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                    CType(e.Row.Cells(7).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                    CType(e.Row.Cells(8).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                    CType(e.Row.Cells(9).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                    CType(e.Row.Cells(10).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")
                    CType(e.Row.Cells(11).Controls(1), TextBox).Attributes.Add("onKeyDown", "BloquearColar();")

                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Seleciona o textbox do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/04/2009 - Criado
    ''' </history>
    Private Sub ProsegurGridViewAgrupacion_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewAgrupacion.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(2).HasControls Then
                Dim Txt As System.Web.UI.WebControls.TextBox
                Txt = e.Row.Cells(2).Controls((1))
                Txt.Attributes.Add("onfocus", "SelecionarTextBox('" & Txt.ClientID & "')")
            End If

        End If

    End Sub

    ''' <summary>
    ''' Seleciona o textbox do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/04/2009 - Criado
    ''' </history>
    Private Sub ProsegurGridViewMedioPago_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewMedioPago.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(6).HasControls Then
                Dim Txt As System.Web.UI.WebControls.TextBox
                Txt = e.Row.Cells(6).Controls((1))
                Txt.Attributes.Add("onfocus", "SelecionarTextBox('" & Txt.ClientID & "')")
            End If

            CType(e.Row.Cells(6).Controls(1), TextBox).Text = IIf(CType(e.Row.Cells(6).Controls(1), TextBox).Text = "0", "0", FormatNumber(CType(e.Row.Cells(6).Controls(1), TextBox).Text, 2))
            CType(e.Row.Cells(7).Controls(1), TextBox).Text = IIf(CType(e.Row.Cells(7).Controls(1), TextBox).Text = "0", "0", FormatNumber(CType(e.Row.Cells(7).Controls(1), TextBox).Text, 2))
            CType(e.Row.Cells(8).Controls(1), TextBox).Text = IIf(CType(e.Row.Cells(8).Controls(1), TextBox).Text = "0", "0", FormatNumber(CType(e.Row.Cells(8).Controls(1), TextBox).Text, 2))
            CType(e.Row.Cells(9).Controls(1), TextBox).Text = IIf(CType(e.Row.Cells(9).Controls(1), TextBox).Text = "0", "0", FormatNumber(CType(e.Row.Cells(9).Controls(1), TextBox).Text, 2))
            CType(e.Row.Cells(10).Controls(1), TextBox).Text = IIf(CType(e.Row.Cells(10).Controls(1), TextBox).Text = "0", "0", FormatNumber(CType(e.Row.Cells(10).Controls(1), TextBox).Text, 2))
            CType(e.Row.Cells(11).Controls(1), TextBox).Text = IIf(CType(e.Row.Cells(11).Controls(1), TextBox).Text = "0", "0", FormatNumber(CType(e.Row.Cells(11).Controls(1), TextBox).Text, 2))

        End If

    End Sub

#End Region

#End Region

#Region "[EVENTOS BOTOES]"

    ''' <summary>
    ''' Evento clique do botão aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try

            ' se o tipo declarado for agrupacion.
            ' senão é do tipo medio pago
            If TipoDeclarado = eTiposDeclarado.Agrupacion Then
                RecuperarDadosAgrupacion()
            Else
                RecuperarDadosMedioPago()
            End If

            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)

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
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnAceptar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                btnAceptar.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                btnAceptar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles

            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                btnAceptar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles

        End Select

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Prepara a tela de acordo com o tipo declarado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Sub PrepararTela()

        If TipoDeclarado = eTiposDeclarado.MedioPago Then

            ' desabilitar grid de agrupacion
            ProsegurGridViewAgrupacion.Visible = False

        Else

            ' desabilitar grid de médio pago
            ProsegurGridViewMedioPago.Visible = False

        End If

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

    ''' <summary>
    ''' Preenche a grid com o objeto recebido.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Sub PreencherGrid()

        If TipoDeclarado = eTiposDeclarado.Agrupacion Then
            If ProsegurGridViewAgrupacion.Rows.Count = 0 Then
                ProsegurGridViewAgrupacion.DataSource = ToleranciaAgrupacion
                ProsegurGridViewAgrupacion.DataBind()

                If ToleranciaAgrupacion.Count > 0 Then
                    ' setar foco no primeiro textbox do grid
                    CType(ProsegurGridViewAgrupacion.Rows(0).Cells(2).Controls(1), TextBox).Focus()
                End If

                ' limpar sessão
                ToleranciaAgrupacion = Nothing

            End If

        Else
            If ProsegurGridViewMedioPago.Rows.Count = 0 Then
                ProsegurGridViewMedioPago.DataSource = ToleranciaMedioPago
                ProsegurGridViewMedioPago.DataBind()

                If ToleranciaMedioPago.Count > 0 Then
                    ' setar foco no primeiro textbox do grid
                    CType(ProsegurGridViewMedioPago.Rows(0).Cells(6).Controls(1), TextBox).Focus()
                End If

                ' limpar sessão
                ToleranciaMedioPago = Nothing

            End If
        End If

    End Sub

    ''' <summary>
    ''' Criar cabeçalho agrupacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 23/03/2009 Criado
    ''' </history>
    Private Sub CriarCabecalhoAgrupacion()

        Dim gdv As New GridViewRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal)

        Dim tcType As TableHeaderCell = New TableHeaderCell()
        tcType.Text = Traduzir("018_col_agrupaciones")
        tcType.ColumnSpan = 1
        gdv.Cells.Add(tcType)

        ' criar demais cabeçalhos
        CriarCabecalhoComum(gdv)

    End Sub

    ''' <summary>
    ''' Criar cabecalho medio pago.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 23/03/2009 Criado
    ''' </history>
    Private Sub CriarCabecalhoMedioPago()

        Dim gdv As New GridViewRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal)

        Dim tcType As TableHeaderCell = New TableHeaderCell()
        tcType.Text = Traduzir("018_col_medios_pago")
        tcType.ColumnSpan = 3
        gdv.Cells.Add(tcType)

        ' criar demais cabeçalhos
        CriarCabecalhoComum(gdv)

    End Sub

    ''' <summary>
    ''' Criar cabeçalho comum.
    ''' </summary>
    ''' <param name="gdv"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 23/03/2009 Criado
    ''' </history>
    Private Sub CriarCabecalhoComum(ByRef gdv As GridViewRow)

        Dim tcType1 As TableHeaderCell = New TableHeaderCell()
        tcType1.Text = Traduzir("018_col_parcial")
        tcType1.ColumnSpan = 2
        gdv.Cells.Add(tcType1)

        Dim tcType2 As TableHeaderCell = New TableHeaderCell()
        tcType2.Text = Traduzir("018_col_bulto")
        tcType2.ColumnSpan = 2
        gdv.Cells.Add(tcType2)

        Dim tcType3 As TableHeaderCell = New TableHeaderCell()
        tcType3.Text = Traduzir("018_col_remesa")
        tcType3.ColumnSpan = 2
        gdv.Cells.Add(tcType3)

        If TipoDeclarado = eTiposDeclarado.Agrupacion Then
            CType(ProsegurGridViewAgrupacion.Controls(0), Table).Rows.AddAt(0, gdv)
        Else
            CType(ProsegurGridViewMedioPago.Controls(0), Table).Rows.AddAt(0, gdv)
        End If

    End Sub

    ''' <summary>
    ''' Recupera dados do grid agrupacion.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Sub RecuperarDadosAgrupacion()

        ' se no grid existir algum registro.
        If ProsegurGridViewAgrupacion.Rows.Count > 0 Then

            Dim objAgrupacionColeccion As New PantallaProceso.ToleranciaAgrupacionColeccion

            ' pecorrer registros do grid
            For Each row As GridViewRow In ProsegurGridViewAgrupacion.Rows

                ' criar objeto agrupacion
                Dim objAgrupacion As New PantallaProceso.ToleranciaAgrupacion

                ' se as row for do tipo datarow
                If row.RowType = DataControlRowType.DataRow Then

                    objAgrupacion.CodigoAgrupacion = ProsegurGridViewAgrupacion.DataKeys(row.RowIndex).Item("CodigoAgrupacion")
                    objAgrupacion.DescripcionAgrupacion = Server.HtmlDecode(row.Cells(1).Text)

                    If CType(row.Cells(2).Controls(1), TextBox).Text.Equals(String.Empty) Then
                        objAgrupacion.ToleranciaParcialMin = 0.ToString("N2")
                    Else
                        objAgrupacion.ToleranciaParcialMin = CType(row.Cells(2).Controls(1), TextBox).Text
                    End If

                    If CType(row.Cells(3).Controls(1), TextBox).Text.Equals("N2") Then
                        objAgrupacion.ToleranciaParcialMax = 0.ToString("N2")
                    Else
                        objAgrupacion.ToleranciaParcialMax = CType(row.Cells(3).Controls(1), TextBox).Text
                    End If

                    If CType(row.Cells(4).Controls(1), TextBox).Text.Equals(String.Empty) Then
                        objAgrupacion.ToleranciaBultoMin = 0.ToString("N2")
                    Else
                        objAgrupacion.ToleranciaBultoMin = CType(row.Cells(4).Controls(1), TextBox).Text
                    End If

                    If CType(row.Cells(5).Controls(1), TextBox).Text.Equals(String.Empty) Then
                        objAgrupacion.ToleranciaBultoMax = 0.ToString("N2")
                    Else
                        objAgrupacion.ToleranciaBultoMax = CType(row.Cells(5).Controls(1), TextBox).Text
                    End If

                    If CType(row.Cells(6).Controls(1), TextBox).Text.Equals(String.Empty) Then
                        objAgrupacion.ToleranciaRemesaMin = 0.ToString("N2")
                    Else
                        objAgrupacion.ToleranciaRemesaMin = CType(row.Cells(6).Controls(1), TextBox).Text
                    End If

                    If CType(row.Cells(7).Controls(1), TextBox).Text.Equals(String.Empty) Then
                        objAgrupacion.ToleranciaRemesaMax = 0.ToString("N2")
                    Else
                        objAgrupacion.ToleranciaRemesaMax = CType(row.Cells(7).Controls(1), TextBox).Text
                    End If

                    ' adicionar objeto para agrupacion
                    objAgrupacionColeccion.Add(objAgrupacion)

                End If

            Next

            ' atualizar sessão
            ToleranciaAgrupacion = objAgrupacionColeccion

        End If

    End Sub

    ''' <summary>
    ''' Recupera dados do grid medio pago.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Sub RecuperarDadosMedioPago()

        ' se no grid existir algum registro.
        If ProsegurGridViewMedioPago.Rows.Count > 0 Then

            Dim objMedioPagoColeccion As New PantallaProceso.ToleranciaMedioPagoColeccion

            ' pecorrer registros do grid
            For Each row As GridViewRow In ProsegurGridViewMedioPago.Rows

                ' criar objeto MedioPago
                Dim objMedioPago As New PantallaProceso.ToleranciaMedioPago

                ' se as row for do tipo datarow
                If row.RowType = DataControlRowType.DataRow Then

                    objMedioPago.CodigoDivisa = ProsegurGridViewMedioPago.DataKeys(row.RowIndex).Item("CodigoDivisa")
                    objMedioPago.DescripcionDivisa = IIf(row.Cells(1).Text = "&nbsp;", String.Empty, Server.HtmlDecode(row.Cells(1).Text))

                    objMedioPago.CodigoTipoMedioPago = ProsegurGridViewMedioPago.DataKeys(row.RowIndex).Item("CodigoTipoMedioPago")
                    objMedioPago.DescripcionTipoMedioPago = IIf(row.Cells(3).Text = "&nbsp;", String.Empty, Server.HtmlDecode(row.Cells(3).Text))

                    objMedioPago.CodigoMedioPago = ProsegurGridViewMedioPago.DataKeys(row.RowIndex).Item("CodigoMedioPago")
                    objMedioPago.DescripcionMedioPago = IIf(row.Cells(5).Text = "&nbsp;", String.Empty, Server.HtmlDecode(row.Cells(5).Text))

                    If CType(row.Cells(6).Controls(1), TextBox).Text.Equals(String.Empty) Then
                        objMedioPago.ToleranciaParcialMin = 0.ToString("N2")
                    Else
                        objMedioPago.ToleranciaParcialMin = CType(row.Cells(6).Controls(1), TextBox).Text
                    End If

                    If CType(row.Cells(7).Controls(1), TextBox).Text.Equals(String.Empty) Then
                        objMedioPago.ToleranciaParcialMax = 0.ToString("N2")
                    Else
                        objMedioPago.ToleranciaParcialMax = CType(row.Cells(7).Controls(1), TextBox).Text
                    End If

                    If CType(row.Cells(8).Controls(1), TextBox).Text.Equals(String.Empty) Then
                        objMedioPago.ToleranciaBultoMin = 0.ToString("N2")
                    Else
                        objMedioPago.ToleranciaBultoMin = CType(row.Cells(8).Controls(1), TextBox).Text
                    End If

                    If CType(row.Cells(9).Controls(1), TextBox).Text.Equals(String.Empty) Then
                        objMedioPago.ToleranciaBultoMax = 0.ToString("N2")
                    Else
                        objMedioPago.ToleranciaBultoMax = CType(row.Cells(9).Controls(1), TextBox).Text
                    End If

                    If CType(row.Cells(10).Controls(1), TextBox).Text.Equals(String.Empty) Then
                        objMedioPago.ToleranciaRemesaMin = 0.ToString("N2")
                    Else
                        objMedioPago.ToleranciaRemesaMin = CType(row.Cells(10).Controls(1), TextBox).Text
                    End If

                    If CType(row.Cells(11).Controls(1), TextBox).Text.Equals(String.Empty) Then
                        objMedioPago.ToleranciaRemesaMax = 0.ToString("N2")
                    Else
                        objMedioPago.ToleranciaRemesaMax = CType(row.Cells(11).Controls(1), TextBox).Text
                    End If

                    ' adicionar objeto para objMedioPagoColeccion
                    objMedioPagoColeccion.Add(objMedioPago)

                End If

            Next

            ' atualizar sessão
            ToleranciaMedioPago = objMedioPagoColeccion

        End If

    End Sub

#End Region

End Class