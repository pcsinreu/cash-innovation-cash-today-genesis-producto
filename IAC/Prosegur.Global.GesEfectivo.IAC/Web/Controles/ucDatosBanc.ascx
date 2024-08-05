<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucDatosBanc.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.ucDatosBanc" %>

<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td align="center">
            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <pro:ProsegurGridView ID="grvDatosBanc" runat="server" AllowPaging="True" AllowSorting="False"
                        EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                        PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                        ExibirCabecalhoQuandoVazio="false" NumeroRegistros="0" OrdenacaoAutomatica="True"
                        PaginaAtual="0" PaginacaoAutomatica="True" Width="99%" OnRowDataBound="grvDatosBanc_RowDataBound" OnRowCommand="grvDatosBanc_RowCommand">
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
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdentificador" runat="server" Visible="false" Text='<%# Eval("Identificador")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" HeaderText="Aprobación">
                                <ItemTemplate>
                                    <asp:Image runat="server"
                                        ImageUrl="~/Imagenes/contain01.png"
                                        ID="imgEstadoAprobado"
                                        CssClass="imgButton"
                                        Visible='<%# Not Eval("Pendiente") %>'
                                        ToolTip='<%#  RecuperarValorDic("lbl_sin_aprobacion_pendiente") %>' />
                                    <asp:ImageButton runat="server"
                                        ImageUrl="~/Imagenes/contain_pending.png"
                                        ID="imgEstadoPendiente"
                                        Visible='<%# Eval("Pendiente") %>'
                                        CssClass="imgButton"
                                        CommandName="PopupComparativo"
                                        CommandArgument='<%# Eval("Identificador") %>'
                                        ToolTip='<%#  RecuperarValorDic("lbl_aprobacion_pendiente") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" HeaderText="Cambiar">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imbModificar"
                                        ImageUrl="../App_Themes/Padrao/css/img/grid/edit.png"
                                        CssClass="imgButton"
                                        runat="server"
                                        CommandName="Cambiar"
                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" HeaderText="Borrar">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imbBorrar"
                                        ImageUrl="../App_Themes/Padrao/css/img/grid/borrar.png"
                                        CssClass="imgButton"
                                        runat="server"
                                        CommandName="Borrar"
                                        CommandArgument='<%# Eval("Identificador") %>'></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Defecto" ItemStyle-Width="50px">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDefecto" runat="server" Checked='<%# Eval("bolDefecto")%>' Enabled='False' OnCheckedChanged="chkDefecto_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Divisa">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdDivisa" runat="server" Text='<%# Eval("Divisa.Identificador") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblDivisa" runat="server" Text='<%# Eval("Divisa.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Banco">
                                <ItemTemplate>
                                    <asp:Label ID="lblBanco" runat="server" Text='<%# Eval("Banco.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("CodigoTipoCuentaBancaria").ToString().ToUpper() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CodigoAgencia">
                                <ItemTemplate>
                                    <asp:Label ID="lblAgencia" runat="server" Text='<%# Eval("CodigoAgencia") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nro. Cuenta">
                                <ItemTemplate>
                                    <asp:Label ID="lblNroCuenta" runat="server" Text='<%# Eval("CodigoCuentaBancaria") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nro. Documento">
                                <ItemTemplate>
                                    <asp:Label ID="lblNroDocumento" runat="server" Text='<%# Eval("CodigoDocumento") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </pro:ProsegurGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<div class="botaoOcultar">
    <asp:Button runat="server" ID="btnAlertaSi" CssClass="btn-excluir" />
</div>
