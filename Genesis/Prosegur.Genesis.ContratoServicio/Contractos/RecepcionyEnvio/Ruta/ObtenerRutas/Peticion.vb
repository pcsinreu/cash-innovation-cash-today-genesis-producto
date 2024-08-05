Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Ruta.ObtenerRutas

    <XmlType(Namespace:="urn:ObtenerRutas")> _
    <XmlRoot(Namespace:="urn:ObtenerRutas")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property UrlSol As String = String.Empty
        Public Property CodigoPais As String = String.Empty
        Public Property CodigoDelegacion As String = String.Empty
        Public Property CodigoDelegacionLV As String = String.Empty
        Public Property CodigoRuta As String = String.Empty
        Public Property FechaRuta As DateTime
        Public Property EsOrigenProsegur As Boolean
        Public Property CodigoIdioma As String
        Public Property Operacion As Integer
        Public Property BuscarOtNOProgramada As Boolean = False
        Public Property retornarDocumentos As Boolean = False
        Public Property ValidarEstadoRuta As Boolean = False
        Public Property gestionaPorBulto As Boolean?
        Public Property CalculoAutomaticoDeclaradoTotal As Boolean
    End Class

End Namespace