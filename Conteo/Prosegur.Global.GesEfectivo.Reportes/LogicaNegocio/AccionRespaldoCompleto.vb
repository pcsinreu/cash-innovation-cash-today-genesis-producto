Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionRespaldoCompleto
    Implements ContractoServ.IRespaldoCompleto

    ''' <summary>
    ''' Lista os respaldos completos de acordo com os filtros passados como parâmetro
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros que deverão ser passados como parâmetro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.olivera] 29/07/2009 Criado
    ''' </history>
    Public Function ListarRespaldoCompleto(objPeticion As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Peticion) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Respuesta Implements ContractoServ.IRespaldoCompleto.ListarRespaldoCompleto

        Dim objRespuesta As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Respuesta

        Try

            ' Verifica o tipo do formato de saída do relatório
            If (objPeticion.FormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV) Then
                ' Recupera os dados para o relatório no formato CSV
                objRespuesta.RespaldosCompletosCSV = AccesoDatos.RespaldoCompleto.ListarRespaldoCompletoCSV(objPeticion)
            Else
                ' Recupera os dados para o relatório no formato PDF
                objRespuesta.RespaldoCompletoPDF = AccesoDatos.RespaldoCompleto.ListarRespaldoCompletoPDF(objPeticion)

                ' Popula as divisas que não foram declaradas e foram contadas na lista de divisas
                PopulaDivisasNaoDeclarados(objRespuesta.RespaldoCompletoPDF.Divisas, objRespuesta.RespaldoCompletoPDF.Detalles)

                ' Popula as divisas que foram declaradas e não foram contadas na lista de divisas
                PopulaDetallesNaoContados(objRespuesta.RespaldoCompletoPDF.Detalles, objRespuesta.RespaldoCompletoPDF.Divisas, objRespuesta.RespaldoCompletoPDF.Sobres)

            End If

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Popula as divisas que não foram declaradas e foram contadas na lista de divisas
    ''' </summary>
    ''' <param name="Divisas"></param>
    ''' <param name="objDetalle"></param>
    ''' <history>
    ''' [magnum.olivera] 04/01/2010 Criado
    ''' </history>
    ''' <remarks></remarks>
    Private Shared Sub PopulaDivisasNaoDeclarados(ByRef Divisas As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DivisaColeccion, _
                                               objDetalle As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DetalleColeccion)

        'Declara a variável usada no For Each
        Dim detalle As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Detalle = Nothing

        'Para cada detalhe existente
        For Each detalle In objDetalle

            'Pesquisa a divisa do detalhe na lista de divisas
            Dim fDivisas = From d In Divisas _
                           Where d.Parcial = detalle.Parcial AndAlso _
                           d.DescripcionMedioPago = detalle.Denominacion AndAlso _
                           d.Divisa = detalle.Divisa

            'Se a divisa não existe na lista de divisas
            If fDivisas IsNot Nothing AndAlso fDivisas.Count = 0 Then

                'Define os dados da nova divisa
                Dim divisa As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Divisa()
                divisa.Parcial = detalle.Parcial
                divisa.DescripcionMedioPago = detalle.Denominacion
                divisa.Divisa = detalle.Divisa

                ' adiciona a divisa na lista de objetos
                Divisas.Add(divisa)
            End If

        Next

    End Sub

    ''' <summary>
    ''' Popula as divisas que foram declaradas e não foram contadas na lista de divisas
    ''' </summary>
    ''' <param name="objDetalle"></param>
    ''' <param name="objDivisas"></param>
    ''' <history>
    ''' [magnum.olivera] 04/01/2010 Criado
    ''' </history>
    ''' <remarks></remarks>
    Private Shared Sub PopulaDetallesNaoContados(ByRef objDetalle As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DetalleColeccion, _
                                               objDivisas As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DivisaColeccion, _
                                               objSobres As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.SobreColeccion)

        'Declara a variável usada no For Each
        Dim divisa As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Divisa = Nothing

        'Para cada divisa existente
        For Each divisa In objDivisas

            Dim sobre = (From s In objSobres _
                         Where s.Parcial = divisa.Parcial).FirstOrDefault()

            Dim parcial = (From p In objDetalle _
                           Where p.Parcial = divisa.Parcial).FirstOrDefault()

            'Define os dados do novo detalhe
            Dim detalle As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Detalle()

            'If sobre IsNot Nothing Then

            detalle.Sucursal = sobre.Sucursal
            detalle.DescricionSucursal = sobre.DescricionSucursal
            detalle.F22 = sobre.F22

            'End If            

            detalle.Letra = "A"
            detalle.Parcial = divisa.Parcial
            detalle.Denominacion = divisa.DescripcionMedioPago
            detalle.Divisa = divisa.Divisa
            detalle.UnidadMoeda = -1

            If parcial IsNot Nothing Then
                detalle.NumeroSecuencia = parcial.NumeroSecuencia
            Else
                detalle.NumeroSecuencia = 0
            End If

            ' adiciona a divisa na lista de objetos
            objDetalle.Add(detalle)

        Next

    End Sub

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 08/02/2010 - criado
    ''' </history>
    Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IRespaldoCompleto.Test
        Dim objRespuesta As New ContractoServ.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("001_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString

        End Try

        Return objRespuesta
    End Function
End Class