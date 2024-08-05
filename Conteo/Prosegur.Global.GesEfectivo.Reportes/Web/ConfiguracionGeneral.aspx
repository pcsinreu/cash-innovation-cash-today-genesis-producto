<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master"
    CodeBehind="ConfiguracionGeneral.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ConfiguracionGeneral"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-modal">
        <asp:Panel runat="server" ID="pnGrid">
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblTituloConfiguracionGeneral" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                    <div>
                        <asp:HiddenField runat="server" ID="hdReportes" />
                        <dx:ASPxGridView runat="server" ID="gvReportes" AutoGenerateColumns="False" KeyFieldName="OIDConfiguracionGeneral" Width="100%" OnHtmlRowCreated="gvReportes_OnHtmlRowCreated" OnPageIndexChanged="gvReportes_OnPageIndexChanged">
                            <SettingsPager Position="Bottom" Mode="ShowPager">
                                <PageSizeItemSettings Items="10, 20, 50" />
                            </SettingsPager>
                            <Styles>
                                <AlternatingRow CssClass="dxgvDataRow tr-color"></AlternatingRow>
                            </Styles>
                            <SettingsBehavior SortMode="DisplayText" AllowSort="False"></SettingsBehavior>
                            <Columns>
                                <dx:GridViewDataColumn Width="10px">
                                    <DataItemTemplate>
                                        <input id="ckSelecionado" type="checkbox" runat="server" class="check_selecao" />
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="DesReporte">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CodReporte">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="FormatoArchivo">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ExtensionArchivo">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Separador">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                    <table cellpadding="10">
                        <tr>
                            <td class="style1">
                                <asp:Button runat="server" ID="btnNovo" CssClass="btn-novo" Width="100px" />
                            </td>
                            <td class="style1">
                               
                                <input type="button" id="btnValidaEditar" class="btn-edit" width="100px" runat="server"/>
                            </td>
                            <td class="style1">
                                <input type="button" id="btnExcluir" class="btn-excluir" width="100px" runat="server"/>
                            </td>
                        </tr>
                    </table>
        </asp:Panel>
        <div style="margin-top: 15px;">
        <asp:Panel runat="server" ID="pnFormulario" Visible="False">
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblPnFormulario" CssClass="ui-panel-title" runat="server"></asp:Label>
            </div>
                    <table class="tabela_campos">
                        <tr >
                            <td class="tamanho_celula">
                                <asp:Label runat="server" ID="lblReporte" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlReporte" runat="server" CssClass="selectRadius ui-gn-mandatory">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr >
                            <td class="tamanho_celula">
                                <asp:Label runat="server" ID="lblIDReporte" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtIDReporte" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="upFormatoSalida" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tabela_campos">
                                <tr >
                                    <td class="tamanho_celula">
                                        <asp:Label runat="server" ID="lblFormatoSaida" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFormatoSaida" runat="server" CssClass="selectRadius ui-gn-mandatory" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr >
                                    <td class="tamanho_celula">
                                        <asp:Label runat="server" ID="lblExtensaoArquivo" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtExtensaoArquivo" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="100"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr >
                                    <td class="tamanho_celula">
                                        <asp:Label runat="server" ID="lblSeparador" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSeparador" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="1"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlFormatoSaida" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
            <div>
                <table cellpadding="10">
                    <tr>
                        <td class="style1">
                            <asp:Button runat="server" ID="btnSalvar" CssClass="btn-salvar" />
                        </td>
                        <td class="style1">
                            <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar" />
                        </td>
                    </tr>
                </table>

            </div>
        </asp:Panel>
            </div>
    </div>
    <div class="botaoOcultar">
        <asp:Button runat="server" ID="btnChamarExcluir" />
         <asp:Button runat="server" ID="btnEditar" />
    </div>
</asp:Content>
