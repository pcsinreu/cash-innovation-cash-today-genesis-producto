Imports Prosegur.Framework.Dicionario
Imports System.IO
Imports System.Xml
Imports Microsoft.Reporting.WinForms
Imports System.Drawing
Imports System.Windows.Forms

Namespace REYD

    ''' <summary>
    ''' Classe Impresion
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GesImpresion

#Region "[CONSTANTES]"
#End Region

#Region "[VARIÁVEIS]"

        ' Qdo for impressão Windows A4, trava o envio de páginas para a impressora para serem enviadas de uma vez só ao final da impressão
        Private Shared _travarPaginas As Boolean = False

#End Region

#Region "[PROPRIEDADES]"

        Public Shared ReadOnly Property EnvioPaginasTravado() As Boolean
            Get
                Return _travarPaginas
            End Get
        End Property

#End Region

#Region "[METODOS]"

        ''' <summary>
        ''' Imprime etiqueta por contenedor
        ''' </summary>
        ''' <param name="pObjContenedor">Estrutura de dados que será exibido na impressão</param>
        ''' <param name="pImpressoraNome">Nome da Impressora que será usada para impressão</param>
        Public Shared Sub EtiquetaContenedor(pObjContenedor As Impresion.REYD.Ticket.Contenedor.Parametros.Contenedor, _
                                        pImpressoraNome As String)

            'Esta etiqueta utilizará apenas impressora ZEBRA
            'Chama o método para realizar a impressão passando o nome da impressora.
            Impresion.REYD.Ticket.Contenedor.Zebra.Layout.Imprimir(pObjContenedor, pImpressoraNome)

        End Sub



        Public Shared Sub TravarImpressao()
            _travarPaginas = True
        End Sub

        Public Shared Sub LiberarImpressao()
            _travarPaginas = False
        End Sub

        Public Shared Sub FinalizarImpressao()

            If _travarPaginas Then
                _travarPaginas = False
            End If

        End Sub


#End Region

    End Class

End Namespace
