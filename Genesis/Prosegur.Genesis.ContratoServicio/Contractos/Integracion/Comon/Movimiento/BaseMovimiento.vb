Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon.Movimientos
    <Serializable()>
    Public Class BaseMovimiento
        Public Property DeviceID As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property ActualId As String
        Public Property CollectionId As String
        Public Property FechaHora As DateTime
        Public Property FechaContable As DateTime?
        Public Property CamposExtras As List(Of Comon.Movimientos.CampoExtra)
        Public Property Detalles As List(Of Comon.Movimientos.Detalle)
    End Class


    Public Class RespuestaPeriodosMasivos
        Public Property IdentificadorMaquina As String
        Public Property TipoResultado As String
        Public Property DeviceID As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property CodigoCanal As String
        Public Property CodigoSubCanal As String
        Public Property CodigoSectorGenesis As String
        Public Property CodigoCodPlantaGenesis As String
        Public Property CodigoDelegacionGenesis As String
        Public Property validaciones As List(Of ContractoServicio.Contractos.Integracion.Comon.Entidad)
    End Class


    Public Class PeticionPeriodosMasivos
        Public Property Movimientos As List(Of Movimiento)
        Public Property CodigoUsuario As String
        Public Property CodigoAjeno As String
    End Class

    Public Class Movimiento

        Public Property Index As String
        Public Property DeviceID As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property CodigoCanal As String
        Public Property CodigoSubCanal As String
        Public Property CodigoDivisa As String
        Public Property FechaHora As DateTime

    End Class
End Namespace