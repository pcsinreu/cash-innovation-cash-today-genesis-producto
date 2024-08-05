Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionValorPosible
    Implements ContractoServicio.IValorPosible

    ''' <summary>
    ''' Obtém os términos e os valores posibles
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Public Function GetValoresPosibles(objPeticion As ContractoServicio.ValorPosible.GetValoresPosibles.Peticion) As ContractoServicio.ValorPosible.GetValoresPosibles.Respuesta Implements ContractoServicio.IValorPosible.GetValoresPosibles

        Dim objRespuesta As New ContractoServicio.ValorPosible.GetValoresPosibles.Respuesta

        Try

            ' obter os terminos
            objRespuesta.Terminos = AccesoDatos.ValorPosible.GetValoresPosibles(objPeticion)

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
    ''' Grava valores possibles para cliente e termino.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Public Function SetValoresPosibles(objPeticion As ContractoServicio.ValorPosible.SetValoresPosibles.Peticion) As ContractoServicio.ValorPosible.SetValoresPosibles.Respuesta Implements ContractoServicio.IValorPosible.SetValoresPosibles

        Dim objRespuesta As New ContractoServicio.ValorPosible.SetValoresPosibles.Respuesta

        Try

            ' caso termino e valores posibles sejam informados
            If objPeticion.Termino IsNot Nothing _
                AndAlso objPeticion.Termino.ValoresPosibles IsNot Nothing Then

                ' para cada valor posible
                For Each objValorPosible As ContractoServicio.ValorPosible.ValorPosible In objPeticion.Termino.ValoresPosibles

                    ' Variáveis relacionamento valores possívei
                    Dim OidCliente As String = String.Empty
                    Dim OidSubCliente As String = String.Empty
                    Dim OidPuntoServicio As String = String.Empty

                    ' obter oidcliente e oid termino
                    If Not String.IsNullOrEmpty(objPeticion.CodigoCliente) Then
                        OidCliente = AccesoDatos.Cliente.BuscarOidCliente(objPeticion.CodigoCliente)

                        ' obter oid sub cliente para o cliente selecionado
                        If Not String.IsNullOrEmpty(objPeticion.CodigoSubCliente) Then

                            OidSubCliente = AccesoDatos.SubCliente.BuscarOidSubCliente(objPeticion.CodigoSubCliente, OidCliente)

                            'obter oid punto servicio para o subcliente informado
                            If Not String.IsNullOrEmpty(objPeticion.CodigoPuntoServicio) Then

                                OidPuntoServicio = AccesoDatos.PuntoServicio.BuscaOidPuntoServicio(objPeticion.CodigoPuntoServicio, OidSubCliente)

                            End If

                        End If

                    End If

                    Dim OidTermino As String = AccesoDatos.TerminoIac.BuscaOidTermino(objPeticion.Termino.Codigo)

                    ' obter oid valor termino verificando se registro já existe na base
                    Dim OidValorTermino As String = AccesoDatos.ValorPosible.ObterOidValorTermino(OidCliente, OidSubCliente, OidPuntoServicio, OidTermino, objValorPosible.Codigo)

                    ' caso o oid não seja vazio (registro ja existe na base)
                    ' senão deve inserir registro na base
                    If Not OidValorTermino.Equals(String.Empty) Then

                        ' modificar valor posible
                        AccesoDatos.ValorPosible.ModificarValorPosible(objValorPosible, OidValorTermino, objPeticion.CodigoUsuario, OidCliente, OidSubCliente, OidPuntoServicio, OidTermino)

                    Else

                        ' inserir valor posible
                        AccesoDatos.ValorPosible.AltaValorPosible(objValorPosible, objPeticion.CodigoUsuario, OidCliente, OidSubCliente, OidPuntoServicio, OidTermino)

                    End If

                Next

            End If

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            Util.TratarErroBugsnag(ex)
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
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IValorPosible.Test
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