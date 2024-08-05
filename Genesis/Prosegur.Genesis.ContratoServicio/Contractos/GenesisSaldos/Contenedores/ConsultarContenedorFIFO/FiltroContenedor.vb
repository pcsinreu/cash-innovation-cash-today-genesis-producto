Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedorFIFO

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <Serializable()>
    Public Class FiltroContenedor
        Inherits BindableBase

#Region "[PROPRIEDADES]"
        
        Private _Contenedor As Comon.Contenedor
        Public Property Contenedor() As Comon.Contenedor
            Get
                Return _Contenedor
            End Get
            Set(value As Comon.Contenedor)
                MyBase.SetProperty(_Contenedor, value, "Contenedor")
            End Set
        End Property

        Private _Precinto As String
        Public Property Precinto() As String
            Get
                Return _Precinto
            End Get
            Set(value As String)
                MyBase.SetProperty(_Precinto, value, "Precinto")
            End Set
        End Property

        Private _Divisa As Clases.Divisa
        Public Property Divisa() As Clases.Divisa
            Get
                Return _Divisa
            End Get
            Set(value As Clases.Divisa)
                MyBase.SetProperty(_Divisa, value, "Divisa")
            End Set
        End Property

        Private _Denominacion As Clases.Denominacion
        Public Property Denominacion() As Clases.Denominacion
            Get
                Return _Denominacion
            End Get
            Set(value As Clases.Denominacion)
                MyBase.SetProperty(_Denominacion, value, "_Denominacion")
            End Set
        End Property

        Private _Importe As Decimal
        Public Property Importe As Decimal
            Get
                Return _Importe
            End Get
            Set(value As Decimal)
                MyBase.SetProperty(_Importe, value, "Importe")
            End Set
        End Property

#End Region

    End Class

End Namespace
