Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class Canal

        Shared Function ObtenerCanalJSON(codigo As String, descripcion As String) As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Return AccesoDatos.Genesis.Canal.ObtenerCanalJSON(codigo, descripcion)
        End Function

        Shared Function ObtenerCanalPatronJSON(codigo As String)
            Return AccesoDatos.Genesis.Canal.ObtenerCanalPatronJSON(codigo)
        End Function

    End Class

End Namespace
