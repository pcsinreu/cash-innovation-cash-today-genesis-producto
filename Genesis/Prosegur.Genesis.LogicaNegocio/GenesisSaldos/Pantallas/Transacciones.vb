Imports System.Data
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarTransacciones

Namespace GenesisSaldos.Pantallas

    Public Class Transacciones

        Public Shared Sub RecuperarInformaciones(ByRef Delegaciones As Dictionary(Of String, String),
                                                 ByRef Canales As Dictionary(Of String, String),
                                                 ByRef Tipo_Planificaciones As Dictionary(Of String, String),
                                                 ByRef Tipo_Transacciones As Dictionary(Of String, String),
                                                 ByVal usuario As String)

            AccesoDatos.GenesisSaldos.Pantallas.Transacciones.RecuperarInformaciones(Delegaciones, Canales, Tipo_Planificaciones, Tipo_Transacciones, usuario)
        End Sub

        Public Shared Sub RecuperarInformacionesDinamico(ByRef Maquinas As Dictionary(Of String, String),
                                                         ByRef PuntosServicios As List(Of PtoServicio),
                                                         ByRef Planificaciones As Dictionary(Of String, String),
                                                         Peticion As Peticion,
                                                         Usuario As String)

            AccesoDatos.GenesisSaldos.Pantallas.Transacciones.RecuperarInformacionesDinamico(Maquinas, PuntosServicios, Planificaciones, Peticion, Usuario)

        End Sub

    End Class

End Namespace

