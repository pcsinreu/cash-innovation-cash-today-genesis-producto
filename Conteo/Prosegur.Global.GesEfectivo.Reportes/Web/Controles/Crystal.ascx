<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Crystal.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.Crystal" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<CR:CrystalReportViewer ID="crvRelatorios" runat="server" 
    AutoDataBind="true" HasCrystalLogo="False" HasSearchButton="False"
    HasToggleGroupTreeButton="False" HasZoomFactorList="False"
    Height="50px" EnableDrillDown="False" 
    Width="1024px" ViewStateMode="Enabled" />

