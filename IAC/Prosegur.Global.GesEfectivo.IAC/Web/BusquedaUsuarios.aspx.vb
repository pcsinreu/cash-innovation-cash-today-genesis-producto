Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.LogicaNegocio


Public Class BusquedaUsuarios
    Inherits Base

    Private Const CODIGO_ROLE_ADMINISTRADOR As String = "Administrador"

#Region "[PROPIEDADES]"
    Private Property UsuariosGrid As List(Of Contractos.Permisos.RespuestaRecuperarUsuario)
        Get
            Return Session("usuariosGrid")
        End Get
        Set(value As List(Of Contractos.Permisos.RespuestaRecuperarUsuario))
            Session("usuariosGrid") = value
        End Set
    End Property

    Private Property IdentificadorDeUsuario As String
        Get
            Return Session("IdentificadorDeUsuario")
        End Get
        Set(value As String)
            Session("IdentificadorDeUsuario") = value
        End Set
    End Property

    Private Property UsuarioEditado As Contractos.Permisos.RespuestaRecuperarUsuario
        Get
            Return Session("UsuarioParaEditar")
        End Get
        Set(value As Contractos.Permisos.RespuestaRecuperarUsuario)
            Session("UsuarioParaEditar") = value
        End Set
    End Property

    Private Property RolesDisponibles As List(Of Clases.Rol)
        Get
            Return Session("rolesDisponibles")
        End Get
        Set(value As List(Of Clases.Rol))
            Session("rolesDisponibles") = value
        End Set
    End Property

    Private Property RolesDisponiblesBase As List(Of Clases.Rol)
        Get
            Return Session("rolesDisponiblesBase")
        End Get
        Set(value As List(Of Clases.Rol))
            Session("rolesDisponiblesBase") = value
        End Set
    End Property

    Private Property RolesAsignados As List(Of Clases.RoleXUsuario)
        Get
            Return Session("rolesAsignados")
        End Get
        Set(value As List(Of Clases.RoleXUsuario))
            Session("rolesAsignados") = value
        End Set
    End Property

    Private Property Paises As List(Of Clases.Pais)
        Get
            If Session("paises") Is Nothing Then Session("paises") = New List(Of Clases.Pais)
            Return Session("paises")
        End Get
        Set(value As List(Of Clases.Pais))
            Session("paises") = value
        End Set
    End Property

    Private Property UsuariosAltaMasiva As List(Of Clases.Usuario)
        Get
            If Session("usuarioAltaMAsiva") Is Nothing Then Session("usuarioAltaMAsiva") = New List(Of Clases.Usuario)
            Return Session("usuarioAltaMAsiva")
        End Get
        Set(value As List(Of Clases.Usuario))
            Session("usuarioAltaMAsiva") = value
        End Set
    End Property

#End Region

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CONFIGURACION_USUARIOS
        MyBase.ValidarAcao = True
        MyBase.ValidarPemissaoAD = False
    End Sub
    Protected Overrides Sub Inicializar()
        Try

            GoogleAnalyticsHelper.TrackAnalytics(Me, "Configuracion de usuarios")

            DevExpress.Web.ASPxGridView.ASPxGridView.RegisterBaseScript(Page)

            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.MenuRodapeVisivel = True
            Master.MostrarRodape = True

            If Not Page.IsPostBack Then
                LimpiarForm()
                CargarPaises()
                CargarRoles()
                CargarAplicaciones()
                CargarIdiomas(Not IsPostBack)
            End If
        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try
    End Sub


    Protected Overrides Sub TraduzirControles()
        MyBase.CodFuncionalidad = "CONFIGURARUSUARIOS"
        CarregaDicinario()

        Master.Titulo = MyBase.RecuperarValorDic("lblBusquedaDeUsuarios")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("041_lbl_SubTitulosCriteriosBusqueda")

        lblTituloUsuarios.Text = MyBase.RecuperarValorDic("lblTituloUsuarios")
        lblTituloMantenimientoUsuarios.Text = MyBase.RecuperarValorDic("lblTituloMantenimientoUsuarios")
        lblTituloRoles.Text = MyBase.RecuperarValorDic("lblRole")

        lblNombreUsuario.Text = MyBase.RecuperarValorDic("lblNombreDesLogin")

        lblNombreDesLoginForm.Text = MyBase.RecuperarValorDic("lblNombreDesLogin")
        lblApellidoForm.Text = MyBase.RecuperarValorDic("lblApellido")
        lblNombreForm.Text = MyBase.RecuperarChavesDic("lblNombre")
        lblModuloForm.Text = MyBase.RecuperarValorDic("lblModulo")
        lblPaisForm.Text = MyBase.RecuperarValorDic("lblPais")
        lblAltaMasiva.Text = MyBase.RecuperarValorDic("lblAltaMasiva")

        lblRolesDisponibles.Text = MyBase.RecuperarValorDic("lblRolesDisponibles")
        lblRolesAsignados.Text = MyBase.RecuperarValorDic("lblRolesUsuarios")
        lblPais.Text = MyBase.RecuperarValorDic("lblPais")
        lblRole.Text = MyBase.RecuperarValorDic("lblRole")


        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")

        btnNovo.Text = Traduzir("btnAlta")
        btnGrabar.Text = Traduzir("btnGrabar")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnCancelar.Text = Traduzir("btnCancelar")
        lblVigenteForm.Text = MyBase.RecuperarValorDic("lblActivo")

        'Grid 
        grid.Columns(1).Caption = MyBase.RecuperarValorDic("lblNombreDesLogin")
        grid.Columns(2).Caption = MyBase.RecuperarValorDic("lblNombre")
        grid.Columns(3).Caption = MyBase.RecuperarValorDic("lblPais")
        grid.Columns(4).Caption = MyBase.RecuperarValorDic("lblRole")
        grid.Columns(5).Caption = MyBase.RecuperarValorDic("lblActivo")
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        TraduzirControles()
        CargarGrilla()
    End Sub

    Private Sub Limpiar()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta
        txtNombreUsuarioDeRed.Text = String.Empty
        RolesAsignados.Clear()
        ddlPais.SelectedIndex = 0
        ddlRole.SelectedIndex = 0
        ddlPaisForm.SelectedIndex = 0
        ddlModuloForm.SelectedIndex = 0
        grid.DataSource = Nothing
        grid.DataBind()
        pnGrid.Visible = False

        LimpiarForm()
    End Sub

    Private Sub LimpiarForm()
        pnForm.Visible = False
        btnNovo.Enabled = True
        btnCancelar.Enabled = False
        btnBajaConfirm.Visible = False
        btnGrabar.Enabled = False

        txtNombreDesLoginForm.Text = String.Empty
        txtNombreForm.Text = String.Empty
        txtApellidoForm.Text = String.Empty
        ddlIdioma.Enabled = True

        RolesDisponibles = New List(Of Clases.Rol)
        RolesAsignados = New List(Of Clases.RoleXUsuario)

        lblAltaMasiva.Visible = False
        chkAltaMasiva.Visible = False
        chkAltaMasiva.Checked = False
        btnAgregarUsuario.Visible = False
        btnQuitarUsuario.Visible = False
        lbUsuariosMasivo.Visible = False


    End Sub

    Protected Function BuscaPostbackGrid(accion As String, identificador As String) As String

        Dim strPostBack As String = String.Empty
        strPostBack = "__doPostBack('" & btnGrid.UniqueID & "', '" & accion & "|" & identificador & "')"
        Return strPostBack
    End Function

    Protected Sub btnGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrid.Click
        Dim p = Page.Request.Params.Get("__EVENTARGUMENT")

        Dim params = p.Split("|")
        Dim desLogin As String = params(1)

        Limpiar()

        Select Case params(0)
            Case "CONSULTAR"
                BtnConsultar(desLogin)
            Case "ELIMINAR"
                BtnBaja(desLogin)
            Case "EDITAR"
                BtnEditar(desLogin)

        End Select
    End Sub

    Private Sub BtnEditar(desLogin As String)
        Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion

        CargarFormularioConDatosDeLaGrilla(desLogin)

        pnForm.Visible = True
        btnGrabar.Enabled = True
        btnNovo.Enabled = True
        btnCancelar.Enabled = True
        ddlIdioma.Enabled = True

        txtNombreDesLoginForm.Enabled = False
        txtNombreForm.Enabled = True
        txtApellidoForm.Enabled = True
        If UsuarioEditado IsNot Nothing AndAlso UsuarioEditado.Activo Then
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
        Else
            chkVigenteForm.Checked = False
            chkVigenteForm.Enabled = True
        End If


        divBotones.Visible = True
    End Sub
    Private Sub BtnConsultar(desLogin As String)
        Acao = Aplicacao.Util.Utilidad.eAcao.Consulta

        CargarFormularioConDatosDeLaGrilla(desLogin)

        pnForm.Visible = True
        btnGrabar.Enabled = False
        btnNovo.Enabled = True
        btnCancelar.Enabled = True

        txtNombreDesLoginForm.Enabled = False
        txtNombreForm.Enabled = False
        txtApellidoForm.Enabled = False
        ddlIdioma.Enabled = False

        divBotones.Visible = False

    End Sub

    Private Sub BtnBaja(desLogin As String)
        Acao = Aplicacao.Util.Utilidad.eAcao.Baja

        CargarFormularioConDatosDeLaGrilla(desLogin)

        pnForm.Visible = True
        btnGrabar.Enabled = True
        btnNovo.Enabled = True
        btnCancelar.Enabled = True

        txtNombreDesLoginForm.Enabled = False
        txtNombreForm.Enabled = False
        txtApellidoForm.Enabled = False
        chkVigenteForm.Checked = False
        chkVigenteForm.Enabled = False
        ddlIdioma.Enabled = False

        divBotones.Visible = False


    End Sub
    Public Sub CargarFormularioConDatosDeLaGrilla(desLogin As String)
        Dim peticion As New Contractos.Permisos.PeticionRecuperarUsuario With {
            .DesLogin = desLogin
        }

        Dim usuario = GestionPermisos.Usuario.ObtenerUsuarios(peticion, True).FirstOrDefault

        If usuario IsNot Nothing Then
            txtNombreDesLoginForm.Text = usuario.DesLogin
            txtNombreForm.Text = usuario.Nombre
            txtApellidoForm.Text = usuario.Apellido
            RolesAsignados = usuario.RoleXUsuario.Where(Function(y) y.Activo = True).ToList()
            IdentificadorDeUsuario = usuario.Identificador
            Dim indice As Integer = 0
            Dim i As Integer = 0
            For Each elementoIdioma In ddlIdioma.Items
                If DirectCast(elementoIdioma, ListItem).Value.Equals(usuario.IdiomaPorDefecto) OrElse (DirectCast(elementoIdioma, ListItem).Value.Length >= 2 AndAlso DirectCast(elementoIdioma, ListItem).Value.Substring(0, 2).Equals(usuario.IdiomaPorDefecto.Substring(0, 2))) Then
                    indice = i
                End If
                i += 1
            Next

            ddlIdioma.SelectedIndex = indice
            UsuarioEditado = usuario
        End If

        CargarListas()
    End Sub


    Private Sub CargarPaises()

        'Obtenemos los permisos del usuario logueado
        Dim peticion As New Contractos.Permisos.PeticionRecuperarUsuario With {
            .DesLogin = LoginUsuario
        }
        Dim usuario = GestionPermisos.Usuario.ObtenerUsuarios(peticion, True).FirstOrDefault
        Paises = New List(Of Clases.Pais)

        'Obtemos los paises con el permiso de administrador del usuario logueado.
        For Each unRolPorUsuario In usuario.RoleXUsuario
            If unRolPorUsuario.Role.Codigo.Equals(CODIGO_ROLE_ADMINISTRADOR) Then
                If Not Paises.Contains(unRolPorUsuario.Pais) Then
                    Paises.Add(unRolPorUsuario.Pais)
                End If
            End If
        Next


        ddlPais.AppendDataBoundItems = True
        ddlPais.DataTextField = "Descripcion"
        ddlPais.DataValueField = "Codigo"
        ddlPais.DataSource = Paises
        ddlPais.DataBind()
        ddlPais.OrdenarPorDesc()
        If Paises.Count > 1 Then
            ddlPais.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))
        Else
            ddlPais.SelectedIndex = 0
        End If

        ddlPaisForm.AppendDataBoundItems = True
        ddlPaisForm.DataTextField = "Descripcion"
        ddlPaisForm.DataValueField = "Codigo"
        ddlPaisForm.DataSource = Paises
        ddlPaisForm.DataBind()
        ddlPaisForm.OrdenarPorDesc()
        If Paises.Count > 1 Then
            ddlPaisForm.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))
        Else
            ddlPaisForm.SelectedIndex = 0
        End If

    End Sub
    Private Sub CargarIdiomas(valorInicial As Boolean)

        Try
            If valorInicial Then
                ddlIdioma.Items.Clear()
                ddlIdioma.Items.Add(New ListItem(Traduzir("036_ddl_selecione"), String.Empty))
                ddlIdioma.Items.Add(New ListItem(MyBase.RecuperarValorDic("en-EN"), "en-EN"))
                ddlIdioma.Items.Add(New ListItem(MyBase.RecuperarValorDic("de-DE"), "de-DE"))
                ddlIdioma.Items.Add(New ListItem(MyBase.RecuperarValorDic("pt-PT"), "pt-PT"))
                ddlIdioma.Items.Add(New ListItem(MyBase.RecuperarValorDic("es-ES"), "es-ES"))
            End If
            ddlIdioma.DataBind()
        Catch ex As Exception
            'No hacer nada
        End Try
    End Sub

    Private Sub CargarRoles()

        Dim peticion As New Clases.Rol With {
            .Activo = True
        }

        RolesDisponiblesBase = Rol.ObtenerRoles(peticion)

        ddlRole.AppendDataBoundItems = True
        ddlRole.DataTextField = "Codigo"
        ddlRole.DataValueField = "Codigo"
        ddlRole.DataSource = RolesDisponiblesBase.GroupBy(Function(x) x.Codigo).Select(Function(y) New Clases.Rol With {.Codigo = y.Key}).ToList()
        ddlRole.DataBind()
        ddlRole.OrdenarPorDesc()
        ddlRole.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))
    End Sub

    Private Sub CargarAplicaciones()

        Dim lista As List(Of Clases.Aplicacion)

        Dim objPeticion As New Clases.Aplicacion With {
            .Activo = True}

        lista = Aplicacion.ObtenerAplicaciones(objPeticion)

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

        Dim strCodigoPais As String = ddlPais.SelectedValue
        Dim strCodigoRol As String = ddlRole.SelectedValue


        Dim peticion As New Contractos.Permisos.PeticionRecuperarUsuario With {
            .DesLogin = txtNombreUsuarioDeRed.Text,
            .CodigoPais = strCodigoPais,
            .CodigoRole = strCodigoRol
        }


        UsuariosGrid = GestionPermisos.Usuario.ObtenerUsuarios(peticion, False)
        CargarGrilla()

        pnGrid.Visible = True
        If limpiaForm Then
            LimpiarForm()
        End If

    End Sub

    Private Sub CargarGrilla()
        grid.DataSource = UsuariosGrid
        grid.DataBind()
    End Sub

    Private Sub CargarListas()
        Dim hayPaisSeleccionado As Boolean = (Paises.Count = 1) OrElse (Paises.Count > 1 AndAlso ddlPaisForm.SelectedIndex > 0)
        Dim paisSeleccionado As Clases.Pais = Nothing

        If hayPaisSeleccionado Then
            If Paises.Count = 1 Then
                paisSeleccionado = Paises(0)
            Else
                paisSeleccionado = Paises.Where(Function(x) x.Codigo = ddlPaisForm.SelectedValue).FirstOrDefault
            End If
        End If

        If ddlModuloForm.SelectedIndex > 0 Then

            'Filtro los permisos disponibles en base a los ya asignados
            If paisSeleccionado IsNot Nothing Then
                lsbRolesDisponibles.DataSource = RolesDisponibles.Where(Function(x) x.Aplicacion.Identificador = ddlModuloForm.SelectedValue AndAlso Not RolesAsignados.Exists(Function(y) y.Role.Identificador = x.Identificador AndAlso y.Pais.Codigo = paisSeleccionado.Codigo AndAlso y.Activo)).OrderBy(Function(x) x.Codigo).ToList()
            Else
                lsbRolesDisponibles.DataSource = RolesDisponiblesBase 'RolesDisponibles.Where(Function(x) x.Aplicacion.Identificador = ddlModuloForm.SelectedValue AndAlso Not RolesAsignados.Exists(Function(y) y.Role.Identificador = x.Identificador)).OrderBy(Function(x) x.Codigo).ToList()
            End If

            lsbRolesDisponibles.DataTextField = "Codigo"
            lsbRolesDisponibles.DataValueField = "Identificador"
            lsbRolesDisponibles.DataBind()
        Else
            'Almaceno el resultado de la consulta a la base de datos en la variable de sesion
            RolesDisponibles = RolesDisponiblesBase

            If hayPaisSeleccionado Then
                lsbRolesDisponibles.DataSource = RolesDisponibles.Where(Function(x) Not RolesAsignados.Exists(Function(y) y.Role.Identificador = x.Identificador AndAlso y.Activo AndAlso y.Pais.Codigo = paisSeleccionado.Codigo)).OrderBy(Function(x) x.AplicacionCodigo).ToList()
            Else
                lsbRolesDisponibles.DataSource = RolesDisponiblesBase ' RolesDisponibles.Where(Function(x) Not RolesAsignados.Exists(Function(y) y.Role.Identificador = x.Identificador AndAlso y.Activo)).OrderBy(Function(x) x.AplicacionCodigo).ToList()
            End If

            lsbRolesDisponibles.DataTextField = "AplicacionCodigo"
            lsbRolesDisponibles.DataValueField = "Identificador"
            lsbRolesDisponibles.DataBind()
        End If

        lsbRolesAsignados.DataSource = RolesAsignados.Where(Function(y) y.Activo = True).OrderBy(Function(x) x.PaisRole).ToList()
        lsbRolesAsignados.DataTextField = "PaisRole"
        lsbRolesAsignados.DataValueField = "PaisRole"
        lsbRolesAsignados.DataBind()
    End Sub

    Private Sub CargarUsuariosAltaMasiva()
        lbUsuariosMasivo.DataSource = UsuariosAltaMasiva.OrderBy(Function(x) x.Login).ToList()
        lbUsuariosMasivo.DataTextField = "LoginNombreApellido"
        lbUsuariosMasivo.DataValueField = "Login"
        lbUsuariosMasivo.DataBind()
    End Sub
    Private Sub btnRoleAgregarTodos_Click(sender As Object, e As ImageClickEventArgs) Handles btnRolesAgregarTodos.Click
        If EstaSeleccionadoElPais() Then
            If RolesDisponibles.Count > 0 Then
                RolesAsignados.AddRange(RolToRolexUsuario(RolesDisponibles))
                CargarListas()
            End If
        End If

    End Sub

    Private Function RolToRolexUsuario(Roles As List(Of Clases.Rol)) As List(Of Clases.RoleXUsuario)
        Dim Resultado = New List(Of Clases.RoleXUsuario)
        For Each rol In Roles
            Dim rolexusuario = New Clases.RoleXUsuario With {
                .Activo = True,
                .Identificador = rol.Identificador,
                .Pais = Paises.FirstOrDefault(Function(x) x.Codigo = ddlPaisForm.SelectedValue),
                .Role = rol
            }

            If Not RolesAsignados.Any(Function(x) x.PaisRole = rolexusuario.PaisRole) Then
                Resultado.Add(rolexusuario)
            End If
        Next
        Return Resultado
    End Function

    Private Sub btnRoleAgregar_Click(sender As Object, e As ImageClickEventArgs) Handles btnRoleAgregar.Click
        If EstaSeleccionadoElPais() Then
            'Recorro los elementos seleccionados
            For Each seleccion In lsbRolesDisponibles.GetSelectedIndices
                Dim itemSeleccionado = lsbRolesDisponibles.Items(seleccion)
                'Busco el elemento seleccionado en la coleccion de permisos disponibles
                Dim roleSeleccionado = RolesDisponibles.FirstOrDefault(Function(x) x.Identificador = itemSeleccionado.Value)

                'Agrego el elemento a la coleccion de permisos asignados
                If roleSeleccionado IsNot Nothing Then
                    RolesAsignados.Add(New Clases.RoleXUsuario() With {
                                            .Pais = Paises.FirstOrDefault(Function(x) x.Codigo = ddlPaisForm.SelectedValue),
                                            .Role = roleSeleccionado,
                                            .Activo = True
                                       })
                End If
            Next
            CargarListas()
        End If
    End Sub

    Private Function EstaSeleccionadoElPais() As Boolean
        Dim retorno As Boolean = False
        Try
            'Solo cuando tenemos un país, el SelectedIndex pertenece al país en cuestión.
            If ddlPaisForm.SelectedIndex <> 0 OrElse Paises.Count = 1 Then
                retorno = True
            End If
        Catch ex As Exception
            retorno = False
        End Try
        If Not retorno Then
            Dim mensaje As String = MyBase.RecuperarValorDic("msgEsObligatorioPais")
            MyBase.MostraMensagem(mensaje)
        End If

        Return retorno
    End Function

    Private Function EstaSeleccionadoUnIdioma() As Boolean
        Dim retorno As Boolean = False

        Try
            If ddlIdioma.SelectedIndex <> 0 Then
                retorno = True
            End If
        Catch ex As Exception
            retorno = False
        End Try

        If Not retorno Then
            Dim mensaje As String = MyBase.RecuperarValorDic("msgEsObligatorioIdioma")
            MyBase.MostraMensagem(mensaje)
        End If

        Return retorno
    End Function

    Private Sub btnRoleQuitar_Click(sender As Object, e As ImageClickEventArgs) Handles btnRoleQuitar.Click
        'Recorro los elementos seleccionadoss
        For Each seleccion In lsbRolesAsignados.GetSelectedIndices
            Dim itemSeleccionado = lsbRolesAsignados.Items(seleccion)

            'Busco el elemento seleccionado en la coleccion de permisos asignados
            Dim roleSeleccionado = RolesAsignados.FirstOrDefault(Function(x) x.PaisRole() = itemSeleccionado.Value)

            'Quito el elemento de la coleccion de permisos asignados
            If roleSeleccionado IsNot Nothing Then

                If Paises.Where(Function(x) x.Identificador = roleSeleccionado.Pais.Identificador).FirstOrDefault Is Nothing Then
                    Dim mensaje As String = MyBase.RecuperarValorDic("msgNoTienePermisoParaQuitarRolePais")
                    MyBase.MostraMensagem(String.Format(mensaje, roleSeleccionado.Pais.Descripcion))
                Else
                    RolesAsignados.Remove(roleSeleccionado)
                End If

            End If
        Next
        CargarListas()
    End Sub

    Private Sub btnRoleQuitarTodos_Click(sender As Object, e As ImageClickEventArgs) Handles btnRolesQuitarTodos.Click
        If RolesAsignados.Count > 0 Then
            Dim tieneRolesDeOtroPais As Boolean = False
            Dim strPais As String = String.Empty

            'RolesAisgnados tengo TODOS LOS ROLES que pueden pertenecer a cualquier pais
            'En Paises tengo solo los paises que es administrador el usuario LOGUEADO
            Dim identificadoresPaises As New List(Of String)
            For Each unPais In Paises
                identificadoresPaises.Add(unPais.Identificador)
            Next

            For Each rol In RolesAsignados
                If Not identificadoresPaises.Contains(rol.Pais.Identificador) Then
                    tieneRolesDeOtroPais = True
                    strPais = rol.Pais.Descripcion
                End If
            Next

            If tieneRolesDeOtroPais Then
                Dim mensaje As String = MyBase.RecuperarValorDic("msgNoTienePermisoParaQuitarRolePais")
                MyBase.MostraMensagem(String.Format(mensaje, strPais))
            Else
                RolesAsignados.Clear()
                CargarListas()
            End If
        End If
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As System.EventArgs) Handles btnNovo.Click
        Acao = Aplicacao.Util.Utilidad.eAcao.Alta
        pnForm.Visible = True
        txtNombreDesLoginForm.Text = String.Empty
        txtApellidoForm.Text = String.Empty
        txtNombreForm.Text = String.Empty
        ddlIdioma.SelectedIndex = 0

        btnNovo.Enabled = False
        btnGrabar.Enabled = True
        btnCancelar.Enabled = True

        txtNombreDesLoginForm.Enabled = True
        txtNombreDesLoginForm.Text = String.Empty
        txtNombreForm.Enabled = True
        txtNombreForm.Text = String.Empty
        txtApellidoForm.Enabled = True
        txtApellidoForm.Text = String.Empty
        ddlIdioma.SelectedIndex = 0
        ddlIdioma.Enabled = True
        divBotones.Visible = True
        chkVigenteForm.Checked = True
        chkVigenteForm.Enabled = False

        UsuariosAltaMasiva = New List(Of Clases.Usuario)
        lblAltaMasiva.Visible = True
        chkAltaMasiva.Visible = True
        chkAltaMasiva.Checked = False

        RolesAsignados = New List(Of Clases.RoleXUsuario)
        lsbRolesDisponibles.ClearSelection()
        txtNombreDesLoginForm.Focus()

        CargarListas()

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        LimpiarForm()
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Limpiar()
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click

        If EstaSeleccionadoUnIdioma() Then
            If String.IsNullOrWhiteSpace(txtNombreDesLoginForm.Text) OrElse
            String.IsNullOrWhiteSpace(txtNombreForm.Text) OrElse
            String.IsNullOrWhiteSpace(txtApellidoForm.Text) OrElse
            String.IsNullOrWhiteSpace(ddlIdioma.SelectedValue) Then
                MostraMensagem(MyBase.RecuperarValorDic("msgCamposObligatorios"))
            Else
                Dim objPeticion As Genesis.ContractoServicio.Contractos.Permisos.PeticionGrabarUsuario

                objPeticion = ObtenerPeticionParaGrabarUsuario()

                Dim respuesta As New Contractos.Permisos.RespuestaGrabarUsuario

                GestionPermisos.Usuario.GrabarUsuario(objPeticion, respuesta)


                MostraMensagem(respuesta.Descripcion)
                If respuesta IsNot Nothing AndAlso respuesta.Codigo IsNot Nothing AndAlso respuesta.Codigo.Equals("msgExito") Then
                    Limpiar()
                    RealizarBusqueda(True)
                End If
            End If
        End If

    End Sub

    Private Sub chkAltaMasiva_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAltaMasiva.CheckedChanged
        btnAgregarUsuario.Visible = chkAltaMasiva.Checked
        btnQuitarUsuario.Visible = chkAltaMasiva.Checked
        lbUsuariosMasivo.Visible = chkAltaMasiva.Checked
    End Sub
    Private Sub btnAgregarUsuario_Click(sender As Object, e As System.EventArgs) Handles btnAgregarUsuario.Click
        If String.IsNullOrWhiteSpace(txtNombreDesLoginForm.Text) OrElse
            String.IsNullOrWhiteSpace(txtNombreForm.Text) OrElse
            String.IsNullOrWhiteSpace(txtApellidoForm.Text) OrElse
            String.IsNullOrWhiteSpace(ddlIdioma.SelectedValue) Then
            MostraMensagem(MyBase.RecuperarValorDic("msgCamposObligatorios"))
        Else
            If (Not UsuariosAltaMasiva.Any(Function(x) x.Login.ToUpper() = txtNombreDesLoginForm.Text.ToUpper())) Then
                UsuariosAltaMasiva.Add(New Clases.Usuario With {
                                .Nombre = txtNombreForm.Text,
                                .Apellido = txtApellidoForm.Text,
                                .Idioma = ddlIdioma.SelectedValue,
                                .Login = txtNombreDesLoginForm.Text,
                                .Activo = True
                               })

                CargarUsuariosAltaMasiva()
            End If
        End If


    End Sub
    Private Sub btnQuitarUsuario_Click(sender As Object, e As System.EventArgs) Handles btnQuitarUsuario.Click

        If (lbUsuariosMasivo.SelectedValue IsNot Nothing) Then
            Dim usuarioSeleccionado = UsuariosAltaMasiva.FirstOrDefault(Function(x) x.Login = lbUsuariosMasivo.SelectedValue)
            If usuarioSeleccionado IsNot Nothing Then
                UsuariosAltaMasiva.Remove(usuarioSeleccionado)
            End If
        End If

        CargarUsuariosAltaMasiva()
    End Sub

    Private Function ObtenerPeticionParaGrabarUsuario() As Contractos.Permisos.PeticionGrabarUsuario
        Dim unaPeticion As New Contractos.Permisos.PeticionGrabarUsuario
        Dim objUsuario As Contractos.Permisos.RespuestaRecuperarUsuario

        Try
            Select Case MyBase.Acao
                Case Aplicacao.Util.Utilidad.eAcao.Alta
                    unaPeticion.Accion = "ALTA"
                    IdentificadorDeUsuario = String.Empty
                Case Aplicacao.Util.Utilidad.eAcao.Baja
                    unaPeticion.Accion = "BAJA"
                Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                    unaPeticion.Accion = "MODIFICAR"
            End Select

            If Not chkAltaMasiva.Checked Then
                objUsuario = New Contractos.Permisos.RespuestaRecuperarUsuario With {
                    .Activo = chkVigenteForm.Checked,
                    .Apellido = txtApellidoForm.Text,
                    .Nombre = txtNombreForm.Text,
                    .IdiomaPorDefecto = ddlIdioma.SelectedValue,
                    .DesLogin = txtNombreDesLoginForm.Text,
                    .RoleXUsuario = RolesAsignados
                }

                'Solo si es una modificación o baja debemos informarle el OID_USUARIO
                If MyBase.Acao <> Aplicacao.Util.Utilidad.eAcao.Alta Then
                    objUsuario.Identificador = IdentificadorDeUsuario
                End If

                unaPeticion.Usuarios.Add(objUsuario)
            Else
                If UsuariosAltaMasiva IsNot Nothing Then
                    For Each entidadUsuario In UsuariosAltaMasiva
                        objUsuario = New Contractos.Permisos.RespuestaRecuperarUsuario With {
                            .Activo = True,
                            .Nombre = entidadUsuario.Nombre,
                            .Apellido = entidadUsuario.Apellido,
                            .DesLogin = entidadUsuario.Login,
                            .RoleXUsuario = RolesAsignados,
                            .IdiomaPorDefecto = entidadUsuario.Idioma
                        }

                        unaPeticion.Usuarios.Add(objUsuario)
                    Next
                End If
            End If

            unaPeticion.CodigoUsuario = LoginUsuario

        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try

        Return unaPeticion
    End Function

    Private Sub ddlModuloForm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlModuloForm.SelectedIndexChanged
        CargarListas()
    End Sub

    Private Sub ddlPaisForm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPaisForm.SelectedIndexChanged
        CargarListas()
    End Sub
End Class