<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="ConsultaSaldo.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ConsultaSaldo" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div id="dvTituloFiltro" runat="server" class="ui-panel-titlebar" style="display: none; margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblFiltros" runat="server" Text="Saldos" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
        <div id="dvFiltros" runat="server" style="display: none; margin-left: 10px; margin-top: 5px;">
            <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upCliente" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upCanal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:PlaceHolder runat="server" ID="phCanal"></asp:PlaceHolder>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="dvUsarFloat" style="margin-top: 5px; margin-left: 7px;">
                <div style="height: auto;">
                    <asp:PlaceHolder runat="server" ID="phConsiderarValores"></asp:PlaceHolder>
                </div>
                <asp:UpdatePanel ID="upDiscriminarPor" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="height: auto;">
                            <asp:PlaceHolder runat="server" ID="phDiscriminarPor"></asp:PlaceHolder>
                        </div>
                        <div style="height: auto; padding-top: 15px;">
                            <asp:CheckBox runat="server" ID="chkConsiderarSaldoSectoresHijos" Text="Discriminar SubSectores/Puestos" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="dvclear"></div>
                <div style="margin: 5px 25px 10px 5px; height: auto;">
                    <asp:Button ID="btnBuscar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                </div>
                <div class="dvclear"></div>
            </div>
        </div>
        <div id="dvTituloResultado" runat="server" class="ui-panel-titlebar" style="display: none; margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblResultados" runat="server" Text="Saldos" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
        <style>
            .ColunaSemMargin {
                margin: 0px !important;
                padding: 0px !important;
            }

            .ColunaDivisa {
                border-left-style: hidden;
            }
        </style>
        <asp:UpdatePanel ID="upResultado" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="dvResultado" runat="server" style="display: none; margin-left: 10px; margin-top: 5px;">
                    <asp:GridView ID="grvResultadoSaldo" runat="server" AutoGenerateColumns="False"
                        EnableModelValidation="True" BorderStyle="None" Width="98%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sector">
                                <ItemTemplate>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Left" BackColor="White" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cliente e Canal">
                                <ItemTemplate>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Left" BackColor="White" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Literal ID="ColorDivisa" runat="server"></asp:Literal>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" CssClass="ColunaSemMargin" Width="7px" />
                                <ItemStyle HorizontalAlign="Center" ForeColor="#767676" CssClass="ColunaSemMargin" Width="7px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Divisa" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Left" BackColor="White" Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor Disponible">
                                <ItemTemplate>
                                    <asp:Label ID="ValorDisponible" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Right" BackColor="White" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor No Disponible">
                                <ItemTemplate>
                                    <asp:Label ID="ValorNoDisponible" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Right" BackColor="White" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Suma">
                                <ItemTemplate>
                                    <asp:Label ID="Suma" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Right" BackColor="White" Width="150px" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="height: auto; text-align: left; color: #767676; border-style: none; border: solid 1px #FFF; font-style: italic; font-weight: bold;">
                                <%# Tradutor.Traduzir("lblSemRegistro")%>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div class="dvclear"></div>

        <table style="width: 98%; margin-left: 3px;">
            <tr>
                <td style="width: 38%"></td>
                <td style="text-align: right; margin-left: 5px;">
                    <asp:GridView ID="gridTotal" runat="server" AutoGenerateColumns="False"
                        EnableModelValidation="True" BorderStyle="None">
                        <Columns>
                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                </ItemTemplate>
                                <HeaderStyle Width="101px" Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Left" BackColor="White" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Literal ID="ColorDivisa" runat="server"></asp:Literal>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" CssClass="ColunaSemMargin" Width="7px" />
                                <ItemStyle HorizontalAlign="Center" ForeColor="#767676" CssClass="ColunaSemMargin" Width="7px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DES_DIVISA" ItemStyle-HorizontalAlign="Left" ItemStyle-BackColor="White" HeaderStyle-Width="200px" />
                            <asp:BoundField DataField="NUM_IMPORTE_DISP" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="White" HeaderStyle-Width="150px" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="NUM_IMPORTE_NODISP" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="White" HeaderStyle-Width="150px" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="NUM_IMPORTE" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="White" HeaderStyle-Width="150px" DataFormatString="{0:N}" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="height: auto; text-align: left; color: #767676; border-style: none; border: solid 1px #FFF; font-style: italic; font-weight: bold;">
                                <%# Tradutor.Traduzir("lblSemRegistro")%>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblSinSaldo" Text="" runat="server" Visible="false"></asp:Label>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <asp:UpdatePanel ID="upBotones" runat="server">
        <ContentTemplate>
            <center>
            <table style="border-collapse: collapse;">
                <tr>
                    <td>
                        <asp:PlaceHolder ID="phAcciones" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

