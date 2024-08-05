Imports System.ComponentModel
Imports System.Xml.Serialization
Namespace Contractos.Integracion.ModificarMovimientos.Entrada


    <XmlType(Namespace:="urn:ModificarMovimientos.Entrada")>
    <XmlRoot(Namespace:="urn:ModificarMovimientos.Entrada")>
    <Serializable()>
    Public Class Movimiento
        ''' <summary>
        ''' Codigo Externo del movimiento a ser modificado
        ''' </summary>
        <XmlAttribute(DataType:="string", AttributeName:="CodigoExterno")>
        <DefaultValue("")>
        Public Property CodigoExterno As String
        ''' <summary>
        ''' Contiene una coleccion de campos y valores CampoExtra.
        ''' </summary>
        Public Property CamposExtra As List(Of CampoExtra)

    End Class
End Namespace