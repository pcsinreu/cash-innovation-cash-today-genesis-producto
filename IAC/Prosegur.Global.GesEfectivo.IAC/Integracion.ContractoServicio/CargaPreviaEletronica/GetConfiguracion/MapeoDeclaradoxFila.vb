Imports System.Xml.Serialization

Namespace CargaPreviaEletronica.GetConfiguracion

    <Serializable()> _
    Public Class MapeoDeclaradoxFila

        Private _valorCampoPlanilla As String
        Private _codISODivisa As String
        Private _codTipoMedioPago As String
        Private _codMedioPago As String


        Public Property CodMedioPago() As String
            Get
                Return _codMedioPago
            End Get
            Set(value As String)
                _codMedioPago = value
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


        Public Property CodISODivisa() As String
            Get
                Return _codISODivisa
            End Get
            Set(value As String)
                _codISODivisa = value
            End Set
        End Property


        Public Property ValorCampoPlanilla() As String
            Get
                Return _valorCampoPlanilla
            End Get
            Set(value As String)
                _valorCampoPlanilla = value
            End Set
        End Property

    End Class
End Namespace
