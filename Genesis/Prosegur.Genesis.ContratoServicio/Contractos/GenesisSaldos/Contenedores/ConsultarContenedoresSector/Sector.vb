Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresSector

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarContenedoresSector")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresSector")> _
    <Serializable()> _
    Public Class Sector

#Region "[PROPRIEDADES]"

        Public Property codSector As String

#End Region

    End Class

End Namespace
