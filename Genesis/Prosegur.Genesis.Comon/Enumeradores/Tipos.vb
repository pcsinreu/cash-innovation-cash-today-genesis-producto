Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ' ***********************************************************************
    '  Module:  Tipos.vb
    '  Author:  CAzevedo
    '  Purpose: Definition of the Enum Tipos
    ' ***********************************************************************

    ''' TipoFormato = 05
    ''' TipoUbicacion = 04
    Public Enum Tipos

        <ValorEnum("05")>
        TipoFormato
        <ValorEnum("04")>
        TipoUbicacion
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace