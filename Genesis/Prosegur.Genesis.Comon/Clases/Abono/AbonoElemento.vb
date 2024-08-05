Namespace Clases.Abono
    <Serializable()>
    Public Class AbonoElemento
        Public Sub New()
            'Me.Divisa = New DivisaAbono()
        End Sub
        Public Property Identificador As String
        Public Property UsuarioCreacion As String
        Public Property UsuarioModificacion As String
        Public Property Divisa As DivisaAbono
        Public Property Importe As Double

        Public Property IdentificadorRemesa As String
        Public Property IdentificadorBulto As String
        Public Property Codigo As String
        ''' <summary>
        ''' Número externo ou Precinto
        ''' </summary>
        Public Property CodigoElemento As String
    End Class
End Namespace
