Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Classe TipoImpresora
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TipoImpresora

        ''' <summary>
        ''' Retorna os tipos de formato
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTiposImpresora() As ObservableCollection(Of Clases.TipoImpresora)
            Return AccesoDatos.Genesis.TipoImpresora.RecuperarTiposImpresora()
        End Function

        Public Shared Function ObtenerTipoImpresoraPorIdentificador(identificador As String) As Clases.TipoImpresora
            Return AccesoDatos.Genesis.TipoImpresora.RecuperarTipoImpresoraPorIdentificador(identificador)
        End Function

        Public Shared Function ObtenerTipoImpresoraPorCodigo(codigo As String) As Clases.TipoImpresora
            Return AccesoDatos.Genesis.TipoImpresora.RecuperarTipoImpresoraPorCodigo(codigo)
        End Function

        Public Shared Function ObtenerTipoServicioPorElemento(identificadorRemessa As String, identificadorBulto As String, identificadorParcial As String) As Clases.TipoImpresora
            Return AccesoDatos.Genesis.TipoImpresora.RecuperarPorElemento(identificadorRemessa, identificadorBulto, identificadorParcial)
        End Function

    End Class

End Namespace