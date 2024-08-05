Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposMedioPagoByDivisa

    ''' <summary>
    ''' Coleção de TipoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class TipoMedioPagoColeccion
        Inherits List(Of TipoMedioPago)

    End Class

End Namespace