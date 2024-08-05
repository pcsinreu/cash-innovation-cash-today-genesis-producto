Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposPuntoServicio

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboTiposPuntoServicio")> _
    <XmlRoot(Namespace:="urn:GetComboTiposPuntoServicio")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _Codigo As String

#End Region

#Region "[Propriedades]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

#End Region

    End Class

End Namespace