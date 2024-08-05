<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucAgregarDivisa.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucAgregarDivisa" %>
<%@ Register Src="~/Controles/Popup.ascx" TagName="Popup" TagPrefix="ucPopup" %>
<%--<style type="text/css">
    .teste
    {
        background-image: url("../Imagenes/Sort-Ascending.png");
        background-repeat: no-repeat; /*background-image:url(/path/to/image.jpg);*/
    }
</style>--%>
<asp:UpdatePanel ID="upAgregarDivisaFull" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="false">
    <ContentTemplate>
        <fieldset class="ui-fieldset ui-fieldset-toggleable">
            <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
            </span>
                <asp:Label ID="lblDivisas" runat="server" Text="Divisas" />
            </legend>
            <div class="ui-fieldset-content">
                <asp:UpdatePanel ID="upGridView" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <div id="divGridView" visible="false" runat="server" style="width: 440px; height:250px;
                            overflow-y: auto; padding-top: 10px">
                            <asp:GridView ID="gdvDivisas" DataKeyNames="Identificador" runat="server" EnableModelValidation="True"
                                AutoGenerateColumns="False" Width="420px"
                                EnableSortingAndPagingCallbacks="True" BorderStyle="None">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkTodos" runat="server" onclick="checkAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDivisas" runat="server" Checked="false" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Identificador" DataField="Identificador" Visible="False" />
                                    <asp:BoundField HeaderText="Divisas" DataField="Descripcion" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </fieldset>
        <asp:UpdatePanel ID="upButton" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <ul class="certificados-btns">
                    <li>
                        <asp:Button ID="btnAceptar" runat="server" SkinID="filter-button" Text="Aceitar" />
                    </li>
                    <li>
                        <asp:Button ID="btnCancelar" runat="server" SkinID="filter-button" Text="Cancelar" />
                    </li>
                </ul>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </ContentTemplate>
</asp:UpdatePanel>
