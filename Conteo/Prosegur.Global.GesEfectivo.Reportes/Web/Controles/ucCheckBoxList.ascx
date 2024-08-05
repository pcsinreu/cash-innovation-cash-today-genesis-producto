<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucCheckBoxList.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ucCheckBoxList" %>
<div style="padding-right:5px;">
    <asp:CheckBox runat="server" ID="chkTodos" Text="Todos" AutoPostBack="true"/>
</div>
<div id="divChk" style="border-style:inset; overflow:auto; height:80px; border-width:thin">
    <asp:CheckBoxList ID="cbl" runat="server" RepeatDirection="Vertical" 
        RepeatLayout="Flow" Height="20px" AutoPostBack="true">
    </asp:CheckBoxList>
</div>
