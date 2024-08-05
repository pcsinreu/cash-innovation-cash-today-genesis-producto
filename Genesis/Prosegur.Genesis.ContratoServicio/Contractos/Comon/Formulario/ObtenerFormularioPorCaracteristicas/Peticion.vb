Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Formulario.ObtenerFormularioPorCaractetisticas

    ''' <summary>
    ''' Clase ObtenerDenominacionesPeticion
    ''' </summary>
    ''' <descripcion>
    ''' Los parámetros seran filtrados si estuveren rellenados
    ''' </descripcion>
    ''' <remarks></remarks>
    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerFormularioPorCaractetisticas")> _
    <XmlRoot(Namespace:="urn:ObtenerFormularioPorCaractetisticas")> _
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Property CaracteristicasFormulario As New List(Of Enumeradores.CaracteristicaFormulario)

    End Class

End Namespace