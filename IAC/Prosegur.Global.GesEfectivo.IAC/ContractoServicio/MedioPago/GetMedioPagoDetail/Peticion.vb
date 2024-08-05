Imports System.Xml.Serialization
Imports System.Xml

Namespace MedioPago.GetMedioPagoDetail

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 19/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetMedioPagoDetail")> _
    <XmlRoot(Namespace:="urn:GetMedioPagoDetail")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        'Medio Pago
        Private _CodigoMedioPago As List(Of String)
        Private _CodigoTipoMedioPago As List(Of String)
        Private _CodigoIsoDivisa As List(Of String)

#End Region

#Region "[Propriedades]"

        Public Property CodigoMedioPago() As List(Of String)
            Get
                Return _CodigoMedioPago
            End Get
            Set(value As List(Of String))
                _CodigoMedioPago = value
            End Set
        End Property
        Public Property CodigoIsoDivisa() As List(Of String)
            Get
                Return _CodigoIsoDivisa
            End Get
            Set(value As List(Of String))
                _CodigoIsoDivisa = value
            End Set
        End Property

        Public Property CodigoTipoMedioPago() As List(Of String)
            Get
                Return _CodigoTipoMedioPago
            End Get
            Set(value As List(Of String))
                _CodigoTipoMedioPago = value
            End Set
        End Property
#End Region

    End Class

End Namespace