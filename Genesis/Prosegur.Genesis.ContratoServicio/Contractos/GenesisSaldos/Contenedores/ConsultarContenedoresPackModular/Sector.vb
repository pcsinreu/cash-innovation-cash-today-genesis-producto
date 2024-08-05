Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresPackModular

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarContenedoresPackModular")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresPackModular")> _
    <Serializable()> _
    Public Class Sector

#Region "[PROPRIEDADES]"

        Public Property codDelegacion As String
        Public Property codPlanta As String
        Public Property codSector As String

#End Region

    End Class

End Namespace
