Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Newtonsoft.Json

Public Class ucBusquedaAvanzada
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        litDiccionario.Text = "<script> _legendaFirst = '" + Traduzir("071_Comon_Campo_First") + "'; _legendaLast = '" + Traduzir("071_Comon_Campo_Last") + "';  </script>"
        Dim _SeleccionadosBusquedaAvanzada As New List(Of Clases.Abono.AbonoInformacion)
        Dim _AbonoInformacion As New Clases.Abono.AbonoInformacion
        litScript.Text = "<script> var _SeleccionadosBusquedaAvanzada = JSON.parse('" & JsonConvert.SerializeObject(_SeleccionadosBusquedaAvanzada) & "'); var _AbonoInformacion  = JSON.parse('" & JsonConvert.SerializeObject(_AbonoInformacion) & "'); </script>"
    End Sub

End Class