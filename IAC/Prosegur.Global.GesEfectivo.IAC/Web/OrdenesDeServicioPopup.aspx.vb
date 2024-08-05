Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Paginacion
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis.ContractoServicio

Public Class OrdenesDeServicioPopup
    Inherits Base

#Region "[Propiedades]"

    Private Property ordenesServiciodetNotificacionesGrid As List(Of Comon.Clases.OrdenServicioDetNotificacion)
        Get
            Return Session("ordenesServicioDetNotificacionesGrid")
        End Get
        Set(value As List(Of Comon.Clases.OrdenServicioDetNotificacion))
            Session("ordenesServicioDetNotificacionesGrid") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.ORDENES_SERVICIO
        MyBase.ValidarAcao = False
        MyBase.ValidarPemissaoAD = False
    End Sub
    Protected Overrides Sub Inicializar()

        Try

            ASPxGridView.RegisterBaseScript(Page)

            Dim oidIntegracion As String = Request.QueryString("OidIntegracion")
            If Not Page.IsPostBack Then

                Dim pPeticionN As New Contractos.Integracion.RecuperarDetNotificacionesOrdenesServicio.Peticion With {
                    .Oid_integracion = oidIntegracion
                }
                ordenesServiciodetNotificacionesGrid = New LogicaNegocio.AccionOrdenServicio().GetOrdenesServicioDetNotificaciones(pPeticionN)
                gridNotificacionesDet.DataSource = ordenesServiciodetNotificacionesGrid
                gridNotificacionesDet.DataBind()

            End If


        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()
        CodFuncionalidad = "ORDENES_SERVICIO"
        CarregaDicinario()

        lblSubTitulo.Text = MyBase.RecuperarValorDic("lblSubtituloDetalles")

        'grilla detalles notificaciones
        gridNotificacionesDet.Columns(1).Caption = MyBase.RecuperarValorDic("lblColNroDeIntento")
        gridNotificacionesDet.Columns(2).Caption = MyBase.RecuperarValorDic("lblColumnaFecha")
        gridNotificacionesDet.Columns(3).Caption = MyBase.RecuperarValorDic("lblEstado")
        gridNotificacionesDet.Columns(4).Caption = MyBase.RecuperarValorDic("lblColObservaciones")
        gridNotificacionesDet.Columns(5).Caption = MyBase.RecuperarValorDic("lblColError")

    End Sub

#End Region
    Protected Sub gridNotificacionesDet_PreRender(sender As Object, e As EventArgs) Handles gridNotificacionesDet.PreRender
        If Page.IsPostBack Then
            gridNotificacionesDet.FocusedRowIndex = -1
        End If
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        gridNotificacionesDet.DataSource = ordenesServiciodetNotificacionesGrid
        If Not Page.IsPostBack Then
            gridNotificacionesDet.DataBind()
        End If
    End Sub
End Class