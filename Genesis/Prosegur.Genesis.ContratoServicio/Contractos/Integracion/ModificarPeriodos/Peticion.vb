Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.ContratoBase

Namespace Contractos.Integracion.ModificarPeriodos
    Public Class Peticion
        ''' <summary>
        ''' Configuracion.
        ''' </summary>
        ''' <returns></returns>
        Public Property Configuracion As Configuracion
        ''' <summary>
        ''' Accion
        ''' </summary>
        ''' <returns></returns>
        Public Property Accion As Comon.Enumeradores.AccionesModificarPeriodo
        ''' <summary>
        ''' Colección de periodos.
        ''' </summary>
        ''' <returns></returns>
        Public Property Periodos As List(Of Entrada.Periodo)
    End Class
End Namespace

