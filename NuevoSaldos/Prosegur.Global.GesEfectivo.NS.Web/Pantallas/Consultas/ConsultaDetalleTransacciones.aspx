<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master" CodeBehind="ConsultaDetalleTransacciones.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ConsultaDetalleTransacciones" %>


<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tooltip {
            position: relative;
            display: inline-block;
        }

            /* Tooltip text */
            .tooltip .tooltiptext {
                visibility: hidden;
                width: auto;
                background-color: #555;
                color: #fff;
                text-align: center;
                padding: 5px 0;
                border-radius: 6px;
                /* Position the tooltip text - see examples below! */
                position: absolute;
                z-index: 1;
                transition: opacity 0.3s;
            }

            /* Show the tooltip text when you mouse over the tooltip container */
            .tooltip:hover .tooltiptext {
                visibility: visible;
            }

        .dvUsarFloat div div {
            margin: 5px 25px 2px 0px;
            width: auto;
            color: #767676;
        }

          
    </style>
    <script type="text/javascript">
      

        function FecharModala() {

            debugger;
            var a = 1;
        }



    </script>
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpTodo" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="margin: 10px;">
                 <div style="margin-bottom: 30px;">
                       <asp:Label ID="Label1" runat="server" Text="">Origen</asp:Label><br />
                    <asp:DropDownList ID="ddlConta" runat="server" AutoPostBack="true"></asp:DropDownList>
                </div>

                <div class="dvUsarFloat">



                    <div>
                        <asp:Label ID="lblCliente" runat="server" Text="">lblCliente</asp:Label><br />
                        <asp:Label ID="txtCliente" runat="server" Text="" CssClass="valor">txtCliente</asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblSubCliente" runat="server" Text="">lblSubCliente</asp:Label><br />
                        <asp:Label ID="txtSubCliente" runat="server" Text="" CssClass="valor">txtSubCliente</asp:Label>
                    </div>
                    <div class="dvclear"></div>
                    <div>
                        <asp:Label ID="lblPuntoServicio" runat="server" Text="">lblPuntoServicio</asp:Label><br />
                        <asp:Label ID="txtPuntoServicio" runat="server" Text="" CssClass="valor">txtPuntoServicio</asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblMaquina" runat="server" Text="">lblMaquina</asp:Label><br />
                        <asp:Label ID="txtMaquina" runat="server" Text="" CssClass="valor">txtMaquina</asp:Label>
                    </div>
                    <div class="dvclear"></div>
                    <div>

                        <div>
                            <asp:Label ID="lblCodigoExterno" runat="server" Text="">lblCodigoExterno</asp:Label><br />
                            <asp:Label ID="txtCodigoExterno" runat="server" Text="" CssClass="valor">txtCodigoExterno</asp:Label>
                        </div>




                        <div id="divTool" runat="server" class="tooltip" style="margin-top: 10px; margin-left: 3px;">

                            <img src="../../Imagenes/help_parametros.png" />
                            <span style="padding: 7px;" id="txtCodigos" runat="server" class="tooltiptext"></span>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblTipoMovimiento" runat="server" Text="">lblTipoMovimiento</asp:Label><br />
                        <asp:Label ID="txtTipoMovimiento" runat="server" Text="" CssClass="valor">TipoMovimiento</asp:Label>
                    </div>

                    <div>
                        <asp:Label ID="lblFormulario" runat="server" Text="">lblFormulario</asp:Label><br />
                        <asp:Label ID="txtFormulario" runat="server" Text="" CssClass="valor">txtFormulario</asp:Label>
                    </div>
                    <div class="dvclear"></div>
                    <div>
                        <asp:Label ID="lblFechaGestion" runat="server" Text="">lblFechaGestion</asp:Label><br />
                        <asp:Label ID="txtFechaGestion" runat="server" Text="" CssClass="valor">txtFechaGestion</asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblFechaCreacion" runat="server" Text="">lblFechaCreacion</asp:Label><br />
                        <asp:Label ID="txtFechaCreacion" runat="server" Text="" CssClass="valor">txtFechaCreacion</asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblCashier" runat="server" Text="">lblCashier</asp:Label><br />
                        <asp:Label ID="txtCashier" runat="server" Text="" CssClass="valor">txtCashier</asp:Label>
                    </div>
                    <div>
                        <asp:CheckBox ID="ckbAcreditado" runat="server" Enabled="False" /><br />
                        <asp:CheckBox ID="ckbNotificado" runat="server" Enabled="False" />
                    </div>

                    <div class="dvclear"></div>

                    <div style="height: 200px; overflow: auto">
                        <asp:GridView ID="gridItem" runat="server" AutoGenerateColumns="False" BorderStyle="None" Style="width: 100%;">
                            <Columns>

                                <asp:BoundField DataField="Canal_Subcanal" ItemStyle-HorizontalAlign="Left" ItemStyle-BackColor="White" HeaderStyle-Width="200px" HeaderText="gvCanalSubcanal">
                                    <HeaderStyle Width="200px"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="Divisa" ItemStyle-HorizontalAlign="Left" ItemStyle-BackColor="White" HeaderStyle-Width="200px" HeaderText="gvDetalleDivisa">
                                    <HeaderStyle Width="200px"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Denominacion" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="White" HeaderStyle-Width="150px" HeaderText="gvDetalleDenominacion">
                                    <HeaderStyle Width="150px"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Right" BackColor="White"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Cantidad" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="White" HeaderStyle-Width="150px" HeaderText="gvDetalleCantidad">
                                    <HeaderStyle Width="150px"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Right" BackColor="White"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ImporteFormatado" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="White" HeaderStyle-Width="150px" DataFormatString="{0:N}" HeaderText="gvDetalleImporte">
                                    <HeaderStyle Width="150px"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Right" BackColor="White"></ItemStyle>
                                </asp:BoundField>

                            </Columns>
                            <EmptyDataTemplate>
                                <div style="height: auto; text-align: left; color: #767676; border-style: none; border: solid 1px #FFF; font-style: italic; font-weight: bold;">
                                    <%# "Sem Registro" %>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>

                    </div>

                    <div class="dvclear"></div>

                    <div>

                        <asp:Label ID="lblTotales" runat="server" Text="">lblTotales</asp:Label><br />
                        <asp:GridView ID="gridTotal" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="350px">
                            <Columns>

                                <asp:BoundField DataField="Canal_Subcanal" ItemStyle-HorizontalAlign="Left" ItemStyle-BackColor="White" HeaderStyle-Width="200px" HeaderText="gvCanalSubcanal">
                                    <HeaderStyle Width="200px"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="Divisa" ItemStyle-HorizontalAlign="Left" ItemStyle-BackColor="White" HeaderStyle-Width="200px" HeaderText="gvDetalleTDivisa">
                                    <HeaderStyle Width="200px"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ImporteFormatado" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="White" HeaderStyle-Width="150px" HeaderText="gvDetalleTImporte">
                                    <HeaderStyle Width="150px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" BackColor="White"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div style="height: auto; text-align: left; color: #767676; border-style: none; border: solid 1px #FFF; font-style: italic; font-weight: bold;">
                                    <%# "Sem Registro" %>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div class="dvclear"></div>




                    <div style="text-align: right; width: 100%;">
                        <%--<asp:Button ID="btnDocumento"  runat="server"  CssClass="ui-button" AutoPostBack="true" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" OnClientClick=" __doPostBack('ctl00$ContentPlaceHolder1$btnPopUp','OID')"/>--%>
                        <asp:Button ID="btnCerrar" runat="server" CssClass="ui-button"  Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
</asp:Content>

