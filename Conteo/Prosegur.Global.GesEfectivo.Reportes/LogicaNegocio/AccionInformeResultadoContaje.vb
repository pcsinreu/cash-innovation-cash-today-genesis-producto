Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionInformeResultadoContaje
    Implements ContractoServ.IInformeResutadoContaje

#Region "ListarResultadoContaje"

    Public Function ListarResultadoContaje(Peticion As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Peticion) As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Respuesta Implements ContractoServ.IInformeResutadoContaje.ListarResultadoContaje

        Dim objRespuesta As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Respuesta

        Try

            'Valida se o campo esta preenchido
            Util.ValidarCampoObrigatorio(Peticion.OidRemesa, "002_OidRemesa", GetType(String), False, True)

            objRespuesta.Remesa = AccesoDatos.InformeResultadoContaje.RecuperarInformacionesResultadoContaje(Peticion)

            If objRespuesta.Remesa IsNot Nothing Then

                CalcularDiferencias(objRespuesta.Remesa)

                ' Valores Absolutos
                VerificaDiferenciaAbs(objRespuesta.Remesa.Diferencias)

                For Each b In objRespuesta.Remesa.Bultos
                    VerificaDiferenciaAbs(b.Diferencias)
                    For Each p In b.Parciales
                        VerificaDiferenciaAbs(p.Diferencias)
                    Next
                Next

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
    ''' Calcula as diferenças de contagem
    ''' </summary>
    ''' <param name="objRemesa"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/06/2011 - Criado
    ''' </history>
    Private Sub CalcularDiferencias(ByRef objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        If objRemesa IsNot Nothing AndAlso objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then

            objRemesa.Diferencias = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion

            'Soma os declarados da remesa e adiciona no objeto de diferenças
            SomarDeclarados(objRemesa.Declarados, objRemesa.Diferencias)

            If objRemesa.Declarados Is Nothing Then
                objRemesa.Declarados = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion()
            End If

            For Each b In objRemesa.Bultos

                b.Diferencias = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion

                'Soma os declarados do bulto e adiciona no objeto de diferenças
                SomarDeclarados(b.Declarados, b.Diferencias)

                If b.Declarados Is Nothing Then
                    b.Declarados = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion()
                End If

                If b.Parciales IsNot Nothing AndAlso b.Parciales.Count > 0 Then

                    For Each p In b.Parciales

                        p.Diferencias = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion

                        'Soma os declarados do parcial e adiciona no objeto de diferenças
                        SomarDeclarados(p.Declarados, p.Diferencias)

                        If p.Declarados Is Nothing Then
                            p.Declarados = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion()
                        End If

                        'Calcula as diferenças dos efectivos
                        CalcularDiferenciasEfectivos(p.Efectivos, p.Diferencias, p.Declarados)
                        CalcularDiferenciasEfectivos(p.Efectivos, b.Diferencias, b.Declarados)
                        CalcularDiferenciasEfectivos(p.Efectivos, objRemesa.Diferencias, objRemesa.Declarados)

                        CalcularDiferenciasMediosPago(p.MediosPagos, p.Diferencias, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_CHEQUE)
                        CalcularDiferenciasMediosPago(p.MediosPagos, p.Diferencias, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_OTROSVALORES)
                        CalcularDiferenciasMediosPago(p.MediosPagos, p.Diferencias, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_TICKET)

                        CalcularDiferenciasMediosPago(p.MediosPagos, b.Diferencias, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_CHEQUE)
                        CalcularDiferenciasMediosPago(p.MediosPagos, b.Diferencias, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_OTROSVALORES)
                        CalcularDiferenciasMediosPago(p.MediosPagos, b.Diferencias, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_TICKET)

                        CalcularDiferenciasMediosPago(p.MediosPagos, objRemesa.Diferencias, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_CHEQUE)
                        CalcularDiferenciasMediosPago(p.MediosPagos, objRemesa.Diferencias, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_OTROSVALORES)
                        CalcularDiferenciasMediosPago(p.MediosPagos, objRemesa.Diferencias, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_TICKET)

                    Next

                End If

            Next

            ' Confere as Diferenças
            VerificaDiferencia(objRemesa.Diferencias)
            For Each b In objRemesa.Bultos
                VerificaDiferencia(b.Diferencias)
                For Each p In b.Parciales
                    VerificaDiferencia(p.Diferencias)
                Next
            Next
        End If

    End Sub

    ''' <summary>
    ''' Verifica coloções de diferenças
    ''' </summary>
    ''' <param name="objDiferencias"></param>
    ''' <remarks></remarks>
    Private Sub VerificaDiferencia(ByRef objDiferencias As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion)
        For Each dif In objDiferencias
            If dif.Contado = 0 AndAlso dif.Declarado > 0 AndAlso dif.Diferencia = 0 Then
                dif.Diferencia -= dif.Declarado
            End If
        Next
    End Sub

    ''' <summary>
    ''' Apenas Coloca os valores Diferencia absoluto
    ''' </summary>
    ''' <param name="objDiferencias"></param>
    ''' <remarks></remarks>
    Private Sub VerificaDiferenciaAbs(ByRef objDiferencias As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion)
        For Each dif In objDiferencias
            dif.Diferencia = System.Math.Abs(dif.Diferencia)
        Next
    End Sub

    ''' <summary>
    ''' Cria um novo objeto de Diferencia e adiciona na coleção
    ''' </summary>
    ''' <param name="objDiferencias"></param>
    ''' <param name="oCodDivisa"></param>
    ''' <param name="oNombreDivisa"></param>
    ''' <param name="oNombreMedioPago"></param>
    ''' <param name="oCodTipoMedioPago"></param>
    ''' <param name="oEsEfectivo"></param>
    ''' <remarks></remarks>
    Private Sub CriarObjetoDiferencias(ByRef objDiferencias As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion, _
                                       oCodDivisa As String, oNombreDivisa As String, _
                                       oNombreMedioPago As String, oCodTipoMedioPago As String, oEsEfectivo As Boolean)

        Dim objDiferencia As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Diferencia
        With objDiferencia
            .CodIsoDivisa = oCodDivisa
            .Contado = 0
            .Falsos = 0
            .Diferencia = 0
            .NombreDivisa = oNombreDivisa
            .NombreMedioPago = oNombreMedioPago
            .CodTipoMedioPago = oCodTipoMedioPago
            .EsEfectivo = oEsEfectivo
        End With
        objDiferencias.Add(objDiferencia)

        Dim DiferenciaTotales = From dif In objDiferencias Where dif.CodIsoDivisa = oCodDivisa AndAlso dif.NombreMedioPago = oNombreMedioPago
        If DiferenciaTotales Is Nothing OrElse DiferenciaTotales.Count = 0 Then

            objDiferencia = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Diferencia
            With objDiferencia
                .CodIsoDivisa = oCodDivisa
                .Contado = 0
                .Falsos = 0
                .Diferencia = 0
                .NombreDivisa = oNombreDivisa
                .NombreMedioPago = Traduzir("002_totales")
                .EsEfectivo = False
            End With
            objDiferencias.Add(objDiferencia)

        End If

    End Sub

    ''' <summary>
    ''' Calcula as diferencas dos efectivos
    ''' </summary>
    ''' <param name="Valor"></param>
    ''' <param name="objDiferencias"></param>
    ''' <remarks></remarks>
    Private Sub CalcularDiferenciasTotales(Valor As Decimal, _
                                           CodDivisa As String, NombreDivisa As String, _
                                           ByRef objDiferencias As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion,
                                           Falsos As Decimal)

        Dim DiferenciaTotales = From dif In objDiferencias Where dif.CodIsoDivisa = CodDivisa AndAlso dif.EsEfectivo = False AndAlso String.IsNullOrEmpty(dif.CodTipoMedioPago)

        If DiferenciaTotales.Count > 0 Then

            DiferenciaTotales.First.Contado += Valor
            DiferenciaTotales.First.Falsos += Falsos
            DiferenciaTotales.First.Diferencia = DiferenciaTotales.First.Contado - DiferenciaTotales.First.Declarado

        End If

    End Sub

    ''' <summary>
    ''' Calcula as diferencas dos efectivos
    ''' </summary>
    ''' <param name="Efectivos"></param>
    ''' <param name="objDiferencias"></param>
    ''' <remarks></remarks>
    Private Sub CalcularDiferenciasEfectivos(Efectivos As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.EfectivoColeccion, _
                                             ByRef objDiferencias As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion, _
                                             objDeclarados As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion)

        If Efectivos IsNot Nothing AndAlso Efectivos.Count > 0 AndAlso objDeclarados IsNot Nothing Then

            Dim ImporteEfectivo = From Ef In Efectivos Group By Ef.CodIsoDivisa, Ef.NombreDivisa Into Importe = Sum(Ef.Denominacion * (Ef.Deteriorado + Ef.Unidades)), Falsos = Sum(Ef.Falso * Ef.Denominacion)

            If ImporteEfectivo.Count > 0 Then

                For Each Ef In ImporteEfectivo

                    Dim CodDivisa As String = Ef.CodIsoDivisa
                    Dim Declarado = From d In objDeclarados Where d.CodIsoDivisa = CodDivisa AndAlso d.ImporteEfectivo > 0

                    If Declarado IsNot Nothing Then

                        Dim Diferencia = From dif In objDiferencias Where dif.CodIsoDivisa = CodDivisa AndAlso dif.EsEfectivo = True

                        If Diferencia Is Nothing OrElse Diferencia.Count = 0 Then
                            CriarObjetoDiferencias(objDiferencias, Ef.CodIsoDivisa, Ef.NombreDivisa, Traduzir("002_efectivo"), String.Empty, True)
                        End If

                        Diferencia.First.Contado += Ef.Importe
                        Diferencia.First.Falsos += Ef.Falsos
                        Diferencia.First.Diferencia = Diferencia.First.Contado - Diferencia.First.Declarado

                        'Calcula as diferenças totais
                        CalcularDiferenciasTotales(Ef.Importe, Ef.CodIsoDivisa, Ef.NombreDivisa, objDiferencias, Ef.Falsos)

                    End If

                Next

            End If

        End If

    End Sub

    ''' <summary>
    ''' Calcula as diferencas dos efectivos
    ''' </summary>
    ''' <param name="MediosPagos"></param>
    ''' <param name="objDiferencias"></param>
    ''' <remarks></remarks>
    Private Sub CalcularDiferenciasMediosPago(MediosPagos As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.MedioPagoColeccion, _
                                              ByRef objDiferencias As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion, _
                                              CodMedioPago As String)

        If MediosPagos IsNot Nothing AndAlso MediosPagos.Count > 0 Then

            Dim ImporteMp = From Mp In MediosPagos Where Mp.CodTipoMedioPago = CodMedioPago _
                              Group By Mp.CodIsoDivisa, Mp.DescripcionDivisa, Mp.NombreMedioPago Into Importe = Sum(Mp.Importe)

            If ImporteMp.Count > 0 Then

                For Each Mp In ImporteMp

                    Dim CodIsoDivisa As String = Mp.CodIsoDivisa
                    Dim Diferencia = From dif In objDiferencias Where dif.CodIsoDivisa = CodIsoDivisa AndAlso dif.CodTipoMedioPago = CodMedioPago

                    If Diferencia Is Nothing OrElse Diferencia.Count = 0 Then
                        CriarObjetoDiferencias(objDiferencias, CodIsoDivisa, Mp.DescripcionDivisa, Mp.NombreMedioPago, CodMedioPago, False)
                    End If

                    Diferencia.First.Contado += Mp.Importe
                    Diferencia.First.Diferencia = Diferencia.First.Contado - Diferencia.First.Declarado

                    'Calcula as diferenças totais
                    CalcularDiferenciasTotales(Mp.Importe, Mp.CodIsoDivisa, Mp.DescripcionDivisa, objDiferencias, 0)

                Next

            End If

        End If

    End Sub

    ''' <summary>
    ''' Faz a soma dos declarados
    ''' </summary>
    ''' <param name="objDeclarados"></param>
    ''' <param name="objDiferencias"></param>
    ''' <remarks></remarks>
    Private Sub SomarDeclarados(objDeclarados As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion, _
                                ByRef objDiferencias As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion)

        If objDeclarados IsNot Nothing Then

            Dim SomaDeclarados = From declarado In objDeclarados Group By declarado.CodIsoDivisa, declarado.DescripcionDivisa _
                                 Into ImporteTotal = Sum(declarado.ImporteTotal), ImporteTicket = Sum(declarado.ImporteTicket), _
                                 ImporteCheque = Sum(declarado.ImporteCheque), ImporteEfectivo = Sum(declarado.ImporteEfectivo), _
                                 ImporteOtroValor = Sum(declarado.ImporteOtroValor)

            If SomaDeclarados IsNot Nothing AndAlso SomaDeclarados.Count > 0 Then

                For Each dec In SomaDeclarados

                    'Preenche os declarados das diferenças
                    PreencherDeclaradoDiferencia(objDiferencias, dec.ImporteCheque, dec.CodIsoDivisa, dec.DescripcionDivisa, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_CHEQUE, False)
                    PreencherDeclaradoDiferencia(objDiferencias, dec.ImporteEfectivo, dec.CodIsoDivisa, dec.DescripcionDivisa, String.Empty, True)
                    PreencherDeclaradoDiferencia(objDiferencias, dec.ImporteOtroValor, dec.CodIsoDivisa, dec.DescripcionDivisa, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_OTROSVALORES, False)
                    PreencherDeclaradoDiferencia(objDiferencias, dec.ImporteTicket, dec.CodIsoDivisa, dec.DescripcionDivisa, ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_TICKET, False)
                    PreencherDeclaradoDiferencia(objDiferencias, dec.ImporteTotal, dec.CodIsoDivisa, dec.DescripcionDivisa, String.Empty, False)

                Next

            End If

        End If

    End Sub

    ''' <summary>
    ''' Metodo Responsavel por preencher os declarados das diferenças
    ''' </summary>
    ''' <param name="objDiferencias"></param>
    ''' <param name="Valor"></param>
    ''' <param name="CodIsoDivisa"></param>
    ''' <param name="DescripcionDivisa"></param>
    ''' <param name="CodTipoMedioPago"></param>
    ''' <param name="EsFectivo"></param>
    ''' <remarks></remarks>
    Private Sub PreencherDeclaradoDiferencia(objDiferencias As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion, _
                                             Valor As Decimal, CodIsoDivisa As String, _
                                             DescripcionDivisa As String, _
                                             CodTipoMedioPago As String, _
                                             EsFectivo As Boolean)

        If Valor = 0 Then
            Exit Sub
        End If

        If objDiferencias Is Nothing Then
            objDiferencias = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion
        End If

        Dim nomeTipoMeioPagamento As String = String.Empty

        ' verifica se é total/efetivo ou outro meio de pagamento
        If String.IsNullOrEmpty(CodTipoMedioPago) Then
            ' verifica se é total ou efectivo
            If EsFectivo Then
                nomeTipoMeioPagamento = Traduzir("002_efectivo")
            Else
                nomeTipoMeioPagamento = Traduzir("002_totales")
            End If
        Else
            Select Case CodTipoMedioPago
                Case ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_CHEQUE
                    nomeTipoMeioPagamento = Traduzir("002_Cheque")
                Case ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_OTROSVALORES
                    nomeTipoMeioPagamento = Traduzir("002_OtroValor")
                Case ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_TICKET
                    nomeTipoMeioPagamento = Traduzir("002_Ticket")
            End Select
        End If

        objDiferencias.Add(New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Diferencia With { _
                                            .CodIsoDivisa = CodIsoDivisa, _
                                            .CodTipoMedioPago = CodTipoMedioPago, _
                                            .Declarado = Valor, _
                                            .EsEfectivo = EsFectivo, _
                                            .NombreDivisa = DescripcionDivisa, _
                                            .NombreMedioPago = nomeTipoMeioPagamento})

    End Sub

#End Region

#Region "[BuscarResultadoContaje]"

    Public Function BuscarResultadoContaje(Peticion As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Peticion) As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Respuesta Implements ContractoServ.IInformeResutadoContaje.BuscarResultadoContaje

        Dim objRespuesta As New ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Respuesta

        Try

            'Faz a validação dos campos obrigatórios.
            ValidarDatosBuscarResultadoContaje(Peticion)

            objRespuesta.Remesas = AccesoDatos.InformeResultadoContaje.BuscarInformacionesResultadoContaje(Peticion)

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
    ''' Faz a validação dos dados
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 04/01/2012 - Criado
    ''' </history>
    Private Sub ValidarDatosBuscarResultadoContaje(Peticion As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Peticion)

        If String.IsNullOrEmpty(Peticion.CodPrecintoBulto) AndAlso String.IsNullOrEmpty(Peticion.CodPrecintoRemesa) AndAlso String.IsNullOrEmpty(Peticion.CodTransporte) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("003_campos_obrigatorios"))
        End If

    End Sub

#End Region

    Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IInformeResutadoContaje.Test

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