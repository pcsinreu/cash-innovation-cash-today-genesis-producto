Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports Prosegur.Global.GesEfectivo.IAC
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

Public Class Sector

#Region "[CONSULTAS]"

    ''' <summary>
    ''' OBTÉM TODOS OS SETORES
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/03/2013 Criado
    ''' </history>
    Public Shared Function getSectores(ObjPeticion As ContractoServicio.Setor.GetSectores.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Setor.GetSectores.SetorColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetSectores)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(ObjPeticion.oidPlanta) Then
            comando.CommandText &= " AND TS.OID_PLANTA = :OID_PLANTA "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Objeto_Id, ObjPeticion.oidPlanta))
        End If

        If Not String.IsNullOrEmpty(ObjPeticion.codSector) Then
            comando.CommandText &= " AND TS.COD_SECTOR = :COD_SECTOR "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.codSector))
        End If

        If Not String.IsNullOrEmpty(ObjPeticion.desSector) Then
            comando.CommandText &= " AND UPPER(TS.DES_SECTOR) LIKE :DES_SECTOR "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SECTOR", ProsegurDbType.Descricao_Longa, "%" & ObjPeticion.desSector.ToUpper & "%"))
        End If

        If Not String.IsNullOrEmpty(ObjPeticion.oidSectorPadre) Then
            comando.CommandText &= " AND TSS.OID_SECTOR = :OID_SECTOR "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.oidSectorPadre))
        End If

        If ObjPeticion.bolCentroProceso IsNot Nothing Then
            comando.CommandText &= " AND TS.BOL_CENTRO_PROCESO = :BOL_CENTRO_PROCESO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CENTRO_PROCESO", ProsegurDbType.Logico, ObjPeticion.bolCentroProceso))
        End If

        If Not String.IsNullOrEmpty(ObjPeticion.oidTipoSector) Then
            comando.CommandText &= " AND TS.OID_TIPO_SECTOR = :OID_TIPO_SECTOR "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTOR", ProsegurDbType.Objeto_Id, ObjPeticion.oidTipoSector))
        End If

        If ObjPeticion.bolActivo IsNot Nothing Then
            comando.CommandText &= " AND TS.BOL_ACTIVO = :BOL_ACTIVO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, ObjPeticion.bolActivo))
        End If

        Dim objSetor As New ContractoServicio.Setor.GetSectores.SetorColeccion

        Dim dtSetor As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, ObjPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        objSetor = RetornaColecaoSetor(dtSetor)

        Return objSetor

    End Function

    Public Shared Function getComboSectores(objPeticion As ContractoServicio.Utilidad.GetComboSectores.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Utilidad.GetComboSectores.SectorColeccion

        ' criar objeto cliente coleccion
        Dim objSectores As New ContractoServicio.Utilidad.GetComboSectores.SectorColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder
        query.Append(" SELECT S.OID_SECTOR, S.COD_SECTOR, S.DES_SECTOR FROM GEPR_TSECTOR S ")
        query.AppendLine(" INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = S.OID_PLANTA ")
        query.AppendLine(" INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION ")
        query.AppendLine(" WHERE 1=1 ")

        If Not String.IsNullOrEmpty(objPeticion.Codigo) Then
            query.Append(" AND UPPER(S.COD_SECTOR) LIKE []COD_SECTOR ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.Codigo.ToUpper() & "%"))
        End If

        If Not String.IsNullOrEmpty(objPeticion.Descripcion) Then
            query.Append(" AND UPPER(S.DES_SECTOR) LIKE []DES_SECTOR ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SECTOR", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.Descripcion.ToUpper() & "%"))
        End If

        If Not String.IsNullOrEmpty(objPeticion.CodigoPlanta) Then
            query.Append(" AND P.COD_PLANTA = []COD_PLANTA ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoPlanta))
        End If

        If Not String.IsNullOrEmpty(objPeticion.CodigoDelegacion) Then
            query.Append(" AND D.COD_DELEGACION = []COD_DELEGACION ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))
        End If

        ' adicionar ordenação
        query.Append(" ORDER BY S.COD_SECTOR ")

        ' obter query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        'Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        Dim dtQuery As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows
                ' adicionar para coleção
                objSectores.Add(PopularGetComboSectores(dr))
            Next

        End If

        ' retornar coleção de Sectores
        Return objSectores

    End Function

    Private Shared Function PopularGetComboSectores(dr As DataRow) As ContractoServicio.Utilidad.GetComboSectores.Sector1

        ' criar objeto formato
        Dim objSector As New ContractoServicio.Utilidad.GetComboSectores.Sector1

        Util.AtribuirValorObjeto(objSector.Codigo, dr("COD_SECTOR"), GetType(String))
        Util.AtribuirValorObjeto(objSector.Descripcion, dr("DES_SECTOR"), GetType(String))
        Util.AtribuirValorObjeto(objSector.OidSector, dr("OID_SECTOR"), GetType(String))

        ' retorna objeto preenchido
        Return objSector

    End Function

    ''' <summary>
    ''' Obtener los sectores activos del IAC
    ''' </summary>
    ''' <param name="ObjPeticion">Objeto Peticion</param>
    ''' <returns>Lista de setores</returns>
    ''' <remarks>
    ''' Buscar setores de origem que tem permissão para Ingreso
    ''' Buscar setores de destino que tem permissão para Ingreso
    ''' </remarks>
    Public Shared Function GetSectoresIAC(ObjPeticion As ContractoServicio.Sector.GetSectoresIAC.Peticion) As List(Of ContractoServicio.Sector.GetSectoresIAC.Sector)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetSectoresIAC)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, ObjPeticion.CodigoDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_RELACION_CON_FORMULARIO", ProsegurDbType.Descricao_Curta, Genesis.Comon.Extenciones.RecuperarValor(ObjPeticion.CodigoTipoSitio)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PLANTA", ProsegurDbType.Descricao_Curta, ObjPeticion.CodigoPlanta))

        ' Sectores de destino também passou a validar formulario de ingreso
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_FORMULARIO", ProsegurDbType.Descricao_Curta, ObjPeticion.CodigoFormulario))

        'If ObjPeticion.CodigoTipoSitio = Genesis.Comon.Enumeradores.TipoSitio.Origen Then
        '    comando.CommandText = String.Format(comando.CommandText, "", "", "AND FORM.COD_FORMULARIO = []COD_FORMULARIO")
        '    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_FORMULARIO", ProsegurDbType.Descricao_Curta, ObjPeticion.CodigoFormulario))
        'Else
        '    Dim Distinct As String = "Distinct"
        '    Dim InnerJoin As New StringBuilder
        '    Dim Where As New StringBuilder

        '    InnerJoin.AppendLine(" INNER JOIN SAPR_TCARACTFORMXFORMULARIO CFF ON CFF.OID_FORMULARIO = FORM.OID_FORMULARIO " _
        '                       & "INNER JOIN SAPR_TCARACT_FORMULARIO CF ON CF.OID_CARACT_FORMULARIO = CFF.OID_CARACT_FORMULARIO")

        '    Where.AppendLine(" AND ((CF.COD_CARACT_FORMULARIO IN ('CARACTERISTICA_PRINCIPAL_GESTION_REMESAS', 'ACCION_REENVIOS', 'REENVIO_AUTOMATICO')) ")
        '    Where.AppendLine(" OR (CF.COD_CARACT_FORMULARIO IN ('CARACTERISTICA_PRINCIPAL_GESTION_BULTOS', 'ACCION_REENVIOS', 'REENVIO_AUTOMATICO')))")

        '    comando.CommandText = String.Format(comando.CommandText, Distinct, InnerJoin, Where)
        'End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        Dim Sectores As New List(Of ContractoServicio.Sector.GetSectoresIAC.Sector)
        Sectores = CargarSectores(dt)
        Return Sectores

    End Function

    Public Shared Function GetSectoresTesoro(ObjPeticion As ContractoServicio.Sector.GetSectoresTesoro.Peticion) As List(Of ContractoServicio.Sector.GetSectoresTesoro.Sector)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetSectoresTesoro)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, ObjPeticion.CodigoDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PLANTA", ProsegurDbType.Descricao_Curta, ObjPeticion.CodigoPlanta))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SECTOR", ProsegurDbType.Descricao_Curta, ObjPeticion.CodigoTipoSector))

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        Dim Sectores As New List(Of ContractoServicio.Sector.GetSectoresTesoro.Sector)
        Sectores = CargarSectoresTesoto(dt)
        Return Sectores

    End Function

    ''' <summary>
    ''' OBTÉM OS DADOS DE UM SETOR
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/03/2013 Criado
    ''' </history>
    Public Shared Function getSectoresDetail(ObjPeticion As ContractoServicio.Setor.GetSectoresDetail.Peticion) As ContractoServicio.Setor.GetSectoresDetail.Sector

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.getSetorDetail)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(ObjPeticion.OidSector) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.OidSector))
        End If

        Dim objSetor As New ContractoServicio.Setor.GetSectoresDetail.Sector

        Dim dtSetor As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        objSetor = RetornaSetor(dtSetor)

        Return objSetor

    End Function

    ''' <summary>
    ''' Verifica se o sector pai esta sendo usado em algum filho
    ''' </summary>
    ''' <param name="OidSectorPai"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/06/2013 Criado
    ''' </history>
    Public Shared Function VerificaUtilizacaoSectorPai(OidSectorPai As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaUtilizacaoSectorPadre)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR_PADRE", ProsegurDbType.Objeto_Id, OidSectorPai))

        Dim Retorno As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If Retorno > 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Verifica se a planta selecionada está ativa
    ''' </summary>
    ''' <param name="OidPlanta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/07/2013 Criado
    ''' </history>
    Public Shared Function VerificaPlantaAtiva(OidPlanta As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaPlantaAtivada)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Objeto_Id, OidPlanta))

        Dim dtPlanta As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim resultado As Boolean

        If dtPlanta.Rows.Count > 0 AndAlso dtPlanta.Rows IsNot Nothing Then
            For Each dt As DataRow In dtPlanta.Rows
                resultado = Util.AtribuirValorObj(dt("BOL_ACTIVO"), GetType(Boolean))
            Next
        End If

        Return resultado

    End Function

    ''' <summary>
    ''' Verifica se o sector informado está ativa
    ''' </summary>
    ''' <param name="OidSector"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/07/2013 Criado
    ''' </history>
    Public Shared Function VerificaSectorAtivo(OidSector As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaSectorPaiAtivado)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Objeto_Id, OidSector))

        Dim dtSector As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim resultado As Boolean

        If dtSector.Rows.Count > 0 AndAlso dtSector.Rows IsNot Nothing Then
            For Each dt As DataRow In dtSector.Rows
                resultado = Util.AtribuirValorObj(dt("BOL_ACTIVO"), GetType(Boolean))
            Next
        End If

        Return resultado
    End Function
#End Region

#Region "[INSERT]"

    ''' <summary>
    ''' INSERE OS DADOS DE SETORES
    ''' </summary>
    ''' <param name="ObjPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/03/2013 Criado
    ''' </history>
    Public Shared Function setSectores(ObjPeticion As ContractoServicio.Setor.SetSectores.Peticion, objTransacao As Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaSector)
        comando.CommandType = CommandType.Text

        Dim oidSector As String = Guid.NewGuid().ToString
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Objeto_Id, oidSector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR_PADRE", ProsegurDbType.Objeto_Id, ObjPeticion.oidSectorPadre))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTOR", ProsegurDbType.Objeto_Id, ObjPeticion.oidTipoSector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Objeto_Id, ObjPeticion.oidPlanta))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.codSector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SECTOR", ProsegurDbType.Descricao_Longa, ObjPeticion.desSector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CENTRO_PROCESO", ProsegurDbType.Logico, ObjPeticion.bolCentroProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MIGRACION", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.codMigracion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, ObjPeticion.bolActivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, ObjPeticion.gmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, ObjPeticion.desUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, ObjPeticion.gmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, ObjPeticion.desUsuarioModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CONTEO", ProsegurDbType.Logico, ObjPeticion.bolConteo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_PERMITE_DISPONER_VALOR", ProsegurDbType.Logico, ObjPeticion.bolPermiteDisponerValor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TESORO", ProsegurDbType.Logico, ObjPeticion.bolTesoro))

        objTransacao.AdicionarItemTransacao(comando)

        Return oidSector

    End Function

    Public Shared Sub GrabarTipoSectirDelegacion(objPeticion As ContractoServicio.Setor.GrabarTipoSectorDelegacion.Peticion)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaTipoSetorDelegacion)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTORXDELEGACION", ProsegurDbType.Objeto_Id, objPeticion.OidTipoSectorDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, objPeticion.OidDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTOR", ProsegurDbType.Objeto_Id, objPeticion.OidTipoSector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, objPeticion.GmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, objPeticion.DesUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, objPeticion.GmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objPeticion.DesUsuarioModificacion))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

    End Sub

    Public Shared Sub GrabarTipoSectorPlanta(objPeticion As ContractoServicio.Setor.GrabarTipoSectorPlanta.Peticion)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaTipoSetorPlanta)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTORXPLANTA", ProsegurDbType.Objeto_Id, objPeticion.OidTipoSectorPlanta))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Objeto_Id, objPeticion.OidPlanta))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTOR", ProsegurDbType.Objeto_Id, objPeticion.OidTipoSector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, objPeticion.GmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, objPeticion.DesUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, objPeticion.GmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objPeticion.DesUsuarioModificacion))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

    End Sub

#End Region

#Region "[DEMAIS]"

    ''' <summary>
    ''' Verifica se o Sector é Centro de Processo.
    ''' </summary>
    ''' <param name="ObjPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/03/2013 Criado
    ''' </history>
    Public Shared Function GetSectoresCentroProcesso(ObjPeticion As ContractoServicio.Setor.SetSectores.Peticion) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaCentroProcesso)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.oidSectorPadre))

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), Boolean)

    End Function

    ''' <summary>
    ''' VERIFICA SE O TIPO SECTOR É ATIVO 
    ''' </summary>
    ''' <param name="oidTipoSector"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 18/03/2013 Criado
    ''' </history>
    Public Shared Function VerificaTipoSectores(oidTipoSector As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim sb As New StringBuilder

        sb.Append("SELECT BOL_ACTIVO FROM GEPR_TTIPO_SECTOR WHERE OID_TIPO_SECTOR = " & "'" & oidTipoSector & "'")

        comando.CommandText = Util.PrepararQuery(sb.ToString())
        comando.CommandType = CommandType.Text

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), Boolean)

    End Function

    Public Shared Function VerificaCodigoExistenteTipoSectorDelegacion(oidTipoSector As String, oidDelegacion As String) As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim sb As New StringBuilder

        sb.Append("SELECT COUNT(OID_TIPO_SECTORXDELEGACION) FROM GEPR_TTIPO_SECTORXDELEGACION WHERE OID_TIPO_SECTOR = " & "'" & oidTipoSector & "'" & " AND OID_DELEGACION = " & "'" & oidDelegacion & "'")

        comando.CommandText = Util.PrepararQuery(sb.ToString())
        comando.CommandType = CommandType.Text

        Dim retorno As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If retorno > 0 Then
            Return False
        End If
        Return True

    End Function

    Public Shared Function VerificaCodigoExistenteTipoSectorPlanta(oidTipoSector As String, oidPlanta As String) As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim sb As New StringBuilder

        sb.Append("SELECT COUNT(OID_TIPO_SECTORXPLANTA) FROM GEPR_TTIPO_SECTORXPLANTA WHERE OID_TIPO_SECTOR = " & "'" & oidTipoSector & "'" & " AND OID_PLANTA = " & "'" & oidPlanta & "'")

        comando.CommandText = Util.PrepararQuery(sb.ToString())
        comando.CommandType = CommandType.Text

        Dim retorno As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If retorno > 0 Then
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' Popula a classe de Sectores
    ''' </summary>
    ''' <param name="dtSetor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/03/2013 Criado
    ''' </history>
    Private Shared Function RetornaColecaoSetor(dtSetor As DataTable) As Setor.GetSectores.SetorColeccion

        Dim objRetornaSetor As New ContractoServicio.Setor.GetSectores.SetorColeccion

        If dtSetor IsNot Nothing AndAlso dtSetor.Rows.Count > 0 Then

            Dim objSetor As ContractoServicio.Setor.GetSectores.Setor = Nothing
            Dim codigoTipoSetor As String = String.Empty

            For Each dr As DataRow In dtSetor.Rows
                objSetor = New ContractoServicio.Setor.GetSectores.Setor()

                Util.AtribuirValorObjeto(objSetor.CodDelegacion, dr("COD_DELEGACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.OidDelegacion, dr("OID_DELEGACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.DesDelegacion, dr("DES_DELEGACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.bolActivo, dr("BOL_ACTIVO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSetor.bolCentroProceso, dr("BOL_CENTRO_PROCESO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSetor.bolConteo, dr("BOL_CONTEO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSetor.bolTesoro, dr("BOL_TESORO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSetor.codMigracion, dr("COD_MIGRACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.codSector, dr("FILHO"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.codSectorPadre, dr("PAI"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.desPlanta, dr("DES_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objSetor.desSector, dr("DESC_SECTOR_FILHO"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.desSectorPadre, dr("DESC_SECTOR_PAI"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.desTipoSector, dr("DES_TIPO_SECTOR"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.desUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
                Util.AtribuirValorObjeto(objSetor.desUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.gmtCreacion, dr("GMT_CREACION"), GetType(Date)) 'OK
                Util.AtribuirValorObjeto(objSetor.gmtModificacion, dr("GMT_MODIFICACION"), GetType(Date)) 'OK
                Util.AtribuirValorObjeto(objSetor.oidPlanta, dr("OID_PLANTA"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.oidSector, dr("OID_SECTOR"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.oidSectorPadre, dr("OIDSECTOR_PAI"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.oidTipoSector, dr("OID_TIPO_SECTOR"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.bolPermiteDisponerValor, dr("BOL_PERMITE_DISPONER_VALOR"), GetType(Boolean)) 'OK
                objSetor.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                objSetor.CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_TIPO_SECTOR").ToString)
                objRetornaSetor.Add(objSetor)
            Next

        End If

        Return objRetornaSetor
    End Function

    Private Shared Function CargarSectores(dt As DataTable) As List(Of ContractoServicio.Sector.GetSectoresIAC.Sector)

        Dim Sectores As New List(Of ContractoServicio.Sector.GetSectoresIAC.Sector)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim codigoTipoSetor As String = String.Empty

            For Each dr As DataRow In dt.Rows
                Dim objSector As New ContractoServicio.Sector.GetSectoresIAC.Sector

                Util.AtribuirValorObjeto(objSector.CodDelegacion, dr("COD_DELEGACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.OidDelegacion, dr("OID_DELEGACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.DesDelegacion, dr("DES_DELEGACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.bolActivo, dr("BOL_ACTIVO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSector.bolCentroProceso, dr("BOL_CENTRO_PROCESO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSector.bolConteo, dr("BOL_CONTEO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSector.bolTesoro, dr("BOL_TESORO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSector.codMigracion, dr("COD_MIGRACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.codSector, dr("FILHO"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.codSectorPadre, dr("PAI"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.desPlanta, dr("DES_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objSector.desSector, dr("DESC_SECTOR_FILHO"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.desSectorPadre, dr("DESC_SECTOR_PAI"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.desTipoSector, dr("DES_TIPO_SECTOR"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.desUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
                Util.AtribuirValorObjeto(objSector.desUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.gmtCreacion, dr("GMT_CREACION"), GetType(Date)) 'OK
                Util.AtribuirValorObjeto(objSector.gmtModificacion, dr("GMT_MODIFICACION"), GetType(Date)) 'OK
                Util.AtribuirValorObjeto(objSector.oidPlanta, dr("OID_PLANTA"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.oidSector, dr("OID_SECTOR"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.oidSectorPadre, dr("OIDSECTOR_PAI"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.oidTipoSector, dr("OID_TIPO_SECTOR"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSector.bolPermiteDisponerValor, dr("BOL_PERMITE_DISPONER_VALOR"), GetType(Boolean)) 'OK
                objSector.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                objSector.CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_TIPO_SECTOR").ToString)
                Sectores.Add(objSector)
            Next

        End If

        Return Sectores
    End Function
    Private Shared Function CargarSectoresTesoto(dt As DataTable) As List(Of ContractoServicio.Sector.GetSectoresTesoro.Sector)

        Dim Sectores As New List(Of ContractoServicio.Sector.GetSectoresTesoro.Sector)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim codigoTipoSetor As String = String.Empty

            For Each dr As DataRow In dt.Rows

                Dim objSector As New ContractoServicio.Sector.GetSectoresTesoro.Sector

                Util.AtribuirValorObjeto(objSector.Identificador, dr("OID_SECTOR"), GetType(String))
                Util.AtribuirValorObjeto(objSector.Codigo, dr("COD_SECTOR"), GetType(String))
                Util.AtribuirValorObjeto(objSector.Descripcion, dr("DES_SECTOR"), GetType(String))

                Sectores.Add(objSector)
            Next

        End If

        Return Sectores
    End Function

    Private Shared Function RetornaSetor(dtSetor As DataTable) As Setor.GetSectoresDetail.Sector

        Dim objSetor As New ContractoServicio.Setor.GetSectoresDetail.Sector

        If dtSetor IsNot Nothing AndAlso dtSetor.Rows.Count > 0 Then
            For Each dr In dtSetor.Rows
                Util.AtribuirValorObjeto(objSetor.CodDelegacion, dr("COD_DELEGACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.OidDelegacion, dr("OID_DELEGACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.DesDelegacion, dr("DES_DELEGACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.bolActivo, dr("BOL_ACTIVO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSetor.bolCentroProceso, dr("BOL_CENTRO_PROCESO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSetor.bolConteo, dr("BOL_CONTEO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSetor.bolTesoro, dr("BOL_TESORO"), GetType(Boolean)) 'OK
                Util.AtribuirValorObjeto(objSetor.codMigracion, dr("COD_MIGRACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.codSector, dr("FILHO"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.codSectorPadre, dr("PAI"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.desPlanta, dr("DES_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objSetor.desSector, dr("DESC_SECTOR_FILHO"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.desSectorPadre, dr("DESC_SECTOR_PAI"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.desTipoSector, dr("DES_TIPO_SECTOR"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.desUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
                Util.AtribuirValorObjeto(objSetor.desUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.gmtCreacion, dr("GMT_CREACION"), GetType(Date)) 'OK
                Util.AtribuirValorObjeto(objSetor.gmtModificacion, dr("GMT_MODIFICACION"), GetType(Date)) 'OK
                Util.AtribuirValorObjeto(objSetor.oidPlanta, dr("OID_PLANTA"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.oidSector, dr("OID_SECTOR"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.oidSectorPadre, dr("OIDSECTOR_PAI"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.oidTipoSector, dr("OID_TIPO_SECTOR"), GetType(String)) 'OK
                Util.AtribuirValorObjeto(objSetor.bolPermiteDisponerValor, dr("BOL_PERMITE_DISPONER_VALOR"), GetType(Boolean))
                objSetor.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                objSetor.CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_SECTOR").ToString())

                objSetor.ImportesMaximos = New ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
                objSetor.ImportesMaximos = ImporteMaximo.RecuperaimporteMaximoBase(String.Empty, dr("OID_SECTOR").ToString())

            Next
            Return objSetor
        End If
        Return Nothing
    End Function

#End Region

#Region "[UPDATE]"

    Public Shared Function AtualizarSector(ObjPeticion As Setor.SetSectores.Peticion, objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.AtualizaSector)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SECTOR", ProsegurDbType.Descricao_Curta, ObjPeticion.codSector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SECTOR", ProsegurDbType.Descricao_Longa, ObjPeticion.desSector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR_PADRE", ProsegurDbType.Objeto_Id, ObjPeticion.oidSectorPadre))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTOR", ProsegurDbType.Objeto_Id, ObjPeticion.oidTipoSector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Objeto_Id, ObjPeticion.oidPlanta))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CENTRO_PROCESO", ProsegurDbType.Logico, ObjPeticion.bolCentroProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_PERMITE_DISPONER_VALOR", ProsegurDbType.Logico, ObjPeticion.bolPermiteDisponerValor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TESORO", ProsegurDbType.Logico, ObjPeticion.bolTesoro))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CONTEO", ProsegurDbType.Logico, ObjPeticion.bolConteo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MIGRACION", ProsegurDbType.Descricao_Curta, ObjPeticion.codMigracion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, ObjPeticion.bolActivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, ObjPeticion.gmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.desUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, ObjPeticion.gmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.desUsuarioModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Objeto_Id, ObjPeticion.oidSector))

        objTransacao.AdicionarItemTransacao(comando)

        Return ObjPeticion.codSector
    End Function
#End Region

End Class
