Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Documento.RecuperarDocumentosSinSalidaRecorrido
Imports Prosegur.Genesis.ContractoServicio.Interfaces

Namespace ProxyWS.NuevoSaldos

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),
     System.Diagnostics.DebuggerStepThroughAttribute(),
     System.ComponentModel.DesignerCategoryAttribute("code"),
     System.Web.Services.WebServiceBindingAttribute(Name:="IngresoContadoSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/")>
    Partial Public Class ProxyNuevoSaldos
        Inherits ProxyWS.ServicioBase
        Implements ContractoServicio.Interfaces.INuevoSaldos

        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "GenesisSaldos/Servicio.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/CambiaEstadoDocumentoFondosSaldos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function CambiaEstadoDocumentoFondosSaldos(Peticion As Prosegur.Global.Saldos.ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Peticion) As Prosegur.Global.Saldos.ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Respuesta Implements ContractoServicio.Interfaces.INuevoSaldos.CambiaEstadoDocumentoFondosSaldos
            Dim results() As Object = Me.Invoke("CambiaEstadoDocumentoFondosSaldos", New Object() {Peticion})
            Return CType(results(0), Prosegur.Global.Saldos.ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/IngresoRemesas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function IngresoRemesas(Peticion As Prosegur.Global.Saldos.ContractoServicio.IngresoRemesas.Peticion) As Prosegur.Global.Saldos.ContractoServicio.IngresoRemesas.Respuesta Implements ContractoServicio.Interfaces.INuevoSaldos.IngresoRemesas
            Dim results() As Object = Me.Invoke("IngresoRemesas", New Object() {Peticion})
            Return CType(results(0), Prosegur.Global.Saldos.ContractoServicio.IngresoRemesas.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/IngresoRemesasNuevo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function IngresoRemesasNuevo(Peticion As Prosegur.Global.Saldos.ContractoServicio.IngresoRemesasNuevo.Peticion) As Prosegur.Global.Saldos.ContractoServicio.IngresoRemesasNuevo.Respuesta Implements ContractoServicio.Interfaces.INuevoSaldos.IngresoRemesasNuevo
            Dim results() As Object = Me.Invoke("IngresoRemesasNuevo", New Object() {Peticion})
            Return CType(results(0), Prosegur.Global.Saldos.ContractoServicio.IngresoRemesasNuevo.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.Interfaces.INuevoSaldos.Test
            Return Nothing
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/RecuperarSaldoExpuestoxDetallado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function RecuperarSaldoExpuestoxDetallado(Peticion As Contractos.GenesisSaldos.Saldo.RecuperarSaldoExpuestoxDetallado.Peticion) As Contractos.GenesisSaldos.Saldo.RecuperarSaldoExpuestoxDetallado.Respuesta Implements Interfaces.INuevoSaldos.RecuperarSaldoExpuestoxDetallado
            Dim results() As Object = Me.Invoke("RecuperarSaldoExpuestoxDetallado", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.GenesisSaldos.Saldo.RecuperarSaldoExpuestoxDetallado.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ConsultaDocumentos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ConsultaDocumentos(Peticion As Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Peticion) As Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Respuesta Implements Interfaces.INuevoSaldos.ConsultaDocumentos
            Dim results() As Object = Me.Invoke("ConsultaDocumentos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/RecuperarDocumentoPorIdentificador", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function RecuperarDocumentoPorIdentificador(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Respuesta Implements Interfaces.INuevoSaldos.RecuperarDocumentoPorIdentificador
            Dim results() As Object = Me.Invoke("RecuperarDocumentoPorIdentificador", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/GuardarDocumento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function GuardarDocumento(Peticion As Contractos.GenesisSaldos.Documento.GuardarDocumento.Peticion) As Contractos.GenesisSaldos.Documento.GuardarDocumento.Respuesta Implements Interfaces.INuevoSaldos.GuardarDocumento
            Dim results() As Object = Me.Invoke("GuardarDocumento", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.GenesisSaldos.Documento.GuardarDocumento.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/GuardarGrupoDocumento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function GuardarGrupoDocumento(Peticion As Contractos.GenesisSaldos.GrupoDocumento.GuardarGrupoDocumento.Peticion) As Contractos.GenesisSaldos.GrupoDocumento.GuardarGrupoDocumento.Respuesta Implements Interfaces.INuevoSaldos.GuardarGrupoDocumento
            Dim results() As Object = Me.Invoke("GuardarGrupoDocumento", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.GenesisSaldos.GrupoDocumento.GuardarGrupoDocumento.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/SalirRecorrido", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function SalirRecorrido(peticion As Contractos.Comon.Elemento.SalirRecorrido.SalirRecorridoPeticion) As Contractos.Comon.Elemento.SalirRecorrido.SalirRecorridoRespuesta Implements Interfaces.INuevoSaldos.SalirRecorrido
            Dim results() As Object = Me.Invoke("SalirRecorrido", New Object() {peticion})
            Return CType(results(0), Contractos.Comon.Elemento.SalirRecorrido.SalirRecorridoRespuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/Reenvio", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function Reenvio(peticion As Contractos.Comon.Elemento.Reenvio.ReenvioPeticion) As Contractos.Comon.Elemento.Reenvio.ReenvioRespuesta Implements Interfaces.INuevoSaldos.Reenvio
            Dim results() As Object = Me.Invoke("Reenvio", New Object() {peticion})
            Return CType(results(0), Contractos.Comon.Elemento.Reenvio.ReenvioRespuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/CrearDocumento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function CrearDocumento(Peticion As Contractos.GenesisSaldos.Documento.CrearDocumento.Peticion) As Contractos.GenesisSaldos.Documento.CrearDocumento.Respuesta Implements Interfaces.INuevoSaldos.CrearDocumento
            Dim results() As Object = Me.Invoke("CrearDocumento", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.GenesisSaldos.Documento.CrearDocumento.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ObtenerCuentaPorCodigos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ObtenerCuentaPorCodigos(Peticion As Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos.Peticion) As Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos.Respuesta Implements Interfaces.INuevoSaldos.ObtenerCuentaPorCodigos
            Dim results() As Object = Me.Invoke("ObtenerCuentaPorCodigos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ObtenerCuentas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ObtenerCuentas(Peticion As Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Peticion) As Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Respuesta Implements Interfaces.INuevoSaldos.ObtenerCuentas
            Dim results() As Object = Me.Invoke("ObtenerCuentas", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ActualizarSaldoPuesto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ActualizarSaldoPuesto(Peticion As Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Peticion) As Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Respuesta Implements Interfaces.INuevoSaldos.ActualizarSaldoPuesto
            Dim results() As Object = Me.Invoke("ActualizarSaldoPuesto", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/AperturarElemento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function AperturarElemento(Peticion As Contractos.GenesisSaldos.Documento.AperturarElemento.Peticion) As Contractos.GenesisSaldos.Documento.AperturarElemento.Respuesta Implements Interfaces.INuevoSaldos.AperturarElemento
            Dim results() As Object = Me.Invoke("AperturarElemento", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.GenesisSaldos.Documento.AperturarElemento.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/obtenerDocumentos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function obtenerDocumentos(Peticion As Contractos.GenesisSaldos.Documento.obtenerDocumentos.Peticion) As Contractos.GenesisSaldos.Documento.obtenerDocumentos.Respuesta Implements Interfaces.INuevoSaldos.obtenerDocumentos
            Dim results() As Object = Me.Invoke("obtenerDocumentos", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.Documento.obtenerDocumentos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/RecuperarDocumentoParaAlocacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function RecuperarDocumentoParaAlocacion(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentoParaAlocacion.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentoParaAlocacion.Respuesta Implements Interfaces.INuevoSaldos.RecuperarDocumentoParaAlocacion
            Dim results() As Object = Me.Invoke("RecuperarDocumentoParaAlocacion", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.Documento.RecuperarDocumentoParaAlocacion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/RecuperarDocumentosElementosConcluidos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function RecuperarDocumentosElementosConcluidos(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentosElementosConcluidos.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentosElementosConcluidos.Respuesta Implements Interfaces.INuevoSaldos.RecuperarDocumentosElementosConcluidos
            Dim results() As Object = Me.Invoke("RecuperarDocumentosElementosConcluidos", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.Documento.RecuperarDocumentosElementosConcluidos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/GenerarInforme", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function GenerarInforme(Peticion As Contractos.GenesisSaldos.Reporte.GenerarInforme.Peticion) As Contractos.GenesisSaldos.Reporte.GenerarInforme.Respuesta Implements Interfaces.INuevoSaldos.GenerarInforme
            Dim results() As Object = Me.Invoke("GenerarInforme", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.Reporte.GenerarInforme.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ActualizaBolImpreso", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ActualizaBolImpreso(Peticion As Contractos.GenesisSaldos.Documento.ActualizaBolImpreso.Peticion) As Contractos.GenesisSaldos.Documento.ActualizaBolImpreso.Respuesta Implements Interfaces.INuevoSaldos.ActualizaBolImpreso
            Dim results() As Object = Me.Invoke("ActualizaBolImpreso", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.Documento.ActualizaBolImpreso.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/RecuperarCaracteristicasPorCodigoComprobante", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function RecuperarCaracteristicasPorCodigoComprobante(Peticion As Contractos.GenesisSaldos.GrupoDocumento.RecuperarCaracteristicasPorCodigoComprobante.Peticion) As Contractos.GenesisSaldos.GrupoDocumento.RecuperarCaracteristicasPorCodigoComprobante.Respuesta Implements Interfaces.INuevoSaldos.RecuperarCaracteristicasPorCodigoComprobante
            Dim results() As Object = Me.Invoke("RecuperarCaracteristicasPorCodigoComprobante", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.GrupoDocumento.RecuperarCaracteristicasPorCodigoComprobante.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ConsultarContenedoresCliente", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ConsultarContenedoresCliente(Peticion As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresCliente.Peticion) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresCliente.Respuesta Implements Interfaces.INuevoSaldos.ConsultarContenedoresCliente
            Dim results() As Object = Me.Invoke("ConsultarContenedoresCliente", New Object() {Peticion})
            Return CType(results(0), Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresCliente.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ConsultarContenedoresPackModular", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ConsultarContenedoresPackModular(Peticion As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresPackModular.Peticion) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresPackModular.Respuesta Implements Interfaces.INuevoSaldos.ConsultarContenedoresPackModular
            Dim results() As Object = Me.Invoke("ConsultarContenedoresPackModular", New Object() {Peticion})
            Return CType(results(0), Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresPackModular.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ConsultarDocumentosGestionContenedores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ConsultarDocumentosGestionContenedores(Peticion As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarDocumentosGestionContenedores.Peticion) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarDocumentosGestionContenedores.Respuesta Implements Interfaces.INuevoSaldos.ConsultarDocumentosGestionContenedores
            Dim results() As Object = Me.Invoke("ConsultarDocumentosGestionContenedores", New Object() {Peticion})
            Return CType(results(0), Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarDocumentosGestionContenedores.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ArmarContenedores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ArmarContenedores(Peticion As GenesisSaldos.Contenedores.ArmarContenedores.Peticion) As GenesisSaldos.Contenedores.ArmarContenedores.Respuesta Implements Interfaces.INuevoSaldos.ArmarContenedores
            Dim results() As Object = Me.Invoke("ArmarContenedores", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.ArmarContenedores.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/DesarmarContenedores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function DesarmarContenedores(Peticion As GenesisSaldos.Contenedores.DesarmarContenedores.Peticion) As GenesisSaldos.Contenedores.DesarmarContenedores.Respuesta Implements Interfaces.INuevoSaldos.DesarmarContenedores
            Dim results() As Object = Me.Invoke("DesarmarContenedores", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.DesarmarContenedores.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ConsultarContenedoresSector", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ConsultarContenedoresSector(Peticion As GenesisSaldos.Contenedores.ConsultarContenedoresSector.Peticion) As GenesisSaldos.Contenedores.ConsultarContenedoresSector.Respuesta Implements Interfaces.INuevoSaldos.ConsultarContenedoresSector
            Dim results() As Object = Me.Invoke("ConsultarContenedoresSector", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.ConsultarContenedoresSector.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/DefinirCambiarPosicionContenedor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function DefinirCambiarExtraerPosicionContenedor(Peticion As GenesisSaldos.Contenedores.DefinirCambiarExtraerPosicionContenedor.Peticion) As GenesisSaldos.Contenedores.DefinirCambiarExtraerPosicionContenedor.Respuesta Implements Interfaces.INuevoSaldos.DefinirCambiarExtraerPosicionContenedor
            Dim results() As Object = Me.Invoke("DefinirCambiarExtraerPosicionContenedor", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.DefinirCambiarExtraerPosicionContenedor.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/GrabarAlertaVencimiento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function GrabarAlertaVencimiento(Peticion As GenesisSaldos.Contenedores.GrabarAlertaVencimiento.Peticion) As GenesisSaldos.Contenedores.GrabarAlertaVencimiento.Respuesta Implements Interfaces.INuevoSaldos.GrabarAlertaVencimiento
            Dim results() As Object = Me.Invoke("GrabarAlertaVencimiento", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.GrabarAlertaVencimiento.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ConsultarContenedorxPosicion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ConsultarContenedorxPosicion(Peticion As GenesisSaldos.Contenedores.ConsultarContenedorxPosicion.Peticion) As GenesisSaldos.Contenedores.ConsultarContenedorxPosicion.Respuesta Implements Interfaces.INuevoSaldos.ConsultarContenedorxPosicion
            Dim results() As Object = Me.Invoke("ConsultarContenedorxPosicion", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.ConsultarContenedorxPosicion.Respuesta)
        End Function


        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/GrabarInventarioContenedor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function GrabarInventarioContenedor(Peticion As GenesisSaldos.Contenedores.GrabarInventarioContenedor.Peticion) As GenesisSaldos.Contenedores.GrabarInventarioContenedor.Respuesta Implements Interfaces.INuevoSaldos.GrabarInventarioContenedor
            Dim results() As Object = Me.Invoke("GrabarInventarioContenedor", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.GrabarInventarioContenedor.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ConsultarInventarioContenedor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ConsultarInventarioContenedor(Peticion As GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Peticion) As GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Respuesta Implements Interfaces.INuevoSaldos.ConsultarInventarioContenedor
            Dim results() As Object = Me.Invoke("ConsultarInventarioContenedor", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ConsultarSeguimientoElemento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ConsultarSeguimientoElemento(Peticion As GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Peticion) As GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Respuesta Implements Interfaces.INuevoSaldos.ConsultarSeguimientoElemento
            Dim results() As Object = Me.Invoke("ConsultarSeguimientoElemento", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ConsultarContenedoresPorFIFO", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ConsultarContenedoresPorFIFO(Peticion As GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Peticion) As GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Respuesta Implements Interfaces.INuevoSaldos.ConsultarContenedoresPorFIFO
            Dim results() As Object = Me.Invoke("ConsultarContenedoresPorFIFO", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ReenvioEntreSectores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ReenvioEntreSectores(Peticion As GenesisSaldos.Contenedores.ReenvioEntreSectores.Peticion) As GenesisSaldos.Contenedores.ReenvioEntreSectores.Respuesta Implements Interfaces.INuevoSaldos.ReenvioEntreSectores
            Dim results() As Object = Me.Invoke("ReenvioEntreSectores", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.ReenvioEntreSectores.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/ReenvioEntreClientes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ReenvioEntreClientes(Peticion As GenesisSaldos.Contenedores.ReenvioEntreClientes.Peticion) As GenesisSaldos.Contenedores.ReenvioEntreClientes.Respuesta Implements Interfaces.INuevoSaldos.ReenvioEntreClientes
            Dim results() As Object = Me.Invoke("ReenvioEntreClientes", New Object() {Peticion})
            Return CType(results(0), GenesisSaldos.Contenedores.ReenvioEntreClientes.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/RecuperarDocumentosSinSalidaRecorrido", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function RecuperarDocumentosSinSalidaRecorrido(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentosSinSalidaRecorrido.Peticion) As Respuesta Implements INuevoSaldos.RecuperarDocumentosSinSalidaRecorrido
            Dim results() As Object = Me.Invoke("RecuperarDocumentosSinSalidaRecorrido", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.GenesisSaldos.Documento.RecuperarDocumentosSinSalidaRecorrido.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisSaldos/RomperPrecintosSaldosSalidas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function RomperPrecintosSaldosSalidas(Peticion As Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas.Peticion) As Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas.Respuesta Implements INuevoSaldos.RomperPrecintosSaldosSalidas
            Dim results() As Object = Me.Invoke("RomperPrecintosSaldosSalidas", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas.Respuesta))
        End Function
    End Class

End Namespace