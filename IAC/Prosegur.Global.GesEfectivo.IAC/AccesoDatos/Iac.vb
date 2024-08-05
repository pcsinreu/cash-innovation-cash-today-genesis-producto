Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports Prosegur.Genesis

Public Class Iac

#Region "[CONSTRUTORES]"

    ''' <summary>
    ''' Contrutor privado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()

    End Sub

#End Region

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Retorna IAC e seus terminos e valores possiveis para determinado cliente.
    ''' </summary>
    ''' <param name="oidIac"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] Criado 11/03/2009
    ''' </history>
    Public Shared Function RetornaIac(oidIac As String, _
                                      codCliente As String, _
                                      codSubCliente As String, _
                                      codPuntoServicio As String) As GetProceso.Iac

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim objColIac As New GetProceso.Iac

        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesoBuscaIac.ToString)
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_IAC", ProsegurDbType.Objeto_Id, oidIac))

        Dim dtIac As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Verifica se o dtIac retornou algum registro.
        If dtIac IsNot Nothing AndAlso dtIac.Rows.Count > 0 Then

            'Armazena a primeira linha no dt no 0bjIac.
            Return PopulaIacGetProceso(dtIac, codCliente, codSubCliente, codPuntoServicio)

        Else

            Return Nothing

        End If


    End Function

    ''' <summary>
    ''' Função faz a pesquisa das informações adicionais ao cliente com os parametros informados.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2008 Created
    ''' </history>
    Public Shared Function GetIac(peticion As ContractoServicio.Iac.GetIac.Peticion) As ContractoServicio.Iac.GetIac.IacColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.getIac.ToString()


        Dim filtro As New Text.StringBuilder

        'Monta Clausua 
        filtro.Append(MontaClausulaIac(peticion, comando))

        comando.CommandText &= filtro.ToString()

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim objColIac As New ContractoServicio.Iac.GetIac.IacColeccion

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Percorre dt e retorna coleção iac.
        objColIac = RetornaColecaoIac(dt)

        Return objColIac
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de iac
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoIac(dt As DataTable) As ContractoServicio.Iac.GetIac.IacColeccion

        Dim objRetornaIac As New ContractoServicio.Iac.GetIac.IacColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaIac.Add(PopularIac(dr))
            Next
        End If

        Return objRetornaIac
    End Function

    ''' <summary>
    ''' Monta Query Canal
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <param name="comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Private Shared Function MontaClausulaIac(peticion As ContractoServicio.Iac.GetIac.Peticion, ByRef comando As IDbCommand) As StringBuilder
        Dim filtro As New Text.StringBuilder

        filtro.Append(" WHERE IAC.BOL_VIGENTE = []BOL_VIGENTE ")

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, peticion.vigente))

        If peticion.CodidoIac <> String.Empty Then

            filtro.Append(" AND UPPER(IAC.COD_IAC) LIKE ([]COD_IAC)")

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IAC", ProsegurDbType.Identificador_Alfanumerico, "%" & peticion.CodidoIac.ToString & "%"))

        End If

        If peticion.DescripcionIac <> String.Empty Then

            filtro.Append(" AND UPPER(IAC.DES_IAC) LIKE ([]DES_IAC)")

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_IAC", ProsegurDbType.Descricao_Longa, "%" & peticion.DescripcionIac.ToString & "%"))

        End If


        Dim CodigoTermino As New List(Of String)
        Dim DescricaoTermino As New List(Of String)
        For Each terminos As ContractoServicio.Iac.GetIac.TerminosIac In peticion.TerminosIac
            CodigoTermino.Add(terminos.CodigoTermino)
            DescricaoTermino.Add(terminos.DescripcionTermino)
        Next

        filtro.Append(Util.MontarClausulaIn(CodigoTermino, "COD_TERMINO", comando, "AND", "TER"))
        filtro.Append(Util.MontarClausulaIn(DescricaoTermino, "DES_TERMINO", comando, "AND", "TER"))

        Return filtro
    End Function

    ''' <summary>
    ''' Função preenche o dr das informações adicionais.
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2008 Created
    ''' </history>
    Private Shared Function PopularIac(dr As DataRow) As ContractoServicio.Iac.GetIac.Iac

        Dim objIac As New ContractoServicio.Iac.GetIac.Iac

        Util.AtribuirValorObjeto(objIac.CodidoIac, dr("COD_IAC"), GetType(String))
        Util.AtribuirValorObjeto(objIac.DescripcionIac, dr("DES_IAC"), GetType(String))
        Util.AtribuirValorObjeto(objIac.ObservacionesIac, dr("OBS_IAC"), GetType(String))
        Util.AtribuirValorObjeto(objIac.vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objIac.DisponibleSaldos, dr("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean))

        ' retornar objeto
        Return objIac

    End Function

    ''' <summary>
    ''' Função faz a pesquisa detalhada das informações adicionais ao cliente com os parametros informados.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2008 Created
    ''' </history>
    Public Shared Function GetIacDetail(peticion As ContractoServicio.Iac.GetIacDetail.Peticion) As ContractoServicio.Iac.GetIacDetail.IacColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.GetIacDetail.ToString()


        Dim filtro As New Text.StringBuilder

        filtro.Append(Util.MontarClausulaIn(peticion.CodidoIac, "COD_IAC", comando, "WHERE", "IAC"))


        comando.CommandText &= filtro.ToString()

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim objColIac As New ContractoServicio.Iac.GetIacDetail.IacColeccion

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim codigoIac As String = dt.Rows(0)("COD_IAC")

            Dim objIac As ContractoServicio.Iac.GetIacDetail.Iac = PopulaIac(dt.Rows(0))
            objIac.TerminosIac = New ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion

            For Each dr As DataRow In dt.Rows
                If dr("COD_IAC") <> codigoIac Then

                    objColIac.Add(objIac)

                    ' Criar outro canal
                    objIac = PopulaIac(dr)
                    objIac.TerminosIac = New ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion

                    If VerificaTerminoIac(dr) Then
                        objIac.TerminosIac.Add(PopulaTerminosIac(dr))
                    End If

                Else
                    ' Se o canal é igual ao corrente, significa que a linha representa outro SubCanal
                    If VerificaTerminoIac(dr) Then
                        objIac.TerminosIac.Add(PopulaTerminosIac(dr))
                    End If

                End If
            Next

            objColIac.Add(objIac)
        End If

        ' retornar objeto
        Return objColIac
    End Function

    ''' <summary>
    ''' Função Selecionar, faz a pesquisa e preenche do datatable
    ''' </summary>
    ''' <param name="objpeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/03/2008 Created
    ''' </history>
    Public Shared Function GetIacIntegracion(objPeticion As GetIac.Peticion) As GetIac.IacColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.GetIacDetail.ToString()

        Dim filtros As New System.Text.StringBuilder

        If objPeticion.Vigente IsNot Nothing Then

            If filtros.Length > 0 Then
                filtros.Append(" AND IAC.BOL_VIGENTE = []BOL_VIGENTE")
            Else
                filtros.Append(" IAC.BOL_VIGENTE = []BOL_VIGENTE")
            End If

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.Vigente))
        End If

        If objPeticion.FechaInical <> Nothing Then

            If filtros.Length > 0 Then
                filtros.Append(" AND IAC.FYH_ACTUALIZACION >= []DATAINI")
            Else
                filtros.Append(" IAC.FYH_ACTUALIZACION >= []DATAINI")
            End If

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DATAINI", ProsegurDbType.Data, objPeticion.FechaInical))

        End If

        If objPeticion.FechaFinal <> Nothing Then

            If filtros.Length > 0 Then
                filtros.Append(" AND IAC.FYH_ACTUALIZACION <= []DATAFIM")
            Else
                filtros.Append(" IAC.FYH_ACTUALIZACION <= []DATAFIM")
            End If

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DATAFIM", ProsegurDbType.Data, objPeticion.FechaFinal & " 23:59"))

        End If


        If (filtros.Length > 0) Then
            comando.CommandText &= " WHERE " & filtros.ToString
        End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objColIac As New GetIac.IacColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objIac As GetIac.Iac
            Dim objTerminosIac As New GetIac.TerminoIac

            objIac = New GetIac.Iac

            For Each dr As DataRow In dt.Rows


                If SelectColIac(objColIac, dr("COD_IAC")) = False Then
                    objIac = New GetIac.Iac
                    objIac = PopulaIacIntegracion(dr)
                    objColIac.Add(objIac)
                End If

            Next

            For Each Ic As GetIac.Iac In objColIac

                Ic.TerminosIac = New GetIac.TerminoIacColeccion
                For Each dr As DataRow In dt.Rows

                    If Ic.Codigo = dr("COD_IAC") Then
                        If VerificaTerminoIac(dr) Then
                            objTerminosIac = New GetIac.TerminoIac
                            objTerminosIac = PopulaTerminosIacIntegracion(dr)
                            Ic.TerminosIac.Add(objTerminosIac)
                        End If
                    End If
                Next
            Next
        End If

        ' retornar objeto
        Return objColIac

    End Function

    ''' <summary>
    ''' Função responsável por fazer um select e verificar se o codigo informado existe na coleção retornando true or false.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2008 Created
    ''' </history>
    Private Shared Function SelectColIac(Iac As GetIac.IacColeccion, codigo As String) As Boolean

        Dim retorno = From c In Iac Where c.Codigo = codigo

        If retorno.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Função preenche o dr das informações adicionais.
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2008 Created
    ''' </history>
    Private Shared Function PopulaIac(dr As DataRow) As ContractoServicio.Iac.GetIacDetail.Iac

        Dim objIac As New ContractoServicio.Iac.GetIacDetail.Iac

        Util.AtribuirValorObjeto(objIac.CodidoIac, dr("COD_IAC"), GetType(String))

        Util.AtribuirValorObjeto(objIac.DescripcionIac, dr("DES_IAC"), GetType(String))

        Util.AtribuirValorObjeto(objIac.ObservacionesIac, dr("OBS_IAC"), GetType(String))

        Util.AtribuirValorObjeto(objIac.vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.EsDeclaradoCopia, dr("BOL_COPIA_DECLARADOS"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.EsInvisible, dr("BOL_INVISIBLE"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.EspecificoSaldos, dr("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean))

        ' retornar objeto
        Return objIac
    End Function

    ''' <summary>
    ''' Popula o objIac
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Private Shared Function PopulaIacGetProceso(dt As DataTable, _
                                                cod_cliente As String, _
                                                cod_sub_cliente As String, _
                                                cod_punto_servicio As String) As GetProceso.Iac

        Dim objIac As New GetProceso.Iac

        Util.AtribuirValorObjeto(objIac.Codigo, dt(0)("COD_IAC"), GetType(String))

        Util.AtribuirValorObjeto(objIac.Descripcion, dt(0)("DES_IAC"), GetType(String))

        Util.AtribuirValorObjeto(objIac.EsDeclaradoCopia, dt(0)("BOL_COPIA_DECLARADOS"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.EsInvisible, dt(0)("BOL_INVISIBLE"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.EspecificoSaldos, dt(0)("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean))

        'Popula os terminos de iac e em seguida os valores dos terminos
        objIac.TerminosIac = PopulaTerminosIac(dt, cod_cliente, cod_sub_cliente, cod_punto_servicio)

        Return objIac
    End Function

    Private Shared Function PopulaTerminosIac(dt As DataTable, _
                                              cod_cliente As String, _
                                              cod_sub_cliente As String, _
                                              cod_punto_servicio As String) As GetProceso.TerminoIacColeccion

        Dim objTerminosIAC As New GetProceso.TerminoIacColeccion

        For Each drTerminos In dt.Rows

            Dim objTermino As New GetProceso.TerminoIac

            objTermino.BusquedaParcial = drTerminos("BOL_BUSQUEDA_PARCIAL")
            objTermino.CampoClave = drTerminos("BOL_CAMPO_CLAVE")
            objTermino.Codigo = drTerminos("COD_TERMINO")
            objTermino.CodigoAlgoritnoValidacionIac = IIf(drTerminos("COD_ALGORITMO_VALIDACION") Is DBNull.Value, String.Empty, drTerminos("COD_ALGORITMO_VALIDACION"))
            objTermino.CodigoFormato = drTerminos("COD_FORMATO")
            objTermino.CodigoMascara = IIf(drTerminos("COD_MASCARA") Is DBNull.Value, String.Empty, drTerminos("COD_MASCARA"))
            objTermino.Descripcion = drTerminos("DES_TERMINO")
            objTermino.DescripcionAlgoritmoValidacionIac = IIf(drTerminos("DES_ALGORITMO_VALIDACION") Is DBNull.Value, String.Empty, drTerminos("DES_ALGORITMO_VALIDACION"))
            objTermino.DescripcionFormato = drTerminos("DES_FORMATO")
            objTermino.DescripcionMascara = IIf(drTerminos("DES_MASCARA") Is DBNull.Value, String.Empty, drTerminos("DES_MASCARA"))
            objTermino.ExpresionRegularMascara = IIf(drTerminos("DES_EXP_REGULAR") Is DBNull.Value, String.Empty, drTerminos("DES_EXP_REGULAR"))
            objTermino.Longitud = IIf(drTerminos("NEC_LONGITUD") Is DBNull.Value, 0, drTerminos("NEC_LONGITUD"))
            objTermino.MostarCodigo = drTerminos("BOL_MOSTRAR_CODIGO")
            objTermino.EsObligatorio = drTerminos("BOL_ES_OBLIGATORIO")
            objTermino.esProtegido = drTerminos("BOL_ES_PROTEGIDO")
            objTermino.Observaciones = IIf(drTerminos("OBS_TERMINO") Is DBNull.Value, String.Empty, drTerminos("OBS_TERMINO"))
            objTermino.Orden = drTerminos("NEC_ORDEN")
            objTermino.EsTerminoCopia = drTerminos("BOL_TERMINO_COPIA")
            objTermino.AceptarDigitacion = drTerminos("BOL_ACEPTAR_DIGITACION")
            objTermino.esInvisibleRpte = drTerminos("BOL_ES_INVISIBLEREPORTE")
            objTermino.esIdMecanizado = drTerminos("BOL_ES_IDMECANIZADO")

            'Obtem valores posiveis do termino IAC
            objTermino.ValoresPosibles = AccesoDatos.ValorPosible.RetornaValoresTerminoIac(cod_cliente, cod_sub_cliente, cod_punto_servicio, drTerminos("OID_TERMINO"))

            objTerminosIAC.Add(objTermino)

            objTermino = Nothing

        Next

        Return objTerminosIAC

    End Function

    ''' <summary>
    ''' Função preenche o dr das informações adicionais.
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2008 Created
    ''' </history>
    Private Shared Function PopulaIacIntegracion(dr As DataRow) As GetIac.Iac

        Dim objIac As New GetIac.Iac

        Util.AtribuirValorObjeto(objIac.Codigo, dr("COD_IAC"), GetType(String))

        Util.AtribuirValorObjeto(objIac.Descripcion, dr("DES_IAC"), GetType(String))

        Util.AtribuirValorObjeto(objIac.Observaciones, dr("OBS_IAC"), GetType(String))

        Util.AtribuirValorObjeto(objIac.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        ' retornar objeto
        Return objIac
    End Function

    ''' <summary>
    ''' Verifica se a informação adicional ao cliente esta retornando algum termino.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/02/2009 Created
    ''' </history>
    Public Shared Function VerificaTerminoIac(dr As DataRow) As Boolean
        If dr("COD_TERMINO") IsNot DBNull.Value Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Função preenche o dr de terminos iac.
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2008 Created
    ''' </history>
    Private Shared Function PopulaTerminosIac(dr As DataRow) As ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim objIac As New ContractoServicio.Iac.GetIacDetail.TerminosIac

        Util.AtribuirValorObjeto(objIac.CodigoTermino, dr("COD_TERMINO"), GetType(String))

        Util.AtribuirValorObjeto(objIac.DescripcionTermino, dr("DES_TERMINO"), GetType(String))

        Util.AtribuirValorObjeto(objIac.ObservacionesTermino, dr("OBS_TERMINO"), GetType(String))

        Util.AtribuirValorObjeto(objIac.CodigoFormatoTermino, dr("COD_FORMATO"), GetType(String))

        Util.AtribuirValorObjeto(objIac.DescripcionFormatoTermino, dr("DES_FORMATO"), GetType(String))

        Util.AtribuirValorObjeto(objIac.LongitudTermino, dr("NEC_LONGITUD"), GetType(Integer))

        Util.AtribuirValorObjeto(objIac.CodigoMascaraTermino, dr("COD_MASCARA"), GetType(String))

        Util.AtribuirValorObjeto(objIac.DescripcionMascaraTermino, dr("DES_MASCARA"), GetType(String))

        Util.AtribuirValorObjeto(objIac.ExpRegularMascaraTermino, dr("DES_EXP_REGULAR"), GetType(String))

        Util.AtribuirValorObjeto(objIac.CodigoAlgoritmoTermino, dr("COD_ALGORITMO_VALIDACION"), GetType(String))

        Util.AtribuirValorObjeto(objIac.DescripcionAlgoritmoTermino, dr("DES_ALGORITMO_VALIDACION"), GetType(String))

        Util.AtribuirValorObjeto(objIac.MostarCodigo, dr("BOL_MOSTRAR_CODIGO"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.AdmiteValoresPosibles, dr("BOL_VALORES_POSIBLES"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.VigenteTermino, dr("BOL_VIGENTE"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.EsBusquedaParcial, dr("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.EsCampoClave, dr("BOL_CAMPO_CLAVE"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.EsObligatorio, dr("BOL_ES_OBLIGATORIO"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.OrdenTermino, dr("NEC_ORDEN"), GetType(Integer))

        Util.AtribuirValorObjeto(objIac.EsTerminoCopia, dr("BOL_TERMINO_COPIA"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.esProtegido, dr("BOL_ES_PROTEGIDO"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.esInvisibleRpte, dr("BOL_ES_INVISIBLEREPORTE"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.esIdMecanizado, dr("BOL_ES_IDMECANIZADO"), GetType(Boolean))

        ' retornar objeto
        Return objIac
    End Function

    ''' <summary>
    ''' Função preenche o dr de terminos iac.
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2008 Created
    ''' </history>
    Private Shared Function PopulaTerminosIacIntegracion(dr As DataRow) As GetIac.TerminoIac

        Dim objIac As New GetIac.TerminoIac

        Util.AtribuirValorObjeto(objIac.Codigo, dr("COD_TERMINO"), GetType(String))

        Util.AtribuirValorObjeto(objIac.Descripcion, dr("DES_TERMINO"), GetType(String))

        Util.AtribuirValorObjeto(objIac.Observaciones, dr("OBS_TERMINO"), GetType(String))

        Util.AtribuirValorObjeto(objIac.CodigoFormatoTermino, dr("COD_FORMATO"), GetType(String))

        Util.AtribuirValorObjeto(objIac.DescripcionFormatoTermino, dr("DES_FORMATO"), GetType(String))

        Util.AtribuirValorObjeto(objIac.LongitudTermino, dr("NEC_LONGITUD"), GetType(Integer))

        Util.AtribuirValorObjeto(objIac.CodigoMascaraTermino, dr("COD_MASCARA"), GetType(String))

        Util.AtribuirValorObjeto(objIac.DescripcionMascaraTermino, dr("DES_MASCARA"), GetType(String))

        Util.AtribuirValorObjeto(objIac.ExpRegularMascara, dr("DES_EXP_REGULAR"), GetType(String))

        Util.AtribuirValorObjeto(objIac.CodigoAlgoritmoTermino, dr("COD_ALGORITMO_VALIDACION"), GetType(String))

        Util.AtribuirValorObjeto(objIac.DescripcionAlgoritmoTermino, dr("DES_ALGORITMO_VALIDACION"), GetType(String))

        Util.AtribuirValorObjeto(objIac.MostarCodigo, dr("BOL_MOSTRAR_CODIGO"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.AdmiteValoresPosibles, dr("BOL_VALORES_POSIBLES"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.EsBusquedaParcial, dr("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.EsCampoClave, dr("BOL_CAMPO_CLAVE"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.EsCampoClave, dr("BOL_ES_OBLIGATORIO"), GetType(Boolean))

        Util.AtribuirValorObjeto(objIac.OrdenTermino, dr("NEC_ORDEN"), GetType(Boolean))

        ' retornar objeto
        Return objIac
    End Function

    ''' <summary>
    ''' Faz a verificação se o iac possui algum processo vigente.
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Function verificarSiPoseeProcesoVigente(codigo As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.IacVerficarSePossuiProcessoVigente.ToString())
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IAC", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Busca o oid da informação adicional
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Function BuscaOidIac(codigo As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidIac.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IAC", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim oid As String = String.Empty

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            oid = dt.Rows(0)("OID_IAC").ToString
        End If

        Return oid

    End Function

    ''' <summary>
    ''' Verifica se o codigo existe no BD retornando verdadeiro ou falso.
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Function VerificarCodigoIac(codigo As String) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidIac.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IAC", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Verifica se a descrição informada existe no BD retornando verdadeiro ou falso.
    ''' </summary>
    ''' <param name="descricao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Function VerificarDescripcionIac(descricao As String) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionIac.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_IAC", ProsegurDbType.Descricao_Longa, descricao))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

#Region "[GETCOMBOINFORMACIONADICIONALCLIENTE]"

    ''' <summary>
    ''' Busca as informações adicionais vigentes e retorna uma coleção de Iac.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    Public Shared Function GetComboInformacionAdicional() As GetComboInformacionAdicional.IacColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetComboInformacionAdicional.ToString())

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaIac As New GetComboInformacionAdicional.IacColeccion

        'Percorre o dt e retorna uma coleção de iac.
        objRetornaIac = RetornaColecaoGetComboIac(dt)

        ' retornar objeto
        Return objRetornaIac

    End Function
    ''' <summary>
    ''' Busca as informações adicionais vigentes e retorna uma coleção de Iac.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    Public Shared Function GetComboInformacionAdicional(Peticion As GetComboInformacionAdicionalConFiltros.Peticion) As GetComboInformacionAdicionalConFiltros.IacColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetComboInformacionAdicional.ToString())

        If Peticion IsNot Nothing AndAlso Peticion.BolEspecificoSaldos Then
            comando.CommandText = comando.CommandText & " AND BOL_ESPECIFICO_DE_SALDOS = 1 "
        Else
            comando.CommandText = comando.CommandText & " AND BOL_ESPECIFICO_DE_SALDOS = 0 "
        End If

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaIac As New GetComboInformacionAdicionalConFiltros.IacColeccion

        'Percorre o dt e retorna uma coleção de iac.
        objRetornaIac = RetornaColecaoGetComboIacConFiltro(dt)

        ' retornar objeto
        Return objRetornaIac

    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de iac.
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoGetComboIac(dt As DataTable) As GetComboInformacionAdicional.IacColeccion

        Dim objRetornaIac As New GetComboInformacionAdicional.IacColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaIac.Add(PopularGetIac(dr))
            Next

        End If

        Return objRetornaIac
    End Function
    Private Shared Function RetornaColecaoGetComboIacConFiltro(dt As DataTable) As GetComboInformacionAdicionalConFiltros.IacColeccion

        Dim objRetornaIac As New GetComboInformacionAdicionalConFiltros.IacColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaIac.Add(PopularGetIacConFiltros(dr))
            Next

        End If

        Return objRetornaIac
    End Function


    ''' <summary>
    ''' Função PopularIac cria e preenche um objeto 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2008 Criado
    ''' </history>
    Private Shared Function PopularGetIac(dr As DataRow) As GetComboInformacionAdicional.Iac

        Dim objIac As New GetComboInformacionAdicional.Iac

        Util.AtribuirValorObjeto(objIac.Codigo, dr("COD_IAC"), GetType(String))
        Util.AtribuirValorObjeto(objIac.Descripcion, dr("DES_IAC"), GetType(String))

        ' retornar objeto
        Return objIac
    End Function
    Private Shared Function PopularGetIacConFiltros(dr As DataRow) As GetComboInformacionAdicionalConFiltros.Iac

        Dim objIac As New GetComboInformacionAdicionalConFiltros.Iac

        Util.AtribuirValorObjeto(objIac.Codigo, dr("COD_IAC"), GetType(String))
        Util.AtribuirValorObjeto(objIac.Descripcion, dr("DES_IAC"), GetType(String))

        ' retornar objeto
        Return objIac
    End Function

#End Region

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Deleta a informação adicional ao cliente.
    ''' </summary>
    ''' <param name="codigoIac"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Sub BajaIac(codigoIac As String, codigoUsuario As String)

        Dim objTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaIac.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IAC", ProsegurDbType.Identificador_Alfanumerico, codigoIac))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        objTransacao.AdicionarItemTransacao(comando)

        objTransacao.RealizarTransacao()

    End Sub

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir a Informação adicional ao cliente no DB.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Sub AltaIac(Peticion As ContractoServicio.Iac.SetIac.Peticion)

        Try

            ' criar transação
            Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaIac.ToString())
            comando.CommandType = CommandType.Text

            Dim oidIac As String = Guid.NewGuid.ToString
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_IAC", ProsegurDbType.Objeto_Id, oidIac))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IAC", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodidoIac))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_IAC", ProsegurDbType.Descricao_Longa, Peticion.DescripcionIac))
            If Peticion.ObservacionesIac <> String.Empty Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_IAC", ProsegurDbType.Observacao_Longa, Peticion.ObservacionesIac))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_IAC", ProsegurDbType.Observacao_Longa, String.Empty))
            End If
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, Peticion.vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_COPIA_DECLARADOS", ProsegurDbType.Logico, Peticion.EsDeclaradoCopia))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_INVISIBLE", ProsegurDbType.Logico, Peticion.EsInvisible))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ESPECIFICO_DE_SALDOS", ProsegurDbType.Logico, Peticion.EspecificoSaldos))

            ' adicionar item para transação
            objtransacion.AdicionarItemTransacao(comando)

            ' caso tenha terminos
            If Peticion.TerminosIac IsNot Nothing Then
                ' para cada termino
                For Each teriac As ContractoServicio.Iac.SetIac.TerminosIac In Peticion.TerminosIac

                    If teriac.EsCampoClave AndAlso teriac.EsTerminoCopia Then

                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                             Traduzir("006_msg_ErroCampoClave"))
                    End If

                    ' efetuar inserção do relacionamento IAC X Termino
                    TerminoIac.AltaTerminoPorIac(teriac, Peticion.CodUsuario, oidIac, objtransacion)
                Next

            End If

            ' realiza a transação
            objtransacion.RealizarTransacao()

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("006_msg_Erro_UKIac"))
        End Try

    End Sub

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por fazer a atualização do Iac do DB.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Sub ActualizarIac(Peticion As ContractoServicio.Iac.SetIac.Peticion, _
                                     ByRef objtransacion As Transacao)
        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            Dim query As New StringBuilder
            query.Append("UPDATE gepr_tinform_adicional_cliente SET ")

            query.Append(Util.AdicionarCampoQuery("des_iac = []des_iac,", "des_iac", comando, Peticion.DescripcionIac, ProsegurDbType.Descricao_Longa))
            query.Append(Util.AdicionarCampoQuery("obs_iac = []obs_iac,", "obs_iac", comando, Peticion.ObservacionesIac, ProsegurDbType.Observacao_Longa))
            query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, Peticion.vigente, ProsegurDbType.Logico))
            query.Append(Util.AdicionarCampoQuery("bol_invisible = []bol_invisible,", "bol_invisible", comando, Peticion.EsInvisible, ProsegurDbType.Logico))

            query.Append("cod_usuario = []cod_usuario, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodUsuario))

            query.Append("bol_copia_declarados = []bol_copia_declarados, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "bol_copia_declarados", ProsegurDbType.Logico, Peticion.EsDeclaradoCopia))

            query.Append("bol_especifico_de_saldos = []bol_especifico_de_saldos, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "bol_especifico_de_saldos", ProsegurDbType.Logico, Peticion.EspecificoSaldos))

            query.Append("fyh_actualizacion = []fyh_actualizacion ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data, DateTime.Now))

            query.Append("WHERE cod_iac = []cod_iac ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_iac", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodidoIac))

            comando.CommandText = Util.PrepararQuery(query.ToString())
            comando.CommandType = CommandType.Text

            objtransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            ' Utilizado para tratar o erro de Unique Constrant
            Excepcion.Util.Tratar(ex, Traduzir("006_msg_Erro_UKIac"))
        End Try

    End Sub

#End Region

End Class
