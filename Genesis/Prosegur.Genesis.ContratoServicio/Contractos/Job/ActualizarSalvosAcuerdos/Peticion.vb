Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Namespace Contractos.Job.ActualizarSaldosAcuerdos
    <XmlType(Namespace:="urn:ActualizarSaldosAcuerdos.Entrada")>
    <XmlRoot(Namespace:="urn:ActualizarSaldosAcuerdos.Entrada")>
    <Serializable()>
    Public Class Peticion
        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property ForzarCalculoDiaActual As Boolean
    End Class
End Namespace
