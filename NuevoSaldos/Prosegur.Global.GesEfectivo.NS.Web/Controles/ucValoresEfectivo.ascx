<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucValoresEfectivo.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucValoresEfectivo" %>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Divisas/CalcularEfectivo.js")%>"></script>

<asp:UpdatePanel ID="upUcDivisaFull" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <asp:Repeater runat="server" ID="rptDivisaEfectivo">
            <ItemTemplate>
                <fieldset class="ui-fieldset ui-fieldset-toggleable">
                    <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
                    </span>
                        <asp:Label ID="lblFiltroDivisaEfectivo" runat="server" Text="Divisas"></asp:Label></legend>
                    <asp:UpdatePanel ID="upUcDivisa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div id="divScrollDivisa" class="ScrollPopup">
                                <table>
                                    <tr>
                                        <td style="padding-right: 10px">
                                            <asp:HiddenField ID="hdfIdentificador" runat="server" />
                                            <asp:HiddenField ID="hdnImporteDivisaAtual" runat="server" />
                                            <asp:HiddenField ID="hdnImporteDenominacion" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDivisa" SkinID="filter-label" Text="Divisa"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCodIso" SkinID="filter-label" Text="Codigo Iso"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblImporteDivisa" SkinID="filter-label" Text="Importe"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblImporteTotal" SkinID="filter-label" Text="ImporteTotal"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-right: 10px">
                                            <asp:Image ID="imgDivisa" runat="server" Height="35px" Width="35px" />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtDivisa" SkinID="filter-textbox" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtCodIso" SkinID="filter-textbox" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtImporteDivisa" SkinID="filter-textbox"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtImporteTotal" SkinID="filter-textbox" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td style="padding-bottom: 10px;">
                                            <asp:Button ID="btnBorrarDivisa" runat="server" Text="Borrar Divisa" SkinID="filter-button" CommandName="btnBorrarDivisa" CommandArgument='<%#Eval("Identificador")%>'/>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="fieldsetDenominacion" runat="server" visible="false">
                                <fieldset class="ui-fieldset ui-fieldset-toggleable">
                                    <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
                                    </span>
                                        <asp:Label ID="lblDenominaciones" runat="server" /></legend>
                                    <div class="ui-fieldset-content">
                                        <div id="divScrollDenominacion" class="ScrollPopup">
                                            <asp:Repeater runat="server" ID="rptDetalle">
                                                <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblUnidad" SkinID="filter-label" Text="Unidad" Width="100px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblCantidad" SkinID="filter-label" Text="Cantidad"
                                                                    Width="115px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblDenominacion" SkinID="filter-label" Text="Denominacion"
                                                                    Width="150px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblCodDenominacion" SkinID="filter-label" Text="Codigo Iso"
                                                                    Width="100px"></asp:Label>
                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:Label runat="server" ID="lblBilleteMoneda" SkinID="filter-label" Text="Billete / Moneda"
                                                                    Width="80px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblImporteDenominacion" SkinID="filter-label" Text="Importe"
                                                                    Width="120px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblCalidad" SkinID="filter-label" Text="Calidad" Width="100px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlUnidad" SkinID="form-dropdownlist-mandatory"
                                                                Width="100px">
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdfIdentificadorDenominacion" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCantidad" SkinID="filter-textbox" Width="115px"
                                                                Height="15px" MaxLength="14"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtDenominacion" SkinID="filter-textbox" Width="150px"
                                                                Height="15px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCodDenominacion" SkinID="filter-textbox" Width="100px"
                                                                Height="15px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Image ID="imgBilleteMoneda" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtImporteDenominacion" SkinID="filter-textbox" Width="120px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlCalidad" SkinID="form-dropdownlist-mandatory"
                                                                Width="100px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table></FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBorrarDivisa" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </fieldset>
            </ItemTemplate>
        </asp:Repeater>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    var ParametrosJSON = '<asp:literal runat="server" id="ltParametrosJSON" ></asp:literal>';
</script>