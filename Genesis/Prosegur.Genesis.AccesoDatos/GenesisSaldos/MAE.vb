Imports Prosegur.DbHelper

Namespace GenesisSaldos
    Public Class MAE

#Region "Dasboard"

        Public Shared Function RetornaSaldoTodasMAEPorDelegacion(codigoDelegacao As List(Of String), canalesValoresValidadosMAE As List(Of String), canalesValoresDepositadosMAE As List(Of String),
                                                                 identificadorDivisa As List(Of String), codigoSector As List(Of String)) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim filtroDelegacion As String = String.Empty
            Dim filtroIdentificadoresDivisa As String = String.Empty
            Dim filtroCodigosSectores As String = String.Empty
            Dim filtroCanalesValoresValidadosMAE As String = String.Empty
            Dim filtroCanalesValoresDepositadosMAE As String = String.Empty

            comando.CommandText = My.Resources.RetornaSaldoTodasMAEPorDelegacion
            comando.CommandType = CommandType.Text

            If (codigoDelegacao IsNot Nothing AndAlso codigoDelegacao.Count > 0) Then
                If (codigoDelegacao.Count = 1) Then
                    filtroDelegacion = " AND DELE.COD_DELEGACION = []COD_DELEGACION"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacao(0)))
                Else
                    filtroDelegacion = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoDelegacao, "COD_DELEGACION", comando, "AND", "DELE")
                End If
            End If

            If (identificadorDivisa IsNot Nothing AndAlso identificadorDivisa.Count > 0) Then
                filtroIdentificadoresDivisa = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorDivisa, "OID_DIVISA", comando, "AND", "DIVI")
            End If

            If (codigoSector IsNot Nothing AndAlso codigoSector.Count > 0) Then
                filtroCodigosSectores &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoSector, "COD_SECTOR", comando, "AND", "SECT")
            End If

            If (canalesValoresValidadosMAE IsNot Nothing AndAlso canalesValoresValidadosMAE.Count > 0) Then
                filtroCanalesValoresValidadosMAE &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, canalesValoresValidadosMAE, "COD_CANAL", comando, "AND", "CANA", "VAL")
            End If

            If (canalesValoresDepositadosMAE IsNot Nothing AndAlso canalesValoresDepositadosMAE.Count > 0) Then
                filtroCanalesValoresDepositadosMAE &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, canalesValoresDepositadosMAE, "COD_CANAL", comando, "AND", "CANA", "DEP")
            End If

            comando.CommandText = String.Format(comando.CommandText,
                                                filtroDelegacion,
                                                filtroIdentificadoresDivisa,
                                                filtroCodigosSectores,
                                                filtroCanalesValoresValidadosMAE,
                                                filtroCanalesValoresDepositadosMAE)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            Return dt
        End Function

        Public Shared Function RetornaCantidadMAESPorClientes(codigoDelegacao As List(Of String), tiposSectoresMAE As List(Of String)) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As String = ""

            comando.CommandText = My.Resources.RetornaCantidadMAESPorClientes
            comando.CommandType = CommandType.Text

            If (codigoDelegacao IsNot Nothing AndAlso codigoDelegacao.Count > 0) Then
                If (codigoDelegacao.Count = 1) Then
                    query = " AND DELE.COD_DELEGACION = []COD_DELEGACION"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacao(0)))
                Else
                    query = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoDelegacao, "COD_DELEGACION", comando, "AND", "DELE")
                End If
            End If

            If (tiposSectoresMAE IsNot Nothing AndAlso tiposSectoresMAE.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, tiposSectoresMAE, "COD_TIPO_SECTOR", comando, "AND", "TISE")
            End If

            comando.CommandText = String.Format(comando.CommandText, query)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            Return dt
        End Function

        Public Shared Function RetornaSaldoMAEPorCliente(codigoDelegacao As List(Of String), canalesValoresValidadosMAE As List(Of String), canalesValoresDepositadosMAE As List(Of String),
                                                         identificadorDivisa As List(Of String), codigoSector As List(Of String)) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim filtroDelegacion As String = String.Empty
            Dim filtroIdentificadoresDivisa As String = String.Empty
            Dim filtroCodigosSectores As String = String.Empty
            Dim filtroCanalesValoresValidadosMAE As String = String.Empty
            Dim filtroCanalesValoresDepositadosMAE As String = String.Empty

            comando.CommandText = My.Resources.RetornaSaldoMAEPorCliente
            comando.CommandType = CommandType.Text

            If (codigoDelegacao IsNot Nothing AndAlso codigoDelegacao.Count > 0) Then
                If (codigoDelegacao.Count = 1) Then
                    filtroDelegacion = " AND DELE.COD_DELEGACION = []COD_DELEGACION"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacao(0)))
                Else
                    filtroDelegacion = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoDelegacao, "COD_DELEGACION", comando, "AND", "DELE")
                End If
            End If

            If (identificadorDivisa IsNot Nothing AndAlso identificadorDivisa.Count > 0) Then
                filtroIdentificadoresDivisa = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorDivisa, "OID_DIVISA", comando, "AND", "DIVI")
            End If

            If (codigoSector IsNot Nothing AndAlso codigoSector.Count > 0) Then
                filtroCodigosSectores &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoSector, "COD_SECTOR", comando, "AND", "SECT")
            End If

            If (canalesValoresValidadosMAE IsNot Nothing AndAlso canalesValoresValidadosMAE.Count > 0) Then
                filtroCanalesValoresValidadosMAE &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, canalesValoresValidadosMAE, "COD_CANAL", comando, "AND", "CANA", "VAL")
            End If

            If (canalesValoresDepositadosMAE IsNot Nothing AndAlso canalesValoresDepositadosMAE.Count > 0) Then
                filtroCanalesValoresDepositadosMAE &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, canalesValoresDepositadosMAE, "COD_CANAL", comando, "AND", "CANA", "DEP")
            End If

            comando.CommandText = String.Format(comando.CommandText,
                                                filtroDelegacion,
                                                filtroIdentificadoresDivisa,
                                                filtroCodigosSectores,
                                                filtroCanalesValoresValidadosMAE,
                                                filtroCanalesValoresDepositadosMAE)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            Return dt
        End Function

        Public Shared Function RetornaSaldoClienteMAEDetallado(codigoDelegacao As List(Of String), tiposSectoresMAE As List(Of String),
                                                               canalesValoresValidadosMAE As List(Of String), canalesValoresDepositadosMAE As List(Of String),
                                                               identificadorDivisa As List(Of String), codigosFormulariosShipOutMAE As List(Of String), codigoSector As List(Of String)) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim filtroDelegacion As String = String.Empty
            Dim filtroIdentificadoresDivisa As String = String.Empty
            Dim filtroCodigosSectores As String = String.Empty
            Dim filtroCanalesValoresValidadosMAE As String = String.Empty
            Dim filtroCanalesValoresDepositadosMAE As String = String.Empty
            Dim filtroTiposSectoresMAE As String = String.Empty
            Dim filtroCodigosFormulariosShipOutMAE As String = String.Empty

            comando.CommandText = My.Resources.RetornaSaldoClienteMAEDetallado
            comando.CommandType = CommandType.Text

            If (codigoDelegacao IsNot Nothing AndAlso codigoDelegacao.Count > 0) Then
                If (codigoDelegacao.Count = 1) Then
                    filtroDelegacion = " AND DELE.COD_DELEGACION = []COD_DELEGACION"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacao(0)))
                Else
                    filtroDelegacion = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoDelegacao, "COD_DELEGACION", comando, "AND", "DELE")
                End If
            End If

            If (identificadorDivisa IsNot Nothing AndAlso identificadorDivisa.Count > 0) Then
                filtroIdentificadoresDivisa = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorDivisa, "OID_DIVISA", comando, "AND", "DIVI")
            End If

            If (codigoSector IsNot Nothing AndAlso codigoSector.Count > 0) Then
                filtroCodigosSectores &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoSector, "COD_SECTOR", comando, "AND", "SECT")
            End If

            If (tiposSectoresMAE IsNot Nothing AndAlso tiposSectoresMAE.Count > 0) Then
                filtroTiposSectoresMAE &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, tiposSectoresMAE, "COD_TIPO_SECTOR", comando, "AND", "TISE")
            End If

            If (canalesValoresValidadosMAE IsNot Nothing AndAlso canalesValoresValidadosMAE.Count > 0) Then
                filtroCanalesValoresValidadosMAE &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, canalesValoresValidadosMAE, "COD_CANAL", comando, "AND", "CANA", "VAL")
            End If

            If (canalesValoresDepositadosMAE IsNot Nothing AndAlso canalesValoresDepositadosMAE.Count > 0) Then
                filtroCanalesValoresDepositadosMAE &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, canalesValoresDepositadosMAE, "COD_CANAL", comando, "AND", "CANA", "DEP")
            End If

            If (codigosFormulariosShipOutMAE IsNot Nothing AndAlso codigosFormulariosShipOutMAE.Count > 0) Then
                filtroCodigosFormulariosShipOutMAE &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosFormulariosShipOutMAE, "COD_FORMULARIO", comando, "AND", "FORM")
            End If

            comando.CommandText = String.Format(comando.CommandText,
                                                filtroDelegacion,
                                                filtroIdentificadoresDivisa,
                                                filtroCodigosSectores,
                                                filtroTiposSectoresMAE,
                                                filtroCanalesValoresValidadosMAE,
                                                filtroCanalesValoresDepositadosMAE,
                                                filtroCodigosFormulariosShipOutMAE)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            Return dt
        End Function
#End Region

    End Class
End Namespace
