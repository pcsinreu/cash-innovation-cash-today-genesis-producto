Imports System.Collections.ObjectModel

Namespace Clases

    <Serializable()>
    Public NotInheritable Class Ruta
        Inherits BaseClase

        Public Property CodigoRuta As String
        Public Property DescripcionRuta As String
        Public Property FechaRuta As DateTime
        Public Property EstadoRuta As Integer
        Public Property DescripcionEstadoRuta As String
        Public Property CodigoDelegacion As String
        Public Property CodigoPais As String
        Public Property EsOrigenProsegur As Boolean
        Public Property PortaValorMatricula() As String
        Public Property PortaValorDocumentoDNI() As String
        Public Property NombreApellidosPortaValor() As String

        ' Navegações Entity Framework
        Public Property OrdenesTrabajo As observablecollection(Of OrdenTrabajo)

    End Class

End Namespace