Imports Prosegur.Framework.Dicionario.Tradutor

Partial Class RespaldosCompletos

    ''' <summary>
    ''' Popula o data set de respaldos completos recuperando os dados da lista passada como parâmetro
    ''' </summary>
    ''' <param name="objRespaldoCompleto">Objeto com os dados do respaldo completo</param>
    ''' <remarks></remarks>
    Public Sub PopularRespaldoCompleto(objRespaldoCompleto As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoPDF)

        ' Popula os dados de detalhe
        Me.PopularDetalle(objRespaldoCompleto.Detalles)

        ' Popula os dados da divisa
        Me.PopularDivisas(objRespaldoCompleto)

        ' Popula os dados do sobre
        Me.PopularSobre(objRespaldoCompleto)

        ' Popula os dados da informação do IAC
        Me.PopularInformacaoIAC(objRespaldoCompleto.InformacionesIAC)

        ' Popula os dados do medio de pago
        Me.PopularMedioPago(objRespaldoCompleto.Detalles)

        ' Define o registro que recebe o total de parciais declarados
        Dim drTotalParcialDeclarado As TotalParcialDeclaradoRow = Me.TotalParcialDeclarado.NewRow
        ' Define a quantidade de parciais declarados
        drTotalParcialDeclarado.Quantidade = objRespaldoCompleto.TotalParcialesDeclarados

        ' Popula a quantidade de parciais contados
        Me.TotalParcialDeclarado.Rows.Add(drTotalParcialDeclarado)

        Me.PopularObservacion(objRespaldoCompleto.Observaciones)

    End Sub

    ''' <summary>
    ''' Popula os detalhes do respaldo completo recuperados da lista na tabela de detalhes do respaldo completo
    ''' </summary>
    ''' <param name="lstDetalhes">Lista de detalhes do respaldo completo</param>
    ''' <remarks></remarks>
    Private Sub PopularDetalle(lstDetalhes As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DetalleColeccion)

        ' Define a linha que receberá os dados da lista
        Dim drDetalhe As DetalleRow

        ' Para cada detalhe de divisa existente na lista de detalhes da divisa
        For Each detalhe As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Detalle In lstDetalhes

            ' Cria uma nova linha do detalhe da divisa
            drDetalhe = Me.Detalle.NewRow

            ' Adiciona os dados na linha
            drDetalhe.Letra = detalhe.Letra
            drDetalhe.Parcial = detalhe.Parcial
            drDetalhe.F22 = detalhe.F22
            drDetalhe.CodSubCliente = detalhe.CodSubCliente
            drDetalhe.OidRemesaOri = detalhe.OidRemesaOri
            drDetalhe.Sucursal = detalhe.Sucursal
            drDetalhe.DescricionSucursal = detalhe.DescricionSucursal
            drDetalhe.Divisa = detalhe.Divisa
            drDetalhe.DescricionDivisa = lstDetalhes.Find(New Predicate(Of ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Detalle)(Function(d) d.Divisa = drDetalhe.Divisa)).DescricionDivisa 'detalhe.DescricionDivisa
            drDetalhe.UnidadMoeda = detalhe.UnidadMoeda
            drDetalhe.Denominacion = detalhe.Denominacion
            drDetalhe.Unidades = detalhe.Unidades
            drDetalhe.Recontado = detalhe.Recontado
            drDetalhe.NumeroSecuencia = detalhe.NumeroSecuencia

            '' Se a unidade é de bilhete ou moeda
            'If (detalhe.UnidadMoeda > 0) Then

            '    'Define a descrição do tipo de efetivo
            '    drDetalhe.DescricionTipo = If(detalhe.BolBillete, _
            '                                Traduzir("lbl_billete").ToUpper, _
            '                                Traduzir("lbl_moneda").ToUpper)
            'ElseIf (detalhe.UnidadMoeda = 0) Then

            'Define a descrição do tipo de meio de pagamento
            drDetalhe.DescricionTipo = detalhe.Denominacion.ToUpper()

            'End If


            ' Adiciona a linha na tabela de detalhes da divisa
            Me.Detalle.Rows.Add(drDetalhe)

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela com os dados de divisas do respaldo completo
    ''' </summary>
    ''' <param name="objRespaldoCompleto">Dados do respaldo completo recuperados do banco</param>
    ''' <remarks></remarks>
    Private Sub PopularDivisas(objRespaldoCompleto As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoPDF)

        ' Recupera uma lista com a descrição das divisas contadas
        Dim lstDesDivisas = From d In objRespaldoCompleto.Detalles Where d.UnidadMoeda >= 0 Select d.Divisa, d.DescricionDivisa Distinct

        Dim lstDivisas = From div In objRespaldoCompleto.Divisas _
                         Join d In lstDesDivisas On div.Divisa Equals d.Divisa _
                         Select div.Parcial, _
                         div.Importe, _
                         div.ImporteIngressado, _
                         DescricionDivisa = d.DescricionDivisa, _
                         Denominacion = div.DescripcionMedioPago _
                         Order By Parcial, DescricionDivisa, Denominacion

        ' Define a linha que receberá os dados da lista 
        Dim drDivisa As DivisaRow

        ' Para cada divisa existente na lista
        For Each divisa As Object In lstDivisas

            ' Cria uma nova instancia da linha da tabela de divisa
            drDivisa = Me.Divisa.NewRow

            ' Preenche os dados de divisas recuperados do banco
            drDivisa.Parcial = divisa.Parcial
            drDivisa.Importe = divisa.Importe
            drDivisa.DescricionDivisa = divisa.DescricionDivisa
            drDivisa.Denominacion = divisa.Denominacion
            drDivisa.ImporteIngressado = divisa.ImporteIngressado

            ' Adiciona a linha na tabela
            Me.Divisa.Rows.Add(drDivisa)

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela com os dados de sobres do respaldo completo
    ''' </summary>
    ''' <param name="objRespaldoCompleto">Dados do respaldo completo recuperados do banco</param>
    ''' <remarks></remarks>
    Private Sub PopularSobre(objRespaldoCompleto As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoPDF)

        ' Recupera os dados do sobre agrupados pelo NumeroSobre
        Dim lstSobres = From ds In (From d In objRespaldoCompleto.Detalles _
                        Group Join s In objRespaldoCompleto.Sobres _
                        On d.Parcial Equals s.Parcial Into Group _
                        From s In Group.DefaultIfEmpty _
                        Select Parcial = d.Parcial, _
                               ParcialesContados = If(s Is Nothing, 0, s.ParcialesContados), _
                               ParcialesIngresados = If(s Is Nothing, 0, s.ParcialesIngresados) _
                        ).Distinct _
                        Group ds By ds.Parcial Into Group _
                        Select Parcial, _
                               ParcialesContados = Group.Sum(Function(ds) ds.ParcialesContados), _
                               ParcialesIngresados = Group.Sum(Function(ds) ds.ParcialesIngresados)

        ' Define a linha que receberá os dados da lista
        Dim drSobre As SobreRow

        ' Para cada sobre existente na lista
        For Each sobre As Object In lstSobres
            Dim sobreLocal = sobre

            Dim datos = (From s In objRespaldoCompleto.Sobres _
                         Where s.Parcial = sobreLocal.Parcial).FirstOrDefault()

            'If datos IsNot Nothing Then

            ' Cria uma nova instancia da linha da tabela de sobre
            drSobre = Me.Sobre.NewRow

            ' Preenche os dados de sobres recuperados do banco
            drSobre.Parcial = sobre.Parcial
            drSobre.ParcialesContados = sobre.ParcialesContados
            drSobre.ParcialesIngresados = IIf(datos.ParcialesIngresados IsNot Nothing, sobre.ParcialesIngresados, sobre.ParcialesContados)
            drSobre.Sucursal = datos.Sucursal
            drSobre.DescricionSucursal = datos.DescricionSucursal
            drSobre.F22 = datos.F22

            ' Adiciona a linha na tabela
            Me.Sobre.Rows.Add(drSobre)

            'End If

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela com as informações do IAC do respaldo completo
    ''' </summary>
    ''' <param name="lstInformacaoIAC">Dados do respaldo completo recuperados do banco</param>
    ''' <remarks></remarks>
    Private Sub PopularInformacaoIAC(lstInformacaoIAC As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACPDFColeccion)

        ' Define a linha que receberá os dados da lista
        Dim drInformacaoIAC As InformacionIACRow

        ' Variável que recebe os dados informações do IAC do parcial
        Dim objInformacoes As String()

        ' Variável que recebe os dados de uma única informação do IAC
        Dim objInformacao As String()

        ' Para cada informação do IAC
        For Each informacionIAC As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACPDF In lstInformacaoIAC

            ' Recupera as informações do IAC do parcial
            objInformacoes = informacionIAC.Descricao.Trim.Split(Constantes.RespaldoCompleto.SEPARADOR_INFORMACOES_IAC.ToArray(), StringSplitOptions.RemoveEmptyEntries)

            ' Para cada informação do IAC recuperada do parcial
            For Each informacao As String In objInformacoes

                ' Cria uma nova instância da linha da tabela de informação do IAC
                drInformacaoIAC = Me.InformacionIAC.NewRow

                ' Preenche as informações do IAC recuperados do banco
                drInformacaoIAC.Parcial = informacionIAC.Parcial

                ' Se existe dados
                If informacao <> String.Empty Then

                    ' Recupera a informação do IAC
                    objInformacao = informacao.Split(Constantes.RespaldoCompleto.SEPARADOR_INFORMACAO_IAC)

                    ' Adiciona a decrição da informação do IAC na linha
                    drInformacaoIAC.Descricao = objInformacao(0).Trim

                    ' Se existir
                    If (objInformacao.Length > 1) Then
                        ' Adiciona o valor da informação do IAC na linha
                        drInformacaoIAC.Valor = objInformacao(1).Trim
                    End If

                    ' Adiciona a linha na tabela
                    Me.InformacionIAC.Rows.Add(drInformacaoIAC)

                End If

            Next

        Next

    End Sub

    ''' <summary>
    ''' Popula os os meios de pagamento agrupados do respaldo completo
    ''' </summary>
    ''' <param name="lstDetalhes">Lista de detalhes do respaldo completo</param>
    ''' <remarks></remarks>
    Private Sub PopularMedioPago(lstDetalhes As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DetalleColeccion)

        ' Define a linha que receberá os dados da lista
        Dim drMedioPago As MedioPagoRow

        Dim lstMedioPago = From d In lstDetalhes _
                           Group d By d.DescricionDivisa, d.UnidadMoeda, d.Denominacion, d.BolBillete Into Group _
                           Order By DescricionDivisa, Denominacion, UnidadMoeda Descending _
                           Select DescricionDivisa, _
                                    Denominacion, _
                                    UnidadMoeda, _
                                    BolBillete, _
                                    Unidades = Group.Sum(Function(d) d.Unidades), _
                                    Recontado = Group.Sum(Function(d) d.Recontado)

        ' Para cada meio de pagamento existente na lista de meios de pagamentos agrupados
        For Each medioPago As Object In lstMedioPago

            ' Cria uma nova linha do meio de pagamento
            drMedioPago = Me.MedioPago.NewRow

            ' Adiciona os dados na linha            
            drMedioPago.DescricionDivisa = medioPago.DescricionDivisa
            drMedioPago.Denominacion = medioPago.Denominacion
            drMedioPago.UnidadMoeda = medioPago.UnidadMoeda
            drMedioPago.Unidades = medioPago.Unidades
            drMedioPago.Recontado = medioPago.Recontado

            drMedioPago.DescricionTipo = If(medioPago.UnidadMoeda > 0, _
                                            If(medioPago.BolBillete, _
                                               Traduzir("lbl_billete").ToUpper, _
                                               Traduzir("lbl_moneda").ToUpper), _
                                               medioPago.Denominacion.ToUpper)

            ' Adiciona a linha na tabela de detalhes da divisa
            Me.MedioPago.Rows.Add(drMedioPago)

        Next

    End Sub

    ''' <summary>
    ''' Popula as observações
    ''' </summary>
    ''' <param name="lstObservaciones">Lista de detalhes do respaldo completo</param>
    ''' <remarks></remarks>
    Private Sub PopularObservacion(lstObservaciones As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.ObservacionColeccion)

        ' Define a linha que receberá os dados da lista
        Dim drObservacion As ObservacionRow

        ' Para cada detalhe de divisa existente na lista de detalhes da divisa
        For Each observacion As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Observacion In lstObservaciones

            ' Cria uma nova linha do detalhe da divisa
            drObservacion = Me.Observacion.NewRow

            ' Adiciona os dados na linha
            drObservacion.Parcial = observacion.Parcial
            drObservacion.Descripcion = observacion.Descripcion

            ' Adiciona a linha na tabela de observaciones
            Me.Observacion.Rows.Add(drObservacion)

        Next

    End Sub

End Class