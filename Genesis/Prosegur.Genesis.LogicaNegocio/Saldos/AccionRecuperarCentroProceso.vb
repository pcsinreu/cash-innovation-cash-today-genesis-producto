Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.Saldos
Imports Prosegur.Global.Saldos.ContractoServicio

Public Class AccionRecuperarCentroProceso

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Método responsável por recuperar os dados dos centros de Processos mapeados a uma determinada delegação
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 29/04/2011 Criado
    ''' </history>
    Public Function Ejecutar(Peticion As RecuperarCentroProceso.Peticion) As RecuperarCentroProceso.Respuesta

        Dim objRespuesta As New RecuperarCentroProceso.Respuesta

        ' setar codigo 0 e mensagem erro vazio
        objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
        objRespuesta.MensajeError = String.Empty
        objRespuesta.Resultado = 1

        Try
            ' valida os dados passados na petição
            If ValidaDadosPeticion(Peticion, objRespuesta) Then

                ' busca os dados das plantas associadas à delegação passada na petição
                Dim objPlanta As New Negocio.Planta
                objPlanta.CodDelegacionGenesis = Peticion.CodigoDelegacion
                objPlanta.RealizarByCodigoDelegacion()

                ' carrega os dados de resposta
                objRespuesta.CodigoDelegacion = Peticion.CodigoDelegacion
                objRespuesta.DescripcionPlanta = objPlanta.Descripcion
                objRespuesta.IDPSPlanta = objPlanta.IdPS

                ' busca os centros de proceso de uma planta
                objPlanta.CentrosProcesoRealizarByIdsPlanta()

                ' instanciando o objeto de processos de resposta
                objRespuesta.CentroProcesoColeccion = New RecuperarCentroProceso.CentroProcesoColeccion

                ' adiciona os centros de proceso na coleção de resposta
                For Each objCentroProcesso As Negocio.CentroProceso In objPlanta.CentrosProceso

                    Dim objCentroProcesoResposta As New RecuperarCentroProceso.CentroProceso

                    objCentroProcesoResposta.CodigoIDPS = objCentroProcesso.IdPS
                    objCentroProcesoResposta.DescripcionCentroProceso = objCentroProcesso.Descripcion

                    objRespuesta.CentroProcesoColeccion.Add(objCentroProcesoResposta)

                Next

                ' o serviço foi executado com sucesso
                objRespuesta.Resultado = 0

            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Valida se os dados da petição são válidos
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidaDadosPeticion(Peticion As RecuperarCentroProceso.Peticion, _
                                         ByRef Respuesta As RecuperarCentroProceso.Respuesta) As Boolean

        If Peticion.CodigoDelegacion Is Nothing OrElse _
           String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then

            'Preenche o objeto de resposta
            Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            Respuesta.MensajeError = String.Format(Traduzir("02_msg_dados_campo_naoinformado"), Traduzir("02_msg_campo_delegacion"))

            Return False

        End If

        Return True

    End Function

#End Region

End Class