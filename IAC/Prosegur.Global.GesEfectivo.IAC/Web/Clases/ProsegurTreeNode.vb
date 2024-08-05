Public Class ProsegurTreeNode
    Inherits TreeNode

    Public Function Clonar() As TreeNode
        Return Me.Clone()
    End Function

    Public Function Clonar(ObjTreeNode As TreeNode) As TreeNode
        Return Nothing
    End Function

    Public Function MemberwiseClonar() As TreeNode
        Return Me.MemberwiseClone()
    End Function

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(text As String)
        MyBase.New(text)
    End Sub

    Public Sub New(text As String, value As String)
        MyBase.New(text, value)        
    End Sub




End Class
