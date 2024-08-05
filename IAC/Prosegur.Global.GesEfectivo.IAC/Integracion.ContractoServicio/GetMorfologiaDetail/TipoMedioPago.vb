
Namespace GetMorfologiaDetail

    ''' <summary>
    ''' Classe TipoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 21/12/2010 Criado
    ''' </history>
    <Serializable()> _
    Public Class TipoMedioPago

#Region "[Variáveis]"

        Private _codTipoMedioPago As String
        Private _desTipoMedioPago As String
        Private _mediosPago As List(Of MedioPago)

#End Region

#Region "[Propriedades]"

        Public Property MediosPago() As List(Of MedioPago)
            Get
                Return _mediosPago
            End Get
            Set(value As List(Of MedioPago))
                _mediosPago = value
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

        Public Property DesTipoMedioPago() As String
            Get
                Return _desTipoMedioPago
            End Get
            Set(value As String)
                _desTipoMedioPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace