Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposPuntoServicio

    ''' <summary>
    ''' Coleção de TipoPuntoServicio
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class TipoPuntoServicioColeccion
        Inherits List(Of TipoPuntoServicio)

    End Class

End Namespace