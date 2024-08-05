Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis

Public Class Delegacion

#Region "[CONSULTAS]"

    ''' <summary>
    ''' Obtém cod_delegacion de todas as delegações utilizando distinct e order by.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/03/2009 Criado
    ''' </history>
    Public Shared Function ObterTodasDelegacoes() As ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion

        ' criar objeto retorno
        Dim objDelegaciones As ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion = Nothing

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterTodasDelegaciones.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' preencher objeto de retorno
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            objDelegaciones = New ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion

            ' para cada item do datatable
            For Each dr As DataRow In dt.Rows

                ' criar delegacao
                Dim objDelegacion As New ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion
                Util.AtribuirValorObjeto(objDelegacion.Codigo, dr("COD_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.Descripcion, dr("DES_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.OidDelegacion, dr("OID_DELEGACION"), GetType(String))
                ' adicionar delegacao para colecao
                objDelegaciones.Add(objDelegacion)

            Next

        End If

        ' retornar coleção de delegações
        Return objDelegaciones

    End Function

    ''' <summary>
    ''' Obter OidPais pelo OidDelegacion
    ''' </summary>
    ''' <param name="OidDelegacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObterOidPais(OidDelegacion As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidPaisPorOidDelegacion.ToString)

        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, OidDelegacion))

        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)

        Return result

    End Function

    ''' <summary>
    ''' Obter OidDelegacion pelo Código da Delegacion
    ''' </summary>
    ''' <param name="codigoDelegacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObterOIDDelegacion(codigoDelegacion As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOIDDelegacion.ToString)

        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)

        Return result

    End Function

    ''' <summary>
    ''' Obter COD_DELEGACION pelo OID_DELEGACION
    ''' </summary>
    ''' <param name="OidDelegacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObterCodDelegacionPorOid(OidDelegacion As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.ObterCodDelegacionPorOid.ToString)

        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, OidDelegacion))

        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)

        Return result

    End Function

    Public Shared Function ObterCodPais(OidPais As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.ObterCodigoPais.ToString)

        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PAIS", ProsegurDbType.Identificador_Alfanumerico, OidPais))

        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)

        Return result

    End Function

    ''' <summary>
    ''' Retorna os dados da delegação por Pais
    ''' </summary>
    ''' <param name="CodPais"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/09/2011 - Criado
    ''' </history>
    Public Shared Function ObterDatosDelegacionPorPais(CodPais As String) As ContractoServicio.Utilidad.GetComboDelegacionesPorPais.DelegacionColeccion

        ' criar objeto retorno
        Dim objDelegaciones As ContractoServicio.Utilidad.GetComboDelegacionesPorPais.DelegacionColeccion = Nothing

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.ObterDatosDelegacionPorPais.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PAIS", ProsegurDbType.Identificador_Alfanumerico, CodPais.ToUpper))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)
        dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)


        ' preencher objeto de retorno
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            objDelegaciones = New ContractoServicio.Utilidad.GetComboDelegacionesPorPais.DelegacionColeccion

            ' para cada item do datatable
            For Each dr As DataRow In dt.Rows

                ' criar delegacao
                Dim objDelegacion As New ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Delegacion
                Util.AtribuirValorObjeto(objDelegacion.Codigo, dr("COD_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.Descripcion, dr("DES_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.CodPais, dr("COD_PAIS"), GetType(String))

                ' adicionar delegacao para colecao
                objDelegaciones.Add(objDelegacion)

            Next

        End If

        ' retornar coleção de delegações
        Return objDelegaciones

    End Function

    Public Shared Function GetPlanta(oidDelegacion As String) As ContractoServicio.Planta.SetPlanta.Peticion
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.ObtenerPlantaAsociada)

        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "pOID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, oidDelegacion))
        Dim peticion = New ContractoServicio.Planta.SetPlanta.Peticion

        Dim dr As IDataReader = Nothing
        Try
            dr = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)
        Finally
            peticion = TransformarParaPlantaValuePeticion(dr)
            comando = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.ObtenerCodigosAjenosPlanta)
            comando.CommandType = CommandType.Text
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "pOID_PLANTA", ProsegurDbType.Identificador_Alfanumerico, peticion.OidPlanta))
            Try
                peticion.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                dr = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)
            Finally
                peticion.CodigosAjenos = TransformarCodigosAjenosValuePeticion(dr)
            End Try
            dr.Close()
            dr.Dispose()
            AcessoDados.Desconectar(comando.Connection)
        End Try

        Return peticion
    End Function

    Public Shared Function ObterOIDDelegacionyCodigoPais(codigoDelegacion As String) As DelegacionVO

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOIDDelegacionyCodigoPais.ToString)

        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))
        Dim delegacion As DelegacionVO
        Dim dr As IDataReader = Nothing
        Try
            dr = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)
        Finally
            delegacion = TransformarParaDelegacionValueVO(dr)
            dr.Close()
            dr.Dispose()
            AcessoDados.Desconectar(comando.Connection)
        End Try

        Return delegacion

    End Function

    ''' <summary>
    ''' Recupera o oidDelegacion e codigo do pais
    ''' </summary>
    ''' <param name="CodDelegacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/10/2011 - Criado
    ''' </history>
    Public Shared Function RecuperarOidDelegacionCodPais(CodDelegacion As String) As Dictionary(Of String, String)

        Dim objDatosDelegacion As Dictionary(Of String, String) = Nothing

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.RecuperarOidDelegacionYCodPais.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodDelegacion))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objDatosDelegacion = New Dictionary(Of String, String)
            objDatosDelegacion.Add(Util.AtribuirValorObj(dt.Rows(0)("OID_DELEGACION"), GetType(String)), Util.AtribuirValorObj(dt.Rows(0)("COD_PAIS"), GetType(String)))
        End If

        Return objDatosDelegacion
    End Function

    ''' <summary>
    ''' Retorna os dados da delegação
    ''' </summary>
    ''' <param name="codDelegacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 23/04/2013 - Criado
    ''' </history>
    Public Shared Function GetDadosDelegacion(codDelegacion As String) As ContractoServicio.Delegacion.DelegacionColeccion

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaDelegacion.ToString)
        cmd.CommandType = CommandType.Text

        cmd.CommandText &= "WHERE COD_DELEGACION = :COD_DELEGACION"

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codDelegacion))

        ' criar objeto delegaciones
        Dim objDelegaciones As New ContractoServicio.Delegacion.DelegacionColeccion

        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then
            For Each dr As DataRow In dtQuery.Rows
                objDelegaciones.Add(PopularGetDelegacioneDetail(dr))
            Next
        End If

        ' retornar objeto preenchido
        Return objDelegaciones
    End Function

    ''' <summary>
    ''' Retorna os dados da delegação
    ''' </summary>
    ''' <param name="CodigoDelegacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/09/2011 - Criado
    ''' </history>
    Public Shared Function ObterDatosDelegacion(CodigoDelegacion As String) As DataTable

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.ObterDatosDelegacion.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion.ToUpper))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

    End Function


    Private Shared Function TransformarParaPlantaValuePeticion(drQuery As IDataReader) As ContractoServicio.Planta.SetPlanta.Peticion
        ' criar objeto aplicacion
        Dim resultado = New ContractoServicio.Planta.SetPlanta.Peticion
        If drQuery Is Nothing Then
            Return New ContractoServicio.Planta.SetPlanta.Peticion
        End If

        While drQuery.Read
            Util.AtribuirValorObjeto(resultado.OidPlanta, drQuery("OID_PLANTA"), GetType(String))
            Util.AtribuirValorObjeto(resultado.CodPlanta, drQuery("COD_PLANTA"), GetType(String))
            Util.AtribuirValorObjeto(resultado.DesPlanta, drQuery("DES_PLANTA"), GetType(String))
            Util.AtribuirValorObjeto(resultado.BolActivo, drQuery("BOL_VIGENTE"), GetType(Boolean))
        End While
        Return resultado
    End Function

    Private Shared Function TransformarCodigosAjenosValuePeticion(drQuery As IDataReader) As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        ' criar objeto aplicacion
        Dim resultado = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        If drQuery Is Nothing Then
            Return New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        End If

        While drQuery.Read
            Dim codigoAjeno = New ContractoServicio.CodigoAjeno.CodigoAjenoBase
            Util.AtribuirValorObjeto(codigoAjeno.OidCodigoAjeno, drQuery("OID_CODIGO_AJENO"), GetType(String))
            Util.AtribuirValorObjeto(codigoAjeno.CodIdentificador, drQuery("COD_IDENTIFICADOR"), GetType(String))
            Util.AtribuirValorObjeto(codigoAjeno.CodAjeno, drQuery("COD_AJENO"), GetType(String))
            Util.AtribuirValorObjeto(codigoAjeno.DesAjeno, drQuery("DES_AJENO"), GetType(String))
            Util.AtribuirValorObjeto(codigoAjeno.BolActivo, drQuery("BOL_ACTIVO"), GetType(Boolean))
            Util.AtribuirValorObjeto(codigoAjeno.BolDefecto, drQuery("BOL_DEFECTO"), GetType(Boolean))
            resultado.Add(codigoAjeno)
        End While
        Return resultado
    End Function

    Private Shared Function TransformarParaDelegacionValueVO(drQuery As IDataReader) As DelegacionVO
        ' criar objeto aplicacion
        Dim objDelegacion As New DelegacionVO

        If drQuery Is Nothing Then
            Return objDelegacion
        End If

        While drQuery.Read
            objDelegacion = New DelegacionVO()
            Util.AtribuirValorObjeto(objDelegacion.OIDDelegacion, drQuery("OID_DELEGACION"), GetType(String))
            Util.AtribuirValorObjeto(objDelegacion.CodigoPais, drQuery("COD_PAIS"), GetType(String))
        End While
        Return objDelegacion
    End Function

    ''' <summary>
    ''' Retorna o código de uma delegação
    ''' </summary>
    ''' <param name="CodigoAplicacion"></param>
    ''' <param name="HostPuesto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 18/05/2012 - Criado
    ''' </history>
    Public Shared Function GetCodigoDelegacion(CodigoAplicacion As String, HostPuesto As String) As String

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.ObterCodigoDelegacion)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, HostPuesto.ToUpper))

        ' Se o código da aplicação foi informado 
        If Not String.IsNullOrEmpty(CodigoAplicacion) Then

            cmd.CommandText &= " AND A.COD_APLICACION = :COD_APLICACION"
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, CodigoAplicacion))

        End If

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, cmd)

    End Function

    ''' <summary>
    ''' Retorna os dados da delegação por Pais
    ''' </summary>
    ''' <param name="objPetion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/09/2011 - Criado
    ''' </history>
    Public Shared Function GetDelegacion(objPetion As ContractoServicio.Delegacion.GetDelegacion.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Delegacion.GetDelegacion.DelegacionColeccion
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaDelegacion)
        cmd.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(objPetion.OidPais) Then
            cmd.CommandText &= " WHERE OID_PAIS = :OID_PAIS"
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PAIS", ProsegurDbType.Identificador_Alfanumerico, objPetion.OidPais))
        End If

        ' Se o codigo delegacion for informado
        If Not String.IsNullOrEmpty(objPetion.CodDelegacion) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(COD_DELEGACION) = :COD_DELEGACION "
            Else
                cmd.CommandText &= " WHERE UPPER(COD_DELEGACION) = :COD_DELEGACION "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPetion.CodDelegacion.ToUpper()))
        End If

        'Se a descricao for informada
        If objPetion.DesDelegacion <> Nothing Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(DES_DELEGACION) LIKE :DES_DELEGACION "
            Else
                cmd.CommandText &= " WHERE UPPER(DES_DELEGACION) LIKE :DES_DELEGACION "
            End If
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, "%" & objPetion.DesDelegacion.ToUpper() & "%"))
        End If

        'Se a validade for informada
        If objPetion.BolVigente IsNot Nothing Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND BOL_VIGENTE = :BOL_VIGENTE"
            Else
                cmd.CommandText &= " WHERE BOL_VIGENTE = :BOL_VIGENTE"
            End If
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPetion.BolVigente))
        End If
        'cria o objeto de Delegacion
        Dim objDelegacion As New ContractoServicio.Delegacion.GetDelegacion.DelegacionColeccion

        Dim dtDelegacion As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, cmd, objPetion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        ' caso encontre algum registro
        If dtDelegacion IsNot Nothing _
            AndAlso dtDelegacion.Rows.Count > 0 Then
            ' percorrer registros encontrados
            For Each dr As DataRow In dtDelegacion.Rows
                ' preencher a coleção com objetos divisa
                objDelegacion.Add(PopularGetDelegacion(dr))
            Next
        End If

        Return objDelegacion

    End Function

    ''' <summary>
    ''' Recupera o oidDelegacion e codigo do pais
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/10/2011 - Criado
    ''' </history>
    Public Shared Function GetDelegacionByCertificado(objPeticion As ContractoServicio.Delegacion.GetDelegacionByCertificado.Peticion) As ContractoServicio.Delegacion.GetDelegacionByCertificado.Respuesta

        ' criar objeto retorno
        Dim objDelegaciones As ContractoServicio.Delegacion.GetDelegacionByCertificado.DelegacionColeccion = Nothing

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.GetDelegacionByCertificado.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CERTIFICADO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCertificado))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)
        dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)


        ' preencher objeto de retorno
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            objDelegaciones = New ContractoServicio.Delegacion.GetDelegacionByCertificado.DelegacionColeccion

            ' para cada item do datatable
            For Each dr As DataRow In dt.Rows

                ' criar delegacao
                Dim objDelegacion As New ContractoServicio.Delegacion.GetDelegacionByCertificado.Delegacion
                Util.AtribuirValorObjeto(objDelegacion.OidDelegacion, dr("OID_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.CodDelegacion, dr("COD_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.DesDelegacion, dr("DES_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.OidPlanta, dr("OID_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.CodPlanta, dr("COD_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.DesPlanta, dr("DES_PLANTA"), GetType(String))

                ' adicionar delegacao para colecao
                objDelegaciones.Add(objDelegacion)
            Next

        End If

        Dim resposta As New ContractoServicio.Delegacion.GetDelegacionByCertificado.Respuesta
        resposta.Delegaciones = objDelegaciones

        ' retornar coleção de delegações
        Return resposta

    End Function


    Shared Function ObtenerCodigoPorIdentificador(identificadorDelegacion As String, identificadorAjeno As String) As ContractoServicio.Delegacion.GetDelegacion.Delegacion

        Dim objDelegacion As ContractoServicio.Delegacion.GetDelegacion.Delegacion = Nothing

        If Not String.IsNullOrEmpty(identificadorDelegacion) Then
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerCodigoPorIdentificador

                'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OID_PLANIFICACION))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Descricao_Curta, identificadorDelegacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, identificadorAjeno))
                cmd.CommandText = Util.PrepararQuery(sqlResource)

                dtDados = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

                If dtDados IsNot Nothing AndAlso dtDados.Rows.Count > 0 Then
                    objDelegacion = New ContractoServicio.Delegacion.GetDelegacion.Delegacion

                    objDelegacion.CodDelegacion = dtDados.Rows(0)("COD_DELEGACION")
                    If IsDBNull(dtDados.Rows(0)("COD_AJENO")) Then
                        'Throw New Excepcion.NegocioExcepcion(RecuperarValorDic(resultadoValidacionDivisa))
                        objDelegacion.CodDelegacionAjeno = ""
                    Else
                        objDelegacion.CodDelegacionAjeno = dtDados.Rows(0)("COD_AJENO")
                    End If
                End If
            End Using
        End If

        Return objDelegacion
    End Function



    Public Shared Function GetDelegacionByPlanificacion(objPeticion As ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Peticion) As ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Respuesta

        ' criar objeto retorno

        Dim objDelegaciones As ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Respuesta = Nothing
        Dim objDelegacion As Comon.Clases.Delegacion = Nothing


        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.GetDelegacionByPlanificacion.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OID_PLANIFICACION))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)
        dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)


        ' preencher objeto de retorno
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            objDelegacion = New Comon.Clases.Delegacion
            ' para cada item do datatable
            For Each dr As DataRow In dt.Rows

                ' criar delegacao

                Util.AtribuirValorObjeto(objDelegacion.Identificador, dr("OID_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.Codigo, dr("COD_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.Descripcion, dr("DES_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.AjusteHorarioVerano, dr("NEC_VERANO_AJUSTE"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.HusoHorarioEnMinutos, dr("NEC_GMT_MINUTOS"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.FechaHoraVeranoInicio, dr("FYH_VERANO_INICIO"), GetType(String))
                Util.AtribuirValorObjeto(objDelegacion.FechaHoraVeranoFin, dr("FYH_VERANO_FIN"), GetType(String))

            Next

        End If

        Dim resposta As New ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Respuesta
        resposta.Delegacion = objDelegacion

        ' retornar coleção de delegações
        Return resposta

    End Function

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir a delegacion no Banco de Dados.
    ''' </summary>
    ''' <param name="objDelegacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/02/2013 Created
    ''' </history>
    Public Shared Function AltaDelegacion(ByRef objDelegacion As ContractoServicio.Delegacion.SetDelegacion.Peticion, objTransacao As Transacao) As String

        Dim OidDelegacion As String

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaDelegacion.ToString())
            comando.CommandType = CommandType.Text

            OidDelegacion = Guid.NewGuid.ToString()

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, OidDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PAIS", ProsegurDbType.Descricao_Curta, objDelegacion.OidPais))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PAIS", ProsegurDbType.Identificador_Alfanumerico, objDelegacion.CodPais))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Descricao_Longa, objDelegacion.CodDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DELEGACION", ProsegurDbType.Descricao_Longa, objDelegacion.DesDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_GMT_MINUTOS", ProsegurDbType.Inteiro_Longo, objDelegacion.NecGmtMinutos))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_VERANO_INICIO", ProsegurDbType.Data, objDelegacion.FyhVeranoInicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_VERANO_FIN", ProsegurDbType.Data, objDelegacion.FyhVeranoFin))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_VERANO_AJUSTE", ProsegurDbType.Identificador_Alfanumerico, objDelegacion.NecVeranoAjuste))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_ZONA", ProsegurDbType.Identificador_Alfanumerico, objDelegacion.DesZona))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objDelegacion.BolVigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data, objDelegacion.GmtCreacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, objDelegacion.DesUsuarioCreacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data, If(objDelegacion.GmtModificacion = DateTime.MinValue, DateTime.Now, objDelegacion.GmtModificacion)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, objDelegacion.DesUsuarioModificacion))

            objTransacao.AdicionarItemTransacao(comando)

            Return OidDelegacion

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, String.Format(Traduzir("046_msg_Erro_AltaDelegacion"), ex))
        End Try

        Return Nothing

    End Function

    Public Shared Sub BajaClienteFacturacion(OidDelegacion As String, objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaDelegacionClienteFacturacion.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, OidDelegacion))

        ' adicionar o comando a transação.
        objTransacao.AdicionarItemTransacao(comando)

    End Sub


    Public Shared Function AltaClienteFacturacion(OidDelegacion As String, ByRef objClienteFacturacion As ContractoServicio.Delegacion.SetDelegacion.ClienteFacturacion, objTransacao As Transacao) As String
        Try
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaDelegacionClienteFacturacion.ToString())
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACIONXCONFIG_FACTUR", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, OidDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE_CAPITAL", ProsegurDbType.Descricao_Curta, objClienteFacturacion.OidClienteCapital))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE_TESORERIA", ProsegurDbType.Identificador_Alfanumerico, objClienteFacturacion.OidSubClienteTesoreria))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO_TESORERIA", ProsegurDbType.Descricao_Longa, objClienteFacturacion.OidPtoServicioTesoreria))

            objTransacao.AdicionarItemTransacao(comando)

            Return OidDelegacion

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, String.Format(Traduzir("046_msg_Erro_AltaDelegacion"), ex))
        End Try

        Return Nothing
    End Function



    Public Shared Function ModificarTodasConfiguracionesRegionales(ByRef objDelegacion As ContractoServicio.Delegacion.SetDelegacion.Peticion, objTransacao As Transacao) As String

        Dim OidDelegacion As String

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AtualizaConfiguracionesRegionales.ToString())
            comando.CommandType = CommandType.Text

            OidDelegacion = objDelegacion.OidDelegacion

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_GMT_MINUTOS", ProsegurDbType.Inteiro_Longo, objDelegacion.NecGmtMinutos))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_VERANO_INICIO", ProsegurDbType.Data, objDelegacion.FyhVeranoInicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_VERANO_FIN", ProsegurDbType.Data, objDelegacion.FyhVeranoFin))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_VERANO_AJUSTE", ProsegurDbType.Identificador_Alfanumerico, objDelegacion.NecVeranoAjuste))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_ZONA", ProsegurDbType.Identificador_Alfanumerico, objDelegacion.DesZona))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data, If(objDelegacion.GmtModificacion = DateTime.MinValue, DateTime.Now, objDelegacion.GmtModificacion)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, objDelegacion.DesUsuarioModificacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PAIS", ProsegurDbType.Descricao_Curta, objDelegacion.OidPais))



            objTransacao.AdicionarItemTransacao(comando)

            Return OidDelegacion

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, String.Format(Traduzir("046_msg_Erro_AltaDelegacion"), ex))
        End Try

        Return Nothing

    End Function


#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por fazer a atualização da Delegacion no Banco de Dados
    ''' </summary>
    ''' <param name="ObjDelegacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/02/2013 Alterado
    ''' </history>
    Public Shared Sub AtualizaDelegacion(ByRef ObjDelegacion As ContractoServicio.Delegacion.SetDelegacion.Peticion)

        Try

            Dim ObjTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = My.Resources.AtualizaDelegacion

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PAIS", ProsegurDbType.Descricao_Curta, ObjDelegacion.OidPais))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PAIS", ProsegurDbType.Identificador_Alfanumerico, ObjDelegacion.CodPais))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DELEGACION", ProsegurDbType.Descricao_Longa, ObjDelegacion.DesDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_GMT_MINUTOS", ProsegurDbType.Inteiro_Curto, ObjDelegacion.NecGmtMinutos))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_VERANO_INICIO", ProsegurDbType.Data, ObjDelegacion.FyhVeranoInicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_VERANO_FIN", ProsegurDbType.Data, ObjDelegacion.FyhVeranoFin))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_VERANO_AJUSTE", ProsegurDbType.Inteiro_Curto, ObjDelegacion.NecVeranoAjuste))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_ZONA", ProsegurDbType.Descricao_Longa, ObjDelegacion.DesZona))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, ObjDelegacion.BolVigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, If(ObjDelegacion.GmtModificacion = DateTime.MinValue, DateTime.Now, ObjDelegacion.GmtModificacion)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, ObjDelegacion.DesUsuarioModificacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Descricao_Longa, ObjDelegacion.CodDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, ObjDelegacion.OidDelegacion))

            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.CommandType = CommandType.Text

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Catch ex As Exception
            Excepcion.Util.Tratar(ex, String.Format(Traduzir("048__msg_Erro_AtualizaDelegacion"), ex))
        End Try
    End Sub
#End Region

#Region "[DELETE]"

    ''' <summary>
    ''' Responsável por fazer a baixa da Delegacion no Banco de Dados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/07/2013 Alterado
    ''' </history>
    Public Shared Sub DeleteDelegacion(objPeticion As ContractoServicio.Delegacion.SetDelegacion.Peticion)

        Try

            Dim ObjTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = My.Resources.DeleteDelegacion

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, False))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, objPeticion.OidDelegacion))

            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.CommandType = CommandType.Text

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Catch ex As Exception
            Excepcion.Util.Tratar(ex, String.Format(Traduzir("047_msg_Erro_BajaDelegacion"), ex))
        End Try

    End Sub
#End Region

#Region "[DEMAIS]"

    ''' <summary>
    ''' Responsável por popular a classe de Delegacion 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/02/2013 Alterado
    ''' </history>
    Private Shared Function PopularGetDelegacion(dr As DataRow) As ContractoServicio.Delegacion.GetDelegacion.Delegacion

        ' criar objeto Delegacion
        Dim objDelegacion As New ContractoServicio.Delegacion.GetDelegacion.Delegacion

        Util.AtribuirValorObjeto(objDelegacion.OidDelegacion, dr("OID_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacion.CodDelegacion, dr("COD_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacion.DesDelegacion, dr("DES_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacion.OidPais, dr("OID_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacion.NecGmtMinutos, dr("NEC_GMT_MINUTOS"), GetType(Integer))
        Util.AtribuirValorObjeto(objDelegacion.FyhVeranoInicio, dr("FYH_VERANO_INICIO"), GetType(Date))
        Util.AtribuirValorObjeto(objDelegacion.FyhVeranoFin, dr("FYH_VERANO_FIN"), GetType(Date))
        Util.AtribuirValorObjeto(objDelegacion.NecVeranoAjuste, dr("NEC_VERANO_AJUSTE"), GetType(Integer))
        Util.AtribuirValorObjeto(objDelegacion.DesZona, dr("DES_ZONA"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacion.BolVigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objDelegacion.GmtCreacion, dr("GMT_CREACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objDelegacion.DesUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacion.GmtModificacion, dr("GMT_MODIFICACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objDelegacion.DesUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacion.CodPais, dr("COD_PAIS"), GetType(String))
        ' retornar objeto divisa preenchido
        Return objDelegacion

    End Function

    ''' <summary>
    ''' Verifica se o código da delegacion ja existe
    ''' </summary>
    ''' <param name="PeticionVerificaCodigo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificaCodigoDelegacion(PeticionVerificaCodigo As ContractoServicio.Delegacion.VerificaCodigoDelegacion.Peticion) As Boolean

        'CRIAR COMANDO
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'OBTER QUERY 
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaCodigoDelegacion.ToString())

        'SETAR PARAMETROS
        If Not String.IsNullOrEmpty(PeticionVerificaCodigo.Codigo) Then
            comando.CommandText &= " OID_DELEGACION = :OID_DELEGACION"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, PeticionVerificaCodigo.Codigo))
        End If

        'EXECUTAR COMANDO E RETORNAR OS DADOS
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0
    End Function

    ''' <summary>
    ''' Popula um objeto Delegacion através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 15/02/2013 Criado
    ''' </history>
    Private Shared Function PopularGetDelegacioneDetail(dr As DataRow) As ContractoServicio.Delegacion.Delegacion

        ' criar objeto termino Iac
        Dim objDelegacionesDetail As New ContractoServicio.Delegacion.Delegacion

        'Termino
        Util.AtribuirValorObjeto(objDelegacionesDetail.OidDelegacion, dr("OID_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacionesDetail.CodDelegacion, dr("COD_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacionesDetail.Description, dr("DES_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacionesDetail.NecGmTminutes, dr("NEC_GMT_MINUTOS"), GetType(Integer))
        Util.AtribuirValorObjeto(objDelegacionesDetail.FhyVeraoInicio, dr("FYH_VERANO_INICIO"), GetType(DateTime))
        Util.AtribuirValorObjeto(objDelegacionesDetail.FhyVeraoFim, dr("FYH_VERANO_FIN"), GetType(DateTime))
        Util.AtribuirValorObjeto(objDelegacionesDetail.NecVeraoAjuste, dr("NEC_VERANO_AJUSTE"), GetType(Integer))
        Util.AtribuirValorObjeto(objDelegacionesDetail.Zona, dr("DES_ZONA"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacionesDetail.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objDelegacionesDetail.OidPais, dr("OID_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacionesDetail.CodPais, dr("COD_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacionesDetail.GmtCreation, Now(), GetType(DateTime))
        Util.AtribuirValorObjeto(objDelegacionesDetail.Gmt_Modificacion, Now(), GetType(DateTime))
        Return objDelegacionesDetail

    End Function

    ''' <summary>
    ''' Faz a pesquisa e preenche do datatable, retornando uma coleção de delegações
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Public Shared Function GetComboDelegacoes() As ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion

        Dim objCombo As New ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetComboDelegaciones)
        comando.CommandType = CommandType.Text

        Dim dtDelegaciones As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtDelegaciones IsNot Nothing _
            AndAlso dtDelegaciones.Rows.Count > 0 Then
            For Each dr As DataRow In dtDelegaciones.Rows
                objCombo.Add(PopularComboDelegacoes(dr))
            Next
        End If

        Return objCombo

    End Function

    ''' <summary>
    ''' Popula um objeto delegações com os valores do datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/02/2012 Criado
    ''' </history>
    Private Shared Function PopularComboDelegacoes(dr As DataRow) As ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion

        ' criar objeto Pais
        Dim objDelegacao As New ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion

        Util.AtribuirValorObjeto(objDelegacao.Codigo, dr("OID_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objDelegacao.Descripcion, dr("DES_DELEGACION"), GetType(String))

        ' retorna objeto preenchido
        Return objDelegacao

    End Function

    ''' <summary>
    ''' Verifica se a delegação informada está sendo utilizada em plantas
    ''' </summary>
    ''' <param name="OidDelegacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 02/07/2012 Criado
    ''' </history>
    Public Shared Function VerificaUtilizacaoPlanta(OidDelegacion As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaUtilizacaoPlanta)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, OidDelegacion))

        Dim resultado As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If resultado > 0 Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Verifica se a delegação ja existe com o pais selecionado
    ''' </summary>
    ''' <param name="OidPais"></param>
    ''' <param name="CodDelegacion"></param>
    ''' <param name="CodPais"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/08/2013 Criado
    ''' </history>
    Public Shared Function VerificaDelegacaoCadastrada(OidPais, CodDelegacion, CodPais)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaDelegacaoCadastrada)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PAIS", ProsegurDbType.Objeto_Id, OidPais))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PAIS", ProsegurDbType.Identificador_Alfanumerico, CodPais))

        Dim resultado As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If resultado > 0 Then
            Return False
        End If

        Return True
    End Function

    Public Shared Function VerificarClienteCapitalYBancoTesoreriaEnUso(pOidDelegacion As String, pOidClienteCapital As String, pOidSubCliTesoreria As String, pOidPtoSerTesoreria As String, ByRef pDelegacion As ContractoServicio.Delegacion.Delegacion) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim cantidadDeRegistros As Integer = 0
        Dim dt As DataTable
        Dim retorno As Boolean = False

        'Primero le asigno Nothing
        pDelegacion = Nothing

        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarClienteCapitalYBancoTesoreriaEnUso)
        comando.CommandType = CommandType.Text


        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE_CAPITAL", ProsegurDbType.Objeto_Id, pOidClienteCapital))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE_TESORERIA", ProsegurDbType.Objeto_Id, pOidSubCliTesoreria))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO_TESORERIA", ProsegurDbType.Objeto_Id, pOidPtoSerTesoreria))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, pOidDelegacion))

        dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        cantidadDeRegistros = dt.Rows.Count
        If cantidadDeRegistros > 0 Then
            retorno = False
            pDelegacion = New ContractoServicio.Delegacion.Delegacion()
            pDelegacion.CodDelegacion = dt.Rows(0)("COD_DELEGACION")
            pDelegacion.Description = dt.Rows(0)("DES_DELEGACION")
        Else
            retorno = True
        End If


        Return retorno
    End Function

#End Region

End Class
