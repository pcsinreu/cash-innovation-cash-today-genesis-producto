<Serializable()> _
Public Class ValidacionEjecucionAux

    Public Property Delegaciones As IAC.ContractoServicio.Delegacion.DelegacionColeccion

    Public Property Sectores As IAC.ContractoServicio.Setor.GetSectores.SetorColeccion

    Public Property Subcanales As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion

    Public Property DescricaoCliente As String

    Public Property CodigosUltimosCertificados As List(Of String)

End Class
