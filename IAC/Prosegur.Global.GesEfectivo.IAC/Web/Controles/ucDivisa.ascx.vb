Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.LogicaNegocio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa
Imports Prosegur.Genesis.ContractoServicio

Public Class ucDivisa
    Inherits System.Web.UI.UserControl


#Region "Properties"
    Public Property Modo As Genesis.Comon.Enumeradores.Modo

    Public Property DivisasPlanificacion() As List(Of Clases.Divisa)
        Get
            Return Session("ucDivisa_DivisaPlanificacion_" + Me.ID.ToString())
        End Get
        Set(value As List(Of Clases.Divisa))
            Session("ucDivisa_DivisaPlanificacion_" + Me.ID.ToString()) = value
        End Set
    End Property

    Private Property Divisas() As List(Of Clases.Divisa)
        Get
            Return Session("ucDivisa_Divisa_" + Me.ID.ToString())
        End Get
        Set(value As List(Of Clases.Divisa))
            Session("ucDivisa_Divisa_" + Me.ID.ToString()) = value
        End Set
    End Property


    Private Property DivisasOriginal() As List(Of Clases.Divisa)
        Get
            Return Session("ucDivisaOriginal_" + Me.ID.ToString() + Modo.ToString)
        End Get
        Set(value As List(Of Clases.Divisa))
            Session("ucDivisaOriginal_" + Me.ID.ToString() + Modo.ToString) = value
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
            CargaDivisas()
            CargarCombo()
        End If

        TraduzirControles()
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        CargarGrilla()
        TraduzirControles()
    End Sub
    Private Sub btnAnadir_Click(sender As Object, e As System.EventArgs) Handles btnAnadir.Click
        Dim divisaSeleccionada = Divisas.FirstOrDefault(Function(x) x.CodigoISO = ddlDivisa.SelectedValue)

        If divisaSeleccionada IsNot Nothing Then
            If DivisasPlanificacion Is Nothing Then
                DivisasPlanificacion = New List(Of Clases.Divisa)
            End If
            If Not DivisasPlanificacion.Exists(Function(x) x.CodigoISO = divisaSeleccionada.CodigoISO) Then
                DivisasPlanificacion.Add(divisaSeleccionada)
                CargarGrilla()
            End If
        End If
    End Sub
    Protected Sub imgExcluirForm_OnClick(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)
        Try
            If Not String.IsNullOrEmpty(e.CommandArgument) Then
                'Borrar
                Dim divisaSeleccionada = DivisasPlanificacion.FirstOrDefault(Function(x) x.CodigoISO = e.CommandArgument)
                DivisasPlanificacion.Remove(divisaSeleccionada)
                CargarGrilla()
            End If
        Catch ex As Exception

        End Try
    End Sub


#End Region


#Region "Metodos"


    Private Sub TraduzirControles()
        grid.Columns(0).Caption = RecuperarValorDic("lblDivisa")
        btnAnadir.Text = RecuperarValorDic("btnAnadir")
        lblDivisa.Text = RecuperarValorDic("lblDivisa")
    End Sub

    Public Sub ConfigurarControles()
        'Oculta la opcion de agregar nuevos limites
        divAlta.Visible = Modo <> Genesis.Comon.Enumeradores.Modo.Consulta

        'Oculta la columna de Borrar en modo consulta
        grid.Columns(1).Visible = Modo <> Genesis.Comon.Enumeradores.Modo.Consulta

    End Sub
    Public Sub CargaRegistrosDeBase()
        DivisasPlanificacion = New List(Of Clases.Divisa)
        Dim oidPlanificacion As String = String.Empty

        If Planificacion IsNot Nothing Then
            oidPlanificacion = Planificacion.Identificador
        End If

        If Not (String.IsNullOrWhiteSpace(oidPlanificacion)) Then
            DivisasPlanificacion = Genesis.LogicaNegocio.Genesis.Divisas.ObtenerPlanxDivisas(oidPlanificacion)
        End If

        DivisasOriginal = DivisasPlanificacion

        CargarGrilla()
    End Sub
    Private Sub CargaDivisas()
        'Solo va a buscar las divisas a la base de datos
        'en caso de no tenerlas cargadas y de no ser consulta
        If DivisasPlanificacion Is Nothing Then
            DivisasPlanificacion = New List(Of Clases.Divisa)
        End If
        If Divisas Is Nothing AndAlso Modo <> Genesis.Comon.Enumeradores.Modo.Consulta Then
            Divisas = New List(Of Clases.Divisa)
            Dim logicaDivisa = New AccionDivisa().getDivisas(New ContractoServicio.Divisa.GetDivisas.Peticion With {.Vigente = True})

            For Each divisa In logicaDivisa.Divisas
                Divisas.Add(New Clases.Divisa With {
                            .CodigoISO = divisa.CodigoIso,
                            .CodigoAcceso = divisa.CodigoAccesoDivisa,
                            .CodigoSimbolo = divisa.CodigoSimbolo,
                            .Descripcion = divisa.Descripcion
                            })
            Next
        End If
    End Sub

    Private Sub CargarCombo()

        Dim divisasCombo = New List(Of Clases.Divisa)

        'Solo carga el combo en caso de no ser consulta
        If Divisas IsNot Nothing AndAlso Divisas.Count > 0 AndAlso Modo <> Genesis.Comon.Enumeradores.Modo.Consulta Then
            If Divisas IsNot Nothing AndAlso Divisas.Count > 0 Then
                divisasCombo = Divisas.Where(Function(d) Not DivisasPlanificacion.Exists(Function(l) l.CodigoISO = d.CodigoISO)).ToList
            Else
                divisasCombo = Divisas
            End If
        End If
        ddlDivisa.AppendDataBoundItems = True
        ddlDivisa.Items.Clear()
        ddlDivisa.Items.Add(New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))
        ddlDivisa.DataValueField = "CodigoIso"
        ddlDivisa.DataTextField = "CodigoDescripcion"
        ddlDivisa.DataSource = divisasCombo.OrderBy(Function(x) x.CodigoDescripcion).ToList
        ddlDivisa.DataBind()
    End Sub

    Private Sub CargarGrilla()
        If DivisasPlanificacion IsNot Nothing Then
            DivisasPlanificacion = DivisasPlanificacion.OrderBy(Function(a) a.CodigoDescripcion).ToList()
        End If

        grid.DataSource = DivisasPlanificacion
        grid.DataBind()
        CargarCombo()
        UpdatePanelGrid.Update()

    End Sub

    Public Function BuscarPeticionDivisa() As List(Of Clases.Divisa)

        Dim peticionMae As New List(Of Clases.Divisa)

        ' baja: todo que esta en DivisasOriginal y no esta DivisasPlanificacion
        For Each divisaOriginal In DivisasOriginal.Where(Function(x) DivisasPlanificacion.FirstOrDefault(Function(y) y.CodigoISO = x.CodigoISO) Is Nothing)

            Dim obj As Clases.Divisa = New Clases.Divisa With {
                .Accion = "BAJA",
                .CodigoISO = divisaOriginal.CodigoISO
            }

            peticionMae.Add(obj)
        Next

        ' alta: todo que esta en DivisasPlanificacion y no esta DivisasOriginal
        For Each divisa In DivisasPlanificacion.Where(Function(x) DivisasOriginal.FirstOrDefault(Function(y) y.CodigoISO = x.CodigoISO) Is Nothing)
            Dim obj As Clases.Divisa = New Clases.Divisa With {
                .Accion = "ALTA",
                .CodigoISO = divisa.CodigoISO
            }

            peticionMae.Add(obj)
        Next

        Return peticionMae

    End Function

#End Region


End Class