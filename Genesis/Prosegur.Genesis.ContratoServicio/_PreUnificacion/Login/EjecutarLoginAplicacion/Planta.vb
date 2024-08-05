Namespace Login.EjecutarLoginAplicacion

    ''' <summary>
    ''' Classe Planta
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Planta

#Region "[PROPRIEDADES]"

        Public Property Codigo As String
        Public Property Descricao As String
        Public Property Identificador As String
        Public Property TiposSectores As New List(Of TipoSector)

#End Region

    End Class

End Namespace