Imports Prosegur.Genesis.Comon.Atributos
Namespace Enumeradores
    ''' <summary>
    ''' Enumerador de acciones al actualizar movimientos
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Enum AccionActualizarMovimiento
        <ValorEnum("Notificar")>
        Notificar = 1
        <ValorEnum("Acreditar")>
        Acreditar = 2
        <ValorEnum("REENVIO_AUTO")>
        RenvioAutomatico = 3
        <ValorEnum("REENVIO_MANUAL")>
        RenvioManual = 4
    End Enum

End Namespace
