Imports System.Xml.Serialization
Imports System.Xml

Namespace PuntoServicio.SetPuntoServicio

    <XmlType(Namespace:="urn:SetPuntoServicio")> _
    <XmlRoot(Namespace:="urn:SetPuntoServicio")> _
    <Serializable()> _
    Public Class Peticion

        Public Property PuntoServicio As PuntoServicioColeccion
        Public Property CodigoUsuario As String
        Public Property BolBaja As Boolean
        Public Property BolEliminaCodigosAjenos As Boolean = False
        Public Sub New()
            BolEliminaCodigosAjenos = False
        End Sub

    End Class

End Namespace
