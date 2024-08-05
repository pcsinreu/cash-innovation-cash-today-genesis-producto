<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PopupBusquedaSubCliente.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.PopupBusquedaSubCliente" %>
<div id="ModalBusqueda">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Busqueda">
                <fieldset class="ui-fieldset ui-fieldset-toggleable">
                    <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
                    </span>
                        <asp:Label ID="lblBuscaCliente" runat="server" Text="Busqueda Sub Cliente" />
                    </legend>
                    <ul class="form-filter">
                        <li>
                            <asp:Label ID="lblCodigo" runat="server" SkinID="filter-label" Text="Código:" />
                            <asp:TextBox ID="txtCodigo" SkinID="filter-textbox" runat="server" MaxLength="15"
                                Width="150px" />
                        </li>
                        <li>
                            <asp:Label ID="lblDescricao" runat="server" SkinID="filter-label" Text="Descripción:" />
                            <asp:TextBox ID="txtDescricao" SkinID="filter-textbox" runat="server" MaxLength="100"
                                Width="200px" />
                        </li>
                    </ul>
                    <ul class="certificados-btns">
                        <li>
                            <asp:Button ID="btnBuscar" SkinID="filter-button" runat="server" Text="Buscar" />
                        </li>
                        <li>
                            <asp:Button ID="btnLimpar" SkinID="filter-button" runat="server" Text="Limpar" />
                        </li>
                    </ul>
                </fieldset>
            </div>
            <asp:Panel ID="resultado" runat="server"  Visible="false">
                <fieldset class="ui-fieldset ui-fieldset-toggleable">
                    <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
                    </span>
                        <asp:Label ID="lblClientes" runat="server" Text="Sub Clientes" />
                    </legend>
                    <asp:GridView ID="gdvClientes" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="OidSubCliente,CodSubCliente,DesSubCliente"  AllowSorting="True" 
                        EnableModelValidation="True" BorderStyle="None" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <FooterTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbSelecionado" ValidationGroup="rbSelecionado" GroupName="rbSelecionado"
                                        runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="10px" />
                                <ItemStyle Width="10px" />
                                <FooterStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CodSubCliente" HeaderText="Codigo" ReadOnly="True">
                            <HeaderStyle Width="150px" />
                            <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="DesSubCliente">
                            <HeaderStyle Width="450px" />
                            <ItemStyle Width="450px" />
                                <ItemTemplate >
                                    <asp:Label ID="Label1"  runat="server" Text='<%# If(Eval("DesSubCliente").Length > 60 , String.Format("{0}...",Eval("DesSubCliente").Substring(0, 60)), Eval("DesSubCliente"))  %>' ToolTip='<%# If(Eval("DesSubCliente").Length > 60 , Eval("DesSubCliente"), String.Empty ) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" />
                    </asp:GridView>
                    <ul class="certificados-btns">
                        <li>
                            <asp:Button ID="btnAceptar" runat="server" SkinID="filter-button" Text="Aceitar" /></li>
                        <li>
                            <asp:Button ID="btnCancelar" runat="server" SkinID="filter-button" Text="Cancelar" /></li></ul>
                    </li>
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
