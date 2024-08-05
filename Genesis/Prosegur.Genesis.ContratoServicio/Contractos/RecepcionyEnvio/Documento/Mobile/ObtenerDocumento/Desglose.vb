Imports System.Xml.Serialization

Namespace Documento.Mobile.ObtenerDocumento

    <Serializable()>
    Public Class Desglose

        Public Property IdDesglose As String
        Public Property DesDenominacion As String
        Public Property Cantidad As Integer
        Public Property Importe As Decimal 'Es el tipo correcto?

    End Class

End Namespace