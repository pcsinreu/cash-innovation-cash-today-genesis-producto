Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.DbHelper

Namespace Genesis

    Public Class CargadorDropDown
        Public Shared Function ObtenerValoresDropDown(Peticion As Contractos.GenesisMovil.ObtenerValoresDropDown.Peticion) As Contractos.GenesisMovil.ObtenerValoresDropDown.Respuesta

            Dim respuesta As Contractos.GenesisMovil.ObtenerValoresDropDown.Respuesta = Nothing
            Using Comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                Dim dt As DataTable = Nothing

                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CARGAR_CLIENTE", ProsegurDbType.Inteiro_Curto, Peticion.CargarCliente))
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CARGAR_CANAL", ProsegurDbType.Inteiro_Curto, Peticion.CargarCanal))
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CARGAR_SUBCANAL", ProsegurDbType.Inteiro_Curto, Peticion.CargarSubCanal))
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CARGAR_DIVISA", ProsegurDbType.Inteiro_Curto, Peticion.CargarDivisa))
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CARGAR_DENOMINACION", ProsegurDbType.Inteiro_Curto, Peticion.CargarDenominacion))
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CARGAR_TIPO_CONTENEDOR", ProsegurDbType.Inteiro_Curto, Peticion.CargarTipoContenedor))
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CANAL", ProsegurDbType.Descricao_Longa, Peticion.IdentificadorCanal))
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Descricao_Longa, Peticion.IdentificadorDivisa))

                Comando.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, My.Resources.ObtenerItensDropDown)
                Comando.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, Comando)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    respuesta = New Contractos.GenesisMovil.ObtenerValoresDropDown.Respuesta()

                    For Each row In dt.Rows
                        Select Case Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("TIPO"), GetType(String))
                            Case "C"
                                LlenarIten(respuesta.ListaCliente, row)
                            Case "CA"
                                LlenarIten(respuesta.ListaCanal, row)
                            Case "SUBCA"
                                LlenarIten(respuesta.ListaSubCanal, row)
                            Case "DIV"
                                LlenarIten(respuesta.ListaDivisa, row)
                            Case "D"
                                LlenarIten(respuesta.ListaDenominacion, row)
                            Case "T"
                                LlenarIten(respuesta.ListaTipoContenedor, row)
                        End Select
                    Next

                End If
            End Using

            Return respuesta
        End Function

        Private Shared Sub LlenarIten(ByRef Lista As List(Of Contractos.GenesisMovil.ObtenerValoresDropDown.Iten), row As DataRow)
            Dim iten As Contractos.GenesisMovil.ObtenerValoresDropDown.Iten = New Contractos.GenesisMovil.ObtenerValoresDropDown.Iten()
            iten.Identificador = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
            iten.Codigo = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("CODIGO"), GetType(String))
            iten.Descricion = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("DESCRICION"), GetType(String))

            Lista.Add(iten)
        End Sub

    End Class

End Namespace



