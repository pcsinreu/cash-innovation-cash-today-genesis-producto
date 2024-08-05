Imports System.Xml.Serialization

Namespace IngresoRemesas

    ''' <summary>
    ''' Classe Parcial
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 27/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:IngresoRemesas")> _
    <XmlRoot(Namespace:="urn:IngresoRemesas")> _
    <Serializable()> _
    Public Class Parcial

#Region "[VARIÁVEIS]"

        Private _IdParcialOrigen As String
        Private _CodigoPrecinto As String
        Private _CodigoFormato As String
        Private _Secuencia As Nullable(Of Integer)
        Private _CodigoEstado As String
        Private _DeclaradoTotaisParcial As DeclaradoTotaisParcial
        Private _DeclaradosDetalleParcial As DeclaradosDetalleParcial
        Private _DeclaradosAgrupacionParcial As DeclaradosAgrupacionParcial
        Private _ValoresParcial As ValoresParcial

#End Region

#Region "[PROPRIEDADES]"

        Public Property IdParcialOrigen() As String
            Get
                Return _IdParcialOrigen
            End Get
            Set(value As String)
                _IdParcialOrigen = value
            End Set
        End Property

        Public Property CodigoPrecinto() As String
            Get
                Return _CodigoPrecinto
            End Get
            Set(value As String)
                _CodigoPrecinto = value
            End Set
        End Property

        Public Property CodigoFormato() As String
            Get
                Return _CodigoFormato
            End Get
            Set(value As String)
                _CodigoFormato = value
            End Set
        End Property

        Public Property Secuencia() As Nullable(Of Integer)
            Get
                Return _Secuencia
            End Get
            Set(value As Nullable(Of Integer))
                _Secuencia = value
            End Set
        End Property

        Public Property CodigoEstado() As String
            Get
                Return _CodigoEstado
            End Get
            Set(value As String)
                _CodigoEstado = value
            End Set
        End Property

        Public Property DeclaradoTotaisParcial() As DeclaradoTotaisParcial
            Get
                Return _DeclaradoTotaisParcial
            End Get
            Set(value As DeclaradoTotaisParcial)
                _DeclaradoTotaisParcial = value
            End Set
        End Property

        Public Property DeclaradosDetalleParcial() As DeclaradosDetalleParcial
            Get
                Return _DeclaradosDetalleParcial
            End Get
            Set(value As DeclaradosDetalleParcial)
                _DeclaradosDetalleParcial = value
            End Set
        End Property

        Public Property DeclaradosAgrupacionParcial() As DeclaradosAgrupacionParcial
            Get
                Return _DeclaradosAgrupacionParcial
            End Get
            Set(value As DeclaradosAgrupacionParcial)
                _DeclaradosAgrupacionParcial = value
            End Set
        End Property

        Public Property ValoresParcial() As ValoresParcial
            Get
                Return _ValoresParcial
            End Get
            Set(value As ValoresParcial)
                _ValoresParcial = value
            End Set
        End Property

#End Region

    End Class

End Namespace