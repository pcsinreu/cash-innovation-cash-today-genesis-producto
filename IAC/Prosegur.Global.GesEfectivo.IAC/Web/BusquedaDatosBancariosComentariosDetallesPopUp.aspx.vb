Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.DatoBancario.SetComentario

Public Class BusquedaDatosBancariosComentariosDetallesPopUp
    Inherits Base

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '
    End Sub

#Region "[Propriedades]"
    Private ReadOnly Property divModal() As String
        Get
            Return Request.QueryString("divModal")
        End Get
    End Property
    Private ReadOnly Property ifrModal() As String
        Get
            Return Request.QueryString("ifrModal")
        End Get
    End Property
    Private ReadOnly Property btnExecutar() As String
        Get
            Return Request.QueryString("btnExecutar")
        End Get
    End Property

#End Region

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.APROBACION_CUENTAS_BANCARIAS
        MyBase.ValidarAcao = False
        MyBase.ValidarPemissaoAD = False
    End Sub
    Protected Overrides Sub Inicializar()

        Try
            'Si abro el popup limpio la variable de sesion
            Session("Dto_Banc_Aprob_Comentario") = String.Empty
            If Not Page.IsPostBack Then
                txtComentario.Focus()
                Dim identificador = Request.QueryString("identificador")
                Dim respuesta = LogicaNegocio.AccionDatoBancario.GetComentarios(identificador)

                MostrarComentarios(respuesta)
            End If

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Private Sub MostrarComentarios(respuesta As Genesis.ContractoServicio.DatoBancario.GetComentario.Respuesta)


        'Muestro en el formato GMT de la delegación del usuario
        For Each mensaje In respuesta.MensajesAprobacion
            mensaje.Fecha = mensaje.Fecha.QuieroExibirEstaFechaEnLaPatalla(DelegacionLogada)
        Next

        For Each mensaje In respuesta.MensajesModificacion
            mensaje.Fecha = mensaje.Fecha.QuieroExibirEstaFechaEnLaPatalla(DelegacionLogada)
        Next

        gridComentariosAprobacion.DataSource = respuesta.MensajesAprobacion
        gridComentariosAprobacion.DataBind()

        gridComentariosModificacion.DataSource = respuesta.MensajesModificacion
        gridComentariosModificacion.DataBind()

    End Sub

    Protected Overrides Sub AdicionarScripts()
        Dim jsScript As String = "window.parent.FecharModal(" & Chr(34) & "#" & divModal & Chr(34) & "," & Chr(34) & "#" & ifrModal & Chr(34) & ",null);"
        btnCancelar.OnClientClick = jsScript
    End Sub

    Protected Overrides Sub TraduzirControles()


        MyBase.CodFuncionalidad = "PopUpComentariosDatoBanc"
        MyBase.CarregaDicinario()

        lblTituloComentarioPopup.Text = MyBase.RecuperarValorDic("lblTituloComentarioPopup")
        lblComentario.Text = MyBase.RecuperarValorDic("lblComentario")
        'Me.Page.Title = MyBase.RecuperarValorDic("lbl")

        'Botones
        btnGrabar.Text = MyBase.RecuperarValorDic("btnGrabar")
        btnGrabar.ToolTip = MyBase.RecuperarValorDic("btnGrabar")
        btnCancelar.Text = MyBase.RecuperarValorDic("btnCancelar")
        btnCancelar.ToolTip = MyBase.RecuperarValorDic("btnCancelar")


        'Grilla aprobación
        lblTituloComentariosAprobacionPopup.Text = MyBase.RecuperarValorDic("lblTituloComentariosAprobacionPopup")
        gridComentariosAprobacion.Columns(0).HeaderText = MyBase.RecuperarValorDic("Usuario_Aprobacion")
        gridComentariosAprobacion.Columns(1).HeaderText = MyBase.RecuperarValorDic("Fecha")
        gridComentariosAprobacion.Columns(2).HeaderText = MyBase.RecuperarValorDic("Comentarios")

        'Grilla modificación
        lblTituloComentariosModificaciónPopup.Text = MyBase.RecuperarValorDic("lblTituloComentariosModificaciónPopup")
        gridComentariosModificacion.Columns(0).HeaderText = MyBase.RecuperarValorDic("Usuario_Modificacion")
        gridComentariosModificacion.Columns(1).HeaderText = MyBase.RecuperarValorDic("Fecha")
        gridComentariosModificacion.Columns(2).HeaderText = MyBase.RecuperarValorDic("Comentarios")
    End Sub

    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Try
            LogicaNegocio.AccionDatoBancario.SetComentario(armarPeticion())
            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ComentarioOK", jsScript, True)
        Catch ex As Exception
            MostraMensagemErro(ex.Message, String.Empty)
        End Try
    End Sub

    Private Function armarPeticion() As Peticion
        Dim peticion As New Peticion

        peticion.Bol_HacerCommit = 1
        peticion.Cod_Usuario = LoginUsuario
        peticion.Oid_DatoBancario_Cambio = Request.QueryString("identificador")
        peticion.Oid_Tabla = Request.QueryString("identificador")
        peticion.Des_Comentario = txtComentario.Text
        peticion.Fecha = Now
        peticion.Des_Tabla = Comon.Helper.Enumeradores.Tabelas.TabelaHelper.DatoBancarioComentario.RecuperarValor()
        Return peticion
    End Function
End Class