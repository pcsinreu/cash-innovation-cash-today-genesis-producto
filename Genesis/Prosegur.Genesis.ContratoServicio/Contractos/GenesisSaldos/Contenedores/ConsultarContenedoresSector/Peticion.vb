Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresSector

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarContenedoresSector")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresSector")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property retornarElementosHijos As Boolean
        Public Property Precintos As List(Of String)
        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property Cliente As Cliente
        Public Property Sectores As List(Of Sector)
        Public Property Canal As Canal
        Public Property Contenedor As Contenedor
        Public Property CodigoUsuario As String
        Public Property RecuperarSoloContenedoresSinPosicion As Boolean

#End Region

    End Class

End Namespace