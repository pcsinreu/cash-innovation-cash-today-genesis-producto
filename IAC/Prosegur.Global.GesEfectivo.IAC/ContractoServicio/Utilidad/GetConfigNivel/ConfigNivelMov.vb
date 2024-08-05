Imports System.Xml.Serialization

Namespace Utilidad.GetConfigNivel

    ''' <summary>
    ''' Coleção de CofigNivelMov
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 20/05/2013 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:GetConfigNivel")> _
    <XmlRoot(Namespace:="urn:GetConfigNivel")> _
    Public Class ConfigNivelMov

        Public Property oidConfigNivelMovimiento As String
        Public Property oidCliente As String
        Public Property codCliente As String
        Public Property desCliente As String
        Public Property oidSubCliente As String
        Public Property codSubCliente As String
        Public Property desSubCliente As String
        Public Property oidPtoServicio As String
        Public Property codPtoServicio As String
        Public Property desPtoServicio As String
        Public Property oidSubCanal As String
        Public Property codSubCanal As String
        Public Property desSubCanal As String
        Public Property fyhVigencia As DateTime
        Public Property bolActivo As Boolean
        Public Property gmtCreacion As DateTime
        Public Property desUsuarioCreacion As String
        Public Property gmtModificacion As DateTime
        Public Property desUsuarioModificacion As String
        Public Property configNivelSaldo As ConfigNivelSaldo
        Public Property bolDefecto As Boolean

    End Class
End Namespace

