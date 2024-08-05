Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo

Namespace Genesis

    Public Class PuntoServicio

        Public Shared Function ObtenerIdentificadorPuntoServicio_v2(CodigoCliente As String, CodigoSubCliente As String, CodigoPuntoServicio As String) As String
            Return AccesoDatos.Genesis.PuntoServicio.ObtenerIdentificadorPuntoServicio_v2(CodigoCliente, CodigoSubCliente, CodigoPuntoServicio)
        End Function

        Public Shared Function ObtenerIdentificadorPuntoServicio(IdentificadorCliente As String, IdentificadorSubCliente As String, CodigoPuntoServicio As String) As String
            Return AccesoDatos.Genesis.PuntoServicio.ObtenerIdentificadorPuntoServicio(IdentificadorCliente, IdentificadorSubCliente, CodigoPuntoServicio)
        End Function


        Public Shared Function ObtenerPuntoServicio(objPeticion As IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Peticion) As IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta

            Dim objRespuesta As New IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta

            Try

                If objPeticion.ParametrosPaginacion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
                ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
                End If

                objRespuesta.PuntoServicio = IAC.AccesoDatos.PuntoServicio.GetPuntoServicio(Of IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio)(objPeticion, objRespuesta.ParametrosPaginacion)
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

        Public Shared Function ObternerPuntoServicioDetalle(objPeticion As IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Peticion) As IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Respuesta

            Dim objRespuesta As New IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Respuesta

            Try
                objRespuesta.PuntoServicio = IAC.AccesoDatos.PuntoServicio.GetPuntoServicioDetalle(objPeticion, objRespuesta.ParametrosPaginacion)

                If objRespuesta.PuntoServicio.Count > 0 Then

                    Dim accionUtilidad As New IAC.LogicaNegocio.AccionUtilidad

                    For Each p In objRespuesta.PuntoServicio

                        Dim objPeticionNivelSaldo As New IAC.ContractoServicio.Utilidad.GetConfigNivel.Peticion

                        objPeticionNivelSaldo.CodCliente = p.CodCliente
                        objPeticionNivelSaldo.CodSubCliente = p.CodSubCliente
                        objPeticionNivelSaldo.CodPtoServicio = p.CodPuntoServicio
                        objPeticionNivelSaldo.BolActivo = True

                        objPeticionNivelSaldo.ParametrosPaginacion.RealizarPaginacion = False

                        p.ConfigNivelSaldo = accionUtilidad.GetConfigNivelSaldo(objPeticionNivelSaldo).ConfigNivelMovs
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

        Public Shared Function grabarPuntoServicio(objPeticion As IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.Peticion) As IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.Respuesta

            Dim oidPuntoServicio As String = String.Empty
            Dim objRespuesta As New IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.Respuesta
            Dim codigoAjenoPROFAT As String = Nothing
            Dim objCodigoAjenoPROFAT As Comon.Clases.CodigoAjeno = Nothing

            objRespuesta.PuntoServicio = New IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicioColeccion

            Dim temErro As Boolean = False
            Dim errorRegistroDatosBancarios As Boolean = False

            Dim _NombreRecurso As String = Prosegur.Genesis.LogeoEntidades.Log.Movimiento.Recurso.IACBusquedaPuntoServicio

            Dim pais = Prosegur.Genesis.LogicaNegocio.Genesis.Pais.ObtenerPaisPorDefault("")
            Dim _CodPais As String = pais.Codigo
            Dim _IdentificadorLlamada As String = String.Empty
            Dim _ObjJson As String = Newtonsoft.Json.JsonConvert.SerializeObject(objPeticion)
            Dim lstCodAjenoExistente As List(Of Comon.Clases.CodigoAjeno)

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(_CodPais, _NombreRecurso, _IdentificadorLlamada)
            If _IdentificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(_IdentificadorLlamada, _NombreRecurso, Comon.Util.VersionCompleta, _ObjJson, _CodPais, _ObjJson.GetHashCode)
            End If

            For Each objPuntoServicio As IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicio In objPeticion.PuntoServicio

                Dim objRespuestaPuntoServicio As New IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicio

                Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio", Comon.Util.VersionCompleta.ToString(), $"Trabajar Objeto: {Newtonsoft.Json.JsonConvert.SerializeObject(objPuntoServicio)}", "")


                Try

                    If String.IsNullOrEmpty(objPuntoServicio.CodCliente) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodCliente"))
                    End If

                    If String.IsNullOrEmpty(objPuntoServicio.CodSubCliente) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodSubCliente"))
                    End If

                    If String.IsNullOrEmpty(objPuntoServicio.CodPuntoServicio) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodPuntoServicio"))
                    End If

                    If String.IsNullOrEmpty(objPeticion.CodigoUsuario) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodigoUsuario"))
                    End If

                    'Recupera o código ajeno PROFAT informado
                    If objPuntoServicio.CodigoAjeno IsNot Nothing AndAlso objPuntoServicio.CodigoAjeno.Count > 0 Then
                        'Procura na lista código ajeno PROFAT
                        Dim objCodAjeno = objPuntoServicio.CodigoAjeno.Find(Function(a) a.CodIdentificador = IAC.AccesoDatos.Constantes.ConstantePROFAT)

                        If objCodAjeno IsNot Nothing Then

                            codigoAjenoPROFAT = objCodAjeno.CodAjeno

                            'Recupera o código ajeno pelo código recebido
                            Dim lstCodAjeno As List(Of Comon.Clases.CodigoAjeno) = AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(String.Empty,
                                                                                                                                  objCodAjeno.CodAjeno,
                                                                                                                                  IAC.AccesoDatos.Constantes.ConstantePROFAT,
                                                                                                                                  IAC.AccesoDatos.Constantes.ConstantePuntoServicio).ToList()
                            If lstCodAjeno IsNot Nothing AndAlso lstCodAjeno.Count > 0 Then
                                objCodigoAjenoPROFAT = lstCodAjeno.First
                            End If

                        End If
                    End If

                    'quando o serviço é chamado pelo SOL não é passado os ids e como todo o método estava preparado para trabalhar com o id
                    'então recupera os objetos necessários pelo o código com os ids preenchidos
                    Dim identificadorCliente As String = AccesoDatos.Genesis.Cliente.ObtenerIdentificadorCliente(objPuntoServicio.CodCliente)

                    'Se existir código ajeno PROFAT utiliza ele como base
                    If objCodigoAjenoPROFAT IsNot Nothing Then

                        objPuntoServicio.OidPuntoServicio = objCodigoAjenoPROFAT.IdentificadorTablaGenesis

                        Dim objSubCliente = AccesoDatos.Genesis.SubCliente.ObtenerSubClientePorPuntoServicio(objPuntoServicio.OidPuntoServicio)
                        If objSubCliente IsNot Nothing Then
                            objPuntoServicio.OidSubCliente = objSubCliente.Identificador

                            Dim objCliente = AccesoDatos.Genesis.Cliente.ObtenerClientePorSubCliente(objSubCliente.Identificador)
                            If objCliente IsNot Nothing Then
                                'Verifica se já existe algum outro punto servicio no subcliente com o mesmo código recebido
                                Dim oidPuntoServ As String = AccesoDatos.Genesis.PuntoServicio.ObtenerIdentificadorPuntoServicio(objCliente.Identificador, objSubCliente.Identificador, objPuntoServicio.CodPuntoServicio)
                                If Not String.IsNullOrEmpty(oidPuntoServ) AndAlso (objPuntoServicio.OidPuntoServicio <> oidPuntoServ OrElse objCliente.Codigo <> objPuntoServicio.CodCliente OrElse objSubCliente.Codigo <> objPuntoServicio.CodSubCliente) Then
                                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_erro_puntoservicio_codigo"), objPuntoServicio.CodPuntoServicio, objCliente.Codigo, objSubCliente.Codigo))
                                End If
                            End If
                        End If

                    Else
                        If String.IsNullOrEmpty(objPuntoServicio.OidSubCliente) AndAlso Not String.IsNullOrEmpty(objPuntoServicio.CodSubCliente) AndAlso Not String.IsNullOrEmpty(identificadorCliente) Then
                            objPuntoServicio.OidSubCliente = AccesoDatos.Genesis.SubCliente.ObtenerIdentificadorSubCliente(identificadorCliente, objPuntoServicio.CodSubCliente)
                        End If
                        If String.IsNullOrEmpty(objPuntoServicio.OidPuntoServicio) AndAlso Not String.IsNullOrEmpty(objPuntoServicio.CodPuntoServicio) AndAlso Not String.IsNullOrEmpty(objPuntoServicio.OidSubCliente) Then
                            objPuntoServicio.OidPuntoServicio = AccesoDatos.Genesis.PuntoServicio.ObtenerIdentificadorPuntoServicio(identificadorCliente, objPuntoServicio.OidSubCliente, objPuntoServicio.CodPuntoServicio)
                        End If

                        If Not String.IsNullOrEmpty(objPuntoServicio.OidSubCliente) AndAlso Not String.IsNullOrEmpty(objPuntoServicio.OidPuntoServicio) Then
                            'Recupera o código ajeno pelo Id do subcliente
                            Dim lstCodAjeno As List(Of Comon.Clases.CodigoAjeno) = AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(objPuntoServicio.OidPuntoServicio,
                                                                                                                                        String.Empty,
                                                                                                                                        IAC.AccesoDatos.Constantes.ConstantePROFAT,
                                                                                                                                        IAC.AccesoDatos.Constantes.ConstantePuntoServicio).ToList()

                            'Se já existir código ajeno PROFAT cadastrado e o código recebido for diferente
                            If Not String.IsNullOrEmpty(codigoAjenoPROFAT) _
                                    AndAlso lstCodAjeno IsNot Nothing _
                                        AndAlso lstCodAjeno.Count > 0 _
                                            AndAlso lstCodAjeno.First.Codigo <> codigoAjenoPROFAT Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_erro_puntoservicio_codigo"), objPuntoServicio.CodPuntoServicio, objPuntoServicio.CodCliente, objPuntoServicio.CodSubCliente))
                            End If
                        End If
                    End If

                    objRespuestaPuntoServicio.OidPuntoServicio = objPuntoServicio.OidPuntoServicio
                    objRespuestaPuntoServicio.CodPuntoServicio = objPuntoServicio.CodPuntoServicio
                    objRespuestaPuntoServicio.DesPuntoServicio = objPuntoServicio.DesPuntoServicio
                    objRespuestaPuntoServicio.OidSubCliente = objPuntoServicio.OidSubCliente
                    objRespuestaPuntoServicio.CodSubCliente = objPuntoServicio.CodSubCliente
                    objRespuestaPuntoServicio.CodCliente = objPuntoServicio.CodCliente

                    If objPeticion.BolBaja Then

                        If String.IsNullOrEmpty(identificadorCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("057_msg_cliente_naoencontrado"))
                        End If

                        If String.IsNullOrEmpty(objPuntoServicio.OidSubCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_subcliente_naoencontrado"))
                        End If

                        If Not String.IsNullOrEmpty(objPuntoServicio.OidPuntoServicio) Then
                            IAC.AccesoDatos.PuntoServicio.BajaPuntoServicio(objPuntoServicio, objPeticion.CodigoUsuario)
                            If objPeticion.BolEliminaCodigosAjenos Then
                                Dim objAccionAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno
                                objAccionAjeno.BorrarCodigosAjenos(objPuntoServicio.OidPuntoServicio, "GEPR_TPUNTO_SERVICIO", Nothing)
                            End If
                        End If

                    Else

                        If Not String.IsNullOrEmpty(objPuntoServicio.CodTipoPuntoServicio) Then

                            Dim tipoPuntoServicio = IAC.AccesoDatos.TipoPuntoServicio.BuscaTipoPuntoServicioPorCodigo(objPuntoServicio.CodTipoPuntoServicio)

                            If tipoPuntoServicio Is Nothing Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_tipoPuntoServicio_naoencontrado"))
                            Else
                                If Not tipoPuntoServicio.bolActivo Then
                                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_tipoPuntoServicio_noactivo"))
                                End If
                            End If

                            objPuntoServicio.OidTipoPuntoServicio = tipoPuntoServicio.oidTipoPuntoServicio

                        ElseIf Not String.IsNullOrEmpty(objPuntoServicio.OidTipoPuntoServicio) Then


                        Else
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodTipoPuntoServicio"))
                        End If


                        If String.IsNullOrEmpty(identificadorCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("057_msg_cliente_naoencontrado"))
                        End If

                        If String.IsNullOrEmpty(objPuntoServicio.OidSubCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_subcliente_naoencontrado"))
                        End If

                        If Not String.IsNullOrEmpty(objPuntoServicio.OidSubCliente) Then

                            'Verifica se o cliente enviado está ativo
                            If Not IAC.AccesoDatos.PuntoServicio.VerificaClienteAtivo(objPuntoServicio.OidSubCliente) Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_erro_punto_servicio_cliente_ativo"), "OidSubCliente"))
                            End If

                            'Verifica se o Subcliente enviado esta ativo
                            If Not IAC.AccesoDatos.PuntoServicio.VerificaSubclienteAtivo(objPuntoServicio.OidSubCliente).BolVigente Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_erro_punto_servicio_sub_cliente_ativo"), "OidSubCliente"))
                            End If

                        End If

                        If String.IsNullOrEmpty(objPuntoServicio.OidSubCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "OidSubCliente"))
                        End If

                        If String.IsNullOrEmpty(objPuntoServicio.DesPuntoServicio) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesPuntoServicio"))
                        End If

                        'Validar se o ponto de serviço é totalizador de saldos de outro ponto de serviço
                        If (Not objPuntoServicio.BolTotalizadorSaldo AndAlso Not String.IsNullOrEmpty(objPuntoServicio.OidPuntoServicio)) Then
                            Dim BolPuntoServicioTotSaldo As Boolean = IAC.AccesoDatos.PuntoServicio.BuscarPuntoServicioNivelSaldo(objPuntoServicio.OidPuntoServicio)
                            If BolPuntoServicioTotSaldo Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_punto_servicio_totalizador"), "BolPuntoServicioTotSaldo"))
                            End If
                        End If



                        If objPuntoServicio.OidPuntoServicio <> String.Empty Then
                            IAC.AccesoDatos.PuntoServicio.ActualizarPuntoServicio(objPuntoServicio, objPeticion.CodigoUsuario)
                            Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio", Comon.Util.VersionCompleta.ToString(), $"Se ejecuto ActualizarPuntoServicio", "")

                            'Lista los códigos ajenos relacionados al subcliente
                            lstCodAjenoExistente = AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(objPuntoServicio.OidPuntoServicio,
                                                                                                                                  "",
                                                                                                                                  "",
                                                                                                                                  IAC.AccesoDatos.Constantes.ConstantePuntoServicio).ToList()

                            'Grava os Codigos Ajenos
                            DefinirCodigoAjeno(objPuntoServicio.OidPuntoServicio, objPuntoServicio.CodigoAjeno, objPeticion.CodigoUsuario, lstCodAjenoExistente)
                            'Grava as Direcciones
                            DefinirDirecciones(objPuntoServicio.OidPuntoServicio, objPuntoServicio.Direcciones, objPeticion.CodigoUsuario)
                        Else
                            oidPuntoServicio = IAC.AccesoDatos.PuntoServicio.AltaPuntoServicio(objPuntoServicio, objPeticion.CodigoUsuario)
                            Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio", Comon.Util.VersionCompleta.ToString(), $"Se ejecuto AltaPuntoServicio", "")

                            If objPuntoServicio.ConfigNivelSaldo IsNot Nothing AndAlso objPuntoServicio.ConfigNivelSaldo.Count > 0 Then
                                For Each nivel In objPuntoServicio.ConfigNivelSaldo.FindAll(Function(x) String.IsNullOrEmpty(x.oidPtoServicio))
                                    'If nivel.configNivelSaldo IsNot Nothing Then
                                    '    nivel.configNivelSaldo.oidPtoServicio = oidPuntoServicio
                                    'End If
                                    nivel.oidPtoServicio = oidPuntoServicio

                                    'Preenche o id do ponto serviço caso seja um ponto de serviço novo
                                    If nivel.configNivelSaldo IsNot Nothing _
                                            AndAlso Not String.IsNullOrEmpty(nivel.configNivelSaldo.codPtoServicio) _
                                                AndAlso String.IsNullOrEmpty(nivel.configNivelSaldo.oidPtoServicio) Then
                                        nivel.configNivelSaldo.oidPtoServicio = oidPuntoServicio
                                    End If
                                Next
                            End If
                            objPuntoServicio.OidPuntoServicio = oidPuntoServicio

                            objRespuestaPuntoServicio.OidPuntoServicio = oidPuntoServicio
                            'Grava os Codigos Ajenos
                            DefinirCodigoAjeno(oidPuntoServicio, objPuntoServicio.CodigoAjeno, objPeticion.CodigoUsuario, lstCodAjenoExistente)
                            'Grava as Direcciones
                            DefinirDirecciones(oidPuntoServicio, objPuntoServicio.Direcciones, objPeticion.CodigoUsuario)
                        End If

                        ' ---------------- OBTEM OS REGISTROS RELACIONADOS ---------
                        Dim objPeticionSubCliente As New IAC.ContractoServicio.SubCliente.GetSubClientes.Peticion() With {.OidSubCliente = objPuntoServicio.OidSubCliente}
                        objPeticionSubCliente.ParametrosPaginacion.RealizarPaginacion = False
                        Dim objSubCliente = SubCliente.ObtenerSubClientes(objPeticionSubCliente)
                        If Not (objSubCliente IsNot Nothing AndAlso objSubCliente.SubClientes IsNot Nothing AndAlso objSubCliente.SubClientes.Count > 0) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_subcliente_naoencontrado"), "SubClientes"))
                        End If

                        Dim objPeticionCliente As New IAC.ContractoServicio.Cliente.GetClientes.Peticion() With {.OidCliente = objSubCliente.SubClientes(0).OidCliente}
                        objPeticionCliente.ParametrosPaginacion.RealizarPaginacion = False
                        Dim objCliente = Cliente.ObtenerClientes(objPeticionCliente)
                        If Not (objCliente IsNot Nothing AndAlso objCliente.Clientes IsNot Nothing AndAlso objCliente.Clientes.Count > 0) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_cliente_naoencontrado"), "Clientes"))
                        End If

                        If objPuntoServicio.ConfigNivelSaldo Is Nothing Then
                            objPuntoServicio.ConfigNivelSaldo = New IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.ConfigNivelMovColeccion
                        End If

                        Dim lstConfigNivelMov As New List(Of IAC.ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov)
                        For Each obj In objPuntoServicio.ConfigNivelSaldo
                            If String.IsNullOrEmpty(obj.oidPtoServicio) Then
                                obj.oidPtoServicio = objPuntoServicio.OidPuntoServicio
                            End If
                            lstConfigNivelMov.Add(obj)
                        Next

                        MaestroCuenta.GrabarConfiguracionesNivelMovimiento(lstConfigNivelMov, objPeticion.CodigoUsuario, objSubCliente.SubClientes(0).OidCliente, objPuntoServicio.OidSubCliente, objPuntoServicio.OidPuntoServicio)
                        Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio", Comon.Util.VersionCompleta.ToString(), $"Se ejecuto GrabarConfiguracionesNivelMovimiento", "")

                        Dim objAccionDatoBancario As New IAC.LogicaNegocio.AccionDatoBancario
                        If objPuntoServicio.PeticionDatosBancarios IsNot Nothing AndAlso objPuntoServicio.PeticionDatosBancarios.DatosBancarios IsNot Nothing AndAlso objPuntoServicio.PeticionDatosBancarios.DatosBancarios.Count > 0 Then
                            For Each peticion In objPuntoServicio.PeticionDatosBancarios.DatosBancarios
                                peticion.IdentificadorCliente = objSubCliente.SubClientes(0).OidCliente
                                peticion.IdentificadorSubCliente = objPuntoServicio.OidSubCliente
                                peticion.IdentificadorPuntoDeServicio = objPuntoServicio.OidPuntoServicio
                            Next

                            Dim objRespuestaDatoBancario = objAccionDatoBancario.SetDatosBancarios(objPuntoServicio.PeticionDatosBancarios)
                            Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio", Comon.Util.VersionCompleta.ToString(), $"Se ejecuto SetDatosBancarios", "")

                            If objRespuestaDatoBancario.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                                errorRegistroDatosBancarios = True
                                Throw New Exception(objRespuestaDatoBancario.MensajeError)
                            End If
                        End If
                    End If

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio", Comon.Util.VersionCompleta.ToString(), $"Finaliza ejecución grabar", "")

                    objRespuestaPuntoServicio.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuestaPuntoServicio.MensajeError = String.Empty

                Catch ex As Excepcion.NegocioExcepcion

                    'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
                    objRespuestaPuntoServicio.CodigoError = ex.Codigo
                    objRespuestaPuntoServicio.MensajeError = ex.Descricao
                    temErro = False

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                    objRespuestaPuntoServicio.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                    objRespuestaPuntoServicio.MensajeError = ex.ToString()
                    temErro = True

                Finally

                    objRespuesta.PuntoServicio.Add(objRespuestaPuntoServicio)

                End Try

            Next

            If temErro Then
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                If errorRegistroDatosBancarios Then
                    objRespuesta.MensajeError = Traduzir("013_msg_ErrorDatosBancarios_PuntoServicio")
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

        Private Shared Sub DefinirCodigoAjeno(oidPuntoServicio As String, codigoAjenoColeccion As IAC.ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase, codigoUsuario As String,
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
                    objItemCast.OidTablaGenesis = oidPuntoServicio
                    codigotablaGenesis = IAC.ContractoServicio.Constantes.COD_PUNTOSERVICIO
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

        Public Shared Function IdentificadoresPuntoServicioTotalizadorSaldo(identificadoresPuntoServicio As List(Of String)) As List(Of String)
            Return AccesoDatos.Genesis.PuntoServicio.IdentificadoresPuntoServicioTotalizadorSaldo(identificadoresPuntoServicio)
        End Function

        Public Shared Function GetPuntoServicioByCodigoAjeno(objPeticion As IAC.ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.Peticion) As IAC.ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.Respuesta

            Dim objRespuesta As New IAC.ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.Respuesta

            Try

                objRespuesta.PuntoServicio = IAC.AccesoDatos.PuntoServicio.GetPuntoServicioByCodigoAjeno(Of IAC.ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.PuntoServicio)(objPeticion.identificadorAjeno, objPeticion.puntoServicioCodigoAjeno)
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

        Private Shared Sub DefinirDirecciones(oidPuntoServicio As String, DireccionColeccion As IAC.ContractoServicio.Direccion.DireccionColeccionBase, codigoUsuario As String)

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

                    objDireciones.oidTablaGenesis = oidPuntoServicio
                    codigotablaGenesis = IAC.ContractoServicio.Constantes.COD_PUNTOSERVICIO
                    objDireciones.codTipoTablaGenesis = (From item In IAC.AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                                       Where item.CodTipoTablaGenesis = codigotablaGenesis
                                                       Select item.Entidade).FirstOrDefault()

                    objAccionDireccion.SetDirecciones(objDireciones)
                Next

            End If

        End Sub
#End Region

        Shared Function ObtenerPuntoServicioJSON(codigo As String, descripcion As String, identificadorPadre As String) As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Return AccesoDatos.Genesis.PuntoServicio.ObtenerPuntoServicioJSON(codigo, descripcion, identificadorPadre)
        End Function

    End Class

End Namespace
