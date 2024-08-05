Imports System.Xml.Serialization
Imports System.Xml

Namespace bcp.RecuperarPedidosReportadosBCP

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/07/2012 - Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarPedidosReportadosBCP")> _
    <XmlRoot(Namespace:="urn:RecuperarPedidosReportadosBCP")> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _CodProceso As String
        Private _FechaHasta As Date
        Private _CodDelegacion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodProceso() As String
            Get
                Return _CodProceso
            End Get
            Set(value As String)
                _CodProceso = value
            End Set
        End Property

        Public Property FechaHasta() As Date
            Get
                Return _FechaHasta
            End Get
            Set(value As Date)
                _FechaHasta = value
            End Set
        End Property

        Public Property CodDelegacion() As String
            Get
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property

#End Region

    End Class
End Namespace