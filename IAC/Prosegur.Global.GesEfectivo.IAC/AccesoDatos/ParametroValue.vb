Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Text
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Parametro
Imports System.Linq
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

Public Class ParametroValue

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Valida se o valor do parametro existe
    ''' </summary>
    ''' <param name="OidParametro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/09/2011 - Criado
    ''' </history>
    Public Shared Function ValidarValorExiste(OidParametro As String, CodAplicacion As String, CodIdentificadorNivel As String) As Boolean

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.VerificarValorParametroExiste.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PARAMETRO", ProsegurDbType.Objeto_Id, OidParametro))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, CodAplicacion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR_NIVEL", ProsegurDbType.Identificador_Alfanumerico, CodIdentificadorNivel))

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, cmd)
    End Function

    Public Shared Function GetParametrosValues(codigoPais As String, oidDelegacion As String, oidPuesto As String, codigoAplicacion As String) As ContractoServicio.Parametro.GetParametrosValues.NivelColeccion

        Dim objNiveles As New ContractoServicio.Parametro.GetParametrosValues.NivelColeccion

        If Not String.IsNullOrEmpty(codigoPais) Then
            GetParametrosValues(codigoAplicacion, codigoPais, TipoNivel.Pais, objNiveles)
        End If

        If Not String.IsNullOrEmpty(oidDelegacion) Then
            GetParametrosValues(codigoAplicacion, oidDelegacion, TipoNivel.Delegacion, objNiveles)
        End If

        If Not String.IsNullOrEmpty(oidPuesto) Then
            GetParametrosValues(codigoAplicacion, oidPuesto, TipoNivel.Puesto, objNiveles)
        End If
        Return objNiveles
    End Function

    Private Shared Sub GetParametrosValues(codigoAplicacion As String, codigoIdentificadorNivel As String, tipoNivel As TipoNivel, ByRef objNiveles As ContractoServicio.Parametro.GetParametrosValues.NivelColeccion)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR_NIVEL", ProsegurDbType.Identificador_Alfanumerico, codigoIdentificadorNivel))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PARAMETRO", ProsegurDbType.Inteiro_Curto, tipoNivel))

        comando.CommandText = Util.PrepararQuery(My.Resources.GetParametrosValues.ToString)
        comando.CommandType = CommandType.Text
        Dim dr As IDataReader = Nothing
        Try
            dr = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)
        Finally
            If dr IsNot Nothing Then
                TransformarParaNivelColeccion(dr, objNiveles)
                dr.Close()
                dr.Dispose()
            End If
            AcessoDados.Desconectar(comando.Connection)
        End Try
    End Sub

    Public Shared Function ListarOIDParametrosValues(codigoPais As String, oidDelegacion As String, oidPuesto As String, codigoAplicacion As String, codigosParametros As List(Of String)) As List(Of ParametroValueVO)
        Dim objParametrosValues As New List(Of ParametroValueVO)

        If Not String.IsNullOrEmpty(codigoPais) Then
            Dim dr As DataTable = ListarOIDParametrosValues(codigoAplicacion, codigoPais, TipoNivel.Pais, codigosParametros)
            TransformarParaParametroValueVO(dr, objParametrosValues)
        End If

        If Not String.IsNullOrEmpty(oidDelegacion) Then
            Dim dr As DataTable = ListarOIDParametrosValues(codigoAplicacion, oidDelegacion, TipoNivel.Delegacion, codigosParametros)
            TransformarParaParametroValueVO(dr, objParametrosValues)
        End If

        If Not String.IsNullOrEmpty(oidPuesto) Then
            Dim dr As DataTable = ListarOIDParametrosValues(codigoAplicacion, oidPuesto, TipoNivel.Puesto, codigosParametros)
            TransformarParaParametroValueVO(dr, objParametrosValues)
        End If

        Return objParametrosValues
    End Function

    Private Shared Function ListarOIDParametrosValues(codigoAplicacion As String, codigoIdentificadorNivel As String, tipoNivel As TipoNivel, codigosParametros As List(Of String)) As DataTable
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder
        query.Append(My.Resources.ListarOIDParametrosValues.ToString)
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR_NIVEL", ProsegurDbType.Identificador_Alfanumerico, codigoIdentificadorNivel))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PARAMETRO", ProsegurDbType.Inteiro_Curto, tipoNivel))
        query.Append(Util.MontarClausulaIn(codigosParametros, "COD_PARAMETRO", comando, "AND", "GEPR_TPARAMETRO", ""))
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function

    Public Shared Sub ActualizarParametroValue(oidParametroValue As String, valor As String, codigoUsuario As String, objTransacion As Transacao)
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarParametroValue.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PARAMETRO_VALOR", ProsegurDbType.Objeto_Id, oidParametroValue))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_VALOR_PARAMETRO", ProsegurDbType.Descricao_Longa, valor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

        objTransacion.AdicionarItemTransacao(comando)
    End Sub

    Public Shared Sub AltaParametroValue(oidParametro As String, codigoIdentificadorNivel As String, valor As String, codigoUsuario As String, objTransacion As Transacao)
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaParametroValue.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PARAMETRO_VALOR", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PARAMETRO", ProsegurDbType.Objeto_Id, oidParametro))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR_NIVEL", ProsegurDbType.Objeto_Id, codigoIdentificadorNivel))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_VALOR_PARAMETRO", ProsegurDbType.Descricao_Longa, valor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

        objTransacion.AdicionarItemTransacao(comando)

    End Sub

    ''' <summary>
    ''' Recupera os parametros de delegação e pais.
    ''' </summary>
    ''' <param name="OidDelegacion"></param>
    ''' <param name="CodPais"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/10/2011 - Criado
    ''' </history>
    Public Shared Function RecuperarParametrosDelegacionPais(OidDelegacion As String, CodPais As String, _
                                                             CodAplicacion As String, objColParametros As GetParametrosDelegacionPais.ParametroColeccion) As GetParametrosDelegacionPais.ParametroRespuestaColeccion

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.RecuperarValorParametros.ToString
        cmd.CommandType = CommandType.Text

        If objColParametros IsNot Nothing AndAlso objColParametros.Count > 0 Then

            Dim objListParametros As New List(Of String)

            For Each Parametro In objColParametros
                objListParametros.Add(Parametro.CodigoParametro)
            Next

            cmd.CommandText &= Util.MontarClausulaIn(objListParametros, "COD_PARAMETRO", cmd, "AND", "P")

        End If

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, CodAplicacion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PARAMETRO_D", ProsegurDbType.Identificador_Alfanumerico, ContractoServicio.Constantes.C_NIVEL_PARAMETRO_DELEGACION))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PARAMETRO_P", ProsegurDbType.Identificador_Alfanumerico, ContractoServicio.Constantes.C_NIVEL_PARAMETRO_PAIS))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, OidDelegacion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PAIS", ProsegurDbType.Identificador_Alfanumerico, CodPais))

        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        Return PopularRecuperarParametrosDelegacionPais(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd))
    End Function

    Public Shared Function RecuperarParametrosDelegacionPais(codigoDelegacion As String, CodAplicacion As String, objColParametros As GetParametrosDelegacionPais.ParametroColeccion) As GetParametrosDelegacionPais.ParametroRespuestaColeccion

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.RecuperarValorParametros.ToString
        cmd.CommandType = CommandType.Text

        If objColParametros IsNot Nothing AndAlso objColParametros.Count > 0 Then

            Dim objListParametros As New List(Of String)

            For Each Parametro In objColParametros
                objListParametros.Add(Parametro.CodigoParametro)
            Next

            cmd.CommandText &= Util.MontarClausulaIn(objListParametros, "COD_PARAMETRO", cmd, "AND", "P")

        End If

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, CodAplicacion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PARAMETRO_D", ProsegurDbType.Identificador_Alfanumerico, ContractoServicio.Constantes.C_NIVEL_PARAMETRO_DELEGACION))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PARAMETRO_P", ProsegurDbType.Identificador_Alfanumerico, ContractoServicio.Constantes.C_NIVEL_PARAMETRO_PAIS))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        Return PopularRecuperarParametrosDelegacionPais(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd))
    End Function


    ''' <summary>
    ''' Popula os parametros da delegação e pais
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/10/2011 - Criado
    ''' </history>
    Private Shared Function PopularRecuperarParametrosDelegacionPais(dt As DataTable) As GetParametrosDelegacionPais.ParametroRespuestaColeccion

        Dim objColParametros As GetParametrosDelegacionPais.ParametroRespuestaColeccion = Nothing

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            objColParametros = New GetParametrosDelegacionPais.ParametroRespuestaColeccion

            For Each dr As DataRow In dt.Rows

                '1 - TEXTBOX
                '2 - COMBOBOX
                '3 - CHECKBOX
                '4 - PALETACOLORES
                Dim tipo As Integer = If(dr.Table.Columns.Contains("NEC_TIPO_COMPONENTE"), Util.AtribuirValorObj(dr("NEC_TIPO_COMPONENTE"), GetType(Integer)), 1)
                Dim valor As String = If(dr.Table.Columns.Contains("DES_VALOR_PARAMETRO"), Util.AtribuirValorObj(dr("DES_VALOR_PARAMETRO"), GetType(String)), "")

                If tipo = 3 AndAlso String.IsNullOrEmpty(valor) Then
                    valor = "0"
                End If

                objColParametros.Add(New GetParametrosDelegacionPais.ParametroRespuesta With { _
                                                          .CodigoParametro = Util.AtribuirValorObj(dr("COD_PARAMETRO"), GetType(String)), _
                                                          .ValorParametro = valor, _
                                                          .EsObligatorio = Util.AtribuirValorObj(dr("BOL_OBLIGATORIO"), GetType(Boolean))})

            Next

        End If

        Return objColParametros
    End Function

#End Region

#Region "[POPULAR]"

    Private Shared Sub TransformarParaNivelColeccion(dr As IDataReader, ByRef objColeccion As ContractoServicio.Parametro.GetParametrosValues.NivelColeccion)
        While dr.Read()
            Dim objNivel As ContractoServicio.Parametro.GetParametrosValues.Nivel = Nothing
            Dim objAgrupacion As ContractoServicio.Parametro.GetParametrosValues.Agrupacion = Nothing
            Dim objParametro As ContractoServicio.Parametro.GetParametrosValues.Parametro = Nothing
            Dim codigoParametro As String = Nothing
            Dim codigoOpcion As String = Nothing
            Dim codigoNivel As String = Nothing
            Dim descripcionCortoAgrupacion As String = Nothing
            Dim descripcionValorParametro As String = Nothing

            Util.AtribuirValorObjeto(codigoNivel, dr("COD_NIVEL_PARAMETRO"), GetType(String))
            objNivel = objColeccion.SingleOrDefault(Function(n) n.CodigoNivel = codigoNivel)

            If objNivel Is Nothing Then
                objNivel = New ContractoServicio.Parametro.GetParametrosValues.Nivel
                Util.AtribuirValorObjeto(objNivel.CodigoPermiso, dr("COD_PERMISO"), GetType(String))
                Util.AtribuirValorObjeto(objNivel.DescripcionNivel, dr("DES_NIVEL_PARAMETRO"), GetType(String))
                Util.AtribuirValorObjeto(objNivel.CodigoNivel, dr("COD_NIVEL_PARAMETRO"), GetType(String))
                objNivel.Agrupaciones = New List(Of ContractoServicio.Parametro.GetParametrosValues.Agrupacion)
                objColeccion.Add(objNivel)
            End If

            Util.AtribuirValorObjeto(descripcionCortoAgrupacion, dr("DES_DESCRIPCION_CORTO"), GetType(String))
            objAgrupacion = objNivel.Agrupaciones.SingleOrDefault(Function(a) a.DescripcionCorto = descripcionCortoAgrupacion)
            If objAgrupacion Is Nothing Then
                objAgrupacion = New ContractoServicio.Parametro.GetParametrosValues.Agrupacion
                objNivel.Agrupaciones.Add(objAgrupacion)
                objAgrupacion.DescripcionCorto = descripcionCortoAgrupacion
                objAgrupacion.Parametros = New List(Of ContractoServicio.Parametro.GetParametrosValues.Parametro)
                Util.AtribuirValorObjeto(objAgrupacion.DescripcionLarga, dr("DES_DESCRIPCION_LARGA"), GetType(String))
                Util.AtribuirValorObjeto(objAgrupacion.NecOrden, dr("NEC_ORDEN"), GetType(Integer))
                Util.AtribuirValorObjeto(objAgrupacion.CodigoPermiso, dr("COD_PERMISO_GRUPO"), GetType(String))
            End If

            Util.AtribuirValorObjeto(codigoParametro, dr("COD_PARAMETRO"), GetType(String))
            objParametro = objAgrupacion.Parametros.SingleOrDefault(Function(p) p.CodigoParametro = codigoParametro)
            If objParametro Is Nothing Then
                objParametro = New ContractoServicio.Parametro.GetParametrosValues.Parametro
                objAgrupacion.Parametros.Add(objParametro)
                objParametro.CodigoParametro = codigoParametro
                objParametro.ParametroOpciones = New List(Of ContractoServicio.Parametro.GetParametrosValues.ParametroOpciones)
                Util.AtribuirValorObjeto(objParametro.BolObligatorio, dr("BOL_OBLIGATORIO"), GetType(Boolean))
                Util.AtribuirValorObjeto(objParametro.DescripcionLarga, dr("DES_DESCRIPCION_LARGA_PARAM"), GetType(String))
                Util.AtribuirValorObjeto(objParametro.DescripcionCorto, dr("DES_DESCRIPCION_CORTO_PARAM"), GetType(String))
                Util.AtribuirValorObjeto(objParametro.ValorParametro, dr("DES_VALOR_PARAMETRO"), GetType(String))
                Util.AtribuirValorObjeto(objParametro.NecOrden, dr("NEC_ORDEN_PARAMETRO"), GetType(Integer))
                Util.AtribuirValorObjeto(objParametro.NecTipoComponente, dr("NEC_TIPO_COMPONENTE"), GetType(TipoComponente))
                Util.AtribuirValorObjeto(objParametro.NecTipoDato, dr("NEC_TIPO_DATO"), GetType(TipoDato))
            End If

            Util.AtribuirValorObjeto(codigoOpcion, dr("COD_OPCION"), GetType(String))
            If Not String.IsNullOrEmpty(codigoOpcion) AndAlso Not objParametro.ParametroOpciones.Exists(Function(po) po.CodigoOpcion = codigoOpcion) Then
                Dim parametro As New ContractoServicio.Parametro.GetParametrosValues.ParametroOpciones
                parametro.CodigoOpcion = codigoOpcion
                Util.AtribuirValorObjeto(parametro.DescriptionOpcion, dr("DES_OPCION"), GetType(String))
                objParametro.ParametroOpciones.Add(parametro)
            End If
        End While
    End Sub

    Private Shared Sub TransformarParaParametroValueVO(drQuery As DataTable, objParametros As List(Of ParametroValueVO))

        For Each dr As DataRow In drQuery.Rows
            Dim parametroValue As New ParametroValueVO

            Util.AtribuirValorObjeto(parametroValue.OIDParametro, dr("OID_PARAMETRO"), GetType(String))
            Util.AtribuirValorObjeto(parametroValue.OIDParametroValue, dr("OID_PARAMETRO_VALOR"), GetType(String))
            Util.AtribuirValorObjeto(parametroValue.TipoNivel, dr("COD_NIVEL_PARAMETRO"), GetType(TipoNivel))
            Util.AtribuirValorObjeto(parametroValue.CodigoParametro, dr("COD_PARAMETRO"), GetType(String))
            Util.AtribuirValorObjeto(parametroValue.Valor, dr("DES_VALOR_PARAMETRO"), GetType(String))

            objParametros.Add(parametroValue)
        Next
    End Sub

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Insere o valor dos parametros
    ''' </summary>
    ''' <param name="OidParametro"></param>
    ''' <param name="CodIdentificador"></param>
    ''' <param name="ValorParametro"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="ObjTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/09/2011 - Criado
    ''' </history>
    Public Shared Sub InserirValorParametro(OidParametro As String, CodIdentificador As String, ValorParametro As String, CodUsuario As String, ObjTransacion As Transacao)

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.InserirParametroValor.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PARAMETRO_VALOR", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PARAMETRO", ProsegurDbType.Objeto_Id, OidParametro))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR_NIVEL", ProsegurDbType.Identificador_Alfanumerico, CodIdentificador))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_VALOR_PARAMETRO", ProsegurDbType.Descricao_Curta, ValorParametro))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

        ObjTransacion.AdicionarItemTransacao(cmd)
    End Sub

#End Region
End Class

