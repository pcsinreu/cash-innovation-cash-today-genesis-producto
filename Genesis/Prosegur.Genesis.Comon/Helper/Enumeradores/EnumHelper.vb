Imports Prosegur.Genesis.Comon.Atributos

Namespace Helper.Enumeradores

    ''' <summary>
    ''' Classe de Enumeradores do Controle Helper.
    ''' </summary>
    ''' <history>
    ''' [Thiago Dias] 22/11/2013
    ''' </history>
    <Serializable()>
    Public Class EnumHelper

        ''' <summary>
        ''' Define Enumeração de Parâmetros Helper.
        ''' </summary>    
        Enum TipoParametro

            Query
            Filtro
            Orden
            Juncao

        End Enum

        Enum TipoCondicion
            <ValorEnum("=")>
            Igual
            <ValorEnum("!=")>
            Diferente
            <ValorEnum("Avancado")>
            Avancado
        End Enum

    End Class

End Namespace
