Imports Prosegur.DBHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

''' <summary>
''' Classe Morfologia
''' </summary>
''' <remarks></remarks>
''' <history>
''' [bruno.costa]  21/12/2010  criado
''' </history>
Public Class Morfologia

#Region "[CONSULTAS]"

    ''' <summary>
    ''' Obtém morfologias através dos filtros informados
    ''' </summary>
    ''' <param name="CodMorfologia"></param>
    ''' <param name="DesMorfologia"></param>
    ''' <param name="BolVigente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  21/12/2010  criado
    ''' </history>
    Public Shared Function GetMorfologias(CodMorfologia As String, DesMorfologia As String, BolVigente As Nullable(Of Boolean)) As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder()
        Dim where As New StringBuilder()
        Dim campos As New List(Of String)
        query.Append(My.Resources.GetMorfologia.ToString)

        'Adiciona Where caso algum parâmetro seja passado
        Dim clausulaWhere As New CriterioColecion

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, BolVigente.Value))

        If Not String.IsNullOrEmpty(CodMorfologia) Then
            'clausulaWhere.addCriterio("AND", " MOR.COD_MORFOLOGIA = []COD_MORFOLOGIA ")
            campos.Add(CodMorfologia)
            where.Append(Util.MontarClausulaLikeUpper(campos, "COD_MORFOLOGIA", comando, "AND", "MOR"))
            'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MORFOLOGIA", ProsegurDbType.Identificador_Alfanumerico, CodMorfologia))
        End If

        If Not String.IsNullOrEmpty(DesMorfologia) Then
            'clausulaWhere.addCriterio("AND", " MOR.DES_MORFOLOGIA = []DES_MORFOLOGIA ")
            'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_MORFOLOGIA", ProsegurDbType.Descricao_Longa, DesMorfologia))
            campos.Clear()
            campos.Add(DesMorfologia)
            where.Append(Util.MontarClausulaLikeUpper(campos, "DES_MORFOLOGIA", comando, "AND", "MOR"))
        End If

        comando.CommandText = Util.PrepararQuery(String.Format(query.ToString(), where.ToString()))
        comando.CommandType = CommandType.Text

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function

    ''' <summary>
    ''' Verifica se existe uma morfologia com os filtros informados
    ''' </summary>
    ''' <param name="CodMorfologia"></param>
    ''' <param name="DesMorfologia"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  29/12/2010  criado
    ''' </history>
    Public Shared Function VerificarMorfologia(CodMorfologia As String, DesMorfologia As String) As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim strClausulaWhere As String = String.Empty
        Dim query As New StringBuilder
        query.Append(My.Resources.VerificarMorfologia.ToString)

        'Adiciona Where caso algum parâmetro seja passado
        Dim clausulaWhere As New CriterioColecion

        If Not String.IsNullOrEmpty(CodMorfologia) Then
            clausulaWhere.addCriterio("AND", " MOR.COD_MORFOLOGIA = []COD_MORFOLOGIA ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MORFOLOGIA", ProsegurDbType.Identificador_Alfanumerico, CodMorfologia))
        End If

        If Not String.IsNullOrEmpty(DesMorfologia) Then
            clausulaWhere.addCriterio("AND", " MOR.DES_MORFOLOGIA = []DES_MORFOLOGIA ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_MORFOLOGIA", ProsegurDbType.Descricao_Longa, DesMorfologia))
        End If

        'Adiciona a clausula Where
        If clausulaWhere.Count > 0 Then
            strClausulaWhere = Util.MontarClausulaWhere(clausulaWhere)
        End If

        comando.CommandText = query.ToString() & " " & Util.PrepararQuery(strClausulaWhere)
        comando.CommandType = CommandType.Text

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

    End Function

    ''' <summary>
    ''' Obtém detalhes de uma morfologia
    ''' </summary>
    ''' <param name="OidMorfologia"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  21/12/2010  criado
    ''' </history>
    Public Shared Function GetMorfologia(OidMorfologia As String) As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim query As New StringBuilder
        query.Append(My.Resources.GetMorfologiaDetail.ToString)

        If Not String.IsNullOrEmpty(OidMorfologia) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MORFOLOGIA", ProsegurDbType.Objeto_Id, OidMorfologia))
        End If

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function

    ''' <summary>
    ''' Obtém data e hora da ultima atualização da morfologia
    ''' </summary>
    ''' <param name="OidMorfologia"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  21/12/2010  criado
    ''' </history>
    Public Shared Function GetFyhActualizacion(OidMorfologia As String) As DateTime

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim query As New StringBuilder
        query.Append(My.Resources.GetMorfologiaFyhActualizacion.ToString)

        If Not String.IsNullOrEmpty(OidMorfologia) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MORFOLOGIA", ProsegurDbType.Objeto_Id, OidMorfologia))
        End If

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        Dim fyhActualizacion As DateTime = Nothing

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso Not IsDBNull(dt.Rows(0)("FYH_ACTUALIZACION")) Then

            fyhActualizacion = dt.Rows(0)("FYH_ACTUALIZACION")

        End If

        Return fyhActualizacion

    End Function

    Public Shared Function GetMorfologiaByIdAtm(IdAtm As String, Optional FechaInicio As Date = Nothing) As List(Of GetATM.Morfologia)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        With comando

            .CommandText = Util.PrepararQuery(My.Resources.GetMorfologiaByIdAtm)
            .CommandType = CommandType.Text

            .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CAJERO", ProsegurDbType.Objeto_Id, IdAtm))

            If Not FechaInicio.Equals(Date.MinValue) Then

                'Si el parámetro de entrada fecServicio = null, recuperar todas las morfologías. 
                'Caso contrario, recuperar apenas la morfología vigente en la fecha informada.
                .CommandText += Util.PrepararQuery(" AND CM.FEC_INICIO <= []FEC_INICIO AND ROWNUM = 1 ORDER BY CM.FEC_INICIO DESC")
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_INICIO", ProsegurDbType.Data, FechaInicio))

            End If

        End With

        Return PreencherObjetoMorfologia(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando))

    End Function

    Private Shared Function PreencherObjetoMorfologia(Table As DataTable) As List(Of GetATM.Morfologia)

        Dim objMorfs As New List(Of GetATM.Morfologia)
        Dim objMorf As GetATM.Morfologia = Nothing

        For Each Row In Table.Rows

            objMorf = New GetATM.Morfologia

            Util.AtribuirValorObjeto(objMorf.CodigoMorfologia, Row("COD_MORFOLOGIA"), GetType(String))
            Util.AtribuirValorObjeto(objMorf.DescripcionMorfologia, Row("DES_MORFOLOGIA"), GetType(String))
            Util.AtribuirValorObjeto(objMorf.EsVigente, Row("BOL_VIGENTE"), GetType(Boolean))
            Util.AtribuirValorObjeto(objMorf.FechaInicio, Row("FEC_INICIO"), GetType(Date))
            Util.AtribuirValorObjeto(objMorf.IdMorfologia, Row("OID_MORFOLOGIA"), GetType(String))
            Util.AtribuirValorObjeto(objMorf.ModalidadRecogida, Row("NEC_MODALIDAD_RECOGIDA"), GetType(Integer))

            'Recuperar componentes da morfologia
            objMorf.Componentes = GetComponentesByIdMorfologia(objMorf.IdMorfologia)

            objMorfs.Add(objMorf)

        Next

        Return objMorfs

    End Function

    Private Shared Function GetComponentesByIdMorfologia(IdMorfologia As String) As List(Of GetATM.Componente)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        With comando

            .CommandText = Util.PrepararQuery(My.Resources.GetComponentesByIdMorfologia)
            .CommandType = CommandType.Text

            .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MORFOLOGIA", ProsegurDbType.Objeto_Id, IdMorfologia))

        End With

        Return PreencherObjetoComponente(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando))

    End Function

    Private Shared Function PreencherObjetoComponente(Table As DataTable) As List(Of GetATM.Componente)

        Dim objComps As New List(Of GetATM.Componente)
        Dim objComp As GetATM.Componente = Nothing

        For Each Row In Table.Rows

            objComp = New GetATM.Componente

            Util.AtribuirValorObjeto(objComp.IdComponenteMorfologia, Row("OID_MORFOLOGIA_COMPONENTE"), GetType(String))
            Util.AtribuirValorObjeto(objComp.CodigoTipoContenedor, Row("COD_TIPO_CONTENEDOR"), GetType(String))
            Util.AtribuirValorObjeto(objComp.FuncionContenedor, Row("NEC_FUNCION_CONTENEDOR"), GetType(Integer))

            'Recuperar objetos do componente
            objComp.Objetos = GetObjetosByIdComponente(objComp.IdComponenteMorfologia)

            objComps.Add(objComp)

        Next

        Return objComps

    End Function

    Private Shared Function GetObjetosByIdComponente(IdComponente As String) As List(Of GetATM.Objeto)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        With comando

            .CommandText = Util.PrepararQuery(My.Resources.GetObjetosByIdComponente)
            .CommandType = CommandType.Text

            .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MORFOLOGIA_COMPONENTE", ProsegurDbType.Objeto_Id, IdComponente))

        End With

        Return PreencherObjetos(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando))

    End Function

    Private Shared Function PreencherObjetos(Table As DataTable) As List(Of GetATM.Objeto)

        Dim objObjs As New List(Of GetATM.Objeto)
        Dim objObj As GetATM.Objeto = Nothing

        For Each Row In Table.Rows

            objObj = New GetATM.Objeto

            Util.AtribuirValorObjeto(objObj.CodigoIsoDivisa, Row("COD_ISO_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objObj.CodigoDenominacion, Row("COD_DENOMINACION"), GetType(String))
            Util.AtribuirValorObjeto(objObj.CodigoTipoMedioPago, Row("COD_TIPO_MEDIO_PAGO"), GetType(String))

            objObjs.Add(objObj)

        Next

        Return objObjs

    End Function

#End Region

#Region "[DELETES]"

    Public Shared Sub BorrarMorfologia(OidMorfologia As String, ByRef Transacion As DbHelper.Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim query As New StringBuilder
        query.Append(My.Resources.BorrarMorfologia.ToString)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MORFOLOGIA", ProsegurDbType.Objeto_Id, OidMorfologia))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        If Transacion IsNot Nothing Then
            Transacion.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

    End Sub

    Public Shared Sub BorrarMorfologiaXComp(OidMorfologia As String, ByRef Transacion As DbHelper.Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim query As New StringBuilder
        query.Append(My.Resources.BorrarMorfolXComp.ToString)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MORFOLOGIA", ProsegurDbType.Objeto_Id, OidMorfologia))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        If Transacion IsNot Nothing Then
            Transacion.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

    End Sub

#End Region

#Region "[INSERTS]"

    ''' <summary>
    ''' Insere uma morfologia
    ''' </summary>
    ''' <param name="CodMorfologia"></param>
    ''' <param name="DesMorfologia"></param>
    ''' <param name="BolVigente"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="FyhActualizacion"></param>
    ''' <param name="Transacion"></param>
    ''' <returns>Oid da morfologia criada</returns>
    ''' <remarks></remarks>
    Public Shared Function InsertMorfologia(CodMorfologia As String, DesMorfologia As String, _
                                            BolVigente As Boolean, _
                                            CodUsuario As String, _
                                            FyhActualizacion As DateTime, _
                                            NecModalidadRecogida As Integer, _
                                            ByRef Transacion As DbHelper.Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim oidMorfologia As String = Guid.NewGuid().ToString()

        Dim query As New StringBuilder
        query.Append(My.Resources.InsertarMorfologia.ToString)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_morfologia", ProsegurDbType.Objeto_Id, oidMorfologia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_morfologia", ProsegurDbType.Identificador_Alfanumerico, CodMorfologia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_morfologia", ProsegurDbType.Descricao_Longa, DesMorfologia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "bol_vigente", ProsegurDbType.Logico, BolVigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data_Hora, FyhActualizacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "nec_modalidad_recogida", ProsegurDbType.Identificador_Numerico, NecModalidadRecogida))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        If Transacion IsNot Nothing Then
            Transacion.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidMorfologia

    End Function

    Public Shared Function InsertMorfologiaXComp(CodMorfologiaComponente As String,
                                                 OidMorfologia As String,
                                                 CodTipoContenedor As String,
                                                 DesTipoContenedor As String,
                                                 CodFuncionContenedor As String,
                                                 BolVigente As Boolean,
                                                 CodUsuario As String,
                                                 FyhActualizacion As DateTime,
                                                 NecOrden As Integer,
                                                 ByRef Transacion As DbHelper.Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim oidMorfologiaComponente As String = Guid.NewGuid().ToString()

        Dim query As New StringBuilder
        query.Append(My.Resources.InsertarMorfologiaXComp.ToString)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_morfologia_componente", ProsegurDbType.Objeto_Id, oidMorfologiaComponente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_morfologia_componente", ProsegurDbType.Identificador_Alfanumerico, CodMorfologiaComponente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_morfologia", ProsegurDbType.Objeto_Id, OidMorfologia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_tipo_contenedor", ProsegurDbType.Identificador_Alfanumerico, CodTipoContenedor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_CONTENEDOR", ProsegurDbType.Identificador_Alfanumerico, DesTipoContenedor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_FUNCION_CONTENEDOR", ProsegurDbType.Identificador_Alfanumerico, CodFuncionContenedor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "bol_vigente", ProsegurDbType.Logico, BolVigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data_Hora, FyhActualizacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "nec_orden", ProsegurDbType.Inteiro_Curto, NecOrden))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        If Transacion IsNot Nothing Then
            Transacion.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidMorfologiaComponente

    End Function

    Public Shared Function InsertComponenteXObj(OidMorfologiaComponente As String, OidDivisa As String, _
                                            OidDenominacion As String, OidMedioPago As String, NecOrdenDivisa As Integer, _
                                            NecOrdenTipoMedioPago As Integer, ByRef Transacion As DbHelper.Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim oidComponenteObjeto As String = Guid.NewGuid().ToString()

        Dim query As New StringBuilder
        query.Append(My.Resources.InsertarComponenteXObj.ToString)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_componente_objeto", ProsegurDbType.Objeto_Id, oidComponenteObjeto))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_morfologia_componente", ProsegurDbType.Objeto_Id, OidMorfologiaComponente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_divisa", ProsegurDbType.Objeto_Id, OidDivisa))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_denominacion", ProsegurDbType.Objeto_Id, OidDenominacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_medio_pago", ProsegurDbType.Objeto_Id, OidMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "nec_orden_divisa", ProsegurDbType.Objeto_Id, NecOrdenDivisa))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "nec_orden_tipo_med_pag", ProsegurDbType.Objeto_Id, NecOrdenTipoMedioPago))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        If Transacion IsNot Nothing Then
            Transacion.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidComponenteObjeto

    End Function


#End Region

#Region "[UPDATES]"

    Public Shared Sub ActualizarMorfologia(OidMorfologia As String, CodMorfologia As String, DesMorfologia As String, _
                                       BolVigente As Boolean, CodUsuario As String, FyhActualizacion As DateTime, _
                                       ByRef Transacion As DbHelper.Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim query As New StringBuilder
        query.Append(My.Resources.ActualizarMorfologia.ToString)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_morfologia", ProsegurDbType.Objeto_Id, OidMorfologia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_morfologia", ProsegurDbType.Identificador_Alfanumerico, CodMorfologia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_morfologia", ProsegurDbType.Descricao_Longa, DesMorfologia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "bol_vigente", ProsegurDbType.Logico, BolVigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data_Hora, FyhActualizacion))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        If Transacion IsNot Nothing Then
            Transacion.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

    End Sub

#End Region

End Class