Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports System.Data
Imports System.Transactions

Namespace Genesis

    Public Class TerminoIAC

        ''' <summary>
        ''' Insere o valor do termino para o documento.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub Inserir_v2(identificadorDocumento As String, grupoTerminos As Clases.GrupoTerminosIAC, usuario As String)
            If grupoTerminos IsNot Nothing AndAlso grupoTerminos.TerminosIAC IsNot Nothing Then

                Dim valores As New ObservableCollection(Of Clases.Transferencias.ValorTerminoInserir)

                'Grava somente terminos com valores
                For Each termino In grupoTerminos.TerminosIAC.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
                    If termino.EsObligatorio AndAlso String.IsNullOrEmpty(termino.Valor) Then
                        Throw New Excepcion.CampoObrigatorioException(String.Format(Traduzir("028_valor_termino_obrigatorio"), Traduzir("028_documento")))
                    End If

                    Dim valor As New Clases.Transferencias.ValorTerminoInserir
                    valor.identificadorDocumento = identificadorDocumento
                    valor.identificadorTermino = termino.Identificador
                    valor.usuarioModificacion = usuario
                    valor.valor = termino.Valor

                    valores.Add(valor)
                Next

                If valores IsNot Nothing AndAlso valores.Count > 0 Then
                    AccesoDatos.Genesis.ValorTerminoDocumento.Inserir_v2(valores)
                End If

            End If
        End Sub



        Public Shared Function ObtenerTerminosIACPorCodigo(Peticion As Contractos.Comon.Terminos.RecuperarTerminosPorCodigos.Peticion) As Contractos.Comon.Terminos.RecuperarTerminosPorCodigos.Respuesta

            Dim respuesta As New Contractos.Comon.Terminos.RecuperarTerminosPorCodigos.Respuesta

            Try

                If Peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "Petición")
                End If

                If Peticion.CodigosTerminosIAC Is Nothing OrElse Peticion.CodigosTerminosIAC.Count = 0 Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "CodigosTerminosIAC")
                End If

                respuesta.Terminos = ObtenerTerminosIACPorCodigo(Peticion.CodigosTerminosIAC)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Message)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.Message)
            End Try

            Return respuesta

        End Function

        ''' <summary>
        ''' Obtener término por código
        ''' </summary>
        ''' <param name="CodigosTerminosIAC"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTerminosIACPorCodigo(CodigosTerminosIAC As List(Of String)) As ObservableCollection(Of Clases.TerminoIAC)

            Dim objTerminoIAC As New ObservableCollection(Of Clases.TerminoIAC)
            Dim tdTerminosIACs As DataTable = AccesoDatos.Genesis.TerminoIAC.ObtenerTerminosIACPorCodigos(CodigosTerminosIAC)
            objTerminoIAC = cargarTerminosIAC(tdTerminosIACs)
            Return objTerminoIAC

        End Function

        Public Shared Function cargarTerminosIAC(tdTerminosIAC As DataTable) As ObservableCollection(Of Clases.TerminoIAC)
            Dim objTerminos As New ObservableCollection(Of Clases.TerminoIAC)

            If tdTerminosIAC IsNot Nothing AndAlso tdTerminosIAC.Rows.Count Then

                For Each objRow In tdTerminosIAC.Rows

                    Dim objTermino As New Clases.TerminoIAC

                    'Objetos de chave estrangeira
                    objTermino.Formato = New Clases.Formato

                    With objTermino
                        .Identificador = Util.AtribuirValorObj(objRow("OID_TERMINO"), GetType(String))
                        .BuscarParcial = Util.AtribuirValorObj(objRow("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean))
                        .EsCampoClave = Util.AtribuirValorObj(objRow("BOL_CAMPO_CLAVE"), GetType(Boolean))
                        .Orden = Util.AtribuirValorObj(objRow("NEC_ORDEN"), GetType(Integer))
                        .EsObligatorio = Util.AtribuirValorObj(objRow("BOL_ES_OBLIGATORIO"), GetType(Boolean))
                        .CodigoUsuario = Util.AtribuirValorObj(objRow("COD_USUARIO"), GetType(String))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(objRow("FYH_ACTUALIZACION"), GetType(DateTime))
                        .EsTerminoCopia = Util.AtribuirValorObj(objRow("BOL_TERMINO_COPIA"), GetType(Boolean))
                        .EsProtegido = Util.AtribuirValorObj(objRow("BOL_ES_PROTEGIDO"), GetType(Boolean))
                        .CodigoMigracion = Util.AtribuirValorObj(objRow("COD_MIGRACION"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(objRow("COD_TERMINO"), GetType(String))
                        .Observacion = Util.AtribuirValorObj(objRow("OBS_TERMINO"), GetType(String))
                        .Longitud = Util.AtribuirValorObj(objRow("NEC_LONGITUD"), GetType(Integer))
                        .MostrarDescripcionConCodigo = Util.AtribuirValorObj(objRow("BOL_MOSTRAR_CODIGO"), GetType(Boolean))
                        .TieneValoresPosibles = Util.AtribuirValorObj(objRow("BOL_VALORES_POSIBLES"), GetType(Boolean))
                        .AceptarDigitacion = Util.AtribuirValorObj(objRow("BOL_ACEPTAR_DIGITACION"), GetType(Boolean))
                        .EstaActivo = Util.AtribuirValorObj(objRow("BOL_VIGENTE"), GetType(Boolean))
                        .EsEspecificoDeSaldos = Util.AtribuirValorObj(objRow("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean))
                        .Descripcion = Util.AtribuirValorObj(objRow("DES_TERMINO"), GetType(String))

                        With objTermino.Formato
                            .Identificador = Util.AtribuirValorObj(objRow("OID_FORMATO"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(objRow("COD_FORMATO"), GetType(String))
                            .CodigoUsuario = Util.AtribuirValorObj(objRow("F_COD_USUARIO"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(objRow("DES_FORMATO"), GetType(String))
                            .FechaHoraActualizacion = Util.AtribuirValorObj(objRow("F_FYH_ACTUALIZACION"), GetType(DateTime))
                        End With

                        'Verifica se possui algoritimo de validação.
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_ALGORITMO_VALIDACION"), GetType(String))) Then
                            objTermino.AlgoritmoValidacion = New Clases.AlgoritmoValidacion
                            With objTermino.AlgoritmoValidacion
                                .Identificador = Util.AtribuirValorObj(objRow("OID_ALGORITMO_VALIDACION"), GetType(String))
                                .Codigo = Util.AtribuirValorObj(objRow("COD_ALGORITMO_VALIDACION"), GetType(String))
                                .Descripcion = Util.AtribuirValorObj(objRow("DES_ALGORITMO_VALIDACION"), GetType(String))
                                .Observacion = Util.AtribuirValorObj(objRow("OBS_ALGORITMO_VALIDACION"), GetType(String))
                                .CodigoUsuario = Util.AtribuirValorObj(objRow("AV_COD_USUARIO"), GetType(String))
                                .FechaHoraAplicacion = Util.AtribuirValorObj(objRow("AV_FYH_ACTUALIZACION"), GetType(DateTime))
                            End With
                        End If

                        'Verifica se possui mascara.
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_MASCARA"), GetType(String))) Then
                            objTermino.Mascara = New Clases.Mascara
                            With objTermino.Mascara
                                .Identificador = Util.AtribuirValorObj(objRow("OID_MASCARA"), GetType(String))
                                .Codigo = Util.AtribuirValorObj(objRow("COD_MASCARA"), GetType(String))
                                .Descripcion = Util.AtribuirValorObj(objRow("DES_MASCARA"), GetType(String))
                                .ExpresionRegular = Util.AtribuirValorObj(objRow("DES_EXP_REGULAR"), GetType(String))
                                .CodigoUsuario = Util.AtribuirValorObj(objRow("M_COD_USUARIO"), GetType(String))
                                .FechaHoraActualizacion = Util.AtribuirValorObj(objRow("M_FYH_ACTUALIZACION"), GetType(DateTime))
                            End With
                        End If
                    End With

                    objTerminos.Add(objTermino)

                Next

            End If
            Return objTerminos
        End Function

        ''' <summary>
        ''' Obtener el documento por lo Identificador
        ''' </summary>
        ''' <param name="identificadorTerminosIAC"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTerminosIACPorIdentificador(identificadorTerminosIAC As String) As ObservableCollection(Of Clases.TerminoIAC)

            Dim objTerminoIAC As New ObservableCollection(Of Clases.TerminoIAC)
            Dim tdTerminosIACs As DataTable = AccesoDatos.Genesis.TerminoIAC.ObtenerTerminosIACPorIdentificador(identificadorTerminosIAC)
            objTerminoIAC = cargarTerminosIAC(tdTerminosIACs)
            Return objTerminoIAC

        End Function

        Public Shared Function ObtenerTerminosIACPorEmissorDocumento(codEmissorDocumento As String) As ObservableCollection(Of Clases.TerminoIAC)

            Dim objTerminoIAC As New ObservableCollection(Of Clases.TerminoIAC)
            Dim tdTerminosIACs As DataTable = AccesoDatos.Genesis.TerminoIAC.ObtenerTerminosIACPorEmissorDocumento(codEmissorDocumento)
            objTerminoIAC = cargarTerminosIAC(tdTerminosIACs)
            Return objTerminoIAC

        End Function

        ''' <summary>
        ''' Insere o valor do IAC para a Remesa.
        ''' </summary>
        ''' <param name="objRemesa">Objeto remesa</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorIACRemesaInserir(objRemesa As Clases.Remesa)
            Try
                'Insere somente remesa que não estiver anulada.
                If objRemesa IsNot Nothing AndAlso objRemesa.Estado <> Enumeradores.EstadoRemesa.Anulado Then
                    If objRemesa.GrupoTerminosIAC IsNot Nothing AndAlso objRemesa.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
                        'ValidarTermino(objRemesa.GrupoTerminosIAC, Traduzir("028_remesa"))

                        For Each termino In objRemesa.GrupoTerminosIAC.TerminosIAC.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
                            AccesoDatos.Genesis.ValorIACRemesa.ValorIACRemesaInserir(objRemesa.Identificador, termino, objRemesa.UsuarioModificacion)
                        Next

                        'se a remesa possui bultos, então insere os valores IAC para os bultos.
                        If objRemesa.Bultos IsNot Nothing Then
                            For Each objBulto In objRemesa.Bultos
                                'Insere somente bulto que não estiver como anulado
                                If objBulto.Estado <> Enumeradores.EstadoBulto.Anulado Then
                                    objBulto.UsuarioModificacion = objRemesa.UsuarioModificacion
                                    ValorIACBultoInserir(objBulto)
                                End If
                            Next
                        End If
                    End If
                End If
            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para Remesa.
        ''' </summary>
        ''' <param name="remesa">Remesa que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorIACRemesaExcluir(remesa As Clases.Remesa)
            'Se a remesa estiver anula então não excluir
            If remesa IsNot Nothing AndAlso remesa.Estado <> Enumeradores.EstadoRemesa.Anulado Then
                AccesoDatos.Genesis.ValorIACRemesa.ValorIACRemesaExcluir(remesa.Identificador)

                'se a remesa possui bultos, então insere exclui os valores IAC desses bultos
                If remesa.Bultos IsNot Nothing Then
                    For Each Bulto In remesa.Bultos
                        ValorIACBultoExcluir(Bulto)
                    Next
                End If
            End If

        End Sub

        ''' <summary>
        ''' Insere o valor do IAC para a bulto.
        ''' </summary>
        ''' <param name="objBulto">Objeto bulto</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorIACBultoInserir(objBulto As Clases.Bulto)
            Try
                If objBulto IsNot Nothing Then
                    If objBulto.GrupoTerminosIAC IsNot Nothing AndAlso objBulto.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
                        'ValidarTermino(objBulto.GrupoTerminosIAC, Traduzir("028_bulto"))

                        For Each termino In objBulto.GrupoTerminosIAC.TerminosIAC.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
                            AccesoDatos.Genesis.ValorIACBulto.ValorIACBultoInserir(objBulto.Identificador, termino, objBulto.UsuarioModificacion)
                        Next

                        'Se o bulto possui parcial, então insere os valores de IAC das parciais.
                        If objBulto.Parciales IsNot Nothing Then
                            For Each objParcial In objBulto.Parciales
                                'Insere somente as parciais que não estiverem como anuladas
                                If objParcial.Estado <> Enumeradores.EstadoParcial.Anulado Then
                                    objParcial.UsuarioModificacion = objBulto.UsuarioModificacion
                                    ValorIACParcialInserir(objParcial)
                                End If
                            Next
                        End If
                    End If

                End If
            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para bulto.
        ''' </summary>
        ''' <param name="bulto">bulto que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorIACBultoExcluir(bulto As Clases.Bulto)
            'Excluir somente se o bulto não estiver como anulado.
            If bulto IsNot Nothing AndAlso bulto.Estado <> Enumeradores.EstadoBulto.Anulado Then
                AccesoDatos.Genesis.ValorIACBulto.ValorIACBultoExcluir(bulto.Identificador)

                'Se o bulto possui parcial, então Eclui os valores de IAC das parciais.
                If bulto.Parciales IsNot Nothing Then
                    For Each parcial In bulto.Parciales
                        'Excluir somente se a parcial não estiver como anulada.
                        If parcial.Estado <> Enumeradores.EstadoParcial.Anulado Then
                            ValorIACParcialExcluir(parcial.Identificador)
                        End If
                    Next
                End If
            End If

        End Sub

        ''' <summary>
        ''' Insere o valor do IAC para a Parcial.
        ''' </summary>
        ''' <param name="objParcial">Objeto Parcial.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorIACParcialInserir(objParcial As Clases.Parcial)
            Try
                If objParcial IsNot Nothing Then
                    If objParcial.GrupoTerminosIAC IsNot Nothing AndAlso objParcial.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
                        'ValidarTermino(objParcial.GrupoTerminosIAC, Traduzir("028_parcial"))

                        For Each termino In objParcial.GrupoTerminosIAC.TerminosIAC.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
                            AccesoDatos.Genesis.ValorIACParcial.ValorIACParcialInserir(objParcial.Identificador, termino, objParcial.UsuarioModificacion)
                        Next
                    End If
                End If
            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Exclui o valor do IAC parcial do IAC.
        ''' </summary>
        ''' <param name="identificadorParcial">Identificador da parcial que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorIACParcialExcluir(identificadorParcial As String)
            AccesoDatos.Genesis.ValorIACParcial.ValorIACParcialExcluir(identificadorParcial)
        End Sub

        ''' <summary>
        ''' Valida o valor do termino.
        ''' </summary>
        ''' <param name="grupoTerminosIAC">objeto grupo termino com o valor.</param>
        ''' <param name="descricaoItemvalidado">Descricao do objeto que está sendo validado.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValidarTermino(grupoTerminosIAC As Clases.GrupoTerminosIAC, descricaoItemvalidado As String)

            ' verifica se o valor do termino foi informado
            If grupoTerminosIAC IsNot Nothing AndAlso grupoTerminosIAC.TerminosIAC IsNot Nothing Then
                For Each termino In grupoTerminosIAC.TerminosIAC
                    If termino.EsObligatorio AndAlso String.IsNullOrEmpty(termino.Valor) Then
                        Throw New Excepcion.CampoObrigatorioException(String.Format(Traduzir("028_valor_termino_obrigatorio"), descricaoItemvalidado))
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' Insere o valor do termino para o documento.
        ''' </summary>
        ''' <param name="documento">Objeto documento.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoDocumentoInserir(documento As Clases.Documento, usuario As String)
            Try
                If documento IsNot Nothing AndAlso documento.GrupoTerminosIAC IsNot Nothing AndAlso documento.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
                    ValidarTermino(documento.GrupoTerminosIAC, Traduzir("028_documento"))

                    'Grava somente terminos com valores
                    For Each termino In documento.GrupoTerminosIAC.TerminosIAC.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
                        AccesoDatos.Genesis.ValorTerminoDocumento.ValorTerminoDocumentoInserir(documento.Identificador, termino, usuario)
                    Next
                End If
            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Sub

        Public Shared Sub ValorTerminoDocumentoInserir(documento As Clases.Documento, usuario As String, ByRef transaccion As DataBaseHelper.Transaccion)
            Try
                If documento IsNot Nothing AndAlso documento.GrupoTerminosIAC IsNot Nothing AndAlso documento.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
                    ValidarTermino(documento.GrupoTerminosIAC, Traduzir("028_documento"))

                    'Grava somente terminos com valores
                    For Each termino In documento.GrupoTerminosIAC.TerminosIAC.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
                        AccesoDatos.Genesis.ValorTerminoDocumento.ValorTerminoDocumentoInserir(documento.Identificador, termino, usuario, transaccion)
                    Next
                End If
            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o documento.
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoDocumentoExcluir(identificadorDocumento As String)
            AccesoDatos.Genesis.ValorTerminoDocumento.ValorTerminoDocumentoExcluir(identificadorDocumento)
        End Sub

        Public Shared Sub ActualizarValoresTerminoDocumento(documento As Clases.Documento, usuario As String)
            Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = Transactions.IsolationLevel.ReadCommitted})

                ValorTerminoDocumentoExcluir(documento.Identificador)
                ValorTerminoDocumentoInserir(documento, usuario)
                AccesoDatos.GenesisSaldos.Documento.LimparValorMensajeExterna(documento.Identificador)

                transaction.Complete()
            End Using

            'GENPLATINT-1184
            'LogicaNegocio.GenesisSaldos.MaestroDocumentos.VerificaEnvioLegado(documento, True)

        End Sub

        ''' <summary>
        ''' Valor do termino para o grupo de documento.
        ''' </summary>
        ''' <param name="grupoDocumento">Objeto grupo de documento.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoGrupoDocumentoInserir(grupoDocumento As Clases.GrupoDocumentos, usuario As String)
            Try
                If grupoDocumento IsNot Nothing AndAlso grupoDocumento.GrupoTerminosIAC IsNot Nothing AndAlso grupoDocumento.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
                    ValidarTermino(grupoDocumento.GrupoTerminosIAC, Traduzir("028_grupo_documento"))

                    For Each termino In grupoDocumento.GrupoTerminosIAC.TerminosIAC.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
                        AccesoDatos.Genesis.ValorTerminoGrupoDocumento.ValorTerminoGrupoDocumentoInserir(grupoDocumento.Identificador, termino, usuario)
                    Next
                End If
            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o grupo de documento.
        ''' </summary>
        ''' <param name="identificadorGrupoDocumento">Identificador do grupo de documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoGrupoDocumentoExcluir(identificadorGrupoDocumento As String)
            AccesoDatos.Genesis.ValorTerminoGrupoDocumento.ValorTerminoGrupoDocumentoExcluir(identificadorGrupoDocumento)
        End Sub

        ''' <summary>
        ''' Valor do termino para o medio pago do documento.
        ''' </summary>
        ''' <param name="medioPago">Objeto médio pago do documento.</param>
        ''' <param name="identificadorMediopPagoDocumento">Identificado do medio pago do documento.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoDocumentoInserir(medioPago As Clases.MedioPago, identificadorMediopPagoDocumento As String, usuario As String)
            Try
                If medioPago IsNot Nothing AndAlso medioPago.Terminos IsNot Nothing AndAlso medioPago.Valores IsNot Nothing AndAlso medioPago.Valores.Count > 0 Then
                    'ValidarTermino(medioPago.Terminos, Traduzir("028_medio_pago"))

                    For Each v In medioPago.Valores
                        If v.Terminos IsNot Nothing AndAlso v.Terminos.Count() > 0 Then
                            For Each termino In v.Terminos.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
                                AccesoDatos.Genesis.ValorTerminoMedioPago.ValorTerminoMedioPagoInserir(identificadorMediopPagoDocumento, termino, usuario)
                            Next
                        End If
                    Next

                End If
            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o documento de medio pago.
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoExcluirRemesa(identificadorDocumento As String, identificadorRemesa As String)
            AccesoDatos.Genesis.ValorTerminoMedioPago.ValorTerminoMedioPagoExcluirRemesa(identificadorDocumento, identificadorRemesa)
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o documento de medio pago.
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoExcluirBulto(identificadorDocumento As String, identificadorBulto As String)
            AccesoDatos.Genesis.ValorTerminoMedioPago.ValorTerminoMedioPagoExcluirBulto(identificadorDocumento, identificadorBulto)
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o documento de medio pago.
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoExcluirParcial(identificadorDocumento As String, identificadorParcial As String)
            AccesoDatos.Genesis.ValorTerminoMedioPago.ValorTerminoMedioPagoExcluirParcial(identificadorDocumento, identificadorParcial)
        End Sub

        ''' <summary>
        ''' Valor do termino contado do medio pago.
        ''' </summary>
        ''' <param name="valor">Objeto valor medio pago.</param>
        ''' <param name="identificadorMediopPago">Identificador do medio pago.</param>
        ''' <param name="tipoValor">Tipo de valor(contado ou declarado).</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoElementoInserir(valor As Clases.ValorMedioPago, identificadorMediopPago As String, tipoValor As Enumeradores.TipoValor, usuario As String)
            Try

                For Each termino In valor.Terminos.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
                    AccesoDatos.Genesis.ValorTerminoMedioPagoElemento.ValorTerminoMedioPagoElementoInserir(identificadorMediopPago, termino, tipoValor, usuario)
                Next

            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o medio pago contado.
        ''' </summary>
        ''' <param name="identificadorMedioPago">Identificador do medio pago que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoElementoExcluirBulto(identificadorMedioPago As String)
            AccesoDatos.Genesis.ValorTerminoMedioPagoElemento.ValorTerminoMedioPagoElementoExcluirBulto(identificadorMedioPago)
        End Sub

        Public Shared Function RecuperarTerminosIAC(peticion As Contractos.Comon.Terminos.RecuperarTerminosIAC.Peticion) As Contractos.Comon.Terminos.RecuperarTerminosIAC.Respuesta

            Dim respuesta As New Contractos.Comon.Terminos.RecuperarTerminosIAC.Respuesta

            Try

                If peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "Petición")
                End If

                If String.IsNullOrEmpty(peticion.IdentificadorIAC) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "IdentificadorIAC")
                End If

                respuesta.TerminosIAC = AccesoDatos.Genesis.TerminoIAC.RecuperarTerminosConValoresPosiblesIAC(peticion.IdentificadorIAC)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Message)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.Message)
            End Try

            Return respuesta

        End Function

    End Class

End Namespace



