Public Class HelperOperarioRepuesta
    Inherits ContractoServ.GetUsuariosDetail.Respuesta
    Implements Prosegur.Genesis.Comon.Paginacion.IRespuestaPaginacion
    Public Property ParametrosPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion Implements Genesis.Comon.Paginacion.IRespuestaPaginacion.ParametrosPaginacion
End Class
Public Class HelperOperarioPeticion
    Inherits ContractoServ.GetUsuariosDetail.Peticion
    Implements Prosegur.Genesis.Comon.Paginacion.IPeticionPaginacion

    Public Property ParametrosPaginacion As Genesis.Comon.Paginacion.ParametrosPeticionPaginacion Implements Genesis.Comon.Paginacion.IPeticionPaginacion.ParametrosPaginacion
End Class