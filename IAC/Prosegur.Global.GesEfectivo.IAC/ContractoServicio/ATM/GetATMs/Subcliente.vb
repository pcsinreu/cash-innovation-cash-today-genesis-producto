
Namespace GetATMs

    ''' <summary>
    ''' Classe Sub Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 07/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Subcliente

#Region "[Variáveis]"

        Private _codigoSubcliente As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoSubcliente() As String
            Get
                Return _codigoSubcliente
            End Get
            Set(value As String)
                _codigoSubcliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace