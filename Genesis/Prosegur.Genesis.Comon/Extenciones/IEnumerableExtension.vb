Namespace Extenciones

    Public Module IEnumerableExtension

        <Runtime.CompilerServices.Extension()>
        Public Function ToSerializableDictionary(Of TSource, TKey, TElement)(source As System.Collections.Generic.IEnumerable(Of TSource), keySelector As System.Func(Of TSource, TKey), elementSelector As System.Func(Of TSource, TElement)) As SerializableDictionary(Of TKey, TElement)
            Dim retorno As New SerializableDictionary(Of TKey, TElement)()
            For Each item In source
                retorno.Add(keySelector.Invoke(item), elementSelector.Invoke(item))
            Next
            Return retorno
        End Function

    End Module

End Namespace

