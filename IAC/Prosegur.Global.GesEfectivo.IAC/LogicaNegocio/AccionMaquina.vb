Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Maquina
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones

Public Class AccionMaquina


    Public Function GetMaquinas(objPeticion As ContractoServicio.Maquina.GetMaquina.Peticion) As ContractoServicio.Maquina.GetMaquina.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Maquina.GetMaquina.Respuesta

        Try

            If objPeticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            ' obter delegaciones
            Dim lstMaquinas = AccesoDatos.MaquinaMAE.GetMaquinas(objPeticion, objRespuesta.ParametrosPaginacion)
            objRespuesta.Maquinas = ConverteGetMaquinas(lstMaquinas)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    Public Function GetMaquinasSinPlanificacion(objPeticion As ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Peticion) As ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Respuesta

        Try

            If objPeticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            ' obter delegaciones
            objRespuesta = AccesoDatos.MaquinaMAE.GetMaquinasSinPlanificacion(objPeticion, objRespuesta.ParametrosPaginacion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    Public Function GetMaquinaDetalle(oidMaquina As String) As ContractoServicio.Maquina.GetMaquinaDetalle.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Maquina.GetMaquinaDetalle.Respuesta

        Try
            objRespuesta.Maquina = AccesoDatos.MaquinaMAE.GetMaquinaDetalle(oidMaquina)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    Private Function ConverteGetMaquinas(lstMaquinas As List(Of Comon.Clases.Maquina)) As List(Of GetMaquina.Maquina)
        Dim lstGetMaquina As New List(Of GetMaquina.Maquina)
        For Each objMaquina In lstMaquinas
            If objMaquina.PlanMaquina Is Nothing Then
                objMaquina.PlanMaquina = New List(Of Comon.Clases.PlanXmaquina)()
            End If

            If objMaquina.PlanMaquina.Count > 0 Then
                For Each planxmaquina In objMaquina.PlanMaquina
                    Dim getMaquina As New GetMaquina.Maquina

                    getMaquina.OidMaquina = objMaquina.Identificador
                    getMaquina.BolActivo = objMaquina.BolActivo
                    getMaquina.Descripcion = objMaquina.Descripcion
                    getMaquina.DesFabricante = objMaquina.Modelo.Fabricante.Descripcion
                    getMaquina.DesModelo = objMaquina.Modelo.Descripcion
                    getMaquina.DeviceID = objMaquina.Codigo

                    getMaquina.CodigoCliente = objMaquina.Cliente.Codigo
                    getMaquina.Cliente = objMaquina.Cliente.Descripcion

                    getMaquina.CodigoSubCliente = objMaquina.SubCliente.Codigo
                    getMaquina.SubCliente = objMaquina.SubCliente.Descripcion

                    getMaquina.CodigoPtoServicio = objMaquina.PtoServicio.Codigo
                    getMaquina.PtoServicio = objMaquina.PtoServicio.Descripcion

                    getMaquina.DesPlanificacion = planxmaquina.Planificacion.Descripcion
                    getMaquina.FechaVigenciaInicio = DateTime.SpecifyKind(planxmaquina.FechaHoraVigenciaInicio, DateTimeKind.Utc)
                    getMaquina.FechaVigenciaFin = DateTime.SpecifyKind(planxmaquina.FechaHoraVigenciaFin, DateTimeKind.Utc)
                    If DateTime.UtcNow > getMaquina.FechaVigenciaInicio AndAlso (DateTime.UtcNow <= getMaquina.FechaVigenciaFin OrElse getMaquina.FechaVigenciaFin = DateTime.MinValue) Then
                        getMaquina.FechaValor = True
                    End If

                    lstGetMaquina.Add(getMaquina)
                Next
            Else
                Dim getMaquina As New GetMaquina.Maquina

                getMaquina.OidMaquina = objMaquina.Identificador
                getMaquina.BolActivo = objMaquina.BolActivo
                getMaquina.Descripcion = objMaquina.Descripcion
                getMaquina.DesFabricante = objMaquina.Modelo.Fabricante.Descripcion
                getMaquina.DesModelo = objMaquina.Modelo.Descripcion
                getMaquina.DeviceID = objMaquina.Codigo
                getMaquina.CodigoCliente = objMaquina.Cliente.Codigo
                getMaquina.Cliente = objMaquina.Cliente.Descripcion

                getMaquina.CodigoSubCliente = objMaquina.SubCliente.Codigo
                getMaquina.SubCliente = objMaquina.SubCliente.Descripcion

                getMaquina.CodigoPtoServicio = objMaquina.PtoServicio.Codigo
                getMaquina.PtoServicio = objMaquina.PtoServicio.Descripcion

                lstGetMaquina.Add(getMaquina)
            End If
            
        Next
        Return lstGetMaquina
    End Function

    Public Function GetTransacaoMaquinas(oidPlanta As String) As ContractoServicio.Maquina.GetMaquinaTransacao.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Maquina.GetMaquinaTransacao.Respuesta

        Try
            ' obter delegaciones
            Dim lstMaquinas = AccesoDatos.MaquinaMAE.GetTransacaoMaquinas(oidPlanta)
            objRespuesta = lstMaquinas
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    Public Shared Function GetMaquinaPuntoServicio(oidPuntoServicio As String) As Comon.Clases.Maquina
        Return AccesoDatos.MaquinaMAE.GetMaquinaPuntoServicio(oidPuntoServicio)
    End Function
End Class