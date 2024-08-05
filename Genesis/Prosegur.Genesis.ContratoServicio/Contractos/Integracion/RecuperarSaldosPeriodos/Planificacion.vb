Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosPeriodos
    <Serializable()>
    Public Class Planificacion
        Inherits Comon.Entidad


        <XmlAttributeAttribute()>
        Public Property CodigoBanco As String

        <XmlAttributeAttribute()>
        Public Property DescripcionBanco As String

        <XmlAttributeAttribute()>
        Public Property CodigoTipoPlanificacion As String

        Public Property Periodos As List(Of Periodo)
        Public Sub New()
            Periodos = New List(Of Periodo)()
        End Sub

    End Class
End Namespace
