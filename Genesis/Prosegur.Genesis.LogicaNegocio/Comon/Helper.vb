Imports Prosegur.Genesis.Comon

''' <summary>
''' Clase EmisorDocumento
''' </summary>
''' <remarks></remarks>
''' <history>
''' [maoliveira] 04/10/2013 - Criado
''' </history>
Public Class Helper

    Public Shared Function Busqueda(Peticion As ContractoServicio.Helper.Busqueda.HelperBusquedaPeticion) As ContractoServicio.Helper.Busqueda.HelperBusquedaRespuesta

        Dim objRespuesta As New ContractoServicio.Helper.Busqueda.HelperBusquedaRespuesta

        Try
            ' Recupera os dados
            Dim respuesta As RespuestaHelper = LogicaNegocio.Classes.Helper.Busqueda(Peticion)
            ' Atualiza para a resposta do serviço
            objRespuesta = New ContractoServicio.Helper.Busqueda.HelperBusquedaRespuesta With {.DatosRespuesta = respuesta.DatosRespuesta}

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        Return objRespuesta

    End Function

    Public Shared Function BusquedaTipoContenedor(Peticion As ContractoServicio.Helper.BusquedaTipoContenedor.HelperBusquedaTipoContenedorPeticion) As ContractoServicio.Helper.BusquedaTipoContenedor.HelperBusquedaTipoContenedorRespuesta

        Dim objRespuesta As New ContractoServicio.Helper.BusquedaTipoContenedor.HelperBusquedaTipoContenedorRespuesta

        Try
            ' Recupera os dados
            Dim respuesta As RespuestaHelperTipoContenedor = LogicaNegocio.Classes.Helper.BusquedaTipoContenedor(Peticion)
            ' Atualiza para a resposta do serviço
            objRespuesta = New ContractoServicio.Helper.BusquedaTipoContenedor.HelperBusquedaTipoContenedorRespuesta With {.DatosRespuesta = respuesta.DatosRespuesta}

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        Return objRespuesta

    End Function

End Class