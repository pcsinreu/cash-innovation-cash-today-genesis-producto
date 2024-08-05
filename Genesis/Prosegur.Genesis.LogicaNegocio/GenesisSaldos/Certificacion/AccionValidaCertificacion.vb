Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion
Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.LogicaNegocio.Genesis
Imports System.Configuration
Imports System.Threading.Tasks

Namespace GenesisSaldos.Certificacion

    Public Class AccionValidaCertificacion

        Private Shared Function RetornarCertificados(objPeticion As ContractoServicio.GenesisSaldos.Certificacion.ValidarCertificacion.Peticion, _
                                                     Optional CodEstado As String = "") As List(Of Certificado)

            Dim PeticionGenerarCertificado As New ContractoServicio.GenesisSaldos.Certificacion.DatosCertificacion.Peticion

            With PeticionGenerarCertificado
                .DelegacionLogada = objPeticion.DelegacionLogada
                .Cliente = New Clases.Cliente With {.Codigo = objPeticion.CodigoCliente}
                .CodigosDelegaciones = objPeticion.CodigoDelegacion
                .CodigoEstado = If(Not String.IsNullOrEmpty(CodEstado), CodEstado, objPeticion.EstadoCertificado)
                .CodigosSectores = objPeticion.CodigoSector
                .CodigosSubCanales = objPeticion.CodigoSubcanal
                .DelegacionLogada = objPeticion.DelegacionLogada
            End With

            'Retorna os certificados existentes na base
            'Recuperar somente os filtros do certificado
            Dim objCertificadosConflitantes As List(Of Certificado) = AccesoDatos.GenesisSaldos.Certificacion.Comun.RetornarCertificados(PeticionGenerarCertificado, False, False)

            Return objCertificadosConflitantes
        End Function

        ''' <summary>
        ''' Valida se já existe uma certificação para a data/hora informada
        ''' </summary>
        ''' <param name="cuentaSaldoOrigen">Conta saldo de origem.</param>
        ''' <param name="cuentaSaldoDestino">Conta saldo de destino.</param>
        ''' <param name="fechaHoraPlanCertificacion">Data/Hora Plano Certificação.</param>
        ''' <param name="codigoCertificadoConflitante">[PARAMETRO DE RETORNO OPCIONAL] Código do certificado que está conflitante com a data.</param>
        ''' <returns>True: Caso a data informada seja válida. / False: Caso a data informada seja inválida</returns>
        ''' <remarks></remarks> 
        Public Shared Function EsFechaHoraPlanCertificacionValida(cuentaSaldoOrigen As Clases.Cuenta,
                                                                  cuentaSaldoDestino As Clases.Cuenta,
                                                                  fechaHoraPlanCertificacion As DateTime,
                                                         Optional ByRef codigoCertificadoConflitante As String = "",
                                                         Optional ByRef fechaHoraPlanCertificacionAtual As Date? = Nothing) As Boolean
            If (cuentaSaldoOrigen Is Nothing) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("028_cuenta_saldo_origen_obrigatorio"))
            End If

            If (cuentaSaldoDestino Is Nothing) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("028_cuenta_saldo_destino_obrigatorio"))
            End If

            If fechaHoraPlanCertificacion = Date.MinValue Then
                Return True
            Else
                Return AccesoDatos.GenesisSaldos.Certificacion.Comun.EsFechaHoraPlanCertificacionValida(cuentaSaldoOrigen, cuentaSaldoDestino, fechaHoraPlanCertificacion, codigoCertificadoConflitante, fechaHoraPlanCertificacionAtual)
            End If
        End Function

        ''' <summary>
        ''' Valida se já existe uma certificação para a data/hora informada
        ''' </summary>
        ''' <param name="cuentaSaldoOrigen">Conta movimento de origem.</param>
        ''' <param name="cuentaSaldoDestino">Conta movimento de destino.</param>
        ''' <param name="fechaHoraPlanCertificacion">Data/Hora Plano Certificação.</param>
        ''' <returns>True: Caso a data informada seja válida. / False: Caso a data informada seja inválida</returns>
        ''' <remarks></remarks>
        Public Shared Function EsFechaHoraPlanCertificacionValidaPorCuentaMovimento(cuentaSaldoOrigen As Clases.Cuenta,
                                                                                    cuentaSaldoDestino As Clases.Cuenta,
                                                                                    fechaHoraPlanCertificacion As DateTime,
                                                                                    crearConfiguiracionNivelSaldo As Boolean,
                                                                                    loginUsuarioLogado As String,
                                                                           Optional ByRef codigoCertificadoConflitante As String = "",
                                                                           Optional ByRef fechaHoraPlanCertificacionAtual As Date? = Nothing) As Boolean
            If (cuentaSaldoOrigen Is Nothing) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("028_cuenta_origen_obrigatorio"))
            End If

            If (cuentaSaldoDestino Is Nothing) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("028_cuenta_destino_obrigatorio"))
            End If

            Return AccesoDatos.GenesisSaldos.Certificacion.Comun.EsFechaHoraPlanCertificacionValida(cuentaSaldoOrigen, cuentaSaldoDestino, fechaHoraPlanCertificacion, codigoCertificadoConflitante, fechaHoraPlanCertificacionAtual)
        End Function

        Public Shared Function EsFechaHoraPlanCertificacionValidaPorCuentaSaldo(cuentaSaldoOrigem As Clases.Cuenta,
                                                                                cuentaSaldoDestino As Clases.Cuenta,
                                                                                fechaHoraPlanCertificacion As DateTime,
                                                                           Optional ByRef codigoCertificadoConflitante As String = "",
                                                                           Optional ByRef fechaHoraPlanCertificacionAtual As Date? = Nothing) As Boolean
            If (cuentaSaldoOrigem Is Nothing) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("028_cuenta_origen_obrigatorio"))
            End If

            If (cuentaSaldoDestino Is Nothing) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("028_cuenta_destino_obrigatorio"))
            End If

            Return AccesoDatos.GenesisSaldos.Certificacion.Comun.EsFechaHoraPlanCertificacionValida(cuentaSaldoOrigem, cuentaSaldoDestino, fechaHoraPlanCertificacion, codigoCertificadoConflitante, fechaHoraPlanCertificacionAtual)
        End Function


        Public Function ValidarCertificacion(objPeticion As ValidarCertificacion.Peticion) As ValidarCertificacion.Respuesta

            Dim objRespuesta As New ValidarCertificacion.Respuesta

            Try
                If String.IsNullOrEmpty(objPeticion.CodigoCliente) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodigoCliente"))
                End If

                If String.IsNullOrEmpty(objPeticion.EstadoCertificado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "EstadoCertificado"))
                End If

                If objPeticion.FechaHoraCertificacion.Equals(DateTime.MinValue) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "FechaHoraCertificacion"))
                End If

                Dim PeticionDatosCertificado As New DatosCertificacion.Peticion

                ' GMT En la fecha
                'objPeticion.FechaHoraCertificacion = Util.DataHoraGMT_GrabarEnLaBase(objPeticion.FechaHoraCertificacion, objPeticion.DelegacionLogada)

                With PeticionDatosCertificado
                    .DelegacionLogada = objPeticion.DelegacionLogada
                    .Cliente = New Clases.Cliente With {.Codigo = objPeticion.CodigoCliente}
                    .CodigosDelegaciones = objPeticion.CodigoDelegacion
                    If .CodigosDelegaciones Is Nothing OrElse .CodigosDelegaciones.Count = 0 Then
                        .EsTodasDelegaciones = True
                    End If
                    .CodigoEstado = objPeticion.EstadoCertificado
                    .CodigosSectores = objPeticion.CodigoSector
                    If .CodigosSectores Is Nothing OrElse .CodigosSectores.Count = 0 Then
                        .EsTodosSectores = True
                    End If
                    .CodigosSubCanales = objPeticion.CodigoSubcanal
                    If .CodigosSubCanales Is Nothing OrElse .CodigosSubCanales.Count = 0 Then
                        .EsTodosCanales = True
                    End If
                End With

                'Retorna os certificados existentes na base
                'Recuperar somente os filtros do certificado
                Dim objCertificadosConflitantes As List(Of Certificado) = AccesoDatos.GenesisSaldos.Certificacion.Comun.RetornarCertificados(PeticionDatosCertificado, False, False, True)
                'Dim objCertificadosConflitantes As List(Of Certificado) = AccesoDatos.GenesisSaldos.Certificado.RecuperarFiltrosCertificados(PeticionDatosCertificado)

                If objCertificadosConflitantes IsNot Nothing AndAlso objCertificadosConflitantes.Count > 0 Then

                    If (objPeticion.EstadoCertificado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE) _
                        OrElse (objPeticion.EstadoCertificado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE) Then

                        objCertificadosConflitantes.RemoveAll(Function(c) c.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO)

                    End If

                    If objCertificadosConflitantes IsNot Nothing AndAlso objCertificadosConflitantes.Count > 0 Then
                        objRespuesta.CodigosUltimosCertificados = (From cf In objCertificadosConflitantes
                                                                   Select cf.CodigoCertificado, cf.FyhCertificado
                                                                   Order By FyhCertificado Descending).Select(Function(a) a.CodigoCertificado).Take(1).ToList
                    End If

                    Dim ListaCertificados As String = Join(CType((From cf In objCertificadosConflitantes
                                                                  Select cf.CodigoCertificado.Trim()), IEnumerable(Of String)).ToArray(), "<BR/>")

                    If (objPeticion.EstadoCertificado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE) _
                        OrElse (objPeticion.EstadoCertificado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE) Then

                        Dim objCertificadosDefinitivos As List(Of Certificado) = RetornarCertificados(objPeticion,
                                                                                                        Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO)

                        Dim ListaCertificadosIguais As String = String.Empty

                        If objCertificadosDefinitivos IsNot Nothing AndAlso objCertificadosDefinitivos.Count > 0 Then

                            ListaCertificadosIguais = Join(CType((From cf In objCertificadosDefinitivos
                                                                  Where cf.FyhCertificado >= objPeticion.FechaHoraCertificacion
                                                                  Select cf.CodigoCertificado.Trim()), IEnumerable(Of String)).ToArray(), "<BR/>")
                        End If


                        objRespuesta.BolError = True

                        If Not String.IsNullOrEmpty(ListaCertificadosIguais) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("0001_MSG_CERTIFICADO_FECHA_EMITIDO"), "<BR/>" & ListaCertificadosIguais))
                        End If

                        If Not String.IsNullOrEmpty(ListaCertificados) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_YES_NO, String.Format(Traduzir("0001_MSG_CERTIFICADO_FILTROS_IGUAL"), "<BR/>" & ListaCertificados))
                        Else
                            objRespuesta.BolError = False
                        End If

                    ElseIf (objPeticion.EstadoCertificado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO) Then


                        Dim ListaCertificadosIguais As String = Join(CType((From cf In objCertificadosConflitantes
                                                                            Where (cf.FyhCertificado >= objPeticion.FechaHoraCertificacion)
                                                                            Select cf.CodigoCertificado.Trim()), IEnumerable(Of String)).ToArray(), "<BR/>")

                        If Not String.IsNullOrEmpty(ListaCertificadosIguais) Then
                            objRespuesta.BolError = True
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("0001_MSG_CERTIFICADO_FECHA_EMITIDO"), "<BR/>" & ListaCertificadosIguais))
                        End If

                    End If

                End If

                'Não será validado se existe transção para o cliente, o certficado será criado com ou sem transção.
                'GENPLATINT -352 Nuevo Saldos - Certificado Estandar y Bancos - No exhibe el reporte cuando el certificado no tiene movimiento
                'Dim HayTransaccionesEfectivo As Boolean = False
                'Dim HayTransaccionesMedioPago As Boolean = False

                'Recupera as transações
                'HayTransacciones(objPeticion, HayTransaccionesMedioPago, HayTransaccionesEfectivo)

                'Se não foi recuperado nenhuma transação para certificar exibe mensagem de erro.
                'If Not HayTransaccionesEfectivo AndAlso Not HayTransaccionesMedioPago Then

                'Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("0002_Msg_CertificadoNoGenerado"))

                'End If

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Message
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function

    End Class

End Namespace

