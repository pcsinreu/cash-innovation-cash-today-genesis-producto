Imports System.IO
Imports System.Reflection

Public Class InterfaceActivator
    Private Shared InterfacesInstance As Dictionary(Of Type, InterfaceType)
    Private interfacesInstanceLock As New Object

    Public Shared Function GetInstance(Of T)(assemblyPath As String) As T
        Dim instance As T = Nothing

        If InterfacesInstance Is Nothing Then InterfacesInstance = New Dictionary(Of Type, InterfaceType)()

        If Not InterfacesInstance.ContainsKey(GetType(T)) Then
            InterfacesInstance.Add(GetType(T), New InterfaceType())
        End If

        If Not InterfacesInstance(GetType(T)).HasAssembly.HasValue Then
            Dim arquivosDLL = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory & assemblyPath, "*.dll")

            For Each arquivoDLL In arquivosDLL
                Dim dll = Assembly.LoadFile(arquivoDLL)
                InterfacesInstance(GetType(T)).ImplementedType = dll.GetTypes().FirstOrDefault(Function(o) o.GetInterfaces().Contains(GetType(T)))
                If (InterfacesInstance(GetType(T)).ImplementedType Is Nothing) Then
                    InterfacesInstance(GetType(T)).HasAssembly = False
                Else
                    InterfacesInstance(GetType(T)).HasAssembly = True
                    Exit For
                End If
            Next
        End If

        If (InterfacesInstance(GetType(T)).HasAssembly) Then
            Dim asm = Assembly.GetAssembly(InterfacesInstance(GetType(T)).ImplementedType)
            If asm IsNot Nothing AndAlso InterfacesInstance(GetType(T)).ImplementedType IsNot Nothing Then
                instance = DirectCast(Activator.CreateInstance(InterfacesInstance(GetType(T)).ImplementedType), T)
            Else
                Throw New ArgumentOutOfRangeException(String.Format("Implementation of {0} was not found."), GetType(T).Name)
            End If
        End If

        Return instance
    End Function
End Class

Public Class InterfaceType
    Public HasAssembly As Boolean?
    Public ImplementedType As Type
End Class

