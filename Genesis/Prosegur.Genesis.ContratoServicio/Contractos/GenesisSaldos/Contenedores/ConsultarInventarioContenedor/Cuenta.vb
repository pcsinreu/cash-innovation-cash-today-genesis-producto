Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarInventarioContenedor

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarInventarioContenedor")> _
    <XmlRoot(Namespace:="urn:ConsultarInventarioContenedor")> _
    <Serializable()> _
    Public Class Cuenta

#Region "[PROPRIEDADES]"

        Public Property Cliente As Cliente
        Public Property Canal As Canal

        Public DetalleEfectivo As List(Of DetalleEfectivo)
        Public DetalleMedioPago As List(Of DetalleMedioPago)

#End Region

    End Class

End Namespace
