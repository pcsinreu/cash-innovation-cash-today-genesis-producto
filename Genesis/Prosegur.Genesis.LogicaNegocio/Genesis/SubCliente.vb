Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo

Namespace Genesis

    Public Class SubCliente

        Public Shared Function ObtenerIdentificadorSubCliente(IdentificadorCliente As String, CodigoSubCliente As String) As String
            Return AccesoDatos.Genesis.SubCliente.ObtenerIdentificadorSubCliente(IdentificadorCliente, CodigoSubCliente)
        End Function

        Public Shared Function ObtenerSubClientes(objPeticion As IAC.ContractoServicio.SubCliente.GetSubClientes.Peticion) As IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta

            Dim objRespuesta As New IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta

            Try

                If objPeticion.ParametrosPaginacion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
                ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
                End If

                objRespuesta.SubClientes = IAC.AccesoDatos.SubCliente.GetSubClientes(Of IAC.ContractoServicio.SubCliente.GetSubClientes.SubCliente)(objPeticion, objRespuesta.ParametrosPaginacion)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty
            Catch ex As Excepcion.NegocioExcepcion
                'CASO OCORRA ALGUMA EXEÇÃO, TRATA O OBJETO RESPUESTA DA FORMA ADEQUADA
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                objRespuesta.MensajeError = ex.Descricao
                objRespuesta.NombreServidorBD = IAC.AccesoDatos.Util.RetornaNomeServidorBD
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString()
                objRespuesta.NombreServidorBD = IAC.AccesoDatos.Util.RetornaNomeServidorBD
            End Try

            Return objRespuesta

        End Function

        Public Shared Function ObtenerSubClientesDetalle(objPeticion As IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.Peticion) As IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.Respuesta

            Dim objRespuesta As New IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.Respuesta

            Try
                objRespuesta.SubClientes = IAC.AccesoDatos.SubCliente.GetSubClientesDetalle(objPeticion, objRespuesta.ParametrosPaginacion)

                If objRespuesta.SubClientes.Count > 0 Then

                    Dim accionUtilidad As New IAC.LogicaNegocio.AccionUtilidad

                    For Each s In objRespuesta.SubClientes

                        Dim objPeticionNivelSaldo As New IAC.ContractoServicio.Utilidad.GetConfigNivel.Peticion

                        objPeticionNivelSaldo.CodCliente = s.CodCliente
                        objPeticionNivelSaldo.CodSubCliente = s.CodSubCliente
                        objPeticionNivelSaldo.BolActivo = True

                        objPeticionNivelSaldo.ParametrosPaginacion.RealizarPaginacion = False

                        s.ConfigNivelSaldo = accionUtilidad.GetConfigNivelSaldo(objPeticionNivelSaldo).ConfigNivelMovs
                    Next

                End If

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty
            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString()
                objRespuesta.NombreServidorBD = IAC.AccesoDatos.Util.RetornaNomeServidorBD
            End Try

            Return objRespuesta

        End Function

        Public Shared Function grabarSubClientes(objPeticion As IAC.ContractoServicio.SubCliente.SetSubClientes.Peticion) As IAC.ContractoServicio.SubCliente.SetSubClientes.Respuesta

            Dim oidSubCliente As String = String.Empty
            Dim objRespuesta As New IAC.ContractoServicio.SubCliente.SetSubClientes.Respuesta
            Dim codigoAjenoPROFAT As String = Nothing
            Dim objCodigoAjenoPROFAT As Comon.Clases.CodigoAjeno = Nothing

            objRespuesta.SubClientes = New IAC.ContractoServicio.SubCliente.SetSubClientes.SubClienteColeccion

            Dim temErro As Boolean = False
            Dim errorRegistroDatosBancarios As Boolean = False

            Dim _NombreRecurso As String = Prosegur.Genesis.LogeoEntidades.Log.Movimiento.Recurso.IACBusquedaSubCliente
            Dim pais = Prosegur.Genesis.LogicaNegocio.Genesis.Pais.ObtenerPaisPorDefault("")
            Dim _CodPais As String = pais.Codigo
            Dim _IdentificadorLlamada As String = String.Empty
            Dim _ObjJson As String = Newtonsoft.Json.JsonConvert.SerializeObject(objPeticion)
            Dim lstCodAjenoExistente As List(Of Comon.Clases.CodigoAjeno)

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(_CodPais, _NombreRecurso, _IdentificadorLlamada)
            If _IdentificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(_IdentificadorLlamada, _NombreRecurso, Comon.Util.VersionCompleta, _ObjJson, _CodPais, _ObjJson.GetHashCode)
            End If

            For Each objSubCliente As IAC.ContractoServicio.SubCliente.SetSubClientes.SubCliente In objPeticion.SubClientes

                Dim objRespuestaSubCliente As New IAC.ContractoServicio.SubCliente.SetSubClientes.SubCliente
                Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio", Comon.Util.VersionCompleta.ToString(), $"Trabajar Objeto: {Newtonsoft.Json.JsonConvert.SerializeObject(objSubCliente)}", "")

                Try

                    If String.IsNullOrEmpty(objSubCliente.CodSubCliente) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodSubCliente"))
                    End If

                    If String.IsNullOrEmpty(objSubCliente.CodCliente) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodCliente"))
                    End If

                    If String.IsNullOrEmpty(objPeticion.CodigoUsuario) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodigoUsuario"))
                    End If

                    'Recupera o código ajeno PROFAT informado
                    If objSubCliente.CodigoAjeno IsNot Nothing AndAlso objSubCliente.CodigoAjeno.Count > 0 Then
                        'Procura na lista código ajeno PROFAT
                        Dim objCodAjeno = objSubCliente.CodigoAjeno.Find(Function(a) a.CodIdentificador = IAC.AccesoDatos.Constantes.ConstantePROFAT)

                        If objCodAjeno IsNot Nothing Then

                            codigoAjenoPROFAT = objCodAjeno.CodAjeno

                            'Recupera o código ajeno pelo código recebido
                            Dim lstCodAjeno As List(Of Comon.Clases.CodigoAjeno) = AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(String.Empty,
                                                                                                                                  objCodAjeno.CodAjeno,
                                                                                                                                  IAC.AccesoDatos.Constantes.ConstantePROFAT,
                                                                                                                                  IAC.AccesoDatos.Constantes.ConstanteSubcliente).ToList()
                            If lstCodAjeno IsNot Nothing AndAlso lstCodAjeno.Count > 0 Then
                                objCodigoAjenoPROFAT = lstCodAjeno.First
                            End If

                        End If
                    End If

                    'quando o serviço é chamado pelo SOL não é passado os ids e como todo o método estava preparado para trabalhar com o id
                    'então recupera os objetos necessários pelo o código com os ids preenchidos

                    'Se existir código ajeno PROFAT utiliza ele como base
                    If objCodigoAjenoPROFAT IsNot Nothing Then

                        objSubCliente.OidSubCliente = objCodigoAjenoPROFAT.IdentificadorTablaGenesis

                        Dim objCliente = AccesoDatos.Genesis.Cliente.ObtenerClientePorSubCliente(objSubCliente.OidSubCliente)
                        If objCliente IsNot Nothing Then
                            objSubCliente.OidCliente = objCliente.Identificador

                            'Verifica se já existe algum outro subcliente no cliente com o mesmo código subcliente recebido
                            Dim oidSubClienteCodigo As String = AccesoDatos.Genesis.SubCliente.ObtenerIdentificadorSubCliente(objCliente.Identificador, objSubCliente.CodSubCliente)
                            If Not String.IsNullOrEmpty(oidSubClienteCodigo) AndAlso (objSubCliente.OidSubCliente <> oidSubClienteCodigo OrElse objCliente.Codigo <> objSubCliente.CodCliente) Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("057_msg_erro_subcliente_codigo"), objSubCliente.CodSubCliente, objCliente.Codigo))
                            End If
                        End If

                    Else
                        If String.IsNullOrEmpty(objSubCliente.OidCliente) AndAlso Not String.IsNullOrEmpty(objSubCliente.CodCliente) Then
                            objSubCliente.OidCliente = AccesoDatos.Genesis.Cliente.ObtenerIdentificadorCliente(objSubCliente.CodCliente)
                        End If
                        If String.IsNullOrEmpty(objSubCliente.OidSubCliente) AndAlso Not String.IsNullOrEmpty(objSubCliente.CodSubCliente) AndAlso Not String.IsNullOrEmpty(objSubCliente.OidCliente) Then
                            objSubCliente.OidSubCliente = AccesoDatos.Genesis.SubCliente.ObtenerIdentificadorSubCliente(objSubCliente.OidCliente, objSubCliente.CodSubCliente)
                        End If

                        If Not String.IsNullOrEmpty(objSubCliente.OidCliente) AndAlso Not String.IsNullOrEmpty(objSubCliente.OidSubCliente) Then
                            'Recupera o código ajeno pelo Id do subcliente
                            Dim lstCodAjeno As List(Of Comon.Clases.CodigoAjeno) = AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(objSubCliente.OidSubCliente,
                                                                                                                                    String.Empty,
                                                                                                                                    IAC.AccesoDatos.Constantes.ConstantePROFAT,
                                                                                                                                    IAC.AccesoDatos.Constantes.ConstanteSubcliente).ToList()

                            'Se já existir código ajeno PROFAT cadastrado e o código recebido for diferente
                            If Not String.IsNullOrEmpty(codigoAjenoPROFAT) _
                                    AndAlso lstCodAjeno IsNot Nothing _
                                        AndAlso lstCodAjeno.Count > 0 _
                                            AndAlso lstCodAjeno.First.Codigo <> codigoAjenoPROFAT Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("057_msg_erro_subcliente_codigo"), objSubCliente.CodSubCliente, objSubCliente.CodCliente))
                            End If
                        End If
                    End If

                    objRespuestaSubCliente.OidSubCliente = objSubCliente.OidSubCliente
                    objRespuestaSubCliente.CodSubCliente = objSubCliente.CodSubCliente
                    objRespuestaSubCliente.DesSubCliente = objSubCliente.DesSubCliente
                    objRespuestaSubCliente.OidCliente = objSubCliente.OidCliente
                    objRespuestaSubCliente.CodCliente = objSubCliente.CodCliente

                    If objPeticion.BolBaja Then
                        Dim objTransacao As New Transacao(IAC.AccesoDatos.Constantes.CONEXAO_GE)

                        If String.IsNullOrEmpty(objSubCliente.OidSubCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "OidSubCliente"))
                        End If

                        IAC.AccesoDatos.SubCliente.BajaSubCliente(objSubCliente, objPeticion.CodigoUsuario, objTransacao)
                        If objPeticion.BolEliminaCodigosAjenos Then
                            Dim objAccionAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno
                            objAccionAjeno.BorrarCodigosAjenos(objRespuestaSubCliente.OidSubCliente, "GEPR_TSUBCLIENTE", Nothing)
                        End If
                        objTransacao.RealizarTransacao()
                    Else

                        ' ------------- VERIFICA TIPO DE SUBCLIENTE ------------ 'POG'
                        If Not String.IsNullOrEmpty(objSubCliente.CodTipoSubCliente) Then
                            Dim tipoSubCliente = IAC.AccesoDatos.TipoSubCliente.BuscaTipoSubClientePorCodigo(objSubCliente.CodTipoSubCliente)
                            If tipoSubCliente Is Nothing Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("057_msg_tipoSubCliente_naoencontrado"))
                            Else
                                If Not tipoSubCliente.bolActivo Then
                                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("057_msg_tipoSubCliente_naoativo"))
                                End If
                            End If
                            objSubCliente.OidTipoSubCliente = tipoSubCliente.oidTipoSubcliente
                        ElseIf Not String.IsNullOrEmpty(objSubCliente.OidTipoSubCliente) Then
                            objSubCliente.OidTipoSubCliente = objSubCliente.OidTipoSubCliente
                        Else
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodTipoSubCliente"))
                        End If

                        If String.IsNullOrEmpty(objSubCliente.OidCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("057_msg_cliente_naoencontrado"))
                        End If

                        If Not String.IsNullOrEmpty(objSubCliente.OidCliente) Then
                            If Not IAC.AccesoDatos.SubCliente.VerificaClienteAtivo(objSubCliente.OidCliente) Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("057_msg_erro_cliente"), "OidCliente"))
                            End If
                        End If

                        If String.IsNullOrEmpty(objSubCliente.CodSubCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodSubCliente"))
                        End If

                        If String.IsNullOrEmpty(objSubCliente.DesSubCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesSubCliente"))
                        End If

                        'Validar se o subcliente é totalizador de saldos de outro subcliente
                        If (Not objSubCliente.BolTotalizadorSaldo AndAlso Not String.IsNullOrEmpty(objSubCliente.OidSubCliente)) Then
                            Dim BolSubClienteTotSaldo As Boolean = IAC.AccesoDatos.SubCliente.BuscarSubClienteNivelSaldo(objSubCliente.OidSubCliente)
                            If BolSubClienteTotSaldo Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("057_msg_subcliente_totalizador"), "BolSubClienteTotSaldo"))
                            End If
                        End If



                        If objSubCliente.OidSubCliente <> String.Empty Then
                            'Cria uma transação, caso não consiga atualiza o cliente, subcliente e punto de servicio dá um rollback.
                            Dim objTransacao As New Transacao(IAC.AccesoDatos.Constantes.CONEXAO_GE)



                            Dim Subcliente = AccesoDatos.Genesis.SubCliente.ObtenerSubCliente(objSubCliente.CodSubCliente)

                            Dim ReplicarVigenteSubclientePtoServicio = False

                            If Subcliente IsNot Nothing Then

                                If Subcliente.EstaActivo <> objSubCliente.BolVigente Then
                                    ReplicarVigenteSubclientePtoServicio = True
                                End If

                            End If




                            IAC.AccesoDatos.SubCliente.ActualizarSubCliente(objSubCliente, objPeticion.CodigoUsuario, objTransacao, ReplicarVigenteSubclientePtoServicio)
                            Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio", Comon.Util.VersionCompleta.ToString(), $"Se ejecuto ActualizarSubCliente", "")

                            objTransacao.RealizarTransacao()
                            'Lista los códigos ajenos relacionados al subcliente
                            lstCodAjenoExistente = AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(objSubCliente.OidSubCliente,
                                                                                                                                  "",
                                                                                                                                  "",
                                                                                                                                  IAC.AccesoDatos.Constantes.ConstanteSubcliente).ToList()
                            'Grava os Codigos Ajenos
                            DefinirCodigoAjeno(objSubCliente.OidSubCliente, objSubCliente.CodigoAjeno, objPeticion.CodigoUsuario, lstCodAjenoExistente)
                            'Grava as Direcciones
                            DefinirDirecciones(objSubCliente.OidSubCliente, objSubCliente.Direcciones, objPeticion.CodigoUsuario)
                        Else
                            oidSubCliente = IAC.AccesoDatos.SubCliente.AltaSubCliente(objSubCliente, objPeticion.CodigoUsuario)
                            Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio", Comon.Util.VersionCompleta.ToString(), $"Se ejecuto AltaSubCliente", "")

                            If objSubCliente.ConfigNivelSaldo IsNot Nothing AndAlso objSubCliente.ConfigNivelSaldo.Count > 0 Then
                                For Each nivel In objSubCliente.ConfigNivelSaldo.FindAll(Function(x) String.IsNullOrEmpty(x.oidSubCliente))

                                    nivel.oidSubCliente = oidSubCliente

                                    'Preenche o id caso seja um registro novo
                                    If nivel.configNivelSaldo IsNot Nothing _
                                            AndAlso Not String.IsNullOrEmpty(nivel.configNivelSaldo.codSubcliente) _
                                                AndAlso String.IsNullOrEmpty(nivel.configNivelSaldo.oidSubcliente) Then
                                        nivel.configNivelSaldo.oidSubcliente = oidSubCliente
                                    End If
                                Next
                            End If

                            objSubCliente.OidSubCliente = oidSubCliente
                            objRespuestaSubCliente.OidSubCliente = oidSubCliente
                            'Grava os Codigos Ajenos
                            DefinirCodigoAjeno(oidSubCliente, objSubCliente.CodigoAjeno, objPeticion.CodigoUsuario, lstCodAjenoExistente)
                            'Grava as Direcciones
                            DefinirDirecciones(oidSubCliente, objSubCliente.Direcciones, objPeticion.CodigoUsuario)
                        End If

                        Dim objPeticionCliente As New IAC.ContractoServicio.Cliente.GetClientes.Peticion() With {.OidCliente = objSubCliente.OidCliente}
                        objPeticionCliente.ParametrosPaginacion.RealizarPaginacion = False

                        ' ---------------- OBTEM OS REGISTROS RELACIONADOS ---------
                        Dim objCliente = Cliente.ObtenerClientes(objPeticionCliente)
                        If Not (objCliente IsNot Nothing AndAlso objCliente.Clientes IsNot Nothing AndAlso objCliente.Clientes.Count > 0) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("057_msg_cliente_naoencontrado"), "Clientes"))
                        End If

                        If objSubCliente.ConfigNivelSaldo Is Nothing Then
                            objSubCliente.ConfigNivelSaldo = New IAC.ContractoServicio.SubCliente.SetSubClientes.ConfigNivelMovColeccion
                        End If

                        Dim lstConfigNivelMov As New List(Of IAC.ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov)
                        For Each obj In objSubCliente.ConfigNivelSaldo
                            lstConfigNivelMov.Add(obj)
                        Next

                        MaestroCuenta.GrabarConfiguracionesNivelMovimiento(lstConfigNivelMov, objPeticion.CodigoUsuario, objSubCliente.OidCliente, objSubCliente.OidSubCliente)
                        Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio", Comon.Util.VersionCompleta.ToString(), $"Se ejecuto GrabarConfiguracionesNivelMovimiento", "")

                        Dim objAccionDatoBancario As New IAC.LogicaNegocio.AccionDatoBancario

                        If objSubCliente.PeticionDatosBancarios IsNot Nothing AndAlso objSubCliente.PeticionDatosBancarios.DatosBancarios IsNot Nothing AndAlso objSubCliente.PeticionDatosBancarios.DatosBancarios.Count > 0 Then

                            For Each peticion In objSubCliente.PeticionDatosBancarios.DatosBancarios
                                peticion.IdentificadorCliente = objSubCliente.OidCliente
                                peticion.IdentificadorSubCliente = objSubCliente.OidSubCliente
                            Next

                            Dim objRespuestaDatoBancario = objAccionDatoBancario.SetDatosBancarios(objSubCliente.PeticionDatosBancarios)
                            If objRespuestaDatoBancario.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                                errorRegistroDatosBancarios = True
                                Throw New Exception(objRespuestaDatoBancario.MensajeError)
                            End If
                        End If

                    End If

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio", Comon.Util.VersionCompleta.ToString(), $"Finaliza ejecución grabar", "")

                    objRespuestaSubCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuestaSubCliente.MensajeError = String.Empty

                Catch ex As Excepcion.NegocioExcepcion

                    'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
                    objRespuestaSubCliente.CodigoError = ex.Codigo
                    objRespuestaSubCliente.MensajeError = ex.Descricao
                    temErro = False

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                    objRespuestaSubCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                    objRespuestaSubCliente.MensajeError = ex.ToString()
                    temErro = True

                Finally

                    objRespuesta.SubClientes.Add(objRespuestaSubCliente)

                End Try

            Next

            If temErro Then
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                If errorRegistroDatosBancarios Then
                    objRespuesta.MensajeError = Traduzir("013_msg_ErrorDatosBancarios_SubCliente")
                Else
                    objRespuesta.MensajeError = Traduzir("013_msg_ErroCollecionClientes")
                End If
                objRespuesta.NombreServidorBD = IAC.AccesoDatos.Util.RetornaNomeServidorBD
            Else
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty
            End If

            If _IdentificadorLlamada IsNot Nothing Then
                Dim _RespJson = Newtonsoft.Json.JsonConvert.SerializeObject(objRespuesta)
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(_IdentificadorLlamada, _RespJson, objRespuesta.CodigoError, objRespuesta.MensajeError, _RespJson.GetHashCode)
            End If

            Return objRespuesta

        End Function

        Private Shared Sub DefinirCodigoAjeno(oidSubCliente As String, codigoAjenoColeccion As IAC.ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase, codigoUsuario As String,
                                              lstCodAjenoExistente As List(Of Comon.Clases.CodigoAjeno))

            Dim objAccionAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno

            'Agrega o actualiza los códigos ajenos modificados por el usuario
            If codigoAjenoColeccion.Count > 0 Then
                Dim objCodigoAjeno As New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion
                objCodigoAjeno.CodigosAjenos = New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion

                For Each objItem In codigoAjenoColeccion
                    Dim objItemCast As New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno
                    Dim codigotablaGenesis As String = String.Empty

                    objItemCast.OidCodigoAjeno = objItem.OidCodigoAjeno
                    objItemCast.BolActivo = objItem.BolActivo
                    objItemCast.BolDefecto = objItem.BolDefecto
                    objItemCast.CodAjeno = objItem.CodAjeno
                    objItemCast.CodIdentificador = objItem.CodIdentificador
                    objItemCast.DesAjeno = objItem.DesAjeno
                    If String.IsNullOrEmpty(objItemCast.DesUsuarioCreacion) Then
                        objItemCast.DesUsuarioCreacion = codigoUsuario
                        objItemCast.GmtCreacion = DateTime.Now
                    End If
                    objItemCast.DesUsuarioModificacion = codigoUsuario
                    objItemCast.GmtModificacion = DateTime.Now
                    objItemCast.OidTablaGenesis = oidSubCliente
                    codigotablaGenesis = IAC.ContractoServicio.Constantes.COD_SUBCLIENTE
                    objItemCast.CodTipoTablaGenesis = (From item In IAC.AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                                       Where item.CodTipoTablaGenesis = codigotablaGenesis
                                                       Select item.Entidade).FirstOrDefault()

                    objCodigoAjeno.CodigosAjenos.Add(objItemCast)
                Next

                objAccionAjeno.SetCodigosAjenos(objCodigoAjeno)

                'Elimina los códigos ajenos borrados por el usuario
                If lstCodAjenoExistente IsNot Nothing Then
                    For Each objItemEliminar In lstCodAjenoExistente
                        If objCodigoAjeno.CodigosAjenos.Find(Function(codAje) codAje.OidCodigoAjeno = objItemEliminar.Identificador) Is Nothing Then objAccionAjeno.BorrarCodigoAjeno(objItemEliminar.Identificador, Nothing)
                    Next
                End If
            Else
                If lstCodAjenoExistente IsNot Nothing Then
                    'Elimina todos los códigos ajenos relacionados al subcliente
                    For Each objItem In lstCodAjenoExistente
                        objAccionAjeno.BorrarCodigoAjeno(objItem.Identificador, Nothing)
                    Next
                End If
            End If
        End Sub


        Public Shared Function IdentificadoresSubClienteTotalizadorSaldo(identificadoresSubCliente As List(Of String)) As List(Of String)
            Return Prosegur.Genesis.AccesoDatos.Genesis.SubCliente.IdentificadoresSubClienteTotalizadorSaldo(identificadoresSubCliente)
        End Function

        Public Shared Function IdentificadoresSubClientePuntoServicioTotalizadorSaldo(identificadoresSubCliente As List(Of String)) As List(Of String)
            Return Prosegur.Genesis.AccesoDatos.Genesis.SubCliente.IdentificadoresSubClientePuntoServicioTotalizadorSaldo(identificadoresSubCliente)
        End Function

        Public Shared Function GetSubclienteByCodigoAjeno(objPeticion As IAC.ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.Peticion) As IAC.ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.Respuesta

            Dim objRespuesta As New IAC.ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.Respuesta

            Try

                objRespuesta.SubClientes = IAC.AccesoDatos.SubCliente.GetSubclienteByCodigoAjeno(Of IAC.ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.SubCliente)(objPeticion.identificadorAjeno, objPeticion.subclienteCodigoAjeno)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty
            Catch ex As Excepcion.NegocioExcepcion
                'CASO OCORRA ALGUMA EXEÇÃO, TRATA O OBJETO RESPUESTA DA FORMA ADEQUADA
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                objRespuesta.MensajeError = ex.Descricao
                objRespuesta.NombreServidorBD = IAC.AccesoDatos.Util.RetornaNomeServidorBD
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString()
                objRespuesta.NombreServidorBD = IAC.AccesoDatos.Util.RetornaNomeServidorBD
            End Try

            Return objRespuesta

        End Function

#Region "DIRECCIONES"

        Private Shared Sub DefinirDirecciones(oidSubCliente As String, DireccionColeccion As IAC.ContractoServicio.Direccion.DireccionColeccionBase, codigoUsuario As String)

            Dim objAccionDireccion As New IAC.LogicaNegocio.AccionDireccion

            If DireccionColeccion.Count > 0 Then

                For Each objItem In DireccionColeccion
                    Dim objDireciones As New IAC.ContractoServicio.Direccion.SetDirecciones.Peticion
                    Dim codigotablaGenesis As String = String.Empty

                    objDireciones.bolBaja = objItem.bolBaja
                    objDireciones.codFiscal = objItem.codFiscal
                    objDireciones.codPostal = objItem.codPostal
                    objDireciones.desCampoAdicional1 = objItem.desCampoAdicional1
                    objDireciones.desCampoAdicional2 = objItem.desCampoAdicional2
                    objDireciones.desCampoAdicional3 = objItem.desCampoAdicional3
                    objDireciones.desCategoriaAdicional1 = objItem.desCategoriaAdicional1
                    objDireciones.desCategoriaAdicional2 = objItem.desCategoriaAdicional2
                    objDireciones.desCategoriaAdicional3 = objItem.desCategoriaAdicional3
                    objDireciones.desCiudad = objItem.desCiudad
                    objDireciones.desDireccionLinea1 = objItem.desDireccionLinea1
                    objDireciones.desDireccionLinea2 = objItem.desDireccionLinea2
                    objDireciones.desEmail = objItem.desEmail
                    objDireciones.desNumeroTelefono = objItem.desNumeroTelefono
                    objDireciones.desPais = objItem.desPais
                    objDireciones.desProvincia = objItem.desProvincia
                    objDireciones.oidDireccion = objItem.oidDireccion
                    objDireciones.desUsuarioModificacion = codigoUsuario
                    objDireciones.gmtModificacion = DateTime.Now
                    If String.IsNullOrEmpty(objDireciones.desUsuarioCreacion) Then
                        objDireciones.desUsuarioCreacion = codigoUsuario
                        objDireciones.gmtCreacion = DateTime.Now
                    End If

                    objDireciones.oidTablaGenesis = oidSubCliente
                    codigotablaGenesis = IAC.ContractoServicio.Constantes.COD_SUBCLIENTE
                    objDireciones.codTipoTablaGenesis = (From item In IAC.AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                                         Where item.CodTipoTablaGenesis = codigotablaGenesis
                                                         Select item.Entidade).FirstOrDefault()

                    objAccionDireccion.SetDirecciones(objDireciones)
                Next


            End If

        End Sub
#End Region

        Shared Function ObtenerSubClienteJSON(codigo As String, descripcion As String, identificadorPadre As String) As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Return AccesoDatos.Genesis.SubCliente.ObtenerSubClienteJSON(codigo, descripcion, identificadorPadre)
        End Function
    End Class

End Namespace