Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon

Namespace Contractos.Infraestructura.RecuperarDatosLogger
    <XmlType(Namespace:="urn:RecuperarDatosLogger.Entrada")>
    <XmlRoot(Namespace:="urn:RecuperarDatosLogger.Entrada")>
    <Serializable()>
    Public Class Peticion
        Inherits BaseRequest

        Public Property Filtro As Entrada.Filtro

    End Class
End Namespace

