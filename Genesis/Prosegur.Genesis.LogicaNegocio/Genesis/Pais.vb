Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Genesis

    Public Class Pais

        Public Shared Function ObtenerPaisPorDelegacion(codigoDelegacion As String, Optional identificadorAjeno As String = "") As Clases.Pais

            Try
                Return AccesoDatos.Genesis.Pais.ObtenerPaisPorDelegacion(codigoDelegacion, identificadorAjeno)

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function ObtenerPaisPorCodigo(CodigoPais As String, Optional identificadorAjeno As String = "") As Clases.Pais

            Try
                If String.IsNullOrWhiteSpace(identificadorAjeno) Then
                    Return AccesoDatos.Genesis.Pais.ObtenerPais(CodigoPais)
                Else
                    Return AccesoDatos.Genesis.Pais.ObtenerPaisPorCodigoAjeno(CodigoPais, identificadorAjeno)
                End If


            Catch ex As Exception
                Throw

            End Try

        End Function
        ''' <summary>
        ''' Devuelve el pais indicado por parametro, 
        ''' en caso de no encontrarse devuelve el primer pais que encuentra en la tabla GEPR_TPAIS
        ''' </summary>
        ''' <param name="CodigoPais"></param>
        ''' <returns></returns>
        Public Shared Function ObtenerPaisPorDefault(CodigoPais As String) As Clases.Pais

            Try
                Return AccesoDatos.Genesis.Pais.ObtenerPaisPorDefault(CodigoPais)

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function ObtenerPaises() As ContractoServicio.Login.ObtenerPaises.PaisColeccion
            Try
                Return AccesoDatos.Genesis.Pais.ObtenerPaises()
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Shared Function ObtenerPaisPorDelegacion(Peticion As Contractos.Comon.Pais.ObtenerPaisPorDelegacion.ObtenerPaisPorDelegacionPeticion) As Contractos.Comon.Pais.ObtenerPaisPorDelegacion.ObtenerPaisPorDelegacionRespuesta

            Dim respuesta As New Contractos.Comon.Pais.ObtenerPaisPorDelegacion.ObtenerPaisPorDelegacionRespuesta()
            Try

                respuesta.Pais = AccesoDatos.Genesis.Pais.ObtenerPaisPorDelegacion(Peticion.CodigoDelegacion)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Descricao)

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.Message)

            End Try

            Return respuesta

        End Function

    End Class
End Namespace
