Namespace IngresoRemesas

    ''' <summary>
    ''' RemesasError
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 27/07/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class RemesaError

#Region " Variáveis "

        Private _IdRemesaError As String
        Private _DescRemesaError As String

#End Region

#Region " Propriedades "

        Public Property IdRemesaError() As String
            Get
                Return _IdRemesaError
            End Get
            Set(value As String)
                _IdRemesaError = value
            End Set
        End Property

        Public Property DescRemesaError() As String
            Get
                Return _DescRemesaError
            End Get
            Set(value As String)
                _DescRemesaError = value
            End Set
        End Property

#End Region

    End Class

End Namespace