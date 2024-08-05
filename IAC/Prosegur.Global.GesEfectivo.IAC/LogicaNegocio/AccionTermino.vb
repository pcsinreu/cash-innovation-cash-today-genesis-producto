Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DBHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Classe AccionTermino
''' </summary>
''' <remarks></remarks>
''' <history>
''' [pda] 12/02/2009 Criado
''' </history>
Public Class AccionTermino
    Implements ContractoServicio.ITermino

    ''' <summary>
    ''' Obtém os términos através do codigo, descrição termino, descrição formato, mostrar código e vigente. Caso nenhum seja informado, todos registros serão retornados.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    Public Function getTerminos(Peticion As ContractoServicio.TerminoIac.GetTerminoIac.Peticion) As ContractoServicio.TerminoIac.GetTerminoIac.Respuesta Implements ContractoServicio.ITermino.getTerminos
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.TerminoIac.GetTerminoIac.Respuesta

        Try

            ' obter divisas
            objRespuesta.TerminosIac = AccesoDatos.Termino.GetTerminos(Peticion)

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getTerminoDetail(Peticion As ContractoServicio.TerminoIac.GetTerminoDetailIac.Peticion) As ContractoServicio.TerminoIac.GetTerminoDetailIac.Respuesta Implements ContractoServicio.ITermino.getTerminoDetail
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.TerminoIac.GetTerminoDetailIac.Respuesta

        Try
            ' obter divisas
            objRespuesta.TerminosDetailIac = AccesoDatos.Termino.GetTerminoDetail(Peticion)

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Efetua inserção, alteração ou baixa no banco dos objetos recebidos
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/01/2009 Criado
    ''' [octavio.piramo] 10/03/2009 Alterado
    ''' </history>
    Public Function setTermino(Peticion As ContractoServicio.TerminoIac.SetTerminoIac.Peticion) As ContractoServicio.TerminoIac.SetTerminoIac.Respuesta Implements ContractoServicio.ITermino.setTermino

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.TerminoIac.SetTerminoIac.Respuesta

        objRespuesta.CodigoTermino = Peticion.Codigo
        objRespuesta.DescricaoTermino = Peticion.Descripcion

        Try

            ' verificar se codigo do termino foi enviado
            If String.IsNullOrEmpty(Peticion.Codigo) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("010_msg_TerminoCodigoVazio"))
            End If

            ' verificar se a descrição do termino foi enviada
            If String.IsNullOrEmpty(Peticion.Descripcion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("010_msg_TerminoDescripcionVazio"))
            End If

            Dim objPeticionVerificaCodigo As New ContractoServicio.TerminoIac.VerificarCodigoTermino.Peticion
            objPeticionVerificaCodigo.Codigo = Peticion.Codigo

            ' verifica se a divisa já existe
            If AccesoDatos.Termino.VerificarCodigoTermino(objPeticionVerificaCodigo) Then

                ' se o termino não for vigente e se tem alguma entidade relacionada
                If Not Peticion.Vigente AndAlso AccesoDatos.Termino.VerificarEntidadesVigentesComTermino(Peticion.Codigo) Then
                    ' lançar erro avisando que existe entidades vigentes que utilizam a termino
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("010_msg_TerminoComEntidadeVigente"))
                Else
                    ' efetuar alteração do termino 
                    AccesoDatos.Termino.ActualizarTermino(Peticion)
                End If

            Else

                ' verificar se o codigo do termino foi enviado
                If String.IsNullOrEmpty(Peticion.CodigoFormato) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("010_msg_CodigoFormatoVazio"))
                End If

                ' verificar se a longitude foi enviada
                If Peticion.CodigoFormato.Equals(ContractoServicio.Constantes.COD_FORMATO_TEXTO) OrElse _
                       Peticion.CodigoFormato.Equals(ContractoServicio.Constantes.COD_FORMATO_DECIMAL) OrElse _
                       Peticion.CodigoFormato.Equals(ContractoServicio.Constantes.COD_FORMATO_ENTERO) Then

                    If Peticion.Longitud < 1 OrElse Peticion.Longitud Is Nothing Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("010_msg_LongitudeVazia"))
                    End If

                End If

                ' efetuar inserção do termino
                AccesoDatos.Termino.AltaTermino(Peticion)

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
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Verifica se o código do termino existe
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/01/2009 Criado
    ''' </history>
    Public Function VerificarCodigoTermino(Peticion As ContractoServicio.TerminoIac.VerificarCodigoTermino.Peticion) As ContractoServicio.TerminoIac.VerificarCodigoTermino.Respuesta Implements ContractoServicio.ITermino.VerificarCodigoTermino
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.TerminoIac.VerificarCodigoTermino.Respuesta

        Try

            ' executar verificação no banco
            objRespuesta.Existe = AccesoDatos.Termino.VerificarCodigoTermino(Peticion)

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Verifica se a descrição do termino existe
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/01/2009 Criado
    ''' </history>
    Public Function VerificarDescripcionTermino(Peticion As ContractoServicio.TerminoIac.VerificarDescripcionTermino.Peticion) As ContractoServicio.TerminoIac.VerificarDescripcionTermino.Respuesta Implements ContractoServicio.ITermino.VerificarDescripcionTermino
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.TerminoIac.VerificarDescripcionTermino.Respuesta

        Try

            ' executar verificação no banco
            objRespuesta.Existe = AccesoDatos.Termino.VerificarDescricaoTermino(Peticion)

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
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
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITermino.Test
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