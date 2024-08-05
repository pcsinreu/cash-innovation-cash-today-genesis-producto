<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="Default.aspx.vb" Inherits="Prosegur.Genesis.Web._Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="./_Scripts/Script_PageDefault.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%;">&nbsp;</div>
    <div class="loginbox" style="">
        <div style="width:100%; height:40px; float: left;"></div>
        <div style="width: 45%; float: left; text-align: center; margin: 1px 0px 20px 4px;">
            <img alt="Genesis" src="_Imagens/genesis.png" style="margin-top: 10px;" />
        </div>
        <div style="width:40%; height:40px; float: left;"></div>
        <div class="legenda">
            <asp:Label runat="server" ID="lblUsuario" Style="font-size:12px;"></asp:Label>
        </div>
        <div class="campo">
            <input id="txtUsuario" type="text" maxlength="50" onkeydown="EventoEnter('btnLogin');" />
        </div>
        <div class="legenda">
            <asp:Label runat="server" ID="lblSenha" Style="font-size:12px;"></asp:Label>
        </div>
        <div class="campo">
            <input id="txtSenha" type="password" maxlength="50" onkeydown="EventoEnter('btnLogin');" />
        </div>
        <div class="legenda">
            <asp:Label runat="server" ID="lblPais" Style="font-size:12px;"></asp:Label>
        </div>
        <div class="campo">
            <select id="ddlPais" onkeydown="EventoEnter('btnLogin');">
            </select>
        </div>
        <div style="width: 100%; float: left; margin: 1px 0px 1px 4px;">
            <button type="button" class="button" value="Login" id="btnLogin" onclick="Javascript: btnLogin_OnClick();">
                <img alt="login" src="_Imagens/true.png" width="16" height="16" />
                <asp:Label runat="server" ID="lblLogin" CssClass="label"></asp:Label>
            </button>
            <button type="button" class="button" value="Cancelar" id="btnCancelar" onclick="Javascript: btnCancelar_OnClick();">
                <img alt="login" src="_Imagens/false.png" width="16" height="16" />
                <asp:Label runat="server" ID="lblCancelar" CssClass="label"></asp:Label>
            </button>
        </div>
        <div style="width:100%; height:40px; float: left;"></div>
        <div style="clear:both;"></div>
    </div>
    <div style="width: 492px; position: relative;">
        <div style="width:100%; height:40px; float:right; text-align:right; margin-right:20px;margin-top: 5px;" class="versionAplicacion"><asp:Label runat="server" ID="lblVersao"></asp:Label></div>
    </div>
</asp:Content>
