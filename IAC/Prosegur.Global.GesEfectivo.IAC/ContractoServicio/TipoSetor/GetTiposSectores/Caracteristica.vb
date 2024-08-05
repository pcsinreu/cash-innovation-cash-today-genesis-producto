Namespace TipoSetor.GetTiposSectores

    <Serializable()> _
    Public Class Caracteristica

#Region "Variáveis"

        Private _codCaractTipoSector As String

#End Region

#Region "Propriedades"

        Public Property codCaractTipoSector() As String
            Get
                Return _codCaractTipoSector
            End Get
            Set(value As String)
                _codCaractTipoSector = value
            End Set
        End Property

#End Region

    End Class
End Namespace
