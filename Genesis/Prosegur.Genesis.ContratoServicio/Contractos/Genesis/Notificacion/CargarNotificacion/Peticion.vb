Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Notificacion.CargarNotificacion

    <XmlType(Namespace:="urn:CargarNotificacion")> _
    <XmlRoot(Namespace:="urn:CargarNotificacion")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property desde As DateTime
        Public Property hasta As DateTime
        Public Property desLogin As String = Nothing
        Public Property codigosSector As List(Of String)
        Public Property codigosPlanta As List(Of String)
        Public Property codigosDelegacion As List(Of String)
        Public Property identificadoresTipoSector As List(Of String)
        Public Property actualDelegacion As Clases.Delegacion
        Public Property leidas As Nullable(Of Boolean)
        ''' <summary>
        ''' Busca os identificadores de delegações,plantas, tipos setores e setores
        ''' necessários para identificar as privacidades das notificações
        ''' carregar apenas uma vez e guardar em memória
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property obtenerIdentificadores As Boolean
        Public Property codigoAplicacion As String

    End Class

End Namespace