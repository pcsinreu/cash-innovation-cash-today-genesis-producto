Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarSeguimientoElemento

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <XmlRoot(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <Serializable()> _
    Public Class Sector

#Region "[PROPRIEDADES]"

        Public Property codDelegacion As String
        Public Property codPlanta As String
        Public Property codSector As String
        Public Property desDelegacion As String
        Public Property desPlanta As String
        Public Property desSector As String

#End Region

    End Class

End Namespace
