Imports System.Xml.Serialization
Imports System.Xml

Namespace Direccion.SetDirecciones

    <XmlType(Namespace:="urn:SetDirecciones")> _
    <XmlRoot(Namespace:="urn:SetDirecciones")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _bolBaja As Boolean
        Private _codTipoTablaGenesis As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property bolBaja() As Boolean
            Get
                Return _bolBaja
            End Get
            Set(value As Boolean)
                _bolBaja = value
            End Set

        End Property

        Public Property codTipoTablaGenesis() As String
            Get
                Return _codTipoTablaGenesis
            End Get
            Set(value As String)
                _codTipoTablaGenesis = value
            End Set

        End Property

#End Region

    End Class
End Namespace

