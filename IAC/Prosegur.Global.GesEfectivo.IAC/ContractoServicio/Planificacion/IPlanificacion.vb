Public Interface IPlanificacion

    Function GetPlanificaciones(objPeticion As Planificacion.GetPlanificaciones.Peticion) As Planificacion.GetPlanificaciones.Respuesta

    Function Test() As Test.Respuesta

    Function GetPlanificacionProgramacion(objPeticion As ContractoServicio.Planificacion.GetPlanificacionProgramacion.Peticion) As ContractoServicio.Planificacion.GetPlanificacionProgramacion.Respuesta

End Interface
