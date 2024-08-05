Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas.Remesa
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon

Namespace NuevoSalidas

    Public Class Modulo

        Public Shared Function RecuperarModulosRemesas(IdentificadoresRemesas As List(Of String)) As Dictionary(Of String, ObservableCollection(Of Clases.Modulo))

            If IdentificadoresRemesas Is Nothing OrElse IdentificadoresRemesas.Count = 0 Then
                Return Nothing
            End If

            Dim objModulosRemesas As Dictionary(Of String, ObservableCollection(Of Clases.Modulo)) = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Modulo_Recuperar_Valores_Remesas
            cmd.CommandType = CommandType.Text

            Dim Query As New System.Text.StringBuilder

            Query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresRemesas, "OID_REMESA", cmd, "WHERE", "ML"))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, Query))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objModulosRemesas = New Dictionary(Of String, ObservableCollection(Of Clases.Modulo))

                Dim objModulo As Clases.Modulo = Nothing
                Dim objDivisa As Clases.Divisa = Nothing
                Dim objDenominacion As Clases.Denominacion = Nothing

                Dim CodigoModulo As String = String.Empty
                Dim IdentificadorDivisa As String = String.Empty
                Dim IdentificadorDenominacion As String = String.Empty
                Dim IdentificadorRemesa As String = String.Empty
                Dim objModulosRemesa As KeyValuePair(Of String, ObservableCollection(Of Clases.Modulo))

                For Each dr In dt.Rows

                    CodigoModulo = Util.AtribuirValorObj(dr("COD_MODULO"), GetType(String))
                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                    IdentificadorDenominacion = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))
                    IdentificadorRemesa = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String))

                    objModulosRemesa = (From objRemMod In objModulosRemesas Where objRemMod.Key = IdentificadorRemesa).FirstOrDefault

                    If String.IsNullOrEmpty(objModulosRemesa.Key) Then

                        objModulosRemesas.Add(IdentificadorRemesa, New ObservableCollection(Of Clases.Modulo))

                        objModulosRemesa = (From objRemMod In objModulosRemesas Where objRemMod.Key = IdentificadorRemesa).FirstOrDefault

                    End If

                    objModulo = objModulosRemesa.Value.Find(Function(m) m.CodigoTipoModulo = CodigoModulo)

                    If objModulo Is Nothing Then

                        objModulosRemesa.Value.Add(New Clases.Modulo With { _
                                                   .Cantidad = Util.AtribuirValorObj(dr("CANTIDAD_MODULO"), GetType(Integer)), _
                                                   .CodigoTipoModulo = CodigoModulo, _
                                                   .DescripcionTipoModulo = Util.AtribuirValorObj(dr("DES_MODULO"), GetType(String)), _
                                                   .Divisas = New ObservableCollection(Of Clases.Divisa)})

                        objModulo = objModulosRemesa.Value.Find(Function(m) m.CodigoTipoModulo = CodigoModulo)

                    End If

                    objDivisa = objModulo.Divisas.Find(Function(d) d.Identificador = IdentificadorDivisa)

                    If objDivisa Is Nothing Then

                        objModulo.Divisas.Add(New Clases.Divisa With { _
                                              .Identificador = IdentificadorDivisa, _
                                              .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                              .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                              .Denominaciones = New ObservableCollection(Of Clases.Denominacion), _
                                              .ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)})

                        objDivisa = objModulo.Divisas.Find(Function(d) d.Identificador = IdentificadorDivisa)

                    End If

                    objDenominacion = objDivisa.Denominaciones.Find(Function(d) d.Identificador = IdentificadorDenominacion)

                    If objDenominacion Is Nothing Then

                        objDivisa.Denominaciones.Add(New Clases.Denominacion With { _
                                             .Identificador = IdentificadorDenominacion, _
                                             .Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)), _
                                             .Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String)), _
                                             .Valor = Util.AtribuirValorObj(dr("NUM_VALOR_FACIAL"), GetType(Decimal)), _
                                             .EsBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Boolean)), _
                                             .ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)})

                        objDenominacion = objDivisa.Denominaciones.Find(Function(d) d.Identificador = IdentificadorDenominacion)

                    End If

                    objDenominacion.ValorDenominacion.Add(New Clases.ValorDenominacion With {
                                                          .Cantidad = Util.AtribuirValorObj(dr("CANTIDAD_DENOMINACION"), GetType(Integer)),
                                                          .TipoValor = TipoValor.NoDefinido,
                                                          .Importe = (.Cantidad * objDenominacion.Valor)})

                    If objDivisa.ValoresTotalesEfectivo.Count > 0 Then
                        objDivisa.ValoresTotalesEfectivo.First.Importe += objDenominacion.ValorDenominacion.Last.Importe
                    Else
                        objDivisa.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo With { _
                                                             .Importe = objDenominacion.ValorDenominacion.Last.Importe,
                                                             .TipoValor = TipoValor.NoDefinido})
                    End If

                Next

            End If

            Return objModulosRemesas

        End Function

        Public Shared Function RecuperarModulosRemesa(IdentificadorRemesa As String) As ObservableCollection(Of Clases.Modulo)

            Dim objModulos As ObservableCollection(Of Clases.Modulo) = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Modulo_Recuperar_Valores)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objModulos = New ObservableCollection(Of Clases.Modulo)

                Dim objModulo As Clases.Modulo = Nothing
                Dim objDivisa As Clases.Divisa = Nothing
                Dim objDenominacion As Clases.Denominacion = Nothing

                Dim CodigoModulo As String = String.Empty
                Dim IdentificadorDivisa As String = String.Empty
                Dim IdentificadorDenominacion As String = String.Empty

                For Each dr In dt.Rows

                    CodigoModulo = Util.AtribuirValorObj(dr("COD_MODULO"), GetType(String))
                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                    IdentificadorDenominacion = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))

                    objModulo = objModulos.Find(Function(m) m.CodigoTipoModulo = CodigoModulo)

                    If objModulo Is Nothing Then

                        objModulos.Add(New Clases.Modulo With { _
                                       .Cantidad = Util.AtribuirValorObj(dr("CANTIDAD_MODULO"), GetType(Integer)), _
                                       .CodigoTipoModulo = CodigoModulo, _
                                       .DescripcionTipoModulo = Util.AtribuirValorObj(dr("DES_MODULO"), GetType(String)), _
                                       .Divisas = New ObservableCollection(Of Clases.Divisa)})

                        objModulo = objModulos.Find(Function(m) m.CodigoTipoModulo = CodigoModulo)

                    End If

                    objDivisa = objModulo.Divisas.Find(Function(d) d.Identificador = IdentificadorDivisa)

                    If objDivisa Is Nothing Then

                        objModulo.Divisas.Add(New Clases.Divisa With { _
                                              .Identificador = IdentificadorDivisa, _
                                              .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                              .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                              .Denominaciones = New ObservableCollection(Of Clases.Denominacion), _
                                              .ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)})

                        objDivisa = objModulo.Divisas.Find(Function(d) d.Identificador = IdentificadorDivisa)

                    End If

                    objDenominacion = objDivisa.Denominaciones.Find(Function(d) d.Identificador = IdentificadorDenominacion)

                    If objDenominacion Is Nothing Then

                        objDivisa.Denominaciones.Add(New Clases.Denominacion With { _
                                             .Identificador = IdentificadorDenominacion, _
                                             .Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)), _
                                             .Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String)), _
                                             .Valor = Util.AtribuirValorObj(dr("NUM_VALOR_FACIAL"), GetType(Decimal)), _
                                             .EsBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Boolean)), _
                                             .ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)})

                        objDenominacion = objDivisa.Denominaciones.Find(Function(d) d.Identificador = IdentificadorDenominacion)

                    End If

                    objDenominacion.ValorDenominacion.Add(New Clases.ValorDenominacion With {
                                                          .Cantidad = Util.AtribuirValorObj(dr("CANTIDAD_DENOMINACION"), GetType(Integer)),
                                                          .TipoValor = TipoValor.NoDefinido,
                                                          .Importe = (.Cantidad * objDenominacion.Valor)})

                    If objDivisa.ValoresTotalesEfectivo.Count > 0 Then
                        objDivisa.ValoresTotalesEfectivo.First.Importe += objDenominacion.ValorDenominacion.Last.Importe
                    Else
                        objDivisa.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo With { _
                                                             .Importe = objDenominacion.ValorDenominacion.Last.Importe,
                                                             .TipoValor = TipoValor.NoDefinido})
                    End If

                Next

            End If

            Return objModulos

        End Function

        ''' <summary>
        ''' Recupera o modulo do bulto
        ''' </summary>
        ''' <param name="IdentificadorBulto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarModuloBulto(IdentificadorBulto As String) As Clases.Modulo

            Dim objModulo As Clases.Modulo = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Modulo_Recuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objModulo = New Clases.Modulo With {.CodigoTipoModulo = Util.AtribuirValorObj(dt.Rows(0)("COD_MODULO"), GetType(String)), _
                                                    .DescripcionTipoModulo = Util.AtribuirValorObj(dt.Rows(0)("DES_MODULO"), GetType(String)), _
                                                    .Divisas = Divisa.RecuperarDivisasBultoOModulo(Util.AtribuirValorObj(dt.Rows(0)("OID_MODULO_BULTO"), GetType(String)), True)}


            End If

            Return objModulo

        End Function


        ''' <summary>
        ''' Método que insere um modulo bulto no banco de dados
        ''' </summary>
        ''' <param name="OidModuloBulto"></param>
        ''' <param name="OidBulto"></param>
        ''' <param name="OidTipoModulo"></param>
        ''' <param name="CodUsuario"></param>
        ''' <history>[cbomtempo]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub InsertarModuloBulto(OidModuloBulto As String, _
                                              OidBulto As String, _
                                              OidTipoModulo As String, _
                                              CodUsuario As String)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Modulo_InsertarModuloBulto)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_MODULO_BULTO", ProsegurDbType.Objeto_Id, OidModuloBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, OidBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_TIPO_MODULO", ProsegurDbType.Objeto_Id, OidTipoModulo))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
            End With

            'Apaga a remesa do banco
            DbHelper.AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        ''' <summary>
        ''' Método que insere um modulo remessa no banco de dados
        ''' </summary>
        ''' <param name="OidModuloRemesa"></param>
        ''' <param name="OidRemesa"></param>
        ''' <param name="OidTipoModulo"></param>
        ''' <param name="CodUsuario"></param>
        ''' <history>[maoliveira]	04/06/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub InsertarModuloRemesa(OidModuloRemesa As String, _
                                              OidRemesa As String, _
                                              OidTipoModulo As String, _
                                              CodUsuario As String)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                '.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Modulo_InsertarModuloRemesa)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_MODULO_Remesa", ProsegurDbType.Objeto_Id, OidModuloRemesa))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_Remesa", ProsegurDbType.Objeto_Id, OidRemesa))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_TIPO_MODULO", ProsegurDbType.Objeto_Id, OidTipoModulo))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
            End With

            'Apaga a remesa do banco
            DbHelper.AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        ''' <summary>
        ''' Método que deleta todos os módulos de um bulto
        ''' </summary>
        ''' <param name="oidBulto"></param>
        ''' <history>[cbomtempo]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub BorrarModulosBulto(oidBulto As String)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Modulo_BorrarModulosBulto)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, oidBulto))
            End With

            'Apaga a remesa do banco
            DbHelper.AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

    End Class

End Namespace