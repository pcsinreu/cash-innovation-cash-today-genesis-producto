Imports Prosegur.DbHelper
Imports Prosegur.Genesis

Public Class AccionInventario
    Implements Prosegur.Genesis.ContractoServicio.Interfaces.IInventario


    Public Function RecuperarInventarios(objPeticion As ContractoServicio.RecuperarInventariosPeticion) As ContractoServicio.RecuperarInventariosRespuesta Implements ContractoServicio.Interfaces.IInventario.RecuperarInventarios
        Dim objRespuesta As New Genesis.ContractoServicio.RecuperarInventariosRespuesta

        Try
            objRespuesta = AccesoDatos.Inventario.RecuperarInventarios(objPeticion)
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
