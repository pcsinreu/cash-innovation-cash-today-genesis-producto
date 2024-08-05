<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="HistoricoElemento.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.HistoricoElemento" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div id="dvTituloFiltro" runat="server" class="ui-panel-titlebar" style="display: none; margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblFiltros" runat="server" Text="Saldos" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
        <div id="dvFiltros" runat="server" style="display: none; margin-left: 10px; margin-top: 5px;">
            <div class="dvUsarFloat" style="margin-top: 5px; margin-left: 7px;">
                <div class="dvclear"></div>
                <div style="margin: 5px 25px 10px 5px; height: auto;">
                    <asp:Button ID="btnBuscar" runat="server" CssClass="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                </div>
                <div class="dvclear"></div>
            </div>
        </div>
        <div id="dvTituloResultado" runat="server" class="ui-panel-titlebar" style="display: block; margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblResultados" runat="server" Text="Saldos" Style="color: #767676 !important; font-size: 9pt;" />
        </div>

        <asp:UpdatePanel ID="upElementosSelecionar" runat="server">
            <ContentTemplate>
                <div id="dvResultado" runat="server" style="display: none; margin-left: 10px; margin-top: 5px;">
                    <asp:GridView ID="grvResultado" runat="server" AutoGenerateColumns="False"
                        EnableModelValidation="True" BorderStyle="None" Width="98%">
                        <Columns>
                            <asp:BoundField DataField="FORMULARIO" />
                            <asp:BoundField DataField="SECTOR_ORIGEN" />
                            <asp:BoundField DataField="SECTOR_DESTINO" />
                            <asp:BoundField DataField="GMT_CREACION" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="redirecionaDocumento" runat="server" ImageUrl="~/App_Themes/Padrao/css/img/iconos/icon_menu_on.png"
                                        OnClick="redirecionaDocumento_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OID_DOCUMENTO")%>' /><%--<%#Eval("OID_DOCUMENTO")%>--%>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="11px" />
                                <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="height: auto; text-align: left; color: #767676; border-style: none; border: solid 1px #FFF; font-style: italic; font-weight: bold;">
                                <%# Tradutor.Traduzir("lblSemRegistro")%>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br />

        <div class="dvclear"></div>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <asp:UpdatePanel ID="upBotones" runat="server">
        <ContentTemplate>
            <center>
            <table style="border-collapse: collapse;">
                <tr>
                    <td>
                        <asp:PlaceHolder ID="phAcciones" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

