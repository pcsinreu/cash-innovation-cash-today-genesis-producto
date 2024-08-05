Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConsultarSaldos

    <Serializable()>
    Public Class Filtro

        Public Property IncluirMediosPago As Boolean
        Public Property SaldoDetallado As Boolean
        Public Property SaldoDisponible As Boolean
        Public Property IncluirSubSectores As Boolean

    End Class

End Namespace