Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DBHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports Prosegur.Genesis

Public Class TiposProcesado

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
    ''' Responsável por pesquisar o tipo procesado do DB.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/02/2009 Created
    ''' </history>
    Public Shared Function GetTiposProcesado(objPeticion As ContractoServicio.TiposProcesado.GetTiposProcesado.Peticion) As ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesadoColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim filtros As New System.Text.StringBuilder

        filtros.AppendLine(My.Resources.GetTiposProcesado.ToString())
        filtros.Append(MontaQueryTipoProcesado(objPeticion, comando))

        comando.CommandText = Util.PrepararQuery(filtros.ToString)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaTipoProcesado As New ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesadoColeccion

        objRetornaTipoProcesado = RetornaColecaoTipoProcesado(dt)

        ' retornar objeto
        Return objRetornaTipoProcesado

    End Function

    ''' <summary>
    ''' Busca o oid do TipoProcesado
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function BuscaOidTipoProcesado(codigo As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidTipoProcesado.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCESADO", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim oid As String = String.Empty

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            oid = dt.Rows(0)("OID_TIPO_PROCESADO").ToString
        End If

        Return oid

    End Function

    ''' <summary>
    ''' Verifica se o codigo do tipo procesado existe no BD retornando verdadeiro ou falso.
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Created
    ''' </history>
    Public Shared Function VerificarCodigoTipoProcesado(codigo As String) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidTipoProcesado.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCESADO", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Verifica se o tipo procesado possui proceso vigente
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/04/2009 Criado
    ''' </history>
    Public Shared Function VerificarTipoProcesadoReferenciaProceso(codigo As String) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarTipoProcesadoReferenciaProceso.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCESADO", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function


    ''' <summary>
    ''' Verifica se a descrição do tipo procesado existe no BD retornando verdadeiro ou falso.
    ''' </summary>
    ''' <param name="descripcion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Created
    ''' </history>
    Public Shared Function VerificarDescripcionTipoProcesado(descripcion As String) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionTipoProcesado.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_PROCESADO", ProsegurDbType.Descricao_Longa, descripcion))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Monta Query Tipo Procesado
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Private Shared Function MontaQueryTipoProcesado(objPeticion As ContractoServicio.TiposProcesado.GetTiposProcesado.Peticion, ByRef comando As IDbCommand) As StringBuilder

        Dim filtros As New System.Text.StringBuilder
        Dim indice As Integer = 0

        If objPeticion.Caracteristicas IsNot Nothing Then
            For Each caracteristica As ContractoServicio.TiposProcesado.GetTiposProcesado.Caracteristica In objPeticion.Caracteristicas
                indice += 1
                If filtros.Length > 0 Then
                    filtros.Append("OR ")
                End If
                filtros.AppendLine("C2.COD_CARACTERISTICA = []COD_CARACTERISTICA" + indice.ToString())
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA" & indice.ToString(), ProsegurDbType.Identificador_Alfanumerico, caracteristica.Codigo))
            Next
        End If

        If filtros.Length > 0 Then
            filtros.Insert(0, " INNER JOIN (SELECT DISTINCT TP2.OID_TIPO_PROCESADO FROM GEPR_TTIPO_PROCESADO TP2 " & _
                                " LEFT OUTER JOIN GEPR_TCARAC_POR_TIPO_PROCESADO CPTP2 ON " & _
                                        " TP2.OID_TIPO_PROCESADO = CPTP2.OID_TIPO_PROCESADO " & _
                                " LEFT OUTER JOIN GEPR_TCARACTERISTICA C2 ON " & _
                                        " CPTP2.OID_CARACTERISTICA = C2.OID_CARACTERISTICA " & _
                                            " WHERE ")
            filtros.AppendLine(" ) TP2 ON TP.OID_TIPO_PROCESADO = TP2.OID_TIPO_PROCESADO WHERE TP.BOL_VIGENTE = []BOL_VIGENTE")
        Else
            filtros.AppendLine(" WHERE")
            filtros.AppendLine("TP.BOL_VIGENTE = []BOL_VIGENTE")
        End If
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.Vigente))

        If Not String.IsNullOrEmpty(objPeticion.Codigo) Then
            filtros.AppendLine("AND UPPER(TP.COD_TIPO_PROCESADO) LIKE []COD_TIPO_PROCESADO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCESADO", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.Codigo.ToString & "%"))
        End If

        If Not String.IsNullOrEmpty(objPeticion.Descripcion) Then
            filtros.AppendLine("AND UPPER(TP.DES_TIPO_PROCESADO) LIKE []DES_TIPO_PROCESADO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_PROCESADO", ProsegurDbType.Descricao_Longa, "%" & objPeticion.Descripcion.ToString & "%"))
        End If

        filtros.AppendLine("ORDER BY TP.COD_TIPO_PROCESADO")

        Return filtros
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de tipo procesado
    ''' </summary>
    ''' <param name="dtCanais"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoTipoProcesado(dtCanais As DataTable) As ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesadoColeccion

        Dim objRetornaTipoProcesado As New ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesadoColeccion

        If dtCanais IsNot Nothing AndAlso dtCanais.Rows.Count > 0 Then

            Dim objTipoProcesado As ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado = Nothing
            Dim objCaracteristica As ContractoServicio.TiposProcesado.GetTiposProcesado.CaracteristicaRespuesta = Nothing
            Dim codigoTipoProcessado As String = String.Empty

            For Each dr As DataRow In dtCanais.Rows
                If objTipoProcesado Is Nothing OrElse dr("COD_TIPO_PROCESADO").ToString() <> codigoTipoProcessado Then
                    objTipoProcesado = New ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado()
                    objRetornaTipoProcesado.Add(objTipoProcesado)
                    objTipoProcesado.Caracteristicas = New ContractoServicio.TiposProcesado.GetTiposProcesado.CaracteristicaRespuestaColeccion()
                    Util.AtribuirValorObjeto(objTipoProcesado.Codigo, dr("COD_TIPO_PROCESADO"), GetType(String))
                    Util.AtribuirValorObjeto(objTipoProcesado.Descripcion, dr("DES_TIPO_PROCESADO"), GetType(String))
                    Util.AtribuirValorObjeto(objTipoProcesado.Observaciones, dr("OBS_TIPO_PROCESADO"), GetType(String))
                    Util.AtribuirValorObjeto(objTipoProcesado.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
                    codigoTipoProcessado = objTipoProcesado.Codigo
                End If
                If dr("COD_CARACTERISTICA") IsNot Nothing AndAlso dr("COD_CARACTERISTICA") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("COD_CARACTERISTICA").ToString()) Then
                    objCaracteristica = New ContractoServicio.TiposProcesado.GetTiposProcesado.CaracteristicaRespuesta()
                    objTipoProcesado.Caracteristicas.Add(objCaracteristica)
                    Util.AtribuirValorObjeto(objCaracteristica.Codigo, dr("COD_CARACTERISTICA"), GetType(String))
                    Util.AtribuirValorObjeto(objCaracteristica.Descripcion, dr("DES_CARACTERISTICA"), GetType(String))
                End If
            Next

        End If

        Return objRetornaTipoProcesado
    End Function

#Region "[GETCOMBOMODALIDADERECUENTO]"

    ''' <summary>
    ''' Busca as modalidades de recuento vigentes e retorna uma coleção de modalidades de recuento.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois]      17/03/2009 Criado
    ''' [carlos.bomtempo]   15/05/2009 - Modificado
    ''' </history>
    Public Shared Function GetComboModalidadeRecuento() As ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuentoColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetComboModalidadesRecuento.ToString())
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA_CONTEO", ProsegurDbType.Identificador_Alfanumerico, ContractoServicio.Constantes.COD_CARAC_ADMITE_IAC))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaTipoProcesado As New ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuentoColeccion

        objRetornaTipoProcesado = RetornaColecaoModalidadeRecuento(dt)

        ' retornar objeto
        Return objRetornaTipoProcesado

    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de modalidades de recuento.
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoModalidadeRecuento(dt As DataTable) As ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuentoColeccion

        Dim objRetornaModalidadeRecuento As New ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuentoColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaModalidadeRecuento.Add(PopularModalidadeRecuento(dr))
            Next

        End If

        Return objRetornaModalidadeRecuento
    End Function

    ''' <summary>
    ''' Função PopularModalidadeRecuento cria e preenche um objeto 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois]       17/03/2008 Criado
    ''' [carlos.bomtempo]    15/05/2008 Modificado
    ''' </history>
    Private Shared Function PopularModalidadeRecuento(dr As DataRow) As GetComboModalidadesRecuento.ModalidadeRecuento

        Dim objModalidadeRecuento As New GetComboModalidadesRecuento.ModalidadeRecuento

        Util.AtribuirValorObjeto(objModalidadeRecuento.Codigo, dr("COD_TIPO_PROCESADO"), GetType(String))
        Util.AtribuirValorObjeto(objModalidadeRecuento.Descripcion, dr("DES_TIPO_PROCESADO"), GetType(String))
        Util.AtribuirValorObjeto(objModalidadeRecuento.AdmiteIac, dr("ADMITE_IAC"), GetType(Boolean))

        'If Convert.ToString(dr("COD_CARACTERISTICA_CONTEO")) = ContractoServicio.Constantes.COD_CARAC_ADMITE_IAC Then
        '    objModalidadeRecuento.AdmiteIac = True
        'Else
        '    objModalidadeRecuento.AdmiteIac = False
        'End If

        ' retornar objeto
        Return objModalidadeRecuento

    End Function

#End Region

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Deleta o Tipo Procesado
    ''' </summary>
    ''' <param name="objCodigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/02/2009 Created
    ''' </history>
    Public Shared Sub BajaTiposProcesado(objCodigo As String, codigoUsuario As String)

        Dim objTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaTiposProcesado.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCESADO", ProsegurDbType.Identificador_Alfanumerico, objCodigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        objTransacao.AdicionarItemTransacao(comando)

        objTransacao.RealizarTransacao()
    End Sub

    ''' <summary>
    ''' Deleta o Tipo Procesado
    ''' </summary>
    ''' <param name="CodigpTipoProcessado"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 25/05/2009 Created
    ''' </history>
    Public Shared Sub BajaCaracteristicasPorTipoProcesado(CodigpTipoProcessado As String, objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaCaracteristicasPorTipoProcesado.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCESADO", ProsegurDbType.Identificador_Alfanumerico, CodigpTipoProcessado))

        ' adicionar o comando a transação.
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir o tipo procesado no DB.
    ''' </summary>
    ''' <param name="objTiposProcesado"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/02/2009 Created
    ''' </history>
    Public Shared Sub AltaTiposProcesado(objTiposProcesado As ContractoServicio.TiposProcesado.SetTiposProcesado.Peticion, codigoUsuario As String)

        Try
            Dim objTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaTiposProcesado.ToString())
            comando.CommandType = CommandType.Text

            Dim oidCanal As String = Guid.NewGuid.ToString
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PROCESADO", ProsegurDbType.Objeto_Id, oidCanal))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCESADO", ProsegurDbType.Identificador_Alfanumerico, objTiposProcesado.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_PROCESADO", ProsegurDbType.Descricao_Longa, objTiposProcesado.Descripcion))
            If objTiposProcesado.Observaciones <> String.Empty Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_TIPO_PROCESADO", ProsegurDbType.Observacao_Longa, objTiposProcesado.Observaciones))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_TIPO_PROCESADO", ProsegurDbType.Observacao_Longa, DBNull.Value))
            End If
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objTiposProcesado.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            objTransacao.AdicionarItemTransacao(comando)

            AltaCaracteristicasPorTipoProcesado(objTiposProcesado, codigoUsuario, objTransacao)

            ' executar comando
            objTransacao.RealizarTransacao()

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("004_msg_Erro_UKTipoProcesado"))
        End Try

    End Sub

    ''' <summary>
    ''' Deleta o Tipo Procesado
    ''' </summary>
    ''' <param name="objTiposProcesado"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 25/05/2009 Created
    ''' </history>
    Public Shared Sub AltaCaracteristicasPorTipoProcesado(objTiposProcesado As ContractoServicio.TiposProcesado.SetTiposProcesado.Peticion, codigoUsuario As String, objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As String = Util.PrepararQuery(My.Resources.AltaCaracteristicasPorTipoProcesado.ToString())

        For Each objCaracteristica As ContractoServicio.TiposProcesado.SetTiposProcesado.Caracteristica In objTiposProcesado.Caracteristicas
            ' define o comando
            comando = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = query
            comando.CommandType = CommandType.Text

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CARACT_TIPO_PROCESADO", ProsegurDbType.Objeto_Id, Guid.NewGuid().ToString()))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCESADO", ProsegurDbType.Identificador_Alfanumerico, objTiposProcesado.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA", ProsegurDbType.Identificador_Alfanumerico, objCaracteristica.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

            ' adicionar o comando a transação.
            objTransacao.AdicionarItemTransacao(comando)
        Next

    End Sub

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por atualizar o tipo procesado no DB.
    ''' </summary>
    ''' <param name="objTiposProcesado"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/02/2009 Created
    ''' </history>
    Public Shared Sub ActualizarTiposProcesado(objTiposProcesado As ContractoServicio.TiposProcesado.SetTiposProcesado.Peticion, codigoUsuario As String)

        Try
            Dim objTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            If objTiposProcesado.Caracteristicas IsNot Nothing Then
                BajaCaracteristicasPorTipoProcesado(objTiposProcesado.Codigo, objTransacao)
            End If

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            ' preparar query
            Dim query As New StringBuilder
            query.Append("UPDATE gepr_ttipo_procesado SET ")

            query.Append(Util.AdicionarCampoQuery("des_tipo_procesado = []des_tipo_procesado,", "des_tipo_procesado", comando, objTiposProcesado.Descripcion, ProsegurDbType.Descricao_Longa))
            query.Append(Util.AdicionarCampoQuery("obs_tipo_procesado = []obs_tipo_procesado,", "obs_tipo_procesado", comando, objTiposProcesado.Observaciones, ProsegurDbType.Observacao_Longa))
            query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, objTiposProcesado.Vigente, ProsegurDbType.Logico))

            query.Append("cod_usuario = []cod_usuario, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

            query.Append("fyh_actualizacion = []fyh_actualizacion ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data, DateTime.Now))

            query.Append("WHERE cod_tipo_procesado = []cod_tipo_procesado ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_tipo_procesado", ProsegurDbType.Identificador_Alfanumerico, objTiposProcesado.Codigo))

            comando.CommandText = Util.PrepararQuery(query.ToString)
            comando.CommandType = CommandType.Text

            objTransacao.AdicionarItemTransacao(comando)

            AltaCaracteristicasPorTipoProcesado(objTiposProcesado, codigoUsuario, objTransacao)

            ' executar comando
            objTransacao.RealizarTransacao()

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("004_msg_Erro_UKTipoProcesado"))
        End Try

    End Sub

#End Region

End Class