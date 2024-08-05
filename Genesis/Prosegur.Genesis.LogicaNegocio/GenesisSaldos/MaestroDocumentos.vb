Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Excepcion
Imports Prosegur.Genesis.Comon.Clases.Transferencias
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.DataBaseHelper

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe responsável pelo mapeamento das transições de um estado para outro de um determinado documento
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MaestroDocumentos

        Public Shared Sub GuardarDocumento(ByRef documento As Clases.Documento,
                                           hacer_commit As Boolean,
                                           confirmar_doc As Boolean,
                                           bol_gestion_bulto As Boolean,
                                           caracteristica_integracion As Enumeradores.CaracteristicaFormulario?,
                                           ByRef TransaccionActual As Transaccion)

            Try

                ' Por patrón las procedures están preparadas para trabajar con más de un documento
                Dim documentos As New ObservableCollection(Of Clases.Documento)

                If documento.Estado <> Enumeradores.EstadoDocumento.Nuevo AndAlso documento.Estado <> Enumeradores.EstadoDocumento.EnCurso Then
                    AccesoDatos.GenesisSaldos.Documento.TransacionesDocumentos(documento, hacer_commit, TransaccionActual)

                    ' TO DO 1: Mejorar performance
                    If documento.Estado = Enumeradores.EstadoDocumento.Aceptado AndAlso documento.Elemento IsNot Nothing Then
                        documento = GenesisSaldos.Documento.recuperarDocumentoPorIdentificador(documento.Identificador, documento.UsuarioModificacion, TransaccionActual)
                    End If

                    documentos.Add(documento)
                Else

                    ' TO DO 2: Mejorar performance
                    If documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos) OrElse _
                       documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then
                        GenesisSaldos.Documento.calcularValorDelDocumentoPorElElemento(documento, TransaccionActual)
                    End If

                    documentos.Add(documento)
                    Prosegur.Genesis.AccesoDatos.GenesisSaldos.Documento.GrabarDocumento(documentos, bol_gestion_bulto, hacer_commit, confirmar_doc, caracteristica_integracion, TransaccionActual)

                End If

            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub

        Public Shared Function CrearDocumento(Peticion As Contractos.GenesisSaldos.Documento.CrearDocumento.Peticion) As Contractos.GenesisSaldos.Documento.CrearDocumento.Respuesta

            Dim Respuesta As New Contractos.GenesisSaldos.Documento.CrearDocumento.Respuesta()

            Try

                ' valida o token passado na petição
                Util.VerificaInformacionesToken(Peticion)

                If Peticion Is Nothing Then
                    Throw New NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_CREARDOCUMENTO_PETICION_OBLIGATORIA"))
                ElseIf String.IsNullOrEmpty(Peticion.IdentificadorFormulario) AndAlso String.IsNullOrEmpty(Peticion.CodigoFormulario) AndAlso (Peticion.CaracteristicasFormulario Is Nothing OrElse Peticion.CaracteristicasFormulario.Count = 0) Then
                    Throw New NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_CREARDOCUMENTO_PARAMETROS_NO_INFORMADOS"))
                Else

                    Dim formulario As Comon.Clases.Formulario = Nothing

                    If Not String.IsNullOrEmpty(Peticion.IdentificadorFormulario) Then

                        formulario = MaestroFormularios.ObtenerFormulario(Peticion.IdentificadorFormulario)

                        If formulario Is Nothing Then
                            Throw New NegocioExcepcion(String.Format(Traduzir("NUEVO_SALDOS_SERVICIO_CREARDOCUMENTO_FORM_NO_ENCONTRADO_IDENTIFICADOR"), Peticion.IdentificadorFormulario))
                        End If

                    Else

                        If Not String.IsNullOrEmpty(Peticion.CodigoFormulario) Then

                            formulario = MaestroFormularios.ObtenerFormularioPorCodigo(Peticion.CodigoFormulario)

                            If formulario Is Nothing Then
                                Throw New NegocioExcepcion(String.Format(Traduzir("NUEVO_SALDOS_SERVICIO_CREARDOCUMENTO_FORM_NO_ENCONTRADO_CODIGO"), Peticion.CodigoFormulario))
                            End If

                        Else

                            formulario = LogicaNegocio.GenesisSaldos.MaestroFormularios.obtenerFormularioConLasCaracteristicas_v2(Peticion.CaracteristicasFormulario)

                        End If

                    End If

                    Respuesta.Documento = CrearDocumento(formulario)

                End If

            Catch ex As Excepcion.NegocioExcepcion
                Respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Respuesta.Excepciones.Add(ex.Message)
            End Try

            Return Respuesta

        End Function

        Public Shared Function CrearDocumento(formulario As Clases.Formulario, Optional documento As Clases.Documento = Nothing, Optional obtenerFormulario As Boolean = True) As Clases.Documento

            'se o documento não foi informado
            If documento Is Nothing Then
                documento = New Clases.Documento
            End If

            If formulario IsNot Nothing AndAlso Not String.IsNullOrEmpty(formulario.Identificador) Then

                Dim formularioRecuperado As Clases.Formulario = formulario

                If obtenerFormulario Then
                    formularioRecuperado = MaestroFormularios.ObtenerFormulario(formulario.Identificador)
                End If

                If formularioRecuperado Is Nothing Then
                    Throw New NegocioExcepcion(String.Format(Traduzir("msg_formulario_no_encuentrado"), formulario.Identificador))
                End If

                If Not formularioRecuperado.EstaActivo Then
                    Throw New NegocioExcepcion(Traduzir("msg_formulario_noactivo"))
                End If

                ' ao criar um documento, o estado inicial deverá ser "Nuevo"
                documento.Estado = Enumeradores.EstadoDocumento.Nuevo

                ' define o formulário que está dando origem ao documento
                documento.Formulario = formularioRecuperado

                If documento.Formulario IsNot Nothing AndAlso documento.Formulario.GrupoTerminosIACIndividual IsNot Nothing Then
                    documento.GrupoTerminosIAC = documento.Formulario.GrupoTerminosIACIndividual
                End If

                ' gera o identificador do novo documento
                documento.Identificador = System.Guid.NewGuid.ToString()

                documento.TipoDocumento = formularioRecuperado.TipoDocumento

            Else
                Throw New NegocioExcepcion(Traduzir("028_msg_formulario_no_cargado_correctamente"))
            End If

            Return documento

        End Function

        Public Shared Function RecuperaTiposDocumentos() As List(Of Clases.TipoDocumento)

            Try
                Return AccesoDatos.GenesisSaldos.TipoDocumento.RecuperarTipoDocumento()
            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Shared Function RecuperarTipoDocumentoCertificacion() As List(Of Clases.TipoDocumento)

            Try
                Return AccesoDatos.GenesisSaldos.TipoDocumento.RecuperarTipoDocumentoCertificacion()
            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Shared Function AperturarElemento(Peticion As Contractos.GenesisSaldos.Documento.AperturarElemento.Peticion) As Contractos.GenesisSaldos.Documento.AperturarElemento.Respuesta

            Dim Respuesta As New Contractos.GenesisSaldos.Documento.AperturarElemento.Respuesta()

            Try
                If Peticion Is Nothing Then
                    Throw New Excepcion.CampoObrigatorioException(Traduzir("028_identificador_documento_nao_informado"))
                End If

                If String.IsNullOrEmpty(Peticion.IdentificadorDocumento) Then
                    Throw New Excepcion.CampoObrigatorioException(Traduzir("028_identificador_documento_nao_informado"))
                End If

                If String.IsNullOrEmpty(Peticion.Usuario) Then
                    Throw New Excepcion.CampoObrigatorioException(Traduzir("028_usuario_nao_informado"))
                End If

                If Peticion.CaracteristicasFormulario Is Nothing OrElse Peticion.CaracteristicasFormulario.Count = 0 Then
                    Throw New Excepcion.CampoObrigatorioException(Traduzir("028_caracteristicas_formulario_nao_inforamado"))
                End If

                'Recupera recupera documento o documento que será criado a acta
                Dim documentoOrigem = LogicaNegocio.GenesisSaldos.Documento.recuperarDocumentoPorIdentificador(Peticion.IdentificadorDocumento, Peticion.Usuario, Nothing)
                If documentoOrigem IsNot Nothing Then
                    Dim peticionCrearDocumento As New Contractos.GenesisSaldos.Documento.CrearDocumento.Peticion
                    peticionCrearDocumento.CaracteristicasFormulario = Peticion.CaracteristicasFormulario

                    Dim objRespuestaCrearDocumento As Contractos.GenesisSaldos.Documento.CrearDocumento.Respuesta = CrearDocumento(peticionCrearDocumento)

                    If Not objRespuestaCrearDocumento.HayMensajes Then
                        Dim dataActual As DateTime = DateTime.Now

                        'Cria o novo documento através com as características informadas
                        Dim novoDocumento = objRespuestaCrearDocumento.Documento
                        novoDocumento.CuentaOrigen = documentoOrigem.CuentaOrigen.Clonar
                        novoDocumento.CuentaDestino = documentoOrigem.CuentaDestino.Clonar
                        novoDocumento.CuentaSaldoOrigen = documentoOrigem.CuentaSaldoOrigen.Clonar
                        novoDocumento.CuentaSaldoDestino = documentoOrigem.CuentaSaldoDestino.Clonar
                        novoDocumento.DocumentoPadre = documentoOrigem.Clonar
                        novoDocumento.Elemento = documentoOrigem.Elemento.Clonar
                        novoDocumento.FechaHoraPlanificacionCertificacion = dataActual
                        novoDocumento.FechaHoraGestion = dataActual
                        novoDocumento.EstaCertificado = False
                        novoDocumento.NumeroExterno = documentoOrigem.NumeroExterno
                        novoDocumento.ExportadoSol = False
                        novoDocumento.UsuarioCreacion = Peticion.Usuario
                        novoDocumento.UsuarioModificacion = Peticion.Usuario

                        GuardarDocumento(novoDocumento, True, True, False, Nothing, Nothing)

                        'Devolve o documento que foi criado...
                        Respuesta.documento = novoDocumento
                    Else
                        Respuesta.Mensajes.AddRange(objRespuestaCrearDocumento.Mensajes)
                    End If
                End If

            Catch ex As Excepcion.NegocioExcepcion
                Respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Respuesta.Excepciones.Add(ex.Message)
            End Try

            Return Respuesta

        End Function

#Region "[VALIDACIONES]"

        Public Shared Function ValidaCertificadoProvisional(identificadorDocumento As String, delegacionLogada As Clases.Delegacion) As Clases.Certificado
            Return AccesoDatos.GenesisSaldos.Documento.ValidaCertificadoProvisional(identificadorDocumento, delegacionLogada)
        End Function

#End Region

#Region "Operaciones Servicios"

        Public Function EjecutarConsultaDocumentos(Peticion As Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Peticion) As Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Respuesta

            Dim respuesta As New Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Respuesta

            Try
                ValidaConsultaDocumentos(Peticion)
                Dim documentos As ObservableCollection(Of Clases.Documento) = Nothing

                documentos = AccesoDatos.GenesisSaldos.Documento.ConsultaDocumentos(Peticion)

                If documentos IsNot Nothing AndAlso documentos.Count > 0 Then

                    Dim FiltroDocumento As ObservableCollection(Of Clases.Transferencias.FiltroDocumento) = documentos.Select(Function(d) New Clases.Transferencias.FiltroDocumento With {.Identificador = d.Identificador}).ToObservableCollection

                    Dim Filtro As New Clases.Transferencias.Filtro With {.Documento = FiltroDocumento}

                    Dim objRemesas As ObservableCollection(Of Clases.Remesa) = LogicaNegocio.Genesis.Remesa.ObtenerRemesas(Filtro)
                    Dim objRemesa As Clases.Remesa = Nothing

                    For Each doc In documentos

                        doc.Divisas = AccesoDatos.Genesis.Divisas.RecuperarValoresPorDocumento(doc.Identificador)

                        If objRemesas IsNot Nothing Then

                            objRemesa = (From r In objRemesas Where r.IdentificadorDocumento = doc.Identificador).FirstOrDefault

                            If objRemesa IsNot Nothing AndAlso objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                                objRemesa.Bultos.RemoveAll(Function(b) b.IdentificadorDocumento <> doc.Identificador)
                            End If

                            doc.Elemento = objRemesa

                        End If

                    Next

                    respuesta.Documentos = documentos
                End If

                Dim grupoDocumentos As ObservableCollection(Of Clases.GrupoDocumentos) = Nothing
                Dim petitionGrupoDocumento As New Contractos.GenesisSaldos.GrupoDocumento.ConsultaGrupoDocumentos.Peticion

                With petitionGrupoDocumento
                    .Canal = Peticion.Canal
                    .Cliente = Peticion.Cliente
                    .ConjuntosCaracteristicas = Peticion.ConjuntosCaracteristicas
                    .Contrasena = Peticion.Contrasena
                    .Delegacion = Peticion.Delegacion
                    .EstadosDocumento = Peticion.EstadosDocumento
                    .FechaHoraDesde = Peticion.FechaHoraDesde
                    .FechaHoraHasta = Peticion.FechaHoraHasta
                    .Planta = Peticion.Planta
                    .Puesto = Peticion.Puesto
                    .PuntoServicio = Peticion.PuntoServicio
                    .Sector = Peticion.Sector
                    .SubCanal = Peticion.SubCanal
                    .SubCliente = Peticion.SubCliente
                    .Token = Peticion.Token
                    .Usuario = Peticion.Usuario
                End With

                grupoDocumentos = AccesoDatos.GenesisSaldos.GrupoDocumentos.ConsultaGrupoDocumentos(petitionGrupoDocumento)

                If grupoDocumentos IsNot Nothing AndAlso grupoDocumentos.Count > 0 Then

                    For Each grupoDocumento In grupoDocumentos
                        grupoDocumento.Documentos = AccesoDatos.GenesisSaldos.Documento.ObtenerDocumentosPorIdentificadorGrupo(grupoDocumento.Identificador)
                    Next

                    respuesta.GrupoDocumentos = grupoDocumentos
                End If

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.Message)
            End Try

            Return respuesta
        End Function
        Private Shared Sub ValidaConsultaDocumentos(ByRef peticion As Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Peticion)

            If peticion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("028_uno_parametro_debe_ser_informado"))
            End If

            If String.IsNullOrEmpty(peticion.Canal) AndAlso String.IsNullOrEmpty(peticion.Cliente) AndAlso String.IsNullOrEmpty(peticion.Delegacion) AndAlso _
                String.IsNullOrEmpty(peticion.Planta) AndAlso String.IsNullOrEmpty(peticion.PuntoServicio) AndAlso String.IsNullOrEmpty(peticion.Sector) AndAlso _
                String.IsNullOrEmpty(peticion.SubCanal) AndAlso String.IsNullOrEmpty(peticion.SubCliente) AndAlso _
                Not (peticion.ConjuntosCaracteristicas IsNot Nothing AndAlso peticion.ConjuntosCaracteristicas.Count > 0) AndAlso _
                Not (peticion.EstadosDocumento IsNot Nothing AndAlso peticion.EstadosDocumento.Count > 0) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("028_uno_parametro_debe_ser_informado"))
            End If

        End Sub
        Public Shared Function RecuperarDocumentoPorIdentificador(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Respuesta

            Dim Respuesta As New Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Respuesta()
            Try
                Respuesta = New Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Respuesta With {.Documento = LogicaNegocio.GenesisSaldos.Documento.recuperarDocumentoPorIdentificador(Peticion.IdentificadorDocumento, "RecuperarDocumentoPorIdentificador", Nothing)}
            Catch ex As Excepcion.NegocioExcepcion
                Respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Respuesta.Excepciones.Add(ex.Message)
            End Try
            Return Respuesta
        End Function
        Public Shared Function GuardarDocumento(Peticion As Contractos.GenesisSaldos.Documento.GuardarDocumento.Peticion) As Contractos.GenesisSaldos.Documento.GuardarDocumento.Respuesta

            Dim Respuesta As New Contractos.GenesisSaldos.Documento.GuardarDocumento.Respuesta()

            Try

                Peticion.Documento.UsuarioModificacion = Peticion.UsuarioLogado
                GuardarDocumento(Peticion.Documento, True, False, False, Nothing, Nothing)

            Catch ex As Excepcion.NegocioExcepcion
                Respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Respuesta.Excepciones.Add(ex.Message)
            End Try

            Return Respuesta

        End Function

#End Region


        ''' <summary>
        ''' Actualiza el campo Bol_Impreso
        ''' </summary>
        ''' <param name="identificadorDocumento"></param>
        ''' <param name="codigoComprobante"></param>
        ''' <param name="impreso"></param>
        ''' <remarks></remarks>
        Public Shared Function ActualizaBolImpreso(identificadorDocumento As String, codigoComprobante As String, impreso As Boolean) As Integer
            If String.IsNullOrEmpty(identificadorDocumento) AndAlso String.IsNullOrEmpty(codigoComprobante) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("028_Identificador_CodCompro_Documento"))
            End If
            AccesoDatos.GenesisSaldos.Documento.ActualizarBolImpreso(identificadorDocumento, codigoComprobante, impreso)

            Return AccesoDatos.GenesisSaldos.Documento.RecuperarRowVerDocumento(identificadorDocumento, codigoComprobante)
        End Function

        Public Shared Function ValidarSiExisteCodigoExterno(codigoExterno As String) As Boolean

            Return AccesoDatos.GenesisSaldos.Documento.ValidarSiExisteCodigoExterno(codigoExterno)

        End Function

        Public Shared Sub ActualizarDocumentosPendientes(lstDocumentos As List(Of DocumentoGrupoDocumento))
            If lstDocumentos IsNot Nothing Then

                Dim objTransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GENESIS)

                For Each objDocumento In lstDocumentos
                    Dim objCertificado As Clases.Certificado = ValidaCertificadoProvisional(objDocumento.Identificador, objDocumento.CuentaSaldoDestino.Sector.Delegacion)

                    If objCertificado IsNot Nothing Then
                        DeletarCertificado(objCertificado, objDocumento.FechaHoraModificacion, objDocumento.UsuarioModificacion, objTransacion)
                    End If

                    AccesoDatos.GenesisSaldos.Documento.ActualizarBolNoCertificar(objDocumento.Identificador, objDocumento.NoCertificar, objDocumento.FechaHoraModificacion, objDocumento.UsuarioModificacion, objTransacion)

                    Dim fecha As DateTime = objDocumento.FechaPlanCertificacion.QuieroGrabarGMTZeroEnLaBBDD(objDocumento.CuentaSaldoDestino.Sector.Delegacion)
                    AccesoDatos.GenesisSaldos.Documento.ActualizarFechaHoraPlanCertificacion(objDocumento.Identificador, fecha, objDocumento.UsuarioModificacion)
                Next

                objTransacion.RealizarTransacao()

            End If

        End Sub

        Public Shared Sub DeletarCertificado(Certificado As Clases.Certificado, _
                                       GmtModificacion As DateTime, DesUsuario As String,
                                       ByRef objTransacion As Transacao)

            'Se o certificado não for do tipo consulta, desassocia as transações
            If Certificado.Estado <> Enumeradores.EstadoCertificado.Consulta Then

                'Desassocia as transações de efetivo.
                AccesoDatos.GenesisSaldos.TransaccionEfectivo.DesasociarTransaccionCertificado(Certificado.Identificador, GmtModificacion, DesUsuario, objTransacion)

                'Desassocia as transações de meio de pagamento.
                AccesoDatos.GenesisSaldos.TransaccionMedioPago.DesassociarTransacionCertificado(Certificado.Identificador, GmtModificacion, DesUsuario, objTransacion)

                'Deleta o saldo efectivo do certificado corrente
                AccesoDatos.GenesisSaldos.Certificacion.SaldoEfectivo.DeletarSaldo(Certificado.Identificador, objTransacion)

                'Deleta o saldo de medio pago do certificado corrente
                AccesoDatos.GenesisSaldos.Certificacion.SaldoMedioPago.DeletarSaldo(Certificado.Identificador, objTransacion)

            End If

            'Deleta os filtros de delegação
            AccesoDatos.GenesisSaldos.Certificacion.Delegacion.DeletarCertificadoDelegacion(Certificado.Identificador, objTransacion)

            'Deleta os filtros de setor
            AccesoDatos.GenesisSaldos.Certificacion.Sector.DeletarCertificadoSector(Certificado.Identificador, objTransacion)

            'Deleta os filtros de subcanal
            AccesoDatos.GenesisSaldos.Certificacion.SubCanal.DeletarCertificadoSubCanal(Certificado.Identificador, objTransacion)

            'Deleta o certificado corrente
            AccesoDatos.GenesisSaldos.Certificacion.Comun.DeletarCertificado(Certificado.Identificador, objTransacion)

        End Sub

        Public Shared Function DocumentoPadreCodigoExternoSustitucion(codigoExterno As String,
                                       cuentaDestino As Clases.Cuenta,
                                       gestionBulto As Boolean) As Clases.Documento
            Dim documentoPadre As Clases.Documento = Nothing

            Dim dt As System.Data.DataTable = AccesoDatos.Genesis.Remesa.DocumentoPadrePorCodigoExterno(codigoExterno, cuentaDestino.Sector.Delegacion.Identificador, gestionBulto)
            If dt.Rows.Count > 0 Then

                'Verifica se existe alguma remesa com esse codigo externo com estado em transito.
                Dim dr = dt.Select(String.Format("COD_ESTADO_DOCXELEMENTO='{0}'", Enumeradores.EstadoDocumentoElemento.EnTransito.RecuperarValor)).FirstOrDefault
                If dr IsNot Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("028_msg_doc_elemento_remesa_existe_en_transicion"), dr("DES_FORMULARIO"), dr("OID_DOCUMENTO"), RecuperarEnum(Of Enumeradores.EstadoDocumento)(dr("COD_ESTADO_DOCUMENTO").ToString), dr("DES_SECTOR")))
                End If

                'Verifica se existe alguma remesa com esse codigo externo com estado concluído e que não seja baja e esteja no mesmo sector do ingreso atual
                dr = dt.Select(String.Format("ES_BAJA =0 AND COD_ESTADO_DOCXELEMENTO='{0}'", Enumeradores.EstadoDocumentoElemento.Concluido.RecuperarValor)).FirstOrDefault
                If dr IsNot Nothing Then
                    'se a remesa estiver no setor mesmo sector que está tentando ingressar, então será uma substituição e o docuemnto encontrado
                    'será o documento Padre do ingreso novo
                    If dr("OID_SECTOR_DESTINO").Equals(cuentaDestino.Sector.Identificador) Then
                        documentoPadre = LogicaNegocio.GenesisSaldos.Documento.recuperarDocumentoPorIdentificador(dr("OID_DOCUMENTO"), cuentaDestino.UsuarioCreacion, Nothing)
                    Else
                        'como o sector é diferente, então o ingreso não poderá ser feito
                        Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("028_msg_doc_elemento_remesa_existe_sin_baja"), dr("DES_FORMULARIO"), dr("OID_DOCUMENTO"), RecuperarEnum(Of Enumeradores.EstadoDocumento)(dr("COD_ESTADO_DOCUMENTO").ToString), dr("DES_SECTOR")))
                    End If
                End If
            End If

            Return documentoPadre

        End Function

    End Class


End Namespace