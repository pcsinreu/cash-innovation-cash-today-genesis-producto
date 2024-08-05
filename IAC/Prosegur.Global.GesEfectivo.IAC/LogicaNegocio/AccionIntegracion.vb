Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports Prosegur.Genesis
Imports System.IO
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Text

Public Class AccionIntegracion
    Implements Integracion.ContractoServicio.IIntegracion


#Region "[Métodos WS]"

    ''' <summary>
    ''' Metodo SetConfiguracionReporte
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetConfiguracionReporte(Peticion As Integracion.ContractoServicio.Reportes.SetConfiguracionReporte.Peticion) As Integracion.ContractoServicio.Reportes.SetConfiguracionReporte.Respuesta Implements Integracion.ContractoServicio.IIntegracion.SetConfiguracionReporte

        Dim objRespuesta As New Reportes.SetConfiguracionReporte.Respuesta

        Try

            'Executa a validação de dados.
            ValidarDatos(Peticion)

            Dim objTransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)
            Dim objConfigExcluir As List(Of String) = If(Peticion.EsExclusion, New List(Of String), Nothing)
            Dim OidParametro As String = String.Empty
            Dim objColParametros As List(Of String) = Nothing

            For Each ConfigReporte In Peticion.ConfiguracionesReportes

                objColParametros = New List(Of String)

                If Peticion.EsExclusion Then
                    'armazena as configurações a serem excluidas.
                    objConfigExcluir.Add(ConfigReporte.IdentificadorConfiguracion)
                Else

                    If String.IsNullOrEmpty(ConfigReporte.IdentificadorConfiguracion) Then
                        'Insere a configuração
                        AccesoDatos.ConfiguracionReportes.InserirConfiguracionReporte(ConfigReporte, objTransacion)

                    Else
                        ' Deleta os parametros da configuração
                        AccesoDatos.ParametrosReporte.DeletarParametros(ConfigReporte.IdentificadorConfiguracion, objTransacion)

                        'Atualiza as configuração
                        AccesoDatos.ConfiguracionReportes.ActualizarConfiguracion(ConfigReporte, objTransacion)

                        'Deleta as configurações gerais.
                        AccesoDatos.ReportesConfiguracionGeneral.DeletarReportesConfiguracion(ConfigReporte.IdentificadorConfiguracion, objTransacion)
                    End If

                    'Insere as configurações gerais
                    For Each objReport In ConfigReporte.Reportes
                        AccesoDatos.ReportesConfiguracionGeneral.InserirReportesConfiguracion(ConfigReporte.IdentificadorConfiguracion, objReport.IdentificadorConfiguracionGeneral, objTransacion)
                    Next

                    If ConfigReporte.ParametrosReporte IsNot Nothing AndAlso ConfigReporte.ParametrosReporte.Count > 0 Then

                        For Each ParamRep In ConfigReporte.ParametrosReporte

                            OidParametro = AccesoDatos.ParametrosReporte.RetornarOidParametro(ConfigReporte.IdentificadorConfiguracion, ParamRep.CodParametro, ParamRep.CodParametro)

                            If String.IsNullOrEmpty(OidParametro) Then
                                'Insere o parametro
                                OidParametro = AccesoDatos.ParametrosReporte.InserirParametros(ConfigReporte.IdentificadorConfiguracion, ParamRep, objTransacion)
                            Else
                                AccesoDatos.ParametrosReporte.ActualizarParametro(OidParametro, ParamRep.DesParametro, objTransacion)
                            End If

                            objColParametros.Add(OidParametro)

                        Next

                    End If

                    objRespuesta.IdentificadorConfiguracion = ConfigReporte.IdentificadorConfiguracion
                    objRespuesta.DesConfiguracion = ConfigReporte.DesConfiguracion

                End If
            Next

            'Exclui as configurações
            If Peticion.EsExclusion Then
                AccesoDatos.ConfiguracionReportes.ExcluirConfiguracionReporte(objConfigExcluir, objTransacion)
            End If

            objTransacion.RealizarTransacao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty


        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            If ex.Message.Contains("AK_IAPR_TCONFIG_REPORTES_1") Then
                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                objRespuesta.MensajeError = Traduzir("063_msg_configuracionDuplicado")
            Else
                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString()
            End If

            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Metodo GetConfiguracionesReportesDetail
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConfiguracionesReportesDetail(Peticion As Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Peticion) As Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetConfiguracionesReportesDetail

        Dim objRespuesta As New Reportes.GetConfiguracionesReportesDetail.Respuesta

        Try

            Util.ValidarCampoObrigatorio(Peticion.IdentificadoresConfiguracion, "013_IdentificadorConfiguracion", GetType(List(Of String)), True, True)

            objRespuesta.ConfiguracionesReportes = AccesoDatos.ConfiguracionReportes.RetornarConfiguracion(Peticion.IdentificadoresConfiguracion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Metodo GetConfiguracionesReportesDetail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RecuperarModulos() As Integracion.ContractoServicio.Modulo.RecuperarModulos.Respuesta Implements Integracion.ContractoServicio.IIntegracion.RecuperarModulos

        Dim objRespuesta As New Modulo.RecuperarModulos.Respuesta

        Try


            objRespuesta.Modulos = AccesoDatos.Modulo.RecuperarModulos()
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Metodo GetConfiguracionesReporte
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Respuesta</remarks>
    Public Function GetConfiguracionesReportes(Peticion As Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Peticion) As Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetConfiguracionesReportes

        Dim objRespuesta As New Reportes.GetConfiguracionesReportes.Respuesta

        Try
            objRespuesta.ConfiguracionesReportes = AccesoDatos.ConfiguracionReportes.RetornarConfiguracionesReportes(Peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty


        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Metodo responsavel por recuperar valores posibles por nível de cliente, subcliente e puntoservicio.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos] 05/12/2011 - Criado
    ''' </history>
    Public Function RecuperaValoresPosiblesPorNivel(Peticion As GesEfectivo.IAC.Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.Peticion) As GesEfectivo.IAC.Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.Respuesta Implements GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.RecuperaValoresPosiblesPorNivel
        Dim objRespuesta As New RecuperaValoresPosiblesPorNivel.Respuesta

        Try
            objRespuesta.Terminos = EjecutarRecuperaValoresPosiblesPorNivel(Peticion)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Metodo responsavel por recuperar os parametros a nivel de delegação e pais.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/10/2011 - Criado
    ''' </history>
    Public Function GetParametrosDelegacionPais(Peticion As Integracion.ContractoServicio.GetParametrosDelegacionPais.Peticion) As Integracion.ContractoServicio.GetParametrosDelegacionPais.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetParametrosDelegacionPais

        Dim objRespuesta As New GetParametrosDelegacionPais.Respuesta

        Try
            objRespuesta.Parametros = EjecutarGetParametrosDelegacionPais(Peticion)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Metodo Responsavel por fazer a inserção dos valores dos parametros vindos do conteo.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/09/2011 - Criado
    ''' </history>
    Public Function ImportarParametros(Peticion As Integracion.ContractoServicio.ImportarParametros.Peticion) As Integracion.ContractoServicio.ImportarParametros.Respuesta Implements Integracion.ContractoServicio.IIntegracion.ImportarParametros

        Dim objRespuesta As New ImportarParametros.Respuesta

        Try

            EjecutarImportarParametros(Peticion)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Método responsável por recuperar os parâmetros de posto configurados.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RecuperarParametros(Peticion As Integracion.ContractoServicio.RecuperarParametros.Peticion) As Integracion.ContractoServicio.RecuperarParametros.Respuesta Implements Integracion.ContractoServicio.IIntegracion.RecuperarParametros

        Dim objRespuesta As New RecuperarParametros.Respuesta

        Try

            If Not String.IsNullOrEmpty(Peticion.CodigoDelegacion) AndAlso String.IsNullOrEmpty(AccesoDatos.Delegacion.ObterOIDDelegacion(Peticion.CodigoDelegacion)) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_parametro_iac_inexistente"))
            End If

            objRespuesta.DatosPuesto = EjecutarRecuperarParametros(Peticion)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Metodo responsavel por fazer a inserção, deleção, e atualização de clientes.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/03/2009 Created
    ''' [octavio.piramo] 10/03/2009 Alterado
    ''' [leandro.andrade] 14/07/2010 Alterado
    ''' </history>
    Public Function SetCliente(Peticion As SetCliente.Peticion) As SetCliente.Respuesta Implements IIntegracion.SetCliente

        Dim objRespuesta As New SetCliente.Respuesta
        objRespuesta.RespuestaClientes = New SetCliente.RespuestaClienteColeccion
        Dim temErro As Boolean = False

        If ValidarPeticion(Peticion, objRespuesta) Then

            For Each cl As SetCliente.Cliente In Peticion.Clientes

                Dim objRespuestaCliente As New SetCliente.RespuestaCliente
                objRespuestaCliente.CodigoCliente = cl.Codigo

                Try

                    If String.IsNullOrEmpty(cl.Codigo) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_ClienteCodigoVazio"))
                    End If

                    ' obter oid cliente
                    Dim oidCliente As String = IAC.AccesoDatos.Cliente.BuscarOidCliente(cl.Codigo)

                    ' caso encontre o cliente
                    If oidCliente <> String.Empty Then

                        'chama metodo de atualizar e deletar subclientes
                        ModificarClientes(cl, Peticion.CodUsuario, oidCliente)

                    Else

                        If String.IsNullOrEmpty(cl.Descripcion) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_ClienteDescripcion"))
                        End If

                        'chama metodo de alta de clientes
                        IAC.AccesoDatos.Cliente.AltaCliente(cl, Peticion.CodUsuario)

                    End If

                    objRespuestaCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuestaCliente.MensajeError = String.Empty
                    objRespuestaCliente.CodigoCliente = cl.Codigo

                Catch ex As Excepcion.NegocioExcepcion

                    objRespuestaCliente.CodigoError = ex.Codigo
                    objRespuestaCliente.MensajeError = ex.Descricao
                    objRespuestaCliente.CodigoCliente = cl.Codigo
                    temErro = True

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)
                    objRespuestaCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                    objRespuestaCliente.MensajeError = ex.ToString()
                    objRespuestaCliente.CodigoCliente = cl.Codigo
                    temErro = True

                Finally

                    objRespuesta.RespuestaClientes.Add(objRespuestaCliente)

                End Try

            Next
        Else
            temErro = True
        End If

        If temErro Then

            If String.IsNullOrEmpty(objRespuesta.CodigoError) AndAlso String.IsNullOrEmpty(objRespuesta.MensajeError) Then
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = Traduzir("013_msg_ErroCollecionClientes")
            End If

        Else
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
        End If

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Faz a pesquisa de medios de pago passando como parametro data inicial, data final e vigente.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/03/2009 Created
    ''' </history>
    Public Function GetMediosPagoIntegracion(Peticion As GetMediosPago.Peticion) As GetMediosPago.Respuesta Implements IIntegracion.GetMediosPago

        Dim objRespuesta As New GetMediosPago.Respuesta

        Try

            objRespuesta.MediosdePago = AccesoDatos.MedioPago.GetMediosPagoIntegracion(Peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Faz a pesquisa de iac passando como parametro data inicial, data final e vigente.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/03/2009 Created
    ''' </history>
    Public Function GetIacIntegracion(Peticion As GetIac.Peticion) As GetIac.Respuesta Implements IIntegracion.GetIac

        Dim objRespuesta As New GetIac.Respuesta

        Try

            objRespuesta.Iacs = AccesoDatos.Iac.GetIacIntegracion(Peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta
    End Function

    Public Function GetProceso(Peticion As GetProceso.Peticion) As GetProceso.Respuesta Implements IIntegracion.GetProceso
        Dim objRespuesta As New GetProceso.Respuesta

        Try

            ValidarDados(Peticion)

            objRespuesta.Proceso = ObtenerProceso(Peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    Public Function GetProcesoCP(Peticion As GetProceso.Peticion) As GetProceso.Respuesta Implements IIntegracion.GetProcesoCP
        Dim objRespuesta As New GetProceso.Respuesta

        Try

            ValidarDadosCP(Peticion)

            objRespuesta.Proceso = ObtenerProceso(Peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    Public Function GetProcesos(Peticion As GetProcesos.Peticion) As GetProcesos.Respuesta Implements IIntegracion.GetProcesos

        'Instancia novo objeto de resposta
        Dim objRespuesta As New GetProcesos.Respuesta

        Try

            objRespuesta.Proceso = ObtenerProcesos(Peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/02/2010 - Criado
    ''' </history>
    Public Function Test() As Integracion.ContractoServicio.Test.Respuesta Implements Integracion.ContractoServicio.IIntegracion.Test
        Dim objRespuesta As New Integracion.ContractoServicio.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("021_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Esta operación es responsable por obtener los datos de los componentes pertenecientes la morfología del mensaje de entrada.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/12/2010 Criado
    ''' </history>
    Public Function GetMorfologiaDetail(Peticion As Integracion.ContractoServicio.GetMorfologiaDetail.Peticion) As Integracion.ContractoServicio.GetMorfologiaDetail.Respuesta Implements IIntegracion.GetMorfologiaDetail

        ' criar objeto respuesta
        Dim objRespuesta As New Integracion.ContractoServicio.GetMorfologiaDetail.Respuesta

        Try

            If ValidarGetMorfologiaDetail(Peticion) Then

                objRespuesta.Morfologia = ObtenerMorfologia(Peticion.OidMorfologia)

            End If

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            'objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    Public Function GetProcesosPorDelegacion(Peticion As GetProcesosPorDelegacion.Peticion) As GetProcesosPorDelegacion.Respuesta Implements IIntegracion.GetProcesosPorDelegacion

        ' instancia novo objeto de resposta
        Dim objRespuesta As New GetProcesosPorDelegacion.Respuesta

        Try

            objRespuesta.Procesos = ObtenerProcesosPorDelegacion(Peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    Public Function GetATM(Peticion As GetATM.Peticion) As GetATM.Respuesta Implements IIntegracion.GetATM

        Dim objRespuesta As New GetATM.Respuesta

        Try

            'Validar obrigatoriedade dos parâmetros de entrada
            ValidarPeticion(Peticion)

            'Recuperar dados do ATM por Cliente, Subcliente e Punto de Servicio
            objRespuesta.ATM = AccesoDatos.Cajero.GetATMByFiliacion(Peticion.CodigoCliente, Peticion.CodigoSubcliente, Peticion.CodigoPuntoServicio)

            If objRespuesta.ATM IsNot Nothing Then

                'Recuperar a morfologia vigente do ATM para a data informada
                objRespuesta.ATM.Morfologias = AccesoDatos.Morfologia.GetMorfologiaByIdAtm(objRespuesta.ATM.IdCajero, Peticion.FechaServicio)

                If Not String.IsNullOrEmpty(Peticion.CodigoClienteFaturacion) Then

                    objRespuesta.ATM.CodigoClienteFaturacion = Peticion.CodigoClienteFaturacion
                    objRespuesta.ATM.DescripcionClienteFaturacion = AccesoDatos.Cliente.BuscarDesCliente(Peticion.CodigoClienteFaturacion)

                    If String.IsNullOrEmpty(objRespuesta.ATM.DescripcionClienteFaturacion) Then

                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_desclientefacturacion"))

                    End If

                End If

            Else

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_nenhum_atm"))

            End If

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Esta operación es responsable por obtener los datos del ATM de acuerdo con los parámetros de entrada. 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos] 17/03/2011 Criado
    ''' </history>
    Public Function GetATMByRegistrarTira(Peticion As Integracion.ContractoServicio.GetATMByRegistrarTira.Peticion) As Integracion.ContractoServicio.GetATMByRegistrarTira.Respuesta Implements IIntegracion.GetATMByRegistrarTira

        ' criar objeto respuesta
        Dim objRespuesta As New Integracion.ContractoServicio.GetATMByRegistrarTira.Respuesta

        Try

            If ValidarGetATMByRegistrarTira(Peticion) Then
                With Peticion

                    objRespuesta.ATMs = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Cajero.GetATMsByRegistrarTira(.CodigoDelegacion,
                                                                                                .CodigoCajero,
                                                                                                .CodigoCliente,
                                                                                                .CodigoSubcliente,
                                                                                                .CodigoPuntoServicio)

                End With

            End If

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            'objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    Public Function GetATMsSimplificado(Peticion As Integracion.ContractoServicio.GetATMsSimplificado.Peticion) As Integracion.ContractoServicio.GetATMsSimplificado.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetATMsSimplificado
        Dim objRespuesta As New Integracion.ContractoServicio.GetATMsSimplificado.Respuesta
        Try

            ValidarPeticon(Peticion)

            objRespuesta.ATM = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Cajero.GetATMsSimplificado(Peticion)

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            'objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    Public Function GetATMsSimplificadoV2(Peticion As Integracion.ContractoServicio.GetATMsSimplificadoV2.Peticion) As Integracion.ContractoServicio.GetATMsSimplificadoV2.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetATMsSimplificadoV2
        Dim objRespuesta As New Integracion.ContractoServicio.GetATMsSimplificadoV2.Respuesta
        Try

            ValidarPeticon(Peticion)

            objRespuesta.ATM = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Cajero.GetATMsSimplificadoV2(Peticion)

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            'objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

#End Region

#Region "[VALIDAR]"

    ''' <summary>
    ''' Ejecuta validaciones basicas en el objeto peticion.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <param name="Respuesta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [leandro.andrade] 14/07/2010 Created
    ''' </history>
    Private Function ValidarPeticion(Peticion As SetCliente.Peticion,
                                     Respuesta As SetCliente.Respuesta) As Boolean


        If Peticion.Clientes Is Nothing OrElse Peticion.Clientes.Count = 0 Then

            Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            Respuesta.MensajeError = Traduzir("013_msg_sinClientes")
            Return False
        End If

        If String.IsNullOrEmpty(Peticion.CodUsuario) Then
            Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            Respuesta.MensajeError = Traduzir("013_msg_sinUsuario")
            Return False
        End If
        Return True

    End Function

    Private Sub ValidarPeticion(Peticion As GetATM.Peticion)

        If String.IsNullOrEmpty(Peticion.CodigoCliente) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_codigocliente"))
        End If

        If String.IsNullOrEmpty(Peticion.CodigoSubcliente) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_codigosubcliente"))
        End If

        If String.IsNullOrEmpty(Peticion.CodigoPuntoServicio) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_codigoptoservicio"))
        End If

    End Sub

    Private Sub ValidarDatos(Peticion As Reportes.SetConfiguracionReporte.Peticion)

        Util.ValidarCampoObrigatorio(Peticion.ConfiguracionesReportes, "013_ConfiguracionesReportes", GetType(Reportes.ConfiguracionReporteColeccion), True, True)

        For Each CR In Peticion.ConfiguracionesReportes

            If Peticion.EsExclusion Then
                Util.ValidarCampoObrigatorio(CR.IdentificadorConfiguracion, "013_IdentificadorConfiguracion", GetType(String), False, True)
                Continue For
            End If

            Util.ValidarCampoObrigatorio(CR.DesConfiguracion, "013_DesConfiguracion", GetType(String), False, True)
            Util.ValidarCampoObrigatorio(CR.Reportes, "013_Reportes", GetType(Reportes.ReportesColeccion), True, True)

            For Each objReporte In CR.Reportes

                Util.ValidarCampoObrigatorio(objReporte.IdentificadorConfiguracionGeneral, "013_IdentificadorConfiguracionGeneral", GetType(String), False, True)

            Next

            Util.ValidarCampoObrigatorio(CR.CodUsuario, "013_CodUsuario", GetType(String), False, True)

            If CR.ParametrosReporte IsNot Nothing AndAlso CR.ParametrosReporte.Count > 0 Then

                For Each PR In CR.ParametrosReporte
                    Util.ValidarCampoObrigatorio(PR.CodNombreParametro, "013_CodParametro", GetType(String), False, True)
                Next

            End If

        Next

    End Sub

    Private Sub ValidarDados(Peticion As GetProceso.Peticion)

        If String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_DelegacionCodigoVazio"))
        End If

        If String.IsNullOrEmpty(Peticion.CodigoCliente) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_ClienteCodigoVazio"))
        End If

        'If String.IsNullOrEmpty(Peticion.CodigoSubcliente) Then
        '    Throw New Excepcion.NegocioExcepcion(Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_SubClienteCodigoVazio"))
        'End If

        'If String.IsNullOrEmpty(Peticion.CodigoPuntoServicio) Then
        '    Throw New Excepcion.NegocioExcepcion(Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_PuntoServicioCodigoVazio"))
        'End If

        If String.IsNullOrEmpty(Peticion.CodigoSubcanal) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_SubCanalCodigoVazio"))
        End If

    End Sub

    Private Sub ValidarDadosCP(Peticion As GetProceso.Peticion)

        If String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_DelegacionCodigoVazio"))
        End If

        If String.IsNullOrEmpty(Peticion.CodigoCliente) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_ClienteCodigoVazio"))
        End If

        If String.IsNullOrEmpty(Peticion.CodigoSubcanal) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_SubCanalCodigoVazio"))
        End If

    End Sub

    ''' <summary>
    '''  valida petição da operação GetMorfologiaDetail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  21/12/2010  criado
    ''' </history>
    Private Shared Function ValidarGetMorfologiaDetail(Peticion As Integracion.ContractoServicio.GetMorfologiaDetail.Peticion) As Boolean

        If String.IsNullOrEmpty(Peticion.OidMorfologia) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("022_msg_oidmorfologia"))

        End If

        Return True

    End Function

    ''' <summary>
    '''  valida petição da operação GetATMByRegistrarTira
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  21/12/2010  criado
    ''' </history>
    Private Shared Function ValidarGetATMByRegistrarTira(Peticion As Integracion.ContractoServicio.GetATMByRegistrarTira.Peticion) As Boolean

        ' codigoDelegacion é obrigatório
        If String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_codigodelegacion"))

        End If

        Return True

    End Function

    ''' <summary>
    '''  valida petição da operação RecuperaValoresPosiblesPorNivel
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos]  05/12/2011  criado
    ''' </history>
    Private Shared Function ValidarRecuperaValoresPosiblesPorNivel(Peticion As Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.Peticion) As Boolean

        ' cliente é obrigatório
        'If String.IsNullOrEmpty(Peticion.Cliente) Then

        '    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("009_msg_cliente"))

        'End If

        If Peticion.Terminos Is Nothing OrElse Peticion.Terminos.Count = 0 Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("009_msg_terminos"))

        End If

        Return True

    End Function

#End Region

    ''' <summary>
    ''' Modifica os clientes e subclientes.
    ''' </summary>
    ''' <param name="objCliente"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/03/2009 Created
    ''' [maoliveira]   12/05/2010 Alterado
    ''' </history>
    Private Sub ModificarClientes(objCliente As SetCliente.Cliente,
                                  codigoUsuario As String,
                                  oidCliente As String)

        Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        'chama metodo atualizar clientes
        IAC.AccesoDatos.Cliente.ActualizarCliente(objCliente, codigoUsuario, objtransacion)


        If objCliente.SubClientes IsNot Nothing AndAlso objCliente.SubClientes.Count > 0 Then

            Dim dtSubCliente As DataTable = AccesoDatos.SubCliente.BuscarTodosOidsSubClienteByOidCliente(oidCliente)

            For Each sc As SetCliente.SubCliente In objCliente.SubClientes

                If String.IsNullOrEmpty(sc.Codigo) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_SubClienteCodigoVazio"))
                End If

                ' obter oid do subcliente
                'Dim oidSubCliente As String = IAC.AccesoDatos.SubCliente.BuscarOidSubCliente(sc.Codigo, oidCliente)
                Dim codSubCliente As String = sc.Codigo

                Dim subClienteRetorno = From dr As DataRow In dtSubCliente Where dr("COD_SUBCLIENTE").Equals(codSubCliente)

                ' caso encontre um oid
                If subClienteRetorno.Count > 0 Then

                    Dim oidSubCliente As String = subClienteRetorno.ElementAt(0)("OID_SUBCLIENTE")

                    'chama metodo de atualizar e deletar subclientes
                    ModificarSubClientes(sc, codigoUsuario, oidCliente, oidSubCliente,
                                         objtransacion)


                Else

                    'verifica se a descrição não e nula
                    If String.IsNullOrEmpty(sc.Descripcion) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_SubClienteDescripcion"))
                    End If

                    'chama metodo de alta de clientes
                    IAC.AccesoDatos.SubCliente.AltaSubCliente(sc, codigoUsuario, oidCliente, objtransacion)

                End If

            Next

        End If

        ' caso o cliente não seja vigente
        If Not objCliente.Vigente Then

            ' deve efetuar a baixa de todos os subclientes e seus pontos de serviço
            IAC.AccesoDatos.SubCliente.BajaSubCliente(oidCliente, codigoUsuario, objtransacion)

        End If

        objtransacion.RealizarTransacao()

    End Sub

    ''' <summary>
    ''' Modifica os subclientes e os punto de serviços.
    ''' </summary>
    ''' <param name="objSubCliente">Objeto com os dados do sub cliente</param>
    ''' <param name="codigoUsuario">Código do usuário</param>
    ''' <param name="oidCliente">Identificador do cliente</param>
    ''' <param name="oidSubCliente">Identificador do sub cliente</param>
    ''' <param name="objtransacion">Objeto com a transação</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/03/2009 Created
    ''' [maoliveira]   12/05/2010 Alterado
    ''' </history>
    Private Sub ModificarSubClientes(objSubCliente As SetCliente.SubCliente,
                                     codigoUsuario As String,
                                     oidCliente As String,
                                     oidSubCliente As String,
                                     ByRef objtransacion As Transacao)

        'chama metodo atualizar ponto servicio
        IAC.AccesoDatos.SubCliente.ActualizarSubCliente(objSubCliente, codigoUsuario, oidCliente, oidSubCliente, objtransacion)

        ' Chamar metodo que Retorna todos os subcanais do canal com o objetivo de ganhar performace
        ' porque não será necessario para subcanal ir ao banco verificar se existe
        Dim dtPtoServicio As DataTable = AccesoDatos.PuntoServicio.BuscaTodosPuntoServicio(oidSubCliente)

        If objSubCliente.PuntoServicio IsNot Nothing AndAlso objSubCliente.PuntoServicio.Count > 0 Then

            For Each ps As SetCliente.PuntoServicio In objSubCliente.PuntoServicio

                If String.IsNullOrEmpty(ps.Codigo) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_PuntoServicioCodigoVazio"))
                End If

                ' Utilizando linq verifica em memoria se o subcanal existe no banco
                ' Se existe chama o modificiacion de subcanal
                ' Se não existe chama o insertar de subcanal
                Dim codPuntoServicio As String = ps.Codigo

                Dim puntoServicioRetorno = From dr As DataRow In dtPtoServicio Where dr("COD_PTO_SERVICIO").Equals(codPuntoServicio)

                If puntoServicioRetorno.Count = 0 Then

                    'verifica se a descrição não e nula
                    If String.IsNullOrEmpty(ps.Descripcion) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                                 Traduzir("013_msg_PuntoServicioDescripcion"))
                    End If

                    'Insere o Punto Servicio
                    IAC.AccesoDatos.PuntoServicio.AltaPuntoServicio(ps, codigoUsuario, oidSubCliente, objtransacion)

                Else

                    Dim oidPuntoServicio As String = puntoServicioRetorno.ElementAt(0)("OID_PTO_SERVICIO")

                    'Atualiza o Punto Servicio
                    IAC.AccesoDatos.PuntoServicio.ActualizarPuntoServicio(ps, codigoUsuario, oidPuntoServicio, objtransacion)

                End If

            Next

        End If

        'Se o subcliente não estiver vigente dar baja em todos os puntos servicios pertencentes ao subcliente
        If Not objSubCliente.Vigente Then

            AccesoDatos.PuntoServicio.BajaPuntoServicioByOidSubCliente(oidSubCliente, codigoUsuario, objtransacion)

        End If

    End Sub

    ''' <summary>
    ''' Verifica se o Punto de serviço exite
    ''' </summary>
    ''' <param name="objPuntoServicio"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/03/2009 Created
    ''' </history>
    Private Function VerificarPuntoServicioExiste(objPuntoServicio As SetCliente.PuntoServicioColeccion,
                                                  codigoPuntoServicio As String) As Boolean

        Dim ps = From c In objPuntoServicio Where c.Codigo = codigoPuntoServicio

        If ps Is Nothing OrElse ps.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Function ObtenerProceso(Peticion As GetProceso.Peticion) As GetProceso.Proceso

        Dim objGetProceso As New AccesoDatos.Proceso()
        Dim objProceso As GetProceso.Proceso

        'Obtem o proceso na camada de dados.
        objProceso = objGetProceso.GetProcesoIntegracion(Peticion)

        'Caso tenha retornado um processo
        If objProceso IsNot Nothing Then

            'Caso admite iac então faz pesquisa do iac e seus terminos para o proceso
            If objGetProceso.BolAdmiteIac Then

                'Obtem a IAC do proceso Parcial
                objProceso.Iac = AccesoDatos.Iac.RetornaIac(objGetProceso.OidIAC, Peticion.CodigoCliente, Peticion.CodigoSubcliente, Peticion.CodigoPuntoServicio)

                'Obtem a IAC do proceso Bulto
                objProceso.IacBulto = AccesoDatos.Iac.RetornaIac(objGetProceso.OidIACBulto, Peticion.CodigoCliente, Peticion.CodigoSubcliente, Peticion.CodigoPuntoServicio)

                'Obtem a IAC do proceso Remesa
                objProceso.IacRemesa = AccesoDatos.Iac.RetornaIac(objGetProceso.OidIACRemesa, Peticion.CodigoCliente, Peticion.CodigoSubcliente, Peticion.CodigoPuntoServicio)

            End If

            'Caso seja por agrupação
            If Not objGetProceso.BolMediosPago Then

                'Obtem as agrupações do processo
                objProceso.Agrupaciones = AccesoDatos.Agrupacion.RetornaAgrupaciones(objGetProceso.OidProceso)

            Else

                'Obtem os medios de pago do processo e todas suas dependencias
                objProceso.MediosPago = AccesoDatos.MedioPago.RetornaMediosPago(objGetProceso.OidProceso, objGetProceso.OidSubCanal, objGetProceso.OidProcesoSubCanal)

                'Obtem as divisas do processo e todas suas dependencias
                objProceso.DivisaProceso = AccesoDatos.Divisa.RetornaDivisa(objGetProceso.OidProceso)

            End If

            'Obtem os dados do ATM relacionado ao punto de servicio do processo
            objProceso.Cajero = AccesoDatos.Cajero.RetornaCajero(objGetProceso.OidPuntoServicio)

        Else

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("016_msg_ProcesoNoEncontrado"))

        End If

        Return objProceso

    End Function

    Private Function ObtenerProcesos(Peticion As GetProcesos.Peticion) As GetProcesos.ProcesoColeccion

        Dim objGetProceso As New AccesoDatos.Proceso()
        Dim objProcesos As GetProcesos.ProcesoColeccion = Nothing

        'Verifica se a Data Inicial foi informada
        If Peticion.FechaInicial <> DateTime.MinValue Then

            'Preenche coleção de Procesos
            objProcesos = AccesoDatos.Proceso.GetProcesos(Peticion)

            If objProcesos Is Nothing Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("016_msg_ProcesoNoEncontrado"))

            End If

        Else

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("019_msg_fecha_inicial"))

        End If

        Return objProcesos

    End Function

    Private Shared Function ObtenerMorfologia(OidMorfologia As String) As Integracion.ContractoServicio.GetMorfologiaDetail.Morfologia

        Dim morfologia As New Integracion.ContractoServicio.GetMorfologiaDetail.Morfologia
        Dim componente As New Integracion.ContractoServicio.GetMorfologiaDetail.Componente
        Dim obj As Integracion.ContractoServicio.GetMorfologiaDetail.Objecto
        Dim tipoMP As Integracion.ContractoServicio.GetMorfologiaDetail.TipoMedioPago
        Dim medioPago As Integracion.ContractoServicio.GetMorfologiaDetail.MedioPago
        Dim oidMedioPago As String
        Dim oidTermino As String

        Dim dt As DataTable = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Morfologia.GetMorfologia(OidMorfologia)

        ' verifica se morfologia foi encontrada
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            Return Nothing
        End If

        ' cria morfologia
        morfologia = CriarMorfologia(dt.Rows(0)("BOL_VIGENTE"), dt.Rows(0)("COD_MORFOLOGIA"), dt.Rows(0)("DES_MORFOLOGIA"), dt.Rows(0)("FYH_ACTUALIZACION"),
                                     Util.VerificarDBNull(dt.Rows(0)("NEC_MODALIDAD_RECOGIDA")))

        For Each row In dt.Rows

            If IsDBNull(row("OID_MORFOLOGIA_COMPONENTE")) Then
                Continue For
            End If

            ' cria/obtém componente
            componente = ObtenerComponente(Util.VerificarDBNull(row("OID_MORFOLOGIA_COMPONENTE")),
                                           Util.VerificarDBNull(row("COD_MORFOLOGIA_COMPONENTE")),
                                           Util.VerificarDBNull(row("BOL_VIGENTE_COMP")),
                                           Util.VerificarDBNull(row("NEC_FUNCION_CONTENEDOR")),
                                           Util.VerificarDBNull(row("COD_TIPO_CONTENEDOR")),
                                           Util.VerificarDBNull(row("DES_TIPO_CONTENEDOR")),
                                           Util.VerificarDBNull(row("NEC_ORDEN")),
                                           morfologia)

            If IsDBNull(row("OID_COMPONENTE_OBJETO")) Then
                Continue For
            End If

            ' cria/obtém objetos do componente
            obj = ObtenerObj(componente, Util.VerificarDBNull(row("DES_DIVISA")),
                             Util.VerificarDBNull(row("OID_COMPONENTE_OBJETO")),
                             Util.VerificarDBNull(row("OID_DIVISA")),
                             Util.VerificarDBNull(row("COD_ISO_DIVISA")),
                             Util.VerificarDBNull(row("nec_orden_tipo_med_pag")),
                             Util.VerificarDBNull(row("nec_orden_divisa")))

            If Not IsDBNull(row("OID_DENOMINACION")) Then

                ' preenche denominação do objeto
                If obj.Denominacion Is Nothing Then
                    obj.Denominacion = New Integracion.ContractoServicio.GetMorfologiaDetail.Denominacion
                End If

                obj.Denominacion = CriarDenominacion(Util.VerificarDBNull(row("COD_DENOMINACION")),
                             Util.VerificarDBNull(row("DES_DENOMINACION")),
                             Util.VerificarDBNull(row("NUM_VALOR")),
                             Util.VerificarDBNull(row("BOL_BILLETE")))

            End If

            If IsDBNull(row("COD_TIPO_MEDIO_PAGO")) Then
                Continue For
            End If

            ' cria/obtém tipos de medio pago
            tipoMP = ObtenerTipoMedioPago(obj, Util.VerificarDBNull(row("COD_TIPO_MEDIO_PAGO")))

            If IsDBNull(row("OID_MEDIO_PAGO")) Then
                Continue For
            End If

            oidMedioPago = row("OID_MEDIO_PAGO")

            ' verifica se término já foi adicionado a lista
            If (From objMP In tipoMP.MediosPago Where objMP.OidMedioPago = oidMedioPago).FirstOrDefault() IsNot Nothing Then
                ' medio pago já foi adicionado a lista
                Continue For
            End If

            ' cria medio pago
            medioPago = CriarMedioPago(row("COD_MEDIO_PAGO"), row("DES_MEDIO_PAGO"), row("OID_MEDIO_PAGO"))

            ' adiciona medio pago
            tipoMP.MediosPago.Add(medioPago)

            ' obtém términos do medio pago
            For Each rTermino In (From r In dt.Rows Where Not IsDBNull(r("OID_TERMINO")) AndAlso Not IsDBNull(r("OID_MEDIO_PAGO")) _
                                  AndAlso r("OID_MEDIO_PAGO") = oidMedioPago).ToList()

                oidTermino = rTermino("OID_TERMINO")

                ' verifica se término já foi adicionado
                If (From objT In medioPago.Terminos Where objT.OidTermino = oidTermino).FirstOrDefault() IsNot Nothing Then
                    ' termino já foi adicionado
                    Continue For
                End If

                ' cria término
                medioPago.Terminos.Add(CriarTerminoMP(Util.VerificarDBNull(rTermino("BOL_ES_OBLIGATORIO")),
                                                      Util.VerificarDBNull(rTermino("BOL_VIGENTE")),
                                                      Util.VerificarDBNull(rTermino("COD_TERMINO")),
                                                      Util.VerificarDBNull(rTermino("DES_TERMINO")),
                                                      Util.VerificarDBNull(rTermino("OID_TERMINO"))))

            Next

        Next

        ' configura campo BolEfectivo
        ConfigurarObjetos(morfologia)

        Return morfologia

    End Function

    ''' <summary>
    ''' Se componente já existe na lista de componentes da morfologia, retorna componente
    ''' Se não, cria, adiciona a lista e retorna o componente criado
    ''' </summary>
    ''' <param name="BolVigente"></param>
    ''' <param name="CodFuncionContenedor"></param>
    ''' <param name="CodTipoContenedor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  21/12/2010  criado
    ''' </history>
    Private Shared Function ObtenerComponente(OidMorfologiaComponente As String,
                                              CodMorfologiaComponente As String,
                                              BolVigente As Boolean,
                                              CodFuncionContenedor As String,
                                              CodTipoContenedor As String,
                                              DesTipoContenedor As String,
                                              Orden As Integer, ByRef Morfologia As Integracion.ContractoServicio.GetMorfologiaDetail.Morfologia) As Integracion.ContractoServicio.GetMorfologiaDetail.Componente

        Dim comp As Integracion.ContractoServicio.GetMorfologiaDetail.Componente

        comp = (From c In Morfologia.Componentes
                Where c.OidMorfologiaComponente = OidMorfologiaComponente).FirstOrDefault()

        If comp Is Nothing Then

            comp = New Integracion.ContractoServicio.GetMorfologiaDetail.Componente

            With comp
                .OidMorfologiaComponente = OidMorfologiaComponente
                .BolVigente = BolVigente
                .NecFuncionContenedor = CodFuncionContenedor
                .CodTipoContenedor = CodTipoContenedor
                .DesTipoContenedor = DesTipoContenedor
                .CodMorfologiaComponente = CodMorfologiaComponente
                .Orden = Orden
                .Objectos = New List(Of Integracion.ContractoServicio.GetMorfologiaDetail.Objecto)
            End With

            Morfologia.Componentes.Add(comp)

        End If

        Return comp

    End Function

    ''' <summary>
    ''' Se tipo de medio pago já existe na lista, retorna objeto
    ''' Se não, cria, adiciona a lista e retorna o objeto criado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  21/12/2010  criado
    ''' </history>
    Private Shared Function ObtenerObj(ByRef Componente As Integracion.ContractoServicio.GetMorfologiaDetail.Componente,
                                       DesDivisa As String, OidComponenteObjeto As String, OidDivisa As String,
                                       CodIsoDivisa As String, OrdenTipoMedPago As Integer, OrdenDivisa As Integer) As Integracion.ContractoServicio.GetMorfologiaDetail.Objecto

        Dim obj As Integracion.ContractoServicio.GetMorfologiaDetail.Objecto

        obj = (From c In Componente.Objectos
               Where c.OidComponenteObjeto = OidComponenteObjeto).FirstOrDefault()

        If obj Is Nothing Then

            obj = New Integracion.ContractoServicio.GetMorfologiaDetail.Objecto

            With obj
                .DesDivisa = DesDivisa
                .OidComponenteObjeto = OidComponenteObjeto
                .CodIsoDivisa = CodIsoDivisa
                .OidDivisa = OidDivisa
                .NecOrdenTipoMedPago = OrdenTipoMedPago
                .NecOrdenDivisa = OrdenDivisa
                .TiposMedioPago = New List(Of Integracion.ContractoServicio.GetMorfologiaDetail.TipoMedioPago)
            End With

            Componente.Objectos.Add(obj)

        End If

        Return obj

    End Function

    Private Shared Function CriarDenominacion(CodDenominacion As String, DesDenominacion As String, NumValor As Decimal,
                                         BolBillete As Boolean) As Integracion.ContractoServicio.GetMorfologiaDetail.Denominacion

        Dim den As New Integracion.ContractoServicio.GetMorfologiaDetail.Denominacion

        With den
            .BolBillete = BolBillete
            .CodDenominacion = CodDenominacion
            .DesDenominacion = DesDenominacion
            .NumValor = NumValor
        End With

        Return den

    End Function

    Private Shared Function CriarMorfologia(BolVigente As Boolean, CodMorfologia As String, DesMorfologia As String,
                                            FyhActualizacion As DateTime, NecModalidadRecogida As Integer) As Integracion.ContractoServicio.GetMorfologiaDetail.Morfologia

        Dim morfologia As New Integracion.ContractoServicio.GetMorfologiaDetail.Morfologia

        ' cria morfologia
        With morfologia
            .BolVigente = BolVigente
            .CodMorfologia = CodMorfologia
            .DesMorfologia = DesMorfologia
            .FyhActualizacion = FyhActualizacion
            .NecModalidadRecogida = NecModalidadRecogida
            .Componentes = New List(Of Integracion.ContractoServicio.GetMorfologiaDetail.Componente)
        End With

        Return morfologia

    End Function

    Private Shared Function CriarMedioPago(CodMedioPago As String, DesMedioPago As String, OidMedioPago As String) As Integracion.ContractoServicio.GetMorfologiaDetail.MedioPago

        ' cria medio pago
        Dim mp As New Integracion.ContractoServicio.GetMorfologiaDetail.MedioPago

        With mp
            .CodMedioPago = CodMedioPago
            .DesMedioPago = DesMedioPago
            .OidMedioPago = OidMedioPago
            .Terminos = New List(Of Integracion.ContractoServicio.GetMorfologiaDetail.TerminoMedioPago)
        End With

        Return mp

    End Function

    Private Shared Function CriarTerminoMP(BolEsObligatorio As Boolean, BolVigente As Boolean, CodTermino As String, DesTermino As String,
                                           OidTermino As String) As Integracion.ContractoServicio.GetMorfologiaDetail.TerminoMedioPago

        Dim termino As New Integracion.ContractoServicio.GetMorfologiaDetail.TerminoMedioPago

        With termino
            .BolEsObligatorio = BolEsObligatorio
            .BolVigente = BolVigente
            .CodTermino = CodTermino
            .DesTermino = DesTermino
            .OidTermino = OidTermino
        End With

        Return termino

    End Function

    ''' <summary>
    ''' Se tipo de medio pago já existe na lista, retorna objeto
    ''' Se não, cria, adiciona a lista e retorna o objeto criado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  21/12/2010  criado
    ''' </history>
    Private Shared Function ObtenerTipoMedioPago(ByRef Objetos As Integracion.ContractoServicio.GetMorfologiaDetail.Objecto,
                                                 CodTipoMedioPago As String) As Integracion.ContractoServicio.GetMorfologiaDetail.TipoMedioPago

        Dim tipoMP As Integracion.ContractoServicio.GetMorfologiaDetail.TipoMedioPago

        tipoMP = (From c In Objetos.TiposMedioPago
                  Where c.CodTipoMedioPago = CodTipoMedioPago).FirstOrDefault()

        If tipoMP Is Nothing Then

            tipoMP = New Integracion.ContractoServicio.GetMorfologiaDetail.TipoMedioPago

            With tipoMP
                .CodTipoMedioPago = CodTipoMedioPago
                .DesTipoMedioPago = ObtenerDesTipoMedioPago(CodTipoMedioPago)
                .MediosPago = New List(Of Integracion.ContractoServicio.GetMorfologiaDetail.MedioPago)
            End With

            Objetos.TiposMedioPago.Add(tipoMP)

        End If

        Return tipoMP

    End Function

    ''' <summary>
    ''' Configura bolEfectivo dos objetos
    ''' </summary>
    ''' <param name="Morfologia"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  21/12/2010  criado
    ''' </history>
    Private Shared Sub ConfigurarObjetos(ByRef Morfologia As Integracion.ContractoServicio.GetMorfologiaDetail.Morfologia)

        For Each comp In Morfologia.Componentes

            For Each obj In comp.Objectos

                If (obj.TiposMedioPago Is Nothing OrElse obj.TiposMedioPago.Count = 0) AndAlso
                   (obj.Denominacion Is Nothing OrElse
                   (obj.Denominacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(obj.Denominacion.CodDenominacion))) Then

                    obj.BolEfectivo = True

                End If

            Next

        Next

    End Sub

    Private Shared Function ObtenerDesTipoMedioPago(CodTipoMedioPago As String) As String

        Select Case CodTipoMedioPago
            Case ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_CHEQUE : Return Traduzir("022_tipopg_cheque")
            Case ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_OTROSVALORES : Return Traduzir("022_tipopq_otrosvalores")
            Case ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_TICKET : Return Traduzir("022_tipopg_ticket")
            Case ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_TARJETA : Return Traduzir("022_tipopq_tarjeta")
            Case Else : Return String.Empty
        End Select

    End Function

    Private Function ObtenerProcesosPorDelegacion(Peticion As GetProcesosPorDelegacion.Peticion) As GetProcesosPorDelegacion.ProcesoColeccion

        Dim objGetProcesoPorDelegacion As New AccesoDatos.Proceso()
        Dim objProcesos As GetProcesosPorDelegacion.ProcesoColeccion = Nothing

        ' verifica se a delegação foi informada
        If Not String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then

            ' preenche coleção de procesos
            objProcesos = AccesoDatos.Proceso.GetProcesosPorDelegacion(Peticion)

            If objProcesos Is Nothing Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("025_msg_proceso_no_encontrados"))

            End If

        Else

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("025_msg_delegacion"))

        End If

        Return objProcesos

    End Function

    Private Sub ValidarPeticon(Peticion As Integracion.ContractoServicio.GetATMsSimplificado.Peticion)
        If Peticion Is Nothing OrElse String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                       String.Format(Traduzir("gen_srv_msg_atributo"), "CodigoDelegacion"))
        End If
    End Sub

    Private Sub ValidarPeticon(Peticion As Integracion.ContractoServicio.GetATMsSimplificadoV2.Peticion)
        If Peticion Is Nothing OrElse String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                       String.Format(Traduzir("gen_srv_msg_atributo"), "CodigoDelegacion"))
        End If
    End Sub

#Region "Carga Previa Eletronica"

    ''' <summary>
    '''Função que faz a busca das configurações de carga previa.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Adans.Klevanskis] 26/03/2013 Criado
    ''' </history>   
    Public Function getConfiguracionesCP(Peticion As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Peticion) As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Respuesta Implements GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.getConfiguracionesCP
        ' criar objeto respuesta
        Dim objRespuesta As New Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Respuesta

        Try

            ValidaPeticion(Peticion)

            objRespuesta.Configuraciones = AccesoDatos.CargaPreviaEletronica.Configuracion.getConfiguraciones(Peticion)

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    Private Shared Sub ValidaPeticion(peticion As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Peticion)

        If String.IsNullOrEmpty(peticion.CodigoDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("036_msg_cod_Delegacion"))
        End If

        If String.IsNullOrEmpty(peticion.CodigoCliente) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("036_msg_cod_Cliente_Configuracao"))
        End If

    End Sub

    Private Shared Sub ValidaPeticion(controle As String, chave As String)

        If String.IsNullOrEmpty(controle) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(chave))
        End If

    End Sub

    Public Function getConfiguracionCP(Peticion As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Peticion) As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Respuesta Implements GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.getConfiguracionCP
        Dim objRespuesta As New Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Respuesta

        Try
            If String.IsNullOrEmpty(Peticion.CodigoConfiguracion) OrElse String.IsNullOrEmpty(Peticion.IdentificadorConfiguracion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("036_msg_cod_IdentificadorConfiguracion_CodigoConfiguracion"))
            End If

            objRespuesta.Configuracion_CP = AccesoDatos.CargaPreviaEletronica.Configuracion.getConfiguracion(Peticion)

            If objRespuesta.Configuracion_CP Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("036_msg_ConfiguracionNoEncontrado"))
            End If

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    Private Sub ValidaPeticion(peticion As CargaPreviaEletronica.SetConfiguraciones.Peticion)

        If String.IsNullOrEmpty(peticion.CodigoConfiguracion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("036_msg_CodigoConfiguracion"))
        End If



    End Sub

    Public Function setConfiguracionCP(Peticion As CargaPreviaEletronica.SetConfiguraciones.Peticion) As CargaPreviaEletronica.SetConfiguraciones.Respuesta

        Dim objRespuesta As New CargaPreviaEletronica.SetConfiguraciones.Respuesta

        Try

            ValidaPeticion(Peticion)

            AccesoDatos.CargaPreviaEletronica.Configuracion.setConfiguracionCP(Peticion)

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try



        Return objRespuesta

    End Function

#End Region


#Region "[IMPORTAR PARAMETROS]"

    Private CONST_USUARIO_IMPORTACION As String = "Importacion"

    ''' <summary>
    ''' Executa a validação de dados do serviço que importa os parâmetros de conteo.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/09/2011 - Criado
    ''' </history>
    Private Function ValidarDatosImportarParametros(Peticion As ImportarParametros.Peticion) As Boolean

        If Peticion Is Nothing OrElse Peticion.DatosPuesto Is Nothing Then
            Return False
        End If


        Util.ValidarCampoObrigatorio(Peticion.DatosPuesto.CodigoAplicacion, "031_codigo_aplicacion", GetType(String), False, True)
        Util.ValidarCampoObrigatorio(Peticion.DatosPuesto.CodigoDelegacion, "031_codigo_delegacion", GetType(String), False, True)
        Util.ValidarCampoObrigatorio(Peticion.DatosPuesto.CodigoHostPuesto, "031_codigo_host_puesto", GetType(String), False, True)
        Util.ValidarCampoObrigatorio(Peticion.DatosPuesto.CodigoPuesto, "031_codigo_puesto", GetType(String), False, True)
        Util.ValidarCampoObrigatorio(Peticion.DatosPuesto.Parametros, "031_parametro", GetType(ImportarParametros.ParametroColeccion), True, True)

        For Each Parametro In Peticion.DatosPuesto.Parametros

            Util.ValidarCampoObrigatorio(Parametro.CodigoParametro, "031_codigo_parametro", GetType(String), False, True)
            Util.ValidarCampoObrigatorio(Parametro.ValorParametro, "031_valor_parametro", GetType(String), False, True)

        Next


        Return True
    End Function

    ''' <summary>
    ''' Recebe os parametros de conteo e grava na base do iac.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/09/2011 - Criado
    ''' </history>
    Private Sub EjecutarImportarParametros(Peticion As ImportarParametros.Peticion)

        'Faz a validação dos dados
        If Not ValidarDatosImportarParametros(Peticion) Then
            Exit Sub
        End If

        Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        Dim OidDelegacion As String = String.Empty
        Dim DesDelegacion As String = String.Empty
        Dim OidAplicacion As String = String.Empty
        Dim dtDatosDelegacion As DataTable = Nothing
        Dim CodNivelParametro As String = String.Empty
        Dim OidParametro As String = String.Empty
        Dim CodPais As String = String.Empty
        Dim OidPuesto As String = String.Empty
        Dim ValorParametro As String = String.Empty
        Dim OidNivelParametro As String = String.Empty

        'Se o posto ja existe na base de dados, não faz a importação dos dados.
        If AccesoDatos.Puesto.ValidarPuestoExiste(Peticion.DatosPuesto.CodigoPuesto, Peticion.DatosPuesto.CodigoHostPuesto, Peticion.DatosPuesto.CodigoAplicacion) Then
            Exit Sub
        End If

        'Recupera os oids da delegação e da planta.
        dtDatosDelegacion = AccesoDatos.Delegacion.ObterDatosDelegacion(Peticion.DatosPuesto.CodigoDelegacion)
        OidAplicacion = AccesoDatos.Aplicacion.ObterOIDAplicacion(Peticion.DatosPuesto.CodigoAplicacion)

        'Verifica se foi retornado o oid da aplicación, se não foi retorna mensagem de erro.
        If String.IsNullOrEmpty(OidAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("031_msg_aplicacion"), Peticion.DatosPuesto.CodigoAplicacion))
        End If

        'Verifica se foi retornado os oids.
        If dtDatosDelegacion IsNot Nothing AndAlso dtDatosDelegacion.Rows.Count > 0 Then

            OidDelegacion = Util.AtribuirValorObj(dtDatosDelegacion.Rows(0)("OID_DELEGACION"), GetType(String))
            DesDelegacion = Util.AtribuirValorObj(dtDatosDelegacion.Rows(0)("DES_DELEGACION"), GetType(String))
            CodPais = Util.AtribuirValorObj(dtDatosDelegacion.Rows(0)("COD_PAIS"), GetType(String))

        Else
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("031_msg_delegacion"), Peticion.DatosPuesto.CodigoDelegacion))
        End If


        'Grava o posto no BD.
        OidPuesto = AccesoDatos.Puesto.InserirPuesto(OidDelegacion, OidAplicacion, Peticion.DatosPuesto.CodigoPuesto, Peticion.DatosPuesto.CodigoHostPuesto, CONST_USUARIO_IMPORTACION, objtransacion)

        'Percorre todos os parametros.
        For Each Parametro In Peticion.DatosPuesto.Parametros

            'Insere o valor do parametro.
            InserirValorParametro(Parametro.ValorParametro, Parametro.CodigoParametro, OidAplicacion, OidDelegacion, OidPuesto, CodPais, Peticion.DatosPuesto.CodigoAplicacion, objtransacion)

        Next

        Dim ParametrosTipoCheckBox As List(Of String) = AccesoDatos.Parametro.RecuperarTodosParametros(OidAplicacion, ContractoServicio.Enumeradores.TipoComponente.CheckBox.GetHashCode)

        If ParametrosTipoCheckBox IsNot Nothing AndAlso ParametrosTipoCheckBox.Count > 0 Then

            'Caso o parametro obrigatorio não seja informado, é informado valor default.
            For Each parm In ParametrosTipoCheckBox
                Dim parmLocal = parm
                If (From p In Peticion.DatosPuesto.Parametros Where p.CodigoParametro = parmLocal).Count = 0 Then

                    'Insere o valor 0 para os parametros checkbox que não foram importados.
                    InserirValorParametro("0", parm, OidAplicacion, OidDelegacion, OidPuesto, CodPais, Peticion.DatosPuesto.CodigoAplicacion, objtransacion)

                End If

            Next

        End If


        'Realiza Transação
        objtransacion.RealizarTransacao()

    End Sub

    ''' <summary>
    ''' Insere o valor do parametro
    ''' </summary>
    ''' <param name="Valor"></param>
    ''' <param name="CodParametro"></param>
    ''' <param name="OidAplicacion"></param>
    ''' <param name="OidDelegacion"></param>
    ''' <param name="OidPuesto"></param>
    ''' <param name="CodPais"></param>
    ''' <param name="CodAplicacion"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 22/09/2011 - Criado
    ''' </history>
    Private Sub InserirValorParametro(Valor As String, CodParametro As String, OidAplicacion As String, OidDelegacion As String, OidPuesto As String,
                                                                     CodPais As String, CodAplicacion As String, ByRef objTransacion As Transacao)

        Dim objOIdYNivelParametro As Dictionary(Of String, String)
        Dim ValorParametro As String = String.Empty

        'Recupera o oid e o nivel do parametro.
        objOIdYNivelParametro = AccesoDatos.Parametro.RecuperarOidYNivelParametro(CodParametro, OidAplicacion)

        If objOIdYNivelParametro Is Nothing OrElse objOIdYNivelParametro.Count = 0 Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("031_msg_parametro"), CodParametro))
        End If

        'Atribui valor as variaveis oid e nivel do parametro.
        Dim OidParametro As String = objOIdYNivelParametro.First.Key
        Dim CodNivelParametro As String = objOIdYNivelParametro.First.Value

        Dim OidNivelParametro As String = If(CodNivelParametro = ContractoServicio.Parametro.TipoNivel.Pais, CodPais, If(CodNivelParametro = ContractoServicio.Parametro.TipoNivel.Delegacion, OidDelegacion, OidPuesto))

        'Verifica se o nivel do parametro não é do nivel posto, se não for e o valor existir, não faz a inserção.
        If Not CodNivelParametro.Equals(ContractoServicio.Parametro.TipoNivel.Puesto) AndAlso
            AccesoDatos.ParametroValue.ValidarValorExiste(OidParametro, CodAplicacion, OidNivelParametro) Then
            Exit Sub
        End If

        If Not String.IsNullOrEmpty(Valor) Then
            ValorParametro = If(Valor = "true", 1, If(Valor = "false", 0, Valor))
        End If

        'Insere o valor dos parametros no bd
        AccesoDatos.ParametroValue.InserirValorParametro(OidParametro, OidNivelParametro, ValorParametro, CONST_USUARIO_IMPORTACION, objTransacion)

    End Sub

#End Region

#Region "[RECUPERAR PARAMETROS]"

    ''' <summary>
    ''' Executa a validação de dados do serviço que recupera os parâmetros de conteo.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarPeticionRecuperarParametros(Peticion As RecuperarParametros.Peticion) As Boolean

        If Peticion Is Nothing Then
            Return False
        End If

        Util.ValidarCampoObrigatorio(Peticion.CodigoPuesto, "032_codigo_puesto", GetType(String), False, True)
        Util.ValidarCampoObrigatorio(Peticion.CodigoHostPuesto, "032_codigo_host_puesto", GetType(String), False, True)
        Util.ValidarCampoObrigatorio(Peticion.CodigoAplicacion, "032_codigo_aplicacion", GetType(String), False, True)

        Return True

    End Function

    ''' <summary>
    ''' Valida os datos retornados pela consulta
    ''' </summary>
    ''' <param name="datosPuesto"></param>
    ''' <remarks></remarks>
    Private Sub ValidarDatosRecuperarParametros(datosPuesto As Integracion.ContractoServicio.RecuperarParametros.DatosPuesto)

        ' percorre os parametros
        For Each p In datosPuesto.Parametros
            Dim pLocal = p
            ' verifica se é lista de valores
            ' sendo uma lista, deverá ter seus valores carregados
            If p.EsObligatorio AndAlso p.ListaValores AndAlso (p.ValoresPosibles Is Nothing OrElse p.ValoresPosibles.Count = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("032_msg_lista_parametro_no_configurado"), p.CodigoParametro))
            End If
            ' verifica se é obrigatório
            If p.EsObligatorio AndAlso String.IsNullOrEmpty(p.ValorParametro) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("032_msg_parametro_no_configurado"), p.CodigoParametro))
            End If
            ' verifica se é obrigatório, se é uma lista de valores e se o valor que está selecionado está na coleção de valores válidos
            If p.EsObligatorio AndAlso p.ListaValores AndAlso p.ValoresPosibles.FirstOrDefault(Function(vp) vp.CodigoValor = pLocal.ValorParametro) Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("032_msg_parametro_no_seleccionado"), p.CodigoParametro))
            End If
            ' verifica se tem valores na lista e se está marcado como lista de valores
            If p.ValoresPosibles IsNot Nothing AndAlso p.ValoresPosibles.Count > 0 AndAlso Not p.ListaValores Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("032_msg_parametro_no_marcado_como_lista"), p.CodigoParametro))
            End If
        Next

    End Sub

    ''' <summary>
    ''' Recupera os Parametros do Posto
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    Private Function EjecutarRecuperarParametros(Peticion As RecuperarParametros.Peticion) As Integracion.ContractoServicio.RecuperarParametros.DatosPuesto

        Dim objDatosPuesto As Integracion.ContractoServicio.RecuperarParametros.DatosPuesto = Nothing

        ' faz a validação dos dados
        If ValidarPeticionRecuperarParametros(Peticion) Then

            'Preenche coleção de Procesos
            objDatosPuesto = AccesoDatos.Parametro.RecuperarParametros(Peticion.CodigoPuesto, Peticion.CodigoHostPuesto, Peticion.CodigoAplicacion, Peticion.CodigoDelegacion)

            ' verifica se ouve retorno na consulta
            If objDatosPuesto IsNot Nothing Then

                ' valida os dados retornados
                ValidarDatosRecuperarParametros(objDatosPuesto)

            End If

        End If

        Return objDatosPuesto

    End Function

#End Region

#Region "[RECUPERAR PARAMETROS DELEGAÇÃO PAIS]"

    ''' <summary>
    '''  Recupera os parametros a nivel de Delegação e pais
    ''' </summary>
    ''' <param name="ObjPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/10/2011 - Criado
    ''' </history>
    Private Function EjecutarGetParametrosDelegacionPais(ObjPeticion As GetParametrosDelegacionPais.Peticion) As GetParametrosDelegacionPais.ParametroRespuestaColeccion

        Dim objParametros As GetParametrosDelegacionPais.ParametroRespuestaColeccion = Nothing

        'Faz a validação dos dos dados
        ValidarDatosRecuperarParametrosDelegacionPais(ObjPeticion)

        'Recupera o oid da delegação e o codigo do pais
        Dim objOidDelegacionCodPais As Dictionary(Of String, String) = AccesoDatos.Delegacion.RecuperarOidDelegacionCodPais(ObjPeticion.CodigoDelegacion)

        If objOidDelegacionCodPais IsNot Nothing AndAlso objOidDelegacionCodPais.Count > 0 Then

            'Recupera os parametros a nivel de delegação e pais.
            objParametros = AccesoDatos.ParametroValue.RecuperarParametrosDelegacionPais(objOidDelegacionCodPais.FirstOrDefault.Key, objOidDelegacionCodPais.FirstOrDefault.Value, ObjPeticion.CodigoAplicacion, _
                                                                                                                                                                        ObjPeticion.Parametros)
            ' Se existem parâmetros e não foi busca um parâmetro em especifico
            If objParametros IsNot Nothing AndAlso (ObjPeticion.Parametros Is Nothing OrElse ObjPeticion.Parametros.Count = 0) Then

                ' Verifica se deve validar os parâmetros
                If ObjPeticion.ValidarParametros Then
                    ' Valida os parâmetros
                    Me.ValidarDatosRecuperarParametrosDelegacionPais(objParametros)
                Else
                    ' Remove os parâmetros sem valores
                    objParametros.RemoveAll(Function(r) String.IsNullOrEmpty(r.ValorParametro))
                End If

            End If

        End If

        Return objParametros

    End Function

    ''' <summary>
    ''' Faz a validação de dados
    ''' </summary>
    ''' <param name="ObjPeticon"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/10/2011 - Criado
    ''' </history>
    Private Sub ValidarDatosRecuperarParametrosDelegacionPais(ObjPeticon As GetParametrosDelegacionPais.Peticion)

        Util.ValidarCampoObrigatorio(ObjPeticon.CodigoDelegacion, "033_codigo_delegacion", GetType(String), False, True)
        Util.ValidarCampoObrigatorio(ObjPeticon.CodigoAplicacion, "033_codigo_aplicacion", GetType(String), False, True)

    End Sub

    ''' <summary>
    ''' Faz a validação dos Pametros
    ''' </summary>
    ''' <param name="parametros"></param>
    ''' <remarks></remarks>
    Private Sub ValidarDatosRecuperarParametrosDelegacionPais(parametros As GetParametrosDelegacionPais.ParametroRespuestaColeccion)

        ' se existem parâmetros
        If parametros IsNot Nothing Then

            ' percorre os parametros
            For Each p In parametros

                ' verifica se é obrigatório
                If p.EsObligatorio AndAlso String.IsNullOrEmpty(p.ValorParametro) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("032_msg_parametro_no_configurado"), p.CodigoParametro))
                End If
            Next

        End If

    End Sub

#End Region

#Region "[RECUPERAR VALORES POSIBLE POR NIVEL]"

    ''' <summary>
    '''  Recupera os parametros por nivel cliente subcliente e ponto de serviço
    ''' </summary>
    ''' <param name="ObjPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos] 06/12/2011 - Criado
    ''' </history>
    Private Function EjecutarRecuperaValoresPosiblesPorNivel(ObjPeticion As RecuperaValoresPosiblesPorNivel.Peticion) As RecuperaValoresPosiblesPorNivel.TerminoRespostaColeccion

        Dim objTerminos As RecuperaValoresPosiblesPorNivel.TerminoResposta = Nothing

        'Faz a validação dos dos dados
        ValidarRecuperaValoresPosiblesPorNivel(ObjPeticion)

        'Recupera e retorna os términos e os respectivos valores possíveis.
        Return AccesoDatos.ValorPosible.RecuperaValoresPosiblesPorNivel(ObjPeticion)

    End Function

#End Region

#Region "[GET PUESTOS]"

    Public Function GetPuestos(Peticion As GetPuestos.Peticion) As GetPuestos.Respuesta Implements IIntegracion.GetPuestos

        Dim objRespuesta As New GetPuestos.Respuesta

        Try

            ValidarPeticion(Peticion)

            Dim objPuestos As ContractoServicio.Puesto.GetPuestos.PuestoColeccion = IAC.AccesoDatos.Puesto.GetPuestos(Peticion.CodigoDelegacion, Peticion.CodigoAplicacion, Peticion.CodigoPuesto, Peticion.HostPuesto, Peticion.BolVigente, Peticion.Permisos, Peticion.BolSoloMecanizado)

            If objPuestos IsNot Nothing AndAlso objPuestos.Count > 0 Then

                objRespuesta.Puestos = New GetPuestos.PuestoColeccion

                For Each objPuesto In objPuestos

                    objRespuesta.Puestos.Add(New GetPuestos.Puesto With {
                                             .Aplicacion = If(objPuesto.Aplicacion IsNot Nothing, New GetPuestos.Aplicacion With {.CodigoAplicacion = objPuesto.Aplicacion.CodigoAplicacion,
                                                                                                                                  .DescripcionAplicacion = objPuesto.Aplicacion.DescripcionAplicacion,
                                                                                                                                  .PermissoAplicacion = objPuesto.Aplicacion.CodigoPermiso}, Nothing),
                                             .CodigoDelegacion = objPuesto.CodigoDelegacion,
                                             .CodigoHostPuesto = objPuesto.CodigoHostPuesto,
                                             .CodigoPlanta = objPuesto.CodigoPlanta,
                                             .CodigoPuesto = objPuesto.CodigoPuesto,
                                             .DescripcionDelegacion = objPuesto.DescripcionDelegacion,
                                             .DescripcionPlanta = objPuesto.DescripcionPlanta,
                                             .PuestoVigente = objPuesto.PuestoVigente})
                Next

            End If

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    Private Sub ValidarPeticion(Peticion As GetPuestos.Peticion)
        If String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_delegacion"))
        End If
    End Sub

#End Region

#Region "GETVALORES"

    Public Function GetValores(Peticion As TiposYValores.GetValores.Peticion) As TiposYValores.GetValores.Respuesta Implements IIntegracion.GetValores

        Dim objRespuesta As New TiposYValores.GetValores.Respuesta
        Try
            objRespuesta.Valores = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.ListaTipoValores.GetValores(Peticion.CodTipo, Peticion.CodValor, Peticion.DesValor, Peticion.BolActivo)

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta
    End Function

#End Region

#Region "SETVALOR"

    Public Function SetValor(Peticion As TiposYValores.SetValor.Peticion) As TiposYValores.SetValor.Respuesta Implements IIntegracion.SetValor

        Dim objRespuesta As New TiposYValores.SetValor.Respuesta
        Try
            ValidarPeticion(Peticion)

            objRespuesta.CodValor = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.ListaTipoValores.SetValor(Peticion)

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta
    End Function

    Private Sub ValidarPeticion(Peticion As TiposYValores.SetValor.Peticion)

        Dim parametrosVacios As String = String.Empty
        Dim msg As String = String.Empty

        If String.IsNullOrEmpty(Peticion.Valor.CodTipo) Then
            parametrosVacios = Traduzir("getvalor_parametro_codTipo")
        End If

        If String.IsNullOrEmpty(Peticion.Valor.CodValor) Then
            parametrosVacios += If(String.IsNullOrEmpty(parametrosVacios), Traduzir("getvalor_parametro_codValor"), ", " & Traduzir("getvalor_parametro_codValor"))
        End If

        If String.IsNullOrEmpty(Peticion.Valor.DesValor) Then
            parametrosVacios += If(String.IsNullOrEmpty(parametrosVacios), Traduzir("getvalor_parametro_desValor"), ", " & Traduzir("getvalor_parametro_desValor"))
        End If

        If Peticion.Valor.BolActivo Is Nothing Then
            parametrosVacios += If(String.IsNullOrEmpty(parametrosVacios), Traduzir("getvalor_parametro_activo"), ", " & Traduzir("getvalor_parametro_activo"))
        End If

        If Peticion.Valor.BolDefecto Is Nothing Then
            parametrosVacios += If(String.IsNullOrEmpty(parametrosVacios), "BolDefecto", ", " & "BolDefecto")
        End If

        If Peticion.Valor.GmtModificacion = DateTime.MinValue Then
            parametrosVacios += If(String.IsNullOrEmpty(parametrosVacios), Traduzir("getvalor_parametro_gmtModificacion"), ", " & Traduzir("getvalor_parametro_gmtModificacion"))
        End If

        If String.IsNullOrEmpty(Peticion.Valor.DesUsuarioModificacion) Then
            parametrosVacios += If(String.IsNullOrEmpty(parametrosVacios), Traduzir("getvalor_parametro_desUsuarioModificacion"), ", " & Traduzir("getvalor_parametro_desUsuarioModificacion"))
        End If

        If Not String.IsNullOrEmpty(parametrosVacios) Then
            msg = String.Format(Traduzir("getvalor_parametros_vazio"), parametrosVacios)
        End If

        If Not String.IsNullOrEmpty(Peticion.Valor.CodTipo) Then
            Dim tipoValor As TiposYValores.Tipo = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.ListaTipoValores.GetTipoValorPorCodigo(Peticion.Valor.CodTipo)
            If tipoValor Is Nothing Then
                msg += Traduzir("getvalor_parametro_codigoTipoInvalido")
            End If
        End If

        If Not String.IsNullOrEmpty(msg) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, msg)
        End If

    End Sub
#End Region

#Region "SETMODULO"

    Public Function SetModulo(Peticion As Modulo.SetModulo.Peticion) As Modulo.SetModulo.Respuesta Implements IIntegracion.SetModulo

        Dim objRespuesta As New Modulo.SetModulo.Respuesta
        Try
            ValidarPeticion(Peticion)

            objRespuesta.CodModulo = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Modulo.SetModulo(Peticion)

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta
    End Function

    Private Sub ValidarPeticion(Peticion As Modulo.SetModulo.Peticion)

        Dim msg As String = String.Empty

        If String.IsNullOrEmpty(Peticion.Modulo.CodEmbalaje) Then
            msg = String.Format(Traduzir("setTipoModulo_parametro_obligatorio"), "CodEmbalaje") & vbNewLine
        End If

        If String.IsNullOrEmpty(Peticion.Modulo.CodModulo) Then
            msg += String.Format(Traduzir("setTipoModulo_parametro_obligatorio"), "CodTipoModulo") & vbNewLine
        End If

        If String.IsNullOrEmpty(Peticion.Modulo.DesModulo) Then
            msg += String.Format(Traduzir("setTipoModulo_parametro_obligatorio"), "DesTipoModulo") & vbNewLine
        End If

        If Peticion.Modulo.BolActivo Is Nothing Then
            msg += String.Format(Traduzir("setTipoModulo_parametro_obligatorio"), "BolActivo") & vbNewLine
        End If

        If Peticion.Modulo.GmtModificacion = DateTime.MinValue Then
            msg += String.Format(Traduzir("setTipoModulo_parametro_obligatorio"), "GmtModificacion") & vbNewLine
        End If

        If String.IsNullOrEmpty(Peticion.Modulo.DesUsuarioModificacion) Then
            msg += String.Format(Traduzir("setTipoModulo_parametro_obligatorio"), "DesUsuarioModificacion") & vbNewLine
        End If

        If Peticion.Modulo.ModulosDesglose IsNot Nothing AndAlso Peticion.Modulo.ModulosDesglose.Count > 0 Then
            msg += ValidarPeticion(Peticion.Modulo.ModulosDesglose)
        End If

        'Valida se o cliente existe
        If Not String.IsNullOrEmpty(Peticion.Modulo.CodCliente) Then
            Dim cliente As String = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Cliente.BuscarOidCliente(Peticion.Modulo.CodCliente)
            If String.IsNullOrEmpty(cliente) Then
                msg += Traduzir("setTipoModulo_cliente_no_encontrado") & vbNewLine
            End If
        End If

        'Valida se o codValor(codEmbalaje) existe
        If Not String.IsNullOrEmpty(Peticion.Modulo.CodEmbalaje) Then
            Dim codigosValores = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.ListaTipoValores.GetValores(Prosegur.Genesis.Comon.Constantes.CODIGO_LISTA_TIPO_FORMATO, Peticion.Modulo.CodEmbalaje, Nothing, Nothing)
            If codigosValores Is Nothing OrElse codigosValores.Count = 0 Then
                msg += Traduzir("setTipoModulo_codEmbalaje_no_encontrado") & vbNewLine
            End If
        End If

        If Not String.IsNullOrEmpty(msg) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, msg)
        End If

    End Sub

    Private Function ValidarPeticion(tiposModulosDesglose As List(Of Modulo.ModuloDesglose)) As String

        Dim msg As String = String.Empty

        If tiposModulosDesglose IsNot Nothing AndAlso tiposModulosDesglose.Count > 0 Then

            Dim hayError As Boolean = False

            For Each t In tiposModulosDesglose

                If String.IsNullOrEmpty(t.CodDivisa) Then
                    msg = String.Format(Traduzir("setTipoModulo_parametro_obligatorio"), "TipoModulosDesglose.CodDivisa") & vbNewLine
                    hayError = True
                End If

                If String.IsNullOrEmpty(t.CodDenominacion) Then
                    msg = String.Format(Traduzir("setTipoModulo_parametro_obligatorio"), "TipoModulosDesglose.CodDenominacion") & vbNewLine
                    hayError = True
                End If

                If String.IsNullOrEmpty(t.NelUnidades) Then
                    msg = String.Format(Traduzir("setTipoModulo_parametro_obligatorio"), "TipoModulosDesglose.NelUnidades") & vbNewLine
                    hayError = True
                End If

                If t.GmtModificacion = DateTime.MinValue Then
                    msg += String.Format(Traduzir("setTipoModulo_parametro_obligatorio"), "TipoModulosDesglose.GmtModificacion") & vbNewLine
                    hayError = True
                End If

                If String.IsNullOrEmpty(t.DesUsuarioModificacion) Then
                    msg += String.Format(Traduzir("setTipoModulo_parametro_obligatorio"), "TipoModulosDesglose.DesUsuarioModificacion") & vbNewLine
                    hayError = True
                End If

                'Valida se a divisa existe
                Dim divisa = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Divisa.ObterOidDivisa(t.CodDivisa)
                If String.IsNullOrEmpty(divisa) Then
                    msg += Traduzir("setTipoModulo_divisa_no_encontrado") & vbNewLine
                    hayError = True
                Else
                    'Valida se a denominação existe
                    Dim denominacion = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Denominacion.ObterOidDenominacion(divisa, t.CodDenominacion)
                    If String.IsNullOrEmpty(denominacion) Then
                        msg += Traduzir("setTipoModulo_denominacion_no_encontrado") & vbNewLine
                        hayError = True
                    End If

                End If

                If hayError Then
                    Exit For
                End If

            Next
        End If

        Return msg

    End Function

#End Region

#Region "GETMODULO"

    Public Function GetModulo(Peticion As Modulo.GetModulo.Peticion) As Modulo.GetModulo.Respuesta Implements IIntegracion.GetModulo

        Dim objRespuesta As New Modulo.GetModulo.Respuesta
        Try

            objRespuesta = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Modulo.GetModulo(Peticion)

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta
    End Function

    Public Function GetModuloCliente(Peticion As Modulo.GetModuloCliente.Peticion) As Modulo.GetModuloCliente.Respuesta Implements IIntegracion.GetModuloCliente

        Dim objRespuesta As New Modulo.GetModuloCliente.Respuesta
        Try

            objRespuesta = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Modulo.GetModuloCliente(Peticion)

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta
    End Function

#End Region

#Region "DIVISAS"
    Public Function obtenerParametros(peticion As Genesis.ContractoServicio.Contractos.Comon.Parametro.obtenerParametros.Peticion) As Genesis.ContractoServicio.Contractos.Comon.Parametro.obtenerParametros.Respuesta Implements IIntegracion.obtenerParametros
        Return Nothing
    End Function


#End Region

    Public Function RecuperarTodasDivisasYDenominaciones() As RecuperarTodasDivisasYDenominaciones.Respuesta Implements IIntegracion.RecuperarTodasDivisasYDenominaciones
        Dim objRespuesta As New RecuperarTodasDivisasYDenominaciones.Respuesta
        Try

            objRespuesta.Divisas = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Divisa.RecuperarTodasDivisasYDenominaciones()

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta
    End Function
End Class
