<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucValoresMedioPago.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucValoresMedioPago" %>
<style type="text/css">
       .AlineacionCheck
        {
            vertical-align:middle;
            *vertical-align: middle;
            
        }
        .AlineacionLabel
        {
            vertical-align:middle;
            *vertical-align: middle;
            *padding-top:7px;
        }
    </style>
    
<asp:UpdatePanel ID="upUcDivisaFull" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <asp:Repeater runat="server" ID="rptDivisa">
            <ItemTemplate>
                <fieldset class="ui-fieldset ui-fieldset-toggleable">
                    <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
                    </span>
                        <asp:Label ID="lblFiltroDivisa" runat="server" Text="Divisas" /></legend>
                    <div class="ui-fieldset-content">
                        <asp:UpdatePanel ID="upUcDivisa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td style="padding-right: 10px">
                                            <asp:HiddenField ID="hdfIdentificador" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDivisa" SkinID="filter-label" Text="Divisa"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCodIso" SkinID="filter-label" Text="Codigo Iso"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-right: 10px">
                                            <asp:Image ID="imgDivisa" runat="server" Height="32px" Width="32px" />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtDivisa" SkinID="filter-textbox" AutoPostBack="true"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtCodIso" SkinID="filter-textbox" AutoPostBack="true"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                        <td style="padding-left: 50px;">
                                            <asp:Button ID="btnBorrarDivisa" runat="server" Text="Borrar Divisa" SkinID="filter-button"
                                                AutoPostBack="true" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Repeater runat="server" ID="rptDetalle">
                                    <ItemTemplate>
                                        <fieldset class="ui-fieldset ui-fieldset-toggleable">
                                            <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
                                            </span>
                                                <asp:Label ID="lblFiltroTipoMedioPago" runat="server" Text="TipoMedioPago" /></legend>
                                            <div class="ui-fieldset-content">
                                                <div id="divTipoMedioPago" class="ui-fieldset-content" runat="server">
                                                    <table id="tblDetallesTpMedioPago">
                                                        <tr>
                                                            <td style="visibility: hidden;">
                                                            </td>
                                                            <td style="padding-left: 20px;">
                                                                <asp:Label ID="lblCodTpMedioPago" runat="server" SkinID="filter-label" Text="Cod. Tipo"></asp:Label>
                                                                <asp:HiddenField ID="hdfIdentificadorTipo" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblImporteTpMedioPago" runat="server" SkinID="filter-label" Text="Importe"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Image ID="imgTpMedioPago" runat="server" />
                                                            </td>
                                                            <td style="padding-left: 20px;">
                                                                <asp:TextBox ID="txtCodTpMedioPago" SkinID="filter-textbox" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtImporteTpMedioPago" SkinID="filter-textbox" runat="server" AutoPostBack="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div id="fieldsetMedioPago" runat="server" visible="true">
                                                        <fieldset class="ui-fieldset ui-fieldset-toggleable">
                                                            <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
                                                            </span>
                                                                <asp:Label ID="lblMedioPago" runat="server" Text="MedioPago" /></legend>
                                                            <div class="ui-fieldset-content">
                                                                <div id="divScroll" class="ScrollPopup">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td class="AlineacionCheck">
                                                                                            <asp:CheckBox ID="chkMostrarTerminos" runat="server" SkinID="form-checkbox" AutoPostBack="true" />
                                                                                        </td>
                                                                                        <td class="AlineacionLabel">
                                                                                            <asp:Label ID="lblchkMostrarTerminos" runat="server" Text="InformarDetalles"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Panel ID="pnlDetallesMedioPago" runat="server">
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnBorrarDivisa" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </fieldset>
            </ItemTemplate>
        </asp:Repeater>
    </ContentTemplate>
</asp:UpdatePanel>
