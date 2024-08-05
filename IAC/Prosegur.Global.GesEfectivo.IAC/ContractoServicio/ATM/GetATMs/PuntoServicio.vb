
Namespace GetATMs

    ''' <summary>
    ''' Classe Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 07/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class PuntoServicio

#Region "[Variáveis]"

        Private _codigoPuntoServicio As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoPuntoServicio() As String
            Get
                Return _codigoPuntoServicio
            End Get
            Set(value As String)
                _codigoPuntoServicio = value
            End Set
        End Property

#End Region

    End Class

End Namespace