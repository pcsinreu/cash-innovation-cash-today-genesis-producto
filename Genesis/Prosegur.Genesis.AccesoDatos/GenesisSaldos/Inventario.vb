Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

Namespace GenesisSaldos

    Public Class Inventario
        Public Shared Function InventarioRecuperar(objPeticion As Prosegur.Genesis.Comon.Peticion(Of Clases.Transferencias.FiltroInventario), _
                                          ByRef objRespuesta As Prosegur.Genesis.Comon.Respuesta(Of List(Of Clases.Inventario)) _
                                         ) As List(Of Clases.Inventario)

            objRespuesta.ParametrosPaginacion = New Prosegur.Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion()
            Dim listaInventarios As New List(Of Clases.Inventario)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.InventarioRecuperar
            cmd.CommandType = CommandType.Text
            Dim strWhere As String = String.Empty

            'If objPeticion.Parametro.Sector Then
            If Not String.IsNullOrEmpty(objPeticion.Parametro.CodigoInventario) Then
                strWhere = " AND INV.COD_INVENTARIO =[]COD_INVENTARIO"
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_INVENTARIO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.Parametro.CodigoInventario))
            Else
                If objPeticion.Parametro.FechaDesde IsNot Nothing Then
                    strWhere += " AND INV.GMT_CREACION >=[]FECHA_DESDE"
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FECHA_DESDE", ProsegurDbType.Data_Hora, objPeticion.Parametro.FechaDesde))
                End If

                If objPeticion.Parametro.FechaHasta IsNot Nothing Then
                    strWhere += " AND INV.GMT_CREACION <=[]FECHA_HASTA"
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FECHA_HASTA", ProsegurDbType.Data_Hora, objPeticion.Parametro.FechaHasta))
                End If

                If objPeticion.Parametro.Sector IsNot Nothing Then
                    If Not String.IsNullOrEmpty(objPeticion.Parametro.Sector.Identificador) Then
                        strWhere += " AND SEC.OID_SECTOR =[]OID_SECTOR"
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, objPeticion.Parametro.Sector.Identificador))
                    Else
                        If objPeticion.Parametro.Sector.Planta IsNot Nothing Then
                            If Not String.IsNullOrEmpty(objPeticion.Parametro.Sector.Planta.Identificador) Then
                                strWhere += " AND PLA.OID_PLANTA =[]OID_PLANTA"
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PLANTA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.Parametro.Sector.Planta.Identificador))
                            End If
                        Else
                            If objPeticion.Parametro.Sector.Delegacion IsNot Nothing Then
                                If Not String.IsNullOrEmpty(objPeticion.Parametro.Sector.Delegacion.Identificador) Then
                                    strWhere += " AND PLA.OID_DELEGACION =[]OID_DELEGACION"
                                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.Parametro.Sector.Delegacion.Identificador))
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            If strWhere.Length > 0 Then
                strWhere = " WHERE" + strWhere.Substring(4)
            End If

            cmd.CommandText += strWhere
            cmd.CommandText += " ORDER BY INV.GMT_CREACION DESC"

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
            Dim DT As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, cmd, objPeticion.ParametrosPaginacion, objRespuesta.ParametrosPaginacion)

            If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
                For Each row In DT.Rows
                    Dim objInventario As New Clases.Inventario
                    With objInventario
                        .Identificador = Util.AtribuirValorObj(row("OID_INVENTARIO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_INVENTARIO"), GetType(String))
                        .Sector = New Clases.Sector()
                        .Sector.Descripcion = Util.AtribuirValorObj(row("DES_SECTOR"), GetType(String))

                        .Sector.Delegacion = AccesoDatos.Genesis.Delegacion.ObtenerPorOid(Util.AtribuirValorObj(row("OID_DELEGACION"), GetType(String)))
                        .Sector.Planta = New Clases.Planta()
                        .Sector.Planta.Descripcion = Util.AtribuirValorObj(row("DES_PLANTA"), GetType(String))

                        .FechaCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                    End With

                    listaInventarios.Add(objInventario)
                Next
            End If

            objRespuesta.Retorno = listaInventarios

            Return listaInventarios

        End Function

    End Class
End Namespace


