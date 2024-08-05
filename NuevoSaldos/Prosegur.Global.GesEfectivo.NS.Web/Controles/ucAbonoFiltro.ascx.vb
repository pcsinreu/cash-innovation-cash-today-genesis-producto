Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon

Public Class ucAbonoFiltro
    Inherits UcBase

    Public Property Sector() As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda
        Get
            Return Me.UcSector
        End Get
        Set(value As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda)
            Me.UcSector = value
        End Set
    End Property

    Public Property Cliente() As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda
        Get
            Return Me.UcClienteFiltroInterno
        End Get
        Set(value As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda)
            Me.UcClienteFiltroInterno = value
        End Set
    End Property

    Public Property SubCliente() As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda
        Get
            Return Me.UcSubClienteFiltroInterno
        End Get
        Set(value As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda)
            Me.UcSubClienteFiltroInterno = value
        End Set
    End Property

    Public Property PtoServicio() As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda
        Get
            Return Me.UcPtoServicioFiltroInterno
        End Get
        Set(value As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda)
            Me.UcPtoServicioFiltroInterno = value
        End Set
    End Property

    Public Property Canal() As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda
        Get
            Return Me.UcCanalFiltroInterno
        End Get
        Set(value As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda)
            Me.UcCanalFiltroInterno = value
        End Set
    End Property

    Public Property SubCanal() As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda
        Get
            Return Me.UcSubCanalFiltroInterno
        End Get
        Set(value As Global.Prosegur.[Global].GesEfectivo.NuevoSaldos.Web.ucBusqueda)
            Me.UcSubCanalFiltroInterno = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Base.InformacionUsuario IsNot Nothing AndAlso Base.InformacionUsuario.SectorSeleccionado IsNot Nothing AndAlso Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
            litAux.Text = "<input type='hidden' id='txtBusquedaSeleccionados_Delegacion' value='[{" & Chr(34) & "Identificador" & Chr(34) & ":" & Chr(34) & Base.InformacionUsuario.DelegacionSeleccionada.Identificador & Chr(34) & "," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & Base.InformacionUsuario.DelegacionSeleccionada.Codigo & Chr(34) & "," & Chr(34) & "Descripcion" & Chr(34) & ":" & Chr(34) & Base.InformacionUsuario.DelegacionSeleccionada.Descripcion & Chr(34) & "}]' />"
            litAux.Text &= "<input type='hidden' id='txtBusquedaSeleccionados_Planta' value='[{" & Chr(34) & "Identificador" & Chr(34) & ":" & Chr(34) & Base.InformacionUsuario.SectorSeleccionado.Planta.Identificador & Chr(34) & "," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & Base.InformacionUsuario.SectorSeleccionado.Planta.Codigo & Chr(34) & "," & Chr(34) & "Descripcion" & Chr(34) & ":" & Chr(34) & Base.InformacionUsuario.SectorSeleccionado.Planta.Descripcion & Chr(34) & "}]' />"
        End If

#If DEBUG Then
        litAux.Text &= "<input type='hidden' id='txtBusquedaSeleccionados_Delegacion' value='[{" & Chr(34) & "Identificador" & Chr(34) & ":" & Chr(34) & "BF52A95E193DA5B3E040360A65F87103" & Chr(34) & "," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & "01" & Chr(34) & "," & Chr(34) & "Descripcion" & Chr(34) & ":" & Chr(34) & "Nazca" & Chr(34) & "}]' />"
        litAux.Text &= "<input type='hidden' id='txtBusquedaSeleccionados_Planta' value='[{" & Chr(34) & "Identificador" & Chr(34) & ":" & Chr(34) & "a5ecc761-1514-482b-ba6c-4dbf4ff0c61d" & Chr(34) & "," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & "01" & Chr(34) & "," & Chr(34) & "Descripcion" & Chr(34) & ":" & Chr(34) & "Nazca" & Chr(34) & "}]' />"
#End If

        configurarControles()

    End Sub

    Private Sub configurarControles()

        ' Sector
        Me.UcSector.Tipo = Enumeradores.TipoBusqueda.Sector
        Me.UcSector.Titulo = Traduzir("071_Comon_Campo_Sector")
        Me.UcSector.EsMulti = True
        Me.UcSector.VisibilidadInicial = True

        ' Cliente
        Me.UcClienteFiltroInterno.Tipo = Enumeradores.TipoBusqueda.Cliente
        'Me.UcClienteFiltroInterno.Titulo = Traduzir("071_Comon_Campo_Cliente")
        Me.UcClienteFiltroInterno.EsMulti = False
        Me.UcClienteFiltroInterno.VisibilidadInicial = True

        ' SubCliente
        Me.UcSubClienteFiltroInterno.Tipo = Enumeradores.TipoBusqueda.SubCliente
        Me.UcSubClienteFiltroInterno.Titulo = Traduzir("071_Comon_Campo_SubCliente")
        Me.UcSubClienteFiltroInterno.EsMulti = False
        Me.UcSubClienteFiltroInterno.VisibilidadInicial = False

        ' PtoServicio
        Me.UcPtoServicioFiltroInterno.Tipo = Enumeradores.TipoBusqueda.PtoServicio
        Me.UcPtoServicioFiltroInterno.Titulo = Traduzir("071_Comon_Campo_PtoServicio")
        Me.UcPtoServicioFiltroInterno.EsMulti = False
        Me.UcPtoServicioFiltroInterno.VisibilidadInicial = False

        ' Canal
        Me.UcCanalFiltroInterno.Tipo = Enumeradores.TipoBusqueda.Canal
        Me.UcCanalFiltroInterno.Titulo = Traduzir("071_Comon_Campo_Canal")
        Me.UcCanalFiltroInterno.EsMulti = False
        Me.UcCanalFiltroInterno.VisibilidadInicial = True

        ' SubCanal
        Me.UcSubCanalFiltroInterno.Tipo = Enumeradores.TipoBusqueda.SubCanal
        Me.UcSubCanalFiltroInterno.Titulo = Traduzir("071_Comon_Campo_SubCanal")
        Me.UcSubCanalFiltroInterno.EsMulti = False
        Me.UcSubCanalFiltroInterno.VisibilidadInicial = False

    End Sub

End Class