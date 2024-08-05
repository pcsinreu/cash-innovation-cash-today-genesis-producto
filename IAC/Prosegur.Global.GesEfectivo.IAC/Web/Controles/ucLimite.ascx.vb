Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.LogicaNegocio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa
Imports Prosegur.Genesis.ContractoServicio

Public Class ucLimite
    Inherits System.Web.UI.UserControl

#Region "Properties"
    Public Property Modo As Genesis.Comon.Enumeradores.Modo

    Private Property Divisas() As List(Of Clases.Divisa)
        Get
            Return Session("ucLimite_Divisa_" + Me.ID.ToString())
        End Get
        Set(value As List(Of Clases.Divisa))
            Session("ucLimite_Divisa_" + Me.ID.ToString()) = value
        End Set
    End Property

    Public Property Limites() As List(Of Clases.Limite)
        Get
            Return Session("ucLimite_" + Me.ID.ToString())
        End Get
        Set(value As List(Of Clases.Limite))
            Session("ucLimite_" + Me.ID.ToString()) = value
        End Set
    End Property

    Private Property LimitesOriginal() As List(Of Clases.Limite)
        Get
            Return Session("ucLimiteOriginal_" + Me.ID.ToString() + Modo.ToString)
        End Get
        Set(value As List(Of Clases.Limite))
            Session("ucLimiteOriginal_" + Me.ID.ToString() + Modo.ToString) = value
        End Set
    End Property

    Public Property Maquina As Clases.Maquina
        Get
            If ViewState("Maquina") Is Nothing Then
                ViewState("Maquina") = New Clases.Maquina
            End If

            Return DirectCast(ViewState("Maquina"), Clases.Maquina)
        End Get

        Set(value As Clases.Maquina)
            ViewState("Maquina") = value
        End Set

    End Property
    Public Property PuntoServicio As Clases.PuntoServicio
        Get
            If ViewState("PuntoServicio") Is Nothing Then
                ViewState("PuntoServicio") = New Clases.PuntoServicio
            End If

            Return DirectCast(ViewState("PuntoServicio"), Clases.PuntoServicio)
        End Get

        Set(value As Clases.PuntoServicio)
            ViewState("PuntoServicio") = value
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

        If divisaSeleccionada IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(txtNumLimite.Text) Then
            If Limites Is Nothing Then
                Limites = New List(Of Limite)
            End If
            If Not Limites.Exists(Function(x) x.Divisa.CodigoISO = divisaSeleccionada.CodigoISO) Then
                Limites.Add(New Clases.Limite With {
                    .Divisa = divisaSeleccionada,
                    .NumLimite = txtNumLimite.Text
                    })
                CargarGrilla()
                txtNumLimite.Text = String.Empty
            End If
        End If
    End Sub
    Protected Sub imgExcluirForm_OnClick(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)
        Try
            If Not String.IsNullOrEmpty(e.CommandArgument) Then
                'Borrar
                Dim limiteSeleccionado = Limites.FirstOrDefault(Function(x) x.Divisa.CodigoISO = e.CommandArgument)
                Limites.Remove(limiteSeleccionado)
                CargarGrilla()
            End If
        Catch ex As Exception

        End Try
    End Sub


#End Region

#Region "Metodos"


    Private Sub TraduzirControles()
        grid.Columns(0).Caption = RecuperarValorDic("lblDivisa")
        grid.Columns(1).Caption = RecuperarValorDic("lblLimite")
        btnAnadir.Text = RecuperarValorDic("btnAnadir")
        lblDivisa.Text = RecuperarValorDic("lblDivisa")
        lblLimite.Text = RecuperarValorDic("lblLimite")
    End Sub

    Public Sub ConfigurarControles()
        'Oculta la opcion de agregar nuevos limites
        divAlta.Visible = Modo <> Genesis.Comon.Enumeradores.Modo.Consulta

        'Oculta la columna de Borrar en modo consulta
        grid.Columns(2).Visible = Modo <> Genesis.Comon.Enumeradores.Modo.Consulta

    End Sub
    Public Sub CargaRegistrosDeBase()
        Limites = New List(Of Limite)
        Dim oidPlanificacion As String = String.Empty
        Dim oidMaquina As String = String.Empty
        Dim oidPuntoServicio As String = String.Empty

        If Planificacion IsNot Nothing Then
            oidPlanificacion = Planificacion.Identificador
        End If
        If Maquina IsNot Nothing Then
            oidMaquina = Maquina.Identificador
        End If
        If PuntoServicio IsNot Nothing Then
            oidPuntoServicio = PuntoServicio.Identificador
        End If

        If Not (String.IsNullOrWhiteSpace(oidPlanificacion) AndAlso String.IsNullOrWhiteSpace(oidMaquina) AndAlso String.IsNullOrWhiteSpace(oidPuntoServicio)) Then
            Limites = Genesis.LogicaNegocio.Genesis.Limites.ObtenerLimites(oidPlanificacion, oidMaquina, oidPuntoServicio)
        End If

        LimitesOriginal = Limites

        CargarGrilla()
    End Sub
    Private Sub CargaDivisas()
        'Solo va a buscar las divisas a la base de datos
        'en caso de no tenerlas cargadas y de no ser consulta
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
            If Limites IsNot Nothing AndAlso Limites.Count > 0 Then
                divisasCombo = Divisas.Where(Function(d) Not Limites.Exists(Function(l) l.Divisa.CodigoISO = d.CodigoISO)).ToList
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
        If Limites IsNot Nothing Then
            Limites = Limites.OrderBy(Function(a) a.Divisa.CodigoDescripcion).ToList()
        End If

        grid.DataSource = Limites
        grid.DataBind()
        CargarCombo()
        UpdatePanelGrid.Update()

    End Sub

    Public Function BuscarPeticionLimite() As List(Of Limite)

        Dim peticionMae As New List(Of Limite)

        ' baja: todo que esta en LimitesOriginal y no esta Limites
        For Each limitOriginal In LimitesOriginal.Where(Function(x) Limites.FirstOrDefault(Function(y) y.Divisa.CodigoISO = x.Divisa.CodigoISO) Is Nothing)

            Dim obj As Limite = New Limite With {
                .Accion = "BAJA",
                .Divisa = limitOriginal.Divisa,
                .NumLimite = limitOriginal.NumLimite
            }

            peticionMae.Add(obj)
        Next

        ' alta: todo que esta en Limites y no esta LimitesOriginal
        For Each limit In Limites.Where(Function(x) LimitesOriginal.FirstOrDefault(Function(y) y.Divisa.CodigoISO = x.Divisa.CodigoISO) Is Nothing)
            Dim obj As Limite = New Limite With {
                .Accion = "ALTA",
                .Divisa = limit.Divisa,
                .NumLimite = limit.NumLimite
            }

            peticionMae.Add(obj)
        Next


        ' modificar: todo que esta en LimitesOriginal y esta Limites y se cambió el nímero de límite
        For Each limit In Limites.Where(Function(x) LimitesOriginal.FirstOrDefault(Function(y) y.Divisa.CodigoISO = x.Divisa.CodigoISO) IsNot Nothing)

            Dim limitOriginal = LimitesOriginal.FirstOrDefault(Function(x) x.Divisa.CodigoISO = limit.Divisa.CodigoISO)

            Dim obj As Limite

            'Si se modificó el valor, agrgo la baja y luego el alta
            'Se deben procesar primero las bajas y luego las altas
            If (limitOriginal.NumLimite <> limit.NumLimite) Then
                'Agrego la baja
                obj = New Limite With {
                    .Accion = "BAJA",
                    .Divisa = limitOriginal.Divisa,
                    .NumLimite = limitOriginal.NumLimite
                }
                peticionMae.Add(obj)


                'Agrego el alta
                obj = New Limite With {
                    .Accion = "ALTA",
                    .Divisa = limit.Divisa,
                    .NumLimite = limit.NumLimite
                }
                peticionMae.Add(obj)

            End If

        Next

        Return peticionMae

    End Function

#End Region


End Class