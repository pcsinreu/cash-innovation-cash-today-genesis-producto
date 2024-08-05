Namespace GetClientesSubClientesPuntoServicios

    ''' <summary>
    ''' Classe Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class ClienteOk

#Region "[VARIAVEIS]"

        Private _CodCliente As String

#End Region

#Region "[METODOS]"

        Public Property CodCliente() As String
            Get
                Return _CodCliente
            End Get
            Set(value As String)
                _CodCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace