Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Data

Namespace Genesis
    Public Class TipoServicio

        ''' <summary>
        ''' Recupera los valores de un DataRow e retorna un tipoServicio. Campos esperados del DataRow: OID_LISTA_VALOR, COD_VALOR y DES_VALOR
        ''' </summary>
        ''' <param name="tdTipoServicios"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function cargarTipoServicio(tdTipoServicios As DataTable) As ObservableCollection(Of Clases.TipoServicio)

            Dim objTipoServicios As New ObservableCollection(Of Clases.TipoServicio)

            If tdTipoServicios IsNot Nothing AndAlso tdTipoServicios.Rows.Count > 0 Then

                For Each objRow As DataRow In tdTipoServicios.Rows

                    Dim objTipoServicio As New Clases.TipoServicio
                    objTipoServicio.Identificador = Util.AtribuirValorObj(objRow("OID_LISTA_VALOR"), GetType(String))
                    objTipoServicio.Codigo = Util.AtribuirValorObj(objRow("COD_VALOR"), GetType(String))
                    objTipoServicio.Descripcion = Util.AtribuirValorObj(objRow("DES_VALOR"), GetType(String))
                    objTipoServicio.EsDefecto = Util.AtribuirValorObj(objRow("BOL_DEFECTO"), GetType(String))

                    objTipoServicios.Add(objTipoServicio)
                Next
            End If

            Return objTipoServicios
        End Function

        Public Shared Function ObtenerTipoServicioPorElemento(identificadorRemessa As String, identificadorBulto As String) As Clases.TipoServicio

            Dim objTipoServicio As New Clases.TipoServicio

            Dim tdTipoServicios As DataTable = AccesoDatos.Genesis.ListaValor.ObtenerValorPorElemento(Enumeradores.TipoListaValor.TipoServicio, identificadorRemessa, identificadorBulto, Nothing)

            Dim objTipoServicios As ObservableCollection(Of Clases.TipoServicio) = cargarTipoServicio(tdTipoServicios)
            If objTipoServicios IsNot Nothing AndAlso objTipoServicios.Count > 0 Then
                objTipoServicio = objTipoServicios.FirstOrDefault
            Else
                Return Nothing
            End If

            Return objTipoServicio
        End Function







        ''' <summary>
        ''' Recuperar os tipos de servicios
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTipoServicios() As observablecollection(Of Prosegur.Genesis.Comon.Clases.TipoServicio)
            Return Prosegur.Genesis.AccesoDatos.Genesis.TipoServicio.RecuperarTiposServicio()
        End Function

        Public Shared Function ObtenerTipoServicioPorIdentificador(identificador As String) As Prosegur.Genesis.Comon.Clases.TipoServicio
            Return AccesoDatos.Genesis.TipoServicio.RecuperarTipoServicioPorIdentificador(identificador)
        End Function

        Public Shared Function ObtenerTipoServicioPorCodigo(codigo As String) As Prosegur.Genesis.Comon.Clases.TipoServicio
            Return AccesoDatos.Genesis.TipoServicio.RecuperarTipoServicioPorCodigo(codigo)
        End Function

        Public Shared Function ObtenerTipoServicioPorBulto(identificadorRemessa As String, identificadorBulto As String) As Prosegur.Genesis.Comon.Clases.TipoServicio
            Return AccesoDatos.Genesis.TipoServicio.RecuperarPorElemento(identificadorRemessa, identificadorBulto)
        End Function

    End Class
End Namespace

