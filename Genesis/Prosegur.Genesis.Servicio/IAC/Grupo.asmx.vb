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
    Public Class Grupo
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IGrupo

        ''' <summary>
        ''' Esta operación es responsable por obtener los datos de todos los ATMs pertenecientes al grupo de la petición.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 13/01/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetATMsbyGrupo(Peticion As ContractoServicio.Grupo.GetATMsbyGrupo.Peticion) As ContractoServicio.Grupo.GetATMsbyGrupo.Respuesta Implements ContractoServicio.IGrupo.GetATMsbyGrupo

            Dim accion As New LogicaNegocio.AccionGrupo
            Return accion.GetATMsbyGrupo(Peticion)

        End Function

        ''' <summary>
        ''' Esta operación es responsable por obtener los datos de los grupos. 
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 13/01/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetGrupos(Peticion As ContractoServicio.Grupo.GetGrupos.Peticion) As ContractoServicio.Grupo.GetGrupos.Respuesta Implements ContractoServicio.IGrupo.GetGrupos

            Dim accion As New LogicaNegocio.AccionGrupo
            Return accion.GetGrupos(Peticion)

        End Function

        ''' <summary>
        ''' Esta operación es responsable por grabar en la base de datos las informaciones del grupo informado en la petición.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 13/01/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function SetGrupo(Peticion As ContractoServicio.Grupo.SetGrupo.Peticion) As ContractoServicio.Grupo.SetGrupo.Respuesta Implements ContractoServicio.IGrupo.SetGrupo

            Dim accion As New LogicaNegocio.AccionGrupo
            Return accion.SetGrupo(Peticion)

        End Function

        ''' <summary>
        ''' Esta operación es responsable por verificar si ya existe grupo con los datos informados en la peticion.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 13/01/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function VerificarGrupo(Peticion As ContractoServicio.Grupo.VerificarGrupo.Peticion) As ContractoServicio.Grupo.VerificarGrupo.Respuesta Implements ContractoServicio.IGrupo.VerificarGrupo

            Dim accion As New LogicaNegocio.AccionGrupo
            Return accion.VerificarGrupo(Peticion)

        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IGrupo.Test
            Dim objAccion As New LogicaNegocio.AccionGrupo
            Return objAccion.Test()
        End Function

    End Class

End Namespace