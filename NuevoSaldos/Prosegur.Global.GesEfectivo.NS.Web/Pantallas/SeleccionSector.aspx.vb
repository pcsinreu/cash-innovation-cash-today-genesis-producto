Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Aplicacao.Util
Imports System.Reflection
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Web.Login
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports Newtonsoft.Json

Public Class SeleccionSector
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub Inicializar()


        Dim TempoInicial As DateTime = Now
        Dim Tempo As New StringBuilder

        Try

            If (InformacionUsuario.Permisos.Count = 0) Then
                Response.Redireccionar(Constantes.NOME_PAGINA_LOGIN & "?SesionExpirada=1")
            End If

            verificarSector()
            configurarSessionDefecto()
            configurarItens()

            Tempo.AppendLine(Now.Subtract(TempoInicial).ToString())

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        MyBase.DefinirParametrosBase()
        'MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.SELECCION_SECTOR
        MyBase.ValidarAcesso = False
        MyBase.ValidarPemissaoAD = False

    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("030_titulo_pagina")
        litDicionario.Text = "msg_loading = '" & Traduzir("msg_loading") & "';"
        litDicionario.Text &= "msg_CargandoSectores = '<strong>" & Traduzir("030_msg_CargandoSector") & "</strong>';"
        litDicionario.Text &= "msg_redirecionando = '<strong>" & Traduzir("000_msg_redirecionando") & "</strong>';"
        litDicionario.Text &= "msg_producidoError = '<strong>" & Traduzir("msg_producidoError") & "</strong>';"
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub configurarSessionDefecto()

        ' Session utilizada en la pantalla de Documentos.aspx
        Session("ultimaConsultaDocumento") = Nothing

        ' Calcula a cuantidad de registro por pagina en un Grid
        If Not String.IsNullOrEmpty(hidHeightPantalla.Value) AndAlso IsNumeric(hidHeightPantalla.Value) Then
            Dim h As Integer = hidHeightPantalla.Value / 50
            Session("MaximoRegistrosPageGrid") = If(h > 10, h, 10)
        Else
            Session("MaximoRegistrosPageGrid") = 10
        End If

        Master.FindControl("pnlMenuRodape").Visible = False

        Me.Master.HaySoloUnoSector = False

    End Sub

    Private Sub configurarItens()

        Dim _itens As ObservableCollection(Of ItemSeleccion) = cargarItensMemoria()

        If _itens IsNot Nothing AndAlso _itens.Count > 0 Then

            litSectores.Text = "treedata_avm = [ "
            For Each _item In _itens

                litSectores.Text &= cargarItensPantalla(_item)

            Next
            litSectores.Text = litSectores.Text.Substring(0, litSectores.Text.Length - 1)
            litSectores.Text &= " ];"

        End If
        
    End Sub

    Private Function cargarItensMemoria() As ObservableCollection(Of ItemSeleccion)

        Dim _itens As New ObservableCollection(Of ItemSeleccion)

        ' Nivel Delegación
        For Each delegacion In InformacionUsuario.Delegaciones

            '== Inicio validar delegación (Nivel1) ==
            Dim itemNivel1 As ItemSeleccion = Nothing
            If _itens IsNot Nothing AndAlso _itens.Count > 0 Then
                itemNivel1 = _itens.FirstOrDefault(Function(x) x.identificador = delegacion.Identificador)
            End If
            If itemNivel1 Is Nothing Then
                itemNivel1 = New ItemSeleccion With {.identificador = delegacion.Identificador,
                                               .children = New ObservableCollection(Of ItemSeleccion),
                                               .label = delegacion.Descripcion.Trim().Replace("'", "´"),
                                               .link = False,
                                               .expand = True}
                _itens.Add(itemNivel1)
            End If
            '== Fim validar delegación (Nivel1) ==

            If itemNivel1 IsNot Nothing AndAlso delegacion.Plantas IsNot Nothing Then
                ' Nivel Planta
                For Each planta In delegacion.Plantas

                    '== Inicio validar planta (Nivel2) ==
                    Dim itemNivel2 As ItemSeleccion = Nothing
                    If itemNivel1.children IsNot Nothing AndAlso itemNivel1.children.Count > 0 Then
                        itemNivel2 = itemNivel1.children.FirstOrDefault(Function(x) x.identificador = planta.Identificador)
                    End If
                    If itemNivel2 Is Nothing Then
                        itemNivel2 = New ItemSeleccion With {.identificador = planta.Identificador,
                                                       .children = New ObservableCollection(Of ItemSeleccion),
                                                       .label = planta.Descripcion.Trim().Replace("'", "´"),
                                                       .link = False,
                                                       .expand = True}
                        itemNivel1.children.Add(itemNivel2)
                    End If
                    '== Fim validar planta (Nivel2) ==

                    If planta.Sectores IsNot Nothing AndAlso planta.Sectores.Count > 0 Then
                        ' Nivel Sectores

                        '== Inicio validar Sectores (Nivel3) ==
                        For Each sector In planta.Sectores.FindAll(Function(x) x.SectorPadre Is Nothing)
                            Dim itemNivel3 As ItemSeleccion = Nothing
                            If itemNivel2.children IsNot Nothing AndAlso itemNivel2.children.Count > 0 Then
                                itemNivel3 = itemNivel2.children.FirstOrDefault(Function(x) x.identificador = planta.Identificador)
                            End If
                            If itemNivel3 Is Nothing Then
                                itemNivel3 = New ItemSeleccion With {.identificador = sector.Identificador,
                                                               .children = New ObservableCollection(Of ItemSeleccion),
                                                               .label = sector.Descripcion.Trim().Replace("'", "´"),
                                                               .link = True,
                                                               .expand = True}
                                If sector.EsPuesto Then
                                    itemNivel3.label = itemNivel3.label.ToUpper
                                    itemNivel3.esPuesto = True
                                End If

                                itemNivel2.children.Add(itemNivel3)
                            End If
                        Next
                        '== Fim validar Sectores (Nivel3) ==

                        '== Inicio validar Sectores (SubItens) ==
                        Dim _subSectores As ObservableCollection(Of Sector) = planta.Sectores.FindAll(Function(x) x.SectorPadre IsNot Nothing).Clonar()
                        Dim cuantidad As Integer = 0
                        While _subSectores.Count > 0

                            cuantidad = _subSectores.Count

                            cargarSectorHijo(_subSectores, itemNivel2)

                            If cuantidad = _subSectores.Count Then
                                Exit While
                            End If

                        End While

                        '== Fim validar Sectores (SubItens) ==

                    End If

                Next


            End If

        Next

        Return _itens

    End Function

    Private Sub cargarSectorHijo(ByRef _subSectores As ObservableCollection(Of Sector), ByRef itemNivel2 As ItemSeleccion)

        For Each sector In _subSectores.Clonar

            'If _subSectores.FirstOrDefault(Function(x) x.Identificador = sector.Identificador) IsNot Nothing Then

            Dim itemNivelN As New ItemSeleccion With {.identificador = sector.Identificador,
                                           .children = New ObservableCollection(Of ItemSeleccion),
                                           .label = sector.Descripcion.Trim().Replace("'", "´"),
                                           .link = True,
                                           .expand = True}

            If sector.EsPuesto Then
                itemNivelN.label = itemNivelN.label.ToUpper
                itemNivelN.esPuesto = True
            End If

            Dim fueAnadido As Boolean = cargarHijo(itemNivel2.children, itemNivelN, sector.SectorPadre.Identificador)

            If fueAnadido Then
                _subSectores.Remove(_subSectores.FirstOrDefault(Function(x) x.Identificador = sector.Identificador))
            End If

            ' End If

        Next

    End Sub

    Private Function cargarHijo(ByRef posiblesPadres As ObservableCollection(Of ItemSeleccion), item As ItemSeleccion, identificadorPadre As String) As Boolean

        If posiblesPadres IsNot Nothing AndAlso posiblesPadres.Count > 0 Then

            For Each padre In posiblesPadres

                If padre.identificador = identificadorPadre Then

                    padre.children.Add(item)
                    Return True

                ElseIf padre.children.Count > 0 Then

                    If cargarHijo(padre.children, item, identificadorPadre) Then
                        Return True
                    End If

                End If

            Next

        End If
        Return False
    End Function

    Private Function cargarItensPantalla(item As ItemSeleccion) As String

        Dim _respuesta As String = ""

        If item IsNot Nothing Then

            _respuesta &= "{ label: '" & item.label & "' "

            If item.link Then
                _respuesta &= ", onSelect: function(branch){ seleccionarSector('" & item.identificador & "'); }"
            End If

            If item.esPuesto Then
                _respuesta &= ", esPuesto: true"
            Else
                _respuesta &= ", esPuesto: false"
            End If

            If item.children IsNot Nothing AndAlso item.children.Count > 0 Then

                _respuesta &= ", children: [ "

                For Each subItem In item.children
                    _respuesta &= cargarItensPantalla(subItem)
                Next

                _respuesta = _respuesta.Substring(0, _respuesta.Length - 1)
                _respuesta &= " ] "

            End If

            _respuesta &= " } ,"

        End If

        Return _respuesta

    End Function

    Private Sub verificarSector()

        Dim identificadorSector As String = Request.QueryString("IdentificadorSector")
        If Not String.IsNullOrEmpty(identificadorSector) Then
            Continuar(LogicaNegocio.Genesis.Sector.ObtenerPorOid(identificadorSector, False, False))
        End If

    End Sub

    Private Sub Continuar(sectorSeleccionado As Sector)
        Try
            Base.InformacionUsuario.SectorSeleccionado = sectorSeleccionado
            Dim urlRedirect As String = Request.QueryString("UrlRedirect")
            If Not String.IsNullOrEmpty(urlRedirect) Then
                Response.Redireccionar(System.Text.UTF8Encoding.UTF8.GetString(System.Web.HttpServerUtility.UrlTokenDecode(urlRedirect)))
            Else

                If Base.PossuiPermissao(Utilidad.eTelas.FORMULARIOS) Then
                    Response.Redireccionar("~/Pantallas/Formularios.aspx")
                Else
                    Response.Redireccionar("~/Default.aspx")
                End If

            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

#Region "[EVENTOS]"

    <System.Web.Services.WebMethod()> _
    Public Shared Function SeleccionSector(identificadorSector As String) As String

        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try
            If Not String.IsNullOrEmpty(identificadorSector) Then
                Base.InformacionUsuario.SectorSeleccionado = LogicaNegocio.Genesis.Sector.ObtenerPorOid(identificadorSector, False, False)
            End If

            If Base.InformacionUsuario.SectorSeleccionado Is Nothing Then
                Throw New Exception(Traduzir("030_msg_ErrorCargarSector"))
            Else
                'If Base.InformacionUsuario.TipoSector IsNot Nothing Then

                '    Dim _tiposector = Base.InformacionUsuario.TipoSector.FirstOrDefault(Function(x) x.Codigo = Base.InformacionUsuario.SectorSeleccionado.TipoSector.Codigo)
                '    If _tiposector IsNot Nothing Then
                '        Base.InformacionUsuario.SectorSeleccionado.TipoSector.Permisos = _tiposector.Permisos
                '    Else

                '        ' Neste cenario, o usuario logou em uma delegação diferente da selecionada
                '        ' E neste caso o TipoSetor do setor selecionado, não existe na lista disponivel
                '        ' TO DO

                '    End If

                'End If
            End If

            _respuesta.Respuesta = "Formularios.aspx"

        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.Message
            _respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(_respuesta)
    End Function

#End Region

End Class

Public Class ItemSeleccion

    Public label As String
    Public children As ObservableCollection(Of ItemSeleccion)
    Public expand As Boolean
    Public link As Boolean
    Public esPuesto As Boolean
    Public identificador As String

End Class