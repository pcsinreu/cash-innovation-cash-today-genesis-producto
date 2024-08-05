Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Terminos.RecuperarTerminosPorCodigos

    ''' <summary>
    ''' Clase ObtenerDenominacionesPeticion
    ''' </summary>
    ''' <descripcion>
    ''' Los parámetros seran filtrados si estuveren rellenados
    ''' </descripcion>
    ''' <remarks></remarks>
    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarTerminosPorCodigos")> _
    <XmlRoot(Namespace:="urn:RecuperarTerminosPorCodigos")> _
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Property CodigosTerminosIAC As List(Of String)

    End Class

End Namespace