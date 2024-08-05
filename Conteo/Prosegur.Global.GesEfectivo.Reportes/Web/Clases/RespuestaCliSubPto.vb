<Serializable()> _
  Public Class RespuestaCliSubPto

    Public Property ClientesSeleccionados As New IAC.ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
    Public Property SubClientesSeleccionados As New IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion
    Public Property PuntoServicioSeleccionados As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
    Public Property GrupoClienteSeleccionados As New IAC.ContractoServicio.GrupoCliente.GrupoClienteColeccion
    Public Property BolTodosSubClientesSeleccionados As Boolean
    Public Property BolTodosPuntoServicioSeleccionados As Boolean

End Class
