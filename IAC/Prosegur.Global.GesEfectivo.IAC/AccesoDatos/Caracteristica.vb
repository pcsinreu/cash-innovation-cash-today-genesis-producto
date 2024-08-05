Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Classe Caracteristica
''' </summary>
''' <remarks></remarks>
''' <history>
''' [rafael.nasorri] 14/05/2009 Criado
''' </history>
Public Class Caracteristica

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Retorna características por tipo processado
    ''' </summary>
    ''' <param name="OidTipoProcesado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 14/05/2009 Criado
    ''' </history>
    Public Shared Function GetCaracteristicaTipoProcesado(OidTipoProcesado As String) As GetProceso.CaracteristicaColeccion

        'Instância nova coleção do tipo CaracteristicaColeccion
        Dim objCaracCol As New GetProceso.CaracteristicaColeccion

        'Prepara comando a ser executado
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetCaracteristicaTipoProcesado.ToString())

        'Seta parâmetro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PROCESADO", ProsegurDbType.Objeto_Id, OidTipoProcesado))

        'Percorre as linhas retornada do comando executado
        For Each row In AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando).Rows

            'Adiciona itens à coleção CaracteristicaColeccion
            objCaracCol.Add(New GetProceso.Caracteristica() With { _
                            .Codigo = row("COD_CARACTERISTICA").ToString(), _
                            .Descripcion = row("DES_CARACTERISTICA").ToString(), _
                            .CodigoCaracteristicaConteo = row("COD_CARACTERISTICA_CONTEO").ToString()})

        Next

        'Retorna coleção CaracteristicaColeccion
        Return objCaracCol

    End Function

    ''' <summary>
    ''' Verifica que el código informado en el mensaje de entrada no existe en la base de datos.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo]   15/05/2009 - Criado
    ''' </history>
    Public Shared Function VerificarCodigoCaracteristica(Codigo As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoCaracteristica.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA", ProsegurDbType.Identificador_Alfanumerico, Codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0

    End Function


    ''' <summary>
    ''' Verifica que el código de conteo  informado en el mensaje de entrada no existe en la base de datos.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo]   15/05/2009 - Criado
    ''' </history>
    Public Shared Function VerificarCodigoConteoCaracteristica(Codigo As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoConteoCaracteristica.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA_CONTEO", ProsegurDbType.Identificador_Alfanumerico, Codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0

    End Function

    ''' <summary>
    ''' Verifica que la descripción de la caracteristica  informada en el mensaje de entrada no existe en la base de datos.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo]   15/05/2009 - Criado
    ''' </history>
    Public Shared Function VerificarDescripcionCaracteristica(Descripcion As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionCaracteristica.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CARACTERISTICA", ProsegurDbType.Descricao_Longa, Descripcion))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0

    End Function

    ''' <summary>
    ''' Recupera las caracteristicas de los tipos de procesado visgentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </history>
    Public Shared Function GetComboCaracteristicas() As ContractoServicio.Utilidad.GetComboCaracteristicas.CaracteristicaColeccion

        ' criar objeto caracteristicas
        Dim objCaracteristicas As New ContractoServicio.Utilidad.GetComboCaracteristicas.CaracteristicaColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.GetComboCaracteristicas.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtCaracteristica As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Retorna coleção de caracteristicas
        objCaracteristicas = PercorreDtCaracteristicas(dtCaracteristica)

        ' retornar coleção de caracteristicas
        Return objCaracteristicas
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna todos as caracteristicas vigentes
    ''' </summary>
    ''' <param name="dtCaracteristica"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <sumary>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </sumary>
    Private Shared Function PercorreDtCaracteristicas(dtCaracteristica As DataTable) As ContractoServicio.Utilidad.GetComboCaracteristicas.CaracteristicaColeccion

        Dim objColCaracteristica As New ContractoServicio.Utilidad.GetComboCaracteristicas.CaracteristicaColeccion

        If dtCaracteristica IsNot Nothing _
            AndAlso dtCaracteristica.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtCaracteristica.Rows
                ' adicionar para coleção
                objColCaracteristica.Add(PopulaCaracteristicaGetComboCaracteristicas(dr))
            Next

        End If

        Return objColCaracteristica
    End Function

    ''' <summary>
    ''' Popula Caracteristica GetComboCaracteristicas
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </history>
    Public Shared Function PopulaCaracteristicaGetComboCaracteristicas(dr As DataRow) As ContractoServicio.Utilidad.GetComboCaracteristicas.Caracteristica

        Dim objCaracteristica As New ContractoServicio.Utilidad.GetComboCaracteristicas.Caracteristica

        Util.AtribuirValorObjeto(objCaracteristica.Codigo, dr("COD_CARACTERISTICA"), GetType(String))

        Util.AtribuirValorObjeto(objCaracteristica.Descripcion, dr("DES_CARACTERISTICA"), GetType(String))

        Return objCaracteristica
    End Function

    ''' <summary>
    ''' Operación para obtener las características de los tipos  de procesado existentes. En el mensaje de entrada se recibe los parámetros por los que se quiere filtrar. En caso de no recibir ningún parámetro de entrada se devolverán todos los registros de la tabla GEPR_TCARACTERISTICA.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo]   15/05/2009 - Criado
    ''' </history>
    Public Shared Function GetCaracteristica(objPeticion As ContractoServicio.Caracteristica.GetCaracteristica.Peticion) As ContractoServicio.Caracteristica.GetCaracteristica.CaracteristicaColeccion

        ' criar objeto caracteristicas
        Dim objCaracteristicas As New ContractoServicio.Caracteristica.GetCaracteristica.CaracteristicaColeccion
        Dim objCaracteristica As ContractoServicio.Caracteristica.GetCaracteristica.Caracteristica

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim query As New StringBuilder()
        Dim where As New StringBuilder()

        query.AppendLine("SELECT")
        query.AppendLine("	COD_CARACTERISTICA,")
        query.AppendLine("	DES_CARACTERISTICA,")
        query.AppendLine("	OBS_CARACTERISTICA,")
        query.AppendLine("	COD_CARACTERISTICA_CONTEO,")
        query.AppendLine("	BOL_VIGENTE")
        query.AppendLine("FROM")
        query.AppendLine("	GEPR_TCARACTERISTICA")

        ' setar parametros
        If objPeticion.Vigente IsNot Nothing Then
            where.AppendLine("BOL_VIGENTE = []BOL_VIGENTE")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, IIf(objPeticion.Vigente, 1, 0)))
        End If

        If Not String.IsNullOrEmpty(objPeticion.Codigo) Then
            If where.Length > 0 Then
                where.Append("AND ")
            End If
            where.AppendLine("UPPER(COD_CARACTERISTICA) LIKE []COD_CARACTERISTICA")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.Codigo & "%"))
        End If

        If Not String.IsNullOrEmpty(objPeticion.CodigoConteo) Then
            If where.Length > 0 Then
                where.Append("AND ")
            End If
            where.AppendLine("UPPER(COD_CARACTERISTICA_CONTEO) LIKE []COD_CARACTERISTICA_CONTEO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA_CONTEO", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.CodigoConteo & "%"))
        End If

        If Not String.IsNullOrEmpty(objPeticion.Descripcion) Then
            If where.Length > 0 Then
                where.Append("AND ")
            End If
            where.AppendLine("UPPER(DES_CARACTERISTICA) LIKE []DES_CARACTERISTICA")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CARACTERISTICA", ProsegurDbType.Descricao_Longa, "%" & objPeticion.Descripcion & "%"))
        End If

        Dim strQuery As String
        If where.Length > 0 Then
            query.AppendLine("WHERE")
        End If
        strQuery = query.ToString() & where.ToString()

        comando.CommandText = Util.PrepararQuery(strQuery)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            ' percorrer todos os registros
            For Each dr As DataRow In dt.Rows
                objCaracteristica = New ContractoServicio.Caracteristica.GetCaracteristica.Caracteristica()
                Util.AtribuirValorObjeto(objCaracteristica.Codigo, dr("COD_CARACTERISTICA"), GetType(String))
                Util.AtribuirValorObjeto(objCaracteristica.Descripcion, dr("DES_CARACTERISTICA"), GetType(String))
                Util.AtribuirValorObjeto(objCaracteristica.Observaciones, dr("OBS_CARACTERISTICA"), GetType(String))
                Util.AtribuirValorObjeto(objCaracteristica.CodigoConteo, dr("COD_CARACTERISTICA_CONTEO"), GetType(String))
                Util.AtribuirValorObjeto(objCaracteristica.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
                ' adicionar para coleção
                objCaracteristicas.Add(objCaracteristica)
            Next
        End If

        ' retornar coleção de caracteristicas
        Return objCaracteristicas

    End Function

    ''' <summary>
    ''' Operación para obtener las características de los tipos  de procesado existentes. En el mensaje de entrada se recibe los parámetros por los que se quiere filtrar. En caso de no recibir ningún parámetro de entrada se devolverán todos los registros de la tabla GEPR_TCARACTERISTICA.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo]   15/05/2009 - Criado
    ''' </history>
    Public Shared Function GetCaracteristica(codigoCaracteristica As String) As ContractoServicio.Caracteristica.GetCaracteristica.CaracteristicaColeccion

        ' criar objeto caracteristicas
        Dim objCaracteristicas As New ContractoServicio.Caracteristica.GetCaracteristica.CaracteristicaColeccion
        Dim objCaracteristica As ContractoServicio.Caracteristica.GetCaracteristica.Caracteristica

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim query As New StringBuilder()

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA", ProsegurDbType.Identificador_Alfanumerico, codigoCaracteristica))

        comando.CommandText = Util.PrepararQuery(My.Resources.GetCaracteristica)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            ' percorrer todos os registros
            For Each dr As DataRow In dt.Rows
                objCaracteristica = New ContractoServicio.Caracteristica.GetCaracteristica.Caracteristica()
                Util.AtribuirValorObjeto(objCaracteristica.Codigo, dr("COD_CARACTERISTICA"), GetType(String))
                Util.AtribuirValorObjeto(objCaracteristica.Descripcion, dr("DES_CARACTERISTICA"), GetType(String))
                Util.AtribuirValorObjeto(objCaracteristica.Observaciones, dr("OBS_CARACTERISTICA"), GetType(String))
                Util.AtribuirValorObjeto(objCaracteristica.CodigoConteo, dr("COD_CARACTERISTICA_CONTEO"), GetType(String))
                Util.AtribuirValorObjeto(objCaracteristica.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
                ' adicionar para coleção
                objCaracteristicas.Add(objCaracteristica)
            Next
        End If

        ' retornar coleção de caracteristicas
        Return objCaracteristicas

    End Function

    ''' <summary>
    ''' Retorna características por tipo processado
    ''' </summary>
    ''' <param name="codigoCaracteristica"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 14/05/2009 Criado
    ''' </history>
    Public Shared Function VerificaCaracteristicaEmUso(codigoCaracteristica As String) As Boolean

        'Prepara comando a ser executado
        Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.CommandText = Util.PrepararQuery(My.Resources.VerificaCaracteristicaEmUso.ToString())

            'Seta parâmetro
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA", ProsegurDbType.Identificador_Alfanumerico, codigoCaracteristica))

            Return Convert.ToInt32(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)) > 0

        End Using

    End Function

#End Region

#Region "[ATUALIZAR]"

    ''' <summary>
    ''' Operación para modificar y dar de baja lógica (modificando el estado de vigencia) características de tipos de procesado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo]   18/05/2009 - Criado
    ''' </history>
    Public Shared Sub ActualizarCaracteristica(caracteristica As ContractoServicio.Caracteristica.SetCaracteristica.Caracteristica, codUsuario As String)
        Try

            ' criar objeto
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            Dim stbQuery As New StringBuilder()

            stbQuery.AppendLine("UPDATE")
            stbQuery.AppendLine("  GEPR_TCARACTERISTICA")
            stbQuery.AppendLine("SET")

            'Código conteo da característica
            If caracteristica.CodigoConteo IsNot Nothing Then
                stbQuery.AppendLine("  COD_CARACTERISTICA_CONTEO = []COD_CARACTERISTICA_CONTEO,")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA_CONTEO", ProsegurDbType.Identificador_Alfanumerico, caracteristica.CodigoConteo))
            End If

            'Descrição da característica
            If caracteristica.Descripcion IsNot Nothing Then
                stbQuery.AppendLine("  DES_CARACTERISTICA = []DES_CARACTERISTICA,")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CARACTERISTICA", ProsegurDbType.Descricao_Longa, caracteristica.Descripcion))
            End If

            'Observações da característica
            If caracteristica.Observaciones IsNot Nothing Then
                stbQuery.AppendLine("  OBS_CARACTERISTICA = []OBS_CARACTERISTICA,")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_CARACTERISTICA", ProsegurDbType.Observacao_Curta, caracteristica.Observaciones))
            End If

            If caracteristica.Vigente IsNot Nothing Then
                'Informa se a característica está vigente ou não
                stbQuery.AppendLine("  BOL_VIGENTE = []BOL_VIGENTE,")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, caracteristica.Vigente))
            End If

            'Data de atualização
            stbQuery.AppendLine("  FYH_ACTUALIZACION = []FYH_ACTUALIZACION,")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, Date.Now))

            'Código do usuário
            stbQuery.AppendLine("  COD_USUARIO = []COD_USUARIO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codUsuario))

            stbQuery.AppendLine("WHERE")

            'Cósigo da característica
            stbQuery.AppendLine("  COD_CARACTERISTICA = []COD_CARACTERISTICA")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA", ProsegurDbType.Identificador_Alfanumerico, caracteristica.Codigo))

            comando.CommandText = Util.PrepararQuery(stbQuery.ToString())

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

        Catch ex As Exception
            ' Utilizado para tratar o erro de Unique Constrant
            Excepcion.Util.Tratar(ex, Traduzir("020_msg_AK_descricao_codigo_existente"))
        End Try
    End Sub

    ''' <summary>
    ''' Operación para dar de alta, modificar y dar de baja lógica (modificando el estado de vigencia) características de tipos de procesado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo]   18/05/2009 - Criado
    ''' </history>
    Public Shared Sub AltaCaracteristica(caracteristica As ContractoServicio.Caracteristica.SetCaracteristica.Caracteristica, codUsuario As String)
        Try


            ' criar objeto
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CARACTERISTICA", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString()))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA", ProsegurDbType.Identificador_Alfanumerico, caracteristica.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CARACTERISTICA", ProsegurDbType.Descricao_Longa, caracteristica.Descripcion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_CARACTERISTICA", ProsegurDbType.Observacao_Curta, IIf(String.IsNullOrEmpty(caracteristica.Observaciones), DBNull.Value, caracteristica.Observaciones)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACTERISTICA_CONTEO", ProsegurDbType.Identificador_Alfanumerico, caracteristica.CodigoConteo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, caracteristica.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, Date.Now))

            comando.CommandText = Util.PrepararQuery(My.Resources.AltaCaracteristica.ToString())

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

        Catch ex As Exception
            ' Utilizado para tratar o erro de Unique Constrant
            Excepcion.Util.Tratar(ex, Traduzir("020_msg_AK_descricao_codigo_existente"))
        End Try
    End Sub

#End Region

End Class
