<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="ConfiguracionFormularios.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ConfiguracionFormularios" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"  style="margin-left: 10px;">
    <div style="margin-right: 0px;">
        <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>

                <div style="width: 98%; padding: 10px;">
                    <asp:GridView ID="grvResultadoFormularios" runat="server" AutoGenerateColumns="False"
                        EnableModelValidation="True" BorderStyle="None" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Literal ID="litImgIcono" runat="server"></asp:Literal>
                                </ItemTemplate>
                                <HeaderStyle Width="11px" Font-Size="10px" />
                                <ItemStyle HorizontalAlign="Center" BackColor="#767676" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                                <HeaderStyle Width="110px" Font-Size="11px" />
                                <ItemStyle ForeColor="#767676" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Descripcion" HeaderText="Descrição">
                                <HeaderStyle Font-Size="11px" />
                                <ItemStyle ForeColor="#767676" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate>
                                    <asp:Literal ID="litImgModificar" runat="server"></asp:Literal>
                                </ItemTemplate>
                                <HeaderStyle Width="90px" Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="ImgEliminar" ImageUrl="~/Imagenes/Quitar2.png" OnCommand="ImgEliminar_Command" Style="background: none !important;" />
                                </ItemTemplate>
                                <HeaderStyle Width="90px" Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
