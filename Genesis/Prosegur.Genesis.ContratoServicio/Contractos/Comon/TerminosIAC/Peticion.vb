Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Terminos.RecuperarTerminosIAC

    ''' <summary>
    ''' Clase ObtenerDenominacionesPeticion
    ''' </summary>
    ''' <descripcion>
    ''' Los parámetros seran filtrados si estuveren rellenados
    ''' </descripcion>
    ''' <remarks></remarks>
    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarTerminosIAC")> _
    <XmlRoot(Namespace:="urn:RecuperarTerminosIAC")> _
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Property IdentificadorIAC As String

    End Class

End Namespace