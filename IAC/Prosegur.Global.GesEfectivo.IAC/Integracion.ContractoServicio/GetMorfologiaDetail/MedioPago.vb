
Namespace GetMorfologiaDetail

    ''' <summary>
    ''' Classe MedioPago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/12/2010 Criado
    ''' [bruno.costa] 14/02/2011 alterado
    ''' </history>
    <Serializable()> _
    Public Class MedioPago

#Region "[Variáveis]"

        Private _oidMedioPago As String
        Private _codMedioPago As String
        Private _desMedioPago As String
        Private _terminos As List(Of TerminoMedioPago)

#End Region

#Region "[Propriedades]"

        Public Property OidMedioPago() As String
            Get
                Return _oidMedioPago
            End Get
            Set(value As String)
                _oidMedioPago = value
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

        Public Property DesMedioPago() As String
            Get
                Return _desMedioPago
            End Get
            Set(value As String)
                _desMedioPago = value
            End Set
        End Property

        Public Property Terminos As List(Of TerminoMedioPago)
            Get
                Return _terminos
            End Get
            Set(value As List(Of TerminoMedioPago))
                _terminos = value
            End Set
        End Property

#End Region

    End Class

End Namespace