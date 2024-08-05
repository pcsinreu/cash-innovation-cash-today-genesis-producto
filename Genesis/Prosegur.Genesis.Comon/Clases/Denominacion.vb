Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Denominacion.vb
    '  Descripción: Clase definición Denominacion
    ' ***********************************************************************
    <Serializable()>
    Public Class Denominacion
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _EsBillete As Boolean
        Private _Valor As Decimal
        Private _Peso As Decimal
        Private _EstaActivo As Boolean
        Private _CodigoUsuario As String
        Private _FechaHoraActualizacion As DateTime
        Private _ValorDenominacion As ObservableCollection(Of ValorDenominacion)
        Private _Divisa As Clases.Divisa
        Private _CodigosAjeno As ObservableCollection(Of Clases.CodigoAjeno)


#End Region

#Region "Propriedades"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property Codigo As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property EsBillete As Boolean
            Get
                Return _EsBillete
            End Get
            Set(value As Boolean)
                SetProperty(_EsBillete, value, "EsBillete")
            End Set
        End Property

        Public Property Valor As Decimal
            Get
                Return _Valor
            End Get
            Set(value As Decimal)
                SetProperty(_Valor, value, "Valor")
            End Set
        End Property

        Public Property Peso As Decimal
            Get
                Return _Peso
            End Get
            Set(value As Decimal)
                SetProperty(_Peso, value, "Peso")
            End Set
        End Property

        Public Property EstaActivo As Boolean
            Get
                Return _EstaActivo
            End Get
            Set(value As Boolean)
                SetProperty(_EstaActivo, value, "EstaActivo")
            End Set
        End Property

        Public Property CodigoUsuario As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                SetProperty(_CodigoUsuario, value, "CodigoUsuario")
            End Set
        End Property

        Public Property FechaHoraActualizacion As DateTime
            Get
                Return _FechaHoraActualizacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraActualizacion, value, "FechaHoraActualizacion")
            End Set
        End Property

        Public Property ValorDenominacion As ObservableCollection(Of ValorDenominacion)
            Get
                Return _ValorDenominacion
            End Get
            Set(value As ObservableCollection(Of ValorDenominacion))
                SetProperty(_ValorDenominacion, value, "ValorDenominacion")
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnore()>
        Public Property Divisa As Clases.Divisa
            Get
                Return _Divisa
            End Get
            Set(value As Clases.Divisa)
                SetProperty(_Divisa, value, "Divisa")
            End Set
        End Property

        Public Property CodigosAjeno As ObservableCollection(Of Clases.CodigoAjeno)
            Get
                Return _CodigosAjeno
            End Get
            Set(value As ObservableCollection(Of Clases.CodigoAjeno))
                SetProperty(_CodigosAjeno, value, "CodigosAjeno")
            End Set
        End Property

#End Region

    End Class

End Namespace
