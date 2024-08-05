Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.GetCodigoDelegacion

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 18/05/2012 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetCodigoDelegacion")> _
    <XmlRoot(Namespace:="urn:GetCodigoDelegacion")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoAplicacion As String
        Private _HostPuesto As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property

        Public Property HostPuesto() As String
            Get
                Return _HostPuesto
            End Get
            Set(value As String)
                _HostPuesto = value
            End Set
        End Property

#End Region

    End Class

End Namespace