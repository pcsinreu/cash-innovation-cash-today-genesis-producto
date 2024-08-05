Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Saldo

Namespace Genesis

    Public Class Cliente

        Public Shared Function ObtenerIdentificadorCliente(CodigoCliente As String) As String
            Return AccesoDatos.Genesis.Cliente.ObtenerIdentificadorCliente(CodigoCliente)
        End Function

        Public Shared Function RecuperarClienteTotalizadorSaldo(Peticion As ContractoServicio.Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo.Peticion) As ContractoServicio.Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo.Respuesta

            Dim objRespuesta As New ContractoServicio.Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo.Respuesta

            Try

                objRespuesta.ClienteSaldo = AccesoDatos.Genesis.Cliente.RecuperarClienteTotalizadorSaldo(Peticion.CodigoCliente,
                                                                                                         Peticion.CodigoSubCliente,
                                                                                                         Peticion.CodigoPuntoServicio,
                                                                                                         Peticion.CodigoSubCanal)

            Catch ex As Excepcion.NegocioExcepcion
                'CASO OCORRA ALGUMA EXEÇÃO, TRATA O OBJETO RESPUESTA DA FORMA ADEQUADA
                objRespuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                objRespuesta.Excepciones.Add(ex.ToString())
            End Try

            Return objRespuesta
        End Function

        Public Shared Function ObtenerClientes(objPeticion As IAC.ContractoServicio.Cliente.GetClientes.Peticion, Optional completo As Boolean = False, Optional exato As Boolean = False) As IAC.ContractoServicio.Cliente.GetClientes.Respuesta

            Dim objRespuesta As New IAC.ContractoServicio.Cliente.GetClientes.Respuesta

            Try

                If objPeticion.ParametrosPaginacion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
                ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
                End If

                objRespuesta.Clientes = IAC.AccesoDatos.Cliente.GetClientes(Of IAC.ContractoServicio.Cliente.GetClientes.Cliente)(objPeticion, objRespuesta.ParametrosPaginacion, completo, exato)
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

        Public Shared Function ObtenerClientesJSON(codigo As String, descripcion As String, esbanco As Boolean) As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Return AccesoDatos.Genesis.Cliente.ObtenerClientesJSON(codigo, descripcion, esbanco)
        End Function


        Public Shared Function ObtenerClientesDetalle(objPeticion As IAC.ContractoServicio.Cliente.GetClientesDetalle.Peticion) As IAC.ContractoServicio.Cliente.GetClientesDetalle.Respuesta

            Dim objRespuesta As New IAC.ContractoServicio.Cliente.GetClientesDetalle.Respuesta

            Try
                objRespuesta.Clientes = IAC.AccesoDatos.Cliente.GetClientesDetalle(objPeticion, objRespuesta.ParametrosPaginacion)

                If objRespuesta.Clientes.Count > 0 Then

                    Dim accionUtilidad As New IAC.LogicaNegocio.AccionUtilidad

                    For Each c In objRespuesta.Clientes

                        Dim objPeticionNivelSaldo As New IAC.ContractoServicio.Utilidad.GetConfigNivel.Peticion

                        objPeticionNivelSaldo.CodCliente = c.CodCliente
                        objPeticionNivelSaldo.ParametrosPaginacion.RealizarPaginacion = False
                        objPeticionNivelSaldo.BolActivo = True

                        c.ConfigNivelMov = accionUtilidad.GetConfigNivelSaldo(objPeticionNivelSaldo).ConfigNivelMovs
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

        Public Shared Function grabarClientes(objPeticion As IAC.ContractoServicio.Cliente.SetClientes.Peticion) As IAC.ContractoServicio.Cliente.SetClientes.Respuesta

            Dim oidCliente As String = String.Empty
            Dim objRespuesta As New IAC.ContractoServicio.Cliente.SetClientes.Respuesta
            Dim objRespuestaValidacaoCliente As New IAC.ContractoServicio.Utilidad.VerificarDescripcionCliente.Respuesta
            Dim codigoAjenoPROFAT As String = Nothing
            Dim objCodigoAjenoPROFAT As Clases.CodigoAjeno = Nothing

            objRespuesta.Clientes = New IAC.ContractoServicio.Cliente.SetClientes.ClienteColeccion

            Dim temErro As Boolean = False
            Dim errorRegistroDatosBancarios As Boolean = False

            For Each objCliente As IAC.ContractoServicio.Cliente.SetClientes.Cliente In objPeticion.Clientes

                Dim objRespuestaCliente As New IAC.ContractoServicio.Cliente.SetClientes.Cliente
                Try
                    'Recupera o código ajeno PROFAT informado
                    If objCliente.CodigoAjeno IsNot Nothing AndAlso objCliente.CodigoAjeno.Count > 0 Then
                        'Procura na lista código ajeno PROFAT
                        Dim objCodAjeno = objCliente.CodigoAjeno.Find(Function(a) a.CodIdentificador = IAC.AccesoDatos.Constantes.ConstantePROFAT)

                        If objCodAjeno IsNot Nothing Then

                            codigoAjenoPROFAT = objCodAjeno.CodAjeno

                            'Recupera o código ajeno pelo código recebido
                            Dim lstCodAjeno As List(Of Clases.CodigoAjeno) = AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(String.Empty,
                                                                                                                                  objCodAjeno.CodAjeno,
                                                                                                                                  IAC.AccesoDatos.Constantes.ConstantePROFAT,
                                                                                                                                  IAC.AccesoDatos.Constantes.ConstanteCliente).ToList()
                            If lstCodAjeno IsNot Nothing AndAlso lstCodAjeno.Count > 0 Then
                                objCodigoAjenoPROFAT = lstCodAjeno.First
                            End If

                        End If
                    End If

                    'Se existir código ajeno PROFAT utiliza ele como base
                    If objCodigoAjenoPROFAT IsNot Nothing Then

                        objCliente.OidCliente = objCodigoAjenoPROFAT.IdentificadorTablaGenesis

                        'Verifica se já existe algum outro cliente com o mesmo código cliente recebido
                        Dim oidClienteCodigo As String = AccesoDatos.Genesis.Cliente.ObtenerIdentificadorCliente(objCliente.CodCliente)
                        If Not String.IsNullOrEmpty(oidClienteCodigo) AndAlso objCliente.OidCliente <> oidClienteCodigo Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("055_msg_erro_cliente_codigo"), objCliente.CodCliente))
                        End If

                    ElseIf String.IsNullOrEmpty(objCliente.OidCliente) AndAlso Not String.IsNullOrEmpty(objCliente.CodCliente) Then
                        objCliente.OidCliente = AccesoDatos.Genesis.Cliente.ObtenerIdentificadorCliente(objCliente.CodCliente)

                        If Not String.IsNullOrEmpty(objCliente.OidCliente) Then
                            'Recupera o código ajeno pelo Id do cliente
                            Dim lstCodAjeno As List(Of Clases.CodigoAjeno) = AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(objCliente.OidCliente,
                                                                                                                                String.Empty,
                                                                                                                                IAC.AccesoDatos.Constantes.ConstantePROFAT,
                                                                                                                                IAC.AccesoDatos.Constantes.ConstanteCliente).ToList()
                            'Se já existir código ajeno PROFAT cadastrado e o código recebido for diferente
                            If Not String.IsNullOrEmpty(codigoAjenoPROFAT) _
                                    AndAlso lstCodAjeno IsNot Nothing _
                                        AndAlso lstCodAjeno.Count > 0 _
                                            AndAlso lstCodAjeno.First.Codigo <> codigoAjenoPROFAT Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("055_msg_erro_cliente_codigo"), objCliente.CodCliente))
                            End If
                        End If

                    End If

                    objRespuestaCliente.OidCliente = objCliente.OidCliente
                    objRespuestaCliente.CodCliente = objCliente.CodCliente
                    objRespuestaCliente.DesCliente = objCliente.DesCliente
                    Dim clienteAnterior = AccesoDatos.Genesis.Cliente.ObtenerCliente(objCliente.CodCliente)

                    If objPeticion.BolBaja Then

                        If String.IsNullOrEmpty(objCliente.OidCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "OidCliente"))
                        End If

                        IAC.AccesoDatos.Cliente.BajaCliente(objCliente, objPeticion.CodigoUsuario)
                        If objPeticion.BolEliminaCodigosAjenos Then
                            Dim objAccionAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno
                            objAccionAjeno.BorrarCodigosAjenos(objRespuestaCliente.OidCliente, "GEPR_TCLIENTE", Nothing)
                        End If
                    Else

                        ' ---------------- CAMPOS OBRIGATORIOS ------------
                        If String.IsNullOrEmpty(objCliente.CodCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodCliente"))
                        End If

                        If String.IsNullOrEmpty(objCliente.DesCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesCliente"))
                        End If


                        'Validar se o cliente é totalizador de saldos de outro  cliente
                        If (Not objCliente.BolClienteTotSaldo AndAlso Not String.IsNullOrEmpty(objCliente.OidCliente)) Then
                            Dim BolClienteTotSaldo As Boolean = IAC.AccesoDatos.Cliente.BuscarClienteNivelSaldo(objCliente.OidCliente)
                            If BolClienteTotSaldo Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("055_msg_cliente_totalizador"), "BolClienteTotSaldo"))
                            End If
                        End If

                        ' ------------- VERIFICA TIPO DE CLIENTE ------------
                        'If Not String.IsNullOrEmpty(objCliente.CodTipoCliente) Then
                        '    Dim accionTipoCliente As New IAC.LogicaNegocio.AccionTipoCliente
                        '    Dim peticionTipoCliente As New IAC.ContractoServicio.TipoCliente.GetTiposClientes.Peticion
                        '    peticionTipoCliente.codTipoCliente = objCliente.CodTipoCliente
                        '    peticionTipoCliente.ParametrosPaginacion.RealizarPaginacion = False

                        '    Dim tipoCliente = accionTipoCliente.getTiposClientes(peticionTipoCliente).TipoCliente.FirstOrDefault()
                        '    If tipoCliente Is Nothing OrElse Not tipoCliente.bolActivo Then
                        '        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("055_msg_tipo_cliente_inactivo"))
                        '    End If
                        'End If

                        ' ------------- ALTERAÇÃO ------------
                        If objCliente.OidCliente <> String.Empty Then
                            'Cria uma transação, caso não consiga atualiza o cliente, subcliente e punto de servicio dá um rollback.
                            Dim objTransacao As New Transacao(IAC.AccesoDatos.Constantes.CONEXAO_GE)




                            Dim cliente = AccesoDatos.Genesis.Cliente.ObtenerCliente(objCliente.CodCliente)

                            Dim ReplicarVigenteSubclientePtoServicio = False

                            If cliente IsNot Nothing Then

                                If cliente.EstaActivo <> objCliente.BolVigente Then
                                    ReplicarVigenteSubclientePtoServicio = True
                                End If

                            End If


                            IAC.AccesoDatos.Cliente.ActualizarCliente(objCliente, objPeticion.CodigoUsuario, objTransacao, ReplicarVigenteSubclientePtoServicio)
                            objTransacao.RealizarTransacao()
                            'Grava os Codigos Ajenos
                            DefinirCodigoAjeno(objCliente.OidCliente, objCliente.CodigoAjeno, objPeticion.CodigoUsuario)
                            'Grava as Direcciones
                            DefinirDirecciones(objCliente.OidCliente, objCliente.Direcciones, objPeticion.CodigoUsuario)
                        Else
                            'La descripción del cliente es única. Entonces en el movimiento de alta se debe validar si la descripción informada existe o no en la base de datos. 
                            'chama o metodo VerificarDescripcionCliente no acesso dados
                            objRespuestaValidacaoCliente.Existe = IAC.AccesoDatos.Cliente.VerificarDescripcionCliente(objCliente.DesCliente)

                            If Not objRespuestaValidacaoCliente.Existe Then

                                oidCliente = IAC.AccesoDatos.Cliente.AltaCliente(objCliente, objPeticion.CodigoUsuario)
                                objCliente.OidCliente = oidCliente
                                objRespuestaCliente.OidCliente = oidCliente
                                'Grava os Codigos Ajenos
                                DefinirCodigoAjeno(oidCliente, objCliente.CodigoAjeno, objPeticion.CodigoUsuario)
                                'Grava as Direcciones
                                DefinirDirecciones(oidCliente, objCliente.Direcciones, objPeticion.CodigoUsuario)
                            Else
                                '¡Ya existe cliente con la descripción {0}!
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("055_msg_erro_Cliente_ya_existe"), objCliente.DesCliente))
                            End If

                        End If

                        If objCliente.ConfigNivelSaldo Is Nothing Then
                            objCliente.ConfigNivelSaldo = New IAC.ContractoServicio.Cliente.SetClientes.ConfigNivelMovColeccion
                        End If

                        Dim lstConfigNivelMov As New List(Of IAC.ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov)
                        For Each obj In objCliente.ConfigNivelSaldo

                            If String.IsNullOrEmpty(obj.oidCliente) Then
                                obj.oidCliente = objCliente.OidCliente

                                If obj.configNivelSaldo IsNot Nothing AndAlso String.IsNullOrEmpty(obj.configNivelSaldo.oidCliente) Then
                                    obj.configNivelSaldo.oidCliente = objCliente.OidCliente
                                End If
                            End If
                            lstConfigNivelMov.Add(obj)
                        Next

                        MaestroCuenta.GrabarConfiguracionesNivelMovimiento(lstConfigNivelMov, objPeticion.CodigoUsuario, objCliente.OidCliente)

                        Dim objAccionDatoBancario As New IAC.LogicaNegocio.AccionDatoBancario

                        If objCliente.PeticionDatosBancarios IsNot Nothing AndAlso objCliente.PeticionDatosBancarios.DatosBancarios IsNot Nothing AndAlso objCliente.PeticionDatosBancarios.DatosBancarios.Count > 0 Then
                            For Each peticion In objCliente.PeticionDatosBancarios.DatosBancarios
                                peticion.IdentificadorCliente = objCliente.OidCliente
                            Next

                            Dim objRespuestaDatoBancario = objAccionDatoBancario.SetDatosBancarios(objCliente.PeticionDatosBancarios)
                            If objRespuestaDatoBancario.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                                errorRegistroDatosBancarios = True
                                Throw New Exception(objRespuestaDatoBancario.MensajeError)
                            End If
                        End If
                    End If

                   If clienteAnterior IsNot Nothing Then

                     'Borrar SaldoHistorico
                     Dim peticionActualizarSaldosHistorico = New ActualizarSaldosHistoricoCliente.Peticion With {
                             .IdentificadorCliente = objCliente.OidCliente,
                             .IdentificadorPais = "",
                             .Usuario = objPeticion.CodigoUsuario
                             }

                     If clienteAnterior.CodFechaSaldoHistorico <> objCliente.CodFechaSaldoHistorico Then
                         'Borrar SaldoHistorico
                         Dim peticionBorrarSaldosHistorico = New BorrarSaldosHistoricoCliente.Peticion With {
                             .IdentificadorCliente = objCliente.OidCliente,
                             .IdentificadorPais = "",
                             .Usuario = objPeticion.CodigoUsuario
                             }

                         LogicaNegocio.Integracion.AccionRecuperarSaldosHistorico.EjecutarBorrarSaldosHistorico(peticionBorrarSaldosHistorico)

                         If objCliente.BolGrabaSaldoHistorico Then
                             'Actualizar SaldoHistorico
                             LogicaNegocio.Integracion.AccionRecuperarSaldosHistorico.EjecutarActualizarSaldosHistoricoCliente(peticionActualizarSaldosHistorico)
                         End If
                     ElseIf Not clienteAnterior.BolGrabaSaldoHistorico AndAlso objCliente.BolGrabaSaldoHistorico Then
                         'Actualizar SaldoHistorico
                         LogicaNegocio.Integracion.AccionRecuperarSaldosHistorico.EjecutarActualizarSaldosHistoricoCliente(peticionActualizarSaldosHistorico)
                     End If
                    End If


                    objRespuestaCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuestaCliente.MensajeError = String.Empty

                Catch ex As Excepcion.NegocioExcepcion

                    'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
                    objRespuestaCliente.CodigoError = ex.Codigo
                    objRespuestaCliente.MensajeError = ex.Descricao
                    temErro = True

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                    objRespuestaCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                    objRespuestaCliente.MensajeError = ex.ToString()
                    temErro = True

                Finally

                    objRespuesta.Clientes.Add(objRespuestaCliente)

                End Try

            Next

            If temErro Then
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                If errorRegistroDatosBancarios Then
                    objRespuesta.MensajeError = Traduzir("013_msg_ErrorDatosBancarios_Cliente")
                Else
                    objRespuesta.MensajeError = Traduzir("013_msg_ErroCollecionClientes")
                End If
                objRespuesta.NombreServidorBD = IAC.AccesoDatos.Util.RetornaNomeServidorBD
            Else
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty
            End If

            Return objRespuesta

        End Function

        Public Shared Function IdentificadoresClienteTotalizadorSaldo(identificadoresCliente As List(Of String)) As List(Of String)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Cliente.IdentificadoresClienteTotalizadorSaldo(identificadoresCliente)
        End Function

        Public Shared Function IdentificadoresClienteOuSubClienteouPuntoServicioTotalizadorSaldo(identificadoresCliente As List(Of String)) As List(Of String)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Cliente.IdentificadoresClienteOuSubClienteouPuntoServicioTotalizadorSaldo(identificadoresCliente)
        End Function
#Region "CODIGO AJENO"

        Private Shared Sub DefinirCodigoAjeno(oidCliente As String, codigoAjenoColeccion As IAC.ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase, codigoUsuario As String)

            Dim objAccionAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno


            objAccionAjeno.BorrarCodigosAjenos(oidCliente, "GEPR_TCLIENTE", Nothing)

            If codigoAjenoColeccion.Count > 0 Then
                Dim objCodigoAjeno As New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion
                objCodigoAjeno.CodigosAjenos = New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion


                For Each objItem In codigoAjenoColeccion
                    Dim objItemCast As New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno
                    Dim codigotablaGenesis As String = String.Empty

                    objItemCast.OidCodigoAjeno = String.Empty 'Enviamos empty ya que previamente borramos todos los códigos ajenos asociados (método BorrarCodigosAjenos)
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
                    objItemCast.OidTablaGenesis = oidCliente
                    codigotablaGenesis = IAC.ContractoServicio.Constantes.COD_CLIENTE
                    objItemCast.CodTipoTablaGenesis = (From item In IAC.AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                                       Where item.CodTipoTablaGenesis = codigotablaGenesis
                                                       Select item.Entidade).FirstOrDefault()

                    objCodigoAjeno.CodigosAjenos.Add(objItemCast)

                Next

                objAccionAjeno.SetCodigosAjenos(objCodigoAjeno)

            End If

        End Sub

        Public Shared Function GetClienteByCodigoAjeno(objPeticion As IAC.ContractoServicio.Cliente.GetClienteByCodigoAjeno.Peticion) As IAC.ContractoServicio.Cliente.GetClienteByCodigoAjeno.Respuesta

            Dim objRespuesta As New IAC.ContractoServicio.Cliente.GetClienteByCodigoAjeno.Respuesta

            Try
                objRespuesta.Cliente = IAC.AccesoDatos.Cliente.GetClienteByCodigoAjeno(Of IAC.ContractoServicio.Cliente.GetClienteByCodigoAjeno.Cliente)(objPeticion.identificadorAjeno, objPeticion.clienteCodigoAjeno)
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

#End Region

#Region "DIRECCIONES"

        Private Shared Sub DefinirDirecciones(oidCliente As String, DireccionColeccion As IAC.ContractoServicio.Direccion.DireccionColeccionBase, codigoUsuario As String)

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

                    objDireciones.oidTablaGenesis = oidCliente
                    codigotablaGenesis = IAC.ContractoServicio.Constantes.COD_CLIENTE
                    objDireciones.codTipoTablaGenesis = (From item In IAC.AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                                         Where item.CodTipoTablaGenesis = codigotablaGenesis
                                                         Select item.Entidade).FirstOrDefault()

                    objAccionDireccion.SetDirecciones(objDireciones)
                Next


            End If

        End Sub

#End Region

#Region "Integracion Salidas -> NuevoSaldos"

        Public Shared Function ObtenerCliente(CodigoCliente As String) As Clases.Cliente
            Return Prosegur.Genesis.AccesoDatos.Genesis.Cliente.ObtenerCliente(CodigoCliente)
        End Function

#End Region

    End Class

End Namespace