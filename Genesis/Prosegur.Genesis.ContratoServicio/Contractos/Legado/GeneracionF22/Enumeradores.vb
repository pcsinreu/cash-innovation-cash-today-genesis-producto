Imports Prosegur.Genesis.Comon.Atributos

Namespace Legado.GeneracionF22

    Public Class Enumeradores

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

        Public Enum TipoNegocio
            MultiagenciaMultirecaudo = 1
            Maquinas = 2
            Delegacion = 3
        End Enum

    End Class

End Namespace

