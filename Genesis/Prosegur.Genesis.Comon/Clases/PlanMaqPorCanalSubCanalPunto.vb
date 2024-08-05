Imports System.Collections.ObjectModel

Namespace Clases
    ' ***********************************************************************
    '  Modulo:  PlanMaqPorCanalSubCanalPunto.vb
    '  Descripción: Clase definición Plan por Maquina Canal SubCanal Punto
    ' ***********************************************************************
    <Serializable()>
    Public Class PlanMaqPorCanalSubCanalPunto
        Inherits BindableBase

#Region "Variables"
        Private _indice As String
        Private _puntos As ObservableCollection(Of Clases.PuntoServicio)
        Private _canales As ObservableCollection(Of Clases.Canal)
        Private _codigoAgrupador As Integer
#End Region

#Region "Propiedades"
        Public ReadOnly Property DisplayPuntos() As String
            Get
                Dim display As String = String.Empty
                If _puntos IsNot Nothing AndAlso _puntos.Count > 0 Then
                    For Each punto In _puntos
                        display += String.Format(" / {0} - {1}", punto.Codigo, punto.Descripcion)
                    Next
                    display = display.Substring(3)
                End If
                Return display
            End Get
        End Property

        Public ReadOnly Property DisplayCanales() As String
            Get
                Dim display As String = String.Empty
                If _canales IsNot Nothing AndAlso _canales.Count > 0 Then
                    For Each canal In _canales
                        display += String.Format(" / {0} - {1}", canal.Codigo, canal.Descripcion)
                    Next
                    If display.Count > 0 Then
                        display = display.Substring(3)
                    End If
                End If
                Return display
            End Get
        End Property

        Public ReadOnly Property DisplaySubCanales() As String
            Get
                Dim display As String = String.Empty
                If _canales IsNot Nothing AndAlso _canales.Count > 0 Then
                    For Each canal In _canales
                        If canal.SubCanales IsNot Nothing AndAlso canal.SubCanales.Count > 0 Then
                            For Each subcanal In canal.SubCanales
                                display += String.Format(" / {0} - {1}", subcanal.Codigo, subcanal.Descripcion)
                            Next
                        End If
                    Next

                    If display.Count > 0 Then
                        display = display.Substring(3)
                    End If
                End If
                Return display
            End Get
        End Property

        Public Property PuntosServicios() As ObservableCollection(Of Clases.PuntoServicio)
            Get
                Return _puntos
            End Get
            Set(ByVal value As ObservableCollection(Of Clases.PuntoServicio))
                SetProperty(_puntos, value, "Puntos")
            End Set
        End Property

        Public Property Canales() As ObservableCollection(Of Clases.Canal)
            Get
                Return _canales
            End Get
            Set(ByVal value As ObservableCollection(Of Clases.Canal))
                SetProperty(_canales, value, "Canales")
            End Set
        End Property
        Public ReadOnly Property Indice() As String
            Get
                If String.IsNullOrWhiteSpace(_indice) Then
                    _indice = Guid.NewGuid().ToString
                End If
                Return _indice
            End Get
        End Property
        Public Property CodigoAgrupador() As Integer
            Get
                Return _codigoAgrupador
            End Get
            Set(ByVal value As Integer)
                _codigoAgrupador = value
            End Set
        End Property
#End Region
        Public Overrides Function Equals(pPlan As Object) As Boolean
            Try
                If Me.Indice = DirectCast(pPlan, PlanMaqPorCanalSubCanalPunto).Indice Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Return MyBase.Equals(pPlan)
            End Try

        End Function
    End Class
End Namespace


