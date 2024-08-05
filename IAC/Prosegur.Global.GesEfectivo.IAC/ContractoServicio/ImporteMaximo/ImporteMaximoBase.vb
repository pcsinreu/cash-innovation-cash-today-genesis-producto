Namespace ImporteMaximo

    <Serializable()> _
    Public Class ImporteMaximoBase

#Region "[VARIAVEIS]"

        Private _oidImporteMaximo As String
        Private _codIdentificador As String
        Private _oidPlanta As String
        Private _Cliente As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Private _CodigoImporte As String
        Private _DescricaoImporte As String
        Private _ValorMaximo As String
        Private _BolVigente As Boolean
        Private _Canal As ContractoServicio.Utilidad.GetComboCanales.Canal
        Private _Subcanal As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
        Private _Sector As ContractoServicio.Utilidad.GetComboSectores.Sector1
        Private _Divisa As ContractoServicio.Utilidad.GetComboDivisas.Divisa


#End Region

#Region "[PROPRIEDADES]"

        Public Property OidImporteMaximo() As String
            Get
                Return _oidImporteMaximo
            End Get
            Set(value As String)
                _oidImporteMaximo = value
            End Set
        End Property

        Public Property CodIdentificador() As String
            Get
                Return _codIdentificador
            End Get
            Set(value As String)
                _codIdentificador = value
            End Set
        End Property

        Public Property CodigoImporte() As String
            Get
                Return _CodigoImporte
            End Get
            Set(value As String)
                _CodigoImporte = value
            End Set
        End Property

        Public Property DescricaoImporte() As String
            Get
                Return _DescricaoImporte
            End Get
            Set(value As String)
                _DescricaoImporte = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _BolVigente
            End Get
            Set(value As Boolean)
                _BolVigente = value
            End Set
        End Property

        Public Property Cliente As ContractoServicio.Utilidad.GetComboClientes.Cliente
            Get
                Return _Cliente
            End Get
            Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
                _Cliente = value
            End Set
        End Property


        Public Property Canal As ContractoServicio.Utilidad.GetComboCanales.Canal
            Get
                Return _Canal
            End Get
            Set(value As ContractoServicio.Utilidad.GetComboCanales.Canal)
                _Canal = value
            End Set
        End Property

        Public Property SubCanal As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
            Get
                Return _Subcanal
            End Get
            Set(value As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal)
                _Subcanal = value
            End Set
        End Property

        Public Property oidPlanta As String
            Get
                Return _oidPlanta
            End Get
            Set(value As String)
                _oidPlanta = value
            End Set
        End Property

        Public Property Divisa As ContractoServicio.Utilidad.GetComboDivisas.Divisa
            Get
                Return _Divisa
            End Get
            Set(value As ContractoServicio.Utilidad.GetComboDivisas.Divisa)
                _Divisa = value
            End Set
        End Property

        Public Property Sector As ContractoServicio.Utilidad.GetComboSectores.Sector1
            Get
                Return _Sector
            End Get
            Set(value As ContractoServicio.Utilidad.GetComboSectores.Sector1)
                _Sector = value
            End Set
        End Property
        Public Property ValorMaximo() As String
            Get
                Return _ValorMaximo
            End Get
            Set(value As String)
                _ValorMaximo = value
            End Set
        End Property

#End Region

    End Class

End Namespace
