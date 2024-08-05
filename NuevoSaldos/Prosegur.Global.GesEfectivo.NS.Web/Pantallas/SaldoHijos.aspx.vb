Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports System.Reflection
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Util

Public Class SaldoHijos
    Inherits Base

#Region "[PROPRIEDADES]"

    Private BuscarSaldo As Boolean = False

    Private _ClaveCuenta As String = Nothing
    Public ReadOnly Property ClaveCuenta() As String
        Get
            If String.IsNullOrEmpty(_ClaveCuenta) Then
                _ClaveCuenta = Request.QueryString("ClaveCuenta")
            End If
            Return _ClaveCuenta
        End Get
    End Property

    Public Property RespuestaSaldo() As Respuesta(Of List(Of DataRow))
        Get
            Return ViewState("_RespuestaSaldo")
        End Get
        Set(value As Respuesta(Of List(Of DataRow)))
            ViewState("_RespuestaSaldo") = value
        End Set
    End Property

    Public Property Filtro() As Clases.Transferencias.Filtro
        Get
            Return ViewState("_Filtro")
        End Get
        Set(value As Clases.Transferencias.Filtro)
            ViewState("_Filtro") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.GRUPO_DOCUMENTO
        MyBase.ValidarAcesso = True
        MyBase.ValidarPemissaoAD = True
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("032_popup_saldohijos_lblTitulo")
        Me.lblResultados.Text = Traduzir("032_popup_saldohijos_lblResultados")

        Me.grvResultadoSaldo.Columns(0).HeaderText = Traduzir("032_popup_saldohijos_canal")
        Me.grvResultadoSaldo.Columns(1).HeaderText = Traduzir("032_popup_saldohijos_cliente")
        Me.grvResultadoSaldo.Columns(2).HeaderText = Traduzir("032_popup_saldohijos_sector")
        Me.grvResultadoSaldo.Columns(3).HeaderText = Traduzir("032_popup_saldohijos_valor_disponible")

    End Sub

#End Region

#Region "[METODOS]"

    Private Sub ConsultaSaldoHijos()

        Me.BuscarSaldo = True
        grvResultadoSaldo.PageIndex = 0
        PopulaGridSaldo()
        grvResultadoSaldo.DataBind()

    End Sub

    Private Sub PopulaGridSaldo()

        Try
            Dim respuesta As Respuesta(Of List(Of DataRow))
            If Me.BuscarSaldo Then
                'Para não executar a busca ao entrar na página
                respuesta = ConsultaSaldo()
                Me.RespuestaSaldo = respuesta
            Else
                respuesta = Me.RespuestaSaldo
            End If

            If respuesta IsNot Nothing AndAlso respuesta.Retorno IsNot Nothing AndAlso respuesta.Retorno.Count > 0 Then
                Me.dvTituloResultado.Style.Item("display") = "block"
                Me.dvResultado.Style.Item("display") = "block"
                Me.grvResultadoSaldo.CssClass = "ui-datatable ui-datatable-data"

                Me.grvResultadoSaldo.DataSource = respuesta.Retorno

            Else
                If String.IsNullOrEmpty(ClaveCuenta) Then
                    Me.dvTituloResultado.Style.Item("display") = "block"
                End If
                Me.dvResultado.Style.Item("display") = "block"
                Me.grvResultadoSaldo.CssClass = ""

                If Me.BuscarSaldo Then
                    Me.BuscarSaldo = False
                    'Se não achou nenhum registro
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                       Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(Traduzir("062_no_hay_saldo"), Nothing), True)
                End If

            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Private Function ConsultaSaldo() As Respuesta(Of List(Of DataRow))

        Dim objRespuesta As New Respuesta(Of List(Of DataRow))
        objRespuesta.Retorno = LogicaNegocio.GenesisSaldos.Saldo.RecuperarSaldoCuentasDetallado(Me.Filtro, True, True)

        Return objRespuesta

    End Function

    Public Shared Function ObtenerVersion() As String
        Dim version = Assembly.GetExecutingAssembly.GetName.Version
        Return version.Build.ToString.PadLeft(4, "0"c) & version.Revision.ToString.PadLeft(4, "0"c)
    End Function

    Private Sub TratativaRowGrvResultado(e As System.Web.UI.WebControls.GridViewRowEventArgs)

        'Pega os dados do item atual.
        Dim Item As DataRow = e.Row.DataItem
        DirectCast(e.Row.FindControl("ValorDisponible"), Label).Text = Me.Filtro.Divisas.First.CodigoISO + " " + String.Format("{0:N}", AtribuirValorObj(Item("NUM_IMPORTE_DISP"), GetType(Double)))

        Dim canal As String = String.Empty

        canal = AtribuirValorObj(Item("COD_CANAL"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_CANAL"), GetType(String))
        canal = canal & " | " & AtribuirValorObj(Item("COD_SUBCANAL"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_SUBCANAL"), GetType(String))

        e.Row.Cells(0).Text = canal

        Dim cliente As String = String.Empty

        cliente = AtribuirValorObj(Item("COD_CLIENTE"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_CLIENTE"), GetType(String))

        If Not String.IsNullOrEmpty(AtribuirValorObj(Item("COD_SUBCLIENTE"), GetType(String))) Then
            cliente = cliente & " | " & AtribuirValorObj(Item("COD_SUBCLIENTE"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_SUBCLIENTE"), GetType(String))
        End If

        If Not String.IsNullOrEmpty(AtribuirValorObj(Item("COD_PTO_SERVICIO"), GetType(String))) Then
            cliente = cliente & " | " & AtribuirValorObj(Item("COD_PTO_SERVICIO"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_PTO_SERVICIO"), GetType(String))
        End If

        e.Row.Cells(1).Text = cliente

        e.Row.Cells(2).Text = AtribuirValorObj(Item("DES_SECTOR"), GetType(String))

    End Sub

    Public Shared Sub MergeRows(gridView As GridView, colunaInicial As Integer, colunaFinal As Integer)
        For rowIndex As Integer = gridView.Rows.Count - 2 To 0 Step -1
            Dim row As GridViewRow = gridView.Rows(rowIndex)
            Dim previousRow As GridViewRow = gridView.Rows(rowIndex + 1)

            For i As Integer = colunaInicial To colunaFinal
                If row.Cells(i).Text = previousRow.Cells(i).Text Then
                    row.Cells(i).RowSpan = If(previousRow.Cells(i).RowSpan < 2, 2, previousRow.Cells(i).RowSpan + 1)
                    previousRow.Cells(i).Visible = False
                End If
            Next
        Next
    End Sub

#End Region

#Region "[EVENTOS]"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ' Se foi chamada pela tela de Documentos não mostra os filtros
        If Not String.IsNullOrEmpty(ClaveCuenta) Then
            Master.HabilitarMenu = False
            Me.Filtro = Me.ObtenerCache(ClaveCuenta)
            ConsultaSaldoHijos()
        End If

    End Sub
    Private Sub grvResultadoSaldo_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grvResultadoSaldo.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                TratativaRowGrvResultado(e)
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub grvResultadoSaldo_PreRender(sender As Object, e As System.EventArgs) Handles grvResultadoSaldo.PreRender
        MergeRows(Me.grvResultadoSaldo, 0, 1)
    End Sub

#End Region
End Class