Imports System.Xml.Serialization
Imports System.Xml

Namespace Morfologia.VerificarMorfologia

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 29/12/2010 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarMorfologia")> _
    <XmlRoot(Namespace:="urn:VerificarMorfologia")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _codigoMorfologia As String
        Private _descripcionMorfologia As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoMorfologia() As String
            Get
                Return _codigoMorfologia
            End Get
            Set(value As String)
                _codigoMorfologia = value
            End Set
        End Property

        Public Property DescripcionMorfologia() As String
            Get
                Return _descripcionMorfologia
            End Get
            Set(value As String)
                _descripcionMorfologia = value
            End Set
        End Property

#End Region

    End Class

End Namespace