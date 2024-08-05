Imports Prosegur.Genesis.ContractoServicio

''' <summary>
''' Classe que possue todos os métodos de extension utilizados para converter uma entidade do legado para a nova entidade de unificação Genesis
''' </summary>
''' <remarks></remarks>
Public Module ConversorPuntoServicio

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio) As Entidades.PuntoServicio
        Dim entidadeUnificacion As New Entidades.PuntoServicio()
        entidadeUnificacion.BolEnviadoSaldos = True
        entidadeUnificacion.BolTotalizadorSaldo = entidadeLegado.BolTotalizadorSaldo
        entidadeUnificacion.BolVigente = entidadeLegado.BolVigente
        entidadeUnificacion.CodPtoServicio = entidadeLegado.CodPuntoServicio
        entidadeUnificacion.DesPtoServicio = entidadeLegado.DesPuntoServicio
        entidadeUnificacion.FyhActualizacion = entidadeLegado.FyhActualizacion
        entidadeUnificacion.OidPtoServicio = entidadeLegado.OidPuntoServicio
        entidadeUnificacion.OidSubcliente = entidadeLegado.OidSubCliente
        entidadeUnificacion.OidTipoPuntoServicio = entidadeLegado.OidTipoPuntoServicio
        Return entidadeUnificacion
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As IList(Of Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio)) As IList(Of Entidades.PuntoServicio)
        Dim entidadeUnificacion As New List(Of Entidades.PuntoServicio)()
        For Each itemLegado In entidadeLegado
            entidadeUnificacion.Add(itemLegado.GenerarEntidadUnificada())
        Next
        Return entidadeUnificacion
    End Function

End Module
