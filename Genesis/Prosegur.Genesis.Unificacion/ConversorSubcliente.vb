Imports Prosegur.Genesis.ContractoServicio

''' <summary>
''' Classe que possue todos os métodos de extension utilizados para converter uma entidade do legado para a nova entidade de unificação Genesis
''' </summary>
''' <remarks></remarks>
Public Module ConversorSubcliente

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientes.SubCliente) As Entidades.Subcliente
        Dim entidadeUnificacion As New Entidades.Subcliente()
        entidadeUnificacion.BolEnviadoSaldos = True
        entidadeUnificacion.BolTotalizadorSaldo = entidadeLegado.BolTotalizadorSaldo
        entidadeUnificacion.BolVigente = entidadeLegado.BolVigente
        entidadeUnificacion.CodSubcliente = entidadeLegado.CodSubCliente
        entidadeUnificacion.DesSubcliente = entidadeLegado.DesSubCliente
        entidadeUnificacion.FyhActualizacion = entidadeLegado.FyhActualizacion
        entidadeUnificacion.OidCliente = entidadeLegado.OidCliente
        entidadeUnificacion.OidSubcliente = entidadeLegado.OidSubCliente
        entidadeUnificacion.OidTipoSubcliente = entidadeLegado.OidTipoSubCliente
        Return entidadeUnificacion
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As IList(Of Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientes.SubCliente)) As IList(Of Entidades.Subcliente)
        Dim entidadeUnificacion As New List(Of Entidades.Subcliente)()
        For Each itemLegado In entidadeLegado
            entidadeUnificacion.Add(itemLegado.GenerarEntidadUnificada())
        Next
        Return entidadeUnificacion
    End Function

End Module
