Imports System.Xml.Serialization
Imports System.Xml

Namespace TerminoIac.GetTerminoDetailIac

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 13/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetTerminoDetailIac")> _
    <XmlRoot(Namespace:="urn:GetTerminoDetailIac")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoTermino As List(Of String)

#End Region

#Region "[Propriedades]"

        Public Property CodigoTermino() As List(Of String)
            Get
                Return _CodigoTermino
            End Get
            Set(value As List(Of String))
                _CodigoTermino = value
            End Set
        End Property


#End Region

    End Class

End Namespace