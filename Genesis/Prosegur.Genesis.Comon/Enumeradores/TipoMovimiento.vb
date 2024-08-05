Imports Prosegur.Genesis.Comon.Atributos
Namespace Enumeradores
    ''' <summary>
    ''' Enumeradores para os tipos de movimiento
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Enum TipoMovimiento
        <ValorEnum("I")>
        Ingreso
        <ValorEnum("E")>
        Egreso
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace
