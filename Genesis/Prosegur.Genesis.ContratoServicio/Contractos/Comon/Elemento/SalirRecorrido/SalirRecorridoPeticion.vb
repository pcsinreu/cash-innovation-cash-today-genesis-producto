Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Elemento.SalirRecorrido

    Public NotInheritable Class SalirRecorridoPeticion
        Inherits BasePeticion

        Property FechaHoraSolicitud As DateTime
        Property CodigoUsuario As String
        Property CodigoRuta As String
        Property CantidadElementos As Integer
        Property Origen As Origen

    End Class

End Namespace
