<%@ Page Title="Detalle" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master" CodeBehind="IntegracionDetalle.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.IntegracionDetalle" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .WordWrap {
            /*width: 100%;*/
            word-break: break-word;
            white-space: pre-line !important
        }

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

        .textColor {
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
                <div style="position: sticky; top: 0; margin-top:18px; background-color: white;" class="dvUsarFloat">
                    <table class="tabela_campos" style="margin: 0px !important;">
                        <tr style="margin-bottom: 30px;">
                            <td style="margin-right: 60px;">
                                <div>
                                    <asp:Label ID="lblActualIdTitle" runat="server" Text="">ActualId</asp:Label>
                                    <br />
                                    <asp:Label ID="lblActualId" runat="server" CssClass="valor" Text="">lblActualId</asp:Label>
                                </div>
                            </td>
                            <td style="margin-right: 60px;">
                                <div>
                                    <asp:Label ID="lblCodigoProcesoTitle" runat="server" Text="">CodigoProceso</asp:Label>
                                    <br />
                                    <asp:Label ID="lblCodigoProceso" runat="server" CssClass="valor" Text="">lblCodigoProceso</asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:Label ID="lblEstadoTitle" runat="server" Text="">Estado</asp:Label>
                                    <br />
                                    <asp:Label ID="lblEstado" runat="server" CssClass="valor" Text="">lblEstado</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr style="margin-bottom: 30px;">
                            <td style="margin-right: 60px;">
                                <div>
                                    <asp:Label ID="lblClienteTitle" runat="server" Text="">Cliente</asp:Label>
                                    <br />
                                    <asp:Label ID="lblCliente" runat="server" CssClass="valor" Text="">lblCliente</asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:Label ID="lblSubClienteTitle" runat="server" Text="">SubCliente</asp:Label>
                                    <br />
                                    <asp:Label ID="lblSubCliente" runat="server" CssClass="valor" Text="">lblSubCliente</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr style="margin-bottom: 30px;">
                            <td style="margin-right: 60px;">
                                <div>
                                    <asp:Label ID="lblPuntoServicioTitle" runat="server" Text="">PuntoServicio</asp:Label>
                                    <br />
                                    <asp:Label ID="lblPuntoServicio" runat="server" CssClass="valor" Text="">lblPuntoServicio</asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:Label ID="lblMaquinaTitle" runat="server" Text="">Maquina</asp:Label>
                                    <br />
                                    <asp:Label ID="lblMaquina" runat="server" CssClass="valor" Text="">lblMaquina</asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>

                <div style="width: 100%;">
                    <asp:Label ID="lblMovimientos" runat="server" Text="" CssClass="textColor">lblMovimientos</asp:Label><br />
                    <asp:GridView ID="gvMovimientos" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="100%">
                        <Columns>

                            <asp:BoundField DataField="CodExterno" ItemStyle-HorizontalAlign="Left" ItemStyle-BackColor="White" HeaderText="gvCodigoExterno">
                                <HeaderStyle Width="30%"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Cod_Des_Formulario" ItemStyle-HorizontalAlign="Left" ItemStyle-BackColor="White" HeaderText="gvTipoFormulario">
                                <HeaderStyle Width="20%"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="FechaGestion" ItemStyle-HorizontalAlign="Left" ItemStyle-BackColor="White" HeaderText="gvFechaGestion">
                                <HeaderStyle Width="20%"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Canal_Subcanal" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="White" HeaderText="gvCanal">
                                <HeaderStyle Width="30%"></HeaderStyle>
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

                <div style="width: 100%; margin-top: 40px; margin-bottom: 40px">
                    <asp:Label ID="lblIntegracion" runat="server" Text="" CssClass="textColor">lblIntegracion</asp:Label><br />
                    <asp:GridView ID="gvIntegracion" runat="server" AutoGenerateColumns="False" BorderStyle="None">
                        <Columns>
                            <asp:BoundField DataField="Fecha" ItemStyle-HorizontalAlign="Left" ItemStyle-BackColor="White" HeaderText="gvFecha">
                                <HeaderStyle></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NumeroIntento" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="White" HeaderText="gvIntento">
                                <HeaderStyle></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BackColor="White"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TipoDeError" ItemStyle-HorizontalAlign="Left" ItemStyle-BackColor="White" HeaderText="gvTipoError">
                                <HeaderStyle></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Comentario" ItemStyle-CssClass="WordWrap" ItemStyle-BackColor="White" HeaderText="gvComentario">
                                <HeaderStyle></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Image ID="imgVigente" ImageUrl='<%# "~/Imagenes/" & (If(Eval("TipoDeError") = Nothing, "contain01.png", "nocontain01.png")) %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="height: auto; text-align: center; color: #767676; border-style: none; border: solid 1px #FFF; font-style: italic; font-weight: bold;">
                                <%# "Sem Registro" %>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div style="text-align: center; width: 100%; position: fixed;  bottom: 0; background-color: white;">
                    <asp:Button ID="btnCerrar" runat="server" CssClass="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 10px; text-align: center" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
</asp:Content>
