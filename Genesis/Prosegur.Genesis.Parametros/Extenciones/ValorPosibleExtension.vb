Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ValorPosible

Namespace Extenciones

    Module ValorPosibleExtension

        <Runtime.CompilerServices.Extension()>
        Public Function GetDefault(valores As List(Of ValorPosible)) As ValorPosible
            Return valores.FirstOrDefault(Function(v) v.esValorDefecto = True)
        End Function

    End Module

End Namespace