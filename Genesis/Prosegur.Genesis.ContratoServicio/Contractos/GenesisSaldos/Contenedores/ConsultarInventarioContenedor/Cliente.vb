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
    Public Class Cliente

#Region "[PROPRIEDADES]"

        Public Property codCliente As String
        Public Property desCliente As String
        Public Property codSubcliente As String
        Public Property desSubcliente As String
        Public Property codPuntoServicio As String
        Public Property desPuntoServicio As String

#End Region

    End Class

End Namespace
