Imports System.Xml.Serialization
Imports System.Xml

Namespace TerminoIac.GetTerminoIac

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetTerminoIac")> _
    <XmlRoot(Namespace:="urn:GetTerminoIac")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoTermino As List(Of String)
        Private _DescripcionTermino As List(Of String)
        Private _DescripcionFormato As List(Of String)
        Private _MostrarCodigo As Nullable(Of Boolean)
        Private _VigenteTermino As Nullable(Of Boolean)



#End Region

#Region "[Propriedades]"

        Public Property CodigoTermino() As List(Of String)
            Get
                Return _CodigoTermino
            End Get
            Set(value As List(Of String))
                _CodigoTermino = value
            End Set
        End Property

        Public Property DescripcionTermino() As List(Of String)
            Get
                Return _DescripcionTermino
            End Get
            Set(value As List(Of String))
                _DescripcionTermino = value
            End Set
        End Property

        Public Property DescripcionFormato() As List(Of String)
            Get
                Return _DescripcionFormato
            End Get
            Set(value As List(Of String))
                _DescripcionFormato = value
            End Set
        End Property

        Public Property MostrarCodigo() As Nullable(Of Boolean)
            Get
                Return _MostrarCodigo
            End Get
            Set(value As Nullable(Of Boolean))
                _MostrarCodigo = value
            End Set
        End Property

        Public Property VigenteTermino() As Nullable(Of Boolean)
            Get
                Return _VigenteTermino
            End Get
            Set(value As Nullable(Of Boolean))
                _VigenteTermino = value
            End Set
        End Property

#End Region

    End Class

End Namespace