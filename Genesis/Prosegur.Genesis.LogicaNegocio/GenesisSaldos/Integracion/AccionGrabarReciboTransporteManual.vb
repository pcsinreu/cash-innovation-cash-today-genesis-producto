Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.DataBaseHelper

Namespace Integracion

    Public Class AccionGrabarReciboTransporteManual

        Public Shared Function Ejecutar(Peticion As GrabarReciboTransporteManual.Peticion) As GrabarReciboTransporteManual.Respuesta

            Dim respuesta As New GrabarReciboTransporteManual.Respuesta

            Try

                ' valida campos obrigatorios
                ValidarGrabarReciboTransporteManual(Peticion)
                respuesta.RecibosRepetidos = AccesoDatos.Integracion.GrabarReciboTransporteManual.Grabar(Peticion.Remesas)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Message)

            Catch ex As Exception

                If ex.InnerException IsNot Nothing AndAlso ex.InnerException.InnerException IsNot Nothing AndAlso ex.InnerException.InnerException.InnerException IsNot Nothing Then
                    respuesta.Excepciones.Add(ex.InnerException.InnerException.InnerException.Message)
                ElseIf ex.InnerException IsNot Nothing AndAlso ex.InnerException.InnerException IsNot Nothing Then
                    respuesta.Excepciones.Add(ex.InnerException.InnerException.Message)
                ElseIf ex.InnerException IsNot Nothing Then
                    respuesta.Excepciones.Add(ex.InnerException.Message)
                Else
                    respuesta.Excepciones.Add(ex.Message)
                End If

            End Try

            Return respuesta

        End Function

        Private Shared Sub ValidarGrabarReciboTransporteManual(Peticion As GrabarReciboTransporteManual.Peticion)

            If Peticion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Petición"))
            End If

            If Peticion.Remesas Is Nothing OrElse Peticion.Remesas.Count = 0 Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Remesas"))
            End If

            If Peticion.Remesas.Exists(Function(r) String.IsNullOrEmpty(r.Identificador)) Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Identificador"))
            End If

            If Peticion.Remesas.Exists(Function(r) String.IsNullOrEmpty(r.CodigoReciboSalida)) Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "CodigoReciboTransporte"))
            End If

            If Peticion.Remesas.Exists(Function(r) String.IsNullOrEmpty(r.UsuarioModificacion)) Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Usuario"))
            End If

            If Peticion.Remesas.Exists(Function(r) String.IsNullOrEmpty(r.FechaHoraModificacion)) Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Fecha Actualización"))
            End If

        End Sub

    End Class

End Namespace
