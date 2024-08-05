Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboProductos

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboProductos")> _
    <XmlRoot(Namespace:="urn:GetComboProductos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _productos As ProductoColeccion

#End Region

#Region "[PROPRIEDADE]"

        Public Property Productos() As ProductoColeccion
            Get
                Return _productos
            End Get
            Set(value As ProductoColeccion)
                _productos = value
            End Set
        End Property

#End Region

    End Class
End Namespace