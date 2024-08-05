Imports System.Xml.Serialization
Imports System.Xml

Namespace GetATMs

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 07/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetATMs")> _
    <XmlRoot(Namespace:="urn:GetATMs")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _codigoDelegacion As String
        Private _codigoCajero As String
        Private _codigoRed As String
        Private _codigoModeloCajero As String
        Private _codigoGrupo As String
        Private _bolVigente As Boolean
        Private _cliente As Cliente
        Private _subClientes As List(Of Subcliente)
        Private _puntoServicio As List(Of PuntoServicio)

#End Region

#Region "[Propriedades]"

        Public Property CodigoDelegacion() As String
            Get
                Return _codigoDelegacion
            End Get
            Set(value As String)
                _codigoDelegacion = value
            End Set
        End Property

        Public Property CodigoCajero() As String
            Get
                Return _codigoCajero
            End Get
            Set(value As String)
                _codigoCajero = value
            End Set
        End Property

        Public Property CodigoRed() As String
            Get
                Return _codigoRed
            End Get
            Set(value As String)
                _codigoRed = value
            End Set
        End Property

        Public Property CodigoModeloCajero() As String
            Get
                Return _codigoModeloCajero
            End Get
            Set(value As String)
                _codigoModeloCajero = value
            End Set
        End Property

        Public Property CodigoGrupo() As String
            Get
                Return _codigoGrupo
            End Get
            Set(value As String)
                _codigoGrupo = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property Cliente() As Cliente
            Get
                Return _cliente
            End Get
            Set(value As Cliente)
                _cliente = value
            End Set
        End Property

        Public Property SubClientes() As List(Of Subcliente)
            Get
                Return _subClientes
            End Get
            Set(value As List(Of Subcliente))
                _subClientes = value
            End Set
        End Property

        Public Property PuntoServicio() As List(Of PuntoServicio)
            Get
                Return _puntoServicio
            End Get
            Set(value As List(Of PuntoServicio))
                _puntoServicio = value
            End Set
        End Property

#End Region

    End Class

End Namespace