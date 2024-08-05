Imports System.Drawing
Imports System.Math
Imports System.Threading.Interlocked
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.LogicaNegocio

Public Class ucHelperBusquedaAvanzada
    Inherits PopupBase

#Region "[PROPRIEDADES]"
    Public identificadorFormulario As String

    ''' <summary>
    ''' Valor del código
    ''' </summary> 
    Public Property Codigo As String

    ''' <summary>
    ''' Valor de la Descrición
    ''' </summary>
    Public Property Descricao As String

    ''' <summary>
    ''' Define Tabela a ser carregada  no Controle Helper.
    ''' </summary>
    Public Property Tabela As Tabela

    ''' <summary>
    ''' Título a ser exibido no filtro da busca.
    ''' </summary>
    Public Property ValorLabelFiltro As String

    ''' <summary>
    ''' Título a ser exibido no resultado da busca.
    ''' </summary>
    Public Property ValorLabelResultado As String

    ''' <summary>
    ''' Determina se será possível selecionar mais de um resultado no Grid.
    ''' </summary>
    Public Property esMultiSelecao As Boolean

    ''' <summary>
    ''' Determina o número de registros a serem exibidos no Grid.
    ''' </summary>
    Public _MaxRegistroPorPagina As Integer
    Public Property MaxRegistroPorPagina As Integer
        Get
            If _MaxRegistroPorPagina <= 0 Then
                _MaxRegistroPorPagina = 10
            End If
            Return _MaxRegistroPorPagina
        End Get
        Set(value As Integer)
            _MaxRegistroPorPagina = value
        End Set
    End Property

    ''' <summary>
    ''' Define o tipo de Ordenação a ser incluída na consulta.
    ''' </summary>
    Public Property OrderConsulta As Dictionary(Of String, UtilHelper.OrderSQL)

    ''' <summary>
    ''' Define os filtros a serem incluídos na consulta.
    ''' </summary>    
    Public Property FiltroConsulta As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property FiltroAvanzadoConsulta As Dictionary(Of String, String)

    ''' <summary>
    ''' Define as junções a serem incluídas na consulta.
    ''' </summary>
    Public Property JoinConsulta As Dictionary(Of String, UtilHelper.JoinSQL)

    ''' <summary>
    ''' Define a query para a pesquisa do dados do controle helper.
    ''' </summary>
    Public Property QueryDefault As UtilHelper.QueryHelperControl

    Public Property PaginaInicial As Boolean = False

    Public Property consultaAvancada As Boolean
        Get
            If ViewState(ID & "_consultaAvancada") Is Nothing Then
                ViewState(ID & "_consultaAvancada") = False
            End If
            Return ViewState(ID & "_consultaAvancada")
        End Get
        Set(value As Boolean)
            ViewState(ID & "_consultaAvancada") = value
        End Set
    End Property

    ''' <summary>
    ''' Mantém informações do itens selecionados no Grid.
    ''' </summary>
    Public Property RegistrosSelecionados As RespuestaHelper
        Get
            If ViewState(ID & "_RegistrosSelecionados") Is Nothing Then
                ViewState(ID & "_RegistrosSelecionados") = New RespuestaHelper With {.DatosRespuesta = New List(Of Prosegur.Genesis.Comon.Helper.Respuesta)}
            End If
            Return ViewState(ID & "_RegistrosSelecionados")
        End Get
        Set(value As RespuestaHelper)
            ViewState(ID & "_RegistrosSelecionados") = value
        End Set
    End Property

    ''' <summary>
    ''' Mantém informações do itens selecionados no Grid.
    ''' </summary>
    Public Property RegistrosTemporarios As List(Of Prosegur.Genesis.Comon.Helper.Respuesta)
        Get
            If ViewState(ID & "_RegistrosTemporarios") Is Nothing Then
                ViewState(ID & "_RegistrosTemporarios") = RegistrosSelecionados.DatosRespuesta
            End If
            Return ViewState(ID & "_RegistrosTemporarios")
        End Get
        Set(value As List(Of Prosegur.Genesis.Comon.Helper.Respuesta))
            ViewState(ID & "_RegistrosTemporarios") = value
        End Set
    End Property

    ''' <summary>
    ''' Caso a consulta retorne apenas um registro, não é para exibir o popup
    ''' </summary>
    Private _noExibirPopUp As Boolean = False
    Public Property noExibirPopUp() As Boolean
        Get
            Return _noExibirPopUp
        End Get
        Set(value As Boolean)
            _noExibirPopUp = value
        End Set
    End Property

    Public Property eventoExecutado As EventoPopup
        Get
            If ViewState(ID & "_eventoExecutado") Is Nothing Then
                ViewState(ID & "_eventoExecutado") = False
            End If
            Return ViewState(ID & "_eventoExecutado")
        End Get
        Set(value As EventoPopup)
            ViewState(ID & "_eventoExecutado") = value
        End Set
    End Property

    Private WithEvents _ucConsiderarHijos As ucRadioButtonList
    Public Property ucConsiderarHijos() As ucRadioButtonList
        Get
            If _ucConsiderarHijos Is Nothing Then
                _ucConsiderarHijos = LoadControl("~\Controles\ucRadioButtonList.ascx")
                _ucConsiderarHijos.ID = Me.ID & "_ucConsiderarHijos"
                _ucConsiderarHijos.Titulo = Traduzir("062_ConsiderarHijos")
                _ucConsiderarHijos.AutoPostBack = True
                AddHandler _ucConsiderarHijos.SelectedIndexChanged, AddressOf ucConsiderarHijos_SelectedIndexChanged
                Me.phConsiderarHijos.Controls.Add(_ucConsiderarHijos)
            End If
            Return _ucConsiderarHijos
        End Get
        Set(value As ucRadioButtonList)
            _ucConsiderarHijos = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Configura Controles da Tela.
    ''' </summary>
    Protected Overrides Sub TraduzirControles()

        MyBase.TraduzirControles()

        ' Configura controles do formulário de busca.
        lblFiltro.Text = Me.ValorLabelFiltro
        lblCodigo.Text = Traduzir("014_codigo")
        lblDescripcion.Text = Traduzir("014_descricion")
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpiar.Text = Traduzir("btnLimpar")

        ' Configura os controles de Resultado.
        lblResultado.Text = Me.ValorLabelResultado
        btnCancelar.Text = Traduzir("btnCancelar")
        btnAceptar.Text = Traduzir("btnAceitar")
        chkConsiderarSectoresHijos.Text = Traduzir("014_ConsiderarSectoresHijos")

    End Sub

    ' ''' <summary>
    ' ''' Define sequência de tabulação dos campos da tela.
    ' ''' </summary>
    'Public Overrides Sub ConfigurarTabIndexControle()
    '    Aplicacao.Util.Utilidad.ConfigurarTabIndex(txtCodigo, txtDescripcion, btnBuscar, btnLimpiar, gdvResultadoBusqueda, btnAceptar, btnCancelar)
    'End Sub

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Método referente ao evento de carregamento da página.
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of RespuestaHelper).SetupGridViewPaginacion(gdvResultadoBusqueda,
                                                                AddressOf PopularGridResultado, Function(p) p.DatosRespuesta, True)

            ConfigurarCampos()
            If Me.ID = "ucSector_ucBusquedaAvanzada" Then
                cargarTiposSectores()
            End If

        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método referente ao evento de fechamento do Popup.
    ''' </summary>
    Protected Sub Page_FechadoAtualizar(sender As Object, e As PopupEventArgs) Handles Me.FechadoAtualizar
        Try
            Me.eventoExecutado = EventoPopup.FecharClick
            Me.RegistrosSelecionados.ResultadoModificado = False
            Me.FecharPopup(RegistrosSelecionados)
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método referente ao evento click do botão limpar campos.
    ''' </summary>
    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        Try
            Me.eventoExecutado = EventoPopup.LimpiarClick

            Me.consultaAvancada = True
            Me.LimparCampos()
            gdvResultadoBusqueda.DataBind()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método referente ao evento click do botão de busca.
    ''' </summary>
    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try
            Me.eventoExecutado = EventoPopup.BuscarClick
            Me.consultaAvancada = True
            Me.TratarListaSelecionados()
            gdvResultadoBusqueda.DataBind()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método referente ao evento click do botão aceitar a seleção dos registros no Grid.
    ''' </summary>
    Protected Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click
        Try
            If (Me.eventoExecutado <> EventoPopup.CodigoTextChanged AndAlso Me.eventoExecutado <> EventoPopup.DescripcionTextChanged) Then
                Me.eventoExecutado = EventoPopup.AceptarClick
            End If

            Me.TratarListaSelecionados()
            Me.FecharPopup(RegistrosSelecionados)
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método referente ao evento click do botão cancelar a busca avançada.
    ''' </summary>
    Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        Try
            RegistrosSelecionados.ResultadoModificado = False
            FecharPopup(RegistrosSelecionados)
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub


    Private Sub gdvResultadoBusqueda_PageIndexChanged(sender As Object, e As System.EventArgs) Handles gdvResultadoBusqueda.PageIndexChanging
        consultaAvancada = True
        TratarListaSelecionados()
    End Sub

    ''' <summary>
    ''' Vincula as informações de Resposta aos controles do Grid.
    ''' </summary>
    Protected Sub gdvResultadoBusqueda_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdvResultadoBusqueda.RowDataBound
        Try
            ' Preenche Cabeçalho das colunas do Grid.
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Text = String.Empty
                e.Row.Cells(1).Text = Traduzir("014_codigo").Replace(":", "")
                e.Row.Cells(2).Text = Traduzir("014_descricion").Replace(":", "")
            End If

            ' Preenche linhas do Grid.
            If (e.Row.RowType = DataControlRowType.DataRow) Then
                Dim item As Prosegur.Genesis.Comon.Helper.Respuesta = e.Row.DataItem
                Dim lblItemCodigo As Label = e.Row.FindControl("lblItemCodigo")
                Dim lblItemDescricao As Label = e.Row.FindControl("lblItemDescricao")

                lblItemCodigo.Text = item.Codigo
                lblItemCodigo.ToolTip = lblItemCodigo.Text

                lblItemDescricao.Text = item.Descricao
                lblItemDescricao.ToolTip = lblItemDescricao.Text

                Dim rbSelecionado As HtmlInputRadioButton = e.Row.FindControl("rbSelecionado")
                Dim chkSelecionado As CheckBox = e.Row.FindControl("chkSelecionado")

                If (Me.esMultiSelecao) Then
                    rbSelecionado.Visible = False
                    chkSelecionado.Visible = True
                Else
                    rbSelecionado.Visible = True
                    chkSelecionado.Visible = False
                End If

                If (RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso RegistrosSelecionados.DatosRespuesta.Count > 0 AndAlso
                    RegistrosSelecionados.DatosRespuesta.Where(Function(f) f.Identificador = item.Identificador).Count > 0) Then
                    If (Me.esMultiSelecao) Then
                        chkSelecionado.Checked = True
                    Else
                        rbSelecionado.Checked = True
                    End If
                End If
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Popula GridView com informações retornadas da consulta a base de dados.
    ''' </summary>
    Protected Sub PopularGridResultado(sender As Object, e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of RespuestaHelper))

        ' Prepara o objeto Petición
        Dim objPeticion As PeticionHelper = Me.ObtenerObjetoPeticion()

        ' If Me.Page.

        objPeticion.ParametrosPaginacion.RealizarPaginacion = True
        If Not PaginaInicial Then
            objPeticion.ParametrosPaginacion.IndicePagina = e.PaginaAtual
        Else
            objPeticion.ParametrosPaginacion.IndicePagina = 0
        End If

        objPeticion.ParametrosPaginacion.RegistrosPorPagina = Me.MaxRegistroPorPagina

        ' Busca Resultado
        e.RespuestaPaginacion = Classes.Helper.Busqueda(objPeticion)

        If e.RespuestaPaginacion.DatosRespuesta IsNot Nothing AndAlso e.RespuestaPaginacion.DatosRespuesta.Count = 1 Then
            noExibirPopUp = True
        End If

        ' Configura visibilidade do botão Aceitar
        Dim display = If(e.RespuestaPaginacion.DatosRespuesta IsNot Nothing AndAlso e.RespuestaPaginacion.DatosRespuesta.Count > 0, "'block'", "'none'")
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "displayElemento_" & Me.ClientID, "displayElemento('divBtnAceptar'," + display + ");", True)
    End Sub

    Private Sub ConfigurarCampos()
        If RegistrosSelecionados IsNot Nothing AndAlso RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso RegistrosSelecionados.DatosRespuesta.Count = 1 Then
            txtCodigo.Text = RegistrosSelecionados.DatosRespuesta(0).Codigo
            txtDescripcion.Text = RegistrosSelecionados.DatosRespuesta(0).Descricao

        End If
        txtCodigo.Focus()
    End Sub

    ''' <summary>
    ''' Elimina informações dos campos da tela.
    ''' </summary>
    Protected Sub LimparCampos()
        Try
            Me.txtCodigo.Text = String.Empty
            Me.txtDescripcion.Text = String.Empty
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Elimina informações armazenadas no ViewState.
    ''' </summary>
    Public Sub LimparMemoria()
        Me.LimparCampos()
        Me.consultaAvancada = Nothing
        Me.Codigo = Nothing
        Me.Descricao = Nothing
        Me.RegistrosSelecionados = Nothing
        Me.RegistrosTemporarios = Nothing
    End Sub

    ''' <summary>
    ''' Implementa objeto do tipo Peticion.
    ''' </summary>
    Protected Function ObtenerObjetoPeticion() As PeticionHelper

        Dim objPeticion As New PeticionHelper

        If consultaAvancada Then
            objPeticion.Codigo = Me.txtCodigo.Text
            objPeticion.Descripcion = txtDescripcion.Text
        Else
            objPeticion.Codigo = Codigo
            objPeticion.Descripcion = Descricao

            txtCodigo.Text = Codigo
            txtDescripcion.Text = Descricao

            ' Obtém os filtros a serem incluídos na consulta.
            If RegistrosTemporarios IsNot Nothing AndAlso RegistrosTemporarios.Count > 0 Then
                objPeticion.DadosPeticao = New List(Of Peticion)
                For Each selecionado In RegistrosTemporarios
                    objPeticion.DadosPeticao.Add(New Peticion With {.Codigo = selecionado.Codigo,
                                                                 .Descricao = selecionado.Descricao,
                                                                 .Identificador = selecionado.Identificador,
                                                                 .IdentificadorPai = selecionado.IdentificadorPai
                                                                })
                Next
            End If

        End If

        objPeticion.Tabela = Me.Tabela
        objPeticion.Query = Me.QueryDefault

        '******** EXECUTAR SOMENTE SE FOR SECTOR *******************************************************************
        If Me.ID = "ucSector_ucBusquedaAvanzada" Then
            Dim strItens As List(Of String)
            If cklTiposSectores IsNot Nothing AndAlso cklTiposSectores.Items.Count > 0 Then

                If cklTiposSectores IsNot Nothing AndAlso (From tb As ListItem In cklTiposSectores.Items Where tb.Selected).Count > 0 Then
                    'Removo todos itens de tipo sector do filtro e adiciono novamente somente os marcados
                    For Each itemNaoSelecionado In cklTiposSectores.Items
                        Dim valorFiltroRemover As New Prosegur.Genesis.Comon.UtilHelper.ArgumentosFiltro With {.NomeColuna = "OID_TIPO_SECTOR", .TipoCondicaoFiltro = Prosegur.Genesis.Comon.Helper.Enumeradores.EnumHelper.TipoCondicion.Igual, .ValorFiltro = DirectCast(itemNaoSelecionado, System.Web.UI.WebControls.ListItem).Value}
                        Me.FiltroConsulta.Item(New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Sector}).Remove(valorFiltroRemover)
                    Next

                    'Adicionando os itens marcados
                    strItens = (From tb As ListItem In cklTiposSectores.Items Where tb.Selected Select tb.Value).ToList()
                    If strItens IsNot Nothing Then
                        For Each itemSelecionado In strItens
                            Dim valorFiltro As New Prosegur.Genesis.Comon.UtilHelper.ArgumentosFiltro With {.NomeColuna = "OID_TIPO_SECTOR", .TipoCondicaoFiltro = Prosegur.Genesis.Comon.Helper.Enumeradores.EnumHelper.TipoCondicion.Igual, .ValorFiltro = itemSelecionado}
                            Me.FiltroConsulta.Item(New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Sector}).Add(valorFiltro)
                        Next
                    End If
                End If

            End If

            Dim table = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Sector
            If Not Me.FiltroConsulta.ContainsKey(New UtilHelper.Tabela With {.Tabela = table}) Then
                table = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.MaquinaSector
            End If

            If chkConsiderarSectoresHijos IsNot Nothing AndAlso Not chkConsiderarSectoresHijos.Checked Then
                Dim valorFiltro As New Prosegur.Genesis.Comon.UtilHelper.ArgumentosFiltro With {.NomeColuna = "OID_SECTOR_PADRE", .TipoCondicaoFiltro = Prosegur.Genesis.Comon.Helper.Enumeradores.EnumHelper.TipoCondicion.Avancado, .ValorFiltro = "IS NULL"}

                Me.FiltroConsulta.Item(New UtilHelper.Tabela With {.Tabela = table}).Add(valorFiltro)
            Else
                Dim valorFiltro As New Prosegur.Genesis.Comon.UtilHelper.ArgumentosFiltro With {.NomeColuna = "OID_SECTOR_PADRE", .TipoCondicaoFiltro = Prosegur.Genesis.Comon.Helper.Enumeradores.EnumHelper.TipoCondicion.Avancado, .ValorFiltro = "IS NULL"}
                Me.FiltroConsulta.Item(New UtilHelper.Tabela With {.Tabela = table}).Remove(valorFiltro)

            End If
        End If
        '**************************************************************************************************************

        objPeticion.FiltroSQL = If(Me.FiltroConsulta Is Nothing, Me.FiltroConsulta, Me.FiltroConsulta.ToSerializableDictionary(Function(k) k.Key, Function(v) v.Value))
        objPeticion.FiltroAvanzadoSQL = If(Me.FiltroAvanzadoConsulta Is Nothing, Me.FiltroAvanzadoConsulta, Me.FiltroAvanzadoConsulta.ToSerializableDictionary(Function(k) k.Key, Function(v) v.Value))
        objPeticion.JuncaoSQL = If(Me.JoinConsulta Is Nothing, Me.JoinConsulta, Me.JoinConsulta.ToSerializableDictionary(Function(k) k.Key, Function(v) v.Value))
        objPeticion.OrdenacaoSQL = If(Me.OrderConsulta Is Nothing, Me.OrderConsulta, Me.OrderConsulta.ToSerializableDictionary(Function(k) k.Key, Function(v) v.Value))
        objPeticion.ParametrosPaginacion = New Genesis.Comon.Paginacion.ParametrosPeticionPaginacion()

        Return objPeticion

    End Function

    ''' <summary>
    ''' Método auxiliar para tratar os valores que deverão permanecer na lista de registros selecionados,
    ''' durante a marcação/desmarcação dos registros entre as paginações no Grid.
    ''' </summary>
    Protected Sub TratarListaSelecionados(Optional retornarTodos As Boolean = False)

        ' Guarda itens selecionados no Grid.
        For Each r As GridViewRow In gdvResultadoBusqueda.Rows
            Dim rLocal = r

            If (DirectCast(gdvResultadoBusqueda.Rows(r.RowIndex).Cells(0).FindControl("chkSelecionado"), CheckBox).Checked _
                OrElse DirectCast(gdvResultadoBusqueda.Rows(r.RowIndex).Cells(0).FindControl("rbSelecionado"), HtmlInputRadioButton).Checked OrElse retornarTodos) Then

                Dim item As New Prosegur.Genesis.Comon.Helper.Respuesta
                With item
                    .Identificador = gdvResultadoBusqueda.DataKeys(r.RowIndex).Values(0).ToString()
                    .Codigo = gdvResultadoBusqueda.DataKeys(r.RowIndex).Values(1).ToString()
                    .Descricao = gdvResultadoBusqueda.DataKeys(r.RowIndex).Values(2).ToString()
                    If (gdvResultadoBusqueda.DataKeys(r.RowIndex).Values(3) IsNot Nothing) Then .IdentificadorPai = gdvResultadoBusqueda.DataKeys(r.RowIndex).Values(3).ToString()
                End With
                If RegistrosSelecionados.DatosRespuesta.FindAll(Function(x) x.Identificador = item.Identificador).Count < 1 Then
                    If Not esMultiSelecao Then
                        RegistrosSelecionados.DatosRespuesta.Clear()
                    End If
                    RegistrosSelecionados.DatosRespuesta.Add(item)
                End If
            Else
                RegistrosSelecionados.DatosRespuesta.RemoveAll(Function(x) (x.Identificador = gdvResultadoBusqueda.DataKeys(rLocal.RowIndex).Values(0).ToString() AndAlso
                                                                   x.Codigo = gdvResultadoBusqueda.DataKeys(rLocal.RowIndex).Values(1).ToString()))
            End If
        Next
        RegistrosSelecionados.ResultadoModificado = True
    End Sub

    Public Sub ActualizarBusqueda()
        Me.PaginaInicial = True
        gdvResultadoBusqueda.DataBind()
        If noExibirPopUp Then
            TratarListaSelecionados(True)
            If Me.eventoExecutado <> EventoPopup.BusquedaClick Then
                Me.FecharPopup(RegistrosSelecionados)
            End If
        End If
    End Sub

    Private Sub cargarTiposSectores()
        Try
            Dim accionTiposSectores As New IAC.LogicaNegocio.AccionTipoSetor
            Dim peticionTipoSector As New IAC.ContractoServicio.TipoSetor.GetTiposSectores.Peticion
            peticionTipoSector.ParametrosPaginacion.RealizarPaginacion = False

            If Me.ID = "ucSector_ucBusquedaAvanzada" Then
                If Not String.IsNullOrEmpty(Request.QueryString("IdentificadorFormulario")) Then

                    Dim objSector As List(Of Prosegur.Genesis.Comon.Clases.Sector) = Base.InformacionUsuario.Delegaciones.Where(Function(d) d.Plantas IsNot Nothing).SelectMany(Function(d) d.Plantas).Where(Function(p) p.Sectores IsNot Nothing).SelectMany(Function(p) p.Sectores).Where(Function(s) s.TipoSector IsNot Nothing).ToList
                    Dim tipoSector = Prosegur.Genesis.LogicaNegocio.Genesis.TipoSector.ObtenerTiposSectores(Request.QueryString("IdentificadorFormulario"), "D")

                    If tipoSector IsNot Nothing AndAlso tipoSector.Count > 0 Then
                        Dim contador As Integer = 0
                        For Each ts In tipoSector
                            If ts.EstaActivo Then
                                cklTiposSectores.Items.Add(New ListItem(ts.Descripcion, ts.Identificador))
                                cklTiposSectores.Items(contador).Selected = True
                                contador = contador + 1

                            End If
                        Next

                    End If
                Else

                    Dim oidDelegaciones As List(Of String) = Nothing
                    Dim oidPlantas As List(Of String) = Nothing
                    If Me.JoinConsulta IsNot Nothing AndAlso Me.JoinConsulta.Count > 0 Then
                        oidDelegaciones = Me.JoinConsulta.Values.Where(Function(d) d.NomeCampoChave.ToUpper = "OID_DELEGACION").Select(Function(x) x.ValorCampoChave).FirstOrDefault.Split(",").ToList
                        oidPlantas = Me.JoinConsulta.Values.Where(Function(d) d.NomeCampoChave.ToUpper = "OID_PLANTA").Select(Function(x) x.ValorCampoChave).FirstOrDefault.Split(",").ToList
                    End If

                    If oidDelegaciones IsNot Nothing AndAlso oidDelegaciones.Count > 0 Then
                        Dim tipoSector = Prosegur.Genesis.LogicaNegocio.Genesis.TipoSector.ObtenerPorIdentificadores(oidDelegaciones, oidPlantas)
                        If tipoSector IsNot Nothing AndAlso tipoSector.Count > 0 Then
                            cklTiposSectores.Items.Clear()
                            For Each TP In tipoSector
                                If TP.EstaActivo Then

                                    cklTiposSectores.Items.Add((New ListItem With {.Text = TP.Descripcion, .Value = TP.Identificador, .Selected = True}))
                                End If
                            Next
                        End If
                    Else
                        Dim tipoSector = accionTiposSectores.GetTiposSectores(peticionTipoSector).TipoSetor
                        Dim objSector As List(Of Prosegur.Genesis.Comon.Clases.Sector) = Base.InformacionUsuario.Delegaciones.Where(Function(d) d.Plantas IsNot Nothing).SelectMany(Function(d) d.Plantas).Where(Function(p) p.Sectores IsNot Nothing).SelectMany(Function(p) p.Sectores).Where(Function(s) s.TipoSector IsNot Nothing).ToList

                        If tipoSector IsNot Nothing AndAlso tipoSector.Count > 0 Then

                            cklTiposSectores.Items.Clear()
                            Dim contador As Integer = 0
                            For Each TP In tipoSector
                                If objSector IsNot Nothing AndAlso TP.bolActivo Then
                                    If objSector.FindAll(Function(k) k.TipoSector.Identificador = TP.oidTipoSector).FirstOrDefault IsNot Nothing Then
                                        cklTiposSectores.Items.Add((New ListItem With {.Text = TP.desTipoSector, .Value = TP.oidTipoSector}))
                                        cklTiposSectores.Items(contador).Selected = True
                                        contador = contador + 1
                                    End If
                                End If
                            Next

                        End If
                    End If
                End If
            End If


        Catch ex As Exception
            'MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

#Region "GENERAL"
    Protected Sub ucConsiderarHijos_SelectedIndexChanged()
        Me.ucConsiderarHijos.GuardarDatos()
        If Not String.IsNullOrEmpty(Me.ucConsiderarHijos.ItemSelecionado) Then
            Dim discriminarPor = RecuperarEnum(Of Enumeradores.DiscriminarPor)(Me.ucConsiderarHijos.ItemSelecionado)
            Me.chkConsiderarSectoresHijos.Enabled = (discriminarPor = Enumeradores.DiscriminarPor.Cuenta OrElse discriminarPor = Enumeradores.DiscriminarPor.Sector)
        Else
            Me.chkConsiderarSectoresHijos.Enabled = False
        End If
    End Sub
#End Region

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If Me.ID = "ucSector_ucBusquedaAvanzada" Then
            dvBotoes.Style.Value = "width: 360px;position:absolute;z-index:999;margin:80px 22px 40px 320px;"
            dvbotaobuscar.Style.Value = "margin: 08px 05px 05px 0px; height: auto; border: 5px solid groove;padding-left: 150px;"
            dvTipoSectores.Visible = True
            dvSector.Visible = True
        Else
            dvbotaobuscar.Style.Value = "margin: 08px 05px 05px 0px; height: auto; border: 5px solid groove;"
            dvTipoSectores.Visible = False
            dvSector.Visible = False
        End If

    End Sub
End Class

Public Enum EventoPopup
    NaoDefinido
    BusquedaClick
    DescripcionTextChanged
    CodigoTextChanged
    LimpiarClick
    BuscarClick
    FecharClick
    AceptarClick
End Enum


