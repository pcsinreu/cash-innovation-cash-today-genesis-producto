
Namespace Morfologia.SetMorfologia

    ''' <summary>
    ''' Classe Componente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 20/12/2010 Criado
    ''' </history>
    <Serializable()> _
    Public Class Objecto

#Region "[Variáveis]"

        Private _codIsoDivisa As String
        Private _codDenominacion As String
        Private _codMedioPago As String
        Private _codTipoMedioPago As String
        Private _necOrdenDivisa As Integer
        Private _necOrdenTipoMedPago As Integer


#End Region

#Region "[Propriedades]"

        Public Property CodIsoDivisa() As String
            Get
                Return _codIsoDivisa
            End Get
            Set(value As String)
                _codIsoDivisa = value
            End Set
        End Property

        Public Property CodDenominacion() As String
            Get
                Return _codDenominacion
            End Get
            Set(value As String)
                _codDenominacion = value
            End Set
        End Property

        Public Property CodMedioPago() As String
            Get
                Return _codMedioPago
            End Get
            Set(value As String)
                _codMedioPago = value
            End Set
        End Property

        Public Property CodTipoMedioPago() As String
            Get
                Return _codTipoMedioPago
            End Get
            Set(value As String)
                _codTipoMedioPago = value
            End Set
        End Property

        Public Property NecOrdenDivisa As Integer
            Get
                Return _necOrdenDivisa
            End Get
            Set(value As Integer)
                _necOrdenDivisa = value
            End Set
        End Property

        Public Property NecOrdenTipoMedPago As Integer
            Get
                Return _necOrdenTipoMedPago
            End Get
            Set(value As Integer)
                _necOrdenTipoMedPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace