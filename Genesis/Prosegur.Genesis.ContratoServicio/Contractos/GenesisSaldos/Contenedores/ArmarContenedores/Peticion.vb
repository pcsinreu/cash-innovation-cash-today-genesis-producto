Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ArmarContenedores

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ArmarContenedores")> _
    <XmlRoot(Namespace:="urn:ArmarContenedores")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property Documento As Documento

#End Region

    End Class

End Namespace