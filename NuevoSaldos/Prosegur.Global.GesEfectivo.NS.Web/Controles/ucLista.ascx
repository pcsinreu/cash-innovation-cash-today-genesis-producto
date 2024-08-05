<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucLista.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucLista" %>

<asp:Literal ID="litDicionario" runat="server"></asp:Literal>
<div id="dvLista" runat="server" class="lista">
    <div id="dvTitulo" runat="server" class="titulo">
        <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
    </div>
    <div class="dvclear"></div>
    <div id="dvValores" runat="server" class="valores">
        <asp:PlaceHolder ID="phLista" runat="server"></asp:PlaceHolder>
    </div>
    <div class="dvclear"></div>
    <div id="dvAcciones" runat="server" class="acciones">        
        <asp:Label ID="legenda" runat="server" Text="" CssClass="legenda"></asp:Label>
        <button type="button" value="" id="btnAgregarTodos" runat="server" class="imagem" >
            <img src="<%= ResolveUrl("~/Imagenes/ckeck_true.png")%>" alt="" />
        </button>
        <button type="button" value="" id="btnDesAgregarTodos" runat="server" class="imagem" >
            <img src="<%= ResolveUrl("~/Imagenes/ckeck_false.png")%>" alt="" />
        </button>
    </div>
</div>
<asp:HiddenField ID="hidValoresSeccionados" runat="server" />
<asp:HiddenField ID="hidCantidadItens" runat="server" />
<asp:Literal ID="litScripts" runat="server"></asp:Literal>