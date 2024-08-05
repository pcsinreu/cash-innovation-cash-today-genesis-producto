Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboSectores

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
   
    <XmlType(Namespace:="urn:GetComboSectores")> _
    <XmlRoot(Namespace:="urn:GetComboSectores")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region " Variáveis "

        Private _Codigo As String
        Private _Descripcion As String
        Private _CodigoPlanta As String
        Private _CodigoDelegacion As String

#End Region

#Region " Propriedades "

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property


        Public Property CodigoPlanta() As String
            Get
                Return _CodigoPlanta
            End Get
            Set(value As String)
                _CodigoPlanta = value
            End Set
        End Property

#End Region

    End Class

End Namespace