Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper
Imports System.Xml.Serialization

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  ConfiguracionReporte.vb
    '  Descripción: Clase definición ConfiguracionReporte
    ' ***********************************************************************
    <Serializable()>
    <XmlType(Namespace:="urn:Prosegur.Genesis.Comon.Clases.ConfiguracionReporte")> _
    <XmlRoot(Namespace:="urn:Prosegur.Genesis.Comon.Clases.ConfiguracionReporte")> _
    Public Class ConfiguracionReporte
        Inherits BindableBase
        Implements IEntidadeHelper

        Public Sub New()

        End Sub
#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _Direccion As String
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _TiposClientes As ObservableCollection(Of TipoCliente)
        Private _TiposReporte As ObservableCollection(Of Comon.Enumeradores.TipoReporte)
        Private _Clientes As ObservableCollection(Of Cliente)
        Private _ResultadoReporte As Clases.ResultadoReporte
        Private _TipoReporte As Comon.Enumeradores.TipoReporte
        Private _MascaraNombre As String
        Private _CodigoRedenrizador As Enumeradores.TipoRenderizador
        Private _DescripcionExtension As String
        Private _DescripcionSeparador As String
        Private _ParametrosReporte As ObservableCollection(Of ParametroReporte)

#End Region

#Region "Propriedades"

        Public Property ParametrosReporte As ObservableCollection(Of ParametroReporte)
            Get
                Return _ParametrosReporte
            End Get
            Set(value As ObservableCollection(Of ParametroReporte))
                SetProperty(_ParametrosReporte, value, "ParametrosReporte")
            End Set
        End Property

        Public Property DescripcionSeparador As String
            Get
                Return _DescripcionSeparador
            End Get
            Set(value As String)
                SetProperty(_DescripcionSeparador, value, "DescripcionSeparador")
            End Set
        End Property

        Public Property DescripcionExtension As String
            Get
                Return _DescripcionExtension
            End Get
            Set(value As String)
                SetProperty(_DescripcionExtension, value, "DescripcionExtension")

            End Set
        End Property

        Public Property CodigoRedenrizador As Enumeradores.TipoRenderizador
            Get
                Return _CodigoRedenrizador
            End Get
            Set(value As Enumeradores.TipoRenderizador)
                SetProperty(_CodigoRedenrizador, value, "CodigoRedenrizador")

            End Set
        End Property

        Public Property TipoReporte As Comon.Enumeradores.TipoReporte
            Get
                Return _TipoReporte
            End Get
            Set(value As Comon.Enumeradores.TipoReporte)
                SetProperty(_TipoReporte, value, "TipoReporte")

            End Set
        End Property

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")

            End Set
        End Property

        Public Property ResultadoReporte As Clases.ResultadoReporte
            Get
                Return _ResultadoReporte
            End Get
            Set(value As Clases.ResultadoReporte)
                SetProperty(_ResultadoReporte, value, "ResultadoReporte")

            End Set
        End Property

        Public Property Codigo As String Implements IEntidadeHelper.Codigo
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String Implements IEntidadeHelper.Descripcion
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property Direccion As String
            Get
                Return _Direccion
            End Get
            Set(value As String)
                SetProperty(_Direccion, value, "Direccion")
            End Set
        End Property

        Public Property FechaHoraCreacion As DateTime
            Get
                Return _FechaHoraCreacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraCreacion, value, "FechaHoraCreacion")
            End Set
        End Property

        Public Property UsuarioCreacion As String
            Get
                Return _UsuarioCreacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioCreacion, value, "UsuarioCreacion")
            End Set
        End Property

        Public Property TiposClientes() As ObservableCollection(Of TipoCliente)
            Get
                Return _TiposClientes
            End Get
            Set(value As ObservableCollection(Of TipoCliente))
                SetProperty(_TiposClientes, value, "TiposClientes")

            End Set
        End Property

        Public Property Clientes() As ObservableCollection(Of Cliente)
            Get
                Return _Clientes
            End Get
            Set(value As ObservableCollection(Of Cliente))
                SetProperty(_Clientes, value, "Clientes")

            End Set
        End Property

        Public Property TiposReporte() As ObservableCollection(Of Comon.Enumeradores.TipoReporte)
            Get
                Return _TiposReporte
            End Get
            Set(value As ObservableCollection(Of Comon.Enumeradores.TipoReporte))
                SetProperty(_TiposReporte, value, "TiposReporte")

            End Set
        End Property

        Public Property MascaraNombre() As String
            Get
                Return _MascaraNombre
            End Get
            Set(value As String)
                SetProperty(_MascaraNombre, value, "MascaraNombre")

            End Set
        End Property


#End Region

    End Class

End Namespace
