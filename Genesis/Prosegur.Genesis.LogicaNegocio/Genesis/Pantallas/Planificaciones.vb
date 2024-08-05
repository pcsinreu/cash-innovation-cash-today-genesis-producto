Imports System.Data
Imports System.Text
'Imports Prosegur.Genesis.ContractoServicio.Contractos.Genesis.Pantallas.Delegaciones

Namespace Genesis.Pantallas

    Public Class Planificaciones


        Public Shared Sub RecuperarBancoTesoreriaEComission(Peticion As Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Peticion,
                                                           ByRef respuesta As Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Respuesta)

            AccesoDatos.Genesis.Maquina.RecuperarBancoTesoreriaEComission(Peticion, respuesta)

        End Sub

    End Class

End Namespace

