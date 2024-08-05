Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarTransacciones

    <XmlType(Namespace:="urn:RecuperarTransacciones")>
    <XmlRoot(Namespace:="urn:RecuperarTransacciones")>
    <Serializable()>
    Public Class Peticion

        ' Delegacion GMT
        Public Property Oid_delegacionGMT As String

        ' Tipos de Transacciones
        Public Property TipoTransacciones As List(Of String)

        ' Modalidad
        Public Property Modalidad As String

        ' Notificación
        Public Property Notificacion As String

        ' Acreditacion
        Public Property Acreditacion As String

        ' Delegaciones
        Public Property Delegaciones As List(Of String)

        ' Canales
        Public Property Canales As List(Of String)

        ' Fechas
        Public Property Fecha As Prosegur.Genesis.Comon.Enumeradores.TipoFechas

        ' Fecha Desde
        Public Property FechaDesde As DateTime?

        ' Fecha Hasta
        Public Property FechaHasta As DateTime?

        ' Fecha Gestion
        Public Property FechaGestion As DateTime?

        ' Clientes
        Public Property Clientes As List(Of String)
        ' Clientes
        Public Property Subclientes As List(Of String)

        ' Maquinas
        Public Property Maquinas As List(Of String)

        ' Sectores
        Public Property Sectores As List(Of String)

        ' Puntos de Servicios
        Public Property PuntosServicios As List(Of String)

        ' BancosCapital
        Public Property BancosCapital As List(Of String)

        ' BancosComision
        Public Property BancosComision As List(Of String)

        ' BancosTesoreria
        Public Property BancosTesoreria As List(Of String)

        ' CuentaTesoreria
        Public Property CuentaTesoreria As List(Of String)

        ' Tipos Planificaciones
        Public Property TipoPlanificaciones As List(Of String)

        ' Planificaciones
        Public Property Planificaciones As List(Of String)

        Public Property ImporteInformativo As Boolean

        ' CampoExtra + CampoExtraValor - PGP-509
        Public Property CampoExtra As String
        Public Property CampoExtraValor As String

    End Class

End Namespace