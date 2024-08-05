Namespace IngresoContado

    ''' <summary>
    ''' Classe IntervencionSupervisorParcial
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class IntervencionSupervisorParcial
        Inherits IntervencionSupervisorGenerico

#Region "Variáveis"

        Private _IdentificadorParcial As String

#End Region

#Region "Propriedades"

        Public Property IdentificadorParcial() As String
            Get
                Return _IdentificadorParcial
            End Get
            Set(value As String)
                _IdentificadorParcial = value
            End Set
        End Property

#End Region

    End Class
End Namespace