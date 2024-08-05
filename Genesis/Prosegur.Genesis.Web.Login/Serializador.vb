Imports System.IO
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class Serializador

    ''' <summary>
    ''' Serializa um objeto do tipo informado
    ''' </summary>
    Shared Function Serializar(Of T)(obj As T) As String

        Try

            Dim dadosSerializados As String = String.Empty

            Using ms As New MemoryStream

                ' serializa
                Dim xmlSerializer As New Xml.Serialization.XmlSerializer(GetType(T))
                xmlSerializer.Serialize(ms, obj)
                ms.Seek(0, SeekOrigin.Begin)

                Using reader As New StreamReader(ms)

                    dadosSerializados = reader.ReadToEnd()
                    Dim cripto As New Prosegur.CriptoHelper.Rsa

                    ' criptografa
                    'dadosSerializados = cripto.Criptografar(dadosSerializados)

                    reader.Close()
                End Using

                ms.Close()
            End Using

            Return dadosSerializados

        Catch ex As Exception
            Throw New Exception(Traduzir("tkn_creartokenacceso_erro_serializar"), ex)
        End Try

    End Function

    ''' <summary>
    ''' Deserializa um objeto do tipo informado
    ''' </summary>
    Shared Function Deserializar(Of T)(dadosSerializados As String) As T

        Try
            Dim objeto As T

            Dim cripto As New Prosegur.CriptoHelper.Rsa

            'permisosSerializados = cripto.Decriptografar(permisosSerializados)

            Dim xmlSerializer As New Xml.Serialization.XmlSerializer(GetType(T))

            Using sr As New StringReader(dadosSerializados)
                objeto = xmlSerializer.Deserialize(sr)
                sr.Close()
            End Using

            Return objeto

        Catch ex As Exception
            Throw New Exception(Traduzir("gen_msg_creartokenacceso_erro_deserializar"), ex)
        End Try


    End Function

End Class
