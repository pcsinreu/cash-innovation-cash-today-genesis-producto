Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace NuevoSalidas

    Public Class TipoBulto

        Public Shared Function ObtenerTipoBulto(IdentificadorTipoBulto As String) As Clases.TipoBulto

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_TipoBulto_Obtener)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_TIPO_BULTO", ProsegurDbType.Objeto_Id, IdentificadorTipoBulto))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
            Dim TipoBulto As Clases.TipoBulto = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                TipoBulto = New Clases.TipoBulto

                Util.AtribuirValorObjeto(TipoBulto.Identificador, dt.Rows(0)("OID_TIPO_BULTO"), GetType(String))
                Util.AtribuirValorObjeto(TipoBulto.Codigo, dt.Rows(0)("COD_TIPO_BULTO"), GetType(String))
                Util.AtribuirValorObjeto(TipoBulto.Descripcion, dt.Rows(0)("DES_TIPO_BULTO"), GetType(String))

            End If

            Return TipoBulto
        End Function

        Public Shared Function ObtenerTiposBultoDisponibles(CodigoDelegacion As String) As ObservableCollection(Of Clases.TipoBulto)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            comando.CommandText = My.Resources.Salidas_TipoBulto_ObtenerTiposBultoDisponibles

            If Not String.IsNullOrEmpty(CodigoDelegacion) Then

                comando.CommandText = String.Format(comando.CommandText, " WHERE TBD.COD_DELEGACION = []COD_DELEGACION")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))

            Else
                comando.CommandText = String.Format(comando.CommandText, String.Empty)

            End If

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim TiposBultoDisponibles As New ObservableCollection(Of Clases.TipoBulto)

                For Each row In dt.Rows
                    TiposBultoDisponibles.Add(CargarTiposBultoDisponibles(row))
                Next row

                Return TiposBultoDisponibles

            End If

            Return Nothing

        End Function

        Public Shared Function ObtenerTiposBulto() As List(Of Clases.TipoBulto)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            comando.CommandText = My.Resources.Salidas_TipoBulto_ObtenerTiposBulto

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim TiposBulto As New List(Of Clases.TipoBulto)

                For Each row In dt.Rows
                    TiposBulto.Add(CargarTiposBulto(row))
                Next row

                Return TiposBulto

            End If

            Return Nothing

        End Function

        Private Shared Function CargarTiposBulto(row As DataRow) As Clases.TipoBulto

            Dim TipoBulto As New Clases.TipoBulto

            Util.AtribuirValorObjeto(TipoBulto.Identificador, row("OID_TIPO_BULTO"), GetType(String))
            Util.AtribuirValorObjeto(TipoBulto.Codigo, row("COD_TIPO_BULTO"), GetType(String))
            Util.AtribuirValorObjeto(TipoBulto.Descripcion, row("DES_TIPO_BULTO"), GetType(String))
            Util.AtribuirValorObjeto(TipoBulto.NumImporteMaxSeguro, row("NUM_IMPORTE_MAXIMO_SEGURO"), GetType(Decimal))
            Util.AtribuirValorObjeto(TipoBulto.EsAptoPicos, row("BOL_APTO_PICOS"), GetType(Int16))
            Util.AtribuirValorObjeto(TipoBulto.EsAptoMezcla, row("BOL_APTO_MEZCLA"), GetType(Int16))
            Util.AtribuirValorObjeto(TipoBulto.UsuarioCreacion, row("COD_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(TipoBulto.FechaHoraModificacion, row("FYH_ACTUALIZACION"), GetType(DateTime))
            Util.AtribuirValorObjeto(TipoBulto.EsCajetin, row("BOL_CAJETIN"), GetType(Int16))
            Util.AtribuirValorObjeto(TipoBulto.CodigoFormato, row("COD_FORMATO"), GetType(String))

            Return TipoBulto

        End Function

        Private Shared Function CargarTiposBultoDisponibles(row As DataRow) As Clases.TipoBulto

            Dim TipoBulto As New Clases.TipoBulto

            Util.AtribuirValorObjeto(TipoBulto.Identificador, row("OID_TIPO_BULTO"), GetType(String))
            Util.AtribuirValorObjeto(TipoBulto.Codigo, row("COD_TIPO_BULTO"), GetType(String))
            Util.AtribuirValorObjeto(TipoBulto.Descripcion, row("DES_TIPO_BULTO"), GetType(String))
            Util.AtribuirValorObjeto(TipoBulto.CodigoFormato, row("COD_FORMATO"), GetType(String))
            Util.AtribuirValorObjeto(TipoBulto.EsCajetin, row("BOL_CAJETIN"), GetType(Int16))
            Util.AtribuirValorObjeto(TipoBulto.NecPrioridad, row("NEC_PRIORIDAD"), GetType(Int16))

            Return TipoBulto

        End Function

        ''' <summary>
        ''' Função que retorna vários OidTipoBulto conforme os códigos de bultos passados.
        ''' </summary>
        ''' <param name="codTipoBultos"></param>
        ''' <returns></returns>
        ''' <history>[cbomtempo] 29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Function RecuperarOidsTipoBulto(codTipoBultos As String) As DataTable

            'Se o parâmetro estiver vazio atribui '' para que a consulta seja executada sem erros
            If String.IsNullOrEmpty(codTipoBultos) Then codTipoBultos = "' '"

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = String.Format(Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_TipoBulto_RecuperarOidsTipoBulto), codTipoBultos)
            End With
            'executa o sql e retorna um valor
            Return DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
        End Function

        ''' <summary>
        ''' Função que recupera os tipos de bultos disponíveis configurados para 
        ''' uma delegação ao dividir os bultos.
        ''' </summary>
        ''' <param name="codDelegacion"></param>
        ''' <param name="objDivisas"></param>
        ''' <returns></returns>
        ''' <history>[Claudioniz]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTiposBultosDisponibles_DividirEnBultos(codDelegacion As String, _
                                                                               objDivisas As ObservableCollection(Of Clases.Divisa)) As List(Of Clases.TipoBulto)
            Dim listaTipoBulto As New List(Of Clases.TipoBulto)
            Dim CodigosDenominaciones As String = String.Empty
            For Each Divisa In objDivisas
                If Divisa.Denominaciones IsNot Nothing AndAlso Divisa.Denominaciones.Count > 0 Then
                    CodigosDenominaciones += "," + Join(Divisa.Denominaciones.Select(Function(d) String.Format("'{0}|{1}'", Divisa.CodigoISO, d.Codigo)).ToArray, ",")
                End If
            Next

            'Se o parâmetro fDivisaDenominacion estiver vazio, seu valor será '' para que a query seja executada sem erros
            If Not String.IsNullOrEmpty(CodigosDenominaciones) Then
                'Retira a primeira virgula
                CodigosDenominaciones = CodigosDenominaciones.Substring(1)
            Else
                CodigosDenominaciones = "' '"
            End If

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = String.Format(My.Resources.Salidas_TipoBulto_DividirEnBultos, CodigosDenominaciones)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codDelegacion))
            End With
            'Executa a query e retorna um datatable
            Dim dt = DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr In dt.Rows
                    Dim tipoBulto As Clases.TipoBulto
                    tipoBulto = listaTipoBulto.FirstOrDefault(Function(tb) tb.Codigo = dr("COD_TIPO_BULTO"))

                    If (tipoBulto Is Nothing) Then
                        tipoBulto = New Clases.TipoBulto
                        tipoBulto.Codigo = Util.AtribuirValorObj(dr("COD_TIPO_BULTO"), GetType(String))
                        tipoBulto.NumImporteMaxSeguro = Util.AtribuirValorObj(dr("NUM_IMPORTE_MAXIMO_SEGURO"), GetType(Decimal))
                        tipoBulto.EsAptoPicos = Util.AtribuirValorObj(dr("BOL_APTO_PICOS"), GetType(String))
                        tipoBulto.EsAptoMezcla = Util.AtribuirValorObj(dr("BOL_APTO_MEZCLA"), GetType(String))

                        listaTipoBulto.Add(tipoBulto)
                    End If

                    ' Preenche tipo bulto denominação
                    Dim tipoBultoDenominacion = New Clases.TipoBultoDenominacion(tipoBulto)
                    tipoBultoDenominacion.NelMaximoUnidades = Util.AtribuirValorObj(dr("NEL_MAXIMO_UNIDADES"), GetType(String))
                    tipoBultoDenominacion.NecAgrupacion = Util.AtribuirValorObj(dr("NEC_AGRUPACION"), GetType(String))

                    ' Preenche denominacion
                    tipoBultoDenominacion.Denominacion = New Clases.Denominacion
                    tipoBultoDenominacion.Denominacion.Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String))
                    tipoBultoDenominacion.Denominacion.Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String))
                    tipoBultoDenominacion.Denominacion.Identificador = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))
                    tipoBultoDenominacion.Denominacion.EsBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Boolean))
                    tipoBultoDenominacion.Denominacion.Valor = Util.AtribuirValorObj(dr("NUM_VALOR_FACIAL"), GetType(Decimal))

                    ' Preenche divisa
                    Dim codIsoDivisa = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
                    Dim divisaTP = (From tp In listaTipoBulto _
                                       From tpd In tp.TiposBultosDenominacion _
                                       Where tpd.Denominacion.Divisa IsNot Nothing AndAlso tpd.Denominacion.Divisa.CodigoISO = codIsoDivisa
                                       Select tpd.Denominacion.Divisa).FirstOrDefault()
                    If (divisaTP Is Nothing) Then
                        tipoBultoDenominacion.Denominacion.Divisa = New Clases.Divisa()
                        tipoBultoDenominacion.Denominacion.Divisa.CodigoISO = codIsoDivisa
                        tipoBultoDenominacion.Denominacion.Divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)
                    Else
                        tipoBultoDenominacion.Denominacion.Divisa = divisaTP
                    End If

                    tipoBultoDenominacion.Denominacion.Divisa.Denominaciones.Add(tipoBultoDenominacion.Denominacion)

                    ' Carrega valores a balancear
                    Dim denominacionDividir = (From div In objDivisas _
                                              From den In div.Denominaciones _
                                              Where den.Codigo = tipoBultoDenominacion.Denominacion.Codigo AndAlso div.CodigoISO = tipoBultoDenominacion.Denominacion.Divisa.CodigoISO _
                                              Select den).FirstOrDefault()
                    If (denominacionDividir IsNot Nothing AndAlso denominacionDividir.ValorDenominacion IsNot Nothing) Then
                        tipoBultoDenominacion.Denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)()
                        For Each valorDividir In denominacionDividir.ValorDenominacion
                            Dim valorDenominacao = New Clases.ValorDenominacion()
                            valorDenominacao.Cantidad = valorDividir.Cantidad
                            valorDenominacao.Importe = valorDividir.Importe
                            tipoBultoDenominacion.Denominacion.ValorDenominacion.Add(valorDenominacao)
                        Next
                    End If

                Next

            End If

            Return listaTipoBulto
        End Function

        Public Shared Function ObtenerTipoBultoPorTipoBulto(codTipoBulto As String) As Clases.TipoBulto
            Dim objTipoBulto As Clases.TipoBulto = Nothing

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_TipoBulto_ObtenerPorTipoModulo)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_TIPO_BULTO", ProsegurDbType.Identificador_Alfanumerico, codTipoBulto))
            End With
            'Executa a query e retorna um datatable
            Dim dt = DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objTipoBulto = New Clases.TipoBulto
                With objTipoBulto
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_BULTO"), GetType(String))
                    .NumImporteMaxSeguro = Util.AtribuirValorObj(dt.Rows(0)("NUM_IMPORTE_MAXIMO_SEGURO"), GetType(Decimal))
                    .EsAptoPicos = Util.AtribuirValorObj(dt.Rows(0)("C_BOL_APTO_PICOS"), GetType(Boolean))
                    .EsAptoMezcla = Util.AtribuirValorObj(dt.Rows(0)("BOL_APTO_MEZCLA"), GetType(Boolean))
                End With
            End If

            Return objTipoBulto
        End Function
    End Class

End Namespace
