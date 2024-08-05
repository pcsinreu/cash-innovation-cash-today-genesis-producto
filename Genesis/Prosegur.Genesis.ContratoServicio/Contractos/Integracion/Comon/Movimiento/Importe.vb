Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon.Movimientos
    <Serializable()>
    Public Class Importe

        Public Property CodigoDivisa As String

        <XmlArray("Denominaciones"), XmlArrayItem(GetType(Comon.Denominacion), ElementName:="Denominacion")>
        Public Property Denominaciones As List(Of Denominacion)
    End Class
End Namespace