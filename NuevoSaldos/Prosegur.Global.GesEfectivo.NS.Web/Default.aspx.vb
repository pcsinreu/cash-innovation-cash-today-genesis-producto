Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Comon.Extenciones

Public Class _Default
    Inherits Base

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.MENU
        MyBase.ValidarAcesso = True
    End Sub

    ''' <summary>
    ''' Inicialização da página de login unificado responsãvel por receber a token e converter as permisos para a sesion atual
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()
        Master.MenuRodapeVisivel = False
        Master.Titulo = "Nuevo Saldos"
        Master.HabilitarHistorico = False

        If (Base.InformacionUsuario Is Nothing) Then
            Throw New Exception("ERRO: INFORMACION USUARIO")
        ElseIf Base.InformacionUsuario.Delegaciones Is Nothing OrElse Base.InformacionUsuario.Delegaciones.Count < 1 Then
            Throw New Exception("ERRO: DELEGACION")
        ElseIf Base.InformacionUsuario.DelegacionSeleccionada Is Nothing Then
            Response.Redireccionar(Constantes.NOME_PAGINA_CONSULTA_TRANSACCIONES)
        End If

    End Sub

End Class