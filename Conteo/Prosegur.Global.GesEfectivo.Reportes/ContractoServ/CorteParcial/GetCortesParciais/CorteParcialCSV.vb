Namespace CorteParcial.GetCortesParciais

    Public Class CorteParcialCSV

#Region " Variáveis "

        Private _Recuento As String = String.Empty
        Private _Fecha As DateTime = DateTime.MinValue
        Private _Letra As String = String.Empty
        Private _F22 As String = String.Empty
        Private _OidRemesaOri As String = String.Empty
        Private _CodSubCliente As String = String.Empty
        Private _Estacion As String = String.Empty
        Private _DescricionEstacion As String = String.Empty
        Private _MedioPago As String = String.Empty
        Private _DescricionMedioPago As String = String.Empty
        Private _Divisa As String = String.Empty
        Private _DescricionDivisa As String = String.Empty
        Private _DeclaradoRemesa As Decimal = 0
        Private _DeclaradoBultoSuMaxRemesa As Decimal = 0
        Private _DeclaradoParcialSuMaxRemesa As Decimal = 0
        Private _IngresadoSobre As Decimal = 0
        Private _Recontado As Decimal = 0
        Private _Observaciones As String = String.Empty
        Private _Remesa As String = String.Empty
        Private _Falsos As ContractoServ.CorteParcial.GetCortesParciais.FalsoColeccion

#End Region

#Region " Propriedades "

        Public Property Recuento() As String
            Get
                Return _Recuento
            End Get
            Set(value As String)
                _Recuento = value
            End Set
        End Property

        Public Property Fecha() As DateTime
            Get
                Return _Fecha
            End Get
            Set(value As DateTime)
                _Fecha = value
            End Set
        End Property

        Public Property Letra() As String
            Get
                Return _Letra
            End Get
            Set(value As String)
                _Letra = value
            End Set
        End Property

        Public Property F22() As String
            Get
                Return _F22
            End Get
            Set(value As String)
                _F22 = value
            End Set
        End Property

        Public Property OidRemesaOri() As String
            Get
                Return _OidRemesaOri
            End Get
            Set(value As String)
                _OidRemesaOri = value
            End Set
        End Property

        Public Property CodSubCliente() As String
            Get
                Return _CodSubCliente
            End Get
            Set(value As String)
                _CodSubCliente = value
            End Set
        End Property

        Public Property Estacion() As String
            Get
                Return _Estacion
            End Get
            Set(value As String)
                _Estacion = value
            End Set
        End Property

        Public Property DescricionEstacion() As String
            Get
                Return _DescricionEstacion
            End Get
            Set(value As String)
                _DescricionEstacion = value
            End Set
        End Property

        Public Property DescricionMedioPago() As String
            Get
                Return _DescricionMedioPago
            End Get
            Set(value As String)
                _DescricionMedioPago = value
            End Set
        End Property

        Public Property MedioPago() As String
            Get
                Return _MedioPago
            End Get
            Set(value As String)
                _MedioPago = value
            End Set
        End Property

        Public Property Divisa() As String
            Get
                Return _Divisa
            End Get
            Set(value As String)
                _Divisa = value
            End Set
        End Property

        Public Property DescricionDivisa() As String
            Get
                Return _DescricionDivisa
            End Get
            Set(value As String)
                _DescricionDivisa = value
            End Set
        End Property

        Public Property DeclaradoRemesa() As Decimal
            Get
                Return _DeclaradoRemesa
            End Get
            Set(value As Decimal)
                _DeclaradoRemesa = value
            End Set
        End Property

        Public Property DeclaradoBultoSuMaxRemesa() As Decimal
            Get
                Return _DeclaradoBultoSuMaxRemesa
            End Get
            Set(value As Decimal)
                _DeclaradoBultoSuMaxRemesa = value
            End Set
        End Property

        Public Property DeclaradoParcialSuMaxRemesa() As Decimal
            Get
                Return _DeclaradoParcialSuMaxRemesa
            End Get
            Set(value As Decimal)
                _DeclaradoParcialSuMaxRemesa = value
            End Set
        End Property

        Public Property IngresadoSobre() As Decimal
            Get
                Return _IngresadoSobre
            End Get
            Set(value As Decimal)
                _IngresadoSobre = value
            End Set
        End Property

        Public Property Recontado() As Decimal
            Get
                Return _Recontado
            End Get
            Set(value As Decimal)
                _Recontado = value
            End Set
        End Property

        Public Property Observaciones() As String
            Get
                Return _Observaciones
            End Get
            Set(value As String)
                _Observaciones = value
            End Set
        End Property

        Public Property Remesa() As String
            Get
                Return _Remesa
            End Get
            Set(value As String)
                _Remesa = value
            End Set
        End Property

        Public Property Falsos() As ContractoServ.CorteParcial.GetCortesParciais.FalsoColeccion
            Get
                Return _Falsos
            End Get
            Set(value As ContractoServ.CorteParcial.GetCortesParciais.FalsoColeccion)
                _Falsos = value
            End Set
        End Property

#End Region

    End Class

End Namespace
