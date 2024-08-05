Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio TipoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 10/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class TipoMedioPago
        Inherits BaseEntidade

#Region "[Variáveis]"

        Private _codTipoMedioPago As String
        Private _desTipoMedioPago As String
        Private _mediosPago As List(Of MedioPago)
        Private _ordenTipoMedioPago As Integer

#End Region

#Region "[Propriedades]"

        Public Property MediosPago() As List(Of MedioPago)
            Get
                Return _mediosPago
            End Get
            Set(value As List(Of MedioPago))
                _mediosPago = value
            End Set
        End Property

        Public Property CodTipoMedioPago() As String
            Get
                Return _codTipoMedioPago
            End Get
            Set(value As String)
                _codTipoMedioPago = value
            End Set
        End Property

        Public Property DesTipoMedioPago() As String
            Get
                Return _desTipoMedioPago
            End Get
            Set(value As String)
                _desTipoMedioPago = value
            End Set
        End Property

        Public Property OrdenTipoMedioPago As Integer
            Get
                Return _ordenTipoMedioPago
            End Get
            Set(value As Integer)
                _ordenTipoMedioPago = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()



        End Sub

        Public Sub New(TipoMP As Integracion.ContractoServicio.GetMorfologiaDetail.TipoMedioPago, OrdenTipoMedioPago As Integer)

            _codTipoMedioPago = TipoMP.CodTipoMedioPago
            _desTipoMedioPago = TipoMP.DesTipoMedioPago
            _ordenTipoMedioPago = OrdenTipoMedioPago
            _mediosPago = New List(Of MedioPago)

            For Each item In TipoMP.MediosPago
                _mediosPago.Add(New MedioPago(item))
            Next

        End Sub

#End Region

    End Class

End Namespace