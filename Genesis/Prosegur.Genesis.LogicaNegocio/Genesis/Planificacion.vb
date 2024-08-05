Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.LogicaNegocio.Integracion
Imports System.Text
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Configuration

Namespace Genesis

    Public Class Planificacion

        Public Shared Sub GrabarPlanificacion(ByRef planificacion As Clases.Planificacion,
                                              dicDatosBancarios As Dictionary(Of String, List(Of Comon.Clases.DatoBancario)),
                                              codigoUsuario As String,
                                              ByRef fechaActual As DateTime)

            'Creamos objeto de respuesta para Generar Periodos
            Dim objRespuesta = New Prosegur.Genesis.Comon.Pantallas.Planificacion.Respuesta()

            'Inicializar objeto de Respuesta
            AccesoDatos.Util.resultado(objRespuesta.Codigo,
                           objRespuesta.Descripcion,
                           Tipo.Exito,
                           Contexto.Job,
                           Funcionalidad.GenerarPeriodos,
                           "0000", "",
                           True)
            'Logueo 
            Dim nombreRecurso As String = LogeoEntidades.Log.Movimiento.Recurso.IACBusquedaPlanificacion
            Dim pais = Genesis.Pais.ObtenerPaisPorDefault("")
            Dim codPais As String = pais.Codigo
            Dim identificadorLlamada As String = String.Empty
            Dim objJson As String = Newtonsoft.Json.JsonConvert.SerializeObject(planificacion)

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codPais, nombreRecurso, identificadorLlamada)
            If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, nombreRecurso, Comon.Util.VersionCompleta, objJson, codPais, objJson.GetHashCode)
            End If


            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Inicia llamada al método ValidarPlanificacion",
                                                      planificacion.Codigo)
            ValidarPlanificacion(planificacion)
            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Finaliza llamada al método ValidarPlanificacion",
                                                      planificacion.Codigo)

            Dim objTransaccionOracle As DataBaseHelper.Transaccion = New DataBaseHelper.Transaccion()
            'Ponemos en una cola de transaccion de Oracle.
            objTransaccionOracle.IniciarTransaccion = True

            Dim maquinasNuevas = New List(Of Clases.Maquina)

            'En caso de una edición reviso si hay máquina nuevas
            If planificacion.Identificador IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Inicia búsqueda de máquinas nuevas en la planificación",
                                                      planificacion.Codigo)
                maquinasNuevas = ObtenerMaquinasNuevasAgregadgasEnPlanificacion(planificacion, codigoUsuario)

                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Finaliza búsqueda de máquinas nuevas en la planificación. Cantidad de máquinas nuevas: {maquinasNuevas.Count()}",
                                                      planificacion.Codigo)
            End If

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Inicia llamada al método AccesoDatos.Genesis.Planificacion.GrabarPlanificacion",
                                                      planificacion.Codigo)
            AccesoDatos.Genesis.Planificacion.GrabarPlanificacion(identificadorLlamada, planificacion, codigoUsuario, objTransaccionOracle)

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Finaliza llamada al método AccesoDatos.Genesis.Planificacion.GrabarPlanificacion",
                                                      planificacion.Codigo)

            Try

                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Inicia el commit de la transacción",
                                                      planificacion.Codigo)

                'Realiza commit tanto de actualizar peridoos por cambios de hora y de grabar planificación
                DataBaseHelper.AccesoDB.TransactionCommit(objTransaccionOracle)

                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Finaliza el commit de la transacción",
                                                      planificacion.Codigo)

                'CREAR PERIODOS
                If planificacion.BolCambioHorario OrElse planificacion.BolCambioHorarioProgramacion OrElse planificacion.BolCambioTipoPlaniFVC Then
                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Inicia creación de períodos por cambio de horario/programación de la planificación",
                                                      planificacion.Codigo)

                    For Each objMaquina In planificacion.Maquinas
                        AccesoDatos.GenesisSaldos.Periodos.generarPeriodos(objMaquina.Identificador, codigoUsuario, identificadorLlamada)
                        AccesoDatos.GenesisSaldos.Periodos.RelacionarDocumentosMAE(objMaquina.Codigo, fechaActual, identificadorLlamada)
                    Next
                ElseIf maquinasNuevas IsNot Nothing AndAlso maquinasNuevas.Count > 0 Then
                    'Por cada maquina nueva, creamos los periodos para la acreditación
                    'y relacionamos los documentos a los periodos
                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Inicia creación de períodos para máquinas nuevas de la planificación",
                                                      planificacion.Codigo)

                    For Each objMaquina As Clases.Maquina In maquinasNuevas
                        AccesoDatos.GenesisSaldos.Periodos.generarPeriodos(objMaquina.Identificador, codigoUsuario, identificadorLlamada)
                        AccesoDatos.GenesisSaldos.Periodos.RelacionarDocumentosMAE(objMaquina.Codigo, fechaActual, identificadorLlamada)
                    Next
                End If

            Catch ex As Exception
                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Se produjo una excepción, Message:{ex.Message}. {vbCrLf}StackTrace: {ex.StackTrace}",
                                                      planificacion.Codigo)

                DataBaseHelper.AccesoDB.TransactionRollback(objTransaccionOracle)
                AccesoDatos.Util.resultado(objRespuesta.Codigo,
                                objRespuesta.Descripcion,
                                Tipo.Error_Aplicacion,
                                Contexto.IAC,
                                Funcionalidad.GenerarPeriodos,
                                "0000", "",
                                True)

            End Try



            If dicDatosBancarios IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Inicia configuración de datos bancarios de la planificación",
                                                      planificacion.Codigo)

                For Each objDatoBancario In dicDatosBancarios

                    Dim DatosBancarios = objDatoBancario.Value

                    Dim objAccionDatoBancario As New LogicaNegocio.DatoBancario


                    Dim objPeticionDatoBancario As New ContractoServicio.DatoBancario.SetDatosBancarios.Peticion
                    With objPeticionDatoBancario
                        .DatosBancarios = DatosBancarios
                        .IdentificadorCliente = DatosBancarios(0).Cliente.Identificador
                        If DatosBancarios(0).SubCliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(DatosBancarios(0).SubCliente.Identificador) Then
                            .IdentificadorSubCliente = DatosBancarios(0).SubCliente.Identificador
                        End If

                        If DatosBancarios(0).PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(DatosBancarios(0).PuntoServicio.Identificador) Then
                            .IdentificadorPuntoServicio = DatosBancarios(0).PuntoServicio.Identificador
                        End If


                        .CodigoUsuario = codigoUsuario
                    End With

                    Dim objRespuestaDatoBancario = objAccionDatoBancario.SetDatosBancarios(objPeticionDatoBancario)

                    If objRespuestaDatoBancario.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                        Dim excepcion As New Exception(objRespuestaDatoBancario.MensajeError)
                        Throw excepcion
                    End If

                Next

                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.Planificacion",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Finaliza configuración de datos bancarios de la planificación",
                                                      planificacion.Codigo)
            End If


            If identificadorLlamada IsNot Nothing Then
                Dim respuestaJson = Newtonsoft.Json.JsonConvert.SerializeObject(objRespuesta)
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, respuestaJson, objRespuesta.Codigo, objRespuesta.Descripcion, respuestaJson.GetHashCode)
            End If
        End Sub

        ''' <summary>
        ''' ObtenerMaquinasNuevasAgregadgasEnPlanificacion se encarga de obtener las maquinas que se agregaron en la planificacion.
        ''' Comparando las maquinas anteriores vs la colección de maquinas actuales.
        ''' </summary>
        ''' <param name="planificacion"></param>
        ''' <param name="codigoUsuario"></param>
        ''' <returns>Colección de máquinas nuevas en la planificación</returns>
        Private Shared Function ObtenerMaquinasNuevasAgregadgasEnPlanificacion(planificacion As Clases.Planificacion, codigoUsuario As String) As List(Of Clases.Maquina)
            Dim maquinasActuales As New List(Of Clases.Maquina)
            Dim maquinasNuevas As New List(Of Clases.Maquina)
            Dim maquinasAnteriores As New List(Of Clases.Maquina)

            Try
                maquinasActuales = planificacion.Maquinas
                maquinasAnteriores = RecuperarPlanificacionDetalle(planificacion.Identificador, codigoUsuario, Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor()).Maquinas

                If maquinasAnteriores Is Nothing Then
                    maquinasNuevas = maquinasActuales
                Else
                    If maquinasActuales IsNot Nothing Then
                        maquinasNuevas.AddRange(maquinasActuales.Where(Function(x) Not maquinasAnteriores.Exists(Function(a) a.Identificador = x.Identificador)))
                    End If

                    If maquinasNuevas IsNot Nothing AndAlso maquinasNuevas.Count > 0 Then
                        maquinasNuevas = AccesoDatos.Genesis.Maquina.GetMaquinasDetalle(maquinasNuevas)
                    End If

                End If
            Catch ex As Exception
                'No tratamos excepciones
            End Try

            Return maquinasNuevas
        End Function

        Public Shared Sub ValidarPlanificacion(ByRef planificacion As Clases.Planificacion)

            If planificacion IsNot Nothing Then
                If AccesoDatos.Genesis.Planificacion.VerificarPlanificacionExistente(planificacion) Then
                    Throw New Excepcion.NegocioExcepcion(Traduzir("MSG_INFO_PLANIFICACION_EXISTENTE"))
                End If
            End If

        End Sub



        Public Shared Function VerificaMaquinaVinculada(oidsMaquinas As List(Of String)) As Boolean
            Try

                Return AccesoDatos.Genesis.Planificacion.VerificarMaquinasVinculadas(oidsMaquinas)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function VerificaPeriodosVinculados(lstMaquinas As List(Of Clases.Maquina)) As Boolean
            Try
                Return AccesoDatos.Genesis.Planificacion.VerificarPeriodosVinculados(lstMaquinas)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function RecuperarPlanificacionDetalle(oidPlanificacion As String, codigoUsuario As String, CodigoIdentificadorAjeno As String) As Clases.Planificacion
            Try
                Dim planificacion = AccesoDatos.Genesis.Planificacion.RecuperarPlanificacionDetalle(oidPlanificacion, codigoUsuario, CodigoIdentificadorAjeno)
                ProcessarProgramaciones(planificacion)
                Return planificacion
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function RecuperarVigenciaPlanificacionMAE(IdentificadorMaquina As String) As System.Data.DataTable
            Try
                Return AccesoDatos.Genesis.Planificacion.RecuperarVigenciaPlanificacionMAE(IdentificadorMaquina)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Shared Function GetHorarioProgramacion(objProgramacion As Clases.PlanXProgramacion) As String
            If objProgramacion IsNot Nothing Then
                Return objProgramacion.FechaHoraFin.ToString("HH:mm:ss")
            Else
                Return String.Empty
            End If
        End Function
        ''' <summary>
        ''' Processa programações para vizualização na tela
        ''' </summary>
        ''' <param name="objPlanificacion"></param>
        ''' <remarks></remarks>
        Public Shared Sub ProcessarProgramaciones(objPlanificacion As Clases.Planificacion, Optional linhasGrid As Integer? = Nothing)

            ' Gera lista de horarios fixos
            Dim listaHorarios As New List(Of Clases.PlanXProgramacion)

            ''Agrupa os horarios gerados por dia da semana
            Dim groupTipo = (From row In objPlanificacion.Programacion
                             Group row By
                           DiaSemana = row.NecDiaFin Into Group
                             Select
                           DiaSemana, Group).ToList

            Dim linhasProgramacao = groupTipo.Max(Function(x) x.Group.Count)

            Dim numlinhas

            If linhasProgramacao > Comon.Constantes.TOTAL_LINEAS Then
                numlinhas = linhasProgramacao
            Else
                numlinhas = Comon.Constantes.TOTAL_LINEAS
            End If

            If linhasGrid < 4 Then
                numlinhas = 4
            ElseIf linhasGrid >= 4 Then
                numlinhas = linhasGrid
            End If


            For index = 1 To numlinhas
                listaHorarios.Add(New Clases.PlanXProgramacion)
            Next


            'Preenche as 4 linhas fixas da tabela de horários
            For i = 0 To listaHorarios.Count - 1
                For index = 0 To groupTipo.Count - 1

                    Dim horario = GetHorarioProgramacion(groupTipo(index).Group(i))

                    Select Case groupTipo(index).DiaSemana
                        Case Comon.Constantes.LUNES
                            listaHorarios(i).FyhLunes = horario

                        Case Comon.Constantes.MARTES
                            listaHorarios(i).FyhMartes = horario

                        Case Comon.Constantes.MIERCOLES
                            listaHorarios(i).FyhMiercoles = horario

                        Case Comon.Constantes.JUEVES
                            listaHorarios(i).FyhJueves = horario

                        Case Comon.Constantes.VIERNES
                            listaHorarios(i).FyhViernes = horario

                        Case Comon.Constantes.SABADO
                            listaHorarios(i).FyhSabado = horario

                        Case Comon.Constantes.DOMINGO
                            listaHorarios(i).FyhDomingo = horario

                    End Select
                Next
            Next

            For i = 0 To listaHorarios.Count - 1
                If i = 0 Then
                    listaHorarios(i).LunesHabilitado = True
                    listaHorarios(i).MartesHabilitado = True
                    listaHorarios(i).MiercolesHabilitado = True
                    listaHorarios(i).JuevesHabilitado = True
                    listaHorarios(i).ViernesHabilitado = True
                    listaHorarios(i).SabadoHabilitado = True
                    listaHorarios(i).DomingoHabilitado = True
                End If

                If Not String.IsNullOrEmpty(listaHorarios(i).FyhLunes) Then
                    listaHorarios(i).LunesHabilitado = True
                    If i < listaHorarios.Count - 1 Then
                        listaHorarios(i + 1).LunesHabilitado = True
                    End If

                End If

                If Not String.IsNullOrEmpty(listaHorarios(i).FyhMartes) Then
                    listaHorarios(i).MartesHabilitado = True
                    If i < listaHorarios.Count - 1 Then
                        listaHorarios(i + 1).MartesHabilitado = True
                    End If
                End If

                If Not String.IsNullOrEmpty(listaHorarios(i).FyhMiercoles) Then
                    listaHorarios(i).MiercolesHabilitado = True
                    If i < listaHorarios.Count - 1 Then
                        listaHorarios(i + 1).MiercolesHabilitado = True
                    End If
                End If

                If Not String.IsNullOrEmpty(listaHorarios(i).FyhJueves) Then
                    listaHorarios(i).JuevesHabilitado = True
                    If i < listaHorarios.Count - 1 Then
                        listaHorarios(i + 1).JuevesHabilitado = True
                    End If
                End If

                If Not String.IsNullOrEmpty(listaHorarios(i).FyhViernes) Then
                    listaHorarios(i).ViernesHabilitado = True
                    If i < listaHorarios.Count - 1 Then
                        listaHorarios(i + 1).ViernesHabilitado = True
                    End If
                End If

                If Not String.IsNullOrEmpty(listaHorarios(i).FyhSabado) Then
                    listaHorarios(i).SabadoHabilitado = True
                    If i < listaHorarios.Count - 1 Then
                        listaHorarios(i + 1).SabadoHabilitado = True
                    End If
                End If

                If Not String.IsNullOrEmpty(listaHorarios(i).FyhDomingo) Then
                    listaHorarios(i).DomingoHabilitado = True
                    If i < listaHorarios.Count - 1 Then
                        listaHorarios(i + 1).DomingoHabilitado = True
                    End If
                End If

            Next

            objPlanificacion.Programacion = listaHorarios

        End Sub

        Public Shared Sub BajaPlanificacion(ByRef planificacion As Clases.Planificacion,
                                      codigoUsuario As String)
            Try
                AccesoDatos.Genesis.Planificacion.BajaPlanificacion(planificacion, codigoUsuario)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

    End Class

End Namespace

