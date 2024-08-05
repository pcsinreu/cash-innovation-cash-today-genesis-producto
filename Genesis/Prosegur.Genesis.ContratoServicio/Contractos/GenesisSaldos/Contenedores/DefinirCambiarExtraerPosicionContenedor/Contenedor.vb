Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.DefinirCambiarExtraerPosicionContenedor

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:DefinirCambiarExtraerPosicionContenedor")> _
    <XmlRoot(Namespace:="urn:DefinirCambiarExtraerPosicionContenedor")> _
    <Serializable()> _
    Public Class Contenedor

#Region "[PROPRIEDADES]"

        Public Property Posicion As Posicion

#End Region

    End Class
End Namespace