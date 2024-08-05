Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Drawing
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports System.Collections.ObjectModel

Public Class ConfiguracionFormularios
    Inherits Base


    Private WithEvents _ucSectores As UcMaquina
    Public Property ucSectores() As UcMaquina
        Get
            If _ucSectores Is Nothing Then
                _ucSectores = LoadControl("~\Controles\UcMaquina.ascx")
                _ucSectores.ID = "ucSectores"
                AddHandler _ucSectores.Erro, AddressOf ErroControles
                phSector.Controls.Add(_ucSectores)


            End If
            Return _ucSectores
        End Get
        Set(value As UcMaquina)
            _ucSectores = value
        End Set
    End Property


    Public Property Sectores As ObservableCollection(Of Clases.Sector)
        Get
            Return ucSectores.Sectores
        End Get
        Set(value As ObservableCollection(Of Clases.Sector))
            ucSectores.Sectores = value
        End Set
    End Property

    Public Property Delegaciones As ObservableCollection(Of Clases.Delegacion)
        Get
            Return ucSectores.Delegaciones
        End Get
        Set(value As ObservableCollection(Of Clases.Delegacion))
            ucSectores.Delegaciones = value
        End Set
    End Property


    Private _btnConfigurar As Boton
    Public ReadOnly Property btnConfigurar() As Boton
        Get
            If _btnConfigurar Is Nothing Then
                _btnConfigurar = LoadControl("~\Controles\Boton.ascx")
                _btnConfigurar.ID = Me.ID & "_btnConfigurar"
                AddHandler _btnConfigurar.Erro, AddressOf ErroControles
                phAcciones.Controls.Add(_btnConfigurar)
            End If
            Return _btnConfigurar
        End Get
    End Property

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Permite definir parametros da base antes de validar acessos e chamar o método inicializar.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CONFIGURACION_FORMULARIOS
        MyBase.ValidarAcesso = True
        MyBase.ValidarPemissaoAD = True
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        grvResultadoFormularios.Columns(1).HeaderText = Traduzir("031_Coluna_Codigo")
        grvResultadoFormularios.Columns(2).HeaderText = Traduzir("031_Coluna_Descricao")
        grvResultadoFormularios.Columns(3).HeaderText = Traduzir("044_Editar")
        grvResultadoFormularios.Columns(4).HeaderText = Traduzir("044_Exluir")
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        GoogleAnalyticsHelper.TrackAnalytics(Me, "Configuracion Formularios")
        Try


            ConfigurarControle_Sector()

            Me.ucSectores.Delegaciones = New ObservableCollection(Of Clases.Delegacion)
            Me.ucSectores.Delegaciones.Add(InformacionUsuario.DelegacionSeleccionada)
            Me.ucSectores.AtualizarRegistrosDelegacion()
            Me.ucSectores.DelegacionHabilitado = True




            Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of Respuesta(Of List(Of Clases.Formulario))).SetupGridViewPaginacion(grvResultadoFormularios, AddressOf PopulaGridView, Function(p) p.Retorno)

            Master.FindControl("pnlMenuRodape").Visible = True
            Master.Titulo = String.Format("{0} ", Traduzir("044_Titulo"))

            grvResultadoFormularios.PageSize = Aplicacao.Util.Utilidad.getMaximoRegistrosPageGrid(If(Session("MaximoRegistrosPageGrid") IsNot Nothing AndAlso IsNumeric(Session("MaximoRegistrosPageGrid")), Session("MaximoRegistrosPageGrid"), 0))

            ConfiguraAcciones()


        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    ''' <summary>
    ''' Evento ao clicar em Grupo no grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ImgEliminar_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)

        Try
            'Se o formulário não possuir nenhum documento
            If Not LogicaNegocio.GenesisSaldos.MaestroFormularios.FormularioHayDocumentos(e.CommandArgument) Then
                LogicaNegocio.GenesisSaldos.MaestroFormularios.ExcluirFormulario(e.CommandArgument)
                Response.Redireccionar("ConfiguracionFormularios.aspx")
            Else
                'Se o formulário possuir documentos
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro",
                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(Traduzir("044_FormularioHayDocumentos"), Nothing) _
                                                       , True)
            End If

        Catch ex As Exception
            Master.ControleErro.MostrarMensagemErro(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento que sera acionado a cada plotagem de linha do grvResultadoFormularios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grvResultadoFormularios_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvResultadoFormularios.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                TratativaRowGrvResultadoFormularios(e)
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub


    Public Sub ucSectores_OnControleAtualizado() Handles _ucSectores.UpdatedControl
        Try
            If Me.ucSectores.Delegaciones IsNot Nothing Then
                Delegaciones = Me.ucSectores.Delegaciones
            End If

            If Me.ucSectores.Sectores IsNot Nothing Then
                Sectores = Me.ucSectores.Sectores
            End If

            Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of Respuesta(Of List(Of Clases.Formulario))).SetupGridViewPaginacion(grvResultadoFormularios, AddressOf PopulaGridView, Function(p) p.Retorno)
            grvResultadoFormularios.DataBind()
            grvResultadoFormularios.PageSize = Aplicacao.Util.Utilidad.getMaximoRegistrosPageGrid(If(Session("MaximoRegistrosPageGrid") IsNot Nothing AndAlso IsNumeric(Session("MaximoRegistrosPageGrid")), Session("MaximoRegistrosPageGrid"), 0))

            ConfiguraAcciones()


        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub


#End Region

#Region "[METODOS]"

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Private Sub ConfiguraAcciones()

        AddHandler btnConfigurar.Click, AddressOf btnConfigurar_onAccionConfigurar
        btnConfigurar.Text = Traduzir("044_ConfigurarFormulario")
        btnConfigurar.ImageUrl = "~/App_Themes/Padrao/css/img/button/nuevo.png"

    End Sub
    Private Sub btnConfigurar_onAccionConfigurar()
        Try
            Response.Redireccionar("Formulario.aspx?IdentificadorFormulario=" & String.Empty & "&Modo=" & Enumeradores.Modo.Alta.ToString())
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Faz o tratamento das regras do Grid grvResultadoFormularios
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TratativaRowGrvResultadoFormularios(e As System.Web.UI.WebControls.GridViewRowEventArgs)

        'Pega os dados do item atual.
        Dim Item As Clases.Formulario = e.Row.DataItem

        'Verifica se existe icone para o item.
        'If Item.Icono IsNot Nothing Then
        '    Aplicacao.Util.Utilidad.CriarCachePorIdentificador("imagen", Item.Identificador, Item.Icono)
        '    CType(e.Row.FindControl("litImgIcono"), System.Web.UI.WebControls.Literal).Text = "<img src='" & String.Format("../Imagem.ashx?id={0}", Item.Identificador) & "' name='ImgIcono' alt='" & Traduzir("031_Coluna_Icone") & "' style='height:25px; width:25px;' />"
        'End If

        ' Botão Modificar
        CType(e.Row.FindControl("litImgModificar"), System.Web.UI.WebControls.Literal).Text = "<a href='Formulario.aspx?IdentificadorFormulario=" & Item.Identificador & "&Modo=" & Enumeradores.Modo.Modificacion.ToString() & "'><img src='../../Imagenes/Editar.png' name='ImgEditar' alt='" & Traduzir("044_Editar") & "' /></a>"

        'Botão Eliminar
        CType(e.Row.FindControl("ImgEliminar"), ImageButton).CommandArgument = Item.Identificador
        CType(e.Row.FindControl("ImgEliminar"), ImageButton).OnClientClick = "return confirm('" & Traduzir("044_ExcluirFormulario") & "')"

        If Item.Color <> Nothing AndAlso Not Item.Color.IsEmpty AndAlso Not String.IsNullOrEmpty(Item.Color.Name) Then
            e.Row.Cells(0).Style.Add("background", If(Item.Color.Name.Substring(0, 1).ToString <> "#", "#" & Item.Color.Name, Item.Color.Name) & " !important")
        End If
        'If (e.Row.RowIndex Mod 2) = 0 Then
        '    e.Row.Style.Add("background", "#e3e3e3 !important")
        'Else
        '    e.Row.Style.Add("background", "#ffffff !important")
        'End If

    End Sub

    ''' <summary>
    ''' Popula o Grid grvResultadoFormularios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopulaGridView(sender As Object, e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of Respuesta(Of List(Of Clases.Formulario))))

        Try
            If Sectores.Count > 0 Then

                Dim respuesta As Respuesta(Of List(Of Clases.Formulario))
                'Ativos e não ativos
                respuesta = PesquisaFormularios(e, Sectores(0).Identificador, True) 'Sectores(0).Identificador, True)
                If respuesta IsNot Nothing AndAlso respuesta.Retorno IsNot Nothing AndAlso respuesta.Retorno.Count > 0 Then
                    e.RespuestaPaginacion = respuesta
                Else
                    'Se não há formulário configurado para o setor selecionado.
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro",
                                                       Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(Traduzir("031_No_Hay_formulario_Configurado"), Nothing) _
                                                           , True)
                End If
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try


    End Sub

    ''' <summary>
    ''' Faz a pesquisa dos formularios existentes, seguindo os parâmetros passados.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="IdSector"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PesquisaFormularios(Optional e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of Respuesta(Of List(Of Clases.Formulario))) = Nothing, Optional IdSector As String = Nothing, Optional soloActivos As Boolean = False) As Respuesta(Of List(Of Clases.Formulario))

        Dim objPeticion As New Peticion(Of Clases.Sector)
        objPeticion.ParametrosPaginacion = New Paginacion.ParametrosPeticionPaginacion()
        Dim objRespuesta As Respuesta(Of List(Of Clases.Formulario))

        If e Is Nothing Then
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        Else
            objPeticion.ParametrosPaginacion.RegistrosPorPagina = Aplicacao.Util.Utilidad.getMaximoRegistrosPageGrid(If(Session("MaximoRegistrosPageGrid") IsNot Nothing AndAlso IsNumeric(Session("MaximoRegistrosPageGrid")), Session("MaximoRegistrosPageGrid"), 0))
            objPeticion.ParametrosPaginacion.IndicePagina = e.PaginaAtual
        End If

        objRespuesta = LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFormularioPaginacion(objPeticion)

        Return objRespuesta
    End Function

#End Region

#Region "[Helpers]"

    Protected Sub ConfigurarControle_Sector()
        Me.ucSectores.SolamenteSectoresPadre = True
        Me.ucSectores.SectorHabilitado = True
        Me.ucSectores.DelegacionHabilitado = True

        If Delegaciones IsNot Nothing Then
            Me.ucSectores.Delegaciones = Delegaciones
        End If
        If Sectores IsNot Nothing Then
            Me.ucSectores.Sectores = Sectores
        End If
    End Sub

#End Region


End Class

