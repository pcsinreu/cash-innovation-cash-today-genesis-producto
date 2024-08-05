Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.ContratoBase

Namespace Contractos.Integracion.ModificarMovimientos
    <XmlType(Namespace:="urn:ModificarMovimientos")>
    <XmlRoot(Namespace:="urn:ModificarMovimientos")>
    <Serializable()>
    Public Class Peticion

        ''' <summary>
        ''' Colección de movimientos.
        ''' </summary>
        ''' <returns></returns>
        Public Property Configuracion As Configuracion
        ''' <summary>
        ''' Colección de movimientos.
        ''' </summary>
        ''' <returns></returns>
        Public Property Movimientos As List(Of Entrada.Movimiento)

    End Class
End Namespace