Namespace Clases
    Public Class Rol
        Public Property Identificador() As String
        Public Property Codigo() As String
        Public Property Descripcion() As String
        Public Property Activo() As Boolean
        Public Property Permisos() As List(Of Permiso)
        Public Property Aplicacion() As Aplicacion

        Public ReadOnly Property AplicacionCodigo() As String
            Get
                Return $"{Aplicacion.Codigo} - {Codigo}"
            End Get
        End Property
    End Class
End Namespace
