<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaDatosBancariosComentariosDetallesPopUp.aspx.vb" 
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaDatosBancariosComentariosDetallesPopUp"
    MasterPageFile="~/Master/MasterModal.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         document.addEventListener("DOMContentLoaded", () => {
             document.getElementById('btnGrabar').disabled = true;
             txtComentario.oninput = function () {
                 if (txtComentario.value != null && txtComentario.value.trim() != "")
                     document.getElementById('btnGrabar').disabled = false;
                 else
                     document.getElementById('btnGrabar').disabled = true;
             };
         });

     </script>
    <style type="text/css">
        .comentario {
            padding-left: 5px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="content-modal" style="margin-bottom: 40px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="margin: 10px">
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblTituloComentarioPopup" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                </div>
                <asp:Label Style="float: left;" ID="lblComentario" CssClass="ui-panel-title" runat="server"></asp:Label>
                <asp:TextBox ClientIDMode="Static" style="resize:none;" Width="70%" Height="80px" MaxLength="255" ID="txtComentario" runat="server" TextMode="MultiLine" />
                <div style="margin: 10px">
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblTituloComentariosModificaciónPopup" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                    <asp:GridView ID="gridComentariosModificacion" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" Width="100%">
                        <Columns>

                            <asp:BoundField DataField="Usuario_Modificacion" ItemStyle-BorderColor="#dbdee1" ItemStyle-BorderWidth="1px" ItemStyle-Height="21px" ItemStyle-BorderStyle="Solid"  HeaderText="Usuario Modificacion">
                                <HeaderStyle Width="30%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BackColor="White"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Fecha" ItemStyle-BorderColor="#dbdee1" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" >
                                <HeaderStyle Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BackColor="White"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Comentario" ItemStyle-BorderColor="#dbdee1" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" HeaderText="Comentarios" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="30%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" BackColor="White" CssClass="comentario"></ItemStyle>
                            </asp:BoundField>

                        </Columns>
                        <EmptyDataTemplate>
                            <div style="height: auto; text-align: center; color: #767676; border-style: none; border: solid 1px #FFF; font-style: italic; font-weight: bold;">
                                 <%# MyBase.RecuperarValorDic("lblSinRegistros") %>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                        
                </div>
                <div style="margin: 10px">
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblTituloComentariosAprobacionPopup" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                    <asp:GridView ID="gridComentariosAprobacion" runat="server" AutoGenerateColumns="False" ItemStyle-BorderColor="#dbdee1" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" Width="100%"  >
                        <Columns>

                            <asp:BoundField ItemStyle-Height="21px" DataField="Usuario_Aprobacion" HeaderText="Usuario Aprobación" ItemStyle-BorderColor="#dbdee1" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" >
                                <HeaderStyle Width="30%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BackColor="White"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-BorderColor="#dbdee1" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" >
                                <HeaderStyle Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BackColor="White"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Comentario"  HeaderText="Comentarios de aprobacion" ItemStyle-BorderColor="#dbdee1" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" >
                                <HeaderStyle Width="45%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" BackColor="White" CssClass="comentario"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="" ItemStyle-BorderColor="#dbdee1" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" >
                                <ItemTemplate>
                                    <asp:Image runat="server" 
                                        ImageUrl="~/Imagenes/contain01.png" 
                                        ID="imgEstadoAprobado" 
                                        CssClass="imgButton" 
                                        Visible='<%#If(Eval("Estado") = 1, True, False) %>'
                                        ToolTip='<%#  RecuperarValorDic("lbl_aprobado") %>'/>
                                    <asp:ImageButton runat="server" 
                                        ImageUrl="~/Imagenes/nocontain01.png" 
                                        ID="imgEstadoPendiente" 
                                        Visible='<%# If(Eval("Estado") = 0, True, False) %>'
                                        CssClass="imgButton" 
                                        ToolTip='<%#  RecuperarValorDic("lbl_rechazado") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="height: auto; text-align: left; color: #767676; border-style: none; border: solid 1px #FFF; font-style: italic; font-weight: bold;">
                                <%# MyBase.RecuperarValorDic("lblSinRegistros") %>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                         
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="text-align: center; width: 100%; position: fixed;  bottom: 0; background-color: white; ">
        <center>
        
            <table>
                <tr align="center">
                    <td>
                        <asp:Button runat="server" ID="btnGrabar" Width="100px" ClientIDMode="Static" CssClass="ui-button" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnCancelar" Width="100px" ClientIDMode="Static" CssClass="ui-button" />
                    </td>
                </tr>
            </table>
             
        </center>
    </div>
</asp:Content>