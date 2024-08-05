Imports System.IO
Imports System.Xml.Serialization


Namespace CodigoAjeno

    <Serializable()> _
    Public Class CodigoAjenoColeccionBase
        Inherits List(Of CodigoAjenoBase)

        Public Overrides Function ToString() As String
            Dim retorno As String = Nothing
            ' Create a memory stream and a formatter.
            Using ms As New MemoryStream()


                'Serialize object to a text file.
                Dim x As New XmlSerializer(Me.GetType)
                x.Serialize(ms, Me)

                ms.Position = 0
                Using sr As New StreamReader(ms)
                    retorno = sr.ReadToEnd
                    sr.Close()
                End Using

                ms.Close()
            End Using

            Return retorno
        End Function

    End Class

End Namespace

