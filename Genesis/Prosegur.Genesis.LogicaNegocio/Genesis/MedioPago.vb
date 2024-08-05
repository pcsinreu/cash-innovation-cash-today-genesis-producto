Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Data

Namespace Genesis
    Public Class MedioPago

        Public Shared Function ObtenerMediosPago(IdentificadorDivisa As String, _
                                                 ListaIdentificadores As List(Of String), _
                                        Optional EsNotIn As Boolean = False) As ObservableCollection(Of Clases.MedioPago)

            Try
                Return AccesoDatos.Genesis.MedioPago.RecuperarMediosPago(IdentificadorDivisa, ListaIdentificadores, EsNotIn)

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function ObtenerMediosPagoConTerminos(IdentificadorDivisa As String, _
                                                            ListaIdentificadores As List(Of String), _
                                                            Optional EsNotIn As Boolean = False) As ObservableCollection(Of Clases.MedioPago)
            Try
                Return AccesoDatos.Genesis.MedioPago.ObtenerMediosPagoConTerminos(IdentificadorDivisa, ListaIdentificadores, EsNotIn)
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Shared Function ObtenerMedioPagoPorCodigo(codigo As String) As Clases.MedioPago
            Return AccesoDatos.Genesis.MedioPago.RecuperarMedioPagoPorCodigo(codigo)
        End Function



















        Public Shared Sub DeclaradoMedioPagoRemesaInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)
            DeclaradoMedioPagoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

            'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
            If objRemesa.Bultos IsNot Nothing Then
                For Each objBulto In objRemesa.Bultos
                    objBulto.UsuarioModificacion = objRemesa.UsuarioModificacion
                    'Insere os efectivo do bulto.
                    DeclaradoMedioPagoBultoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
                Next
            End If
        End Sub

        Public Shared Sub DeclaradoMedioPagoBultoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
            DeclaradoMedioPagoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

            If objBulto.Parciales IsNot Nothing Then
                For Each objParcial In objBulto.Parciales
                    objParcial.UsuarioModificacion = objBulto.UsuarioModificacion
                    'se o bulto tiver parcial então insere declarado da parcial.
                    DeclaradoMedioPagoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objParcial.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
                Next
            End If
        End Sub

        Private Shared Sub DeclaradoMedioPagoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
            Dim identificadorDivisa As String = String.Empty,
                identificadorMedioPago As String = String.Empty,
                tipoMedioPago As String = String.Empty,
                importe As Decimal,
                cantidad As Long,
                nivelDetalhe As String = String.Empty,
                ingresado As Boolean

            If listaDivisas IsNot Nothing Then
                For Each divisa In listaDivisas
                    identificadorDivisa = divisa.Identificador

                    'se existe valores então 
                    If divisa.MediosPago IsNot Nothing Then
                        'recupera somente registros com valores declarados
                        For Each medioPago In divisa.MediosPago.Where(Function(m) m.Valores IsNot Nothing AndAlso m.Valores.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado))
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()
                            identificadorMedioPago = medioPago.Identificador
                            tipoMedioPago = medioPago.Tipo.RecuperarValor()

                            For Each valor In medioPago.Valores.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado AndAlso v.Importe <> 0)
                                importe = valor.Importe
                                cantidad = valor.Cantidad

                                'Insere o declarado Efectivo
                                Dim identificadorDeclaradoMedioPago = AccesoDatos.Genesis.DeclaradoMedioPago.DeclaradoMedioPagoInserir(identificadorRemesa, _
                                                identificadorBulto, _
                                                identificadorParcial, _
                                                identificadorDivisa, _
                                                identificadorMedioPago, _
                                                tipoMedioPago, _
                                                importe, _
                                                cantidad, _
                                                nivelDetalhe, _
                                                ingresado, _
                                                usuario)

                                If valor.Terminos IsNot Nothing Then
                                    LogicaNegocio.Genesis.TerminoIAC.ValorTerminoMedioPagoElementoInserir(valor, identificadorDeclaradoMedioPago, Enumeradores.TipoValor.Declarado, usuario)
                                End If
                            Next
                        Next
                    End If

                    'se existe valores então 
                    If divisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
                        'recupera somente registros com valores declarados
                        For Each valor In divisa.ValoresTotalesTipoMedioPago.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado AndAlso v.Importe <> 0)
                            identificadorMedioPago = String.Empty
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
                            importe = valor.Importe
                            cantidad = valor.Cantidad
                            tipoMedioPago = valor.TipoMedioPago.RecuperarValor()

                            'Insere o declarado Efectivo
                            AccesoDatos.Genesis.DeclaradoMedioPago.DeclaradoMedioPagoInserir(identificadorRemesa, _
                                            identificadorBulto, _
                                            identificadorParcial, _
                                            identificadorDivisa, _
                                            identificadorMedioPago, _
                                            tipoMedioPago, _
                                            importe, _
                                            cantidad, _
                                            nivelDetalhe, _
                                            ingresado, _
                                            usuario)


                        Next
                    End If
                Next
            End If
        End Sub

        Public Shared Sub ContadoMedioRemesaInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)
            ContadoMedioPagoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

            'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
            If objRemesa.Bultos IsNot Nothing Then
                For Each objBulto In objRemesa.Bultos
                    objBulto.UsuarioModificacion = objRemesa.UsuarioModificacion
                    'Insere os efectivo do bulto.
                    ContadoMedioPagoBultoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
                Next
            End If
        End Sub

        Public Shared Sub ContadoMedioPagoBultoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
            ContadoMedioPagoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

            If objBulto.Parciales IsNot Nothing Then
                For Each objParcial In objBulto.Parciales
                    'se o bulto tiver parcial então insere declarado da parcial.
                    ContadoMedioPagoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objParcial.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
                Next
            End If
        End Sub

        Private Shared Sub ContadoMedioPagoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
            Dim identificadorMedioPago As String, _
                    tipoContado As String, _
                    importe As Decimal, _
                    cantidad As Long, _
                    nivelDetalhe As String, _
                    secuencia As Integer

            If listaDivisas IsNot Nothing Then

                For Each divisa In listaDivisas
                    identificadorMedioPago = String.Empty

                    'se existe valores então 
                    If divisa.MediosPago IsNot Nothing Then
                        'recupera somente registros com valores declarados
                        For Each medioPago In divisa.MediosPago.Where(Function(d) d.Valores IsNot Nothing AndAlso d.Valores.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Contado))
                            identificadorMedioPago = medioPago.Identificador

                            For Each valor In medioPago.Valores.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Contado)
                                importe = valor.Importe
                                cantidad = valor.Cantidad
                                tipoContado = valor.InformadoPor.RecuperarValor()
                                nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()

                                Dim identificadorContadoMedioPago = AccesoDatos.Genesis.ContadoMedioPago.ContadoMedioPagoInserir(identificadorRemesa, _
                                            identificadorBulto, _
                                            identificadorParcial, _
                                            identificadorMedioPago, _
                                            tipoContado, _
                                            importe, _
                                            cantidad, _
                                            nivelDetalhe, _
                                            secuencia, _
                                            usuario)

                                If valor.Terminos IsNot Nothing Then
                                    LogicaNegocio.Genesis.TerminoIAC.ValorTerminoMedioPagoElementoInserir(valor, identificadorContadoMedioPago, Enumeradores.TipoValor.Contado, usuario)
                                End If
                            Next
                        Next
                    End If
                Next
            End If
        End Sub

        Public Shared Sub DiferenciaMedioPagoRemesaInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)
            DiferenciaMedioPagoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

            'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
            If objRemesa.Bultos IsNot Nothing Then
                For Each objBulto In objRemesa.Bultos
                    objBulto.UsuarioModificacion = objRemesa.UsuarioModificacion
                    'Insere os efectivo do bulto.
                    DiferenciaMedioPagoBultoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
                Next
            End If
        End Sub

        Public Shared Sub DiferenciaMedioPagoBultoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
            DiferenciaMedioPagoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

            If objBulto.Parciales IsNot Nothing Then
                For Each objParcial In objBulto.Parciales
                    objParcial.UsuarioModificacion = objBulto.UsuarioModificacion
                    'se o bulto tiver parcial então insere declarado da parcial.
                    DiferenciaMedioPagoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objParcial.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
                Next
            End If
        End Sub

        Private Shared Sub DiferenciaMedioPagoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
            Dim identificadorDivisa As String = String.Empty,
                identificadorMedioPago As String = String.Empty,
                tipoMedioPago As String = String.Empty,
                importe As Decimal,
                cantidad As Decimal,
                nivelDetalhe As String = String.Empty

            If listaDivisas IsNot Nothing Then

                For Each divisa In listaDivisas
                    identificadorMedioPago = String.Empty
                    identificadorDivisa = divisa.Identificador

                    'se existe valores então 
                    If divisa.MediosPago IsNot Nothing Then
                        'recupera somente registros com valores declarados
                        For Each medioPago In divisa.MediosPago.Where(Function(d) d.Valores IsNot Nothing AndAlso d.Valores.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia))
                            identificadorMedioPago = medioPago.Identificador
                            tipoMedioPago = medioPago.Tipo.RecuperarValor()

                            For Each valor In medioPago.Valores.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia)
                                importe = valor.Importe
                                cantidad = valor.Cantidad
                                nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()

                                'Insere a diferencia do medio pago
                                AccesoDatos.Genesis.DiferenciaMedioPago.DiferenciaMedioPagoInserir(identificadorRemesa, _
                                                  identificadorBulto, _
                                                  identificadorParcial, _
                                                  identificadorDivisa, _
                                                  identificadorMedioPago, _
                                                  tipoMedioPago, _
                                                  importe, _
                                                  cantidad, _
                                                  nivelDetalhe, _
                                                  usuario)
                            Next
                        Next
                    End If

                    'se existe valores então 
                    If divisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
                        'recupera somente registros com valores declarados
                        For Each valor In divisa.ValoresTotalesTipoMedioPago.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia)
                            identificadorMedioPago = String.Empty
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
                            importe = valor.Importe
                            cantidad = valor.Cantidad
                            tipoMedioPago = valor.TipoMedioPago.RecuperarValor()

                            'Insere a diferencia do medio pago
                            AccesoDatos.Genesis.DiferenciaMedioPago.DiferenciaMedioPagoInserir(identificadorRemesa, _
                                                identificadorBulto, _
                                                identificadorParcial, _
                                                identificadorDivisa, _
                                                identificadorMedioPago, _
                                                tipoMedioPago, _
                                                importe, _
                                                cantidad, _
                                                nivelDetalhe, _
                                                usuario)

                        Next
                    End If
                Next
            End If
        End Sub
    End Class

End Namespace