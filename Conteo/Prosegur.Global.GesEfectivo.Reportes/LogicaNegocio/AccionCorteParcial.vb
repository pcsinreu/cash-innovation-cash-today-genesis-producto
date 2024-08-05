Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionCorteParcial
    Implements ContractoServ.ICorteParcial

    ''' <summary>
    ''' Lista os cortes parciais de acordo com os filtros passados como parâmetro
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros que deverão ser passados como parâmetro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.olivera] 28/07/2009 Criado
    ''' </history>
    Public Function ListarCorteParcial(objPeticion As ContractoServ.CorteParcial.GetCortesParciais.Peticion) As ContractoServ.CorteParcial.GetCortesParciais.Respuesta Implements ContractoServ.ICorteParcial.ListarCorteParcial

        Dim objRespuesta As New ContractoServ.CorteParcial.GetCortesParciais.Respuesta

        Try

            ' Verifica o tipo do formato de saída do relatório
            If (objPeticion.FormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV) Then
                ' Recupera os dados para o relatório no formato CSV
                objRespuesta.CortesParciaisCSV = AccesoDatos.CorteParcial.ListarCorteParcialCSV(objPeticion)
            Else
                ' Recupera os dados para o relatório no formato PDF
                objRespuesta.CorteParcialPDF = AccesoDatos.CorteParcial.ListarCorteParcialPDF(objPeticion)
            End If

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

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 08/02/2010 - Criado
    ''' </history>
    Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.ICorteParcial.Test
        Dim objRespuesta As New ContractoServ.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("001_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString

        End Try

        Return objRespuesta
    End Function
End Class