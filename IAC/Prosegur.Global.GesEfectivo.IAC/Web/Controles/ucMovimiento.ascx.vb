Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.LogicaNegocio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa
Imports Prosegur.Genesis.ContractoServicio

Public Class ucMovimiento
    Inherits System.Web.UI.UserControl


#Region "Properties"
    Public Property Modo As Genesis.Comon.Enumeradores.Modo

    Private Property MovimientosPlanificacion() As List(Of Clases.Formulario)
        Get
            Return Session("ucMovimiento_MovimientoPlanificacion_" + Me.ID.ToString())
        End Get
        Set(value As List(Of Clases.Formulario))
            Session("ucMovimiento_MovimientoPlanificacion_" + Me.ID.ToString()) = value
        End Set
    End Property

    Private Property Movimientos() As List(Of Clases.Formulario)
        Get
            Return Session("ucMovimiento_Movimiento_" + Me.ID.ToString())
        End Get
        Set(value As List(Of Clases.Formulario))
            Session("ucMovimiento_Movimiento_" + Me.ID.ToString()) = value
        End Set
    End Property


    Private Property MovimientosOriginal() As List(Of Clases.Formulario)
        Get
            Return Session("ucMovimientoOriginal_" + Me.ID.ToString() + Modo.ToString)
        End Get
        Set(value As List(Of Clases.Formulario))
            Session("ucMovimientoOriginal_" + Me.ID.ToString() + Modo.ToString) = value
        End Set
    End Property

    Public Property Planificacion As Clases.Planificacion
        Get
            If ViewState("Planificacion") Is Nothing Then
                ViewState("Planificacion") = New Clases.Planificacion
            End If

            Return DirectCast(ViewState("Planificacion"), Clases.Planificacion)
        End Get

        Set(value As Clases.Planificacion)
            ViewState("Planificacion") = value
        End Set

    End Property


#End Region

#Region "Diccionario"
    Private Sub CarregaDicionario()
        CarregaChavesDicinario(Me.CodFuncionalidadGenerica)
        CarregaChavesDicinario(Me.CodFuncionalidad)
    End Sub
    Private Property dicionario() As Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
        Get
            If Session("uc_dicionario") IsNot Nothing Then
                Return Session("uc_dicionario")
            Else
                Session("uc_dicionario") = New Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
            End If

            Return Session("uc_dicionario")
        End Get
        Set(value As Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String)))
            Session("uc_dicionario") = value
        End Set
    End Property
    Private Property CodFuncionalidad() As String
        Get
            Return ViewState("CodFuncionalidad")
        End Get
        Set(value As String)
            ViewState("CodFuncionalidad") = value
        End Set
    End Property
    Private ReadOnly Property CodFuncionalidadGenerica() As String
        Get
            Return "GENERICO"
        End Get
    End Property
    Private Sub CarregaChavesDicinario(CodigoFuncionalidad As String)
        If Not String.IsNullOrEmpty(CodigoFuncionalidad) Then
            If dicionario Is Nothing Then
                dicionario = New Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
            End If

            'Se já tiver carregado os dicionarios da funcionalidade nao carrega novamente
            If dicionario.ContainsKey(CodigoFuncionalidad) AndAlso dicionario(CodigoFuncionalidad).Values IsNot Nothing AndAlso dicionario(CodigoFuncionalidad).Values.Count > 0 Then
                Exit Sub
            End If

            Dim codigoCultura As String = If(CulturaSistema IsNot Nothing AndAlso
                                                                                 Not String.IsNullOrEmpty(CulturaSistema.Name),
                                                                                 CulturaSistema.Name,
                                                                                 If(CulturaPadrao IsNot Nothing, CulturaPadrao.Name, Globalization.CultureInfo.CurrentCulture.ToString()))

            Dim peticion As New Prosegur.Genesis.ContractoServicio.Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Peticion
            With peticion
                .CodigoFuncionalidad = CodigoFuncionalidad
                .Cultura = codigoCultura
            End With
            Dim respuesta = Prosegur.Genesis.LogicaNegocio.Genesis.Diccionario.ObtenerValoresDicionario(peticion)

            If dicionario.ContainsKey(CodigoFuncionalidad) Then
                dicionario(CodigoFuncionalidad) = respuesta.Valores
            Else
                dicionario.Add(CodigoFuncionalidad, respuesta.Valores)
            End If
        End If
    End Sub
    Private Function RecuperarValorDic(chave) As String
        Try
            If dicionario IsNot Nothing AndAlso dicionario.Count > 0 Then

                If Not String.IsNullOrWhiteSpace(Me.CodFuncionalidad) AndAlso dicionario.ContainsKey(Me.CodFuncionalidad) Then
                    Dim chavesDic = dicionario(Me.CodFuncionalidad)
                    If chavesDic IsNot Nothing AndAlso chavesDic.ContainsKey(chave) Then
                        Return chavesDic(chave)
                    End If
                End If

                If (Not String.IsNullOrWhiteSpace(Me.CodFuncionalidadGenerica) AndAlso dicionario.ContainsKey(Me.CodFuncionalidadGenerica)) Then
                    Dim chavesDic = dicionario(Me.CodFuncionalidadGenerica)
                    If chavesDic IsNot Nothing AndAlso chavesDic.ContainsKey(chave) Then
                        Return chavesDic(chave)
                    End If
                End If

            End If
        Catch ex As Exception

        End Try

        Return chave
    End Function
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ConfigurarControles()

        If Not IsPostBack Then
            CarregaDicionario()
            CargaMovimientos()
            CargarCombo()
        End If

        TraduzirControles()
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        CargarGrilla()
        TraduzirControles()
    End Sub
    Private Sub btnAnadir_Click(sender As Object, e As System.EventArgs) Handles btnAnadir.Click
        Dim movimientoSeleccionado = Movimientos.FirstOrDefault(Function(x) x.Identificador = ddlMovimiento.SelectedValue)

        If movimientoSeleccionado IsNot Nothing Then
            If MovimientosPlanificacion Is Nothing Then
                MovimientosPlanificacion = New List(Of Clases.Formulario)
            End If
            If Not MovimientosPlanificacion.Exists(Function(x) x.Codigo = movimientoSeleccionado.Codigo) Then
                MovimientosPlanificacion.Add(movimientoSeleccionado)
                CargarGrilla()
            End If
        End If
    End Sub
    Protected Sub imgExcluirForm_OnClick(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)
        Try
            If Not String.IsNullOrEmpty(e.CommandArgument) Then
                'Borrar
                Dim movimientoSeleccionado = MovimientosPlanificacion.FirstOrDefault(Function(x) x.Identificador = e.CommandArgument)
                MovimientosPlanificacion.Remove(movimientoSeleccionado)
                CargarGrilla()
            End If
        Catch ex As Exception

        End Try
    End Sub


#End Region


#Region "Metodos"


    Private Sub TraduzirControles()
        grid.Columns(0).Caption = RecuperarValorDic("lblCodigo")
        btnAnadir.Text = RecuperarValorDic("btnAnadir")
        lblDividirPorMovimiento.Text = RecuperarValorDic("lblDividirPorMovimiento")
    End Sub

    Public Sub ConfigurarControles()
        'Oculta la opcion de agregar nuevos limites
        divAlta.Visible = Modo <> Genesis.Comon.Enumeradores.Modo.Consulta

        'Oculta la columna de Borrar en modo consulta
        grid.Columns(1).Visible = Modo <> Genesis.Comon.Enumeradores.Modo.Consulta

    End Sub
    Public Sub CargaRegistrosDeBase()
        MovimientosPlanificacion = New List(Of Clases.Formulario)
        Dim oidPlanificacion As String = String.Empty
        Dim oidMaquina As String = String.Empty
        Dim oidPuntoServicio As String = String.Empty

        If Planificacion IsNot Nothing Then
            oidPlanificacion = Planificacion.Identificador
        End If


        If Not (String.IsNullOrWhiteSpace(oidPlanificacion)) Then
            MovimientosPlanificacion = Genesis.LogicaNegocio.Genesis.Formularios.ObtenerPlanxMovimientos(oidPlanificacion)
        End If

        MovimientosOriginal = MovimientosPlanificacion

        CargarGrilla()
    End Sub
    Private Sub CargaMovimientos()

        If MovimientosPlanificacion Is Nothing Then
            MovimientosPlanificacion = New List(Of Clases.Formulario)
        End If
        If Movimientos Is Nothing AndAlso Modo <> Genesis.Comon.Enumeradores.Modo.Consulta Then
            Movimientos = New List(Of Clases.Formulario)
            Dim logicaMovimiento = New AccionFormulario().getFormularios(New ContractoServicio.Formulario.GetFormularios.Peticion)

            For Each movimiento In logicaMovimiento.Formularios

                Movimientos.Add(New Clases.Formulario With {
                                .Identificador = movimiento.Identificador,
                                .Codigo = movimiento.Codigo,
                                .Descripcion = movimiento.Descripcion
                                })
            Next
        End If
    End Sub

    Private Sub CargarCombo()

        Dim movimientosCombo = New List(Of Clases.Formulario)

        'Solo carga el combo en caso de no ser consulta
        If Movimientos IsNot Nothing AndAlso Movimientos.Count > 0 AndAlso Modo <> Genesis.Comon.Enumeradores.Modo.Consulta Then
            If Movimientos IsNot Nothing AndAlso Movimientos.Count > 0 Then
                movimientosCombo = Movimientos.Where(Function(d) Not MovimientosPlanificacion.Exists(Function(l) l.Codigo = d.Codigo)).ToList
            Else
                movimientosCombo = Movimientos
            End If
        End If
        ddlMovimiento.AppendDataBoundItems = True
        ddlMovimiento.Items.Clear()
        ddlMovimiento.Items.Add(New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))
        ddlMovimiento.DataValueField = "Identificador"
        ddlMovimiento.DataTextField = "CodigoDescripcion"
        ddlMovimiento.DataSource = movimientosCombo.OrderBy(Function(x) x.CodigoDescripcion).ToList
        ddlMovimiento.DataBind()
    End Sub

    Private Sub CargarGrilla()
        If MovimientosPlanificacion IsNot Nothing Then
            MovimientosPlanificacion = MovimientosPlanificacion.OrderBy(Function(a) a.CodigoDescripcion).ToList()
        End If

        grid.DataSource = MovimientosPlanificacion
        grid.DataBind()
        CargarCombo()
        UpdatePanelGrid.Update()

    End Sub


    Public Function BuscarPeticionMovimiento() As List(Of Clases.Formulario)

        Dim peticionMae As New List(Of Clases.Formulario)

        ' baja: todo que esta en LimitesOriginal y no esta Limites
        For Each movimientoOriginal In MovimientosOriginal.Where(Function(x) MovimientosPlanificacion.FirstOrDefault(Function(y) y.Identificador = x.Identificador) Is Nothing)

            Dim obj As Clases.Formulario = New Clases.Formulario With {
                .Accion = "BAJA",
                .Identificador = movimientoOriginal.Identificador
            }

            peticionMae.Add(obj)
        Next

        ' alta: todo que esta en Limites y no esta LimitesOriginal
        For Each formulario In MovimientosPlanificacion.Where(Function(x) MovimientosOriginal.FirstOrDefault(Function(y) y.Identificador = x.Identificador) Is Nothing)
            Dim obj As Clases.Formulario = New Clases.Formulario With {
                .Accion = "ALTA",
                .Identificador = formulario.Identificador
            }

            peticionMae.Add(obj)
        Next

        Return peticionMae

    End Function

#End Region



End Class