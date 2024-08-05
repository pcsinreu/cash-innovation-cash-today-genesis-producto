Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis

Public Class TipoSetor

#Region "[CONSULTAS]"

    ''' <summary>
    ''' Obtém todos os tipos setores.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/03/2013 Criado
    ''' </history>
    Public Shared Function GetTiposSectores(objPeticion As ContractoServicio.TipoSetor.GetTiposSectores.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BucaTipoSetor)
        comando.CommandType = CommandType.Text

        Dim filtros As New System.Text.StringBuilder
        Dim indice As Integer = 0

        If objPeticion IsNot Nothing Then

            If Not String.IsNullOrEmpty(objPeticion.codTipoSector) Then
               
                comando.CommandText &= " WHERE UPPER(TSETOR.COD_TIPO_SECTOR) = :COD_TIPO_SECTOR "
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SECTOR", ProsegurDbType.Identificador_Alfanumerico, objPeticion.codTipoSector.ToUpper))
            End If

            If objPeticion.bolActivo IsNot Nothing Then
                If comando.CommandText.Contains("WHERE") Then
                    comando.CommandText &= " AND TSETOR.BOL_ACTIVO = :BOL_ACTIVO"
                Else
                    comando.CommandText &= " WHERE TSETOR.BOL_ACTIVO = :BOL_ACTIVO"
                End If
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.bolActivo))
            End If

            If objPeticion.desTipoSector <> Nothing Then
                If comando.CommandText.Contains("WHERE") Then
                    comando.CommandText &= " AND UPPER(TSETOR.DES_TIPO_SECTOR) LIKE :DES_TIPO_SECTOR "
                Else
                    comando.CommandText &= " WHERE UPPER(TSETOR.DES_TIPO_SECTOR) LIKE :DES_TIPO_SECTOR"
                End If
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_SECTOR", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.desTipoSector.ToUpper & "%"))
            End If

            If objPeticion.CaractTipoSector IsNot Nothing Then
                For Each caracteristica As ContractoServicio.TipoSetor.GetTiposSectores.Caracteristica In objPeticion.CaractTipoSector
                    indice += 1
                    If filtros.Length > 0 Then
                        filtros.Append(",")
                    End If
                    filtros.AppendLine("'" & caracteristica.codCaractTipoSector & "'")
                Next

                If filtros.Length > 0 Then
                    If comando.CommandText.Contains("WHERE") Then
                        comando.CommandText &= " AND TCT.COD_CARACT_TIPOSECTOR IN (" & filtros.ToString() & ") "
                    Else
                        comando.CommandText &= " WHERE TCT.COD_CARACT_TIPOSECTOR IN (" & filtros.ToString() & ") "
                    End If
                End If
            End If

        End If

        comando.CommandText &= " ORDER BY TSETOR.COD_TIPO_SECTOR"

        'Cria o objeto de TipoSetor
        Dim objTipoSetor As New ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion

        Dim dtTipoSetor As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        objTipoSetor = RetornaColecaoTiposSetor(dtTipoSetor)

        Return objTipoSetor
    End Function

    ''' <summary>
    ''' Obter COD_TIPO_SECTOR pelo OID_TIPO_SECTOR
    ''' </summary>
    ''' <param name="OidTipoSector"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObterCodTipoSectorPotOid(OidTipoSector As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.ObterCodTipoSectorPorOid.ToString)

        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTOR", ProsegurDbType.Identificador_Alfanumerico, OidTipoSector))

        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)

        Return result

    End Function

    ''' <summary>
    ''' Operación que permite realizar la consulta de las características que no pertenecen al tipo sector.
    ''' </summary>
    ''' <param name="oidTipoSector"></param>
    ''' <param name="codSetor" ></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history>
    Public Shared Function GetCaractNoPertenecTipoSector(oidTipoSector As String, codSetor As String) As ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.TipoSectorNotPerceteColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.getCaractNoPertenecTipoSector
        comando.CommandType = CommandType.Text

        Dim objTipoSetor As New ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.TipoSectorNotPerceteColeccion

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTOR", ProsegurDbType.Identificador_Alfanumerico, oidTipoSector))

        Dim dtTipoSetor As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtTipoSetor IsNot Nothing _
            AndAlso dtTipoSetor.Rows.Count > 0 Then

            For Each dr As DataRow In dtTipoSetor.Rows
                objTipoSetor.Add(PopulaGetTipoSetor(dr, codSetor))
            Next
        End If

        Return objTipoSetor
    End Function

    ''' <summary>
    ''' Verifica se o Tipo Sector esta sendo utilizado em Sector
    ''' </summary>
    ''' <param name="OidTipoSector"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/06/2013 Criado
    ''' </history>
    Public Shared Function VerificaTipoSectorUtilizadoSetor(OidTipoSector As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VertificaTipoSector)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTOR", ProsegurDbType.Objeto_Id, OidTipoSector))

        Dim retorno As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If retorno > 0 Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por atualizar o tipo Setor no DB.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history>
    Public Shared Sub AtualizarTipoSetor(objPeticion As ContractoServicio.TipoSetor.SetTiposSectores.Peticion, objTransacao As Transacao)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AtualizaTipoSector)
            comando.CommandType = CommandType.Text

            If objPeticion.codCaractTipoSector IsNot Nothing Then
                BajaCaracteristicasPorTipoSector(objPeticion.codTipoSector, objTransacao)
            End If

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SECTOR", ProsegurDbType.Objeto_Id, objPeticion.codTipoSector))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_SECTOR", ProsegurDbType.Descricao_Longa, objPeticion.desTipoSector))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.bolActivo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objPeticion.desUsuarioModificacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTOR", ProsegurDbType.Identificador_Alfanumerico, objPeticion.oidTipoSector))

            objTransacao.AdicionarItemTransacao(comando)

            AltaCaracteristicasPorTipoSetor(objPeticion, objTransacao, objPeticion.desUsuarioModificacion)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("054_msg_erro_TipoSetorGravar"))
        End Try
    End Sub
#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Insere os dados de tipos setores.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history>
    Public Shared Function SetTiposSectores(objPeticion As ContractoServicio.TipoSetor.SetTiposSectores.Peticion, objTransacao As Transacao) As String
        Dim oid_tipo_sector As String

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.CommandText = Util.PrepararQuery(My.Resources.AltaTipoSetor)
            comando.CommandType = CommandType.Text

            oid_tipo_sector = Guid.NewGuid().ToString

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SECTOR", ProsegurDbType.Objeto_Id, oid_tipo_sector))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SECTOR", ProsegurDbType.Identificador_Alfanumerico, objPeticion.codTipoSector))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_SECTOR", ProsegurDbType.Descricao_Longa, objPeticion.desTipoSector))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.bolActivo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data, objPeticion.gmtCreacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, objPeticion.desUsuarioCreacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data, objPeticion.gmtModificacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objPeticion.desUsuarioCreacion))

            objTransacao.AdicionarItemTransacao(comando)

            AltaCaracteristicasPorTipoSetor(objPeticion, objTransacao, objPeticion.desUsuarioCreacion)

            Return oid_tipo_sector
        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("054_msg_erro_TipoSetorGravar"))
        End Try

        Return Nothing
    End Function

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Deleta caracteristica do Tipo Sector
    ''' </summary>
    ''' <param name="CodigoTipoSetor"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/03/2013 Created
    ''' </history>
    Public Shared Sub BajaCaracteristicasPorTipoSector(CodigoTipoSetor As String, objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaCaracteristicaTipoSetor.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SECTOR", ProsegurDbType.Identificador_Alfanumerico, CodigoTipoSetor))

        ' adicionar o comando a transação.
        objTransacao.AdicionarItemTransacao(comando)

    End Sub
#End Region

#Region "[DEMAIS METODOS]"

    ''' <summary>
    ''' Retorna uma coleção de TipoSetor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/03/2013 Criado
    ''' </history>
    Private Shared Function RetornaColecaoTiposSetor(dtTipoSetor As DataTable) As ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion

        Dim objRetornaTipoSetor As New ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion

        If dtTipoSetor IsNot Nothing AndAlso dtTipoSetor.Rows.Count > 0 Then

            Dim objTipoSetor As ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor = Nothing
            Dim objCaracteristica As ContractoServicio.TipoSetor.GetTiposSectores.CaracteristicaRespuesta = Nothing
            Dim codigoTipoSetor As String = String.Empty

            For Each dr As DataRow In dtTipoSetor.Rows
                If objTipoSetor Is Nothing OrElse dr("COD_TIPO_SECTOR").ToString() <> codigoTipoSetor Then
                    objTipoSetor = New ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor()
                    objRetornaTipoSetor.Add(objTipoSetor)
                    objTipoSetor.CaractTipoSector = New ContractoServicio.TipoSetor.GetTiposSectores.CaracteristicaRespuestaColeccion()
                    Util.AtribuirValorObjeto(objTipoSetor.bolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
                    Util.AtribuirValorObjeto(objTipoSetor.codTipoSector, dr("COD_TIPO_SECTOR"), GetType(String))
                    Util.AtribuirValorObjeto(objTipoSetor.desTipoSector, dr("DES_TIPO_SECTOR"), GetType(String))
                    Util.AtribuirValorObjeto(objTipoSetor.desUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
                    Util.AtribuirValorObjeto(objTipoSetor.desUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))
                    Util.AtribuirValorObjeto(objTipoSetor.gmtCreacion, dr("GMT_CREACION"), GetType(Date))
                    Util.AtribuirValorObjeto(objTipoSetor.gmtModificacion, dr("GMT_MODIFICACION"), GetType(Date))
                    Util.AtribuirValorObjeto(objTipoSetor.oidTipoSector, dr("OID_TIPO_SECTOR"), GetType(String))
                    codigoTipoSetor = objTipoSetor.codTipoSector
                End If
                If dr("cod_caract_tiposector") IsNot Nothing AndAlso dr("cod_caract_tiposector") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("cod_caract_tiposector").ToString()) Then
                    objCaracteristica = New ContractoServicio.TipoSetor.GetTiposSectores.CaracteristicaRespuesta()
                    objTipoSetor.CaractTipoSector.Add(objCaracteristica)
                    Util.AtribuirValorObjeto(objCaracteristica.Codigo, dr("cod_caract_tiposector"), GetType(String))
                    Util.AtribuirValorObjeto(objCaracteristica.Descripcion, dr("des_caract_tiposector"), GetType(String))
                End If
                objTipoSetor.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                objTipoSetor.CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_TIPO_SECTOR").ToString)

            Next

        End If

        Return objRetornaTipoSetor
    End Function

    ''' <summary>
    ''' Obtém todas as caracteristicas de tipos setores ativos.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/03/2013 Criado
    ''' </history>
    Public Shared Function GetComboCaractTipoSector() As ContractoServicio.Utilidad.GetComboCaractTipoSector.CaracteristicaColeccion

        'cria commando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.getComboCaract)
        comando.CommandType = CommandType.Text

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornoCaract As New ContractoServicio.Utilidad.GetComboCaractTipoSector.CaracteristicaColeccion

        If dt IsNot Nothing _
            AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                objRetornoCaract.Add(PopulaComboCaracteristicas(dr))
            Next
        End If
        Return objRetornoCaract
    End Function

    ''' <summary>
    ''' Preenche a coleção de carateristícas do setor através do DataTable.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/03/2013 Criado
    ''' </history>
    Private Shared Function PopulaComboCaracteristicas(dr As DataRow) As Utilidad.GetComboCaractTipoSector.Caracteristica

        Dim objCaractSetor As New ContractoServicio.Utilidad.GetComboCaractTipoSector.Caracteristica

        Util.AtribuirValorObjeto(objCaractSetor.Codigo, dr("COD_CARACT_TIPOSECTOR"), GetType(String))
        Util.AtribuirValorObjeto(objCaractSetor.Descripcion, dr("DES_CARACT_TIPOSECTOR"), GetType(String))

        Return objCaractSetor
    End Function

    ''' <summary>
    ''' Popula a classe de tipo setor.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/03/2013 Criado
    ''' </history>
    Private Shared Function PopulaGetTipoSetor(dr As DataRow, codSetor As String) As ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.TipoSectorNotPercete

        Dim objCaracterTipoSector As New ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.TipoSectorNotPercete

        Util.AtribuirValorObjeto(objCaracterTipoSector.codCaractTipoSector, dr("COD_CARACT_TIPOSECTOR"), GetType(String))
        Util.AtribuirValorObjeto(objCaracterTipoSector.codTipoSector, codSetor, GetType(String))
        Util.AtribuirValorObjeto(objCaracterTipoSector.desCaractTipoSector, dr("DES_CARACT_TIPOSECTOR"), GetType(String))

        Return objCaracterTipoSector
    End Function

    ''' <summary>
    ''' Insere as caracteristicas do tipo setor
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 06/03/2013 Criado
    ''' </history>
    Public Shared Sub AltaCaracteristicasPorTipoSetor(objPeticion As ContractoServicio.TipoSetor.SetTiposSectores.Peticion, objTransacao As Transacao, codigoUsuario As String)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As String = Util.PrepararQuery(My.Resources.AltaCaracteristicasPorTipoSetor.ToString())

        For Each objCaracteristicas As ContractoServicio.TipoSetor.SetTiposSectores.Caracteristica In objPeticion.codCaractTipoSector

            comando = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = query
            comando.CommandType = CommandType.Text

            Dim oidCaractxTipoSetor As String = Guid.NewGuid.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CARACTTIPOSECTORXTIPOSEC", ProsegurDbType.Objeto_Id, oidCaractxTipoSetor))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SECTOR", ProsegurDbType.Descricao_Curta, objPeticion.codTipoSector))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CARACT_TIPOSECTOR", ProsegurDbType.Descricao_Curta, objCaracteristicas.codCaractTipoSector))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, codigoUsuario))

            objTransacao.AdicionarItemTransacao(comando)
        Next
    End Sub

#End Region

End Class
