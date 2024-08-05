Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario
Imports System.Transactions
Imports Prosegur.Genesis.DataBaseHelper

Namespace Genesis
    Public Class Utilidad

        Public Shared Function ObtenerDescripcionesFiltroExtracion(peticion As ContractoServicio.Contractos.GenesisMovil.ObtenerDescripcionesFiltroExtracion.Peticion) As ContractoServicio.Contractos.GenesisMovil.ObtenerDescripcionesFiltroExtracion.Respuesta

            Dim respuesta As New ContractoServicio.Contractos.GenesisMovil.ObtenerDescripcionesFiltroExtracion.Respuesta

            Using Comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                Dim dt As DataTable = Nothing

                If String.IsNullOrEmpty(peticion.CodigoCliente) Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, ""))
                Else
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoCliente))
                End If

                If String.IsNullOrEmpty(peticion.CodigoCanal) Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, ""))
                Else
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoCanal))
                End If

                If String.IsNullOrEmpty(peticion.CodigoSubCanal) Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, ""))
                Else
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoSubCanal))
                End If

                If String.IsNullOrEmpty(peticion.CodigoDivisa) Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, ""))
                Else
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoDivisa))
                End If

                If String.IsNullOrEmpty(peticion.CodigoDenominacion) Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DENOMINACION", ProsegurDbType.Identificador_Alfanumerico, ""))
                Else
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DENOMINACION", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoDenominacion))
                End If

                If String.IsNullOrEmpty(peticion.CodigoTipoContenedor) Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_CONTENEDOR", ProsegurDbType.Identificador_Alfanumerico, ""))
                Else
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_CONTENEDOR", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoTipoContenedor))
                End If

                If String.IsNullOrEmpty(peticion.CodigoDelegacion) Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, ""))
                Else
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoDelegacion))
                End If

                If String.IsNullOrEmpty(peticion.CodigoPlanta) Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, ""))
                Else
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPlanta))
                End If

                If String.IsNullOrEmpty(peticion.CodigoSector) Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, ""))
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA2", ProsegurDbType.Identificador_Alfanumerico, ""))
                Else
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoSector))
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA2", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPlanta))
                End If

                Comando.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, My.Resources.ObtenerDescripcionesFiltroExtracion)
                Comando.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, Comando)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    For Each row In dt.Rows
                        If Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("TIPO"), GetType(String)) = "CLI" Then
                            respuesta.IdentificadorCliente = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                            respuesta.DescripcionCliente = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                        ElseIf Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("TIPO"), GetType(String)) = "CAN" Then
                            respuesta.IdentificadorCanal = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                            respuesta.DescripcionCanal = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                        ElseIf Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("TIPO"), GetType(String)) = "SCN" Then
                            respuesta.IdentificadorSubCanal = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                            respuesta.DescripcionSubCanal = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                        ElseIf Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("TIPO"), GetType(String)) = "DIV" Then
                            respuesta.IdentificadorDivisa = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                            respuesta.DescripcionDivisa = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                        ElseIf Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("TIPO"), GetType(String)) = "DEN" Then
                            respuesta.IdentificadorDenominacion = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                            respuesta.DescripcionDenominacion = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                            If Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("NUM_VALOR"), GetType(Decimal)) > 0 Then
                                respuesta.NumValor = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("NUM_VALOR"), GetType(Decimal))
                            End If
                        ElseIf Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("TIPO"), GetType(String)) = "TCON" Then
                            respuesta.IdentificadorTipoContenedor = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                            respuesta.DescripcionTipoContenedor = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                            If Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("UNIDAD_MEDIDA"), GetType(Decimal)) > 0 Then
                                respuesta.UnidadMedida = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("UNIDAD_MEDIDA"), GetType(Decimal))
                            End If
                        ElseIf Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("TIPO"), GetType(String)) = "DEL" Then
                            respuesta.IdentificadorDelegacion = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                            respuesta.DescripcionDelegacion = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                        ElseIf Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("TIPO"), GetType(String)) = "PLA" Then
                            respuesta.IdentificadorPlanta = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                            respuesta.DescripcionPlanta = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                        ElseIf Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("TIPO"), GetType(String)) = "SEC" Then
                            respuesta.IdentificadorSector = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                            respuesta.DescripcionSector = Prosegur.Genesis.Comon.Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))

                        End If
                    Next
                End If

            End Using

            Return respuesta
        End Function

    End Class
End Namespace
