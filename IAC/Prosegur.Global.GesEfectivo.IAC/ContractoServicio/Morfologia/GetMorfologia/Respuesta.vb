Imports System.Xml.Serialization
Imports System.Xml

Namespace Morfologia.GetMorfologia

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/12/2010 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetMorfologia")> _
    <XmlRoot(Namespace:="urn:GetMorfologia")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _morfologias As List(Of Morfologia)

#End Region

#Region "[Propriedades]"

        Public Property Morfologias() As List(Of Morfologia)
            Get
                Return _morfologias
            End Get
            Set(value As List(Of Morfologia))
                _morfologias = value
            End Set
        End Property

#End Region

    End Class

End Namespace