Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel

Namespace Genesis

    Public Class Puesto


        Public Shared Function ObtenerPuestos(Peticion As ContractoServicio.Contractos.Genesis.Puesto.ObtenerPuestos.Peticion) As ContractoServicio.Contractos.Genesis.Puesto.ObtenerPuestos.Respuesta

            Dim respuesta As ContractoServicio.Contractos.Genesis.Puesto.ObtenerPuestos.Respuesta = Nothing

            Try

                If Peticion IsNot Nothing Then

                    ' Recupera as situações da remessa
                    respuesta = New ContractoServicio.Contractos.Genesis.Puesto.ObtenerPuestos.Respuesta With {.Puestos = AccesoDatos.Genesis.Puesto.ObtenerPuestos(Peticion.CodigoDelegacion, Peticion.BolVigente, Peticion.Aplicacion.RecuperarValor)}

                End If

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