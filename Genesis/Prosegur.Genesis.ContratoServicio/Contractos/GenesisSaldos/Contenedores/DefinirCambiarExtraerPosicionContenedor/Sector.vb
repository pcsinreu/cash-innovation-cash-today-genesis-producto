Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.DefinirCambiarExtraerPosicionContenedor

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:DefinirCambiarExtraerPosicionContenedor")> _
    <XmlRoot(Namespace:="urn:DefinirCambiarExtraerPosicionContenedor")> _
    <Serializable()> _
    Public Class Sector

#Region "[PROPRIEDADES]"

        Public Property codDelegacion As String
        Public Property codPlanta As String
        Public Property codSector As String

#End Region

    End Class

End Namespace
