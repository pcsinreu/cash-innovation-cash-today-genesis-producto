Namespace Caracteristicas

    <Serializable()>
    Public Class GrupoCaracteristicas
        Public Property Tipo As Caracteristicas.TipoVerificacionCaracteristicas
        Public Property Caracteristicas As Enumeradores.CaracteristicaFormulario()
        Public Sub New(tipo As Caracteristicas.TipoVerificacionCaracteristicas, ParamArray caracteristicas As Enumeradores.CaracteristicaFormulario())
            Me.Tipo = tipo
            Me.Caracteristicas = caracteristicas
        End Sub
    End Class

End Namespace