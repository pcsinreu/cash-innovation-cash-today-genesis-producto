Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis

Public Class Planta

#Region "[CONSULTAS]"

    ''' <summary>
    ''' Retorna os dados da Planta
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/02/2013 - Criado
    ''' </history>
    Public Shared Function GetPlanta(objPeticion As ContractoServicio.Planta.GetPlanta.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Planta.GetPlanta.PlantaColeccion
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaPlanta)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.oidDelegacion))

        If Not String.IsNullOrEmpty(objPeticion.CodPlanta) Then
            comando.CommandText &= " AND UPPER(COD_PLANTA) = :COD_PLANTA "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodPlanta.ToUpper()))
        End If

        If Not String.IsNullOrEmpty(objPeticion.DesPlanta) Then
            comando.CommandText &= " AND UPPER(DES_PLANTA) LIKE :DES_PLANTA "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PLANTA", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.DesPlanta.ToUpper() & "%"))
        End If

        If objPeticion.BolActivo IsNot Nothing Then
            comando.CommandText &= " AND BOL_ACTIVO = :BOL_ACTIVO"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.BolActivo))
        End If

        comando.CommandText &= " ORDER BY DES_PLANTA"

        Dim objPlanta As New ContractoServicio.Planta.GetPlanta.PlantaColeccion

        Dim dtPlanta As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        objPlanta = RetornaColecaoPlanta(dtPlanta)

        Return objPlanta

    End Function

    Public Shared Function ObterOidDelegacionPorOidPlanta(OidPlanta As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidDelegacionPorOidPlanta)

        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Identificador_Alfanumerico, OidPlanta))

        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)

        Return result

    End Function

    ''' <summary>
    '''  Obter COD_PLANTA pelo OID_PLANTA
    ''' </summary>
    ''' <param name="OidPlanta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObterCodPlantaPorOid(OidPlanta As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.ObterCodPlantaPorOid.ToString())

        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Identificador_Alfanumerico, OidPlanta))

        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)

        Return result

    End Function

    ''' <summary>
    ''' Retorna os dados da Planta
    ''' </summary>
    ''' <param name="identificadorPlanta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/02/2012 - Criado
    ''' </history>
    Public Shared Function GetDadosPlanta(identificadorPlanta As String) As ContractoServicio.Planta.GetPlantaDetail.Planta

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.ObterDadosPlantas)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Identificador_Alfanumerico, identificadorPlanta))

        ' criar objeto agrupaciones
        Dim objPlantas As New ContractoServicio.Planta.GetPlantaDetail.Planta

        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        objPlantas = RetornaPlanta(dtQuery)

        ' retornar objeto preenchido
        Return objPlantas
    End Function

    ''' <summary>
    ''' Verifica a planta esta sendo utilizada em algum sector
    ''' </summary>
    ''' <param name="OidPlanta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 02/07/2012 - Criado
    ''' </history>
    Public Shared Function VerificaUtilizacaoSector(OidPlanta As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaUtilizacaoSector)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Objeto_Id, OidPlanta))

        Dim resultado As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If resultado > 0 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' Verifica se o delegação passado está ativa
    ''' </summary>
    ''' <param name="OidDelegacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 02/07/2012 - Criado
    ''' </history>
    Public Shared Function VerificaActivoDelegacion(OidDelegacion As String) As Boolean

        Dim commando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        commando.CommandText = Util.PrepararQuery(My.Resources.VerificaAtivoDelegacion)
        commando.CommandType = CommandType.Text

        commando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, OidDelegacion))

        Dim resultado As Boolean

        Dim dtDelegacion As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, commando)

        If dtDelegacion.Rows.Count > 0 AndAlso dtDelegacion.Rows IsNot Nothing Then
            For Each dr As DataRow In dtDelegacion.Rows
                resultado = Util.AtribuirValorObj(dr("BOL_VIGENTE"), GetType(Boolean))
            Next
        End If

        Return resultado
    End Function
#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir a Planta no Banco de Dados.
    ''' </summary>
    ''' <param name="objPlanta"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/02/2013 Created
    ''' </history>
    Public Shared Function AltaPlanta(ObjPlanta As ContractoServicio.Planta.SetPlanta.Peticion, objTransacao As Transacao) As String
        Dim oidPlanta As String
        Try

            'Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaPlanta)
            comando.CommandType = CommandType.Text

            oidPlanta = Guid.NewGuid.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Objeto_Id, oidPlanta))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, ObjPlanta.OidDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, ObjPlanta.CodPlanta))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PLANTA", ProsegurDbType.Identificador_Alfanumerico, ObjPlanta.DesPlanta))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, ObjPlanta.BolActivo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, ObjPlanta.DesUsuarioCreacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICAION", ProsegurDbType.Identificador_Alfanumerico, ObjPlanta.DesUsuarioModificacion))

            objTransacao.AdicionarItemTransacao(comando)

            'objTransacao.RealizarTransacao()

            Return oidPlanta
        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("053_msg_erro_Insereregistro"))
        End Try

        Return Nothing

    End Function

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Responsável por dar baixa na Planta no Banco de Dados.
    ''' </summary>
    ''' <param name="CodPlanta"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/02/2013 Created
    ''' </history>
    Public Shared Sub BajaPlanta(CodPlanta As String, CodUsuario As String)

        Try

            Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, False))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, CodPlanta))

            comando.CommandText = Util.PrepararQuery(My.Resources.BajaPlanta)
            comando.CommandType = CommandType.Text

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("053_msg_erro_BajaRegistro"))
        End Try
    End Sub
#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por fazer a atualização da Planta no Banco de Dados
    ''' </summary>
    ''' <param name="objPlanta"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/02/2013 Alterado
    ''' </history>
    Public Shared Sub AtualizacionPlanta(objPlanta As ContractoServicio.Planta.SetPlanta.Peticion, objTransacao As Transacao)

        Try

            'Dim objTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = My.Resources.Actualizacion

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Descricao_Curta, objPlanta.OidDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PLANTA", ProsegurDbType.Descricao_Longa, objPlanta.DesPlanta))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPlanta.BolActivo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objPlanta.DesUsuarioModificacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PLANTA", ProsegurDbType.Descricao_Longa, objPlanta.CodPlanta))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Objeto_Id, objPlanta.OidPlanta))

            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.CommandType = CommandType.Text

            'AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
            objTransacao.AdicionarItemTransacao(comando)
        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("053_msg_erro_Atualizaregistro"))
        End Try
    End Sub
#End Region

#Region "[DEMAIS]"

    ''' <summary>
    ''' Verifica se o  se o código Plante e Delegacao ja estao vinculado
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificaCodigoDelegacionPlanta(objPeticion As ContractoServicio.Planta.VerificaExistencia.Peticion) As Boolean

        'CRIAR COMANDO
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'OBTER QUERY 
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaExistenciaPlanta)

        comando.CommandText &= " UPPER(COD_PLANTA) = :COD_PLANTA"
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodPlanta.ToUpper()))

        comando.CommandText &= " AND OID_DELEGACION = :OID_DELEGACION"
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodDelegacion))

        'EXECUTAR COMANDO E RETORNAR OS DADOS
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0
    End Function

    ''' <summary>
    ''' Verifica se o código da Planta ja existe
    ''' </summary>
    ''' <param name="PeticionVerificaCodigo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificaCodigoPlanta(PeticionVerificaCodigo As ContractoServicio.Planta.VerificaCodigoPlanta.Peticion) As Boolean

        'CRIAR COMANDO
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'OBTER QUERY 
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaCodigo)

        If (String.IsNullOrEmpty(PeticionVerificaCodigo.Codigo)) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, Traduzir("049_msg_erro_PlantaCodigo"))
        End If

        'SETAR PARAMETROS
        If Not String.IsNullOrEmpty(PeticionVerificaCodigo.Codigo) Then
            comando.CommandText &= " UPPER(OID_PLANTA) = :OID_PLANTA"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Identificador_Alfanumerico, PeticionVerificaCodigo.Codigo.ToUpper()))
        End If

        'EXECUTAR COMANDO E RETORNAR OS DADOS
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0
    End Function

    'Retorna uma coleção de Planta
    Private Shared Function RetornaColecaoPlanta(dtPlanta As DataTable) As ContractoServicio.Planta.GetPlanta.PlantaColeccion

        Dim objPlantaColeccion As New ContractoServicio.Planta.GetPlanta.PlantaColeccion

        If dtPlanta.Rows.Count > 0 AndAlso dtPlanta IsNot Nothing Then

            Dim objPlanta As ContractoServicio.Planta.GetPlanta.Planta = Nothing

            For Each dr In dtPlanta.Rows
                objPlanta = New ContractoServicio.Planta.GetPlanta.Planta()
                Util.AtribuirValorObjeto(objPlanta.OidDelegacion, dr("OID_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.CodDelegacion, dr("COD_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.DesDelegacion, dr("DES_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.CodPlanta, dr("COD_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.DesPlanta, dr("DES_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.BolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
                Util.AtribuirValorObjeto(objPlanta.OidPlanta, dr("OID_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.DesUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.DesUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))

                objPlanta.CodigoAjeno = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                objPlanta.CodigoAjeno = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_PLANTA").ToString())

                objPlantaColeccion.Add(objPlanta)
            Next
        End If

        Return objPlantaColeccion
    End Function

    Private Shared Function RetornaPlanta(dt As DataTable) As ContractoServicio.Planta.GetPlantaDetail.Planta

        Dim objPlanta As New ContractoServicio.Planta.GetPlantaDetail.Planta

        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then

            For Each dr In dt.Rows
                Util.AtribuirValorObjeto(objPlanta.OidDelegacion, dr("OID_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.CodDelegacion, dr("COD_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.DesDelegacion, dr("DES_DELEGACION"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.CodPlanta, dr("COD_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.DesPlanta, dr("DES_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.BolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
                Util.AtribuirValorObjeto(objPlanta.OidPlanta, dr("OID_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.DesUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
                Util.AtribuirValorObjeto(objPlanta.DesUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))

                objPlanta.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                objPlanta.CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_PLANTA").ToString())

                objPlanta.ImportesMaximos = New ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
                objPlanta.ImportesMaximos = ImporteMaximo.RecuperaimporteMaximoBase(dr("OID_PLANTA").ToString(), String.Empty)

            Next
        End If

        Return objPlanta
    End Function

#End Region
End Class
