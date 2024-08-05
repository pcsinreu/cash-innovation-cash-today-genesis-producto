Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization

Namespace Extenciones

    Public Module ObservableCollectionExtension

        ''' <summary>
        ''' Extensão para AddRange do List para Observable
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="e"></param>
        ''' <param name="collection"></param>
        ''' <remarks></remarks>
        <Runtime.CompilerServices.Extension()>
        Public Sub AddRange(Of T)(e As ObservableCollection(Of T), collection As IEnumerable(Of T))
            For Each item In collection
                e.Add(item)
            Next item
        End Sub

        ''' <summary>
        ''' Extensão do FindAll do List para Observable
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="e"></param>
        ''' <param name="match"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Runtime.CompilerServices.Extension()>
        Public Function FindAll(Of T)(e As ObservableCollection(Of T), match As Predicate(Of T)) As ObservableCollection(Of T)
            Dim _itensAdd As New ObservableCollection(Of T)()
            For Each item In e
                If match(item) Then
                    _itensAdd.Add(item)
                End If
            Next item

            e = _itensAdd
            Return e

        End Function

        ''' <summary>
        ''' Extensão do FindLast do List para Observable
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="e"></param>
        ''' <param name="match"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Runtime.CompilerServices.Extension()>
        Public Function FindLast(Of T)(e As ObservableCollection(Of T), match As Predicate(Of T)) As T
            Dim _itensAdd As New ObservableCollection(Of T)()
            For Each item In e
                If match(item) Then
                    Return item
                End If
            Next item

        End Function

        ''' <summary>
        ''' Extensão do Foreach do List para Observable
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="e"></param>
        ''' <param name="accion"></param>
        ''' <remarks></remarks>
        <Runtime.CompilerServices.Extension()>
        Public Sub Foreach(Of T)(e As ObservableCollection(Of T), accion As Action(Of T))

            For Each item In e
                accion(item)
            Next item

        End Sub

        ''' <summary>
        ''' Extensão do Foreach do List para Observable
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        <Runtime.CompilerServices.Extension()>
        Public Function ToObservableCollection(Of T)(e As IEnumerable(Of T)) As ObservableCollection(Of T)

            Return New ObservableCollection(Of T)(e)

        End Function

        ''' <summary>
        ''' Extensão do Exists do Find para Observable
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="e"></param>
        ''' <param name="match"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Runtime.CompilerServices.Extension()>
        Public Function Find(Of T)(e As ObservableCollection(Of T), match As Predicate(Of T)) As T

            For Each item In e
                If match(item) Then
                    Return item
                End If
            Next item

            Return Nothing
        End Function

        ''' <summary>
        ''' Extensão do Exists do List para Observable
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="e"></param>
        ''' <param name="match"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Runtime.CompilerServices.Extension()>
        Public Function Exists(Of T)(e As ObservableCollection(Of T), match As Predicate(Of T)) As Boolean

            For Each item In e
                If match(item) Then
                    Return True
                End If
            Next item

            Return False
        End Function

        ''' <summary>
        ''' Extensão do RemoveAll do List para Observable
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="e"></param>
        ''' <param name="match"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Runtime.CompilerServices.Extension()>
        Public Function RemoveAll(Of T)(e As ObservableCollection(Of T), match As Predicate(Of T)) As ObservableCollection(Of T)
            Dim _itensRemove As New ObservableCollection(Of T)()
            Dim _itensAdd As New ObservableCollection(Of T)()
            For Each item In e
                If match(item) Then
                    _itensRemove.Add(item)
                Else
                    _itensAdd.Add(item)
                End If
            Next item
            e.Clear()
            For Each _item In _itensAdd
                e.Add(_item)
            Next _item
            _itensAdd = Nothing
            _itensRemove = Nothing

            Return e

        End Function

    End Module

End Namespace