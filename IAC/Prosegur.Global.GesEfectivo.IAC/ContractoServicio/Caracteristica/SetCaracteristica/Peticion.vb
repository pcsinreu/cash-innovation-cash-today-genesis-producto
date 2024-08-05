Imports System.Xml.Serialization
Imports System.Xml

Namespace Caracteristica.SetCaracteristica

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 18/05/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetCaracteristica")> _
    <XmlRoot(Namespace:="urn:SetCaracteristica")> _
    <Serializable()> _
Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _caracteristicas As CaracteristicaColeccion
        Private _codigoUsuario As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Caracteristicas() As CaracteristicaColeccion
            Get
                Return _caracteristicas
            End Get
            Set(value As CaracteristicaColeccion)
                _caracteristicas = value
            End Set
        End Property

        Public Property CodigoUsuario() As String
            Get
                Return _codigoUsuario
            End Get
            Set(value As String)
                _codigoUsuario = value
            End Set
        End Property

#End Region
    End Class
End Namespace