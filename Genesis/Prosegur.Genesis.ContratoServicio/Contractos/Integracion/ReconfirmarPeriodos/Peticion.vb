Imports System.Xml.Serialization

Namespace Contractos.Integracion.ReconfirmarPeriodos
    <XmlType(Namespace:="urn:ReconfirmarPeriodos")>
    <XmlRoot(Namespace:="urn:ReconfirmarPeriodos")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest
        Public Property CodigoPais As String

        'Instanciamos la lista de Periodos
        Public Property Periodos As List(Of Periodo) = New List(Of Periodo)()

    End Class
End Namespace