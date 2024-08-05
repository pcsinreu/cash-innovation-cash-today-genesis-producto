Imports System.Xml.Serialization
Imports System.Xml

Namespace Paginacion

    <Serializable()> _
    Public MustInherit Class RespuestaPaginacionBase
        Inherits RespuestaGenerico
        Implements Genesis.Comon.Paginacion.IRespuestaPaginacion

        Private _ParametrosPaginacion As New Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion()
        Public Property ParametrosPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion Implements Genesis.Comon.Paginacion.IRespuestaPaginacion.ParametrosPaginacion
            Get
                Return _ParametrosPaginacion
            End Get
            Set(value As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion)
                _ParametrosPaginacion = value
            End Set
        End Property
    End Class

End Namespace