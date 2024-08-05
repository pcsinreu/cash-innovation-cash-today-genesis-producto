<%@ Control Language="vb" EnableViewState ="true" AutoEventWireup="false" CodeBehind="UcDatosCanal.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.UcDatosCanal" %>    

<div id="ucDatosCanal" style="width:100%; height:100%; padding-left:25px;">
    <div id="DatosCanal" style="float:left; width:45%; height:100%">
        <asp:Label ID="lblCodCanal" runat="server" Text="Canal" Width="100px" SkinID="form-label" />
        <asp:DropDownList ID="ddlCanal" runat="server" TabIndex="1" AutoPostBack ="true" SkinID="form-dropdownlist-mandatory"
            OnSelectedIndexChanged="ddlCanal_SelectedIndexChanged" />
    </div>
    <div id="ucDatosSubCanal" style="float:left; width:45%; height:100%; padding-left:25px;">
        <asp:Label ID="lblCodSubCanal" runat="server" Text="SubCanal" Width="100px" SkinID="form-label" />
        <asp:DropDownList ID="ddlSubCanal" runat="server" TabIndex="2" Enabled="false" SkinID="form-dropdownlist-mandatory" />
    </div>
</div>
