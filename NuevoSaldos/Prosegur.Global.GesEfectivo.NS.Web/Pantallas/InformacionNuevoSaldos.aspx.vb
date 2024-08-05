Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones


Public Class InformacionNuevoSaldos
    Inherits Base    

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Permite definir parametros da base antes de validar acessos e chamar o método inicializar.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        Try
            MyBase.DefinirParametrosBase()            
            MyBase.ValidarAcesso = False
            MyBase.ValidarPemissaoAD = False
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()
        Try
            MyBase.TraduzirControles()
            Master.Titulo = String.Format(Traduzir("InformacionNuevoSaldos_Titulo"), Genesis.Comon.Util.VersionPantallas(System.Reflection.Assembly.GetExecutingAssembly))
            lblTitulo.Text = String.Format(Traduzir("InformacionNuevoSaldos_Titulo"), Genesis.Comon.Util.VersionPantallas(System.Reflection.Assembly.GetExecutingAssembly))
            btnExportar.Text = Traduzir("InformacionNuevoSaldos_btnExportar")
            Dim objDiccionario As Dictionary(Of String, String) = New Dictionary(Of String, String)
            objDiccionario.Add("InformacionGenesis_grid_LastWrite", MyBase.RecuperarValorDic("InformacionGenesis_grid_LastWrite"))

            informacionGenesis.Diccionario = objDiccionario
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Protected Overrides Sub Inicializar()
        Try
            MyBase.Inicializar()
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Sobre")
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region
    
    Protected Sub btnExportar_Click()
        Me.informacionGenesis.Exportar(Genesis.Comon.Enumeradores.Aplicacion.GenesisNuevoSaldos.RecuperarValor(),
                                        InformacionUsuario.Nombre,
                                        InformacionUsuario.DelegacionSeleccionada.Codigo,
                                        url:=HttpContext.Current.Request.Url.AbsoluteUri)
        'InformacionUsuario.SectorSeleccionado.Codigo,
    End Sub
End Class