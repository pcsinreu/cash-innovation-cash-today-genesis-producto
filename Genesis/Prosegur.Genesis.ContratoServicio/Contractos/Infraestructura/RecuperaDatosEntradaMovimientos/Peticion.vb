Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon

Namespace Contractos.Infraestructura.RecuperarDatosEntradaMovimientos
    <XmlType(Namespace:="urn:RecuperarDatosEntradaMovimientos.Entrada")>
    <XmlRoot(Namespace:="urn:RecuperarDatosEntradaMovimientos.Entrada")>
    <Serializable()>
    Public Class Peticion
        Inherits BaseRequest

        Public Property Filtro As Entrada.Filtro

    End Class
End Namespace

