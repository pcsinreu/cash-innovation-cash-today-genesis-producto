Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposMedioPago

    ''' <summary>
    ''' Coleção de TipoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class TipoMedioPagoColeccion
        Inherits List(Of TipoMedioPago)

    End Class

End Namespace