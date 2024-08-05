Imports Prosegur.Genesis.Comon.Clases
Namespace Clases
    Public Class RoleXUsuario
        Public Sub New()
            Role = New Rol()
            Pais = New Pais
        End Sub

        Public Property Identificador As String
        Public Property Role As Rol
        Public Property Pais As Pais
        Public Property Activo As Boolean

        Public ReadOnly Property PaisRole() As String
            Get
                Return $"{Pais.Codigo} - {Role.Codigo}"
            End Get
        End Property
    End Class
End Namespace
