Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis

Public Class Procedencia

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
    ''' Função Selecionar, faz a pesquisa e preenche do datatable
    ''' </summary>
    ''' <param name="objpeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Shared Function GetProcedencias(objPeticion As ContractoServicio.Procedencia.GetProcedencias.Peticion) As ContractoServicio.Procedencia.GetProcedencias.ProcedenciaColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = My.Resources.GetProcedencias.ToString()

        Dim Comandos As New CriterioColecion

        If objPeticion.Activo.HasValue Then
            Comandos.addCriterio("AND", "TPRO.BOL_ACTIVO = []BOL_ACTIVO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.Activo))
        End If

        ' Verifica se o identificador da procedencia foi informado
        If Not String.IsNullOrEmpty(objPeticion.OidProcedencia) Then
            Comandos.addCriterio("AND", "TPRO.OID_PROCEDENCIA = []OID_PROCEDENCIA")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCEDENCIA", ProsegurDbType.Objeto_Id, objPeticion.OidProcedencia))
        Else

            ' Verifica se o tipo de subcliente foi informado
            If Not String.IsNullOrEmpty(objPeticion.CodigoTipoSubCliente) Then
                Comandos.addCriterio("AND", "TSUBCLI.COD_TIPO_SUBCLIENTE = []COD_TIPO_SUBCLIENTE")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoTipoSubCliente))
            End If

            ' Verifica se o tipo de punto de servicio foi informado
            If Not String.IsNullOrEmpty(objPeticion.CodigoTipoPuntoServicio) Then
                Comandos.addCriterio("AND", "TPTOSER.COD_TIPO_PUNTO_SERVICIO = []COD_TIPO_PUNTO_SERVICIO")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PUNTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoTipoPuntoServicio))
            End If

            ' Verifica se o tipo de procedencia foi informado
            If Not String.IsNullOrEmpty(objPeticion.CodigoTipoProcedencia) Then
                Comandos.addCriterio("AND", "TPROC.COD_TIPO_PROCEDENCIA = []COD_TIPO_PROCEDENCIA")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCEDENCIA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoTipoProcedencia))
            End If

        End If

        'Adiciona a clausula Where
        If Comandos.Count > 0 Then
            comando.CommandText &= Util.MontarClausulaWhere(Comandos)
        End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaProcedencias As New ContractoServicio.Procedencia.GetProcedencias.ProcedenciaColeccion

        'Percorre o dt e retorna uma coleção de Procedencias.
        objRetornaProcedencias = RetornaColecaoProcedencias(dt)

        ' retornar objeto
        Return objRetornaProcedencias

    End Function

    ''' <summary>
    ''' Verifica se o Procedencia existe no BD retornando verdadeiro ou falso.
    ''' </summary>
    ''' <param name="OidTipoSubCliente"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Shared Function VerificarExisteProcedencia(OidProcedencia As String, OidTipoSubCliente As String, OidTipoPuntoServicio As String, OidTipoProcedencia As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcedenciaPorTipoSubClienteYTipoPuntoServicioYTipoProcedencia.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SUBCLIENTE", ProsegurDbType.Objeto_Id, OidTipoSubCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PUNTO_SERVICIO", ProsegurDbType.Objeto_Id, OidTipoPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PROCEDENCIA", ProsegurDbType.Objeto_Id, OidTipoProcedencia))

        Dim Comandos As New CriterioColecion

        ' Verifica se o identificador da procedencia foi informado
        If Not String.IsNullOrEmpty(OidProcedencia) Then
            comando.CommandText &= vbCrLf & Util.PrepararQuery(Util.AdicionarCampoQuery(" AND TPRO.OID_PROCEDENCIA != []OID_PROCEDENCIA", "OID_PROCEDENCIA", comando, OidProcedencia, ProsegurDbType.Objeto_Id))
        End If

        Dim existe As Decimal = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If existe > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de Procedencias
    ''' </summary>
    ''' <param name="dtProcedencias"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Private Shared Function RetornaColecaoProcedencias(dtProcedencias As DataTable) As ContractoServicio.Procedencia.GetProcedencias.ProcedenciaColeccion

        Dim objRetornaProcedencias As New ContractoServicio.Procedencia.GetProcedencias.ProcedenciaColeccion

        If dtProcedencias IsNot Nothing AndAlso dtProcedencias.Rows.Count > 0 Then

            Dim objProcedencia As ContractoServicio.Procedencia.GetProcedencias.Procedencia = Nothing

            For Each dr As DataRow In dtProcedencias.Rows
                objProcedencia = New ContractoServicio.Procedencia.GetProcedencias.Procedencia()

                Util.AtribuirValorObjeto(objProcedencia.OidProcedencia, dr("OID_PROCEDENCIA"), GetType(String))
                Util.AtribuirValorObjeto(objProcedencia.CodigoTipoSubCliente, dr("COD_TIPO_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objProcedencia.CodigoTipoPuntoServicio, dr("COD_TIPO_PUNTO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(objProcedencia.CodigoTipoProcedencia, dr("COD_TIPO_PROCEDENCIA"), GetType(String))
                Util.AtribuirValorObjeto(objProcedencia.DescripcionTipoSubCliente, dr("DES_TIPO_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objProcedencia.DescripcionTipoPuntoServicio, dr("DES_TIPO_PUNTO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(objProcedencia.DescripcionTipoProcedencia, dr("DES_TIPO_PROCEDENCIA"), GetType(String))
                Util.AtribuirValorObjeto(objProcedencia.Activo, dr("BOL_ACTIVO"), GetType(Boolean))
                Util.AtribuirValorObjeto(objProcedencia.FyhActualizacion, dr("GMT_MODIFICACION"), GetType(Date))
                Util.AtribuirValorObjeto(objProcedencia.CodigoUsuario, dr("DES_USUARIO_MODIFICACION"), GetType(String))
                Util.AtribuirValorObjeto(objProcedencia.GmtCreacion, dr("GMT_CREACION"), GetType(Date))
                Util.AtribuirValorObjeto(objProcedencia.DesUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
                ' adicionar para objeto
                objRetornaProcedencias.Add(objProcedencia)
            Next
        End If

        Return objRetornaProcedencias
    End Function

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir o Procedencia no DB.
    ''' </summary>
    ''' <param name="objProcedencia"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Shared Function AltaProcedencia(objProcedencia As ContractoServicio.Procedencia.SetProcedencia.Procedencia) As String
        Dim oidProcedencia As String = Nothing

        Try

        Dim objRespuesta As New ContractoServicio.Procedencia.SetProcedencia.Respuesta

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaProcedencia.ToString())
        comando.CommandType = CommandType.Text

        oidProcedencia = Guid.NewGuid.ToString
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCEDENCIA", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SUBCLIENTE", ProsegurDbType.Objeto_Id, objProcedencia.OidTipoSubCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PUNTO_SERVICIO", ProsegurDbType.Objeto_Id, objProcedencia.OidTipoPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PROCEDENCIA", ProsegurDbType.Objeto_Id, objProcedencia.OidTipoProcedencia))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, objProcedencia.FyhCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, objProcedencia.CodigoUsuarioCreacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, objProcedencia.FyhActualizacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, objProcedencia.CodigoUsuarioActualizacion))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

            Return oidProcedencia
        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("061_msg_erro_Procedencia"))
            Return Nothing
        End Try

    End Function

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por fazer a atualização do Procedencia do DB.
    ''' </summary>
    ''' <param name="objProcedencia"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Shared Sub ActualizarProcedencia(objProcedencia As ContractoServicio.Procedencia.SetProcedencia.Procedencia)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarProcedencia.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SUBCLIENTE", ProsegurDbType.Objeto_Id, objProcedencia.OidTipoSubCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PUNTO_SERVICIO", ProsegurDbType.Objeto_Id, objProcedencia.OidTipoPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PROCEDENCIA", ProsegurDbType.Objeto_Id, objProcedencia.OidTipoProcedencia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objProcedencia.CodigoUsuarioActualizacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, objProcedencia.FyhActualizacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCEDENCIA", ProsegurDbType.Objeto_Id, objProcedencia.OidProcedencia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objProcedencia.Activo))

        ' executa o comando
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

    End Sub

#End Region

End Class