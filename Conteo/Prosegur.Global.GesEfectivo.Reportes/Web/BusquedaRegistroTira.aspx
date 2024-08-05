<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    EnableEventValidation="false" CodeBehind="BusquedaRegistroTira.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.BusquedaRegistroTira" %>
<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         function AddRemovIdSelect(obj,hidden,isRadio,btnAceptar) {
             if (isRadio) {
                 document.getElementById(hidden).value = '';
             }
             if (obj.checked) {
                 //Caso id já exista na lista o id duplicado é descartado.
                 if (document.getElementById(hidden).value.indexOf(obj.value + "|") < 0) {
                     document.getElementById(hidden).value += obj.value + "|";
                 }
             }
             else {

                 var strtemp = document.getElementById(hidden).value.replace(id + "|", "");
                 document.getElementById(hidden).value = strtemp;
             }
             if (isRadio) {
                 document.getElementById(btnAceptar).click();
             }
         }
           function ExecutarClick(btn) {
                 document.getElementById(btn).click();
           }

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblSubTituloCriteriosBusqueda" CssClass="ui-panel-title" runat="server"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td valign="top">
                            <table class="tabela_campos">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoATM" Text="lblCodigoATM" CssClass="label2" Style="white-space: nowrap" runat="server" />
                                    </td>
                                    <td style="width: 130px" colspan="8">
                                        <asp:TextBox ID="txtCodigoATM" Width="130px" MaxLength="20" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table class="tabela_helper">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                            <ContentTemplate>
                                                <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>

                        </td>
                        <td valign="top" style="width: 650px;">
                            <table class="tabela_campos">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label runat="server" ID="lblData" Text="Data"></asp:Label>
                                     </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblFechaInicio" Text="lblFechaInicio" CssClass="label2" Style="white-space: nowrap" runat="server" />

                                        <asp:TextBox ID="txtFechaInicio" Width="77px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" />
                                        <asp:Label ID="lblFechaFim" Text="lblFechaFim" CssClass="label2" Style="white-space: nowrap" runat="server" />
                                        <asp:TextBox ID="txtFechaFim" Width="77px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblPeriodoContable" Text="lblPeriodoContable" CssClass="label2" Style="white-space: nowrap" runat="server" />
                                    </td>
                                    <td style="width: 130px" >
                                        <asp:TextBox ID="txtPeriodoContable" Width="250px" MaxLength="1024" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" TextMode="MultiLine" Rows="3" />
                                    </td>
                                    <td style="padding-left: 16px;">
                                          <table>
                                            <tr>
                                                <td>
                                                    <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="100px" />
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="100px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                 
            </ContentTemplate>
        </asp:UpdatePanel>
         <div class="ui-panel-titlebar">
            <asp:Label ID="lblTituloTabela" CssClass="ui-panel-title" runat="server"></asp:Label>
        </div>
        <div>
                    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server">
                        <ContentTemplate>
                             <div style="float: none;">
                                <asp:Panel runat="server" ID="pnGridTiras" Visible="False">
                                    <dx:ASPxGridView runat="server" ID="gvTiras" AutoGenerateColumns="False" KeyFieldName="Oidtira" Width="100%" OnHtmlRowCreated="gvTiras_OnHtmlRowCreated">
                                        <SettingsPager Position="Bottom" Mode="ShowPager">
                                            <PageSizeItemSettings Items="10, 20, 50" />
                                        </SettingsPager>
                                        <Styles>
                                            <AlternatingRow CssClass="dxgvDataRow tr-color"></AlternatingRow>
                                        </Styles>
                                        <SettingsBehavior SortMode="DisplayText" AllowSort="False"></SettingsBehavior>
                                      <Columns>
                                            <dx:GridViewDataTextColumn FieldName="FyhRegistroTira">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <DataItemTemplate>
                                                    <asp:Label runat="server" ID="lblItemFyhRegistroTira"></asp:Label>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="CodCajero">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <DataItemTemplate>
                                                    <asp:Label runat="server" ID="lblItemCodCajero"></asp:Label>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="DesPeriodoContable">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <DataItemTemplate>
                                                    <asp:Label runat="server" ID="lblItemDesPeriodoContable"></asp:Label>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="CodCliente" Visible="False">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="DesCliente">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <DataItemTemplate>
                                                    <asp:Label runat="server" ID="lblItemDesCliente"></asp:Label>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                             <dx:GridViewDataTextColumn FieldName="CodSubcliente" Visible="False">
                                                </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="DesSubcliente">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <DataItemTemplate>
                                                    <asp:Label runat="server" ID="lblItemDesSubcliente"></asp:Label>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                           <dx:GridViewDataColumn FieldName="CodPtoServicio" Visible="False">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="DesPtoServicio">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <DataItemTemplate>
                                                    <asp:Label runat="server" ID="lblItemDesPtoServicio"></asp:Label>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="OidTira">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                                                <DataItemTemplate>
                                                      <asp:Image ID="imgConsultar" runat="server" />
                                                      <asp:HiddenField ID="hidOidTira" runat="server" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
              </div>
  </div>
</asp:Content>
