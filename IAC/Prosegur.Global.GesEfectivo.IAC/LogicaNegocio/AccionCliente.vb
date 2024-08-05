'Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente
'Imports Prosegur.DbHelper
'Imports Prosegur.Framework.Dicionario.Tradutor
'Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
'Imports Prosegur.Framework.Dicionario
'Imports Prosegur.Global.GesEfectivo.IAC.LogicaNegocio

'Public Class AccionCliente
'    Implements ICliente

'    Public Function GetClientes(objPeticion As ContractoServicio.Cliente.GetClientes.Peticion) As ContractoServicio.Cliente.GetClientes.Respuesta Implements ContractoServicio.ICliente.GetClientes

'        Dim objRespuesta As New ContractoServicio.Cliente.GetClientes.Respuesta

'        Try

'            If objPeticion.ParametrosPaginacion Is Nothing Then
'                Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
'            ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
'                Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
'            End If

'            objRespuesta.Clientes = AccesoDatos.Cliente.GetClientes(Of ContractoServicio.Cliente.GetClientes.Cliente)(objPeticion, objRespuesta.ParametrosPaginacion)
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

'    Public Function GetClientesDetalle(objPeticion As ContractoServicio.Cliente.GetClientesDetalle.Peticion) As ContractoServicio.Cliente.GetClientesDetalle.Respuesta Implements ContractoServicio.ICliente.GetClientesDetalle

'        Dim objRespuesta As New ContractoServicio.Cliente.GetClientesDetalle.Respuesta

'        Try
'            objRespuesta.Clientes = AccesoDatos.Cliente.GetClientesDetalle(objPeticion, objRespuesta.ParametrosPaginacion)

'            If objRespuesta.Clientes.Count > 0 Then

'                Dim accionUtilidad As New AccionUtilidad

'                For Each c In objRespuesta.Clientes

'                    Dim objPeticionNivelSaldo As New ContractoServicio.Utilidad.GetConfigNivel.Peticion

'                    objPeticionNivelSaldo.CodCliente = c.CodCliente
'                    objPeticionNivelSaldo.ParametrosPaginacion.RealizarPaginacion = False
'                    objPeticionNivelSaldo.BolActivo = True

'                    c.ConfigNivelMov = accionUtilidad.GetConfigNivelSaldo(objPeticionNivelSaldo).ConfigNivelMovs
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

'    Public Function SetClientes(objPeticion As ContractoServicio.Cliente.SetClientes.Peticion) As ContractoServicio.Cliente.SetClientes.Respuesta Implements ContractoServicio.ICliente.SetClientes

'        Dim oidCliente As String = String.Empty
'        Dim objRespuesta As New ContractoServicio.Cliente.SetClientes.Respuesta

'        objRespuesta.Clientes = New ContractoServicio.Cliente.SetClientes.ClienteColeccion

'        Dim temErro As Boolean = False

'        For Each objCliente As ContractoServicio.Cliente.SetClientes.Cliente In objPeticion.Clientes

'            Dim objRespuestaCliente As New ContractoServicio.Cliente.SetClientes.Cliente

'            objRespuestaCliente.OidCliente = objCliente.OidCliente
'            objRespuestaCliente.CodCliente = objCliente.CodCliente
'            objRespuestaCliente.DesCliente = objCliente.DesCliente

'            Try

'                If objPeticion.BolBaja Then

'                    If String.IsNullOrEmpty(objCliente.OidCliente) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "OidCliente"))
'                    End If

'                    IAC.AccesoDatos.Cliente.BajaCliente(objCliente, objPeticion.CodigoUsuario)

'                Else

'                    ' ---------------- CAMPOS OBRIGATORIOS ------------
'                    If String.IsNullOrEmpty(objCliente.CodCliente) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodCliente"))
'                    End If

'                    If String.IsNullOrEmpty(objCliente.DesCliente) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesCliente"))
'                    End If

'                    If String.IsNullOrEmpty(objCliente.OidTipoCliente) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "OidTipoCliente"))
'                    End If

'                    If (Not objCliente.BolClienteTotSaldo AndAlso objCliente.ConfigNivelSaldo.Count = 0) Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "NivelSaldo"))
'                    End If

'                    'Validar se o cliente é totalizador de saldos de outro  cliente
'                    If (Not objCliente.BolTotalizadorSaldo AndAlso Not String.IsNullOrEmpty(objCliente.OidCliente)) Then
'                        Dim BolClienteTotSaldo As Boolean = IAC.AccesoDatos.Cliente.BuscarClienteNivelSaldo(objCliente.OidCliente)
'                        If BolClienteTotSaldo Then
'                            Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, String.Format(Traduzir("055_msg_cliente_totalizador"), "BolClienteTotSaldo"))
'                        End If
'                    End If

'                    ' ------------- VERIFICA TIPO DE CLIENTE ------------
'                    Dim accionTipoCliente As New AccionTipoCliente
'                    Dim peticionTipoCliente As New TipoCliente.GetTiposClientes.Peticion
'                    peticionTipoCliente.codTipoCliente = objCliente.CodTipoCliente
'                    peticionTipoCliente.ParametrosPaginacion.RealizarPaginacion = False

'                    Dim tipoCliente = accionTipoCliente.getTiposClientes(peticionTipoCliente).TipoCliente.FirstOrDefault()
'                    If Not tipoCliente.bolActivo Then
'                        Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, Traduzir("055_msg_tipo_cliente_inactivo"))
'                    End If

'                    ' ------------- ALTERAÇÃO ------------
'                    If objCliente.OidCliente <> String.Empty Then
'                        'Cria uma transação, caso não consiga atualiza o cliente, subcliente e punto de servicio dá um rollback.
'                        Dim objTransacao As New Transacao(IAC.AccesoDatos.Constantes.CONEXAO_GE)
'                        IAC.AccesoDatos.Cliente.ActualizarCliente(objCliente, objPeticion.CodigoUsuario, objTransacao)
'                        objTransacao.RealizarTransacao()
'                        'Grava os Codigos Ajenos
'                        SetCodigoAjeno(objCliente.OidCliente, objCliente.CodigoAjeno, objPeticion.CodigoUsuario)
'                        'Grava as Direcciones
'                        SetDirecciones(objCliente.OidCliente, objCliente.Direcciones, objPeticion.CodigoUsuario)
'                    Else
'                        oidCliente = IAC.AccesoDatos.Cliente.AltaCliente(objCliente, objPeticion.CodigoUsuario)
'                        objCliente.OidCliente = oidCliente
'                        objRespuestaCliente.OidCliente = oidCliente
'                        'Grava os Codigos Ajenos
'                        SetCodigoAjeno(oidCliente, objCliente.CodigoAjeno, objPeticion.CodigoUsuario)
'                        'Grava as Direcciones
'                        SetDirecciones(oidCliente, objCliente.Direcciones, objPeticion.CodigoUsuario)
'                    End If

'                    Dim nivelDefectos = objCliente.ConfigNivelSaldo.Where(Function(n) n.oidSubCanal Is Nothing AndAlso _
'                                                                                      n.codCliente IsNot Nothing AndAlso _
'                                                                                      n.codSubCliente Is Nothing AndAlso _
'                                                                                      n.codPtoServicio Is Nothing).ToList()

'                    If (nivelDefectos.Count > 1) Then

'                        For Each defecto In nivelDefectos
'                            If defecto.bolActivo = False Then
'                                MaestroCuenta.DesactivarConfiguracionNivelMovimiento(defecto.oidConfigNivelMovimiento, objPeticion.CodigoUsuario)
'                            End If
'                        Next

'                        nivelDefectos.RemoveAll(Function(n) Not n.bolActivo)

'                        If nivelDefectos.Count > 1 Then
'                            Throw New IAC.Excepcion.NegocioExcepcion(IAC.Excepcion.Constantes.CONST_CODIGO_ERRO_NEGOCIO_DEFAULT, Traduzir("055_msg_mas_de_uno_defecto"))
'                        End If

'                    End If

'                    '  --------------- MAESTRO DE CUENTA --------------- 
'                    ' verifica se ele mesmo é o totalizador de contas
'                    If objCliente.BolClienteTotSaldo AndAlso nivelDefectos.Count = 0 Then
'                        Dim defecto As New Cliente.SetClientes.ConfigNivelMov()

'                        ' verifica se esta sendo criado um registro novo, caso seja passa o oidCliente gerado anteriormente
'                        If objCliente IsNot Nothing AndAlso String.IsNullOrEmpty(objCliente.OidCliente) Then
'                            objCliente.OidCliente = oidCliente
'                        End If

'                        defecto.oidCliente = objCliente.OidCliente
'                        defecto.configNivelSaldo = New Cliente.SetClientes.ConfigNivelSaldo() With {.oidCliente = objCliente.OidCliente}
'                        objCliente.ConfigNivelSaldo.Add(defecto)
'                    End If

'                    ' cria cada um dos niveis de saldo e movimiento
'                    For Each mov In objCliente.ConfigNivelSaldo

'                        Dim oidNivelSaldo As String
'                        Dim oidNivelMovimiento As String

'                        ' verifica se já existe uma configuração nivel saldo (SAPR_TCONF_NIVEL_SADO)
'                        Dim temp As Dictionary(Of String, String) = MaestroCuenta.RecuperarConfiguracaoNivelSaldo(mov.configNivelSaldo.oidCliente, mov.configNivelSaldo.oidSubcliente, mov.configNivelSaldo.oidPtoServicio)

'                        If temp.ContainsKey("OID_CONFIG_NIVEL_SALDO") AndAlso Not String.IsNullOrEmpty(temp("OID_CONFIG_NIVEL_SALDO")) Then
'                            oidNivelSaldo = temp("OID_CONFIG_NIVEL_SALDO")
'                        Else
'                            oidNivelSaldo = MaestroCuenta.GenerarConfiguracionNivelSaldo(mov.configNivelSaldo.oidCliente, objPeticion.CodigoUsuario, mov.configNivelSaldo.oidSubcliente, mov.configNivelSaldo.oidPtoServicio)
'                        End If

'                        ' verifica se o cliente e novo, caso seja novo pegar o oid da variavel oidCliente
'                        If (mov.oidCliente Is Nothing) Then
'                            mov.oidCliente = oidCliente
'                        End If

'                        ' cria o nivel movimento padrão
'                        Dim oids = MaestroCuenta.RecuperarConfiguracaoNivelMovimientoSaldo(mov.oidCliente, Nothing, Nothing, mov.oidSubCanal)

'                        Dim existe As Boolean = oids.ContainsKey("OID_CONFIG_NIVEL_MOVIMIENTO") AndAlso Not String.IsNullOrEmpty(oids("OID_CONFIG_NIVEL_MOVIMIENTO"))
'                        Dim novoSaldo As Boolean = oids.ContainsKey("OID_CONFIG_NIVEL_SALDO") AndAlso Not String.IsNullOrEmpty(oids("OID_CONFIG_NIVEL_SALDO")) AndAlso oids("OID_CONFIG_NIVEL_SALDO") <> oidNivelSaldo

'                        If (Not existe OrElse novoSaldo) AndAlso mov.bolActivo Then
'                            oidNivelMovimiento = MaestroCuenta.GenerarConfiguracionNivelMovimiento(mov.oidCliente, oidNivelSaldo, objPeticion.CodigoUsuario, Nothing, Nothing, mov.oidSubCanal)
'                        ElseIf (Not mov.bolActivo AndAlso Not String.IsNullOrEmpty(mov.oidConfigNivelMovimiento)) Then
'                            MaestroCuenta.DesactivarConfiguracionNivelMovimiento(mov.oidConfigNivelMovimiento, objPeticion.CodigoUsuario)
'                        End If

'                    Next

'                End If

'                objRespuestaCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERRO_DEFAULT
'                objRespuestaCliente.MensajeError = String.Empty

'            Catch ex As Excepcion.NegocioExcepcion

'                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
'                objRespuestaCliente.CodigoError = ex.Codigo
'                objRespuestaCliente.MensajeError = ex.Descricao
'                temErro = False

'            Catch ex As Exception

'                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
'                objRespuestaCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERRO_AMBIENTE_DEFAULT
'                objRespuestaCliente.MensajeError = ex.ToString()
'                temErro = True

'            Finally

'                objRespuesta.Clientes.Add(objRespuestaCliente)

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

'    ''' <summary>
'    ''' Metodo Test
'    ''' </summary>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    ''' <history>
'    ''' [danielnunes] 18/06/2013 - Criado
'    ''' </history>
'    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ICliente.Test

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

'#Region "CODIGO AJENO"

'    Private Sub SetCodigoAjeno(oidCliente As String, codigoAjenoColeccion As CodigoAjeno.CodigoAjenoColeccionBase, codigoUsuario As String)

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
'                objItemCast.OidTablaGenesis = oidCliente
'                codigotablaGenesis = ContractoServicio.Constantes.COD_CLIENTE
'                objItemCast.CodTipoTablaGenesis = (From item In AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
'                                                   Where item.CodTipoTablaGenesis = codigotablaGenesis
'                                                   Select item.Entidade).FirstOrDefault()

'                objCodigoAjeno.CodigosAjenos.Add(objItemCast)

'            Next

'            objAccionAjeno.SetCodigosAjenos(objCodigoAjeno)

'        End If

'    End Sub

'#End Region

'#Region "DIRECCIONES"

'    Private Sub SetDirecciones(oidCliente As String, DireccionColeccion As Direccion.DireccionColeccionBase, codigoUsuario As String)

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

'                objDireciones.oidTablaGenesis = oidCliente
'                codigotablaGenesis = ContractoServicio.Constantes.COD_CLIENTE
'                objDireciones.codTipoTablaGenesis = (From item In AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
'                                                   Where item.CodTipoTablaGenesis = codigotablaGenesis
'                                                   Select item.Entidade).FirstOrDefault()

'                objAccionDireccion.SetDirecciones(objDireciones)
'            Next


'        End If

'    End Sub
'#End Region

'End Class