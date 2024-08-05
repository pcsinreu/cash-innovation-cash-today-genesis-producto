
Namespace Login.EjecutarLoginAplicacion

    ''' <summary>
    ''' InformacionUsuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 27/06/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class InformacionUsuario

        Public Property Nombre As String
        Public Property Apelido As String
        Public Property Delegaciones As New List(Of Delegacion)
       
    End Class

End Namespace
