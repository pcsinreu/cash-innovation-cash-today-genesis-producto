Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper

Namespace Clases
    <Serializable()>
    Public Class CamposExtrasDeIAC
        Private _IdentificadorIAC As String
        Public Property IdentificadorIAC() As String
            Get
                Return _IdentificadorIAC
            End Get
            Set(ByVal value As String)
                _IdentificadorIAC = value
            End Set
        End Property

        Private _codigoIAC As String
        Public Property CodigoIAC() As String
            Get
                Return _codigoIAC
            End Get
            Set(ByVal value As String)
                _codigoIAC = value
            End Set
        End Property

        Private _camposExtras As List(Of Clases.Termino)
        Public Property CamposExtras() As List(Of Clases.Termino)
            Get
                Return _camposExtras
            End Get
            Set(ByVal value As List(Of Clases.Termino))
                _camposExtras = value
            End Set
        End Property
    End Class
End Namespace

