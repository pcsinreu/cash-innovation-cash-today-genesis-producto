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
    Public Class Peticion

#Region "[PROPRIEDADES]"
        Public Property Inventario As Inventario

#End Region

    End Class

End Namespace