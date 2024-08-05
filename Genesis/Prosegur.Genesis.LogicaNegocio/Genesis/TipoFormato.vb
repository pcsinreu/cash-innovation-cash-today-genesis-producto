Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Data

Namespace Genesis

    ''' <summary>
    ''' Classe TipoFormato
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TipoFormato

        ''' <summary>
        ''' Recupera los valores de un DataTable e retorna un TipoFormato. Campos esperados del DataRow: OID_LISTA_VALOR, COD_VALOR y DES_VALOR
        ''' </summary>
        ''' <param name="tdTipoFormatos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function cargarTipoFormato(tdTipoFormatos As DataTable) As ObservableCollection(Of Clases.TipoFormato)

            Dim objTipoFormatos As New ObservableCollection(Of Clases.TipoFormato)

            If tdTipoFormatos IsNot Nothing AndAlso tdTipoFormatos.Rows.Count > 0 Then

                For Each objRow As DataRow In tdTipoFormatos.Rows

                    Dim objTipoFormato As New Clases.TipoFormato
                    objTipoFormato.Identificador = Util.AtribuirValorObj(objRow("OID_LISTA_VALOR"), GetType(String))
                    objTipoFormato.Codigo = Util.AtribuirValorObj(objRow("COD_VALOR"), GetType(String))
                    objTipoFormato.Descripcion = Util.AtribuirValorObj(objRow("DES_VALOR"), GetType(String))
                    objTipoFormato.EsDefecto = Util.AtribuirValorObj(objRow("BOL_DEFECTO"), GetType(Boolean))

                    objTipoFormatos.Add(objTipoFormato)
                Next
            End If

            Return objTipoFormatos
        End Function

        Public Shared Function ObtenerTipoFormatoPorElemento(identificadorRemessa As String, identificadorBulto As String, identificadorParcial As String) As Clases.TipoFormato

            Dim objTipoFormato As New Clases.TipoFormato

            Dim tdTipoFormatos As DataTable = AccesoDatos.Genesis.ListaValor.ObtenerValorPorElemento(Enumeradores.TipoListaValor.TipoFormato, identificadorRemessa, identificadorBulto, identificadorParcial)

            Dim objTipoFormatos As ObservableCollection(Of Clases.TipoFormato) = cargarTipoFormato(tdTipoFormatos)
            If objTipoFormatos IsNot Nothing AndAlso objTipoFormatos.Count > 0 Then
                objTipoFormato = objTipoFormatos.FirstOrDefault
            Else
                Return Nothing
            End If

            Return objTipoFormato
        End Function

        ''' <summary>
        ''' Inseri lo formato del bulto
        ''' </summary>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub InserirTipoFormatoPorBulto(identificadorRemesa As String, identificadorBulto As String, identificadorListaValor As String, usuario As String,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)
            AccesoDatos.Genesis.ListaValor.InserirPorElemento(identificadorRemesa, identificadorBulto, Nothing, identificadorListaValor, usuario, Enumeradores.TipoListaValor.TipoFormato.RecuperarValor(), _transacion)
        End Sub

        ''' <summary>
        ''' Inseri lo formato de la parcial
        ''' </summary>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="identificadorBulto"></param>
        ''' <param name="identificadorParcial"></param>
        ''' <remarks></remarks>
        Public Shared Sub InserirTipoFormatoPorParcial(identificadorRemesa As String, identificadorBulto As String, identificadorParcial As String, identificadorListaValor As String, usuario As String,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)
            AccesoDatos.Genesis.ListaValor.InserirPorElemento(identificadorRemesa, identificadorBulto, identificadorParcial, identificadorListaValor, usuario, Enumeradores.TipoListaValor.TipoFormato.RecuperarValor(), _transacion)
        End Sub

        ''' <summary>
        ''' Exclui lo formato del bulto
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub ExcluirTipoFormatoPorBulto(identificadorBulto As String, identificadorListaValor As String)
            AccesoDatos.Genesis.ListaValor.ExcluirPorElemento(Nothing, identificadorBulto, Nothing, identificadorListaValor)
        End Sub

        ''' <summary>
        ''' Exclui lo formato de la parcial
        ''' </summary>
        ''' <param name="identificadorParcial"></param>
        ''' <remarks></remarks>
        Public Shared Sub ExcluirTipoFormatoPorParcial(identificadorParcial As String, identificadorListaValor As String)
            AccesoDatos.Genesis.ListaValor.ExcluirPorElemento(Nothing, Nothing, identificadorParcial, identificadorListaValor)
        End Sub

        ''' <summary>
        ''' Retorna os tipos de formato
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTiposFormato() As ObservableCollection(Of Clases.TipoFormato)
            Return AccesoDatos.Genesis.TipoFormato.RecuperarTiposFormato()
        End Function

        Public Shared Function RecuperarPorTipoComModulo(peticion As ContractoServicio.TipoFormato.RecuperarPorTipoComModulo.RecuperarPorTipoComModuloPeticion) As ObservableCollection(Of Clases.TipoFormato)
            Return AccesoDatos.Genesis.TipoFormato.RecuperarTiposFormatoComModulos(peticion.Codigos)
        End Function


        Public Shared Function ObtenerTipoFormatoPorIdentificador(identificador As String) As Clases.TipoFormato
            Return AccesoDatos.Genesis.TipoFormato.RecuperarTipoFormatoPorIdentificador(identificador)
        End Function

        Public Shared Function ObtenerTipoFormatoPorCodigo(codigo As String) As Clases.TipoFormato
            Return AccesoDatos.Genesis.TipoFormato.RecuperarTipoFormatoPorCodigo(codigo)
        End Function

        Public Shared Function ObtenerTipoServicioPorElemento(identificadorRemessa As String, identificadorBulto As String, identificadorParcial As String) As Clases.TipoFormato
            Return AccesoDatos.Genesis.TipoFormato.RecuperarPorElemento(identificadorRemessa, identificadorBulto, identificadorParcial)
        End Function

    End Class

End Namespace