Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.IO
Imports Prosegur.Genesis
Imports System.Xml
Imports System.Text

Public Class Util

    Public Shared Function VerificarDBNull(Valor As Object) As Object

        If IsDBNull(Valor) Then
            Return Nothing
        Else
            Return Valor
        End If

    End Function

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
                                 String.Format(Traduzir("Gen_msg_atributo"), _
                                               Traduzir(msgErro)))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(DateTime) Then

            If campo.Equals(Date.MinValue) Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                 String.Format(Traduzir("Gen_msg_atributo"), _
                                               Traduzir(msgErro)))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(Nullable(Of Decimal)) Then

            If campo Is Nothing Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                 String.Format(Traduzir("Gen_msg_atributo"), _
                                               Traduzir(msgErro)))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(Nullable(Of Boolean)) Then

            If campo Is Nothing Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                 String.Format(Traduzir("Gen_msg_atributo"), _
                                               Traduzir(msgErro)))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(Nullable(Of Integer)) Then

            If campo Is Nothing Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                 String.Format(Traduzir("Gen_msg_atributo"), _
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
                                     String.Format(Traduzir("Gen_msg_atributo"), _
                                                   Traduzir(msgErro)))
                    Else
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(msgErro))
                    End If
                End If

            Else

                If campo Is Nothing Then

                    If bolMsgAtributo Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                     String.Format(Traduzir("Gen_msg_atributo"), _
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
    Public Shared Function AtribuirValorObj(Valor As Object,
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

            If TipoCampo Is GetType(Decimal) OrElse TipoCampo Is GetType(Int16) OrElse TipoCampo Is GetType(Int32) OrElse TipoCampo Is GetType(Int64) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(String) Then
                Campo = String.Empty
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

    Public Shared Function URLValida(URL As String) As Boolean
        Try
            Dim Response As Net.WebResponse = Nothing
            Dim WebReq As Net.HttpWebRequest = Net.HttpWebRequest.Create(URL)
            Response = WebReq.GetResponse
            Response.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function GenerarCSV(datos As DataTable,
                             Optional sDelimitadorColunas As String = ";",
                             Optional sDelimitadorRegistros As String = vbNewLine) As String

        Dim culturaCorrente = Threading.Thread.CurrentThread.CurrentCulture
        Dim archivoCSV As New StringBuilder("")

        Try

            If datos IsNot Nothing AndAlso datos.Rows IsNot Nothing AndAlso datos.Rows.Count > 0 Then

                Dim sRegistro As String = ""

                ' Nombre de las columnas
                For Each columna In datos.Columns
                    sRegistro &= columna.ToString.ToUpper & sDelimitadorColunas
                Next
                archivoCSV.Append(sRegistro)
                archivoCSV.Append(sDelimitadorRegistros)

                Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")

                ' Registros
                For Each registro As DataRow In datos.Rows

                    sRegistro = String.Empty

                    For Each columna In datos.Columns
                        sRegistro &= registro(columna).ToString.Trim & sDelimitadorColunas
                    Next
                    archivoCSV.Append(sRegistro)
                    archivoCSV.Append(sDelimitadorRegistros)

                Next

            End If

        Catch ex As Exception
            Throw
        Finally
            GC.Collect()
        End Try

        Threading.Thread.CurrentThread.CurrentCulture = culturaCorrente

        Return archivoCSV.ToString()

    End Function

End Class
