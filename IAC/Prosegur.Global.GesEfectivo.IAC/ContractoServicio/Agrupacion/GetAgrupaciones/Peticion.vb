Imports System.Xml.Serialization
Imports System.Xml

Namespace Agrupacion.GetAgrupaciones

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetAgrupaciones")> _
    <XmlRoot(Namespace:="urn:GetAgrupaciones")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoAgrupacion As List(Of String)
        Private _DescripcionAgrupacion As List(Of String)
        Private _VigenteAgrupacion As Boolean
        Private _DivisaEfectivo As List(Of String)
        Private _DivisaTicket As List(Of String)
        Private _CodigoTicket As List(Of String)
        Private _DivisaOtroValor As List(Of String)
        Private _CodigoOtroValor As List(Of String)
        Private _DivisaCheque As List(Of String)
        Private _CodigoCheque As List(Of String)

#End Region

#Region "[Propriedades]"

        Public Property CodigoAgrupacion() As List(Of String)
            Get
                Return _CodigoAgrupacion
            End Get
            Set(value As List(Of String))
                _CodigoAgrupacion = value
            End Set
        End Property

        Public Property DescripcionAgrupacion() As List(Of String)
            Get
                Return _DescripcionAgrupacion
            End Get
            Set(value As List(Of String))
                _DescripcionAgrupacion = value
            End Set
        End Property

        Public Property VigenteAgrupacion() As Boolean
            Get
                Return _VigenteAgrupacion
            End Get
            Set(value As Boolean)
                _VigenteAgrupacion = value
            End Set
        End Property

        Public Property DivisaEfectivo() As List(Of String)
            Get
                Return _DivisaEfectivo
            End Get
            Set(value As List(Of String))
                _DivisaEfectivo = value
            End Set
        End Property

        Public Property DivisaTicket() As List(Of String)
            Get
                Return _DivisaTicket
            End Get
            Set(value As List(Of String))
                _DivisaTicket = value
            End Set
        End Property

        Public Property CodigoTicket() As List(Of String)
            Get
                Return _CodigoTicket
            End Get
            Set(value As List(Of String))
                _CodigoTicket = value
            End Set
        End Property

        Public Property DivisaOtroValor() As List(Of String)
            Get
                Return _DivisaOtroValor
            End Get
            Set(value As List(Of String))
                _DivisaOtroValor = value
            End Set
        End Property

        Public Property CodigoOtroValor() As List(Of String)
            Get
                Return _CodigoOtroValor
            End Get
            Set(value As List(Of String))
                _CodigoOtroValor = value
            End Set
        End Property

        Public Property DivisaCheque() As List(Of String)
            Get
                Return _DivisaCheque
            End Get
            Set(value As List(Of String))
                _DivisaCheque = value
            End Set
        End Property

        Public Property CodigoCheque() As List(Of String)
            Get
                Return _CodigoCheque
            End Get
            Set(value As List(Of String))
                _CodigoCheque = value
            End Set
        End Property

#End Region

    End Class

End Namespace