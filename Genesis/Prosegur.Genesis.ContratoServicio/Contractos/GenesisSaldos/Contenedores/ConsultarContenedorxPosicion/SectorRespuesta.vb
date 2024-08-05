Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedorxPosicion

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarContenedorxPosicion")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedorxPosicion")> _
    <Serializable()> _
    Public Class SectorRespuesta

#Region "[PROPRIEDADES]"

        Public Property oidSector As String
        Public Property codSector As String
        Public Property desSector As String
        Public Property oidDelegacion As String
        Public Property codDelegacion As String
        Public Property desDelegacion As String
        Public Property oidPlanta As String
        Public Property codPlanta As String
        Public Property desPlanta As String
        Public Property codPosicion As String

#End Region

    End Class

End Namespace
