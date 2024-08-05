'Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente
'Imports Prosegur.DbHelper
'Imports Prosegur.Framework.Dicionario.Tradutor
'Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
'Imports Prosegur.Framework.Dicionario
'Imports Prosegur.Global.GesEfectivo.IAC.LogicaNegocio

'Public Class AccionPuntoServicio
'    Implements ContractoServicio.IPuntoServicio

'    Public Function GetPuntoServicio(objPeticion As ContractoServicio.PuntoServicio.GetPuntoServicio.Peticion) As ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta Implements ContractoServicio.IPuntoServicio.GetPuntoServicio

'        Dim objRespuesta As New ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta

'        Try

'            If objPeticion.ParametrosPaginacion Is Nothing Then
'                Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
'            ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
'                Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
'            End If

'            objRespuesta.PuntoServicio = AccesoDatos.PuntoServicio.GetPuntoServicio(Of ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio)(objPeticion, objRespuesta.ParametrosPaginacion)
'            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERRO_DEFAULT
'            objRespuesta.MensajeError = String.Empty
'        Catch ex As Excepcion.NegocioExcepcion
'            'CASO OCORRA ALGUMA EXEÇÃO, TRATA O OBJETO RESPUESTA DA FORMA ADEQUADA
'            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT
'            objRespuesta.MensajeError = ex.Descricao
'            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
'        Catch ex As Exception
'            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
'            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERRO_AMBIENTE_DEFAULT
'            objRespuesta.MensajeError = ex.ToString()
'            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
'        End Try

'        Return objRespuesta

'    End Function

'    Public Function GetPuntoServicioDetalle(objPeticion As ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Peticion) As ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Respuesta Implements ContractoServicio.IPuntoServicio.GetPuntoServicioDetalle

'        Dim objRespuesta As New ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Respuesta

'        Try
'            objRespuesta.PuntoServicio = AccesoDatos.PuntoServicio.GetPuntoServicioDetalle(objPeticion, objRespuesta.ParametrosPaginacion)

'            If objRespuesta.PuntoServicio.Count > 0 Then

'                Dim accionUtilidad As New AccionUtilidad

'                For Each p In objRespuesta.PuntoServicio

'                    Dim objPeticionNivelSaldo As New ContractoServicio.Utilidad.GetConfigNivel.Peticion

'                    objPeticionNivelSaldo.CodCliente = p.CodCliente
'                    objPeticionNivelSaldo.CodSubCliente = p.CodSubCliente
'                    objPeticionNivelSaldo.CodPtoServicio = p.CodPuntoServicio
'                    objPeticionNivelSaldo.BolActivo = True

'                    objPeticionNivelSaldo.ParametrosPaginacion.RealizarPaginacion = False

'                    p.ConfigNivelSaldo = accionUtilidad.GetConfigNivelSaldo(objPeticionNivelSaldo).ConfigNivelMovs
'                Next

'            End If


'            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERRO_DEFAULT
'            objRespuesta.MensajeError = String.Empty
'        Catch ex As Excepcion.NegocioExcepcion
'            objRespuesta.CodigoError = ex.Codigo
'            objRespuesta.MensajeError = ex.Descricao
'        Catch ex As Exception
'            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERRO_AMBIENTE_DEFAULT
'            objRespuesta.MensajeError = ex.ToString()
'            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
'        End Try

'        Return objRespuesta

'    End Function

'    Public Function SetPuntoServicio(objPeticion As ContractoServicio.PuntoServicio.SetPuntoServicio.Peticion) As ContractoServicio.PuntoServicio.SetPuntoServicio.Respuesta Implements ContractoServicio.IPuntoServicio.SetPuntoServicio

'        Dim oidPuntoServicio As String = String.Empty
'        Dim objRespuesta As New ContractoServicio.PuntoServicio.SetPuntoServicio.Respuesta

'        objRespuesta.PuntoServicio = New ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicioColeccion

'        Dim temErro As Boolean = False

'        For Each objPuntoServicio As ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicio In objPeticion.PuntoServicio

'            Dim objRespuestaPuntoServicio As New ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicio

'            objRespuestaPuntoServicio.OidPuntoServicio = objPuntoServicio.OidPuntoServicio
'            objRespuestaPuntoServicio.CodPuntoServicio = objPuntoServicio.CodPuntoServicio
'            objRespuestaPuntoServicio.DesPuntoServicio = objPuntoServicio.DesPuntoServicio

'            Try

'                If objPeticion.BolBaja Then

'                    If String.IsNullOrEmpty(objPuntoServicio.OidPuntoServicio) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "OidPuntoServicio"))
'                    End If

'                    IAC.AccesoDatos.PuntoServicio.BajaPuntoServicio(objPuntoServicio, objPeticion.CodigoUsuario)

'                Else

'                    If Not String.IsNullOrEmpty(objPuntoServicio.OidSubCliente) Then

'                        'Verifica se o cliente enviado está ativo
'                        If Not AccesoDatos.PuntoServicio.VerificaClienteAtivo(objPuntoServicio.OidSubCliente) Then
'                            Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_erro_cliente"), "OidSubCliente"))
'                        End If

'                        'Verifica se o Subcliente enviado esta ativo
'                        If Not AccesoDatos.PuntoServicio.VerificaSubclienteAtivo(objPuntoServicio.OidSubCliente).BolVigente Then
'                            Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_erro_Subcliente"), "OidSubCliente"))
'                        End If

'                    End If

'                    If String.IsNullOrEmpty(objPuntoServicio.OidSubCliente) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "OidSubCliente"))
'                    End If

'                    If String.IsNullOrEmpty(objPuntoServicio.CodPuntoServicio) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodPuntoServicio"))
'                    End If

'                    If String.IsNullOrEmpty(objPuntoServicio.DesPuntoServicio) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesPuntoServicio"))
'                    End If

'                    If String.IsNullOrEmpty(objPuntoServicio.OidTipoPuntoServicio) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "OidTipoPuntoServicio"))
'                    End If

'                    If (Not objPuntoServicio.BolTotalizadorSaldo AndAlso objPuntoServicio.ConfigNivelSaldo.Count = 0) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "NivelSaldo"))
'                    End If

'                    'Validar se o ponto de serviço é totalizador de saldos de outro ponto de serviço
'                    If (Not objPuntoServicio.BolTotalizadorSaldo AndAlso Not String.IsNullOrEmpty(objPuntoServicio.OidPuntoServicio)) Then
'                        Dim BolPuntoServicioTotSaldo As Boolean = IAC.AccesoDatos.PuntoServicio.BuscarPuntoServicioNivelSaldo(objPuntoServicio.OidPuntoServicio)
'                        If BolPuntoServicioTotSaldo Then
'                            Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_punto_servicio_totalizador"), "BolPuntoServicioTotSaldo"))
'                        End If
'                    End If

'                    If objPuntoServicio.OidPuntoServicio <> String.Empty Then
'                        IAC.AccesoDatos.PuntoServicio.ActualizarPuntoServicio(objPuntoServicio, objPeticion.CodigoUsuario)
'                        'Grava os Codigos Ajenos
'                        SetCodigoAjeno(objPuntoServicio.OidPuntoServicio, objPuntoServicio.CodigoAjeno, objPeticion.CodigoUsuario)
'                        'Grava as Direcciones
'                        SetDirecciones(objPuntoServicio.OidPuntoServicio, objPuntoServicio.Direcciones, objPeticion.CodigoUsuario)
'                    Else
'                        oidPuntoServicio = IAC.AccesoDatos.PuntoServicio.AltaPuntoServicio(objPuntoServicio, objPeticion.CodigoUsuario)
'                        objRespuestaPuntoServicio.OidPuntoServicio = oidPuntoServicio
'                        'Grava os Codigos Ajenos
'                        SetCodigoAjeno(oidPuntoServicio, objPuntoServicio.CodigoAjeno, objPeticion.CodigoUsuario)
'                        'Grava as Direcciones
'                        SetDirecciones(oidPuntoServicio, objPuntoServicio.Direcciones, objPeticion.CodigoUsuario)
'                    End If

'                    Dim nivelDefectos = objPuntoServicio.ConfigNivelSaldo.Where(Function(n) n.oidSubCanal Is Nothing AndAlso _
'                                                                                            n.codCliente IsNot Nothing AndAlso _
'                                                                                            n.codSubCliente IsNot Nothing AndAlso _
'                                                                                            n.codPtoServicio IsNot Nothing).ToList()

'                    If (nivelDefectos.Count > 1) Then

'                        For Each defecto In nivelDefectos
'                            If defecto.bolActivo = False Then
'                                MaestroCuenta.DesactivarConfiguracionNivelMovimiento(defecto.oidConfigNivelMovimiento, objPeticion.CodigoUsuario)
'                            End If
'                        Next

'                        nivelDefectos.RemoveAll(Function(n) Not n.bolActivo)

'                        If (nivelDefectos.Count > 1) Then
'                            Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, Traduzir("056_msg_mas_de_uno_defecto"))
'                        End If

'                    End If

'                    ' ---------------- OBTEM OS REGISTROS RELACIONADOS ---------
'                    Dim objPeticionSubCliente As New SubCliente.GetSubClientes.Peticion() With {.OidSubCliente = objPuntoServicio.OidSubCliente}
'                    objPeticionSubCliente.ParametrosPaginacion.RealizarPaginacion = False
'                    Dim objSubCliente = New AccionSubCliente().GetSubClientes(objPeticionSubCliente)
'                    If Not (objSubCliente IsNot Nothing AndAlso objSubCliente.SubClientes IsNot Nothing AndAlso objSubCliente.SubClientes.Count > 0) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_subcliente_naoencontrado"), "SubClientes"))
'                    End If

'                    Dim objPeticionCliente As New Cliente.GetClientes.Peticion() With {.OidCliente = objSubCliente.SubClientes(0).OidCliente}
'                    objPeticionCliente.ParametrosPaginacion.RealizarPaginacion = False
'                    Dim objCliente = New AccionCliente().GetClientes(objPeticionCliente)
'                    If Not (objCliente IsNot Nothing AndAlso objCliente.Clientes IsNot Nothing AndAlso objCliente.Clientes.Count > 0) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("056_msg_cliente_naoencontrado"), "Clientes"))
'                    End If

'                    '  --------------- MAESTRO DE CUENTA --------------- 
'                    ' verifica se ele mesmo é o totalizador de contas
'                    If objPuntoServicio.BolPuntoServicioTotSaldo AndAlso nivelDefectos.Count = 0 Then

'                        Dim defecto As New PuntoServicio.SetPuntoServicio.ConfigNivelMov()
'                        defecto.oidCliente = objCliente.Clientes(0).OidCliente
'                        defecto.oidSubCliente = objSubCliente.SubClientes(0).OidSubCliente

'                        ' verifica se esta sendo criado um registro novo, caso seja passa o objPuntoServicio gerado anteriormente
'                        If objPuntoServicio IsNot Nothing AndAlso String.IsNullOrEmpty(objPuntoServicio.OidPuntoServicio) Then
'                            objPuntoServicio.OidPuntoServicio = oidPuntoServicio
'                        End If

'                        defecto.oidPtoServicio = objPuntoServicio.OidPuntoServicio
'                        defecto.configNivelSaldo = New PuntoServicio.SetPuntoServicio.ConfigNivelSaldo With {.oidCliente = objCliente.Clientes(0).OidCliente, _
'                                                                                                             .oidSubcliente = objSubCliente.SubClientes(0).OidSubCliente, _
'                                                                                                             .oidPtoServicio = objPuntoServicio.OidPuntoServicio}
'                        objPuntoServicio.ConfigNivelSaldo.Add(defecto)

'                    End If

'                    ' cria cada um dos niveis de saldo e movimiento
'                    For Each mov In objPuntoServicio.ConfigNivelSaldo

'                        Dim oidNivelSaldo As String
'                        Dim oidNivelMovimiento As String

'                        ' verifica se já existe uma configuração nivel saldo (SAPR_TCONF_NIVEL_SADO)
'                        Dim temp As Dictionary(Of String, String) = MaestroCuenta.RecuperarConfiguracaoNivelSaldo(mov.configNivelSaldo.oidCliente, mov.configNivelSaldo.oidSubcliente, mov.configNivelSaldo.oidPtoServicio)

'                        If temp.ContainsKey("OID_CONFIG_NIVEL_SALDO") AndAlso Not String.IsNullOrEmpty(temp.Item("OID_CONFIG_NIVEL_SALDO")) Then
'                            oidNivelSaldo = temp.Item("OID_CONFIG_NIVEL_SALDO")
'                        Else
'                            oidNivelSaldo = MaestroCuenta.GenerarConfiguracionNivelSaldo(mov.configNivelSaldo.oidCliente, objPeticion.CodigoUsuario, mov.configNivelSaldo.oidSubcliente, mov.configNivelSaldo.oidPtoServicio)
'                        End If

'                        If (mov.oidPtoServicio Is Nothing) Then
'                            mov.oidPtoServicio = oidPuntoServicio
'                        End If

'                        ' cria o nivel movimento padrão
'                        Dim oids = MaestroCuenta.RecuperarConfiguracaoNivelMovimientoSaldo(mov.oidCliente, mov.oidSubCliente, mov.oidPtoServicio, mov.oidSubCanal)

'                        Dim existe As Boolean = oids.ContainsKey("OID_CONFIG_NIVEL_MOVIMIENTO") AndAlso Not String.IsNullOrEmpty(oids("OID_CONFIG_NIVEL_MOVIMIENTO"))
'                        Dim novoSaldo As Boolean = oids.ContainsKey("OID_CONFIG_NIVEL_SALDO") AndAlso Not String.IsNullOrEmpty(oids("OID_CONFIG_NIVEL_SALDO")) AndAlso oids("OID_CONFIG_NIVEL_SALDO") <> oidNivelSaldo

'                        If (Not existe OrElse novoSaldo) AndAlso mov.bolActivo Then
'                            oidNivelMovimiento = MaestroCuenta.GenerarConfiguracionNivelMovimiento(mov.oidCliente, oidNivelSaldo, objPeticion.CodigoUsuario, mov.oidSubCliente, mov.oidPtoServicio, mov.oidSubCanal)
'                        ElseIf (Not mov.bolActivo AndAlso Not String.IsNullOrEmpty(mov.oidConfigNivelMovimiento)) Then
'                            MaestroCuenta.DesactivarConfiguracionNivelMovimiento(mov.oidConfigNivelMovimiento, objPeticion.CodigoUsuario)
'                        End If

'                    Next

'                    End If

'                objRespuestaPuntoServicio.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERRO_DEFAULT
'                objRespuestaPuntoServicio.MensajeError = String.Empty

'            Catch ex As Excepcion.NegocioExcepcion

'                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
'                objRespuestaPuntoServicio.CodigoError = ex.Codigo
'                objRespuestaPuntoServicio.MensajeError = ex.Descricao
'                temErro = False

'            Catch ex As Exception

'                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
'                objRespuestaPuntoServicio.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERRO_AMBIENTE_DEFAULT
'                objRespuestaPuntoServicio.MensajeError = ex.ToString()
'                temErro = True

'            Finally

'                objRespuesta.PuntoServicio.Add(objRespuestaPuntoServicio)

'            End Try

'        Next

'        If temErro Then
'            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERRO_AMBIENTE_DEFAULT
'            objRespuesta.MensajeError = Tradutor.Traduzir("013_msg_ErroCollecionClientes")
'            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
'        Else
'            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERRO_DEFAULT
'            objRespuesta.MensajeError = String.Empty
'        End If

'        Return objRespuesta

'    End Function

'    Private Sub SetCodigoAjeno(oidPuntoServicio As String, codigoAjenoColeccion As CodigoAjeno.CodigoAjenoColeccionBase, codigoUsuario As String)

'        Dim objAccionAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno

'        If codigoAjenoColeccion.Count > 0 Then
'            Dim objCodigoAjeno As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion
'            objCodigoAjeno.CodigosAjenos = New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion


'            For Each objItem In codigoAjenoColeccion
'                Dim objItemCast As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno
'                Dim codigotablaGenesis As String = String.Empty

'                objItemCast.OidCodigoAjeno = objItem.OidCodigoAjeno
'                objItemCast.BolActivo = objItem.BolActivo
'                objItemCast.BolDefecto = objItem.BolDefecto
'                objItemCast.CodAjeno = objItem.CodAjeno
'                objItemCast.CodIdentificador = objItem.CodIdentificador
'                objItemCast.DesAjeno = objItem.DesAjeno
'                If String.IsNullOrEmpty(objItemCast.DesUsuarioCreacion) Then
'                    objItemCast.DesUsuarioCreacion = codigoUsuario
'                    objItemCast.GmtCreacion = DateTime.Now
'                End If
'                objItemCast.DesUsuarioModificacion = codigoUsuario
'                objItemCast.GmtModificacion = DateTime.Now
'                objItemCast.OidTablaGenesis = oidPuntoServicio
'                codigotablaGenesis = ContractoServicio.Constantes.COD_PUNTOSERVICIO
'                objItemCast.CodTipoTablaGenesis = (From item In AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
'                                                   Where item.CodTipoTablaGenesis = codigotablaGenesis
'                                                   Select item.Entidade).FirstOrDefault()

'                objCodigoAjeno.CodigosAjenos.Add(objItemCast)

'            Next

'            objAccionAjeno.SetCodigosAjenos(objCodigoAjeno)

'        End If
'    End Sub

'    ''' <summary>
'    ''' Metodo Test
'    ''' </summary>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    ''' <history>
'    ''' [danielnunes] 18/06/2013 - Criado
'    ''' </history>
'    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IPuntoServicio.Test
'        Dim objRespuesta As New ContractoServicio.Test.Respuesta

'        Try

'            AccesoDatos.Test.TestarConexao()

'            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERRO_DEFAULT
'            objRespuesta.MensajeError = Tradutor.Traduzir("021_SemErro")

'        Catch ex As Excepcion.NegocioExcepcion

'            objRespuesta.CodigoError = ex.Codigo
'            objRespuesta.MensajeError = ex.Descricao


'        Catch ex As Exception

'            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERRO_AMBIENTE_DEFAULT
'            objRespuesta.MensajeError = ex.ToString
'            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

'        End Try

'        Return objRespuesta

'    End Function

'#Region "DIRECCIONES"

'    Private Sub SetDirecciones(oidPuntoServicio As String, DireccionColeccion As Direccion.DireccionColeccionBase, codigoUsuario As String)

'        Dim objAccionDireccion As New IAC.LogicaNegocio.AccionDireccion

'        If DireccionColeccion.Count > 0 Then

'            For Each objItem In DireccionColeccion
'                Dim objDireciones As New ContractoServicio.Direccion.SetDirecciones.Peticion
'                Dim codigotablaGenesis As String = String.Empty

'                objDireciones.bolBaja = objItem.bolBaja
'                objDireciones.codFiscal = objItem.codFiscal
'                objDireciones.codPostal = objItem.codPostal
'                objDireciones.desCampoAdicional1 = objItem.desCampoAdicional1
'                objDireciones.desCampoAdicional2 = objItem.desCampoAdicional2
'                objDireciones.desCampoAdicional3 = objItem.desCampoAdicional3
'                objDireciones.desCategoriaAdicional1 = objItem.desCategoriaAdicional1
'                objDireciones.desCategoriaAdicional2 = objItem.desCategoriaAdicional2
'                objDireciones.desCategoriaAdicional3 = objItem.desCategoriaAdicional3
'                objDireciones.desCiudad = objItem.desCiudad
'                objDireciones.desDireccionLinea1 = objItem.desDireccionLinea1
'                objDireciones.desDireccionLinea2 = objItem.desDireccionLinea2
'                objDireciones.desEmail = objItem.desEmail
'                objDireciones.desNumeroTelefono = objItem.desNumeroTelefono
'                objDireciones.desPais = objItem.desPais
'                objDireciones.desProvincia = objItem.desProvincia
'                objDireciones.oidDireccion = objItem.oidDireccion
'                objDireciones.desUsuarioModificacion = codigoUsuario
'                objDireciones.gmtModificacion = DateTime.Now
'                If String.IsNullOrEmpty(objDireciones.desUsuarioCreacion) Then
'                    objDireciones.desUsuarioCreacion = codigoUsuario
'                    objDireciones.gmtCreacion = DateTime.Now
'                End If

'                objDireciones.oidTablaGenesis = oidPuntoServicio
'                codigotablaGenesis = ContractoServicio.Constantes.COD_PUNTOSERVICIO
'                objDireciones.codTipoTablaGenesis = (From item In AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
'                                                   Where item.CodTipoTablaGenesis = codigotablaGenesis
'                                                   Select item.Entidade).FirstOrDefault()

'                objAccionDireccion.SetDirecciones(objDireciones)
'            Next


'        End If

'    End Sub
'#End Region

'End Class
