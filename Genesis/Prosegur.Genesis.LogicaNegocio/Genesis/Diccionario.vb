Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Configuration

Namespace Genesis
    Public Class Diccionario

        Public Shared Function ObtenerValorDicionarioSimples(Cultura As String, CodigoFuncionalidad As String, CodigoExpresion As String) As String
            Return AccesoDatos.Genesis.Diccionario.ObtenerValorDicionarioSimples(Cultura, CodigoFuncionalidad, CodigoExpresion)
        End Function

        Public Shared Function ObtenerValorDicionario(peticion As ContractoServicio.Contractos.Comon.Diccionario.ObtenerValorDiccionario.Peticion) As ContractoServicio.Contractos.Comon.Diccionario.ObtenerValorDiccionario.Respuesta

            Dim respuesta As New ContractoServicio.Contractos.Comon.Diccionario.ObtenerValorDiccionario.Respuesta

            Try
                Dim dt As DataTable = AccesoDatos.Genesis.Diccionario.ObtenerValorDicionario(peticion.Cultura, peticion.CodigoFuncionalidad, peticion.CodigoExpresion)


                If dt IsNot Nothing AndAlso dt.Rows.Count = 1 Then
                    respuesta.Valor = dt(0)("VALOR")
                End If

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.CodigoError = ex.Codigo
                respuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.MensajeError = ex.ToString()
            End Try

            Return respuesta

        End Function

        Public Shared Function ObtenerValoresDicionario(peticion As ContractoServicio.Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Peticion) As ContractoServicio.Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Respuesta

            Dim respuesta As New ContractoServicio.Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Respuesta
            respuesta.Valores = New Prosegur.Genesis.Comon.SerializableDictionary(Of String, String)

            Try
                Dim dt As DataTable = AccesoDatos.Genesis.Diccionario.ObtenerValorDicionario(peticion.Cultura, peticion.CodigoFuncionalidad)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If Not respuesta.Valores.ContainsKey(dr("COD_EXPRESION")) Then
                            respuesta.Valores.Add(dr("COD_EXPRESION"), dr("VALOR"))
                        End If
                    Next
                End If
            Catch ex As Excepcion.NegocioExcepcion
                respuesta.CodigoError = ex.Codigo
                respuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.MensajeError = ex.ToString()
            End Try

            Return respuesta

        End Function

    End Class
End Namespace
