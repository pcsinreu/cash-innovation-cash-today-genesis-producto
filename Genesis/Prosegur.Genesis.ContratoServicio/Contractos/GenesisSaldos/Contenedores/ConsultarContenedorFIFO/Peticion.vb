Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedorFIFO

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarContenedorFIFO")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedorFIFO")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property Contenedor As Comon.Contenedor
        Public Property CodigoUsuario As String
#End Region

    End Class

End Namespace