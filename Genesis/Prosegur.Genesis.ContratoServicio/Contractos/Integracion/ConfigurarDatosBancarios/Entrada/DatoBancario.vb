Imports System.ComponentModel
Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Integracion.ConfigurarDatosBancarios.Entrada
    <Serializable()>
    Public Class DatoBancario
        Inherits BindableBase

        <XmlAttributeAttribute()>
        <DefaultValue(0)>
        Private _Accion As Comon.Enumeradores.AccionABM
        Private _Identificador As String
        Private _IdentificadorCliente As String
        Private _IdentificadorSubCliente As String
        Private _IdentificadorPuntoDeServicio As String
        Private _CodigoBanco As String
        Private _CodigoAgencia As String
        Private _NumeroCuenta As String
        Private _Tipo As String
        Private _NumeroDocumento As String
        Private _Titularidad As String
        Private _CodigoDivisa As String
        Private _Observaciones As String
        Private _Patron As String
        Private _CampoAdicional1 As String
        Private _CampoAdicional2 As String
        Private _CampoAdicional3 As String
        Private _CampoAdicional4 As String
        Private _CampoAdicional5 As String
        Private _CampoAdicional6 As String
        Private _CampoAdicional7 As String
        Private _CampoAdicional8 As String
        Private _Comentario As String

        Public Property Accion As Comon.Enumeradores.AccionABM
            Get
                Return _Accion
            End Get
            Set(value As Comon.Enumeradores.AccionABM)
                SetProperty(_Accion, value, "Accion")
            End Set
        End Property
        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property
        Public Property IdentificadorCliente As String
            Get
                Return _IdentificadorCliente
            End Get
            Set(value As String)
                SetProperty(_IdentificadorCliente, value, "IdentificadorCliente")
            End Set
        End Property
        Public Property IdentificadorSubCliente As String
            Get
                Return _IdentificadorSubCliente
            End Get
            Set(value As String)
                SetProperty(_IdentificadorSubCliente, value, "IdentificadorSubCliente")
            End Set
        End Property

        Public Property IdentificadorPuntoDeServicio As String
            Get
                Return _IdentificadorPuntoDeServicio
            End Get
            Set(value As String)
                SetProperty(_IdentificadorPuntoDeServicio, value, "IdentificadorPuntoDeServicio")
            End Set
        End Property
        Public Property CodigoBanco As String
            Get
                Return _CodigoBanco
            End Get
            Set(value As String)
                SetProperty(_CodigoBanco, value, "CodigoBanco")
            End Set
        End Property

        Public Property CodigoAgencia As String
            Get
                Return _CodigoAgencia
            End Get
            Set(value As String)
                SetProperty(_CodigoAgencia, value, "CodigoAgencia")
            End Set
        End Property

        Public Property NumeroCuenta As String
            Get
                Return _NumeroCuenta
            End Get
            Set(value As String)
                SetProperty(_NumeroCuenta, value, "NumeroCuenta")
            End Set
        End Property

        Public Property Tipo As String
            Get
                Return _Tipo
            End Get
            Set(value As String)
                SetProperty(_Tipo, value, "Tipo")
            End Set
        End Property


        Public Property NumeroDocumento As String
            Get
                Return _NumeroDocumento
            End Get
            Set(value As String)
                SetProperty(_NumeroDocumento, value, "NumeroDocumento")
            End Set
        End Property
        Public Property Titularidad As String
            Get
                Return _Titularidad
            End Get
            Set(value As String)
                SetProperty(_Titularidad, value, "Titularidad")
            End Set
        End Property
        Public Property CodigoDivisa As String
            Get
                Return _CodigoDivisa
            End Get
            Set(value As String)
                SetProperty(_CodigoDivisa, value, "CodigoDivisa")
            End Set
        End Property
        Public Property Observaciones As String
            Get
                Return _Observaciones
            End Get
            Set(value As String)
                SetProperty(_Observaciones, value, "Observaciones")
            End Set
        End Property
        Public Property Patron As String
            Get
                Return _Patron
            End Get
            Set(value As String)
                SetProperty(_Patron, value, "Patron")
            End Set
        End Property
        Public Property CampoAdicional1 As String
            Get
                Return _CampoAdicional1
            End Get
            Set(value As String)
                SetProperty(_CampoAdicional1, value, "CampoAdicional1")
            End Set
        End Property
        Public Property CampoAdicional2 As String
            Get
                Return _CampoAdicional2
            End Get
            Set(value As String)
                SetProperty(_CampoAdicional2, value, "CampoAdicional2")
            End Set
        End Property
        Public Property CampoAdicional3 As String
            Get
                Return _CampoAdicional3
            End Get
            Set(value As String)
                SetProperty(_CampoAdicional3, value, "CampoAdicional3")
            End Set
        End Property
        Public Property CampoAdicional4 As String
            Get
                Return _CampoAdicional4
            End Get
            Set(value As String)
                SetProperty(_CampoAdicional4, value, "CampoAdicional4")
            End Set
        End Property
        Public Property CampoAdicional5 As String
            Get
                Return _CampoAdicional5
            End Get
            Set(value As String)
                SetProperty(_CampoAdicional5, value, "CampoAdicional5")
            End Set
        End Property
        Public Property CampoAdicional6 As String
            Get
                Return _CampoAdicional6
            End Get
            Set(value As String)
                SetProperty(_CampoAdicional6, value, "CampoAdicional6")
            End Set
        End Property
        Public Property CampoAdicional7 As String
            Get
                Return _CampoAdicional7
            End Get
            Set(value As String)
                SetProperty(_CampoAdicional7, value, "CampoAdicional7")
            End Set
        End Property

        Public Property CampoAdicional8 As String
            Get
                Return _CampoAdicional8
            End Get
            Set(value As String)
                SetProperty(_CampoAdicional8, value, "CampoAdicional8")
            End Set
        End Property
        Public Property Comentario As String
            Get
                Return _Comentario
            End Get
            Set(value As String)
                SetProperty(_Comentario, value, "Comentario")
            End Set
        End Property

    End Class
End Namespace

