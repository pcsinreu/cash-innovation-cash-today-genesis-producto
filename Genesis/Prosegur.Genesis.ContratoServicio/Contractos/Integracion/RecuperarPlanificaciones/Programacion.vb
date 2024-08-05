Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarPlanificaciones

    <XmlType(Namespace:="urn:RecuperarPlanificaciones")>
    <XmlRoot(Namespace:="urn:RecuperarPlanificaciones")>
    <Serializable()>
    Public Class Programacion
        Public Property HoraInicio As String
        Public Property DiaInicio As Integer
        Public Property HoraFin As String
        Public Property DiaFin As Integer

    End Class

End Namespace