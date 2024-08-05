Namespace Clases

    <Serializable()>
    Public Class Direccion
        Inherits DireccionBase

#Region "[VARIAVEIS]"

        Private _codTipoTablaGenesis As String
        Private _oidTablaGenesis As String
        Private _codTablaGenesis As String
        Private _desTablaGenesis As String
        Private _gmtCreacion As Nullable(Of DateTime)
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As Nullable(Of DateTime)
        Private _desUsuarioModificacion As String

#End Region

#Region "[PROPRIEDADES]"


        Public Property oidTablaGenesis() As String
            Get
                Return _oidTablaGenesis
            End Get
            Set(value As String)
                _oidTablaGenesis = value
            End Set
        End Property

        Public Property codTipoTablaGenesis() As String
            Get
                Return _codTipoTablaGenesis
            End Get
            Set(value As String)
                _codTipoTablaGenesis = value
            End Set
        End Property

        Public Property gmtCreacion() As Nullable(Of DateTime)
            Get
                Return _gmtCreacion
            End Get
            Set(value As Nullable(Of DateTime))
                _gmtCreacion = value
            End Set
        End Property

        Public Property desUsuarioCreacion() As String
            Get
                Return _desUsuarioCreacion
            End Get
            Set(value As String)
                _desUsuarioCreacion = value
            End Set
        End Property

        Public Property gmtModificacion() As Nullable(Of DateTime)
            Get
                Return _gmtModificacion
            End Get
            Set(value As Nullable(Of DateTime))
                _gmtModificacion = value
            End Set
        End Property

        Public Property desUsuarioModificacion() As String
            Get
                Return _desUsuarioModificacion
            End Get
            Set(value As String)
                _desUsuarioModificacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace
