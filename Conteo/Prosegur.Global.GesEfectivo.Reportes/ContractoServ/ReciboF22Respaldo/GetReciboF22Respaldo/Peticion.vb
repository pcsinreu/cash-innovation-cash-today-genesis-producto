Imports System.Xml.Serialization
Imports System.Xml

Namespace ReciboF22Respaldo.GetReciboF22Respaldo

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:RespaldoCompleto")> _
    <XmlRoot(Namespace:="urn:RespaldoCompleto")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _CodigoCliente As String
        Private _CodigoDelegacion As String
        Private _FechaDesde As Date
        Private _FechaHasta As Date        

#End Region

#Region " Propriedades "

        Public Property CodigoCliente() As String
            Get
                Return _CodigoCliente
            End Get
            Set(value As String)
                _CodigoCliente = value
            End Set
        End Property

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property FechaDesde() As Date
            Get
                Return _FechaDesde
            End Get
            Set(value As Date)
                _FechaDesde = value
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

#End Region

    End Class

End Namespace
