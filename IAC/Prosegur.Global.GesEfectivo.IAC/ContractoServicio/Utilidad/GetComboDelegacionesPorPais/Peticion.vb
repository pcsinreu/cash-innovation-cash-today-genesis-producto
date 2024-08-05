Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboDelegacionesPorPais

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 10/04/2012 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboDelegacionesPorPais")> _
    <XmlRoot(Namespace:="urn:GetComboDelegacionesPorPais")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _codDelegacion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodDelegacion() As String
            Get
                Return _codDelegacion
            End Get
            Set(value As String)
                _codDelegacion = value
            End Set
        End Property

#End Region
    End Class
End Namespace