Namespace BilletajeSucursal.GetBilletajesSucursais

    Public Class BilletajeSucursalPDF

#Region " Variáveis "

        Private _Estacion As String = String.Empty
        Private _DescricionEstacion As String = String.Empty
        Private _Divisas As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaColeccion
        Private _Respaldos As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.RespaldoColeccion

#End Region

#Region " Propriedades "

        Public Property Estacion() As String
            Get
                Return _Estacion
            End Get
            Set(value As String)
                _Estacion = value
            End Set
        End Property

        Public Property DescricionEstacion() As String
            Get
                Return _DescricionEstacion
            End Get
            Set(value As String)
                _DescricionEstacion = value
            End Set
        End Property

        Public Property Divisas() As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaColeccion
            Get
                Return _Divisas
            End Get
            Set(value As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaColeccion)
                _Divisas = value
            End Set
        End Property

        Public Property Respaldos() As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.RespaldoColeccion
            Get
                Return _Respaldos
            End Get
            Set(value As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.RespaldoColeccion)
                _Respaldos = value
            End Set
        End Property

#End Region

    End Class

End Namespace
