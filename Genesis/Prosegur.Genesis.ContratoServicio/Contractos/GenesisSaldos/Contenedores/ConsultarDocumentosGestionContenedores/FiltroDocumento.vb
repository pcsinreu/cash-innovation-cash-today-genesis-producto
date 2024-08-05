Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarDocumentosGestionContenedores

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <Serializable()>
    Public Class FiltroDocumento
        Inherits BindableBase

#Region "[PROPRIEDADES]"

        Private _FechaDocumentoDesde As DateTime
        Public Property FechaDocumentoDesde() As DateTime
            Get
                Return _FechaDocumentoDesde
            End Get
            Set(value As DateTime)
                MyBase.SetProperty(_FechaDocumentoDesde, value, "FechaDocumentoDesde")
            End Set
        End Property

        Private _FechaDocumentoHasta As DateTime
        Public Property FechaDocumentoHasta() As DateTime
            Get
                Return _FechaDocumentoHasta
            End Get
            Set(value As DateTime)
                MyBase.SetProperty(_FechaDocumentoHasta, value, "FechaDocumentoHasta")
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

        Private _OrigenBusqueda As String
        Public Property OrigenBusqueda() As String
            Get
                Return _OrigenBusqueda
            End Get
            Set(value As String)
                MyBase.SetProperty(_OrigenBusqueda, value, "OrigenBusqueda")
            End Set
        End Property

        Private _Cuenta As Genesis.Comon.Clases.Cuenta
        Public Property Cuenta() As Genesis.Comon.Clases.Cuenta
            Get
                Return _Cuenta
            End Get
            Set(value As Genesis.Comon.Clases.Cuenta)
                MyBase.SetProperty(_Cuenta, value, "Cuenta")
            End Set
        End Property

        Private _Contenedor As Comon.Contenedor
        Public Property Contenedor() As Comon.Contenedor
            Get
                Return _Contenedor
            End Get
            Set(value As Comon.Contenedor)
                MyBase.SetProperty(_Contenedor, value, "Contenedor")
            End Set
        End Property

        Private _Precinto As String
        Public Property Precinto() As String
            Get
                Return _Precinto
            End Get
            Set(value As String)
                MyBase.SetProperty(_Precinto, value, "Precinto")
            End Set
        End Property

#End Region

    End Class

End Namespace
