Namespace IngresoContado

    ''' <summary>
    ''' Classe IntervencionSupervisorParcial
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class IntervencionSupervisorBulto
        Inherits IntervencionSupervisorGenerico

#Region "Variáveis"

        Private _IdentificadorBulto As String

#End Region

#Region "Propriedades"

        Public Property IdentificadorBulto() As String
            Get
                Return _IdentificadorBulto
            End Get
            Set(value As String)
                _IdentificadorBulto = value
            End Set
        End Property

#End Region

    End Class
End Namespace