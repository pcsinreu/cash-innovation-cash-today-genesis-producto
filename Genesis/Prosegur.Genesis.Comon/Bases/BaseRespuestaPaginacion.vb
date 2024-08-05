Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon

<Serializable()> _
Public MustInherit Class BaseRespuestaPaginacion
    Inherits BaseRespuesta
    Implements IRespuestaPaginacion

    ''' <summary>
    ''' Parametros utilizados para a paginação da resposta, inicializar esta propriedade para que o resultado seja paginado
    ''' </summary>
    Public Property ParametrosPaginacion As ParametrosRespuestaPaginacion Implements IRespuestaPaginacion.ParametrosPaginacion

    ''' <summary>
    ''' Informa ao serializador se esta propriedade deve ser repassada via soap, somente quando o valor for diferente de nulo
    ''' </summary>
    Public Function ShouldSerializeParametrosPaginacion() As Boolean
        Return ParametrosPaginacion IsNot Nothing
    End Function

End Class
