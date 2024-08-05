Public Class Enumeradores

    Public Enum ePermiteCreacion
        SoloIndividual = 0
        SoloEnGrupo = 1
        Ambos = 2
    End Enum

    Public Enum eDistinguirPorNivel
        NoDistinguir = 0
        SoloMatrices = 1
        SoloSectores = 2
    End Enum

    Public Enum eEstadoComprobante
        EnProceso = 1
        Impreso = 2
        Aceptados = 3
        Rechazados = 4
        Baja = 5
    End Enum

    Public Enum eVistaDestinatario
        No = 0
        Si = 1
    End Enum

    Public Enum eBasadoEnSaldos
        NoEsBasadoEnSaldos = 0
        EsBasadoEnSaldos = 1
        EsBasadoSoloEnSaldoDisponible = 2
    End Enum

    Public Enum eAccion
        Aceptar = 0
        Actualizar = 1
        Crear = 2
        Sustituir = 3
        CrearAceptar = 4
        Rechazar = 5
        CrearImprimir = 6
    End Enum

    Public Enum eCampos
        Banco = 0
        CentroProcesoOrigen = 1
        CentroProcesoDestino = 2
        ClienteOrigen = 3
        ClienteDestino = 4
        BancoDeposito = 5
        NumExterno = 6
    End Enum

    Public Enum eRegla
        Automata = 0
        IngresarRemesaNumeroExternoRepetido = 1
    End Enum

    Public Enum TipoNegocio
        MultiagenciaMultirecaudo = 1
        Maquinas = 2
        Delegacion = 3
    End Enum

    Public Enum TipoMedioPago
        <ValorEnum("codtipob")>
        Cheque
        <ValorEnum("codtipo")>
        OtroValor
        <ValorEnum("codtipoc")>
        Tarjeta
        <ValorEnum("codtipoa")>
        Ticket
    End Enum

End Class

<AttributeUsage(AttributeTargets.Field, AllowMultiple:=False)>
Public Class ValorEnum
    Inherits Attribute

    Public Property Valor As String

    Sub New(valor As String)
        Me.Valor = valor
    End Sub

End Class