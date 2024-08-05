Imports System.Data
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.Contractos.Genesis.Pantallas.Delegaciones

Namespace Genesis.Pantallas

    Public Class Delegaciones


        Public Shared Sub RecuperarInformacionesDelegacion(identificadorDelegacion As String,
                                                         ByRef PuntosServicios As List(Of DelegacionFacturacion),
                                                         Usuario As String)

            AccesoDatos.Genesis.Pantallas.DelegacionesPorFacturacion.RecuperarInformacionesDelegacion(identificadorDelegacion, PuntosServicios, Usuario)

        End Sub

    End Class

End Namespace

