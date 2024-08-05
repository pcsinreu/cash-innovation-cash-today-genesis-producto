
Namespace ImporteMaximo.GetImporteMaximo

    ''' <summary>
    ''' Classe EntidadImporteMaximo
    ''' </summary>
    ''' <remarks></remarks>
  
    <Serializable()> _
    Public Class EntidadImporteMaximo

#Region "[VARIAVEIS]"

        Private _ImportesMaximo As ImporteMaximoRespuestaColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property ImportesMaximo() As ImporteMaximoRespuestaColeccion
            Get
                Return _ImportesMaximo
            End Get
            Set(value As ImporteMaximoRespuestaColeccion)
                _ImportesMaximo = value
            End Set
        End Property


#End Region

    End Class

End Namespace
