Namespace Clases

    ' ***********************************************************************
    '  Modulo:  GrupoTerminos.vb
    '  Descripción: Clase definición GrupoTerminos
    ' ***********************************************************************
    <Serializable()>
    Public Class GrupoTerminos
        Inherits BaseClase

        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Observacion As Boolean
        Public Property CopiaDeclarados As Boolean
        Public Property EstaActivo As String
        Public Property EsInvisible As Boolean
        Public Property CodigoUsuario As String
        Public Property FechaHoraActualizacion As DateTime
        Public Property TerminosIAC As List(Of TerminoIAC)

    End Class

End Namespace
