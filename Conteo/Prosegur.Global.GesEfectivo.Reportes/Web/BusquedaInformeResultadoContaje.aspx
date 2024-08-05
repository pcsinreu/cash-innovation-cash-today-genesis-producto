<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="BusquedaInformeResultadoContaje.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.BusquedaInformeResultadoContaje" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>

<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="Controles/Erro.ascx" TagName="Erro" TagPrefix="uc1" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Reportes - Infome Resultado Contaje</title>
    <style type="text/css">
        .botaoOcultar {
            display: none;
        }
    </style>
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <script type="text/javascript">
        function AddRemovIdSelect(obj, hidden, isRadio, btnAceptar) {
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
        <div class="botaoOcultar">
            <asp:Button runat="server" ID="btnConfEscogido" />
        </div>
        <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" >
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnlFiltros" Visible="True">
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblSubTituloCriteriosBusqueda" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                    <table class="tabela_campos">
                        <tr>
                            <td class="tamanho_celula" align="left">
                                <asp:Label ID="lblDelegacion" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDelegacion" runat="server" Width="150px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula" align="left">
                                <asp:Label ID="lblPrecintoRemesa" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrecintoRemesa" runat="server" Width="145px" MaxLength="36" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula" align="left">
                                <asp:Label ID="lblPrecintoBulto" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrecintoBulto" runat="server" Width="145px" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula" align="left">
                                <asp:Label ID="lblNumAlbaran" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumAlbaran" runat="server" Width="145px" MaxLength="36" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
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
                    <table class="tabela_campos">
                        <tr>
                            <td class="tamanho_celula" align="left">
                                <asp:Label ID="lblFechaConteo" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFechaConteoDesde" runat="server" CssClass="label2"></asp:Label>
                                <asp:TextBox ID="txtFechaConteoDesde" runat="server" MaxLength="16" Width="130px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                <asp:Label ID="lblFechaConteoHasta" runat="server" CssClass="label2"></asp:Label>
                                <asp:TextBox ID="txtFechaConteoHasta" runat="server" MaxLength="16" Width="130px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula" align="left">
                                <asp:Label ID="lblFechaTransporte" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFechaTransporteDesde" runat="server" CssClass="label2"></asp:Label>
                                <asp:TextBox ID="txtFechaTransporteDesde" runat="server" MaxLength="16" Width="130px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                <asp:Label ID="lblFechaTransporteHasta" runat="server" CssClass="label2"></asp:Label>
                                <asp:TextBox ID="txtFechaTransporteHasta" runat="server" MaxLength="16" Width="130px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlGrid" Visible="False">
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblTituloGrid" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField runat="server" ID="hdResultadoContaje" />
                                <dx:ASPxGridView runat="server" ID="gvResultadoContaje" AutoGenerateColumns="False" KeyFieldName="OidRemsa" Width="100%" OnHtmlRowCreated="gvResultadoContaje_OnHtmlRowCreated">
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
                                                <input id="rbSelecionado" type="radio" runat="server" class="radio_selecao" />
                                            </DataItemTemplate>
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="FechaConteo">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PrecintoRemesa">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CodTransporte">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FechaTransporte">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataColumn FieldName="Cliente">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SubCliente">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="PuntoServicio">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <center>
                <asp:Panel runat="server" ID="pnBotoesPesquisa" Visible="True">

                    <table>
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnBuscar" CssClass="btn-buscar" />
                            </td>
                            <td>
                               <asp:Button runat="server" id="btnLimpar" CssClass="btn-limpar"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnBotoesGrid" Visible="False">
                    <table>
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnVisualizar" CssClass="btn-visualizar" />
                            </td>
                            <td>
                               <asp:Button runat="server" id="btnVoltar" CssClass="btn-voltar"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
              </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
