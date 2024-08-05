Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Integracion.ConfigurarDatosBancarios
    <XmlType(Namespace:="urn:ConfigurarDatosBancarios")>
    <XmlRoot(Namespace:="urn:ConfigurarDatosBancarios")>
    <Serializable()>
    Public Class Peticion
        Inherits BindableBase

        Private _DatosBancarios As List(Of Entrada.DatoBancario)
        Private _Cultura As String
        Private _Usuario As String
        Private _CodigoPais As String

        Public Property DatosBancarios As List(Of Entrada.DatoBancario)
            Get
                Return _DatosBancarios
            End Get
            Set(value As List(Of Entrada.DatoBancario))
                SetProperty(_DatosBancarios, value, "DatosBancarios")
            End Set
        End Property


        Public Property Cultura As String
            Get
                Return _Cultura
            End Get
            Set(value As String)
                SetProperty(_Cultura, value, "Cultura")
            End Set
        End Property

        Public Property Usuario As String
            Get
                Return _Usuario
            End Get
            Set(value As String)
                SetProperty(_Usuario, value, "Usuario")
            End Set
        End Property

        Public Property CodigoPais As String
            Get
                Return _CodigoPais
            End Get
            Set(value As String)
                SetProperty(_CodigoPais, value, "CodigoPais")
            End Set
        End Property

    End Class
End Namespace

