Imports System.Xml.Serialization
Imports System.Xml

Namespace DetalleParciales.GetDetalleParciales

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:DetalleParciales")> _
    <XmlRoot(Namespace:="urn:DetalleParciales")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _DetalleParciales As List(Of DetalleParcial)

#End Region

#Region "Propriedades"

        Public Property DetalleParciales() As List(Of DetalleParcial)
            Get
                Return _DetalleParciales
            End Get
            Set(value As List(Of DetalleParcial))
                _DetalleParciales = value
            End Set
        End Property

#End Region

    End Class

End Namespace