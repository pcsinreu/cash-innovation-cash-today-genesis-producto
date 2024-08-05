Namespace Clases
    Public Class Permiso
        Public Property Identificador() As String
        Public Property Codigo() As String
        Public Property Descripcion() As String
        Public Property Activo() As Boolean
        Public Property Aplicacion() As Aplicacion

        Public ReadOnly Property AplicacionCodigo() As String
            Get
                Return $"{Aplicacion.Codigo} - {Codigo}"
            End Get
        End Property

    End Class
End Namespace
