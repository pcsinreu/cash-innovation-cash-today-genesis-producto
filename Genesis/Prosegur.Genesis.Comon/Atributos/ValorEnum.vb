Namespace Atributos

    <AttributeUsage(AttributeTargets.Field, AllowMultiple:=False)>
    Public Class ValorEnum
        Inherits Attribute

        Public Property Valor As String

        Sub New(valor As String)
            Me.Valor = valor
        End Sub

    End Class

End Namespace