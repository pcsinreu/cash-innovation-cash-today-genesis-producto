Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

Namespace IAC.Servicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Parametro
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IParametro

        ''' <summary>
        ''' Esta operación es responsable por listar los datos de todos los Parametros .
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 19/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetParametros(peticion As ContractoServicio.Parametro.GetParametros.Peticion) As ContractoServicio.Parametro.GetParametros.Respuesta Implements ContractoServicio.IParametro.GetParametros
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.GetParametros(peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por listar los datos de todos los Parametros .
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 19/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetParametroDetail(peticion As ContractoServicio.Parametro.GetParametroDetail.Peticion) As ContractoServicio.Parametro.GetParametroDetail.Respuesta Implements ContractoServicio.IParametro.GetParametroDetail
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.GetParametroDetail(peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por listar los datos de todos los Parametros .
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 19/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetParametroOpciones(peticion As ContractoServicio.Parametro.GetParametroOpciones.Peticion) As ContractoServicio.Parametro.GetParametroOpciones.Respuesta Implements ContractoServicio.IParametro.GetParametroOpciones
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.GetParametroOpciones(peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por listar los datos de todos los Parametros .
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 19/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function SetParametro(peticion As ContractoServicio.Parametro.SetParametro.Peticion) As ContractoServicio.Parametro.SetParametro.Respuesta Implements ContractoServicio.IParametro.SetParametro
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.SetParametro(peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por listar los datos de todos los Parametros .
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 19/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetParametrosValues(peticion As ContractoServicio.Parametro.GetParametrosValues.Peticion) As ContractoServicio.Parametro.GetParametrosValues.Respuesta Implements ContractoServicio.IParametro.GetParametrosValues
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.GetParametrosValues(peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por listar los datos de todos los Parametros .
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 19/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function SetParametrosValues(peticion As ContractoServicio.Parametro.SetParametrosValues.Peticion) As ContractoServicio.Parametro.SetParametrosValues.Respuesta Implements ContractoServicio.IParametro.SetParametrosValues
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.SetParametrosValues(peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por listar los datos de todos los Parametros .
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 25/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetAgrupaciones(peticion As ContractoServicio.Parametro.GetAgrupaciones.Peticion) As ContractoServicio.Parametro.GetAgrupaciones.Respuesta Implements ContractoServicio.IParametro.GetAgrupaciones
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.GetAgrupaciones(peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por listar los datos de todos los Parametros .
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 25/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetAgrupacionDetail(peticion As ContractoServicio.Parametro.GetAgrupacionDetail.Peticion) As ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta Implements ContractoServicio.IParametro.GetAgrupacionDetail
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.GetAgrupacionDetail(peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por listar los datos de todos los Parametros .
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 25/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function SetAgrupacion(peticion As ContractoServicio.Parametro.SetAgrupacion.Peticion) As ContractoServicio.Parametro.SetAgrupacion.Respuesta Implements ContractoServicio.IParametro.SetAgrupacion
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.SetAgrupacion(peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por bajar una agrupacion .
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 26/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function BajarAgrupacion(peticion As ContractoServicio.Parametro.BajarAgrupacion.Peticion) As ContractoServicio.Parametro.BajarAgrupacion.Respuesta Implements ContractoServicio.IParametro.BajarAgrupacion
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.BajarAgrupacion(peticion)
        End Function

        ''' <summary>
        ''' Responsable por verificar si ya existe lo código de la opción en la base de datos.
        ''' </summary>
        ''' <param name="peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function VerificarCodigoParametroOpcion(peticion As ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Peticion, codParametro As String, codAplicacion As String) As ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Respuesta Implements ContractoServicio.IParametro.VerificarCodigoParametroOpcion
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.VerificarCodigoParametroOpcion(peticion, codParametro, codAplicacion)
        End Function

        ''' <summary>
        ''' Responsable por verificar si ya existe la descripción de la opción en la base de datos.
        ''' </summary>
        ''' <param name="peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function VerificarDescricaoParametroOpcion(peticion As ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Peticion, codParametro As String, codAplicacion As String) As ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Respuesta Implements ContractoServicio.IParametro.VerificarDescricaoParametroOpcion
            Dim objAccionTermino As New LogicaNegocio.AccionParametro
            Return objAccionTermino.VerificarDescricaoParametroOpcion(peticion, codParametro, codAplicacion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IParametro.Test
            Dim objAccion As New LogicaNegocio.AccionParametro
            Return objAccion.Test()
        End Function

    End Class

End Namespace