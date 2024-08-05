Public Class ucCheckBoxList
    Inherits System.Web.UI.UserControl
    Public Property AutoPostBack As Boolean
    Public Event SelectedChanged As EventHandler

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub Popular(DataTextField As String, DataValueField As String, dataSource As Object)
        Me.cbl.DataTextField = "Descripcion"
        Me.cbl.DataValueField = "Codigo"
        Me.cbl.DataSource = dataSource
        Me.cbl.DataBind()
    End Sub

    ''' <summary>
    ''' Preenche os valores selecionados
    ''' </summary>
    ''' <param name="Codigos"></param>
    ''' <remarks></remarks>
    Public Sub PreencherValoresSeleccionados(Codigos As List(Of String))

        If cbl.Items IsNot Nothing AndAlso cbl.Items.Count > 0 Then

            For Each item As ListItem In cbl.Items

                If Codigos.Contains(item.Value) Then
                    item.Selected = True
                End If

            Next

            If Codigos.Count = cbl.Items.Count Then
                chkTodos.Checked = True
            End If

        End If

    End Sub

    Public Sub Limpar()
        Me.cbl.Items.Clear()
        Me.chkTodos.Checked = False
    End Sub

    Public Function Selecionados() As List(Of String)
        Dim lista As New List(Of String)
        Dim sb As New StringBuilder
        For Each item In cbl.Items.Cast(Of ListItem)().Where(Function(l) l.Selected)
            lista.Add(item.Value)
        Next

        Return lista
    End Function
    Protected Sub cbl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbl.SelectedIndexChanged
        ' verifica se todos os itens estão selecionados
        If cbl.Items.Cast(Of ListItem)().Where(Function(l) l.Selected).Count = cbl.Items.Count Then
            Me.chkTodos.Checked = True
        Else
            Me.chkTodos.Checked = False
        End If

        If (Me.AutoPostBack) Then
            RaiseEvent SelectedChanged(Me, e)
        End If
    End Sub

    Protected Sub chkTodos_CheckedChanged(sender As Object, e As EventArgs) Handles chkTodos.CheckedChanged
        If Me.chkTodos.Checked Then
            For Each item In cbl.Items.Cast(Of ListItem)().Where(Function(l) Not l.Selected)
                item.Selected = True
            Next
        Else
            Me.cbl.ClearSelection()
        End If

        If (Me.AutoPostBack) Then
            RaiseEvent SelectedChanged(Me, e)
        End If
    End Sub

End Class