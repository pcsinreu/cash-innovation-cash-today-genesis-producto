Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Genesis
    Public Class TipoFormato

        Shared Function ObtenerTipoServicioDeElementos(identificadoresRemesas As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ObtenerListaValorPorElemento_v2
                Dim filtro As String = ""

                If identificadoresRemesas IsNot Nothing Then
                    If identificadoresRemesas.Count = 1 Then
                        filtro &= " AND VE.OID_REMESA = []OID_REMESA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Descricao_Curta, identificadoresRemesas(0)))
                    ElseIf identificadoresRemesas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_REMESA", cmd, "AND", "VE", , False)
                    End If
                End If
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.TipoListaValor.TipoFormato.RecuperarValor()))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        ''' <summary>
        ''' Recupera todos los tipo de formato.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTiposFormato() As ObservableCollection(Of Clases.TipoFormato)
            Dim dt As DataTable = ListaValor.RecuperarPorTipo(Enumeradores.TipoListaValor.TipoFormato)
            Dim listaTiposFormato As New ObservableCollection(Of Clases.TipoFormato)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    listaTiposFormato.Add(PreencherTipoFormatoLinha(dr))
                Next
            End If
            Return listaTiposFormato
        End Function

        Public Shared Function RecuperarTiposFormatoComModulos(codigos As List(Of String)) As ObservableCollection(Of Clases.TipoFormato)
            Dim dt As DataTable = ListaValor.RecuperarPorTipoComModulos(Enumeradores.TipoListaValor.TipoFormato, codigos)
            Dim listaTiposFormato As New ObservableCollection(Of Clases.TipoFormato)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim tipoFormato = listaTiposFormato.FirstOrDefault(Function(t) t.Identificador = dr("OID_LISTA_VALOR"))
                    If tipoFormato Is Nothing Then
                        tipoFormato = New Clases.TipoFormato
                        tipoFormato.Identificador = Util.AtribuirValorObj(dr("OID_LISTA_VALOR"), GetType(String))
                        tipoFormato.Codigo = Util.AtribuirValorObj(dr("COD_VALOR"), GetType(String))
                        tipoFormato.Descripcion = Util.AtribuirValorObj(dr("DES_VALOR"), GetType(String))
                        tipoFormato.EsDefecto = Util.AtribuirValorObj(dr("BOL_DEFECTO"), GetType(Boolean))
                        If Convert.ToBoolean(dr("ES_MODULO")) = True Then
                            tipoFormato.Modulo = New Clases.Modulo With {.Identificador = Util.AtribuirValorObj(dr("OID_MODULO"), GetType(String))}
                        End If

                        listaTiposFormato.Add(tipoFormato)
                    End If
                    If tipoFormato.Modulo IsNot Nothing Then

                        If tipoFormato.Modulo.Divisas Is Nothing Then
                            tipoFormato.Modulo.Divisas = New ObservableCollection(Of Clases.Divisa)
                            tipoFormato.Modulo.Divisas.Add(New Clases.Divisa() With {.Identificador = dr("OID_DIVISA"), .CodigoISO = dr("COD_ISO_DIVISA")})
                        End If
                        If tipoFormato.Modulo.Divisas.First.Denominaciones Is Nothing Then
                            tipoFormato.Modulo.Divisas.First.Denominaciones = New ObservableCollection(Of Clases.Denominacion)
                        End If

                        Dim denom = New Clases.Denominacion() With {.Valor = dr("NUM_VALOR"), .Identificador = dr("OID_DENOMINACION"), .Codigo = dr("COD_DENOMINACION")}
                        Dim vlr As New Clases.ValorDenominacion With {.Cantidad = dr("NEL_UNIDADES")}
                        vlr.Importe = vlr.Cantidad * denom.Valor
                        denom.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)()
                        denom.ValorDenominacion.Add(vlr)
                        tipoFormato.Modulo.Divisas.First().Denominaciones.Add(denom)
                        tipoFormato.Modulo.Importe += denom.Valor * vlr.Cantidad
                    End If
                Next
            End If
            Return listaTiposFormato
        End Function

        Public Shared Function RecuperarTipoFormatoPorIdentificador(identificador As String) As Clases.TipoFormato
            Dim dt As DataTable = ListaValor.RecuperarPorIdentificador(identificador)
            Return PreencherTipoFormato(dt)
        End Function

        Public Shared Function RecuperarTipoFormatoPorCodigo(codigo As String) As Clases.TipoFormato
            Dim dt As DataTable = ListaValor.RecuperarPorCodigo(codigo, Prosegur.Genesis.Comon.Enumeradores.TipoListaValor.TipoFormato)
            Return PreencherTipoFormato(dt)
        End Function

        Public Shared Function ObtenerTipoFormatoPorCodigos(codigos As List(Of String)) As ObservableCollection(Of Clases.TipoFormato)

            Dim tipoFormatos As ObservableCollection(Of Clases.TipoFormato) = Nothing

            Dim dt As DataTable = ListaValor.RecuperarPorCodigos(codigos, Prosegur.Genesis.Comon.Enumeradores.TipoListaValor.TipoFormato)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                tipoFormatos = New ObservableCollection(Of Clases.TipoFormato)
                For Each _row In dt.Rows
                    If tipoFormatos.FirstOrDefault(Function(x) x.Codigo = Util.AtribuirValorObj(_row("COD_VALOR"), GetType(String))) Is Nothing Then
                        tipoFormatos.Add(PreencherTipoFormatoLinha(_row))
                    End If
                Next
            End If

            Return tipoFormatos

        End Function

        Public Shared Function RecuperarPorElemento(identificadorRemessa As String, identificadorBulto As String, identificadorParcial As String) As Clases.TipoFormato
            Dim dt As DataTable = ListaValor.RecuperarPorElemento(identificadorRemessa, identificadorBulto, identificadorParcial, Prosegur.Genesis.Comon.Enumeradores.TipoListaValor.TipoFormato)
            Return PreencherTipoFormato(dt)
        End Function

        ''' <summary>
        ''' Recupera los valores de un DataTable e retorna un tipoformato. Campos esperados del DataRow: OID_LISTA_VALOR, COD_VALOR y DES_VALOR
        ''' </summary>
        ''' <param name="datos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PreencherTipoFormato(datos As DataTable) As Clases.TipoFormato
            If datos IsNot Nothing AndAlso datos.Rows.Count = 1 Then
                Return PreencherTipoFormatoLinha(datos.Rows(0))
            End If
            Return Nothing
        End Function

        'Private Shared Function PreencherTipoFormatoModulo(datos As DataTable) As Clases.TipoFormato
        '    If datos IsNot Nothing AndAlso datos.Rows.Count = 1 Then
        '        Return PreencherTipoFormatoModuloLinha(datos.Rows(0))
        '    End If
        '    Return Nothing
        'End Function

        ''' <summary>
        ''' Recupera los valores de un DataRow e retorna un tipoformato. Campos esperados del DataRow: OID_LISTA_VALOR, COD_VALOR y DES_VALOR
        ''' </summary>
        ''' <param name="linha"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PreencherTipoFormatoLinha(linha As DataRow) As Clases.TipoFormato
            Dim tipoFormato As New Clases.TipoFormato()
            tipoFormato.Identificador = Util.AtribuirValorObj(linha("OID_LISTA_VALOR"), GetType(String))
            tipoFormato.Codigo = Util.AtribuirValorObj(linha("COD_VALOR"), GetType(String))
            tipoFormato.Descripcion = Util.AtribuirValorObj(linha("DES_VALOR"), GetType(String))
            tipoFormato.EsDefecto = Util.AtribuirValorObj(linha("BOL_DEFECTO"), GetType(Boolean))
            Return tipoFormato
        End Function

        

    End Class

End Namespace

