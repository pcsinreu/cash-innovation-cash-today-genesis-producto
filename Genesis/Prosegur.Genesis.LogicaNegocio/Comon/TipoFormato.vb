Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

''' <summary>
''' Clase EmisorDocumento
''' </summary>
''' <remarks></remarks>
''' <history>
''' [maoliveira] 04/10/2013 - Criado
''' </history>
Public Class TipoFormato

    Public Shared Function ObtenerTiposFormato() As ContractoServicio.TipoFormato.ObtenerTiposFormato.ObtenerTiposFormatoRespuesta

        Dim objRespuesta As New ContractoServicio.TipoFormato.ObtenerTiposFormato.ObtenerTiposFormatoRespuesta

        Try
            ' Recupera os formatos
            objRespuesta.ListaTiposFormato = LogicaNegocio.Genesis.TipoFormato.ObtenerTiposFormato()

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de formatos
        Return objRespuesta

    End Function

    Public Shared Function RecuperarPorTipoComModulo(peticion As ContractoServicio.TipoFormato.RecuperarPorTipoComModulo.RecuperarPorTipoComModuloPeticion) As ContractoServicio.TipoFormato.RecuperarPorTipoComModulo.RecuperarPorTipoComModuloRespuesta


        'Cria a resposta do serviço
        Dim objRespuesta As New ContractoServicio.TipoFormato.RecuperarPorTipoComModulo.RecuperarPorTipoComModuloRespuesta
        Try
            ' Recupera os formatos

            objRespuesta.ListaTiposFormato = LogicaNegocio.Genesis.TipoFormato.RecuperarPorTipoComModulo(peticion)

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de formatos
        Return objRespuesta

    End Function



End Class