Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ' ***********************************************************************
    '  Module:  TipoUbicacion.vb
    '  Author:  CAzevedo
    '  Purpose: Definition of the Enum TipoUbicacion
    ' ***********************************************************************

    ''' Enumeração:
    ''' TipoUbicacion= 04
    ''' 
    ''' Valores:
    ''' SalaConteo = 01
    ''' SalaProcesado = 02
    ''' SalaElaboracionSalidas = 03
    ''' SalaATM = 04
    ''' CamaraAlmacen = 05
    ''' EnTransicion = 06
    ''' Legado = 07
    '''
    Public Enum TipoUbicacion

        <ValorEnum("01")>
        SalaConteo
        <ValorEnum("02")>
        SalaProcesado
        <ValorEnum("03")>
        SalaElaboracionSalidas
        <ValorEnum("04")>
        SalaATM
        <ValorEnum("05")>
        CamaraAlmacen
        <ValorEnum("06")>
        EnTransicion
        <ValorEnum("07")>
        Legado
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace