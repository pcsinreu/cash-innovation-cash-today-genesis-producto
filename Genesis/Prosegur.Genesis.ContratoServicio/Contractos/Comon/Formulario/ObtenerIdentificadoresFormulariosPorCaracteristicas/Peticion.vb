Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Formulario.ObtenerIdentificadoresFormulariosPorCaracteristicas

    ''' <summary>
    ''' Clase ObtenerDenominacionesPeticion
    ''' </summary>
    ''' <descripcion>
    ''' Los parámetros seran filtrados si estuveren rellenados
    ''' </descripcion>
    ''' <remarks></remarks>
    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerIdentificadoresFormulariosPorCaracteristicas")> _
    <XmlRoot(Namespace:="urn:ObtenerIdentificadoresFormulariosPorCaracteristicas")> _
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Property CaracteristicasFormulario As New List(Of Enumeradores.CaracteristicaFormulario)

    End Class

End Namespace