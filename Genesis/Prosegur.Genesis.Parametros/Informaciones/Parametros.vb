Namespace Parametros_v2

    <Serializable()>
    Public Class Usuario
        Public Property login As String
        Public Property chave As String
        Public Property nombre As String
        Public Property apellido As String
        Public Property Sector As Sector
    End Class

    <Serializable()>
    Public Class Aplicacion

        Public Property identificador As String
        Public Property codigo As String
        Public Property descripcion As String
        Public Property codigoBuild As String
        Public Property codigoVersion As String
        Public Property descricionURLServicio As String
        Public Property descricionURLSitio As String
        Public Property puesto As Puesto
        Public Property delegacion As Delegacion
        Public Property pais As Pais
        Public Property planta As Planta
        Public Property permisos As List(Of String)

    End Class

    <Serializable()>
    Public Class Puesto

        Public Property codigo As String
        Public Property host As String
        Public Property parametros As IDictionary(Of String, Object)

    End Class

    <Serializable()>
    Public Class Delegacion

        Public Property codigo As String
        Public Property descripcion As String
        Public Property parametros As IDictionary(Of String, Object)

    End Class

    <Serializable()>
    Public Class Planta

        Public Property codigo As String
        Public Property descripcion As String

    End Class

    <Serializable()>
    Public Class Sector

        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String

    End Class

    <Serializable()>
    Public Class Pais

        Public Property codigo As String
        Public Property parametros As IDictionary(Of String, Object)

    End Class

    <Serializable()>
    Public Class Archivo

        Public Property parametros As IDictionary(Of String, Object)

    End Class

End Namespace

