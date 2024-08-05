Imports Prosegur.Framework.Dicionario.Tradutor

Partial Class InformeResultadoContaje

    Private objPrecintosBulto As String = ""
    Private OrdemRemesa As Integer = 10000

    Public Function CuentaPorParciales(Caracteristicas As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.CaracteristicaColeccion) As Boolean
        Return Util.VerificarCaracteristica(Caracteristicas, ContractoServ.Constantes.COD_CARAC_PROCESAR_POR_PARCIALE)
    End Function

    Public Function CuentaPorRemesa(Caracteristicas As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.CaracteristicaColeccion) As Boolean
        Return Util.VerificarCaracteristica(Caracteristicas, ContractoServ.Constantes.COD_CARAC_CONTEO_POR_REMESA)
    End Function

    Public Sub PopularInformeResultadoContaje(Remesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        If Remesa IsNot Nothing Then

            ' Preenche as Informações do Cliente
            PopularInformacionCliente(Remesa.InfoCliente)

            ' Preenche os dados no nível da Remessa
            PopularDatosRemesa(Remesa)

            ' Preenche os dados no nível dos Bultos e das Parciais
            PopularDatosBultos(Remesa.Bultos, Remesa)

            ' Preenche os Parâmetros
            PreencherParametros(CuentaPorRemesa(Remesa.Bultos(0).Caracteristicas), CuentaPorParciales(Remesa.Bultos(0).Caracteristicas))

        End If

    End Sub

    Private Sub PreencherParametros(ConteoRemesa As Boolean, ProcesaParciales As Boolean)

        Dim drParametro As ParametrosRow

        drParametro = Parametros.NewRow

        drParametro.NombreCampoPrecintoBulto = Traduzir("rpt_019_precintobulto")
        drParametro.NombreCampoPrecintoParcial = Traduzir("rpt_019_precintoparcial")
        drParametro.NombreCampoPrecintoRemesa = Traduzir("rpt_019_precintoremesa")
        drParametro.NombreCampoPrecintosBulto = Traduzir("rpt_019_precintosbultos")
        drParametro.NombreCampoPrecintosParcial = Traduzir("rpt_019_precintosparciales")
        drParametro.ConteoRemesa = ConteoRemesa
        drParametro.ProcesaParciales = ProcesaParciales

        Parametros.Rows.Add(drParametro)

    End Sub

    Private Sub PopularInformacionCliente(InfoCliente1 As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.InformacionCliente)

        Dim drInformacionCliente As InfoClienteRow = Nothing

        If InfoCliente1 IsNot Nothing Then

            drInformacionCliente = InfoCliente.NewRow

            drInformacionCliente.CodCliente = InfoCliente1.CodCliente & " - " & InfoCliente1.DescCliente
            drInformacionCliente.CodSubCliente = InfoCliente1.CodSubCliente & " - " & InfoCliente1.DescSubCliente
            drInformacionCliente.CodPuntoServicio = InfoCliente1.CodPuntoServicio & " - " & InfoCliente1.DescPuntoServicio
            drInformacionCliente.CodTransporte = InfoCliente1.CodTransporte
            drInformacionCliente.FechaProceso = InfoCliente1.FechaProceso
            drInformacionCliente.FechaTransporte = InfoCliente1.FechaTransporte
            drInformacionCliente.CodDelegacion = InfoCliente1.CodDelegacion
            If Not String.IsNullOrEmpty(InfoCliente1.DesDelegacion) Then
                drInformacionCliente.CodDelegacion &= " - " & InfoCliente1.DesDelegacion
            End If

            InfoCliente.Rows.Add(drInformacionCliente)

        End If

    End Sub

    ''' <summary>
    ''' Preenche os dados da remesa
    ''' </summary>
    ''' <param name="Remesa"></param>
    ''' <remarks></remarks>
    Private Sub PopularDatosRemesa(Remesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        Dim drDatosContenedor As DatosContenedorRow = Nothing

        drDatosContenedor = DatosContenedor.NewRow

        drDatosContenedor.TipoContenedor = "R"
        drDatosContenedor.Ordem = OrdemRemesa
        drDatosContenedor.CodigoUsuario = Remesa.CodUsuario
        drDatosContenedor.CodigoPrecinto = Remesa.CodPrecinto
        drDatosContenedor.FechaConteo = Remesa.FechaFinConteo
        drDatosContenedor.IdentificadorContenedor = Remesa.IdentificadorRemesa
        drDatosContenedor.NumeroDocumento = Remesa.IdentificadorRemesa

        ' ####################################
        ' Preenche os IACs no nível da Remessa
        ' ####################################
        If Remesa.IACs IsNot Nothing AndAlso Remesa.IACs.Count > 0 Then
            drDatosContenedor.InformacionesAdicionales = ""
            For Each objIac In Remesa.IACs
                drDatosContenedor.InformacionesAdicionales &= objIac.DesTermino & ":" & objIac.ValorTermino & "; "
            Next
            If Not String.IsNullOrEmpty(drDatosContenedor.InformacionesAdicionales) Then
                drDatosContenedor.InformacionesAdicionales = drDatosContenedor.InformacionesAdicionales.Substring(0, drDatosContenedor.InformacionesAdicionales.Length - 2) & "."
            End If
        End If

        ' ############################################
        ' Preenche a quantidade de Parciais da Remessa
        ' ############################################
        Dim oNumParciales As Integer = 0
        Dim NumeroParcialesDeclarados As Integer = 0
        For Each b In Remesa.Bultos
            oNumParciales += b.Parciales.Count()
            NumeroParcialesDeclarados += b.NumeroParcialesDeclarados
        Next
        drDatosContenedor.NumeroParcialesContados = oNumParciales
        drDatosContenedor.NumeroParcialesDeclarados = NumeroParcialesDeclarados

        ' #########################################
        ' Preenche os códigos dos Bultos da Remessa
        ' #########################################
        drDatosContenedor.PrecintosBultos = ""
        For Each precintoBulto In Remesa.CodPrecintosBultos.Split(ContractoServ.Constantes.CONST_DELIMITADOR)
            If Not String.IsNullOrEmpty(precintoBulto) Then
                drDatosContenedor.PrecintosBultos &= precintoBulto & "; "
            End If
        Next
        If Not String.IsNullOrEmpty(drDatosContenedor.PrecintosBultos) Then
            drDatosContenedor.PrecintosBultos = drDatosContenedor.PrecintosBultos.Substring(0, drDatosContenedor.PrecintosBultos.Length - 2)
        End If
        objPrecintosBulto = drDatosContenedor.PrecintosBultos

        ' ###########################################
        ' Preenche os códigos das Parciais da Remessa
        ' ###########################################
        drDatosContenedor.PrecintosParciales = ""
        For Each b In Remesa.Bultos
            For Each p In b.Parciales
                If Not String.IsNullOrEmpty(p.NumParcial) Then
                    drDatosContenedor.PrecintosParciales &= p.NumParcial & "; "
                End If
            Next
        Next
        If Not String.IsNullOrEmpty(drDatosContenedor.PrecintosParciales) Then
            drDatosContenedor.PrecintosParciales = drDatosContenedor.PrecintosParciales.Substring(0, drDatosContenedor.PrecintosParciales.Length - 2)
        End If

        ' ############################################
        ' Preenche as intervenções no nível de Remessa
        ' ############################################
        drDatosContenedor.CodigoSupervisor = PreencherIntervenciones(Remesa.Intervenciones, Remesa.IdentificadorRemesa)

        ' ######################################################
        ' Preenche as diferenças encontradas no nível de Remessa
        ' ######################################################
        PreencherDiferencias(Remesa.Diferencias, Remesa.IdentificadorRemesa, Remesa.CodIsoDivisaLocal)

        ' #######################################################
        ' Ordena os dados detalhados de acordo com a divisa local
        ' #######################################################
        Dim objDetallesRemesa As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.EfectivoColeccion
        For Each b In Remesa.Bultos
            If b.Parciales IsNot Nothing AndAlso b.Parciales.Count > 0 Then
                For Each p In b.Parciales
                    If p.Efectivos IsNot Nothing AndAlso p.Efectivos.Count > 0 Then
                        'Ordenando por divisa local e demais divisas
                        Dim efectivos = (From efectivo In p.Efectivos
                                           Select New With {efectivo, .ordem = If(efectivo.CodIsoDivisa = Remesa.CodIsoDivisaLocal, 0, 1)}).ToList
                        efectivos = efectivos.OrderBy(Function(f) f.ordem).ThenBy(Function(f) f.efectivo.NombreDivisa).ToList()
                        For Each Efx In efectivos
                            Dim Ef = Efx.efectivo
                            Dim objDetalle = From det In objDetallesRemesa Where det.CodIsoDivisa = Ef.CodIsoDivisa AndAlso det.Denominacion = Ef.Denominacion AndAlso det.TipoEfectivo = Ef.TipoEfectivo
                            If objDetalle.Count > 0 Then
                                objDetalle.First.Deteriorado += Ef.Deteriorado
                                objDetalle.First.Falso += Ef.Falso
                                objDetalle.First.Unidades += Ef.Unidades
                            Else
                                objDetallesRemesa.Add(New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Efectivo With { _
                                                                            .CodIsoDivisa = Ef.CodIsoDivisa, _
                                                                            .Denominacion = Ef.Denominacion, _
                                                                            .Deteriorado = Ef.Deteriorado, _
                                                                            .Falso = Ef.Falso, _
                                                                            .NombreDivisa = Ef.NombreDivisa, _
                                                                            .TipoEfectivo = Ef.TipoEfectivo, _
                                                                            .Unidades = Ef.Unidades, _
                                                                            .desDenominacion = Ef.desDenominacion})
                            End If
                        Next
                    End If
                Next
            End If
        Next

        ' ########################################
        ' Preenche os detalhes no nível da Remessa
        ' ########################################
        PreencherDetalles(objDetallesRemesa, Remesa.IdentificadorRemesa, Remesa.CodIsoDivisaLocal)

        ' ########################################################################
        ' Adiciona as informações no DataTable DatosContenedor no nível da Remessa
        ' ########################################################################
        DatosContenedor.Rows.Add(drDatosContenedor)

        ' ######################################################################
        ' Preenche o DataTable Parciales com os detalhes de cada um dos parciais
        ' ######################################################################
        If CuentaPorRemesa(Remesa.Bultos(0).Caracteristicas) AndAlso CuentaPorParciales(Remesa.Bultos(0).Caracteristicas) Then
            Dim objParciales As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.ParcialColeccion
            For Each bulto In Remesa.Bultos
                If bulto.Parciales IsNot Nothing AndAlso bulto.Parciales.Count > 0 Then
                    objParciales.AddRange(bulto.Parciales)
                End If
            Next
            PopularDetalleParcialesRemesa(objParciales, Remesa.IdentificadorRemesa, Remesa.InfoCliente.DescCliente, Remesa.InfoCliente.DescSubCliente, Remesa.InfoCliente.DescPuntoServicio, Remesa.CodIsoDivisaLocal)
        End If

    End Sub

    ''' <summary>
    ''' Preeenche os dados do bulto
    ''' </summary>
    ''' <param name="Bultos"></param>
    Private Sub PopularDatosBultos(Bultos As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.BultoColeccion, Remesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        If Bultos IsNot Nothing Then

            Dim drDatosContenedor As DatosContenedorRow = Nothing
            Dim OrdemBulto As Integer = 1

            For Each b In Bultos

                ' incrementa a ordem de apresentação do bulto na lista de datoscontenedor
                ' este valor é utilizado para organizar a ordem das parciais (agrupação)
                OrdemBulto += 1

                ' Verifica se NÃO conta por Remessa
                If Not CuentaPorRemesa(b.Caracteristicas) Then

                    ' Verifica se conta com Parciais
                    If CuentaPorParciales(b.Caracteristicas) Then

                        drDatosContenedor = DatosContenedor.NewRow

                        drDatosContenedor.TipoContenedor = "B"
                        drDatosContenedor.Ordem = (OrdemBulto * OrdemRemesa)
                        drDatosContenedor.PrecintosBultos = objPrecintosBulto
                        drDatosContenedor.CodigoUsuario = b.CodUsuario
                        drDatosContenedor.CodigoPrecinto = b.NumPrecinto
                        drDatosContenedor.FechaConteo = b.FechaFinConteo
                        drDatosContenedor.IdentificadorContenedor = b.IdentificadorBulto
                        drDatosContenedor.NumeroDocumento = b.IdentificadorBulto

                        ' ##########################################
                        ' Preenche os dados de IAC no nível de Bulto
                        ' ##########################################
                        If b.IACs IsNot Nothing AndAlso b.IACs.Count > 0 Then
                            drDatosContenedor.InformacionesAdicionales = ""
                            For Each objIac In b.IACs
                                drDatosContenedor.InformacionesAdicionales &= objIac.DesTermino & ":" & objIac.ValorTermino & "; "
                            Next
                            If Not String.IsNullOrEmpty(drDatosContenedor.InformacionesAdicionales) Then
                                drDatosContenedor.InformacionesAdicionales = drDatosContenedor.InformacionesAdicionales.Substring(0, drDatosContenedor.InformacionesAdicionales.Length - 2) & "."
                            End If
                        End If

                        ' #######################################
                        ' Preenche o número de Parciais do Bulto e Preenche os códigos das Parciais do Bulto
                        ' #######################################
                        drDatosContenedor.PrecintosParciales = ""
                        Dim oNumParciales As Integer = 0
                        For Each p In b.Parciales
                            oNumParciales += 1
                            If Not String.IsNullOrEmpty(p.NumParcial) Then
                                drDatosContenedor.PrecintosParciales &= p.NumParcial & "; "
                            End If
                        Next
                        If Not String.IsNullOrEmpty(drDatosContenedor.PrecintosParciales) Then
                            drDatosContenedor.PrecintosParciales = drDatosContenedor.PrecintosParciales.Substring(0, drDatosContenedor.PrecintosParciales.Length - 2)
                        End If
                        drDatosContenedor.NumeroParcialesContados = oNumParciales
                        drDatosContenedor.NumeroParcialesDeclarados = b.NumeroParcialesDeclarados

                        ' ##########################################
                        ' Preenche as intervençoes no nível de Bulto
                        ' ##########################################
                        drDatosContenedor.CodigoSupervisor = PreencherIntervenciones(b.Intervenciones, b.IdentificadorBulto)

                        ' ########################################
                        ' Preenche as diferenças no nível de Bulto
                        ' ########################################
                        PreencherDiferencias(b.Diferencias, b.IdentificadorBulto, Remesa.CodIsoDivisaLocal)

                        ' ###############################################
                        ' Ordena so detalhes de acordo com a divisa local
                        ' ###############################################
                        Dim objDetallesBulto As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.EfectivoColeccion
                        If b.Parciales IsNot Nothing AndAlso b.Parciales.Count > 0 Then
                            For Each p In b.Parciales
                                If p.Efectivos IsNot Nothing AndAlso p.Efectivos.Count > 0 Then
                                    'Ordenando por divisa local e demais divisas
                                    Dim efectivos = (From efectivo In p.Efectivos
                                                     Select New With {efectivo, .ordem = If(efectivo.CodIsoDivisa = Remesa.CodIsoDivisaLocal, 0, 1)}).ToList
                                    efectivos = efectivos.OrderBy(Function(f) f.ordem).ThenBy(Function(f) f.efectivo.NombreDivisa).ToList()
                                    For Each Efx In efectivos
                                        Dim Ef = Efx.efectivo
                                        Dim objDetalle = From det In objDetallesBulto Where det.CodIsoDivisa = Ef.CodIsoDivisa AndAlso det.Denominacion = Ef.Denominacion AndAlso det.TipoEfectivo = Ef.TipoEfectivo
                                        If objDetalle.Count > 0 Then
                                            objDetalle.First.Deteriorado += Ef.Deteriorado
                                            objDetalle.First.Falso += Ef.Falso
                                            objDetalle.First.Unidades += Ef.Unidades
                                        Else
                                            objDetallesBulto.Add(New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Efectivo With { _
                                                                                        .CodIsoDivisa = Ef.CodIsoDivisa, _
                                                                                        .Denominacion = Ef.Denominacion, _
                                                                                        .Deteriorado = Ef.Deteriorado, _
                                                                                        .Falso = Ef.Falso, _
                                                                                        .NombreDivisa = Ef.NombreDivisa, _
                                                                                        .TipoEfectivo = Ef.TipoEfectivo, _
                                                                                        .Unidades = Ef.Unidades, _
                                                                                        .desDenominacion = Ef.desDenominacion})
                                        End If
                                    Next
                                End If

                                ' #############################################################
                                ' Preenche o DataTable de Parciales com os detalhes da contagem
                                ' #############################################################
                                PopularDetalleParcialesBulto(p, b.IdentificadorBulto, Remesa.InfoCliente.DescCliente, Remesa.InfoCliente.DescSubCliente, Remesa.InfoCliente.DescPuntoServicio, Remesa.CodIsoDivisaLocal)

                            Next

                        End If

                        ' ######################################
                        ' Preenche os detalhes no nível do Bulto
                        ' ######################################
                        PreencherDetalles(objDetallesBulto, b.IdentificadorBulto, Remesa.CodIsoDivisaLocal)

                        ' ########################################################################
                        ' Adiciona as informações no DataTable DatosContenedor no nível da Remessa
                        ' ########################################################################
                        DatosContenedor.Rows.Add(drDatosContenedor)

                    End If

                    ' Preenche os dados no nível da Parcial
                    PopularDatosParciales(b.Parciales, b.NumPrecinto, b.Caracteristicas, Remesa.CodPrecinto, Remesa.CodIsoDivisaLocal, (OrdemBulto * OrdemRemesa))

                ElseIf CuentaPorRemesa(b.Caracteristicas) AndAlso CuentaPorParciales(b.Caracteristicas) Then

                    ' Preenche os dados no nível da Parcial
                    PopularDatosParciales(b.Parciales, "", b.Caracteristicas, Remesa.CodPrecinto, Remesa.CodIsoDivisaLocal, (OrdemBulto * OrdemRemesa))

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Popopula os Detalhes dos parciais de la remesa
    ''' </summary>
    ''' <param name="objParcial"></param>
    ''' <param name="IdentificadorContenedor"></param>
    ''' <param name="Cliente"></param>
    ''' <param name="SubCliente"></param>
    ''' <param name="PuntoServicio"></param>
    ''' <param name="CodIsoDivisaLocal"></param>
    ''' <remarks></remarks>
    Private Sub PopularDetalleParcialesBulto(objParcial As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Parcial, IdentificadorContenedor As String, _
                                                                                  Cliente As String, SubCliente As String, PuntoServicio As String, CodIsoDivisaLocal As String)

        If objParcial IsNot Nothing Then

            Dim ListaDivisas As Dictionary(Of String, String)

            ListaDivisas = New Dictionary(Of String, String)

            If objParcial.Efectivos IsNot Nothing AndAlso objParcial.Efectivos.Count > 0 Then
                For Each Ef In objParcial.Efectivos
                    If Not ListaDivisas.ContainsKey(Ef.CodIsoDivisa) Then
                        ListaDivisas.Add(Ef.CodIsoDivisa, Ef.NombreDivisa)
                    End If
                Next
            End If
            If objParcial.MediosPagos IsNot Nothing AndAlso objParcial.MediosPagos.Count > 0 Then
                For Each Mp In objParcial.MediosPagos
                    If Not ListaDivisas.ContainsKey(Mp.CodIsoDivisa) Then
                        ListaDivisas.Add(Mp.CodIsoDivisa, Mp.DescripcionDivisa)
                    End If
                Next
            End If
            If objParcial.Declarados IsNot Nothing AndAlso objParcial.Declarados.Count > 0 Then
                For Each De In objParcial.Declarados
                    If Not ListaDivisas.ContainsKey(De.CodIsoDivisa) Then
                        ListaDivisas.Add(De.CodIsoDivisa, De.DescripcionDivisa)
                    End If
                Next
            End If

            For Each Di In ListaDivisas
                Dim DiLocal = Di

                Dim ImporteDeclaradoEfectivo As Decimal = RetornarImporteDeclarado(objParcial.Declarados, Enumeradores.TipoMedioPago.Efectivo, Di.Key)
                Dim ImporteContadoEfectivo As Decimal = 0
                If objParcial.Efectivos IsNot Nothing AndAlso objParcial.Efectivos.Count > 0 Then
                    For Each Ef In objParcial.Efectivos.Where(Function(e) e.CodIsoDivisa = DiLocal.Key)
                        ImporteContadoEfectivo += Ef.Denominacion * (Ef.Deteriorado + Ef.Unidades)
                    Next
                End If
                If ImporteDeclaradoEfectivo > 0 OrElse ImporteContadoEfectivo > 0 Then
                    AdicionarLinhaParcial(Cliente, SubCliente, PuntoServicio, objParcial.NumParcial, ImporteContadoEfectivo, ImporteDeclaradoEfectivo, IdentificadorContenedor, Di.Value)
                End If

                If objParcial.MediosPagos IsNot Nothing AndAlso objParcial.MediosPagos.Count > 0 Then
                    For Each Mp In objParcial.MediosPagos.Where(Function(e) e.CodIsoDivisa = DiLocal.Key)
                        AdicionarLinhaMedioPago(Mp.CodTipoMedioPago, objParcial.Declarados, Mp.CodIsoDivisa, Cliente, SubCliente, PuntoServicio, objParcial.NumParcial, IdentificadorContenedor, Mp.Importe, Di.Value, Mp.NombreMedioPago)
                    Next
                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Popopula os Detalhes dos parciais de la remesa
    ''' </summary>
    ''' <param name="objParciales"></param>
    ''' <param name="IdentificadorContenedor"></param>
    ''' <param name="Cliente"></param>
    ''' <param name="SubCliente"></param>
    ''' <param name="PuntoServicio"></param>
    ''' <param name="CodIsoDivisaLocal"></param>
    ''' <remarks></remarks>
    Private Sub PopularDetalleParcialesRemesa(objParciales As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.ParcialColeccion, IdentificadorContenedor As String, _
                                                                                      Cliente As String, SubCliente As String, PuntoServicio As String, CodIsoDivisaLocal As String)

        If objParciales IsNot Nothing AndAlso objParciales.Count > 0 Then

            Dim ListaDivisas As Dictionary(Of String, String)

            For Each Parc In objParciales

                ListaDivisas = New Dictionary(Of String, String)

                If Parc.Efectivos IsNot Nothing AndAlso Parc.Efectivos.Count > 0 Then
                    For Each Ef In Parc.Efectivos
                        If Not ListaDivisas.ContainsKey(Ef.CodIsoDivisa) Then
                            ListaDivisas.Add(Ef.CodIsoDivisa, Ef.NombreDivisa)
                        End If
                    Next
                End If
                If Parc.MediosPagos IsNot Nothing AndAlso Parc.MediosPagos.Count > 0 Then
                    For Each Mp In Parc.MediosPagos
                        If Not ListaDivisas.ContainsKey(Mp.CodIsoDivisa) Then
                            ListaDivisas.Add(Mp.CodIsoDivisa, Mp.DescripcionDivisa)
                        End If
                    Next
                End If
                If Parc.Declarados IsNot Nothing AndAlso Parc.Declarados.Count > 0 Then
                    For Each De In Parc.Declarados
                        If Not ListaDivisas.ContainsKey(De.CodIsoDivisa) Then
                            ListaDivisas.Add(De.CodIsoDivisa, De.DescripcionDivisa)
                        End If
                    Next
                End If

                For Each Di In ListaDivisas
                    Dim DiLocal = Di

                    Dim ImporteDeclaradoEfectivo As Decimal = RetornarImporteDeclarado(Parc.Declarados, Enumeradores.TipoMedioPago.Efectivo, Di.Key)
                    Dim ImporteContadoEfectivo As Decimal = 0
                    If Parc.Efectivos IsNot Nothing AndAlso Parc.Efectivos.Count > 0 Then
                        For Each Ef In Parc.Efectivos.Where(Function(e) e.CodIsoDivisa = DiLocal.Key)
                            ImporteContadoEfectivo += Ef.Denominacion * (Ef.Deteriorado + Ef.Unidades)
                        Next
                    End If
                    If ImporteDeclaradoEfectivo > 0 OrElse ImporteContadoEfectivo > 0 Then
                        AdicionarLinhaParcial(Cliente, SubCliente, PuntoServicio, Parc.NumParcial, ImporteContadoEfectivo, ImporteDeclaradoEfectivo, IdentificadorContenedor, Di.Value)
                    End If

                    If Parc.MediosPagos IsNot Nothing AndAlso Parc.MediosPagos.Count > 0 Then
                        For Each Mp In Parc.MediosPagos.Where(Function(e) e.CodIsoDivisa = DiLocal.Key)
                            AdicionarLinhaMedioPago(Mp.CodTipoMedioPago, Parc.Declarados, Mp.CodIsoDivisa, Cliente, SubCliente, PuntoServicio, Parc.NumParcial, IdentificadorContenedor, Mp.Importe, Di.Value, Mp.NombreMedioPago)
                        Next
                    End If

                Next

            Next

        End If

    End Sub

    ''' <summary>
    ''' Adiciona Linha Medio Pago
    ''' </summary>
    ''' <param name="CodTipoMedioPago"></param>
    ''' <param name="objDeclarados"></param>
    ''' <param name="CodIsoDivisa"></param>
    ''' <param name="Cliente"></param>
    ''' <param name="SubCliente"></param>
    ''' <param name="PuntoServicio"></param>
    ''' <param name="NumParcial"></param>
    ''' <param name="IdentificadorContenedor"></param>
    ''' <param name="Importe"></param>
    ''' <param name="DescripcionDivisa"></param>
    ''' <param name="NombreMedioPago"></param>
    ''' <remarks></remarks>
    Private Sub AdicionarLinhaMedioPago(CodTipoMedioPago As String, objDeclarados As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion, CodIsoDivisa As String, _
                                                                            Cliente As String, SubCliente As String, PuntoServicio As String, NumParcial As String, IdentificadorContenedor As String, _
                                                                            Importe As Decimal, DescripcionDivisa As String, NombreMedioPago As String)

        Dim ImporteDeclarado As Decimal = 0

        Select Case CodTipoMedioPago

            Case ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_CHEQUE

                ImporteDeclarado = RetornarImporteDeclarado(objDeclarados, Enumeradores.TipoMedioPago.Cheque, CodIsoDivisa)

            Case ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_OTROSVALORES

                ImporteDeclarado = RetornarImporteDeclarado(objDeclarados, Enumeradores.TipoMedioPago.OtroValor, CodIsoDivisa)

            Case ContractoServ.Constantes.COD_TIPO_MEDIO_PAGO_TICKET

                ImporteDeclarado = RetornarImporteDeclarado(objDeclarados, Enumeradores.TipoMedioPago.Ticket, CodIsoDivisa)

        End Select

        'Adiciona a linha de efectivos
        AdicionarLinhaParcial(Cliente, SubCliente, PuntoServicio, NumParcial, Importe, ImporteDeclarado, IdentificadorContenedor, DescripcionDivisa & " - " & NombreMedioPago)

    End Sub

    ''' <summary>
    ''' Adiciona a linha do parcial
    ''' </summary>
    ''' <param name="Cliente"></param>
    ''' <param name="SubCliente"></param>
    ''' <param name="PuntoServicio"></param>
    ''' <param name="NumParcial"></param>
    ''' <param name="ImporteContado"></param>
    ''' <param name="ImporteDeclarado"></param>
    ''' <param name="IdentificadorContenedor"></param>
    ''' <remarks></remarks>
    Private Sub AdicionarLinhaParcial(Cliente As String, SubCliente As String, PuntoServicio As String, NumParcial As String, _
                                                                    ImporteContado As Decimal, ImporteDeclarado As Decimal, IdentificadorContenedor As String, NombreDivisa As String)

        Dim drParciales As ParcialesRow = Nothing

        drParciales = Parciales.NewRow

        drParciales.Cliente = Cliente
        drParciales.SubCliente = SubCliente
        drParciales.PtoServico = PuntoServicio
        drParciales.NumeroParcial = NumParcial
        drParciales.Declarado = ImporteDeclarado
        drParciales.Contado = ImporteContado
        drParciales.IdentificadorContenedor = IdentificadorContenedor
        drParciales.Divisa = NombreDivisa

        Parciales.Rows.Add(drParciales)
    End Sub

    ''' <summary>
    ''' Retorna o Importe do declarado
    ''' </summary>
    ''' <param name="objDeclarados"></param>
    ''' <param name="TipoDeclarado"></param>
    ''' <param name="CodigoIsoDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornarImporteDeclarado(objDeclarados As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion, TipoDeclarado As Enumeradores.TipoMedioPago, _
                                                                                      CodigoIsoDivisa As String) As Decimal

        Dim ImporteDeclarado As Decimal = 0

        If objDeclarados IsNot Nothing AndAlso objDeclarados.Count > 0 Then

            Select Case TipoDeclarado

                Case Enumeradores.TipoMedioPago.Efectivo
                    ImporteDeclarado = (From dec In objDeclarados Where dec.CodIsoDivisa = CodigoIsoDivisa Group By dec.CodIsoDivisa Into Importe = Sum(dec.ImporteEfectivo) Select Importe).SingleOrDefault
                Case Enumeradores.TipoMedioPago.Cheque
                    ImporteDeclarado = (From dec In objDeclarados Where dec.CodIsoDivisa = CodigoIsoDivisa Group By dec.CodIsoDivisa Into Importe = Sum(dec.ImporteCheque) Select Importe).SingleOrDefault
                Case Enumeradores.TipoMedioPago.Ticket
                    ImporteDeclarado = (From dec In objDeclarados Where dec.CodIsoDivisa = CodigoIsoDivisa Group By dec.CodIsoDivisa Into Importe = Sum(dec.ImporteTicket) Select Importe).SingleOrDefault
                Case Enumeradores.TipoMedioPago.OtroValor
                    ImporteDeclarado = (From dec In objDeclarados Where dec.CodIsoDivisa = CodigoIsoDivisa Group By dec.CodIsoDivisa Into Importe = Sum(dec.ImporteOtroValor) Select Importe).SingleOrDefault
            End Select

        End If

        Return ImporteDeclarado
    End Function

    ''' <summary>
    ''' Preenche os dados dos parciais
    ''' </summary>
    ''' <param name="objParciales"></param>
    ''' <param name="PrecintoBulto"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/07/2011 - Criado
    ''' </history>
    Private Sub PopularDatosParciales(objParciales As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.ParcialColeccion, _
                                       PrecintoBulto As String, _
                                       Caracteristicas As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.CaracteristicaColeccion, _
                                       PrecintoRemesa As String, divisaLocal As String, OrdemBulto As Integer)

        Dim drDatosContenedor As DatosContenedorRow = Nothing
        Dim OrdemParcial As Integer = 0

        If objParciales IsNot Nothing AndAlso objParciales.Count > 0 Then

            For Each p In objParciales

                OrdemParcial += 1

                drDatosContenedor = DatosContenedor.NewRow

                drDatosContenedor.Ordem = (OrdemBulto + OrdemParcial)
                drDatosContenedor.PrecintosBultos = objPrecintosBulto
                drDatosContenedor.CodigoUsuario = p.CodUsuario
                drDatosContenedor.FechaConteo = p.FechaFinConteo
                drDatosContenedor.IdentificadorContenedor = p.IdentificadorParcial
                drDatosContenedor.NumeroParcial = p.NumParcial
                drDatosContenedor.NumeroDocumento = p.IdentificadorParcial

                If CuentaPorRemesa(Caracteristicas) AndAlso Not CuentaPorParciales(Caracteristicas) Then
                    drDatosContenedor.TipoContenedor = "R"
                    drDatosContenedor.CodigoPrecinto = PrecintoRemesa
                ElseIf CuentaPorRemesa(Caracteristicas) AndAlso CuentaPorParciales(Caracteristicas) Then
                    drDatosContenedor.TipoContenedor = "P"
                    drDatosContenedor.CodigoPrecinto = p.NumParcial
                ElseIf Not CuentaPorRemesa(Caracteristicas) AndAlso Not CuentaPorParciales(Caracteristicas) Then
                    drDatosContenedor.TipoContenedor = "B"
                    drDatosContenedor.CodigoPrecinto = PrecintoBulto
                Else
                    drDatosContenedor.TipoContenedor = "P"
                    drDatosContenedor.CodigoPrecinto = PrecintoBulto
                End If

                ' ############################################
                ' Preenche os dados de IAC no nível de Parcial
                ' ############################################
                If p.IACs IsNot Nothing AndAlso p.IACs.Count > 0 Then
                    drDatosContenedor.InformacionesAdicionales = ""
                    For Each objIac In p.IACs
                        drDatosContenedor.InformacionesAdicionales &= objIac.DesTermino & ":" & objIac.ValorTermino & "; "
                    Next
                    If Not String.IsNullOrEmpty(drDatosContenedor.InformacionesAdicionales) Then
                        drDatosContenedor.InformacionesAdicionales = drDatosContenedor.InformacionesAdicionales.Substring(0, drDatosContenedor.InformacionesAdicionales.Length - 2) & "."
                    End If
                End If

                ' ############################################
                ' Preenche as intervenções no nível de Parcial
                ' ############################################
                drDatosContenedor.CodigoSupervisor = PreencherIntervenciones(p.Intervenciones, p.IdentificadorParcial)

                ' ##########################################
                ' Preenche as diferenças no nível de Parcial
                ' ##########################################
                PreencherDiferencias(p.Diferencias, p.IdentificadorParcial, divisaLocal)

                ' #####################################
                ' Ordena os detalhes por divisa do país
                ' ######################################
                Dim objDetallesParcial As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.EfectivoColeccion
                If p.Efectivos IsNot Nothing AndAlso p.Efectivos.Count > 0 Then
                    'Ordenando por divisa local e demais divisas
                    Dim efectivos = (From efectivo In p.Efectivos
                                     Select New With {efectivo, .ordem = If(efectivo.CodIsoDivisa = divisaLocal, 0, 1)}).ToList
                    efectivos = efectivos.OrderBy(Function(f) f.ordem).ThenBy(Function(f) f.efectivo.NombreDivisa).ToList()
                    For Each Efx In efectivos
                        Dim Ef = Efx.efectivo
                        Dim objDetalle = From det In objDetallesParcial Where det.CodIsoDivisa = Ef.CodIsoDivisa AndAlso det.Denominacion = Ef.Denominacion AndAlso det.TipoEfectivo = Ef.TipoEfectivo
                        If objDetalle.Count > 0 Then
                            objDetalle.First.Deteriorado += Ef.Deteriorado
                            objDetalle.First.Falso += Ef.Falso
                            objDetalle.First.Unidades += Ef.Unidades
                        Else
                            objDetallesParcial.Add(New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Efectivo With { _
                                                                        .CodIsoDivisa = Ef.CodIsoDivisa, _
                                                                        .Denominacion = Ef.Denominacion, _
                                                                        .Deteriorado = Ef.Deteriorado, _
                                                                        .Falso = Ef.Falso, _
                                                                        .NombreDivisa = Ef.NombreDivisa, _
                                                                        .TipoEfectivo = Ef.TipoEfectivo, _
                                                                        .Unidades = Ef.Unidades, _
                                                                        .desDenominacion = Ef.desDenominacion})
                        End If
                    Next
                End If

                ' ########################################
                ' Preenche os detalhes no nível de Parcial
                ' ########################################
                PreencherDetalles(objDetallesParcial, p.IdentificadorParcial, divisaLocal)

                ' ########################################################################
                ' Adiciona as informações no DataTable DatosContenedor no nível de Parcial
                ' ########################################################################
                DatosContenedor.Rows.Add(drDatosContenedor)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche as intervenções do contenedor
    ''' </summary>
    ''' <param name="objIntervenciones"></param>
    ''' <param name="IdentificadorContenedor"></param>
    ''' <remarks></remarks>
    Private Function PreencherIntervenciones(objIntervenciones As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IntervencionColeccion, _
                                                                        IdentificadorContenedor As String) As String

        Dim oCodSupervisor As String = ""

        If objIntervenciones IsNot Nothing AndAlso objIntervenciones.Count > 0 Then

            Dim drIntervencion As IntervencionesRow = Nothing
            Dim drMotivos As MotivosRow = Nothing

            For Each Intervencion In objIntervenciones

                drIntervencion = Intervenciones.NewRow

                drIntervencion.IdentificadorContenedor = IdentificadorContenedor
                drIntervencion.Incidencia = Intervencion.Comentario
                drIntervencion.CodigoSupervisor = Intervencion.CodSupervisor
                drIntervencion.IdentificadorIntervencion = Intervencion.IdentificadorIntervencion
                oCodSupervisor = Intervencion.CodSupervisor

                For Each objMotivo In Intervencion.Motivos
                    drMotivos = Motivos.NewRow

                    drMotivos.IdentificadorContenedor = IdentificadorContenedor
                    drMotivos.IdentificadorIntervencion = Intervencion.IdentificadorIntervencion
                    drMotivos.CodigoMotivo = objMotivo

                    Motivos.Rows.Add(drMotivos)
                Next

                Intervenciones.Rows.Add(drIntervencion)

            Next

        End If

        Return oCodSupervisor
    End Function

    ''' <summary>
    ''' Preenche as diferenças do contenedor
    ''' </summary>
    ''' <param name="objDiferencias"></param>
    ''' <param name="IdentificadorContenedor"></param>
    ''' <remarks></remarks>
    Private Sub PreencherDiferencias(objDiferencias As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion, _
                                                                  IdentificadorContenedor As String, CodIsoDivisaLocal As String)

        If objDiferencias IsNot Nothing AndAlso objDiferencias.Count > 0 Then

            Dim drDiferencia As DiferenciasRow = Nothing

            'Ordenando por divisa local e demais divisas
            Dim DiferenciasOrdenadas = (From efectivo In objDiferencias
                             Select New With {efectivo, .ordemDivisa = If(efectivo.CodIsoDivisa = CodIsoDivisaLocal, 0, 1), .ordemMedioPago = If(efectivo.EsEfectivo, 1, If(String.IsNullOrEmpty(efectivo.CodTipoMedioPago), 3, 2))}).ToList
            DiferenciasOrdenadas = DiferenciasOrdenadas.OrderBy(Function(f) f.ordemDivisa).ThenBy(Function(f) f.efectivo.NombreDivisa).ThenBy(Function(f) f.ordemMedioPago).ToList()

            For Each Difx In DiferenciasOrdenadas

                ' Solicitação feita por Raul Ruiz Blanco, o importe total não é considerado em este relatório
                If Difx.efectivo.EsEfectivo OrElse Not String.IsNullOrEmpty(Difx.efectivo.CodTipoMedioPago) Then

                    Dim Dif = Difx.efectivo

                    drDiferencia = Diferencias.NewRow

                    drDiferencia.Contado = Dif.Contado
                    drDiferencia.Declarado = Dif.Declarado
                    drDiferencia.Falsos = Dif.Falsos
                    drDiferencia.Falta = If((Dif.Contado - Dif.Declarado + Dif.Falsos) < 0, Math.Abs(Dif.Contado - Dif.Declarado + Dif.Falsos), 0)
                    drDiferencia.IdentificadorContenedor = IdentificadorContenedor
                    If Dif.EsEfectivo Then
                        drDiferencia.Moneda = Dif.NombreDivisa
                    Else
                        drDiferencia.Moneda = Dif.NombreDivisa & " - " & Dif.NombreMedioPago
                    End If
                    drDiferencia.Sobra = If((Dif.Contado - Dif.Declarado + Dif.Falsos) > 0, Math.Abs(Dif.Contado - Dif.Declarado + Dif.Falsos), 0)

                    Diferencias.Rows.Add(drDiferencia)

                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche os detalhes da contagem
    ''' </summary>
    ''' <param name="objDetalles"></param>
    ''' <param name="IdentificadorContenedor"></param>
    ''' <remarks></remarks>
    Private Sub PreencherDetalles(objDetalles As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.EfectivoColeccion, _
                                  IdentificadorContenedor As String, divisaLocal As String)

        If objDetalles IsNot Nothing AndAlso objDetalles.Count > 0 Then

            Dim drDetalles As DetalleConteoRow = Nothing

            'Ordenando por divisa local e demais divisas
            Dim DetallesOrdenadas = (From detalle In objDetalles
                             Select New With {detalle, .ordem = If(detalle.CodIsoDivisa = divisaLocal, 0, 1)}).ToList
            DetallesOrdenadas = DetallesOrdenadas.OrderBy(Function(f) f.ordem).ThenBy(Function(f) f.detalle.NombreDivisa).ThenByDescending(Function(f) f.detalle.Denominacion).ToList()

            ' Para cada divisa existente
            For Each d In DetallesOrdenadas

                ' Recupera as informações da denominação
                Dim det As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Efectivo = d.detalle

                ' Verifica se o valor da denominação é maior que 0
                If det.Denominacion > 0 Then

                    drDetalles = DetalleConteo.NewRow

                    drDetalles.Denominacion = det.desDenominacion
                    drDetalles.Deteriorados = det.Deteriorado
                    drDetalles.Falsos = det.Falso
                    drDetalles.IdentificadorContenedor = IdentificadorContenedor
                    drDetalles.Importe = (det.Denominacion * (det.Deteriorado + det.Unidades))
                    drDetalles.Unidades = det.Unidades
                    drDetalles.CodIsoDivisa = det.CodIsoDivisa
                    drDetalles.TipoEfectivo = det.TipoEfectivo

                    If det.CodIsoDivisa = divisaLocal Then
                        drDetalles.Ordem = 0
                    Else
                        drDetalles.Ordem = 1
                    End If

                    DetalleConteo.Rows.Add(drDetalles)

                End If

            Next

        End If

    End Sub

    Private Function HayDiferenciasBultos(Remesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa) As Boolean

        If Remesa.Bultos IsNot Nothing AndAlso Remesa.Bultos.Count > 0 Then

            For Each b In Remesa.Bultos
                If b.Diferencias IsNot Nothing AndAlso b.Diferencias.Count > 0 AndAlso (From dif In b.Diferencias Where dif.Diferencia <> 0).Count > 0 Then Return True
            Next

        End If

        Return False
    End Function

    Private Function HayDiferenciasParciales(Bultos As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.BultoColeccion) As Boolean

        If Bultos IsNot Nothing AndAlso Bultos.Count > 0 Then

            For Each b In Bultos

                If b.Parciales IsNot Nothing AndAlso b.Parciales.Count > 0 Then

                    For Each p In b.Parciales
                        If p.Diferencias IsNot Nothing AndAlso p.Diferencias.Count > 0 AndAlso (From dif In p.Diferencias Where dif.Diferencia <> 0 OrElse dif.Falsos > 0).Count > 0 Then Return True
                    Next

                End If

            Next

        End If

        Return False

    End Function

    Private Function HayDiferenciasParciales(Bulto As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Bulto) As Boolean

        If Bulto.Parciales IsNot Nothing AndAlso Bulto.Parciales.Count > 0 Then

            For Each p In Bulto.Parciales
                If p.Diferencias IsNot Nothing AndAlso p.Diferencias.Count > 0 AndAlso (From dif In p.Diferencias Where dif.Diferencia <> 0 OrElse dif.Falsos > 0).Count > 0 Then Return True
            Next

        End If

        Return False

    End Function

End Class
