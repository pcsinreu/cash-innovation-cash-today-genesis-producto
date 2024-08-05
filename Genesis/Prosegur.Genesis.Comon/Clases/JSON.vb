Imports Newtonsoft.Json

Namespace JSON

    Public Class EnumConverter
        Inherits Newtonsoft.Json.Converters.StringEnumConverter

        Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
            writer.WriteValue(Extenciones.RecuperarValor(value))
        End Sub

    End Class

End Namespace
