Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Genesis
    Public Class TipoImpresora

        ''' <summary>
        ''' Recupera todos los tipo de impresoras.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTiposImpresora() As ObservableCollection(Of Clases.TipoImpresora)
            Dim dt As DataTable = ListaValor.RecuperarPorTipo(Enumeradores.TipoListaValor.TipoImpresora)
            Dim listaTiposImpresora As New ObservableCollection(Of Clases.TipoImpresora)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    listaTiposImpresora.Add(PreencherTipoImpresora(dr))
                Next
            End If
            Return listaTiposImpresora
        End Function

        Public Shared Function RecuperarTipoImpresoraPorIdentificador(identificador As String) As Clases.TipoImpresora
            Dim dt As DataTable = ListaValor.RecuperarPorIdentificador(identificador)
            Dim tipoFormato As Clases.TipoImpresora = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                tipoFormato = PreencherTipoImpresora(dt.Rows(0))
            End If
            Return tipoFormato
        End Function

        Public Shared Function RecuperarTipoImpresoraPorCodigo(codigo As String) As Clases.TipoImpresora
            Dim dt As DataTable = ListaValor.RecuperarPorCodigo(codigo, Prosegur.Genesis.Comon.Enumeradores.TipoListaValor.TipoImpresora)
            Return PreencherTipoImpresora(dt)
        End Function

        Public Shared Function RecuperarPorElemento(identificadorRemessa As String, identificadorBulto As String, identificadorParcial As String) As Clases.TipoImpresora
            Dim dt As DataTable = ListaValor.RecuperarPorElemento(identificadorRemessa, identificadorBulto, identificadorParcial, Prosegur.Genesis.Comon.Enumeradores.TipoListaValor.TipoImpresora)
            Return PreencherTipoImpresora(dt)
        End Function

        ''' <summary>
        ''' Recupera los valores de un DataTable e retorna un tipoimpresora. Campos esperados del DataRow: OID_LISTA_VALOR, COD_VALOR y DES_VALOR
        ''' </summary>
        ''' <param name="datos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PreencherTipoImpresora(datos As DataTable) As Clases.TipoImpresora
            If datos IsNot Nothing AndAlso datos.Rows.Count = 1 Then
                Return PreencherTipoImpresora(datos.Rows(0))
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Recupera los valores de un DataRow e retorna un tipoimpresora. Campos esperados del DataRow: OID_LISTA_VALOR, COD_VALOR y DES_VALOR
        ''' </summary>
        ''' <param name="linha"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PreencherTipoImpresora(linha As DataRow) As Clases.TipoImpresora
            Dim tipoImpresora As New Clases.TipoImpresora()
            tipoImpresora.Identificador = Util.AtribuirValorObj(linha("OID_LISTA_VALOR"), GetType(String))
            tipoImpresora.Codigo = Util.AtribuirValorObj(linha("COD_VALOR"), GetType(String))
            tipoImpresora.Descripcion = Util.AtribuirValorObj(linha("DES_VALOR"), GetType(String))
            tipoImpresora.EsDefecto = Util.AtribuirValorObj(linha("BOL_DEFECTO"), GetType(Boolean))
            Return tipoImpresora
        End Function


        ''' <summary>
        ''' Inseri lo formato del bulto
        ''' </summary>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub InserirTipoImpresoraPorBulto(identificadorRemesa As String, identificadorBulto As String, identificadorListaValor As String, usuario As String)
            ListaValor.InserirPorElemento(identificadorRemesa, identificadorBulto, Nothing, identificadorListaValor, usuario, Enumeradores.TipoListaValor.TipoImpresora.RecuperarValor())
        End Sub

        ''' <summary>
        ''' Inseri lo formato de la parcial
        ''' </summary>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="identificadorBulto"></param>
        ''' <param name="identificadorParcial"></param>
        ''' <remarks></remarks>
        Public Shared Sub InserirTipoImpresoraPorParcial(identificadorRemesa As String, identificadorBulto As String, identificadorParcial As String, identificadorListaValor As String, usuario As String)
            ListaValor.InserirPorElemento(identificadorRemesa, identificadorBulto, identificadorParcial, identificadorListaValor, usuario, Enumeradores.TipoListaValor.TipoImpresora.RecuperarValor())
        End Sub

        ''' <summary>
        ''' Exclui lo formato del bulto
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub ExcluirTipoImpresoraPorBulto(identificadorBulto As String, identificadorListaValor As String)
            ListaValor.ExcluirPorElemento(Nothing, identificadorBulto, Nothing, identificadorListaValor)
        End Sub

        ''' <summary>
        ''' Exclui lo formato de la parcial
        ''' </summary>
        ''' <param name="identificadorParcial"></param>
        ''' <remarks></remarks>
        Public Shared Sub ExcluirTipoImpresoraPorParcial(identificadorParcial As String, identificadorListaValor As String)
            ListaValor.ExcluirPorElemento(Nothing, Nothing, identificadorParcial, identificadorListaValor)
        End Sub

    End Class

End Namespace

