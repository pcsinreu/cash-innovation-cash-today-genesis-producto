<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Empty.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.Empty" %>

<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IAC</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        &nbsp;&nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 700px" border="1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 300px">
                            <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
                                <SelectedNodeStyle BackColor="Lime" />
                            </asp:TreeView>
                        </td>
                        <td align="center" style="width: 100px">
                            <asp:Button ID="Button2" runat="server" Text=">" />
                            <br />
                            <asp:Button ID="Button1" runat="server" Text="<" />
                        </td>
                        <td style="width: 300px">
                            <asp:TreeView ID="TreeView2" runat="server">
                                <SelectedNodeStyle BackColor="Lime" />
                            </asp:TreeView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td>
                </td>
                <td style="width: 39px;">
                    <asp:Button ID="Button3" runat="server" Text="get Childs" Visible="False" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 39px;">
                    <asp:Button ID="Button4" runat="server" Text="get Parent" Visible="False" />
                </td>
                <td>
                    <asp:Button ID="Button5" runat="server" Text="Remover Selecionado" Visible="False" />
                </td>
            </tr>
        </table>
        <div id="AlertDivAll" class="AlertLoading" style="visibility: hidden;"></div>
    </div>
    </form>
</body>
</html>
