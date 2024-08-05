Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.GenesisSaldos.Saldo.RecuperarSaldoExpuestoxDetallado

    <XmlType(Namespace:="urn:RecuperarSaldoExpuestoxDetallado")> _
    <XmlRoot(Namespace:="urn:RecuperarSaldoExpuestoxDetallado")> _
    <Serializable()>
    Public Class InformacionesAdicionales
        Inherits BindableBase

        Private _IdentificadorElemento As String
        Private _SectorOrigenElemento As Clases.Sector
        Private _EstadosDocumento As ObservableCollection(Of Enumeradores.EstadoDocumento)
        Private _EstadosDocumentoElemento As ObservableCollection(Of Enumeradores.EstadoDocumentoElemento)

#Region "PROPRIEDADES"
        Public Property IdentificadorElemento As String
            Get
                Return _IdentificadorElemento
            End Get
            Set(value As String)
                SetProperty(_IdentificadorElemento, value, "IdentificadorElemento")
            End Set
        End Property

        Public Property SectorOrigenElemento As Clases.Sector
            Get
                Return _SectorOrigenElemento
            End Get
            Set(value As Clases.Sector)
                SetProperty(_SectorOrigenElemento, value, "SectorOrigenElemento")
            End Set
        End Property

        Public Property EstadosDocumento As ObservableCollection(Of Enumeradores.EstadoDocumento)
            Get
                Return _EstadosDocumento
            End Get
            Set(value As ObservableCollection(Of Enumeradores.EstadoDocumento))
                SetProperty(_EstadosDocumento, value, "EstadosDocumento")
            End Set
        End Property

        Public Property EstadosDocumentoElemento As ObservableCollection(Of Enumeradores.EstadoDocumentoElemento)
            Get
                Return _EstadosDocumentoElemento
            End Get
            Set(value As ObservableCollection(Of Enumeradores.EstadoDocumentoElemento))
                SetProperty(_EstadosDocumentoElemento, value, "EstadosDocumentoElemento")
            End Set
        End Property
#End Region

    End Class
End Namespace
