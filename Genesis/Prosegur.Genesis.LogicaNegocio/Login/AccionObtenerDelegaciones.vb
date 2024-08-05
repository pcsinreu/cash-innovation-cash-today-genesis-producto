Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global
Imports Prosegur.Genesis.ContractoServicio.Login
Imports System.Xml.Serialization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class AccionObtenerDelegaciones

    Shared Function Ejecutar(Peticion As ObtenerDelegaciones.Peticion) As ObtenerDelegaciones.Respuesta


        Dim respuesta As New Login.ObtenerDelegaciones.Respuesta
        Dim objRespuesta As Seguridad.ContractoServicio.ObtenerDelegaciones.Respuesta

        Try
            objRespuesta = ObtenerDelegaciones(Peticion)

            If objRespuesta.Codigo > 0 Then
                If objRespuesta.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
                    respuesta.MensajeError = objRespuesta.Descripcion
                    respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                ElseIf objRespuesta.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    respuesta.MensajeError = objRespuesta.Descripcion
                    respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                Else
                    respuesta.MensajeError = objRespuesta.Descripcion
                    respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                End If
            Else

                respuesta.Delegaciones = New ObtenerDelegaciones.DelegacionColeccion

                For Each delegacionSeguridad In objRespuesta.Delegaciones.OrderBy(Function(x) x.Nombre)
                    Dim delegacionServicio As New ObtenerDelegaciones.Delegacion
                    delegacionServicio.CodigoDelegacion = delegacionSeguridad.Codigo
                    delegacionServicio.NombreDelegacion = delegacionSeguridad.Nombre
                    delegacionServicio.Plantas = New ObtenerDelegaciones.PlantaColeccion

                    For Each itemPlanta In delegacionSeguridad.Plantas.OrderBy(Function(x) x.DesPlanta)

                        Dim objPlanta As New ObtenerDelegaciones.Planta()

                        objPlanta.oidPlanta = itemPlanta.oidPlanta
                        objPlanta.CodigoPlanta = itemPlanta.CodigoPlanta
                        objPlanta.DesPlanta = itemPlanta.DesPlanta

                        delegacionServicio.Plantas.Add(objPlanta)

                    Next

                    respuesta.Delegaciones.Add(delegacionServicio)
                Next

                Return respuesta

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

    Public Shared Function CopyProperties(Of TSource, TDestination)(source As TSource) As TDestination

        Using sw As New MemoryStream()

            Dim serializador As New XmlSerializer(GetType(TSource))
            serializador.Serialize(sw, source)

            sw.Seek(0, SeekOrigin.Begin)

            Dim deserializador As New XmlSerializer(GetType(TDestination))
            Dim obj As TDestination = deserializador.Deserialize(sw)

            sw.Close()

            Return obj

        End Using

    End Function

    Public Shared Function ObtenerDelegaciones(Peticion As Login.ObtenerDelegaciones.Peticion) As Seguridad.ContractoServicio.ObtenerDelegaciones.Respuesta

        Dim objPeticion As New Seguridad.ContractoServicio.ObtenerDelegaciones.Peticion()
        Dim objProxyLogin As New LoginGlobal.Seguridad()

        objPeticion.CodigoPais = Peticion.CodigoPais
        objPeticion.NombreContinente = Peticion.NombreContinente
        objPeticion.NombreZona = Peticion.NombreZona

        Return objProxyLogin.ObtenerDelegaciones(objPeticion)

    End Function


End Class
