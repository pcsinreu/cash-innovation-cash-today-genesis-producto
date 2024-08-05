Namespace GenesisConteo.Cuadrar

    ''' <summary>
    ''' Classe DescuadreDiferencias
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois]  14/08/2013 Criado
    ''' </history>
    <Serializable()>
    Public NotInheritable Class DifDeclarados

        Public Property DeclaradosRemesa As DifDeclaradoColeccion
        Public Property DeclaradosBultos As DifDeclaradoColeccion
        Public Property DeclaradosBultosDet As DifDeclaradoDetColeccion
        Public Property DeclaradosParciales As DifDeclaradoColeccion
        Public Property DeclaradosParcialesDet As DifDeclaradoDetColeccion
        Public Property DeclaradoMedioPagoRemesa As DifDeclaradoMedioPagoColeccion
        Public Property DeclaradoMedioPagoBulto As DifDeclaradoMedioPagoColeccion
        Public Property DeclaradoMedioPagoParcial As DifDeclaradoMedioPagoColeccion

    End Class

End Namespace