Partial Class CortesParciais

    ''' <summary>
    ''' Popula o data set de corte parcial recuperando os dados da lista passada como parâmetro
    ''' </summary>
    ''' <param name="objCorteParcial">Objeto com os dados do corte parcial</param>
    ''' <remarks></remarks>
    Public Sub PopularCorteParcial(objCorteParcial As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialPDF)

        ' Popula os dados do cabeçalho
        Me.PopularCabecalho(objCorteParcial.Detalles)

        ' Popula os dados do detalhe processo
        Me.PopularDetalleProcesso(objCorteParcial.Detalles)

        ' Popula os dados do detalhe da remesa
        Me.PopularDetalleRemesa(objCorteParcial.Detalles)

        ' Popula os dados do sobre
        Me.PopularSobreProcesso(objCorteParcial)

        ' Popula os dados do sobres da remesa
        Me.PopularSobreRemesa(objCorteParcial)

        ' Popula os dados da observação
        Me.PopularObservacion(objCorteParcial)

        ' Popula os dados dos falsos
        Me.PopularFalso(objCorteParcial)

    End Sub

    ''' <summary>
    ''' Popula a tabela com os dados do cabeçalho do relatório
    ''' </summary>
    ''' <param name="lstDetalles">Dados dos detalles recuperados do banco</param>
    ''' <remarks></remarks>
    Private Sub PopularCabecalho(lstDetalles As ContractoServ.CorteParcial.GetCortesParciais.DetalleColeccion)

        ' Define a linha que receberá os dados da lista
        Dim drCabecalho As CabecalhoRow

        ' Seleciona os dados do cabeçalho, ignorando os campos que se repetem
        Dim lstCabecalho = (From d In lstDetalles _
                           Select d.Proceso, d.Letra, d.OidRemesaOri, d.CodSubCliente, d.Estacion, d.DescricionEstacion, d.FechaTransporte, d.Remesa).Distinct

        ' Variável que recebe os dados do cabeçalho
        Dim cabecalho

        ' Para cada detalhe existente na lista
        For Each cabecalho In lstCabecalho

            ' Cria uma nova instancia da linha da tabela de detalhe
            drCabecalho = Me.Cabecalho.NewRow

            ' Preenche os dados de detalhes recuperados do banco
            drCabecalho.Letra = cabecalho.Letra
            drCabecalho.Sucursal = cabecalho.Estacion
            drCabecalho.DescricionSucursal = cabecalho.DescricionEstacion
            drCabecalho.Fecha = cabecalho.FechaTransporte
            drCabecalho.Proceso = cabecalho.Proceso
            drCabecalho.Sucursal = cabecalho.Estacion
            drCabecalho.Remesa = cabecalho.Remesa
            drCabecalho.OidRemesaOri = cabecalho.OidRemesaOri
            drCabecalho.CodSubCliente = cabecalho.CodSubCliente

            ' Adiciona a linha na tabela
            Me.Cabecalho.Rows.Add(drCabecalho)

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela com os dados de detalhes do processo do relatório
    ''' </summary>
    ''' <param name="lstDetalles">Dados dos detalles recuperados do banco</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 04/11/2009 Alterado
    ''' </history>
    Private Sub PopularDetalleProcesso(lstDetalles As ContractoServ.CorteParcial.GetCortesParciais.DetalleColeccion)

        ' Define a linha que receberá os dados da lista
        Dim drProcesso As ProcessoRow

        ' Recupera os dados do processo agrupados
        Dim lstProcesso = From d In lstDetalles _
                        Group d By d.Proceso, d.DescricionDivisa, d.DescricionMedioPago Into Group _
                        Order By Proceso, DescricionDivisa, DescricionMedioPago _
                        Select Proceso, DescricionDivisa, DescricionMedioPago, Declarado = Group.Sum(Function(d) d.Declarado), Ingresado = Group.Sum(Function(d) d.Ingresado), Recontado = Group.Sum(Function(d) d.Recontado)

        ' Variavel que recebe os dados do processo
        Dim detalhe

        ' Para cada detalhe existente na lista
        For Each detalhe In lstProcesso

            ' Cria uma nova instancia da linha da tabela de detalhe
            drProcesso = Me.Processo.NewRow

            ' Preenche os dados de detalhes recuperados do banco            
            drProcesso.Proceso = detalhe.Proceso
            drProcesso.DescricionDivisa = detalhe.DescricionDivisa
            drProcesso.DescricionMedioPago = detalhe.DescricionMedioPago
            drProcesso.Declarado = detalhe.Declarado
            drProcesso.Ingresado = detalhe.Ingresado
            drProcesso.Recontado = detalhe.Recontado

            ' Adiciona a linha na tabela
            Me.Processo.Rows.Add(drProcesso)

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela com os dados de detalhes da sucursal do corte parcial
    ''' </summary>
    ''' <param name="lstDetalles">Dados dos detalles recuperados do banco</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 04/11/2009 Alterado
    ''' </history>
    Private Sub PopularDetalleRemesa(lstDetalles As ContractoServ.CorteParcial.GetCortesParciais.DetalleColeccion)

        ' Define a linha que receberá os dados da lista
        Dim drRemesa As RemesaRow

        ' Recupera os dados da Remesa agrupados
        Dim lstRemesa = From d In lstDetalles _
                        Group d By d.Proceso, d.F22, d.Remesa, d.DescricionDivisa, d.DescricionEstacion, d.DescricionMedioPago Into Group _
                        Order By Proceso, F22, Remesa, DescricionDivisa, DescricionMedioPago _
                        Select Proceso, F22, Remesa, DescricionDivisa, DescricionMedioPago, Declarado = Group.Sum(Function(d) d.Declarado), Ingresado = Group.Sum(Function(d) d.Ingresado), Recontado = Group.Sum(Function(d) d.Recontado)

        ' Variavel que recebe os dados do processo
        Dim detalhe

        ' Para cada detalhe existente na lista
        For Each detalhe In lstRemesa

            ' Cria uma nova instancia da linha da tabela de detalhe
            drRemesa = Me.Remesa.NewRow

            ' Preenche os dados de detalhes recuperados do banco            
            drRemesa.Proceso = detalhe.Proceso
            drRemesa.Remesa = detalhe.Remesa
            drRemesa.F22 = detalhe.F22
            drRemesa.DescricionDivisa = detalhe.DescricionDivisa
            drRemesa.DescricionMedioPago = detalhe.DescricionMedioPago
            drRemesa.Declarado = detalhe.Declarado
            drRemesa.Ingresado = detalhe.Ingresado
            drRemesa.Recontado = detalhe.Recontado

            ' Adiciona a linha na tabela
            Me.Remesa.Rows.Add(drRemesa)

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela com os dados de sobres do processo do corte parcial
    ''' </summary>
    ''' <param name="objCorteParcial">Dados dos cortes parciais recuperados do banco</param>
    ''' <remarks></remarks>
    Private Sub PopularSobreProcesso(objCorteParcial As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialPDF)

        ' Recupera os dados do sobre agrupados pela Estacion
        Dim lstSobres = From ds In (From d In objCorteParcial.Detalles _
                        Group Join s In objCorteParcial.Sobres _
                        On d.Remesa Equals s.Remesa Into Group _
                        From s In Group.DefaultIfEmpty() _
                        Select d.Proceso, d.Remesa, _
                        ParcialesContados = If(s Is Nothing, 0, s.ParcialesContados), _
                        ParcialesDeclarados = If(s Is Nothing, 0, s.ParcialesDeclarados), _
                        ParcialesIngresados = If(s Is Nothing, 0, s.ParcialesIngresados)).Distinct _
                        Group ds By ds.Proceso Into Group _
                        Select Proceso, _
                        ParcialesContados = Group.Sum(Function(ds) ds.ParcialesContados), _
                        ParcialesDeclarados = Group.Sum(Function(ds) ds.ParcialesDeclarados), _
                        ParcialesIngresados = Group.Sum(Function(ds) ds.ParcialesIngresados)

        ' Define a linha que receberá os dados da lista
        Dim drSobre As SobreProcessoRow

        ' Variavel que recebe os dados do sobre
        Dim sobre

        ' Para cada sobre existente na lista
        For Each sobre In lstSobres

            ' Cria uma nova instancia da linha da tabela de sobre
            drSobre = Me.SobreProcesso.NewRow

            ' Preenche os dados de sobres recuperados do banco
            drSobre.Proceso = sobre.Proceso
            drSobre.ParcialesContados = sobre.ParcialesContados
            drSobre.ParcialesDeclarados = sobre.ParcialesDeclarados
            drSobre.ParcialesIngresados = sobre.ParcialesIngresados

            ' Adiciona a linha na tabela
            Me.SobreProcesso.Rows.Add(drSobre)

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela com os dados de sobres da sucursal do corte parcial
    ''' </summary>
    ''' <param name="objCorteParcial">Dados dos cortes parciais recuperados do banco</param>
    ''' <remarks></remarks>
    Private Sub PopularSobreRemesa(objCorteParcial As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialPDF)

        ' Recupera os dados do sobre agrupados pela Estacion
        Dim lstSobres = From ds In (From d In objCorteParcial.Detalles _
                        Group Join s In objCorteParcial.Sobres _
                        On d.Remesa Equals s.Remesa Into Group _
                        From s In Group.DefaultIfEmpty() _
                        Select d.Proceso, d.Remesa, _
                        ParcialesContados = If(s Is Nothing, 0, s.ParcialesContados), _
                        ParcialesDeclarados = If(s Is Nothing, 0, s.ParcialesDeclarados), _
                        ParcialesIngresados = If(s Is Nothing, 0, s.ParcialesIngresados)).Distinct _
                        Group ds By ds.Remesa Into Group _
                        Select Remesa, _
                        ParcialesContados = Group.Sum(Function(ds) ds.ParcialesContados), _
                        ParcialesDeclarados = Group.Sum(Function(ds) ds.ParcialesDeclarados), _
                        ParcialesIngresados = Group.Sum(Function(ds) ds.ParcialesIngresados)

        ' Define a linha que receberá os dados da lista
        Dim drSobre As SobreRemesaRow

        ' Variavel que recebe os dados do sobre
        Dim sobre

        ' Para cada sobre existente na lista
        For Each sobre In lstSobres

            ' Cria uma nova instancia da linha da tabela de sobre
            drSobre = Me.SobreRemesa.NewRow

            ' Preenche os dados de sobres recuperados do banco
            drSobre.Remesa = sobre.Remesa
            drSobre.ParcialesContados = sobre.ParcialesContados
            drSobre.ParcialesDeclarados = sobre.ParcialesDeclarados
            drSobre.ParcialesIngresados = sobre.ParcialesIngresados

            ' Adiciona a linha na tabela
            Me.SobreRemesa.Rows.Add(drSobre)

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela com os dados de observações do corte parcial
    ''' </summary>
    ''' <param name="objCorteParcial">Dados dos cortes parciais recuperados do banco</param>
    ''' <remarks></remarks>
    Private Sub PopularObservacion(objCorteParcial As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialPDF)

        ' Recupera os dados do sobre agrupados pela Estacion
        Dim lstObservaciones = From dob In (From d In objCorteParcial.Detalles _
                        Join o In objCorteParcial.Observaciones _
                        On d.Remesa Equals o.Remesa _
                        Select d.Remesa, o.Descricion).Distinct

        ' Define a linha que receberá os dados da lista
        Dim drObservacion As ObservacionRow

        'Variavel que recebe os dados da observacao
        Dim observacion

        ' Para cada sobre existente na lista
        For Each observacion In lstObservaciones

            ' Cria uma nova instancia da linha da tabela de sobre
            drObservacion = Me.Observacion.NewRow

            ' Preenche os dados da observacion recuperados do banco
            drObservacion.Remesa = observacion.Remesa
            drObservacion.Descricion = observacion.Descricion

            ' Adiciona a linha na tabela
            Me.Observacion.Rows.Add(drObservacion)

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela com os dados de falsos
    ''' </summary>
    ''' <param name="objCorteParcial">Dados dos cortes parciais recuperados do banco</param>
    ''' <remarks></remarks>
    Private Sub PopularFalso(objCorteParcial As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialPDF)

        ' Define a linha que receberá os dados da lista
        Dim drFalso As FalsoRow

        'Variavel que recebe os dados da falso
        Dim falso

        ' Para cada sobre existente na lista
        For Each falso In objCorteParcial.Falsos

            ' Cria uma nova instancia da linha da tabela de sobre
            drFalso = Me.Falso.NewRow

            ' Preenche os dados do falson recuperados do banco
            drFalso.Remesa = falso.Remesa
            drFalso.Tipo = falso.Tipo
            drFalso.Divisa = falso.Divisa
            drFalso.Denominacion = falso.Denominacion
            drFalso.NumeroSerie = falso.NumeroSerie
            drFalso.NumeroPlancha = falso.NumeroPlancha
            drFalso.Observacion = falso.Observacion
            drFalso.NumeroUnidades = falso.NumeroUnidades

            ' Adiciona a linha na tabela
            Me.Falso.Rows.Add(drFalso)

        Next

    End Sub

End Class