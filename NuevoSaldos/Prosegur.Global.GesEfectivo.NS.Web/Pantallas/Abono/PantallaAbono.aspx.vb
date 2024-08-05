Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Report
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Util
Imports System.Reflection
Imports Newtonsoft.Json
Imports System.IO
Imports System.Web.Services
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros

Public Class PantallaAbono
    Inherits Base

#Region "[VARIAVEIS]"

    Shared canalSaldos As Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor

#End Region

#Region "[PROPRIEDADES]"

    Private _Modo As Enumeradores.Modo?
    Public ReadOnly Property Modo() As Enumeradores.Modo
        Get
            If Not _Modo.HasValue Then
                _Modo = [Enum].Parse(GetType(Enumeradores.Modo), Request.QueryString("Modo"), True)
            End If
            Return _Modo.Value
        End Get

    End Property


#End Region

#Region "[OVERRIDES]"
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.ABONO
        MyBase.ValidarAcesso = True
        MyBase.ValidarPemissaoAD = True
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("071_Abono_Titulo")
        Dim _Diccionario As New Dictionary(Of String, String)() From {
            {"msg_loading", Traduzir("071_Comon_msg_loading")},
            {"msg_obtenerValores", Traduzir("071_Comon_msg_obtenerValores")},
            {"msg_nenhumRegistroEncontrado", Traduzir("071_Comon_msg_nenhumRegistroEncontrado")},
            {"msg_producidoError", Traduzir("071_Comon_msg_producidoError")}
        }

        litDicionario.Text = "<script> var _Diccionario = JSON.parse('" & JsonConvert.SerializeObject(_Diccionario) & "'); </script>"
    End Sub

    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
    End Sub

    Protected Overrides Sub Inicializar()
        If Not IsPostBack Then
            Me.InicializarDados()
        End If
    End Sub

#End Region

#Region "[METODOS]"

    <System.Web.Services.WebMethod()> _
    Public Shared Function obtenerUserControl(name As String) As String

        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try
            If Not String.IsNullOrEmpty(name) Then

                Using _Page As New Page()
                    Dim userControl As UserControl = _Page.LoadControl("~\Controles\" & name & ".ascx")
                    _Page.Controls.Add(userControl)
                    Using writer As New StringWriter()
                        HttpContext.Current.Server.Execute(_Page, writer, False)
                        _respuesta.Respuesta = writer.ToString()
                    End Using
                End Using

            End If

        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.ToString
            _respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(_respuesta)

    End Function

#End Region

    Private Sub InicializarDados()
        Dim respuesta As New ContractoServicio.Comon.BaseRespuestaJSON
        Dim abono As Clases.Abono.Abono

        If Me.Modo = Enumeradores.Modo.Modificacion Then
            'Buscar abono do banco
            abono = LogicaNegocio.GenesisSaldos.Abono.ObtenerAbono(Me.Request.QueryString("IdentificadorAbono"), InformacionUsuario.DelegacionSeleccionada.Identificador, InformacionUsuario.DelegacionSeleccionada.Codigo)
            If abono.CodigoEstado <> Enumeradores.EstadoAbono.EnCurso Then
                Response.Redireccionar("~/Pantallas/Abono/Busqueda.aspx")
            End If

            For Each abonoValor In abono.AbonosValor
                'TODO - passar o solicitante
                ObtenerDatosBancarios(abonoValor, abono.TipoAbono, abono.Bancos(0).Identificador)
            Next
        Else
            'Novo Abono
            abono = LogicaNegocio.GenesisSaldos.Abono.ObtenerNovoAbono(InformacionUsuario.DelegacionSeleccionada)
        End If

        canalSaldos = LogicaNegocio.Genesis.Canal.ObtenerCanalPatronJSON("0")

        'Carregar TipoAbono
        Dim listaTiposAbono = New List(Of Object)
        Dim filtro As Clases.Transferencias.FiltroConsultaValoresAbono = Nothing
        If (abono.CodigoEstado = Enumeradores.EstadoAbono.Nuevo) Then
            listaTiposAbono.Add(New With {.Codigo = "NoDefinido", .Descripcion = Traduzir("071_Comon_Valores_Seleccionar"), .Filtro = Nothing})
        End If
        For Each tipoAbono As Enumeradores.TipoAbono In [Enum].GetValues(GetType(Enumeradores.TipoAbono))
            If tipoAbono <> Enumeradores.TipoAbono.NoDefinido Then
                Dim codigo As String = Comon.Extenciones.EnumExtension.RecuperarValor(tipoAbono)

                'GENIPLANT-2160, Não deve salvar nem carregar mais configurações de filtro pré preenchido
                'If (tipoAbono = Enumeradores.TipoAbono.Elemento) Then
                'filtro = PreferenciasAplicacion.ObternerPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
                '            (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_ELEMENTO)
                'ElseIf (tipoAbono = Enumeradores.TipoAbono.Saldos) Then
                '    filtro = PreferenciasAplicacion.ObternerPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
                '                            (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_SALDOS)
                'ElseIf (tipoAbono = Enumeradores.TipoAbono.Pedido) Then
                '    filtro = PreferenciasAplicacion.ObternerPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
                '                            (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_PEDIDO)
                'End If

                'Sempre carregar filtros
                'If filtro Is Nothing Then
                If (abono.CodigoEstado <> Enumeradores.EstadoAbono.Nuevo AndAlso tipoAbono = abono.TipoAbono) Then
                    Dim abonoFiltro = New Clases.Abono.Abono() With {.TipoAbono = tipoAbono, .CodigoEstado = abono.CodigoEstado, .AbonosValor = abono.AbonosValor}

                    filtro = Me.RetornaFiltrosAbono(abonoFiltro, False)
                    filtro.Clientes = New List(Of Clases.Abono.AbonoInformacion)()

                    If abono.TipoAbono = Enumeradores.TipoAbono.Pedido AndAlso abono.Bancos IsNot Nothing AndAlso abono.Bancos.Count > 0 Then
                        filtro.Clientes.Add(abono.Bancos(0))
                    End If
                Else
                    filtro = Nothing
                End If

                'End If

                listaTiposAbono.Add(New With
                                    {
                                        .Codigo = tipoAbono.ToString(),
                                        .Descripcion = Traduzir("071_Comon_Valores_TipoAbono_" & codigo),
                                        .Filtro = filtro
                                    })
            End If
        Next

        'Obtém o filtro pelo tipo abono
        filtro = listaTiposAbono.Where(Function(t) t.Codigo = abono.TipoAbono.ToString()) _
                     .Select(Function(t) t.Filtro) _
                     .FirstOrDefault()
        Dim listaResultadoFiltro = Me.RetornaResultadoFiltro(filtro, abono)

        'Carregar lista Valores Efectivo
        Dim listaValoresEfectivo = New List(Of Object)
        'Esto campo define cuales valores deberán ser utilizados para abonar. Dos opciones: DECLARADOS y CONTADOS. Campo desprotegido.
        'listaValoresEfectivo.Add(New With {.Codigo = "NoDefinido", .Descripcion = Traduzir("071_Comon_Valores_Seleccionar")})
        For Each tipoValorAbono In [Enum].GetValues(GetType(Clases.Abono.TipoValorAbono))
            If tipoValorAbono <> Clases.Abono.TipoValorAbono.NoDefinido Then
                Dim codigo As String = Comon.Extenciones.EnumExtension.RecuperarValor(tipoValorAbono)
                listaValoresEfectivo.Add(New With {.Codigo = Convert.ToString(tipoValorAbono), .Descripcion = Traduzir("071_Comon_Valores_TipoValor_" & codigo)})
            End If
        Next

        'Validações do formulário
        Dim _permiteSaldoNegativo As Boolean = True
        Dim _codigoFormulario As String = String.Empty
        Dim _descripcionFormulario As String = String.Empty

        LogicaNegocio.GenesisSaldos.Abono.VerificarFormularioDocPasesPermiteSaldoNegativo(InformacionUsuario.SectorSeleccionado.Identificador, Parametros.Permisos.Usuario.Login, _descripcionFormulario, _codigoFormulario, _permiteSaldoNegativo)

        'Carregar Diccionarios
        Dim diccionarios = New Dictionary(Of String, String)() From {
            {"msg_loading", Traduzir("071_Comon_msg_loading")},
            {"msg_obtenerValores", Traduzir("071_Comon_msg_obtenerValores")},
            {"msg_nenhumRegistroEncontrado", Traduzir("071_Comon_msg_nenhumRegistroEncontrado")},
            {"msg_producidoError", Traduzir("071_Comon_msg_producidoError")},
            {"msg_ValidacaoFiltro", Traduzir("071_Abono_msg_ValidacaoFiltro")},
            {"msg_ValidacionTerminosIllenos", Traduzir("071_Abono_msg_ValidacionTerminosIllenos")},
            {"msg_RetornandoResultadoFiltro", Traduzir("071_Abono_msg_RetornandoResultadoFiltro")},
            {"msg_VinculandoAbono", Traduzir("071_Abono_msg_VinculandoAbono")},
            {"msg_VinculandoAbonos", Traduzir("071_Abono_msg_VinculandoAbonos")},
            {"msg_EliminandoAbono", Traduzir("071_Abono_msg_EliminandoAbono")},
            {"msg_EliminandoAbonos", Traduzir("071_Abono_msg_EliminandoAbonos")},
            {"msg_GrabandoAbono", Traduzir("071_Abono_msg_GrabandoAbono")},
            {"msg_GrabadoElAbono", Traduzir("071_Abono_msg_GrabadoElAbono")},
            {"msg_ConfirGrabaryFinalizarAbono", Traduzir("071_Abono_msg_ConfirGrabaryFinalizarAbono")},
            {"msg_GrabandoyFinalizandoAbono", Traduzir("071_Abono_msg_GrabandoyFinalizandoAbono")},
            {"msg_GrabadoyFinalizadoElAbono", Traduzir("071_Abono_msg_GrabadoyFinalizadoElAbono")},
            {"msg_ConfirAnulandoAbono", Traduzir("071_Abono_msg_ConfirAnulandoAbono")},
            {"msg_AnulandoAbono", Traduzir("071_Abono_msg_AnulandoAbono")},
            {"msg_AnuladoElAbono", Traduzir("071_Abono_msg_AnuladoElAbono")},
            {"msg_AbonoSinInformarValor", Traduzir("071_Detalle_msg_AbonoSinInformarValor")},
            {"msg_NingunDivisaConValorDisp", Traduzir("071_Detalle_msg_NingunDivisaConValorDisp")},
            {"msg_ConfirmarExcluirAbonoEle", Traduzir("071_Detalle_msg_ConfirmarExcluirAbonoEle")},
            {"msg_DivisasConValoresDisp", Traduzir("071_Detalle_msg_DivisasConValoresDisp")},
            {"msg_DivisasConLaMismaCuenta", Traduzir("071_Detalle_msg_DivisasConLaMismaCuenta")},
            {"msg_ListaCuentasOCuentaVacia", Traduzir("071_Detalle_msg_ListaCuentasOCuentaVacia")},
            {"msg_CamposDocPasesObligatorio", Traduzir("071_Comon_msg_CamposDocPasesObligatorio")},
            {"msg_CamposCadastroDatosBancarios", Traduzir("071_Comon_msg_CamposCadastroDatosBancarios")},
            {"msg_CuentaEstandarYaExiste", Traduzir("071_msg_CuentaEstandarYaExiste")},
            {"Comon_Campo_Banco", Traduzir("071_Comon_Campo_Banco")},
            {"Comon_Campo_Banco_Saldos", Traduzir("071_Comon_Campo_Banco_Saldos")},
            {"Abono_Grid_TituloColumna_Cliente_Saldos", Traduzir("071_Abono_Grid_TituloColumna_Cliente_Saldos")},
            {"Comon_Campo_Cliente_Saldos", Traduzir("071_Comon_Campo_Cliente_Saldos")},
            {"Detalle_Campo_Cliente_Saldos", Traduzir("071_Detalle_Campo_Cliente_Saldos")},
            {"Abono_Grid2_TituloColumna_Cliente_Saldos", Traduzir("071_Abono_Grid2_TituloColumna_Cliente_Saldos")},
            {"Comon_Campo_Banco_Pedido", Traduzir("071_Comon_Campo_Banco_Pedido")},
            {"Abono_Grid_TituloColumna_Cliente_Pedido", Traduzir("071_Abono_Grid_TituloColumna_Cliente_Pedido")},
            {"Comon_Campo_Cliente_Pedido", Traduzir("071_Comon_Campo_Cliente_Pedido")},
            {"Detalle_Campo_Cliente_Pedido", Traduzir("071_Detalle_Campo_Cliente_Pedido")},
            {"Abono_Grid2_TituloColumna_Cliente_Pedido", Traduzir("071_Abono_Grid2_TituloColumna_Cliente_Pedido")},
            {"Comon_Campo_Banco_Elemento", Traduzir("071_Comon_Campo_Banco_Elemento")},
            {"Abono_Grid_TituloColumna_Cliente_Elemento", Traduzir("071_Abono_Grid_TituloColumna_Cliente_Elemento")},
            {"Comon_Campo_Cliente_Elemento", Traduzir("071_Comon_Campo_Cliente_Elemento")},
            {"Detalle_Campo_Cliente_Elemento", Traduzir("071_Detalle_Campo_Cliente_Elemento")},
            {"Abono_Grid2_TituloColumna_Cliente_Elemento", Traduzir("071_Abono_Grid2_TituloColumna_Cliente_Elemento")},
            {"msg_PermiteSaldoNegativo", String.Format(Traduzir("071_msg_NoPermiteSaldoNegativo"), _descripcionFormulario, _codigoFormulario)}
        }

        respuesta.Respuesta = New With
                            {
                                .Abono = abono,
                                .ListaResultadoFiltro = listaResultadoFiltro,
                                .ListaTiposAbono = listaTiposAbono,
                                .ListaValoresEfectivo = listaValoresEfectivo,
                                .Diccionarios = diccionarios,
                                .NombreUsuario = InformacionUsuario.Nombre,
                                .PermiteSaldoNegativo = _permiteSaldoNegativo
                            }

        ClientScript.RegisterStartupScript(Me.GetType(), "LOAD", "InicializarPantallaAbonoVM(" & JsonConvert.SerializeObject(respuesta, New Converters.StringEnumConverter()) & ");", True)

    End Sub

    Public Function RetornaFiltrosAbono(abono As Clases.Abono.Abono, esLimpiar As Boolean) As Clases.Transferencias.FiltroConsultaValoresAbono
        Dim filtro As Clases.Transferencias.FiltroConsultaValoresAbono = Nothing

        'GENIPLANT-2160, Não deve salvar nem carregar mais configurações de filtro pré preenchido
        'If (abono.TipoAbono = Enumeradores.TipoAbono.Elemento) Then
        '    filtro = PreferenciasAplicacion.ObternerPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
        '                (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_ELEMENTO)
        'ElseIf (abono.TipoAbono = Enumeradores.TipoAbono.Saldos) Then
        '    filtro = PreferenciasAplicacion.ObternerPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
        '                            (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_SALDOS)
        'ElseIf (abono.TipoAbono = Enumeradores.TipoAbono.Pedido) Then
        '    filtro = PreferenciasAplicacion.ObternerPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
        '                            (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_PEDIDO)
        'End If

        If (filtro Is Nothing) Then
            filtro = New Clases.Transferencias.FiltroConsultaValoresAbono()

            filtro.ValoresElegiveis.Add(New Clases.Transferencias.OpcionElegivel(Traduzir("071_Comon_Valores_TipoEfectivo"), "codefec"))

            If (abono.TipoAbono = Enumeradores.TipoAbono.Saldos OrElse abono.TipoAbono = Enumeradores.TipoAbono.Pedido) AndAlso Not esLimpiar Then

                filtro.ValoresElegiveis(0).Elegivel = True

                If canalSaldos IsNot Nothing Then
                    Dim canal As New Comon.Clases.Abono.AbonoInformacion
                    canal.Identificador = canalSaldos.Identificador
                    canal.Codigo = canalSaldos.Codigo
                    canal.Descripcion = canalSaldos.Descripcion
                    filtro.Canales.Add(canal)
                End If

            End If

            For Each tipoMedioPago In [Enum].GetValues(GetType(Enumeradores.TipoMedioPago))
                If (tipoMedioPago <> Enumeradores.TipoMedioPago.NoDefinido) Then
                    Dim codigo As String = Comon.Extenciones.EnumExtension.RecuperarValor(tipoMedioPago)
                    filtro.ValoresElegiveis.Add(New Clases.Transferencias.OpcionElegivel(Traduzir("071_Comon_Valores_TipoMedioPago_" & codigo), EnumExtension.RecuperarValor(tipoMedioPago)))
                End If
            Next
            filtro.ValoresElegiveis = filtro.ValoresElegiveis.OrderBy(Function(o) o.Opcion).ToList()

            Dim divisas = LogicaNegocio.Genesis.Divisas.ObtenerListaDivisas()
            For Each divisa In divisas
                filtro.DivisasElegiveis.Add(New Clases.Transferencias.OpcionElegivel(divisa.Descripcion, divisa.Identificador))
            Next
            filtro.DivisasElegiveis = filtro.DivisasElegiveis.OrderBy(Function(o) o.Opcion).ToList()

            If (abono.CodigoEstado <> Enumeradores.EstadoAbono.Nuevo) Then
                If (abono.TipoAbono = Enumeradores.TipoAbono.Elemento) Then
                    filtro.IdentificadoresElementosSeleccionados = abono.AbonosValor _
                        .Select(Function(a) a.AbonoElemento.IdentificadorRemesa) _
                        .Distinct() _
                        .ToList()
                    'Else
                    '    filtro.IdentificadoresElementosSeleccionados = (From av In abono.AbonosValor
                    '                                                    From avc In av.AbonoSaldo.ListaSaldoCuenta
                    '                                                    Select avc.IdentificadorCuenta).ToList()
                End If
            End If
            filtro.IdentificadoresDivisas = filtro.DivisasElegiveis.Where(Function(d) d.Elegivel).Select(Function(d) d.Identificador).ToList()
            filtro.IdentificadoresValores = filtro.ValoresElegiveis.Where(Function(d) d.Elegivel).Select(Function(d) d.Identificador).ToList()
            filtro.TipoAbono = abono.TipoAbono
        End If

        Return filtro
    End Function

    Public Function RetornaResultadoFiltro(filtro As Clases.Transferencias.FiltroConsultaValoresAbono, abono As Clases.Abono.Abono) As List(Of Clases.Abono.AbonoValor)
        Dim listaFiltro As New List(Of Clases.Abono.AbonoValor)()

        If (filtro IsNot Nothing AndAlso abono.TipoAbono <> Enumeradores.TipoAbono.NoDefinido) Then
            'Ajusta Opções elegíveis
            filtro.IdentificadoresValores = filtro.ValoresElegiveis.Where(Function(v) v.Elegivel).Select(Function(v) v.Identificador).ToList()
            filtro.IdentificadoresDivisas = filtro.DivisasElegiveis.Where(Function(d) d.Elegivel).Select(Function(d) d.Identificador).ToList()
            filtro.IdentificadorDelegacion = abono.Delegacion.Identificador

            'Buscar a lista de elementos a vincular se possuir filtro em preferencias
            listaFiltro = LogicaNegocio.GenesisSaldos.Abono.ObtenerDivisasAbonar(filtro, abono)

            'GENIPLANT-2160, Não deve salvar nem carregar mais configurações de filtro pré preenchido
            'If (filtro IsNot Nothing) Then
            '    If (abono.TipoAbono = Enumeradores.TipoAbono.Elemento) Then
            ' PreferenciasAplicacion.AtualizaPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
            '     (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_ELEMENTO, filtro)
            'ElseIf (abono.TipoAbono = Enumeradores.TipoAbono.Saldos) Then
            '    PreferenciasAplicacion.AtualizaPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
            '        (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_SALDOS, filtro)
            'ElseIf (abono.TipoAbono = Enumeradores.TipoAbono.Pedido) Then
            '    PreferenciasAplicacion.AtualizaPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
            '        (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_PEDIDO, filtro)
            'End If
            '    End If

        End If

        Return listaFiltro
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function RetornaResultadoFiltro(filtroJson As String, abonoJson As String) As String
        Dim respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try
            Dim pantalla As New PantallaAbono()
            Dim filtro As Clases.Transferencias.FiltroConsultaValoresAbono = Nothing
            Dim abono = JsonConvert.DeserializeObject(Of Clases.Abono.Abono)(abonoJson, New Converters.StringEnumConverter())

            If (String.IsNullOrEmpty(filtroJson)) Then
                filtro = pantalla.RetornaFiltrosAbono(abono, False)
            Else
                filtro = JsonConvert.DeserializeObject(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
                             (filtroJson, New Converters.StringEnumConverter())

                If filtro.TipoAbono <> abono.TipoAbono Then
                    filtro = pantalla.RetornaFiltrosAbono(abono, False)
                End If
            End If

            respuesta.Respuesta = New With
                                  {
                                      .ListaResultado = pantalla.RetornaResultadoFiltro(filtro, abono),
                                      .Filtro = filtro
                                  }

        Catch ex As Exception
            respuesta.CodigoError = "100"
            respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            respuesta.MensajeErrorDescriptiva = ex.ToString
            respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(respuesta, New Converters.StringEnumConverter())
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function ListaTerminosDocPases() As String
        Dim respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try
            respuesta.Respuesta = LogicaNegocio.GenesisSaldos.Abono.ObtenerTerminosFormularioDocPases(InformacionUsuario.DelegacionSeleccionada.Codigo)

            Dim Formulario As Clases.Formulario = LogicaNegocio.GenesisSaldos.Abono.ObtenerFormularioDocPases(InformacionUsuario.SectorSeleccionado.Identificador, Parametros.Permisos.Usuario.Login, Nothing)

            If Formulario IsNot Nothing AndAlso Not LogicaNegocio.GenesisSaldos.MaestroFormularios.FormularioVerificarSector(Formulario.Identificador, InformacionUsuario.SectorSeleccionado.Identificador, True) Then
                Throw New Excepciones.ExcepcionLogica(String.Format(Traduzir("071_msg_FormularioNoConfigurado"), InformacionUsuario.SectorSeleccionado.Descripcion, Formulario.Descripcion, Formulario.Codigo))
            End If
            'parametroFormularioDocPases.valorParametro


        Catch excepcionLogica As Excepciones.ExcepcionLogica
            respuesta.CodigoError = "10"
            respuesta.MensajeError = excepcionLogica.Message()
            respuesta.MensajeErrorDescriptiva = excepcionLogica.ToString
            respuesta.Respuesta = Nothing
        Catch excepcion As Exception
            respuesta.CodigoError = "100"
            respuesta.MensajeError = Traduzir("msg_producidoError") & excepcion.Message()
            respuesta.MensajeErrorDescriptiva = excepcion.ToString
            respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(respuesta, New Converters.StringEnumConverter())
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function AbonarDivisa(abonosJson As String, todos As Boolean, tipoAbonoStr As String, identificadorSolicitante As String) As String
        Dim respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try
            Dim abonosVincular = JsonConvert.DeserializeObject(Of List(Of Clases.Abono.AbonoValor))(abonosJson, New Converters.StringEnumConverter())
            Dim abonosInvalidos = New List(Of Clases.Abono.AbonoValor)
            Dim listaDivisa = New List(Of String)
            Dim tipoAbono = [Enum].Parse(GetType(Enumeradores.TipoAbono), tipoAbonoStr)

            For Each abonoVincular In abonosVincular
                If (tipoAbono = Enumeradores.TipoAbono.Pedido) Then


                End If

                ObtenerDatosBancarios(abonoVincular, tipoAbono, identificadorSolicitante)


                Dim divisaOriginal As Clases.Abono.DivisaAbono = abonoVincular.AbonoPorTipo(tipoAbono).Divisa
                Dim divisaClone = divisaOriginal.Clonar()
                abonoVincular.Divisa = divisaClone
                abonoVincular.Importe = abonoVincular.AbonoPorTipo(tipoAbono).Importe

                If tipoAbono = Enumeradores.TipoAbono.Saldos OrElse tipoAbono = Enumeradores.TipoAbono.Pedido Then
                    abonoVincular.AbonoSaldo.ListaSaldoCuenta.Clear()
                End If

                'Remove o cliente se abono por pedido
                'If tipoAbono = Enumeradores.TipoAbono.Pedido Then
                '    abonoVincular.Cliente = New Clases.Abono.AbonoInformacion()
                '    abonoVincular.SubCliente = New Clases.Abono.AbonoInformacion()
                '    abonoVincular.PtoServicio = New Clases.Abono.AbonoInformacion()
                '    abonoVincular.CuentasDisponibles.Clear()
                '    abonoVincular.MultiplesCuentas = False
                'End If

                'Else
                'listaDivisa.Add(abonoVincular.AbonoPorTipo(tipoAbono).Divisa.Descripcion)
                'For Each _abonoVincular In abonosVincular
                '    If _abonoVincular.AbonoPorTipo(tipoAbono) IsNot Nothing AndAlso _
                '        abonoVincular.AbonoPorTipo(tipoAbono) IsNot Nothing AndAlso _
                '        ((tipoAbono = Enumeradores.TipoAbono.Elemento AndAlso abonoVincular.AbonoElemento.IdentificadorRemesa = _abonoVincular.AbonoElemento.IdentificadorRemesa) OrElse
                '         ((tipoAbono = Enumeradores.TipoAbono.Saldos OrElse tipoAbono = Enumeradores.TipoAbono.Pedido) AndAlso abonoVincular.AbonoSaldo.IdentificadorSnapshot = _abonoVincular.AbonoSaldo.IdentificadorSnapshot)) Then

                '        abonosInvalidos.Add(_abonoVincular)
                '    End If
                'Next

                'End If
            Next

            If abonosInvalidos IsNot Nothing AndAlso abonosInvalidos.Count > 0 Then
                For Each abonoVincular In abonosVincular
                    If abonoVincular.AbonoPorTipo(tipoAbono) IsNot Nothing AndAlso
                       (abonosInvalidos.FindAll(Function(x) x.AbonoPorTipo(tipoAbono) IsNot Nothing AndAlso
                                                   ((tipoAbono = Enumeradores.TipoAbono.Elemento AndAlso x.AbonoElemento.IdentificadorRemesa = abonoVincular.AbonoElemento.IdentificadorRemesa) OrElse
                                                    ((tipoAbono = Enumeradores.TipoAbono.Saldos OrElse tipoAbono = Enumeradores.TipoAbono.Pedido) AndAlso x.AbonoSaldo.IdentificadorSnapshot = abonoVincular.AbonoSaldo.IdentificadorSnapshot))) IsNot Nothing) Then
                        abonoVincular.Divisa = Nothing
                    End If
                Next
            End If

            If abonosVincular.RemoveAll(Function(a) (a.Divisa Is Nothing OrElse String.IsNullOrEmpty(a.Divisa.CodigoISO))) > 0 Then
                If todos Then
                    respuesta.MensajeError = Traduzir("071_Abono_msg_FaltaConfiguraciónDatosBanc")
                Else
                    respuesta.MensajeError = Traduzir("071_Abono_msg_NoExistenDatosBancarios")
                    Dim infoCliente = abonosInvalidos.First.Cliente.Codigo & " " & abonosInvalidos.First.Cliente.Descripcion
                    If (Not String.IsNullOrEmpty(abonosInvalidos.First.SubCliente.Identificador)) Then
                        infoCliente &= " " & abonosInvalidos.First.SubCliente.Codigo & " " & abonosInvalidos.First.SubCliente.Descripcion
                        If (Not String.IsNullOrEmpty(abonosInvalidos.First.PtoServicio.Identificador)) Then
                            infoCliente &= " " & abonosInvalidos.First.PtoServicio.Codigo & " " & abonosInvalidos.First.PtoServicio.Descripcion
                        End If
                    End If
                    respuesta.MensajeError = String.Format(respuesta.MensajeError, infoCliente, _
                                                           String.Join(", ", listaDivisa), abonosInvalidos.First.AbonoElemento.CodigoElemento)
                End If
            End If

            respuesta.Respuesta = abonosVincular

        Catch ex As Exception
            respuesta.CodigoError = "100"
            respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            respuesta.MensajeErrorDescriptiva = ex.ToString
            respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(respuesta, New Converters.StringEnumConverter())
    End Function

    Private Shared Function ObtenerDatosBancarios(ByRef abonoVincular As Clases.Abono.AbonoValor, tipoAbono As Enumeradores.TipoAbono, _
                                                  identificadorSolicitante As String) As Boolean

        Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Abono.ObtenerDatosBancarios(abonoVincular, tipoAbono, identificadorSolicitante)

    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function ObtenerDatosBancarios(identificadorCliente As String, identificadorDivisa As String) As String
        Dim respuesta As New ContractoServicio.Comon.BaseRespuestaJSON
        Try
            Dim accionDatoBancario = New Prosegur.Global.GesEfectivo.IAC.LogicaNegocio.AccionDatoBancario()
            Dim peticionDatoBancario = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.DatoBancario.GetDatosBancarios.Peticion()
            peticionDatoBancario.IdentificadorCliente = identificadorCliente
            peticionDatoBancario.IdentificadorDivisa = identificadorDivisa
            peticionDatoBancario.ObtenerSubNiveis = True

            Dim respuestaDatosBancarios = accionDatoBancario.GetDatosBancarios(peticionDatoBancario)
            Dim listaCuentas = respuestaDatosBancarios.DatosBancarios

            If listaCuentas IsNot Nothing AndAlso listaCuentas.Count > 0 Then
                respuesta.Respuesta = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Abono.ObtenerListaCuentasDisponibles(listaCuentas)
            End If
        Catch ex As Exception
            respuesta.CodigoError = "100"
            respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            respuesta.MensajeErrorDescriptiva = ex.ToString
            respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(respuesta, New Converters.StringEnumConverter())
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GrabarAbono(abonoJson As Clases.Abono.Abono) As String
        Dim respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try

            'Grabar na LogicaNegocio
            LogicaNegocio.GenesisSaldos.Abono.GrabarAbono(abonoJson, False, Parametros.Permisos.Usuario.Login, InformacionUsuario.SectorSeleccionado, Parametros.Parametro.FormatoCodigoProcesoAbono)

            respuesta.Respuesta = abonoJson

        Catch ex As Exception
            respuesta.CodigoError = "100"
            respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            respuesta.MensajeErrorDescriptiva = ex.ToString
            respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(respuesta, New Converters.StringEnumConverter())
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GrabarYFinalizarAbono(abonoJson As Clases.Abono.Abono) As String
        Dim respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try
            If String.IsNullOrEmpty(Parametros.Parametro.FormatoCodigoProcesoAbono) Then
                Throw New Exception(String.Format(Traduzir("014_parametro_obrigatorio"), "FormatoCodigoProcesoAbono"))
            End If

            If abonoJson IsNot Nothing AndAlso abonoJson.AbonosValor IsNot Nothing Then

                For Each av In abonoJson.AbonosValor

                    If av.AbonoSaldo IsNot Nothing Then

                        If av.AbonoSaldo.SectoresDocumento IsNot Nothing AndAlso av.AbonoSaldo.SectoresDocumento.Count > 0 Then
                            For Each sd In av.AbonoSaldo.SectoresDocumento
                                sd.Codigo = InformacionUsuario.SectorSeleccionado.Codigo
                            Next
                        Else
                            av.AbonoSaldo.SectoresDocumento = New List(Of Clases.Abono.AbonoInformacion)
                            av.AbonoSaldo.SectoresDocumento.Add(New Clases.Abono.AbonoInformacion With { _
                                                                .Codigo = InformacionUsuario.SectorSeleccionado.Codigo})
                        End If


                    End If

                Next

            End If

            'Grabar y Finalizar na LogicaNegocio
            LogicaNegocio.GenesisSaldos.Abono.GrabarAbono(abonoJson, True, Parametros.Permisos.Usuario.Login, InformacionUsuario.SectorSeleccionado, Parametros.Parametro.FormatoCodigoProcesoAbono)

            respuesta.Respuesta = abonoJson

        Catch ex As Exception
            respuesta.CodigoError = "100"
            respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            respuesta.MensajeErrorDescriptiva = ex.ToString
            respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(respuesta, New Converters.StringEnumConverter())
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function AnularAbono(abonoJson As String) As String
        Dim respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try

            Dim abonoGrabar = JsonConvert.DeserializeObject(Of Clases.Abono.Abono)(abonoJson, New Converters.StringEnumConverter())

            'Grabar y Finalizar na LogicaNegocio
            abonoGrabar.CodigoEstado = Enumeradores.EstadoAbono.Anulado
            LogicaNegocio.GenesisSaldos.Abono.CambiarEstadoAbono(abonoGrabar)

            respuesta.Respuesta = abonoGrabar

        Catch ex As Exception
            respuesta.CodigoError = "100"
            respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            respuesta.MensajeErrorDescriptiva = ex.ToString
            respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(respuesta, New Converters.StringEnumConverter())
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function LimparFiltro(abonoJson As String, esLimpiar As Boolean) As String
        Dim respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try

            Dim abono = JsonConvert.DeserializeObject(Of Clases.Abono.Abono)(abonoJson, New Converters.StringEnumConverter())

            'GENIPLANT-2160, Não deve salvar nem carregar mais configurações de filtro pré preenchido
            'If (abono.TipoAbono = Enumeradores.TipoAbono.Elemento) Then
            '    PreferenciasAplicacion.AtualizaPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
            '        (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_ELEMENTO, Nothing)
            'ElseIf (abono.TipoAbono = Enumeradores.TipoAbono.Saldos) Then
            '    PreferenciasAplicacion.AtualizaPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
            '        (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_SALDOS, Nothing)
            'ElseIf (abono.TipoAbono = Enumeradores.TipoAbono.Pedido) Then
            '    PreferenciasAplicacion.AtualizaPreferenciaSerializadaBinario(Of Clases.Transferencias.FiltroConsultaValoresAbono) _
            '        (Constantes.Preferencias.Abono.FUNCIONALIDAD_ABONO, Constantes.Preferencias.Abono.PROPRIEDAD_FILTRO_PEDIDO, Nothing)
            'End If

            respuesta.Respuesta = New PantallaAbono().RetornaFiltrosAbono(abono, esLimpiar)
        Catch ex As Exception
            respuesta.CodigoError = "100"
            respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            respuesta.MensajeErrorDescriptiva = ex.ToString
            respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(respuesta, New Converters.StringEnumConverter())
    End Function

#Region "[METODOS PRIVADOS]"

    Private Shared Function ObtenerListaCuentasDisponibles(ByRef listaCuentas As List(Of Clases.DatoBancario)) As List(Of Clases.Abono.BancoInformacion)
        Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Abono.ObtenerListaCuentasDisponibles(listaCuentas)
    End Function

#End Region

End Class