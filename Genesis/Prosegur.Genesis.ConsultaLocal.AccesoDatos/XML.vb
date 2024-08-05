
Imports System.Xml
Imports System.Xml.Xsl

Public Class XML
    Public Shared Function LeerXSL(XSLpath As String) As String
        XSLpath = XSLpath.Trim
        Dim s As String = ""
        If Not XSLpath = "" Then
            Try
                s = IO.File.ReadAllText(XSLpath)
            Catch ex As Exception
                Throw New Exception("Error al leer el archivo " & XSLpath, ex)
            End Try
        Else
            Throw New Exception("Archivo inválido")
        End If
        Return s
    End Function

    Public Shared Function Compilar(sXML As String) As XslCompiledTransform
        Dim xslt As New XslCompiledTransform
        Dim xr As New XmlDocument
        Try
            xr.LoadXml(sXML)
            xslt.Load(xr)

        Catch ex As Exception
            Throw New Exception("Se produjo un error al cargar la transformación", ex)
        End Try

        Return xslt
    End Function

    Public Shared Function Transformar(sXML As String, PathRelativo As String, Archivo As String) As String
        Dim path As String = System.Web.HttpRuntime.AppDomainAppPath

        Dim path1 As String = FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(path, PathRelativo), Archivo)

        Return Transformar(sXML, Compilar(LeerXSL(path1)))
    End Function

    Public Shared Function Transformar(sXML As String, xslt As XslCompiledTransform) As String

        Dim xRead As XmlReader
        Dim s As New System.IO.StringReader(sXML)

        Dim xW As XmlWriter
        Dim tW As New System.IO.StringWriter
        xW = XmlWriter.Create(tW)
        xRead = XmlReader.Create(s)

        xslt.Transform(xRead, xW)

        Dim ss As String = tW.ToString
        Dim pos As Integer
        pos = ss.IndexOf("?>")
        If pos > 0 Then
            If ss.ToUpper.Contains("<HTML>") Then
                ss = ss.Substring(pos + 2)
            Else
                ss = "<?xml version='1.0' encoding='ISO-8859-1' ?>" & ss.Substring(pos + 2)
            End If

        End If

        Return ss
    End Function

    Public Shared Function DSaXML(ds As DataSet) As String
        Dim s As String = ""
        If ds IsNot Nothing Then
            s = ds.GetXml()
        End If
        Return s
    End Function


End Class
