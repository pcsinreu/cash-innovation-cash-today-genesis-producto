Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas.Remesa
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports System.Threading.Tasks

Namespace NuevoSalidas

    Public Class Divisa

        ''' <summary>
        ''' Recupera as divisas da remesa
        ''' </summary>
        ''' <param name="IdentificadoresRemesas"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarDivisasRemesas(IdentificadoresRemesas As List(Of String)) As Dictionary(Of String, ObservableCollection(Of Clases.Divisa))

            Dim objRemesasDivisas As Dictionary(Of String, ObservableCollection(Of Clases.Divisa)) = Nothing
            Dim objDivisasConValoresTotais As Dictionary(Of String, ObservableCollection(Of Clases.Divisa)) = Nothing
            Dim objDivisasValoresMedioPago As Dictionary(Of String, ObservableCollection(Of Clases.Divisa)) = Nothing

            Dim TfDivisas As Task = Nothing
            Dim TfValoresTotales As Task = Nothing
            Dim TfValoresMedioPago As Task = Nothing

            TfDivisas = New Task(Sub()
                                     objRemesasDivisas = RecuperarValoresDetalladosDivisasRemesas(IdentificadoresRemesas)
                                 End Sub)

            TfValoresTotales = New Task(Sub()
                                            objDivisasConValoresTotais = RecuperarValoresTotaisRemesas(IdentificadoresRemesas, False)
                                        End Sub)

            TfValoresMedioPago = New Task(Sub()
                                              objDivisasValoresMedioPago = RecuperarValoresRemesasTiposMedioPago(IdentificadoresRemesas)
                                          End Sub)
            TfDivisas.Start()
            TfValoresTotales.Start()
            TfValoresMedioPago.Start()

            Task.WaitAll(New Task() {TfDivisas, TfValoresTotales, TfValoresMedioPago})


            If objDivisasConValoresTotais Is Nothing OrElse
               objDivisasConValoresTotais.Count < IdentificadoresRemesas.Count Then

                Dim IdentificadoresRemesasSinValores As List(Of String) = Nothing

                If objDivisasConValoresTotais IsNot Nothing AndAlso objDivisasConValoresTotais.Count > 0 Then

                    IdentificadoresRemesasSinValores = (From idrem In IdentificadoresRemesas Join idremcont In _
                                                       (From idrem1 In IdentificadoresRemesas Select idrem1).Except _
                                                       (From idremcv In objDivisasConValoresTotais Select idremcv.Key) On idrem Equals idremcont Select idrem).ToList
                Else
                    IdentificadoresRemesasSinValores = IdentificadoresRemesas
                End If


                Dim objDivisasRemesasSinValores As Dictionary(Of String, ObservableCollection(Of Clases.Divisa)) = RecuperarValoresTotaisRemesas(IdentificadoresRemesasSinValores, True)

                If objDivisasRemesasSinValores IsNot Nothing AndAlso objDivisasRemesasSinValores.Count > 0 Then

                    If objDivisasConValoresTotais Is Nothing Then objDivisasConValoresTotais = New Dictionary(Of String, ObservableCollection(Of Clases.Divisa))

                    For Each objRem In objDivisasRemesasSinValores
                        objDivisasConValoresTotais.Add(objRem.Key, objRem.Value)
                    Next

                End If

            End If

            If objDivisasConValoresTotais IsNot Nothing AndAlso objDivisasConValoresTotais.Count > 0 Then

                If objRemesasDivisas Is Nothing Then objRemesasDivisas = New Dictionary(Of String, ObservableCollection(Of Clases.Divisa))

                Dim objDivisa As Clases.Divisa = Nothing
                Dim objDivisaRemesa As KeyValuePair(Of String, ObservableCollection(Of Clases.Divisa))

                For Each objRemDiv In objDivisasConValoresTotais

                    objDivisaRemesa = (From divRem In objRemesasDivisas Where divRem.Key = objRemDiv.Key).FirstOrDefault

                    If String.IsNullOrEmpty(objDivisaRemesa.Key) Then

                        objRemesasDivisas.Add(objRemDiv.Key, New ObservableCollection(Of Clases.Divisa))
                        objDivisaRemesa = (From divRem In objRemesasDivisas Where divRem.Key = objRemDiv.Key).FirstOrDefault

                    End If

                    If objRemDiv.Value IsNot Nothing AndAlso objRemDiv.Value.Count > 0 Then

                        For Each objDiv In objRemDiv.Value

                            objDivisa = (From Div In objDivisaRemesa.Value Where Div.Identificador = objDiv.Identificador).FirstOrDefault

                            If objDivisa Is Nothing Then
                                objDivisaRemesa.Value.Add(New Clases.Divisa With {.Identificador = objDiv.Identificador, _
                                                                                   .CodigoISO = objDiv.CodigoISO, _
                                                                                   .Descripcion = objDiv.Descripcion, _
                                                                                   .ValoresTotalesEfectivo = objDiv.ValoresTotalesEfectivo})
                            Else
                                objDivisa.ValoresTotalesEfectivo = objDiv.ValoresTotalesEfectivo
                            End If

                        Next

                    End If

                Next

            End If

            If objDivisasValoresMedioPago IsNot Nothing AndAlso objDivisasValoresMedioPago.Count > 0 Then

                If objRemesasDivisas Is Nothing Then objRemesasDivisas = New Dictionary(Of String, ObservableCollection(Of Clases.Divisa))

                Dim objDivisa As Clases.Divisa = Nothing
                Dim objDivisaRemesa As KeyValuePair(Of String, ObservableCollection(Of Clases.Divisa))

                For Each objRemdiv In objDivisasValoresMedioPago

                    objDivisaRemesa = (From divRem In objRemesasDivisas Where divRem.Key = objRemdiv.Key).FirstOrDefault

                    If String.IsNullOrEmpty(objDivisaRemesa.Key) Then

                        objRemesasDivisas.Add(objRemdiv.Key, New ObservableCollection(Of Clases.Divisa))
                        objDivisaRemesa = (From divRem In objRemesasDivisas Where divRem.Key = objRemdiv.Key).FirstOrDefault

                    End If

                    If objRemdiv.Value IsNot Nothing AndAlso objRemdiv.Value.Count > 0 Then

                        For Each objDiv In objRemdiv.Value

                            objDivisa = (From Div In objDivisaRemesa.Value Where Div.Identificador = objDiv.Identificador).FirstOrDefault

                            If objDivisa Is Nothing Then
                                objDivisaRemesa.Value.Add(New Clases.Divisa With {.Identificador = objDiv.Identificador, _
                                                                       .CodigoISO = objDiv.CodigoISO, _
                                                                       .Descripcion = objDiv.Descripcion, _
                                                                       .ValoresTotalesTipoMedioPago = objDiv.ValoresTotalesTipoMedioPago})
                            Else
                                objDivisa.ValoresTotalesTipoMedioPago = objDiv.ValoresTotalesTipoMedioPago
                            End If

                        Next

                    End If
                Next

            End If

            Return objRemesasDivisas
        End Function

        ''' <summary>
        ''' Recupera as divisas da remesa
        ''' </summary>
        ''' <param name="IdentificadorRemesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarDivisasRemesa(IdentificadorRemesa As String) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisas As ObservableCollection(Of Clases.Divisa) = Nothing
            Dim objDivisasConValoresTotais As ObservableCollection(Of Clases.Divisa) = Nothing
            Dim objDivisasValoresMedioPago As ObservableCollection(Of Clases.Divisa) = Nothing

            objDivisas = RecuperarValoresDetalladosDivisaRemesa(IdentificadorRemesa)

            objDivisasConValoresTotais = RecuperarValoresTotaisRemesa(IdentificadorRemesa, False)
            objDivisasValoresMedioPago = RecuperarValoresTipoMedioPago(IdentificadorRemesa)

            If objDivisasConValoresTotais Is Nothing Then
                objDivisasConValoresTotais = RecuperarValoresTotaisRemesa(IdentificadorRemesa, True)
            End If

            If objDivisasConValoresTotais IsNot Nothing AndAlso objDivisasConValoresTotais.Count > 0 Then

                If objDivisas Is Nothing Then objDivisas = New ObservableCollection(Of Clases.Divisa)

                Dim objDivisa As Clases.Divisa = Nothing

                For Each objDiv In objDivisasConValoresTotais

                    objDivisa = (From Div In objDivisas Where Div.Identificador = objDiv.Identificador).FirstOrDefault

                    If objDivisa Is Nothing Then
                        objDivisas.Add(New Clases.Divisa With {.Identificador = objDiv.Identificador, _
                                                               .CodigoISO = objDiv.CodigoISO, _
                                                               .Descripcion = objDiv.Descripcion, _
                                                               .ValoresTotalesEfectivo = objDiv.ValoresTotalesEfectivo})
                    Else
                        objDivisa.ValoresTotalesEfectivo = objDiv.ValoresTotalesEfectivo
                    End If

                Next

            End If

            If objDivisasValoresMedioPago IsNot Nothing AndAlso objDivisasValoresMedioPago.Count > 0 Then

                If objDivisas Is Nothing Then objDivisas = New ObservableCollection(Of Clases.Divisa)

                Dim objDivisa As Clases.Divisa = Nothing

                For Each objdiv In objDivisasValoresMedioPago

                    objDivisa = (From Div In objDivisas Where Div.Identificador = objdiv.Identificador).FirstOrDefault

                    If objDivisa Is Nothing Then
                        objDivisas.Add(New Clases.Divisa With {.Identificador = objdiv.Identificador, _
                                                               .CodigoISO = objdiv.CodigoISO, _
                                                               .Descripcion = objdiv.Descripcion, _
                                                               .ValoresTotalesTipoMedioPago = objdiv.ValoresTotalesTipoMedioPago})
                    Else
                        objDivisa.ValoresTotalesTipoMedioPago = objdiv.ValoresTotalesTipoMedioPago
                    End If

                Next

            End If

            Return objDivisas
        End Function

        ''' <summary>
        ''' Recupera as divisas da remesa
        ''' </summary>
        ''' <param name="IdentificadoresBultos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarDivisasBultosModulos(IdentificadoresBultos As List(Of String)) As List(Of Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa)))

            If IdentificadoresBultos Is Nothing OrElse IdentificadoresBultos.Count = 0 Then
                Return Nothing
            End If

            Dim objBultosDivisas As List(Of Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa))) = Nothing
            Dim objDivisasConValoresTotais As List(Of Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa))) = Nothing

            Dim TfDivisas As Task = Nothing
            Dim TfValoresTotales As Task = Nothing

            TfDivisas = New Task(Sub()
                                     objBultosDivisas = RecuperarValoresDetalladosBultosModulos(IdentificadoresBultos)
                                 End Sub)

            TfValoresTotales = New Task(Sub()
                                            objDivisasConValoresTotais = RecuperarValoresTotaisBultosModulos(IdentificadoresBultos)
                                        End Sub)
            TfDivisas.Start()
            TfValoresTotales.Start()

            Task.WaitAll(New Task() {TfDivisas, TfValoresTotales})

            If objDivisasConValoresTotais IsNot Nothing AndAlso objDivisasConValoresTotais.Count > 0 Then

                If objBultosDivisas Is Nothing Then objBultosDivisas = New List(Of Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa)))

                Dim objDivisa As Clases.Divisa = Nothing
                Dim objDivisaBulto As Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa))

                For Each objBulDiv In objDivisasConValoresTotais

                    objDivisaBulto = (From divBul In objBultosDivisas Where divBul.Item1 = objBulDiv.Item1).FirstOrDefault

                    If objDivisaBulto Is Nothing Then

                        objBultosDivisas.Add(New Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa))(
                                             objBulDiv.Item1, objBulDiv.Item2, New ObservableCollection(Of Clases.Divisa)))
                        objDivisaBulto = (From divBul In objBultosDivisas Where divBul.Item1 = objBulDiv.Item1).FirstOrDefault

                    End If

                    If objBulDiv.Item3 IsNot Nothing AndAlso objBulDiv.Item3.Count > 0 Then

                        For Each objDiv In objBulDiv.Item3

                            objDivisa = (From Div In objDivisaBulto.Item3 Where Div.Identificador = objDiv.Identificador).FirstOrDefault

                            If objDivisa Is Nothing Then
                                objDivisaBulto.Item3.Add(New Clases.Divisa With {.Identificador = objDiv.Identificador, _
                                                                                   .CodigoISO = objDiv.CodigoISO, _
                                                                                   .Descripcion = objDiv.Descripcion, _
                                                                                   .ValoresTotalesEfectivo = objDiv.ValoresTotalesEfectivo})
                            Else
                                objDivisa.ValoresTotalesEfectivo = objDiv.ValoresTotalesEfectivo
                            End If

                        Next

                    End If

                Next

            End If

            Return objBultosDivisas
        End Function

        ''' <summary>
        ''' Recupera as divisas da remesa
        ''' </summary>
        ''' <param name="IdentificadorBultoModulo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarDivisasBultoOModulo(IdentificadorBultoModulo As String, ByRef EsModulo As Boolean) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisas As ObservableCollection(Of Clases.Divisa) = Nothing
            Dim objDivisasConValoresTotais As ObservableCollection(Of Clases.Divisa) = Nothing

            objDivisas = RecuperarValoresDetalladosBultoOMOdulo(IdentificadorBultoModulo, EsModulo)

            objDivisasConValoresTotais = RecuperarValoresTotaisBultoOModulo(IdentificadorBultoModulo, EsModulo)

            If objDivisasConValoresTotais IsNot Nothing AndAlso objDivisasConValoresTotais.Count > 0 Then

                If objDivisas Is Nothing Then objDivisas = New ObservableCollection(Of Clases.Divisa)

                Dim objDivisa As Clases.Divisa = Nothing

                For Each objDiv In objDivisasConValoresTotais

                    objDivisa = (From Div In objDivisas Where Div.Identificador = objDiv.Identificador).FirstOrDefault

                    If objDivisa Is Nothing Then
                        objDivisas.Add(New Clases.Divisa With {.Identificador = objDiv.Identificador, _
                                                               .CodigoISO = objDiv.CodigoISO, _
                                                               .Descripcion = objDiv.Descripcion, _
                                                               .ValoresTotalesEfectivo = objDiv.ValoresTotalesEfectivo})
                    Else
                        objDivisa.ValoresTotalesEfectivo = objDiv.ValoresTotalesEfectivo
                    End If

                Next

            End If

            Return objDivisas
        End Function

        ''' <summary>
        ''' Recupera as divisas da remesa
        ''' </summary>
        ''' <param name="IdentificadorRemesaModulo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarDivisasRemesaOModulo(IdentificadorRemesaModulo As String, EsModulo As Boolean) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisas As ObservableCollection(Of Clases.Divisa) = Nothing
            Dim objDivisasConValoresTotais As ObservableCollection(Of Clases.Divisa) = Nothing

            objDivisas = RecuperarValoresDetalladosRemesaOMOdulo(IdentificadorRemesaModulo, EsModulo)

            objDivisasConValoresTotais = RecuperarValoresTotaisRemesaOModulo(IdentificadorRemesaModulo, EsModulo)

            If objDivisasConValoresTotais IsNot Nothing AndAlso objDivisasConValoresTotais.Count > 0 Then

                If objDivisas Is Nothing Then objDivisas = New ObservableCollection(Of Clases.Divisa)

                Dim objDivisa As Clases.Divisa = Nothing

                For Each objDiv In objDivisasConValoresTotais

                    objDivisa = (From Div In objDivisas Where Div.Identificador = objDiv.Identificador).FirstOrDefault

                    If objDivisa Is Nothing Then
                        objDivisas.Add(New Clases.Divisa With {.Identificador = objDiv.Identificador, _
                                                               .CodigoISO = objDiv.CodigoISO, _
                                                               .Descripcion = objDiv.Descripcion, _
                                                               .ValoresTotalesEfectivo = objDiv.ValoresTotalesEfectivo})
                    Else
                        objDivisa.ValoresTotalesEfectivo = objDiv.ValoresTotalesEfectivo
                    End If

                Next

            End If

            Return objDivisas
        End Function

        ''' <summary>
        ''' Recupera os efectivos detalhados da remesa
        ''' </summary>
        ''' <param name="IdentificadoresRemesas"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarValoresDetalladosDivisasRemesas(IdentificadoresRemesas As List(Of String)) As Dictionary(Of String, ObservableCollection(Of Clases.Divisa))

            If IdentificadoresRemesas Is Nothing OrElse IdentificadoresRemesas.Count = 0 Then
                Return Nothing
            End If

            Dim objDivisasRemesas As Dictionary(Of String, ObservableCollection(Of Clases.Divisa)) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Divisa_RecuperarEfectivosRemesasDetallado
            cmd.CommandType = CommandType.Text

            Dim query As String = Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresRemesas, "OID_REMESA", cmd, "WHERE", "EL")

            cmd.CommandText = String.Format(cmd.CommandText, query)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisasRemesas = New Dictionary(Of String, ObservableCollection(Of Clases.Divisa))
                Dim objDivisaRemesa As KeyValuePair(Of String, ObservableCollection(Of Clases.Divisa))

                Dim ObjDivisa As Clases.Divisa = Nothing
                Dim IdentificadorDivisa As String = String.Empty
                Dim IdentificadorRemesa As String = String.Empty
                Dim objValoesrDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = Nothing

                For Each dr In dt.Rows

                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                    IdentificadorRemesa = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String))

                    objDivisaRemesa = (From DivRem In objDivisasRemesas Where DivRem.Key = IdentificadorRemesa).FirstOrDefault

                    If String.IsNullOrEmpty(objDivisaRemesa.Key) Then
                        objDivisasRemesas.Add(IdentificadorRemesa, New ObservableCollection(Of Clases.Divisa))
                        objDivisaRemesa = (From DivRem In objDivisasRemesas Where DivRem.Key = IdentificadorRemesa).FirstOrDefault
                    End If

                    objValoesrDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)()

                    ObjDivisa = (From Div In objDivisaRemesa.Value Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    If ObjDivisa Is Nothing Then

                        objDivisaRemesa.Value.Add(New Clases.Divisa With {.Identificador = IdentificadorDivisa, _
                                                                          .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                                          .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                                                          .Denominaciones = New ObservableCollection(Of Clases.Denominacion)})

                        ObjDivisa = (From Div In objDivisaRemesa.Value Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    End If

                    objValoesrDenominacion.Add(New Clases.ValorDenominacion With {.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Integer)), _
                                                                                  .Importe = Util.AtribuirValorObj(dr("NEL_IMPORTE_EFECTIVO"), GetType(Decimal))})

                    ObjDivisa.Denominaciones.Add(New Clases.Denominacion With {.Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)), _
                                                                               .Identificador = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String)), _
                                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String)), _
                                                                               .EsBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Int16)), _
                                                                               .Valor = Util.AtribuirValorObj(dr("NUM_VALOR_FACIAL"), GetType(Decimal)), _
                                                                               .ValorDenominacion = objValoesrDenominacion})
                Next

            End If

            Return objDivisasRemesas
        End Function

        ''' <summary>
        ''' Recupera os efectivos detalhados da remesa
        ''' </summary>
        ''' <param name="IdentificadorRemesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarValoresDetalladosDivisaRemesa(IdentificadorRemesa As String) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Divisa_RecuperarEfectivoRemesaDetallado)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisas = New ObservableCollection(Of Clases.Divisa)()
                Dim ObjDivisa As Clases.Divisa = Nothing
                Dim IdentificadorDivisa As String = String.Empty
                Dim objValoesrDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = Nothing

                For Each dr In dt.Rows

                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))

                    objValoesrDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)()

                    ObjDivisa = (From Div In objDivisas Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    If ObjDivisa Is Nothing Then

                        objDivisas.Add(New Clases.Divisa With {.Identificador = IdentificadorDivisa, _
                                                               .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                                               .Denominaciones = New ObservableCollection(Of Clases.Denominacion)})

                        ObjDivisa = (From Div In objDivisas Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    End If

                    objValoesrDenominacion.Add(New Clases.ValorDenominacion With {.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Integer)), _
                                                                                  .Importe = Util.AtribuirValorObj(dr("NEL_IMPORTE_EFECTIVO"), GetType(Decimal))})

                    ObjDivisa.Denominaciones.Add(New Clases.Denominacion With {.Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)), _
                                                                               .Identificador = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String)), _
                                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String)), _
                                                                               .EsBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Int16)), _
                                                                               .Valor = Util.AtribuirValorObj(dr("NUM_VALOR_FACIAL"), GetType(Decimal)), _
                                                                               .ValorDenominacion = objValoesrDenominacion})
                Next

            End If

            Return objDivisas
        End Function

        ''' <summary>
        ''' Recupera os efectivos detalhados da remesa
        ''' </summary>
        ''' <param name="IdentificadoresBultosModulos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarValoresDetalladosBultosModulos(IdentificadoresBultosModulos As List(Of String)) As List(Of Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa)))

            Dim objDivisasBultos As List(Of Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa))) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Divisa_RecuperarEfectivosBultos
            cmd.CommandType = CommandType.Text

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS,
                                                 String.Format(cmd.CommandText,
                                                               Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresBultosModulos, "OID_BULTO", cmd, "WHERE", "EB")))


            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisasBultos = New List(Of Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa)))
                Dim objDivisaBulto As Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa))

                Dim ObjDivisa As Clases.Divisa = Nothing
                Dim IdentificadorDivisa As String = String.Empty
                Dim IdentificadorBulto As String = String.Empty
                Dim objValoesrDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = Nothing
                Dim identificadorModulo As String = String.Empty

                For Each dr In dt.Rows

                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                    IdentificadorBulto = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))
                    identificadorModulo = Util.AtribuirValorObj(dr("OID_MODULO_BULTO"), GetType(String))

                    objDivisaBulto = (From DivBul In objDivisasBultos Where DivBul.Item1 = IdentificadorBulto).FirstOrDefault

                    If objDivisaBulto Is Nothing Then
                        objDivisasBultos.Add(New Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa))( _
                                             IdentificadorBulto, Not String.IsNullOrEmpty(identificadorModulo), New ObservableCollection(Of Clases.Divisa)))
                        objDivisaBulto = (From DivBul In objDivisasBultos Where DivBul.Item1 = IdentificadorBulto).FirstOrDefault
                    End If

                    objValoesrDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)()

                    ObjDivisa = (From Div In objDivisaBulto.Item3 Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    If ObjDivisa Is Nothing Then

                        objDivisaBulto.Item3.Add(New Clases.Divisa With {.Identificador = IdentificadorDivisa, _
                                                                          .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                                          .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                                                          .Denominaciones = New ObservableCollection(Of Clases.Denominacion)})

                        ObjDivisa = (From Div In objDivisaBulto.Item3 Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    End If

                    objValoesrDenominacion.Add(New Clases.ValorDenominacion With {.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Integer)), _
                                                                                  .Importe = Util.AtribuirValorObj(dr("NEL_IMPORTE_EFECTIVO"), GetType(Decimal))})

                    ObjDivisa.Denominaciones.Add(New Clases.Denominacion With {.Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)), _
                                                                               .Identificador = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String)), _
                                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String)), _
                                                                               .EsBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Int16)), _
                                                                               .Valor = Util.AtribuirValorObj(dr("NUM_VALOR_FACIAL"), GetType(Decimal)), _
                                                                               .ValorDenominacion = objValoesrDenominacion})
                Next

            End If

            Return objDivisasBultos
        End Function

        ''' <summary>
        ''' Recupera os efectivos detalhados da remesa
        ''' </summary>
        ''' <param name="IdentificadorBultoModulo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarValoresDetalladosBultoOMOdulo(IdentificadorBultoModulo As String, _
                                                                       ByRef EsModulo As Boolean) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Divisa_RecuperarEfectivoBultoDetallado
            cmd.CommandType = CommandType.Text

            If EsModulo Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, "EB.OID_MODULO_BULTO = []OID_MODULO_BULTO"))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_MODULO_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBultoModulo))
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, "EB.OID_BULTO = []OID_BULTO"))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBultoModulo))
            End If

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim identificadorModulo As String = Util.AtribuirValorObj(dt.Rows(0)("OID_MODULO_BULTO"), GetType(String))

                EsModulo = Not String.IsNullOrEmpty(identificadorModulo)

                objDivisas = New ObservableCollection(Of Clases.Divisa)()
                Dim ObjDivisa As Clases.Divisa = Nothing
                Dim IdentificadorDivisa As String = String.Empty
                Dim objValoesrDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = Nothing

                For Each dr In dt.Rows

                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))

                    objValoesrDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)()

                    ObjDivisa = (From Div In objDivisas Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    If ObjDivisa Is Nothing Then

                        objDivisas.Add(New Clases.Divisa With {.Identificador = IdentificadorDivisa, _
                                                               .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                                               .Denominaciones = New ObservableCollection(Of Clases.Denominacion)})

                        ObjDivisa = (From Div In objDivisas Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    End If

                    objValoesrDenominacion.Add(New Clases.ValorDenominacion With {.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64)), _
                                                                                  .Importe = Util.AtribuirValorObj(dr("NEL_IMPORTE_EFECTIVO"), GetType(Decimal))})

                    ObjDivisa.Denominaciones.Add(New Clases.Denominacion With {.Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)), _
                                                                               .Identificador = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String)), _
                                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String)), _
                                                                               .EsBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Int16)), _
                                                                               .Valor = Util.AtribuirValorObj(dr("NUM_VALOR_FACIAL"), GetType(Decimal)), _
                                                                               .ValorDenominacion = objValoesrDenominacion})
                Next

            End If

            Return objDivisas
        End Function

        ''' <summary>
        ''' Recupera os efectivos detalhados da remesa
        ''' </summary>
        ''' <param name="IdentificadorRemesaModulo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarValoresDetalladosRemesaOMOdulo(IdentificadorRemesaModulo As String, _
                                                                       EsModulo As Boolean) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Divisa_RecuperarEfectivoRemesaDetallado
            cmd.CommandType = CommandType.Text

            If EsModulo Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, "EB.OID_MODULO_Remesa = []OID_MODULO_Remesa"))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_MODULO_Remesa", ProsegurDbType.Objeto_Id, IdentificadorRemesaModulo))
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, "EB.OID_Remesa = []OID_Remesa"))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_Remesa", ProsegurDbType.Objeto_Id, IdentificadorRemesaModulo))
            End If

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisas = New ObservableCollection(Of Clases.Divisa)()
                Dim ObjDivisa As Clases.Divisa = Nothing
                Dim IdentificadorDivisa As String = String.Empty
                Dim objValoesrDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = Nothing

                For Each dr In dt.Rows

                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))

                    objValoesrDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)()

                    ObjDivisa = (From Div In objDivisas Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    If ObjDivisa Is Nothing Then

                        objDivisas.Add(New Clases.Divisa With {.Identificador = IdentificadorDivisa, _
                                                               .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                                               .Denominaciones = New ObservableCollection(Of Clases.Denominacion)})

                        ObjDivisa = (From Div In objDivisas Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    End If

                    objValoesrDenominacion.Add(New Clases.ValorDenominacion With {.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Integer)), _
                                                                                  .Importe = Util.AtribuirValorObj(dr("NEL_IMPORTE_EFECTIVO"), GetType(Decimal))})

                    ObjDivisa.Denominaciones.Add(New Clases.Denominacion With {.Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)), _
                                                                               .Identificador = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String)), _
                                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String)), _
                                                                               .ValorDenominacion = objValoesrDenominacion})
                Next

            End If

            Return objDivisas
        End Function

        Private Shared Function RecuperarValoresRemesasTiposMedioPago(IdentificadoresRemesas As List(Of String)) As Dictionary(Of String, ObservableCollection(Of Clases.Divisa))

            Dim objDivisasRemesas As Dictionary(Of String, ObservableCollection(Of Clases.Divisa)) = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_TipoMedioPagoRecuperarValoresRemesas
            cmd.CommandType = CommandType.Text

            Dim Query As New System.Text.StringBuilder

            Query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresRemesas, "OID_REMESA", cmd, "WHERE", "TMP"))

            cmd.CommandText = String.Format(cmd.CommandText, Query.ToString)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
            Dim ObjDivisa As Clases.Divisa = Nothing
            Dim IdentificadorDivisa As String = String.Empty
            Dim objValoresTipoMedioPago As ObservableCollection(Of Clases.ValorTipoMedioPago) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisasRemesas = New Dictionary(Of String, ObservableCollection(Of Clases.Divisa))
                Dim objDivisaRemesa As KeyValuePair(Of String, ObservableCollection(Of Clases.Divisa))

                Dim IdentificadorRemesa As String = String.Empty

                For Each dr In dt.Rows

                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                    IdentificadorRemesa = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String))

                    objDivisaRemesa = (From DivRem In objDivisasRemesas Where DivRem.Key = IdentificadorRemesa).FirstOrDefault

                    If String.IsNullOrEmpty(objDivisaRemesa.Key) Then

                        objDivisasRemesas.Add(IdentificadorRemesa, New ObservableCollection(Of Clases.Divisa))
                        objDivisaRemesa = (From DivRem In objDivisasRemesas Where DivRem.Key = IdentificadorRemesa).FirstOrDefault

                    End If

                    objValoresTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()

                    ObjDivisa = (From Div In objDivisaRemesa.Value Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    If ObjDivisa Is Nothing Then

                        objDivisaRemesa.Value.Add(New Clases.Divisa With {.Identificador = IdentificadorDivisa, _
                                                               .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                                               .ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)})

                        ObjDivisa = (From Div In objDivisaRemesa.Value Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    End If

                    ObjDivisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago With {.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE_TIPO_MP_LEGADO"), GetType(Decimal)), _
                                                                                                  .TipoMedioPago = Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(dr("COD_TIPO_MEDIO_PAGO"), GetType(String)))})

                Next

            End If

            Return objDivisasRemesas
        End Function

        Private Shared Function RecuperarValoresTipoMedioPago(IdentificadorRemesa As String) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisas As ObservableCollection(Of Clases.Divisa) = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_TipoMedioPagoRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
            Dim ObjDivisa As Clases.Divisa = Nothing
            Dim IdentificadorDivisa As String = String.Empty
            Dim objValoresTipoMedioPago As ObservableCollection(Of Clases.ValorTipoMedioPago) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisas = New ObservableCollection(Of Clases.Divisa)

                For Each dr In dt.Rows

                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))

                    objValoresTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()

                    ObjDivisa = (From Div In objDivisas Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    If ObjDivisa Is Nothing Then

                        objDivisas.Add(New Clases.Divisa With {.Identificador = IdentificadorDivisa, _
                                                               .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                                               .ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)})

                        ObjDivisa = (From Div In objDivisas Where Div.Identificador = IdentificadorDivisa).FirstOrDefault

                    End If

                    ObjDivisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago With {.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE_TIPO_MP_LEGADO"), GetType(Decimal)), _
                                                                                                  .TipoMedioPago = Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(dr("COD_TIPO_MEDIO_PAGO"), GetType(String)))})

                Next

            End If

            Return objDivisas
        End Function

        ''' <summary>
        ''' Recupera os valores totais dos efectivos da remesa
        ''' </summary>
        ''' <param name="IdentificadoresRemesas"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarValoresTotaisRemesas(IdentificadoresRemesas As List(Of String), _
                                                              SomarImporteDetallado As Boolean) As Dictionary(Of String, ObservableCollection(Of Clases.Divisa))

            If IdentificadoresRemesas Is Nothing OrElse IdentificadoresRemesas.Count = 0 Then
                Return Nothing
            End If

            Dim objDivisasRemesas As Dictionary(Of String, ObservableCollection(Of Clases.Divisa)) = Nothing
            Dim query As New System.Text.StringBuilder
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Divisa_RecuperarEfectivosRemesasTotales
            cmd.CommandType = CommandType.Text

            If Not SomarImporteDetallado Then
                query.Append(" WHERE EL.OID_DENOMINACION IS NULL ")
            Else
                query.Append(" WHERE EL.OID_DENOMINACION IS NOT NULL ")
            End If

            query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresRemesas, "OID_REMESA", cmd, "AND", "EL"))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, query.ToString))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisasRemesas = New Dictionary(Of String, ObservableCollection(Of Clases.Divisa))
                Dim objDivisaRemesa As KeyValuePair(Of String, ObservableCollection(Of Clases.Divisa))

                Dim IdentificadorRemesa As String = String.Empty
                Dim objValoresEfectivo As ObservableCollection(Of Clases.ValorEfectivo) = Nothing

                For Each dr In dt.Rows

                    IdentificadorRemesa = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String))

                    objDivisaRemesa = (From DivRem In objDivisasRemesas Where DivRem.Key = IdentificadorRemesa).FirstOrDefault

                    If String.IsNullOrEmpty(objDivisaRemesa.Key) Then

                        objDivisasRemesas.Add(IdentificadorRemesa, New ObservableCollection(Of Clases.Divisa))

                        objDivisaRemesa = (From DivRem In objDivisasRemesas Where DivRem.Key = IdentificadorRemesa).FirstOrDefault

                    End If
                    objValoresEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)()

                    objValoresEfectivo.Add(New Clases.ValorEfectivo With {.Importe = Util.AtribuirValorObj(dr("NEL_IMPORTE_EFECTIVO"), GetType(Decimal))})

                    objDivisaRemesa.Value.Add(New Clases.Divisa With {.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String)), _
                                              .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                              .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                              .Denominaciones = New ObservableCollection(Of Clases.Denominacion), _
                                              .ValoresTotalesEfectivo = objValoresEfectivo})
                Next

            End If

            Return objDivisasRemesas
        End Function

        ''' <summary>
        ''' Recupera os valores totais dos efectivos da remesa
        ''' </summary>
        ''' <param name="IdentificadorRemesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarValoresTotaisRemesa(IdentificadorRemesa As String, _
                                                             SomarImporteDetallado As Boolean) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisas As ObservableCollection(Of Clases.Divisa) = Nothing
            Dim query As New System.Text.StringBuilder
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Divisa_RecuperarEfectivosRemesaTotais
            cmd.CommandType = CommandType.Text

            If Not SomarImporteDetallado Then
                query.Append(" WHERE EL.OID_DENOMINACION IS NULL AND OID_REMESA = []OID_REMESA ")
            Else
                query.Append(" WHERE EL.OID_DENOMINACION IS NOT NULL AND OID_REMESA = []OID_REMESA ")
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, query.ToString))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisas = New ObservableCollection(Of Clases.Divisa)()
                Dim objValoresEfectivo As ObservableCollection(Of Clases.ValorEfectivo) = Nothing

                For Each dr In dt.Rows

                    objValoresEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)()

                    objValoresEfectivo.Add(New Clases.ValorEfectivo With {.Importe = Util.AtribuirValorObj(dr("NEL_IMPORTE_EFECTIVO"), GetType(Decimal))})

                    objDivisas.Add(New Clases.Divisa With {.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String)), _
                                                           .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                           .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                                           .Denominaciones = New ObservableCollection(Of Clases.Denominacion), _
                                                           .ValoresTotalesEfectivo = objValoresEfectivo})
                Next

            End If

            Return objDivisas
        End Function

        ''' <summary>
        ''' Recupera os valores totais dos efectivos da remesa
        ''' </summary>
        ''' <param name="IdentificadoresBultosModulos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarValoresTotaisBultosModulos(IdentificadoresBultosModulos As List(Of String)) As List(Of Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa)))

            Dim objDivisasBultos As List(Of Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa))) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_DivisasRecuperarEfectivosTotalesBultosModulos
            cmd.CommandType = CommandType.Text

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS,
                                                 String.Format(cmd.CommandText,
                                                               Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresBultosModulos, "OID_BULTO", cmd, "WHERE", "EB")))


            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisasBultos = New List(Of Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa)))
                Dim objDivisaBulto As Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa))

                Dim IdentificadorBulto As String = String.Empty
                Dim objValoresEfectivo As ObservableCollection(Of Clases.ValorEfectivo) = Nothing

                For Each dr In dt.Rows

                    IdentificadorBulto = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))

                    objDivisaBulto = (From DivBul In objDivisasBultos Where DivBul.Item1 = IdentificadorBulto).FirstOrDefault

                    If objDivisaBulto Is Nothing Then

                        objDivisasBultos.Add(New Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa))(
                                             IdentificadorBulto, False, New ObservableCollection(Of Clases.Divisa)))

                        objDivisaBulto = (From DivBul In objDivisasBultos Where DivBul.Item1 = IdentificadorBulto).FirstOrDefault

                    End If

                    objValoresEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)()

                    objValoresEfectivo.Add(New Clases.ValorEfectivo With {.Importe = Util.AtribuirValorObj(dr("NEL_IMPORTE_EFECTIVO"), GetType(Decimal))})

                    objDivisaBulto.Item3.Add(New Clases.Divisa With {.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String)), _
                                              .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                              .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                              .Denominaciones = New ObservableCollection(Of Clases.Denominacion), _
                                              .ValoresTotalesEfectivo = objValoresEfectivo})
                Next

            End If

            Return objDivisasBultos
        End Function

        ''' <summary>
        ''' Recupera os valores totais dos efectivos da remesa
        ''' </summary>
        ''' <param name="IdentificadorBultoModulo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarValoresTotaisBultoOModulo(IdentificadorBultoModulo As String, _
                                                                   EsModulo As Boolean) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Divisa_RecuperarEfectivosBultoTotais)
            cmd.CommandType = CommandType.Text

            If EsModulo Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, " EB.OID_MODULO_BULTO = []OID_MODULO_BULTO"))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_MODULO_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBultoModulo))
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, " EB.OID_BULTO = []OID_BULTO"))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBultoModulo))
            End If

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisas = New ObservableCollection(Of Clases.Divisa)()
                Dim objValoresEfectivo As ObservableCollection(Of Clases.ValorEfectivo) = Nothing

                For Each dr In dt.Rows

                    objValoresEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)()

                    objValoresEfectivo.Add(New Clases.ValorEfectivo With {.Importe = Util.AtribuirValorObj(dr("NEL_IMPORTE_EFECTIVO"), GetType(Decimal))})

                    objDivisas.Add(New Clases.Divisa With {.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String)), _
                                                           .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                           .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                                           .Denominaciones = New ObservableCollection(Of Clases.Denominacion), _
                                                           .ValoresTotalesEfectivo = objValoresEfectivo})
                Next

            End If

            Return objDivisas
        End Function

        ''' <summary>
        ''' Recupera os valores totais dos efectivos da remesa
        ''' </summary>
        ''' <param name="IdentificadorRemesaModulo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarValoresTotaisRemesaOModulo(IdentificadorRemesaModulo As String, _
                                                                   EsModulo As Boolean) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Divisa_RecuperarEfectivosRemesaTotais)
            cmd.CommandType = CommandType.Text

            If EsModulo Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, " EB.OID_MODULO_Remesa = []OID_MODULO_Remesa"))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_MODULO_Remesa", ProsegurDbType.Objeto_Id, IdentificadorRemesaModulo))
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, " EB.OID_Remesa = []OID_Remesa"))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_Remesa", ProsegurDbType.Objeto_Id, IdentificadorRemesaModulo))
            End If

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisas = New ObservableCollection(Of Clases.Divisa)()
                Dim objValoresEfectivo As ObservableCollection(Of Clases.ValorEfectivo) = Nothing

                For Each dr In dt.Rows

                    objValoresEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)()

                    objValoresEfectivo.Add(New Clases.ValorEfectivo With {.Importe = Util.AtribuirValorObj(dr("NEL_IMPORTE_EFECTIVO"), GetType(Decimal))})

                    objDivisas.Add(New Clases.Divisa With {.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String)), _
                                                           .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                           .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                                           .Denominaciones = New ObservableCollection(Of Clases.Denominacion), _
                                                           .ValoresTotalesEfectivo = objValoresEfectivo})
                Next

            End If

            Return objDivisas

        End Function

    End Class

End Namespace