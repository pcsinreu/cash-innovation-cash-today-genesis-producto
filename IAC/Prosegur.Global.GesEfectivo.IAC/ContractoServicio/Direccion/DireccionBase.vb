Imports System.Xml.Serialization

Namespace Direccion

    <Serializable()>
    Public Class DireccionBase

#Region "[VARIAVEIS]"

        Private _oidDireccion As String
        Private _desPais As String
        Private _desProvincia As String
        Private _desCiudad As String
        Private _desNumeroTelefono As String
        Private _desEmail As String
        Private _codFiscal As String
        Private _codPostal As String
        Private _desDireccionLinea1 As String
        Private _desDireccionLinea2 As String
        Private _desCampoAdicional1 As String
        Private _desCampoAdicional2 As String
        Private _desCampoAdicional3 As String
        Private _desCategoriaAdicional1 As String
        Private _desCategoriaAdicional2 As String
        Private _desCategoriaAdicional3 As String
        Private _bolBaja As Boolean


#End Region

#Region "[PROPRIEDADES]"

        Public Property oidDireccion() As String
            Get
                Return _oidDireccion
            End Get
            Set(value As String)
                _oidDireccion = value
            End Set
        End Property

        Public Property desPais() As String
            Get
                Return _desPais
            End Get
            Set(value As String)
                _desPais = value
            End Set
        End Property

        Public Property desProvincia() As String
            Get
                Return _desProvincia
            End Get
            Set(value As String)
                _desProvincia = value
            End Set
        End Property

        Public Property desCiudad() As String
            Get
                Return _desCiudad
            End Get
            Set(value As String)
                _desCiudad = value
            End Set
        End Property

        Public Property desNumeroTelefono() As String
            Get
                Return _desNumeroTelefono
            End Get
            Set(value As String)
                _desNumeroTelefono = value
            End Set
        End Property

        Public Property desEmail() As String
            Get
                Return _desEmail
            End Get
            Set(value As String)
                _desEmail = value
            End Set
        End Property

        Public Property codFiscal() As String
            Get
                Return _codFiscal
            End Get
            Set(value As String)
                _codFiscal = value
            End Set
        End Property

        Public Property codPostal() As String
            Get
                Return _codPostal
            End Get
            Set(value As String)
                _codPostal = value
            End Set
        End Property

        Public Property desDireccionLinea1() As String
            Get
                Return _desDireccionLinea1
            End Get
            Set(value As String)
                _desDireccionLinea1 = value
            End Set
        End Property

        Public Property desDireccionLinea2() As String
            Get
                Return _desDireccionLinea2
            End Get
            Set(value As String)
                _desDireccionLinea2 = value
            End Set
        End Property

        Public Property desCampoAdicional1() As String
            Get
                Return _desCampoAdicional1
            End Get
            Set(value As String)
                _desCampoAdicional1 = value
            End Set
        End Property

        Public Property desCampoAdicional2() As String
            Get
                Return _desCampoAdicional2
            End Get
            Set(value As String)
                _desCampoAdicional2 = value
            End Set
        End Property

        Public Property desCampoAdicional3() As String
            Get
                Return _desCampoAdicional3
            End Get
            Set(value As String)
                _desCampoAdicional3 = value
            End Set
        End Property

        Public Property desCategoriaAdicional1() As String
            Get
                Return _desCategoriaAdicional1
            End Get
            Set(value As String)
                _desCategoriaAdicional1 = value
            End Set
        End Property

        Public Property desCategoriaAdicional2() As String
            Get
                Return _desCategoriaAdicional2
            End Get
            Set(value As String)
                _desCategoriaAdicional2 = value
            End Set
        End Property

        Public Property desCategoriaAdicional3() As String
            Get
                Return _desCategoriaAdicional3
            End Get
            Set(value As String)
                _desCategoriaAdicional3 = value
            End Set
        End Property

        '<XmlIgnore()> _
        Public Property bolBaja() As Boolean
            Get
                Return _bolBaja
            End Get
            Set(value As Boolean)
                _bolBaja = value
            End Set
        End Property

#End Region

    End Class

End Namespace
