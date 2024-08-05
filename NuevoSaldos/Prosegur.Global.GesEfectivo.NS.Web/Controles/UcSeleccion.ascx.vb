Imports Prosegur.Framework.Dicionario.Tradutor

Public Class UcSeleccion
    Inherits System.Web.UI.UserControl

#Region "[METODOS]"

    ''' <summary>
    ''' Preenche os controles
    ''' </summary>
    ''' <param name="objItensDisponiveis"></param>
    ''' <remarks></remarks>
    Public Sub PreencherControle(objItensDisponiveis As ListItemCollection)

        If objItensDisponiveis IsNot Nothing AndAlso objItensDisponiveis.Count > 0 Then

            lstItensDisponiveis.Items.Clear()

            For Each item In objItensDisponiveis
                lstItensDisponiveis.Items.Add(item)
            Next

        End If

    End Sub

    ''' <summary>
    ''' Carrega os itens selecionados
    ''' </summary>
    ''' <param name="Itens"></param>
    ''' <remarks></remarks>
    Public Sub CarregarItensSelecionados(Itens As ListItemCollection)

        If Itens IsNot Nothing AndAlso Itens.Count > 0 Then

            lstItensSelecionadas.Items.Clear()

            For Each item As ListItem In Itens

                lstItensSelecionadas.Items.Add(item)
                lstItensDisponiveis.Items.Remove(item)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Retorna os itens selecionados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RetornarItensSelecionados() As ListItemCollection

        Dim objItensSelecionados As ListItemCollection = Nothing

        If lstItensSelecionadas.Items IsNot Nothing AndAlso lstItensSelecionadas.Items.Count > 0 Then

            objItensSelecionados = New ListItemCollection

            For Each item As ListItem In lstItensSelecionadas.Items
                objItensSelecionados.Add(item)
            Next

        End If

        Return objItensSelecionados
    End Function

    ''' <summary>
    ''' Retorna os itens selecionados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RetornarItenSelecionado() As ListItem

        Dim objItenSelecionado As ListItem = Nothing

        If lstItensSelecionadas.Items IsNot Nothing AndAlso lstItensSelecionadas.Items.Count > 0 Then

            objItenSelecionado = New ListItem

            For Each item As ListItem In lstItensSelecionadas.Items
                objItenSelecionado = item
            Next

        End If

        Return objItenSelecionado
    End Function

    ''' <summary>
    ''' Limpa o controle de itens disponiveis
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LimparControleItensDisponiveis()
        lstItensDisponiveis.Items.Clear()
    End Sub

    ''' <summary>
    ''' Limpa o controle de itens seleccionados
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LimparControleItensSeleccionados()
        lstItensSelecionadas.Items.Clear()
    End Sub

    Public Sub SetarFoco()
        lstItensDisponiveis.Focus()
    End Sub

    ''' <summary>
    ''' Habilita e desabilita o controle
    ''' </summary>
    ''' <param name="Habilitar"></param>
    ''' <remarks></remarks>
    Public Sub HabilitarControle(Habilitar As Boolean)

        lstItensDisponiveis.Enabled = Habilitar
        lstItensSelecionadas.Enabled = Habilitar
        imbAddAllItens.Enabled = Habilitar
        imbAddItens.Enabled = Habilitar
        imbRemoveAllItens.Enabled = Habilitar
        imbRemoveItens.Enabled = Habilitar

    End Sub

    ''' <summary>
    ''' Configura a visibilidade do controle
    ''' </summary>
    ''' <param name="ExhibirControle"></param>
    ''' <remarks></remarks>
    Public Sub ConfigurarControle(ExhibirControle As Boolean,
                         Optional ExhibirOrdenacion As Boolean = False,
                         Optional ExhibirTitulos As Boolean = False,
                         Optional ComprimentoLista As Integer = 200,
                         Optional AlturaLista As Integer = 150)

        DivPrincipal.Visible = ExhibirControle
        If ExhibirControle Then

            tdOrdenacion.Visible = ExhibirOrdenacion
            trTitulos.Visible = ExhibirTitulos

            lstItensDisponiveis.Width = ComprimentoLista
            lstItensDisponiveis.Height = AlturaLista
            lstItensSelecionadas.Width = lstItensDisponiveis.Width
            lstItensSelecionadas.Height = lstItensDisponiveis.Height

        End If

    End Sub

    ''' <summary>
    ''' Carregar titulos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherTitulos(TituloDisponibles As String, TituloSeleccionados As String)

        Me.tdTituloDisponiveis.InnerHtml = TituloDisponibles
        Me.tdTituloSeleccionados.InnerHtml = TituloSeleccionados

        imbAddAllItens.ToolTip = Traduzir("btnAgregarTodos")
        imbAddItens.ToolTip = Traduzir("btnAgregar")
        imbRemoveAllItens.ToolTip = Traduzir("btnRemoverTodos")
        imbRemoveItens.ToolTip = Traduzir("btnRemover")
        imbMoverItenArriba.ToolTip = Traduzir("btnMoverArriba")
        imbMoverItenAbajo.ToolTip = Traduzir("btnMoverAbajo")

    End Sub

    ''' <summary>
    ''' Seleciona todos os itens disponiveis.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SelecionarTodosItens()

        If lstItensDisponiveis.Items IsNot Nothing AndAlso lstItensDisponiveis.Items.Count > 0 Then

            For Each item In lstItensDisponiveis.Items
                lstItensSelecionadas.Items.Add(item)
            Next

            lstItensDisponiveis.Items.Clear()

        End If

    End Sub

    ''' <summary>
    ''' Verifica se o controle está preenchido.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ControlePreenchido() As Boolean

        If lstItensDisponiveis.Items.Count > 0 Then
            Return True
        End If

        Return False

    End Function

#End Region

#Region "[PROPRIEDADES]"

    Public ReadOnly Property TodosItensSeleccionados As Boolean
        Get
            Return (lstItensDisponiveis.Items Is Nothing OrElse lstItensDisponiveis.Items.Count = 0)
        End Get
    End Property

    Public ReadOnly Property VariosItensSeleccionados As Boolean
        Get
            Return (lstItensSelecionadas.Items IsNot Nothing AndAlso lstItensSelecionadas.Items.Count > 1)
        End Get
    End Property

    Public ReadOnly Property HayItensSeleccionados As Boolean
        Get
            Return (lstItensSelecionadas.Items IsNot Nothing AndAlso lstItensSelecionadas.Items.Count > 0)
        End Get
    End Property

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Seleciona todos os itens
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub imbAddAllItens_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbAddAllItens.Click

        Try

            SelecionarTodosItens()


        Catch ex As Exception

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(ex.ToString, Nothing) _
                                                       , True)
        End Try

    End Sub

    Private Sub imbRemoveAllItens_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbRemoveAllItens.Click

        Try

            If lstItensSelecionadas.Items IsNot Nothing AndAlso lstItensSelecionadas.Items.Count > 0 Then

                For Each item In lstItensSelecionadas.Items
                    lstItensDisponiveis.Items.Add(item)
                Next

                lstItensSelecionadas.Items.Clear()

            End If

        Catch ex As Exception

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(ex.ToString, Nothing) _
                                                       , True)
        End Try

    End Sub

    Private Sub imbAddItens_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbAddItens.Click

        Try

            If lstItensDisponiveis.Items IsNot Nothing AndAlso lstItensDisponiveis.Items.Count > 0 Then

                While lstItensDisponiveis.SelectedItem IsNot Nothing
                    Dim objListItem As ListItem
                    objListItem = lstItensDisponiveis.SelectedItem
                    lstItensDisponiveis.Items.Remove(objListItem)
                    lstItensSelecionadas.Items.Add(objListItem)
                End While

            End If

        Catch ex As Exception

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(ex.ToString, Nothing) _
                                                       , True)
        End Try

    End Sub

    Private Sub imbRemoveItens_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbRemoveItens.Click

        Try

            If lstItensSelecionadas.Items IsNot Nothing AndAlso lstItensSelecionadas.Items.Count > 0 Then

                While lstItensSelecionadas.SelectedItem IsNot Nothing
                    Dim objListItem As ListItem
                    objListItem = lstItensSelecionadas.SelectedItem
                    lstItensSelecionadas.Items.Remove(objListItem)
                    lstItensDisponiveis.Items.Add(objListItem)
                End While

            End If

        Catch ex As Exception

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(ex.ToString, Nothing) _
                                                       , True)
        End Try

    End Sub

    Private Sub imbMoverArriba_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbMoverItenArriba.Click

        Try

            If lstItensSelecionadas.Items IsNot Nothing AndAlso lstItensSelecionadas.Items.Count > 0 AndAlso lstItensSelecionadas.SelectedItem IsNot Nothing Then

                If lstItensSelecionadas.SelectedIndex > 0 Then

                    Dim item As ListItem = lstItensSelecionadas.SelectedItem

                    Dim I = lstItensSelecionadas.SelectedIndex - 1
                    lstItensSelecionadas.Items.RemoveAt(lstItensSelecionadas.SelectedIndex)
                    lstItensSelecionadas.Items.Insert(I, item)
                    lstItensSelecionadas.SelectedIndex = I

                End If

            End If

        Catch ex As Exception

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(ex.ToString, Nothing) _
                                                       , True)
        End Try

    End Sub

    Private Sub imbMoverAbajo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbMoverItenAbajo.Click

        Try

            If lstItensSelecionadas.Items IsNot Nothing AndAlso lstItensSelecionadas.Items.Count > 0 AndAlso lstItensSelecionadas.SelectedItem IsNot Nothing Then

                'Make sure our item is not the last one on the list.

                If lstItensSelecionadas.SelectedIndex < lstItensSelecionadas.Items.Count - 1 Then

                    'Insert places items above the index you supply, since we want
                    'to move it down the list we have to do + 2
                    Dim I = lstItensSelecionadas.SelectedIndex + 2
                    lstItensSelecionadas.Items.Insert(I, lstItensSelecionadas.SelectedItem)
                    lstItensSelecionadas.Items.RemoveAt(lstItensSelecionadas.SelectedIndex)
                    lstItensSelecionadas.SelectedIndex = I - 1

                End If

            End If

        Catch ex As Exception

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(ex.ToString, Nothing) _
                                                       , True)
        End Try

    End Sub

#End Region

End Class