<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucMovimiento.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.ucMovimiento" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <div id="divAlta" runat="server">
            <div style="width: inherit; height: 35px;">
                <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                    <asp:Label ID="lblDividirPorMovimiento" Text="Dividir periodo por movimiento" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                </div>
                <div style="width: 250px; float: left; margin-left: 2px">
                    <asp:DropDownList ID="ddlMovimiento" runat="server"
                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                        Width="225px">
                    </asp:DropDownList>
                </div>

                
            <div>
                <asp:Button runat="server" ID="btnAnadir" CssClass="ui-button" Width="100px" style="margin-left:50px"/>
            </div>
            </div>

            <br />
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate >
                <dx:ASPxGridView ID="grid" runat="server" 
                    Width="550"  AutoGenerateColumns="False">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="CodigoDescripcion" VisibleIndex="0">
                            <Settings AllowDragDrop="True" />
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataColumn VisibleIndex="2" CellStyle-HorizontalAlign="Left">
                            <DataItemTemplate>
                                <asp:ImageButton runat="server"
                                    ImageUrl="~/App_Themes/Padrao/css/img/button/borrar.png"
                                    ID="imgExcluirForm"
                                    CssClass="imgButton"
                                    OnCommand="imgExcluirForm_OnClick"
                                    CommandArgument='<%# Eval("Identificador") %>' />
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" ShowFilterRowMenu="true" />
                </dx:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </ContentTemplate>
</asp:UpdatePanel>