<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="BusquedaRespaldoCompleto.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.BusquedaRespaldoCompleto" title="Untitled Page" %>
<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="JS/Funcoes.js" type="text/javascript"></script>
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
        <asp:UpdatePanel ID="UpdatePanelGeral" runat="server">
            <ContentTemplate>
                <table class="tabela_campos">
                    <tr>
                        <td class="tamanho_celula" align="left">
                            <asp:Label ID="lblDelegacion" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDelegacion" runat="server" Width="150px" CssClass="selectRadius ui-gn-mandatory"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table class="tabela_helper">
                    <tr >
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
                            <asp:Label ID="lblSector" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSector" runat="server" Width="310px" MaxLength="50"
                                CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula" align="left">
                            <asp:Label ID="lblCanal" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>

                                    <td style="width: 317px;">
                                        <asp:UpdatePanel ID="UpdatePanelProceso" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="listCanal" runat="server" Width="317px" CssClass="selectRadius" ></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula" align="left">
                            <asp:Label ID="lblFechaTransporte" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFechaTransporteDesde" runat="server" CssClass="label2"></asp:Label>
                            <asp:TextBox ID="txtFechaTransporteDesde" runat="server" MaxLength="12" Width="77px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                            <asp:Label ID="lblFechaTransporteHasta" runat="server" CssClass="label2"></asp:Label>
                            <asp:TextBox ID="txtFechaTransporteHasta" runat="server" MaxLength="12" Width="77px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula" align="left">
                            <asp:Label ID="lblFecha" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFechaDesde" runat="server" CssClass="label2"></asp:Label>
                            <asp:TextBox ID="txtFechaDesde" runat="server" MaxLength="16" Width="130px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                            <asp:Label ID="lblFechaHasta" runat="server" CssClass="label2"></asp:Label>
                            <asp:TextBox ID="txtFechaHasta" runat="server" MaxLength="16" Width="130px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula" align="left">
                            <asp:Label ID="lblFechaFinConteo" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFechaDesdeFinConteo" runat="server" CssClass="label2"></asp:Label>
                            <asp:TextBox ID="txtFechaDesdeFinConteo" runat="server" MaxLength="16" Width="130px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                            <asp:Label ID="lblFechaHastaFinConteo" runat="server" CssClass="label2"></asp:Label>
                            <asp:TextBox ID="txtFechaHastaFinConteo" runat="server" MaxLength="16" Width="130px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula" align="left">
                            <asp:Label ID="lblRemesasEnCurso" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:CheckBox ID="chkRemesasEnCurso" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula" align="left">
                            <asp:Label ID="lblFormatoSaida" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rblFormatoSaida" runat="server"
                                RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <center>
                <table>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnBuscar" CssClass="btn-check" />
                        </td>
                        <td>
                           <asp:Button runat="server" id="btnLimpar" CssClass="btn-limpar"/>
                        </td>
                    </tr>
                </table>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>