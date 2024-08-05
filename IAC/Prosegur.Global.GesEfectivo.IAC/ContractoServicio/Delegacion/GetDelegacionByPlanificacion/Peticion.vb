Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.GetDelegacionByPlanificacion

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Claudioniz.Pereira] 06/06/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDelegacionByPlanificacion")>
    <XmlRoot(Namespace:="urn:GetDelegacionByPlanificacion")>
    <Serializable()>
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _OID_PLANIFICACION As String

#End Region

#Region "[PROPRIEDADE]"

        Public Property OID_PLANIFICACION As String
            Get
                Return _OID_PLANIFICACION
            End Get
            Set(value As String)
                _OID_PLANIFICACION = value
            End Set
        End Property
#End Region

    End Class
End Namespace
