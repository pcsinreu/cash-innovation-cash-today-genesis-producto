Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.Comon

Namespace Genesis

    Public Class TipoCliente

        Public Shared Function RecuperarTipoCliente(CodigoTipoCliente As String) As Comon.Clases.TipoCliente
            Return AccesoDatos.Genesis.TipoCliente.RecuperarTipoCliente(CodigoTipoCliente)
        End Function

    End Class

End Namespace