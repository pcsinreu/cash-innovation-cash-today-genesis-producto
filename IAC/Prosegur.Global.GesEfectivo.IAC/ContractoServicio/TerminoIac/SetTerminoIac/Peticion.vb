Imports System.Xml.Serialization
Imports System.Xml

Namespace TerminoIac.SetTerminoIac

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetTerminoIac")> _
    <XmlRoot(Namespace:="urn:SetTerminoIac")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        'Termino
        Private _Codigo As String
        Private _Descripcion As String
        Private _Observacion As String
        Private _Vigente As Nullable(Of Boolean)
        Private _Longitud As Nullable(Of Integer)
        Private _MostrarCodigo As Nullable(Of Boolean)
        Private _AdmiteValoresPosibles As Nullable(Of Boolean)
        Private _AceptarDigitacion As Boolean

        'Formato
        Private _CodigoFormato As String        

        'Mascara
        Private _CodigoMascara As String        

        'Algoritmo
        Private _CodigoAlgoritmo As String

        'Informação
        Private _CodigoUsuario As String

#End Region

#Region "[Propriedades]"

        Public Property AceptarDigitacion() As Boolean
            Get
                Return _AceptarDigitacion
            End Get
            Set(value As Boolean)
                _AceptarDigitacion = value
            End Set
        End Property

        'Termino
        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
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

        Public Property Observacion() As String
            Get
                Return _Observacion
            End Get
            Set(value As String)
                _Observacion = value
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
        Public Property Longitud() As Nullable(Of Integer)
            Get
                Return _Longitud
            End Get
            Set(value As Nullable(Of Integer))
                _Longitud = value
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

        Public Property AdmiteValoresPosibles() As Nullable(Of Boolean)
            Get
                Return _AdmiteValoresPosibles
            End Get
            Set(value As Nullable(Of Boolean))
                _AdmiteValoresPosibles = value
            End Set
        End Property


        'Formato
        Public Property CodigoFormato() As String
            Get
                Return _CodigoFormato
            End Get
            Set(value As String)
                _CodigoFormato = value
            End Set
        End Property

       

        'Mascara
        Public Property CodigoMascara() As String
            Get
                Return _CodigoMascara
            End Get
            Set(value As String)
                _CodigoMascara = value
            End Set
        End Property

       

        'Algoritmo
        Public Property CodigoAlgoritmo() As String
            Get
                Return _CodigoAlgoritmo
            End Get
            Set(value As String)
                _CodigoAlgoritmo = value
            End Set
        End Property

        'Informação
        Public Property CodigoUsuario() As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

        

#End Region

    End Class

End Namespace