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
    Public Class Bulto

#Region "[PROPRIEDADES]"

        Public Property codPrecinto As String
        Public Property codigoBolsa As String
        Public Property tipoServicio As String
        Public Property fechaHoraCriacion As DateTime

#End Region

    End Class
End Namespace