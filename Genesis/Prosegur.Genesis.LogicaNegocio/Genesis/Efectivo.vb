Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Data

Namespace Genesis

    Public Class Efectivo


        ''' <summary>
        ''' Valida os campos obrigatórios de Remessa.
        ''' </summary>
        ''' <param name="remesa">Objeto remessa preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValidarRemesaDeclaradoEfectivo(remesa As Clases.Remesa, usuario As String)
            If remesa Is Nothing Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_remesa_vazio"))
            End If

            If String.IsNullOrEmpty(remesa.Identificador) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_remesa_obrigatorio"))
            End If

            If remesa.Divisas Is Nothing OrElse remesa.Divisas.Count = 0 Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_divisa_obrigatorio"))
            End If

            If String.IsNullOrEmpty(usuario) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_usuario_creacion_obrigatorio"))
            End If

            If String.IsNullOrEmpty(usuario) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_usuario_modification_obrigatorio"))
            End If

            ' TODO: Unidade de Medida


            'Verifica se foi informado algum valor para Denominacion, Efeftivo ou Total de divisas.
            If remesa.Divisas.Exists(Function(d) (d.Denominaciones Is Nothing OrElse d.Denominaciones.Count = 0)) AndAlso _
                remesa.Divisas.Exists(Function(d) (d.ValoresTotalesDivisa Is Nothing OrElse d.ValoresTotalesDivisa.Count = 0)) AndAlso _
                remesa.Divisas.Exists(Function(d) (d.ValoresTotalesEfectivo Is Nothing OrElse d.ValoresTotalesEfectivo.Count = 0)) Then

                'Verifica se algum valor foi informado no bulto ou parcial
                If remesa.Bultos Is Nothing OrElse remesa.Bultos.Count = 0 Then
                    Throw New Excepcion.CampoObrigatorioException(Traduzir("028_denomincion_ou_total_divisa_obrigatorio"))
                End If

                For Each Bulto In remesa.Bultos
                    ValidarBultoDeclaradoEfectivo(Bulto, usuario)
                Next
            End If

        End Sub

        ''' <summary>
        ''' Valida os campos obrigatórios de Bulto.
        ''' </summary>
        ''' <param name="bulto">Objeto bulto preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValidarBultoDeclaradoEfectivo(bulto As Clases.Bulto, usuario As String)

            If bulto Is Nothing Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_bulto_vazio"))
            End If

            If String.IsNullOrEmpty(bulto.Identificador) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_bulto_obrigatorio"))
            End If

            If bulto.Divisas Is Nothing OrElse bulto.Divisas.Count = 0 Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_divisa_obrigatorio"))
            End If

            If String.IsNullOrEmpty(usuario) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_usuario_creacion_obrigatorio"))
            End If

            If String.IsNullOrEmpty(usuario) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_usuario_modification_obrigatorio"))
            End If

            ' TODO: Unidade de Medida

            'Verifica se foi informado algum valor para Denominacion, Efeftivo ou Total de divisas.
            If bulto.Divisas.Exists(Function(d) (d.Denominaciones Is Nothing OrElse d.Denominaciones.Count = 0)) AndAlso _
                bulto.Divisas.Exists(Function(d) (d.ValoresTotalesDivisa Is Nothing OrElse d.ValoresTotalesDivisa.Count = 0)) AndAlso _
                bulto.Divisas.Exists(Function(d) (d.ValoresTotalesEfectivo Is Nothing OrElse d.ValoresTotalesEfectivo.Count = 0)) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_denomincion_ou_total_divisa_obrigatorio"))

                'Verifica se algum valor foi informado na parcial
                If bulto.Parciales Is Nothing OrElse bulto.Parciales.Count = 0 Then
                    Throw New Excepcion.CampoObrigatorioException(Traduzir("028_denomincion_ou_total_divisa_obrigatorio"))
                End If

                For Each parcial In bulto.Parciales
                    ValidarParcialDeclaradoEfectivo(parcial, usuario)
                Next
            End If
        End Sub

        ''' <summary>
        ''' Valida os campos obrigatórios de Parcial.
        ''' </summary>
        ''' <param name="parcial">Objeto parcial preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValidarParcialDeclaradoEfectivo(parcial As Clases.Parcial, usuario As String)
            If parcial Is Nothing Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_parcial_vazio"))
            End If

            If String.IsNullOrEmpty(parcial.Identificador) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_parcial_obrigatorio"))
            End If

            If parcial.Divisas Is Nothing OrElse parcial.Divisas.Count = 0 Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_divisa_obrigatorio"))
            End If

            If String.IsNullOrEmpty(usuario) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_usuario_creacion_obrigatorio"))
            End If

            If String.IsNullOrEmpty(usuario) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_usuario_modification_obrigatorio"))
            End If

            ' TODO: Unidade de Medida

            'Verifica se foi informado algum valor para Denominacion, Efeftivo ou Total de divisas.
            If parcial.Divisas.Exists(Function(d) (d.Denominaciones Is Nothing OrElse d.Denominaciones.Count = 0)) AndAlso _
                parcial.Divisas.Exists(Function(d) (d.ValoresTotalesDivisa Is Nothing OrElse d.ValoresTotalesDivisa.Count = 0)) AndAlso _
                parcial.Divisas.Exists(Function(d) (d.ValoresTotalesEfectivo Is Nothing OrElse d.ValoresTotalesEfectivo.Count = 0)) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_denomincion_ou_total_divisa_obrigatorio"))
            End If
        End Sub

        ''' <summary>
        ''' Valida os campos obrigatórios de Parcial.
        ''' </summary>
        ''' <param name="listaDivisas">Objeto lista de divisas.</param>
        ''' <remarks></remarks>
        Private Shared Sub DeclaradoEfectivoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
            Dim identificadorDivisa As String = String.Empty,
                identificadorDenominacion As String = String.Empty,
                identificadorUnidadMedida As String = String.Empty,
                tipoEfectivoTotal As String = String.Empty,
                importe As Decimal = 0,
                cantidad As Long = 0,
                nivelDetalhe As String = String.Empty,
                ingresado As Boolean

            If listaDivisas IsNot Nothing AndAlso listaDivisas.Count > 0 Then

                For Each divisa In listaDivisas
                    identificadorDenominacion = String.Empty
                    identificadorDivisa = divisa.Identificador

                    'se existe valores então 
                    If divisa.Denominaciones IsNot Nothing Then
                        'recupera somente registros com valores declarados
                        For Each Denominacion In divisa.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado))
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()
                            tipoEfectivoTotal = String.Empty
                            identificadorDenominacion = Denominacion.Identificador

                            For Each valor In Denominacion.ValorDenominacion.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado AndAlso v.Importe <> 0)
                                importe = valor.Importe
                                cantidad = valor.Cantidad

                                If valor.UnidadMedida IsNot Nothing Then
                                    identificadorUnidadMedida = valor.UnidadMedida.Identificador
                                Else
                                    'Recupera a unidade de medida padrao
                                    Dim unidadMedida = AccesoDatos.Genesis.UnidadMedida.RecuperarUnidadMedida(True, String.Empty, _
                                                  If(Denominacion.EsBillete, Enumeradores.TipoUnidadMedida.Billete, Enumeradores.TipoUnidadMedida.Moneda))
                                    If unidadMedida IsNot Nothing Then
                                        identificadorUnidadMedida = unidadMedida.Identificador
                                    End If
                                End If

                                'Insere o declarado Efectivo
                                AccesoDatos.Genesis.DeclaradoEfectivo.DeclaradoEfectivoInserir(identificadorRemesa, _
                                                                                               identificadorBulto, _
                                                                                               identificadorParcial, _
                                                                                               identificadorDivisa, _
                                                                                               identificadorDenominacion, _
                                                                                               identificadorUnidadMedida, _
                                                                                               tipoEfectivoTotal, _
                                                                                               importe, _
                                                                                               cantidad, _
                                                                                               nivelDetalhe, _
                                                                                               ingresado, _
                                                                                               usuario)

                            Next
                        Next
                    End If

                    'se existe valores total de efectivo somente valores declarados
                    If divisa.ValoresTotalesEfectivo IsNot Nothing Then
                        cantidad = 0
                        identificadorUnidadMedida = String.Empty
                        For Each valor In divisa.ValoresTotalesEfectivo.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado AndAlso v.Importe <> 0)
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
                            tipoEfectivoTotal = valor.TipoDetalleEfectivo.RecuperarValor()
                            identificadorDenominacion = String.Empty
                            importe = valor.Importe

                            'Insere o declarado Efectivo
                            AccesoDatos.Genesis.DeclaradoEfectivo.DeclaradoEfectivoInserir(identificadorRemesa, _
                                                identificadorBulto, _
                                                identificadorParcial, _
                                                identificadorDivisa, _
                                                identificadorDenominacion, _
                                                identificadorUnidadMedida, _
                                                tipoEfectivoTotal, _
                                                importe, _
                                                cantidad, _
                                                nivelDetalhe, _
                                                ingresado, _
                                                usuario)

                        Next
                    End If

                    'se existe valores total de divisas
                    If divisa.ValoresTotalesDivisa IsNot Nothing Then
                        cantidad = 0
                        identificadorUnidadMedida = String.Empty
                        For Each valor In divisa.ValoresTotalesDivisa.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado AndAlso v.Importe <> 0)
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor()
                            tipoEfectivoTotal = String.Empty
                            identificadorDenominacion = String.Empty

                            importe = valor.Importe

                            'Insere o declarado Efectivo
                            AccesoDatos.Genesis.DeclaradoEfectivo.DeclaradoEfectivoInserir(identificadorRemesa, _
                                             identificadorBulto, _
                                              identificadorParcial, _
                                              identificadorDivisa, _
                                              identificadorDenominacion, _
                                              identificadorUnidadMedida, _
                                              tipoEfectivoTotal, _
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

        Public Shared Sub DeclaradoEfectivoRemesaInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)

            DeclaradoEfectivoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

            'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                For Each objBulto In objRemesa.Bultos.Where(Function(a) (a.Divisas IsNot Nothing AndAlso a.Divisas.Count > 0) OrElse (a.Parciales IsNot Nothing AndAlso a.Parciales.Count > 0))
                    objBulto.UsuarioModificacion = objRemesa.UsuarioModificacion
                    'Insere os efectivo do bulto.
                    DeclaradoEfectivoBultoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
                Next
            End If
        End Sub

        Public Shared Sub DeclaradoEfectivoBultoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
            DeclaradoEfectivoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

            If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
                For Each objParcial In objBulto.Parciales.Where(Function(a) (a.Divisas IsNot Nothing AndAlso a.Divisas.Count > 0))
                    'se o bulto tiver parcial então insere declarado da parcial.
                    DeclaradoEfectivoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
                Next
            End If
        End Sub

        Public Shared Sub ContadoEfectivoRemesaInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)
            ContadoEfectivoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

            'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                For Each objBulto In objRemesa.Bultos
                    objBulto.UsuarioModificacion = objBulto.UsuarioModificacion
                    'Insere os efectivo do bulto.
                    ContadoEfectivoBultoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
                Next
            End If
        End Sub

        Public Shared Sub ContadoEfectivoBultoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
            ContadoEfectivoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

            If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
                For Each objParcial In objBulto.Parciales
                    objParcial.UsuarioModificacion = objBulto.UsuarioModificacion
                    'se o bulto tiver parcial então insere declarado da parcial.
                    ContadoEfectivoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objParcial.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
                Next
            End If
        End Sub

        Private Shared Sub ContadoEfectivoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
            Dim identificadorDenominacion As String, _
                identificadorUnidadMedida As String, _
                tipoContado As String = String.Empty, _
                importe As Decimal, _
                cantidad As Decimal = 0, _
                identificadorCalidad As String = String.Empty, _
                unidadeMedidaPadron As String = String.Empty

            If listaDivisas IsNot Nothing AndAlso listaDivisas.Count > 0 Then

                'Recupera a unidade de medida padrao
                Dim unidadMedida = AccesoDatos.Genesis.UnidadMedida.RecuperarUnidadMedida(True)
                If unidadMedida IsNot Nothing Then
                    unidadeMedidaPadron = unidadMedida.Identificador
                End If

                For Each divisa In listaDivisas
                    identificadorDenominacion = String.Empty

                    'se existe valores então 
                    If divisa.Denominaciones IsNot Nothing Then
                        'recupera somente registros com valores declarados
                        For Each Denominacion In divisa.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Contado))
                            identificadorDenominacion = Denominacion.Identificador

                            For Each valor In Denominacion.ValorDenominacion.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Contado)
                                importe = valor.Importe
                                cantidad = valor.Cantidad

                                If valor.Calidad IsNot Nothing Then
                                    identificadorCalidad = valor.Calidad.Identificador
                                Else
                                    identificadorCalidad = String.Empty
                                End If

                                tipoContado = valor.InformadoPor.RecuperarValor()

                                If valor.UnidadMedida IsNot Nothing Then
                                    identificadorUnidadMedida = valor.UnidadMedida.Identificador
                                Else
                                    identificadorUnidadMedida = unidadeMedidaPadron
                                End If

                                'Insere o declarado Efectivo
                                AccesoDatos.Genesis.ContadoEfectivo.ContadoEfectivoInserir(identificadorRemesa, _
                                            identificadorBulto, _
                                            identificadorParcial, _
                                            identificadorDenominacion, _
                                            identificadorUnidadMedida, _
                                            tipoContado, _
                                            importe, _
                                            cantidad, _
                                            identificadorCalidad, _
                                            usuario)
                            Next
                        Next
                    End If
                Next
            End If
        End Sub

        Public Shared Sub DiferenciaEfectivoRemesaInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)
            DiferenciaEfectivoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

            'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                For Each objBulto In objRemesa.Bultos
                    objBulto.UsuarioModificacion = objRemesa.UsuarioModificacion
                    'Insere os efectivo do bulto.
                    DiferenciaEfectivoBultoInserir(estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
                Next
            End If
        End Sub

        Public Shared Sub DiferenciaEfectivoBultoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
            DiferenciaEfectivoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

            If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
                For Each objParcial In objBulto.Parciales
                    objParcial.UsuarioModificacion = objBulto.UsuarioModificacion
                    'se o bulto tiver parcial então insere declarado da parcial.
                    DiferenciaEfectivoInserir(estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objParcial.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
                Next
            End If
        End Sub

        Private Shared Sub DiferenciaEfectivoInserir(estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
            Dim identificadorDivisa As String = String.Empty,
                identificadorDenominacion As String = String.Empty,
                identificadorUnidadMedida As String = String.Empty,
                tipoEfectivoTotal As String = String.Empty,
                importe As Decimal,
                cantidad As Decimal,
                nivelDetalhe As String,
                unidadeMedidaPadron As String = String.Empty

            If listaDivisas IsNot Nothing AndAlso listaDivisas.Count > 0 Then

                'Recupera a unidade de medida padrao
                Dim unidadMedida = AccesoDatos.Genesis.UnidadMedida.RecuperarUnidadMedida(True)
                If unidadMedida IsNot Nothing Then
                    unidadeMedidaPadron = unidadMedida.Identificador
                End If

                For Each divisa In listaDivisas
                    identificadorDenominacion = String.Empty
                    identificadorDivisa = divisa.Identificador

                    'se existe valores então 
                    If divisa.Denominaciones IsNot Nothing Then
                        'recupera somente registros com valores declarados
                        For Each Denominacion In divisa.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia))
                            identificadorDenominacion = Denominacion.Identificador

                            For Each valor In Denominacion.ValorDenominacion.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia)
                                importe = valor.Importe
                                cantidad = valor.Cantidad
                                tipoEfectivoTotal = String.Empty
                                nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()

                                If valor.UnidadMedida IsNot Nothing Then
                                    identificadorUnidadMedida = valor.UnidadMedida.Identificador
                                Else
                                    identificadorUnidadMedida = unidadeMedidaPadron
                                End If

                                'Insere o declarado Efectivo
                                AccesoDatos.Genesis.DiferenciaEfectivo.DiferenciaEfectivoInserir(identificadorRemesa, _
                                                  identificadorBulto, _
                                                  identificadorParcial, _
                                                  identificadorDivisa, _
                                                  identificadorDenominacion, _
                                                  identificadorUnidadMedida, _
                                                  tipoEfectivoTotal, _
                                                  importe, _
                                                  cantidad, _
                                                  nivelDetalhe, _
                                                  usuario)
                            Next
                        Next
                    End If

                    'se existe valores total de efectivo somente valores declarados
                    If divisa.ValoresTotalesEfectivo IsNot Nothing Then
                        cantidad = 0
                        identificadorUnidadMedida = String.Empty
                        For Each valor In divisa.ValoresTotalesEfectivo.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia)
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
                            tipoEfectivoTotal = valor.TipoDetalleEfectivo.RecuperarValor()
                            identificadorDenominacion = String.Empty
                            importe = valor.Importe

                            'Insere o declarado Efectivo
                            AccesoDatos.Genesis.DiferenciaEfectivo.DiferenciaEfectivoInserir(identificadorRemesa, _
                                                  identificadorBulto, _
                                                  identificadorParcial, _
                                                  identificadorDivisa, _
                                                  identificadorDenominacion, _
                                                  identificadorUnidadMedida, _
                                                  tipoEfectivoTotal, _
                                                  importe, _
                                                  cantidad, _
                                                  nivelDetalhe, _
                                                  usuario)

                        Next
                    End If

                    'se existe valores total de divisas
                    If divisa.ValoresTotalesDivisa IsNot Nothing Then
                        cantidad = 0
                        identificadorUnidadMedida = String.Empty
                        For Each valor In divisa.ValoresTotalesDivisa.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia)
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor()
                            tipoEfectivoTotal = String.Empty
                            identificadorDenominacion = String.Empty
                            importe = valor.Importe

                            'Insere o declarado Efectivo
                            AccesoDatos.Genesis.DiferenciaEfectivo.DiferenciaEfectivoInserir(identificadorRemesa, _
                                                  identificadorBulto, _
                                                  identificadorParcial, _
                                                  identificadorDivisa, _
                                                  identificadorDenominacion, _
                                                  identificadorUnidadMedida, _
                                                  tipoEfectivoTotal, _
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