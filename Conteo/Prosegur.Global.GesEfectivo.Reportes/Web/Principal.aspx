<%@ Page Title="Principal" Language="vb" AutoEventWireup="false" MasterPageFile="~/Principal.Master"
    CodeBehind="Principal.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.PrincipalPag" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table width="25%" align="center">
        <tr>
            <td align="right">
                <asp:Label ID="lblDelegacion" runat="server" Text="Delegação" CssClass="Lbl2"></asp:Label>
            </td>
            <td align="right">
                <asp:DropDownList ID="ddlDelegacion" runat="server" CssClass="Text02" Width="200px"
                    TabIndex="0">
                </asp:DropDownList>
                <asp:CustomValidator ID="csvDelegacion" runat="server" ControlToValidate="ddlDelegacion">*</asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <table>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:Botao ID="btnConfirmar" ExecutaValidacaoCliente="true" runat="server" Habilitado="True"
                                        Tipo="Confirmar" Titulo="012_btn_confirmar" ExibirLabelBtn="True" TabIndex="1">
                                    </pro:Botao>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>                        
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
