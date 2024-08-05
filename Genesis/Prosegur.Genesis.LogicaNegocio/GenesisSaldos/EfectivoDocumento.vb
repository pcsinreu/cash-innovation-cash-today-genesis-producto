Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones

Namespace GenesisSaldos
    Public Class EfectivoDocumento


        Public Shared Sub Inserir_v2(identificadorDocumento As String, divisas As ObservableCollection(Of Clases.Divisa), usuario As String,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            Dim valores As New ObservableCollection(Of Clases.Transferencias.EfectivoDocumentoInserir)

            If divisas IsNot Nothing AndAlso divisas.Count > 0 Then

                For Each _divisa In divisas

                    ' Denominaciones
                    If _divisa.Denominaciones IsNot Nothing Then

                        Dim unidadesMedidasPadron As ObservableCollection(Of Clases.UnidadMedida) = Nothing
                        Dim identificadorUnidadMedida As String = ""

                        For Each _denominacion In _divisa.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Exists(Function(v) v.TipoValor = TipoValor.NoDefinido))

                            For Each valor In _denominacion.ValorDenominacion.Where(Function(v) v.TipoValor = TipoValor.NoDefinido)

                                If valor.UnidadMedida IsNot Nothing Then
                                    identificadorUnidadMedida = valor.UnidadMedida.Identificador
                                    'Else
                                    '    If unidadesMedidasPadron Is Nothing Then
                                    '        unidadesMedidasPadron = AccesoDatos.Genesis.UnidadMedida.ObtenerUnidadesMedida(True)
                                    '    End If
                                    '    identificadorUnidadMedida = unidadesMedidasPadron.FirstOrDefault(Function(x) x.TipoUnidadMedida = If(_denominacion.EsBillete, TipoUnidadMedida.Billete, TipoUnidadMedida.Moneda)).Identificador

                                End If

                                valores.Add(New Clases.Transferencias.EfectivoDocumentoInserir With {
                                            .identificadorDocumento = identificadorDocumento,
                                            .identificadorDivisa = _divisa.Identificador,
                                            .identificadorDenominacion = _denominacion.Identificador,
                                            .identificadorUnidadMedida = identificadorUnidadMedida,
                                            .nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor,
                                            .tipoEfectivoTotal = String.Empty, _
                                            .identificadorCalidad = If(valor.Calidad IsNot Nothing, valor.Calidad.Identificador, String.Empty),
                                            .numImporte = valor.Importe,
                                            .usuarioModificacion = usuario,
                                            .cantidad = valor.Cantidad
                                         })

                            Next
                        Next
                    End If

                    ' Total de efectivo
                    If _divisa.ValoresTotalesEfectivo IsNot Nothing Then

                        For Each valor In _divisa.ValoresTotalesEfectivo.Where(Function(v) v.TipoValor = TipoValor.NoDefinido)

                            valores.Add(New Clases.Transferencias.EfectivoDocumentoInserir With {
                                            .identificadorDocumento = identificadorDocumento,
                                            .identificadorDivisa = _divisa.Identificador,
                                            .identificadorDenominacion = String.Empty,
                                            .identificadorUnidadMedida = String.Empty,
                                            .nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor,
                                            .tipoEfectivoTotal = valor.TipoDetalleEfectivo.RecuperarValor(), _
                                            .identificadorCalidad = String.Empty,
                                            .numImporte = valor.Importe,
                                            .usuarioModificacion = usuario,
                                            .cantidad = 0
                                         })

                        Next
                    End If

                    ' Total de Divisas
                    If _divisa.ValoresTotalesDivisa IsNot Nothing Then

                        For Each valor In _divisa.ValoresTotalesDivisa.Where(Function(v) v.TipoValor = TipoValor.NoDefinido)

                            valores.Add(New Clases.Transferencias.EfectivoDocumentoInserir With {
                                            .identificadorDocumento = identificadorDocumento,
                                            .identificadorDivisa = _divisa.Identificador,
                                            .identificadorDenominacion = String.Empty,
                                            .identificadorUnidadMedida = String.Empty,
                                            .nivelDetalhe = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor,
                                            .tipoEfectivoTotal = String.Empty, _
                                            .identificadorCalidad = String.Empty,
                                            .numImporte = valor.Importe,
                                            .usuarioModificacion = usuario,
                                            .cantidad = 0
                                         })

                        Next
                    End If
                Next

                If valores IsNot Nothing AndAlso valores.Count > 0 Then
                    AccesoDatos.GenesisSaldos.EfectivoDocumento.Inserir_v2(valores, _transacion)
                End If

            End If


        End Sub



        Public Shared Sub Inserir(documento As Clases.Documento)

            Dim erros As New System.Text.StringBuilder
            Dim identificadorDenominacion As String, _
                identificadorUnidadMedida As String = String.Empty, _
                nivelDetalhe As String, _
                tipoEfectivoTotal As String, _
                identificadorCalidad As String, _
                numImporte As String, _
                cantidad As Decimal

            'Recupera a unidades de medida padrao
            Dim unidadesMedidasPadron = AccesoDatos.Genesis.UnidadMedida.ObtenerUnidadesMedida(True)
            If unidadesMedidasPadron Is Nothing Then
                unidadesMedidasPadron = New ObservableCollection(Of Clases.UnidadMedida)
            End If

            If documento IsNot Nothing Then
                For Each divisa In documento.Divisas
                    identificadorDenominacion = String.Empty
                    identificadorCalidad = String.Empty

                    'se existe valores então 
                    If divisa.Denominaciones IsNot Nothing Then
                        For Each Denominacion In divisa.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Exists(Function(v) v.TipoValor = TipoValor.NoDefinido))
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor
                            tipoEfectivoTotal = String.Empty

                            identificadorDenominacion = Denominacion.Identificador

                            For Each valor In Denominacion.ValorDenominacion.Where(Function(v) v.TipoValor = TipoValor.NoDefinido)

                                If valor.Calidad IsNot Nothing Then
                                    identificadorCalidad = valor.Calidad.Identificador
                                Else
                                    identificadorCalidad = String.Empty
                                End If

                                numImporte = valor.Importe
                                cantidad = valor.Cantidad

                                If valor.UnidadMedida IsNot Nothing Then
                                    identificadorUnidadMedida = valor.UnidadMedida.Identificador
                                    'Else
                                    '    'Recupera a unidade de medida padrão
                                    '    Dim unidadeMedidaPadron = unidadesMedidasPadron.Where(Function(u) u.TipoUnidadMedida = If(Denominacion.EsBillete, TipoUnidadMedida.Billete, TipoUnidadMedida.Moneda)).FirstOrDefault()
                                    '    If unidadeMedidaPadron IsNot Nothing Then
                                    '        identificadorUnidadMedida = unidadeMedidaPadron.Identificador
                                    '    End If
                                End If

                                'Insere o efectivo de documento
                                AccesoDatos.GenesisSaldos.EfectivoDocumento.EfectivoDocumentoInserir(documento.Identificador, _
                                             divisa.Identificador, _
                                             identificadorDenominacion, _
                                             identificadorUnidadMedida, _
                                             nivelDetalhe, _
                                             tipoEfectivoTotal, _
                                             identificadorCalidad, _
                                             numImporte, _
                                             documento.UsuarioModificacion, _
                                             cantidad)

                            Next
                        Next
                    End If

                    'se existe valores total de efectivo
                    If divisa.ValoresTotalesEfectivo IsNot Nothing Then
                        identificadorCalidad = String.Empty
                        identificadorUnidadMedida = String.Empty

                        For Each valor In divisa.ValoresTotalesEfectivo.Where(Function(v) v.TipoValor = TipoValor.NoDefinido)
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor
                            tipoEfectivoTotal = valor.TipoDetalleEfectivo.RecuperarValor()

                            identificadorDenominacion = String.Empty
                            numImporte = valor.Importe

                            'TODO: Verificar cantidad quando for total de efectivo.
                            cantidad = 0

                            'Insere o efectivo de documento
                            AccesoDatos.GenesisSaldos.EfectivoDocumento.EfectivoDocumentoInserir(documento.Identificador, _
                                         divisa.Identificador, _
                                         identificadorDenominacion, _
                                         identificadorUnidadMedida, _
                                         nivelDetalhe, _
                                         tipoEfectivoTotal, _
                                         identificadorCalidad, _
                                         numImporte, _
                                         documento.UsuarioModificacion, _
                                         cantidad)
                        Next
                    End If

                    'se existe valores total de divisas
                    If divisa.ValoresTotalesDivisa IsNot Nothing Then
                        identificadorCalidad = String.Empty
                        identificadorUnidadMedida = String.Empty

                        For Each valor In divisa.ValoresTotalesDivisa.Where(Function(v) v.TipoValor = TipoValor.NoDefinido)
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor
                            tipoEfectivoTotal = String.Empty
                            identificadorDenominacion = String.Empty

                            numImporte = valor.Importe

                            'TODO: Verificar cantidad quando for total de divisa.
                            cantidad = 0

                            'Insere o efectivo de documento
                            AccesoDatos.GenesisSaldos.EfectivoDocumento.EfectivoDocumentoInserir(documento.Identificador, _
                                         divisa.Identificador, _
                                         identificadorDenominacion,
                                         identificadorUnidadMedida, _
                                         nivelDetalhe, _
                                         tipoEfectivoTotal, _
                                         identificadorCalidad, _
                                         numImporte, _
                                         documento.UsuarioModificacion, _
                                         cantidad)
                        Next
                    End If
                Next
            End If
        End Sub

        Public Shared Sub Excluir(IdentificadorDocumento As String)
            AccesoDatos.GenesisSaldos.EfectivoDocumento.EfectivoDocumentoExcluir(IdentificadorDocumento)
        End Sub

    End Class
End Namespace

