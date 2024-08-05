Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Global.GesEfectivo.IAC.LogicaNegocio

Public Class AccionSubCliente

    Public Function BuscarSubCliente(Codigo As String) As ContractoServicio.SubCliente.GetSubClientes.SubCliente
        Return AccesoDatos.SubCliente.BuscarSubCliente(Codigo)
    End Function

End Class