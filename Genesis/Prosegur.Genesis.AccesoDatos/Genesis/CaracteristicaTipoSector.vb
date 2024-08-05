Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Namespace Genesis
    Public Class CaracteristicaTipoSector

#Region "Consultas"

        Public Shared Function obtenerCaracteristicasPorTipoSector(identificadorTipoSector As List(Of String)) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim filtro As String = ""

                If identificadorTipoSector IsNot Nothing Then
                    If identificadorTipoSector.Count = 1 Then
                        filtro &= " AND CTSTS.OID_TIPO_SECTOR =[]OID_TIPO_SECTOR "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_SECTOR", ProsegurDbType.Descricao_Curta, identificadorTipoSector(0)))
                    ElseIf identificadorTipoSector.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorTipoSector, "OID_TIPO_SECTOR", cmd, "AND", "CTSTS", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.CaracteristicaTipoSectorPorIdentificador, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function


        ''' <summary>
        ''' Recupera as características por tipo de sector.
        ''' </summary>
        ''' <param name="identificadorTipoSector"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarCaracteristicasPorTipoSector(identificadorTipoSector As String) As ObservableCollection(Of Enumeradores.CaracteristicaTipoSector)

            Dim filtro As String = ""
            Dim caracteristicas As New ObservableCollection(Of Enumeradores.CaracteristicaTipoSector)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            filtro &= " AND CTSTS.OID_TIPO_SECTOR =[]OID_TIPO_SECTOR "

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.CaracteristicaTipoSectorPorIdentificador, filtro))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_SECTOR", ProsegurDbType.Objeto_Id, identificadorTipoSector))
            cmd.CommandType = CommandType.Text
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    'Verifica se o Enum existe antes de add
                    'Quando voltava versão e havia alguma característica salva no BD que não existia no Enum dava erro, pois na versão que voltou não existia o Enum
                    If ExisteEnum(Of Enumeradores.CaracteristicaTipoSector)(row("COD_CARACT_TIPOSECTOR")) Then
                        caracteristicas.Add(RecuperarEnum(Of Enumeradores.CaracteristicaTipoSector)(row("COD_CARACT_TIPOSECTOR").ToString))
                    End If
                Next row

            End If

            Return caracteristicas

        End Function
#End Region

    End Class
End Namespace

