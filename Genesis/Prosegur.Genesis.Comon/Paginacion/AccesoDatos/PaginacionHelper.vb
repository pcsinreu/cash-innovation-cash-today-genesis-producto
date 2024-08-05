Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Paginacion

Namespace Paginacion.AccesoDatos

    Public Class PaginacionHelper
        Public Shared Function EjecutarPaginacion(idConexion As String, comando As IDbCommand, parametroPaginacion As ParametrosPeticionPaginacion, ByRef parametrosRespuestaPaginacion As ParametrosRespuestaPaginacion) As DataTable

            Dim result As DataTable

            If parametroPaginacion.RealizarPaginacion AndAlso parametroPaginacion.RegistrosPorPagina > 0 Then
                ' criar comando
                Dim comandoPaginacao As IDbCommand = AcessoDados.CriarComando(idConexion)

                ' obter query
                Dim queryOriginal As String = comando.CommandText
                Dim queryCount As String = "SELECT count(0) FROM (" & queryOriginal & ") a"

                comando.CommandText = Util.PrepararQuery(queryCount, idConexion)

                parametrosRespuestaPaginacion.TotalRegistrosPaginados = AcessoDados.ExecutarScalar(idConexion, comando)

                Dim querySelect As String = "SELECT * FROM (SELECT a.*, ROWNUM rnum FROM (" & queryOriginal & ") a WHERE ROWNUM <= []ROWNUM_FIM) WHERE rnum > []ROWNUM_INICIO"

                comando.CommandText = Util.PrepararQuery(querySelect, idConexion)

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(idConexion, "ROWNUM_INICIO", ProsegurDbType.Inteiro_Longo, parametroPaginacion.RegistrosPorPagina * parametroPaginacion.IndicePagina))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(idConexion, "ROWNUM_FIM", ProsegurDbType.Inteiro_Longo, (parametroPaginacion.RegistrosPorPagina * parametroPaginacion.IndicePagina) + parametroPaginacion.RegistrosPorPagina))

                result = AcessoDados.ExecutarDataTable(idConexion, comando)
            Else
                result = AcessoDados.ExecutarDataTable(idConexion, comando)
                parametrosRespuestaPaginacion.TotalRegistrosPaginados = result.Rows.Count
            End If

            Return result

        End Function

        'Public Shared Function EjecutarPaginacion2(idConexion As String, comando As IDbCommand, parametroPaginacion As ParametrosPeticionPaginacion, ByRef parametrosRespuestaPaginacion As ParametrosRespuestaPaginacion) As DataTable

        '    Dim result As DataTable

        '    If parametroPaginacion.RealizarPaginacion AndAlso parametroPaginacion.RegistrosPorPagina > 0 Then
        '        ' criar comando
        '        Dim comandoPaginacao As IDbCommand = AcessoDados.CriarComando(idConexion)

        '        ' obter query
        '        Dim queryOriginal As String = comando.CommandText
        '        Dim queryCount As String = "SELECT count(0) FROM (" & queryOriginal & ") a"

        '        comando.CommandText = Util.PrepararQuery(queryCount, idConexion)

        '        parametrosRespuestaPaginacion.TotalRegistrosPaginados = AcessoDados.ExecutarScalar(idConexion, comando)

        '        Dim querySelect = "SELECT *  FROM ( SELECT queryResult.*, DENSE_RANK() over (order by rownum desc) rnum  FROM ( " & queryOriginal & ") queryResult ) WHERE rnum BETWEEN []ROWNUM_INICIO AND []ROWNUM_FIM"

        '        comando.CommandText = Util.PrepararQuery(querySelect, idConexion)

        '        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(idConexion, "ROWNUM_INICIO", ProsegurDbType.Inteiro_Longo, parametroPaginacion.RegistrosPorPagina * parametroPaginacion.IndicePagina))
        '        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(idConexion, "ROWNUM_FIM", ProsegurDbType.Inteiro_Longo, (parametroPaginacion.RegistrosPorPagina * parametroPaginacion.IndicePagina) + parametroPaginacion.RegistrosPorPagina))

        '        result = AcessoDados.ExecutarDataTable(idConexion, comando)
        '    Else
        '        result = AcessoDados.ExecutarDataTable(idConexion, comando)
        '        parametrosRespuestaPaginacion.TotalRegistrosPaginados = result.Rows.Count
        '    End If

        '    Return result

        'End Function

    End Class

End Namespace