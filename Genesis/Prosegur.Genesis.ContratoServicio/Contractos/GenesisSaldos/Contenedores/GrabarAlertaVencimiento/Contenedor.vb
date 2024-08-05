Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.GrabarAlertaVencimiento

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:GrabarAlertaVencimiento")> _
    <XmlRoot(Namespace:="urn:GrabarAlertaVencimiento")> _
    <Serializable()> _
    Public Class Contenedor

#Region "[PROPRIEDADES]"

        Public Property codPrecinto As String
        Public Property AlertaVencimento As AlertaVencimento
        Public Property UsuarioCreacion As String
#End Region

    End Class
End Namespace