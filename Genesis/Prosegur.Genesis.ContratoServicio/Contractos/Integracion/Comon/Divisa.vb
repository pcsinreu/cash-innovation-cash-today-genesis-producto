Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <XmlType(Namespace:="urn:Comon")> _
    <XmlRoot(Namespace:="urn:Comon")> _
    <Serializable()>
    Public Class Divisa

        ''' <summary>
        ''' Código ISO de la Divisa
        ''' </summary>
        Public Property codigoDivisa As String

        ''' <summary>
        ''' Colección de denominaciones de divisa
        ''' </summary>
        Public Property denominaciones As List(Of Denominacion)

        ''' <summary>
        ''' Importe de Divisa ( a nivel total por Divisa)
        ''' </summary>
        Public Property importe As Double

    End Class

End Namespace