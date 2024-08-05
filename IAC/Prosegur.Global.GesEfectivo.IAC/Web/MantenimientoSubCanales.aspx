<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoSubCanales.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoSubCanales" MasterPageFile="Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Mantenimiento de Subcanales</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <base target="_self" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel runat="server" ID="updGeral">
            <ContentTemplate>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblTituloCanales" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_campos">
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodigoSubCanal" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelCodigoSubCanal" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCodigoSubCanal" Width="130" runat="server" MaxLength="15" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                    <asp:CustomValidator ID="csvCodigoSubCanalObrigatorio" runat="server" ControlToValidate="txtCodigoSubCanal">*</asp:CustomValidator>
                                    <asp:CustomValidator ID="csvCodigoCanal" runat="server" ErrorMessage="" ControlToValidate="txtCodigoSubCanal">*</asp:CustomValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblDescricaoSubCanal" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelDescricaoSubCanal" runat="server" >
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDescricaoSubCanal" runat="server" Width="384px" MaxLength="50"
                                        AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                    <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ControlToValidate="txtDescricaoSubCanal">*</asp:CustomValidator>
                                    <asp:CustomValidator ID="csvDescripcion" runat="server" ErrorMessage="" ControlToValidate="txtDescricaoSubCanal">*</asp:CustomValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td >
                            <asp:TextBox ID="txtCodigoAjeno" runat="server" MaxLength="25" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                AutoPostBack="True" Enabled="False" Width="130" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblDescricaoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescricaoAjeno" runat="server" Width="280px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                AutoPostBack="True" Enabled="False" ReadOnly="True"></asp:TextBox>
                            <asp:Button runat="server" ID="btnAlta" CssClass="ui-button" Width="100"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblObservaciones" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="4000" TextMode="MultiLine"
                                CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Height="76px" Width="674px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="chkVigente" runat="server" AutoPostBack="true" OnCheckedChanged="chkVigente_CheckedChanged" />
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                    <ContentTemplate>
                        <table style="margin: 0px !Important;">
                            <tr>
                                <td >
                                    <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="100"/>
                                    <div class="botaoOcultar">
                                        <asp:Button runat="server" ID="btnConsomeCodigoAjeno"/>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
