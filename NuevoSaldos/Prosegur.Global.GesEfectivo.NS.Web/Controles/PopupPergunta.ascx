<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PopupPergunta.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.PopupPergunta" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="ui-fieldset-content-message">
         <div   >
            <asp:Literal ID="ltlimgPergunta" runat="server"></asp:Literal>
            <asp:Label ID="lblPegunta" runat="server" Text="lblPegunta"></asp:Label>
            </div>
            <div class="pergunta-btns" >
            
              <ul class="certificados-btns">
                                <li>
            <asp:Button ID="btnAceitar" SkinID="filter-button" runat="server" Text="Aceitar" /> </li>
             <li><asp:Button ID="btnRecusar" SkinID="filter-button" runat="server" Text="Recusar" /> </li>
            </ul>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
