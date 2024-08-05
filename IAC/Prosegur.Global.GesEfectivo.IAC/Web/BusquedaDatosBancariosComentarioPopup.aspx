<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master" CodeBehind="BusquedaDatosBancariosComentarioPopup.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaDatosBancariosComentarioPopup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        document.addEventListener("DOMContentLoaded", () => {
            document.getElementById('btnAceptar').disabled = true;
            txtComentario.oninput = function () {
                if (txtComentario.value != null && txtComentario.value.trim() != "")
                    document.getElementById('btnAceptar').disabled = false;
                else
                    document.getElementById('btnAceptar').disabled = true;
            };
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="margin: 0 10px 10px 10px;">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="label2"></asp:Label>
                </div>
                <div style="margin-left: 10px; margin-right: 10px;">
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblTituloComentarioPopup" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                </div>
                <asp:Label Style="float: left;" ID="lblComentario" CssClass="ui-panel-title" runat="server"></asp:Label>
                <asp:TextBox ClientIDMode="Static" style="resize:none;" Width="70%" Height="80px" MaxLength="255" ID="txtComentario" runat="server" TextMode="MultiLine" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <center>
        <table style="margin-top: 10px;">
            <tr align="center">
                <td>
                    <asp:Button runat="server" ID="btnAceptar" Width="100px" ClientIDMode="Static" CssClass="ui-button" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" Width="100px" ClientIDMode="Static" CssClass="ui-button" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
