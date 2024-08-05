Imports System.Xml.Serialization
Imports System.Xml

Namespace Planta.SetPlanta
    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pagoncalves] 19/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetPlanta")> _
    <XmlRoot(Namespace:="urn:SetPlanta")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _OidPlanta As String
        Private _OidDelegacion As String
        Private _CodPlanta As String
        Private _DesPlanta As String
        Private _BolActivo As Boolean
        Private _GmtCreacion As Date
        Private _DesUsuarioCreacion As String
        Private _GmtModificacion As Date
        Private _DesUsuarioMoficacion As String
        Private _CodigosAjenos As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Private _ImportesMaximos As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidPlanta() As String
            Get
                Return _OidPlanta
            End Get
            Set(value As String)
                _OidPlanta = value
            End Set
        End Property

        Public Property OidDelegacion() As String
            Get
                Return _OidDelegacion
            End Get
            Set(value As String)
                _OidDelegacion = value
            End Set
        End Property

        Public Property CodPlanta() As String
            Get
                Return _CodPlanta
            End Get
            Set(value As String)
                _CodPlanta = value
            End Set
        End Property

        Public Property DesPlanta() As String
            Get
                Return _DesPlanta
            End Get
            Set(value As String)
                _DesPlanta = value
            End Set
        End Property

        Public Property BolActivo() As Boolean
            Get
                Return _BolActivo
            End Get
            Set(value As Boolean)
                _BolActivo = value
            End Set
        End Property

        Public Property GmtCreacion() As Date
            Get
                Return _GmtCreacion
            End Get
            Set(value As Date)
                _GmtCreacion = value
            End Set
        End Property

        Public Property DesUsuarioCreacion() As String
            Get
                Return _DesUsuarioCreacion
            End Get
            Set(value As String)
                _DesUsuarioCreacion = value
            End Set
        End Property

        Public Property GmtModificacion() As Date
            Get
                Return _GmtModificacion
            End Get
            Set(value As Date)
                _GmtModificacion = value
            End Set
        End Property

        Public Property DesUsuarioModificacion() As String
            Get
                Return _DesUsuarioMoficacion
            End Get
            Set(value As String)
                _DesUsuarioMoficacion = value
            End Set
        End Property

        Public Property CodigosAjenos() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            Get
                Return _CodigosAjenos
            End Get
            Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
                _CodigosAjenos = value
            End Set
        End Property

        Public Property ImporteMaximo() As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
            Get
                Return _ImportesMaximos
            End Get
            Set(value As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase)
                _ImportesMaximos = value
            End Set
        End Property

#End Region
    End Class
End Namespace

