<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="Formularios.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Formularios" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional" style="margin-left: 10px;">
        <ContentTemplate>
            <div style="padding-left: 10px;">
                <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
            </div>
            <div style="width: 98%; padding: 10px;">
                <asp:GridView ID="grvResultadoFormularios" runat="server" AutoGenerateColumns="False"
                    EnableModelValidation="True" BorderStyle="None" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Icono">
                            <ItemTemplate>
                                <asp:Literal ID="litImgIcono" runat="server"></asp:Literal>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                            <HeaderStyle Width="110px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Descripcion" HeaderText="Descrição">
                            <%--<ItemStyle Width="100%" />--%>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Individual">
                            <ItemTemplate>
                                <asp:Literal ID="litImgIndividual" runat="server"></asp:Literal>
                            </ItemTemplate>
                            <HeaderStyle Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grupo">
                            <ItemTemplate>
                                <asp:Literal ID="litImgGrupo" runat="server"></asp:Literal>
                            </ItemTemplate>
                            <HeaderStyle Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
