Namespace CargaPreviaEletronica

    ''' <summary>
    ''' Interface Configuracion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 26/03/2013 Criado
    ''' </history>
    Public Interface IConfiguracion

        Function getConfiguracionesCP(Peticion As ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Peticion) As ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Respuesta

        Function getConfiguracionCP(Peticion As ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Peticion) As ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Respuesta


    End Interface


End Namespace
