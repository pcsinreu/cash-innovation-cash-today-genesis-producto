<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="SaldoHijos.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.SaldoHijos" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                        <asp:TemplateField HeaderText="Canal">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle HorizontalAlign="Left" BackColor="White" Wrap="true" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cliente">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle HorizontalAlign="Left" BackColor="White" Wrap="true" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle HorizontalAlign="Left" BackColor="White" Wrap="true" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Disponible">
                            <ItemTemplate>
                                <asp:Label ID="ValorDisponible" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle HorizontalAlign="Left" BackColor="White" Width="150px" />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
</asp:Content>
