Namespace Agrupacion.SetAgrupaciones

    ''' <summary>
    ''' Classe Agrupacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class Agrupacion

#Region "[Variáveis]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _Observacion As String
        Private _Vigente As Nullable(Of Boolean)
        Private _Efectivos As EfetivoColeccion
        Private _MediosPago As MedioPagoColeccion

#End Region

#Region "[Propriedades]"

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

        Public Property Efectivos() As EfetivoColeccion
            Get
                Return _Efectivos
            End Get
            Set(value As EfetivoColeccion)
                _Efectivos = value
            End Set
        End Property

        Public Property MediosPago() As MedioPagoColeccion
            Get
                Return _MediosPago
            End Get
            Set(value As MedioPagoColeccion)
                _MediosPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace