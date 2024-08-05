Namespace Utilidad.GetConfigNivel

    ''' <summary>
    ''' Coleção de CofigNivelSaldo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 20/05/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class ConfigNivelSaldo

        Public Property oidConfigNivelSaldo As String
        Public Property oidCliente As String
        Public Property codCliente As String
        Public Property desCliente As String
        Public Property oidSubcliente As String
        Public Property codSubcliente As String
        Public Property desSubcliente As String
        Public Property oidPtoServicio As String
        Public Property codPtoServicio As String
        Public Property desPtoServicio As String
        Public Property gmtCreacion As DateTime
        Public Property desUsuarioCreacion As String
        Public Property gmtModificacion As DateTime
        Public Property desUsuarioModificacion As String

    End Class
End Namespace

