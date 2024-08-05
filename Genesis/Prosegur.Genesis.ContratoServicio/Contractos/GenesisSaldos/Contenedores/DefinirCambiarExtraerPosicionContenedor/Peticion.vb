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
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property Contenedores As List(Of Contenedor)
        Public Property Sector As Sector
        Public Property UsuarioCreacion As String

#End Region

    End Class

End Namespace