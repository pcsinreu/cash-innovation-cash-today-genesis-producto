Imports System.Web
Imports System.Web.UI
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.IO
Imports System.Web.UI.Page
Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports Ionic.Zip

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<System.Web.Script.Services.ScriptService> _
<ToolboxItem(False)> _
Public Class ServiciosInterface
    Inherits System.Web.Services.WebService


    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function validarSaldoClasificacion(identificadorDocumento As String, identificadorCuentaSaldos As String) As String

        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try

            If Not String.IsNullOrEmpty(identificadorDocumento) AndAlso Not String.IsNullOrEmpty(identificadorCuentaSaldos) Then

                Dim _validacion As String = LogicaNegocio.GenesisSaldos.Saldo.validarSaldoEfectivoAnteriorXAtual(identificadorDocumento, identificadorCuentaSaldos)

                If Not String.IsNullOrEmpty(_validacion) AndAlso _validacion.Length > 1 Then

                    Dim msg As String = ""
                    Dim aux = _validacion.Split("|")

                    If Not String.IsNullOrEmpty(aux(0)) Then
                        msg &= Traduzir("072_diferenciaefectivo") & aux(0)
                    End If

                    If Not String.IsNullOrEmpty(aux(1)) Then

                        If Not String.IsNullOrEmpty(msg) Then
                            msg &= "</br></br>"
                        End If
                        msg &= Traduzir("072_diferenciamediopago") & aux(1)
                    End If

                    Throw New Exception(msg)
                End If

            Else
                Throw New Exception("No fue informando un identificador de documento.")
            End If

        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.ToString
            _respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(_respuesta)

    End Function

    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function obtenerValoresAvanzadoTipoSector(codigo As String, descripcion As String, identificadorPadre As String, considerarTodosNiveis As Boolean) As String
        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try
            _respuesta.Respuesta = LogicaNegocio.Genesis.Sector.ObtenerSectorJSON(codigo, descripcion, identificadorPadre, considerarTodosNiveis)
        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.ToString
            _respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(_respuesta)
    End Function

    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function obtenerCanalPatron(codigo As String)
        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try
            _respuesta.Respuesta = LogicaNegocio.Genesis.Canal.ObtenerCanalPatronJSON(codigo)
        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.ToString
            _respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(_respuesta)
    End Function

    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function obtenerValoresAvanzado(codigo As String, descripcion As String, identificadorPadre As String, tipo As String) As String

        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try

            Select Case tipo

                Case "Sector"
                    _respuesta.Respuesta = LogicaNegocio.Genesis.Sector.ObtenerSectorJSON(codigo, descripcion, identificadorPadre, False)
                Case "Delegacion"
                    _respuesta.Respuesta = LogicaNegocio.Genesis.Delegacion.ObtenerDelegacionJSON(codigo, descripcion)
                Case "Planta"
                    _respuesta.Respuesta = LogicaNegocio.Genesis.Planta.ObtenerPlantaJSON(codigo, descripcion, identificadorPadre)
                Case "Cliente"
                    _respuesta.Respuesta = LogicaNegocio.Genesis.Cliente.ObtenerClientesJSON(codigo, descripcion, False)
                Case "Banco"
                    _respuesta.Respuesta = LogicaNegocio.Genesis.Cliente.ObtenerClientesJSON(codigo, descripcion, True)
                Case "SubCliente"
                    _respuesta.Respuesta = LogicaNegocio.Genesis.SubCliente.ObtenerSubClienteJSON(codigo, descripcion, identificadorPadre)
                Case "PtoServicio"
                    _respuesta.Respuesta = LogicaNegocio.Genesis.PuntoServicio.ObtenerPuntoServicioJSON(codigo, descripcion, identificadorPadre)
                Case "Canal"
                    _respuesta.Respuesta = LogicaNegocio.Genesis.Canal.ObtenerCanalJSON(codigo, descripcion)
                Case "SubCanal"
                    _respuesta.Respuesta = LogicaNegocio.Genesis.SubCanal.ObtenerSubCanalJSON(codigo, descripcion, identificadorPadre)
            End Select

        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.ToString
            _respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(_respuesta)

    End Function

    <WebMethod()> _
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function obtenerTiposCuenta() As String
        Dim listaCuenta = New List(Of Object)

        For Each item In [Enum].GetValues(GetType(Comon.Enumeradores.TipoCuentaBancaria))
            listaCuenta.Add(New With {.Id = item, .Descripcion = Comon.Extenciones.EnumExtension.RecuperarValor(item)})
        Next

        Return JsonConvert.SerializeObject(listaCuenta)
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerNuevoDatosBancarios() As String
        Dim peticionDatosBancarios As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.DatoBancario.SetDatosBancarios.Peticion
        peticionDatosBancarios.DatosBancarios = New List(Of Comon.Clases.DatoBancario)
        peticionDatosBancarios.DatosBancarios.Add(New Comon.Clases.DatoBancario)
        peticionDatosBancarios.DatosBancarios(0).Banco = New Comon.Clases.Cliente()
        peticionDatosBancarios.DatosBancarios(0).Cliente = New Comon.Clases.Cliente()
        peticionDatosBancarios.DatosBancarios(0).Divisa = New Comon.Clases.Divisa()
        peticionDatosBancarios.DatosBancarios(0).PuntoServicio = New Comon.Clases.PuntoServicio()
        peticionDatosBancarios.DatosBancarios(0).SubCliente = New Comon.Clases.SubCliente()


        Return JsonConvert.SerializeObject(peticionDatosBancarios)
    End Function

    <WebMethod()> _
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function GrabarNuevaConta(peticionDatosBancarios As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.DatoBancario.SetDatosBancarios.Peticion) As String

        Dim logicaNegocio As New Prosegur.Global.GesEfectivo.IAC.LogicaNegocio.AccionDatoBancario

        If peticionDatosBancarios.DatosBancarios(0).Identificador Is Nothing Then
            peticionDatosBancarios.DatosBancarios(0).Identificador = Guid.NewGuid().ToString()
        End If

        Dim respuesta = logicaNegocio.SetDatosBancariosSenBaja(peticionDatosBancarios)

        Return JsonConvert.SerializeObject(respuesta)

    End Function

    <WebMethod()> _
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function AlterarCuentaEstandar(identificador As String, bolDefecto As Boolean)

        Dim logicaNegocio As New Prosegur.Global.GesEfectivo.IAC.LogicaNegocio.AccionDatoBancario
        Dim peticion As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.DatoBancario.AlterarCuentaEstandar.Peticion

        peticion.Identificador = identificador
        peticion.BolDefecto = bolDefecto

        Return logicaNegocio.AlterarCuentaEstandar(peticion)

    End Function

End Class