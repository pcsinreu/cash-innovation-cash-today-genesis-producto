Imports Prosegur.Genesis.Utilidad.Preferencias
Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.IO
Imports System.Xml

Public Class PreferenciasAplicacion
    Public Shared ReadOnly Property Preferencias As Prosegur.Genesis.Utilidad.Preferencias.Preferencias
        Get
            Dim prefs = Nothing

            If Not String.IsNullOrEmpty(SessionManager.InformacionUsuario.DelegacionSeleccionada.Codigo) Then

                prefs = New Preferencias(SessionManager.InformacionUsuario.DelegacionSeleccionada.CodigoPais,
                                             Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Login,
                                             Prosegur.Genesis.Web.Login.Parametros.URLServicio)

            End If

            Return prefs
        End Get
    End Property

    Public Shared Sub AtualizaPreferencia(funcionalidad As String, propriedad As String, valor As String)
        If Preferencias IsNot Nothing Then
            Preferencias.Aplicacion(Enumeradores.CodigoAplicacion.SaldosNuevo).Funcionalidad(funcionalidad).Propriedad(propriedad).Valor = valor
        End If
    End Sub

    Public Shared Sub AtualizaPreferenciaSerializada(Of T)(funcionalidad As String, propriedad As String, obj As T)
        If (obj IsNot Nothing) Then
            Dim serializer As New XmlSerializer(GetType(T))
            Dim writer = New StringWriter()
            serializer.Serialize(writer, obj)
            Dim xml = writer.ToString()
            PreferenciasAplicacion.AtualizaPreferencia(funcionalidad, propriedad, xml)
        Else
            PreferenciasAplicacion.AtualizaPreferencia(funcionalidad, propriedad, Nothing)
        End If

    End Sub

    Public Shared Function ObterListaPeferencia(funcionalidad As String, propriedad As String) As List(Of String)
        If Preferencias IsNot Nothing Then
            Dim valor = Preferencias _
                .Aplicacion(Enumeradores.CodigoAplicacion.SaldosNuevo) _
                .Funcionalidad(funcionalidad) _
                .Propriedad(propriedad) _
                .Valor

            If (String.IsNullOrEmpty(valor)) Then
                Return Nothing
            Else
                Return valor.Split(",").ToList()
            End If
        End If

        Return Nothing

    End Function

    Public Shared Function ObternerPreferenciaSerializada(Of T)(funcionalidad As String, propriedad As String) As T
        Dim valor = Preferencias _
                .Aplicacion(Enumeradores.CodigoAplicacion.SaldosNuevo) _
                .Funcionalidad(funcionalidad) _
                .Propriedad(propriedad) _
                .Valor

        If (String.IsNullOrEmpty(valor)) Then
            Return Nothing
        Else
            Dim serializer As New XmlSerializer(GetType(T))
            Dim reader = New StringReader(valor)
            Dim obj As T = serializer.Deserialize(reader)
            Return obj
        End If
    End Function

    Public Shared Sub AtualizaPreferenciaBinario(funcionalidad As String, propriedad As String, valor As Byte())
        If Preferencias IsNot Nothing Then
            Preferencias.Aplicacion(Enumeradores.CodigoAplicacion.SaldosNuevo).Funcionalidad(funcionalidad).Propriedad(propriedad).Binario.Valor = valor
            If valor IsNot Nothing Then
                Preferencias.Aplicacion(Enumeradores.CodigoAplicacion.SaldosNuevo).Funcionalidad(funcionalidad).Propriedad(propriedad).Binario.Tipo = valor.GetType().ToString()
            Else
                Preferencias.Aplicacion(Enumeradores.CodigoAplicacion.SaldosNuevo).Funcionalidad(funcionalidad).Propriedad(propriedad).Binario.Tipo = Nothing
            End If
        End If
    End Sub

    Public Shared Sub AtualizaPreferenciaSerializadaBinario(Of T)(funcionalidad As String, propriedad As String, obj As T)
        If (obj IsNot Nothing) Then
            Dim serializer As New XmlSerializer(GetType(T))
            Dim writer = New StringWriter()
            serializer.Serialize(writer, obj)
            PreferenciasAplicacion.AtualizaPreferenciaBinario(funcionalidad, propriedad, writer.Encoding.GetBytes(writer.ToString()))
        Else
            PreferenciasAplicacion.AtualizaPreferenciaBinario(funcionalidad, propriedad, Nothing)
        End If

    End Sub

    Public Shared Function ObterListaPeferenciaBinario(funcionalidad As String, propriedad As String) As List(Of String)
        If Preferencias IsNot Nothing Then
            Dim valor = Encoding.UTF8.GetString(Preferencias _
                .Aplicacion(Enumeradores.CodigoAplicacion.SaldosNuevo) _
                .Funcionalidad(funcionalidad) _
                .Propriedad(propriedad) _
                .Binario.Valor)

            If (String.IsNullOrEmpty(valor)) Then
                Return Nothing
            Else
                Return valor.Split(",").ToList()
            End If
        End If

        Return Nothing

    End Function

    Public Shared Function ObternerPreferenciaSerializadaBinario(Of T)(funcionalidad As String, propriedad As String) As T

        Dim buffer = Preferencias _
                .Aplicacion(Enumeradores.CodigoAplicacion.SaldosNuevo) _
                .Funcionalidad(funcionalidad) _
                .Propriedad(propriedad) _
                .Binario.Valor

        If buffer Is Nothing Then
            Return Nothing
        Else
            Dim xmlDocument = New XmlDocument
            Dim memoryStreamXml = New MemoryStream(CType(buffer, Byte()))

            xmlDocument.Load(memoryStreamXml)

            Dim sw = New StringWriter()
            Dim tx = New XmlTextWriter(sw)
            xmlDocument.WriteTo(tx)
            Dim valor = sw.ToString()


            Dim serializer As New XmlSerializer(GetType(T))
            Dim reader = New StringReader(valor)
            Dim obj As T = serializer.Deserialize(reader)
            Return obj
        End If

    End Function
End Class
