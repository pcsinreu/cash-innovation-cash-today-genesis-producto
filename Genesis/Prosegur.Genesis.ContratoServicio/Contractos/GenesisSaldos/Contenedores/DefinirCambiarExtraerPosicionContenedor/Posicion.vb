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
    Public Class Posicion

#Region "[PROPRIEDADES]"

        Public Property codPrecinto As String
        Public Property codPosicion As String
        Public Property codPosicionDestino As String

#End Region

    End Class

End Namespace
