Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization
' GenesisSaldos.Contenedores.Comon

Namespace GenesisSaldos.Contenedores.DesarmarContenedores

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:DesarmarContenedores")> _
    <XmlRoot(Namespace:="urn:DesarmarContenedores")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property Documento As GenesisSaldos.Contenedores.Comon.Documento

#End Region

    End Class

End Namespace