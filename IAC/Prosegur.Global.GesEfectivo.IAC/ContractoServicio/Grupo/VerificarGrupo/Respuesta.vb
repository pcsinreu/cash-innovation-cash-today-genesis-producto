Imports System.Xml.Serialization
Imports System.Xml

Namespace Grupo.VerificarGrupo

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarGrupo")> _
    <XmlRoot(Namespace:="urn:VerificarGrupo")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _morfologia As Morfologia

#End Region

#Region "[Propriedades]"

        Public Property Morfologia() As Morfologia
            Get
                Return _morfologia
            End Get
            Set(value As Morfologia)
                _morfologia = value
            End Set
        End Property

#End Region

    End Class

End Namespace