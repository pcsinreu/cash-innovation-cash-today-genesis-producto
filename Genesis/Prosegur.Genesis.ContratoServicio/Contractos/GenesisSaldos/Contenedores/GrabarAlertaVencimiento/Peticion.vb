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
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property Contenedor As Contenedor

#End Region

    End Class

End Namespace