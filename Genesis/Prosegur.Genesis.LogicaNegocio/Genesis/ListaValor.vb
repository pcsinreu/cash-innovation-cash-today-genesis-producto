Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports System.Data

Namespace Genesis

    Public Class ListaValor

        Public Shared Function ObtenerValor(Tipo As Comon.Enumeradores.Tipos, _
                                            IdentificadorRemesa As String, _
                                            IdentificadorBulto As String, _
                                            IdentificadorParcial As String) As String
            Return AccesoDatos.Genesis.ListaValor.ObtenerValor(Tipo, IdentificadorRemesa, IdentificadorBulto, IdentificadorParcial)
        End Function

    End Class

End Namespace
