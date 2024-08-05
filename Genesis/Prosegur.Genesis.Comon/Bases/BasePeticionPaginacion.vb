Imports Prosegur.Genesis.Comon.Paginacion

<Serializable()> _
Public MustInherit Class BasePeticionPaginacion
    Inherits BasePeticion
    Implements IPeticionPaginacion

    ''' <summary>
    ''' Parametros utilizados para a paginação da resposta, inicializar esta propriedade para que o resultado seja paginado
    ''' </summary>
    Public Property ParametrosPaginacion As ParametrosPeticionPaginacion Implements Paginacion.IPeticionPaginacion.ParametrosPaginacion

    ''' <summary>
    ''' Informa ao serializador se esta propriedade deve ser repassada via soap, somente quando o valor for diferente de nulo
    ''' </summary>
    Public Function ShouldSerializeParametrosPaginacion() As Boolean
        Return ParametrosPaginacion IsNot Nothing
    End Function

End Class
