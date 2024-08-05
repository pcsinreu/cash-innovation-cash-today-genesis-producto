<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoTerminoMedioPagoProcesso.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoTerminoMedioPagoProcesso"
    EnableEventValidation="false" MasterPageFile="Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Mantenimiento de Términos de Medios de Pago</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblTituloMediosPago" CssClass="ui-panel-title" runat="server"></asp:Label>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <table style="margin: 0px !Important; width: 100%;">
                            <tr>
                                <td align="center" width="100%">
                                    <pro:ProsegurGridView ID="ProsegurGridViewMediosPagos" runat="server" AllowSorting="False"
                                        ColunasSelecao="CodigoDivisa,CodigoTipoMedioPago,CodigoMedioPago" EstiloDestaque="GridLinhaDestaque"
                                        GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                        NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="False"
                                        Width="99%" Height="100%" ExibirCabecalhoQuandoVazio="False">
                                        <Pager ID="Pager1">
                                            <FirstPageButton Visible="True">
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Página {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                        <Columns>
                                            <asp:BoundField DataField="DescripcionDivisa" HeaderText="DescripcionDivisa" />
                                            <asp:BoundField DataField="DescripcionTipoMedioPago" HeaderText="DescripcionTipoMedioPago" />
                                            <asp:BoundField DataField="CodigoMedioPago" HeaderText="CodigoMedioPago" />
                                            <asp:BoundField DataField="DescripcionMedioPago" HeaderText="DescripcionMedioPago" />
                                        </Columns>
                                    </pro:ProsegurGridView>
                                </td>
                            </tr>
                        </table>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblSubTitulosTerminosMedioPago" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table style="margin: 0px !Important; width: 100%;">
                            <tr>
                                <td align="center" width="100%">
                                    <pro:ProsegurGridView ID="ProsegurGridViewTerminosMedioPago" runat="server" AllowSorting="False"
                                        EstiloDestaque="GridLinhaDestaque" GridPadrao="False" AutoGenerateColumns="False"
                                        Ajax="True" GerenciarControleManualmente="True" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="False" Width="99%" Height="100%" ExibirCabecalhoQuandoVazio="False">
                                        <Pager ID="objPager_ProsegurGridView1">
                                            <FirstPageButton Visible="True">
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Página {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelecionadoNormal" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Obligatorio">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEsObligatorio" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CodigoTermino" HeaderText="Codigo" />
                                            <asp:BoundField DataField="DescripcionTermino" HeaderText="Descripcion" />
                                        </Columns>
                                    </pro:ProsegurGridView>
                                </td>
                            </tr>
                            <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                                <tr>
                                    <td align="center" colspan="4">
                                        <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                            <tr>
                                                <td style="border-width: 0;">
                                                   
                                                </td>
                                                <td style="border-width: 0;">
                                                    <asp:Label ID="lblSemRegistro" runat="server" Text="Label" CssClass="Lbl2">Não existem dados a serem exibidos.</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                         <table style="margin: 0px !Important;">
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnAceptar" CssClass="ui-button" Width="120"/>
                                </td>
                           </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
                //Script necessário para evitar que dê erro ao clicar duas vezes em algum controle que esteja dentro do updatepanel.
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_initializeRequest(initializeRequest);

                function initializeRequest(sender, args) {
                    if (prm.get_isInAsyncPostBack()) {

                        args.set_cancel(true);

                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

