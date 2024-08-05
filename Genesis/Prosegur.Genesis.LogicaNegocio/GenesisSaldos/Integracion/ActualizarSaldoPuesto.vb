Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports System.Configuration
Imports Prosegur.Global.GesEfectivo


Namespace Integracion
    Public Class ActualizarSaldoPuesto

        Private Shared formularioAjustePositivo As Clases.Formulario = Nothing
        Private Shared formularioAjusteNegativo As Clases.Formulario = Nothing

        ''' <summary>
        ''' Atualiza o saldo do posto, criando os documentos de ajustes.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [victor.ramos]  26/12/2014  criado
        ''' </history>
        Public Shared Function Ejecutar(Peticion As Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Peticion) As Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Respuesta

            Return ActualizarSaldoPuesto(Peticion)

        End Function

        Public Shared Function ActualizarSaldoPuesto(Peticion As Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Peticion) As Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Respuesta

            Dim respuesta As New Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Respuesta
            Dim validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            Try
                Dim divisasPositivas As ObservableCollection(Of Clases.Divisa) = Nothing
                Dim divisasNegativas As ObservableCollection(Of Clases.Divisa) = Nothing

                Dim documentoAjustePositivo As Clases.Documento = Nothing
                Dim documentoAjusteNegativo As Clases.Documento = Nothing
                Dim esDocumentoDeValor As Boolean = True

                ValidarPeticion(Peticion)
                ValidarCuenta(Peticion.DiferenciaSaldo.Cuenta, Peticion.DiferenciaSaldo.Divisas)

                divisasPositivas = Peticion.DiferenciaSaldo.Divisas.Clonar()
                divisasNegativas = Peticion.DiferenciaSaldo.Divisas.Clonar()

                Util.BorrarItemsDivisasValoresCantidades(divisasPositivas, False)
                Util.BorrarItemsDivisasValoresCantidades(divisasNegativas, True)

                Dim unidadesMedidas As ObservableCollection(Of Clases.UnidadMedida) = LogicaNegocio.Genesis.UnidadMedida.ObtenerUnidadesMedida()
                Dim unidadMedidaBillete As Clases.UnidadMedida = unidadesMedidas.Where(Function(um) um.EsPadron AndAlso um.TipoUnidadMedida = Enumeradores.TipoUnidadMedida.Billete).FirstOrDefault()
                Dim unidadMedidaMoneda As Clases.UnidadMedida = unidadesMedidas.Where(Function(um) um.EsPadron AndAlso um.TipoUnidadMedida = Enumeradores.TipoUnidadMedida.Moneda).FirstOrDefault()

                ConverterParaValoresAbsolutos(divisasPositivas, unidadMedidaBillete, unidadMedidaMoneda)
                ConverterParaValoresAbsolutos(divisasNegativas, unidadMedidaBillete, unidadMedidaMoneda)

                ' Cuentas
                Dim cuentaOrigen As Clases.Cuenta = Nothing
                Dim cuentaDestino As Clases.Cuenta = Nothing
                Dim cuentaSaldoOrigen As Clases.Cuenta = Nothing
                Dim cuentaSaldoDestino As Clases.Cuenta = Nothing

                cuentaOrigen = New Clases.Cuenta With {
                                                          .Cliente = New Clases.Cliente With {.Codigo = Peticion.DiferenciaSaldo.Cuenta.Cliente.Codigo},
                                                          .SubCliente = Nothing,
                                                          .PuntoServicio = Nothing,
                                                          .Canal = New Clases.Canal With {.Codigo = Peticion.DiferenciaSaldo.Cuenta.Canal.Codigo},
                                                          .SubCanal = New Clases.SubCanal With {.Codigo = Peticion.DiferenciaSaldo.Cuenta.SubCanal.Codigo},
                                                          .Sector = New Clases.Sector With {.Codigo = Peticion.DiferenciaSaldo.Cuenta.Sector.Codigo,
                                                                                            .Delegacion = New Clases.Delegacion With {.Codigo = Peticion.CodigoDelegacion},
                                                                                            .Planta = New Clases.Planta With {.Codigo = Peticion.CodigoPlanta}},
                                                          .UsuarioModificacion = Peticion.CodigoUsuario,
                                                          .TipoCuenta = Comon.Enumeradores.TipoCuenta.Movimiento}

                If Peticion.DiferenciaSaldo.Cuenta.SubCliente IsNot Nothing Then
                    cuentaOrigen.SubCliente = New Clases.SubCliente With {.Codigo = Peticion.DiferenciaSaldo.Cuenta.SubCliente.Codigo}
                End If
                If Peticion.DiferenciaSaldo.Cuenta.PuntoServicio IsNot Nothing Then
                    cuentaOrigen.PuntoServicio = New Clases.PuntoServicio With {.Codigo = Peticion.DiferenciaSaldo.Cuenta.PuntoServicio.Codigo}
                End If

                If divisasPositivas IsNot Nothing AndAlso divisasPositivas.Count > 0 Then
                    If Caracteristicas.Util.VerificarCaracteristicas(formularioAjustePositivo.Caracteristicas,
                         New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                                Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then
                        esDocumentoDeValor = False
                    End If

                    LogicaNegocio.Genesis.MaestroCuenta.ObtenerCuentas(cuentaOrigen, cuentaSaldoOrigen, esDocumentoDeValor, "", True)

                    cuentaDestino = cuentaOrigen
                    cuentaSaldoDestino = cuentaSaldoOrigen

                    documentoAjustePositivo = LogicaNegocio.GenesisSaldos.MaestroDocumentos.CrearDocumento(formularioAjustePositivo, Nothing, False)

                    documentoAjustePositivo.Divisas = divisasPositivas

                    documentoAjustePositivo.Estado = Enumeradores.EstadoDocumento.Nuevo
                    documentoAjustePositivo.CuentaOrigen = cuentaOrigen
                    documentoAjustePositivo.CuentaSaldoOrigen = cuentaSaldoOrigen
                    documentoAjustePositivo.CuentaDestino = cuentaDestino
                    documentoAjustePositivo.CuentaSaldoDestino = cuentaSaldoDestino
                    documentoAjustePositivo.EstaCertificado = False
                    documentoAjustePositivo.UsuarioCreacion = Peticion.CodigoUsuario
                    documentoAjustePositivo.UsuarioModificacion = Peticion.CodigoUsuario
                    documentoAjustePositivo.FechaHoraGestion = DateTime.Now
                    documentoAjustePositivo.GrupoTerminosIAC = documentoAjustePositivo.Formulario.GrupoTerminosIACIndividual

                    ' grava o documento
                    LogicaNegocio.GenesisSaldos.MaestroDocumentos.GuardarDocumento(documentoAjustePositivo, True, True, False, Nothing, Nothing)

                End If

                If divisasNegativas IsNot Nothing AndAlso divisasNegativas.Count > 0 Then
                    If Caracteristicas.Util.VerificarCaracteristicas(formularioAjusteNegativo.Caracteristicas,
                         New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                                Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then
                        esDocumentoDeValor = False
                    End If

                    LogicaNegocio.Genesis.MaestroCuenta.ObtenerCuentas(cuentaOrigen, cuentaSaldoOrigen, esDocumentoDeValor, "", True)

                    cuentaDestino = cuentaOrigen
                    cuentaSaldoDestino = cuentaSaldoOrigen


                    documentoAjusteNegativo = LogicaNegocio.GenesisSaldos.MaestroDocumentos.CrearDocumento(formularioAjusteNegativo, Nothing, False)
                    documentoAjusteNegativo.Divisas = divisasNegativas
                    documentoAjusteNegativo.Estado = Enumeradores.EstadoDocumento.Nuevo
                    documentoAjusteNegativo.CuentaOrigen = cuentaOrigen
                    documentoAjusteNegativo.CuentaSaldoOrigen = cuentaSaldoOrigen
                    documentoAjusteNegativo.CuentaDestino = cuentaDestino
                    documentoAjusteNegativo.CuentaSaldoDestino = cuentaSaldoDestino
                    documentoAjusteNegativo.EstaCertificado = False
                    documentoAjusteNegativo.UsuarioCreacion = Peticion.CodigoUsuario
                    documentoAjusteNegativo.UsuarioModificacion = Peticion.CodigoUsuario
                    documentoAjusteNegativo.FechaHoraGestion = DateTime.Now
                    documentoAjusteNegativo.GrupoTerminosIAC = documentoAjusteNegativo.Formulario.GrupoTerminosIACIndividual

                    ' grava o documento
                    LogicaNegocio.GenesisSaldos.MaestroDocumentos.GuardarDocumento(documentoAjusteNegativo, True, True, False, Nothing, Nothing)

                End If

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Message)

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.GuardarLogExecucao(String.Format("Message: {0} : Stack: {1}", ex.Message, ex.StackTrace), Peticion.CodigoUsuario, Peticion.CodigoDelegacion, Enumeradores.Aplicacion.GenesisSalidas, Peticion.CodigoPuesto)

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(String.Format("Message: {0} : Stack: {1}", ex.Message, ex.StackTrace))

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.GuardarLogExecucao(String.Format("Message: {0} : Stack: {1}", ex.Message, ex.StackTrace), Peticion.CodigoUsuario, Peticion.CodigoDelegacion, Enumeradores.Aplicacion.GenesisSalidas, Peticion.CodigoPuesto)

            End Try

            Return respuesta

        End Function

        Private Shared Sub ConverterParaValoresAbsolutos(ByRef divisas As ObservableCollection(Of Clases.Divisa), _
                                                         unidadMedidaBillete As Clases.UnidadMedida, _
                                                         unidadMedidaMoneda As Clases.UnidadMedida)

            If divisas IsNot Nothing AndAlso divisas.Count > 0 Then

                For Each div In divisas

                    If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 Then

                        For Each den In div.Denominaciones

                            If den.ValorDenominacion IsNot Nothing AndAlso den.ValorDenominacion.Count > 0 Then

                                den.ValorDenominacion(0).UnidadMedida = If(den.EsBillete, unidadMedidaBillete, unidadMedidaMoneda)

                                If den.ValorDenominacion(0).Importe < 0 Then

                                    den.ValorDenominacion(0).Importe = den.ValorDenominacion(0).Importe * -1
                                    den.ValorDenominacion(0).Cantidad = den.ValorDenominacion(0).Cantidad * -1
                                    div.ValoresTotales(0).Importe = div.ValoresTotales(0).Importe * -1

                                End If
                            End If

                        Next

                    End If

                Next

            End If

        End Sub

        Private Shared Function HaySaldoNegativo(divisas As ObservableCollection(Of Clases.Divisa)) As Boolean

            If divisas IsNot Nothing AndAlso divisas.Count > 0 Then

                For Each div In divisas

                    If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 Then

                        For Each den In div.Denominaciones

                            If den.ValorDenominacion IsNot Nothing AndAlso den.ValorDenominacion.Count > 0 Then
                                If den.ValorDenominacion(0).Importe < 0 Then

                                    Return True

                                End If
                            End If

                        Next

                    End If

                Next

            End If

            Return False

        End Function

        Private Shared Function HaySaldoPositivo(divisas As ObservableCollection(Of Clases.Divisa)) As Boolean

            If divisas IsNot Nothing AndAlso divisas.Count > 0 Then

                For Each div In divisas

                    If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 Then

                        For Each den In div.Denominaciones

                            If den.ValorDenominacion IsNot Nothing AndAlso den.ValorDenominacion.Count > 0 Then
                                If den.ValorDenominacion(0).Importe > 0 Then

                                    Return True

                                End If
                            End If

                        Next

                    End If

                Next

            End If

            Return False

        End Function

        Private Shared Sub ValidarCuenta(cuenta As Comon.Clases.Cuenta, divisas As ObservableCollection(Of Comon.Clases.Divisa))

            'Objeto que recebe as mensagens
            Dim mensagens As New System.Text.StringBuilder()

            If HaySaldoPositivo(divisas) Then
                formularioAjustePositivo = LogicaNegocio.GenesisSaldos.MaestroFormularios.obtenerFormularioDeGestiondeFondosAjustes(False, True, True, Enumeradores.CaracteristicaFormulario.IntegracionSalidas)
            End If

            'Se houver saldo negativo vai precisar do formulário ajustes negativos
            If HaySaldoNegativo(divisas) Then

                formularioAjusteNegativo = LogicaNegocio.GenesisSaldos.MaestroFormularios.obtenerFormularioDeGestiondeFondosAjustes(False, True, False, Enumeradores.CaracteristicaFormulario.IntegracionSalidas)

            End If


            If cuenta.Canal Is Nothing Then
                mensagens.AppendLine(String.Format(Traduzir("Gen_msg_atributo"), "Peticion.Remesas[].Cuenta.Canal"))
            End If

            If cuenta.SubCanal Is Nothing Then
                mensagens.AppendLine(String.Format(Traduzir("Gen_msg_atributo"), "Peticion.Remesas[].Cuenta.SubCanal"))
            End If

            If cuenta.Sector Is Nothing Then
                mensagens.AppendLine(String.Format(Traduzir("Gen_msg_atributo"), "Peticion.Remesas[].Cuenta.Sector"))
            End If

            If cuenta.Cliente Is Nothing Then
                mensagens.AppendLine(String.Format(Traduzir("Gen_msg_atributo"), "Peticion.Remesas[].Cuenta.Cliente"))
            End If

            If mensagens.Length > 0 Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, mensagens.ToString())
            End If

        End Sub

        Private Shared Sub ValidarPeticion(peticion As Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Peticion)

            'Objeto que recebe as mensagens
            Dim mensagens As New System.Text.StringBuilder()

            'Verifica se o usuário foi informado
            If String.IsNullOrEmpty(peticion.CodigoUsuario) Then
                mensagens.AppendLine(String.Format(Traduzir("Gen_msg_atributo"), "Peticion.CodigoUsuario"))
            End If

            If peticion.DiferenciaSaldo Is Nothing Then
                mensagens.AppendLine(String.Format(Traduzir("Gen_msg_atributo"), "Peticion.DiferenciaSaldo"))
            End If

            If peticion.DiferenciaSaldo.Divisas Is Nothing OrElse peticion.DiferenciaSaldo.Divisas.Count = 0 Then
                mensagens.AppendLine(String.Format(Traduzir("Gen_msg_atributo"), "Peticion.DiferenciaSaldo.Divisas"))
            End If

            If peticion.DiferenciaSaldo.Cuenta Is Nothing Then
                mensagens.AppendLine(String.Format(Traduzir("Gen_msg_atributo"), "Peticion.DiferenciaSaldo.Cuenta"))
            End If

            If mensagens.Length > 0 Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, mensagens.ToString())
            End If

        End Sub

    End Class
End Namespace
