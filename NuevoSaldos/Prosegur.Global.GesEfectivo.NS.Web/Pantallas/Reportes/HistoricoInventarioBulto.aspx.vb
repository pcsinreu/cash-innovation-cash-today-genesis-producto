Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Report
Imports System.Collections.ObjectModel

Public Class HistoricoInventarioBulto
    Inherits Base

#Region "[PROPRIEDADES]"

    Public Property Sector As Clases.Sector
        Get
            Return ucSectores.Sector
        End Get
        Set(value As Clases.Sector)
            ucSectores.Sector = value
        End Set
    End Property

    Private WithEvents _ucSectores As ucSector
    Public Property ucSectores() As ucSector
        Get
            If _ucSectores Is Nothing Then
                _ucSectores = LoadControl("~\Controles\ucSector.ascx")
                _ucSectores.ID = "ucSectores"
                AddHandler _ucSectores.Erro, AddressOf ErroControles
                phSector.Controls.Add(_ucSectores)
            End If
            Return _ucSectores
        End Get
        Set(value As ucSector)
            _ucSectores = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.HISTORICO_INVENTARIO_BULTO_CONSULTAR
        MyBase.ValidarAcesso = True
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("054_lblTitulo")
        Me.lblFiltro.Text = Traduzir("054_lblFiltro")
        Me.ucOrdenar.Titulo = Traduzir("054_OrdenarPor")
        Me.ucFormato.Titulo = Traduzir("054_formato")
        Me.ucFechaHoraDesde.Titulo = Traduzir("054_fecha_hora_desde")
        Me.ucFechaHoraHasta.Titulo = Traduzir("054_fecha_hora_hasta")
        Me.btnGenerarReporte.Text = Traduzir("054_btnGerar")
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(btnGenerarReporte)

        ConfigurarControle_Sector()

        If Not Me.IsPostBack Then
            AjustarControlOrdenar()
            AjustarControlFormato()
            AjustarControlCodigoInventario()
            Me.ucSectores.Focus()
        End If

        AddHandler Me.ucListaInventario.AtualizarFiltro, AddressOf AtualizarFiltro
        AddHandler Me.ucListaInventario.Erro, AddressOf ErroControles
        AddHandler Me.ucFechaHoraDesde.Erro, AddressOf ErroControles
        AddHandler Me.ucFechaHoraHasta.Erro, AddressOf ErroControles
    End Sub

    Protected Sub btnGenerarReporte_Click(sender As Object, e As System.EventArgs) Handles btnGenerarReporte.Click
        Try
            Me.ucOrdenar.GuardarDatos()
            Me.ucFormato.GuardarDatos()
            Me.ucFechaHoraDesde.GuardarDatos()
            Me.ucFechaHoraHasta.GuardarDatos()
            Me.ucCodigoInventario.GuardarDatos()

            Dim inventario = Me.ucListaInventario.RecuperarSelecionadoGrid()
            If inventario Is Nothing Then
                MyBase.MostraMensagemErro(Traduzir("052_NenhumInventarioSeleccionado"))
            Else

                ' Recupera os parametros do relatório.
                Dim objReport As New Prosegur.Genesis.Report.Gerar()
                objReport.Autenticar(False)

                Dim listaParametros2010 As List(Of RS2010.ItemParameter)
                Dim objValores2010 As RS2010.ParameterValue() = Nothing

                'Lista os parametros do relatório
                Dim dirRelatorio = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes")
                Dim nomeRelatorio As String = "INVENTARIO_BULTOS"
                Dim fullPathReport As String = Nothing

                ' Verificar se existe "/" no final da URL configurada para o Report
                If (dirRelatorio.Substring(dirRelatorio.Length - 1, 1) = "/") Then
                    dirRelatorio = dirRelatorio.Substring(0, dirRelatorio.Length - 1)
                End If

                fullPathReport = String.Format("{0}/INVENTARIO_BULTOS/{1}", dirRelatorio, nomeRelatorio)
                listaParametros2010 = objReport.ListarParametros(fullPathReport, objValores2010)

                Dim listaParametros As New List(Of RSE.ParameterValue)
                Dim bolCertificadoConsulta As Integer = 0

                'Recupera o pais da delegação que o usuário está logado.
                Dim pais As New Prosegur.Genesis.Comon.Clases.Pais
                If Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
                    pais = Prosegur.Genesis.LogicaNegocio.Genesis.Pais.ObtenerPaisPorDelegacion(Base.InformacionUsuario.DelegacionSeleccionada.Codigo)
                End If
                Dim ParamDelegacion = InformacionUsuario.DelegacionSeleccionada.Identificador

                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_ORDENACAO", .Value = Me.ucOrdenar.ItemSelecionado})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_NOME_PAIS", .Value = pais.Descripcion})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_LOGIN", .Value = InformacionUsuario.Nombre})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_OID_SECTOR", .Value = Nothing})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_DISCRIMINAR_SUBSECTORES", .Value = If(Me.chkDiscriminarSubSectores.Checked, 1, 0)})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_BULTO_CONTENEDOR", .Value = 1})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_INVENTARIO", .Value = inventario.Identificador})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "PARAM_DELEGACION", .Value = ParamDelegacion})

                Dim extensao = If(Me.ucFormato.ItemSelecionado = "PDF", "pdf", "xls")

                Dim Buffer = objReport.RenderReport(fullPathReport, Me.ucFormato.ItemSelecionado, listaParametros, extensao)
                Response.ClearHeaders()
                Response.ClearContent()
                Response.Clear()
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.{1}", nomeRelatorio, extensao))
                Response.AddHeader("Content-Length", Buffer.Length)
                Response.BinaryWrite(Buffer)
                Response.Buffer = True
                Response.Flush()
                Response.Clear()

                Response.End()
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

#End Region

#Region "[METODOS]"

    Protected Sub ConfigurarControle_Sector()
        Me.ucSectores.SolamenteSectoresPadre = True
        Me.ucSectores.SelecaoMultipla = False
        Me.ucSectores.SectorHabilitado = True
        Me.ucSectores.DelegacionHabilitado = True
        Me.ucSectores.PlantaHabilitado = True

        If Sector IsNot Nothing Then
            Me.ucSectores.Sector = Sector
        End If

    End Sub

    Private Sub AjustarControlOrdenar()
        Dim lista As New List(Of KeyValuePair(Of String, String))
        lista.Add(New KeyValuePair(Of String, String)("cliente", Traduzir("053_cliente_canal")))
        lista.Add(New KeyValuePair(Of String, String)("documento", Traduzir("053_documento")))
        lista.Add(New KeyValuePair(Of String, String)("ubicacion", Traduzir("053_ubicacion")))
        Me.ucOrdenar.Opciones = lista
    End Sub

    Private Sub AjustarControlFormato()
        Dim lista As New List(Of KeyValuePair(Of String, String))
        lista.Add(New KeyValuePair(Of String, String)("PDF", Traduzir("054_formato_pdf")))
        lista.Add(New KeyValuePair(Of String, String)("EXCEL", Traduzir("054_formato_excel")))
        Me.ucFormato.Opciones = lista
    End Sub

    Private Sub AjustarControlCodigoInventario()
        Me.ucCodigoInventario.Titulo = Traduzir("054_codigo_inventario")
        Me.ucCodigoInventario.TipoInterno = Genesis.Comon.Enumeradores.TiposInternos.Entero
    End Sub

    Private Sub AtualizarFiltro()
        Dim mensagem As String = String.Empty

        Me.ucOrdenar.GuardarDatos()
        Me.ucFormato.GuardarDatos()
        Me.ucFechaHoraDesde.GuardarDatos()
        Me.ucFechaHoraHasta.GuardarDatos()
        Me.ucCodigoInventario.GuardarDatos()

        Me.ucListaInventario.CodigoInventario = String.Empty
        Me.ucListaInventario.FechaDesde = Nothing
        Me.ucListaInventario.FechaHasta = Nothing
        Me.ucListaInventario.SectorSelecionado = Nothing

        If Me.ucCodigoInventario.Valor Is Nothing OrElse String.IsNullOrEmpty(Me.ucCodigoInventario.Valor) Then
            If Me.ucFechaHoraDesde.Valor Is Nothing OrElse String.IsNullOrEmpty(Me.ucFechaHoraDesde.Valor) Then
                mensagem = Traduzir("054_campoFechaHoraDesde_obrigatorio")
            Else
                Me.ucListaInventario.FechaDesde = Convert.ToDateTime(Me.ucFechaHoraDesde.Valor)
                Me.ucListaInventario.FechaHasta = Nothing
            End If

            If Me.ucFechaHoraHasta.Valor Is Nothing OrElse String.IsNullOrEmpty(Me.ucFechaHoraHasta.Valor) Then
                mensagem += "<br/>" + Traduzir("054_campoFechaHoraHasta_obrigatorio")
            Else
                Me.ucListaInventario.FechaHasta = Convert.ToDateTime(Me.ucFechaHoraHasta.Valor)
            End If

            Me.ucListaInventario.SectorSelecionado = Me.Sector

            If Not String.IsNullOrEmpty(mensagem) Then
                Throw New Exception(mensagem)
            End If
        Else
            Me.ucListaInventario.CodigoInventario = Me.ucCodigoInventario.Valor.ToString
        End If

        Me.ucListaInventario.Consultar = True
    End Sub

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(e.Erro.Message)
    End Sub

#End Region

End Class