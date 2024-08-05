Imports Prosegur.Impressao.Zebra
Imports Prosegur.Genesis.Impresion.NuevoSalidas.Ticket.Bulto
Imports System.Drawing
Imports Prosegur.Genesis.Impresion.NuevoSalidas.Ticket.Bulto.Parametros
Imports Prosegur.Framework.Dicionario

Namespace NuevoSalidas.Ticket.Bulto.Zebra

    Public Class LayoutFr

        Private Shared printer As TicketImpressao = Nothing
        'Private Shared printerName As String = String.Empty
        Private Shared XPosEscrituraColumna1 As Integer = ConvertMM2DPI(1) '2
        Private Shared XPosEscrituraColumnaIntermedia As Integer = ConvertMM2DPI(22) '26
        Private Shared XPosEscrituraDenominacionBillete As Integer = ConvertMM2DPI(51) '66
        Private Shared XPosEscrituraBillete As Integer = ConvertMM2DPI(60) '72
        Private Shared XPosEscrituraDenominacionMoneda As Integer = ConvertMM2DPI(76) '81
        Private Shared XPosEscrituraMoneda As Integer = ConvertMM2DPI(84) '88

        Private Shared XPosDivisionColumna1 As Integer = ConvertMM2DPI(50) '65
        Private Shared XPosDivisionColumna2 As Integer = ConvertMM2DPI(75) '80
        Private Shared XPosDivisionFinal As Integer = ConvertMM2DPI(100)
        Private Shared XPosSubDivisionCol1 As Integer = ConvertMM2DPI(21) '25

        Private Shared tamMaxCol1 As Integer = ConvertMM2DPI(50) '65
        Private Shared tamMaxCol2 As Integer = ConvertMM2DPI(25) '15
        Private Shared tamMaxCol3 As Integer = ConvertMM2DPI(25) '20

        Private Shared MaxCantCaracterC1 As Integer = 34
        Private Shared C2Y As Integer
        Private Shared C3Y As Integer

        Private Enum TipoDenominacion
            Billete
            Moneda
        End Enum

        Public Shared Sub Imprimir(bulto As Parametros.Bulto, Impressora As String)

            'If Not (Impressora = printerName) Then
            '    printer = Nothing
            'printerName = Impressora 'No se puedemantener la instancia de la impresora porque no limpia el buffer de impresion. Problema en el objeto TicketImpressao
            'End If

            'If IsNothing(printer) Then

            'Sustituir por el constructor que permite definir los bits de datos (p1), charset (p2), country code (p3).
            'printer = New TicketImpressao(printerName, 100, 50, 3)
            'Nuevo constructor que tiene en cuenta: bits de datos (p1), charset (p2), country code (p3).
            ' Los parámetros son consistentes con la prueba que realizó Elvio Ramirez en Francia.
            printer = New TicketImpressao(Impressora, 100, 50, 3, "8", "A", "033")
            'End If

            ImprimirEtiqueta(bulto)

            '#If Not Debug Then
            printer.Imprimir(1)
            '#End If

        End Sub

        Private Shared Sub ImprimirEtiqueta(bulto As Parametros.Bulto)

            C2Y = 1
            C3Y = 1
            PrintLayoutBase()
            Dim haybilletes As Boolean = False
            Dim haymonedas As Boolean = False

            ImprimirDatos(bulto)

            If bulto.DenominacionBillete IsNot Nothing AndAlso bulto.DenominacionBillete.Count > 0 Then

                For i = 0 To bulto.DenominacionBillete.Count - 1 Step 1
                    If bulto.DenominacionBillete(i).NelCantidad > 0 Then
                        haybilletes = True
                        Exit For
                    End If
                Next

            End If

            If bulto.DenominacionMoneda IsNot Nothing AndAlso bulto.DenominacionMoneda.Count > 0 Then

                For i = 0 To bulto.DenominacionMoneda.Count - 1 Step 1

                    If bulto.DenominacionMoneda(i).NelCantidad > 0 Then
                        haymonedas = True
                        Exit For
                    End If

                Next

            End If


            If haybilletes AndAlso haymonedas Then

                PrintLayoutMonedasBilletes()
                ImprimirDenomBilleteMoneda(bulto)

            Else
                If haybilletes Then

                    ImprimirDenominaciones(bulto.DenominacionBillete)

                ElseIf haymonedas Then

                    ImprimirDenominaciones(bulto.DenominacionMoneda)

                End If

            End If

        End Sub

        Private Shared Sub PrintLayoutBase()
            'Imprime division de columnas
            PrintLine(New Point(XPosDivisionColumna1, 0), 2, TicketImpressao.ConverteParaDpi(45))
            'PrintLine(New Point(XPosDivisionColumna2, 0), 2, TicketImpressao.ConverteParaDpi(45))
            PrintLine(New Point(XPosDivisionFinal, 0), 2, TicketImpressao.ConverteParaDpi(45))
            PrintLine(New Point(XPosSubDivisionCol1, TicketImpressao.ConverteParaDpi(20)), 2, TicketImpressao.ConverteParaDpi(5))
            'Imprime lineas Horizontales
            PrintLine(New Point(0, TicketImpressao.ConverteParaDpi(20)), TicketImpressao.ConverteParaDpi(50), 2)
            PrintLine(New Point(0, TicketImpressao.ConverteParaDpi(25)), TicketImpressao.ConverteParaDpi(50), 2)
            PrintLine(New Point(0, TicketImpressao.ConverteParaDpi(30)), TicketImpressao.ConverteParaDpi(50), 2)
            PrintLine(New Point(0, TicketImpressao.ConverteParaDpi(35)), TicketImpressao.ConverteParaDpi(50), 2)
            PrintLine(New Point(0, TicketImpressao.ConverteParaDpi(40)), TicketImpressao.ConverteParaDpi(100), 2)
            PrintLine(New Point(0, TicketImpressao.ConverteParaDpi(45)), TicketImpressao.ConverteParaDpi(100), 2)
        End Sub
        Private Shared Sub PrintLayoutMonedasBilletes()

            PrintLine(New Point(XPosDivisionColumna2, 0), 2, TicketImpressao.ConverteParaDpi(45))

        End Sub

        Private Shared Sub ImprimirDatos(bulto As Parametros.Bulto)

            'PrintText(GetTextoLinea(bulto.DesCliente, 23), FonteZebra.GRANDE, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(1)), tamMaxCol1)
            Dim lineas() As String = GetTexto(bulto.DesCliente, 23, 1)
            If lineas IsNot Nothing AndAlso lineas.Count > 0 Then
                PrintText(lineas(0), FonteZebra.GRANDE, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(1)), tamMaxCol1)
            End If

            'PrintText(GetTextoLinea(bulto.CodPuntoServicio & " - " & bulto.DesPuntoServicio, 23), FonteZebra.GRANDE, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(6)), tamMaxCol1)
            'Dim text As String = GetTextoLinea(bulto.CodPuntoServicio & " - " & bulto.DesPuntoServicio, 23, 23)
            'If Not String.IsNullOrEmpty(text) Then
            '    PrintText(text, FonteZebra.GRANDE, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(11)), tamMaxCol1)
            'End If
            lineas = GetTexto(bulto.CodPuntoServicio & " - " & bulto.DesPuntoServicio, 23, 2)
            If lineas IsNot Nothing AndAlso lineas.Count > 0 Then
                PrintText(lineas(0), FonteZebra.GRANDE, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(6)), tamMaxCol1)
                If lineas.Count >= 2 Then
                    PrintText(lineas(1), FonteZebra.GRANDE, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(11)), tamMaxCol1)
                End If

            End If

            'PrintText(GetTextoLinea(Tradutor.Traduzir("imp_item_ruta_et") & ":" & bulto.CodRuta, tamMaxCol1 / 2), FonteZebra.MEDIO, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(21)), tamMaxCol1)
            lineas = GetTexto(Tradutor.Traduzir("imp_item_ruta_et") & ":" & bulto.CodRuta, tamMaxCol1 / 2, 1)
            If lineas IsNot Nothing AndAlso lineas.Count > 0 Then
                PrintText(lineas(0), FonteZebra.MEDIO, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(21)), tamMaxCol1)
            End If

            'PrintText(GetTextoLinea(Tradutor.Traduzir("imp_item_nro_bulto_et") & ":" & bulto.NroDocumento, tamMaxCol1 / 2), FonteZebra.MEDIO, XPosEscrituraColumnaIntermedia, Convert.ToInt32(TicketImpressao.ConverteParaDpi(21)), tamMaxCol1)
            lineas = GetTexto(Tradutor.Traduzir("imp_item_nro_bulto_et") & ":" & bulto.NroDocumento, tamMaxCol1 / 2, 1)
            If lineas IsNot Nothing AndAlso lineas.Count > 0 Then
                PrintText(lineas(0), FonteZebra.MEDIO, XPosEscrituraColumnaIntermedia, Convert.ToInt32(TicketImpressao.ConverteParaDpi(21)), tamMaxCol1)
            End If

            'PrintText(GetTextoLinea(Tradutor.Traduzir("imp_item_fecha_entrega") & ":" & bulto.FechaEntrega.ToString("dd/MM/yyyy"), tamMaxCol1), FonteZebra.MEDIO, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(26)), tamMaxCol1)
            lineas = GetTexto(Tradutor.Traduzir("imp_item_fecha_entrega") & ":" & bulto.FechaEntrega.ToString("dd/MM/yyyy"), tamMaxCol1, 1)
            If lineas IsNot Nothing AndAlso lineas.Count > 0 Then
                PrintText(lineas(0), FonteZebra.MEDIO, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(26)), tamMaxCol1)
            End If

            'PrintText(GetTextoLinea(Tradutor.Traduzir("imp_item_Precinto") & ":" & bulto.DesPrecinto, tamMaxCol1), FonteZebra.GRANDE, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(31)), tamMaxCol1)
            lineas = GetTexto(Tradutor.Traduzir("imp_item_Precinto") & ":" & bulto.DesPrecinto, tamMaxCol1, 1)
            If lineas IsNot Nothing AndAlso lineas.Count > 0 Then
                PrintText(lineas(0), FonteZebra.GRANDE, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(31)), tamMaxCol1)
            End If

            'PrintText(GetTextoLinea(Tradutor.Traduzir("imp_item_bulto") & " " & bulto.NumBulto.ToString() & "/" & bulto.TotalBultosRemesa.ToString(), tamMaxCol1), FonteZebra.MEDIO, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(41)), tamMaxCol1)
            lineas = GetTexto(Tradutor.Traduzir("imp_item_bulto") & " " & bulto.NumBulto.ToString() & "/" & bulto.TotalBultosRemesa.ToString(), tamMaxCol1, 1)
            If lineas IsNot Nothing AndAlso lineas.Count > 0 Then
                PrintText(lineas(0), FonteZebra.MEDIO, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(36)), tamMaxCol1)
            End If

            Dim totalToPrint As String = GeneraTotalToPrint(bulto)

            'PrintText(GetTextoLinea(Tradutor.Traduzir("imp_item_tot_remesa") & ": " & totalToPrint, tamMaxCol1), FonteZebra.GRANDE, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(46)), tamMaxCol1)
            lineas = GetTexto(Tradutor.Traduzir("imp_item_tot_remesa") & ": " & totalToPrint, tamMaxCol1, 1)
            If lineas IsNot Nothing AndAlso lineas.Count > 0 Then
                PrintText(lineas(0), FonteZebra.GRANDE, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(41)), tamMaxCol1)
            End If

            If bulto.RemesaTrabajaPorBulto Then
                PrintText(GeneraTotalToPrint(bulto, True), FonteZebra.GRANDE, XPosEscrituraBillete, Convert.ToInt32(TicketImpressao.ConverteParaDpi(41)), (tamMaxCol2 + tamMaxCol3))
            Else
                PrintText(totalToPrint, FonteZebra.GRANDE, XPosEscrituraBillete, Convert.ToInt32(TicketImpressao.ConverteParaDpi(41)), (tamMaxCol2 + tamMaxCol3))
            End If

            'PrintText(GetTextoLinea(Tradutor.Traduzir("imp_item_cod_ref") & ":" & bulto.DesRefCliente, 20), FonteZebra.MEDIO, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(36)), tamMaxCol1)
            If (String.IsNullOrEmpty(bulto.DesRefCliente)) Then bulto.DesRefCliente = String.Empty
            If (bulto.DesRefCliente.Length > 50) Then bulto.DesRefCliente = bulto.DesRefCliente.Substring(0, 50)
            lineas = GetTexto(Tradutor.Traduzir("imp_item_cod_ref") & ": " & bulto.DesRefCliente, 58, 0)
            If lineas IsNot Nothing AndAlso lineas.Count > 0 Then
                For Each linea In lineas
                    PrintText(linea, FonteZebra.MEDIO, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(46)), tamMaxCol1 + tamMaxCol2 + tamMaxCol3)
                Next
            End If

        End Sub

        ''' <summary>
        ''' Utilizado para imprimir las denominaciones y valores, cuando existen tanto billetes como monedas(Asume validacion previa). Utiliza un layout diferente.
        ''' </summary>
        ''' <param name="bulto"></param>
        ''' <remarks></remarks>
        Private Shared Sub ImprimirDenomBilleteMoneda(bulto As Parametros.Bulto)

            Dim importe As String = String.Empty

            For i = 0 To bulto.DenominacionBillete.Count - 1 Step 1

                If bulto.DenominacionBillete(i).NelCantidad > 0 Then
                    importe = ValidaMoneySeparator(bulto.DenominacionBillete(i).Importe)
                    importe = importe.PadLeft(10)
                    ImprimeDenominacion((bulto.DenominacionBillete(i).Importe / bulto.DenominacionBillete(i).NelCantidad).ToString() & bulto.DenominacionBillete(i).CodigoSimbolo, importe, XPosEscrituraDenominacionBillete, XPosEscrituraBillete, TicketImpressao.ConverteParaDpi(C2Y), tamMaxCol2, FonteZebra.MINUSCULO, FonteZebra.PEQUENO)
                    C2Y += 3
                End If

            Next

            For i = 0 To bulto.DenominacionMoneda.Count - 1 Step 1

                If bulto.DenominacionMoneda(i).NelCantidad > 0 Then
                    importe = ValidaMoneySeparator(bulto.DenominacionMoneda(i).Importe)
                    importe = importe.PadLeft(10)
                    ImprimeDenominacion((bulto.DenominacionMoneda(i).Importe / bulto.DenominacionMoneda(i).NelCantidad).ToString() & bulto.DenominacionMoneda(i).CodigoSimbolo, importe, XPosEscrituraDenominacionMoneda, XPosEscrituraMoneda, TicketImpressao.ConverteParaDpi(C3Y), tamMaxCol3, FonteZebra.MINUSCULO, FonteZebra.PEQUENO)
                    C3Y += 3
                End If

            Next

        End Sub

        ''' <summary>
        ''' Utilizado cuando se detectó que solo existe un tipode denominacion Billete o Moneda. Layout diferente.
        ''' </summary>
        ''' <param name="coleccion"></param>
        ''' <remarks></remarks>
        Private Shared Sub ImprimirDenominaciones(coleccion As DenominacionColeccion)

            If coleccion IsNot Nothing AndAlso coleccion.Count > 0 Then
                Dim importe As String = String.Empty
                For i = 0 To coleccion.Count - 1 Step 1
                    If coleccion(i).NelCantidad > 0 Then
                        importe = ValidaMoneySeparator(coleccion(i).Importe)
                        importe = importe.PadLeft(18)
                        ImprimeDenominacion((coleccion(i).Importe / coleccion(i).NelCantidad).ToString() & coleccion(i).CodigoSimbolo, importe, XPosEscrituraDenominacionBillete, XPosEscrituraBillete, TicketImpressao.ConverteParaDpi(C2Y), tamMaxCol2, FonteZebra.GRANDE, FonteZebra.GRANDE)
                        C2Y += 3
                    End If
                Next

            End If

        End Sub



        'Private Shared Sub ImprimeDenominacion(codDenominacion As String, cantidad As Double, posCod As Integer, posUnid As Integer, posY As Integer, tamMaxColumna As Integer, fuenteDenom As FonteZebra, fuenteValor As FonteZebra)

        '    PrintText(codDenominacion, fuenteDenom, posCod, posY, tamMaxColumna)
        '    If cantidad = -1 Then
        '        PrintText(String.Empty, FonteZebra.MINUSCULO, posUnid, tamMaxColumna)
        '    Else
        '        PrintText(ValidaMoneySeparator(cantidad), fuenteValor, posUnid, posY, tamMaxColumna)
        '    End If

        'End Sub

        Private Shared Sub ImprimeDenominacion(codDenominacion As String, cantidad As String, posCod As Integer, posUnid As Integer, posY As Integer, tamMaxColumna As Integer, fuenteDenom As FonteZebra, fuenteValor As FonteZebra)

            PrintText(codDenominacion, fuenteDenom, posCod, posY, tamMaxColumna)
            If cantidad = -1 Then
                PrintText(String.Empty, FonteZebra.MINUSCULO, posUnid, tamMaxColumna)
            Else
                PrintText(cantidad, fuenteValor, posUnid, posY, tamMaxColumna)
            End If

        End Sub

        Private Shared Sub PrintLine(ptoInicial As Point, Optional Hlength As Integer = 2, Optional VLength As Integer = 2, Optional colorLinea As CorLinha = CorLinha.Preto)

            printer.DesenhaLinha(ptoInicial, Hlength, VLength, colorLinea)

        End Sub

        Private Shared Sub PrintText(texto As String, font As FonteZebra, PosXLinea As Integer, tamMaxColumna As Integer, Optional alineacion As Alinhamento = Alinhamento.Esquerda)

            printer.X = PosXLinea
            printer.EscreveTexto(texto, font, alineacion, tamMaxColumna)

        End Sub



        Private Shared Sub PrintText(texto As String, font As FonteZebra, PosXLinea As Integer, PosYLinea As Integer, tamMaxColumna As Integer, Optional alineacion As Alinhamento = Alinhamento.Esquerda)

            printer.X = PosXLinea
            printer.Y = PosYLinea
            printer.EscreveTexto(texto, font, alineacion, tamMaxColumna)

        End Sub

        Private Shared Function ConvertMM2DPI(mm As Double) As Double
            Return TicketImpressao.ConverteParaDpi(mm)
        End Function

        'Private Shared Function GetTextoLinea(texto As String, maxCantCaracteres As Integer, Optional index As Integer = 0) As String

        '    Dim extraer As Integer = texto.Length - index
        '    If maxCantCaracteres < extraer Then
        '        extraer = maxCantCaracteres
        '    End If
        '    If extraer > 0 Then
        '        texto = texto.Substring(index, extraer)
        '    Else
        '        texto = String.Empty
        '    End If
        '    Return texto
        'End Function

        ''' <summary>
        ''' Retorna las lineas indicadas, de acuerdo a la máxima cantidad de caracteres permitidos por lineas.
        ''' Intenta devolver palabras completas, solo en caso que la palabra no encuadre en una linea, será truncada.
        ''' </summary>
        ''' <param name="texto">texto a tratar</param>
        ''' <param name="maxCantCaracteres"> maxima cantidad de caracteres por linea</param>
        ''' <param name="devolverLinea"> Indica la cantidad de lineas a devolver {0- todas las lineas; 1- solo 1 linea</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function GetTexto(texto As String, maxCantCaracteres As Integer, Optional devolverLinea As Integer = 0) As String()

            If maxCantCaracteres <= 0 OrElse devolverLinea < 0 Then

                Return Nothing

            End If

            Dim res As New List(Of String)

            If texto.Length <= maxCantCaracteres Then

                res.Add(texto)

            Else

                Dim cantLineasPosibles As Integer = Math.Ceiling(texto.Length / maxCantCaracteres)

                If devolverLinea = 0 Then
                    devolverLinea = cantLineasPosibles
                End If

                Dim temp() As String = texto.Split(" ")

                Dim linea As String = String.Empty

                For i = 0 To temp.Length - 1 Step 1

                    If i > 0 AndAlso linea.Length > 0 Then
                        linea &= " "
                    End If

                    If linea.Length + temp(i).Length <= maxCantCaracteres Then
                        linea &= temp(i)

                        If linea.Length = maxCantCaracteres Then
                            res.Add(linea)
                            linea = String.Empty
                        End If

                    Else

                        If linea.Length > 0 Then
                            res.Add(linea)
                            If temp(i).Length > maxCantCaracteres Then
                                linea = temp(i).Substring(0, maxCantCaracteres)
                                res.Add(linea)
                                linea = String.Empty
                            Else
                                linea = temp(i)
                            End If

                        Else
                            'si la palabra en temp(i) por si sola es mas grande que la cantidad de caracteres permitidos en la linea, solo tomo los permitidos.
                            linea = temp(i).Substring(0, maxCantCaracteres)
                            res.Add(linea)
                            linea = String.Empty
                        End If

                    End If

                    If res.Count = devolverLinea Then
                        Exit For
                    End If

                Next

                If linea.Length > 0 AndAlso res.Count < devolverLinea Then
                    res.Add(linea)
                End If

            End If

            Return res.ToArray()

        End Function

        Private Shared Function ValidaMoneySeparator(money As Double) As String
            Dim aux As Double = Math.Truncate(money) 'Extrae la parte entera
            Dim valor As String = String.Empty

            If (money - aux) > 0 Then
                valor = FormatNumber(money, 2) 'El separador de miles y decimal depende de la cultura del equipo. 2 valores decimales
            Else
                valor = FormatNumber(money, 0) 'El separador de miles y decimal depende de la cultura del equipo. Sin decimales
            End If

            Return valor

        End Function

        Private Shared Function GeneraTotalToPrint(bulto As Parametros.Bulto, Optional esTotalBulto As Boolean = False) As String
            Dim total As Double = 0
            Dim totalBultoORemesa As String = IIf(esTotalBulto, bulto.TotalBulto, bulto.TotalRemesa)

            If Not String.IsNullOrEmpty(totalBultoORemesa) Then

                If Not Double.TryParse(totalBultoORemesa, total) Then
                    total = 0
                    'intentar separar y localizar el valor; Ejemplo de fallo: "10.000,00 EUR"- Ocurre cuando la remesa está en moneda.
                    Dim aux As String() = totalBultoORemesa.Split(" ")

                    If aux.Count >= 1 Then
                        If Not Double.TryParse(aux(0), total) Then
                            Throw New System.Exception("Valor total de remesa con formato incorrecto")
                        End If
                    Else
                        Throw New System.Exception("Valor total de remesa con formato incorrecto")
                    End If

                End If

            End If

            'PrintText(GetTextoLinea("Tot Commande :" & ValidaMoneySeparator(total), tamMaxCol1), FonteZebra.MEDIO, XPosEscrituraColumna1, Convert.ToInt32(TicketImpressao.ConverteParaDpi(46)), tamMaxCol1)
            Dim totalToPrint As String = ValidaMoneySeparator(total)
            If Not IsNothing(bulto.DenominacionBillete) AndAlso bulto.DenominacionBillete.Count > 0 Then
                totalToPrint &= bulto.DenominacionBillete(0).CodigoSimbolo
            Else
                If Not IsNothing(bulto.DenominacionMoneda) AndAlso bulto.DenominacionMoneda.Count > 0 Then
                    totalToPrint &= bulto.DenominacionMoneda(0).CodigoSimbolo
                End If
            End If

            Return totalToPrint
        End Function


    End Class

End Namespace