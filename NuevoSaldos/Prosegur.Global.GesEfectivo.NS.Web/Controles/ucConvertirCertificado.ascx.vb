Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucConvertirCertificado
    Inherits PopupBase

    Public Property Usuario As String
        Get
            Return Session("Usuario")
        End Get
        Set(value As String)
            Session("Usuario") = value
        End Set
    End Property

    Public Property Delegacion As Clases.Delegacion
        Get
            Return Session("Delegacion")
        End Get
        Set(value As Clases.Delegacion)
            Session("Delegacion") = value
        End Set
    End Property

    Public Property CertificadosDisponiveis() As List(Of Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado)
        Get
            Return Session("CertificadosDisponiveis")
        End Get
        Set(value As List(Of Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado))
            Session("CertificadosDisponiveis") = value
        End Set
    End Property

    Public Property CertificadosSelecionados() As List(Of Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado)
        Get
            Return Session("CertificadosSelecionados")
        End Get
        Set(value As List(Of Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado))
            Session("CertificadosSelecionados") = value
        End Set
    End Property

    Public Event UpdatedControl(reabrirModal As Boolean)

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.lblError.Visible = False
        If Request.Form("__EVENTARGUMENT") = "Fechar" Then
            'Verifica se deve fechar a tela ou exibir os erros
            If Me.CertificadosSelecionados.Exists(Function(er) er.CodigoEstado = "ERROR") Then

                For Each ok In Me.CertificadosSelecionados.Where(Function(c) c.CodigoEstado = "OK")
                    CertificadosDisponiveis.RemoveAll(Function(d) d.IdentificadorCertificado = ok.IdentificadorCertificado)
                Next

                Dim erros As New System.Text.StringBuilder
                erros.AppendLine("</br>")
                For Each er In Me.CertificadosSelecionados.Where(Function(c) c.CodigoEstado = "ERROR")
                    erros.AppendFormat("Cliente: <b>{0}</b>", er.Cliente.Descripcion)
                    erros.AppendLine(er.IdentificadorCertificadoAnterior)
                    erros.AppendLine("</br></br>")
                Next

                Me.lblError.Visible = True
                Me.lblError.Text = erros.ToString

                'Atualiza o Grid
                PopularGrid(CertificadosDisponiveis)
            Else
                RaiseEvent UpdatedControl(False)
            End If
        End If
    End Sub

    Protected Overrides Sub TraduzirControles()
        Me.lblCertificadosSelecionados.Text = Traduzir("073_lblCertificadosSelecionados")
    End Sub

    Public Sub Cancelar(idDiv)
        Me.btnCancelar.Attributes.Add("onclick", String.Format("displayElemento('{0}','none'); return false;", idDiv))
    End Sub

    Public Sub PopularGrid(certificados As List(Of Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado))
        Me.CertificadosDisponiveis = certificados
        Me.gridCertificados.DataSource = certificados
        Me.gridCertificados.DataBind()
    End Sub
    Protected Sub gridCertificados_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Text = Traduzir("073_grid_fyhcertificado")
            e.Row.Cells(2).Text = Traduzir("073_grid_cliente")
            e.Row.Cells(3).Text = Traduzir("073_grid_codigo")
            e.Row.Cells(4).Text = Traduzir("073_grid_de_tipo")
            e.Row.Cells(5).Text = Traduzir("073_grid_para_tipo")
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            Dim certificado As Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado = e.Row.DataItem
            If certificado IsNot Nothing Then
                Dim do_tipo As Label = e.Row.FindControl("do_tipo")
                If do_tipo IsNot Nothing Then
                    do_tipo.Text = Traduzir("073_estado_certificado_" + certificado.CodigoEstado)
                End If

                Dim para_tipo As Label = e.Row.FindControl("para_tipo")
                If para_tipo IsNot Nothing Then
                    If certificado.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE Then
                        para_tipo.Text = Traduzir("073_estado_certificado_" + Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE)
                    ElseIf certificado.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE Then
                        para_tipo.Text = Traduzir("073_estado_certificado_" + Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO)
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As System.EventArgs)
        Me.Delegacion = Base.InformacionUsuario.DelegacionSeleccionada
        Me.Usuario = Base.InformacionUsuario.Nombre
        Dim selecionados As New List(Of String)
        For Each gdRow As GridViewRow In gridCertificados.Rows
            Dim chbSelecionar As CheckBox = gdRow.FindControl("chbSelecionar")
            If chbSelecionar.Checked Then
                Dim lblIdCertificado As Label = gdRow.FindControl("IdCertificado")
                selecionados.Add(lblIdCertificado.Text)
            End If
        Next

        If selecionados.Count = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(Traduzir("073_lbl_cerificao_converitr_nao_selecionado"), Nothing) _
                                                       , True)
        Else
            Dim objCertificadosSelecionados As New List(Of Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado)
            Dim aConverter As New List(Of Integer)
            Dim i As Integer = 0
            For Each oidCertificado In selecionados
                aConverter.Add(i)
                i = i + 1
                objCertificadosSelecionados.Add(Me.CertificadosDisponiveis.Where(Function(c) c.IdentificadorCertificado = oidCertificado).FirstOrDefault.Clonar())
            Next

            Me.CertificadosSelecionados = objCertificadosSelecionados
            Dim objCertificado = objCertificadosSelecionados.FirstOrDefault

            Dim do_tipo As String = String.Empty
            Dim para_tipo As String = String.Empty
            If objCertificado.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE Then
                do_tipo = Traduzir("073_estado_certificado_" + Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE)
                para_tipo = Traduzir("073_estado_certificado_" + Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE)
            ElseIf objCertificado.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE Then
                do_tipo = Traduzir("073_estado_certificado_" + Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE)
                para_tipo = Traduzir("073_estado_certificado_" + Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO)
            End If

            Dim faltam As Integer = selecionados.Count
            Dim mensagem = String.Format(Traduzir("073_lbl_convertendo"), do_tipo, objCertificado.Cliente.Descripcion, para_tipo, faltam, selecionados.Count)

            Dim script As String = String.Format("iniciarExecucao('{0}',[" + String.Join(",", aConverter) + "]);", mensagem)
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, Guid.NewGuid.ToString(), script, True)

        End If
    End Sub

End Class