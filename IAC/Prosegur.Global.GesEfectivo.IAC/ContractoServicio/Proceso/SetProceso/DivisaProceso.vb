Namespace Proceso.SetProceso

    ''' <summary>
    ''' Classe DivisaProceso
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class DivisaProceso

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _orden As Integer
        Private _toleranciaParcialMin As Decimal
        Private _toleranciaParcialMax As Decimal
        Private _toleranciaBultoMin As Decimal
        Private _toleranciaBultolMax As Decimal
        Private _toleranciaRemesaMin As Decimal
        Private _toleranciaRemesaMax As Decimal

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Orden() As Integer
            Get
                Return _orden
            End Get
            Set(value As Integer)
                _orden = value
            End Set
        End Property

        Public Property ToleranciaParcialMin() As Decimal
            Get
                Return _toleranciaParcialMin
            End Get
            Set(value As Decimal)
                _toleranciaParcialMin = value
            End Set
        End Property

        Public Property ToleranciaParcialMax() As Decimal
            Get
                Return _toleranciaParcialMax
            End Get
            Set(value As Decimal)
                _toleranciaParcialMax = value
            End Set
        End Property

        Public Property ToleranciaBultoMin() As Decimal
            Get
                Return _toleranciaBultoMin
            End Get
            Set(value As Decimal)
                _toleranciaBultoMin = value
            End Set
        End Property

        Public Property ToleranciaBultolMax() As Decimal
            Get
                Return _toleranciaBultolMax
            End Get
            Set(value As Decimal)
                _toleranciaBultolMax = value
            End Set
        End Property

        Public Property ToleranciaRemesaMin() As Decimal
            Get
                Return _toleranciaRemesaMin
            End Get
            Set(value As Decimal)
                _toleranciaRemesaMin = value
            End Set
        End Property

        Public Property ToleranciaRemesaMax() As Decimal
            Get
                Return _toleranciaRemesaMax
            End Get
            Set(value As Decimal)
                _toleranciaRemesaMax = value
            End Set
        End Property

#End Region
    End Class
End Namespace