Namespace Clases

    <Serializable()>
    Public Class Parametro

        Public Property codigoHostPuesto As String
        Public Property codigoPuesto As String
        Public Property codigoPais As String
        Public Property codigoDelegacion As String
        Public Property codigoAplicacion As String
        Public Property codigoParametro As String
        Public Property esObligatorio As Boolean

        '1 - TEXTBOX
        '2 - COMBOBOX
        '3 - CHECKBOX
        '4 - PALETACOLORES
        Public Property tipoParametro As Integer

        '1	Pais
        '2	Delegacion
        '3	Puesto
        Public Property nivelParametro As Integer

        Private _valorParametro As String
        Public Property valorParametro() As String
            Get
                If tipoParametro = 3 AndAlso String.IsNullOrEmpty(_valorParametro) Then
                    _valorParametro = "0"
                End If
                Return _valorParametro
            End Get
            Set(value As String)
                _valorParametro = value
            End Set
        End Property


    End Class

End Namespace
