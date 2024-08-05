Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Public Module ConversorCuenta
    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Cuenta) As Clases.Cuenta
        Dim entidadeUnificacion As New Clases.Cuenta
        If entidadeLegado IsNot Nothing Then
            entidadeUnificacion.Identificador = entidadeLegado.Identificador

            entidadeUnificacion.Cliente = New Clases.Cliente
            entidadeUnificacion.Cliente.Identificador = entidadeLegado.IdentificadorCliente

            entidadeUnificacion.SubCliente = New Clases.SubCliente
            entidadeUnificacion.SubCliente.Identificador = entidadeLegado.IdentificadorSubCliente

            entidadeUnificacion.PuntoServicio = New Clases.PuntoServicio
            entidadeUnificacion.PuntoServicio.Identificador = entidadeLegado.IdentificadorPuntoServicio

            entidadeUnificacion.SubCanal = New Clases.SubCanal
            entidadeUnificacion.SubCanal.Identificador = entidadeLegado.IdentificadorSubCanal

            entidadeUnificacion.Sector = New Clases.Sector
            entidadeUnificacion.Sector.Identificador = entidadeLegado.IdentificadorSector
            entidadeUnificacion.TipoCuenta = entidadeLegado.TipoCuenta
        End If

        Return entidadeUnificacion
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As IList(Of Prosegur.Genesis.ContractoServicio.GenesisSaldos.Cuenta)) As IList(Of Clases.Cuenta)
        Dim entidadeUnificacion As New ObservableCollection(Of Clases.Cuenta)()
        For Each itemLegado In entidadeLegado
            entidadeUnificacion.Add(itemLegado.GenerarEntidadUnificada())
        Next
        Return entidadeUnificacion
    End Function
End Module
