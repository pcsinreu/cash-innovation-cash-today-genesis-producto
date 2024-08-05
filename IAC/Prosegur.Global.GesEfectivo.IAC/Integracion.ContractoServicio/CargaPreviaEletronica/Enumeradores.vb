Imports System.Xml.Serialization

Namespace CargaPreviaEletronica

    ''' <summary>
    ''' Tipo do arquivo de configuracao
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 25/03/2013 Criado
    ''' </history>
    Public Enum eTipoArchivo
        <XmlEnum("1")>
        Coluna = 1
        <XmlEnum("2")>
        Linha
    End Enum

    ''' <summary>
    ''' Formato do arquivo de configuracao
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 25/03/2013 Criado
    ''' </history>
    Public Enum eFormatoArchivo
        <XmlEnum("1")>
        Excel = 1
        <XmlEnum("2")>
        Texto
    End Enum

    Public Enum eAlineacion
        <XmlEnum("1")>
        Izquierda = 1
        <XmlEnum("2")>
        Derecha
    End Enum


    Public Enum eSimbologia
        <XmlEnum("1")>
        Si = 1
        <XmlEnum("2")>
        No
    End Enum

    Public Enum eTipoDato
        <XmlEnum("1")>
        Alfanumerico = 1
        <XmlEnum("2")>
        Numerico
        <XmlEnum("3")>
        Fecha
        <XmlEnum("4")>
        Monetario
    End Enum

    Public Enum eDato
        <XmlEnum("1")>
        Importe = 1
        <XmlEnum("2")>
        Detalle
    End Enum

End Namespace