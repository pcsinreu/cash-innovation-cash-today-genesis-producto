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

Namespace Abono

    Public Class PantallaVisualizar
        Inherits Base

#Region "[PROPRIEDADES]"

        Private _Abono As Clases.Abono.Abono
        Public ReadOnly Property Abono() As Clases.Abono.Abono
            Get
                If _Abono Is Nothing AndAlso Not String.IsNullOrEmpty(Request.QueryString("IdentificadorAbono")) Then
                    _Abono = LogicaNegocio.GenesisSaldos.Abono.ObtenerAbono(Request.QueryString("IdentificadorAbono"), InformacionUsuario.DelegacionSeleccionada.Identificador, InformacionUsuario.DelegacionSeleccionada.Codigo)

                    If _Abono.CodigoEstado <> Enumeradores.EstadoAbono.Anulado AndAlso _Abono.CodigoEstado <> Enumeradores.EstadoAbono.Procesado Then
                        Response.Redireccionar("~/Pantallas/Abono/Busqueda.aspx")
                    End If
                End If
                Return _Abono
            End Get
        End Property

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
            Master.Titulo = Traduzir("071_Visualizar_Titulo")

            Dim _Diccionario As New Dictionary(Of String, String)() From {
                {"msg_loading", Traduzir("071_Comon_msg_loading")},
                {"msg_obtenerValores", Traduzir("071_Comon_msg_obtenerValores")},
                {"msg_nenhumRegistroEncontrado", Traduzir("071_Comon_msg_nenhumRegistroEncontrado")},
                {"msg_informacionesInvalidas", Traduzir("071_Comon_msg_informacionesInvalidas")},
                {"msg_Archivos_Parciales", Traduzir("071_Busqueda_msg_Archivos_Parciales")},
                {"msg_Reportes_Parciales", Traduzir("071_Busqueda_msg_Reportes_Parciales")},
                {"msg_actualizaAutomatico", Traduzir("071_Visualizar_msg_ActualizaAutomatico")},
                {"msg_segundos", Traduzir("071_Visualizar_msg_Segundos")},
                {"msg_producidoError", Traduzir("071_Comon_msg_producidoError")}
            }

            litDicionario.Text = "<script> var _Diccionario = JSON.parse('" & JsonConvert.SerializeObject(_Diccionario) & "'); </script>"
        End Sub

        Protected Overrides Sub AdicionarScripts()
            MyBase.AdicionarScripts()
        End Sub

        Protected Overrides Sub Inicializar()

            If Abono IsNot Nothing Then
                cargarValores()
            End If

        End Sub

#End Region

#Region "[METODOS]"

        Private Sub cargarValores()

            lblBanco.Text = Abono.Bancos(0).Codigo & " - " & Abono.Bancos(0).Descripcion
            lblTipoAbono.Text = Traduzir("071_Comon_Valores_TipoAbono_" & Abono.TipoAbono.RecuperarValor)
            lblEstado.Text = Traduzir("071_Comon_Valores_Estado_" & Abono.CodigoEstado.RecuperarValor)

            Dim nombresArchivos As String = ""
            Dim nombresReportes As String = ""
            Dim ArchivosParciales As Boolean
            Dim ReportesParciales As Boolean
            Dim ReportesPendentes As Boolean

            If Abono.DatosReporte IsNot Nothing AndAlso Abono.DatosReporte.Count > 0 Then

                For Each reporte In Abono.DatosReporte
                    If Not String.IsNullOrEmpty(reporte.NombreArchivo) Then
                        If reporte.Tipo = Enumeradores.TipoReporte.AbonoExportacion Then
                            nombresArchivos &= reporte.NombreArchivo & ";"
                            If reporte.CodigoSituacion <> "PR" Then
                                ArchivosParciales = True
                            End If
                        Else
                            nombresReportes &= reporte.NombreArchivo & ";"
                            If reporte.CodigoSituacion <> "PR" Then
                                ReportesParciales = True
                            End If
                        End If
                    End If

                    If reporte.CodigoSituacion = "PE" Then
                        ReportesPendentes = If(Modo = Enumeradores.Modo.AltaImpresion, True, False)
                    End If

                    If reporte.CodigoSituacion = "ER" AndAlso String.IsNullOrEmpty(litReporteError.Text) Then
                        litReporteError.Text &= Traduzir("071_Visualizar_msg_Error") & " <br>" & reporte.DesErrorEjecucion & "<br>"
                    End If
                Next

                If ReportesPendentes Then
                    litReporteError.Text = ""
                    nombresArchivos = ""
                    nombresReportes = ""
                End If

                Dim codigoDelegacion As String = ""

                If InformacionUsuario IsNot Nothing AndAlso InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
                    codigoDelegacion = InformacionUsuario.DelegacionSeleccionada.Codigo
                End If

                litScript.Text = "<script>nombresArchivos = '" & nombresArchivos & "'; nombresReportes = '" & nombresReportes & "'; codigoDelegacion = '" & codigoDelegacion & "'; ArchivosParciales = " & ArchivosParciales.ToString.ToLower & ";  ReportesParciales = " & ReportesParciales.ToString.ToLower & "; </script>"

            End If

            litBotones.Text = ""

            If Abono.TipoAbono = Enumeradores.TipoAbono.Elemento Then
                lblValores.Text = Traduzir("071_Comon_Valores_TipoValor_" & Abono.TipoValor.RecuperarValor)
                dvValores.Style.Item("display") = "block"
                lblTituloAbonos.Text = Traduzir("071_Visualizar_Titulo_Elementos_Abonados")
                litBotones.Text &= "<input type='button' value='" & Traduzir("071_Comon_Boton_Voltar") & "' class='boton' style='width:150px; color:#232323;' onclick='javascript: window.document.location = " & Chr(34) & "Busqueda.aspx" & Chr(34) & ";' />"
                litBotones.Text &= "<input type='button' value='" & Traduzir("071_Visualizar_Boton_VisualizarArchivo") & "'  class='boton' style='width:150px; color:#232323;' " & If(String.IsNullOrEmpty(nombresArchivos), "disabled='disabled'", "") & " onclick='javascript: accionVisualizarArchivo();' />"
                litBotones.Text &= "<input type='button' value='" & Traduzir("071_Visualizar_Boton_VisualizarReporte") & "'  class='boton' style='width:150px; color:#232323;' " & If(String.IsNullOrEmpty(nombresReportes), "disabled='disabled'", "") & " onclick='javascript: accionVisualizarReporte();' />"
            Else
                dvValores.Style.Item("display") = "none"
                lblTituloAbonos.Text = Traduzir("071_Visualizar_Titulo_Saldos_Abonados")

                'Definição de botões relacioanados a geração de documento de pases
                Dim enableButtons As String = String.Empty
                If Not Abono.CrearDocumentoPases Then
                    enableButtons = "disabled"
                End If


                If Abono.CodigoEstado = Enumeradores.EstadoAbono.EnCurso Then
                    litBotones.Text &= "<input type='button' value='" & Traduzir("071_Visualizar_Boton_GenerarPase") & "' class='boton' style='width:250px; color:#232323;' />"
                Else
                    litBotones.Text &= "<input type='button' value='" & Traduzir("071_Comon_Boton_Voltar") & "' class='boton' style='width:150px; color:#232323;' onclick='javascript: window.document.location = " & Chr(34) & "Busqueda.aspx" & Chr(34) & ";' />"
                    litBotones.Text &= "<input type='button' value='" & Traduzir("071_Visualizar_Boton_VisualizarArchivo") & "'  class='boton' style='width:150px; color:#232323;' onclick='javascript: accionVisualizarArchivo();'" & enableButtons & "/>"
                    litBotones.Text &= "<input type='button' value='" & Traduzir("071_Visualizar_Boton_VisualizarReporte") & "'  class='boton' style='width:150px; color:#232323;' onclick='javascript: accionVisualizarReporte();' />"
                    litBotones.Text &= "<input type='button' value='" & Traduzir("071_Visualizar_Boton_VisualizarPase") & "' class='boton' style='width:200px; color:#232323;' onclick='javascript: window.document.location = " & Chr(34) & "../GrupoDocumento.aspx?IdentificadorGrupoDocumentos=" & Abono.IdenficadorGrupoDocumento & "&Modo=Consulta&SectorHijo=False" & Chr(34) & ";'" & enableButtons & "/>"
                End If
            End If

            cargarGrid()

            'If ReportesPendentes Then
            litScript.Text &= "<script> startCountdown(); </script>"
            'End If

        End Sub

        Private Sub cargarGrid()

            If Abono.AbonosValor IsNot Nothing AndAlso Abono.AbonosValor.Count > 0 Then

                dvAbonos.Style.Item("display") = "block"

                litAbonosHead.Text = "<tr>"
                If Abono.TipoAbono = Enumeradores.TipoAbono.Elemento Then
                    litAbonosHead.Text &= "<th style='width: 10%;'>" & Traduzir("071_Visualizar_Grid_TituloColumna_NumExterno") & "</th>"
                End If
                litAbonosHead.Text &= "<th style=''>" & Traduzir("071_Visualizar_Grid_TituloColumna_Cliente") & "</th>"
                litAbonosHead.Text &= "<th style='width: 10%;'>" & Traduzir("071_Visualizar_Grid_TituloColumna_Cuenta") & "</th>"
                litAbonosHead.Text &= "<th style='width: 10%;'>" & Traduzir("071_Visualizar_Grid_TituloColumna_Documento") & "</th>"
                If Abono.TipoAbono = Enumeradores.TipoAbono.Elemento Then
                    litAbonosHead.Text &= "<th style='width: 10%;'>" & Traduzir("071_Visualizar_Grid_TituloColumna_Codigo") & "</th>"
                End If
                litAbonosHead.Text &= "<th style=''>" & Traduzir("071_Visualizar_Grid_TituloColumna_Titularid") & "</th>"
                litAbonosHead.Text &= "<th style=''>" & Traduzir("071_Visualizar_Grid_TituloColumna_Obs") & "</th>"
                litAbonosHead.Text &= "<th style='width: 10%;'>" & Traduzir("071_Visualizar_Grid_TituloColumna_Valor") & "</th>"
                litAbonosHead.Text &= "</tr>"

                litAbonosBody.Text = ""
                For Each a In Abono.AbonosValor
                    litAbonosBody.Text &= "<tr>"
                    If Abono.TipoAbono = Enumeradores.TipoAbono.Elemento Then
                        litAbonosBody.Text &= "<td style='width: 10%; text-align: right; padding: 3px 4px 3px 4px;'>" & a.AbonoElemento.CodigoElemento & "</td>"
                    End If
                    litAbonosBody.Text &= "<td style=' padding: 3px 4px 3px 4px;'>" & a.Cliente.Codigo & " - " & a.Cliente.Descripcion & "</td>"
                    litAbonosBody.Text &= "<td style='width: 10%; text-align: right; padding: 3px 4px 3px 4px;'>" & a.Cuenta.CodigoCuentaBancaria & "</td>"
                    litAbonosBody.Text &= "<td style='width: 10%; padding: 3px 4px 3px 4px;'>" & a.Cuenta.CodigoDocumento & "</td>"
                    If Abono.TipoAbono = Enumeradores.TipoAbono.Elemento Then
                        litAbonosBody.Text &= "<td style='width: 10%; text-align: right; padding: 3px 4px 3px 4px;'>" & a.AbonoElemento.Codigo & "</td>"
                    End If
                    litAbonosBody.Text &= "<td style=' padding: 3px 4px 3px 4px;'>" & a.Cuenta.DescripcionTitularidad & "</td>"
                    litAbonosBody.Text &= "<td style=' padding: 3px 4px 3px 4px;'>" & a.Observaciones & "</td>"
                    litAbonosBody.Text &= "<td style='width: 10%; text-align:right; padding: 3px 4px 3px 4px; color: " & a.Divisa.ColorHTML & "'>(" & a.Divisa.CodigoISO & ") " & String.Format("{0:N2}", a.Importe) & "</td>"
                    litAbonosBody.Text &= "</tr>"
                Next

            End If

        End Sub

#End Region

    End Class

End Namespace