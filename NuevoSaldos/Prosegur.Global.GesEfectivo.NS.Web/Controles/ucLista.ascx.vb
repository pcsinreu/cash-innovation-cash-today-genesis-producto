Imports Prosegur.Framework.Dicionario.Tradutor

Public Class ucLista
    Inherits UcBase


#Region "[PROPRIEDADES]"

    Public Property modo As Genesis.Comon.Enumeradores.Modo
    Public Property titulo As String
    Public Property width As Integer = 200
    Public Property height As Integer = 150

    Public Property cargarLista As String = Nothing
    Public Property obtenerValores As String = Nothing

    Public Property TodosSelecionados As Boolean

    Public Property Lista As Dictionary(Of String, String)
        Get
            If ViewState(ID & "_Lista") Is Nothing Then
                ViewState(ID & "_Lista") = New Dictionary(Of String, String)
            End If
            Return ViewState(ID & "_Lista")
        End Get
        Set(value As Dictionary(Of String, String))
            ViewState(ID & "_Lista") = value

            phLista.Controls.Clear()
            CantidadItens = Lista.Count()

            ConfigurarControles()
        End Set
    End Property
    Public Property legenda_AgregarTodos As String
    Public Property legenda_DesAgregarTodos As String

    Public Property valoresSeleccionados() As List(Of String)
        Get
            Dim itens = New List(Of String)
            For Each item In hidValoresSeccionados.Value.Split(";")
                If Not String.IsNullOrEmpty(item.Trim) Then
                    itens.Add(item)
                End If
            Next

            Dim cantidad As Integer = 0
            Integer.TryParse(hidCantidadItens.Value, cantidad)

            If cantidad = 0 Then
                cantidad = Me.CantidadItens
            End If

            If itens.Count = cantidad Then
                Me.TodosSelecionados = True
            Else
                Me.TodosSelecionados = False
            End If

            Return itens
        End Get
        Set(value As List(Of String))

            If value Is Nothing Then
                hidValoresSeccionados.Value = ""

            Else
                hidValoresSeccionados.Value = ""
                For Each item In value
                    hidValoresSeccionados.Value &= item & ";"
                Next
            End If
        End Set
    End Property

    Public Property CantidadItens() As Integer
        Get
            If Session(ID & "_CantidadItens") Is Nothing Then
                Session(ID & "_CantidadItens") = 0
            End If

            Return Session(ID & "_CantidadItens")
        End Get
        Set(value As Integer)
            Session(ID & "_CantidadItens") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Inicializa controles na tela.
    ''' </summary>
    Protected Overrides Sub Inicializar()
        Try
            Me.ConfigurarControles()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Configura foco nos UserControls 
    ''' </summary>
    Public Overrides Sub Focus()
        MyBase.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"
    Public Class ValoresSeleccionadosListaEventArgs
        Inherits System.EventArgs

    End Class

    Public Event ValoresSelecionadosLista(sender As Object, e As ValoresSeleccionadosListaEventArgs)

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Configura Controles do Controle. Ignorando o Overrides.
    ''' </summary>
    Protected Sub TraduzirControle()
        lblTitulo.Text = titulo
        If String.IsNullOrEmpty(legenda_DesAgregarTodos) Then
            legenda_DesAgregarTodos = Traduzir("btnDesAgregarTodos")
        End If
        If String.IsNullOrEmpty(legenda_AgregarTodos) Then
            legenda_AgregarTodos = Traduzir("btnAgregarTodos")
        End If
    End Sub

    Public Sub LimpiarDatos()
        phLista.Controls.Clear()
    End Sub

    ''' <summary>
    ''' Carrega Canal e SubCanal utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "script_UcLista", ResolveUrl("~/js/UcLista.js"))

        If width = 0 Then
            dvValores.Style.Item("width") = "auto"
        Else
            dvValores.Style.Item("width") = width & "px"
        End If

        If height = 0 Then
            dvValores.Style.Item("height") = "auto"
        Else
            dvValores.Style.Item("height") = height & "px"
        End If

        If Lista IsNot Nothing AndAlso Lista.Count > 0 Then
            phLista.Controls.Clear()
            CantidadItens = Lista.Count()
            For Each item In Lista
                If modo = Genesis.Comon.Enumeradores.Modo.Consulta Then
                    phLista.Controls.Add(New LiteralControl("<li>" & item.Value & "</li>"))
                Else
                    Dim NewCheckBox As HtmlInputCheckBox = New HtmlInputCheckBox()
                    NewCheckBox.ID = item.Key
                    NewCheckBox.Value = item.Key

                    If Not String.IsNullOrEmpty(cargarLista) AndAlso Not String.IsNullOrEmpty(obtenerValores) Then
                        NewCheckBox.Attributes.Add("onchange", "SeleccionarValorObtenerLista('" & item.Key & "', '" & Me.ClientID & "_" & NewCheckBox.ClientID & "', '" & hidValoresSeccionados.ClientID & "', '" & cargarLista & "', '" & obtenerValores & "')")
                    Else
                        NewCheckBox.Attributes.Add("onchange", "SeleccionarValor('" & item.Key & "', '" & Me.ClientID & "_" & NewCheckBox.ClientID & "', '" & hidValoresSeccionados.ClientID & "')")
                    End If
                    'dvctl00_ContentPlaceHolder1_listaSectores_1a62ad3d-544d-482f-bd33-0941ec3ab424
                    phLista.Controls.Add(New LiteralControl("<div id='dv" & Me.ClientID & "_" & NewCheckBox.ClientID & "' style='width: 100%; display: inline-block;'>"))
                    If valoresSeleccionados IsNot Nothing AndAlso valoresSeleccionados.Contains(item.Key) Then
                        NewCheckBox.Checked = True
                    End If
                    phLista.Controls.Add(NewCheckBox)
                    phLista.Controls.Add(New LiteralControl(item.Value & "</div>"))
                End If
            Next

        End If

        If modo = Genesis.Comon.Enumeradores.Modo.Consulta Then
            dvAcciones.Style.Item("display") = "none"
        Else
            dvAcciones.Style.Item("display") = "block"
            litScripts.Text = "<script type='text/javascript'> atribuirFuncion(document.getElementById('" & dvValores.ClientID & "')); </script>"
        End If

        btnAgregarTodos.Attributes.Add("onclick", "seleccionarTodos(document.getElementById('" & Me.dvValores.ClientID & "'), true, '" & Me.hidValoresSeccionados.ClientID & "', '" & cargarLista & "', '" & obtenerValores & "')")
        btnAgregarTodos.Attributes.Add("onfocus", "cambiarLegenda(document.getElementById('" & Me.legenda.ClientID & "'), '" & legenda_DesAgregarTodos & "')")

        btnDesAgregarTodos.Attributes.Add("onclick", "seleccionarTodos(document.getElementById('" & Me.dvValores.ClientID & "'), false, '" & Me.hidValoresSeccionados.ClientID & "', '" & cargarLista & "', '" & obtenerValores & "')")
        btnDesAgregarTodos.Attributes.Add("onfocus", "cambiarLegenda(document.getElementById('" & Me.legenda.ClientID & "'), '" & legenda_AgregarTodos & "')")
        btnDesAgregarTodos.Attributes.Add("onkeydown", "HomeEnd()")

    End Sub

    ''' <summary>
    ''' Trata mensgem de erro dos Controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.NotificarErro(e.Erro)
    End Sub

#End Region

End Class