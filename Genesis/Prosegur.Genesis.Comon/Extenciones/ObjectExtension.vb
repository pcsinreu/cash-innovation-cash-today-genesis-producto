Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization

Namespace Extenciones

    Public Module ObjectExtension

        <Runtime.CompilerServices.Extension()>
        Public Function Clonar(Of TObjeto As Class)(objeto As TObjeto) As TObjeto

            If objeto Is Nothing Then Return Nothing

            Using ms As New MemoryStream()
                Dim bf As New BinaryFormatter()
                bf.Serialize(ms, objeto)
                ms.Seek(0, SeekOrigin.Begin)
                Return DirectCast(bf.Deserialize(ms), TObjeto)
                ms.Close()
            End Using

        End Function

        
    End Module

End Namespace