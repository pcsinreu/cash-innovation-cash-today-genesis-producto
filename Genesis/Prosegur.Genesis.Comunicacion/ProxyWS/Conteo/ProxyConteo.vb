Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports ContractoConteo = Prosegur.Global.GesEfectivo.Conteo.ContractoServicio

Namespace ProxyWS.Conteo

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.Diagnostics.DebuggerStepThroughAttribute(), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="ConteoSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.Conteo")> _
    Public Class ProxyConteo
        Inherits ProxyWS.ServicioBase
        Implements ContractoConteo.IConteo

        Private sesionInfoValueField As New ContractoConteo.ServicoBase.SesionInfo()

        Private useDefaultCredentialsSetExplicitly As Boolean
        '''<remarks/>
        Public Sub New()
            MyBase.New()
            Me.useDefaultCredentialsSetExplicitly = True
        End Sub

        Public Property SesionInfoValue() As ContractoConteo.ServicoBase.SesionInfo
            Get
                Return Me.sesionInfoValueField
            End Get
            Set(ByVal value As ContractoConteo.ServicoBase.SesionInfo)
                Me.sesionInfoValueField = value
            End Set
        End Property

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/AsociarBultoaPuesto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AsociarBultoaPuesto(ByVal Peticion As ContractoConteo.AsociarBultoaPuesto.Peticion) As ContractoConteo.AsociarBultoaPuesto.Respuesta Implements ContractoConteo.IConteo.AsociarBultoaPuesto
            Dim results() As Object = Me.Invoke("AsociarBultoaPuesto", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.AsociarBultoaPuesto.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarDatosRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarDatosRemesa(ByVal objRecuperarDatosRemesaPeticion As ContractoConteo.RecuperarDatosRemesa.Peticion) As ContractoConteo.RecuperarDatosRemesa.Respuesta Implements ContractoConteo.IConteo.RecuperarDatosRemesa
            Dim results() As Object = Me.Invoke("RecuperarDatosRemesa", New Object() {objRecuperarDatosRemesaPeticion})
            Return CType(results(0), ContractoConteo.RecuperarDatosRemesa.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarDatosIAC", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarDatosIAC(ByVal Peticion As ContractoConteo.RecuperarDatosIAC.Peticion) As ContractoConteo.RecuperarDatosIAC.Respuesta Implements ContractoConteo.IConteo.RecuperarDatosIAC
            Dim results() As Object = Me.Invoke("RecuperarDatosIAC", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarDatosIAC.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarTrabajoEnCurso", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarTrabajoEnCurso(ByVal Peticion As ContractoConteo.RecuperarTrabajoEnCurso.Peticion) As ContractoConteo.RecuperarTrabajoEnCurso.Respuesta Implements ContractoConteo.IConteo.RecuperarTrabajoEnCurso
            Dim results() As Object = Me.Invoke("RecuperarTrabajoEnCurso", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarTrabajoEnCurso.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarSecuenciaTrabajo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarSecuenciaTrabajo(ByVal Peticion As ContractoConteo.RecuperarSecuenciaTrabajo.Peticion) As ContractoConteo.RecuperarSecuenciaTrabajo.Respuesta Implements ContractoConteo.IConteo.RecuperarSecuenciaTrabajo
            Dim results() As Object = Me.Invoke("RecuperarSecuenciaTrabajo", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarSecuenciaTrabajo.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/AsignarBultoConfeccion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AsignarBultoConfeccion(ByVal Peticion As ContractoConteo.AsignarBultoConfeccion.Peticion) As ContractoConteo.AsignarBultoConfeccion.Respuesta Implements ContractoConteo.IConteo.AsignarBultoConfeccion
            Dim results() As Object = Me.Invoke("AsignarBultoConfeccion", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.AsignarBultoConfeccion.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarDatosConteoParcial", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarDatosConteoParcial(ByVal Peticion As ContractoConteo.GuardarDatosConteoParcial.Peticion) As ContractoConteo.GuardarDatosConteoParcial.Respuesta Implements ContractoConteo.IConteo.GuardarDatosConteoParcial
            Dim results() As Object = Me.Invoke("GuardarDatosConteoParcial", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarDatosConteoParcial.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarDatosIAC", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarDatosIAC(ByVal Peticion As ContractoConteo.GuardarDatosIAC.Peticion) As ContractoConteo.GuardarDatosIAC.Respuesta Implements ContractoConteo.IConteo.GuardarDatosIAC
            Dim results() As Object = Me.Invoke("GuardarDatosIAC", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarDatosIAC.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/VerificarTodosBultosProcesados", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function VerificarTodosBultosProcesados(ByVal Peticion As ContractoConteo.VerificarTodosBultosProcesados.Peticion) As ContractoConteo.VerificarTodosBultosProcesados.Respuesta Implements ContractoConteo.IConteo.VerificarTodosBultosProcesados
            Dim results() As Object = Me.Invoke("VerificarTodosBultosProcesados", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.VerificarTodosBultosProcesados.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/VerificarHeCuadradoDiferencias", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function VerificarHeCuadradoDiferencias(ByVal Peticion As ContractoConteo.VerificarHeCuadradoDiferencias.Peticion) As ContractoConteo.VerificarHeCuadradoDiferencias.Respuesta Implements ContractoConteo.IConteo.VerificarHeCuadradoDiferencias
            Dim results() As Object = Me.Invoke("VerificarHeCuadradoDiferencias", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.VerificarHeCuadradoDiferencias.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RecibirRemesasPendientes
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 12/05/2009 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
           System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecibirRemesasPendientes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecibirRemesasPendientes(ByVal Peticion As ContractoConteo.RecibirRemesasPendientes.Peticion) As ContractoConteo.RecibirRemesasPendientes.Respuesta Implements ContractoConteo.IConteo.RecibirRemesasPendientes
            Dim results() As Object = Me.Invoke("RecibirRemesasPendientes", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecibirRemesasPendientes.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RegistroTiempo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RegistroTiempo(ByVal Peticion As ContractoConteo.RegistroTiempo.Peticion) As ContractoConteo.RegistroTiempo.Respuesta Implements ContractoConteo.IConteo.RegistroTiempo
            Dim results() As Object = Me.Invoke("RegistroTiempo", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RegistroTiempo.Respuesta)
        End Function

        ''' <summary>
        ''' Cerrar Bulto
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 17/07/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/CerrarBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarBulto(ByVal Peticion As ContractoConteo.CerrarBulto.Peticion) As ContractoConteo.CerrarBulto.Respuesta Implements ContractoConteo.IConteo.CerrarBulto
            Dim results() As Object = Me.Invoke("CerrarBulto", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.CerrarBulto.Respuesta)
        End Function

        ''' <summary>
        ''' Cerrar Remesa
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 21/07/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/CerrarRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarRemesa(ByVal Peticion As ContractoConteo.CerrarRemesa.Peticion) As ContractoConteo.CerrarRemesa.Respuesta Implements ContractoConteo.IConteo.CerrarRemesa
            Dim results() As Object = Me.Invoke("CerrarRemesa", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.CerrarRemesa.Respuesta)
        End Function

        ''' <summary>
        ''' Recuperar Saldo Deglosado
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [rafael.nasorri] 23/07/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarSaldoDeglosado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarSaldoDeglosado(ByVal Peticion As ContractoConteo.RecuperarSaldoDeglosado.Peticion) As ContractoConteo.RecuperarSaldoDeglosado.Respuesta Implements ContractoConteo.IConteo.RecuperarSaldoDeglosado
            Dim results() As Object = Me.Invoke("RecuperarSaldoDeglosado", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarSaldoDeglosado.Respuesta)
        End Function

        ''' <summary>
        ''' Cerrar Bulto Hijo
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [rodrigo.almeida] 29/07/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/CerrarBultoHijo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarBultoHijo(ByVal Peticion As ContractoConteo.CerrarBultoHijo.Peticion) As ContractoConteo.CerrarBultoHijo.Respuesta Implements ContractoConteo.IConteo.CerrarBultoHijo
            Dim results() As Object = Me.Invoke("CerrarBultoHijo", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.CerrarBultoHijo.Respuesta)
        End Function

        ''' <summary>
        ''' Recuperar Estado Bulto Parcializado
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [rafael.nasorri] 30/07/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarEstadoBultoParcializado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarEstadoBultoParcializado(ByVal Peticion As ContractoConteo.RecuperarEstadoBultoParcializado.Peticion) As ContractoConteo.RecuperarEstadoBultoParcializado.Respuesta Implements ContractoConteo.IConteo.RecuperarEstadoBultoParcializado
            Dim results() As Object = Me.Invoke("RecuperarEstadoBultoParcializado", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarEstadoBultoParcializado.Respuesta)
        End Function

        ''' <summary>
        ''' Recuperar Info Mantenimiento
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 31/07/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarInfoMantenimiento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarInfoMantenimiento() As ContractoConteo.RecuperarInfoMantenimiento.Respuesta Implements ContractoConteo.IConteo.RecuperarInfoMantenimiento
            Dim results() As Object = Me.Invoke("RecuperarInfoMantenimiento", New Object() {})
            Return CType(results(0), ContractoConteo.RecuperarInfoMantenimiento.Respuesta)
        End Function

        ''' <summary>
        ''' Guardar Datos Confeccion Bulto
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [rafael.nasorri] 14/08/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarDatosConfeccionBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarDatosConfeccionBulto(ByVal Peticion As ContractoConteo.GuardarDatosConfeccionBulto.Peticion) As ContractoConteo.GuardarDatosConfeccionBulto.Respuesta Implements ContractoConteo.IConteo.GuardarDatosConfeccionBulto
            Dim results() As Object = Me.Invoke("GuardarDatosConfeccionBulto", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarDatosConfeccionBulto.Respuesta)
        End Function

        ''' <summary>
        ''' Parcializar Bulto
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [rafael.nasorri] 19/08/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ParcializarBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ParcializarBulto(ByVal Peticion As ContractoConteo.ParcializarBulto.Peticion) As ContractoConteo.ParcializarBulto.Respuesta Implements ContractoConteo.IConteo.ParcializarBulto
            Dim results() As Object = Me.Invoke("ParcializarBulto", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ParcializarBulto.Respuesta)
        End Function

        ''' <summary>
        ''' Recuperar Valores Contados
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [rafael.nasorri] 22/08/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarValoresContados", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarValoresContados(ByVal Peticion As ContractoConteo.RecuperarValoresContados.Peticion) As ContractoConteo.RecuperarValoresContados.Respuesta Implements ContractoConteo.IConteo.RecuperarValoresContados
            Dim results() As Object = Me.Invoke("RecuperarValoresContados", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarValoresContados.Respuesta)
        End Function

        ''' <summary>
        ''' Loga os erros gerados pela aplicação.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [cbomtempo] 28/08/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/LogarErro", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function LogarErro(ByVal Peticion As ContractoConteo.LogarErro.Peticion) As ContractoConteo.LogarErro.Respuesta Implements ContractoConteo.IConteo.LogarErro
            Dim results() As Object = Me.Invoke("LogarErro", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.LogarErro.Respuesta)
        End Function

        ''' <summary>
        ''' Insere o fechamento de uma parcial e de um bulto em uma única transação
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 27/10/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/CerrarParcialBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarParcialBulto(ByVal Peticion As ContractoConteo.CerrarParcialBulto.Peticion) As ContractoConteo.CerrarParcialBulto.Respuesta Implements ContractoConteo.IConteo.CerrarParcialBulto
            Dim results() As Object = Me.Invoke("CerrarParcialBulto", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.CerrarParcialBulto.Respuesta)
        End Function

        ''' <summary>
        ''' Recupera os declarados contados
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 26/11/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarDeclarados", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarDeclarados(ByVal Peticion As ContractoConteo.RecuperarDeclarados.Peticion) As ContractoConteo.RecuperarDeclarados.Respuesta Implements ContractoConteo.IConteo.RecuperarDeclarados
            Dim results() As Object = Me.Invoke("RecuperarDeclarados", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarDeclarados.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy AddIntervencionSupervisor
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 07/05/2009 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/AddIntervencionSupervisor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AddIntervencionSupervisor(ByVal Peticion As ContractoConteo.IntervencionSupervisor.AddIntervencionSupervisor.Peticion) As ContractoConteo.IntervencionSupervisor.AddIntervencionSupervisor.Respuesta Implements ContractoConteo.IConteo.AddIntervencionSupervisor
            Dim results() As Object = Me.Invoke("AddIntervencionSupervisor", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.IntervencionSupervisor.AddIntervencionSupervisor.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy SetIntervencionSupervisor
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 07/05/2009 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/SetIntervencionSupervisor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function SetIntervencionSupervisor(ByVal Peticion As ContractoConteo.IntervencionSupervisor.SetIntervencionSupervisor.Peticion) As ContractoConteo.IntervencionSupervisor.SetIntervencionSupervisor.Respuesta Implements ContractoConteo.IConteo.SetIntervencionSupervisor
            Dim results() As Object = Me.Invoke("SetIntervencionSupervisor", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.IntervencionSupervisor.SetIntervencionSupervisor.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy ValidarTermino
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 11/05/2009 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ValidarTermino", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ValidarTermino(ByVal Peticion As ContractoConteo.ValidarTermino.Peticion) As ContractoConteo.ValidarTermino.Respuesta Implements ContractoConteo.IConteo.ValidarTermino
            Dim results() As Object = Me.Invoke("ValidarTermino", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ValidarTermino.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy ConsultarEstadosParciales
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 12/05/2009 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
           System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ConsultarEstadosParciales", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ConsultarEstadosParciales(ByVal Peticion As ContractoConteo.ConsultarEstadosParciales.Peticion) As ContractoConteo.ConsultarEstadosParciales.Respuesta Implements ContractoConteo.IConteo.ConsultarEstadosParciales
            Dim results() As Object = Me.Invoke("ConsultarEstadosParciales", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ConsultarEstadosParciales.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RecibirRemesasPendientes
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 12/05/2009 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarMotivos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarMotivos(ByVal Peticion As ContractoConteo.IntervencionSupervisor.RecuperarMotivos.Peticion) As ContractoConteo.IntervencionSupervisor.RecuperarMotivos.Respuesta Implements ContractoConteo.IConteo.RecuperarMotivos
            Dim results() As Object = Me.Invoke("RecuperarMotivos", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.IntervencionSupervisor.RecuperarMotivos.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RechazarBulto
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcelo.msoliveira] 15/07/2009 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RechazarBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RechazarBulto(ByVal Peticion As ContractoConteo.RechazarBulto.Peticion) As ContractoConteo.RechazarBulto.Respuesta Implements ContractoConteo.IConteo.RechazarBulto
            Dim results() As Object = Me.Invoke("RechazarBulto", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RechazarBulto.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RecuperarEntregaDeValores
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcelo.msoliveira] 22/07/2009 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarEntregaDeValores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarEntregaDeValores(ByVal Peticion As ContractoConteo.RecuperarEntregaDeValores.Peticion) As ContractoConteo.RecuperarEntregaDeValores.Respuesta Implements ContractoConteo.IConteo.RecuperarEntregaDeValores
            Dim results() As Object = Me.Invoke("RecuperarEntregaDeValores", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarEntregaDeValores.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RecuperarEntregaDeValoresNaoRetirados
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [maoliveira] 20/10/2009 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarEntregaDeValoresNaoRetirados", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarEntregaDeValoresNaoRetirados(ByVal Peticion As ContractoConteo.RecuperarEntregaDeValoresNaoRetirados.Peticion) As ContractoConteo.RecuperarEntregaDeValoresNaoRetirados.Respuesta Implements ContractoConteo.IConteo.RecuperarEntregaDeValoresNaoRetirados
            Dim results() As Object = Me.Invoke("RecuperarEntregaDeValoresNaoRetirados", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarEntregaDeValoresNaoRetirados.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy CerrarBultoRemesa
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 23/07/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/CerrarBultoRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarBultoRemesa(ByVal Peticion As ContractoConteo.CerrarBultoRemesa.Peticion) As ContractoConteo.CerrarBultoRemesa.Respuesta Implements ContractoConteo.IConteo.CerrarBultoRemesa
            Dim results() As Object = Me.Invoke("CerrarBultoRemesa", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.CerrarBultoRemesa.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy CerrarParcialBultoRemesa
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 24/07/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/CerrarParcialBultoRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarParcialBultoRemesa(ByVal Peticion As ContractoConteo.CerrarParcialBultoRemesa.Peticion) As ContractoConteo.CerrarParcialBultoRemesa.Respuesta Implements ContractoConteo.IConteo.CerrarParcialBultoRemesa
            Dim results() As Object = Me.Invoke("CerrarParcialBultoRemesa", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.CerrarParcialBultoRemesa.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RechazarRemesa
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 24/07/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RechazarRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RechazarRemesa(ByVal Peticion As ContractoConteo.RechazarRemesa.Peticion) As ContractoConteo.RechazarRemesa.Respuesta Implements ContractoConteo.IConteo.RechazarRemesa
            Dim results() As Object = Me.Invoke("RechazarRemesa", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RechazarRemesa.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RegistrarImpresionTicket
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcelo.msoliveira] 24/07/2009 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RegistrarImpresionTicket", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RegistrarImpresionTicket(ByVal Peticion As ContractoConteo.RegistrarImpresionTicket.Peticion) As ContractoConteo.RegistrarImpresionTicket.Respuesta Implements ContractoConteo.IConteo.RegistrarImpresionTicket
            Dim results() As Object = Me.Invoke("RegistrarImpresionTicket", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RegistrarImpresionTicket.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy GuardarDatosRetiroValores
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 27/07/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarDatosRetiroValores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarDatosRetiroValores(ByVal Peticion As ContractoConteo.GuardarDatosRetiroValores.Peticion) As ContractoConteo.GuardarDatosRetiroValores.Respuesta Implements ContractoConteo.IConteo.GuardarDatosRetiroValores
            Dim results() As Object = Me.Invoke("GuardarDatosRetiroValores", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarDatosRetiroValores.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RechazarConteo
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 13/08/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RechazarConteo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RechazarConteo(ByVal Peticion As ContractoConteo.RechazarConteo.Peticion) As ContractoConteo.RechazarConteo.Respuesta Implements ContractoConteo.IConteo.RechazarConteo
            Dim results() As Object = Me.Invoke("RechazarConteo", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RechazarConteo.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy AgregarDivisaPosible
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/09/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/AgregarDivisaPosible", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AgregarDivisaPosible(ByVal Peticion As ContractoConteo.AgregarDivisaPosible.Peticion) As ContractoConteo.AgregarDivisaPosible.Respuesta Implements ContractoConteo.IConteo.AgregarDivisaPosible
            Dim results() As Object = Me.Invoke("AgregarDivisaPosible", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.AgregarDivisaPosible.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy CerrarBultoParcializado
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcelo.msoliveira] 25/09/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/CerrarBultoParcializado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarBultoParcializado(ByVal Peticion As ContractoConteo.CerrarBultoParcializado.Peticion) As ContractoConteo.CerrarBultoParcializado.Respuesta Implements ContractoConteo.IConteo.CerrarBultoParcializado
            Dim results() As Object = Me.Invoke("CerrarBultoParcializado", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.CerrarBultoParcializado.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy CerrarBultoParcializado
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcelo.msoliveira] 28/09/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/CerrarRemesaParcializada", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarRemesaParcializada(ByVal Peticion As ContractoConteo.CerrarRemesaParcializada.Peticion) As ContractoConteo.CerrarRemesaParcializada.Respuesta Implements ContractoConteo.IConteo.CerrarRemesaParcializada
            Dim results() As Object = Me.Invoke("CerrarRemesaParcializada", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.CerrarRemesaParcializada.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy GuardarComentario
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 27/10/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarComentario", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarComentario(ByVal Peticion As ContractoConteo.GuardarComentario.Peticion) As ContractoConteo.GuardarComentario.Respuesta Implements ContractoConteo.IConteo.GuardarComentario
            Dim results() As Object = Me.Invoke("GuardarComentario", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarComentario.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy ObtenerComentario
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 27/10/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ObtenerComentario", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerComentario(ByVal Peticion As ContractoConteo.ObtenerComentario.Peticion) As ContractoConteo.ObtenerComentario.Respuesta Implements ContractoConteo.IConteo.ObtenerComentario
            Dim results() As Object = Me.Invoke("ObtenerComentario", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ObtenerComentario.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy EliminarComentario
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/11/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/EliminarComentario", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EliminarComentario(ByVal Peticion As ContractoConteo.EliminarComentario.Peticion) As ContractoConteo.EliminarComentario.Respuesta Implements ContractoConteo.IConteo.EliminarComentario
            Dim results() As Object = Me.Invoke("EliminarComentario", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.EliminarComentario.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy EliminarComentario
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/11/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/BuscarBultosORemesas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BuscarBultosORemesas(ByVal Peticion As ContractoConteo.BuscarBultosORemesas.Peticion) As ContractoConteo.BuscarBultosORemesas.Respuesta Implements ContractoConteo.IConteo.BuscarBultosORemesas
            Dim results() As Object = Me.Invoke("BuscarBultosORemesas", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.BuscarBultosORemesas.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RehabilitarConteo
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/11/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RehabilitarConteo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RehabilitarConteo(ByVal Peticion As ContractoConteo.RehabilitarConteo.Peticion) As ContractoConteo.RehabilitarConteo.Respuesta Implements ContractoConteo.IConteo.RehabilitarConteo
            Dim results() As Object = Me.Invoke("RehabilitarConteo", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RehabilitarConteo.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy GuardarDatosModificadosSupervisor
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 25/11/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarDatosModificadosSupervisor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarDatosModificadosSupervisor(ByVal Peticion As ContractoConteo.GuardarDatosModificadosSupervisor.Peticion) As ContractoConteo.GuardarDatosModificadosSupervisor.Respuesta Implements ContractoConteo.IConteo.GuardarDatosModificadosSupervisor
            Dim results() As Object = Me.Invoke("GuardarDatosModificadosSupervisor", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarDatosModificadosSupervisor.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy EliminarParcial
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 25/11/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/EliminarParcial", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EliminarParcial(ByVal Peticion As ContractoConteo.EliminarParcial.Peticion) As ContractoConteo.EliminarParcial.Respuesta Implements ContractoConteo.IConteo.EliminarParcial
            Dim results() As Object = Me.Invoke("EliminarParcial", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.EliminarParcial.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy EliminarBulto
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 01/12/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/EliminarBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EliminarBulto(ByVal Peticion As ContractoConteo.EliminarBulto.Peticion) As ContractoConteo.EliminarBulto.Respuesta Implements ContractoConteo.IConteo.EliminarBulto
            Dim results() As Object = Me.Invoke("EliminarBulto", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.EliminarBulto.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy EliminarBulto
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 01/12/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/EliminarRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EliminarRemesa(ByVal Peticion As ContractoConteo.EliminarRemesa.Peticion) As ContractoConteo.EliminarRemesa.Respuesta Implements ContractoConteo.IConteo.EliminarRemesa
            Dim results() As Object = Me.Invoke("EliminarRemesa", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.EliminarRemesa.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy MarcarModificadaRemesa
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/12/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/MarcarModificadaRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function MarcarModificadaRemesa(ByVal Peticion As ContractoConteo.MarcarModificadaRemesa.Peticion) As ContractoConteo.MarcarModificadaRemesa.Respuesta Implements ContractoConteo.IConteo.MarcarModificadaRemesa
            Dim results() As Object = Me.Invoke("MarcarModificadaRemesa", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.MarcarModificadaRemesa.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy ActualizarConteoReabierto
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/12/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ActualizarConteoReabierto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ActualizarConteoReabierto(ByVal Peticion As ContractoConteo.ActualizarConteoReabierto.Peticion) As ContractoConteo.ActualizarConteoReabierto.Respuesta Implements ContractoConteo.IConteo.ActualizarConteoReabierto
            Dim results() As Object = Me.Invoke("ActualizarConteoReabierto", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ActualizarConteoReabierto.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RecuperarConteosReabiertos
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 18/12/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarConteosReabiertos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarConteosReabiertos(ByVal Peticion As ContractoConteo.RecuperarConteosReabiertos.Peticion) As ContractoConteo.RecuperarConteosReabiertos.Respuesta Implements ContractoConteo.IConteo.RecuperarConteosReabiertos
            Dim results() As Object = Me.Invoke("RecuperarConteosReabiertos", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarConteosReabiertos.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy ReabrirConteo
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 21/12/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ReabrirConteo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ReabrirConteo(ByVal Peticion As ContractoConteo.ReabrirConteo.Peticion) As ContractoConteo.ReabrirConteo.Respuesta Implements ContractoConteo.IConteo.ReabrirConteo
            Dim results() As Object = Me.Invoke("ReabrirConteo", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ReabrirConteo.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy ReabrirConteo
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 21/12/2009 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/SuperServicioSupervisor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function SuperServicioSupervisor(ByVal Peticion As ContractoConteo.SuperServicioSupervisor.Peticion) As ContractoConteo.SuperServicioSupervisor.Respuesta Implements ContractoConteo.IConteo.SuperServicioSupervisor
            Dim results() As Object = Me.Invoke("SuperServicioSupervisor", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.SuperServicioSupervisor.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RecuperarDatosParcialCP
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcelo.msoliveira] 07/01/2010 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarDatosParcialCP", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarDatosParcialCP(ByVal Peticion As ContractoConteo.RecuperarDatosParcialCP.Peticion) As ContractoConteo.RecuperarDatosParcialCP.Respuesta Implements ContractoConteo.IConteo.RecuperarDatosParcialCP
            Dim results() As Object = Me.Invoke("RecuperarDatosParcialCP", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarDatosParcialCP.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy MarcarParcialCP
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcelo.msoliveira] 07/01/2010 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/MarcarParcialCP", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function MarcarParcialCP(ByVal Peticion As ContractoConteo.MarcarParcialCP.Peticion) As ContractoConteo.MarcarParcialCP.Respuesta Implements ContractoConteo.IConteo.MarcarParcialCP
            Dim results() As Object = Me.Invoke("MarcarParcialCP", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.MarcarParcialCP.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy GuardarParcialCP
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcelo.msoliveira] 07/01/2010 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarParcialCP", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarParcialCP(ByVal Peticion As ContractoConteo.GuardarParcialCP.Peticion) As ContractoConteo.GuardarParcialCP.Respuesta Implements ContractoConteo.IConteo.GuardarParcialCP
            Dim results() As Object = Me.Invoke("GuardarParcialCP", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarParcialCP.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy ActualizarCantidadParciales
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcelo.msoliveira] 07/01/2010 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ActualizarCantidadParciales", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ActualizarCantidadParciales(ByVal Peticion As ContractoConteo.ActualizarCantidadParciales.Peticion) As ContractoConteo.ActualizarCantidadParciales.Respuesta Implements ContractoConteo.IConteo.ActualizarCantidadParciales
            Dim results() As Object = Me.Invoke("ActualizarCantidadParciales", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ActualizarCantidadParciales.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy Test
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 15/01/2010 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As ContractoConteo.Test.Respuesta Implements ContractoConteo.IConteo.Test
            Dim results() As Object = Me.Invoke("Test", New Object())
            Return CType(results(0), ContractoConteo.Test.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy GuardarDatosConfeccionRemesa
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 05/03/2010 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarDatosConfeccionRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarDatosConfeccionRemesa(ByVal Peticion As ContractoConteo.GuardarDatosConfeccionRemesa.Peticion) As ContractoConteo.GuardarDatosConfeccionRemesa.Respuesta Implements ContractoConteo.IConteo.GuardarDatosConfeccionRemesa
            Dim results() As Object = Me.Invoke("GuardarDatosConfeccionRemesa", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarDatosConfeccionRemesa.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy GuardarParcialCpAtm
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 26/04/2010 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarParcialCpAtm", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarParcialCpAtm(ByVal Peticion As ContractoConteo.GuardarParcialCpAtm.Peticion) As ContractoConteo.GuardarParcialCpAtm.Respuesta Implements ContractoConteo.IConteo.GuardarParcialCpAtm
            Dim results() As Object = Me.Invoke("GuardarParcialCpAtm", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarParcialCpAtm.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy GuardarDeclaradosModificadosSupervisorAtm
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 27/04/2010 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarDeclaradosModificadosSupervisorAtm", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarDeclaradosModificadosSupervisorAtm(ByVal Peticion As ContractoConteo.GuardarDeclaradosModificadosSupervisorAtm.Peticion) As ContractoConteo.GuardarDeclaradosModificadosSupervisorAtm.Respuesta Implements ContractoConteo.IConteo.GuardarDeclaradosModificadosSupervisorAtm
            Dim results() As Object = Me.Invoke("GuardarDeclaradosModificadosSupervisorAtm", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarDeclaradosModificadosSupervisorAtm.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy GetIntervencionSupervisor
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [cazevedo] 25/05/2010 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GetIntervencionSupervisor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetIntervencionSupervisor(ByVal Peticion As ContractoConteo.IntervencionSupervisor.GetIntervencionSupervisor.Peticion) As ContractoConteo.IntervencionSupervisor.GetIntervencionSupervisor.Respuesta Implements ContractoConteo.IConteo.GetIntervencionSupervisor
            Dim results() As Object = Me.Invoke("GetIntervencionSupervisor", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.IntervencionSupervisor.GetIntervencionSupervisor.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy BuscarPuestos
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 08/10/2010 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/BuscarPuestos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BuscarPuestos(ByVal Peticion As ContractoConteo.ActivacionDesactivacionPuesto.BuscarPuestos.Peticion) As ContractoConteo.ActivacionDesactivacionPuesto.BuscarPuestos.Respuesta Implements ContractoConteo.IConteo.BuscarPuestos
            Dim results() As Object = Me.Invoke("BuscarPuestos", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ActivacionDesactivacionPuesto.BuscarPuestos.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy ActivarDesactivarPuestos
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 08/10/2010 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ActivarDesactivarPuestos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ActivarDesactivarPuestos(ByVal Peticion As ContractoConteo.ActivacionDesactivacionPuesto.ActivarDesactivarPuestos.Peticion) As ContractoConteo.ActivacionDesactivacionPuesto.ActivarDesactivarPuestos.Respuesta Implements ContractoConteo.IConteo.ActivarDesactivarPuestos
            Dim results() As Object = Me.Invoke("ActivarDesactivarPuestos", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ActivacionDesactivacionPuesto.ActivarDesactivarPuestos.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy DeshacerReapertura
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/11/2010 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/DeshacerReapertura", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function DeshacerReapertura(ByVal Peticion As ContractoConteo.DeshacerReapertura.Peticion) As ContractoConteo.DeshacerReapertura.Respuesta Implements ContractoConteo.IConteo.DeshacerReapertura
            Dim results() As Object = Me.Invoke("DeshacerReapertura", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.DeshacerReapertura.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy DeshacerReapertura
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [gustavo.fraga] 03/03/2011 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/EnvioDatosConteo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EnvioDatosConteo(ByVal Peticion As ContractoConteo.EnvioDatosConteo.Peticion) As ContractoConteo.EnvioDatosConteo.Respuesta Implements ContractoConteo.IConteo.EnvioDatosConteo
            Dim results() As Object = Me.Invoke("EnvioDatosConteo", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.EnvioDatosConteo.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy RecuperarValoresParametros
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 11/03/2011 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarDatosParametros", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarDatosParametros(ByVal Peticion As ContractoConteo.RecuperarDatosParametros.Peticion) As ContractoConteo.RecuperarDatosParametros.Respuesta Implements ContractoConteo.IConteo.RecuperarDatosParametros
            Dim results() As Object = Me.Invoke("RecuperarDatosParametros", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarDatosParametros.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy GuardarDatosParametros
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 11/03/2011 - Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarDatosParametros", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarDatosParametros(ByVal Peticion As ContractoConteo.GuardarDatosParametros.Peticion) As ContractoConteo.GuardarDatosParametros.Respuesta Implements ContractoConteo.IConteo.GuardarDatosParametros
            Dim results() As Object = Me.Invoke("GuardarDatosParametros", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarDatosParametros.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy ModificarRegistroMovimiento
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>  
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ModificarRegistroMovimiento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ModificarRegistroMovimiento(ByVal Peticion As ContractoConteo.ModificarRegistroMovimiento.Peticion) As ContractoConteo.ModificarRegistroMovimiento.Respuesta Implements ContractoConteo.IConteo.ModificarRegistroMovimiento
            Dim results() As Object = Me.Invoke("ModificarRegistroMovimiento", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ModificarRegistroMovimiento.Respuesta)
        End Function

        ''' <summary>
        ''' Proxy ModificarRegistroMovimiento
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>  
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ModificarDatosRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ModificarDatosRemesa(ByVal Peticion As ContractoConteo.ModificarDatosRemesa.Peticion) As ContractoConteo.ModificarDatosRemesa.Respuesta Implements ContractoConteo.IConteo.ModificarDatosRemesa
            Dim results() As Object = Me.Invoke("ModificarDatosRemesa", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ModificarDatosRemesa.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/BuscaListaValor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BuscaListaValor() As ContractoConteo.ListaValor.Respuesta Implements ContractoConteo.IConteo.BuscaListaValor
            Dim results() As Object = Me.Invoke("BuscaListaValor", New Object() {})
            Return CType(results(0), ContractoConteo.ListaValor.Respuesta)
        End Function

        '<System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        'System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarDatosTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        'Public Function RecuperarDatosTira(ByVal Peticion As ContractoConteo.RecuperarDatosTira.Peticion) As ContractoConteo.RecuperarDatosTira.Respuesta Implements ContractoConteo.IConteo.RecuperarDatosTira
        '    Dim results() As Object = Me.Invoke("RecuperarDatosTira", New Object() {Peticion})
        '    Return CType(results(0), ContractoConteo.RecuperarDatosTira.Respuesta)
        'End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/DividirRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function DividirRemesa(ByVal Peticion As ContractoConteo.DividirRemesa.Peticion) As ContractoConteo.DividirRemesa.Respuesta Implements ContractoConteo.IConteo.DividirRemesa
            Dim results() As Object = Me.Invoke("DividirRemesa", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.DividirRemesa.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarCantidadParcialesDeclarados", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarCantidadParcialesDeclarados(ByVal Peticion As ContractoConteo.GuardarCantidadParcialesDeclarados.Peticion) As ContractoConteo.GuardarCantidadParcialesDeclarados.Respuesta Implements ContractoConteo.IConteo.GuardarCantidadParcialesDeclarados
            Dim results() As Object = Me.Invoke("GuardarCantidadParcialesDeclarados", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarCantidadParcialesDeclarados.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarDatosParteDiferencias", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarDatosParteDiferencias(Peticion As ContractoConteo.RecuperarDatosParteDiferencias.Peticion) As ContractoConteo.RecuperarDatosParteDiferencias.Respuesta Implements ContractoConteo.IConteo.RecuperarDatosParteDiferencias
            Dim results() As Object = Me.Invoke("RecuperarDatosParteDiferencias", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarDatosParteDiferencias.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GravarDocumentoParteDiferencias", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GravarDocumentoParteDiferencias(Peticion As ContractoConteo.GravarDocumentoParteDiferencias.Peticion) As ContractoConteo.GravarDocumentoParteDiferencias.Respuesta Implements ContractoConteo.IConteo.GravarDocumentoParteDiferencias
            Dim results() As Object = Me.Invoke("GravarDocumentoParteDiferencias", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GravarDocumentoParteDiferencias.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarDivisas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarDivisas(ByVal Peticion As ContractoConteo.RecuperarDivisas.Peticion) As ContractoConteo.RecuperarDivisas.Respuesta Implements ContractoConteo.IConteo.RecuperarDivisas
            Dim results() As Object = Me.Invoke("RecuperarDivisas", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarDivisas.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarMovimientoEnProceso", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarMovimientoEnProceso(Peticion As ContractoConteo.RecuperarMovimientoEnProceso.Peticion) As ContractoConteo.RecuperarMovimientoEnProceso.Respuesta Implements ContractoConteo.IConteo.RecuperarMovimientoEnProceso
            Dim results() As Object = Me.Invoke("RecuperarMovimientoEnProceso", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarMovimientoEnProceso.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarDatosTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarDatosTira(Peticion As ContractoConteo.RecuperarDatosTira.Peticion) As ContractoConteo.RecuperarDatosTira.Respuesta Implements ContractoConteo.IConteo.RecuperarDatosTira
            Dim results() As Object = Me.Invoke("RecuperarDatosTira", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarDatosTira.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/VerificaRemesaExiste", _
            RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", _
            Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function VerificaRemesaExiste(Peticion As ContractoConteo.VerificaRemesaExiste.Peticion) As ContractoConteo.VerificaRemesaExiste.Respuesta
            Dim results() As Object = Me.Invoke("VerificaRemesaExiste", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RespuestaGenerico)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
       System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GetImportaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetImportaciones(Peticion As ContractoConteo.GetImportaciones.Peticion) As ContractoConteo.GetImportaciones.Respuesta Implements ContractoConteo.IConteo.GetImportaciones
            Dim results() As Object = Me.Invoke("GetImportaciones", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GetImportaciones.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
       System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarEstadoRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarEstadoRemesa(Peticion As ContractoConteo.RecuperarEstadoRemesa.Peticion) As ContractoConteo.RecuperarEstadoRemesa.Respuesta Implements ContractoConteo.IConteo.RecuperarEstadoRemesa
            Dim results() As Object = Me.Invoke("RecuperarEstadoRemesa", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarEstadoRemesa.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
       System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/EnvioDatosConteoBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EnvioDatosConteoBulto(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.EnvioDatosConteo.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.EnvioDatosConteo.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.EnvioDatosConteoBulto
            Dim results() As Object = Me.Invoke("EnvioDatosConteoBulto", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.EnvioDatosConteo.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
       System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/BuscarRemesaPorTerminoParcial", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BuscarRemesaPorTerminoParcial(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.BuscarRemesaPorTerminoParcial.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.BuscarRemesaPorTerminoParcial.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.BuscarRemesaPorTerminoParcial
            Dim results() As Object = Me.Invoke("BuscarRemesaPorTerminoParcial", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.BuscarRemesaPorTerminoParcial.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
       System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/BuscarArchivos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BuscarArchivos(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.BusquedaArchivos.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.BusquedaArchivos.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.BuscarArchivos
            Dim results() As Object = Me.Invoke("BuscarArchivos", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.BusquedaArchivos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
      System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarModificarDatosConteo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarModificarDatosConteo(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.GuardarModificarDatosConteo.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.GuardarModificarDatosConteo.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.GuardarModificarDatosConteo
            Dim results() As Object = Me.Invoke("GuardarModificarDatosConteo", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarModificarDatosConteo.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/BuscarBultosORemesasMecanizado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BuscarBultosORemesasMecanizado(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.BuscarBultosORemesasMecanizado.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.BuscarBultosORemesasMecanizado.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.BuscarBultosORemesasMecanizado
            Dim results() As Object = Me.Invoke("BuscarBultosORemesasMecanizado", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.BuscarBultosORemesasMecanizado.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
          System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/AsociarArchivoMecanizado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AsociarArchivoMecanizado(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.ProcesarArchivos.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.ProcesarArchivos.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.AsociarArchivoMecanizado
            Dim results() As Object = Me.Invoke("AsociarArchivoMecanizado", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.ProcesarArchivos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
          System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarValorTermino", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarValorTermino(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.GuardarValorTermino.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.GuardarValorTermino.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.GuardarValorTermino
            Dim results() As Object = Me.Invoke("GuardarValorTermino", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.GuardarValorTermino.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
          System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/LeeEscribeEstadoRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function LeeEscribeEstadoRemesa(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.LeeEscribeEstadoRemesa.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.LeeEscribeEstadoRemesa.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.LeeEscribeEstadoRemesa
            Dim results() As Object = Me.Invoke("LeeEscribeEstadoRemesa", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.LeeEscribeEstadoRemesa.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
          System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/EfectuarSalida", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EfectuarSalida(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.EfectuarSalida.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.EfectuarSalida.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.EfectuarSalida
            Dim results() As Object = Me.Invoke("EfectuarSalida", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.EfectuarSalida.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
          System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/EfectuarSalida", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RestarValoresSaldoPuesto(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.RestarValoresSaldoPuesto.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.RestarValoresSaldoPuesto.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.RestarValoresSaldoPuesto
            Dim results() As Object = Me.Invoke("RestarValoresSaldoPuesto", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.RestarValoresSaldoPuesto.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
          System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecibirRemesas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecibirRemesas(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.RecibirRemesas.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.RecibirRemesas.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.RecibirRemesas
            Dim results() As Object = Me.Invoke("RecibirRemesas", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.RecibirRemesas.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ModificarRemesaATM", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ModificarRemesaATM(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.ModificarRemesaATM.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.ModificarRemesaATM.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.ModificarRemesaATM
            Dim results() As Object = Me.Invoke("ModificarRemesaATM", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.ModificarRemesaATM.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/BusquedaParcialPorTerminoBusqueda", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BusquedaParcialPorTerminoBusqueda(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.BusquedaParcialPorTerminoBusqueda.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.BusquedaParcialPorTerminoBusqueda.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.BusquedaParcialPorTerminoBusqueda
            Dim results() As Object = Me.Invoke("BusquedaParcialPorTerminoBusqueda", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.BusquedaParcialPorTerminoBusqueda.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
       System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/VolverEstadoParcial", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function VolverEstadoParcial(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.VolverEstadoParcial.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.VolverEstadoParcial.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.VolverEstadoParcial
            Dim results() As Object = Me.Invoke("VolverEstadoParcial", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.VolverEstadoParcial.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarEstadoModificadosSupervisor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarEstadoModificadosSupervisor(ByVal Peticion As ContractoConteo.GuardarEstadoModificadosSupervisor.Peticion) As ContractoConteo.GuardarEstadoModificadosSupervisor.Respuesta Implements ContractoConteo.IConteo.GuardarEstadoModificadosSupervisor
            Dim results() As Object = Me.Invoke("GuardarEstadoModificadosSupervisor", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarEstadoModificadosSupervisor.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
           System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/CambiarComentarioSupervisor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CambiarComentarioSupervisor(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.IntervencionSupervisor.CambiarComentarioSupervisor.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.IntervencionSupervisor.CambiarComentarioSupervisor.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.CambiarComentarioSupervisor
            Dim results() As Object = Me.Invoke("CambiarComentarioSupervisor", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.IntervencionSupervisor.CambiarComentarioSupervisor.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
                    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/BuscarBultosDocumento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BuscarBultosDocumento(ByVal Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.BuscarBultosDocumento.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.BuscarBultosDocumento.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.BuscarBultosDocumento
            Dim results() As Object = Me.Invoke("BuscarBultosDocumento", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.BuscarBultosDocumento.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
                    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GravarDocumentoBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GravarDocumentoBulto(ByVal Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.GravarDocumentoBulto.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.GravarDocumentoBulto.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.GravarDocumentoBulto
            Dim results() As Object = Me.Invoke("GravarDocumentoBulto", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.GravarDocumentoBulto.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
                    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarGrupoDocumento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarGrupoDocumento(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.GuardarGrupoDocumento.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.GuardarGrupoDocumento.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.GuardarGrupoDocumento
            Dim results() As Object = Me.Invoke("GuardarGrupoDocumento", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.GuardarGrupoDocumento.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
                    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarDocumentoPorIdentificador", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarDocumentoPorIdentificador(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.RecuperarDocumentoPorIdentificador.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.RecuperarDocumentoPorIdentificador.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.RecuperarDocumentoPorIdentificador
            Dim results() As Object = Me.Invoke("RecuperarDocumentoPorIdentificador", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.RecuperarDocumentoPorIdentificador.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
                    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarDocumento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarDocumento(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.GuardarDocumento.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.GuardarDocumento.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.GuardarDocumento
            Dim results() As Object = Me.Invoke("GuardarDocumento", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.GuardarDocumento.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
                    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ConsultaDocumentos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ConsultaDocumentos(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.ConsultaDocumentos.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.ConsultaDocumentos.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.ConsultaDocumentos
            Dim results() As Object = Me.Invoke("ConsultaDocumentos", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.ConsultaDocumentos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
                    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/AperturarElemento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AperturarElemento(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.AperturarElemento.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.AperturarElemento.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.AperturarElemento
            Dim results() As Object = Me.Invoke("AperturarElemento", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.AperturarElemento.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
                    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/CrearDocumentoFondos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CrearDocumentoFondos(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.CrearDocumentoFondos.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.CrearDocumentoFondos.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.CrearDocumentoFondos
            Dim results() As Object = Me.Invoke("CrearDocumentoFondos", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.CrearDocumentoFondos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
                    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ExcluirTrabajoEnCurso", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ExcluirTrabajoEnCurso(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.ExcluirTrabajoEnCurso.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.ExcluirTrabajoEnCurso.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IConteo.ExcluirTrabajoEnCurso
            Dim results() As Object = Me.Invoke("ExcluirTrabajoEnCurso", New Object() {Peticion})
            Return CType(results(0), [Global].GesEfectivo.Conteo.ContractoServicio.ExcluirTrabajoEnCurso.Respuesta)
        End Function
    End Class

End Namespace