<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="ProvisionEfectivo.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ProvisionEfectivo" %>



<%@ MasterType VirtualPath="~/Master/Master.Master" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpTodo" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="content filtro" style="color: #767676;">

                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblProvisionEfectivo" CssClass="ui-panel-title" runat="server">lblProvisionEfectivo</asp:Label>
                </div>

                <div runat="server" id="dvFecha" name="dvFecha" style="float: left; margin: 8px 10px 10px 0px;">
                    <div runat="server" id="Div1" name="dvFechaGestion" style="width: 450px; float: left; margin: 0px 10px 10px 10px;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <asp:PlaceHolder runat="server" ID="phBanco"></asp:PlaceHolder>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                    <div runat="server" id="dvFechaGestion" name="dvFechaGestion" style="float: left; margin: 0px 10px 10px 0px;">
                        <asp:Label ID="lblFechaGestion" runat="server" Text="lblFechaGestion" /><br />
                        <asp:TextBox ID="txtFechaGestion" runat="server" />
                    </div>
                </div>
                <div class="dvclear"></div>
                <div style="margin-bottom:10px; margin-left:10px;">
                    <asp:GridView ID="grdDivisas" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="300px">
                        <Columns>
                            <asp:BoundField DataField="Descripcion" HeaderText="Codigo">
                                <HeaderStyle Width="110px" Font-Size="11px" />
                                <ItemStyle ForeColor="#767676" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Efectivo Total">

                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpTxtValor" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
                                    <asp:TextBox id="txtValor" runat="server"   onkeyup="moedaIAC(event,this,',','.');" onkeypress="return bloqueialetras(event, this);" onblur="moedaIAC(event, this,',','.');"
                                       ></asp:TextBox>
            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    <asp:HiddenField ID="hfIdentificador" runat="server"  Value='<%# Eval("Identificador").ToString() %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            

                        </Columns>
                    </asp:GridView>
                </div>
                <div class="dvclear"></div>

                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblDetalheCuentaCapital" CssClass="ui-panel-title" runat="server">lblDetalheCuentaCapital</asp:Label>
                </div>
                <div runat="server" id="Div5" name="dvFecha" style="float: left; margin-top:8px; margin-left: 10px;">
                    <div id="dvSecuencia" runat="server" style="float: left; width: 140px;">
                        <asp:Label ID="lblTituloDelegacion" runat="server" Text="lblDelegacion" /><br />
                        <asp:Label ID="lblDelegacion" runat="server" Enabled="true" Style="width: 200px;font-size: 14px;font-weight: bold; " />
                    </div>

                    <div id="Div2" runat="server" style="float: left; margin-left: 10px; width: 140px;">
                        <asp:Label ID="lblTituloSector" runat="server" Text="lblSector" /><br />
                        <asp:Label ID="lblSector" runat="server"  Style="width: 200px;font-size: 14px;font-weight: bold; "/>
                    </div>

                    <div id="Div3" runat="server" style="float: left; margin-left: 10px; width: 140px;">
                        <asp:Label ID="lblTituloCanal" runat="server" Text="lblCanal" /><br />
                        <asp:Label ID="lblCanal" runat="server" Enabled="true" Style="width: 200px;font-size: 14px;font-weight: bold; " />
                    </div>

                    <div id="Div4" runat="server" style="float: left; margin-left: 10px; width: 140px;">
                        <asp:Label ID="lblTituloSubCanal" runat="server" Text="lblSubCanal" /><br />
                        <asp:Label ID="lblSubCanal" runat="server"  Style="width: 200px;font-size: 14px;font-weight: bold;" />
                    </div>
                </div>

                <div class="dvclear">
                    <br />
                </div>


            
                <asp:PlaceHolder ID="phInfAdicionales" runat="server"></asp:PlaceHolder>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
        <asp:PlaceHolder ID="phAcciones" runat="server"></asp:PlaceHolder>
</asp:Content>
