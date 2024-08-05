Imports ContractoLogin = Prosegur.Genesis.ContractoServicio.GenesisLogin
Imports ContractosSeguridad = Prosegur.Global.Seguridad.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.LogicaNegocio

Namespace AccionGenesisLogin

    Public Class ConsumirTokenAcceso

        ''' <summary>
        ''' Flujo principal de la operación
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Ejecutar(Peticion As ContractoLogin.ConsumirTokenAcceso.Peticion) As ContractoLogin.ConsumirTokenAcceso.Respuesta

            Dim respuesta As New ContractoLogin.ConsumirTokenAcceso.Respuesta

            Try

                ValidarPeticion(Peticion)

                ' TO DO

            Catch ex As Excepcion.NegocioExcepcion

                respuesta.CodigoError = ex.Codigo
                respuesta.MensajeError = ex.Descricao
                'respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.Error

            Catch ex As Exception

                Util.TratarErroBugsnag(ex)
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.MensajeError = ex.ToString()
                'respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.Error

            End Try

            Return respuesta

        End Function

        ''' <summary>
        ''' Validar los parámetros de entrada
        ''' </summary>
        Private Shared Sub ValidarPeticion(Peticion As ContractoLogin.ConsumirTokenAcceso.Peticion)

        End Sub

        ''' <summary>
        ''' Comunicación con el servicio de Seguridad
        ''' </summary>
        ''' <param name="peticion"></param>
        Private Shared Function llamarSeguridad(peticion As ContractosSeguridad.ObtenerTokenAcceso.Peticion) As ContractosSeguridad.ObtenerTokenAcceso.Respuesta

            Dim proxySeguridad As New Proxy.Seguridad()
            Return proxySeguridad.ObtenerTokenAcceso(peticion)

        End Function

    End Class

End Namespace

