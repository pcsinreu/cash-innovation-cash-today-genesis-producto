Imports Prosegur.Genesis.ContractoServicio

''' <summary>
''' Interface do serviço Login
''' </summary>
''' <remarks></remarks>
''' <history>
''' [prezende]  23/05/2012  criado
''' </history>
Public Interface ILogin

    Function Test() As Test.Respuesta

    ''' <summary>
    ''' Firma del método para obtener una colección de paises
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [achimuris]  05/05/2021  Creado
    ''' </history>
    Function ObtenerPaises() As Login.ObtenerPaises.Respuesta

    ''' <summary>
    ''' Metodo para obtener la información del usuario que inicia sesión
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lsassi]  30/07/2021  Creado
    ''' </history>
    Function ObtenerInformacionLogin(Peticion As Login.ObtenerInformacionLogin.Peticion) As Login.EjecutarLogin.Respuesta

    ''' <summary>
    ''' Assinatura do método Login
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende]  23/05/2012  criado
    ''' </history>
    Function EjecutarLogin(Peticion As Login.EjecutarLogin.Peticion) As Login.EjecutarLogin.Respuesta

    ''' <summary>
    ''' Assinatura do método ObtenerAplicacionVersion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende]  23/05/2012  criado
    ''' </history>
    Function ObtenerAplicacionVersion(Peticion As Login.ObtenerAplicacionVersion.Peticion) As Login.ObtenerAplicacionVersion.Respuesta

    ''' <summary>
    ''' Assinatura do método ObtenerVersiones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ObtenerVersiones(Peticion As Login.ObtenerVersiones.Peticion) As Login.ObtenerVersiones.Respuesta

    ''' <summary>
    ''' Assinatura do método ObtenerDelegaciones para a tela de login unificado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [fmiranda]  30/07/2012  criado
    ''' </history>
    Function ObtenerDelegaciones(Peticion As Login.ObtenerDelegaciones.Peticion) As Login.ObtenerDelegaciones.Respuesta

    ''' <summary>
    ''' Assinatura do método CrearTokenAcceso para a tela de login unificado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [fmiranda]  02/08/2012  criado
    ''' </history>
    Function CrearTokenAcceso(Peticion As Login.CrearTokenAcceso.Peticion) As Login.CrearTokenAcceso.Respuesta

    ''' <summary>
    ''' Assinatura do método ConsumirTokenAcceso para a tela de login unificado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [fmiranda]  03/08/2012  criado
    ''' </history>
    Function ConsumirTokenAcceso(Peticion As Login.ConsumirTokenAcceso.Peticion) As Login.ConsumirTokenAcceso.Respuesta

    ''' <summary>
    ''' Assinatura do método que irá buscar as delegaciones que o usuário possui permissão
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.seabra]  06/03/2013  criado
    ''' </history>
    Function GetDelegacionesUsuario(Peticion As Login.GetDelegacionesUsuario.Peticion) As Login.GetDelegacionesUsuario.Respuesta

    Function EjecutarLoginAplicacion(Peticion As Genesis.ContractoServicio.Login.EjecutarLoginAplicacion.Peticion) As Genesis.ContractoServicio.Login.EjecutarLoginAplicacion.Respuesta

    Function AutenticarUsuarioAplicacion(Peticion As Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionPeticion) As Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionRespuesta

    Function ObtenerPermisosUsuario(Peticion As Login.ObtenerPermisosUsuario.Peticion) As Login.ObtenerPermisosUsuario.Respuesta

    Function ValidarPermisosUsuario(Peticion As Login.ValidarPermisosUsuario.Peticion) As Login.ValidarPermisosUsuario.Respuesta

End Interface
