Imports Prosegur.Global.Saldos
Imports Prosegur.Global.Saldos.ContractoServicio


Public Class AccionRecuperarDatosDocumento

    ''' <summary>
    ''' Método principal responsável por obter dados do documento 
    ''' e chamar os metodos que converte para o objeto respuesta
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 14/07/2009 Criado
    ''' </history>
    Public Function Ejecutar(Peticion As RecuperarDatosDocumento.Peticion) As RecuperarDatosDocumento.Respuesta

        Dim objRespuesta As New RecuperarDatosDocumento.Respuesta
        Dim objDocumento As New Negocio.Documento

        Try

            If Peticion.IdDocumento <> 0 Then ' Se é para obter um documento a partir do seu ID

                objDocumento.Id = Peticion.IdDocumento
                objDocumento.Realizar()

                ' Se é documento grupo, obtém os documentos agregados
                If objDocumento.EsGrupo Then objDocumento.GrupoDocumentosRealizar()

                ' obtém os seus campos extras, bultos e detallhes
                ObterDados(objDocumento)

                PreencherRespuesta(objRespuesta, objDocumento)

            ElseIf Peticion.IdGrupo <> 0 Then 'Se é para obter documentos agregados a partir do ID do Grupo

                objDocumento.Id = Peticion.IdGrupo
                objDocumento.GrupoDocumentosRealizar()

                For Each Documento As Negocio.Documento In objDocumento.Documentos

                    ' obtém os seus campos extras, bultos e detallhes
                    ObterDados(Documento)

                    PreencherRespuesta(objRespuesta, Documento)

                Next

            ElseIf Peticion.IdPSSector <> String.Empty Then  ' Se é para obter documento(s) a partir do setor e status

                Dim objCentrosProceso As New Negocio.CentrosProceso
                objCentrosProceso.IdPS = Peticion.IdPSSector
                objCentrosProceso.Realizar()

                Dim objCarpeta As New Negocio.Carpeta
                objCarpeta.CentroProceso.Id = objCentrosProceso(0).Id
                objCarpeta.EstadoComprobante.Id = Peticion.EstadoComprobante
                objCarpeta.VistaDestinatario = Peticion.VistaDestinatario
                objCarpeta.Realizar()
                objDocumento.Documentos = objCarpeta.Documentos()

                For Each Documento As Negocio.Documento In objDocumento.Documentos

                    ' obtém os seus campos extras, bultos e detallhes
                    ObterDados(Documento)

                    PreencherRespuesta(objRespuesta, Documento)

                Next

            End If

            'Caso não ocorra exceção, retorna o objrespuesta com codigo 0 e mensagem erro vazio
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Para todos os documentos recuperados, obtém os seus campos extras, bultos e detallhes
    ''' </summary>
    ''' <param name="objDocumento"></param>
    ''' <remarks></remarks>
    Private Sub ObterDados(ByRef objDocumento As Negocio.Documento)

        objDocumento.Formulario.CamposExtra.Realizar()

        If Not objDocumento.EsGrupo Then
            If objDocumento.Formulario.ConBultos Then
                objDocumento.Bultos.Realizar()
            End If

            If objDocumento.Formulario.ConValores Then
                objDocumento.Detalles.Realizar()
            End If

            If objDocumento.Formulario.EsActaProceso Then
                objDocumento.Sobres.Documento.Id = objDocumento.Id
                objDocumento.Sobres.Realizar()
            End If
        End If

    End Sub

    ''' <summary>
    ''' Preenche o objeto respuesta com os dados do objeto documento
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 14/07/2009 Criado
    ''' </history>
    Private Sub PreencherRespuesta(ByRef objRespuesta As RecuperarDatosDocumento.Respuesta, _
                                   objDocumento As Negocio.Documento)

        Dim objDocumentoRespuesta As New RecuperarDatosDocumento.Documento

        With objDocumentoRespuesta

            .Id = objDocumento.Id
            .IdGrupo = objDocumento.Grupo.Id
            .IdOrigen = objDocumento.Origen.Id
            .IdSustituto = objDocumento.Sustituto.Id
            .Descripcion = objDocumento.Formulario.Descripcion
            .NumeroComprobante = objDocumento.NumComprobante
            .FechaGestion = objDocumento.FechaGestion
            .FechaUltimaActualizacion = objDocumento.Fecha
            .FechaResolucion = objDocumento.FechaResolucion
            .FechaDispone = objDocumento.FechaDispone
            .EsActaProceso = objDocumento.Formulario.EsActaProceso
            .EsDisponible = objDocumento.Disponible
            .EsReenviado = objDocumento.Reenviado
            .EsAgrupado = objDocumento.Agrupado
            .EsGrupo = objDocumento.EsGrupo
            .EsImportado = objDocumento.Importado
            .EsExportado = objDocumento.Exportado
            .EsSustituto = objDocumento.EsSustituto
            .EsSustituido = objDocumento.Sustituido
            .Documentos = ConverterDocumentos(objDocumento.Documentos)
            ' criar nova instancia do estado comprobante
            .EstadoComprobante = New RecuperarDatosDocumento.EstadoComprobante
            .EstadoComprobante.Id = objDocumento.EstadoComprobante.Id
            .EstadoComprobante.Descripcion = objDocumento.EstadoComprobante.Descripcion
            .EstadoComprobante.Codigo = objDocumento.EstadoComprobante.Codigo
            .Campos = ConverterCampos(objDocumento.Formulario.Campos)
            .CamposExtras = ConverterCamposExtras(objDocumento.Formulario.CamposExtra)
            .Bultos = ConverterBultos(objDocumento.Bultos)
            .Detalles = ConverterDetalles(objDocumento.Detalles)
            .Sobres = ConverterSobres(objDocumento.Sobres)

        End With

        objRespuesta.Documentos.Add(objDocumentoRespuesta)

    End Sub

    ''' <summary>
    ''' Converte a lista de Documentos do objeto documento para o objeto Documento do respuesta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 14/07/2009 Criado
    ''' </history>
    Private Function ConverterDocumentos(objDocumentos As Negocio.Documentos) As RecuperarDatosDocumento.Documentos

        Dim objDocumentosRespuesta As New RecuperarDatosDocumento.Documentos

        If objDocumentos IsNot Nothing AndAlso objDocumentos.Count > 0 Then

            Dim objDocumentoRespuesta As RecuperarDatosDocumento.Documento = Nothing

            For Each objDocumento As Negocio.Documento In objDocumentos

                ' criar novo campo respuesta
                objDocumentoRespuesta = New RecuperarDatosDocumento.Documento

                objDocumentoRespuesta.Id = objDocumento.Id
                objDocumentoRespuesta.IdGrupo = objDocumento.Grupo.Id
                objDocumentoRespuesta.IdOrigen = objDocumento.Origen.Id
                objDocumentoRespuesta.IdSustituto = objDocumento.Sustituto.Id
                objDocumentoRespuesta.Descripcion = objDocumento.Formulario.Descripcion
                objDocumentoRespuesta.NumeroComprobante = objDocumento.NumComprobante
                objDocumentoRespuesta.FechaGestion = objDocumento.FechaGestion
                objDocumentoRespuesta.FechaUltimaActualizacion = objDocumento.Fecha
                objDocumentoRespuesta.FechaResolucion = objDocumento.FechaResolucion
                objDocumentoRespuesta.FechaDispone = objDocumento.FechaDispone
                objDocumentoRespuesta.EsActaProceso = objDocumento.Formulario.EsActaProceso
                objDocumentoRespuesta.EsDisponible = objDocumento.Disponible
                objDocumentoRespuesta.EsReenviado = objDocumento.Reenviado
                objDocumentoRespuesta.EsAgrupado = objDocumento.Agrupado
                objDocumentoRespuesta.EsGrupo = objDocumento.EsGrupo
                objDocumentoRespuesta.EsImportado = objDocumento.Importado
                objDocumentoRespuesta.EsExportado = objDocumento.Exportado
                objDocumentoRespuesta.EsSustituto = objDocumento.EsSustituto
                objDocumentoRespuesta.EsSustituido = objDocumento.Sustituido
                objDocumentoRespuesta.Documentos = ConverterDocumentos(objDocumento.Documentos)
                objDocumentoRespuesta.EstadoComprobante.Id = objDocumento.EstadoComprobante.Id
                objDocumentoRespuesta.EstadoComprobante.Descripcion = objDocumento.EstadoComprobante.Descripcion
                objDocumentoRespuesta.EstadoComprobante.Codigo = objDocumento.EstadoComprobante.Codigo
                objDocumentoRespuesta.Campos = ConverterCampos(objDocumento.Formulario.Campos)
                objDocumentoRespuesta.CamposExtras = ConverterCamposExtras(objDocumento.Formulario.CamposExtra)
                objDocumentoRespuesta.Bultos = ConverterBultos(objDocumento.Bultos)
                objDocumentoRespuesta.Detalles = ConverterDetalles(objDocumento.Detalles)
                objDocumentoRespuesta.Sobres = ConverterSobres(objDocumento.Sobres)

                ' adicionar para coleção
                objDocumentosRespuesta.Add(objDocumentoRespuesta)

            Next

        End If

        Return objDocumentosRespuesta

    End Function

    ''' <summary>
    ''' Converte a lista de campos do objeto formulario para o objeto caracteristica do respuesta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 14/07/2009 Criado
    ''' </history>
    Private Function ConverterCampos(objCampos As Negocio.Campos) As RecuperarDatosDocumento.Campos

        Dim objCamposRespuesta As New RecuperarDatosDocumento.Campos

        If objCampos IsNot Nothing AndAlso objCampos.Count > 0 Then

            Dim objCampoRespuesta As RecuperarDatosDocumento.Campo = Nothing

            For Each objCampo As Negocio.Campo In objCampos

                ' criar novo campo respuesta
                objCampoRespuesta = New RecuperarDatosDocumento.Campo
                objCampoRespuesta.Etiqueta = objCampo.Etiqueta
                objCampoRespuesta.Identificador = objCampo.Id
                objCampoRespuesta.Nombre = objCampo.Nombre
                objCampoRespuesta.Tipo = objCampo.Tipo

                ' adicionar para coleção
                objCamposRespuesta.Add(objCampoRespuesta)

            Next

        End If

        Return objCamposRespuesta

    End Function

    ''' <summary>
    ''' Converte lista de campos extras do formulario para o objeto resposta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 14/07/2009 Criado
    ''' </history>
    Private Function ConverterCamposExtras(objCamposExtras As Negocio.CamposExtra) As RecuperarDatosDocumento.CamposExtras

        Dim objCamposExtrasRespuesta As New RecuperarDatosDocumento.CamposExtras

        If objCamposExtras IsNot Nothing AndAlso objCamposExtras.Count > 0 Then

            Dim objCampoExtraRespuesta As RecuperarDatosDocumento.CampoExtra = Nothing

            For Each objCampoExtra As Negocio.CampoExtra In objCamposExtras

                ' criar novo campo respuesta
                objCampoExtraRespuesta = New RecuperarDatosDocumento.CampoExtra
                objCampoExtraRespuesta.SeValida = objCampoExtra.SeValida
                objCampoExtraRespuesta.Identificador = objCampoExtra.Id
                objCampoExtraRespuesta.Nombre = objCampoExtra.Nombre

                ' preencher campo extra
                objCampoExtraRespuesta.TipoCampoExtra.Id = objCampoExtra.TipoCampoExtra.Id
                objCampoExtraRespuesta.TipoCampoExtra.Descripcion = objCampoExtra.TipoCampoExtra.Descripcion
                objCampoExtraRespuesta.TipoCampoExtra.Codigo = objCampoExtra.TipoCampoExtra.Codigo

                ' adicionar para coleção
                objCamposExtrasRespuesta.Add(objCampoExtraRespuesta)

            Next

        End If

        Return objCamposExtrasRespuesta

    End Function

    Private Function ConverterBultos(objBultos As Negocio.Bultos) As RecuperarDatosDocumento.Bultos

        Dim objBultosRespuesta As New RecuperarDatosDocumento.Bultos

        If objBultos IsNot Nothing AndAlso objBultos.Count > 0 Then

            Dim objBultoRespuesta As RecuperarDatosDocumento.Bulto = Nothing

            For Each objBulto As Negocio.Bulto In objBultos

                ' criar novo campo respuesta
                objBultoRespuesta = New RecuperarDatosDocumento.Bulto
                objBultoRespuesta.NumeroPrecinto = objBulto.NumPrecinto()
                objBultoRespuesta.CodigoBolsa = objBulto.CodBolsa()
                objBultoRespuesta.Destino.Id = objBulto.Destino.Id()
                objBultoRespuesta.Destino.Descripcion = objBulto.Destino.Descripcion()

                ' adicionar para coleção
                objBultosRespuesta.Add(objBultoRespuesta)

            Next

        End If

        Return objBultosRespuesta

    End Function

    ''' <summary>
    ''' Converte lista de detalles do documento para o objeto resposta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 14/07/2009 Criado
    ''' </history>
    Private Function ConverterDetalles(objDetalles As Negocio.Detalles) As RecuperarDatosDocumento.Detalles

        Dim objDetallesRespuesta As New RecuperarDatosDocumento.Detalles

        If objDetalles IsNot Nothing AndAlso objDetalles.Count > 0 Then

            Dim objDetalleRespuesta As RecuperarDatosDocumento.Detalle = Nothing

            For Each objDetalle As Negocio.Detalle In objDetalles

                ' criar novo campo respuesta
                objDetalleRespuesta = New RecuperarDatosDocumento.Detalle

                objDetalleRespuesta.Cantidad = objDetalle.Cantidad
                objDetalleRespuesta.Importe = objDetalle.Importe
                objDetalleRespuesta.Especie.Id = objDetalle.Especie.Id()
                objDetalleRespuesta.Especie.Descripcion = objDetalle.Especie.Descripcion()
                objDetalleRespuesta.Especie.EsUniforme = objDetalle.Especie.Uniforme()
                objDetalleRespuesta.Especie.Moneda.Id = objDetalle.Especie.Moneda.Id()
                objDetalleRespuesta.Especie.Moneda.Descripcion = objDetalle.Especie.Moneda.Descripcion()
                objDetalleRespuesta.Especie.Moneda.Simbolo = objDetalle.Especie.Moneda.Simbolo()

                ' adicionar para coleção
                objDetallesRespuesta.Add(objDetalleRespuesta)

            Next

        End If

        Return objDetallesRespuesta

    End Function

    ''' <summary>
    ''' Converte lista de sobres do documento para o objeto resposta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 14/07/2009 Criado
    ''' </history>
    Private Function ConverterSobres(objSobres As Negocio.Sobres) As RecuperarDatosDocumento.Sobres

        Dim objSobresRespuesta As New RecuperarDatosDocumento.Sobres

        If objSobres IsNot Nothing AndAlso objSobres.Count > 0 Then

            Dim objSobreRespuesta As RecuperarDatosDocumento.Sobre = Nothing

            For Each objSobre As Negocio.Sobre In objSobres

                ' criar novo campo respuesta
                objSobreRespuesta.Importe = objSobre.Importe
                objSobreRespuesta.NumeroSobre = objSobre.NumSobre()
                objSobreRespuesta.ConDiferencia = objSobre.ConDiferencia()
                objSobreRespuesta.Moneda.Id = objSobre.Moneda.Id()
                objSobreRespuesta.Moneda.Descripcion = objSobre.Moneda.Descripcion()
                objSobreRespuesta.Moneda.Simbolo = objSobre.Moneda.Simbolo()

                ' adicionar para coleção
                objSobresRespuesta.Add(objSobreRespuesta)

            Next

        End If

        Return objSobresRespuesta

    End Function

End Class