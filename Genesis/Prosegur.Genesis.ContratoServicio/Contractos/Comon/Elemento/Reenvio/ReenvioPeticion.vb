Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Elemento.Reenvio

    Public Enum ReenvioPeticionAcciones
        Crear
        Aceptar
        Rechazar
    End Enum

    Public NotInheritable Class ReenvioPeticion
        Inherits BasePeticion

        Property Origen As Origen
        Property Destino As Destino
        Property Accion As Reenvio.ReenvioPeticionAcciones
        Property Elementos As List(Of Elemento)
        Property Documentos As List(Of Documento)
        Property FechaHoraSolicitud As DateTime
        Property CodigoUsuario As String
        Property CantidadElementos As Integer

    End Class

End Namespace
