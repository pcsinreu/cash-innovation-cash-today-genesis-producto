Namespace RespaldoCompleto.GetRespaldosCompletos

    Public Class RespaldoCompletoPDF

#Region " Variáveis "

        Private _Detalles As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DetalleColeccion
        Private _Sobres As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.SobreColeccion
        Private _Divisas As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DivisaColeccion
        Private _InformacionesIAC As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACPDFColeccion
        Private _TotalParcialesDeclarados As Integer
        Private _Observaciones As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.ObservacionColeccion

#End Region

#Region " Propriedades "

        Public Property Detalles() As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DetalleColeccion
            Get
                Return _Detalles
            End Get
            Set(value As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DetalleColeccion)
                _Detalles = value
            End Set
        End Property

        Public Property Sobres() As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.SobreColeccion
            Get
                Return _Sobres
            End Get
            Set(value As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.SobreColeccion)
                _Sobres = value
            End Set
        End Property

        Public Property Divisas() As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DivisaColeccion
            Get
                Return _Divisas
            End Get
            Set(value As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DivisaColeccion)
                _Divisas = value
            End Set
        End Property

        Public Property InformacionesIAC() As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACPDFColeccion
            Get
                Return _InformacionesIAC
            End Get
            Set(value As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACPDFColeccion)
                _InformacionesIAC = value
            End Set
        End Property

        Public Property TotalParcialesDeclarados() As Integer
            Get
                Return _TotalParcialesDeclarados
            End Get
            Set(value As Integer)
                _TotalParcialesDeclarados = value
            End Set
        End Property

        Public Property Observaciones() As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.ObservacionColeccion
            Get
                Return _Observaciones
            End Get
            Set(value As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.ObservacionColeccion)
                _Observaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace
