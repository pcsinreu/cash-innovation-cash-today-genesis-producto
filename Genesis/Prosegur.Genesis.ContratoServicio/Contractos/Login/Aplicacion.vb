Namespace Entidades.Login
    Public Class Aplicacion
        Inherits EntidadBase

        Public Property Permisos As List(Of Permiso)

        ''' <summary>
        ''' Constructor por defecto de Aplicacion
        ''' </summary>
        Public Sub New()
            Permisos = New List(Of Permiso)()
        End Sub

        ''' <summary>
        ''' Constructor personalizado que devuelve una Aplicacion
        ''' </summary>
        ''' <param name="pIdentificador">OID_APLICACION</param>
        ''' <param name="pCodigo">COD_APLICACION</param>
        ''' <param name="pDescripcion">DES_APLICACION</param>
        Public Sub New(pIdentificador As String, pCodigo As String, pDescripcion As String)
            Me.New()
            Me.Identificador = pIdentificador
            Me.Codigo = pCodigo
            Me.Descripcion = pDescripcion
        End Sub

    End Class
End Namespace

