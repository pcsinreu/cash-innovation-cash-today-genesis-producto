Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Collections.ObjectModel
Imports System.Text

Namespace Integracion

    Public Class AccionConsultarSaldos

        Public Shared Function ConsultarSaldos(peticion As ConsultarSaldos.Peticion) As ConsultarSaldos.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializa objeto de respuesta
            Dim respuesta As New ConsultarSaldos.Respuesta
            respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_INTEGRACION_SEM_ERRO
            respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_INTEGRACION_SEM_ERRO
            respuesta.ValidacionesError = New List(Of ValidacionError)

            Try

                ' Validar el token
                validarToken(peticion)

            Catch ex As Excepcion.NegocioExcepcion

                respuesta.ValidacionesError.Add(New ValidacionError With {.codigo = "VAL101", .descripcion = ex.Message})
                ' Respuesta defecto para Excepciones de Negocio
                respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_FUNCIONAL
                respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_FUNCIONAL

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                ' Validar si es un error de Infraestructura o de Aplicacion
                If ValidarException(ex) Then

                    ' Respuesta defecto para Excepciones de Infraestructura
                    respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_INFRAESTRUCTURA
                    respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_INFRAESTRUCTURA

                Else

                    ' Respuesta defecto para Excepciones de Aplicaciones
                    respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_APLICACION
                    respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_APLICACION

                End If

                If peticion.validarPostError AndAlso ex.Message <> "ValidacionesError" Then
                    respuesta.ValidacionesError.Add(New ValidacionError With {.codigo = "VAL000", .descripcion = Util.RecuperarMensagemTratada(ex)})
                End If

            End Try

            Try

                If respuesta.ValidacionesError Is Nothing OrElse respuesta.ValidacionesError.Count = 0 Then
                    Dim _peticion As ConsultarSaldos.Peticion = peticion.Clonar
                    Dim codigosAjenos As ObservableCollection(Of Clases.CodigoAjeno) = Nothing

                    If Not String.IsNullOrEmpty(_peticion.IdentificadorAjeno) Then
                        codigosAjenos = VerificarCodigosAjenos(_peticion)
                    End If

                    respuesta.Saldo = AccesoDatos.GenesisSaldos.Saldo.ConsultarSaldos(_peticion, respuesta.ValidacionesError, codigosAjenos)

                End If

            Catch ex As Excepcion.NegocioExcepcion

                If peticion.validarPostError Then

                    ' Validar campos obligatorios
                    ' Esta validacion es hecha en la petición, validando si fue informado los campos base para crear el documento
                    validarPeticion(peticion, respuesta.ValidacionesError)

                Else
                    respuesta.ValidacionesError = Nothing
                End If


                ' Respuesta defecto para Excepciones de Negocio
                respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_FUNCIONAL
                respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_FUNCIONAL

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                ' Validar si es un error de Infraestructura o de Aplicacion
                If ValidarException(ex) Then

                    ' Respuesta defecto para Excepciones de Infraestructura
                    respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_INFRAESTRUCTURA
                    respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_INFRAESTRUCTURA

                Else

                    ' Respuesta defecto para Excepciones de Aplicaciones
                    respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_APLICACION
                    respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_APLICACION

                End If

                If peticion.validarPostError Then
                    respuesta.ValidacionesError.Add(New ValidacionError With {.codigo = "VAL000", .descripcion = ex.ToString})
                End If

            End Try

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & Now.Subtract(TiempoInicial).ToString() & vbNewLine & "; ")

            ' Añadir el log en la respuesta del servicio
            respuesta.TiempoDeEjecucion = log.ToString()

            ' Respuesta
            Return respuesta

        End Function

#Region "[Validaciones]"

        ''' <summary>
        ''' Validar las informaciones basicas
        ''' </summary>
        ''' <param name="peticion">Petición del Servicio</param>
        ''' <remarks></remarks>
        Public Shared Sub validarToken(peticion As ConsultarSaldos.Peticion)

            If peticion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_TOKEN_PETICION_OBLIGATORIA"))
            Else
                If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                    If String.IsNullOrEmpty(peticion.tokenAcceso) OrElse Not AppSettings("Token").Equals(peticion.tokenAcceso) Then
                        Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("NUEVO_SALDOS_TOKEN_TOKEN_INVALIDO"), peticion.tokenAcceso))
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' Validar los atributos obrigatorios de la petición
        ''' So es llamado si el campo validarPostError es TRUE
        ''' </summary>
        ''' <param name="peticion">Petición del Servicio</param>
        ''' <param name="validaciones">Objeto de Respuesta con las validaciones</param>
        ''' <remarks></remarks>
        Private Shared Sub validarPeticion(peticion As ConsultarSaldos.Peticion,
                                           ByRef validaciones As List(Of ValidacionError))

            If peticion Is Nothing Then
                validaciones.Add(New ValidacionError With {.codigo = "VAL101", .descripcion = Traduzir("VAL101")})

            Else

                If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                    If String.IsNullOrEmpty(peticion.tokenAcceso) OrElse Not AppSettings("Token").Equals(peticion.tokenAcceso) Then
                        validaciones.Add(New ValidacionError With {.codigo = "VAL102", .descripcion = Traduzir("VAL102")})
                    End If
                End If

                If peticion.validarPostError Then


                End If

            End If

        End Sub

        ''' <summary>
        ''' Define si la Exception es de InfraEstructura o de Aplicacion
        ''' </summary>
        ''' <param name="ex"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function ValidarException(ex As Exception) As Boolean

            Dim msg As String = ex.ToString.ToUpper()

            If msg.Contains("TIMEOUT") OrElse
                msg.Contains("ORA-25408") OrElse
                msg.Contains("ORA-03113") OrElse
                msg.Contains("ORA-01033") Then
                ' Si ocurre un TIMEOUT, retorna un error de INTEGRACION_APLICACION
                Return False
            Else
                Return True
            End If

        End Function

#End Region

        Private Shared Function VerificarCodigosAjenos(ByRef peticion As ConsultarSaldos.Peticion) As ObservableCollection(Of Clases.CodigoAjeno)

            Dim codigosAjenos As ObservableCollection(Of Clases.CodigoAjeno) = Nothing

            If peticion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.IdentificadorAjeno) Then

                codigosAjenos = New ObservableCollection(Of Clases.CodigoAjeno)

                If Not String.IsNullOrEmpty(peticion.CodigoCliente) Then
                    codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TCLIENTE", .Codigo = peticion.CodigoCliente})
                End If

                If Not String.IsNullOrEmpty(peticion.CodigoSubCliente) Then
                    codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSUBCLIENTE", .Codigo = peticion.CodigoSubCliente})
                End If

                If Not String.IsNullOrEmpty(peticion.CodigoPtoServicio) Then
                    codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO", .Codigo = peticion.CodigoPtoServicio})
                End If

                If Not String.IsNullOrEmpty(peticion.CodigoDelegacion) Then
                    codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TDELEGACION", .Codigo = peticion.CodigoDelegacion})
                End If

                If Not String.IsNullOrEmpty(peticion.CodigoPlanta) Then
                    codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TPLANTA", .Codigo = peticion.CodigoPlanta})
                End If

                If peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count > 0 Then
                    For Each _sector In peticion.Sectores
                        If Not String.IsNullOrEmpty(_sector.CodigoSector) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSECTOR", .Codigo = _sector.CodigoSector})
                        End If
                    Next
                End If

                If peticion.Canales IsNot Nothing AndAlso peticion.Canales.Count > 0 Then
                    For Each _canal In peticion.Canales
                        If Not String.IsNullOrEmpty(_canal.CodigoCanal) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TCANAL", .Codigo = _canal.CodigoCanal})
                        End If
                        If Not String.IsNullOrEmpty(_canal.CodigoSubCanal) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSUBCANAL", .Codigo = _canal.CodigoSubCanal})
                        End If
                    Next
                End If

                If Not String.IsNullOrEmpty(peticion.CodigoIsoDivisa) Then
                    codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TDIVISA", .Codigo = peticion.CodigoIsoDivisa})
                End If

                AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(peticion.IdentificadorAjeno, codigosAjenos)

            End If

            Return codigosAjenos
        End Function

    End Class

End Namespace

