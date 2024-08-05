Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarPlanificaciones

    <XmlType(Namespace:="urn:RecuperarPlanificaciones")>
    <XmlRoot(Namespace:="urn:RecuperarPlanificaciones")>
    <Serializable()>
    Public Class Planificacione
        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property FechaHoraVigenciaInicio As DateTime
        Public Property FechaHoraVigenciaFin As DateTime
        Public Property Vigente As Boolean
        Public Property MinutosAcreditacion As Integer
        Public Property Tipo As Comon.Entidad
        Public Property Banco As Comon.Entidad
        Public Property Deleagacion As Comon.Entidad
        Public Property Canales As List(Of Comon.Entidad)
        Public Property Programaciones As List(Of Programacion)
        Public Property Maquinas As List(Of Maquina)
        Public Property Limites As List(Of Limite)

        Public Property Divisas As List(Of Divisa)
        Public Property Movimientos As List(Of Formulario)
    End Class

End Namespace