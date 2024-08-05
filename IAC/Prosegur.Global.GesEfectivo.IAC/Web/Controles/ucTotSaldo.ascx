<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucTotSaldo.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.ucTotSaldo" %>

<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td align="center">
            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <pro:ProsegurGridView ID="grvTotSaldo" runat="server" AllowPaging="True" AllowSorting="False" ColunasSelecao="OidTotSaldo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False" PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True" ExibirCabecalhoQuandoVazio="false" NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True" Width="99%" OnRowDataBound="grvTotSaldo_RowDataBound" OnRowCommand="grvTotSaldo_RowCommand">
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
                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">1</TextBox>
                        <Columns>
                            <asp:TemplateField  Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdConfigNivelMov" runat="server" Visible="false" Text='<%# Eval("IdentificadorNivelMovimiento")%>'></asp:Label>
                                    <asp:Label ID="lblIdConfigNivelSaldo" runat="server" Visible="false" Text='<%# Eval("IdentificadorNivelSaldo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" HeaderText="Cambiar">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imbModificar" ImageUrl="../App_Themes/Padrao/css/img/grid/edit.png" runat="server" CommandName="Cambiar" CssClass="imgButton" CommandArgument='<%# Container.DataItemIndex %>'></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px"  HeaderText="Borrar">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imbBorrar" ImageUrl="../App_Themes/Padrao/css/img/grid/borrar.png" runat="server" CommandName="Borrar" CssClass="imgButton" CommandArgument='<%# Container.DataItemIndex %>'></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Defecto" ItemStyle-Width="50px">
                                   <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDefecto" runat="server" Checked='<%# Eval("bolDefecto") %>' AutoPostBack="true" OnCheckedChanged="chkDefecto_CheckedChanged"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubCanal" ItemStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesSubCanal" runat="server"></asp:Label>
                                    <asp:Label ID="lblIdSubCanal" runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Totalizador">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalizador" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </pro:ProsegurGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
