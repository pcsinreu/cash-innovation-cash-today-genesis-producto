Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos
    Public Class EstadoAccionContable

#Region "[CONSULTAS]"
        ''' <summary>
        ''' Recupera a EstadoaccionContable pelo identificador da accion contable.
        ''' </summary>
        ''' <param name="identificadorAccionContable"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [claudioniz.pereira] 05/09/2013 - Criado
        ''' </history>
        Public Shared Function ObtenerEstadosAccionContable(identificadorAccionContable As String) As List(Of Clases.EstadoAccionContable)

            Dim listaEstadoAccionContable As New List(Of Clases.EstadoAccionContable)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EstadoAccionContableRecuperarPorAccionContable)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ACCION_CONTABLE", ProsegurDbType.Objeto_Id, identificadorAccionContable))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Dim objEstadoAccionContable As New Clases.EstadoAccionContable
                    With objEstadoAccionContable
                        .Identificador = Util.AtribuirValorObj(row("OID_ESTADOXACCION_CONTABLE"), GetType(String))
                        .IdentificadorAccionContable = Util.AtribuirValorObj(row("OID_ACCION_CONTABLE"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_ESTADO"), GetType(String))
                        .OrigemDisponible = Util.AtribuirValorObj(row("COD_ACCION_ORIGEN_DISPONIBLE"), GetType(String))
                        .OrigemNoDisponible = Util.AtribuirValorObj(row("COD_ACCION_ORIGEN_NODISP"), GetType(String))
                        .DestinoDisponible = Util.AtribuirValorObj(row("COD_ACCION_DESTINO_DISPONIBLE"), GetType(String))
                        .DestinoNoDisponible = Util.AtribuirValorObj(row("COD_ACCION_DESTINO_NODISP"), GetType(String))
                        .DestinoDisponibleBloqueado = Util.AtribuirValorObj(row("COD_ACCION_DESTINO_DISPBLOQ"), GetType(String))
                        .OrigenDisponibleBloqueado = Util.AtribuirValorObj(row("COD_ACCION_ORIGEN_DISPBLOQ"), GetType(String))
                        .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))
                    End With

                    listaEstadoAccionContable.Add(objEstadoAccionContable)
                Next
            End If

            Return listaEstadoAccionContable
        End Function
#End Region

#Region "INSERTS"
        ''' <summary>
        ''' Insere uma novo estado ação contábil
        ''' </summary>
        ''' <param name="estadoAccionContable"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [victor.ramos] 15/01/2014 - Criado
        ''' </history>
        Public Shared Sub GuardarEstadoAccionContable(estadoAccionContable As Clases.EstadoAccionContable)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EstadoAccionContableInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ESTADOXACCION_CONTABLE", ProsegurDbType.Objeto_Id, estadoAccionContable.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ACCION_CONTABLE", ProsegurDbType.Objeto_Id, estadoAccionContable.IdentificadorAccionContable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, estadoAccionContable.Codigo))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ACCION_ORIGEN_DISPONIBLE", ProsegurDbType.Descricao_Curta, estadoAccionContable.OrigemDisponible))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ACCION_ORIGEN_NODISP", ProsegurDbType.Descricao_Curta, estadoAccionContable.OrigemNoDisponible))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ACCION_DESTINO_DISPONIBLE", ProsegurDbType.Descricao_Curta, estadoAccionContable.DestinoDisponible))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ACCION_DESTINO_NODISP", ProsegurDbType.Descricao_Curta, estadoAccionContable.DestinoNoDisponible))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ACCION_ORIGEN_DISPBLOQ", ProsegurDbType.Descricao_Curta, estadoAccionContable.OrigenDisponibleBloqueado))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ACCION_DESTINO_DISPBLOQ", ProsegurDbType.Descricao_Curta, estadoAccionContable.DestinoDisponibleBloqueado))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, estadoAccionContable.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, estadoAccionContable.UsuarioModificacion))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub
        ''' <summary>
        ''' Exclui os estados ação contábil de uma ação contábil
        ''' </summary>
        ''' <param name="identificadorAccionContable"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [victor.ramos] 20/01/2014 - Criado
        ''' </history>
        Public Shared Sub BorrarEstadoAccionContablePorAccionContable(identificadorAccionContable As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EstadoAccionContableBorrarPorAccionContable)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ACCION_CONTABLE", ProsegurDbType.Objeto_Id, identificadorAccionContable))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub
#End Region

    End Class
End Namespace



