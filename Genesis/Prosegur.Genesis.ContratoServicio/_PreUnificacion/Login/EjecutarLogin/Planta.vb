Namespace Login.EjecutarLogin

    <Serializable()> _
    Public Class Planta

#Region "Variáveis"

        Private _codPlanta As String
        Private _desPlanta As String
        Private _oidPlanta As String
        Private _TiposSectores As New List(Of TipoSector)

#End Region

#Region "Propriedades"

        Public Property CodigoPlanta() As String
            Get
                Return _codPlanta
            End Get
            Set(value As String)
                _codPlanta = value
            End Set
        End Property

        Public Property DesPlanta() As String
            Get
                Return _desPlanta
            End Get
            Set(value As String)
                _desPlanta = value
            End Set
        End Property

        Public Property oidPlanta() As String
            Get
                Return _oidPlanta
            End Get
            Set(value As String)
                _oidPlanta = value
            End Set
        End Property

        Public Property TiposSectores() As List(Of TipoSector)
            Get
                Return _TiposSectores
            End Get
            Set(value As List(Of TipoSector))
                _TiposSectores = value
            End Set
        End Property

#End Region

    End Class
End Namespace