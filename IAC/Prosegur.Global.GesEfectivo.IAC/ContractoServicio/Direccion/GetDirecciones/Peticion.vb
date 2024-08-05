Imports System.Xml.Serialization
Imports System.Xml

Namespace Direccion.GetDirecciones

    ''' Peticon do Serviço de GetDirecciones
    ''' Criado em 24/04/2012
    <XmlType(Namespace:="urn:GetDirecciones")> _
    <XmlRoot(Namespace:="urn:GetDirecciones")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _codTipoTablaGenesis As String
        Private _oidTablaGenesis As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property codTipoTablaGenesis() As String
            Get
                Return _codTipoTablaGenesis
            End Get
            Set(value As String)
                _codTipoTablaGenesis = value
            End Set
        End Property

        Public Property oidTablaGenesis() As String
            Get
                Return _oidTablaGenesis
            End Get
            Set(value As String)
                _oidTablaGenesis = value
            End Set
        End Property

#End Region

    End Class
End Namespace

