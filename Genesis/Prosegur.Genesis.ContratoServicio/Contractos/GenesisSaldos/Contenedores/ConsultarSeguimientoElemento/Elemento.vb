Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarSeguimientoElemento

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <XmlRoot(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <Serializable()> _
    Public Class Elemento

#Region "[PROPRIEDADES]"

        Public Property oidElemento As String
        Public Property codTipoElemento As String
        Public Property codPrecinto As String

#End Region

    End Class
End Namespace