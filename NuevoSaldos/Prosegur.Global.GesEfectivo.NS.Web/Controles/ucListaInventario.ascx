<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucListaInventario.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucListaInventario" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<asp:UpdatePanel runat="server" ID="upInventario">
    <ContentTemplate>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Button ID="btnBuscar" SkinID="filter-button" runat="server" Text="Buscar" /></td>
            </tr>
            <tr>
                <td style="padding-left: 5px;">
                    <asp:GridView ID="gdvInventarios" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="Identificador" AllowSorting="True" Visible="false"
                        EnableModelValidation="True" BorderStyle="None" EmptyDataRowStyle-BorderWidth="5" emp>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbSelecionado" ValidationGroup="rbSelecionado" GroupName="rbSelecionado"
                                        runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="10px" />
                                <ItemStyle Width="10px" />
                                <FooterStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codigo" HeaderText="Codigo" ReadOnly="True">
                                <HeaderStyle Width="75px" />
                                <ItemStyle Width="75px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha/Hora Inventário" ReadOnly="True">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Delegacion">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Sector.Delegacion.Descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Planta">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Sector.Planta.Descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sector">
                                <ItemTemplate>
                                    <p style="width: 350px; white-space: normal"><%# DataBinder.Eval(Container.DataItem, "Sector.Descripcion") %></p>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" />
                        <EmptyDataTemplate>
                            <div class="EmptyData">
                                <span>
                                    <%# Tradutor.Traduzir("lblSemRegistro")%> 
                                </span>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

