Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper
Imports System.Xml.Serialization

Namespace Clases

    <Serializable()>
    Public Class ResultadoReporte

        Public Property CodigoEstado As String
        Public Property DescripcionErro As String
        Public Property FechaInicioEjecucion As DateTime
        Public Property FechaFinEjecucion As DateTime

    End Class

End Namespace