<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucClienteSubPto.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ucClienteSubPto" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="~/Controles/Popup.ascx" TagName="Popup" TagPrefix="ucPopup" %>
<div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnCliente" Style="padding-top: 10px;" runat="server">
                <table class="tabela_campos">
                    <tr>
                        <td class="tamanho_celula" align="left">
                            <asp:Label ID="lblCliente" runat="server" Text="" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lbCliente" Width="317px" runat="server"></asp:ListBox>
                                        <asp:CustomValidator ID="cvCliente" runat="server" ErrorMessage="*"></asp:CustomValidator>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Button runat="server" ID="btnBuscaCliente" CssClass="btn-buscar"/>
                                        </div>
                                        <div>
                                            <asp:ImageButton ID="imgButtonLimpaCliente" runat="server" ImageUrl="~/Imagenes/borrar.png" 
                                                CssClass="butondefectoPequeno" Enabled="False" Width="16px" Height="16px"/>
                                        </div>
                                        <div>
                                            <asp:ImageButton ID="imgButtonLimpaTodoCliente" runat="server" ImageUrl="~/Imagenes/limpiar.png" 
                                                CssClass="butondefectoPequeno" Enabled="False" Width="16px" Height="16px"/>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnSubCliente" Style="padding-top: 10px;" runat="server">
                <table class="tabela_campos" cellspacing="0" cellpadding="3">
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkTodosSubCliente" runat="server" AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula" align="left">
                            <asp:Label ID="lblSubCliente" runat="server" Text="" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lbSubCliente" Width="317px" runat="server"></asp:ListBox>
                                        <asp:CustomValidator ID="cvSubCliente" runat="server" ErrorMessage="*"></asp:CustomValidator>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Button runat="server" ID="btnBuscaSubCliente" CssClass="btn-buscar" Enabled="False"/>
                                        </div>
                                        <div>
                                            <asp:ImageButton ID="imgButtonLimpaSubCliente" runat="server" ImageUrl="~/Imagenes/borrar.png" 
                                                CssClass="butondefectoPequeno" Enabled="False" Width="16px" Height="16px"/>
                                        </div>
                                        <div>
                                            <asp:ImageButton ID="imgButtonLimpaTodoSubCliente" runat="server" ImageUrl="~/Imagenes/limpiar.png" 
                                                CssClass="butondefectoPequeno" Enabled="False" Width="16px" Height="16px"/>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnPtoServico" Style="padding-top: 10px;" runat="server">
                <table class="tabela_campos" cellspacing="0" cellpadding="3">
                    <tr style="padding-top: 50px;">
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkTodosPtosServico" runat="server" AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula" align="left">
                            <asp:Label ID="lblPtoServico" runat="server" Text="" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lbPtoServico" Width="317px" runat="server"></asp:ListBox>
                                        <asp:CustomValidator ID="cvPtoServico" runat="server" ErrorMessage="*"></asp:CustomValidator>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Button runat="server" ID="btnBuscaPtoServicio" CssClass="btn-buscar" Enabled="False"/>
                                        </div>
                                        <div>
                                            <asp:ImageButton ID="imgButtonLimpaPtoServicio" runat="server" ImageUrl="~/Imagenes/borrar.png" 
                                                CssClass="butondefectoPequeno" Enabled="False" Width="16px" Height="16px"/>
                                        </div>
                                        <div>
                                            <asp:ImageButton ID="imgButtonLimpaTodoPtoServicio" runat="server" ImageUrl="~/Imagenes/limpiar.png" 
                                                CssClass="butondefectoPequeno" Enabled="False" Width="16px" Height="16px"/>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnGrupoCliente" runat="server">
                <table class="tabela_campos" cellspacing="0" cellpadding="3">
                    <tr>
                        <td class="tamanho_celula" align="left">
                            <asp:Label ID="lblGrupoCliente" runat="server" Text="" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstGrupoCliente" Width="317px" runat="server" AutoPostBack="True"
                                            SelectionMode="Multiple"></asp:ListBox>
                                        <asp:CustomValidator ID="cvGrupoCliente" runat="server" ErrorMessage="*"></asp:CustomValidator>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
<%--    <div id="modal1" class="modal1">
        <ucPopup:Popup ID="ucPopupBuscaCliente" Width="820" EsModal="True" AutoAbrirPopup="false"
            runat="server" />
    </div>--%>
    <asp:PlaceHolder ID="phUcPopUp" runat="server"></asp:PlaceHolder>

    <asp:UpdateProgress ID="upCarregar" runat="server">
        <ProgressTemplate>
            <div id="JanelaCarregar" class="JanelaCarregar">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
