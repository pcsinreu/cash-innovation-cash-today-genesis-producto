Imports Prosegur.Global.GesEfectivo.Conteo.ContractoServicio
Imports Prosegur.Global.GesEfectivo.ATM.ContractoServicio.Integracion
Imports Prosegur.Global.GesEfectivo.Salidas.ContractoServicio.Divisa
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Net
Imports Prosegur.Genesis

Public Class ResultadoSincronizacion
    Inherits System.Web.UI.UserControl

#Region "[CONSTANTES]"
    Private Const DENOMINACION_SIN_ERROR As String = "OK"
#End Region

#Region "[METODOS]"

    Private Sub TraduzirControles()

        lblResultado.Text = Traduzir("005_lbl_resultado")
        lblUrl.Text = Traduzir("005_lbl_url")

    End Sub

    ''' <summary>
    ''' Carrega os dados da atualização
    ''' </summary>
    ''' <param name="Resultado"></param>
    ''' <param name="url"></param>
    ''' <remarks></remarks>
    Public Sub CarregarDados(ByVal Resultado As SincronizarDivisasConteo.Respuesta, ByVal url As String)

        Dim msgError As String = String.Empty

        If Resultado.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT AndAlso (Resultado.Divisas Is Nothing OrElse Resultado.Divisas.Count = 0) Then
            msgError = Traduzir("005_msg_error_geral")
        ElseIf Resultado.Divisas IsNot Nothing AndAlso Resultado.Divisas.Count > 0 Then

            For Each div In Resultado.Divisas

                If div.CodError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    msgError = Traduzir("005_msg_error_denominacion")
                    Exit For
                End If

                If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 AndAlso _
                   (From den In div.Denominaciones Where den.Status <> DENOMINACION_SIN_ERROR).Count > 0 Then msgError = Traduzir("005_msg_error_denominacion")

            Next

        End If

        Dim urlValida As Boolean = UrlIsValid(url)

        lblResultadoMostrar.Text = If(Not String.IsNullOrEmpty(msgError), msgError, Traduzir("005_msg_sin_error"))
        lblUrlMostrar.Text = If(urlValida, url, Traduzir("005_msg_url"))

        Dim tabela As Table = Nothing
        Dim linha As TableRow = Nothing
        Dim coluna As TableCell = Nothing

        If Resultado.Divisas IsNot Nothing AndAlso Resultado.Divisas.Count > 0 Then

            tabela = New Table
            tabela.BorderWidth = New Unit(0, UnitType.Point)
            tabela.Width = New Unit(90, UnitType.Percentage)
            tabela.CellPadding = 0
            tabela.CellSpacing = 0

            linha = New TableRow
            linha.Height = 20
            linha.Cells.Add(CriarColunaDetalhe(String.Empty, "#FFFFFF", False, 0, False, , "left", 4))
            tabela.Rows.Add(linha)

            For Each div In Resultado.Divisas

                linha = New TableRow
                linha.Cells.Add(CriarColunaDetalhe(String.Format(Traduzir("005_lbl_divisa"), div.DescripcionDivisa & " - " & div.CodIsoDivisa), "#FFFFFF", True, 0, False, , "left", 4))
                tabela.Rows.Add(linha)

                If Not String.IsNullOrEmpty(div.MensajeError) Then
                    linha = New TableRow
                    linha.Cells.Add(CriarColunaDetalhe(String.Format(Traduzir("005_msg_error"), div.MensajeError), "#FFFFFF", True, 0, False, , "left", 4))
                    tabela.Rows.Add(linha)
                End If

                If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 Then

                    linha = New TableRow
                    linha.BorderWidth = New Unit(1, UnitType.Point)
                    linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_cod_denominacion"), "#F4E854", True, 1, False, 25, "left"))
                    linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_des_denominacion"), "#F4E854", True, 1, False, 25, "left"))
                    linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_cod_erro"), "#F4E854", True, 1, False, 25, "left"))
                    linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_des_erro"), "#F4E854", True, 1, False, 25, "left"))

                    tabela.Rows.Add(linha)

                    Dim alterarCor As Boolean = False

                    For Each den In div.Denominaciones

                        linha = New TableRow
                        linha.BorderWidth = New Unit(1, UnitType.Point)
                        linha.Cells.Add(CriarColunaDetalhe(den.CodDenominacion, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))
                        linha.Cells.Add(CriarColunaDetalhe(den.DescripcionDenominacion, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))
                        linha.Cells.Add(CriarColunaDetalhe(den.numeroValor, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))
                        linha.Cells.Add(CriarColunaDetalhe(den.Status, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))

                        alterarCor = Not alterarCor

                        tabela.Rows.Add(linha)
                    Next

                End If

                linha = New TableRow
                linha.Height = 20
                linha.Cells.Add(CriarColunaDetalhe(String.Empty, "#FFFFFF", False, 0, False, , "center", 4))
                tabela.Rows.Add(linha)

            Next

        ElseIf urlValida AndAlso Resultado.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

            tabela = New Table
            tabela.BorderWidth = New Unit(0, UnitType.Point)
            tabela.Width = New Unit(90, UnitType.Percentage)
            tabela.CellPadding = 0
            tabela.CellSpacing = 0

            linha = New TableRow
            linha.Cells.Add(CriarColunaDetalhe(Resultado.MensajeError, "#FFFFFF", False, 0, True, , "left", 4))
            tabela.Rows.Add(linha)

        End If

        If tabela IsNot Nothing Then pnlResultado.Controls.Add(tabela)

    End Sub

    ''' <summary>
    ''' Carrega os dados da atualização
    ''' </summary>
    ''' <param name="Resultado"></param>
    ''' <param name="url"></param>
    ''' <remarks></remarks>
    Public Sub CarregarDados(ByVal Resultado As SincronizarDivisasSalidas.Respuesta, ByVal url As String)

        Dim msgError As String = String.Empty

        If Resultado.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT AndAlso (Resultado.Divisas Is Nothing OrElse Resultado.Divisas.Count = 0) Then
            msgError = Traduzir("005_msg_error_geral")
        ElseIf Resultado.Divisas IsNot Nothing AndAlso Resultado.Divisas.Count > 0 Then

            For Each div In Resultado.Divisas

                If div.CodError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    msgError = Traduzir("005_msg_error_denominacion")
                    Exit For
                End If

                If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 AndAlso _
                   (From den In div.Denominaciones Where den.Status <> DENOMINACION_SIN_ERROR).Count > 0 Then msgError = Traduzir("005_msg_error_denominacion")

            Next

        End If

        Dim urlValida As Boolean = UrlIsValid(url)

        lblResultadoMostrar.Text = If(Not String.IsNullOrEmpty(msgError), msgError, Traduzir("005_msg_sin_error"))
        lblUrlMostrar.Text = If(urlValida, url, Traduzir("005_msg_url"))

        Dim tabela As Table = Nothing
        Dim linha As TableRow = Nothing
        Dim coluna As TableCell = Nothing

        If Resultado.Divisas IsNot Nothing AndAlso Resultado.Divisas.Count > 0 Then

            tabela = New Table
            tabela.BorderWidth = New Unit(0, UnitType.Point)
            tabela.Width = New Unit(90, UnitType.Percentage)
            tabela.CellPadding = 0
            tabela.CellSpacing = 0

            linha = New TableRow
            linha.Height = 20
            linha.Cells.Add(CriarColunaDetalhe(String.Empty, "#FFFFFF", False, 0, False, , "left", 4))
            tabela.Rows.Add(linha)

            For Each div In Resultado.Divisas

                linha = New TableRow
                linha.Cells.Add(CriarColunaDetalhe(String.Format(Traduzir("005_lbl_divisa"), div.DescripcionDivisa & " - " & div.CodIsoDivisa), "#FFFFFF", True, 0, False, , "left", 4))
                tabela.Rows.Add(linha)


                If Not String.IsNullOrEmpty(div.MensajeError) Then
                    linha = New TableRow
                    linha.Cells.Add(CriarColunaDetalhe(String.Format(Traduzir("005_msg_error"), div.MensajeError), "#FFFFFF", True, 0, False, , "left", 4))
                    tabela.Rows.Add(linha)
                End If

                If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 Then


                    linha = New TableRow
                    linha.BorderWidth = New Unit(1, UnitType.Point)
                    linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_cod_denominacion"), "#F4E854", True, 1, False, 25, "left"))
                    linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_des_denominacion"), "#F4E854", True, 1, False, 25, "left"))
                    linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_valor"), "#F4E854", True, 1, False, 25, "left"))
                    linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_status"), "#F4E854", True, 1, False, 25, "left"))

                    tabela.Rows.Add(linha)

                    Dim alterarCor As Boolean = False

                    For Each den In div.Denominaciones

                        linha = New TableRow
                        linha.BorderWidth = New Unit(1, UnitType.Point)
                        linha.Cells.Add(CriarColunaDetalhe(den.CodDenominacion, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))
                        linha.Cells.Add(CriarColunaDetalhe(den.DescripcionDenominacion, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))
                        linha.Cells.Add(CriarColunaDetalhe(den.numeroValor, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))
                        linha.Cells.Add(CriarColunaDetalhe(den.Status, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))

                        alterarCor = Not alterarCor

                        tabela.Rows.Add(linha)
                    Next

                End If

                linha = New TableRow
                linha.Height = 20
                linha.Cells.Add(CriarColunaDetalhe(String.Empty, "#FFFFFF", False, 0, False, , "center", 4))
                tabela.Rows.Add(linha)

            Next

        ElseIf urlValida AndAlso Resultado.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

            tabela = New Table
            tabela.BorderWidth = New Unit(0, UnitType.Point)
            tabela.Width = New Unit(90, UnitType.Percentage)
            tabela.CellPadding = 0
            tabela.CellSpacing = 0

            linha = New TableRow
            linha.Cells.Add(CriarColunaDetalhe(Resultado.MensajeError, "#FFFFFF", False, 0, True, , "left", 4))
            tabela.Rows.Add(linha)
        End If

        If tabela IsNot Nothing Then pnlResultado.Controls.Add(tabela)

    End Sub

    ' ''' <summary>
    ' ''' Carrega os dados da atualização
    ' ''' </summary>
    ' ''' <param name="Resultado"></param>
    ' ''' <param name="url"></param>
    ' ''' <remarks></remarks>
    'Public Sub CarregarDados(ByVal Resultado As SincronizarDivisasATM.Respuesta, ByVal url As String)

    '    Dim msgError As String = String.Empty

    '    If Resultado.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT AndAlso (Resultado.Divisas Is Nothing OrElse Resultado.Divisas.Count = 0) Then
    '        msgError = Traduzir("005_msg_error_geral")
    '    ElseIf Resultado.Divisas IsNot Nothing AndAlso Resultado.Divisas.Count > 0 Then

    '        For Each div In Resultado.Divisas

    '            If div.CodError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
    '                msgError = Traduzir("005_msg_error_denominacion")
    '                Exit For
    '            End If

    '            If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 AndAlso _
    '               (From den In div.Denominaciones Where den.Status <> DENOMINACION_SIN_ERROR).Count > 0 Then msgError = Traduzir("005_msg_error_denominacion")

    '        Next

    '    End If

    '    Dim urlValida As Boolean = UrlIsValid(url)

    '    lblResultadoMostrar.Text = If(Not String.IsNullOrEmpty(msgError), msgError, Traduzir("005_msg_sin_error"))
    '    lblUrlMostrar.Text = If(urlValida, url, Traduzir("005_msg_url"))

    '    Dim tabela As Table = Nothing
    '    Dim linha As TableRow = Nothing
    '    Dim coluna As TableCell = Nothing

    '    If Resultado.Divisas IsNot Nothing AndAlso Resultado.Divisas.Count > 0 Then

    '        tabela = New Table
    '        tabela.BorderWidth = New Unit(0, UnitType.Point)
    '        tabela.Width = New Unit(90, UnitType.Percentage)
    '        tabela.CellPadding = 0
    '        tabela.CellSpacing = 0

    '        linha = New TableRow
    '        linha.Height = 20
    '        linha.Cells.Add(CriarColunaDetalhe(String.Empty, "#FFFFFF", False, 0, False, , "left", 4))
    '        tabela.Rows.Add(linha)

    '        For Each div In Resultado.Divisas

    '            linha = New TableRow
    '            linha.Cells.Add(CriarColunaDetalhe(String.Format(Traduzir("005_lbl_divisa"), div.DescripcionDivisa & " - " & div.CodIsoDivisa), "#FFFFFF", True, 0, False, , "left", 4))
    '            tabela.Rows.Add(linha)

    '            If Not String.IsNullOrEmpty(div.MensajeError) Then
    '                linha = New TableRow
    '                linha.Cells.Add(CriarColunaDetalhe(String.Format(Traduzir("005_msg_error"), div.MensajeError), "#FFFFFF", True, 0, False, , "left", 4))
    '                tabela.Rows.Add(linha)
    '            End If

    '            If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 Then

    '                linha = New TableRow
    '                linha.BorderWidth = New Unit(1, UnitType.Point)
    '                linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_cod_denominacion"), "#F4E854", True, 1, False, 25, "left"))
    '                linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_des_denominacion"), "#F4E854", True, 1, False, 25, "left"))
    '                linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_valor"), "#F4E854", True, 1, False, 25, "left"))
    '                linha.Cells.Add(CriarColunaDetalhe(Traduzir("005_lbl_status"), "#F4E854", True, 1, False, 25, "left"))

    '                tabela.Rows.Add(linha)

    '                Dim alterarCor As Boolean = False

    '                For Each den In div.Denominaciones

    '                    linha = New TableRow
    '                    linha.BorderWidth = New Unit(1, UnitType.Point)
    '                    linha.Cells.Add(CriarColunaDetalhe(den.CodDenominacion, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))
    '                    linha.Cells.Add(CriarColunaDetalhe(den.DescripcionDenominacion, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))
    '                    linha.Cells.Add(CriarColunaDetalhe(den.numeroValor, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))
    '                    linha.Cells.Add(CriarColunaDetalhe(den.Status, If(alterarCor, "#FFFFCC", "#FFFFFF"), False, 1, False, 25, "center"))

    '                    alterarCor = Not alterarCor

    '                    tabela.Rows.Add(linha)
    '                Next

    '            End If

    '            linha = New TableRow
    '            linha.Height = 20
    '            linha.Cells.Add(CriarColunaDetalhe(String.Empty, "#FFFFFF", False, 0, False, , "center", 4))
    '            tabela.Rows.Add(linha)

    '        Next

    '    ElseIf urlValida AndAlso Resultado.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

    '        tabela = New Table
    '        tabela.BorderWidth = New Unit(0, UnitType.Point)
    '        tabela.Width = New Unit(90, UnitType.Percentage)
    '        tabela.CellPadding = 0
    '        tabela.CellSpacing = 0

    '        linha = New TableRow
    '        linha.Cells.Add(CriarColunaDetalhe(Resultado.MensajeError, "#FFFFFF", False, 0, True, , "left", 4))
    '        tabela.Rows.Add(linha)
    '    End If

    '    If tabela IsNot Nothing Then pnlResultado.Controls.Add(tabela)

    'End Sub

    ''' <summary>
    ''' Metodo responsavel por criar a coluna das tabelas
    ''' </summary>
    ''' <param name="valor"></param>
    ''' <param name="cor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CriarColunaDetalhe(ByVal valor As String, ByVal cor As String, ByVal negrito As Boolean, ByVal borda As Integer, ByVal negritoItalico As Boolean, Optional ByVal largura As Integer = 0, _
                                                                        Optional ByVal alinhamento As String = "center", Optional ByVal colSpan As Integer = 0) As TableCell

        Dim coluna As New TableCell

        coluna.Attributes.Add("bgcolor", cor)
        coluna.Attributes.Add("align", alinhamento)
        coluna.BorderWidth = New Unit(borda, UnitType.Point)

        If largura > 0 Then
            coluna.Width = New Unit(largura, UnitType.Percentage)
        End If

        If colSpan > 0 Then
            coluna.ColumnSpan = colSpan
        End If

        If negritoItalico Then
            coluna.Text = String.Format("<b><i>{0}</i></b>", valor)
        Else
            coluna.Text = If(negrito, String.Format("<b>{0}</b>", valor), valor)
        End If


        Return coluna
    End Function

    ''' <summary>
    ''' Valida se a url é valida
    ''' </summary>
    ''' <param name="url"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 22/02/2012 - Criado
    ''' </history>
    Private Function UrlIsValid(ByVal url As String) As Boolean
        Dim is_valid As Boolean = False
        If url.ToLower().StartsWith("www.") Then url = _
            "http://" & url

        Dim web_response As HttpWebResponse = Nothing
        Try
            Dim web_request As HttpWebRequest = _
                HttpWebRequest.Create(url)
            web_response = _
                DirectCast(web_request.GetResponse(),  _
                HttpWebResponse)
            Return True
        Catch ex As Exception
            Return False
        Finally
            If Not (web_response Is Nothing) Then _
                web_response.Close()
        End Try
    End Function


#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            TraduzirControles()
        End If

    End Sub

#End Region

End Class