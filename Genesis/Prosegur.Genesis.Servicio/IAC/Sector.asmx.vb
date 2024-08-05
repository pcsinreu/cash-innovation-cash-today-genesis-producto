Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

Namespace IAC.Servicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Sector
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ISetor

        ''' <summary>
        ''' Obtem todos os setores
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 08/03/2013 Criado
        ''' </history>
        <WebMethod()> _
        Public Function getSectores(objPeticion As ContractoServicio.Setor.GetSectores.Peticion) As ContractoServicio.Setor.GetSectores.Respuesta Implements ContractoServicio.ISetor.getSectores
            Dim objAccionSetor As New LogicaNegocio.AccionSetor
            Return objAccionSetor.getSectores(objPeticion)

        End Function

        ''' <summary>
        ''' Métodos resposável por efetuar a gravação dos dados.
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [poncalves] 08/03/2013 Criado
        ''' </history>
        <WebMethod()> _
        Public Function setSectores(ObjPeticion As ContractoServicio.Setor.SetSectores.Peticion) As ContractoServicio.Setor.SetSectores.Respuesta Implements ContractoServicio.ISetor.setSectores
            Dim objAccionSetor As New LogicaNegocio.AccionSetor
            Return objAccionSetor.setSectores(ObjPeticion)
        End Function

        ''' <summary>
        ''' Obtem todos os setores
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 08/03/2013 Criado
        ''' </history>
        <WebMethod()> _
        Public Function getSetorDetail(objPeticion As ContractoServicio.Setor.GetSectoresDetail.Peticion) As ContractoServicio.Setor.GetSectoresDetail.Respuesta Implements ContractoServicio.ISetor.getSetorDetail
            Dim objAccionSetor As New LogicaNegocio.AccionSetor
            Return objAccionSetor.getSetorDetail(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ISetor.Test
            ' criar objeto
            Dim objAccionSetor As New LogicaNegocio.AccionSetor
            Return objAccionSetor.Test()
        End Function

        <WebMethod()> _
        Public Function GetSectoresIAC(objPeticion As ContractoServicio.Sector.GetSectoresIAC.Peticion) As ContractoServicio.Sector.GetSectoresIAC.Respuesta Implements ContractoServicio.ISetor.GetSectoresIAC
            Dim objAccionSetor As New LogicaNegocio.AccionSetor
            Return objAccionSetor.GetSectoresIAC(objPeticion)
        End Function

        <WebMethod()> _
        Public Function getSectoresTesoro(objPeticion As ContractoServicio.Sector.GetSectoresTesoro.Peticion) As ContractoServicio.Sector.GetSectoresTesoro.Respuesta Implements ContractoServicio.ISetor.GetSectoresTesoro
            Dim objAccionSetor As New LogicaNegocio.AccionSetor
            Return objAccionSetor.GetSectoresTesoro(objPeticion)
        End Function

    End Class
End Namespace