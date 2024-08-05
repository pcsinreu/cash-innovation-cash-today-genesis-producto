Namespace Extenciones

    Public Module EnumExtension

        <Runtime.CompilerServices.Extension()>
        Public Function RecuperarValor(e As System.Enum) As String
            Dim tipoEnum As Type = e.GetType()
            Dim field As Reflection.FieldInfo = tipoEnum.GetField(System.Enum.GetName(tipoEnum, e))
            If field IsNot Nothing Then
                Dim atributo As Atributos.ValorEnum = Attribute.GetCustomAttribute(field, GetType(Atributos.ValorEnum))
                If atributo IsNot Nothing Then
                    Return atributo.Valor
                End If
                Throw New NotImplementedException(tipoEnum.ToString() & "." & field.Name)
            End If
            Return Nothing
        End Function

        Public Function RecuperarEnum(Of TEnum)(valor As String) As TEnum
            Dim tipoEnum As Type = GetType(TEnum)
            If Not tipoEnum.IsEnum Then
                Throw New ArgumentException("TEnum", "TEnum")
            End If
            Dim fields As Reflection.FieldInfo() = tipoEnum.GetFields()
            If fields IsNot Nothing Then
                Dim atributo As Atributos.ValorEnum
                For Each field In fields
                    atributo = Attribute.GetCustomAttribute(field, GetType(Atributos.ValorEnum))
                    If atributo IsNot Nothing Then
                        If atributo.Valor = valor Then
                            Return System.Enum.Parse(GetType(TEnum), field.Name)
                        End If
                    End If
                Next

                Return RecuperarEnum(Of TEnum)("NO-DEFINIDO")

            End If
            Throw New NotImplementedException(valor)
        End Function

        Public Function ExisteEnum(Of TEnum)(valor As String) As Boolean
            Dim tipoEnum As Type = GetType(TEnum)
            If Not tipoEnum.IsEnum Then
                Throw New ArgumentException("TEnum", "TEnum")
            End If
            Dim fields As Reflection.FieldInfo() = tipoEnum.GetFields()
            If fields IsNot Nothing Then
                Dim atributo As Atributos.ValorEnum
                For Each field In fields
                    atributo = Attribute.GetCustomAttribute(field, GetType(Atributos.ValorEnum))
                    If atributo IsNot Nothing Then
                        If atributo.Valor = valor Then
                            'Se o Enum existe
                            Return True
                        End If
                    End If
                Next
            End If
            'Se o Enum não existe
            Return False
        End Function

    End Module

End Namespace