Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class ucInventario
    Inherits UserControlBase

#Region "[PROPRIEDADES]"
    Private Property InventarioSelecionado As String
        Get
            Return ViewState("InventarioSelecionado")
        End Get
        Set(value As String)
            ViewState("InventarioSelecionado") = value
        End Set
    End Property

    Public Property DescricaoSetor As String
        Get
            Return ViewState("DescricaoSetor")
        End Get
        Set(value As String)
            ViewState("DescricaoSetor") = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.TraduzirControles()
        Me.RegistrarScripts()
        If (Me.ddlSetorNovo.Items.Count = 0) Then
            Me.PopularSector()
        End If

        If IsPostBack Then
            If Me.gdvInventario.Rows.Count > 0 Then
                Me.InventarioSelecionado = (From p In gdvInventario.Rows.Cast(Of GridViewRow)() _
                            Where DirectCast(p.Cells(0).FindControl("rdbSelecionado"), RadioButton).Checked _
                            Select Convert.ToString(gdvInventario.DataKeys(p.RowIndex).Value)).FirstOrDefault
            End If
        End If
    End Sub

    Protected Sub tabMenu_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles MenuTab.MenuItemClick
        mvTab.ActiveViewIndex = Convert.ToInt32(e.Item.Value)
        MenuTab.Items(0).Enabled = True
        MenuTab.Items(1).Enabled = True
        MenuTab.Items(mvTab.ActiveViewIndex).Enabled = False
        Me.DescricaoSetor = String.Empty
    End Sub

    Private Sub RegistrarScripts()
        Try
            Dim str = "SelecionarRegistroGridTipoRadio(""" & gdvInventario.ClientID & """);"
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "checked", str, True)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnInventario_Click(sender As Object, e As EventArgs) Handles btnInventario.Click
        If Me.validarBuscar() Then
            Me.PreencherGridViewInventarios(Me.getInventarios())
            Me.DescricaoSetor = Me.ddlSetorHistorico.SelectedItem.Text
        End If
    End Sub

    ''' <summary>
    ''' Quando a linha do grid for criada, traduzir o cabeçalho.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gdvInventario_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvInventario.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.Header Then

                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("030_col_selecionar")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("030_col_inventario")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("030_col_data")
            End If

        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Private Sub gdvInventario_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvInventario.RowDataBound
        Try
            e.Row.Attributes.Clear()
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub TraduzirControles()
        Me.lblSectorNovo.Text = Traduzir("030_lblSector")
        Me.lblSectorHistorico.Text = lblSectorNovo.Text
        Me.dataHistorico.LabelData = Traduzir("030_lblInventario")
        Me.lblInventario.Text = Traduzir("030_lblInventario")
        Me.MenuTab.Items(0).Text = Traduzir("030_lblNovo")
        Me.MenuTab.Items(1).Text = Traduzir("030_lblHistorico")
    End Sub

    Public Sub SelecionarMenu()
        MenuTab.Items(0).Selected = True
        MenuTab.Items(0).Enabled = False
    End Sub

    Public Sub PopularSector()
        'Recupera os setores que o usuário tem permissão
        'TODO: Deve ser atlerado para recuperar as permissões do usuário logado.
        Try
            Dim respuesta As Prosegur.Genesis.ContractoServicio.RecuperarSectorPorDelegacionRespuesta = Nothing
            Dim objSector As New LogicaNegocio.AccionSector
            Dim peticion As New Prosegur.Genesis.ContractoServicio.RecuperarSectorPorDelegacionPeticion
            peticion.CodigoDelegacion = InformacionUsuario.DelegacionLogin.Codigo
            respuesta = objSector.RecuperarSectorPorDelegacion(peticion)
            If respuesta.HayMensajes Then
                Me.MostraMensagemErro(respuesta.TodasMensajes)
            End If

            If Not respuesta.HayMensajes Then
                Me.ddlSetorNovo.DataTextField = "Descripcion"
                Me.ddlSetorNovo.DataValueField = "Identificador"
                Me.ddlSetorNovo.DataSource = respuesta.ListaSectores
                Me.ddlSetorNovo.DataBind()
                Me.ddlSetorNovo.Items.Insert(0, New ListItem(Traduzir("gen_selecione"), "0"))

                Me.ddlSetorHistorico.DataTextField = "Descripcion"
                Me.ddlSetorHistorico.DataValueField = "Identificador"
                Me.ddlSetorHistorico.DataSource = respuesta.ListaSectores
                Me.ddlSetorHistorico.DataBind()
                Me.ddlSetorHistorico.Items.Insert(0, New ListItem(Traduzir("gen_selecione"), "0"))
            Else
                Throw New Exception(respuesta.TodasMensajes)
            End If
        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' Preenche o gridview Histórico de inventário.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 08/08/2013 Criado
    ''' </history>
    Private Sub PreencherGridViewInventarios(invetarios As List(Of Prosegur.Genesis.Comon.Clases.Inventario))
        Try
            If invetarios IsNot Nothing AndAlso invetarios.Count > 0 Then

                Dim objDt As DataTable = gdvInventario.ConvertListToDataTable(invetarios)

                If gdvInventario.SortCommand.Equals(String.Empty) Then
                    objDt.DefaultView.Sort = " Codigo ASC"
                Else
                    objDt.DefaultView.Sort = gdvInventario.SortCommand
                End If

                gdvInventario.CarregaControle(objDt)
                gdvInventario_EPreencheDados(Nothing, Nothing)
            Else
                gdvInventario.DataSource = Nothing
                gdvInventario.DataBind()
                pnlSemRegistro.Visible = True
                lblSemRegistro.Text = Traduzir("info_msg_grd_vazio")
            End If

        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Quando for paginação.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gdvInventario_EPreencheDados(sender As Object, e As EventArgs) Handles gdvInventario.EPreencheDados
        Try
            Dim objDt As DataTable = gdvInventario.ConvertListToDataTable(Me.getInventarios())

            If gdvInventario.SortCommand.Equals(String.Empty) Then
                objDt.DefaultView.Sort = " Codigo ASC"
            Else
                objDt.DefaultView.Sort = gdvInventario.SortCommand
            End If

            gdvInventario.ControleDataSource = objDt

        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Recupera histórico de inventário.
    ''' </summary>
    ''' <remarks></remarks>
    Private Function getInventarios() As List(Of Prosegur.Genesis.Comon.Clases.Inventario)
        Dim respuesta As Prosegur.Genesis.ContractoServicio.RecuperarInventariosRespuesta = Nothing
        Dim objInventario As New LogicaNegocio.AccionInventario
        Dim peticion As New Prosegur.Genesis.ContractoServicio.RecuperarInventariosPeticion
        peticion.OIDSector = Me.ddlSetorHistorico.SelectedValue
        peticion.DataInicial = Me.dataHistorico.DataInicio
        peticion.DataFinal = Me.dataHistorico.DataFin
        respuesta = objInventario.RecuperarInventarios(peticion)
        If respuesta.HayMensajes Then
            Me.MostraMensagemErro(respuesta.TodasExcepciones)
        End If

        Return respuesta.ListaInventarios
    End Function

    Protected Function validarBuscar() As Boolean
        Dim retorno As Boolean = True
        Try
            retorno = Me.dataHistorico.Validar()
        Catch ex As Excepcion.NegocioExcepcion
            Me.MostraMensagemErro(ex.Descricao)
            retorno = False
        Catch ex As Exception
            Me.MostraMensagemErro(ex.Message)
            retorno = False
        End Try

        If Me.ddlSetorHistorico.SelectedValue = "0" Then
            Me.rfvddlSetorHistorico.IsValid = False
            Me.MostraMensagemErro(String.Format(Traduzir("err_campo_obrigatorio"), Traduzir("030_lblSector")))
            retorno = False
        End If

        Return retorno
    End Function

    ''' <summary>
    ''' Recupera o valor selecionado no grid, quando for a aba histórico
    ''' Recupera o valor do setor quando for a aba novo
    ''' </summary>
    ''' <param name="erro">Erro de campo obrigatório não preenchido.</param>
    ''' <returns>Valor a ser utilizado na consulta do relatório.</returns>
    ''' <remarks>Verifica qual aba está selecinada e recupera o valr da aba.</remarks>
    Public Function recuperarSelecionado(ByRef erro) As String
        Dim valor As String = String.Empty
        If Me.mvTab.ActiveViewIndex = 0 Then
            If Me.ddlSetorNovo.SelectedValue = "0" Then
                Me.rfvddlSetorNovo.IsValid = False
                erro = String.Format(Traduzir("err_campo_obrigatorio"), Traduzir("030_lblSector"))
            Else
                valor = Me.ddlSetorNovo.SelectedValue
                Me.DescricaoSetor = Me.ddlSetorNovo.SelectedItem.Text
            End If
        Else
            ' Recuperar o valor selecinado no grid    
            'valor = Me.gdvInventario.Rows.se
            For Each row As GridViewRow In gdvInventario.Rows
                Dim rdb As RadioButton = CType(row.Cells(0).FindControl("rdbSelecionado"), RadioButton)
                If rdb IsNot Nothing Then
                    If rdb.Checked Then
                        valor = gdvInventario.DataKeys(row.RowIndex).Value
                        Exit For
                    End If
                End If
            Next

            If String.IsNullOrEmpty(valor) Then
                erro = String.Format(Traduzir("err_campo_obrigatorio"), Traduzir("030_lblInventario"))
            End If
        End If

        Return valor
    End Function

    ''' <summary>
    ''' Retorna o campo ordenação
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function recuperarOrdenacao() As String
        Dim retorno As String
        If Me.rbCliente.Checked Then
            retorno = "0"
        Else
            retorno = "1"
        End If

        Return retorno
    End Function

#End Region

End Class
