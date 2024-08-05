Imports Prosegur.Genesis.ContractoServicio
Imports ContractoLogin = Prosegur.Genesis.ContractoServicio.GenesisLogin

''' <summary>
''' Interface do serviço Login
''' </summary>
Public Interface IGenesisLogin

    Function Test() As Test.Respuesta

    ''' <summary>
    ''' Assinatura do método Login
    ''' </summary>
    Function EjecutarLogin2(Peticion As ContractoLogin.EjecutarLogin.Peticion) As ContractoLogin.EjecutarLogin.Respuesta

    ''' <summary>
    ''' Assinatura do método ObtenerDelegaciones
    ''' </summary>
    Function ObtenerDelegaciones2(Peticion As ContractoLogin.ObtenerDelegaciones.Peticion) As ContractoLogin.ObtenerDelegaciones.Respuesta

    ''' <summary>
    ''' Assinatura do método ObtenerAplicaciones
    ''' </summary>
    Function ObtenerAplicaciones2(Peticion As ContractoLogin.ObtenerAplicaciones.Peticion) As ContractoLogin.ObtenerAplicaciones.Respuesta

    ''' <summary>
    ''' Assinatura do método ObtenerPermisos
    ''' </summary>
    Function ObtenerPermisos2(Peticion As ContractoLogin.ObtenerPermisos.Peticion) As ContractoLogin.ObtenerPermisos.Respuesta

    ''' <summary>
    ''' Assinatura do método CrearTokenAcceso para a tela de login unificado
    ''' </summary>
    Function CrearTokenAcceso2(Peticion As ContractoLogin.CrearTokenAcceso.Peticion) As ContractoLogin.CrearTokenAcceso.Respuesta

    ''' <summary>
    ''' Assinatura do método ConsumirTokenAcceso para a tela de login unificado
    ''' </summary>
    Function ConsumirTokenAcceso2(Peticion As ContractoLogin.ConsumirTokenAcceso.Peticion) As ContractoLogin.ConsumirTokenAcceso.Respuesta

    Function ObtenerAplicacionVersion(Peticion As Login.ObtenerAplicacionVersion.Peticion) As Login.ObtenerAplicacionVersion.Respuesta

End Interface
