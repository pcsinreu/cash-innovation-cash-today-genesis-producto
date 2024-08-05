Imports System.Reflection
Imports System.Xml

Public Class Descriptor
    Public Shared Function Habilitado() As Boolean

#If DEBUG Then
        If My.Settings.ActivarDescriptor Then
            Return False
        Else
            Return False
        End If
#Else
                Return False
#End If

    End Function


    Public Shared Sub Describir(obj As Object, Optional Comentario As String = "")
        If Habilitado() Then
            If obj IsNot Nothing Then
                Dim Clase As String = obj.GetType.FullName
                Dim DirDest As String = FileIO.FileSystem.CombinePath(System.IO.Path.GetTempPath, My.Application.Info.AssemblyName & ".tmp")
                If Not FileIO.FileSystem.DirectoryExists(DirDest) Then
                    FileIO.FileSystem.CreateDirectory(DirDest)
                End If
                Dim arch As String = FileIO.FileSystem.CombinePath(DirDest, Clase & " (" & Format(Now, "yyyy-MM-dd HH-mm-ss") & ").xml")

                Dim ds As DataSet = TryCast(obj, DataSet)
                If ds IsNot Nothing Then
                    ds.WriteXml(arch, XmlWriteMode.WriteSchema)
                Else
                    Dim xDesc As XmlElement = CrearDocXML(obj.GetType.FullName, Comentario)
                    GuardarPropiedades(xDesc, obj)
                    xDesc.OwnerDocument.Save(arch)
                End If
            End If
        End If
    End Sub


    Private Shared Function CrearDocXML(Raiz As String, Optional Comentario As String = "") As XmlElement
        Dim Doc As New XmlDocument
        Doc.AppendChild(Doc.CreateXmlDeclaration("1.0", "ISO-8859-1", ""))
        Dim r As XmlElement = Doc.CreateElement(Raiz)
        Doc.AppendChild(r)
        If Comentario <> "" Then
            r.AppendChild(Doc.CreateComment(Comentario))
        End If
        Return r
    End Function

    Private Shared Sub GuardarPropiedades(xElemPadre As XmlElement, obj As Object)
        If obj IsNot Nothing AndAlso xElemPadre IsNot Nothing Then
            Dim props() As PropertyInfo = obj.GetType.GetProperties
            If props IsNot Nothing AndAlso props.Length > 0 Then
                For Each p As PropertyInfo In props
                    Dim e As XmlElement = xElemPadre.OwnerDocument.CreateElement(p.Name)
                    xElemPadre.AppendChild(e)
                    Try
                        If p.PropertyType.IsEnum Then
                            Dim v As Object = p.GetValue(obj, Nothing)
                            e.InnerText = v.ToString & "{" & Convert.ChangeType(v, [Enum].GetUnderlyingType(p.PropertyType)).ToString() & "}"

                        ElseIf p.PropertyType.FullName Like "Prosegur*" Then
                            GuardarPropiedades(e, p.GetValue(obj, Nothing))
                        ElseIf p.PropertyType.FullName Like "*ObservableCollection*" OrElse p.PropertyType.FullName Like "*Generic.List*" Then

                            Dim ie As IEnumerable = TryCast(p.GetValue(obj, Nothing), IEnumerable)
                            If ie IsNot Nothing Then
                                For Each oo As Object In ie
                                    If oo IsNot Nothing Then
                                        Dim ee As XmlElement = xElemPadre.OwnerDocument.CreateElement(oo.GetType.FullName)
                                        e.AppendChild(ee)
                                        If oo.GetType.IsEnum Then
                                            ee.InnerText = oo.ToString & "{" & Convert.ChangeType(oo, [Enum].GetUnderlyingType(oo.GetType)).ToString() & "}"
                                        ElseIf oo.GetType.IsPrimitive OrElse TypeOf oo Is Date OrElse TypeOf oo Is String Then
                                            ee.InnerText = oo.ToString
                                        Else
                                            GuardarPropiedades(ee, oo)
                                        End If
                                    End If
                                Next
                            End If
                        Else
                            Dim v As Object = p.GetValue(obj, Nothing)
                            If v IsNot Nothing Then
                                Try
                                    e.InnerText = CStr(v)
                                Catch exx As Exception
                                    e.InnerText = v.ToString
                                End Try
                            Else
                                e.InnerText = ""
                            End If
                        End If

                    Catch ex As Exception
                        Dim eex As Exception = ex
                        Dim s As String = eex.Message
                        Do While eex.InnerException IsNot Nothing
                            s += vbCrLf & eex.InnerException.Message
                            eex = eex.InnerException
                        Loop
                        e.InnerText = s
                    End Try

                Next
            End If
        End If
    End Sub




End Class
