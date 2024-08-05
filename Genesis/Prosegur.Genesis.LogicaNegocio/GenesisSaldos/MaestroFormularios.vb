Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Transactions
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Text
Imports Prosegur.Genesis.DataBaseHelper

Namespace GenesisSaldos
    ''' <summary>
    ''' Classe responsável pelo gerenciamento de formularios
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MaestroFormularios

        Public Shared Function ObtenerFormulariosPorCodigos_v2(codigosFormularios As List(Of String)) As ObservableCollection(Of Clases.Formulario)
            Return AccesoDatos.GenesisSaldos.Formulario.ObtenerFormulariosPorCodigos_v2(codigosFormularios)
        End Function

        Public Shared Function ObtenerFormularioPorCodigo_v2(codigoFormulario As String) As Clases.Formulario
            Dim listacodigoFormulario As New List(Of String)
            If Not String.IsNullOrEmpty(codigoFormulario) Then
                listacodigoFormulario.Add(codigoFormulario)
            End If
            Dim formularios As ObservableCollection(Of Clases.Formulario) = ObtenerFormulariosPorCodigos_v2(listacodigoFormulario)
            If formularios IsNot Nothing AndAlso formularios.Count > 0 Then
                Return formularios.FirstOrDefault
            End If
            Return Nothing
        End Function

        Public Shared Function ObtenerTerminosIACPorCodigoFormulario(codigoFormulario As String,
                                                                     esIndividual As Boolean,
                                                            Optional terminosPeticion As List(Of ContractoServicio.Contractos.Integracion.Comon.CampoAdicional) = Nothing) As Clases.GrupoTerminosIAC
            Dim listaTerminos As List(Of String) = Nothing

            If terminosPeticion IsNot Nothing AndAlso terminosPeticion.Count > 0 Then
                listaTerminos = New List(Of String)
                For Each termino In terminosPeticion
                    listaTerminos.Add(termino.nombre)
                Next
            End If

            Return AccesoDatos.GenesisSaldos.Formulario.ObtenerTerminosIACPorCodigoFormulario(codigoFormulario, esIndividual, listaTerminos)

        End Function
        Public Shared Function ObtenerIdentificadorGrupoTerminosIACPorCodigoFormulario(codigoFormulario As String, esIndividual As Boolean) As String
            If codigoFormulario IsNot Nothing Then
                Return AccesoDatos.GenesisSaldos.Formulario.ObtenerGrupoTerminoIACPorCodigoFormulario(codigoFormulario, esIndividual)
            End If

                Return Nothing
        End Function

        Public Shared Function cargarFormularios(tdFormularios As DataTable, Optional esSimplificado As Boolean = False) As ObservableCollection(Of Clases.Formulario)
            Dim objFormularios As New ObservableCollection(Of Clases.Formulario)

            If tdFormularios IsNot Nothing AndAlso tdFormularios.Rows.Count > 0 Then

                For Each objRow In tdFormularios.Rows

                    Dim objFormulario As New Clases.Formulario
                    objFormulario.AccionContable = New Clases.AccionContable
                    objFormulario.TipoDocumento = New Clases.TipoDocumento

                    With objFormulario

                        .Identificador = Util.AtribuirValorObj(objRow("OID_FORMULARIO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(objRow("COD_FORMULARIO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(objRow("DES_FORMULARIO"), GetType(String))

                        If objRow("cod_color") IsNot Nothing Then
                            .Color = System.Drawing.Color.FromName(objRow("cod_color").ToString)
                        End If

                        .Icono = Util.AtribuirValorObj(objRow("BIN_ICONO_FORMULARIO"), GetType(Byte()))
                        .EstaActivo = Util.AtribuirValorObj(objRow("BOL_ACTIVO"), GetType(String))
                        .FechaHoraCreacion = Util.AtribuirValorObj(objRow("GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(objRow("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(objRow("DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(objRow("DES_USUARIO_MODIFICACION"), GetType(String))
                        .DescripcionCodigoExterno = Util.AtribuirValorObj(objRow("DES_COD_EXTERNO"), GetType(String))
                        .AccionContable = AccionContable.ObtenerAccionContable(Util.AtribuirValorObj(objRow("OID_ACCION_CONTABLE"), GetType(String)))

                        objFormulario.Caracteristicas = CaracteristicaFormulario.RecuperarCaracteristicasFormulario(objFormulario.Identificador)

                        If Not esSimplificado Then

                            'GrupoTerminosIAC individual
                            'Verifica se não é nulo
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_IAC_INDIVIDUAL"), GetType(String))) Then
                                objFormulario.GrupoTerminosIACIndividual = AccesoDatos.Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIAC(objRow("OID_IAC_INDIVIDUAL"))
                            End If

                            'GrupoTerminosIAC Grupo
                            'Verifica se não é nulo
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_IAC_GRUPO"), GetType(String))) Then
                                objFormulario.GrupoTerminosIACGrupo = AccesoDatos.Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIAC(objRow("OID_IAC_GRUPO"))
                            End If

                            ' TipoDocumento
                            With objFormulario.TipoDocumento
                                .Identificador = Util.AtribuirValorObj(objRow("OID_TIPO_DOCUMENTO"), GetType(String))
                                .Codigo = Util.AtribuirValorObj(objRow("COD_TIPO_DOCUMENTO"), GetType(String))
                                .Descripcion = Util.AtribuirValorObj(objRow("DES_TIPO_DOCUMENTO"), GetType(String))
                                .EstaActivo = Util.AtribuirValorObj(objRow("TD_BOL_ACTIVO"), GetType(Boolean))
                                .FechaHoraCreacion = Util.AtribuirValorObj(objRow("TD_GMT_CREACION"), GetType(DateTime))
                                .FechaHoraModificacion = Util.AtribuirValorObj(objRow("TD_GMT_MODIFICACION"), GetType(DateTime))
                                .UsuarioCreacion = Util.AtribuirValorObj(objRow("TD_DES_USUARIO_CREACION"), GetType(String))
                                .UsuarioModificacion = Util.AtribuirValorObj(objRow("TD_DES_USUARIO_MODIFICACION"), GetType(String))
                            End With

                            'OID_FILTRO_FORMULARIO
                            'Verifica se não é nulo
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_FILTRO_FORMULARIO"), GetType(String))) Then
                                objFormulario.FiltroFormulario = New Clases.Transferencias.FiltroFormulario
                                With objFormulario.FiltroFormulario
                                    .Identificador = Util.AtribuirValorObj(objRow("OID_FILTRO_FORMULARIO"), GetType(String))
                                    .Descripcion = Util.AtribuirValorObj(objRow("DES_FILTRO_FORMULARIO"), GetType(String))
                                    .SoloMovimientoDisponible = Util.AtribuirValorObj(objRow("BOL_SOLO_DISPONIBLE"), GetType(Boolean))
                                    .UtilizaDocumentosConValor = Util.AtribuirValorObj(objRow("BOL_CON_VALOR"), GetType(Boolean))
                                    .UtilizaDocumentosConBulto = Util.AtribuirValorObj(objRow("FF_BOL_CON_BULTO"), GetType(Boolean))
                                    .SoloMovimientoDeReenvio = Util.AtribuirValorObj(objRow("BOL_SOLO_REENVIO"), GetType(Boolean))
                                    .SoloMovimientoDeSustitucion = Util.AtribuirValorObj(objRow("BOL_SOLO_SUSTITUCION"), GetType(Boolean))
                                    .MovimientoConFechaEspecifica = Util.AtribuirValorObj(objRow("BOL_CON_FECHA_ESPECIFICA"), GetType(Boolean))
                                    .CantidadDiasBusquedaInicio = Util.AtribuirValorObj(objRow("NEC_DIAS_BUSQUEDA_INICIO"), GetType(Boolean))
                                    .CantidadDiasBusquedaFin = Util.AtribuirValorObj(objRow("NEC_DIAS_BUSQUEDA_FIN"), GetType(Boolean))
                                    .EstaActivo = Util.AtribuirValorObj(objRow("FF_BOL_ACTIVO"), GetType(Boolean))
                                    .FechaHoraCreacion = Util.AtribuirValorObj(objRow("FF_GMT_CREACION"), GetType(DateTime))
                                    .UsuarioCreacion = Util.AtribuirValorObj(objRow("FF_DES_USUARIO_CREACION"), GetType(String))
                                    .FechaHoraModificacion = Util.AtribuirValorObj(objRow("FF_GMT_MODIFICACION"), GetType(DateTime))
                                    .UsuarioModificacion = Util.AtribuirValorObj(objRow("FF_DES_USUARIO_MODIFICACION"), GetType(String))
                                End With
                            End If
                        End If
                    End With

                    objFormularios.Add(objFormulario)

                Next

            End If

            Return objFormularios
        End Function

        Public Shared Function ObtenerFormularios(objIdentificadoresFormularios As List(Of String)) As ObservableCollection(Of Clases.Formulario)
            Dim tdFormularios As DataTable = AccesoDatos.GenesisSaldos.Formulario.ObtenerFormularios(objIdentificadoresFormularios)
            Return cargarFormularios(tdFormularios)
        End Function

        Public Shared Function ObtenerFormularios() As List(Of Clases.Formulario)
            Try
                Return AccesoDatos.GenesisSaldos.Formulario.ObtenerFormularios()

            Catch ex As Excepcion.NegocioExcepcion
                Throw

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function ObtenerFormulario(objIdentificadorFormulario As String, Optional ByRef FormulariosPosibles As ObservableCollection(Of Clases.Formulario) = Nothing) As Clases.Formulario

            If FormulariosPosibles IsNot Nothing AndAlso FormulariosPosibles.Count > 0 AndAlso FormulariosPosibles.FirstOrDefault(Function(x) x.Identificador = objIdentificadorFormulario) IsNot Nothing Then

                Return FormulariosPosibles.FirstOrDefault(Function(x) x.Identificador = objIdentificadorFormulario)

            Else

                If FormulariosPosibles Is Nothing Then
                    FormulariosPosibles = New ObservableCollection(Of Clases.Formulario)
                End If

                Dim objIdentificadoresFormularios As New List(Of String)
                objIdentificadoresFormularios.Add(objIdentificadorFormulario)

                Dim objFormularios As ObservableCollection(Of Clases.Formulario) = ObtenerFormularios(objIdentificadoresFormularios)

                If objFormularios IsNot Nothing AndAlso objFormularios.Count > 0 Then
                    FormulariosPosibles.Add(objFormularios(0))

                    Return objFormularios(0)
                End If
            End If

            Return Nothing
        End Function

        Public Shared Function ObtenerFormulariosPorCodigo(objCodigosFormularios As List(Of String)) As ObservableCollection(Of Clases.Formulario)
            If objCodigosFormularios IsNot Nothing AndAlso objCodigosFormularios.Count > 0 Then
                Dim tdFormularios As DataTable = AccesoDatos.GenesisSaldos.Formulario.ObtenerFormulariosPorCodigo(objCodigosFormularios)
                Return cargarFormularios(tdFormularios)
            End If
            Return Nothing
        End Function

        Public Shared Function ObtenerFormularioPorCodigo(objCodigoFormulario As String) As Clases.Formulario
            If Not String.IsNullOrEmpty(objCodigoFormulario) Then
                Dim objCodigosFormularios As New List(Of String)
                objCodigosFormularios.Add(objCodigoFormulario)

                Dim objFormularios As ObservableCollection(Of Clases.Formulario) = ObtenerFormulariosPorCodigo(objCodigosFormularios)
                If objFormularios IsNot Nothing AndAlso objFormularios.Count > 0 Then
                    Return objFormularios(0)
                End If
            End If
            Return Nothing
        End Function

        Public Shared Function ObtenerFormulariosReenvioAutomatico(caracteristicas As List(Of Enumeradores.CaracteristicaFormulario)) As Clases.Formulario
            Dim formulario As Clases.Formulario = Nothing
            Try
                ' obter formulário por identificador
                Dim listaFormularios = AccesoDatos.GenesisSaldos.Formulario.ObtenerFormulariosReenvioAutomatico()
                If listaFormularios IsNot Nothing AndAlso listaFormularios.Count > 0 Then
                    'Verifica se o formulário atual possui caracteristica de gestão de remessa, e recupera o primeiro formulário de reenvio com gestão de remesa
                    If caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then
                        formulario = listaFormularios.Where(Function(f) f.Caracteristicas IsNot Nothing AndAlso f.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeRemesas)).FirstOrDefault

                        'Verifica se o formulário atual possui caracteristica de gestão de bultos, e recupera o primeiro formulário de reenvio com gestão de bultos
                    ElseIf caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then
                        formulario = listaFormularios.Where(Function(f) f.Caracteristicas IsNot Nothing AndAlso f.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos)).FirstOrDefault
                    End If

                    'Recupera o formulário com suas propriedades.
                    If formulario IsNot Nothing Then
                        formulario = LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFormulario(formulario.Identificador)
                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return formulario
        End Function

        ''' <summary>
        ''' Valida de acordo com as caracteristicas do formulário se já existe os formulários de integração
        ''' </summary>
        ''' <param name="formulario"></param>
        ''' <remarks></remarks>
        Public Shared Sub ValidarFormulariosDeIntegraciones(formulario As Clases.Formulario)

            Dim errores As New StringBuilder
            Dim caracteristicas As New List(Of Enumeradores.CaracteristicaFormulario)

            'Verifica se o formulario tem alguma característica de integração
            If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionConteo) _
                OrElse VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionLegado) _
                OrElse VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionRecepcionEnvio) _
                OrElse VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionSalidas) _
                OrElse VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos) Then

                caracteristicas = formulario.Caracteristicas.Clonar
                BorrarCaracteristicasAdicionales(caracteristicas)

                'Valida se já existe um formulário de integração com o conteo desse tipo (conjunto de caracteristicas)
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionConteo) Then
                    ValidarFormulariosDeIntegraciones(caracteristicas, formulario.Codigo, Enumeradores.CaracteristicaFormulario.IntegracionConteo, formulario.AccionContable, errores)
                End If

                'Valida se já existe um formulário de integração com o legado desse tipo (conjunto de caracteristicas)
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionLegado) Then
                    ValidarFormulariosDeIntegraciones(caracteristicas, formulario.Codigo, Enumeradores.CaracteristicaFormulario.IntegracionLegado, formulario.AccionContable, errores)
                End If

                'Valida se já existe um formulário de integração com o recepción y envio desse tipo (conjunto de caracteristicas)
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionRecepcionEnvio) Then
                    ValidarFormulariosDeIntegraciones(caracteristicas, formulario.Codigo, Enumeradores.CaracteristicaFormulario.IntegracionRecepcionEnvio, formulario.AccionContable, errores)
                End If

                'Valida se já existe um formulário de integração com o salidas desse tipo (conjunto de caracteristicas)
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionSalidas) _
                   AndAlso Not VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos) _
                   AndAlso Not VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ContestarSolicitacionDeFondos) Then

                    ValidarFormulariosDeIntegraciones(caracteristicas, formulario.Codigo, Enumeradores.CaracteristicaFormulario.IntegracionSalidas, formulario.AccionContable, errores)
                End If

                'Valida se já existe um formulário de solicitação de fundos desse tipo (conjunto de caracteristicas)
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos) Then

                    'Se o formulário é para solicitação de fundos, então é obrigatório ter integração com o salidas
                    If Not VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionSalidas) Then
                        errores.AppendLine(String.Format(Traduzir("045_Integracion_Salidas_Obligatorio")))
                        errores.AppendLine()
                    End If

                    ValidarFormulariosDeIntegraciones(caracteristicas, formulario.Codigo, Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos, formulario.AccionContable, errores)
                End If

                'Valida se já existe um formulário de contestar fundos desse tipo (conjunto de caracteristicas)
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ContestarSolicitacionDeFondos) Then

                    'Se o formulário é para solicitação de fundos, então é obrigatório ter integração com o salidas
                    If Not VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionSalidas) Then
                        errores.AppendLine(String.Format(Traduzir("045_Integracion_Salidas_Obligatorio")))
                        errores.AppendLine()
                    End If

                    ValidarFormulariosDeIntegraciones(caracteristicas, formulario.Codigo, Enumeradores.CaracteristicaFormulario.ContestarSolicitacionDeFondos, formulario.AccionContable, errores)
                End If

                'Valida se já existe um formulário de contestar fundos desse tipo (conjunto de caracteristicas)
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeFondos) AndAlso _
                   VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Ajustes) AndAlso _
                   VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SoloPermitirAjustesNegativos) Then

                    'Se o formulário é para solicitação de fundos, então é obrigatório ter integração com o salidas
                    If Not VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionSalidas) Then
                        errores.AppendLine(String.Format(Traduzir("045_Integracion_Salidas_Obligatorio")))
                        errores.AppendLine()
                    End If

                    ValidarFormulariosDeIntegraciones(caracteristicas, formulario.Codigo, Enumeradores.CaracteristicaFormulario.SoloPermitirAjustesNegativos, formulario.AccionContable, errores)
                End If

                'Valida se já existe um formulário de contestar fundos desse tipo (conjunto de caracteristicas)
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeFondos) AndAlso _
                   VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Ajustes) AndAlso _
                   VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SoloPermitirAjustesPositivos) Then

                    'Se o formulário é para solicitação de fundos, então é obrigatório ter integração com o salidas
                    If Not VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionSalidas) Then
                        errores.AppendLine(String.Format(Traduzir("045_Integracion_Salidas_Obligatorio")))
                        errores.AppendLine()
                    End If

                    ValidarFormulariosDeIntegraciones(caracteristicas, formulario.Codigo, Enumeradores.CaracteristicaFormulario.SoloPermitirAjustesPositivos, formulario.AccionContable, errores)
                End If
            End If

            If errores.Length Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, errores.ToString)
            End If

        End Sub

        Private Shared Sub ValidarFormulariosDeIntegraciones(caracteristicas As List(Of Enumeradores.CaracteristicaFormulario),
                                                             codigoFormulario As String,
                                                             caracteristicaPrincipal As Enumeradores.CaracteristicaFormulario,
                                                             accionContable As Clases.AccionContable,
                                                             ByRef errores As StringBuilder)

            Dim _formularios As ObservableCollection(Of Clases.Formulario) = Nothing

            BorrarCaracteristicasIntegracion(caracteristicas)
            caracteristicas.Add(caracteristicaPrincipal)

            If caracteristicaPrincipal = Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos OrElse _
                caracteristicaPrincipal = Enumeradores.CaracteristicaFormulario.ContestarSolicitacionDeFondos Then
                caracteristicas.Add(Enumeradores.CaracteristicaFormulario.IntegracionSalidas)
            End If

            ' TO DO: Retirar esta consulta e colocar uma com performance melhor
            _formularios = AccesoDatos.GenesisSaldos.Formulario.ObtenerFormulariosConLasCaracteristicas_v2(caracteristicas, False)

            If _formularios IsNot Nothing AndAlso _formularios.Count > 0 Then

                For Each _formulario In _formularios
                    If accionContable IsNot Nothing Then
                        If accionContable.Identificador = _formulario.AccionContable.Identificador AndAlso Not codigoFormulario.Equals(_formulario.Codigo) Then
                            errores.AppendLine(String.Format(Traduzir("045_Formulario_Integracion_Existe"), Traduzir(caracteristicaPrincipal.RecuperarValor), _formulario.Codigo, TraduzirCaracteristicas(caracteristicas)))
                            errores.AppendLine()
                        End If
                    Else
                        If Not codigoFormulario.Equals(_formulario.Codigo) Then
                            errores.AppendLine(String.Format(Traduzir("045_Formulario_Integracion_Existe"), Traduzir(caracteristicaPrincipal.RecuperarValor), _formulario.Codigo, TraduzirCaracteristicas(caracteristicas)))
                            errores.AppendLine()
                        End If
                    End If
                Next
            End If

        End Sub

        Private Shared Function TraduzirCaracteristicas(caracteristicas As List(Of Enumeradores.CaracteristicaFormulario)) As String
            Dim traduzido As String = String.Empty

            Dim sbCaracteristicas As New StringBuilder

            If caracteristicas IsNot Nothing AndAlso caracteristicas.Count > 0 Then
                'Traduz as características
                For Each caracteristica In caracteristicas
                    sbCaracteristicas.Append(", <b>")
                    sbCaracteristicas.Append(Traduzir(caracteristica.RecuperarValor()))
                    sbCaracteristicas.Append("</b>")
                Next

                traduzido = sbCaracteristicas.ToString.Substring(2)
            End If

            Return traduzido
        End Function

        ''' <summary>
        ''' Remove as características de integração
        ''' </summary>
        ''' <param name="caracteristicas"></param>
        ''' <remarks></remarks>
        Private Shared Sub BorrarCaracteristicasIntegracion(ByRef caracteristicas As List(Of Enumeradores.CaracteristicaFormulario))

            If caracteristicas IsNot Nothing AndAlso caracteristicas.Count > 0 Then

                caracteristicas.RemoveAll(Function(c) c = Enumeradores.CaracteristicaFormulario.IntegracionConteo)
                caracteristicas.RemoveAll(Function(c) c = Enumeradores.CaracteristicaFormulario.IntegracionLegado)
                caracteristicas.RemoveAll(Function(c) c = Enumeradores.CaracteristicaFormulario.IntegracionRecepcionEnvio)
                caracteristicas.RemoveAll(Function(c) c = Enumeradores.CaracteristicaFormulario.IntegracionSalidas)
                caracteristicas.RemoveAll(Function(c) c = Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos)
                caracteristicas.RemoveAll(Function(c) c = Enumeradores.CaracteristicaFormulario.ContestarSolicitacionDeFondos)

            End If

        End Sub

        ''' <summary>
        ''' Remove as características de adicionais
        ''' </summary>
        ''' <param name="caracteristicas"></param>
        ''' <remarks></remarks>
        Private Shared Sub BorrarCaracteristicasAdicionales(ByRef caracteristicas As List(Of Enumeradores.CaracteristicaFormulario))

            If caracteristicas IsNot Nothing AndAlso caracteristicas.Count > 0 Then

                caracteristicas.RemoveAll(Function(c) c = Enumeradores.CaracteristicaFormulario.AlConfirmarElDocumentoSeImprime)
                caracteristicas.RemoveAll(Function(c) c = Enumeradores.CaracteristicaFormulario.MovimientoConInformacionesDeRota)
                caracteristicas.RemoveAll(Function(c) c = Enumeradores.CaracteristicaFormulario.CodigoExternoObligatorio)

            End If
        End Sub

        Protected Shared Function VerificaCaracteristicaExiste(formulario As Clases.Formulario, caracteristica As Enumeradores.CaracteristicaFormulario) As Boolean
            If formulario IsNot Nothing AndAlso formulario.Caracteristicas IsNot Nothing Then
                Return formulario.Caracteristicas.Where(Function(c) c.Equals(caracteristica)).Count > 0
            End If
            Return False
        End Function

        Private Shared Sub ValidarGuardarFormulario(formulario As Clases.Formulario)
            Dim erros As New System.Text.StringBuilder
            If formulario Is Nothing Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("045_Formulario_Vazio"))
            End If

            If String.IsNullOrEmpty(formulario.Identificador) Then
                erros.AppendLine(Traduzir("045_Identificador_Obligatorio"))
            End If

            If Not (formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Sustitucion) OrElse formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Classificacion)) Then
                If (formulario.AccionContable Is Nothing OrElse String.IsNullOrEmpty(formulario.AccionContable.Identificador)) AndAlso Not formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Sustitucion) Then
                    erros.AppendLine(Traduzir("046_Identificador_Accion_Contable_Obrigatorio"))
                End If
            End If

            If formulario.TipoDocumento Is Nothing OrElse String.IsNullOrEmpty(formulario.TipoDocumento.Identificador) Then
                erros.AppendLine(Traduzir("045_Tipo_Documento_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.Codigo) Then
                erros.AppendLine(Traduzir("045_Codigo_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.Descripcion) Then
                erros.AppendLine(Traduzir("045_Descricion_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.Color.Name) Then
                erros.AppendLine(Traduzir("045_Color_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.EstaActivo) Then
                erros.AppendLine(Traduzir("045_Bol_Activo_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.FechaHoraCreacion) Then
                erros.AppendLine(Traduzir("045_Fecha_Creacion_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.UsuarioCreacion) Then
                erros.AppendLine(Traduzir("045_Fecha_Creacion_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.FechaHoraModificacion) Then
                erros.AppendLine(Traduzir("045_Fecha_Modificacion_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.UsuarioModificacion) Then
                erros.AppendLine(Traduzir("045_Usuario_Modificacion_Obligatorio"))
            End If

            If erros.Length > 0 Then
                Throw New Excepcion.CampoObrigatorioException(erros.ToString)
            End If

        End Sub

        Public Shared Sub GuardarFormulario(formulario As Clases.Formulario, estadosAccionesContables As List(Of Clases.EstadoAccionContable), _
                                                 tipoSectoresOrigen As List(Of Clases.TipoSector), tipoSectoresDestino As List(Of Clases.TipoSector), _
                                                 tiposBultos As List(Of Clases.TipoBulto), copias As List(Of Clases.Copia))
            Try
                ValidarGuardarFormulario(formulario)
                ValidarFormulariosDeIntegraciones(formulario)

                'Transação
                Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted})

                    'Accion Contable
                    If formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Sustitucion) _
                        OrElse formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos) _
                       OrElse formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Classificacion) Then

                        Dim accionContableNoMueveValores As Clases.AccionContable = LogicaNegocio.GenesisSaldos.AccionContable.ObtenerAccionContablePorCodigo("AC_000000000001")

                        If accionContableNoMueveValores IsNot Nothing Then
                            formulario.AccionContable = accionContableNoMueveValores
                        Else
                            Throw New Excepcion.CampoObrigatorioException(Traduzir("047_Accion_No_Mueve_Valores_No_Encontrada"))
                        End If

                    Else

                        'Se a ação contábil não existir, adiciona a nova ação contábil e seus estados
                        If LogicaNegocio.GenesisSaldos.AccionContable.ObtenerAccionContable(formulario.AccionContable.Identificador) Is Nothing Then

                            If estadosAccionesContables Is Nothing OrElse estadosAccionesContables.Count = 0 Then
                                Throw New Excepcion.CampoObrigatorioException(Traduzir("047_Estados_Vazio"))
                            End If

                            LogicaNegocio.GenesisSaldos.AccionContable.GuardarAccionContable(formulario.AccionContable)
                            For Each estado In estadosAccionesContables
                                estado.IdentificadorAccionContable = formulario.AccionContable.Identificador
                            Next
                            LogicaNegocio.GenesisSaldos.EstadoAccionContable.GuardarEstadosAccionesContables(estadosAccionesContables)
                        End If

                    End If

                    'Formulário
                    AccesoDatos.GenesisSaldos.Formulario.GuardarFormulario(formulario)
                    'Características Formulário
                    LogicaNegocio.GenesisSaldos.CaracteristicaFormulario.GuardarCaracteristicasFormulario(formulario)
                    'Tipos Setores do Formulario - Origem
                    LogicaNegocio.Genesis.TipoSector.GuardarTiposSectoresFormulario(formulario.Identificador, "O", tipoSectoresOrigen)
                    'Tipos Setores do Formulario - Destino
                    LogicaNegocio.Genesis.TipoSector.GuardarTiposSectoresFormulario(formulario.Identificador, "D", tipoSectoresDestino)
                    'Tipos de Bultos do Formulário
                    If tiposBultos IsNot Nothing AndAlso tiposBultos.Count > 0 Then
                        LogicaNegocio.Genesis.TipoBulto.GuardaTiposBultosFormulario(formulario.Identificador, tiposBultos)
                    End If
                    'Copias do formulario
                    If copias IsNot Nothing AndAlso copias.Count > 0 Then
                        LogicaNegocio.GenesisSaldos.Copia.GuardarCopiasFormulario(formulario.Identificador, copias)
                    End If

                    'se não deu erro então realiza a transação.
                    transaction.Complete()

                End Using

            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub

        Public Shared Sub AtualizarFormulario(formulario As Clases.Formulario, estadosAccionesContables As List(Of Clases.EstadoAccionContable), _
                                                 tipoSectoresOrigen As List(Of Clases.TipoSector), tipoSectoresDestino As List(Of Clases.TipoSector), _
                                                 tiposBultos As List(Of Clases.TipoBulto), copias As List(Of Clases.Copia))
            Try
                ValidarGuardarFormulario(formulario)
                ValidarFormulariosDeIntegraciones(formulario)

                'Transação
                Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted})

                    'Accion Contable
                    If formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Sustitucion) _
                        OrElse formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos) _
                       OrElse formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Classificacion) Then

                        Dim accionContableNoMueveValores As Clases.AccionContable = LogicaNegocio.GenesisSaldos.AccionContable.ObtenerAccionContablePorCodigo("AC_000000000001")

                        If accionContableNoMueveValores IsNot Nothing Then
                            formulario.AccionContable = accionContableNoMueveValores
                        Else
                            Throw New Excepcion.CampoObrigatorioException(Traduzir("047_Accion_No_Mueve_Valores_No_Encontrada"))
                        End If

                    Else

                        'Se a ação contábil não existir, adiciona a nova ação contábil e seus estados
                        If LogicaNegocio.GenesisSaldos.AccionContable.ObtenerAccionContable(formulario.AccionContable.Identificador) Is Nothing Then

                            If estadosAccionesContables Is Nothing OrElse estadosAccionesContables.Count = 0 Then
                                Throw New Excepcion.CampoObrigatorioException(Traduzir("047_Estados_Vazio"))
                            End If

                            LogicaNegocio.GenesisSaldos.AccionContable.GuardarAccionContable(formulario.AccionContable)
                            For Each estado In estadosAccionesContables
                                estado.IdentificadorAccionContable = formulario.AccionContable.Identificador
                            Next
                            LogicaNegocio.GenesisSaldos.EstadoAccionContable.GuardarEstadosAccionesContables(estadosAccionesContables)
                        End If

                    End If

                    'EXCLUI OS DADOS DO FORMULARIO E DAS TABELAS RELACIONADAS PARA INSERIR NOVAMENTE
                    'COM OS DADOS ATUALIZADOS
                    LogicaNegocio.GenesisSaldos.CaracteristicaFormulario.BorrarCaracteristicaFormularioPorFormulario(formulario.Identificador)
                    LogicaNegocio.Genesis.TipoSector.BorrarTiposSectoresFormulario(formulario.Identificador)
                    LogicaNegocio.Genesis.TipoBulto.BorrarTiposBultosFormulario(formulario.Identificador)
                    LogicaNegocio.GenesisSaldos.Copia.BorrarCopiasFormulario(formulario.Identificador)

                    'INSERE OS DADOS NOVAMENTE
                    AccesoDatos.GenesisSaldos.Formulario.ActualizaFormulario(formulario)
                    'Características Formulário
                    LogicaNegocio.GenesisSaldos.CaracteristicaFormulario.GuardarCaracteristicasFormulario(formulario)
                    'Tipos Setores do Formulario - Origem
                    LogicaNegocio.Genesis.TipoSector.GuardarTiposSectoresFormulario(formulario.Identificador, "O", tipoSectoresOrigen)
                    'Tipos Setores do Formulario - Destino
                    LogicaNegocio.Genesis.TipoSector.GuardarTiposSectoresFormulario(formulario.Identificador, "D", tipoSectoresDestino)
                    'Tipos de Bultos do Formulário
                    If tiposBultos IsNot Nothing AndAlso tiposBultos.Count > 0 Then
                        LogicaNegocio.Genesis.TipoBulto.GuardaTiposBultosFormulario(formulario.Identificador, tiposBultos)
                    End If
                    'Copias do formulario
                    If copias IsNot Nothing AndAlso copias.Count > 0 Then
                        LogicaNegocio.GenesisSaldos.Copia.GuardarCopiasFormulario(formulario.Identificador, copias)
                    End If

                    'se não deu erro então realiza a transação.
                    transaction.Complete()

                End Using

            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub

        ''' <summary>
        ''' Exclui o formulário e as tabelas relacionadas
        ''' </summary>
        ''' <param name="identificadorFormulario"></param>
        ''' <remarks></remarks>
        Public Shared Sub ExcluirFormulario(identificadorFormulario As String)
            Try
                If String.IsNullOrEmpty(identificadorFormulario) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("msg_formulario_no_encuentrado"))
                End If

                'Se o formulário não possuir nenhum documento
                If Not FormularioHayDocumentos(identificadorFormulario) Then
                    AccesoDatos.GenesisSaldos.Formulario.BorrarFormulario(identificadorFormulario)
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Verifica se o formulário possui documentos
        ''' </summary>
        ''' <param name="idetificadorFormulario"></param>
        ''' <remarks></remarks>
        Public Shared Function FormularioHayDocumentos(idetificadorFormulario As String) As Boolean
            Try
                If String.IsNullOrEmpty(idetificadorFormulario) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("msg_formulario_no_encuentrado"))
                End If

                Return AccesoDatos.GenesisSaldos.Formulario.FormularioHayDocumentos(idetificadorFormulario)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        ''' <summary>
        ''' Verifica se o sector está configurado para ser utilizado no formulário informado
        ''' </summary>
        ''' <param name="identificadorFormulario"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FormularioVerificarSector(identificadorFormulario As String, identificadorSector As String, SectorOrigem As Boolean) As Boolean
            Try
                Return AccesoDatos.GenesisSaldos.Formulario.FormularioVerificarSector(identificadorFormulario, identificadorSector, SectorOrigem)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        ''' <summary>
        ''' Recupera os filtros formulário
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function ObtenerFiltrosFormulario() As List(Of Clases.FiltroFormulario)
            Try
                Return AccesoDatos.GenesisSaldos.FiltroFormulario.RecuperaFiltrosFormulario
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function ObtenerFormularioPaginacion(objPeticion As Prosegur.Genesis.Comon.Peticion(Of Clases.Sector)) _
                                                  As Comon.Respuesta(Of List(Of Clases.Formulario))

            Dim objRespuesta As New Comon.Respuesta(Of List(Of Clases.Formulario))

            Try

                ' obter formulário por identificador
                objRespuesta.Retorno = AccesoDatos.GenesisSaldos.Formulario.ObtenerFormularioPaginacion(objPeticion, objRespuesta)

                'Verifica se encontrou o formulário informado.
                If objRespuesta.Retorno.Count > 0 Then

                    For Each itemFormulario In objRespuesta.Retorno

                        'Recupera os terminos do grupoIAC para o documento individual
                        If itemFormulario.GrupoTerminosIACIndividual IsNot Nothing Then
                            itemFormulario.GrupoTerminosIACIndividual.TerminosIAC = Genesis.TerminoIAC.ObtenerTerminosIACPorIdentificador(itemFormulario.GrupoTerminosIACIndividual.Identificador)

                            ' Se existe o grupo termino então recupera os valores pssiveis para cada termino
                            If itemFormulario.GrupoTerminosIACIndividual.TerminosIAC IsNot Nothing AndAlso itemFormulario.GrupoTerminosIACIndividual.TerminosIAC.Count > 0 Then
                                For Each termino In itemFormulario.GrupoTerminosIACIndividual.TerminosIAC
                                    termino.ValoresPosibles = Genesis.ValorTerminoIAC.RecuperarValoresTerminosIAC(termino.Identificador)
                                Next
                            End If
                        End If

                        'Recupera os terminos do grupoIAC para o documento grupo
                        If itemFormulario.GrupoTerminosIACGrupo IsNot Nothing Then
                            itemFormulario.GrupoTerminosIACGrupo.TerminosIAC = Genesis.TerminoIAC.ObtenerTerminosIACPorIdentificador(itemFormulario.GrupoTerminosIACGrupo.Identificador)

                            ' Se existe o grupo termino então recupera os valores pssiveis para cada termino
                            If itemFormulario.GrupoTerminosIACGrupo.TerminosIAC IsNot Nothing AndAlso itemFormulario.GrupoTerminosIACGrupo.TerminosIAC.Count > 0 Then
                                For Each termino In itemFormulario.GrupoTerminosIACGrupo.TerminosIAC
                                    termino.ValoresPosibles = Genesis.ValorTerminoIAC.RecuperarValoresTerminosIAC(termino.Identificador)
                                Next
                            End If
                        End If
                    Next

                End If

            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

            Return objRespuesta
        End Function

        Public Shared Function ObtenerFormularioPaginacion(objPeticion As Prosegur.Genesis.Comon.Peticion(Of Clases.Sector), _
                                                           Optional RegresarFormulariosSustitucion As Boolean = False) _
                                                  As Comon.Respuesta(Of List(Of Clases.Formulario))

            Dim objRespuesta As New Comon.Respuesta(Of List(Of Clases.Formulario))

            Try

                Dim CodigosCaracteristicasParaIgnorar As New List(Of String)()

                If Not RegresarFormulariosSustitucion Then
                    CodigosCaracteristicasParaIgnorar.Add("ACCION_SUSTITUCION")
                End If

#If DEBUG Then
                CodigosCaracteristicasParaIgnorar.Clear()
#End If

                ' obter formulário por identificador
                objRespuesta.Retorno = AccesoDatos.GenesisSaldos.Formulario.ObtenerFormulario(objPeticion, objRespuesta, CodigosCaracteristicasParaIgnorar, ContractoServicio.Constantes.COD_RELACION_TIPO_SECTOR_ORIGEN)

                'Verifica se encontrou o formulário informado.
                If objRespuesta.Retorno.Count > 0 Then

                    For Each itemFormulario In objRespuesta.Retorno

                        'Recupera os terminos do grupoIAC documento individual
                        If itemFormulario.GrupoTerminosIACIndividual IsNot Nothing Then
                            itemFormulario.GrupoTerminosIACIndividual.TerminosIAC = Genesis.TerminoIAC.ObtenerTerminosIACPorIdentificador(itemFormulario.GrupoTerminosIACIndividual.Identificador)

                            ' Se existe o grupo termino então recupera os valores pssiveis para cada termino
                            If itemFormulario.GrupoTerminosIACIndividual.TerminosIAC IsNot Nothing AndAlso itemFormulario.GrupoTerminosIACIndividual.TerminosIAC.Count > 0 Then
                                For Each termino In itemFormulario.GrupoTerminosIACIndividual.TerminosIAC
                                    termino.ValoresPosibles = Genesis.ValorTerminoIAC.RecuperarValoresTerminosIAC(termino.Identificador)
                                Next
                            End If
                        End If

                        'Recupera os terminos do grupoIAC grupo de documento
                        If itemFormulario.GrupoTerminosIACGrupo IsNot Nothing Then
                            itemFormulario.GrupoTerminosIACGrupo.TerminosIAC = Genesis.TerminoIAC.ObtenerTerminosIACPorIdentificador(itemFormulario.GrupoTerminosIACGrupo.Identificador)

                            ' Se existe o grupo termino então recupera os valores pssiveis para cada termino
                            If itemFormulario.GrupoTerminosIACGrupo.TerminosIAC IsNot Nothing AndAlso itemFormulario.GrupoTerminosIACGrupo.TerminosIAC.Count > 0 Then
                                For Each termino In itemFormulario.GrupoTerminosIACGrupo.TerminosIAC
                                    termino.ValoresPosibles = Genesis.ValorTerminoIAC.RecuperarValoresTerminosIAC(termino.Identificador)
                                Next
                            End If
                        End If

                    Next

                End If

            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

            Return objRespuesta
        End Function


#Region "[Obtener Formulario por Caracteristica]"
        Public Shared Function obtenerFormularioConLasCaracteristicas_v2(CaracteristicasFormularioSalidas As List(Of Comon.Enumeradores.CaracteristicaFormulario),
                                                                Optional simplificado As Boolean = False,
                                                                Optional ByRef TransaccionActual As Transaccion = Nothing) As Clases.Formulario

            Dim formulario As Clases.Formulario
            Dim respuestaCaracteristicas As String = ""

            If CaracteristicasFormularioSalidas IsNot Nothing AndAlso CaracteristicasFormularioSalidas.Count > 0 Then
                For Each c In CaracteristicasFormularioSalidas
                    If String.IsNullOrEmpty(respuestaCaracteristicas) Then
                        respuestaCaracteristicas &= "Caracteristicas: "
                    Else
                        respuestaCaracteristicas &= ", "
                    End If
                    respuestaCaracteristicas &= c.RecuperarValor
                Next
                respuestaCaracteristicas &= ")"
            End If

            ' Recupera lista de formulários com estas caracteristicas
            Dim posiblesFormularios As ObservableCollection(Of Clases.Formulario) = Nothing

            If TransaccionActual Is Nothing Then
                posiblesFormularios = AccesoDatos.GenesisSaldos.Formulario.ObtenerFormulariosConLasCaracteristicas_v2(CaracteristicasFormularioSalidas, simplificado)
            Else
                posiblesFormularios = AccesoDatos.GenesisSaldos.Formulario.ObtenerFormulariosConLasCaracteristicas_v2(CaracteristicasFormularioSalidas, simplificado, TransaccionActual)
            End If

            If posiblesFormularios Is Nothing OrElse posiblesFormularios.Count = 0 Then
                ' Caso no retorne ningún formulario el Sistema deberá exhibir el mensaje: “No fue encontrado un formulario en el Nuevo Saldos que posibilite la secuencia del flujo”.
                Throw New Excepcion.NegocioExcepcion(Traduzir("101_formularioNoEncontrado") & respuestaCaracteristicas)
            ElseIf posiblesFormularios.Count > 1 Then
                ' Caso retorne más de uno formulario el Sistema deberá exhibir el mensaje: “No será posible seguir con el flujo porque hay más de un formulario disponible en Nuevo Saldos”.
                Throw New Excepcion.NegocioExcepcion(Traduzir("101_MasDeUnoformulario") & respuestaCaracteristicas)
            Else
                formulario = posiblesFormularios.FirstOrDefault
            End If

            Return formulario

        End Function
        Public Shared Function obtenerFormularioDeBaja(SalidasRecorrido As Boolean,
                                              Optional BolGestionaPorBulto As Boolean? = Nothing,
                                              Optional CodigoDelegacion As String = "",
                                              Optional Integracion As Comon.Enumeradores.CaracteristicaFormulario? = Nothing,
                                              Optional simplificado As Boolean = False,
                                              Optional ByRef TransaccionActual As Transaccion = Nothing) As Clases.Formulario

            Dim CaracteristicasFormulario As New List(Of Comon.Enumeradores.CaracteristicaFormulario)
            CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.Bajas)

            If SalidasRecorrido Then
                CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.SalidasRecorrido)
            End If

            If Integracion IsNot Nothing Then
                CaracteristicasFormulario.Add(Integracion)
            End If

            If BolGestionaPorBulto Is Nothing Then
                If String.IsNullOrEmpty(CodigoDelegacion) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_campo_obrigatorio"), Traduzir("gen_atr_obteneraplicacionversion_coddelegacion")))
                End If
                Dim listParametros As New List(Of String)
                listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_TRABAJA_POR_BULTO)
                Dim parametros As List(Of Clases.Parametro)
                If TransaccionActual Is Nothing Then
                    parametros = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(CodigoDelegacion, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, listParametros)
                Else
                    parametros = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(CodigoDelegacion, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, listParametros, TransaccionActual)
                End If
                If parametros IsNot Nothing AndAlso parametros.Count > 0 AndAlso parametros(0).valorParametro <> "0" Then
                    BolGestionaPorBulto = True
                Else
                    BolGestionaPorBulto = False
                End If
            End If

            If BolGestionaPorBulto Then
                CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.GestiondeBultos)
            Else
                CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.GestiondeRemesas)
            End If

            Return obtenerFormularioConLasCaracteristicas_v2(CaracteristicasFormulario, simplificado, TransaccionActual)

        End Function
        Public Shared Function obtenerFormularioDeAltas(Optional BolGestionaPorBulto As Boolean? = Nothing,
                                                        Optional CodigoDelegacion As String = "",
                                                        Optional Integracion As Comon.Enumeradores.CaracteristicaFormulario? = Nothing,
                                                        Optional simplificado As Boolean = False) As Clases.Formulario

            Dim CaracteristicasFormulario As New List(Of Comon.Enumeradores.CaracteristicaFormulario)
            CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.Altas)

            If Integracion IsNot Nothing Then
                CaracteristicasFormulario.Add(Integracion)
            End If

            If BolGestionaPorBulto Is Nothing Then
                If String.IsNullOrEmpty(CodigoDelegacion) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_campo_obrigatorio"), Traduzir("gen_atr_obteneraplicacionversion_coddelegacion")))
                End If
                Dim listParametros As New List(Of String)
                listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_TRABAJA_POR_BULTO)
                Dim parametros As List(Of Clases.Parametro) = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(CodigoDelegacion, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, listParametros)
                If parametros IsNot Nothing AndAlso parametros.Count > 0 AndAlso parametros(0).valorParametro <> "0" Then
                    BolGestionaPorBulto = True
                Else
                    BolGestionaPorBulto = False
                End If
            End If

            If BolGestionaPorBulto Then
                CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.GestiondeBultos)
            Else
                CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.GestiondeRemesas)
            End If

            Return obtenerFormularioConLasCaracteristicas_v2(CaracteristicasFormulario, simplificado)

        End Function
        Public Shared Function obtenerFormularioDeReenvio(Optional EntreSectoresDeLaMismaPlanta As Boolean? = Nothing,
                                                 Optional BolGestionaPorBulto As Boolean? = Nothing,
                                                 Optional CodigoDelegacion As String = "",
                                                 Optional Integracion As Comon.Enumeradores.CaracteristicaFormulario? = Nothing,
                                                 Optional reenvioAutomatico As Boolean = False,
                                                 Optional simplificado As Boolean = False) As Clases.Formulario

            Dim CaracteristicasFormulario As New List(Of Comon.Enumeradores.CaracteristicaFormulario)
            CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.Reenvios)

            If EntreSectoresDeLaMismaPlanta IsNot Nothing Then
                If EntreSectoresDeLaMismaPlanta Then
                    CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta)
                Else
                    CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes)
                End If
            End If

            If Integracion IsNot Nothing Then
                CaracteristicasFormulario.Add(Integracion)
            End If

            If BolGestionaPorBulto Is Nothing Then
                If String.IsNullOrEmpty(CodigoDelegacion) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_campo_obrigatorio"), Traduzir("gen_atr_obteneraplicacionversion_coddelegacion")))
                End If
                Dim listParametros As New List(Of String)
                listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_TRABAJA_POR_BULTO)
                Dim parametros As List(Of Clases.Parametro) = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(CodigoDelegacion, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, listParametros)
                If parametros IsNot Nothing AndAlso parametros.Count > 0 AndAlso parametros(0).valorParametro <> "0" Then
                    BolGestionaPorBulto = True
                Else
                    BolGestionaPorBulto = False
                End If
            End If

            If reenvioAutomatico Then
                CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.ReenvioAutomatico)
            End If

            If BolGestionaPorBulto Then
                CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.GestiondeBultos)
            Else
                CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.GestiondeRemesas)
            End If

            Return obtenerFormularioConLasCaracteristicas_v2(CaracteristicasFormulario, simplificado)
        End Function
        Public Shared Function obtenerFormularioDeGestiondeFondos(EntreSectoresDeLaMismaPlanta As Boolean,
                                                                  PermiteLlegarASaldoNegativo As Boolean,
                                                                  DocumentoEsDelTipoIndividual As Boolean,
                                                         Optional Integracion As Comon.Enumeradores.CaracteristicaFormulario? = Nothing,
                                                         Optional simplificado As Boolean = False) As Clases.Formulario

            Dim CaracteristicasFormulario As New List(Of Comon.Enumeradores.CaracteristicaFormulario)
            CaracteristicasFormulario.Add(Enumeradores.CaracteristicaFormulario.GestiondeFondos)
            CaracteristicasFormulario.Add(Enumeradores.CaracteristicaFormulario.MovimientodeFondos)

            If EntreSectoresDeLaMismaPlanta Then
                CaracteristicasFormulario.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta)
            End If

            If PermiteLlegarASaldoNegativo Then
                CaracteristicasFormulario.Add(Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo)
            End If

            If DocumentoEsDelTipoIndividual Then
                CaracteristicasFormulario.Add(Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoIndividual)
            Else
                CaracteristicasFormulario.Add(Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoGrupo)
            End If

            If Integracion IsNot Nothing Then
                CaracteristicasFormulario.Add(Integracion)
            End If

            Return obtenerFormularioConLasCaracteristicas_v2(CaracteristicasFormulario, simplificado)
        End Function
        Public Shared Function obtenerFormularioDeGestiondeFondosAjustes(EntreSectoresDeLaMismaPlanta As Boolean,
                                                                         DocumentoEsDelTipoIndividual As Boolean,
                                                                Optional SoloPermitirAjustesPositivos As Boolean? = Nothing,
                                                                Optional Integracion As Comon.Enumeradores.CaracteristicaFormulario? = Nothing,
                                                                Optional simplificado As Boolean = False) As Clases.Formulario

            Dim CaracteristicasFormulario As New List(Of Comon.Enumeradores.CaracteristicaFormulario)
            CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.GestiondeFondos)
            CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.Ajustes)

            If EntreSectoresDeLaMismaPlanta Then
                CaracteristicasFormulario.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta)
            End If

            If SoloPermitirAjustesPositivos IsNot Nothing Then
                If SoloPermitirAjustesPositivos Then
                    CaracteristicasFormulario.Add(Enumeradores.CaracteristicaFormulario.SoloPermitirAjustesPositivos)
                Else
                    CaracteristicasFormulario.Add(Enumeradores.CaracteristicaFormulario.SoloPermitirAjustesNegativos)
                End If
            End If

            If DocumentoEsDelTipoIndividual Then
                CaracteristicasFormulario.Add(Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoIndividual)
            Else
                CaracteristicasFormulario.Add(Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoGrupo)
            End If

            If Integracion IsNot Nothing Then
                CaracteristicasFormulario.Add(Integracion)
            End If

            Return obtenerFormularioConLasCaracteristicas_v2(CaracteristicasFormulario, simplificado)
        End Function
        Public Shared Function obtenerFormularioDeSustitucion(Optional BolGestionaPorBulto As Boolean? = Nothing,
                                                        Optional CodigoDelegacion As String = "",
                                                        Optional Integracion As Comon.Enumeradores.CaracteristicaFormulario? = Nothing,
                                                        Optional simplificado As Boolean = False) As Clases.Formulario

            Dim CaracteristicasFormulario As New List(Of Comon.Enumeradores.CaracteristicaFormulario)
            CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.Sustitucion)

            If Integracion IsNot Nothing Then
                CaracteristicasFormulario.Add(Integracion)
            End If

            If BolGestionaPorBulto Is Nothing Then
                If String.IsNullOrEmpty(CodigoDelegacion) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_campo_obrigatorio"), Traduzir("gen_atr_obteneraplicacionversion_coddelegacion")))
                End If
                Dim listParametros As New List(Of String)
                listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_TRABAJA_POR_BULTO)
                Dim parametros As List(Of Clases.Parametro) = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(CodigoDelegacion, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, listParametros)
                If parametros IsNot Nothing AndAlso parametros.Count > 0 AndAlso parametros(0).valorParametro <> "0" Then
                    BolGestionaPorBulto = True
                Else
                    BolGestionaPorBulto = False
                End If
            End If

            If BolGestionaPorBulto Then
                CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.GestiondeBultos)
            Else
                CaracteristicasFormulario.Add(Comon.Enumeradores.CaracteristicaFormulario.GestiondeRemesas)
            End If

            Return obtenerFormularioConLasCaracteristicas_v2(CaracteristicasFormulario, simplificado)

        End Function

#End Region

    End Class

End Namespace
