Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio

Namespace Interfaces.Dashboard
    Public Interface IDashboard
#Region "Serviços Comuns"
        Function ObtenerDivisas() As Contractos.Comon.Divisa.ObtenerDivisas.ObtenerDivisasRespuesta
        Function RecuperarCodigoIsoDivisaDefecto(Peticion As Contractos.Dashboard.RecuperarCodigoIsoDivisaDefecto.Peticion) As Contractos.Dashboard.RecuperarCodigoIsoDivisaDefecto.Respuesta
        Function ObtenerSectores(Peticion As Contractos.Comon.Sector.ObtenerSectoresPeticion) As Contractos.Comon.Sector.ObtenerSectoresRespuesta
        Function ObtenerClientes(Peticion As Contractos.Dashboard.ObtenerClientes.Peticion) As Contractos.Dashboard.ObtenerClientes.Respuesta
        Function ObtenerSectoresPorDelegacion(Peticion As ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresPorDelegacion.Peticion) As ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresPorDelegacion.Respuesta
#End Region
    End Interface

End Namespace