Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Transactions
Imports System.Threading.Tasks
Imports System.Text
Imports System.Configuration
Imports System.Windows.Documents

Namespace Integracion

    Public Class AccionGenerarCertificado

        Public Shared Function Ejecutar(peticion As generarCertificado.Peticion) As generarCertificado.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializa objeto de respuesta
            Dim respuesta As New generarCertificado.Respuesta
            respuesta.ValidacionesError = New List(Of ValidacionError)

            ' Validaciones
            TiempoParcial = Now
            Try
                validarPeticion(peticion)
            Catch ex As Excepcion.NegocioExcepcion
                respuesta.MenErr = ex.Message
                respuesta.CodErr = Excepcion.Constantes.CONST_CODIGO_ERROR_CERTIFICADO_PARAMETRO
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.MenErr = ex.Message
                respuesta.CodErr = Excepcion.Constantes.CONST_CODIGO_ERROR_CERTIFICADO_TOKEN
            End Try
            log.AppendLine("Tiempo de Validaciones: " & Now.Subtract(TiempoParcial).ToString() & "; ")

            If String.IsNullOrEmpty(respuesta.MenErr) Then

                Try

                    Dim codigosAjenos As ObservableCollection(Of Clases.CodigoAjeno) = Nothing

                    If Not String.IsNullOrEmpty(peticion.IdentificadorAjeno) Then
                        codigosAjenos = VerificarCodigosAjenos(peticion)
                        ConverterPeticionCodigoAjeno(peticion, codigosAjenos, respuesta.ValidacionesError)
                    End If

                    Dim delegacion As Clases.Delegacion = Nothing

                    ' Validaciones de integridade
                    validar(peticion, delegacion)

                    ' Generar Codigo del certificado
                    TiempoParcial = Now
                    Dim codigoCertificado As String = GenerarIdentificador(peticion)
                    log.AppendLine("Tiempo de GenerarIdentificador: " & Now.Subtract(TiempoParcial).ToString() & "; ")

                    Dim CertificadosConflitantes = ValidarCertificado(peticion, delegacion)

                    ' Generar Certificado
                    TiempoParcial = Now

                    Dim objPeticionGenerarCertificado As New Certificacion.GenerarCertificado.Peticion
                    With objPeticionGenerarCertificado
                        .Cliente = New Clases.Cliente With {
                            .Codigo = peticion.CodigoCliente
                            }
                        If peticion.Opciones.Certificado.ToString = Prosegur.Genesis.Comon.Enumeradores.EstadoCertificado.Definitivo.RecuperarValor Then

                            .CodigoCertificadoDefinitivo = codigoCertificado

                            If CertificadosConflitantes IsNot Nothing AndAlso CertificadosConflitantes.Count > 0 Then
                                .CodigoCertificado = CertificadosConflitantes(0)
                            End If

                        Else
                            .CodigoCertificado = codigoCertificado
                        End If

                        .CodigoEstado = peticion.Opciones.Certificado.ToString

                        If peticion.Opciones.Certificado.ToString = Prosegur.Genesis.Comon.Enumeradores.EstadoCertificado.Definitivo.RecuperarValor _
                            AndAlso CertificadosConflitantes IsNot Nothing AndAlso CertificadosConflitantes.Count > 0 Then
                            .CodigoExterno = CertificadosConflitantes(0)
                        Else
                            .CodigoExterno = codigoCertificado
                        End If

                        .CodigoPais = ConfigurationManager.AppSettings("CodigoPais")

                        If String.IsNullOrEmpty(peticion.CodigoDelegacion) Then
                            .EsTodasDelegaciones = True
                        Else
                            .CodigosDelegaciones = New List(Of String)
                            .CodigosDelegaciones.Add(peticion.CodigoDelegacion)
                            .DelegacionLogada = New Clases.Delegacion() With {.Codigo = peticion.CodigoDelegacion, .CodigoPais = ConfigurationManager.AppSettings("CodigoPais")}
                        End If

                        If peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count > 0 Then
                            .CodigosSectores = New List(Of String)
                            .CodigosSectores.AddRange(peticion.Sectores.Select(Function(a) a.CodigoSector))
                        Else
                            .EsTodosSectores = True
                        End If

                        If peticion.Canales IsNot Nothing AndAlso peticion.Canales.Count > 0 Then
                            Dim codigosCanales As New List(Of String)
                            .CodigosSubCanales = New List(Of String)

                            For Each Canal In peticion.Canales
                                If Canal.SubCanales IsNot Nothing AndAlso Canal.SubCanales.Count > 0 Then
                                    For Each _SubCanal In Canal.SubCanales
                                        If Not String.IsNullOrEmpty(_SubCanal.CodigoSubCanal) Then
                                            .CodigosSubCanales.Add(_SubCanal.CodigoSubCanal)
                                        End If
                                    Next
                                Else
                                    codigosCanales.Add(Canal.CodigoCanal)
                                End If
                            Next

                            If codigosCanales.Count > 0 Then
                                .CodigosSubCanales.AddRange(AccesoDatos.Genesis.SubCanal.ObtenerCodigosSubCanalPorCodigoCanal(codigosCanales, peticion.CodigoDelegacion))
                            End If
                        Else
                            .EsTodosCanales = True
                        End If

                        .FyhCertificado = peticion.FechaCertificacion.QuieroGrabarGMTZeroEnLaBBDD(delegacion)
                        .UsuarioCreacion = Prosegur.Genesis.Comon.Constantes.CONST_CERTIFICADO_USUARIO
                    End With

                    AccesoDatos.GenesisSaldos.Certificacion.Comun.GenerarCertificado(objPeticionGenerarCertificado)
                    log.AppendLine("Tiempo de GenerarCertificado: " & Now.Subtract(TiempoParcial).ToString() & "; ")

                    ' Generar Certificado
                    TiempoParcial = Now
                    respuesta.Docs = AccesoDatos.GenesisSaldos.Documento.recuperarDocumentosCertificados(codigoCertificado,
                                                                                                         peticion.IdentificadorAjeno,
                                                                                                         peticion.Opciones.DetallarDesgloses,
                                                                                                         peticion.Opciones.CamposAdicionales,
                                                                                                         Comon.Constantes.CONST_CERTIFICADO_USUARIO,
                                                                                                         delegacion,
                                                                                                         log)
                    respuesta.CodCer = codigoCertificado

                    Dim fecCer = peticion.FechaCertificacion

                    If Not String.IsNullOrEmpty(peticion.FechaCertificacion.ToString("%K")) Then
                        fecCer = peticion.FechaCertificacion.QuieroGrabarGMTZeroEnLaBBDD(delegacion)
                    End If

                    respuesta.FecCer = fecCer.QuieroExibirEstaFechaEnLaPatalla(delegacion)
                    respuesta.FecCer = DateTime.SpecifyKind(respuesta.FecCer, DateTimeKind.Local)

                    respuesta.TimeZone = delegacion.GMT

                    log.AppendLine("Tiempo de recuperarDocumentosCertificados: " & Now.Subtract(TiempoParcial).ToString() & "; ")

                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    respuesta.MenErr = ex.Message
                    respuesta.CodErr = Excepcion.Constantes.CONST_CODIGO_ERROR_CERTIFICADO_FUNCIONAL

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    respuesta.MenErr = ex.Message
                    respuesta.CodErr = Excepcion.Constantes.CONST_CODIGO_ERROR_CERTIFICADO_GENERICA

                End Try

            End If

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & Now.Subtract(TiempoInicial).ToString() & vbNewLine & "; ")

            ' Respuesta
            Return respuesta

        End Function

#Region "[Validaciones]"

        ''' <summary>
        ''' Validar informaciones de peticion
        ''' </summary>
        ''' <param name="peticion">Petición del Servicio</param>
        ''' <remarks></remarks>
        Private Shared Sub validarPeticion(peticion As generarCertificado.Peticion)

            If peticion Is Nothing Then
                Throw New Exception(Traduzir("VAL101"))
            Else
                If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                    If String.IsNullOrEmpty(peticion.TokenAcceso) OrElse Not AppSettings("Token").Equals(peticion.TokenAcceso) Then
                        Throw New Exception(String.Format(Traduzir("VAL102"), peticion.TokenAcceso))
                    End If
                End If
            End If

            Dim errores As New StringBuilder

            'If String.IsNullOrEmpty(peticion.CodigoCliente) Then
            '    errores.AppendLine(String.Format(Traduzir("VAL100"), "CodigoCliente"))
            'End If

            If String.IsNullOrEmpty(peticion.CodigoDelegacion) Then
                errores.AppendLine(String.Format(Traduzir("VAL100"), "CodigoDelegacion"))
            End If

            If peticion.FechaCertificacion = DateTime.MinValue Then
                errores.AppendLine(String.Format(Traduzir("VAL100"), "FechaCertificacion"))
            End If

            If peticion.Opciones Is Nothing Then
                errores.AppendLine(String.Format(Traduzir("VAL100"), "Opciones"))
            End If

            If Not String.IsNullOrEmpty(errores.ToString) Then
                Throw New Excepcion.NegocioExcepcion(errores.ToString)
            End If

        End Sub

        ''' <summary>
        ''' Validar informaciones de peticion
        ''' </summary>
        ''' <param name="peticion">Petición del Servicio</param>
        ''' <remarks></remarks>
        Private Shared Sub validar(peticion As generarCertificado.Peticion, ByRef delegacion As Clases.Delegacion)

            Dim errores As New StringBuilder

            If peticion.Opciones.ValidarPostError Then

                'Se deberá validar el valor del atributo “CodigoDelegacion” de modo que certifique si existe o no una delegación con el dicho código. 
                'Caso no se encuentre la delegación (tabla “GEPR_TDELEGACION”, por el campo “COD_DELEGACION”), se deberá regresar un mensaje de error “003”, 
                '“¡El código de la delegación informada, ‘{0}’, no es válido!” -  donde el “{0}” es el código de la delegación informada.
                delegacion = AccesoDatos.Genesis.Delegacion.ObtenerDelegacionGMT(peticion.CodigoDelegacion)
                If delegacion Is Nothing Then
                    errores.AppendLine(String.Format(Traduzir("102_validacion_delegacion"), peticion.CodigoDelegacion))
                End If

                'De acuerdo con la delegación validada, se deberá aplicar el cálculo de GMT en la hora del servidor (caso la hora del servidor sea distinta del 
                'GMT de la delegación validada),  y validar si el valor en el atributo “FechaCertificacion” es mayor. Caso sea mayor, deberá regresar un mensaje 
                'de error “003”, “¡No es posible certificar con la fecha y hora informada. La fecha y hora informada deberá ser menor o igual a la hora local: 
                'fecha y hora informada ‘{0}’, fecha y hora local ‘{1}’!” – donde el “{0}” es el valor formateado (dia/mês/ano horas:minutos:segundos) informado 
                'en el atributo “FechaCertificacion” y el “{1}” es el valor de la fecha y hora del servidor formateado (dia/mês/ano horas:minutos:segundos) 
                'utilizado en la verificación (con el GMT aplicado, caso tenga sido necesario).
                Dim fechaCertificacion As DateTime = peticion.FechaCertificacion.QuieroGrabarGMTZeroEnLaBBDD(delegacion)
                If fechaCertificacion > DateTime.UtcNow Then
                    Dim HusoHorarioEnMinutos As Integer = delegacion.HusoHorarioEnMinutos
                    Dim AjusteHorarioVerano As Integer = 0

                    ' Verifica se utilia Horario Verano
                    If delegacion.AjusteHorarioVerano > 0 AndAlso
                        (DateTime.Now >= delegacion.FechaHoraVeranoInicio AndAlso
                         DateTime.Now < delegacion.FechaHoraVeranoFin.AddMinutes(delegacion.HusoHorarioEnMinutos)) Then
                        AjusteHorarioVerano = delegacion.AjusteHorarioVerano
                        HusoHorarioEnMinutos = HusoHorarioEnMinutos + AjusteHorarioVerano
                    End If
                    Dim horaGMT As Integer = HusoHorarioEnMinutos / 60
                    Dim minGMT As Integer = HusoHorarioEnMinutos - (horaGMT * 60)
                    Dim GMT As String = horaGMT.ToString("00") + ":" + minGMT.ToString("00")
                    errores.AppendLine(String.Format(Traduzir("102_validacion_fechacertificado"), fechaCertificacion.QuieroExibirEstaFechaEnLaPatalla(delegacion) + GMT, DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(delegacion) + GMT))
                End If

                'Se deberá validar el valor del atributo “CodigoCliente” de modo que certifique si existe o no un cliente con el dicho código. Caso no se encuentre 
                'el cliente (tabla “GEPR_TCLIENTE”, por el campo “COD_CLIENTE”), se deberá regresar un mensaje de error “003”, “¡El código del cliente informado, ‘{0}’, 
                'no es válido!” -  donde el “{0}” es el código del cliente informado.
                If Not String.IsNullOrEmpty(peticion.CodigoCliente) Then
                    Dim identificadorCliente As String = AccesoDatos.Genesis.Cliente.Validar(peticion.CodigoCliente, String.Empty)
                    If String.IsNullOrEmpty(identificadorCliente) Then
                        errores.AppendLine(String.Format(Traduzir("102_validacion_cliente"), peticion.CodigoCliente))
                    End If
                End If

                'Caso el atributo “Sectores” tenga ítems informados, se deberán certificar que todos los códigos de sectores pertenecen a misma delegación (de acuerdo con
                ' el atributo “CodigoDelegacion”). Caso se encuentre algún sector que no sea de la delegación informada, se deberá regresar un mensaje de error “003”, 
                '“¡El sector de código ‘{0}’ no fue encontrado o no pertenece a la delegación ‘{1}’!” - donde el “{0}” es el código del sector validado y el “{1}” es el 
                'código de la delegación informada.
                If peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count > 0 Then
                    For Each _sector In peticion.Sectores
                        Dim identificadorSector As String = AccesoDatos.Genesis.Sector.Validar(delegacion.Identificador, String.Empty, _sector.CodigoSector, String.Empty)
                        If String.IsNullOrEmpty(identificadorSector) Then
                            errores.AppendLine(String.Format(Traduzir("102_validacion_sectores"), _sector.CodigoSector, delegacion.Codigo))
                        End If
                    Next
                End If

                'Caso el atributo “Canales” tenga ítems informados, se deberán certificar que todos los códigos de canales existan (tabla “GEPR_TCANAL”, por el campo “COD_CANAL”). 
                'Caso se encuentre algún canal que no exista, se deberá regresar un mensaje de error “003”, “¡El código del canal informado, ‘{0}’, no es válido!” - donde el “{0}” 
                'es el código del canal informado.
                If peticion.Canales IsNot Nothing AndAlso peticion.Canales.Count > 0 Then
                    For Each _canal In peticion.Canales
                        Dim identificadorCanal As String = AccesoDatos.Genesis.Canal.Validar(_canal.CodigoCanal, String.Empty)
                        If String.IsNullOrEmpty(identificadorCanal) Then
                            errores.AppendLine(String.Format(Traduzir("102_validacion_canal"), _canal.CodigoCanal))
                        Else

                            'Caso el atributo “SubCanales” tenga ítems informados, se deberán certificar que todos los códigos de subcanales pertenezcan  a el mismo canal. Caso se encuentre 
                            'algún subcanal que no sea del canal informado, se deberá regresar un mensaje de error “003”, “¡El subcanal de código ‘{0}’ no fue encontrado o no pertenece al 
                            'canal() '{1}’!” - donde el “{0}” es el código del subcanal validado y el “{1}” es el código del canal informado.
                            If _canal.SubCanales IsNot Nothing AndAlso _canal.SubCanales.Count > 0 Then
                                For Each _subcanal In _canal.SubCanales
                                    Dim identificadorSubCanal As String = AccesoDatos.Genesis.SubCanal.Validar(identificadorCanal, _subcanal.CodigoSubCanal, String.Empty)
                                    If String.IsNullOrEmpty(identificadorSubCanal) Then
                                        errores.AppendLine(String.Format(Traduzir("102_validacion_subcanal"), _subcanal.CodigoSubCanal, _canal.CodigoCanal))
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If

            End If

            If Not String.IsNullOrEmpty(errores.ToString) Then
                Throw New Excepcion.NegocioExcepcion(errores.ToString)
            End If

        End Sub

        ''' <summary>
        ''' Define si la Exception es de InfraEstructura o de Aplicacion
        ''' </summary>
        ''' <param name="ex"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function ValidarException(ex As Exception) As Boolean
            If ex.Message.ToUpper() = "TIMEOUT" Then
                ' Si ocurre un TIMEOUT, retorna un error de InfraEstructura
                Return True
            Else
                Return False
            End If
        End Function

        Private Shared Function VerificarCodigosAjenos(ByRef peticion As generarCertificado.Peticion) As ObservableCollection(Of Clases.CodigoAjeno)

            Dim codigosAjenos As ObservableCollection(Of Clases.CodigoAjeno) = Nothing

            If peticion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.IdentificadorAjeno) Then

                codigosAjenos = New ObservableCollection(Of Clases.CodigoAjeno)

                If Not String.IsNullOrEmpty(peticion.CodigoDelegacion) Then
                    codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TDELEGACION", .Codigo = peticion.CodigoDelegacion})
                End If

                If Not String.IsNullOrEmpty(peticion.CodigoCliente) Then
                    codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TCLIENTE", .Codigo = peticion.CodigoCliente})
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
                        If _canal.SubCanales IsNot Nothing AndAlso _canal.SubCanales.Count > 0 Then
                            For Each _subCanal In _canal.SubCanales
                                If Not String.IsNullOrEmpty(_subCanal.CodigoSubCanal) Then
                                    codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSUBCANAL", .Codigo = _subCanal.CodigoSubCanal})
                                End If
                            Next
                        End If
                    Next
                End If

                AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(peticion.IdentificadorAjeno, codigosAjenos)

            End If

            Return codigosAjenos
        End Function

#End Region

#Region "[Metodos]"
        Private Shared Function GenerarIdentificador(peticion As generarCertificado.Peticion) As String

            Dim _peticion As New Certificacion.GenerarCodigoCertificado.Peticion
            Dim _respuesta As New Certificacion.GenerarCodigoCertificado.Respuesta

            _peticion.CodEstado = peticion.Opciones.Certificado.ToString
            _peticion.CodDelegacion = peticion.CodigoDelegacion
            _peticion.CodCliente = peticion.CodigoCliente
            _peticion.FyhCertificado = peticion.FechaCertificacion
            _peticion.BolTodasDelegaciones = False
            _peticion.BolVariosDelegaciones = False

            If peticion.Sectores Is Nothing OrElse peticion.Sectores.Count = 0 Then
                _peticion.BolTodosSectores = True
            ElseIf peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count = 1 Then
                _peticion.CodSector = peticion.Sectores(0).CodigoSector
            ElseIf peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count > 1 Then
                _peticion.BolVariosSectores = True
            End If

            If peticion.Canales IsNot Nothing AndAlso peticion.Canales.Count = 1 AndAlso peticion.Canales(0).SubCanales IsNot Nothing _
               AndAlso peticion.Canales(0).SubCanales.Count = 1 Then
                _peticion.CodSubcanal = peticion.Canales(0).SubCanales(0).CodigoSubCanal
            End If

            If peticion.Canales Is Nothing OrElse peticion.Canales.Count = 0 Then
                _peticion.BolTodosCanales = True
            ElseIf peticion.Canales IsNot Nothing AndAlso peticion.Canales.Count > 1 Then
                _peticion.BolVariosCanales = True
            End If

            Dim _Accion As New GenesisSaldos.Certificacion.AccionGenerarCodigoCertificado()
            _respuesta = _Accion.Ejecutar(_peticion)

            Return _respuesta.CodCertificado

        End Function
#End Region

        Private Shared Sub ConverterPeticionCodigoAjeno(peticion As generarCertificado.Peticion, codigosAjenos As ObservableCollection(Of Clases.CodigoAjeno), ByRef ValidacionesError As List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError))
            If peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count > 0 Then
                For Each _Sector In peticion.Sectores

                    Dim _sectorAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = _Sector.CodigoSector AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSECTOR")
                    If _sectorAjeno Is Nothing Then
                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), _Sector.CodigoSector)})
                        Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                    End If

                    Dim objSector As Clases.Sector = Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerPorOid(_sectorAjeno.IdentificadorTablaGenesis)
                    _Sector.CodigoSector = objSector.Codigo
                Next
            End If

            If peticion.Canales IsNot Nothing AndAlso peticion.Canales.Count > 0 Then
                For Each Canal In peticion.Canales
                    If Not String.IsNullOrEmpty(Canal.CodigoCanal) Then
                        Dim _canalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = Canal.CodigoCanal AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCANAL")
                        If _canalAjeno Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), Canal.CodigoCanal)})
                            Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                        End If

                        Dim codCanal As String = Prosegur.Genesis.AccesoDatos.Genesis.Canal.ObtenerCanalPorIdentificador(_canalAjeno.IdentificadorTablaGenesis)
                        Canal.CodigoCanal = codCanal
                    End If

                    If Canal.SubCanales IsNot Nothing AndAlso Canal.SubCanales.Count > 0 Then
                        For Each _SubCanal In Canal.SubCanales
                            If Not String.IsNullOrEmpty(_SubCanal.CodigoSubCanal) Then
                                Dim _subCanalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = _SubCanal.CodigoSubCanal AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSUBCANAL")
                                If _subCanalAjeno Is Nothing Then
                                    ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), _SubCanal.CodigoSubCanal)})
                                    Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                                End If

                                Dim objSubCanal As Clases.SubCanal = Prosegur.Genesis.AccesoDatos.Genesis.SubCanal.ObtenerSubCanalPorIdentificador(_subCanalAjeno.IdentificadorTablaGenesis)
                                _SubCanal.CodigoSubCanal = objSubCanal.Codigo
                            End If
                        Next
                    End If
                Next

            End If

            If Not String.IsNullOrEmpty(peticion.CodigoCliente) Then
                Dim _cliente As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = peticion.CodigoCliente AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCLIENTE")
                If _cliente Is Nothing Then
                    ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), peticion.CodigoCliente)})
                    Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                End If

                Dim codCliente As String = Prosegur.Genesis.AccesoDatos.Genesis.Cliente.ObtenerCodigoCliente(_cliente.IdentificadorTablaGenesis)
                peticion.CodigoCliente = codCliente
            End If

            Dim _delegacion As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = peticion.CodigoDelegacion AndAlso x.CodigoTipoTablaGenesis = "GEPR_TDELEGACION")
            If _delegacion Is Nothing Then
                ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), peticion.CodigoDelegacion)})
                Throw New Excepcion.NegocioExcepcion("codigoAjeno")
            End If

            Dim codDelegacion As String = Prosegur.Genesis.AccesoDatos.Genesis.Delegacion.ObtenerCodigoPorIdentificador(_delegacion.IdentificadorTablaGenesis)
            peticion.CodigoDelegacion = codDelegacion

        End Sub

        Private Shared Function ValidarCertificado(Peticion As generarCertificado.Peticion, delegacion As Clases.Delegacion) As List(Of String)
            Dim objPeticion As New Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion.ValidarCertificacion.Peticion
            Dim objRespuesta As New Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion.ValidarCertificacion.Respuesta
            Dim objAccion As New Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Certificacion.AccionValidaCertificacion

            PreencherPeticaoValidarCertificado(Peticion, objPeticion, delegacion)

            objRespuesta = objAccion.ValidarCertificacion(objPeticion)

            'Preenche os codigos dos ultimos certificados executados.
            Dim CodigosUltimosCertificados = objRespuesta.CodigosUltimosCertificados

            If Not (objRespuesta.CodigoError.Equals(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT) OrElse _
                objRespuesta.CodigoError.Equals(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_YES_NO)) Then

                Throw New Exception(objRespuesta.MensajeError.Replace("<BR/>", Environment.NewLine))

            ElseIf objRespuesta.CodigoError.Equals(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_YES_NO) Then

                'Throw New Exception(objRespuesta.MensajeError.Replace("<BR/>", Environment.NewLine))

            End If

            Return CodigosUltimosCertificados
        End Function

        Private Shared Sub PreencherPeticaoValidarCertificado(ByRef Peticion As generarCertificado.Peticion,
                                                        ByRef objPeticion As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion.ValidarCertificacion.Peticion, delegacion As Clases.Delegacion)

            Dim objSubCanalColeccion As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion
            Dim objSectorColeccion As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Setor.GetSectores.SetorColeccion

            Dim objItensSeleccionados As ListItemCollection = Nothing

            If delegacion IsNot Nothing Then
                objPeticion.DelegacionLogada = delegacion
            End If

            'Delegações Selecionadas
            If Not String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then

                objPeticion.CodigoDelegacion = New List(Of String)
                objPeticion.CodigoDelegacion.Add(Peticion.CodigoDelegacion)

            End If

            'Sectores Selecionados
            If Peticion.Sectores IsNot Nothing AndAlso Peticion.Sectores.Count > 0 Then

                objPeticion.CodigoSector = New List(Of String)

                For Each sector In Peticion.Sectores

                    objPeticion.CodigoSector.Add(sector.CodigoSector)

                Next

            End If

            'SubCanais Selecionados
            If Peticion.Canales IsNot Nothing AndAlso Peticion.Canales.Count > 0 Then

                objPeticion.CodigoSubcanal = New List(Of String)

                For Each canal In Peticion.Canales

                    If canal.SubCanales IsNot Nothing Then

                        For Each subcanal In canal.SubCanales

                            If Not String.IsNullOrEmpty(subcanal.CodigoSubCanal) Then

                                objPeticion.CodigoSubcanal.Add(subcanal.CodigoSubCanal)

                            End If

                        Next

                    End If

                Next

                Dim canales = Peticion.Canales.Where(Function(a) a.SubCanales Is Nothing OrElse a.SubCanales.Count = 0).Select(Function(b) b.CodigoCanal)
                If canales.Count > 0 Then
                    Dim subcanales = AccesoDatos.Genesis.SubCanal.ObtenerCodigosSubCanalPorCodigoCanal(canales.ToList(), Nothing)

                    For Each CodigoSubCanal In subcanales

                        If Not String.IsNullOrEmpty(CodigoSubCanal) Then

                            objPeticion.CodigoSubcanal.Add(CodigoSubCanal)

                        End If

                    Next
                End If

            End If

            If Not String.IsNullOrEmpty(Peticion.CodigoCliente) Then

                objPeticion.CodigoCliente = Peticion.CodigoCliente

            End If

            objPeticion.EstadoCertificado = Peticion.Opciones.Certificado.ToString()

            If Peticion.Opciones.Certificado = Comon.Enumeradores.TipoCertificado.DE Then
                objPeticion.EstadoCertificado = Comon.Enumeradores.TipoCertificado.PC.ToString()
            End If

            objPeticion.FechaHoraCertificacion = Peticion.FechaCertificacion

        End Sub

    End Class

End Namespace
