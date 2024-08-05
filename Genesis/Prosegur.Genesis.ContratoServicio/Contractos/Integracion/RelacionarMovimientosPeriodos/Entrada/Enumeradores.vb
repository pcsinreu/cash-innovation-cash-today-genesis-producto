Imports System.Xml.Serialization

Namespace Contractos.Integracion.RelacionarMovimientosPeriodos.Entrada

    <XmlType(Namespace:="urn:RelacionarMovimientosPeriodos.Entrada")>
    <XmlRoot(Namespace:="urn:RelacionarMovimientosPeriodos.Entrada")>
    <Serializable()>
    Public Class Enumeradores

        Public Enum Accion
            RELACIONAR = 0
            QUITAR = 1
        End Enum

        Public Enum TipoCodigo
            SIMPLE = 0
            COMPLETO = 1
        End Enum

        Public Enum TipoPeriodo
            ACREDITACION = 0
            BOVEDA = 1
            RECOJO = 2
        End Enum



    End Class

End Namespace
