Imports System.Xml
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos.Contenedores.Comon

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class Contenedor
        Inherits BindableBase


#Region "Propriedades"

        Public Property IdentificadorContenedor As String
        Public Property TipoElemento As Enumeradores.TipoElemento
        Public Property TipoServicio As Clases.TipoServicio
        Public Property TipoContenedor As Clases.TipoContenedor
        Public Property TipoFormato As Clases.TipoFormato
        Public Property CodigoPuesto As String
        Public Property Elementos As List(Of Clases.Elemento)
        Public Property FechaHoraCreacion As DateTime
        Public Property FechaHoraCreacionMobile As String
        Public Property CodigoPosicion As String
        Public Property ValoresContenedor As List(Of ImporteContenedor)

#Region "Bindables"

        Private _CodTipoContenedor As String
        Public Property CodTipoContenedor() As String
            Get
                Return _CodTipoContenedor
            End Get
            Set(value As String)
                MyBase.SetProperty(_CodTipoContenedor, value, "CodTipoContenedor")
            End Set
        End Property

        Private _CodEstadoContenedor As String
        Public Property CodEstadoContenedor() As String
            Get
                Return _CodEstadoContenedor
            End Get
            Set(value As String)
                MyBase.SetProperty(_CodEstadoContenedor, value, "CodEstadoContenedor")
            End Set
        End Property

        Private _Precintos As ObservableCollection(Of String)
        Public Property Precintos() As ObservableCollection(Of String)
            Get
                Return _Precintos
            End Get
            Set(value As ObservableCollection(Of String))
                MyBase.SetProperty(_Precintos, value, "Precintos")
            End Set
        End Property

        Private _cuentaContenedor As Clases.CuentasContenedor
        Public Property CuentaContenedor() As Clases.CuentasContenedor
            Get
                Return _cuentaContenedor
            End Get
            Set(value As Clases.CuentasContenedor)
                MyBase.SetProperty(_cuentaContenedor, value, "CuentaContenedor")
            End Set
        End Property

#End Region

#End Region

        Public Class ImporteContenedor
            Public Property IdentificadorDivisa As String
            Public Property CodigoIsoDivisa As String
            Public Property IdentificadorDenominacion As String
            Public Property CodigoDenominacion As String
            Public Property IdentificadorCalidad As String
            Public Property CodigoCalidad As String
            Public Property Disponible As Boolean
            Public Property Bloqueado As Boolean
            Public Property Cantidad As Int64
            Public Property Importe As Decimal
        End Class

    End Class

End Namespace