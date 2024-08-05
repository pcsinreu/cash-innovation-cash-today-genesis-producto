Namespace CorteParcial.GetCortesParciais

    Public Class CorteParcialPDF

#Region " Variáveis "

        Private _Detalles As ContractoServ.CorteParcial.GetCortesParciais.DetalleColeccion
        Private _Sobres As ContractoServ.CorteParcial.GetCortesParciais.SobreColeccion
        Private _Observaciones As ContractoServ.CorteParcial.GetCortesParciais.ObservacionColeccion
        Private _Falsos As ContractoServ.CorteParcial.GetCortesParciais.FalsoColeccion

#End Region

#Region " Propriedades "

        Public Property Detalles() As ContractoServ.CorteParcial.GetCortesParciais.DetalleColeccion
            Get
                Return _Detalles
            End Get
            Set(value As ContractoServ.CorteParcial.GetCortesParciais.DetalleColeccion)
                _Detalles = value
            End Set
        End Property

        Public Property Sobres() As ContractoServ.CorteParcial.GetCortesParciais.SobreColeccion
            Get
                Return _Sobres
            End Get
            Set(value As ContractoServ.CorteParcial.GetCortesParciais.SobreColeccion)
                _Sobres = value
            End Set
        End Property

        Public Property Observaciones() As ContractoServ.CorteParcial.GetCortesParciais.ObservacionColeccion
            Get
                Return _Observaciones
            End Get
            Set(value As ContractoServ.CorteParcial.GetCortesParciais.ObservacionColeccion)
                _Observaciones = value
            End Set
        End Property

        Public Property Falsos() As ContractoServ.CorteParcial.GetCortesParciais.FalsoColeccion
            Get
                Return _Falsos
            End Get
            Set(value As ContractoServ.CorteParcial.GetCortesParciais.FalsoColeccion)
                _Falsos = value
            End Set
        End Property

#End Region

    End Class

End Namespace
