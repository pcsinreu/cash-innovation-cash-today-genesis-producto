Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper
Imports System.Xml.Serialization
Imports System.Xml

Namespace Clases

    <Serializable()>
    <XmlType(Namespace:="urn:DatoBancario")>
    <XmlRoot(Namespace:="urn:DatoBancario")>
    Public Class DatoBancario
        Inherits BindableBase

#Region "Variaveis"
        Private _identificador As String
        Private _bolDefecto As Nullable(Of Boolean)
        Private _banco As Cliente
        Private _cliente As Cliente
        Private _subCliente As SubCliente
        Private _puntoServicio As PuntoServicio
        Private _divisa As Divisa
        Private _codigoTipoCuentaBancaria As String
        Private _codigoCuentaBancaria As String
        Private _codigoDocumento As String
        Private _descripcionTitularidad As String
        Private _descripcionObs As String
        Private _codigoAgencia As String
        Private _descripcionAdicionalCampo1 As String
        Private _descripcionAdicionalCampo2 As String
        Private _descripcionAdicionalCampo3 As String
        Private _descripcionAdicionalCampo4 As String
        Private _descripcionAdicionalCampo5 As String
        Private _descripcionAdicionalCampo6 As String
        Private _descripcionAdicionalCampo7 As String
        Private _descripcionAdicionalCampo8 As String
        Private _comentario As String
        Private _pendiente As Boolean
        Private _nuevo As Boolean

        Private _modificados As SerializableDictionary(Of String, Boolean)


#End Region

#Region "Propriedades"
        Public Property Identificador As String
            Get
                Return _identificador
            End Get
            Set(value As String)
                SetProperty(_identificador, value, "Identificador")
            End Set
        End Property

        Public Property bolDefecto As Nullable(Of Boolean)
            Get
                Return _bolDefecto
            End Get
            Set(value As Nullable(Of Boolean))
                SetProperty(_bolDefecto, value, "BolDefecto")
            End Set
        End Property

        Public Property Banco As Cliente
            Get
                Return _banco
            End Get
            Set(value As Cliente)
                SetProperty(_banco, value, "Banco")
            End Set
        End Property

        Public Property Cliente As Cliente
            Get
                Return _cliente
            End Get
            Set(value As Cliente)
                SetProperty(_cliente, value, "Cliente")
            End Set
        End Property

        Public Property SubCliente As SubCliente
            Get
                Return _subCliente
            End Get
            Set(value As SubCliente)
                SetProperty(_subCliente, value, "SubCliente")
            End Set
        End Property

        Public Property PuntoServicio As PuntoServicio
            Get
                Return _puntoServicio
            End Get
            Set(value As PuntoServicio)
                SetProperty(_puntoServicio, value, "PuntoServicio")
            End Set
        End Property

        Public Property Divisa As Divisa
            Get
                Return _divisa
            End Get
            Set(value As Divisa)
                SetProperty(_divisa, value, "Divisa")
            End Set
        End Property

        Public Property CodigoTipoCuentaBancaria As String
            Get
                Return _codigoTipoCuentaBancaria
            End Get
            Set(value As String)
                SetProperty(_codigoTipoCuentaBancaria, value, "CodigoTipoCuentaBancaria")
            End Set
        End Property

        Public Property CodigoCuentaBancaria As String
            Get
                Return _codigoCuentaBancaria
            End Get
            Set(value As String)
                SetProperty(_codigoCuentaBancaria, value, "CodigoCuentaBancaria")
            End Set
        End Property

        Public Property DescripcionTitularidad As String
            Get
                Return _descripcionTitularidad
            End Get
            Set(value As String)
                SetProperty(_descripcionTitularidad, value, "DescripcionTitularidad")
            End Set
        End Property

        Public Property CodigoDocumento As String
            Get
                Return _codigoDocumento
            End Get
            Set(value As String)
                SetProperty(_codigoDocumento, value, "CodigoDocumento")
            End Set
        End Property

        Public Property DescripcionObs As String
            Get
                Return _descripcionObs
            End Get
            Set(value As String)
                SetProperty(_descripcionObs, value, "DescripcionObs")
            End Set
        End Property




        Public Property CodigoAgencia As String
            Get
                Return _codigoAgencia
            End Get
            Set(value As String)
                SetProperty(_codigoAgencia, value, "CodigoAgencia")
            End Set
        End Property

        Public Property DescripcionAdicionalCampo1 As String
            Get
                Return _descripcionAdicionalCampo1
            End Get
            Set(value As String)
                SetProperty(_descripcionAdicionalCampo1, value, "DescripcionAdicionalCampo1")
            End Set
        End Property

        Public Property DescripcionAdicionalCampo2 As String
            Get
                Return _descripcionAdicionalCampo2
            End Get
            Set(value As String)
                SetProperty(_descripcionAdicionalCampo2, value, "DescripcionAdicionalCampo2")
            End Set
        End Property

        Public Property DescripcionAdicionalCampo3 As String
            Get
                Return _descripcionAdicionalCampo3
            End Get
            Set(value As String)
                SetProperty(_descripcionAdicionalCampo3, value, "DescripcionAdicionalCampo3")
            End Set
        End Property

        Public Property DescripcionAdicionalCampo4 As String
            Get
                Return _descripcionAdicionalCampo4
            End Get
            Set(value As String)
                SetProperty(_descripcionAdicionalCampo4, value, "DescripcionAdicionalCampo4")
            End Set
        End Property

        Public Property DescripcionAdicionalCampo5 As String
            Get
                Return _descripcionAdicionalCampo5
            End Get
            Set(value As String)
                SetProperty(_descripcionAdicionalCampo5, value, "DescripcionAdicionalCampo5")
            End Set
        End Property

        Public Property DescripcionAdicionalCampo6 As String
            Get
                Return _descripcionAdicionalCampo6
            End Get
            Set(value As String)
                SetProperty(_descripcionAdicionalCampo6, value, "DescripcionAdicionalCampo6")
            End Set
        End Property

        Public Property DescripcionAdicionalCampo7 As String
            Get
                Return _descripcionAdicionalCampo7
            End Get
            Set(value As String)
                SetProperty(_descripcionAdicionalCampo7, value, "DescripcionAdicionalCampo7")
            End Set
        End Property

        Public Property DescripcionAdicionalCampo8 As String
            Get
                Return _descripcionAdicionalCampo8
            End Get
            Set(value As String)
                SetProperty(_descripcionAdicionalCampo8, value, "DescripcionAdicionalCampo8")
            End Set
        End Property
        Public Property Comentario As String
            Get
                Return _comentario
            End Get
            Set(value As String)
                SetProperty(_comentario, value, "Comentario")
            End Set
        End Property

        Public Property Pendiente As Boolean
            Get
                Return _pendiente
            End Get
            Set(value As Boolean)
                SetProperty(_pendiente, value, "Pendiente")
            End Set
        End Property
        Public Property Nuevo As Boolean
            Get
                Return _nuevo
            End Get
            Set(value As Boolean)
                SetProperty(_nuevo, value, "Nuevo")
            End Set
        End Property

#End Region
    End Class

End Namespace

