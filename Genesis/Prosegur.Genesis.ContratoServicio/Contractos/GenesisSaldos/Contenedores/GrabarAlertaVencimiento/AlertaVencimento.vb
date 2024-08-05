Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.GrabarAlertaVencimiento

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:GrabarAlertaVencimiento")> _
    <XmlRoot(Namespace:="urn:GrabarAlertaVencimiento")> _
    <Serializable()> _
    Public Class AlertaVencimento

#Region "[PROPRIEDADES]"

        Public Property fechaAlerta As DateTime
        Public Property codTipoAlerta As String
        Public Property diasVencer As Integer

        Public Property emails As String

#End Region

    End Class

End Namespace
