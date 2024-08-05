Imports System.Xml.Serialization
Imports System.Xml

Namespace Paginacion

    <Serializable()> _
    Public Class ParametrosPeticionPaginacion

        Public Property RealizarPaginacion() As Boolean = True
        Public Property IndicePagina() As Integer
        Public Property RegistrosPorPagina() As Integer

    End Class

End Namespace