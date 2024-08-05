Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

''' <summary>
''' Utilidades
''' </summary>
''' <remarks></remarks>
''' <history>
''' marcel.espiritosanto] 11/02/2013 Criado
''' </history>
Public Class Util

    Public Shared Sub AtribuirValorObjeto(ByRef Campo As Object, _
                                          Valor As Object, _
                                          TipoCampo As System.Type)

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
            ElseIf TipoCampo Is GetType(Long) Then
                Campo = 0
            Else
                Campo = Nothing
            End If

        End If

    End Sub

End Class

