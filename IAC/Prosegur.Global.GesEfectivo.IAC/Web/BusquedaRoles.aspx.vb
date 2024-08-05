Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.LogicaNegocio


Public Class BusquedaRoles
    Inherits Base

#Region "[PROPIEDADES]"
    Private Property rolesGrid As List(Of Clases.Rol)
        Get
            Return Session("rolesGrid")
        End Get
        Set(value As List(Of Clases.Rol))
            Session("rolesGrid") = value
        End Set
    End Property

    Private Property PermisosDisponibles As List(Of Clases.Permiso)
        Get
            Return Session("permisosDisponibles")
        End Get
        Set(value As List(Of Clases.Permiso))
            Session("permisosDisponibles") = value
        End Set
    End Property

    Private Property PermisosAsignados As List(Of Clases.Permiso)
        Get
            Return Session("permisosAsignados")
        End Get
        Set(value As List(Of Clases.Permiso))
            Session("permisosAsignados") = value
        End Set
    End Property

    Private Property IdentificadorDeRol As String
        Get
            Return Session("IdentificadorDeRol")
        End Get
        Set(value As String)
            Session("IdentificadorDeRol") = value
        End Set
    End Property
#End Region

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CONFIGURACION_ROLES
        MyBase.ValidarAcao = True
        MyBase.ValidarPemissaoAD = False
    End Sub
    Protected Overrides Sub Inicializar()

        Try

            GoogleAnalyticsHelper.TrackAnalytics(Me, "Configuracion de roles")

            DevExpress.Web.ASPxGridView.ASPxGridView.RegisterBaseScript(Page)

            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.MenuRodapeVisivel = True
            Master.MostrarRodape = True

            If Not Page.IsPostBack Then
                LimpiarForm()
                CargarAplicaciones()

            End If


        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try
    End Sub
    Protected Overrides Sub TraduzirControles()
        MyBase.CodFuncionalidad = "CONFIGURARROLES"
        CarregaDicinario()

        Master.Titulo = MyBase.RecuperarValorDic("lblBusquedaDeRoles")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("041_lbl_SubTitulosCriteriosBusqueda")

        lblTituloRoles.Text = MyBase.RecuperarValorDic("lblRoles")
        lblTituloMantenimientoRoles.Text = MyBase.RecuperarValorDic("lblMantenimientoRoles")
        lblTituloPermisos.Text = MyBase.RecuperarValorDic("lblPermisos")

        lblNombreRol.Text = MyBase.RecuperarValorDic("lblNombre")
        lblModulo.Text = MyBase.RecuperarValorDic("lblModulo")
        lblVigente.Text = MyBase.RecuperarValorDic("lblVigente")

        lblNombreForm.Text = MyBase.RecuperarValorDic("lblNombre")
        lblDescripcionForm.Text = MyBase.RecuperarValorDic("lblDescripcion")
        lblVigenteForm.Text = MyBase.RecuperarValorDic("lblVigente")
        lblModuloForm.Text = MyBase.RecuperarValorDic("lblModulo")

        lblPermisosDisponibles.Text = MyBase.RecuperarValorDic("lblPermisosDisponibles")
        lblPermisosAsignados.Text = MyBase.RecuperarValorDic("lblPermisosAsignados")


        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")

        btnNovo.Text = Traduzir("btnAlta")
        btnGrabar.Text = Traduzir("btnGrabar")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnCancelar.Text = Traduzir("btnCancelar")

        'Grid 
        grid.Columns(1).Caption = MyBase.RecuperarValorDic("lblModulo")
        grid.Columns(2).Caption = MyBase.RecuperarValorDic("lblNombre")
        grid.Columns(3).Caption = MyBase.RecuperarValorDic("lblVigente")
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        TraduzirControles()
        CargarGrilla()
    End Sub

    Private Sub Limpiar()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta
        txtNombreRol.Text = String.Empty
        ddlModulo.SelectedIndex = 0
        grid.DataSource = Nothing
        grid.DataBind()
        pnGrid.Visible = False
        IdentificadorDeRol = String.Empty
        LimpiarForm()
    End Sub

    Private Sub LimpiarForm()
        pnForm.Visible = False
        btnNovo.Enabled = True
        btnCancelar.Enabled = False
        btnBajaConfirm.Visible = False
        btnGrabar.Enabled = False
        chkVigente.Checked = True
        chkVigenteForm.Checked = True

        txtNombreRoleForm.Text = String.Empty
        txtDescRolForm.Text = String.Empty
        chkVigenteForm.Checked = True

        PermisosDisponibles = New List(Of Clases.Permiso)
        PermisosAsignados = New List(Of Clases.Permiso)

    End Sub

    Protected Function BuscaPostbackGrid(accion As String, identificador As String) As String

        Dim strPostBack As String = String.Empty
        strPostBack = "__doPostBack('" & btnGrid.UniqueID & "', '" & accion & "|" & identificador & "')"
        Return strPostBack
    End Function

    Protected Sub btnGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrid.Click
        Dim p = Page.Request.Params.Get("__EVENTARGUMENT")

        Dim params = p.Split("|")
        Dim codigoDeRol As String = params(1)

        Select Case params(0)
            Case "CONSULTAR"
                BtnConsultar(codigoDeRol)
            Case "ELIMINAR"
                BtnBaja(codigoDeRol)
            Case "EDITAR"
                BtnEditar(codigoDeRol)

        End Select
    End Sub

    Private Sub BtnEditar(codigo As String)
        Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion

        CargarFormularioRolXCodigo(codigo)

        pnForm.Visible = True
        btnGrabar.Enabled = True
        btnNovo.Enabled = True
        btnCancelar.Enabled = True

        txtNombreRoleForm.Enabled = False
        txtDescRolForm.Enabled = True
        chkVigenteForm.Enabled = Not chkVigenteForm.Checked

        divBotones.Visible = True
    End Sub
    Private Sub BtnConsultar(codigo As String)
        Acao = Aplicacao.Util.Utilidad.eAcao.Consulta

        CargarFormularioRolXCodigo(codigo)

        pnForm.Visible = True
        btnGrabar.Enabled = False
        btnNovo.Enabled = True
        btnCancelar.Enabled = True

        txtNombreRoleForm.Enabled = False
        txtDescRolForm.Enabled = False
        chkVigenteForm.Enabled = False

        divBotones.Visible = False

    End Sub

    Private Sub BtnBaja(codigo As String)
        Acao = Aplicacao.Util.Utilidad.eAcao.Baja

        CargarFormularioRolXCodigo(codigo)

        pnForm.Visible = True
        btnGrabar.Enabled = True
        btnNovo.Enabled = True
        btnCancelar.Enabled = True

        txtNombreRoleForm.Enabled = False
        txtDescRolForm.Enabled = False
        chkVigenteForm.Enabled = False

        divBotones.Visible = False


    End Sub
    Public Sub CargarFormularioRolXCodigo(codigo As String)
        Dim peticion As New Clases.Rol With {
            .Codigo = codigo
        }

        Dim role = Rol.ObtenerRoles(peticion, True).FirstOrDefault

        If role IsNot Nothing Then
            txtNombreRoleForm.Text = role.Codigo
            txtDescRolForm.Text = role.Descripcion
            chkVigenteForm.Checked = role.Activo
            PermisosAsignados = role.Permisos
        End If

        ddlModuloForm.SelectedIndex = 0
        CargarListas()
    End Sub


    Private Sub CargarAplicaciones()

        Dim lista As List(Of Clases.Aplicacion)

        Dim objPeticion As New Clases.Aplicacion With {
            .Activo = True}

        lista = Aplicacion.ObtenerAplicaciones(objPeticion)

        ddlModulo.AppendDataBoundItems = True
        ddlModulo.DataTextField = "Descripcion"
        ddlModulo.DataValueField = "Identificador"
        ddlModulo.DataSource = lista
        ddlModulo.DataBind()
        ddlModulo.OrdenarPorDesc()
        ddlModulo.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))

        ddlModuloForm.AppendDataBoundItems = True
        ddlModuloForm.DataTextField = "Descripcion"
        ddlModuloForm.DataValueField = "Identificador"
        ddlModuloForm.DataSource = lista
        ddlModuloForm.DataBind()
        ddlModuloForm.OrdenarPorDesc()
        ddlModuloForm.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        RealizarBusqueda(True)
    End Sub

    Private Sub RealizarBusqueda(limpiaForm As Boolean)
        Dim peticion As New Clases.Rol With {
            .Codigo = txtNombreRol.Text,
            .Aplicacion = New Clases.Aplicacion,
            .Activo = chkVigente.Checked
        }

        If ddlModulo.SelectedIndex > 0 Then
            peticion.Aplicacion.Identificador = ddlModulo.SelectedValue
        End If
        rolesGrid = Rol.ObtenerRoles(peticion).OrderBy(Function(x) x.Aplicacion.Codigo).ThenBy(Function(y) y.Codigo).ToList()
        CargarGrilla()
        pnGrid.Visible = True
        If limpiaForm Then
            LimpiarForm()
        End If

    End Sub

    Private Sub CargarGrilla()
        grid.DataSource = rolesGrid
        grid.DataBind()
    End Sub
    Private Sub ddlModuloForm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlModuloForm.SelectedIndexChanged
        If ddlModuloForm.SelectedIndex > 0 Then
            Dim objPeticion As New Clases.Permiso With {
                .Aplicacion = New Clases.Aplicacion With {.Identificador = ddlModuloForm.SelectedValue},
                .Activo = True}

            'Almaceno el resultado de la consulta a la base de datos en la variable de sesion
            PermisosDisponibles = PermisoIAC.ObtenerPermisos(objPeticion)
        End If
        CargarListas()

    End Sub

    Private Sub CargarListas()
        If ddlModuloForm.SelectedIndex > 0 Then
            'Filtro los permisos disponibles en base a los ya asignados
            lsbPermisosDisponibles.DataSource = PermisosDisponibles.Where(Function(x) Not PermisosAsignados.Exists(Function(y) y.Identificador = x.Identificador)).OrderBy(Function(x) x.Codigo).ToList()
            lsbPermisosDisponibles.DataTextField = "Codigo"
            lsbPermisosDisponibles.DataValueField = "Identificador"
            lsbPermisosDisponibles.DataBind()


            lsbPermisosAsignados.DataSource = PermisosAsignados.Where(Function(x) x.Aplicacion.Identificador = ddlModuloForm.SelectedValue).OrderBy(Function(x) x.Codigo).ToList()
            lsbPermisosAsignados.DataTextField = "Codigo"
            lsbPermisosAsignados.DataValueField = "Identificador"
            lsbPermisosAsignados.DataBind()

        Else
            Dim objPeticion As New Clases.Permiso With {
           .Activo = True}

            'Almaceno el resultado de la consulta a la base de datos en la variable de sesion
            PermisosDisponibles = PermisoIAC.ObtenerPermisos(objPeticion)

            lsbPermisosDisponibles.DataSource = PermisosDisponibles.Where(Function(x) Not PermisosAsignados.Exists(Function(y) y.Identificador = x.Identificador)).OrderBy(Function(x) x.AplicacionCodigo).ToList()
            lsbPermisosDisponibles.DataTextField = "AplicacionCodigo"
            lsbPermisosDisponibles.DataValueField = "Identificador"
            lsbPermisosDisponibles.DataBind()

            lsbPermisosAsignados.DataSource = PermisosAsignados.OrderBy(Function(x) x.AplicacionCodigo).ToList()
            lsbPermisosAsignados.DataTextField = "AplicacionCodigo"
            lsbPermisosAsignados.DataValueField = "Identificador"
            lsbPermisosAsignados.DataBind()
        End If
    End Sub

    Private Sub btnPermisoAgregarTodos_Click(sender As Object, e As ImageClickEventArgs) Handles btnPermisoAgregarTodos.Click
        If PermisosDisponibles.Count > 0 Then
            PermisosAsignados.AddRange(PermisosDisponibles)
            CargarListas()
        End If
    End Sub

    Private Sub btnPermisoAgregar_Click(sender As Object, e As ImageClickEventArgs) Handles btnPermisoAgregar.Click
        'Recorro los elementos seleccionados
        For Each seleccion In lsbPermisosDisponibles.GetSelectedIndices
            Dim itemSeleccionado = lsbPermisosDisponibles.Items(seleccion)
            'Busco el elemento seleccionado en la coleccion de permisos disponibles
            Dim permisoSeleccionado = PermisosDisponibles.FirstOrDefault(Function(x) x.Identificador = itemSeleccionado.Value)

            'Agrego el elemento a la coleccion de permisos asignados
            If permisoSeleccionado IsNot Nothing Then
                PermisosAsignados.Add(permisoSeleccionado)
            End If
        Next
        CargarListas()
    End Sub

    Private Sub btnPermisoQuitar_Click(sender As Object, e As ImageClickEventArgs) Handles btnPermisoQuitar.Click
        'Recorro los elementos seleccionadoss
        For Each seleccion In lsbPermisosAsignados.GetSelectedIndices
            Dim itemSeleccionado = lsbPermisosAsignados.Items(seleccion)

            'Busco el elemento seleccionado en la coleccion de permisos asignados
            Dim permisoSeleccionado = PermisosAsignados.FirstOrDefault(Function(x) x.Identificador = itemSeleccionado.Value)

            'Quito el elemento de la coleccion de permisos asignados
            If permisoSeleccionado IsNot Nothing Then
                PermisosAsignados.Remove(permisoSeleccionado)
            End If
        Next
        CargarListas()
    End Sub

    Private Sub btnPermisoQuitarTodos_Click(sender As Object, e As ImageClickEventArgs) Handles btnPermisoQuitarTodos.Click
        If PermisosAsignados.Count > 0 Then
            PermisosAsignados.RemoveAll(Function(x) x.Aplicacion.Identificador = ddlModuloForm.SelectedValue)
            CargarListas()
        End If
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As System.EventArgs) Handles btnNovo.Click
        Acao = Aplicacao.Util.Utilidad.eAcao.Alta
        pnForm.Visible = True
        txtNombreRoleForm.Text = String.Empty
        txtDescRolForm.Text = String.Empty
        chkVigenteForm.Checked = True

        btnNovo.Enabled = False
        btnGrabar.Enabled = True
        btnCancelar.Enabled = True

        txtNombreRoleForm.Enabled = True
        txtDescRolForm.Enabled = True
        chkVigenteForm.Enabled = True
        divBotones.Visible = True

        PermisosAsignados = New List(Of Clases.Permiso)
        lsbPermisosDisponibles.ClearSelection()
        txtNombreRoleForm.Focus()
        CargarListas()

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        LimpiarForm()
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Limpiar()
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click

        Dim objPeticion As Genesis.ContractoServicio.Contractos.Permisos.PeticionGrabarRol

        objPeticion = ObtenerPeticionParaGrabarRol()

        Dim respuesta As Genesis.ContractoServicio.Contractos.Permisos.RespuestaGrabarRol = Rol.Grabar(objPeticion)

        MostraMensagem(respuesta.Descripcion)

        If respuesta IsNot Nothing AndAlso respuesta.Codigo IsNot Nothing AndAlso respuesta.Codigo.Equals("msgExito") Then
            Limpiar()
            RealizarBusqueda(False)
        End If

    End Sub

    Private Function ObtenerPeticionParaGrabarRol() As Genesis.ContractoServicio.Contractos.Permisos.PeticionGrabarRol


        Dim unaPeticion As New Genesis.ContractoServicio.Contractos.Permisos.PeticionGrabarRol
        Dim objRol As New Clases.Rol

        Try
            Select Case MyBase.Acao
                Case Aplicacao.Util.Utilidad.eAcao.Alta
                    unaPeticion.AccionGrabar = "ALTA"
                Case Aplicacao.Util.Utilidad.eAcao.Baja
                    unaPeticion.AccionGrabar = "BAJA"
                Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                    unaPeticion.AccionGrabar = "MODIFICAR"
            End Select


            objRol.Activo = Me.chkVigenteForm.Checked
            objRol.Codigo = txtNombreRoleForm.Text
            objRol.Descripcion = txtDescRolForm.Text
            objRol.Permisos = PermisosAsignados
            unaPeticion.Rol = objRol
            unaPeticion.CodigoUsuario = LoginUsuario
        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try

        Return unaPeticion
    End Function
End Class