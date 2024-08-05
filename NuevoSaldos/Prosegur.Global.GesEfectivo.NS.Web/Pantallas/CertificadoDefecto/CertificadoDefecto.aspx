<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="CertificadoDefecto.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.CertificadoDefecto" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <fieldset class="ui-fieldset ui-fieldset-toggleable">
            <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
            </span>
                <asp:Label ID="lblFiltro" runat="server" Text="Filtro Reporte" />
            </legend>
            <asp:UpdatePanel ID="upFiltros" runat="server">
                <ContentTemplate>
                    <div class="ui-fieldset-content">
                        <ul class="form-filter">
                            <li>
                                <script type="text/javascript">
                                    function LegendaCertifacado(a, c) {
                                        var b = document.getElementById('<%= hidLegendaCertificado.ClientID%>');
                                        if (c) {
                                            if (a.value == b.value) {
                                                a.value = '';
                                            }
                                        } else {
                                            if (a.value == '') {
                                                a.value = b.value;
                                            }
                                        }

                                    }
                                </script>
                                <asp:Label ID="lblCodigoCertificado" Width="20%" runat="server" SkinID="filter-label"
                                    Text="Codigo Certificado:" />
                                <asp:TextBox ID="txtCertificado" SkinID="filter-textbox" runat="server" onfocus="javascript:LegendaCertifacado(this, true);"
                                     onblur="javascript:LegendaCertifacado(this, false);" AutoPostBack="true" Width="320px" />
                                <asp:HiddenField ID="hidLegendaCertificado" runat="server" />
                            </li>
                            <li>
                                <asp:Label ID="lblSubCanal" Width="20%" runat="server" SkinID="filter-label" Text="SubCanal:" />
                                <asp:DropDownList runat="server" ID="ddlSubCanal" Style="min-width: 20%" Enabled="false">
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <center>
        <table>
            <tr>
                <td>
                    <ns:Boton ID="btnGenerarReporte" runat="server" Enabled="false" Text="Generar Reporte"
                        ImageUrl="~/App_Themes/Padrao/css/img/button/xls.png" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
