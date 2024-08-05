Imports System.IO
Imports System.IO.Compression

Namespace Compactor
    Public Class GZip

        Public Shared Sub Compress(fileNameToCompress As String)
            If File.Exists(fileNameToCompress) Then
                Compress(New FileInfo(fileNameToCompress))
            End If
        End Sub

        Public Shared Sub Compress(fileToCompress As FileInfo)
            Using originalFileStream As FileStream = File.OpenRead(fileToCompress.FullName)
                If (File.GetAttributes(fileToCompress.FullName) AndAlso FileAttributes.Hidden) <> FileAttributes.Hidden AndAlso fileToCompress.Extension <> ".gz" Then
                    Dim fileNameToCompress As String = fileToCompress.FullName & ".gz"
                    If File.Exists(fileNameToCompress) Then
                        fileNameToCompress = fileToCompress.FullName & DateTime.Now.ToString("-mm") & ".gz"
                        If File.Exists(fileNameToCompress) Then
                            fileNameToCompress = fileToCompress.FullName & DateTime.Now.ToString("-mm-ss") & ".gz"
                            If File.Exists(fileNameToCompress) Then
                                fileNameToCompress = fileToCompress.FullName & DateTime.Now.ToString("-mm-ss-ffffff") & ".gz"
                            End If
                        End If
                    End If
                    Using compressedFileStream As FileStream = File.Create(fileNameToCompress)
                        Using compressionStream As GZipStream = New GZipStream(compressedFileStream, CompressionMode.Compress)
                            originalFileStream.CopyTo(compressionStream)
                        End Using
                    End Using
                End If
            End Using
        End Sub

        Public Shared Sub Decompress(fileToDecompress As FileInfo)
            Using originalFileStream As FileStream = fileToDecompress.OpenRead()
                Dim currentFileName As String = fileToDecompress.FullName
                Dim newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length)

                Using decompressedFileStream As FileStream = File.Create(newFileName)
                    Using decompressionStream As GZipStream = New GZipStream(originalFileStream, CompressionMode.Decompress)
                        decompressionStream.CopyTo(decompressedFileStream)
                    End Using
                End Using
            End Using
        End Sub
    End Class
End Namespace