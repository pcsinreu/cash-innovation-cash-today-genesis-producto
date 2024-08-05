Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboSubcanalesByCanal

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboSubcanalesByCanal")> _
    <XmlRoot(Namespace:="urn:GetComboSubcanalesByCanal")> _
    <Serializable()> _
Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _codigo As List(Of String)

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As List(Of String)
            Get
                Return _codigo
            End Get
            Set(value As List(Of String))
                _codigo = value
            End Set
        End Property

#End Region
    End Class
End Namespace