Public Interface IConfiguracionGeneral
    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 04/07/2013 Criado
    ''' </history>
    Function Test() As ContractoServicio.Test.Respuesta

    ''' <summary>
    ''' Interface que deve ser implementada para tratar das configuracines generales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 04/07/2013 Criado
    ''' </history>
    Function GetConfiguracionGeneralReportes() As ContractoServicio.Configuracion.General.Respuesta

    ''' <summary>
    ''' Insere uma nova configuração geral de relatório.
    ''' </summary>
    ''' <param name="peticion">Configuração a ser inserida.</param>
    ''' <returns>Reposta do insert.</returns>
    ''' <remarks></remarks>
    Function InserirConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta

    ''' <summary>
    ''' Exclui uma ou mais configuração geral de relatório.
    ''' </summary>
    ''' <param name="peticion">Configurações a serem excluídas.</param>
    ''' <returns>Reposta do delete.</returns>
    ''' <remarks></remarks>
    Function ExcluirConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta

    ''' <summary>
    ''' Atualiza a configuração geral de relatório.
    ''' </summary>
    ''' <param name="peticion">Configuração a ser atualizada.</param>
    ''' <returns>Reposta da atualização.</returns>
    ''' <remarks></remarks>
    Function AtualizarConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta

    ''' <summary>
    ''' Recupera uma configuração geral.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 04/07/2013 Criado
    ''' </history>
    Function GetConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta
End Interface

