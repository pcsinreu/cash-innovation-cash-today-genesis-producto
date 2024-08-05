Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfirmarPeriodos
    <XmlType(Namespace:="urn:ConfirmarPeriodos")>
    <XmlRoot(Namespace:="urn:ConfirmarPeriodos")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest
        Public Property CodigoPais As String

        'Instanciamos la lista de Periodos
        Public Property Periodos As List(Of Periodo) = New List(Of Periodo)()

    End Class
End Namespace