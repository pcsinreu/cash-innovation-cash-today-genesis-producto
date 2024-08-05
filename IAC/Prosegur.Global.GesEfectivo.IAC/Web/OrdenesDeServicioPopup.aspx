<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master" CodeBehind="OrdenesDeServicioPopup.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.OrdenesDeServicioPopup" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 160px;
        }
    </style>
    <script type="text/javascript">
        function checkRadioBtn(id) {
            var hdn = document.getElementById('<%=hdnSelecionado.ClientID %>');

            for (var i = 1; i < gv.rows.length; i++) {
                var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

                // Check if the id not same
                if (radioBtn[0].id != id.id) {
                    radioBtn[0].checked = false;
                }
                else {
                    radioBtn[0].checked = true;
                    hdn.value = radioBtn[0].id;
                }
            }
        }

    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdnSelecionado" />
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTitulo" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>

                <table class="tabela_interna" style="margin-top: 5px;">
                    <tr>
                        <td align="center">
                            <div style="text-align: center;">
                                        <dx:ASPxGridView ID="gridNotificacionesDet" runat="server"
                                            KeyFieldName="OidIntegracion" Width="99%" AutoGenerateColumns="False"
                                            EnableTheming="True"
                                            Theme="Office2010Silver">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="OidIntegracion" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                    <Settings AllowDragDrop="False" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Left">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="NumeroDeIntento" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                    <Settings AllowDragDrop="False" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Left">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Fecha" VisibleIndex="3">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Left">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Estado" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                                                    <Settings AllowDragDrop="False" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Left">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Observaciones" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center">
                                                    <Settings AllowDragDrop="False" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Left">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="BError" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                                                    <Settings AllowDragDrop="False" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Left">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
  
                                            </Columns>
                                            <Settings ShowFilterRow="False" ShowFilterRowMenu="false" />
                                            <SettingsPager PageSize="20" Mode="ShowAllRecords" >
                                                <PageSizeItemSettings Visible="false" />
                                            </SettingsPager>
                                            <SettingsBehavior AllowSort="False"/>
                                        </dx:ASPxGridView>
                                    </div>
                        </td>
                    </tr>
                </table>
                <script type="text/javascript">
                    //Script necessário para evitar que dê erro ao clicar duas vezes em algum controle que esteja dentro do updatepanel.
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    prm.add_initializeRequest(initializeRequest);

                    function initializeRequest(sender, args) {
                        if (prm.get_isInAsyncPostBack()) {
                            args.set_cancel(true);
                        }
                    }
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="position: fixed; bottom: 0; width: 100%;">
<%--        <table class="tabela_campos">
            <tr>
                <td style="text-align: center">
                    <asp:Button runat="server" ID="btnSeleccionar" CssClass="ui-button" Width="100" />
                    <asp:Button runat="server" ID="btnCancelar" CssClass="ui-button" Width="100" />
                </td>
            </tr>
        </table>--%>
    </div>
</asp:Content>
