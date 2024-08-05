<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Principal.Master"
    CodeBehind="ReportarPedidoBCP.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ReportarPedidoBCP"  EnableEventValidation="false"  %>

<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="Controles/Erro.ascx" TagName="Erro" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Reportes - Reportar Pedido BCP</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
        <ContentTemplate>
            <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <asp:Panel runat="server" ID="pnlFiltros" Visible="True">
                    <tr>
                        <td class="titulo02">
                            <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSubTituloCriteriosBusqueda" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <table class="tabela_campos" cellspacing="0" cellpadding="3">
                                <tr class="TrImpar">
                                    <td class="espaco_inicial" align="right">
                                        <img alt="" src="imagenes/MarcadorCampo.gif" />
                                    </td>
                                    <td width="140px" align="left">
                                        <asp:Label ID="lblDelegacion" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDelegacion" runat="server" Width="150px" CssClass="Text01">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="csvDelegacion" runat="server" ControlToValidate="ddlDelegacion">*</asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr class="TrPar">
                                    <td class="espaco_inicial" align="right">
                                        <img alt="" src="imagenes/MarcadorCampo.gif" />
                                    </td>
                                    <td width="140px" align="left">
                                        <asp:Label ID="lblTipoEspecie" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstTipoEspecie" runat="server" CssClass="Text01" Width="150px" Height="40px"
                                            SelectionMode="Multiple"></asp:ListBox>
                                        <asp:CustomValidator ID="csvTipoEspecie" runat="server" ControlToValidate="lstTipoEspecie">*</asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr class="TrImpar">
                                    <td class="espaco_inicial" align="right">
                                        <img alt="" src="imagenes/MarcadorCampo.gif" />
                                    </td>
                                    <td width="140px" align="left">
                                        <asp:Label ID="lblTipoDeposito" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstTipoDeposito" runat="server" CssClass="Text01" Width="150px"
                                            Height="40px" SelectionMode="Multiple"></asp:ListBox>
                                        <asp:CustomValidator ID="csvTipoDeposito" runat="server" ControlToValidate="lstTipoDeposito">*</asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr class="TrPar">
                                    <td class="espaco_inicial" align="right">
                                        <img alt="" src="imagenes/MarcadorCampo.gif" />
                                    </td>
                                    <td width="140px" align="left">
                                        <asp:Label ID="lblFechaConteo" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFechaConteoDesde" runat="server" CssClass="Lbl2"></asp:Label>
                                        <asp:TextBox ID="txtFechaConteoDesde" runat="server" MaxLength="16" Width="90px"
                                            CssClass="Text01"></asp:TextBox>
                                        <asp:ImageButton ID="imbFechaConteoDesde" ImageUrl="~/Imagenes/calendar.gif" runat="server" />
                                        <asp:CustomValidator ID="csvFechaConteoDesde" runat="server" ControlToValidate="txtFechaConteoDesde">*</asp:CustomValidator>
                                        <asp:Label ID="lblFechaConteoHasta" runat="server" CssClass="Lbl2"></asp:Label>
                                        <asp:TextBox ID="txtFechaConteoHasta" runat="server" MaxLength="16" Width="90px"
                                            CssClass="Text01"></asp:TextBox>
                                        <asp:ImageButton ID="imbFechaConteoHasta" ImageUrl="~/Imagenes/calendar.gif" runat="server" />
                                        <asp:CustomValidator ID="csvFechaConteoHasta" runat="server" ControlToValidate="txtFechaConteoHasta">*</asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr class="TrImpar">
                                    <td class="espaco_inicial" align="right">
                                        <img alt="" src="imagenes/MarcadorCampo.gif" />
                                    </td>
                                    <td width="140px" align="left">
                                        <asp:Label ID="lblFechaUltimoEnvio" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValorFechaUltimoEnvio" runat="server" CssClass="Text01"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <pro:Botao ID="btnReportar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                                Tipo="Relatorio" Titulo="020_btn_reportar">
                                                            </pro:Botao>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td>
                                                    <pro:Botao ID="btnLimpar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                        Tipo="Cancelar" Titulo="020_btn_limpar">
                                                    </pro:Botao>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlGrid" Visible="true">
                    <tr>
                        <td class="titulo02">
                            <table cellpadding="0" cellspacing="4" border="0">
                                <tr>
                                    <td>
                                        <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTituloGrid" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Erro ID="ControleErro" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <table class="TABLE" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td align="center">
                                        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server">
                                            <ContentTemplate>
                                                <pro:ProsegurGridView ID="gdvItensProceso" runat="server" AllowPaging="True" AllowSorting="True"
                                                    EstiloDestaque="GridLinhaDestaque" GridPadrao="False" AutoGenerateColumns="False"
                                                    Ajax="True" GerenciarControleManualmente="True" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                                    PaginaAtual="0" PaginacaoAutomatica="True" Width="95%" Height="100%" ExibirCabecalhoQuandoVazio="False">
                                                    <Pager ID="objPager_gdvItensProceso">
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
                                                    <TextBox ID="objTextogdvItensProceso" AutoPostBack="True" MaxLength="10" Width="30px">&nbsp;</TextBox>
                                                    <Columns>
                                                        <asp:BoundField DataField="TipoEspecie" HeaderText="colEspecie" SortExpression="TipoEspecie" />
                                                        <asp:BoundField DataField="TipoDeposito" HeaderText="colDeposito" SortExpression="TipoDeposito" />
                                                        <asp:BoundField DataField="FechaDesde" HeaderText="colFechaDesde" SortExpression="FechaDesde"
                                                            DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-Width="110px" />
                                                        <asp:BoundField DataField="FechaHasta" HeaderText="colFechaHasta" SortExpression="FechaHasta"
                                                            DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-Width="110px" />
                                                        <asp:BoundField DataField="CodDelegacion" HeaderText="colDelegacion" SortExpression="CodDelegacion" />
                                                        <asp:BoundField DataField="CodEstado" HeaderText="colEstado" SortExpression="CodEstado" />
                                                        <asp:BoundField DataField="DesError" HeaderText="colObservacion" SortExpression="DesError"
                                                            ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="FechaCreacion" HeaderText="colFechaCreacion" SortExpression="FechaCreacion"
                                                            DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" ItemStyle-Width="120px" />
                                                    </Columns>
                                                </pro:ProsegurGridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <pro:Botao ID="btnAtualizar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                                Tipo="Confirmar" Titulo="020_btn_atualizar">
                                                            </pro:Botao>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReportar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnAtualizar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
