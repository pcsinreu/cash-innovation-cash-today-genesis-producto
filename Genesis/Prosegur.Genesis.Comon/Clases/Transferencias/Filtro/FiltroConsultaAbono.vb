Imports System.Collections.ObjectModel

Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de Abono
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroConsultaAbono
        Inherits BindableBase

        Public Property IdentificadorDelegacion As String = String.Empty
        Public Property codigoDelegacion As String = String.Empty
        Public Property IdentificadorBanco As String = String.Empty
        Public Property IdentificadorCliente As String = String.Empty
        Public Property FechaAbonoDesde As Date
        Public Property FechaAbonoHasta As Date
        Public Property CodigoEstado As String = String.Empty
        Public Property IdentificadoresDivisas As List(Of String)
        Public Property CodigoTipo As String
        Public Property NumeroExterno As String
        Public Property Codigo As String = String.Empty
        Public Property IdentificadorAbono As String = String.Empty

    End Class
End Namespace