Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis

Public Class TerminoIac

#Region "[CONSULTA]"

#Region "DEMAIS METODOS"

    ''' <summary>
    ''' Busca o oid do Termino
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Function BuscaOidTermino(codigo As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidTermino.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim oid As String = String.Empty

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            oid = dt.Rows(0)("OID_TERMINO").ToString
        End If

        Return oid

    End Function

    ''' <summary>
    ''' Busca Todos os terminos iac
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Function BuscaTodosTerminosIac(codigo As String) As ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTodosTerminosIac.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IAC", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaTerminosIac As New ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaTerminosIac.Add(PopularTerminosIac(dr))
            Next
        End If
        Return objRetornaTerminosIac
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
    Private Shared Function PopularTerminosIac(dr As DataRow) As ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim objIac As New ContractoServicio.Iac.GetIacDetail.TerminosIac

        Util.AtribuirValorObjeto(objIac.CodigoTermino, dr("COD_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objIac.DescripcionTermino, dr("DES_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objIac.EsBusquedaParcial, dr("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objIac.EsCampoClave, dr("BOL_CAMPO_CLAVE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objIac.OrdenTermino, dr("NEC_ORDEN"), GetType(Integer))

        ' retornar objeto
        Return objIac
    End Function

    ''' <summary>
    ''' Obtém os TerminosIAC
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Public Shared Function GetComboTerminosIAC(objPeticion As ContractoServicio.Utilidad.GetComboTerminosIAC.Peticion) As ContractoServicio.Utilidad.GetComboTerminosIAC.TerminoColeccion

        ' criar objeto termino coleccion
        Dim objTerminosIAC As New ContractoServicio.Utilidad.GetComboTerminosIAC.TerminoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.getComboTerminosIAC.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.EsVigente))

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows

                ' adicionar para coleção
                objTerminosIAC.Add(PopularComboTerminosIAC(dr))

            Next

        End If

        ' retornar coleção de termino
        Return objTerminosIAC

    End Function

    ''' <summary>
    ''' Popula um objeto terminoIAC com os valores do datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Private Shared Function PopularComboTerminosIAC(dr As DataRow) As ContractoServicio.Utilidad.GetComboTerminosIAC.Termino

        ' criar objeto termino
        Dim objTerminoIAC As New ContractoServicio.Utilidad.GetComboTerminosIAC.Termino

        Util.AtribuirValorObjeto(objTerminoIAC.Codigo, dr("cod_termino"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoIAC.Descripcion, dr("des_termino"), GetType(String))

        ' retorna objeto preenchido
        Return objTerminoIAC

    End Function

    ''' <summary>
    ''' Preenche Termino por IAC - GetProcesos
    ''' </summary>
    ''' <param name="Oid_Iac"></param>
    ''' <param name="Oid_Cliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 31/03/2009 Criado
    ''' </history>
    Public Shared Function PopularTerminoIac(Oid_Iac As String, Oid_Cliente As String) As GetProcesos.TerminoIacColeccion

        'Cria objetos         
        Dim objTerminoIac As GetProcesos.TerminoIac = Nothing
        Dim objTerminoIacColeccion As GetProcesos.TerminoIacColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscarTerminoPorIac.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_IAC", ProsegurDbType.Objeto_Id, Oid_Iac))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, Oid_Cliente))

        'Preenche DataTable com o resultado da consulta
        Dim ProcesoPS As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If ProcesoPS IsNot Nothing AndAlso ProcesoPS.Rows.Count > 0 Then

            'Instancia objeto TerminoIacColeccion
            objTerminoIacColeccion = New GetProcesos.TerminoIacColeccion

            'Variável de comparação CodTermino
            Dim CodTermino As String = String.Empty

            'Cria objeto do tipo ValorPosible
            Dim objValPosibles As GetProcesos.ValorPosible = Nothing

            'Para cada registro do DataTable
            For Each row As DataRow In ProcesoPS.Rows

                If CodTermino <> row("COD_TERMINO").ToString() Then

                    'Instancia objeto TerminoIac
                    objTerminoIac = New GetProcesos.TerminoIac

                    'Preenche propriedades do objeto
                    With objTerminoIac

                        Util.AtribuirValorObjeto(.Codigo, row("COD_TERMINO"), GetType(String))
                        Util.AtribuirValorObjeto(.Descripcion, row("DES_TERMINO"), GetType(String))
                        Util.AtribuirValorObjeto(.Observacion, row("OBS_TERMINO"), GetType(String))
                        Util.AtribuirValorObjeto(.CodigoFormato, row("COD_FORMATO"), GetType(String))
                        Util.AtribuirValorObjeto(.DescripcionFormato, row("DES_FORMATO"), GetType(String))
                        Util.AtribuirValorObjeto(.Longitud, row("NEC_LONGITUD"), GetType(Integer))
                        Util.AtribuirValorObjeto(.CodigoMascara, row("COD_MASCARA"), GetType(String))
                        Util.AtribuirValorObjeto(.DescripcionMascara, row("DES_MASCARA"), GetType(String))
                        Util.AtribuirValorObjeto(.ExpRegularMascaraTerminoIAC, row("DES_EXP_REGULAR"), GetType(String))
                        Util.AtribuirValorObjeto(.CodigoAlgValidacion, row("COD_ALGORITMO_VALIDACION"), GetType(String))
                        Util.AtribuirValorObjeto(.DescripcionAlgValidacion, row("DES_ALGORITMO_VALIDACION"), GetType(String))
                        Util.AtribuirValorObjeto(.MostrarCodigo, row("BOL_MOSTRAR_CODIGO"), GetType(Boolean))
                        Util.AtribuirValorObjeto(.BusquedaParcial, row("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean))
                        Util.AtribuirValorObjeto(.CampoClave, row("BOL_CAMPO_CLAVE"), GetType(Boolean))
                        Util.AtribuirValorObjeto(.EsObligatorioTermino, row("BOL_ES_OBLIGATORIO"), GetType(Boolean))
                        Util.AtribuirValorObjeto(.esProtegidoTermino, row("BOL_ES_PROTEGIDO"), GetType(Boolean))
                        Util.AtribuirValorObjeto(.Orden, row("NEC_ORDEN"), GetType(Integer))
                        Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                        'Cria nova instância para ValorPosibleColeccion
                        .ValoresPosibles = New GetProcesos.ValorPosibleColeccion

                    End With

                    'Adciona objeto TerminoIac à coleção de TerminoIac
                    objTerminoIacColeccion.Add(objTerminoIac)

                End If

                'Instancia objeto ValorPosible
                objValPosibles = New GetProcesos.ValorPosible

                'Preenche propriedades objeto
                With objValPosibles

                    Util.AtribuirValorObjeto(.Codigo, row("COD_VALOR"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_VALOR"), GetType(String))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                End With

                If objValPosibles.Codigo IsNot Nothing Then
                    objTerminoIac.ValoresPosibles.Add(objValPosibles)
                End If

                CodTermino = row("COD_TERMINO").ToString()

            Next

        End If

        Return objTerminoIacColeccion

    End Function

#End Region

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir o termino iac no DB.
    ''' </summary>
    ''' <param name="objTerminoIac"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Sub AltaTerminoPorIac(objTerminoIac As ContractoServicio.Iac.SetIac.TerminosIac, _
                                        CodigoUsuario As String, _
                                        oidIac As String, _
                                        ByRef objtransacion As Transacao)
        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            ' Obtêm o comando
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaTerminoPorIac.ToString())
            comando.CommandType = CommandType.Text
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO_IAC", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_IAC", ProsegurDbType.Objeto_Id, oidIac))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, BuscaOidTermino(objTerminoIac.CodigoTermino)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_BUSQUEDA_PARCIAL", ProsegurDbType.Logico, objTerminoIac.EsBusquedaParcial))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CAMPO_CLAVE", ProsegurDbType.Logico, objTerminoIac.EsCampoClave))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ES_OBLIGATORIO", ProsegurDbType.Logico, objTerminoIac.EsObligatorio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_ORDEN", ProsegurDbType.Inteiro_Curto, objTerminoIac.OrdenTermino))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TERMINO_COPIA", ProsegurDbType.Logico, objTerminoIac.EsTerminoCopia))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ES_PROTEGIDO", ProsegurDbType.Logico, objTerminoIac.esProtegido))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ES_INVISIBLEREPORTE", ProsegurDbType.Logico, objTerminoIac.esInvisibleRpte))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ES_IDMECANIZADO", ProsegurDbType.Logico, objTerminoIac.esIdMecanizado))

            objtransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("006_msg_Erro_UKTerminoIac"))
        End Try

    End Sub

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por atualizar o termino iac no DB.
    ''' </summary>
    ''' <param name="objTerminoIac"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Sub ActualizarTerminoPorIac(objTerminoIac As ContractoServicio.Iac.SetIac.TerminosIac, _
                                              CodigoUsuario As String, _
                                              codIac As String, ByRef objtransacion As Transacao)
        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            ' Obtêm o comando
            comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarTerminoIac.ToString())
            comando.CommandType = CommandType.Text
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IAC", ProsegurDbType.Identificador_Alfanumerico, codIac))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_BUSQUEDA_PARCIAL", ProsegurDbType.Logico, objTerminoIac.EsBusquedaParcial))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CAMPO_CLAVE", ProsegurDbType.Logico, objTerminoIac.EsCampoClave))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ES_OBLIGATORIO", ProsegurDbType.Logico, objTerminoIac.EsObligatorio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_ORDEN", ProsegurDbType.Inteiro_Curto, objTerminoIac.OrdenTermino))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ES_PROTEGIDO", ProsegurDbType.Logico, objTerminoIac.EsProtegido))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ES_INVISIBLEREPORTE", ProsegurDbType.Logico, objTerminoIac.esInvisibleRpte))


            objtransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("006_msg_Erro_UKTerminoIac"))
        End Try

    End Sub

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Responsável por inserir o termino iac no DB.
    ''' </summary>
    ''' <param name="oidIac"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Sub BajaTerminoPorIac(oidIac As String, ByRef objtransacion As Transacao)
        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            ' Obtêm o comando
            comando.CommandText = Util.PrepararQuery(My.Resources.BajaTerminoPorIac.ToString())
            comando.CommandType = CommandType.Text
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_IAC", ProsegurDbType.Objeto_Id, oidIac))



            objtransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("006_msg_Erro_UKTerminoIac"))
        End Try

    End Sub

#End Region

End Class