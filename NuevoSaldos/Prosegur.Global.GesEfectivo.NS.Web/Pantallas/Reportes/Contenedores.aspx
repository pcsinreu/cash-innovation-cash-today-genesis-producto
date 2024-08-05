<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="Contenedores.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Contenedores" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="~/Controles/ucFiltroDivisas.ascx" TagName="ucFiltroDivisas" TagPrefix="ns" %>
<%@ Register Src="~/Controles/ucRadioButtonList.ascx" TagName="ucRadioButtonList" TagPrefix="ns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("../../js/Pantallas/Reportes/Contenedores.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .search-box {
            width: 80%;
            border: 1px solid #ccc;
            outline: 0;
            border-radius: 15px;
            background-color: #d9d5d5;
            
        }
        .close-icon {
            border: 1px solid transparent;
            background-color: transparent;
            display: inline-block;
            vertical-align: middle;
            outline: 0;
            cursor: pointer;
        }

            .close-icon:before {
                content: "X";
                display: block;
                width: 10px;
                height: 10px;
                z-index: 1;
                right: 0px;
                top: 0;
                bottom: 0;
                margin-left: -22px;
                padding-bottom: 5px;
                border-radius: 50%;
                text-align: center;
                color: black;
                font-weight: normal;
                font-size: 12px;
                cursor: pointer;
            }
    </style>
    <div class="content">
        <div class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblSubTitulo" runat="server" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
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
        <asp:UpdatePanel ID="upTipoContenedor" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="dvUsarFloat">
                    <div>
                        <asp:PlaceHolder runat="server" ID="phTipoContenedor"></asp:PlaceHolder>
                    </div>
                    <div style="height: 20px; color: #5e5e5e;">
                        <asp:Label ID="lblFechaArmadoDesde" runat="server" Text="Fecha/Hora Armado Desde" />
                        <br />
                        <asp:TextBox ID="txtFechaArmadoDesde" SkinID="filter-textbox" runat="server" Height="10px" Width="118px" />
                        <br />
                    </div>
                    <div style="height: 20px; color: #5e5e5e;">
                        <asp:Label ID="lblFechaArmadoHasta" runat="server" Text="Fecha/Hora Armado Hasta" />
                        <br />
                        <asp:TextBox ID="txtFechaArmadoHasta" SkinID="filter-textbox" runat="server" Height="10px" Width="118px" />
                        <br />
                    </div>                    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="dvclear">
        </div>
        <div class="dvclear">
        </div>
        <div class="dvUsarFloat">
            <div>
                <b>
                    <asp:CheckBox runat="server" ID="chkContenedoresPackModular" Text="Contenedores Pack Modular" /></b>
                <br />
                <br />
                <b>
                    <asp:CheckBox runat="server" ID="chkNoConsiderarHijos" Text="No considerar sectores hijos" /></b>
            </div>
            <div>
                <b>
                    <ns:ucRadioButtonList ID="ucDiscriminarPor" runat="server" Titulo="Discriminar por" />
                </b>
                <br />
                <br />
                <br />
                <b>
                    <ns:ucRadioButtonList ID="ucFormato" runat="server" />
                </b>
            </div>
            <div style="width: 145px; margin-left:10px;">
                <asp:Label ID="lblEstado" runat="server"></asp:Label><br />
                <asp:CheckBoxList ID="chkListEstados" SelectionMode="Multiple" runat="server" Width="130px" BorderColor="#a8a8a8" BorderWidth="1px"></asp:CheckBoxList>
            </div>
            <div style="width: 103px; padding-left: 10px; height: auto; color: #5e5e5e;">
                <asp:Label ID="lblPrecinto" runat="server" Text="Fecha/Hora Armado Hasta" />
                <br />
                <input id="txtPrecinto" type="text" class="filter-textbox" style="height:10px; width:118px" />
                <br />
            </div>
            <div runat="server" style="display: block; margin: 12px 2px 0px 2px; height: auto; padding-top: 5px">
                <input type="image" id="ImageButtonAdicionar" src="../../Imagenes/icon-add-green.png" class="butondefectoPequeno" onclick="return insertarPrecinto()">
            </div>
            <div id="dvPrecintos" style="padding-top: 10px; padding-left: 30px; max-width:600px; height:auto">                      
            </div>
        <input  type='hidden' id='hdfprecinto' name='hdfprecinto'/>     
        <div class="dvclear">
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="text-align: center">
        <ns:Boton ID="btnGenerarReporte" runat="server" Enabled="true" Text="F4 Grabar" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png"
            TeclaAtalho="F4" />        
    </div>
</asp:Content>

