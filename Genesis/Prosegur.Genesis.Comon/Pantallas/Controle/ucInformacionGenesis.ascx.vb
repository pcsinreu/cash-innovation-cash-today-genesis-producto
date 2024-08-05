Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports Prosegur.Framework.Dicionario.Tradutor


Namespace Pantallas.Controles

    'Como usar:
    '<%@ Register TagPrefix="info" Namespace="Prosegur.Genesis.Comon.Pantallas.Controles" Assembly="Prosegur.Genesis.Comon.Pantallas" %>
    '<info:ucInformacionGenesis ID="informacionGenesis" runat="server" />        

    Public Class ucInformacionGenesis
        Inherits System.Web.UI.UserControl

        Private Property ResultadoGrid As List(Of Clases.Assembly)
            Get
                Return Session("ResultadoGrid")
            End Get
            Set(value As List(Of Clases.Assembly))
                Session("ResultadoGrid") = value
            End Set
        End Property

        Public Property Diccionario As Dictionary(Of String, String)
            Get
                Return Session("Diccionario")
            End Get
            Set(value As Dictionary(Of String, String))
                Session("Diccionario") = value
            End Set
        End Property


        Private _colFileName As Integer = 0
        Private _colFileVersion As Integer = 1
        Private _colProductVersion As Integer = 2
        Private _colLastWrite As Integer = 3

#Region "[OVERRIDES]"

        Protected Sub TraduzirControles()
            grvResultado.Columns(_colFileName).HeaderText = Traduzir("InformacionGenesis_grid_FileName")
            grvResultado.Columns(_colFileVersion).HeaderText = Traduzir("InformacionGenesis_grid_FileVersion")
            grvResultado.Columns(_colProductVersion).HeaderText = Traduzir("InformacionGenesis_grid_ProductVersion")
            If Me.Diccionario IsNot Nothing AndAlso Diccionario.ContainsKey("InformacionGenesis_grid_LastWrite") Then
                grvResultado.Columns(_colLastWrite).HeaderText = Diccionario("InformacionGenesis_grid_LastWrite")
            Else
                grvResultado.Columns(_colLastWrite).HeaderText = Traduzir("DateModified")
            End If
        End Sub

#End Region

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            If Request.QueryString("NOME_ARQUIVO") IsNot Nothing Then
                If Session(Request.QueryString("NOME_ARQUIVO")) IsNot Nothing Then
                    Dim Buffer As Byte() = Session(Request.QueryString("NOME_ARQUIVO"))
                    Response.ClearHeaders()
                    Response.ClearContent()
                    Response.Clear()
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", Request.QueryString("NOME_ARQUIVO")))
                    Response.AddHeader("Content-Length", Buffer.Length)
                    Response.BinaryWrite(Buffer)

                    'Limpa a sessão
                    Session(Request.QueryString("NOME_ARQUIVO")) = Nothing

                    Response.Buffer = True
                    Response.Flush()
                    Response.Clear()
                    Response.End()
                End If
            Else
                ConfiguraGrid()
                TraduzirControles()

                If Not Me.IsPostBack Then
                    CarregarGrid()
                End If
            End If
        End Sub

        Private Sub ConfiguraGrid()
            grvResultado = New WebControls.GridView()
            grvResultado.ID = "grvResultado"
            grvResultado.AutoGenerateColumns = False
            grvResultado.BorderStyle = WebControls.BorderStyle.None
            grvResultado.Width = WebControls.Unit.Parse("97%")
            grvResultado.EnableModelValidation = False
            grvResultado.Columns.Add(New WebControls.BoundField With {.DataField = "Name"})
            grvResultado.Columns(_colFileName).ItemStyle.HorizontalAlign = WebControls.HorizontalAlign.Left
            grvResultado.Columns.Add(New WebControls.BoundField With {.DataField = "Version"})
            grvResultado.Columns(_colFileVersion).ItemStyle.HorizontalAlign = WebControls.HorizontalAlign.Center
            grvResultado.Columns.Add(New WebControls.BoundField With {.DataField = "Build"})
            grvResultado.Columns(_colProductVersion).ItemStyle.HorizontalAlign = WebControls.HorizontalAlign.Center
            grvResultado.Columns.Add(New WebControls.BoundField With {.DataField = "FechaHora"})
            grvResultado.Columns(_colLastWrite).ItemStyle.HorizontalAlign = WebControls.HorizontalAlign.Center


            AddHandler grvResultado.RowDataBound, AddressOf grvResultado_RowDataBound

            For Each column As WebControls.BoundField In grvResultado.Columns
                column.HeaderStyle.Font.Size = WebControls.FontUnit.Parse("11px")
                column.ItemStyle.ForeColor = Drawing.Color.FromName("#767676")
            Next

            Me.Controls.Add(grvResultado)
        End Sub

        Private Sub CarregarGrid()
            If ResultadoGrid Is Nothing Then
                ResultadoGrid = Genesis.Comon.Util.RetornaDLLsAssembly(Path.Combine(HttpRuntime.AppDomainAppPath, "bin"))
            End If

            grvResultado.DataSource = ResultadoGrid
            grvResultado.DataBind()
        End Sub

        Public Sub Exportar(aplicacion As String, usuario As String, codDelegacion As String, Optional codSector As String = Nothing, Optional codPuesto As String = Nothing, Optional host As String = Nothing, Optional url As String = Nothing)
            Dim FileName As String = String.Format("Info_{0}_{1}.txt", aplicacion, DateTime.Now.ToString("yyMMddHHmmss"))

            Dim strConteudoArquivo = Genesis.Comon.Util.RetornaInformacionGenesis(ResultadoGrid,
                                                                                  aplicacion,
                                                                                  usuario,
                                                                                  codDelegacion,
                                                                                  codSector,
                                                                                  codPuesto,
                                                                                  host,
                                                                                  url)

            Session(FileName) = System.Text.Encoding.UTF8.GetBytes(strConteudoArquivo)
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "download", String.Format("window.location.href = '{1}?NOME_ARQUIVO={0}'", FileName, HttpContext.Current.Request.Url.AbsolutePath), True)
        End Sub


        Protected Sub grvResultado_RowDataBound(sender As Object, e As WebControls.GridViewRowEventArgs)
            If e.Row.RowIndex >= 0 Then
                e.Row.Cells(_colFileName).Text = e.Row.Cells(_colFileName).Text.Split("\")(e.Row.Cells(_colFileName).Text.Split("\").Count - 1)
            End If
        End Sub
    End Class

End Namespace