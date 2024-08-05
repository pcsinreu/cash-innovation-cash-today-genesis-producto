
Namespace Paginacion.Web

    Public Class SelectDataEventArgs(Of T As IRespuestaPaginacion)
        Inherits System.EventArgs

        Private _PaginaAtual As Integer = 0
        Public Property PaginaAtual() As Integer
            Get
                Return _PaginaAtual
            End Get
            Set(value As Integer)
                _PaginaAtual = value
            End Set
        End Property

        Private _RegistrosPorPagina As Integer
        Public Property RegistrosPorPagina() As Integer
            Get
                Return _RegistrosPorPagina
            End Get
            Set(value As Integer)
                _RegistrosPorPagina = value
            End Set
        End Property

        Private _RespuestaPaginacion As T
        Public Property RespuestaPaginacion() As T
            Get
                Return _RespuestaPaginacion
            End Get
            Set(value As T)
                _RespuestaPaginacion = value
            End Set
        End Property

        Friend Sub New(PaginaAtual As String, RegistrosPorPagina As String)
            _PaginaAtual = PaginaAtual
            _RegistrosPorPagina = RegistrosPorPagina
        End Sub

    End Class

End Namespace