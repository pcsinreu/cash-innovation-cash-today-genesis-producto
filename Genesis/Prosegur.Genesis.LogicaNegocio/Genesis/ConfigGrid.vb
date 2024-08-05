Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Transactions

Namespace Genesis
    Public Class ConfigGrid
        Public Shared Function GuardarConfigGrid(Peticion As ContractoServicio.Contractos.Genesis.ConfigGrid.GuardarConfigGrid.Peticion) As ContractoServicio.Contractos.Genesis.ConfigGrid.GuardarConfigGrid.Respuesta

            Dim Respuesta As ContractoServicio.Contractos.Genesis.ConfigGrid.GuardarConfigGrid.Respuesta = Nothing

            Try

                ' Define que está numa transação
                Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted})

                    Respuesta = New ContractoServicio.Contractos.Genesis.ConfigGrid.GuardarConfigGrid.Respuesta

                    AccesoDatos.Genesis.ConfigGrid.GuardarConfigGrid(Peticion.CodigoUsuario, Peticion.CodigoFuncionalidade, Peticion.ConfigGrid)

                    'se não deu erro então realiza a transação.
                    transaction.Complete()

                End Using

            Catch ex As Excepcion.NegocioExcepcion
                Respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Respuesta.Excepciones.Add(ex.Message)
            End Try

            Return Respuesta

        End Function

        Public Shared Function RecuperarConfigGrid(Peticion As ContractoServicio.Contractos.Genesis.ConfigGrid.RecuperarConfigGrid.Peticion) As ContractoServicio.Contractos.Genesis.ConfigGrid.RecuperarConfigGrid.Respuesta

            Dim Respuesta As New ContractoServicio.Contractos.Genesis.ConfigGrid.RecuperarConfigGrid.Respuesta()

            Try

                Respuesta = New ContractoServicio.Contractos.Genesis.ConfigGrid.RecuperarConfigGrid.Respuesta With {.ConfigGrids = AccesoDatos.Genesis.ConfigGrid.RecuperarConfigGrid(Peticion.CodigoUsuario, Peticion.CodigoFuncionalidade)}

            Catch ex As Excepcion.NegocioExcepcion
                Respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Respuesta.Excepciones.Add(ex.Message)
            End Try

            Return Respuesta

        End Function
    End Class

End Namespace

