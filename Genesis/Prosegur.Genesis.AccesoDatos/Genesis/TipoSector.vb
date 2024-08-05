Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Classe Tipo Sector
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TipoSector

#Region "Consultas"

        ''' <summary>
        ''' Recupera os tipos de sectores
        ''' </summary>
        ''' <param name="IdPlanta"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTiposSectores(IdPlanta As String) As ObservableCollection(Of Clases.TipoSector)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoSectorRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PLANTA", ProsegurDbType.Objeto_Id, IdPlanta))

            Dim TiposSectores As ObservableCollection(Of Clases.TipoSector) = Nothing

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                TiposSectores = New ObservableCollection(Of Clases.TipoSector)

                For Each dr In dt.Rows
                    TiposSectores.Add(Cargar(dr))
                Next

            End If

            Return TiposSectores
        End Function
        ''' <summary>
        ''' Recupera os tipos de sectores
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTiposSectores() As List(Of Clases.TipoSector)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoSectorRecuperarTodos)
            cmd.CommandType = CommandType.Text

            Dim TiposSectores As New List(Of Clases.TipoSector)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                TiposSectores = New List(Of Clases.TipoSector)

                For Each dr In dt.Rows
                    TiposSectores.Add(Cargar(dr))
                Next

            End If

            Return TiposSectores
        End Function
        ''' <summary>
        ''' Recupera os tipos de sectores de um formulário
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTiposSectores(identificadorFormulario As String, codigoRelacionConFormulario As String) As List(Of Clases.TipoSector)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerTiposSectoresFormulario)
            cmd.CommandText += " AND TSF.COD_RELACION_CON_FORMULARIO = :COD_RELACION_CON_FORMULARIO"
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RELACION_CON_FORMULARIO", ProsegurDbType.Objeto_Id, codigoRelacionConFormulario))

            Dim TiposSectores As New List(Of Clases.TipoSector)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                TiposSectores = New List(Of Clases.TipoSector)

                For Each dr In dt.Rows
                    TiposSectores.Add(Cargar(dr))
                Next

            End If

            Return TiposSectores
        End Function

        ''' <summary>
        ''' Método que insere os tipos de setores para um formulario
        ''' </summary>
        ''' <param name="identificadorFormulario">Oid Formulário</param>
        ''' <param name="relacionConFormulario">O para Origem, D para Destino</param>
        ''' <param name="tiposSectores">Tipos Sectores</param>
        ''' <remarks></remarks>
        Public Shared Sub GuardarTiposSectoresFormulario(identificadorFormulario As String, relacionConFormulario As String, tiposSectores As List(Of Clases.TipoSector))

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoSectoresInserirPorFormulario)
            cmd.CommandType = CommandType.Text

            For Each tipoSector In tiposSectores

                cmd.Parameters.Clear()

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_SECTORXFORMULARIO", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_SECTOR", ProsegurDbType.Objeto_Id, tipoSector.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MIGRACION", ProsegurDbType.Descricao_Longa, tipoSector.CodigoMigracion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, tipoSector.UsuarioCreacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, tipoSector.UsuarioModificacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RELACION_CON_FORMULARIO", ProsegurDbType.Descricao_Curta, relacionConFormulario))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            Next

        End Sub
        ''' <summary>
        ''' Método que exclui os tipos de setores de um formulario
        ''' </summary>
        ''' <param name="identificadorFormulario">Oid Formulário</param>
        ''' <remarks></remarks>
        Public Shared Sub BorrarTiposSectoresFormulario(identificadorFormulario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoSectorExcluirPorFormulario)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub


        ''' <summary>
        ''' Método que recupera os tipos de setores pelos seus respectivos codigos.
        ''' </summary>
        ''' <param name="codigosTiposSector">Codigos a serem pesquisados</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorCodigos(codigoDelegacion As String, codigoPlanta As String, ParamArray codigosTiposSector As String()) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.TipoSector)
            Dim tiposSector As New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.TipoSector)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerTiposSectoresPorCodigos
                Dim sqlWhere As New StringBuilder()

                If codigosTiposSector IsNot Nothing AndAlso (codigosTiposSector.Length > 0) Then
                    sqlWhere.AppendLine(Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, codigosTiposSector.ToList(), "COD_TIPO_SECTOR", cmd, "AND", , "D"))
                End If

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "pCOD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "pCOD_PLANTA", ProsegurDbType.Descricao_Curta, codigoPlanta))
                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource & sqlWhere.ToString())

                dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                For Each row As DataRow In dtDados.Rows
                    tiposSector.Add(Cargar(row))
                Next
            End Using

            Return tiposSector
        End Function

        ''' <summary>
        ''' Recupera o tipo sector por identificador.
        ''' </summary>
        ''' <param name="identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTipoSectorPorIdentificador(identificador As String) As Clases.TipoSector
            Dim tipoSector As Clases.TipoSector = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoSectorPorIdentificador)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_TIPO_SECTOR", ProsegurDbType.Objeto_Id, identificador))
            cmd.CommandType = CommandType.Text
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                tipoSector = (Cargar(dt.Rows(0)))
            End If

            Return tipoSector

        End Function

        ''' <summary>
        ''' Obtener el tiposector por lo identificador del sector
        ''' </summary>
        ''' <param name="identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTipoSectorPorIdentificadorSector(identificador As String) As Clases.TipoSector
            Dim tipoSector As Clases.TipoSector = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoSectorPorIdentificadorSector)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Objeto_Id, identificador))
            cmd.CommandType = CommandType.Text
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                tipoSector = (Cargar(dt.Rows(0)))
            End If

            Return tipoSector

        End Function

        Public Shared Function RecuperarTipoSectorPorCodigos(codigoDelegacion As String, codigoPlanta As String, codigoSector As String) As Clases.TipoSector

            Dim tipoSector As Clases.TipoSector = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoSectorPorCodigos)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Objeto_Id, codigoDelegacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Objeto_Id, codigoPlanta))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Objeto_Id, codigoSector))
            cmd.CommandType = CommandType.Text
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                tipoSector = (Cargar(dt.Rows(0)))
            End If

            Return tipoSector

        End Function


        Public Shared Function ObtenerPorIdentificadores(identificadoresDelegaciones As List(Of String), identificadoresPlantas As List(Of String)) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.TipoSector)
            Dim tiposSector As New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.TipoSector)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.TipoSectorPorDelegacionYPlanta
                Dim sqlWhere As New StringBuilder()

                If identificadoresDelegaciones IsNot Nothing AndAlso (identificadoresDelegaciones.Count > 0) Then
                    sqlWhere.AppendLine(Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, identificadoresDelegaciones, "OID_DELEGACION", cmd, "AND", "D"))
                End If

                If identificadoresPlantas IsNot Nothing AndAlso (identificadoresPlantas.Count > 0) Then
                    sqlWhere.AppendLine(Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, identificadoresPlantas, "OID_PLANTA", cmd, "AND", "P"))
                End If

                If sqlWhere.ToString.StartsWith(" AND ") Then
                    sqlWhere = sqlWhere.Remove(0, 5)
                    sqlWhere = sqlWhere.Insert(0, " WHERE ")
                End If

                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource & sqlWhere.ToString())

                dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                For Each row As DataRow In dtDados.Rows
                    tiposSector.Add(Cargar(row))
                Next
            End Using

            Return tiposSector
        End Function
#End Region

        Private Shared Function Cargar(dr As DataRow) As Clases.TipoSector
            Return New Clases.TipoSector With
            {
                .Codigo = Util.AtribuirValorObj(dr("COD_TIPO_SECTOR"), GetType(String)), _
                .CodigoMigracion = Util.AtribuirValorObj(dr("COD_MIGRACION"), GetType(String)), _
                .Descripcion = Util.AtribuirValorObj(dr("DES_TIPO_SECTOR"), GetType(String)), _
                .EstaActivo = Util.AtribuirValorObj(dr("BOL_ACTIVO"), GetType(Boolean)), _
                .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime)), _
                .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime)), _
                .Identificador = Util.AtribuirValorObj(dr("OID_TIPO_SECTOR"), GetType(String)), _
                .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String)), _
                .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String)),
                .CaracteristicasTipoSector = AccesoDatos.Genesis.CaracteristicaTipoSector.RecuperarCaracteristicasPorTipoSector(dr("OID_TIPO_SECTOR"))
            }
        End Function

    End Class

End Namespace