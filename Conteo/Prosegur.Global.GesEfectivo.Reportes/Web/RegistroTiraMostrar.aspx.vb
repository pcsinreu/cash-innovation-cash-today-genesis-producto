Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.Reportes.Web.Enumeradores
Imports Prosegur.Genesis.Comunicacion

Public Class RegistroTiraMostrar
    Inherits System.Web.UI.Page

#Region "[ATRIBUTOS]"

    Private _IdTira As String
    Private row As GridViewRow = Nothing

    Public Property DelegacionConectada() As Dictionary(Of String, String)
        Get
            Return Session("BaseDelegacionConectada")
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            Session("BaseDelegacionConectada") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena login do usuário logado.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 25/03/2009 Criado
    ''' </history>
    Public Property LoginUsuario() As String
        Get
            Return Session("BaseLoginUsuario")
        End Get
        Set(ByVal value As String)
            Session("BaseLoginUsuario") = value
        End Set
    End Property
#End Region

#Region "[MÉTODOS]"

    Protected Sub TraduzirControles()

        Me.Page.Title = Traduzir("015_titulo_pagina")
        lblIdentificacionATM.Text = Traduzir("015_pnlATM")
        lblCodigoATM.Text = Traduzir("015_lblCodigoATM")
        lblCodCliente.Text = Traduzir("015_lblCliente")
        lblCodSubcliente.Text = Traduzir("015_lblSubcliente")
        lblCodPtoServicio.Text = Traduzir("015_lblPuntoServicio")
        'btnBuscar.Titulo = Traduzir("014_btnBuscar")
        'btnLimpar.Titulo = Traduzir("btnLimpiar")
        lblHabilitaciones.Text = Traduzir("015_pnlHabilitaciones")

        tdDeclaradoDispensadores.InnerText = Traduzir("015_tdDeclaradoDispensadores")
        tdCajetines.InnerText = Traduzir("015_tdCajetines")
        tdCantidad.InnerText = Traduzir("015_tdCantidad")
        tdImporte.InnerText = Traduzir("015_tdImporte")
        tdDivisa.InnerText = Traduzir("015_tdDivisa")
        tdConteoDispensadores.InnerText = Traduzir("015_tdConteoDispensadores")
        tdCajetinesConteo.InnerText = Traduzir("015_tdCajetinesConteo")
        tdDivisaConteo.InnerText = Traduzir("015_tdDivisaConteo")
        tdIngresadoresDec.InnerText = Traduzir("015_lblDeclaradosIngresadores")
        tdIngresadoresCont.InnerText = Traduzir("015_lblContadosIngresadores")
        tdDepositoDec.InnerText = Traduzir("015_lblDeclaradosDepositos")
        tdDepositoCont.InnerText = Traduzir("015_lblContadosDepositos")

        lblPeriodoContable.Text = Traduzir("015_lblPeriodoContable")
        lblFecha.Text = Traduzir("015_lblFecha")

        Msg001.Text = Traduzir("015_msg001")
        Msg002.Text = Traduzir("015_msg002")
        Msg003.Text = Traduzir("015_msg003")
        Msg004.Text = Traduzir("015_msg004")
        Msg005.Text = Traduzir("015_msg005")
        Msg006.Text = Traduzir("015_msg006")
        Msg007.Text = Traduzir("015_msg007")

    End Sub


    ''' <summary>
    ''' Recupera os parametros passados para a página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos] 30/06/2011 Criado    
    ''' </history>
    Private Sub RecuperarParametros()

        ' Se existir
        If (Request.QueryString("IdTira") IsNot Nothing) Then
            ' Recupera o idTira
            _IdTira = Request.QueryString("IdTira")
        End If

    End Sub

    ''' <summary>
    ''' Carrega os dados no banco e preenche o grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos] 30/06/2011 Criado    
    ''' </history>
    Private Sub CarregarDados()

        Try

            ' Cria objeto para chamada do serviço
            Dim objAtmIntegracion As New ProxyAtmIntegracion

            ' Define os valores dos filtros da consulta
            Dim objPeticion As New ATM.ContractoServicio.Integracion.GetTirasDetail.Peticion
            objPeticion.OidTira = _IdTira

            ' Recupera os dados do tira para popular o grid
            Dim objRespuesta As ATM.ContractoServicio.Integracion.GetTirasDetail.Respuesta = objAtmIntegracion.GetTirasDetail(objPeticion)

            ' Verifica se aconteceu algum erro ao pesquisar os dados do relatório      
            Dim msgErro As String = String.Empty
            If Not Master.ControleErro.VerificaErro2(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, msgErro, objRespuesta.MensajeError, True) Then
                MostraMensagem(msgErro)
                Exit Sub
            End If

            'Preechimento dos campos de identificação do ATM
            txtCodigoATM.Text = objRespuesta.Tira.CodCajero
            txtCodCliente.Text = objRespuesta.Tira.CodCliente & " - " & objRespuesta.Tira.DesCliente
            txtCodSubcliente.Text = objRespuesta.Tira.CodSubcliente & " - " & objRespuesta.Tira.DesSubcliente
            txtCodPtoServicio.Text = objRespuesta.Tira.CodPtoServicio & " - " & objRespuesta.Tira.DesPtoServicio
            txtPeriodoContable.Text = objRespuesta.Tira.DesPeriodoContable
            If Not String.IsNullOrEmpty(txtPeriodoContable.Text) Then
                lblPeriodoContable.Visible = True
                txtPeriodoContable.Visible = True
            End If
            If objRespuesta.Tira.FyhFecha IsNot Nothing Then
                txtFecha.Text = CType(objRespuesta.Tira.FyhFecha, Date).ToString("dd/MM/yyyy HH:mm:ss")
            End If
            If Not String.IsNullOrEmpty(txtFecha.Text) Then
                lblFecha.Visible = True
                txtFecha.Visible = True
            End If

            'Mescla lista com sublista "HABILITAÇÕES"
            Dim objH = (From objHab In objRespuesta.Habilitacion _
                        From objhabcom In objHab.HabilitacioneComponentes _
                        Order By objHab.FyhHabilitacion, objhabcom.DesDivisa, objhabcom.NumValorFacial, objhabcom.NecOrden
                        Select New With {objHab.CodRemesaLegado, objHab.FyhHabilitacion, objhabcom.CodInventarioContenedor,
                        objhabcom.CodPrecintoContenedor, objhabcom.CodMorfologiaComponente, objhabcom.CodIsoDivisa,
                        objhabcom.DesDivisa, objhabcom.DesDenominacion, objhabcom.NumValorFacial, objhabcom.NelCantidadHabilitada, objhabcom.NumImporteHabilitado,
                        .TotalDivisa = 0, .Divisa = "", .exibeInv = (Not String.IsNullOrEmpty(objhabcom.CodInventarioContenedor)),
                        .exibePre = (Not String.IsNullOrEmpty(objhabcom.CodPrecintoContenedor))}).ToList()

            'Agrupa campo de totalização da tabela "objH"
            Dim exibeInv As Boolean = False
            Dim exibePre As Boolean = False
            Dim soma As Decimal = 0
            For i = 0 To objH.Count - 1
                If objH(i).exibeInv AndAlso Not exibeInv Then
                    exibeInv = True
                End If
                If objH(i).exibePre AndAlso Not exibePre Then
                    exibePre = True
                End If

                If i < objH.Count - 1 Then
                    soma += objH(i).NumImporteHabilitado
                    If objH(i).FyhHabilitacion <> objH(i + 1).FyhHabilitacion OrElse objH(i).DesDivisa <> objH(i + 1).DesDivisa Then
                        objH(i).TotalDivisa = soma
                        objH(i).Divisa = objH(i).DesDivisa
                        soma = 0
                    End If
                Else
                    soma += objH(i).NumImporteHabilitado
                    objH(i).TotalDivisa = soma
                    objH(i).Divisa = objH(i).DesDivisa
                End If
            Next
            Msg001.Visible = objH Is Nothing OrElse objH.Count = 0
            If Not exibeInv Then gdvHabilitaciones.Columns(2).Visible = False
            If Not exibePre Then gdvHabilitaciones.Columns(3).Visible = False

            'Cria objto de totalização por cajetín das "HABILITAÇÕES"
            Dim objHabTotal = (From objHab In objRespuesta.Habilitacion _
                        From objhabcom In objHab.HabilitacioneComponentes _
                        Order By objhabcom.DesDivisa, objhabcom.NumValorFacial, objhabcom.NecOrden
                        Group objhabcom By objhabcom.CodMorfologiaComponente, objhabcom.CodIsoDivisa,
                        objhabcom.DesDivisa, objhabcom.DesDenominacion, objhabcom.NumValorFacial Into Group
                        Select New With {CodMorfologiaComponente, CodIsoDivisa,
                        DesDivisa, DesDenominacion, NumValorFacial,
                        .NelCantidadHabilitada = Group.Sum(Function(c) c.NelCantidadHabilitada),
                        .NumImporteHabilitado = Group.Sum(Function(c) c.NumImporteHabilitado),
                        .TotalDivisa = 0, .Divisa = "", .Confere = True, .ConfereTot = True}).ToList()

            'Agrupa campo de totalização da tabela "objHabTotal"
            soma = 0
            For i = 0 To objHabTotal.Count - 1
                If i < objHabTotal.Count - 1 Then
                    soma += objHabTotal(i).NumImporteHabilitado
                    If objHabTotal(i).DesDivisa <> objHabTotal(i + 1).DesDivisa Then
                        objHabTotal(i).TotalDivisa = soma
                        objHabTotal(i).Divisa = objHabTotal(i).DesDivisa
                        soma = 0
                    End If
                Else
                    soma += objHabTotal(i).NumImporteHabilitado
                    objHabTotal(i).TotalDivisa = soma
                    objHabTotal(i).Divisa = objHabTotal(i).DesDivisa
                End If
            Next

            'Mescla lista com sublista "TIRA DECLARADOS" X "DECLARADOS NO CONTEO"
            Dim objTiraDeclarados = (From objComp In objRespuesta.Tira.Componentes _
                                     From objDivisa In objComp.Divisas _
                                     Where objComp.NecFuncionComponente = eCodTipoConteo.Dispensadores
                                     Order By objDivisa.DesDivisa, objDivisa.Denominacion.NumValorFacial, objComp.NecOrden
                                     Select New With {objComp.CodMorfologiaComponente, objDivisa.CodIsoDivisa,
                                                      objDivisa.DesDivisa, objDivisa.Denominacion.DesDenominacion,
                                                      objDivisa.Denominacion.NelCantidadIntroducida, objDivisa.Denominacion.NelCantidadDispensada,
                                                      objDivisa.Denominacion.NelCantidadRechazada, objDivisa.Denominacion.NelCantidadRemanente,
                                                      objDivisa.Denominacion.NumImporteIntroducido, objDivisa.Denominacion.NumImporteDispensado,
                                                      objDivisa.Denominacion.NumImporteRechazado, objDivisa.Denominacion.NumImporteRemanente,
                                                      .Confere = "N",
                                                      .numImporteDeclarado = (From objContado In objRespuesta.Contados _
                                                                              From objValor In objContado.ValoresContados _
                                                                              Where objContado.CodTipoConteo = CType(eCodTipoConteo.Dispensadores, Integer).ToString And
                                                                              objValor.CodIsoDivisa = objDivisa.CodIsoDivisa And objValor.CodDenominacion = objDivisa.Denominacion.CodDenominacion
                                                                              Select objValor.NumImporteDeclarado).FirstOrDefault}).ToList

            For i = 0 To objHabTotal.Count - 1
                Dim iLocal = i
                Dim primeiro = objTiraDeclarados.FirstOrDefault(Function(f) f.CodMorfologiaComponente = objHabTotal(iLocal).CodMorfologiaComponente)
                If primeiro IsNot Nothing AndAlso objHabTotal(i).NumImporteHabilitado <> primeiro.NumImporteIntroducido Then
                    objHabTotal(i).Confere = False
                    primeiro.Confere = "F"
                ElseIf primeiro IsNot Nothing AndAlso objHabTotal(i).NumImporteHabilitado = primeiro.NumImporteIntroducido Then
                    primeiro.Confere = "T"
                End If
            Next

            'Totaliza "TIRA DECLARADOS" X "DECLARADOS NO CONTEO"
            Dim objTiraDeclaradosTot = (From objComp In objRespuesta.Tira.Componentes _
                                     From objDivisa In objComp.Divisas _
                                     Where objComp.NecFuncionComponente = eCodTipoConteo.Dispensadores
                                     Order By objDivisa.DesDivisa, objDivisa.Denominacion.NumValorFacial
                                     Group objDivisa By objDivisa.CodIsoDivisa, objDivisa.DesDivisa Into Group
                                     Select New With {CodIsoDivisa, DesDivisa,
                                                      .NumImporteIntroducido = Group.Sum(Function(c) c.Denominacion.NumImporteIntroducido),
                                                      .NumImporteRechazado = Group.Sum(Function(c) c.Denominacion.NumImporteRechazado),
                                                      .NumImporteRemanente = Group.Sum(Function(c) c.Denominacion.NumImporteRemanente),
                                                      .Confere = "N", .ConfereDeclarado = "N",
                                                      .numImporteDeclarado = (From objContado In objRespuesta.Contados _
                                                                              From objValor In objContado.ValoresContados _
                                                                              Where objContado.CodTipoConteo = CType(eCodTipoConteo.Dispensadores, Integer).ToString And
                                                                              objValor.CodIsoDivisa = CodIsoDivisa
                                                                              Select objValor.NumImporteDeclarado).Sum}).ToList

            For i = 0 To objTiraDeclaradosTot.Count - 1
                Dim iLocal = i
                Dim primeiro = objHabTotal.FirstOrDefault(Function(f) f.Divisa = objTiraDeclaradosTot(iLocal).DesDivisa)
                If primeiro IsNot Nothing AndAlso objTiraDeclaradosTot(i).NumImporteIntroducido <> primeiro.TotalDivisa Then
                    objTiraDeclaradosTot(i).Confere = "F"
                    primeiro.ConfereTot = False
                ElseIf primeiro IsNot Nothing AndAlso objTiraDeclaradosTot(i).NumImporteIntroducido = primeiro.TotalDivisa Then
                    objTiraDeclaradosTot(i).Confere = "T"
                End If
            Next

            Msg002.Visible = objTiraDeclarados Is Nothing OrElse objTiraDeclarados.Count = 0

            'Selecina os dados cotandos de denominação
            Dim objContados = (From objContado In objRespuesta.Contados _
                               From objValor In objContado.ValoresContados _
                               Where objContado.CodTipoConteo = CType(eCodTipoConteo.Dispensadores, Integer).ToString
                               Order By objValor.DesDivisa, objValor.NumValorFacial
                               Select New With {objContado.CodRemesaConteo, objValor.DesDivisa,
                               objValor.DesDenominacion, objValor.NumImporteContado}).ToList

            'Totaliza por divisa seleção os dados cotandos de denominação
            Dim objContadosTot = (From objContado In objRespuesta.Contados _
                               From objValor In objContado.ValoresContados _
                               Where objContado.CodTipoConteo = CType(eCodTipoConteo.Dispensadores, Integer).ToString
                               Group objValor By objValor.DesDivisa Into Group
                               Order By DesDivisa
                               Select New With {DesDivisa,
                                                .NumImporteContado = Group.Sum(Function(f) f.NumImporteContado),
                                                .Confere = True}).ToList

            For i = 0 To objContadosTot.Count - 1
                Dim iLocal = i
                Dim primeiro = objTiraDeclaradosTot.FirstOrDefault(Function(f) f.DesDivisa = objContadosTot(iLocal).DesDivisa)
                If primeiro IsNot Nothing AndAlso objContadosTot(i).NumImporteContado <> primeiro.numImporteDeclarado Then
                    primeiro.ConfereDeclarado = "F"
                    objContadosTot(i).Confere = False
                ElseIf primeiro IsNot Nothing AndAlso objTiraDeclaradosTot(i).numImporteDeclarado = primeiro.numImporteDeclarado Then
                    primeiro.ConfereDeclarado = "T"
                End If
            Next

            Msg003.Visible = objContados Is Nothing OrElse objContados.Count = 0

            'Seleciona os dados declarados ingresados
            Dim objDeclaradosIng = (From objComp In objRespuesta.Tira.Componentes _
                                     From objDivisa In objComp.Divisas _
                                     From objMedioPag In objDivisa.TipoMedioPago _
                                     Where objComp.NecFuncionComponente = eCodTipoConteo.Ingresadores
                                     Group objMedioPag By objDivisa.CodIsoDivisa, objDivisa.DesDivisa,
                                     objMedioPag.CodTipoMedioPago, objMedioPag.DesTipoMedioPago Into Group
                                     Order By DesDivisa, DesTipoMedioPago
                                     Select New With {CodIsoDivisa, DesDivisa, CodTipoMedioPago, DesTipoMedioPago,
                                                      .NumImporteIngresado = Group.Sum(Function(f) f.NumImporteIngresado),
                                                      .Confere = "N"}).ToList

            Msg006.Visible = objDeclaradosIng Is Nothing OrElse objDeclaradosIng.Count = 0
            tdIngresadoresDec.Visible = Not Msg006.Visible

            'Seleciona os dados contados ingresados
            Dim objContadosIng = (From objComp In objRespuesta.Contados _
                                     From objValCont In objComp.ValoresContados _
                                     Where objComp.CodTipoConteo = CType(eCodTipoConteo.Ingresadores, Integer).ToString
                                     Group objValCont By objComp.CodRemesaConteo, objValCont.CodIsoDivisa, objValCont.DesDivisa,
                                     objValCont.DesTipoMedioPago Into Group
                                     Order By DesDivisa, DesTipoMedioPago
                                     Select New With {CodRemesaConteo, CodIsoDivisa, DesDivisa,
                                                      .DesTipoMedioPagoOri = IIf(String.IsNullOrEmpty(DesTipoMedioPago), ATM.ContractoServicio.Constantes.C_TIPO_MEDIO_PAGO_EFECTIVO, DesTipoMedioPago),
                                                      .DesTipoMedioPago = IIf(String.IsNullOrEmpty(DesTipoMedioPago), Traduzir("gen_Efectivo"),
                                                      IIf(DesTipoMedioPago = ATM.ContractoServicio.Constantes.C_TIPO_MEDIO_PAGO_CHEQUE, Traduzir("gen_Cheque"),
                                                      IIf(DesTipoMedioPago = ATM.ContractoServicio.Constantes.C_TIPO_MEDIO_PAGO_TICKET, Traduzir("gen_Ticket"), Traduzir("gen_OtroValor")))),
                                                      .NumImporteIngresado = Group.Sum(Function(f) f.NumImporteContado),
                                                      .Confere = "N"}).ToList

            trIngesadorGrid.Visible = (objDeclaradosIng IsNot Nothing AndAlso objDeclaradosIng.Count > 0) OrElse (objContadosIng IsNot Nothing AndAlso objContadosIng.Count > 0)


            For i = 0 To objContadosIng.Count - 1
                Dim iLocal = i
                Dim primeiro = objDeclaradosIng.FirstOrDefault(Function(f) f.DesTipoMedioPago = objContadosIng(iLocal).DesTipoMedioPagoOri)
                If primeiro IsNot Nothing AndAlso objContadosIng(i).NumImporteIngresado <> primeiro.NumImporteIngresado Then
                    objContadosIng(i).Confere = "F"
                    primeiro.Confere = "F"
                ElseIf primeiro IsNot Nothing AndAlso objContadosIng(i).NumImporteIngresado = primeiro.NumImporteIngresado Then
                    objContadosIng(i).Confere = "T"
                    primeiro.Confere = "T"
                End If
            Next

            Msg004.Visible = objContadosIng Is Nothing OrElse objContadosIng.Count = 0
            tdIngresadoresCont.Visible = Not Msg004.Visible

            'Seleciona os dados declarados deposito
            Dim objDeclaradosDep = (From objComp In objRespuesta.Tira.Componentes _
                                     From objDivisa In objComp.Divisas _
                                     From objMedioPag In objDivisa.TipoMedioPago _
                                     Where objComp.NecFuncionComponente = eCodTipoConteo.Depositos
                                     Group objMedioPag By objDivisa.CodIsoDivisa, objDivisa.DesDivisa,
                                     objMedioPag.CodTipoMedioPago, objMedioPag.DesTipoMedioPago Into Group
                                     Order By DesDivisa, DesTipoMedioPago
                                     Select New With {CodIsoDivisa, DesDivisa, CodTipoMedioPago, DesTipoMedioPago,
                                                      .NumImporteIngresado = Group.Sum(Function(f) f.NumImporteIngresado),
                                                      .Confere = "N"}).ToList

            Msg007.Visible = objDeclaradosDep Is Nothing OrElse objDeclaradosDep.Count = 0
            tdDepositoDec.Visible = Not Msg007.Visible

            'Seleciona os dados contados deposito
            Dim objContadosDep = (From objComp In objRespuesta.Contados _
                                     From objValCont In objComp.ValoresContados _
                                     Where objComp.CodTipoConteo = CType(eCodTipoConteo.Depositos, Integer).ToString
                                     Group objValCont By objComp.CodRemesaConteo, objValCont.CodIsoDivisa, objValCont.DesDivisa,
                                     objValCont.DesTipoMedioPago Into Group
                                     Order By DesDivisa, DesTipoMedioPago
                                     Select New With {CodRemesaConteo, CodIsoDivisa, DesDivisa,
                                                      .DesTipoMedioPagoOri = IIf(String.IsNullOrEmpty(DesTipoMedioPago), ATM.ContractoServicio.Constantes.C_TIPO_MEDIO_PAGO_EFECTIVO, DesTipoMedioPago),
                                                      .DesTipoMedioPago = IIf(String.IsNullOrEmpty(DesTipoMedioPago), Traduzir("gen_Efectivo"),
                                                      IIf(DesTipoMedioPago = ATM.ContractoServicio.Constantes.C_TIPO_MEDIO_PAGO_CHEQUE, Traduzir("gen_Cheque"),
                                                      IIf(DesTipoMedioPago = ATM.ContractoServicio.Constantes.C_TIPO_MEDIO_PAGO_TICKET, Traduzir("gen_Ticket"), Traduzir("gen_OtroValor")))),
                                                      .NumImporteIngresado = Group.Sum(Function(f) f.NumImporteContado),
                                                      .Confere = "N"}).ToList


            trDepositoGrid.Visible = (objDeclaradosDep IsNot Nothing AndAlso objDeclaradosDep.Count > 0) OrElse (objContadosDep IsNot Nothing AndAlso objContadosDep.Count > 0)


            For i = 0 To objContadosDep.Count - 1
                Dim iLocal = i
                Dim primeiro = objDeclaradosDep.FirstOrDefault(Function(f) f.DesTipoMedioPago = objContadosDep(iLocal).DesTipoMedioPagoOri)
                If primeiro IsNot Nothing AndAlso objContadosDep(i).NumImporteIngresado <> primeiro.NumImporteIngresado Then
                    objContadosDep(i).Confere = "F"
                    primeiro.Confere = "F"
                ElseIf primeiro IsNot Nothing AndAlso objContadosDep(i).NumImporteIngresado = primeiro.NumImporteIngresado Then
                    objContadosDep(i).Confere = "T"
                    primeiro.Confere = "T"
                End If
            Next

            Msg005.Visible = objContadosDep Is Nothing OrElse objContadosDep.Count = 0
            tdDepositoCont.Visible = Not Msg005.Visible

            ' converter objeto para datatable            
            Dim objHabDt As DataTable = gdvHabilitaciones.ConvertListToDataTable(objH)
            objHabDt.DefaultView.Sort = gdvHabilitaciones.SortCommand

            Dim objHabTotDt As DataTable = gdvHabilitacionesTotales.ConvertListToDataTable(objHabTotal)
            objHabTotDt.DefaultView.Sort = gdvHabilitacionesTotales.SortCommand

            Dim objDecDt As DataTable = gdvConteudoDeclarados.ConvertListToDataTable(objTiraDeclarados)
            objDecDt.DefaultView.Sort = gdvConteudoDeclarados.SortCommand

            Dim objDecTotDt As DataTable = gdvConteudoDeclaradosTotales.ConvertListToDataTable(objTiraDeclaradosTot)
            objDecTotDt.DefaultView.Sort = gdvConteudoDeclaradosTotales.SortCommand

            Dim objContadosDt As DataTable = gdvConteudoContado.ConvertListToDataTable(objContados)
            objContadosDt.DefaultView.Sort = gdvConteudoContado.SortCommand

            Dim objContadosTotDt As DataTable = gdvConteudoContadoTot.ConvertListToDataTable(objContadosTot)
            objContadosTotDt.DefaultView.Sort = gdvConteudoContadoTot.SortCommand

            Dim objDeclaradosIngDt As DataTable = gdvDeclaradosIngresadores.ConvertListToDataTable(objDeclaradosIng)
            objDeclaradosIngDt.DefaultView.Sort = gdvDeclaradosIngresadores.SortCommand

            Dim objContadosIngDt As DataTable = gdvContadosIngresadores.ConvertListToDataTable(objContadosIng)
            objContadosIngDt.DefaultView.Sort = gdvContadosIngresadores.SortCommand

            Dim objDeclaradosDepDt As DataTable = gdvDeclaradosDeposito.ConvertListToDataTable(objDeclaradosDep)
            objDeclaradosDepDt.DefaultView.Sort = gdvDeclaradosDeposito.SortCommand

            Dim objContadosDepDt As DataTable = gdvContadosDeposito.ConvertListToDataTable(objContadosDep)
            objContadosDepDt.DefaultView.Sort = gdvContadosDeposito.SortCommand

            ' carregar controle
            gdvHabilitaciones.CarregaControle(objHabDt)
            gdvHabilitacionesTotales.CarregaControle(objHabTotDt)
            gdvConteudoDeclarados.CarregaControle(objDecDt)
            gdvConteudoDeclaradosTotales.CarregaControle(objDecTotDt)
            gdvConteudoContado.CarregaControle(objContadosDt)
            gdvConteudoContadoTot.CarregaControle(objContadosTotDt)
            gdvDeclaradosIngresadores.CarregaControle(objDeclaradosIngDt)
            gdvContadosIngresadores.CarregaControle(objContadosIngDt)
            gdvDeclaradosDeposito.CarregaControle(objDeclaradosDepDt)
            gdvContadosDeposito.CarregaControle(objContadosDepDt)

            gdvHabilitacionesTotales.Visible = objRespuesta.Habilitacion.Count > 1

            lblTiraRegistrada.Text = Traduzir("015_lblTiraRegistrada") & " " & objRespuesta.Tira.FyhRegistroTira.ToString("dd/MM/yyyy")

        Catch ex As Exception
            ' Mostra as mensagens de erros
            MostraMensagemExcecao(ex)
        End Try

    End Sub


    Private Sub SetWidthColumnsHab(e As System.Web.UI.WebControls.GridViewRowEventArgs)
        e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(4).Width = 80
        e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(5).Width = 70
        e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(6).Width = 90
        e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(7).Width = 80
        e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(8).Width = 80
        e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(10).Width = 100
        e.Row.Cells(10).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(11).Width = 70
        e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Left
    End Sub

    Private Sub SetWidthColumnsHabTot(e As System.Web.UI.WebControls.GridViewRowEventArgs)
        e.Row.Cells(0).Font.Size = 11
        e.Row.Cells(0).Font.Bold = True

        e.Row.Cells(0).Width = 140
        e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
        e.Row.Cells(1).Width = 80
        e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(2).Width = 70
        e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(3).Width = 90
        e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(4).Width = 80
        e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(5).Width = 80
        e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(7).Width = 100
        e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(8).Width = 70
        e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Left
    End Sub

    Private Sub SetWidthColumnsDeclarado(e As System.Web.UI.WebControls.GridViewRowEventArgs)
        e.Row.Cells(0).Width = 60
        e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(1).Width = 60
        e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(2).Width = 80
        e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(3).Width = 60
        e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(4).Width = 60
        e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(5).Width = 60
        e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(6).Width = 60
        e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(7).Width = 80
        e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(8).Width = 80
        e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(9).Width = 80
        e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(10).Width = 80
        e.Row.Cells(10).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(11).Width = 80
        e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Right
    End Sub

    Private Sub SetWidthColumnsDeclaradoTot(e As System.Web.UI.WebControls.GridViewRowEventArgs)
        e.Row.Cells(0).Width = 80
        e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(1).Width = 80
        e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(2).Width = 80
        e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(3).Width = 80
        e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
        e.Row.Cells(4).Width = 80
        e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
    End Sub

    Private Sub SetWidthColumnsContados(e As System.Web.UI.WebControls.GridViewRowEventArgs)
        e.Row.Cells(0).Width = 130
        e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(1).Width = 60
        e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(2).Width = 80
        e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(3).Width = 80
        e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
    End Sub

    Private Sub SetWidthColumnsContadosTot(e As System.Web.UI.WebControls.GridViewRowEventArgs)
        e.Row.Cells(0).Width = 60
        e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(1).Width = 80
        e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
    End Sub

    Private Sub SetWidthColumnsDeclaradosIng(e As System.Web.UI.WebControls.GridViewRowEventArgs)
        e.Row.Cells(0).Width = 60
        e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(1).Width = 150
        e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(2).Width = 80
        e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
    End Sub

    Private Sub SetWidthColumnsContadosIng(e As System.Web.UI.WebControls.GridViewRowEventArgs)
        e.Row.Cells(0).Width = 245
        e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(1).Width = 60
        e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(2).Width = 150
        e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(3).Width = 110
        e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
    End Sub

    Private Sub MergeCell(gvRow As GridViewRow, gvPreviousRow As GridViewRow, cellCount As Integer)
        If gvPreviousRow.Cells(cellCount).RowSpan < 2 Then
            gvRow.Cells(cellCount).RowSpan = 2
        Else
            gvRow.Cells(cellCount).RowSpan = gvPreviousRow.Cells(cellCount).RowSpan + 1
        End If
        gvPreviousRow.Cells(cellCount).Visible = False
    End Sub

    Private Sub MergeColCellHabilitacion()
        For rowIndex As Integer = gdvHabilitaciones.Rows.Count - 2 To 0 Step -1
            Dim gvRow As GridViewRow = gdvHabilitaciones.Rows(rowIndex)
            Dim gvPreviousRow As GridViewRow = gdvHabilitaciones.Rows(rowIndex + 1)
            If gvRow.Cells(0).Text = gvPreviousRow.Cells(0).Text Then
                MergeCell(gvRow, gvPreviousRow, 0)
            End If
            If gvRow.Cells(1).Text = gvPreviousRow.Cells(1).Text And
                gvRow.Cells(0).Text = gvPreviousRow.Cells(0).Text Then
                MergeCell(gvRow, gvPreviousRow, 1)
            End If
        Next
    End Sub

    Private Sub MergeColCellDeclarado()
        For rowIndex As Integer = gdvConteudoDeclarados.Rows.Count - 2 To 0 Step -1
            Dim gvRow As GridViewRow = gdvConteudoDeclarados.Rows(rowIndex)
            Dim gvPreviousRow As GridViewRow = gdvConteudoDeclarados.Rows(rowIndex + 1)
            If gvRow.Cells(1).Text = gvPreviousRow.Cells(1).Text And gvRow.Cells(2).Text = gvPreviousRow.Cells(2).Text Then
                MergeCell(gvRow, gvPreviousRow, 11)
            End If
        Next
    End Sub

    Private Sub MergeColCellContado()
        For rowIndex As Integer = gdvConteudoContado.Rows.Count - 2 To 0 Step -1
            Dim gvRow As GridViewRow = gdvConteudoContado.Rows(rowIndex)
            Dim gvPreviousRow As GridViewRow = gdvConteudoContado.Rows(rowIndex + 1)
            If gvRow.Cells(0).Text = gvPreviousRow.Cells(0).Text Then
                MergeCell(gvRow, gvPreviousRow, 0)
            End If
        Next
    End Sub

    Private Sub MergeColCellContadoIng()
        For rowIndex As Integer = gdvContadosIngresadores.Rows.Count - 2 To 0 Step -1
            Dim gvRow As GridViewRow = gdvContadosIngresadores.Rows(rowIndex)
            Dim gvPreviousRow As GridViewRow = gdvContadosIngresadores.Rows(rowIndex + 1)
            If gvRow.Cells(0).Text = gvPreviousRow.Cells(0).Text Then
                MergeCell(gvRow, gvPreviousRow, 0)
            End If
        Next
    End Sub

    Private Sub MergeColCellContadoDep()
        For rowIndex As Integer = gdvContadosDeposito.Rows.Count - 2 To 0 Step -1
            Dim gvRow As GridViewRow = gdvContadosDeposito.Rows(rowIndex)
            Dim gvPreviousRow As GridViewRow = gdvContadosDeposito.Rows(rowIndex + 1)
            If gvRow.Cells(0).Text = gvPreviousRow.Cells(0).Text Then
                MergeCell(gvRow, gvPreviousRow, 0)
            End If
        Next
    End Sub

#End Region

#Region "[EVENTOS]"


    Protected Sub gdvHabilitaciones_EPager_SetCss(sender As Object, e As EventArgs) Handles gdvHabilitaciones.EPager_SetCss, gdvHabilitacionesTotales.EPager_SetCss
        Try

            'Configura o estilo dos controles presentes no pager
            CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
            CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
            CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

            CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
            CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
            CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 9

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub gdvHabilitaciones_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvHabilitaciones.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Text = Traduzir("015_colCodRemesaLegado")
                e.Row.Cells(1).Text = Traduzir("015_colFyhHabilitacion")
                e.Row.Cells(2).Text = Traduzir("015_colCodInventarioContenedor")
                e.Row.Cells(3).Text = Traduzir("015_colCodPrecintoContenedor")
                e.Row.Cells(4).Text = Traduzir("015_colCodMorfologiaComponente")
                e.Row.Cells(5).Text = Traduzir("015_colDesDivisa")
                e.Row.Cells(6).Text = Traduzir("015_colCodDenominacion")
                e.Row.Cells(7).Text = Traduzir("015_colNelCantidadHabilitada")
                e.Row.Cells(8).Text = Traduzir("015_colNumImporteHabilitado")
                e.Row.Cells(10).Text = Traduzir("015_colTotalDivisa")
                e.Row.Cells(11).Text = Traduzir("015_colDivisa")
                'SetWidthColumnsHab(e)
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub gdvConteudoDeclarados_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvConteudoDeclarados.RowCreated

        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Text = Traduzir("015_TcolCodMorfologiaComponente")
                e.Row.Cells(1).Text = Traduzir("015_TcolDesDivisa")
                e.Row.Cells(2).Text = Traduzir("015_TcolDesDenominacion")
                e.Row.Cells(3).Text = Traduzir("015_TcolNelCantidadIntroducida")
                e.Row.Cells(4).Text = Traduzir("015_TcolNelCantidadDispensada")
                e.Row.Cells(5).Text = Traduzir("015_TcolNelCantidadRechazada")
                e.Row.Cells(6).Text = Traduzir("015_TcolNelCantidadRemanente")
                e.Row.Cells(7).Text = Traduzir("015_TcolNumImporteIntroducido")
                e.Row.Cells(8).Text = Traduzir("015_TcolNumImporteDispensado")
                e.Row.Cells(9).Text = Traduzir("015_TcolNumImporteRechazado")
                e.Row.Cells(10).Text = Traduzir("015_TcolNumImporteRemanente")
                e.Row.Cells(11).Text = Traduzir("015_TcolnumImporteDeclarado")

            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub gdvConteudoDeclaradosTotales_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvConteudoDeclaradosTotales.RowCreated

        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Text = Traduzir("015_TotcolDesDivisa")
                e.Row.Cells(1).Text = Traduzir("015_TotcolNumImporteIntroducido")
                e.Row.Cells(2).Text = Traduzir("015_TotcolNumImporteRechazado")
                e.Row.Cells(3).Text = Traduzir("015_TotcolNumImporteRemanente")
                e.Row.Cells(4).Text = Traduzir("015_TotcolnumImporteDeclarado")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub gdvConteudoContado_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvConteudoContado.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Text = Traduzir("015_ContcolCodRemesaConteo")
                e.Row.Cells(1).Text = Traduzir("015_ContcolDesDivisa")
                e.Row.Cells(2).Text = Traduzir("015_ContcolDesDenominacion")
                e.Row.Cells(3).Text = Traduzir("015_ContcolNumImporteContado")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvConteudoContadoTot_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvConteudoContadoTot.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Text = Traduzir("015_ContTotcolDesDivisa")
                e.Row.Cells(1).Text = Traduzir("015_ContTotcolNumImporteContado")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvDeclaradosIngresadores_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvDeclaradosIngresadores.RowCreated, gdvDeclaradosDeposito.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Text = Traduzir("015_DecIngcolDesDivisa")
                e.Row.Cells(1).Text = Traduzir("015_DecIngcolDesTipoMedioPago")
                e.Row.Cells(2).Text = Traduzir("015_DecIngcolNumImporteIngresado")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvContadosIngresadores_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvContadosIngresadores.RowCreated, gdvContadosDeposito.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Text = Traduzir("015_ContIngcolCodRemesaConteo")
                e.Row.Cells(1).Text = Traduzir("015_ContIngcolDesDivisa")
                e.Row.Cells(2).Text = Traduzir("015_ContIngcolDesTipoMedioPago")
                e.Row.Cells(3).Text = Traduzir("015_ContIngcolNumImporteIngresado")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        'tdCajetines.Width = (gdvConteudoDeclarados.Rows(0).Cells(0).Width.Value +
        '                     gdvConteudoDeclarados.Rows(0).Cells(1).Width.Value +
        '                     gdvConteudoDeclarados.Rows(0).Cells(2).Width.Value + 6).ToString

        'tdCantidad.Width = (gdvConteudoDeclarados.Rows(0).Cells(3).Width.Value +
        '                     gdvConteudoDeclarados.Rows(0).Cells(4).Width.Value +
        '                     gdvConteudoDeclarados.Rows(0).Cells(5).Width.Value +
        '                     gdvConteudoDeclarados.Rows(0).Cells(6).Width.Value + 8).ToString

        'tdImporte.Width = (gdvConteudoDeclarados.Rows(0).Cells(7).Width.Value +
        '                     gdvConteudoDeclarados.Rows(0).Cells(8).Width.Value +
        '                     gdvConteudoDeclarados.Rows(0).Cells(9).Width.Value +
        '                     gdvConteudoDeclarados.Rows(0).Cells(10).Width.Value +
        '                     gdvConteudoDeclarados.Rows(0).Cells(11).Width.Value + 11).ToString

    End Sub


    Private Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        Try
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = False
            Master.HabilitarHistorico = False
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = False
            Master.Titulo = Traduzir("015_titulo_pagina")

            ' Recuperar Parametros
            RecuperarParametros()

            ' Traduz texto dos controles
            TraduzirControles()

            ' Carrega os dados do relatório
            CarregarDados()

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

    End Sub


    Protected Sub gdvHabilitaciones_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvHabilitaciones.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(1).Text = IIf(e.Row.DataItem("FyhHabilitacion").ToString <> "", CType(e.Row.DataItem("FyhHabilitacion").ToString, Date).ToString("dd/MM/yyyy"), "")
                e.Row.Cells(8).Text = IIf(e.Row.DataItem("NumImporteHabilitado").ToString <> "", CType(e.Row.DataItem("NumImporteHabilitado").ToString, Decimal).ToString("N2"), "")
                e.Row.Cells(10).Text = IIf(e.Row.DataItem("Divisa").ToString <> "", CType(e.Row.DataItem("TotalDivisa").ToString, Decimal).ToString("N2"), "")

                SetWidthColumnsHab(e)
                e.Row.Attributes.Remove("onMouseOut")
                e.Row.Attributes.Remove("onMouseOver")
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub gdvHabilitacionesTotales_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvHabilitacionesTotales.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(5).Text = IIf(e.Row.DataItem("NumImporteHabilitado").ToString <> "", CType(e.Row.DataItem("NumImporteHabilitado").ToString, Decimal).ToString("N2"), "")
                e.Row.Cells(7).Text = IIf(e.Row.DataItem("Divisa").ToString <> "", CType(e.Row.DataItem("TotalDivisa").ToString, Decimal).ToString("N2"), "")

                If e.Row.DataItem("Divisa").ToString = "" Then
                    e.Row.Cells(7).CssClass = "styleGridCellNoBorder"
                    e.Row.Cells(8).CssClass = "styleGridCellNoBorder"
                End If

                If e.Row.DataItem("Confere") Then
                    e.Row.Cells(4).CssClass = "styleGridCellConfere"
                    e.Row.Cells(5).CssClass = "styleGridCellConfere"
                Else
                    e.Row.Cells(4).CssClass = "styleGridCellNoConfere"
                    e.Row.Cells(5).CssClass = "styleGridCellNoConfere"
                End If

                If e.Row.Cells(7).CssClass = "" AndAlso e.Row.DataItem("ConfereTot") Then
                    e.Row.Cells(7).CssClass = "styleGridCellConfere"
                ElseIf e.Row.Cells(7).CssClass = "" Then
                    e.Row.Cells(7).CssClass = "styleGridCellNoConfere"
                End If
                e.Row.Cells(0).Text = Traduzir("015_TotalHabilitado")
                SetWidthColumnsHabTot(e)
                e.Row.Attributes.Remove("onMouseOut")
                e.Row.Attributes.Remove("onMouseOver")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub gdvHabilitaciones_DataBound(sender As Object, e As EventArgs) Handles gdvHabilitaciones.DataBound
        Try
            MergeColCellHabilitacion()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvHabilitacionesTotales_DataBound(sender As Object, e As EventArgs) Handles gdvHabilitacionesTotales.DataBound
        Try
            For rowIndex As Integer = gdvHabilitacionesTotales.Rows.Count - 2 To 0 Step -1
                Dim gvRow As GridViewRow = gdvHabilitacionesTotales.Rows(rowIndex)
                Dim gvPreviousRow As GridViewRow = gdvHabilitacionesTotales.Rows(rowIndex + 1)
                MergeCell(gvRow, gvPreviousRow, 0)
            Next
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub gdvConteudoDeclarados_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvConteudoDeclarados.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(7).Text = IIf(e.Row.DataItem("NumImporteIntroducido").ToString <> "", CType(e.Row.DataItem("NumImporteIntroducido").ToString, Decimal).ToString("N2"), 0.ToString("N2"))
                e.Row.Cells(8).Text = IIf(e.Row.DataItem("NumImporteDispensado").ToString <> "", CType(e.Row.DataItem("NumImporteDispensado").ToString, Decimal).ToString("N2"), 0.ToString("N2"))
                e.Row.Cells(9).Text = IIf(e.Row.DataItem("NumImporteRechazado").ToString <> "", CType(e.Row.DataItem("NumImporteRechazado").ToString, Decimal).ToString("N2"), 0.ToString("N2"))
                e.Row.Cells(10).Text = IIf(e.Row.DataItem("NumImporteRemanente").ToString <> "", CType(e.Row.DataItem("NumImporteRemanente").ToString, Decimal).ToString("N2"), 0.ToString("N2"))
                e.Row.Cells(11).Text = IIf(e.Row.DataItem("numImporteDeclarado").ToString <> "", CType(e.Row.DataItem("numImporteDeclarado").ToString, Decimal).ToString("N2"), 0.ToString("N2"))

                If e.Row.DataItem("Confere") = "T" Then
                    e.Row.Cells(3).CssClass = "styleGridCellConfere"
                    e.Row.Cells(7).CssClass = "styleGridCellConfere"
                ElseIf e.Row.DataItem("Confere") = "F" Then
                    e.Row.Cells(3).CssClass = "styleGridCellNoConfere"
                    e.Row.Cells(7).CssClass = "styleGridCellNoConfere"
                End If

                SetWidthColumnsDeclarado(e)
                e.Row.Attributes.Remove("onMouseOut")
                e.Row.Attributes.Remove("onMouseOver")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub gdvConteudoDeclaradosTotales_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvConteudoDeclaradosTotales.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(1).Text = IIf(e.Row.DataItem("NumImporteIntroducido").ToString <> "", CType(e.Row.DataItem("NumImporteIntroducido").ToString, Decimal).ToString("N2"), 0.ToString("N2"))
                e.Row.Cells(2).Text = IIf(e.Row.DataItem("NumImporteRechazado").ToString <> "", CType(e.Row.DataItem("NumImporteRechazado").ToString, Decimal).ToString("N2"), 0.ToString("N2"))
                e.Row.Cells(3).Text = IIf(e.Row.DataItem("NumImporteRemanente").ToString <> "", CType(e.Row.DataItem("NumImporteRemanente").ToString, Decimal).ToString("N2"), 0.ToString("N2"))
                e.Row.Cells(4).Text = IIf(e.Row.DataItem("numImporteDeclarado").ToString <> "", CType(e.Row.DataItem("numImporteDeclarado").ToString, Decimal).ToString("N2"), 0.ToString("N2"))

                If e.Row.DataItem("Confere") = "T" Then
                    e.Row.Cells(1).CssClass = "styleGridCellConfere"
                ElseIf e.Row.DataItem("Confere") = "F" Then
                    e.Row.Cells(1).CssClass = "styleGridCellNoConfere"
                End If
                If e.Row.DataItem("ConfereDeclarado") = "T" Then
                    e.Row.Cells(4).CssClass = "styleGridCellConfereDec"
                ElseIf e.Row.DataItem("ConfereDeclarado") = "F" Then
                    e.Row.Cells(4).CssClass = "styleGridCellNoConfereDec"
                End If

                SetWidthColumnsDeclaradoTot(e)
                e.Row.Attributes.Remove("onMouseOut")
                e.Row.Attributes.Remove("onMouseOver")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvConteudoContado_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvConteudoContado.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(3).Text = IIf(e.Row.DataItem("NumImporteContado").ToString <> "", CType(e.Row.DataItem("NumImporteContado").ToString, Decimal).ToString("N2"), 0.ToString("N2"))
                SetWidthColumnsContados(e)
                e.Row.Attributes.Remove("onMouseOut")
                e.Row.Attributes.Remove("onMouseOver")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvConteudoContadoTot_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvConteudoContadoTot.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(1).Text = IIf(e.Row.DataItem("NumImporteContado").ToString <> "", CType(e.Row.DataItem("NumImporteContado").ToString, Decimal).ToString("N2"), 0.ToString("N2"))

                If e.Row.DataItem("Confere") Then
                    e.Row.Cells(1).CssClass = "styleGridCellConfereDec"
                Else
                    e.Row.Cells(1).CssClass = "styleGridCellNoConfereDec"
                End If

                SetWidthColumnsContadosTot(e)
                e.Row.Attributes.Remove("onMouseOut")
                e.Row.Attributes.Remove("onMouseOver")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvDeclaradosIngresadores_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvDeclaradosIngresadores.RowDataBound, gdvDeclaradosDeposito.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(2).Text = IIf(e.Row.DataItem("NumImporteIngresado").ToString <> "", CType(e.Row.DataItem("NumImporteIngresado").ToString, Decimal).ToString("N2"), 0.ToString("N2"))

                If e.Row.DataItem("Confere") = "T" Then
                    e.Row.Cells(2).CssClass = "styleGridCellConfereDec"
                ElseIf e.Row.DataItem("Confere") = "F" Then
                    e.Row.Cells(2).CssClass = "styleGridCellNoConfereDec"
                End If

                SetWidthColumnsDeclaradosIng(e)
                e.Row.Attributes.Remove("onMouseOut")
                e.Row.Attributes.Remove("onMouseOver")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvContadosIngresadores_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvContadosIngresadores.RowDataBound, gdvContadosDeposito.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(3).Text = IIf(e.Row.DataItem("NumImporteIngresado").ToString <> "", CType(e.Row.DataItem("NumImporteIngresado").ToString, Decimal).ToString("N2"), 0.ToString("N2"))
                SetWidthColumnsContadosIng(e)

                If e.Row.DataItem("Confere") = "T" Then
                    e.Row.Cells(3).CssClass = "styleGridCellConfereDec"
                ElseIf e.Row.DataItem("Confere") = "F" Then
                    e.Row.Cells(3).CssClass = "styleGridCellNoConfereDec"
                End If

                e.Row.Attributes.Remove("onMouseOut")
                e.Row.Attributes.Remove("onMouseOver")
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvConteudoDeclarados_DataBound(sender As Object, e As EventArgs) Handles gdvConteudoDeclarados.DataBound
        Try
            MergeColCellDeclarado()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvConteudoContado_DataBound(sender As Object, e As EventArgs) Handles gdvConteudoContado.DataBound
        Try
            MergeColCellContado()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvContadosIngresadores_DataBound(sender As Object, e As EventArgs) Handles gdvContadosIngresadores.DataBound
        Try
            MergeColCellContadoIng()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvContadosDeposito_DataBound(sender As Object, e As EventArgs) Handles gdvContadosDeposito.DataBound
        Try
            MergeColCellContadoDep()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region
#Region "Metodos ExibirMensagens"
    Protected Sub MostraMensagemErro(ByVal erro As String, ByVal script As String)
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")
        erro = erro.Replace("'", "&#39;")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                   CriarChamadaMensagemErro(erro, script) _
                                                       , True)
    End Sub

    Protected Sub MostraMensagem(ByVal erro As String)
        MostraMensagemErro(erro, String.Empty)
    End Sub
    Protected Sub MostraMensagemExcecao(ByVal exception As Exception)
        ' logar erro no banco
        Util.LogarErroAplicacao(Nothing, exception.ToString(), String.Empty, LoginUsuario, DelegacionConectada.Keys(0))
        MostraMensagemErro(exception.Message, String.Empty)
    End Sub

    Protected Sub MostraMensagemErroConScript(ByVal erro As String, Optional ByVal script As String = "")
        MostraMensagemErro(erro, script)
    End Sub

    Protected Shared Function CriarChamadaMensagemErro(ByVal erro As String, ByVal script As String) As String
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")

        Return String.Format("ExibirMensagem('{0}','{1}', '{2}' , '{3}');", _
                                                                erro, _
                                                                 Traduzir("aplicacao"), script, Traduzir("btnFechar"))
    End Function
#End Region

End Class