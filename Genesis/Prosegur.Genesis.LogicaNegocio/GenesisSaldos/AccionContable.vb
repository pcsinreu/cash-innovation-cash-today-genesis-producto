Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Transactions
Imports Prosegur.Genesis.AccesoDatos
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel

Namespace GenesisSaldos

    ''' <summary>
    ''' Clase AccionContable
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 05/09/2013 - Criado
    ''' </history>
    Public Class AccionContable

        Public Shared Function ObtenerAccionesContables() As List(Of Clases.AccionContable)
            Dim listaAccionContable As List(Of Clases.AccionContable)
            Try
                listaAccionContable = AccesoDatos.GenesisSaldos.AccionContable.ObtenerAccionesContables()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return listaAccionContable
        End Function

        Public Shared Function ObtenerAccionContable(identificador As String) As Clases.AccionContable
            Dim accionContable As Clases.AccionContable
            Try
                accionContable = AccesoDatos.GenesisSaldos.AccionContable.ObtenerAccionContable(identificador)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return accionContable
        End Function

        Public Shared Function ObtenerAccionContablePorCodigo(codigo As String) As Clases.AccionContable
            Dim accionContable As Clases.AccionContable
            Try
                accionContable = AccesoDatos.GenesisSaldos.AccionContable.ObtenerAccionContablePorCodigo(codigo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return accionContable
        End Function

        Public Shared Function ObtenerAccionesContablesSoloConEstadoAceptado() As List(Of Clases.AccionContable)
            Dim listaAccionContable As List(Of Clases.AccionContable)
            Try
                listaAccionContable = AccesoDatos.GenesisSaldos.AccionContable.ObtenerAccionesContablesSoloConEstadoAceptado()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return listaAccionContable
        End Function

        Public Shared Sub GuardarAccionContable(accionContable As Clases.AccionContable)
            Try
                ValidarObrigatorioAccionContable(accionContable)

                AccesoDatos.GenesisSaldos.AccionContable.GuardarAccionContable(accionContable)

            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Valida os campos obrigatórios da accion contable.
        ''' </summary>
        ''' <param name="accionContable">Objeto accion contable preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValidarObrigatorioAccionContable(accionContable As Clases.AccionContable)

            Dim erros As New System.Text.StringBuilder
            If accionContable Is Nothing Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("046_Accion_Contable_Vazio"))
            End If

            If String.IsNullOrEmpty(accionContable.Identificador) Then
                erros.AppendLine(Traduzir("046_Identificador_Accion_Contable_Obrigatorio"))
            End If

            If String.IsNullOrEmpty(accionContable.Codigo) Then
                erros.AppendLine(Traduzir("046_Codigo_Accion_Contable_Obrigatorio"))
            End If

            If String.IsNullOrEmpty(accionContable.Descripcion) Then
                erros.AppendLine(Traduzir("046_Descripcion_Accion_Contable_Obrigatorio"))
            End If


            If String.IsNullOrEmpty(accionContable.UsuarioCreacion) Then
                erros.AppendLine(Traduzir("046_Usuario_Cri_Accion_Contable_Obrigatorio"))
            End If

            If String.IsNullOrEmpty(accionContable.FechaHoraCreacion) Then
                erros.AppendLine(Traduzir("046_Fecha_Creacion_Obrigatorio"))
            End If

            If String.IsNullOrEmpty(accionContable.FechaHoraModificacion) Then
                erros.AppendLine(Traduzir("046_Fecha_Modificacion_Obrigatorio"))
            End If

            If String.IsNullOrEmpty(accionContable.UsuarioModificacion) Then
                erros.AppendLine(Traduzir("046_Usuario_Mod_Accion_Contable_Obrigatorio"))
            End If

            If erros.Length > 0 Then
                Throw New Excepcion.CampoObrigatorioException(erros.ToString)
            End If
        End Sub


        ''' <summary>
        ''' Executa a ação contabil para o documento informado.
        ''' </summary>
        ''' <param name="documento"></param>
        ''' <remarks></remarks>
        Public Shared Sub EejecutarAccionContableEfectivo(documento As Clases.Documento, Optional usuario As String = "")

            Dim identificadorDivisa As String = String.Empty,
                identificadorDenominacion As String = String.Empty,
                identificadorUnidadMedida As String = String.Empty,
                codigoNivelDetalhe As String = String.Empty,
                codigoTipoEfectivoTotal As String = String.Empty,
                identificadorCalidad As String = String.Empty,
                numImporte As Decimal = 0,
                cantidad As Int64,
                codigoMigracion As String = String.Empty,
                unidadMedidaPadron As String = String.Empty

            Dim permiteLlegarSaldoNegativo As Boolean = False

            If documento IsNot Nothing AndAlso documento.Formulario IsNot Nothing AndAlso documento.Formulario.Caracteristicas IsNot Nothing AndAlso documento.Formulario.Caracteristicas.Count > 0 Then
                permiteLlegarSaldoNegativo = documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo)
            Else
                Throw New Excepcion.NegocioExcepcion(Traduzir("028_msg_formulario_no_cargado_correctamente"))
            End If

            If documento.Formulario IsNot Nothing AndAlso documento.Formulario.AccionContable IsNot Nothing _
                                                    AndAlso documento.Formulario.AccionContable.EstaActivo _
                                                    AndAlso documento.Formulario.AccionContable.Acciones IsNot Nothing Then

                'Dim divisas = LogicaNegocio.GenesisSaldos.Saldos.RecuperarEfectivoPorDocumento(documento.Identificador)
                If documento.Divisas IsNot Nothing Then

                    If String.IsNullOrEmpty(usuario) AndAlso Not String.IsNullOrEmpty(documento.UsuarioModificacion) Then
                        usuario = documento.UsuarioModificacion
                    End If

                    Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted})

                        'Recupera a unidade de medida padrao
                        Dim unidadMedida = AccesoDatos.Genesis.UnidadMedida.RecuperarUnidadMedida(True)
                        If unidadMedida IsNot Nothing Then
                            unidadMedidaPadron = unidadMedida.Identificador
                        End If

                        '' Só da o commit se todoas as transaciones ocorrem com sucesso.

                        ' Recupera os valores do efectivo de documento que ainda não foram para a tabela de transação para o estado desse documento
                        ' Se já existe alguma transação para esse documento então será feito o estorno invertendo a ação anterior.
                        For Each accionTransacion In documento.Formulario.AccionContable.Acciones.Where(Function(t) t.Estado = documento.Estado)
                            Dim objTransaccion As New Clases.Transaccion
                            objTransaccion.Documento = documento
                            objTransaccion.EstadoDocumento = documento.Estado
                            objTransaccion.TipoSitio = accionTransacion.TipoSitio
                            objTransaccion.TipoSaldo = accionTransacion.TipoSaldo
                            objTransaccion.TipoMovimiento = accionTransacion.TipoMovimiento

                            codigoMigracion = String.Empty

                            For Each divisa In documento.Divisas
                                identificadorDivisa = divisa.Identificador

                                'para cada divisa recuperar o valor de cada tiem de 
                                'Denominaciones
                                If divisa.Denominaciones IsNot Nothing Then
                                    For Each Denominacion In divisa.Denominaciones
                                        identificadorDenominacion = Denominacion.Identificador
                                        codigoNivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor
                                        codigoTipoEfectivoTotal = String.Empty

                                        For Each valor In Denominacion.ValorDenominacion

                                            If valor.Calidad IsNot Nothing Then
                                                identificadorCalidad = valor.Calidad.Identificador
                                            Else
                                                identificadorCalidad = String.Empty
                                            End If

                                            numImporte = valor.Importe
                                            cantidad = valor.Cantidad

                                            If valor.UnidadMedida IsNot Nothing AndAlso Not String.IsNullOrEmpty(valor.UnidadMedida.Identificador) Then
                                                identificadorUnidadMedida = valor.UnidadMedida.Identificador
                                            Else
                                                identificadorUnidadMedida = unidadMedidaPadron
                                            End If

                                            If accionTransacion.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso Then
                                                'coloca o valor como negativo.
                                                numImporte = numImporte * (-1)
                                                cantidad = cantidad * (-1)
                                            End If

                                            'Insere o efectivo de documento
                                            AccesoDatos.GenesisSaldos.TransaccionEfectivo.TransaccionEfectivoInserir(permiteLlegarSaldoNegativo, objTransaccion, _
                                                                        identificadorDivisa, _
                                                                        identificadorDenominacion, _
                                                                        identificadorUnidadMedida, _
                                                                        codigoNivelDetalhe, _
                                                                        codigoTipoEfectivoTotal, _
                                                                        identificadorCalidad, _
                                                                        numImporte, _
                                                                        cantidad, _
                                                                        codigoMigracion, _
                                                                        usuario)

                                        Next
                                    Next
                                End If

                                'se existe valores total de efectivo
                                If divisa.ValoresTotalesEfectivo IsNot Nothing Then
                                    For Each valor In divisa.ValoresTotalesEfectivo
                                        codigoNivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor
                                        codigoTipoEfectivoTotal = valor.TipoDetalleEfectivo.RecuperarValor()

                                        identificadorDenominacion = String.Empty
                                        identificadorUnidadMedida = String.Empty
                                        identificadorCalidad = String.Empty
                                        numImporte = valor.Importe
                                        cantidad = 0

                                        If accionTransacion.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso Then
                                            'coloca o valor como negativo.
                                            numImporte = numImporte * (-1)
                                        End If

                                        'Insere o efectivo de documento
                                        AccesoDatos.GenesisSaldos.TransaccionEfectivo.TransaccionEfectivoInserir(permiteLlegarSaldoNegativo, objTransaccion, _
                                                                 identificadorDivisa, _
                                                                 identificadorDenominacion, _
                                                                 identificadorUnidadMedida, _
                                                                 codigoNivelDetalhe, _
                                                                 codigoTipoEfectivoTotal, _
                                                                 identificadorCalidad, _
                                                                 numImporte, _
                                                                 cantidad, _
                                                                 codigoMigracion, _
                                                                 usuario)
                                    Next
                                End If

                                'se existe valores total de divisas
                                If divisa.ValoresTotalesDivisa IsNot Nothing Then
                                    For Each valor In divisa.ValoresTotalesDivisa
                                        codigoNivelDetalhe = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor
                                        codigoTipoEfectivoTotal = String.Empty
                                        identificadorDenominacion = String.Empty
                                        identificadorUnidadMedida = String.Empty
                                        identificadorCalidad = String.Empty
                                        numImporte = valor.Importe

                                        'TODO: Verificar cantidad quando for total de divisa.
                                        cantidad = 0

                                        If accionTransacion.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso Then
                                            'coloca o valor como negativo.
                                            numImporte = numImporte * (-1)
                                        End If

                                        'Insere o efectivo de documento
                                        AccesoDatos.GenesisSaldos.TransaccionEfectivo.TransaccionEfectivoInserir(permiteLlegarSaldoNegativo, objTransaccion, _
                                                                    identificadorDivisa, _
                                                                    identificadorDenominacion, _
                                                                    identificadorUnidadMedida, _
                                                                    codigoNivelDetalhe, _
                                                                    codigoTipoEfectivoTotal, _
                                                                    identificadorCalidad, _
                                                                    numImporte, _
                                                                    cantidad, _
                                                                    codigoMigracion, _
                                                                    usuario)
                                    Next
                                End If
                            Next
                        Next

                        Transaction.Complete()
                    End Using
                End If
            End If
        End Sub

        ''' <summary>
        ''' Executa a ação contabil para o documento informado.
        ''' </summary>
        ''' <param name="documento"></param>
        ''' <remarks></remarks>
        Public Shared Sub EejecutarAccionContableMedioPago(documento As Clases.Documento, Optional usuario As String = "")
            Dim identificadorDivisa As String = String.Empty,
                identificadorMedioPago As String = String.Empty,
                codigoTipoMedioPago As String = String.Empty,
                codigoNivelDetalhe As String = String.Empty,
                numImporte As Decimal = 0,
                cantidad As Int64,
                codigoMigracion As String = String.Empty

            Dim permiteLlegarSaldoNegativo As Boolean = False

            If documento IsNot Nothing AndAlso documento.Formulario IsNot Nothing AndAlso documento.Formulario.Caracteristicas IsNot Nothing AndAlso documento.Formulario.Caracteristicas.Count > 0 Then
                permiteLlegarSaldoNegativo = documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo)
            Else
                Throw New Excepcion.NegocioExcepcion(Traduzir("028_msg_formulario_no_cargado_correctamente"))
            End If

            If documento.Formulario IsNot Nothing AndAlso documento.Formulario.AccionContable IsNot Nothing _
                                                    AndAlso documento.Formulario.AccionContable.EstaActivo _
                                                    AndAlso documento.Formulario.AccionContable.Acciones IsNot Nothing Then

                'Dim divisas = LogicaNegocio.GenesisSaldos.Saldos.RecuperarMedioPagoPorDocumento(documento.Identificador)

                If documento.Divisas IsNot Nothing Then

                    If String.IsNullOrEmpty(usuario) AndAlso Not String.IsNullOrEmpty(documento.UsuarioModificacion) Then
                        usuario = documento.UsuarioModificacion
                    End If

                    Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted})

                        ' Recupera os valores do efectivo de documento que ainda não foram para a tabela de transação para o estado desse documento
                        ' Se já existe alguma transação para esse documento então será feito o estorno invertendo a ação anterior.
                        For Each accionTransacion In documento.Formulario.AccionContable.Acciones.Where(Function(t) t.Estado = documento.Estado)
                            Dim objTransaccion As New Clases.Transaccion
                            objTransaccion.Documento = documento
                            objTransaccion.EstadoDocumento = documento.Estado
                            objTransaccion.TipoSitio = accionTransacion.TipoSitio
                            objTransaccion.TipoSaldo = accionTransacion.TipoSaldo
                            objTransaccion.TipoMovimiento = accionTransacion.TipoMovimiento

                            For Each divisa In documento.Divisas
                                identificadorDivisa = divisa.Identificador

                                'para cada divisa recuperar o valor de cada tiem de 
                                'Denominaciones
                                If divisa.MediosPago IsNot Nothing Then
                                    For Each medioPago In divisa.MediosPago
                                        identificadorMedioPago = medioPago.Identificador
                                        codigoNivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor
                                        codigoTipoMedioPago = medioPago.Tipo.RecuperarValor

                                        For Each valor In medioPago.Valores
                                            numImporte = valor.Importe
                                            cantidad = valor.Cantidad


                                            If accionTransacion.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso Then
                                                'coloca o valor como negativo.
                                                numImporte = numImporte * (-1)
                                                cantidad = cantidad * (-1)
                                            End If

                                            'Insere a transaccion do mediopago de documento
                                            AccesoDatos.GenesisSaldos.TransaccionMedioPago.TransaccionMedioPagoInserir(permiteLlegarSaldoNegativo, objTransaccion, _
                                                            identificadorDivisa, _
                                                            identificadorMedioPago, _
                                                            codigoNivelDetalhe, _
                                                            codigoTipoMedioPago, _
                                                            numImporte, _
                                                            cantidad, _
                                                            codigoMigracion, _
                                                            usuario)

                                        Next
                                    Next
                                End If

                                'se existe valores total de efectivo
                                If divisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
                                    For Each valor In divisa.ValoresTotalesTipoMedioPago
                                        codigoNivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor
                                        identificadorMedioPago = String.Empty
                                        codigoTipoMedioPago = valor.TipoMedioPago.RecuperarValor
                                        numImporte = valor.Importe
                                        cantidad = 0

                                        If accionTransacion.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso Then
                                            'coloca o valor como negativo.
                                            numImporte = numImporte * (-1)
                                        End If

                                        'Insere a transaccion do mediopago de documento
                                        AccesoDatos.GenesisSaldos.TransaccionMedioPago.TransaccionMedioPagoInserir(permiteLlegarSaldoNegativo, objTransaccion, _
                                                        identificadorDivisa, _
                                                        identificadorMedioPago, _
                                                        codigoNivelDetalhe, _
                                                        codigoTipoMedioPago, _
                                                        numImporte, _
                                                        cantidad, _
                                                        codigoMigracion, _
                                                        usuario)

                                    Next
                                End If
                            Next
                        Next

                        transaction.Complete()
                    End Using
                End If
            End If
        End Sub

        ''' <summary>
        ''' Revertir Transaccion Efectivo
        ''' </summary>
        ''' <param name="identificadorDocumento"></param>
        ''' <param name="estadoTransacionParaEstorno"></param>
        ''' <param name="estadoTransacionEstornada"></param>
        ''' <param name="usuario"></param>
        ''' <remarks></remarks>
        Public Shared Sub RevertirTransaccionEfectivo(permiteLlegarSaldoNegativo As Boolean, identificadorDocumento As String, estadoTransacionParaEstorno As Enumeradores.EstadoDocumento, estadoTransacionEstornada As Enumeradores.EstadoDocumento, usuario As String, identificadorSectorDestino As String)
            Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted})

                'Recupera dos dados do documento 
                'Recupera as transações que serão estornadas
                Dim listaTransacciones = AccesoDatos.GenesisSaldos.TransaccionEfectivo.ObtenerTransaccionesEfectivoParaEstorno(identificadorDocumento, estadoTransacionParaEstorno.RecuperarValor)

                If listaTransacciones IsNot Nothing AndAlso listaTransacciones.Count() > 0 Then

                    ' verifica se existem transações para o destino
                    Dim existeTransaccionesDestino As Boolean = listaTransacciones.Exists(Function(t) t.TipoSitio = Enumeradores.TipoSitio.Destino)
                    If existeTransaccionesDestino Then

                        ' se existem transações para o destino, as mesmas terão de ser desfeitas (contra-movimientos)
                        For Each objTransacion In listaTransacciones.Where(Function(t) t.TipoSitio = Enumeradores.TipoSitio.Destino)

                            Dim novaTransacao = AccesoDatos.GenesisSaldos.TransaccionEfectivo.TransaccionEfectivoEstornoSustituicion(objTransacion)

                            'Atualiza o saldo da transação nova de estorno.
                            AccesoDatos.GenesisSaldos.SaldoEfectivo.SaldoEfectivoAtualizar(novaTransacao, usuario, permiteLlegarSaldoNegativo)

                        Next

                    Else

                        ' se não existem transações no destino, significa que devem existir tranções para a origem
                        ' sendo a origem igual ao parâmetro sector de origem (tendo o mesmo), elas deverão ser desfeitas
                        For Each objTransacion In listaTransacciones.Where(Function(t) t.Cuenta.Sector.Identificador.Equals(identificadorSectorDestino))

                            Dim novaTransacao = AccesoDatos.GenesisSaldos.TransaccionEfectivo.TransaccionEfectivoEstornoSustituicion(objTransacion)

                            'Atualiza o saldo da transação nova de estorno.
                            AccesoDatos.GenesisSaldos.SaldoEfectivo.SaldoEfectivoAtualizar(novaTransacao, usuario, permiteLlegarSaldoNegativo)

                        Next

                    End If

                End If

                Transaction.Complete()

            End Using
        End Sub

        ''' <summary>
        ''' Estorna as transações de Efectivo de MedioPago.
        ''' </summary>
        ''' <param name="identificadorDocumento"></param>
        ''' <param name="estadoTransacionParaEstorno"></param>
        ''' <param name="estadoTransacionEstornada"></param>
        ''' <param name="usuario"></param>
        ''' <remarks></remarks>
        Public Shared Sub RevertirTransaccionMedioPago(permiteLlegarSaldoNegativo As Boolean, identificadorDocumento As String, estadoTransacionParaEstorno As Enumeradores.EstadoDocumento, estadoTransacionEstornada As Enumeradores.EstadoDocumento, usuario As String, identificadorSectorDestino As String)
            Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted})

                'Recupera dos dados do documento 
                'Recupera as transações que não são de estorno
                Dim listaTransacciones = AccesoDatos.GenesisSaldos.TransaccionMedioPago.RecuperarTransaccionMedioPagoParaEstorno(identificadorDocumento, estadoTransacionParaEstorno.RecuperarValor)

                If listaTransacciones IsNot Nothing AndAlso listaTransacciones.Count() > 0 Then

                    ' verifica se existem transações para o destino
                    Dim existeTransaccionesDestino As Boolean = listaTransacciones.Exists(Function(t) t.TipoSitio = Enumeradores.TipoSitio.Destino)
                    If existeTransaccionesDestino Then

                        ' se existem transações para o destino, as mesmas terão de ser desfeitas (contra-movimientos)
                        For Each objTransacion In listaTransacciones.Where(Function(t) t.TipoSitio = Enumeradores.TipoSitio.Destino)

                            Dim novaTransacao = AccesoDatos.GenesisSaldos.TransaccionMedioPago.TransaccionMedioPagoEstornoSustituicion(objTransacion)

                            'Atualiza o saldo da transação nova de estorno.
                            AccesoDatos.GenesisSaldos.SaldoMedioPago.SaldoMedioPagoAtualizar(novaTransacao, usuario, permiteLlegarSaldoNegativo)

                        Next

                    Else

                        ' se não existem transações no destino, significa que devem existir tranções para a origem
                        ' sendo a origem igual ao parâmetro sector de origem (tendo o mesmo), elas deverão ser desfeitas
                        For Each objTransacion In listaTransacciones.Where(Function(t) t.Cuenta.Sector.Identificador.Equals(identificadorSectorDestino))

                            Dim novaTransacao = AccesoDatos.GenesisSaldos.TransaccionMedioPago.TransaccionMedioPagoEstornoSustituicion(objTransacion)

                            'Atualiza o saldo da transação nova de estorno.
                            AccesoDatos.GenesisSaldos.SaldoMedioPago.SaldoMedioPagoAtualizar(novaTransacao, usuario, permiteLlegarSaldoNegativo)

                        Next

                    End If

                End If

                transaction.Complete()

            End Using
        End Sub

        ''' <summary>
        ''' Reverter transações para um documento padre certificado
        ''' </summary>
        ''' <param name="documento">Documento</param>
        ''' <remarks></remarks>
        Public Shared Sub RevertirTransaccionEfectivoDocumentoPadreCertificado(permiteLlegarSaldoNegativo As Boolean, documento As Clases.Documento, usuario As String, Optional transaccionEstadoDocumento As Enumeradores.EstadoDocumento = Enumeradores.EstadoDocumento.Nuevo)
            Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted})
                'i)	Se o documento relacionado (padre) esta certificado: buscar as transações do documento para o estado “Aceptado”
                Dim listaTransacciones = AccesoDatos.GenesisSaldos.TransaccionEfectivo.ObtenerTransaccionesEfectivoParaEstorno(documento.DocumentoPadre.Identificador, Enumeradores.EstadoDocumento.Aceptado.RecuperarValor)

                If listaTransacciones IsNot Nothing AndAlso listaTransacciones.Count() > 0 Then

                    Dim transacaoEstorno As Clases.Transaccion = Nothing

                    ' verifica se existem transações para o destino
                    Dim existeTransaccionesDestino As Boolean = listaTransacciones.Exists(Function(t) t.TipoSitio = Enumeradores.TipoSitio.Destino)
                    If existeTransaccionesDestino Then

                        ' se existem transações para o destino, as mesmas terão de ser desfeitas (contra-movimientos)
                        For Each objTransacion In listaTransacciones.Where(Function(t) t.TipoSitio = Enumeradores.TipoSitio.Destino)

                            transacaoEstorno = New Clases.Transaccion
                            transacaoEstorno.TipoSitio = Enumeradores.TipoSitio.Destino
                            transacaoEstorno.Identificador = objTransacion.Identificador
                            transacaoEstorno.UsuarioCreacion = usuario
                            If Not transaccionEstadoDocumento.Equals(Enumeradores.EstadoDocumento.Nuevo) Then
                                transacaoEstorno.EstadoDocumento = transaccionEstadoDocumento
                            Else
                                transacaoEstorno.EstadoDocumento = Enumeradores.EstadoDocumento.Confirmado
                            End If
                            transacaoEstorno.Documento = New Clases.Documento()
                            transacaoEstorno.Documento.Identificador = documento.Identificador

                            Dim novaTransacao = AccesoDatos.GenesisSaldos.TransaccionEfectivo.TransaccionEfectivoEstornoSustituicion(transacaoEstorno)

                            'Atualiza o saldo da transação nova de estorno.
                            AccesoDatos.GenesisSaldos.SaldoEfectivo.SaldoEfectivoAtualizar(novaTransacao, usuario, permiteLlegarSaldoNegativo)

                        Next

                    Else

                        ' se não existem transações no destino, significa que devem existir tranções para a origem
                        ' sendo a origem igual ao parâmetro sector de origem (tendo o mesmo), elas deverão ser desfeitas
                        For Each objTransacion In listaTransacciones.Where(Function(t) t.Cuenta.Sector.Identificador.Equals(documento.SectorDestino.Identificador))

                            transacaoEstorno = New Clases.Transaccion
                            transacaoEstorno.TipoSitio = Enumeradores.TipoSitio.Origen
                            transacaoEstorno.Identificador = objTransacion.Identificador
                            transacaoEstorno.UsuarioCreacion = usuario
                            If Not transaccionEstadoDocumento.Equals(Enumeradores.EstadoDocumento.Nuevo) Then
                                transacaoEstorno.EstadoDocumento = transaccionEstadoDocumento
                            Else
                                transacaoEstorno.EstadoDocumento = Enumeradores.EstadoDocumento.Confirmado
                            End If
                            transacaoEstorno.Documento = New Clases.Documento()
                            transacaoEstorno.Documento.Identificador = documento.Identificador

                            Dim novaTransacao = AccesoDatos.GenesisSaldos.TransaccionEfectivo.TransaccionEfectivoEstornoSustituicion(transacaoEstorno)

                            'Atualiza o saldo da transação nova de estorno.
                            AccesoDatos.GenesisSaldos.SaldoEfectivo.SaldoEfectivoAtualizar(novaTransacao, usuario, permiteLlegarSaldoNegativo)

                        Next

                    End If

                End If

                transaction.Complete()
            End Using
        End Sub

        ''' <summary>
        ''' Reverter transações para um documento padre certificado
        ''' </summary>
        ''' <param name="documento">Documento</param>
        ''' <remarks></remarks>
        Public Shared Sub RevertirTransaccionMedioPagoDocumentoPadreCertificado(permiteLlegarSaldoNegativo As Boolean, documento As Clases.Documento, usuario As String, Optional transaccionEstadoDocumento As Enumeradores.EstadoDocumento = Enumeradores.EstadoDocumento.Nuevo)
            Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted})
                'i)	Se o documento relacionado (padre) esta certificado: buscar as transações do documento para o estado “Aceptado”
                Dim listaTransacciones = AccesoDatos.GenesisSaldos.TransaccionMedioPago.RecuperarTransaccionMedioPagoParaEstorno(documento.DocumentoPadre.Identificador, Enumeradores.EstadoDocumento.Aceptado.RecuperarValor)

                If listaTransacciones IsNot Nothing AndAlso listaTransacciones.Count() > 0 Then

                    Dim transacaoEstorno As Clases.Transaccion = Nothing

                    ' verifica se existem transações para o destino
                    Dim existeTransaccionesDestino As Boolean = listaTransacciones.Exists(Function(t) t.TipoSitio = Enumeradores.TipoSitio.Destino)
                    If existeTransaccionesDestino Then

                        ' se existem transações para o destino, as mesmas terão de ser desfeitas (contra-movimientos)
                        For Each objTransacion In listaTransacciones.Where(Function(t) t.TipoSitio = Enumeradores.TipoSitio.Destino)

                            transacaoEstorno = New Clases.Transaccion
                            transacaoEstorno.TipoSitio = Enumeradores.TipoSitio.Destino
                            transacaoEstorno.Identificador = objTransacion.Identificador
                            transacaoEstorno.UsuarioCreacion = usuario
                            If Not transaccionEstadoDocumento.Equals(Enumeradores.EstadoDocumento.Nuevo) Then
                                transacaoEstorno.EstadoDocumento = transaccionEstadoDocumento
                            Else
                                transacaoEstorno.EstadoDocumento = Enumeradores.EstadoDocumento.Confirmado
                            End If
                            transacaoEstorno.Documento = New Clases.Documento()
                            transacaoEstorno.Documento.Identificador = documento.Identificador

                            Dim novaTransacao = AccesoDatos.GenesisSaldos.TransaccionMedioPago.TransaccionMedioPagoEstornoSustituicion(transacaoEstorno)

                            'Atualiza o saldo da transação nova de estorno.
                            AccesoDatos.GenesisSaldos.SaldoMedioPago.SaldoMedioPagoAtualizar(novaTransacao, usuario, permiteLlegarSaldoNegativo)

                        Next

                    Else

                        ' se não existem transações no destino, significa que devem existir tranções para a origem
                        ' sendo a origem igual ao parâmetro sector de origem (tendo o mesmo), elas deverão ser desfeitas
                        For Each objTransacion In listaTransacciones.Where(Function(t) t.Cuenta.Sector.Identificador.Equals(documento.SectorDestino.Identificador))

                            transacaoEstorno = New Clases.Transaccion
                            transacaoEstorno.TipoSitio = Enumeradores.TipoSitio.Origen
                            transacaoEstorno.Identificador = objTransacion.Identificador
                            transacaoEstorno.UsuarioCreacion = usuario
                            If Not transaccionEstadoDocumento.Equals(Enumeradores.EstadoDocumento.Nuevo) Then
                                transacaoEstorno.EstadoDocumento = transaccionEstadoDocumento
                            Else
                                transacaoEstorno.EstadoDocumento = Enumeradores.EstadoDocumento.Confirmado
                            End If
                            transacaoEstorno.Documento = New Clases.Documento()
                            transacaoEstorno.Documento.Identificador = documento.Identificador

                            Dim novaTransacao = AccesoDatos.GenesisSaldos.TransaccionMedioPago.TransaccionMedioPagoEstornoSustituicion(transacaoEstorno)

                            'Atualiza o saldo da transação nova de estorno.
                            AccesoDatos.GenesisSaldos.SaldoMedioPago.SaldoMedioPagoAtualizar(novaTransacao, usuario, permiteLlegarSaldoNegativo)

                        Next

                    End If

                End If


                transaction.Complete()
            End Using
        End Sub


        Public Shared Sub EjecutarAccionContableEfectivo(identificadorDocumento As String, _
                                                         identificadorCuentaMovimento As String, _
                                                         identificadorCuentaSaldo As String, _
                                                         divisas As ObservableCollection(Of Clases.Divisa), _
                                                         FechaHoraPlanificacionCertificacion As DateTime, _
                                                         accionTransaccion As Clases.AccionTransaccion, _
                                                         unidadMedidaPadron As String, _
                                                         usuario As String)

            Dim identificadorDivisa As String = String.Empty,
                identificadorDenominacion As String = String.Empty,
                identificadorUnidadMedida As String = String.Empty,
                codigoNivelDetalhe As String = String.Empty,
                codigoTipoEfectivoTotal As String = String.Empty,
                identificadorCalidad As String = String.Empty,
                numImporte As Decimal = 0,
                cantidad As Int64,
                codigoMigracion As String = String.Empty

            If divisas IsNot Nothing Then

                'Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted})

                Dim _Transaccion As New Clases.Transaccion
                _Transaccion.EstadoDocumento = Enumeradores.EstadoDocumento.Aceptado
                _Transaccion.TipoSitio = accionTransaccion.TipoSitio
                _Transaccion.TipoSaldo = accionTransaccion.TipoSaldo
                _Transaccion.TipoMovimiento = accionTransaccion.TipoMovimiento

                codigoMigracion = String.Empty

                For Each divisa In divisas
                    identificadorDivisa = divisa.Identificador

                    'para cada divisa recuperar o valor de cada tiem de 
                    'Denominaciones
                    If divisa.Denominaciones IsNot Nothing Then
                        For Each Denominacion In divisa.Denominaciones
                            identificadorDenominacion = Denominacion.Identificador
                            codigoNivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor
                            codigoTipoEfectivoTotal = String.Empty

                            For Each valor In Denominacion.ValorDenominacion

                                If valor.Calidad IsNot Nothing Then
                                    identificadorCalidad = valor.Calidad.Identificador
                                Else
                                    identificadorCalidad = String.Empty
                                End If

                                numImporte = valor.Importe
                                cantidad = valor.Cantidad

                                If valor.UnidadMedida IsNot Nothing AndAlso Not String.IsNullOrEmpty(valor.UnidadMedida.Identificador) Then
                                    identificadorUnidadMedida = valor.UnidadMedida.Identificador
                                Else
                                    identificadorUnidadMedida = unidadMedidaPadron
                                End If

                                If accionTransaccion.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso Then
                                    'coloca o valor como negativo.
                                    numImporte = numImporte * (-1)
                                    cantidad = cantidad * (-1)
                                End If

                                'Insere o efectivo de documento
                                AccesoDatos.GenesisSaldos.TransaccionEfectivo.TransaccionEfectivoInserir_v2(True,
                                                                                                            _Transaccion, _
                                                                                                            identificadorDocumento, _
                                                                                                            identificadorCuentaMovimento, _
                                                                                                            identificadorCuentaSaldo, _
                                                                                                            FechaHoraPlanificacionCertificacion, _
                                                                                                            identificadorDivisa, _
                                                                                                            identificadorDenominacion, _
                                                                                                            identificadorUnidadMedida, _
                                                                                                            codigoNivelDetalhe, _
                                                                                                            codigoTipoEfectivoTotal, _
                                                                                                            identificadorCalidad, _
                                                                                                            numImporte, _
                                                                                                            cantidad, _
                                                                                                            codigoMigracion, _
                                                                                                            usuario)

                            Next
                        Next
                    End If

                    'se existe valores total de efectivo
                    If divisa.ValoresTotalesEfectivo IsNot Nothing Then
                        For Each valor In divisa.ValoresTotalesEfectivo
                            codigoNivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor
                            codigoTipoEfectivoTotal = valor.TipoDetalleEfectivo.RecuperarValor()

                            identificadorDenominacion = String.Empty
                            identificadorUnidadMedida = String.Empty
                            identificadorCalidad = String.Empty
                            numImporte = valor.Importe
                            cantidad = 0

                            If accionTransaccion.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso Then
                                'coloca o valor como negativo.
                                numImporte = numImporte * (-1)
                            End If

                            'Insere o efectivo de documento
                            AccesoDatos.GenesisSaldos.TransaccionEfectivo.TransaccionEfectivoInserir_v2(True,
                                                                                                        _Transaccion, _
                                                                                                        identificadorDocumento, _
                                                                                                        identificadorCuentaMovimento, _
                                                                                                        identificadorCuentaSaldo, _
                                                                                                        FechaHoraPlanificacionCertificacion, _
                                                                                                        identificadorDivisa, _
                                                                                                        identificadorDenominacion, _
                                                                                                        identificadorUnidadMedida, _
                                                                                                        codigoNivelDetalhe, _
                                                                                                        codigoTipoEfectivoTotal, _
                                                                                                        identificadorCalidad, _
                                                                                                        numImporte, _
                                                                                                        cantidad, _
                                                                                                        codigoMigracion, _
                                                                                                        usuario)
                        Next
                    End If

                    'se existe valores total de divisas
                    If divisa.ValoresTotalesDivisa IsNot Nothing Then
                        For Each valor In divisa.ValoresTotalesDivisa
                            codigoNivelDetalhe = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor
                            codigoTipoEfectivoTotal = String.Empty
                            identificadorDenominacion = String.Empty
                            identificadorUnidadMedida = String.Empty
                            identificadorCalidad = String.Empty
                            numImporte = valor.Importe

                            'TODO: Verificar cantidad quando for total de divisa.
                            cantidad = 0

                            If accionTransaccion.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso Then
                                'coloca o valor como negativo.
                                numImporte = numImporte * (-1)
                            End If

                            'Insere o efectivo de documento
                            AccesoDatos.GenesisSaldos.TransaccionEfectivo.TransaccionEfectivoInserir_v2(True,
                                                                                                        _Transaccion, _
                                                                                                        identificadorDocumento, _
                                                                                                        identificadorCuentaMovimento, _
                                                                                                        identificadorCuentaSaldo, _
                                                                                                        FechaHoraPlanificacionCertificacion, _
                                                                                                        identificadorDivisa, _
                                                                                                        identificadorDenominacion, _
                                                                                                        identificadorUnidadMedida, _
                                                                                                        codigoNivelDetalhe, _
                                                                                                        codigoTipoEfectivoTotal, _
                                                                                                        identificadorCalidad, _
                                                                                                        numImporte, _
                                                                                                        cantidad, _
                                                                                                        codigoMigracion, _
                                                                                                        usuario)
                        Next
                    End If
                Next

                'transaction.Complete()
                'End Using
            End If

        End Sub

        Public Shared Sub EjecutarAccionContableMedioPago(identificadorDocumento As String, _
                                                         identificadorCuentaMovimento As String, _
                                                         identificadorCuentaSaldo As String, _
                                                         divisas As ObservableCollection(Of Clases.Divisa), _
                                                         FechaHoraPlanificacionCertificacion As DateTime, _
                                                         accionTransaccion As Clases.AccionTransaccion, _
                                                         unidadMedidaPadron As String, _
                                                         usuario As String)

            Dim identificadorDivisa As String = String.Empty,
                identificadorMedioPago As String = String.Empty,
                codigoTipoMedioPago As String = String.Empty,
                codigoNivelDetalhe As String = String.Empty,
                identificadorCalidad As String = String.Empty,
                numImporte As Decimal = 0,
                cantidad As Int64,
                codigoMigracion As String = String.Empty

            If divisas IsNot Nothing Then

                'Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted})

                Dim _Transaccion As New Clases.Transaccion
                _Transaccion.EstadoDocumento = Enumeradores.EstadoDocumento.Aceptado
                _Transaccion.TipoSitio = accionTransaccion.TipoSitio
                _Transaccion.TipoSaldo = accionTransaccion.TipoSaldo
                _Transaccion.TipoMovimiento = accionTransaccion.TipoMovimiento

                For Each divisa In divisas
                    identificadorDivisa = divisa.Identificador

                    'para cada divisa recuperar o valor de cada tiem de 
                    'Denominaciones
                    If divisa.MediosPago IsNot Nothing Then
                        For Each medioPago In divisa.MediosPago
                            identificadorMedioPago = medioPago.Identificador
                            codigoNivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor
                            codigoTipoMedioPago = medioPago.Tipo.RecuperarValor

                            For Each valor In medioPago.Valores
                                numImporte = valor.Importe
                                cantidad = valor.Cantidad

                                If accionTransaccion.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso Then
                                    'coloca o valor como negativo.
                                    numImporte = numImporte * (-1)
                                    cantidad = cantidad * (-1)
                                End If

                                'Insere a transaccion do mediopago de documento
                                AccesoDatos.GenesisSaldos.TransaccionMedioPago.TransaccionMedioPagoInserir_V2(True, _
                                                                                                           _Transaccion, _
                                                                                                           identificadorDocumento, _
                                                                                                           identificadorCuentaMovimento, _
                                                                                                           identificadorCuentaSaldo, _
                                                                                                           FechaHoraPlanificacionCertificacion, _
                                                                                                           identificadorDivisa, _
                                                                                                           identificadorMedioPago, _
                                                                                                           codigoNivelDetalhe, _
                                                                                                           codigoTipoMedioPago, _
                                                                                                           identificadorCalidad, _
                                                                                                           numImporte, _
                                                                                                           cantidad, _
                                                                                                           codigoMigracion, _
                                                                                                           usuario)

                            Next
                        Next
                    End If

                    'se existe valores total de efectivo
                    If divisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
                        For Each valor In divisa.ValoresTotalesTipoMedioPago
                            codigoNivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor
                            identificadorMedioPago = String.Empty
                            codigoTipoMedioPago = valor.TipoMedioPago.RecuperarValor
                            numImporte = valor.Importe
                            cantidad = 0

                            If accionTransaccion.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso Then
                                'coloca o valor como negativo.
                                numImporte = numImporte * (-1)
                            End If

                            'Insere a transaccion do mediopago de documento
                            AccesoDatos.GenesisSaldos.TransaccionMedioPago.TransaccionMedioPagoInserir_V2(True, _
                                                                                                           _Transaccion, _
                                                                                                           identificadorDocumento, _
                                                                                                           identificadorCuentaMovimento, _
                                                                                                           identificadorCuentaSaldo, _
                                                                                                           FechaHoraPlanificacionCertificacion, _
                                                                                                           identificadorDivisa, _
                                                                                                           identificadorMedioPago, _
                                                                                                           codigoNivelDetalhe, _
                                                                                                           codigoTipoMedioPago, _
                                                                                                           identificadorCalidad, _
                                                                                                           numImporte, _
                                                                                                           cantidad, _
                                                                                                           codigoMigracion, _
                                                                                                           usuario)

                        Next
                    End If
                Next

                'Transaction.Complete()
                'End Using
            End If

        End Sub


    End Class

End Namespace