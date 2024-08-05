<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucHelperBusquedaAvanzada.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ucHelperBusquedaAvanzada" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:UpdatePanel ID="upBusquedaAvanzada" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px; width: 730px; display:none">
            <asp:Label ID="lblFiltro" runat="server" Text="" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
        <div class="dvclear">
        </div>
        <div class="dvUsarFloat" style="margin-left: 5px; width: 730px;">
            <div id="dvCodigo" style="margin: 0px 25px 2px 0px; height: auto; display: none;">
                <asp:Label ID="lblCodigo" runat="server" /><br />
                <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" onkeydown="javascript: return event.keyCode != 13" />
            </div>
            <div id="dvDescripcion" style="margin: 0px 25px 2px 0px; height: auto; display:none;">
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
            <div runat="server" id="dvBotoes" style="display:none;" >
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
            <div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <dx:ASPxGridView runat="server" ID="gvDatos" AutoGenerateColumns="False" KeyFieldName="Identificador" Width="730" OnHtmlRowCreated="gvDatos_OnHtmlRowCreated" DataSourceForceStandardPaging="True" OnPageIndexChanged="gvDatos_OnPageIndexChanged" OnProcessOnClickRowFilter="gvDatos_OnProcessOnClickRowFilter">
                            <SettingsPager Position="Bottom" Mode="ShowPager">
                                <PageSizeItemSettings Items="10, 20, 50" />
                            </SettingsPager>
                            <Styles>
                                <AlternatingRow CssClass="dxgvDataRow tr-color"></AlternatingRow>
                            </Styles>
                            <Settings ShowFilterRow="True" EnableFilterControlPopupMenuScrolling="True"></Settings>
                           <SettingsBehavior FilterRowMode="OnClick" SortMode="DisplayText" AllowSort="False"></SettingsBehavior>
                            <Columns>
                                <dx:GridViewDataColumn Width="10px">
                                    <DataItemTemplate>
                                        <input id="rbSelecionado" type="radio" runat="server" class="radio_selecao" />
                                        <asp:CheckBox ID="chkSelecionado" ValidationGroup="chkSelecionado" runat="server" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="150px" FieldName="Codigo">
                                    <Settings AutoFilterCondition="Contains" FilterMode="DisplayText"></Settings>
                                    <DataItemTemplate>
                                        <asp:Label ID="lblItemCodigo" CssClass="limitText" Width="150px" runat="server" />
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="10px"></HeaderStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="450px" FieldName="Descricao">
                                    <DataItemTemplate>
                                        <asp:Label ID="lblItemDescricao" CssClass="limitText" Width="450px" runat="server" />
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="10px"></HeaderStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="IdentificadorPai" Visible="False"></dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="dvUsarFloat" style="width: 730px;">
    <div id="divBtnAceptar" style="margin: 12px 25px 2px 0px; height: auto; min-width: 40px;">
        <asp:Button ID="btnAceptar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
    </div>
    <div id="divBtnCancelar" style="margin: 12px 25px 2px 0px; height: auto;">
        <asp:Button ID="btnCancelar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
    </div>
    <div class="dvclear">
    </div>
</div>
