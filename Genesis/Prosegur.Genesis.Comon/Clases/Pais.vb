Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Pais.vb
    '  Descripción: Clase definición Pais
    ' ***********************************************************************
    <Serializable()>
    Public Class Pais
        Inherits BaseClase

        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property EsActivo As Boolean
        Public Property FechaHoraCreacion As DateTime
        Public Property UsuarioCreacion As String
        Public Property FechaHoraModificacion As DateTime
        Public Property UsuarioModificacion As String
        Public Property Delegaciones As List(Of Delegacion)

    End Class

End Namespace
