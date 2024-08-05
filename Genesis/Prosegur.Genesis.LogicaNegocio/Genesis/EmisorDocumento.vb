Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Clase EmisorDocumento
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 04/10/2013 - Criado
    ''' </history>
    Public Class EmisorDocumento

        ''' <summary>
        ''' Método que recupera os emissores do documento.
        ''' </summary>
        ''' <returns>List(Of Comon.Clases.EmisorDocumento)</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerEmisoresDocumento(Optional objEmissor As Comon.Clases.EmisorDocumento = Nothing) As ObservableCollection(Of Comon.Clases.EmisorDocumento)
            Return Prosegur.Genesis.AccesoDatos.Genesis.EmisorDocumento.RecuperarEmisoresDocumento(objEmissor)
        End Function

    End Class

End Namespace
