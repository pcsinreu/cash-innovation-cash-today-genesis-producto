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
    Public Class Sector

#Region "[PROPRIEDADES]"

        Public Property codDelegacion As String
        Public Property codPlanta As String
        Public Property codSector As String
        Public Property packModular As String


#End Region

    End Class

End Namespace
