Namespace RecuperarParametros

    <Serializable()> _
    Public Class Parametro

#Region "[PROPRIEDADES]"

        Public Property CodigoParametro As String
        Public Property EsObligatorio As Boolean
        Public Property DescripcionCortaParametro As String
        Public Property DescripcionLargaParametro As String
        Public Property ValorParametro As String
        Public Property ValoresPosibles As ValorPosibleColeccion
        Public Property ListaValores As Boolean

#End Region

    End Class

End Namespace