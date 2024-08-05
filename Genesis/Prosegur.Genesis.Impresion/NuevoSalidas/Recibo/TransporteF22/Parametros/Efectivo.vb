Namespace NuevoSalidas.Recibo.TransporteF22.Parametros

    ''' <summary>
    ''' Entidad Efectivo
    ''' </summary>
    ''' <history>[jviana] 23/08/2010 Creado</history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Efectivo

#Region "[VARIABLES]"

        Private _CodIsoDivisa As String
        Private _DesDivisa As String
        Private _ImporteTotal As Double?
        Private _EfectivoDetalles As Recibo.TransporteF22.Parametros.EfectivoDetalleColeccion

#End Region

#Region "[PROPIEDADES]"

        ''' <summary>
        ''' Propriedad CodIsoDivisa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodIsoDivisa() As String
            Get
                Return _CodIsoDivisa
            End Get
            Set(value As String)
                _CodIsoDivisa = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesDivisa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesDivisa() As String
            Get
                Return _DesDivisa
            End Get
            Set(value As String)
                _DesDivisa = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad Importe Total
        ''' </summary>
        ''' <value>Double</value>
        ''' <returns>Double</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property ImporteTotal() As Double
            Get
                If (_ImporteTotal Is Nothing) AndAlso _
                   (EfectivoDetalles IsNot Nothing) Then
                    _ImporteTotal = EfectivoDetalles.Sum(Function(o) o.ImporteTotal)
                ElseIf _ImporteTotal Is Nothing Then
                    _ImporteTotal = 0
                End If

                Return _ImporteTotal
            End Get
            Set(value As Double)
                _ImporteTotal = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad EfectivoDetalles
        ''' </summary>
        ''' <value>Recibo.TransporteF22.Parametros.EfectivoDetalleColeccion</value>
        ''' <returns>Recibo.TransporteF22.Parametros.EfectivoDetalleColeccion</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property EfectivoDetalles() As Recibo.TransporteF22.Parametros.EfectivoDetalleColeccion
            Get
                Return _EfectivoDetalles
            End Get
            Set(value As Recibo.TransporteF22.Parametros.EfectivoDetalleColeccion)
                _EfectivoDetalles = value
            End Set
        End Property

#End Region

    End Class

End Namespace