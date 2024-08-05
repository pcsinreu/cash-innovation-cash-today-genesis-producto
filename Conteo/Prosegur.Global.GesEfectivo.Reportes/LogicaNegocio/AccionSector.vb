Imports Prosegur.DbHelper
Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class AccionSector
    Implements Prosegur.Genesis.ContractoServicio.Interfaces.ISector

    Public Function RecuperarSectorPorDelegacion(objPeticion As Genesis.ContractoServicio.RecuperarSectorPorDelegacionPeticion) As Genesis.ContractoServicio.RecuperarSectorPorDelegacionRespuesta Implements Genesis.ContractoServicio.Interfaces.ISector.RecuperarSectorPorDelegacion
        Dim objRespuesta As New Genesis.ContractoServicio.RecuperarSectorPorDelegacionRespuesta

        Try
            objRespuesta = AccesoDatos.Sector.RecuperarSectorPorDelegacion(objPeticion)
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Codigo)
            objRespuesta.Mensajes.Add(ex.Message)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT)
            objRespuesta.Mensajes.Add(ex.Message)

        End Try

        Return objRespuesta
    End Function
End Class
