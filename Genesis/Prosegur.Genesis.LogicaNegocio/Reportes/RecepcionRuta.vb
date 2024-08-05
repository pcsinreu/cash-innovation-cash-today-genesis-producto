Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Interfaces
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Transactions

Namespace Reportes
    Public Class RecepcionRuta

        Public Shared Function GrabarRecepcionRuta(objPeticion As Contractos.Reportes.GrabarRecepcionRuta.GrabarRecepcionRutaPeticion) As Contractos.Reportes.GrabarRecepcionRuta.GrabarRecepcionRutaRespuesta

            Dim respuesta As New Contractos.Reportes.GrabarRecepcionRuta.GrabarRecepcionRutaRespuesta

            Try
                'Verifica se foi informado a recpecionRuta
                If objPeticion Is Nothing OrElse objPeticion.RecepcionesRuta Is Nothing OrElse objPeticion.RecepcionesRuta.Count = 0 Then
                    'Ruta não informada
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("RECEPCION_RUTA_NAO_INFORMADA"))
                End If

                Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted})

                    For Each objRecepcionesRuta In objPeticion.RecepcionesRuta
                        AccesoDatos.Reportes.RecepcionRuta.GrabarRecepcionRuta(objRecepcionesRuta)
                    Next

                    ' se não deu erro então realiza a transação.
                    transaction.Complete()
                End Using

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

