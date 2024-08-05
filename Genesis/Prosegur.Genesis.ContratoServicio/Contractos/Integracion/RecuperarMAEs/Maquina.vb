Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarMAEs

    <Serializable()>
    Public Class Maquina

        Public Property Identificador As String
        Public Property DeviceID As String
        Public Property Descripcion As String
        Public Property Vigente As Boolean
        Public Property ConsideraRecuentos As Boolean
        Public Property Modelo As Comon.Entidad
        Public Property Marca As Comon.Entidad
        Public Property Planta As Comon.Entidad
        Public Property Delegacion As Comon.Entidad
        Public Property PuntosServicio As List(Of PuntosServicio)
        Public Property Planificacion As Planificacion
        Public Property CodigosAjenos As List(Of CodigoAjeno)
        Public Property Limites As List(Of Limite)
    End Class

End Namespace