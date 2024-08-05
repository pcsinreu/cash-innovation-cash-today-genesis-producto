Namespace Contractos.Integracion.RecuperarTransacciones
    Public Class RespuestaDetalleValores

        Public Property Canal_Subcanal As String
        Public Property Canal As String
        Public Property Subcanal As String
        Public Property Divisa As String
        Public Property Denominacion As String
        Public Property Simbolo As String
        Public Property Importe As Double
        Public Property Cantidad As Integer
        Public Property OidCuenta As String
        Public Property ImporteFormatado() As String

            Get
                Return Simbolo + " " + Convert.ToDouble(Importe).ToString("N2")
            End Get
            Set(ByVal value As String)
            End Set
        End Property

    End Class
End Namespace