Imports System.Collections.ObjectModel

Namespace Caracteristicas

    Public NotInheritable Class Util
        Public Shared Function VerificarCaracteristicas(caracteristicas As List(Of Enumeradores.CaracteristicaFormulario), ParamArray grupoCaracteristicas As Caracteristicas.GrupoCaracteristicas()) As Boolean
            Dim respuesta As Boolean = True
            If caracteristicas Is Nothing OrElse caracteristicas.Count = 0 Then
                respuesta = False
            Else
                For Each grupo As Caracteristicas.GrupoCaracteristicas In grupoCaracteristicas
                    Dim result As Boolean
                    If grupo.Tipo = Prosegur.Genesis.Comon.Caracteristicas.TipoVerificacionCaracteristicas.And Then
                        result = True
                        For Each caracteristica As Enumeradores.CaracteristicaFormulario In grupo.Caracteristicas
                            result = result AndAlso caracteristicas.Contains(caracteristica)
                        Next
                    ElseIf grupo.Tipo = Prosegur.Genesis.Comon.Caracteristicas.TipoVerificacionCaracteristicas.Or Then
                        result = False
                        For Each caracteristica As Enumeradores.CaracteristicaFormulario In grupo.Caracteristicas
                            result = result OrElse caracteristicas.Contains(caracteristica)
                        Next
                    Else
                        result = False
                        For Each caracteristica As Enumeradores.CaracteristicaFormulario In grupo.Caracteristicas
                            result = result Xor caracteristicas.Contains(caracteristica)
                        Next
                    End If
                    respuesta = respuesta AndAlso result
                    If Not respuesta Then
                        Exit For
                    End If
                Next
            End If
            Return respuesta
        End Function
    End Class

End Namespace