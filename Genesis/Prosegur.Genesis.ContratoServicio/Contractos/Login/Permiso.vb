Namespace Entidades.Login
    Public Class Permiso
        Inherits EntidadBase

        ''' <summary>
        ''' Constructor por defecto de Permiso
        ''' </summary>
        Public Sub New()

        End Sub


        ''' <summary>
        ''' Método constructor de un Permiso
        ''' </summary>
        ''' <param name="pIdentificador">OID_PERMISO</param>
        ''' <param name="pCodigo">COD_PERMISO</param>
        ''' <param name="pDescripcion">DES_PERMISO</param>
        Public Sub New(pIdentificador As String, pCodigo As String, pDescripcion As String)
            Me.New()
            Me.Identificador = pIdentificador
            Me.Codigo = pCodigo
            Me.Descripcion = pDescripcion
        End Sub
    End Class
End Namespace

