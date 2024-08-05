Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones

Namespace GenesisSaldos
    Public Class MedioPagoDocumento

        Public Shared Sub Inserir_v2(identificadorDocumento As String, divisas As ObservableCollection(Of Clases.Divisa), usuario As String,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            Dim valores As New ObservableCollection(Of Clases.Transferencias.MedioPagoDocumentoInserir)

            If divisas IsNot Nothing AndAlso divisas.Count > 0 Then

                For Each _divisa In divisas

                    If _divisa.MediosPago IsNot Nothing Then

                        For Each _medioPago In _divisa.MediosPago.Where(Function(d) d.Valores IsNot Nothing AndAlso d.Valores.Exists(Function(v) v.TipoValor = TipoValor.NoDefinido))

                            For Each valor In _medioPago.Valores.Where(Function(v) v.TipoValor = TipoValor.NoDefinido)

                                valores.Add(New Clases.Transferencias.MedioPagoDocumentoInserir With {
                                            .identificadorDocumento = identificadorDocumento,
                                            .identificadorDivisa = _divisa.Identificador,
                                            .identificadorMedioPago = _medioPago.Identificador,
                                            .nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor,
                                            .tipoMedioPago = _medioPago.Tipo.RecuperarValor(),
                                            .numImporte = valor.Importe,
                                            .usuarioModificacion = usuario,
                                            .cantidad = valor.Cantidad
                                         })

                            Next
                        Next
                    End If

                    ' Total de medioPago
                    If _divisa.ValoresTotalesTipoMedioPago IsNot Nothing Then

                        For Each valor In _divisa.ValoresTotalesTipoMedioPago.Where(Function(v) v.TipoValor = TipoValor.NoDefinido)

                            valores.Add(New Clases.Transferencias.MedioPagoDocumentoInserir With {
                                            .identificadorDocumento = identificadorDocumento,
                                            .identificadorDivisa = _divisa.Identificador,
                                            .identificadorMedioPago = String.Empty,
                                            .nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor,
                                            .tipoMedioPago = valor.TipoMedioPago.RecuperarValor(),
                                            .numImporte = valor.Importe,
                                            .usuarioModificacion = usuario,
                                            .cantidad = valor.Cantidad
                                         })
                        Next
                    End If

                Next

                If valores IsNot Nothing AndAlso valores.Count > 0 Then
                    AccesoDatos.GenesisSaldos.MedioPagoDocumento.Inserir_v2(valores, _transacion)
                End If

            End If


        End Sub



        Public Shared Sub Inserir(documento As Clases.Documento)

            Dim erros As New System.Text.StringBuilder
            Dim identificadorMedioPago As String, _
                nivelDetalhe As String, _
                tipoMedioPago As String, _
                cantidad As Int64, _
                numImporte As Decimal

            If documento.Divisas IsNot Nothing Then
                For Each divisa In documento.Divisas
                    identificadorMedioPago = String.Empty

                    'se existe valores então 
                    If divisa.MediosPago IsNot Nothing Then
                        For Each medioPago In divisa.MediosPago.Where(Function(d) d.Valores IsNot Nothing AndAlso d.Valores.Exists(Function(v) v.TipoValor = TipoValor.NoDefinido))
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor
                            tipoMedioPago = medioPago.Tipo.RecuperarValor()
                            identificadorMedioPago = medioPago.Identificador

                            'Verificar se tem mais de um valor
                            'se tiver gerar erro.
                            For Each valor In medioPago.Valores.Where(Function(v) v.TipoValor = TipoValor.NoDefinido)
                                cantidad = valor.Cantidad
                                numImporte = valor.Importe

                                'Insere o efectivo de documento
                                Dim identificadorMedioPagoDocumento = AccesoDatos.GenesisSaldos.MedioPagoDocumento.MedioPagoDocumentoInserir(documento.Identificador, _
                                             divisa.Identificador, _
                                             identificadorMedioPago, _
                                             nivelDetalhe, _
                                             tipoMedioPago, _
                                             cantidad, _
                                             numImporte, _
                                             documento.UsuarioModificacion)

                                'Verifica se o medio pago possui terminos
                                'Se sim então insere os valores para cada termino
                                If medioPago.Terminos IsNot Nothing Then
                                    LogicaNegocio.Genesis.TerminoIAC.ValorTerminoMedioPagoDocumentoInserir(medioPago, identificadorMedioPagoDocumento, documento.UsuarioModificacion)
                                End If
                            Next
                        Next
                    End If

                    'se existe valores total de medioPago
                    If divisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
                        identificadorMedioPago = String.Empty
                        For Each valor In divisa.ValoresTotalesTipoMedioPago.Where(Function(v) v.TipoValor = TipoValor.NoDefinido)
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor
                            tipoMedioPago = valor.TipoMedioPago.RecuperarValor()
                            numImporte = valor.Importe
                            cantidad = valor.Cantidad

                            'Insere o efectivo de documento
                            AccesoDatos.GenesisSaldos.MedioPagoDocumento.MedioPagoDocumentoInserir(documento.Identificador, _
                                               divisa.Identificador, _
                                               identificadorMedioPago, _
                                               nivelDetalhe, _
                                               tipoMedioPago, _
                                               cantidad, _
                                               numImporte, _
                                               documento.UsuarioModificacion)
                        Next
                    End If
                Next
            End If
        End Sub

        Public Shared Sub Excluir(IdentificadorDocumento As String)
            AccesoDatos.GenesisSaldos.MedioPagoDocumento.MedioPagoDocumentoExcluir(IdentificadorDocumento)
        End Sub
    End Class
End Namespace

