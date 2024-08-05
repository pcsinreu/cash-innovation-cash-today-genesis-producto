Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Clase AccionPlanta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [henrique.ribeiro] 11/10/2013 - Criado
    ''' </history>
    Public Class Planta
        ''' <summary>
        ''' Método que recupera as plantas pelos seus respectivos codigos.
        ''' </summary>
        ''' <param name="codigosPlantas">Codigos a serem pesquisados</param>
        ''' <param name="codigoDelegacion">Codigo da delegação da planta</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorCodigos(codigoDelegacion As String, ParamArray codigosPlantas As String()) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Planta)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Planta.ObtenerPorCodigos(codigoDelegacion, codigosPlantas)
        End Function

        ''' <summary>
        ''' Método que recupera a planta pelo seu OID.
        ''' </summary>
        ''' <param name="oid">OID a ser pesquisado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorOid(oid As String) As Prosegur.Genesis.Comon.Clases.Planta
            Return AccesoDatos.Genesis.Planta.ObtenerPorOid(oid)
        End Function

        Shared Function ObtenerPlantaJSON(codigo As String, descripcion As String, identificadorPadre As String) As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Return AccesoDatos.Genesis.Planta.ObtenerPlantaJSON(codigo, descripcion, identificadorPadre)
        End Function

        Shared Function ObtenerCodigoPlanta(codigoDelegacion As String, codigoSector As String) As String
            Return AccesoDatos.Genesis.Planta.ObtenerCodigoPlanta(codigoDelegacion, codigoSector)
        End Function

    End Class
End Namespace
