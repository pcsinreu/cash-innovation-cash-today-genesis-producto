Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Saldo.RecuperarSaldoExpuestoxDetallado

    <XmlType(Namespace:="urn:RecuperarSaldoExpuestoxDetallado")> _
    <XmlRoot(Namespace:="urn:RecuperarSaldoExpuestoxDetallado")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property CodigoCanal As String
        Public Property CodigoSubCanal As String
        Public Property CodigoPlanta As String
        Public Property CodigoDelegacion As String
        Public Property CodigosSectores As ObservableCollection(Of String)
        Public Property CodigoSectorPadre As String
        Public Property TipoDeSaldo As Enumeradores.TipoBuscaSaldo
        ''' <summary>
        ''' Se estiver True, buscará somente os bultos que não foram cuadrados
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BuscarBultosSinCuadre As Boolean
        ''' <summary>
        ''' Se estiver True, considerará apenas o cliente, o sub-cliente e pto serviço não serão
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BuscarClienteTodosNiveis As Boolean

        ''' <summary>
        ''' Indica se para recuperar as informações adicionais dos elementos..
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BuscaInfoAdicionalesElementos As Boolean
    End Class

End Namespace