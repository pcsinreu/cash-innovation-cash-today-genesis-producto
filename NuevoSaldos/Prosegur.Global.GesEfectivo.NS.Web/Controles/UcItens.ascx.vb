﻿Imports Prosegur.Framework.Dicionario.Tradutor

Public Class UcItens
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
    ''' <param name="Visivel"></param>
    ''' <remarks></remarks>
    Public Sub ConfigurarVisibilidade(Visivel As Boolean)

        DivPrincipal.Visible = Visivel

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Titulo"></param>
    ''' <remarks></remarks>
    Public Sub PreencherTitulo(Titulo As String)

        lblTitulo.Text = Titulo

        imbAddAllItens.ToolTip = Traduzir("btnAgregarTodos")
        imbAddItens.ToolTip = Traduzir("btnAgregar")
        imbRemoveAllItens.ToolTip = Traduzir("btnRemoverTodos")
        imbRemoveItens.ToolTip = Traduzir("btnRemover")

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

#End Region

End Class