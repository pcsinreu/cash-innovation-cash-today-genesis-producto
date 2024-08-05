Namespace TipoSetor.GetCaractNoPertenecTipoSector
    <Serializable()> _
    Public Class TipoSectorNotPercete

#Region "[VARIAVEIS]"

        Private _codTipoSector As String
        Private _codCaractTipoSector As String
        Private _desCaractTipoSector As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property codTipoSector() As String
            Get
                Return _codTipoSector
            End Get
            Set(value As String)
                _codTipoSector = value
            End Set
        End Property

        Public Property codCaractTipoSector() As String
            Get
                Return _codCaractTipoSector
            End Get
            Set(value As String)
                _codCaractTipoSector = value
            End Set
        End Property

        Public Property desCaractTipoSector() As String
            Get
                Return _desCaractTipoSector
            End Get
            Set(value As String)
                _desCaractTipoSector = value
            End Set
        End Property
#End Region

    End Class
End Namespace
