Imports System.Xml.Serialization

Namespace Legado.GeneracionF22

    ''' <summary>
    ''' Entidad Remesa
    ''' </summary>
    ''' <history>[abueno] 13/07/2010 Creado</history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Remesa

#Region "[VARIÁVEIS]"

        Private _OidRemesaSalidas As String
        Private _OidRemesaLegado As String
        Private _NelPedidoLegado As Integer
        Private _NelControlLegado As Integer
        Private _CodDelegacion As String
        Private _CodUsuarioArmado As String
        Private _CodClienteMandante As String
        Private _CodClienteDestino As String
        Private _CodCentroProceso As String
        Private _CodCanal As String
        Private _CodRuta As String
        Private _FecEntrega As Date
        Private _FyhInicioArmado As Date
        Private _FyhFinArmado As Date
        Private _CodEstado As String
        Private _CodRecibo As String
        Private _NecImpresiones As Integer
        Private _Efetivos As EfectivoColeccion
        Private _Bultos As BultoColeccion
        Private _MediosPago As MedioPagoColeccion

        Private _CodSubCliente As String
        Private _CodPuntoServicio As String
        Private _CodEmpresaTrans As String
        Private _CodCajaCentralizada As String
        Private _CodSecuencia As String
        Private _CodPuesto As String
        Private _OidOT As String
        Private _CodigoServicio As String
        Private _CodigoClienteLV As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodPuesto() As String
            Get
                Return _CodPuesto
            End Get
            Set(value As String)
                _CodPuesto = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad OidRemesaSalidas
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property OidRemesaSalidas() As String
            Get
                Return _OidRemesaSalidas
            End Get
            Set(value As String)
                _OidRemesaSalidas = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad OidRemesaLegado
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property OidRemesaLegado() As String
            Get
                Return _OidRemesaLegado
            End Get
            Set(value As String)
                _OidRemesaLegado = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NelPedidoLegado
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NelPedidoLegado() As Integer
            Get
                Return _NelPedidoLegado
            End Get
            Set(value As Integer)
                _NelPedidoLegado = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NelControlLegado
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NelControlLegado() As Integer
            Get
                Return _NelControlLegado
            End Get
            Set(value As Integer)
                _NelControlLegado = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodDelegacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodDelegacion() As String
            Get
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodUsuarioArmado
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodUsuarioArmado() As String
            Get
                Return _CodUsuarioArmado
            End Get
            Set(value As String)
                _CodUsuarioArmado = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodClienteMandante
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodClienteMandante() As String
            Get
                Return _CodClienteMandante
            End Get
            Set(value As String)
                _CodClienteMandante = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodClienteDestino
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodClienteDestino() As String
            Get
                Return _CodClienteDestino
            End Get
            Set(value As String)
                _CodClienteDestino = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodCentroProceso
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodCentroProceso() As String
            Get
                Return _CodCentroProceso
            End Get
            Set(value As String)
                _CodCentroProceso = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodCanal
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodCanal() As String
            Get
                Return _CodCanal
            End Get
            Set(value As String)
                _CodCanal = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodRuta
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodRuta() As String
            Get
                Return _CodRuta
            End Get
            Set(value As String)
                _CodRuta = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad FecEntrega
        ''' </summary>
        ''' <value>Date</value>
        ''' <returns>Date</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property FecEntrega() As Date
            Get
                Return _FecEntrega
            End Get
            Set(value As Date)
                _FecEntrega = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad FyhInicioArmado
        ''' </summary>
        ''' <value>Date</value>
        ''' <returns>Date</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property FyhInicioArmado() As Date
            Get
                Return _FyhInicioArmado
            End Get
            Set(value As Date)
                _FyhInicioArmado = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad FyhFinArmado
        ''' </summary>
        ''' <value>Date</value>
        ''' <returns>Date</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property FyhFinArmado() As Date
            Get
                Return _FyhFinArmado
            End Get
            Set(value As Date)
                _FyhFinArmado = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodEstado
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodEstado() As String
            Get
                Return _CodEstado
            End Get
            Set(value As String)
                _CodEstado = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodRecibo
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodRecibo() As String
            Get
                Return _CodRecibo
            End Get
            Set(value As String)
                _CodRecibo = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NecImpresiones
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NecImpresiones() As Integer
            Get
                Return _NecImpresiones
            End Get
            Set(value As Integer)
                _NecImpresiones = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad Efetivo
        ''' </summary>
        ''' <value>EfectivoColeccion</value>
        ''' <returns>EfectivoColeccion</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property Efetivos() As EfectivoColeccion
            Get
                Return _Efetivos
            End Get
            Set(value As EfectivoColeccion)
                _Efetivos = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad Bulto
        ''' </summary>
        ''' <value>BultoColeccion</value>
        ''' <returns>BultoColeccion</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property Bultos() As BultoColeccion
            Get
                Return _Bultos
            End Get
            Set(value As BultoColeccion)
                _Bultos = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodSubCliente
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 11/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodSubCliente() As String
            Get
                Return _CodSubCliente
            End Get
            Set(value As String)
                _CodSubCliente = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodPuntoServicio
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 11/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodPuntoServicio() As String
            Get
                Return _CodPuntoServicio
            End Get
            Set(value As String)
                _CodPuntoServicio = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodEmpresaTrans
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 11/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodEmpresaTrans() As String
            Get
                Return _CodEmpresaTrans
            End Get
            Set(value As String)
                _CodEmpresaTrans = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodCajaCentralizada
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 11/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodCajaCentralizada() As String
            Get
                Return _CodCajaCentralizada
            End Get
            Set(value As String)
                _CodCajaCentralizada = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodSecuencia
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 11/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodSecuencia() As String
            Get
                Return _CodSecuencia
            End Get
            Set(value As String)
                _CodSecuencia = value
            End Set
        End Property


        Public Property OidOT As String
            Get
                Return _OidOT
            End Get
            Set(value As String)
                _OidOT = value
            End Set
        End Property

        Public Property CodigoServicio As String
            Get
                Return _CodigoServicio
            End Get
            Set(value As String)
                _CodigoServicio = value
            End Set
        End Property

        Public Property CodigoClienteLV As String
            Get
                Return _CodigoClienteLV
            End Get
            Set(value As String)
                _CodigoClienteLV = value
            End Set
        End Property

        Public Property MediosPago() As MedioPagoColeccion
            Get
                Return _MediosPago
            End Get
            Set(value As MedioPagoColeccion)
                _MediosPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace