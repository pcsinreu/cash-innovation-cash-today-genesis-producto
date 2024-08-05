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
    Public Class SectorRespuesta

#Region "[PROPRIEDADES]"

        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property CodigoSector As String

#End Region

    End Class

End Namespace
