Namespace RecuperarCentroProceso

    ''' <summary>
    ''' Classe CentroProceso
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 29/04/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class CentroProceso

#Region "[VARIÁVEIS]"

        Private _CodigoIDPS As String
        Private _DescripcionCentroProceso As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoIDPS() As String
            Get
                Return _CodigoIDPS
            End Get
            Set(value As String)
                _CodigoIDPS = value
            End Set
        End Property

        Public Property DescripcionCentroProceso() As String
            Get
                Return _DescripcionCentroProceso
            End Get
            Set(value As String)
                _DescripcionCentroProceso = value
            End Set
        End Property

#End Region

    End Class

End Namespace