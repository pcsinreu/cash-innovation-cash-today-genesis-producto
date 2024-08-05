Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.getComboAplicaciones

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:getComboAplicaciones")> _
    <XmlRoot(Namespace:="urn:getComboAplicaciones")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _Permisos As List(Of String)

#End Region

#Region " Propriedades "
        Public Property Permisos() As List(Of String)
            Get
                Return _Permisos
            End Get
            Set(value As List(Of String))
                _Permisos = value
            End Set
        End Property
#End Region

    End Class

End Namespace