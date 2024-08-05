Namespace IngresoContado

    ''' <summary>
    ''' Classe IntervencionSupervisorParcial
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class IntervencionSupervisorRemesa
        Inherits IntervencionSupervisorGenerico

#Region "Variáveis"

        Private _IdentificadorRemesa As String

#End Region

#Region "Propriedades"

        Public Property IdentificadorRemesa() As String
            Get
                Return _IdentificadorRemesa
            End Get
            Set(value As String)
                _IdentificadorRemesa = value
            End Set
        End Property

#End Region

    End Class

End Namespace