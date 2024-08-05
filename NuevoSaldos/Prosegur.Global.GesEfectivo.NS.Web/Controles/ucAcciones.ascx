<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucAcciones.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucAcciones" %>
<%@ Register Src="Boton.ascx" TagName="Boton" TagPrefix="uc1" %>
<asp:UpdatePanel ID="upBotones" runat="server">
    <ContentTemplate>
        <center>
            <table style="border-collapse: collapse;">
                <tr>
                    <td>
                        <uc1:Boton ID="btnGuardar" runat="server" Text="F9 Grabar" ImageUrl="~/App_Themes/Padrao/css/img/button/gravar.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnGuardarConfirmar" runat="server" Text="GuardarConfirmar" ImageUrl="~/App_Themes/Padrao/css/img/button/confirmar.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnConfirmar" runat="server" Text="F10 Eliminar" ImageUrl="~/App_Themes/Padrao/css/img/button/confirmar.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnAceptar" runat="server" Text="F11 Nuevo" ImageUrl="~/App_Themes/Padrao/css/img/button/confirmar.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnRechazar" runat="server" Text="F12 Exportar" ImageUrl="~/App_Themes/Padrao/css/img/button/rechazar.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnAnular" runat="server" Text="F12 Exportar" ImageUrl="~/App_Themes/Padrao/css/img/button/borrar.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnModificar" runat="server" Text="F12 Exportar" ImageUrl="~/App_Themes/Padrao/css/img/button/edit.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnModificarTermino" runat="server" Text="Modificar Términos" ImageUrl="~/App_Themes/Padrao/css/img/button/gravar.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnSalvarTerminos" runat="server" Text="Salvar Términos" ImageUrl="~/App_Themes/Padrao/css/img/button/gravar.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnImprimir" runat="server" Text="F12 Exportar" ImageUrl="~/App_Themes/Padrao/css/img/button/gravar.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnVisualizar" runat="server" Text="F12 Exportar" ImageUrl="~/App_Themes/Padrao/css/img/button/buscar.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnCancelar" runat="server" Text="F12 Exportar" ImageUrl="~/App_Themes/Padrao/css/img/button/back.png" Visible="true" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnAgregarDocumento" runat="server" Text="F12 Exportar" ImageUrl="~/App_Themes/Padrao/css/img/button/nuevo.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnConfigurarNovoReporte" runat="server" Text="F12 Exportar" ImageUrl="~/App_Themes/Padrao/css/img/button/nuevo.png" Visible="false" />
                    </td>
                    <%--<td>
                        <uc1:Boton ID="btnAgregarBulto" runat="server" Text="F12 Exportar" ImageUrl="~/App_Themes/Padrao/css/img/button/adicionar.png" Visible="false" />
                    </td>
                    <td>
                        <uc1:Boton ID="btnAgregarParcial" runat="server" Text="F12 Exportar" ImageUrl="~/App_Themes/Padrao/css/img/button/adicionar.png" Visible="false" />
                    </td>--%>
                </tr>
            </table>
        </center>
    </ContentTemplate>
</asp:UpdatePanel>
