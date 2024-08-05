Imports System.Xml.Serialization
Imports System.IO
Imports System.Configuration
Imports System.Xml
Imports Prosegur.Genesis.Comon.Contadoras.Configuracion.Interno
Imports Prosegur.Genesis.Comon.Contadoras.Configuracion

Namespace Contadoras

    ''' <summary>
    ''' Classe para armazenar os parâmetros globais de configuração das divisas das contadoras.
    ''' </summary>
    Public Class UtilContadoras

        Private Shared _Contadoras As ContadoraInternoColeccion = Nothing

        ''' <summary>
        ''' Recupera a configuração das contadoras.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property ConfiguracaoDivisas(ArquivoMapeo As String) As ContadoraInternoColeccion
            Get
                If _Contadoras Is Nothing Then ' Verifica se o parametro ja foi inicializado.
                    CarregaDados(ArquivoMapeo)
                End If
                Return _Contadoras
            End Get
        End Property

        Private Shared Sub CarregaDados(ArquivoMapeo As String)
            Dim reader As New XmlSerializer(GetType(ContadoraColeccion))
            '   Using sr As New StreamReader(System.Configuration.ConfigurationManager.AppSettings("PathXMLContadoras"))
            'Using sr As New StreamReader(ArquivoMapeo)
            Using sr As New StreamReader(ArquivoMapeo)
                Dim objContadoras As ContadoraColeccion
                Dim _contadora As ContadoraInterno
                Dim _divisa As DivisaInterno
                objContadoras = reader.Deserialize(sr) 'Carrega o XML de configuração de divisas das contadoras
                sr.Close()
                _Contadoras = New ContadoraInternoColeccion()
                For Each objContadora In objContadoras
                    _contadora = New ContadoraInterno()
                    _Contadoras.Add(objContadora.Modelo, _contadora)
                    For Each objDivisa In objContadora.Divisas
                        _divisa = New DivisaInterno()
                        _contadora.Divisas.Add(objDivisa.Nombre, _divisa)
                        For Each objDenominacion In objDivisa.Denominaciones
                            _divisa.Denominaciones.Add(objDenominacion.Origen, objDenominacion.Destino)
                        Next
                    Next
                Next
            End Using
        End Sub

        Public Shared Sub Reset()
            _Contadoras = Nothing
        End Sub

    End Class

End Namespace