Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DBHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionProducto
    Implements ContractoServicio.IProducto

    ''' <summary>
    '''Função que faz a busca dos produtos.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 27/01/2009 Created
    ''' </history>    
    Public Function GetProductos(peticion As ContractoServicio.Producto.GetProductos.Peticion) As ContractoServicio.Producto.GetProductos.Respuesta Implements ContractoServicio.IProducto.GetProductos

        Dim objRespuesta As New ContractoServicio.Producto.GetProductos.Respuesta

        Try

            objRespuesta.Productos = AccesoDatos.Producto.GetProductos(peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/02/2010 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IProducto.Test
        Dim objRespuesta As New ContractoServicio.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("021_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function
End Class
