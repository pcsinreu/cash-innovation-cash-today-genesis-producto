Namespace Clases
    ''' <summary>
    ''' Classe de AccionTransaccion.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class AccionTransaccion
        Inherits BaseClase

        Public Property IdentificadorAccionContable As String
        Public Property Estado As Enumeradores.EstadoDocumento
        Public Property TipoSitio As Enumeradores.TipoSitio
        Public Property TipoSaldo As Enumeradores.TipoSaldo
        Public Property TipoMovimiento As Enumeradores.TipoMovimiento
    End Class
End Namespace

'1	AC	0	-	0	+
'2	CF	0	0	0	0
'3	RC	0	0	0	0

'1	AC	0	-	0	+
'==
'Estado = Enumeradores.EstadoDocumento.Aceptado
'TipoSitio = TipoSitio.Origen
'TipoSaldo = TipoSaldo.NoDisponible
'TipoMovimiento = TipoMovimiento.Egreso

'Estado = Enumeradores.EstadoDocumento.Aceptado
'TipoSitio = TipoSitio.Destino
'TipoSaldo = TipoSaldo.NoDisponible
'TipoMovimiento = TipoMovimiento.Ingreso
