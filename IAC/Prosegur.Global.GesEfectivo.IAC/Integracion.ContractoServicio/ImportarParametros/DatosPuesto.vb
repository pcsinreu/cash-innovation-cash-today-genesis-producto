Namespace ImportarParametros

    ''' <summary>
    ''' Classe DatosPuesto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/09/2011 - Criado
    ''' </history>
    <Serializable()> _
    Public Class DatosPuesto

#Region "[PROPRIEDADES]"

        Public Property CodigoPuesto As String
        Public Property CodigoHostPuesto As String
        Public Property CodigoAplicacion As String
        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property Parametros As ParametroColeccion

#End Region

    End Class
End Namespace