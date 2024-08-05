Imports System.Xml.Serialization
Imports System.Xml

Namespace Procedencia.SetProcedencia
    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pagoncalves] 13/05/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetCanal")> _
    <XmlRoot(Namespace:="urn:SetCanal")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"
        Private _Procedencia As Procedencia
#End Region

#Region "Propriedades"

        Public Property Procedencia As Procedencia
            Get
                Return _Procedencia
            End Get
            Set(value As Procedencia)
                _Procedencia = value
            End Set
        End Property

#End Region

    End Class

End Namespace