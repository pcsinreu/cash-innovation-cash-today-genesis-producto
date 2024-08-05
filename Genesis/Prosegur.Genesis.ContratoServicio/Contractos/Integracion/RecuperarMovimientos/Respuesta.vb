Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarMovimientos

    <XmlType(Namespace:="urn:RecuperarMovimientos")>
    <XmlRoot(Namespace:="urn:RecuperarMovimientos")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse

        ''' <summary>
        ''' Estructura donde vendrá informado el movimiento a crear
        ''' </summary>

        Public Property Paginacion As Comon.Paginacion
        Public Property Delegaciones As List(Of Delegacion)

    End Class

End Namespace
