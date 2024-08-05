Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresPackModular

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarContenedoresPackModular")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresPackModular")> _
    <Serializable()> _
    Public Class AlertaVencimento

#Region "[PROPRIEDADES]"

        Public Property fechaAlerta As DateTime
        Public Property codTipoAlerta As String
        Public Property diasAlerta As Integer
        Public Property diasVencer As Integer
        Public Property emails As String

#End Region

    End Class
End Namespace