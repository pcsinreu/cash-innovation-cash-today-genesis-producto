Imports System.Xml.Serialization

Namespace Login.EjecutarLogin

    ''' <summary>
    ''' Delegacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011  Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:EjecutarLogin")> _
    <XmlRoot(Namespace:="urn:EjecutarLogin")> _
    <XmlInclude(GetType(DelegacionPlanta))> _
    Public Class Delegacion

#Region " Variáveis "

        Private _Identificador As String
        Private _Codigo As String
        Private _Nombre As String
        Private _Zona As String
        Private _GMT As Short
        Private _VeranoFechaHoraIni As DateTime
        Private _VeranoFechaHoraFin As DateTime
        Private _VeranoAjuste As Short
        Private _CantidadMinutosIni As Short
        Private _CantidadMetrosBase As Short
        Private _CantidadMinutosSalida As Short
        Private _DelegacionesLegado As New List(Of DelegacionLegado)
        Private _Sectores As New List(Of Sector)

#End Region

#Region "Propriedades"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                _Identificador = value
            End Set
        End Property

        Public Property Zona() As String
            Get
                Return _Zona
            End Get
            Set(value As String)
                _Zona = value
            End Set
        End Property

        Public Property GMT() As Short
            Get
                Return _GMT
            End Get
            Set(value As Short)
                _GMT = value
            End Set
        End Property

        Public Property VeranoFechaHoraIni() As DateTime
            Get
                Return _VeranoFechaHoraIni
            End Get
            Set(value As DateTime)
                _VeranoFechaHoraIni = value
            End Set
        End Property

        Public Property VeranoFechaHoraFin() As DateTime
            Get
                Return _VeranoFechaHoraFin
            End Get
            Set(value As DateTime)
                _VeranoFechaHoraFin = value
            End Set
        End Property

        Public Property VeranoAjuste() As Short
            Get
                Return _VeranoAjuste
            End Get
            Set(value As Short)
                _VeranoAjuste = value
            End Set
        End Property

        Public Property CantidadMinutosIni() As Short
            Get
                Return _CantidadMinutosIni
            End Get
            Set(value As Short)
                _CantidadMinutosIni = value
            End Set
        End Property

        Public Property CantidadMetrosBase() As Short
            Get
                Return _CantidadMetrosBase
            End Get
            Set(value As Short)
                _CantidadMetrosBase = value
            End Set
        End Property

        Public Property CantidadMinutosSalida() As Short
            Get
                Return _CantidadMinutosSalida
            End Get
            Set(value As Short)
                _CantidadMinutosSalida = value
            End Set
        End Property

        Public Property DelegacionesLegado() As List(Of DelegacionLegado)
            Get
                Return _DelegacionesLegado
            End Get
            Set(value As List(Of DelegacionLegado))
                _DelegacionesLegado = value
            End Set
        End Property

        Public Property Sectores() As List(Of Sector)
            Get
                Return _Sectores
            End Get
            Set(value As List(Of Sector))
                _Sectores = value
            End Set
        End Property


#End Region

    End Class

End Namespace