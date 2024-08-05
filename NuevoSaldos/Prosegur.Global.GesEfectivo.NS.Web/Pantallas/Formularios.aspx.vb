Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Drawing
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports System.Collections.ObjectModel

Public Class Formularios
    Inherits Base

#Region "[PROPRIEDADES]"


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


    Private _IdentificadorSector As String = Nothing
    Public ReadOnly Property IdentificadorSector() As String
        Get
            If String.IsNullOrEmpty(_IdentificadorSector) Then
                _IdentificadorSector = Request.QueryString("IdentificadorSector")


            End If
            Return _IdentificadorSector
        End Get
    End Property


    Private _SectorSelecionado As Clases.Sector = Nothing
    Public ReadOnly Property SectorSelecionado() As Clases.Sector
        Get
            If _SectorSelecionado Is Nothing AndAlso IdentificadorSector IsNot Nothing Then
                _SectorSelecionado = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerPorOid(IdentificadorSector)
            End If
            Return _SectorSelecionado
        End Get
    End Property


#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Permite definir parametros da base antes de validar acessos e chamar o método inicializar.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.FORMULARIOS
        MyBase.ValidarAcesso = True
        MyBase.ValidarPemissaoAD = True
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        grvResultadoFormularios.Columns(0).HeaderText = Traduzir("031_Coluna_Icone")
        grvResultadoFormularios.Columns(1).HeaderText = Traduzir("031_Coluna_Codigo")
        grvResultadoFormularios.Columns(2).HeaderText = Traduzir("031_Coluna_Descricao")
        grvResultadoFormularios.Columns(3).HeaderText = Traduzir("031_Coluna_Individual")
        grvResultadoFormularios.Columns(4).HeaderText = Traduzir("031_Coluna_Grupo")
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        GoogleAnalyticsHelper.TrackAnalytics(Me, "Formularios")
        ConfigurarControle_Sector()
        Try
            If Not Page.IsPostBack Then 'Me.ucSectores Is Nothing 
                Me.ucSectores.Delegaciones = New ObservableCollection(Of Clases.Delegacion)

                If SectorSelecionado Is Nothing Then

                    Me.ucSectores.Delegaciones.Add(InformacionUsuario.DelegacionSeleccionada)
                Else
                    Me.ucSectores.Delegaciones.Add(SectorSelecionado.Delegacion)
                    Me.ucSectores.Sectores.Add(SectorSelecionado)

                    Me.ucSectores.AtualizarRegistrosSector()

                    Me.ucSectores.SectorHabilitado = True
                End If
                Me.ucSectores.AtualizarRegistrosDelegacion()
                Me.ucSectores.DelegacionHabilitado = True

            End If

            'Me.ucSectores.Sectores = New ObservableCollection(Of Clases.Sector)
            'Me.ucSectores.AtualizarRegistrosSector()
            Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of Respuesta(Of List(Of Clases.Formulario))).SetupGridViewPaginacion(grvResultadoFormularios, AddressOf PopulaGridView, Function(p) p.Retorno)

            Master.FindControl("pnlMenuRodape").Visible = False
            Master.Titulo = String.Format("{0} ", Traduzir("031_Titulo"))

            grvResultadoFormularios.PageSize = Aplicacao.Util.Utilidad.getMaximoRegistrosPageGrid(If(Session("MaximoRegistrosPageGrid") IsNot Nothing AndAlso IsNumeric(Session("MaximoRegistrosPageGrid")), Session("MaximoRegistrosPageGrid"), 0))


        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
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
            'Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of Respuesta(Of List(Of Clases.Formulario))).SetupGridViewPaginacion(grvResultadoFormularios, AddressOf PopulaGridView, Function(p) p.Retorno)
            grvResultadoFormularios.DataBind()

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(e.Erro.Message)
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
        If Item.Icono IsNot Nothing Then
            Aplicacao.Util.Utilidad.CriarCachePorIdentificador("imagen", Item.Identificador, Item.Icono)
            CType(e.Row.FindControl("litImgIcono"), System.Web.UI.WebControls.Literal).Text = "<img src='" & String.Format("../Imagem.ashx?id={0}", Item.Identificador) & "' name='ImgIcono' alt='" & Traduzir("031_Coluna_Icone") & "' style='height:25px; width:25px;' />"
        End If

        'Verifica se existem caracteristicas para o Item.
        If Item.Caracteristicas IsNot Nothing Then

            If Item.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoIndividual) Then
                CType(e.Row.FindControl("litImgIndividual"), System.Web.UI.WebControls.Literal).Text = "<a href='Documento.aspx?IdentificadorSector=" & Sectores(0).Identificador & "&IdentificadorFormulario=" & Item.Identificador & "&Modo=" & Enumeradores.Modo.Alta.ToString() & "'><img src='../Imagenes/ICO_DOCUMENTO_25x25.png' name='ImgIndividual' alt='" & Traduzir("031_Coluna_Individual") & "' /></a>"
            End If

            If Item.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoGrupo) Then
                CType(e.Row.FindControl("litImgGrupo"), System.Web.UI.WebControls.Literal).Text = "<a href='GrupoDocumento.aspx?IdentificadorSector=" & Sectores(0).Identificador & "&IdentificadorFormulario=" & Item.Identificador & "&Modo=" & Enumeradores.Modo.Alta.ToString() & "'><img src='../Imagenes/ICO_GRUPODOCUMENTOS_25x25.png' name='ImgGrupo' alt='" & Traduzir("031_Coluna_Grupo") & "' /></a>"
            End If

        End If

        If Item.Color <> Nothing AndAlso Not Item.Color.IsEmpty AndAlso Not String.IsNullOrEmpty(Item.Color.Name) Then
            e.Row.Style.Add("background", If(Item.Color.Name.Substring(0, 1).ToString <> "#", "#" & Item.Color.Name, Item.Color.Name) & " !important")
        End If

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
                respuesta = PesquisaFormularios(e, Sectores(0).Identificador, True)
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

        objPeticion.Parametro = New Clases.Sector

        objPeticion.Parametro.Identificador = IdSector
        objPeticion.Parametro.EsActivo = soloActivos

        Dim permitirFormulariosSustitucion = (From permiso In InformacionUsuario.Permisos
                                              Where permiso.Equals("PERMITE_SUSTITUCION")
                                              Select permiso).Count() > 0

        objRespuesta = LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFormularioPaginacion(objPeticion, permitirFormulariosSustitucion)

        Return objRespuesta
    End Function

#End Region

#Region "[HELPERS]"
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
        Me.ucSectores.ucMaquina.Titulo = "MAE"
    End Sub
#End Region

End Class

