<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucHelperBusquedaAvanzada.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucHelperBusquedaAvanzada" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<asp:UpdatePanel ID="upBusquedaAvanzada" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px; width: 730px;">
            <asp:Label ID="lblFiltro" runat="server" Text="" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
        <div class="dvclear">
        </div>
        <div class="dvUsarFloat" style="margin-left: 5px; width: 730px;">
            <div id="dvCodigo" style="margin: 0px 25px 2px 0px; height: auto;">
                <asp:Label ID="lblCodigo" runat="server" /><br />
                <asp:TextBox ID="txtCodigo" runat="server" MaxLength="100" onkeydown="javascript: return event.keyCode != 13" />
            </div>
            <div id="dvDescripcion" style="margin: 0px 25px 2px 0px; height: auto;">
                <asp:Label ID="lblDescripcion" runat="server" /><br />
                <asp:TextBox ID="txtDescripcion" runat="server" Width="520px" MaxLength="100" onkeydown="javascript: return event.keyCode != 13" />
            </div>
            <div id="dvSector" runat="server" style="margin: 08px 25px 0px 0px; height: auto;width: 720px;">
                <div id="dvTipoSectores" runat="server" style="margin: 0px 25px 2px 0px; height: auto;">
                    <div id="dvCheckBoxListTipoSectores" runat="server" style="margin-top: 0px;">
                        <asp:Label ID="lblTiposSectores" runat="server" Text="Tipo Sector"></asp:Label><br />
                        <div style="width: 300px; height: 50px; overflow: auto; border-radius: 5px !important; padding: 2px; border: solid 1px #A8A8A8;">
                            <asp:CheckBoxList ID="cklTiposSectores" runat="server"></asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div id="dvConsiderarHijos" runat="server" style="margin: 08px 25px 2px 0px; height: auto; width: 360px;">
                    <asp:UpdatePanel ID="upConsiderarHijos" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="height: auto;">
                                <asp:PlaceHolder runat="server" ID="phConsiderarHijos"></asp:PlaceHolder>
                            </div>
                            <div style="height: auto; padding-top: 05px;">
                                <asp:CheckBox runat="server" ID="chkConsiderarSectoresHijos" Text=" Considerar los sectores hijos en el resultado de la búsqueda" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div runat="server" id="dvBotoes" >
                    <div id="dvbotaobuscar" runat="server"  style="margin: 08px 05px 05px 0px; height: auto; border: 5px solid groove; ">
                        <asp:Button ID="btnBuscar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                    </div>
                    <div style="margin-top: 08px; height: auto;">
                        <asp:Button ID="btnLimpiar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                    </div>
                </div>
            <div class="dvclear">
            </div>
        </div>
        <asp:Panel ID="pnlResultado" runat="server">
            <div class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px; width: 730px;">
                <asp:Label ID="lblResultado" runat="server" Text="" Style="color: #767676 !important; font-size: 9pt;" />
            </div>
            <div class="dvclear">
            </div>
            <asp:HiddenField runat="server" ID="hdnSelecionado" />
            <asp:GridView ID="gdvResultadoBusqueda" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataKeyNames="Identificador,Codigo,Descricao,IdentificadorPai" AllowSorting="True"
                EnableModelValidation="True" OnRowDataBound="gdvResultadoBusqueda_RowDataBound" Width="730">
                <Columns>
                    <asp:TemplateField>
                        <FooterTemplate>
                        </FooterTemplate>
                        <ItemTemplate>
                            <input id="rbSelecionado" type="radio" runat="server" class="radio_selecao" onclick="javascript: $('.radio_selecao').not(this).each(function () { this.checked = false; });" />
                            <asp:CheckBox ID="chkSelecionado" ValidationGroup="chkSelecionado" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Width="10px" />
                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                        <FooterStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:Label ID="lblTituloColCodigo" runat="server" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemCodigo" CssClass="limitText" Width="150px" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:Label ID="lblTituloColDescricao" runat="server" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemDescricao" CssClass="limitText" Width="450px" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Width="450px" />
                        <ItemStyle Width="450px" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="EmptyData">
                        <%# Tradutor.Traduzir("lblSemRegistro")%>
                    </div>
                </EmptyDataTemplate>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="dvUsarFloat" style="width: 730px;">
    <div style="width: 28%; margin: 12px 25px 2px 0px; height: auto;">
        &nbsp;
    </div>
    <div id="divBtnAceptar" style="margin: 12px 25px 2px 0px; height: auto; min-width: 40px;">
        <asp:Button ID="btnAceptar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
    </div>
    <div style="margin: 12px 25px 2px 0px; height: auto;">
        <asp:Button ID="btnCancelar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
    </div>
    <div class="dvclear">
    </div>
</div>
