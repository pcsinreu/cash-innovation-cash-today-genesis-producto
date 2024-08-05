Namespace Entidades.Login
    Public Class Rol
        Inherits EntidadBase
        Public Property Permisos As List(Of Permiso)


        ''' <summary>
        ''' Constructor por defecto de Rol
        ''' </summary>
        Public Sub New()
            Permisos = New List(Of Permiso)()
        End Sub

        ''' <summary>
        ''' Constructor que devuelve un Rol
        ''' </summary>
        ''' <param name="pIdentificador">OID_ROLE</param>
        ''' <param name="pCodigo">COD_ROLE</param>
        ''' <param name="pDescripcion">DES_ROLE</param>
        Public Sub New(pIdentificador As String, pCodigo As String, pDescripcion As String)
            Me.New()
            Me.Identificador = pIdentificador
            Me.Codigo = pCodigo
            Me.Descripcion = pDescripcion
        End Sub
    End Class
End Namespace

