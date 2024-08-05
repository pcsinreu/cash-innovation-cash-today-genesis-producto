
Namespace ImporteMaximo.GetImporteMaximo

    ''' <summary>
    ''' Classe ImporteMaximo
    ''' </summary>
    ''' <remarks></remarks>
   
    <Serializable()> _
    Public Class ImporteMaximo

#Region "[VARIAVEIS]"

       
        Private _BolDefecto As Boolean?
        Private _Cliente As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Private _Canal As ContractoServicio.Utilidad.GetComboCanales.Canal
        Private _Subcanal As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
        Private _Sector As ContractoServicio.Utilidad.GetComboSectores.Sector1
        Private _Divisa As ContractoServicio.Utilidad.GetComboDivisas.Divisa
        Private _ValorMaximo As String

#End Region

#Region "[PROPRIEDADES]"

      

        Public Property BolDefecto() As Boolean?
            Get
                Return _BolDefecto
            End Get
            Set(value As Boolean?)
                _BolDefecto = value
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
