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
Imports Prosegur.Genesis.Web.Login

Namespace Abono

    Public Class Busqueda
        Inherits Base

#Region "[PROPRIEDADES]"

        Public Property _Divisas As New List(Of Clases.Abono.AbonoInformacion)
        Public Property _Estados As New List(Of Clases.Abono.AbonoInformacion)
        Public Property _TipoAbono As New List(Of Clases.Abono.AbonoInformacion)
        Public Property _Filtro As New Clases.Transferencias.FiltroConsultaAbono

#End Region

#Region "[OVERRIDES]"
        Protected Overrides Sub DefinirParametrosBase()
            MyBase.DefinirParametrosBase()
            MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.ABONO
            MyBase.ValidarAcesso = True
            MyBase.ValidarPemissaoAD = True
        End Sub

        Protected Overrides Sub TraduzirControles()

            Master.Titulo = Traduzir("071_Busqueda_Titulo")

            Dim _Diccionario As New Dictionary(Of String, String)() From {
                {"Valores_Seleccionar", Traduzir("071_Comon_Valores_Seleccionar")},
                {"Valores_Estado_EN", Traduzir("071_Comon_Valores_Estado_EN")},
                {"Valores_Estado_PR", Traduzir("071_Comon_Valores_Estado_PR")},
                {"Valores_Estado_AN", Traduzir("071_Comon_Valores_Estado_AN")},
                {"Valores_TipoAbono_SA", Traduzir("071_Comon_Valores_TipoAbono_SA")},
                {"Valores_TipoAbono_EL", Traduzir("071_Comon_Valores_TipoAbono_EL")},
                {"Valores_TipoAbono_PE", Traduzir("071_Comon_Valores_TipoAbono_PE")},
                {"Grid_TituloColumna_FechaHora", Traduzir("071_Busqueda_Grid_TituloColumna_FechaHora")},
                {"Grid_TituloColumna_Banco", Traduzir("071_Busqueda_Grid_TituloColumna_Banco")},
                {"Grid_TituloColumna_Codigo", Traduzir("071_Busqueda_Grid_TituloColumna_Codigo")},
                {"Grid_TituloColumna_TipoAbono", Traduzir("071_Busqueda_Grid_TituloColumna_TipoAbono")},
                {"Grid_TituloColumna_Estado", Traduzir("071_Busqueda_Grid_TituloColumna_Estado")},
                {"Grid_TituloColumna_Aciones", Traduzir("071_Comon_Grid_TituloColumna_Aciones")},
                {"Grid_TituloColumna_Situacion", Traduzir("071_Comon_Grid_TituloColumna_Situacion")},
                {"Boton_Visualizar", Traduzir("071_Comon_Boton_Visualizar")},
                {"Boton_Editar", Traduzir("071_Comon_Boton_Editar")},
                {"Boton_Eliminar", Traduzir("071_Comon_Boton_Eliminar")},
                {"msg_Anular_Abono", Traduzir("071_Busqueda_msg_Anular_Abono")},
                {"msg_loading", Traduzir("071_Comon_msg_loading")},
                {"msg_obtenerValores", Traduzir("071_Comon_msg_obtenerValores")},
                {"msg_nenhumRegistroEncontrado", Traduzir("071_Comon_msg_nenhumRegistroEncontrado")},
                {"msg_informacionesInvalidas", Traduzir("071_Comon_msg_informacionesInvalidas")},
                {"msg_Archivos_Parciales", Traduzir("071_Busqueda_msg_Archivos_Parciales")},
                {"msg_Reportes_Parciales", Traduzir("071_Busqueda_msg_Reportes_Parciales")},
                {"msg_generandoReportes", Traduzir("071_Busqueda_msg_Generando_Reportes")},
                {"msg_anulandoAbono", Traduzir("071_Busqueda_msg_Anulando_Abono")},
                {"msg_producidoError", Traduzir("071_Comon_msg_producidoError")}
            }

            litDicionario.Text = "<script> var _Diccionario = JSON.parse('" & JsonConvert.SerializeObject(_Diccionario) & "'); </script>"
        End Sub

        Protected Overrides Sub AdicionarScripts()
            MyBase.AdicionarScripts()
        End Sub

        Protected Overrides Sub Inicializar()

            If _Divisas Is Nothing OrElse _Divisas.Count = 0 Then
                _Divisas = LogicaNegocio.Genesis.Divisas.ObtenerListaDivisas()
            End If

            If _Estados Is Nothing OrElse _Estados.Count = 0 Then
                For Each estado As Enumeradores.EstadoAbono In [Enum].GetValues(GetType(Enumeradores.EstadoAbono))
                    If estado <> Enumeradores.EstadoAbono.NoDefinido AndAlso estado <> Enumeradores.EstadoAbono.Nuevo Then
                        Dim codigo As String = Comon.Extenciones.EnumExtension.RecuperarValor(estado)
                        _Estados.Add(New Clases.Abono.AbonoInformacion With {.Codigo = codigo, .Descripcion = Traduzir("071_Comon_Valores_Estado_" & codigo), .Identificador = codigo})
                    End If
                Next
            End If

            If _TipoAbono Is Nothing OrElse _TipoAbono.Count = 0 Then
                For Each tipoAbono In [Enum].GetValues(GetType(Enumeradores.TipoAbono))
                    If tipoAbono <> Enumeradores.TipoAbono.NoDefinido Then
                        Dim codigo As String = Comon.Extenciones.EnumExtension.RecuperarValor(tipoAbono)
                        _TipoAbono.Add(New Clases.Abono.AbonoInformacion With {.Codigo = codigo, .Descripcion = Traduzir("071_Comon_Valores_TipoAbono_" & codigo), .Identificador = codigo})
                    End If
                Next
            End If

            If InformacionUsuario IsNot Nothing AndAlso InformacionUsuario IsNot Nothing AndAlso InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
                _Filtro.IdentificadorDelegacion = InformacionUsuario.DelegacionSeleccionada.Identificador
                _Filtro.codigoDelegacion = InformacionUsuario.DelegacionSeleccionada.Codigo
            End If

#If DEBUG Then
            _Filtro.IdentificadorDelegacion = "BF52A95E193DA5B3E040360A65F87103"
            _Filtro.codigoDelegacion = "01"
#End If
            _Filtro.IdentificadoresDivisas = New List(Of String)

            litCargaInicial.Text = "<script> var _Divisas = JSON.parse('" & JsonConvert.SerializeObject(_Divisas) & "');" &
                                            "var _Estados = JSON.parse('" & JsonConvert.SerializeObject(_Estados) & "');" &
                                            "var _TipoAbono = JSON.parse('" & JsonConvert.SerializeObject(_TipoAbono) & "');" &
                                            "var _Filtro = JSON.parse('" & JsonConvert.SerializeObject(_Filtro) & "');</script>"

            configurarControles()

        End Sub

#End Region

#Region "[METODOS]"

        Private Sub configurarControles()

            ' Banco
            Me.UcBanco.Tipo = Enumeradores.TipoBusqueda.Banco
            Me.UcBanco.Titulo = Traduzir("071_Busqueda_Campo_Banco")
            Me.UcBanco.EsMulti = False
            Me.UcBanco.VisibilidadInicial = True

            ' Cliente
            Me.UcCliente.Tipo = Enumeradores.TipoBusqueda.Cliente
            Me.UcCliente.Titulo = Traduzir("071_Busqueda_Campo_Cliente")
            Me.UcCliente.EsMulti = False
            Me.UcCliente.VisibilidadInicial = True

        End Sub

        <System.Web.Services.WebMethod()>
        Public Shared Function obtenerClientes(codigo As String, descripcion As String, esbanco As Boolean) As String

            Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

            Try
                If Not String.IsNullOrEmpty(codigo) Then
                    _respuesta.Respuesta = LogicaNegocio.Genesis.Cliente.ObtenerClientesJSON(codigo, descripcion, esbanco)
                End If

            Catch ex As Exception
                _respuesta.CodigoError = "100"
                _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
                _respuesta.MensajeErrorDescriptiva = ex.ToString
                _respuesta.Respuesta = Nothing
            End Try

            Return JsonConvert.SerializeObject(_respuesta)

        End Function

        <System.Web.Services.WebMethod()>
        Public Shared Function obtenerAbonos(filtro As String) As String

            Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

            Try

                If Not String.IsNullOrEmpty(filtro) Then

                    Dim _Filtro As Clases.Transferencias.FiltroConsultaAbono = JsonConvert.DeserializeObject(Of Clases.Transferencias.FiltroConsultaAbono)(filtro)
                    _Filtro.IdentificadorDelegacion = InformacionUsuario.DelegacionSeleccionada.Identificador

                    Dim abonos As List(Of Clases.Abono.Abono) = LogicaNegocio.GenesisSaldos.Abono.RecuperarAbonos(_Filtro, InformacionUsuario.DelegacionSeleccionada)

                    If abonos IsNot Nothing AndAlso abonos.Count > 0 Then
                        _respuesta.Respuesta = abonos.ToList
                    End If
                End If

            Catch ex As Exception
                _respuesta.CodigoError = "100"
                _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
                _respuesta.MensajeErrorDescriptiva = ex.ToString
                _respuesta.Respuesta = Nothing
            End Try

            Return JsonConvert.SerializeObject(_respuesta, New Comon.JSON.EnumConverter())

        End Function

        <System.Web.Services.WebMethod()>
        Public Shared Function generarReportesAbono(identificador As String) As String

            Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

            Try

                If Not String.IsNullOrEmpty(identificador) Then

                    Dim abono As New Clases.Abono.Abono
                    abono = LogicaNegocio.GenesisSaldos.Abono.ObtenerAbono(identificador, InformacionUsuario.DelegacionSeleccionada.Identificador, InformacionUsuario.DelegacionSeleccionada.Codigo)

                    If abono IsNot Nothing Then
                        LogicaNegocio.GenesisSaldos.Abono.GenerarInforme(abono, True, InformacionUsuario.DelegacionSeleccionada)
                        _respuesta.Respuesta = identificador
                    End If

                End If

            Catch ex As Exception
                _respuesta.CodigoError = "100"
                _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
                _respuesta.MensajeErrorDescriptiva = ex.ToString
                _respuesta.Respuesta = Nothing
            End Try

            Return JsonConvert.SerializeObject(_respuesta)

        End Function

        <System.Web.Services.WebMethod()>
        Public Shared Function anularAbono(identificador As String, filtro As String) As String

            Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

            Try

                If Not String.IsNullOrEmpty(identificador) Then

                    Dim abono As New Clases.Abono.Abono With {.Identificador = identificador, .CodigoEstado = Enumeradores.EstadoAbono.Anulado, .UsuarioModificacion = Parametros.Permisos.Usuario.Login}
                    LogicaNegocio.GenesisSaldos.Abono.CambiarEstadoAbono(abono)

                    If Not String.IsNullOrEmpty(filtro) Then

                        Dim _Filtro As Clases.Transferencias.FiltroConsultaAbono = JsonConvert.DeserializeObject(Of Clases.Transferencias.FiltroConsultaAbono)(filtro)
                        _Filtro.IdentificadorDelegacion = InformacionUsuario.DelegacionSeleccionada.Identificador

                        Dim abonos As List(Of Clases.Abono.Abono) = LogicaNegocio.GenesisSaldos.Abono.RecuperarAbonos(_Filtro, InformacionUsuario.DelegacionSeleccionada)

                        If abonos IsNot Nothing AndAlso abonos.Count > 0 Then
                            _respuesta.Respuesta = abonos.ToList
                        End If
                    End If

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

    End Class

End Namespace