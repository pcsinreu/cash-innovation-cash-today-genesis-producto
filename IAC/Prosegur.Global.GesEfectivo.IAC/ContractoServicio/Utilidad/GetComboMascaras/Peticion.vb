Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboMascaras

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboMascaras")> _
    <XmlRoot(Namespace:="urn:GetComboMascaras")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _AplicaTerminosMediosPago As Nullable(Of Boolean)
        Private _AplicaTerminosIac As Nullable(Of Boolean)

#End Region

#Region " Propriedades "

        Public Property AplicaTerminosMediosPago() As Nullable(Of Boolean)
            Get
                Return _AplicaTerminosMediosPago
            End Get
            Set(value As Nullable(Of Boolean))
                _AplicaTerminosMediosPago = value
            End Set
        End Property

        Public Property AplicaTerminosIac() As Nullable(Of Boolean)
            Get
                Return _AplicaTerminosIac
            End Get
            Set(value As Nullable(Of Boolean))
                _AplicaTerminosIac = value
            End Set
        End Property

#End Region

    End Class

End Namespace