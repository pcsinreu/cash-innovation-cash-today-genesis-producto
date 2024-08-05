Imports Prosegur.Framework.Dicionario
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.AccesoDatos
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Paginacion
Imports System.Data
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Genesis
    ''' <summary>
    ''' Clase AccionSubCanal
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [henrique.ribeiro] 13/11/2013 - Criado
    ''' </history>
    Public Class SubCanal



        Public Shared Function ObtenerIdentificadorSubCanal(CodigoSubCanal As String) As String
            Return AccesoDatos.Genesis.SubCanal.ObtenerIdentificadorSubCanal(CodigoSubCanal)
        End Function




        ''' <summary>
        ''' Obtém os subcanais pelo identificador do canal.
        ''' </summary>
        ''' <param name="codigos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorIdentificadorCanal(ParamArray codigos As String()) As ObservableCollection(Of Clases.SubCanal)
            Return AccesoDatos.Genesis.SubCanal.ObtenerPorIdentificadorCanal(codigos)
        End Function

        Public Shared Function ObtenerSubCanalPorIDPS(IDPS As String) As Clases.SubCanal
            Return AccesoDatos.Genesis.SubCanal.ObtenerSubCanalPorIDPS(IDPS)
        End Function

        Public Shared Function ObtenerSubCanalPorCodigo(CodigoSubCanal As String) As Clases.SubCanal
            Return AccesoDatos.Genesis.SubCanal.ObtenerSubCanalPorCodigo(CodigoSubCanal)
        End Function

        Public Shared Function ObtenerSubCanalYCanalPorCodigo(CodigoSubCanal As String) As DataTable
            Return AccesoDatos.Genesis.SubCanal.ObtenerSubCanalYCanalPorCodigo(CodigoSubCanal)
        End Function

        Shared Function ObtenerSubCanalJSON(codigo As String, descripcion As String, identificadorPadre As String) As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Return AccesoDatos.Genesis.SubCanal.ObtenerSubCanalJSON(codigo, descripcion, identificadorPadre)
        End Function

    End Class
End Namespace