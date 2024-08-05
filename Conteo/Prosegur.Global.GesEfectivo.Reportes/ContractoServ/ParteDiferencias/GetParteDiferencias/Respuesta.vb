Imports System.Xml.Serialization
Imports System.Xml

Namespace ParteDiferencias.GetParteDiferencias

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:ParteDiferencias")> _
    <XmlRoot(Namespace:="urn:ParteDiferencias")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _PartesDiferencias As List(Of ParteDiferencias)

#End Region

#Region "Propriedades"

        Public Property PartesDiferencias() As List(Of ParteDiferencias)
            Get
                Return _PartesDiferencias
            End Get
            Set(value As List(Of ParteDiferencias))
                _PartesDiferencias = value
            End Set
        End Property

#End Region

    End Class

End Namespace