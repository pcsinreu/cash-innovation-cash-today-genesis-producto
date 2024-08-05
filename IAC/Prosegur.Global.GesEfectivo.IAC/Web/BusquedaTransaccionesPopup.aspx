<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master" CodeBehind="BusquedaTransaccionesPopup.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaTransaccionesPopup" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:HiddenField runat="server" ID="hdnSelecionado" />
        <asp:UpdatePanel ID="UpdatePanelGeral" runat="server">
            <ContentTemplate>
                <div id="Filtros" style="display: block;">
                    <fieldset id="ExpandCollapseDiv"
                        class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-collapse" onclick="ocultarExibir();">
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server">
                                    Criterios de busqueda</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: block;">
                            <table class="tabela_campos">
                                <tr>

                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblFecha" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <div style="position: relative">
                                            <asp:TextBox ID="txtFecha" runat="server"
                                                CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                Width="195"></asp:TextBox>

                                            <asp:CustomValidator ID="csvFecha" runat="server"
                                                ControlToValidate="txtFecha" Display="Dynamic" ErrorMessage=""
                                                Text="*"></asp:CustomValidator>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="100px"
                                            ClientIDMode="Static" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="100px" />
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </fieldset>
                </div>
                <div style="width: 100%; margin-bottom: 40px">
                    <dx:ASPxGridView runat="server" ID="gvDatos" AutoGenerateColumns="False" KeyFieldName="FechaGestion"
                        OnHtmlRowCreated="gvDatos_OnHtmlRowCreated" Width="100%">
                        <Styles>
                            <AlternatingRow CssClass="dxgvDataRow tr-color"></AlternatingRow>
                        </Styles>
                        <Settings ShowGroupPanel="false" VerticalScrollBarMode="Auto" VerticalScrollableHeight="400"
                            HorizontalScrollBarMode="Auto" />
                        <SettingsBehavior EnableRowHotTrack="false" SortMode="DisplayText" AllowSort="False"></SettingsBehavior>
                        <Columns>
                            <dx:GridViewDataColumn Width="20">
                                <DataItemTemplate>
                                    <input id="rbSelecionado" type="radio" runat="server" class="radio_selecao" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="CodigoTransaccion" Width="187">
                                <CellStyle Font-Size="9px">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="DescripcionFormulario" Width="119">
                                <CellStyle Font-Size="9px">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FechaGestion">
                                <CellStyle Font-Size="9px">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CodigoPuntoServicio">
                                <CellStyle Font-Size="9px">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="DescripcionPuntoServicio">
                                <CellStyle Font-Size="9px">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="MAE" Width="95">
                                <CellStyle Font-Size="9px">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CodigoIsoDivisa">
                                <CellStyle Font-Size="9px">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Importe" PropertiesTextEdit-DisplayFormatString="N2">
                                <CellStyle Font-Size="9px">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="text-align: center; width: 100%; position: fixed; bottom: 0; background-color: white;">
        <table class="tabela_campos">
            <tr>
                <td style="text-align: center">
                    <asp:Button runat="server" ID="btnAceptar" CssClass="ui-button" Width="100" />
                    <asp:Button runat="server" ID="btnCancelar" CssClass="ui-button" Width="100" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
