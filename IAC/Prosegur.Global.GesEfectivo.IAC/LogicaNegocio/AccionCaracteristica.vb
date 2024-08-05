Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DBHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Text
Imports Prosegur.Genesis

Public Class AccionCaracteristica
    Implements ContractoServicio.ICaracteristica

    ''' <summary>
    ''' Operación para obtener las características de los tipos  de procesado existentes. En el mensaje de entrada se recibe los parámetros por los que se quiere filtrar. En caso de no recibir ningún parámetro de entrada se devolverán todos los registros de la tabla GEPR_TCARACTERISTICA
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </history>
    Public Function GetCaracteristica(objPeticion As ContractoServicio.Caracteristica.GetCaracteristica.Peticion) As ContractoServicio.Caracteristica.GetCaracteristica.Respuesta Implements ContractoServicio.ICaracteristica.GetCaracteristica
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Caracteristica.GetCaracteristica.Respuesta

        Try

            ' obter terminos
            objRespuesta.Caracteristicas = AccesoDatos.Caracteristica.GetCaracteristica(objPeticion)

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
    ''' Operación para dar de alta, modificar y dar de baja lógica (modificando el estado de vigencia) características de tipos de procesado.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 18/05/2009 Criado
    ''' </history>
    Public Function SetCaracteristica(objPeticion As ContractoServicio.Caracteristica.SetCaracteristica.Peticion) As ContractoServicio.Caracteristica.SetCaracteristica.Respuesta Implements ContractoServicio.ICaracteristica.SetCaracteristica
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Caracteristica.SetCaracteristica.Respuesta
        objRespuesta.Caracteristicas = New ContractoServicio.Caracteristica.SetCaracteristica.CaracteristicaRespuestaColeccion()

        Dim objCaracteristica As ContractoServicio.Caracteristica.SetCaracteristica.CaracteristicaRespuesta = Nothing

        objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
        objRespuesta.MensajeError = String.Empty

        'Valida se o código do usuário foi informado.
        If String.IsNullOrEmpty(objPeticion.CodigoUsuario) Then
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = Traduzir("020_msg_codigoUsuario")
        Else

            For Each caracteristica As ContractoServicio.Caracteristica.SetCaracteristica.Caracteristica In objPeticion.Caracteristicas

                Try

                    objCaracteristica = New ContractoServicio.Caracteristica.SetCaracteristica.CaracteristicaRespuesta

                    ' verificar se codigo foi enviado. Ele é obrigatório para qualquer operação.
                    If String.IsNullOrEmpty(caracteristica.Codigo) Then

                        objCaracteristica.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        objCaracteristica.MensajeError = Traduzir("020_msg_codigoCaracteristica")

                    Else

                        'Seta os dados de retorno
                        objCaracteristica.Codigo = caracteristica.Codigo

                        'Valida a ação
                        Dim objResp = AccesoDatos.Caracteristica.GetCaracteristica(caracteristica.Codigo)

                        If objResp.Count = 1 Then

                            'Verifica se é baixa
                            If caracteristica.Vigente IsNot Nothing AndAlso caracteristica.Vigente = False Then
                                'Verifica se a caracteristica está sendo usanda.
                                If AccesoDatos.Caracteristica.VerificaCaracteristicaEmUso(caracteristica.Codigo) Then
                                    objCaracteristica.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                                    objCaracteristica.MensajeError = Traduzir("020_msg_CaracteristicaEnUso")
                                    Continue For
                                End If
                            End If

                            'Seta os dados de retorno
                            If caracteristica.Descripcion Is Nothing Then
                                objCaracteristica.Descripcion = objResp(0).Descripcion
                            Else
                                objCaracteristica.Descripcion = caracteristica.Descripcion
                            End If

                            'Actializar
                            AccesoDatos.Caracteristica.ActualizarCaracteristica(caracteristica, objPeticion.CodigoUsuario)

                        Else 'Alta

                            'Seta os dados de retorno
                            objCaracteristica.Descripcion = caracteristica.Descripcion

                            'Valida os campos obrigatórios na inclusão
                            If String.IsNullOrEmpty(caracteristica.CodigoConteo) Then
                                objCaracteristica.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                                objCaracteristica.MensajeError = Traduzir("020_msg_codigoConteoCaracteristica")
                            End If
                            If String.IsNullOrEmpty(caracteristica.Descripcion) Then
                                objCaracteristica.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                                If objCaracteristica.MensajeError <> String.Empty Then
                                    objCaracteristica.MensajeError &= Environment.NewLine
                                End If
                                objCaracteristica.MensajeError &= Traduzir("020_msg_descripcionCaracteristica")
                            End If

                            'Realiza a inclusão se não houveram erros.
                            If objCaracteristica.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                                AccesoDatos.Caracteristica.AltaCaracteristica(caracteristica, objPeticion.CodigoUsuario)
                            End If
                        End If
                    End If

                    objCaracteristica.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objCaracteristica.MensajeError = String.Empty

                Catch ex As Excepcion.NegocioExcepcion

                    'Caso ocorra alguma exceção, trata da forma adequada
                    objCaracteristica.CodigoError = ex.Codigo
                    objCaracteristica.MensajeError = ex.Descricao

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    'Caso ocorra alguma exceção, trata da forma adequada            
                    objCaracteristica.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                    objCaracteristica.MensajeError = ex.ToString()
                    objCaracteristica.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
                Finally

                    objRespuesta.Caracteristicas.Add(objCaracteristica)

                End Try

            Next

        End If

        Return objRespuesta

    End Function

    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ICaracteristica.Test
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