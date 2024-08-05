Imports System.Xml.Serialization
Imports System.Xml

Namespace ATM.SetATM

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 07/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetATM")> _
    <XmlRoot(Namespace:="urn:SetATM")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _oidRed As String
        Private _oidModeloCajero As String
        Private _oidGrupo As String
        Private _codigoDelegacion As String
        Private _bolRegistroTira As Boolean
        Private _cajeros As List(Of Cajero)
        Private _morfologias As List(Of Morfologia)
        Private _procesos As List(Of Proceso)
        Private _bolBorrar As Boolean
        Private _codUsuario As String

#End Region

#Region "[Propriedades]"

        Public Property OidRed() As String
            Get
                Return _oidRed
            End Get
            Set(value As String)
                _oidRed = value
            End Set
        End Property

        Public Property OidModeloCajero() As String
            Get
                Return _oidModeloCajero
            End Get
            Set(value As String)
                _oidModeloCajero = value
            End Set
        End Property

        Public Property OidGrupo() As String
            Get
                Return _oidGrupo
            End Get
            Set(value As String)
                _oidGrupo = value
            End Set
        End Property

        Public Property CodigoDelegacion() As String
            Get
                Return _codigoDelegacion
            End Get
            Set(value As String)
                _codigoDelegacion = value
            End Set
        End Property

        Public Property BolRegistroTira() As Boolean
            Get
                Return _bolRegistroTira
            End Get
            Set(value As Boolean)
                _bolRegistroTira = value
            End Set
        End Property

        Public Property Morfologias() As List(Of Morfologia)
            Get
                Return _morfologias
            End Get
            Set(value As List(Of Morfologia))
                _morfologias = value
            End Set
        End Property

        Public Property Cajeros() As List(Of Cajero)
            Get
                Return _cajeros
            End Get
            Set(value As List(Of Cajero))
                _cajeros = value
            End Set
        End Property

        Public Property Procesos() As List(Of Proceso)
            Get
                Return _procesos
            End Get
            Set(value As List(Of Proceso))
                _procesos = value
            End Set
        End Property

        Public Property BolBorrar() As Boolean
            Get
                Return _bolBorrar
            End Get
            Set(value As Boolean)
                _bolBorrar = value
            End Set
        End Property

        Public Property CodUsuario() As String
            Get
                Return _codUsuario
            End Get
            Set(value As String)
                _codUsuario = value
            End Set
        End Property

#End Region

    End Class

End Namespace