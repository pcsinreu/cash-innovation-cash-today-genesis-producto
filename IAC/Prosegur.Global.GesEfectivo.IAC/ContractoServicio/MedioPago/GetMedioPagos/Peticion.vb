Imports System.Xml.Serialization
Imports System.Xml

Namespace MedioPago.GetMedioPagos

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 19/02/2009 Criado
    ''' [blcosta] 28/06/2010  modificado
    ''' </history>
    <XmlType(Namespace:="urn:GetMedioPagos")> _
    <XmlRoot(Namespace:="urn:GetMedioPagos")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        'Medio Pago
        Private _Codigo As List(Of String)
        Private _Descripcion As List(Of String)
        Private _EsMercancia As Boolean
        Private _Vigente As Nullable(Of Boolean)
        'Tipo Medio Pago
        Private _CodigoTipoMedioPago As List(Of String)

        'Divisa
        Private _CodigoDivisa As List(Of String)
        Private _DescripcionDivisa As List(Of String)
   
        




#End Region

#Region "[Propriedades]"

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

        Public Property EsMercancia() As Boolean
            Get
                Return _EsMercancia
            End Get
            Set(value As Boolean)
                _EsMercancia = value
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

        Public Property CodigoTipoMedioPago() As List(Of String)
            Get
                Return _CodigoTipoMedioPago
            End Get
            Set(value As List(Of String))
                _CodigoTipoMedioPago = value
            End Set
        End Property

        Public Property CodigoDivisa() As List(Of String)
            Get
                Return _CodigoDivisa
            End Get
            Set(value As List(Of String))
                _CodigoDivisa = value
            End Set
        End Property
        Public Property DescripcionDivisa() As List(Of String)
            Get
                Return _DescripcionDivisa
            End Get
            Set(value As List(Of String))
                _DescripcionDivisa = value
            End Set
        End Property

 


#End Region

    End Class

End Namespace