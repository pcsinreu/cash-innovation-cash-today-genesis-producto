Imports System.Xml.Serialization
Imports System.Xml

Namespace MedioPago.GetMedioPagoDetail
    ''' <summary>
    ''' Classe MedioPago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>

    <Serializable()> _
    Public Class MedioPago

#Region "[Variáveis]"

        'Medio Pago
        Private _Codigo As String
        Private _Descripcion As String
        Private _Observaciones As String
        Private _Vigente As Boolean
        Private _EsMercancia As Boolean
        Private _codigoAccesoMedioPago As String

        'Tipo Medio Pago
        Private _CodigoTipoMedioPago As String
        Private _DescripcionTipoMedioPago As String

        'Divisa
        Private _CodigoDivisa As String
        Private _DescripcionDivisa As String

        Private _TerminosMedioPago As TerminoMedioPagoColeccion

#End Region

#Region "[Propriedades]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property CodigoAccesoMedioPago() As String
            Get
                Return _codigoAccesoMedioPago
            End Get
            Set(value As String)
                _codigoAccesoMedioPago = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property Observaciones() As String
            Get
                Return _Observaciones
            End Get
            Set(value As String)
                _Observaciones = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
            End Set
        End Property

        Public Property EsMercancia() As Boolean
            Get
                Return _EsMercancia
            End Get
            Set(value As Boolean)
                _EsMercancia = value
            End Set
        End Property

        Public Property CodigoTipoMedioPago() As String
            Get
                Return _CodigoTipoMedioPago
            End Get
            Set(value As String)
                _CodigoTipoMedioPago = value
            End Set
        End Property

        Public Property DescripcionTipoMedioPago() As String
            Get
                Return _DescripcionTipoMedioPago
            End Get
            Set(value As String)
                _DescripcionTipoMedioPago = value
            End Set
        End Property

        Public Property CodigoDivisa() As String
            Get
                Return _CodigoDivisa
            End Get
            Set(value As String)
                _CodigoDivisa = value
            End Set
        End Property
        Public Property DescripcionDivisa() As String
            Get
                Return _DescripcionDivisa
            End Get
            Set(value As String)
                _DescripcionDivisa = value
            End Set
        End Property

        Public Property TerminosMedioPago() As TerminoMedioPagoColeccion
            Get
                Return _TerminosMedioPago
            End Get
            Set(value As TerminoMedioPagoColeccion)
                _TerminosMedioPago = value
            End Set
        End Property
#End Region

    End Class

End Namespace