Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace GenesisSaldos.Contenedores.Comon

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <Serializable()>
    Public Class Documento
        Inherits BindableBase

#Region "Propriedades"

        Public Property CodFormulario As String
        Public Property CodUsuario As String
        Public Property FechaPlanCertificacion As DateTime
        Public Property FechaGestion As DateTime
        Public Property EsGrupo As Boolean
        Public Property Rowver As Integer

#Region "Bindables"

        Private _CodExterno As String
        Public Property CodExterno() As String
            Get
                Return _CodExterno
            End Get
            Set(value As String)
                MyBase.SetProperty(_CodExterno, value, "CodExterno")
            End Set
        End Property

        Private _Identificador As String
        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                MyBase.SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Private _CodComprobante As String
        Public Property CodComprobante() As String
            Get
                Return _CodComprobante
            End Get
            Set(value As String)
                MyBase.SetProperty(_CodComprobante, value, "CodComprobante")
            End Set
        End Property

        Private _CodEstadoDocumento As String
        Public Property CodEstadoDocumento() As String
            Get
                Return _CodEstadoDocumento
            End Get
            Set(value As String)
                MyBase.SetProperty(_CodEstadoDocumento, value, "CodEstadoDocumento")
            End Set
        End Property

        Private _FechaCreacion As DateTime
        Public Property FechaCreacion() As DateTime
            Get
                Return _FechaCreacion
            End Get
            Set(value As DateTime)
                MyBase.SetProperty(_FechaCreacion, value, "FechaCreacion")
            End Set
        End Property

        Private _FechaModificacion As DateTime
        Public Property FechaModificacion() As DateTime
            Get
                Return _FechaModificacion
            End Get
            Set(value As DateTime)
                MyBase.SetProperty(_FechaModificacion, value, "FechaModificacion")
            End Set
        End Property

        Private _SectorOrigen As Genesis.Comon.Clases.Sector
        Public Property SectorOrigen() As Genesis.Comon.Clases.Sector
            Get
                Return _SectorOrigen
            End Get
            Set(value As Genesis.Comon.Clases.Sector)
                MyBase.SetProperty(_SectorOrigen, value, "SectorOrigen")
            End Set
        End Property

        Private _SectorDestino As Genesis.Comon.Clases.Sector
        Public Property SectorDestino() As Genesis.Comon.Clases.Sector
            Get
                Return _SectorDestino
            End Get
            Set(value As Genesis.Comon.Clases.Sector)
                MyBase.SetProperty(_SectorDestino, value, "SectorDestino")
            End Set
        End Property

        Private _Cuenta As Clases.Cuenta
        Public Property Cuenta() As Clases.Cuenta
            Get
                Return _Cuenta
            End Get
            Set(value As Clases.Cuenta)
                MyBase.SetProperty(_Cuenta, value, "Cuenta")
            End Set
        End Property

        Private _Contenedores As ObservableCollection(Of Contenedor)
        Public Property Contenedores() As ObservableCollection(Of Contenedor)
            Get
                Return _Contenedores
            End Get
            Set(value As ObservableCollection(Of Contenedor))
                MyBase.SetProperty(_Contenedores, value, "Contenedores")
            End Set
        End Property

        Private _divisas As ObservableCollection(Of Clases.Divisa)
        Public Property Divisas As ObservableCollection(Of Clases.Divisa)
            Get
                Return _divisas
            End Get
            Set(value As ObservableCollection(Of Clases.Divisa))
                MyBase.SetProperty(_divisas, value, "Divisas")
            End Set
        End Property

#End Region

#End Region

    End Class

End Namespace
