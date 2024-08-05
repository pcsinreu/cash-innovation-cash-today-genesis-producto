Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresPackModular

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarContenedoresPackModular")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresPackModular")> _
    <Serializable()> _
    Public Class Cliente

#Region "[PROPRIEDADES]"

        Public Property codCliente As String
        Public Property descCliente As String
        Public Property codSubcliente As String
        Public Property codPuntoServicio As String


#End Region

    End Class

End Namespace
