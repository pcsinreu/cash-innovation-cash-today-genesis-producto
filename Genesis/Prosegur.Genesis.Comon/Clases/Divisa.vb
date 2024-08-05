Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Divisa.vb
    '  Descripción: Clase definición Divisa
    ' ***********************************************************************

    <Serializable()>
    Public Class Divisa
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _CodigoISO As String
        Private _Descripcion As String
        Private _EstaActivo As Boolean
        Private _Icono As Byte()
        Private _CodigoSimbolo As String
        Private _CodigoUsuario As String
        Private _FechaHoraTransporte As DateTime
        Private _Denominaciones As ObservableCollection(Of Denominacion)
        Private _MediosPago As ObservableCollection(Of MedioPago)
        Private _ValoresTotalesEfectivo As ObservableCollection(Of ValorEfectivo)
        Private _ValoresTotalesTipoMedioPago As ObservableCollection(Of ValorTipoMedioPago)
        Private _ValoresTotalesDivisa As ObservableCollection(Of ValorDivisa)
        Private _Color As Drawing.Color
        Private _CodigoAcceso As Char
        Private _ValoresTotales As ObservableCollection(Of ImporteTotal)
        Private _CodigosAjeno As ObservableCollection(Of Clases.CodigoAjeno)

#End Region

#Region "Propriedades"

        Public Property Accion As String
        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property CodigoISO As String
            Get
                Return _CodigoISO
            End Get
            Set(value As String)
                SetProperty(_CodigoISO, value, "CodigoISO")
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

        Public Property EstaActivo As Boolean
            Get
                Return _EstaActivo
            End Get
            Set(value As Boolean)
                SetProperty(_EstaActivo, value, "EstaActivo")
            End Set
        End Property

        Public Property Icono As Byte()
            Get
                Return _Icono
            End Get
            Set(value As Byte())
                SetProperty(_Icono, value, "Icono")
            End Set
        End Property

        Public Property CodigoSimbolo As String
            Get
                Return _CodigoSimbolo
            End Get
            Set(value As String)
                SetProperty(_CodigoSimbolo, value, "CodigoSimbolo")
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

        Public Property FechaHoraTransporte As DateTime
            Get
                Return _FechaHoraTransporte
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraTransporte, value, "FechaHoraTransporte")
            End Set
        End Property

        Public Property Denominaciones As ObservableCollection(Of Denominacion)
            Get
                Return _Denominaciones
            End Get
            Set(value As ObservableCollection(Of Denominacion))
                SetProperty(_Denominaciones, value, "Denominaciones")

            End Set
        End Property

        Public Property MediosPago As ObservableCollection(Of MedioPago)
            Get
                Return _MediosPago
            End Get
            Set(value As ObservableCollection(Of MedioPago))
                SetProperty(_MediosPago, value, "MediosPago")
            End Set
        End Property

        Public Property ValoresTotalesEfectivo As ObservableCollection(Of ValorEfectivo)
            Get
                Return _ValoresTotalesEfectivo
            End Get
            Set(value As ObservableCollection(Of ValorEfectivo))
                SetProperty(_ValoresTotalesEfectivo, value, "ValoresTotalesEfectivo")
            End Set
        End Property

        Public Property ValoresTotalesTipoMedioPago As ObservableCollection(Of ValorTipoMedioPago)
            Get
                Return _ValoresTotalesTipoMedioPago
            End Get
            Set(value As ObservableCollection(Of ValorTipoMedioPago))
                SetProperty(_ValoresTotalesTipoMedioPago, value, "ValoresTotalesTipoMedioPago")
            End Set
        End Property

        Public Property ValoresTotalesDivisa As ObservableCollection(Of ValorDivisa)
            Get
                Return _ValoresTotalesDivisa
            End Get
            Set(value As ObservableCollection(Of ValorDivisa))
                SetProperty(_ValoresTotalesDivisa, value, "ValoresTotalesDivisa")
            End Set
        End Property

        Public Property Color As Drawing.Color
            Get
                Return _Color
            End Get
            Set(value As Drawing.Color)
                SetProperty(_Color, value, "Color")
            End Set
        End Property

        Public Property CodigoAcceso As Char
            Get
                Return _CodigoAcceso
            End Get
            Set(value As Char)
                SetProperty(_CodigoAcceso, value, "CodigoAcceso")
            End Set
        End Property

        Public Property ValoresTotales As ObservableCollection(Of ImporteTotal)
            Get
                'if _ValoresTotales = Nothing Then
                'CalcularTotales 
                'end if
                'Return _ValoresTotales
                Return _ValoresTotales
            End Get
            Set(value As ObservableCollection(Of ImporteTotal))
                SetProperty(_ValoresTotales, value, "ValoresTotales")
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

        Public ReadOnly Property CodigoDescripcion As String
            Get
                Return $"{CodigoISO} - {Descripcion}"
            End Get
        End Property

#End Region

        Public ReadOnly Property TotalImporte As Decimal
            Get
                If Me.Denominaciones IsNot Nothing Then
                    Return Me.Denominaciones.Sum(Function(denominacion)
                                                     Return denominacion.ValorDenominacion.Sum(Function(valor)
                                                                                                   Return valor.Importe
                                                                                               End Function)
                                                 End Function)
                End If
                Return 0.0
            End Get
        End Property

    End Class

End Namespace
