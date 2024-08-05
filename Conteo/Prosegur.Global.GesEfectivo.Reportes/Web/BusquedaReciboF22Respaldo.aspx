<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="BusquedaReciboF22Respaldo.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.BusquedaReciboF22Respaldo" %>
<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Reportes - Recibo F22 respaldo</title>
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
    <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" >
        <ContentTemplate>
            <table class="tabela_campos">
                <tr>
                    <td class="tamanho_celula" align="left">
                        <asp:Label ID="lblDelegacion" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDelegacion" runat="server" Width="150px" CssClass="selectRadius ui-gn-mandatory">
                        </asp:DropDownList>
                    </td>
                </tr>
                </table>
                 <table class="tabela_helper">
                            <tr>
                                <td >
                                    <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                        <ContentTemplate>
                                            <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                  </td>
                                </tr>
                    </table>
              <table class="tabela_campos">
                <tr >
                    <td class="tamanho_celula" align="left">
                        <asp:Label ID="lblFechaFinConteo" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblFechaDesdeFinConteo" runat="server" CssClass="label2"></asp:Label>
                        <asp:TextBox ID="txtFechaDesdeFinConteo" runat="server" MaxLength="16" Width="130px"
                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                        <asp:Label ID="lblFechaHastaFinConteo" runat="server" CssClass="label2"></asp:Label>
                        <asp:TextBox ID="txtFechaHastaFinConteo" runat="server" MaxLength="16" Width="130px"
                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
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
