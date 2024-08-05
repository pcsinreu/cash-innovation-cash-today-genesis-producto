Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno
Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports System.Configuration
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno

Public Class CodigoAjeno


#Region "[CONSULTAR]"

    Public Shared Function GetCodigosAjenos(objPeticion As ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As GetCodigosAjenos.EntidadCodigosAjenoColeccion
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetCodigoAjeno.ToString)

        'Adiciona Where caso algum parâmetro seja passado
        Dim clausulaWhere As New CriterioColecion
        Dim strClausulaWhere As String = String.Empty

        ' adiciona filtros
        If Not String.IsNullOrEmpty(objPeticion.CodigosAjeno.CodTipoTablaGenesis) Then
            clausulaWhere.addCriterio("AND", " CA.COD_TIPO_TABLA_GENESIS = []COD_TIPO_TABLA_GENESIS ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_TABLA_GENESIS", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigosAjeno.CodTipoTablaGenesis))
        End If
        If Not String.IsNullOrEmpty(objPeticion.CodigosAjeno.OidTablaGenesis) Then
            clausulaWhere.addCriterio("AND", " CA.OID_TABLA_GENESIS = []OID_TABLA_GENESIS ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TABLA_GENESIS", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigosAjeno.OidTablaGenesis))
        End If
        If Not String.IsNullOrEmpty(objPeticion.CodigosAjeno.CodIdentificador) Then
            clausulaWhere.addCriterio("AND", " CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigosAjeno.CodIdentificador))
        End If
        If objPeticion.CodigosAjeno.BolDefecto IsNot Nothing Then
            clausulaWhere.addCriterio("AND", " CA.BOL_DEFECTO = []BOL_DEFECTO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_DEFECTO", ProsegurDbType.Logico, objPeticion.CodigosAjeno.BolDefecto))
        End If
        If objPeticion.CodigosAjeno.BolActivo IsNot Nothing Then
            clausulaWhere.addCriterio("AND", " CA.BOL_ACTIVO = []BOL_ACTIVO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.CodigosAjeno.BolActivo))
        End If

        'Adiciona a clausula Where
        If clausulaWhere.Count > 0 Then
            strClausulaWhere = Util.MontarClausulaWhere(clausulaWhere)
        End If

        ' preparar query
        comando.CommandText = Util.PrepararQuery(String.Format(query.ToString, strClausulaWhere))

        comando.CommandType = CommandType.Text


        ' executar query
        'Dim dtDivisa As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        Dim dtCodigoAjeno As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        ' retornar coleção de EntidadCodigosAjeno
        Return PopularGeCodigosAjenos(dtCodigoAjeno)

    End Function

    Public Shared Function PopularGeCodigosAjenos(dtCodigoAjeno As DataTable) As GetCodigosAjenos.EntidadCodigosAjenoColeccion


        Dim objEntidadsCodigoAjenoColeccion As New GetCodigosAjenos.EntidadCodigosAjenoColeccion

        ' caso encontre algum registro
        If dtCodigoAjeno IsNot Nothing AndAlso dtCodigoAjeno.Rows.Count > 0 Then

            Dim dv As DataView = New DataView(dtCodigoAjeno)

            ' Cria lista CodigoAjenoRespuesta com todos o resultado da query
            Dim codigosAjenos As List(Of GetCodigosAjenos.CodigoAjenoRespuesta) =
                (From f In dtCodigoAjeno.Rows
                 Select PopularGeCodigosAjenosRespuesta(f)).ToList

            ' Cria datatable com os valores distintos retornando as colunas "COD_TIPO_TABLA_GENESIS", "OID_TABLA_GENESIS"
            Dim dtTablaGenesis = dv.ToTable(True, {"COD_TIPO_TABLA_GENESIS", "OID_TABLA_GENESIS"})

            For Each itemTG As DataRow In dtTablaGenesis.Rows
                Dim codTipoTabla As String = Nothing
                Dim oidTabla As String = Nothing
                Dim codTabla As String = Nothing
                Dim desTabla As String = Nothing
                Util.AtribuirValorObjeto(codTipoTabla, itemTG("COD_TIPO_TABLA_GENESIS"), GetType(String))
                Util.AtribuirValorObjeto(oidTabla, itemTG("OID_TABLA_GENESIS"), GetType(String))

                'Busca mapeo das entidades
                Dim mapeoEntidade = Constantes.MapeoEntidadesCodigoAjeno.FirstOrDefault(Function(f) f.CodTipoTablaGenesis = codTipoTabla)

                If mapeoEntidade IsNot Nothing Then
                    ' criar comando
                    Dim cmdEntidade As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

                    ' obter query passando para o string.format os dados do mapeoEntidade encontrado
                    cmdEntidade.CommandText = String.Format(My.Resources.GetCodigoAjenoEntidade.ToString,
                                                            mapeoEntidade.CodTablaGenesis & ", " & mapeoEntidade.DesTablaGenesis,
                                                            mapeoEntidade.Entidade, mapeoEntidade.OidTablaGenesis, oidTabla)

                    cmdEntidade.CommandText = Util.PrepararQuery(cmdEntidade.CommandText)

                    cmdEntidade.CommandType = CommandType.Text

                    ' executar query
                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmdEntidade)
                    If dt.Rows.Count > 0 Then
                        Util.AtribuirValorObjeto(codTabla, dt.Rows(0)(mapeoEntidade.CodTablaGenesis), GetType(String))
                        Util.AtribuirValorObjeto(desTabla, dt.Rows(0)(mapeoEntidade.DesTablaGenesis), GetType(String))
                    End If
                End If

                ' cria e preenche objeto EntidadCodigosAjeno
                Dim objEntidadCodigoAjeno As New GetCodigosAjenos.EntidadCodigosAjeno
                objEntidadCodigoAjeno.CodTipoTablaGenesis = codTipoTabla
                objEntidadCodigoAjeno.OidTablaGenesis = oidTabla
                objEntidadCodigoAjeno.CodTablaGenesis = codTabla
                objEntidadCodigoAjeno.DesTablaGenesis = desTabla

                ' cria objeto CodigoAjenoRespuesta coleccion
                Dim objCodigosAjenos As New GetCodigosAjenos.CodigoAjenoRespuestaColeccion
                objEntidadCodigoAjeno.CodigosAjenos = objCodigosAjenos

                'Adiciona Entidad CodigoAjeno na coleção
                objEntidadsCodigoAjenoColeccion.Add(objEntidadCodigoAjeno)

                'Filtra datatable dtCodigoAjeno
                Dim coleccionRows = dtCodigoAjeno.Select("COD_TIPO_TABLA_GENESIS = '" & codTipoTabla & "' AND OID_TABLA_GENESIS = '" & oidTabla & "'")

                ' percorrer registros encontrados
                For Each dr As DataRow In coleccionRows

                    Dim OidCodigoAjeno As String = Nothing
                    Util.AtribuirValorObjeto(OidCodigoAjeno, dr("OID_CODIGO_AJENO"), GetType(String))

                    ' preencher a coleção com objetos divisa
                    objCodigosAjenos.Add(codigosAjenos.FirstOrDefault(Function(f) f.OidCodigoAjeno = OidCodigoAjeno))

                Next

            Next

        End If

        ' retornar coleção de divisas
        Return objEntidadsCodigoAjenoColeccion

    End Function

    Public Shared Sub EliminarCodigosAjenos(nombreTabla As String, oidTablaGenesis As String, Optional objTransacion As Transacao = Nothing)
        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.CommandText = Util.PrepararQuery(My.Resources.DeleteCodigosAjenos())
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TABLA_GENESIS", ProsegurDbType.Identificador_Alfanumerico, oidTablaGenesis))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_TABLA_GENESIS", ProsegurDbType.Identificador_Alfanumerico, nombreTabla))

            comando.CommandType = CommandType.Text

            If objTransacion Is Nothing Then
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
            Else
                objTransacion.AdicionarItemTransacao(comando)
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub EliminarCodigoAjeno(oidCodigoAjeno As String, Optional objTransacion As Transacao = Nothing)
        Try
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.CommandText = Util.PrepararQuery(My.Resources.DeleteCodigoAjeno())
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CODIGO_AJENO", ProsegurDbType.Identificador_Alfanumerico, oidCodigoAjeno))

            comando.CommandType = CommandType.Text

            If objTransacion Is Nothing Then
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
            Else
                objTransacion.AdicionarItemTransacao(comando)
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function PopularGeCodigosAjenosRespuesta(dtCodigoAjeno As DataRow) As GetCodigosAjenos.CodigoAjenoRespuesta

        Dim retorno As New GetCodigosAjenos.CodigoAjenoRespuesta

        With retorno

            Util.AtribuirValorObjeto(.BolActivo, dtCodigoAjeno("BOL_ACTIVO"), GetType(Boolean))
            Util.AtribuirValorObjeto(.BolDefecto, dtCodigoAjeno("BOL_DEFECTO"), GetType(Boolean))
            Util.AtribuirValorObjeto(.BolMigrado, dtCodigoAjeno("BOL_MIGRADO"), GetType(Boolean))
            Util.AtribuirValorObjeto(.CodAjeno, dtCodigoAjeno("COD_AJENO"), GetType(String))
            Util.AtribuirValorObjeto(.DesAjeno, dtCodigoAjeno("DES_AJENO"), GetType(String))
            Util.AtribuirValorObjeto(.DesUsuarioCreacion, dtCodigoAjeno("DES_USUARIO_CREACION"), GetType(String))
            Util.AtribuirValorObjeto(.DesUsuarioModificacion, dtCodigoAjeno("DES_USUARIO_MODIFICACION"), GetType(String))
            Util.AtribuirValorObjeto(.GmtCreacion, dtCodigoAjeno("GMT_CREACION"), GetType(Date))
            Util.AtribuirValorObjeto(.GmtModificacion, dtCodigoAjeno("GMT_MODIFICACION"), GetType(Date))
            Util.AtribuirValorObjeto(.CodIdentificador, dtCodigoAjeno("COD_IDENTIFICADOR"), GetType(String))
            Util.AtribuirValorObjeto(.OidCodigoAjeno, dtCodigoAjeno("OID_CODIGO_AJENO"), GetType(String))

        End With

        Return retorno

    End Function

    Public Shared Function VerificarIdentificadorXCodigoAjeno(objPeticion As ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Peticion) As DatosEntidad
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(String.Format(My.Resources.VerificarIdentificadorXCodigoAjeno.ToString, objPeticion.NombreCampoCod, objPeticion.NombreCampoDes, objPeticion.CodTipoTablaGenesis, objPeticion.NombreCampoOid))

        ' adiciona filtros



        If Not String.IsNullOrEmpty(objPeticion.CodTipoTablaGenesis) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_TABLA_GENESIS", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodTipoTablaGenesis))
        End If
        If Not String.IsNullOrEmpty(objPeticion.CodAjeno) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AJENO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodAjeno))
        End If
        If Not String.IsNullOrEmpty(objPeticion.CodIdentificador) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodIdentificador))
        End If
        If Not String.IsNullOrEmpty(objPeticion.OidCodigoAjeno) Then
            query.Append(" AND CA.OID_CODIGO_AJENO <> []OID_CODIGO_AJENO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CODIGO_AJENO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidCodigoAjeno))
        End If

        ' preparar query
        comando.CommandText = Util.PrepararQuery(query.ToString)

        comando.CommandType = CommandType.Text

        ' executar query
        Dim respuesta = New DatosEntidad
        Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        If dt.Rows.Count = 1 AndAlso
            dt IsNot Nothing Then

            Dim dr = dt.Rows(0)
            Dim objCodigoAjeno As New ContractoServicio.CodigoAjeno.CodigoAjenoBase
            Util.AtribuirValorObjeto(respuesta.Codigo, dr("COD"), GetType(String))
            Util.AtribuirValorObjeto(respuesta.Descripcion, dr("DESCR"), GetType(String))


        End If
        'Retorna verdadeiro se não tem nenhum gravado
        Return respuesta

    End Function

    'Recupera os codigos ajenos da planta
    Public Shared Function RecuperaCodigoAjenoBase(oidTablaGenesis As String) As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Dim objCodigoAjenoColeccion As New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetCodigoAjenos)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TABLA_GENESIS", ProsegurDbType.Objeto_Id, oidTablaGenesis))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt.Rows.Count > 0 AndAlso
            dt IsNot Nothing Then

            For Each dr In dt.Rows
                Dim objCodigoAjeno As New ContractoServicio.CodigoAjeno.CodigoAjenoBase
                Util.AtribuirValorObjeto(objCodigoAjeno.OidCodigoAjeno, dr("OID_CODIGO_AJENO"), GetType(String))
                Util.AtribuirValorObjeto(objCodigoAjeno.CodAjeno, dr("COD_AJENO"), GetType(String))
                Util.AtribuirValorObjeto(objCodigoAjeno.CodIdentificador, dr("COD_IDENTIFICADOR"), GetType(String))
                Util.AtribuirValorObjeto(objCodigoAjeno.DesAjeno, dr("DES_AJENO"), GetType(String))
                Util.AtribuirValorObjeto(objCodigoAjeno.BolDefecto, dr("BOL_DEFECTO"), GetType(Boolean))
                Util.AtribuirValorObjeto(objCodigoAjeno.BolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
                Util.AtribuirValorObjeto(objCodigoAjeno.BolMigrado, dr("BOL_MIGRADO"), GetType(Boolean))
                objCodigoAjenoColeccion.Add(objCodigoAjeno)
            Next
            Return objCodigoAjenoColeccion
        End If

        Return Nothing

    End Function

#End Region

    Public Shared Function SetCodigosAjenos(ByRef objCodigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno,
                                            Optional ByRef objTransacion As Transacao = Nothing) As Boolean
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim codigoAjeno As String = Nothing

        ' obter query
        Dim query As New StringBuilder
        If String.IsNullOrEmpty(objCodigoAjeno.OidCodigoAjeno) Then
            query.Append(My.Resources.SetCodigoAjenoInsert.ToString)
            codigoAjeno = Guid.NewGuid.ToString
            objCodigoAjeno.OidCodigoAjeno = codigoAjeno
        Else
            query.Append(My.Resources.SetCodigoAjenoUpdate.ToString)
            codigoAjeno = objCodigoAjeno.OidCodigoAjeno
        End If

        ' setar parametro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CODIGO_AJENO", ProsegurDbType.Identificador_Alfanumerico, codigoAjeno))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TABLA_GENESIS", ProsegurDbType.Identificador_Alfanumerico, objCodigoAjeno.OidTablaGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_TABLA_GENESIS", ProsegurDbType.Identificador_Alfanumerico, objCodigoAjeno.CodTipoTablaGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR", ProsegurDbType.Identificador_Alfanumerico, objCodigoAjeno.CodIdentificador))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AJENO", ProsegurDbType.Identificador_Alfanumerico, objCodigoAjeno.CodAjeno))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_AJENO", ProsegurDbType.Identificador_Alfanumerico, objCodigoAjeno.DesAjeno))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_DEFECTO", ProsegurDbType.Logico, objCodigoAjeno.BolDefecto))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objCodigoAjeno.BolActivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, objCodigoAjeno.GmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, objCodigoAjeno.DesUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, objCodigoAjeno.GmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objCodigoAjeno.DesUsuarioModificacion))

        ' preparar query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        Dim Atualizados As Integer = 0

        If objTransacion Is Nothing Then
            Atualizados = AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Else
            objTransacion.AdicionarItemTransacao(comando)
            Return 1
        End If

        Return Atualizados > 0

    End Function

End Class
