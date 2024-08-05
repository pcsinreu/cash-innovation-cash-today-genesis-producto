Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Reporte.GenerarInforme

    <Serializable()>
    Public Class Reporte

        Public Property CodigoDelegacion As String
        Public Property IdentificadorConfiguracionReporte As String
        Public Property TipoReporte As Enumeradores.TipoReporte
        Public Property ParametrosReporte As ObservableCollection(Of Clases.ParametroReporte)

    End Class

End Namespace