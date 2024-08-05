Imports Prosegur.Genesis.ContractoServicio

''' <summary>
''' Classe que possue todos os métodos de extension utilizados para converter uma entidade do legado para a nova entidade de unificação Genesis
''' </summary>
''' <remarks></remarks>
Public Module ConversorCliente

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente.GetClientes.Cliente) As Entidades.Cliente
        Dim entidadeUnificacion As New Entidades.Cliente()
        entidadeUnificacion.BolEnviadoSaldos = True
        entidadeUnificacion.BolTotalizadorSaldo = entidadeLegado.BolTotalizadorSaldo
        entidadeUnificacion.BolVigente = entidadeLegado.BolVigente
        entidadeUnificacion.CodCliente = entidadeLegado.CodCliente
        entidadeUnificacion.DesCliente = entidadeLegado.DesCliente
        entidadeUnificacion.FyhActualizacion = entidadeLegado.FyhActualizacion
        entidadeUnificacion.OidCliente = entidadeLegado.OidCliente
        entidadeUnificacion.OidTipoCliente = entidadeLegado.OidTipoCliente
        Return entidadeUnificacion
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As IList(Of Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente.GetClientes.Cliente)) As IList(Of Entidades.Cliente)
        Dim entidadeUnificacion As New List(Of Entidades.Cliente)()
        For Each itemLegado In entidadeLegado
            entidadeUnificacion.Add(itemLegado.GenerarEntidadUnificada())
        Next
        Return entidadeUnificacion
    End Function

End Module
