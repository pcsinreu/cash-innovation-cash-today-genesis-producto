Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.GrabarInventarioContenedor

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:GrabarInventarioContenedor")> _
    <XmlRoot(Namespace:="urn:GrabarInventarioContenedor")> _
    <Serializable()> _
    Public Class Cliente

#Region "[PROPRIEDADES]"

        Public Property codCliente As String
        Public Property codSubcliente As String
        Public Property codPuntoServicio As String

#End Region

    End Class

End Namespace
