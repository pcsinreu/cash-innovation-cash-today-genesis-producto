Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio MedioPago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 20/12/2010 Criado
    ''' </history>
    <Serializable()> _
    Public Class MedioPago
        Inherits BaseEntidade

#Region "[Variáveis]"

        Private _oidMedioPago As String
        Private _codMedioPago As String
        Private _desMedioPago As String

        Private _divisa As New Divisa
        Private _TipoMedioPago As New TipoMedioPago
        Private _Terminos As New List(Of TerminoMedioPago)

#End Region

#Region "[Propriedades]"

        Public Property OidMedioPago() As String
            Get
                Return _oidMedioPago
            End Get
            Set(value As String)
                _oidMedioPago = value
            End Set
        End Property

        Public Property CodMedioPago() As String
            Get
                Return _codMedioPago
            End Get
            Set(value As String)
                _codMedioPago = value
            End Set
        End Property

        Public Property DesMedioPago() As String
            Get
                Return _desMedioPago
            End Get
            Set(value As String)
                _desMedioPago = value
            End Set
        End Property

        Public Property Divisa As Divisa
            Get
                Return _divisa
            End Get
            Set(value As Divisa)
                _divisa = value
            End Set
        End Property
        
        Public Property TipoMedioPago As TipoMedioPago
            Get
                Return _TipoMedioPago
            End Get
            Set(value As TipoMedioPago)
                _TipoMedioPago = value
            End Set
        End Property

        Public Property Terminos As List(Of TerminoMedioPago)
            Get
                Return _Terminos
            End Get
            Set(value As List(Of TerminoMedioPago))
                _Terminos = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()



        End Sub

        Public Sub New(MedioP As Integracion.ContractoServicio.GetMorfologiaDetail.MedioPago)

            _oidMedioPago = MedioP.OidMedioPago
            _codMedioPago = MedioP.CodMedioPago
            _desMedioPago = MedioP.DesMedioPago

            _Terminos = New List(Of TerminoMedioPago)

            If MedioP.Terminos Is Nothing Then
                Exit Sub
            End If

            For Each termino In MedioP.Terminos
                _Terminos.Add(New TerminoMedioPago(termino))
            Next

        End Sub

        Public Sub New(ObjMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso)

            _codMedioPago = ObjMedioPago.Codigo
            _desMedioPago = ObjMedioPago.Descripcion

            _divisa.CodigoDivisa = ObjMedioPago.CodigoIsoDivisa
            _divisa.DescripcionDivisa = ObjMedioPago.DescripcionDivisa

            _TipoMedioPago.CodTipoMedioPago = ObjMedioPago.CodigoTipoMedioPago
            _TipoMedioPago.DesTipoMedioPago = ObjMedioPago.DescripcionTipoMedioPago

            For Each term In ObjMedioPago.TerminosMedioPago
                _Terminos.Add(New TerminoMedioPago(term))
            Next

        End Sub

#End Region

#Region "[MÉTODOS]"


#End Region

    End Class

End Namespace