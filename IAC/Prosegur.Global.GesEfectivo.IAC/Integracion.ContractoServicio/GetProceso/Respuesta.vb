Imports System.Xml.Serialization
Imports System.Xml

Namespace GetProceso

    <XmlType(Namespace:="urn:GetProceso")> _
    <XmlRoot(Namespace:="urn:GetProceso")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _proceso As Proceso
#End Region

#Region "Propriedades"

        Public Property Proceso() As Proceso
            Get
                Return _proceso
            End Get
            Set(value As Proceso)
                _proceso = value
            End Set
        End Property
#End Region
    End Class

End Namespace