Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboClientes

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboClientes")>
    <XmlRoot(Namespace:="urn:GetComboClientes")>
    <Serializable()>
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase



#Region " Variáveis "

        Private _Codigo As List(Of String)
        Private _Descripcion As List(Of String)
        Private _Vigente As Nullable(Of Boolean)
        Private _TotalizadorSaldo As Nullable(Of Boolean)
        Private _TipoBanco As Nullable(Of Boolean)


#End Region

#Region " Propriedades "
        Public Property Identificador As List(Of String)

        Public Property Codigo() As List(Of String)
            Get
                Return _Codigo
            End Get
            Set(value As List(Of String))
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion() As List(Of String)
            Get
                Return _Descripcion
            End Get
            Set(value As List(Of String))
                _Descripcion = value
            End Set
        End Property

        Public Property Vigente() As Nullable(Of Boolean)
            Get
                Return _Vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _Vigente = value
            End Set
        End Property

        Public Property TotalizadorSaldo() As Nullable(Of Boolean)
            Get
                Return _TotalizadorSaldo
            End Get
            Set(value As Nullable(Of Boolean))
                _TotalizadorSaldo = value
            End Set
        End Property

        Public Property TipoBanco() As Nullable(Of Boolean)
            Get
                Return _TipoBanco
            End Get
            Set(value As Nullable(Of Boolean))
                _TipoBanco = value
            End Set
        End Property

#End Region

    End Class

End Namespace