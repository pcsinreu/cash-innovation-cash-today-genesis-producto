Imports System.Reflection

<Serializable()>
Public MustInherit Class BaseClase

    Public Function BaseToDerivada(Of baseClass, derivadaClass As {baseClass, New})(base As baseClass) As derivadaClass
        ' create derived instance
        Dim derived As New derivadaClass()
        ' get all base class properties
        Dim properties As PropertyInfo() = GetType(baseClass).GetProperties()
        For Each bp As PropertyInfo In properties
            ' get derived matching property
            Dim dp As PropertyInfo = GetType(derivadaClass).GetProperty(bp.Name, bp.PropertyType)

            ' this property must not be index property
            If (dp IsNot Nothing) AndAlso (dp.GetSetMethod() IsNot Nothing) AndAlso (bp.GetIndexParameters().Length = 0) AndAlso (dp.GetIndexParameters().Length = 0) Then
                dp.SetValue(derived, dp.GetValue(base, Nothing), Nothing)
            End If
        Next

        Return derived
    End Function
End Class
