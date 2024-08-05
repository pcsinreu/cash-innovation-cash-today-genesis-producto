Imports System.Xml.Serialization
Imports System.Xml

Namespace Comon.SetNumeroDeSerieBillete


    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [mult.guilherme.corsino]  17/07/2013 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:SetNumeroDeSerieBillete")> _
    <XmlRoot(Namespace:="urn:SetNumeroDeSerieBillete")> _
    Public Class Peticion

#Region "Variaveis"

        Private _Archivos As Base.ArchivoColeccion
        Private _CodAplicacionGenesis As Integer
        Private _idRemesa As String
        Private _idBulto As String
        Private _CodDelegacion As String
        Private _CodUsuario As String
        Private _FyhActualizacion As Date


#End Region

#Region "Propriedades"

        Public Property Archivos() As Base.ArchivoColeccion
            Get
                Return _Archivos
            End Get
            Set(value As Base.ArchivoColeccion)
                _Archivos = value
            End Set
        End Property

        Public Property CodUsuario() As String
            Get
                Return _CodUsuario
            End Get
            Set(value As String)
                _CodUsuario = value
            End Set
        End Property

        Public Property CodAplicacionGenesis() As Integer
            Get
                Return _CodAplicacionGenesis
            End Get
            Set(value As Integer)
                _CodAplicacionGenesis = value
            End Set
        End Property

        Public Property idRemesa() As String
            Get
                Return _idRemesa
            End Get
            Set(value As String)
                _idRemesa = value
            End Set
        End Property

        Public Property idBulto() As String
            Get
                Return _idBulto
            End Get
            Set(value As String)
                _idBulto = value
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

        Public Property FyhActualizacion() As Date
            Get
                Return _FyhActualizacion
            End Get
            Set(value As Date)
                _FyhActualizacion = value
            End Set
        End Property


#End Region


    End Class


End Namespace
