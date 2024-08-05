Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.Reportes.ContractoServ
Imports System.Xml.Serialization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports Prosegur.Genesis

''' <summary>
''' Classe Util
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 21/01/2010 - Criado
''' </history>
Public Class Util

    ''' <summary>
    ''' Verifica se o campo foi preenchido
    ''' </summary>
    ''' <param name="campo"></param>
    ''' <param name="msgErro"></param>
    ''' <param name="TipoCampo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2010 - Criado
    ''' </history>
    Public Shared Sub ValidarCampoObrigatorio(campo As Object, msgErro As String, _
                                              TipoCampo As System.Type, bolColeccion As Boolean, _
                                              bolMsgAtributo As Boolean)

        If TipoCampo Is GetType(String) Then

            If String.IsNullOrEmpty(campo) Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                 String.Format(Traduzir("Gen_msg_atributo_invalido"), _
                                               Traduzir(msgErro)))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(DateTime) Then

            If campo Is Nothing OrElse campo.Equals(Date.MinValue) Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                 String.Format(Traduzir("Gen_msg_atributo_invalido"), _
                                               Traduzir(msgErro)))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(Nullable(Of Decimal)) Then

            If campo Is Nothing Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                 String.Format(Traduzir("Gen_msg_atributo_invalido"), _
                                               Traduzir(msgErro)))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(Nullable(Of Boolean)) Then

            If campo Is Nothing Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                 String.Format(Traduzir("Gen_msg_atributo_invalido"), _
                                               Traduzir(msgErro)))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(Nullable(Of Integer)) Then

            If campo Is Nothing Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                 String.Format(Traduzir("Gen_msg_atributo_invalido"), _
                                               Traduzir(msgErro)))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                End If

            End If

        Else

            If bolColeccion Then

                If campo Is Nothing OrElse campo.count = 0 Then

                    If bolMsgAtributo Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                     String.Format(Traduzir("Gen_msg_atributo_invalido"), _
                                                   Traduzir(msgErro)))
                    Else
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                    End If
                End If

            Else

                If campo Is Nothing Then

                    If bolMsgAtributo Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                     String.Format(Traduzir("Gen_msg_atributo_invalido"), _
                                                   Traduzir(msgErro)))
                    Else
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                    End If

                End If

            End If

        End If
    End Sub

    ''' <summary>
    ''' Atribui o valor ao objeto passado, faz a conversão do tipo do banco para o tipo da propriedade.
    ''' </summary>
    ''' <param name="Valor"></param>
    ''' <param name="TipoCampo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/11/2009 Criado
    ''' </history>
    Public Shared Function AtribuirValorObjeto(Valor As Object, _
                                               TipoCampo As System.Type) As Object

        Dim Campo As New Object

        If Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
            If TipoCampo Is Nothing Then
                Campo = Valor
            ElseIf TipoCampo Is GetType(String) Then
                Campo = Convert.ToString(Valor)
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = Convert.ToInt16(Valor)
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = Convert.ToInt32(Valor)
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = Convert.ToInt64(Valor)
            ElseIf TipoCampo Is GetType(Decimal) Then
                Campo = Convert.ToDecimal(Valor)
            ElseIf TipoCampo Is GetType(Double) Then
                Campo = Convert.ToDouble(Valor)
            ElseIf TipoCampo Is GetType(Boolean) Then
                Campo = Convert.ToBoolean(Convert.ToInt16(Valor.ToString.Trim))
            ElseIf TipoCampo Is GetType(DateTime) Then
                Campo = Convert.ToDateTime(Valor)
            End If
        Else

            If TipoCampo Is GetType(Decimal) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Boolean) Then
                Campo = 0
            Else
                Campo = Nothing
            End If

        End If

        Return Campo
    End Function
    Public Shared Sub TratarErroBugsnag(ex As Exception)

        If (Not (TypeOf ex Is Genesis.Excepcion.NegocioExcepcion)) Then

            Prosegur.BugsnagHelper.NotifyIfEnabled(ex)

        End If
    End Sub
    ''' <summary>
    ''' Faz o tratamento de erro de retorno do serviço
    ''' </summary>
    ''' <param name="MensajeErrorGenerica"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 24/09/2010 - Criado
    ''' </history>
    Public Shared Function TratarError(MensajeErrorGenerica As String) As String

        If Not String.IsNullOrEmpty(MensajeErrorGenerica) Then

            Dim palavras() As String = MensajeErrorGenerica.Split(" ")

            If (From p In palavras Where p.Trim.ToLower = Constantes.CONST_ERRO_DNS.ToLower).Count > 0 Then
                'Retorna erro de dns
                Return Traduzir("gen_msg_error_01")

            ElseIf (From p In palavras Where p.Trim.ToLower = Constantes.CONST_ERRO_URL.ToLower _
                                       OrElse p.Trim.ToLower = Constantes.CONST_ERRO_404B.ToLower _
                                       OrElse p.Trim.ToLower = Constantes.CONST_ERRO_404A.ToLower _
                                       OrElse p.Trim.ToLower = Constantes.CONST_ERRO_407B.ToLower _
                                       OrElse p.Trim.ToLower = Constantes.CONST_ERRO_407A.ToLower).Count > 0 Then

                'Retorna erro de url invalida
                Return Traduzir("gen_msg_error_02") & " " & Parametros.Configuracion.UrlLoginGlobal


            ElseIf (From p In palavras Where p.Trim.ToLower = Constantes.CONST_ERRO_ACCESO_LDAP.ToLower).Count > 0 Then
                'Retorna erro relacionado a usuario se senha do servidor de ad.
                Return Traduzir("gen_msg_error_03")

            ElseIf (From p In palavras Where p.Trim.ToLower = Constantes.CONST_ERRO_BANCO.ToLower OrElse _
                                             p.Trim.ToLower = Constantes.CONST_ERRO_BANCO2.ToLower OrElse _
                                             p.Trim.ToLower = Constantes.CONST_ERRO_BANCO3.ToLower OrElse _
                                             p.Trim.ToLower = Constantes.CONST_ERRO_BANCO4.ToLower).Count > 0 Then

                'Retorna erro de banco de dados
                Return String.Format(Traduzir("gen_msg_error_05"), AccesoDatos.Util.RetornaNomeServidorBD)

            End If

        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' Serializa um objeto
    ''' </summary>
    ''' <param name="Objeto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Serializar(Objeto As Object) As String

        Dim writer As StringWriter = New StringWriter
        Dim serializer As XmlSerializer = New XmlSerializer(Objeto.GetType())


        serializer.Serialize(writer, Objeto)


        Return writer.ToString()

    End Function

    ''' <summary>
    ''' Retorna cópia do objeto passado como parâmetro
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ClonarObjeto(obj As Object) As Object

        If obj Is Nothing Then
            Return Nothing
        End If

        ' Create a memory stream and a formatter.
        Dim ms As New MemoryStream()
        Dim bf As New BinaryFormatter()

        ' Serialize the object into the stream.
        bf.Serialize(ms, obj)

        ' Position streem pointer back to first byte.
        ms.Seek(0, SeekOrigin.Begin)

        ' Deserialize into another object.
        ClonarObjeto = bf.Deserialize(ms)

        ' Release memory.
        ms.Close()

    End Function

End Class
