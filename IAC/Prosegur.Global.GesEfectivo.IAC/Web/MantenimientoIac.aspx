<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoIac.aspx.vb"  MasterPageFile="~/Principal.Master"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoIac" EnableEventValidation="false"
    %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento IAC</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hidTerminoIacSelecionado" runat="server" />
            <asp:HiddenField ID="hidTerminoSelecionado" runat="server" />
            <asp:HiddenField ID="hidObligatorio" runat="server" />
            <asp:HiddenField ID="hidObligatorioFalse" runat="server" />
            <asp:HiddenField ID="hidObligatorioValorTrue" runat="server" />
            <asp:HiddenField ID="hidObligatorioValorFalse" runat="server" />
            <asp:HiddenField ID="hidBusqueda" runat="server" />
            <asp:HiddenField ID="hidBusquedaFalse" runat="server" />
            <asp:HiddenField ID="hidCampoClave" runat="server" />
            <asp:HiddenField ID="hidOrden" runat="server" />
            <asp:HiddenField ID="hidCampoClaveFalse" runat="server" />
            <asp:HiddenField ID="hidBusquedaValorTrue" runat="server" />
            <asp:HiddenField ID="hidBusquedaValorFalse" runat="server" />
            <asp:HiddenField ID="hidClaveValorVerdadeiro" runat="server" />
            <asp:HiddenField ID="hidClaveValorFalso" runat="server" />
            <asp:HiddenField ID="hdnObjeto" runat="server" />
            <asp:HiddenField ID="hidCopiarTermino" runat="server" />
            <asp:HiddenField ID="hidCopiarTerminoFalse" runat="server" />
            <asp:HiddenField ID="hidCopiarTerminoValorVerdadeiro" runat="server" />
            <asp:HiddenField ID="hidCopiarTerminoValorFalso" runat="server" />
            <asp:HiddenField ID="hidProtegido" runat="server" />
            <asp:HiddenField ID="hidProtegidoFalse" runat="server" />
            <asp:HiddenField ID="hidProtegidoValorTrue" runat="server" />
            <asp:HiddenField ID="hidProtegidoValorFalse" runat="server" />
             <asp:HiddenField ID="hidInvisibleReporte" runat="server" />
            <asp:HiddenField ID="hidInvisibleReporteFalse" runat="server" />
            <asp:HiddenField ID="hidInvisibleReporteValorTrue" runat="server" />
            <asp:HiddenField ID="hidInvisibleReporteValorFalse" runat="server" />
            <asp:HiddenField ID="hidIdMecanizado" runat="server" />
            <asp:HiddenField ID="hidIdMecanizadoFalse" runat="server" />
            <asp:HiddenField ID="hidIdMecanizadoValorTrue" runat="server" />
            <asp:HiddenField ID="hidIdMecanizadoValorFalse" runat="server" />
            
            <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="titulo02">
                        <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                </td>
                                <td>
                                    <asp:Label ID="lblTituloIac" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tabela_campos"  >
                            <tr>
                                <td class="espaco_inicial">
                                    &nbsp;
                                </td>
                                <td align="right" width="120px">
                                    <asp:Label ID="lblCodigoIac" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="170px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCodigoIac" runat="server" MaxLength="15" AutoPostBack="true"
                                                            CssClass="Text02" Width="150px"></asp:TextBox>
                                                        <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="001_msg_canalcodigoobrigatorio"
                                                            ControlToValidate="txtCodigoIac" Text="*"></asp:CustomValidator>
                                                        <asp:CustomValidator ID="csvCodigoExistente" runat="server" ErrorMessage="006_msg_codigoiacexistente"
                                                            ControlToValidate="txtCodigoIac" Text="*"></asp:CustomValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnAdiciona" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnremove" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnAcima" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnAbaixo" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgressCodigo" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigo">
                                                    <ProgressTemplate>
                                                        <img src="Imagenes/loader1.gif" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="tamanho_celula" align="right">
                                    <asp:Label ID="lblDescricaoIac" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" width="250px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDescricaoIac" runat="server" AutoPostBack="true" CssClass="Text02"
                                                            MaxLength="50" Width="230px"></asp:TextBox>
                                                        <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ControlToValidate="txtDescricaoIac"
                                                            ErrorMessage="001_msg_canaldescripcionobrigatorio" Text="*"></asp:CustomValidator>
                                                        <asp:CustomValidator ID="csvDescricaoExistente" runat="server" ControlToValidate="txtDescricaoIac"
                                                            ErrorMessage="006_msg_descricaoiacexistente" Text="*"></asp:CustomValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnAdiciona" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnremove" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnAcima" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnAbaixo" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgressDescricao" runat="server" AssociatedUpdatePanelID="UpdatePanelDescricao">
                                                    <ProgressTemplate>
                                                        <img src="Imagenes/loader1.gif" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblObservacionesIac" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="4" style="margin-left: 40px">
                                    <asp:TextBox ID="txtObservacionesIac" runat="server" MaxLength="4000" AutoPostBack="true"
                                        CssClass="Text02" Height="96px" Width="532px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblCopiarDeclarados" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkCopiarDeclarados" runat="server" CssClass="Lbl2" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblDisponivelNuevoSaldos" runat="server" CssClass="Lbl2"></asp:Label>                                                               
                                    <asp:CheckBox ID="chkDisponivelNuevoSaldos" runat="server" CssClass="Lbl2" />
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkVigente" runat="server" CssClass="Lbl2" />
                                </td>
                                <td align="right" class="tamanho_celula">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblInvisible" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkInvisible" runat="server" CssClass="Lbl2" />
                                </td>
                                <td align="right" class="tamanho_celula">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table class="tabela_interna"  >
                                    <tr>
                                        <td align="center" width="100%" colspan="2">
                                            <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                                ColunasSelecao="Codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                                AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True" NumeroRegistros="0"
                                                OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True" Width="95%"
                                                Height="100%" ExibirCabecalhoQuandoVazio="False">
                                                <Pager ID="objPager_ProsegurGridView1">
                                                    <FirstPageButton Visible="True">
                                                    </FirstPageButton>
                                                    <LastPageButton Visible="True">
                                                    </LastPageButton>
                                                    <Summary Text="Página {0} de {1} ({2} itens)" />
                                                    <SummaryStyle>
                                                    </SummaryStyle>
                                                </Pager>
                                                <HeaderStyle CssClass="GridTitulo" Font-Bold="True" />
                                                <AlternatingRowStyle CssClass="GridLinhaAlternada" />
                                                <RowStyle CssClass="GridLinhaPadrao" />
                                                <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">
                                                &nbsp;            
                                                </TextBox>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="adiciona">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAdicionaGridTerminos" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                                    <asp:BoundField DataField="Observacion" HeaderText="Observacion" />
                                                </Columns>
                                            </pro:ProsegurGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" width="100%" colspan="2">
                                            <asp:ImageButton ID="btnAdiciona" runat="server" ImageUrl="~/Imagenes/pag02.png" />
                                            <asp:ImageButton ID="btnremove" runat="server" ImageUrl="~/Imagenes/pag03.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" width="100%" colspan="2">
                                            <table width="95%">
                                                <tr>
                                                    <td rowspan="3" width="95%">
                                                        <pro:ProsegurGridView ID="ProsegurGridView2" runat="server" AllowPaging="False" AllowSorting="True"
                                                            ColunasSelecao="CodigoTermino" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                                            PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                                            NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="false"
                                                            Width="100%" Height="100%" ExibirCabecalhoQuandoVazio="true">
                                                            <Pager ID="Pager1">
                                                                <FirstPageButton Visible="True">
                                                                </FirstPageButton>
                                                                <LastPageButton Visible="True">
                                                                </LastPageButton>
                                                                <Summary Text="P�gina {0} de {1} ({2} itens)" />
                                                                <SummaryStyle>
                                                                </SummaryStyle>
                                                            </Pager>
                                                            <HeaderStyle CssClass="GridTitulo" Font-Bold="True" />
                                                            <AlternatingRowStyle CssClass="GridLinhaAlternada" />
                                                            <RowStyle CssClass="GridLinhaPadrao" />
                                                            <TextBox ID="TextBox1" AutoPostBack="True" MaxLength="10" Width="30px"></TextBox>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="adiciona">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkBusqueda" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="CodigoTermino" HeaderText="CodigoTermino" />
                                                                <asp:BoundField DataField="DescripcionTermino" HeaderText="DescripcionTermino" />
                                                                <asp:TemplateField HeaderText="EsObligatorio">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkObligatorio" runat="server" AutoPostBack="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EsBusquedaParcial">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkBusqueda1" runat="server" AutoPostBack="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EsCampoClave">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkAdicionaGridTerminosIac" runat="server" AutoPostBack="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EsTerminoCopia">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkCopiaTermino" runat="server" AutoPostBack="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="esProtegido">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkProtegido" runat="server" AutoPostBack="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="OrdenTermino" HeaderText="OrdenTermino" Visible="false" />
                                                                <asp:TemplateField HeaderText="esInvisibleRpte">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkInvisibleRpte" runat="server" AutoPostBack="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="esIdMecanizado">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkIdMecanizado" runat="server" AutoPostBack="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </pro:ProsegurGridView>
                                                    </td>
                                                    <td width="5%">
                                                        <asp:CustomValidator ID="csvTerminoObligatorio" runat="server" ControlToValidate="hidValidaGrid2"
                                                            ErrorMessage="006_msg_iacTerminoObligatorio" Text="*"></asp:CustomValidator>
                                                        <asp:TextBox ID="hidValidaGrid2" Style="visibility: hidden;" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="5%">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnAcima" runat="server" Height="23px" ImageUrl="~/Imagenes/pag03.png"
                                                                        Width="25px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnAbaixo" runat="server" ImageUrl="~/Imagenes/pag02.png" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="5%">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style13">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 97%">
                                                <tr>
                                                    <td width="45%">
                                                    </td>
                                                    <td class="style11" style="text-align: center">
                                                        <pro:Botao ID="btnGrabar" runat="server" ExecutaValidacaoCliente="true" ExibirLabelBtn="True"
                                                            Habilitado="True" Tipo="Salvar" Titulo="btnGrabar">
                                                        </pro:Botao>
                                                    </td>
                                                    <td class="style7" style="text-align: center">
                                                        <pro:Botao ID="btnCancelar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                            Tipo="Sair" Titulo="btnCancelar">
                                                        </pro:Botao>
                                                    </td>
                                                    <td class="style10" style="text-align: center">
                                                        <pro:Botao ID="btnVolver" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                            Tipo="Voltar" Titulo="btnVolver">
                                                        </pro:Botao>
                                                    </td>
                                                    <td width="45%">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
                //Script necessário para evitar que dê erro ao clicar duas vezes em algum controle que esteja dentro do updatepanel.
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_initializeRequest(initializeRequest);

                function initializeRequest(sender, args) {
                    if (prm.get_isInAsyncPostBack()) {

                        //args.set_cancel(true);
                        prm.abortPostBack();

                    }
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnAdiciona" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnremove" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnAcima" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnAbaixo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
