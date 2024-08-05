Imports System.Collections.ObjectModel

Namespace Clases
    ' ***********************************************************************
    '  Modulo:  AutoDesglose.vb
    '  Descripción: Clase definición Configuración de los Desgloses de Salidas
    ' ***********************************************************************
    ''' <summary>
    ''' Entidad AutoDesglose
    ''' </summary>
    ''' <history>[cbomtempo] 05/05/2014 Creado</history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class AutoDesglose

#Region "[VARIÁVEIS]"

        Private _CodIsoDivisa As String
        Private _CodConfiguracion As String
        Private _CodDenominacion As String
        Private _NumPorcentaje As Integer
        Private _NumValorFacial As Decimal

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Propriedad CodIsoDivisa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 02/07/2010 Creado</history>
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
        ''' Propriedad CodConfiguracion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 02/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodConfiguracion() As String
            Get
                Return _CodConfiguracion
            End Get
            Set(value As String)
                _CodConfiguracion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodDenominacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 02/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodDenominacion() As String
            Get
                Return _CodDenominacion
            End Get
            Set(value As String)
                _CodDenominacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NumPorcentaje
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <history>[jviana] 02/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NumPorcentaje() As Integer
            Get
                Return _NumPorcentaje
            End Get
            Set(value As Integer)
                _NumPorcentaje = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NumValorFacial
        ''' </summary>
        ''' <value>Decimal</value>
        ''' <returns>Decimal</returns>
        ''' <history>[jviana] 02/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NumValorFacial() As Decimal
            Get
                Return _NumValorFacial
            End Get
            Set(value As Decimal)
                _NumValorFacial = value
            End Set
        End Property

#End Region

    End Class
End Namespace

