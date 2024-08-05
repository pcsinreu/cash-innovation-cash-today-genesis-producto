Imports System.Xml.Serialization
Imports System.Xml


Namespace Proceso.SetProceso
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 17/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetProceso")> _
    <XmlRoot(Namespace:="urn:SetProceso")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _codigoDelegacion As String
        Private _codigoCliente As String
        Private _descripcionCliente As String
        Private _codigoSubcliente As String
        Private _descripcionSubcliente As String
        Private _codigoPuntoServicio As String
        Private _descripcionPuntoServicio As String
        Private _codigoSubcanal As String
        Private _descripcionSubcanal As String
        Private _identificadorProceso As String
        Private _identificadorProcesoPuntoServicio As String
        Private _identificadorProcesoPtoServicioSubcanal As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoDelegacion() As String
            Get
                Return _codigoDelegacion
            End Get
            Set(value As String)
                _codigoDelegacion = value
            End Set
        End Property

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

        Public Property DescripcionCliente() As String
            Get
                Return _descripcionCliente
            End Get
            Set(value As String)
                _descripcionCliente = value
            End Set
        End Property

        Public Property CodigoSubcliente() As String
            Get
                Return _codigoSubcliente
            End Get
            Set(value As String)
                _codigoSubcliente = value
            End Set
        End Property

        Public Property DescripcionSubcliente() As String
            Get
                Return _descripcionSubcliente
            End Get
            Set(value As String)
                _descripcionSubcliente = value
            End Set
        End Property

        Public Property CodigoPuntoServicio() As String
            Get
                Return _codigoPuntoServicio
            End Get
            Set(value As String)
                _codigoPuntoServicio = value
            End Set
        End Property

        Public Property DescripcionPuntoServicio() As String
            Get
                Return _descripcionPuntoServicio
            End Get
            Set(value As String)
                _descripcionPuntoServicio = value
            End Set
        End Property

        Public Property CodigoSubcanal() As String
            Get
                Return _codigoSubcanal
            End Get
            Set(value As String)
                _codigoSubcanal = value
            End Set
        End Property

        Public Property DescripcionSubcanal() As String
            Get
                Return _descripcionSubcanal
            End Get
            Set(value As String)
                _descripcionSubcanal = value
            End Set
        End Property

        Public Property IdentificadorProceso() As String
            Get
                Return _identificadorProceso
            End Get
            Set(value As String)
                _identificadorProceso = value
            End Set
        End Property

        Public Property IdentificadorProcesoPuntoServicio() As String
            Get
                Return _identificadorProcesoPuntoServicio
            End Get
            Set(value As String)
                _identificadorProcesoPuntoServicio = value
            End Set
        End Property

        Public Property IdentificadorProcesoPtoServicioSubcana() As String
            Get
                Return _identificadorProcesoPtoServicioSubcanal
            End Get
            Set(value As String)
                _identificadorProcesoPtoServicioSubcanal = value
            End Set
        End Property

#End Region

    End Class

End Namespace
