Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTerminosIAC

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboTerminosIAC")> _
    <XmlRoot(Namespace:="urn:GetComboTerminosIAC")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _EsVigente As Boolean

#End Region

#Region " Propriedades "

        Public Property EsVigente() As Boolean
            Get
                Return _EsVigente
            End Get
            Set(value As Boolean)
                _EsVigente = value
            End Set
        End Property

#End Region

    End Class

End Namespace