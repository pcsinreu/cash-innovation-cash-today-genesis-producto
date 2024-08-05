Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento

Namespace Integracion

    Public Class AccionSalidasRecorrido


        Public Shared Function Ejecutar(peticion As SalirRecorrido.SalirRecorridoPeticion) As SalirRecorrido.SalirRecorridoRespuesta

            Dim respuesta As New SalirRecorrido.SalirRecorridoRespuesta()

            Try

                Validar(peticion)

                AccesoDatos.GenesisSaldos.GrupoDocumentos.GrabarGrupoDocumentoSalidasRecorrido(peticion, Comon.Enumeradores.CaracteristicaFormulario.IntegracionLegado)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                respuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.MensajeError = ex.ToString()
            End Try

            Return respuesta

        End Function

        Public Shared Sub Validar(peticion As SalirRecorrido.SalirRecorridoPeticion)

            ' valida o token passado na petição
            Util.VerificaInformacionesToken(peticion)

            ' valida se a petição existe
            If peticion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_SALIRRECORRIDO_PETICION_OBLIGATORIA"))
            End If

            ' verifica se o código de rota ou pelo menos um código de recibo de transporte foram informados
            If String.IsNullOrEmpty(peticion.CodigoRuta) AndAlso (peticion.Origen Is Nothing OrElse peticion.Origen.Elementos Is Nothing OrElse peticion.Origen.Elementos.Where(Function(e) String.IsNullOrEmpty(e.ReciboTransporte)).Count() = peticion.Origen.Elementos.Count()) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_SALIRRECORRIDO_CODIGO_RUTA_O_RECIBO_OBLIGATORIOS"))
            End If

            ' certifica-se que a origem foi informada que os dados relacionados a ela são válidos
            If Not (peticion.Origen IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Origen.CodigoDelegacion) AndAlso Not String.IsNullOrEmpty(peticion.Origen.CodigoPlanta) AndAlso Not String.IsNullOrEmpty(peticion.Origen.CodigoSector)) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_SALIRRECORRIDO_DATOS_ORIGEN_OBLIGATORIOS"))
            End If

        End Sub

    End Class

End Namespace

