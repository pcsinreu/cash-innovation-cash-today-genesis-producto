Namespace Clases.Abono
    <Serializable()>
    Public Class SnapshotSaldo

        Public Sub New()
            Me.Divisa = New DivisaAbono()
        End Sub


        Public Property IdentificadorCuenta As String
        Public Property Divisa As DivisaAbono
        Public Property Importe As Double

        Public Property UsuarioCreacion As String
        Public Property UsuarioModificacion As String
    End Class
End Namespace
