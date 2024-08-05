Imports Prosegur.Genesis

Partial Public Class Empty
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'Carregando a TREEVIEW



        If Not Page.IsPostBack Then
            ' adicionar codigo
            'objPeticion.CodigoAgrupacion.Add("agrupaa")
            'objPeticion.CodigoAgrupacion.Add("agrupab")

            Dim strCodigoAgrupacao As String = String.Empty

            ObterDadosAgrupaciones("agrupaa")

            'Dim objDs As DataSet = ObterDados()


            'For Each masterRow As DataRow In objDs.Tables("Pai").Rows

            '    'Carrega o Pai
            '    Dim masternode As New TreeNode(CStr(masterRow("Nome")))
            '    'masternode.ImageUrl = "~/Imagenes/contain.gif"
            '    TreeView1.Nodes.Add(masternode)

            '    Dim objeto As New TreeNode

            '    For Each childrow As Object In masterRow.GetChildRows("pai_filho")
            '        Dim childNode As New TreeNode
            '        childNode.Text = childrow("Nome").ToString
            '        childNode.Value = childrow("CodigoFilho").ToString
            '        masternode.ChildNodes.Add(childNode)

            '        For x As Integer = 0 To 4 Step 1
            '            childNode.ChildNodes.Add(New TreeNode("Sub Produto Neto " & x.ToString))

            '            'For y As Integer = 1 To 2 Step 1
            '            childNode.ChildNodes(0).ChildNodes.Add(New TreeNode("Sub Produto Bis Neto " & (x + 1).ToString))
            '            'y += 1
            '            'Next

            '            'x += 1
            '        Next

            'Next

            '    Next


        End If





    End Sub


    Public Sub ObterDadosAgrupaciones(pCodigoAgrupacao As String)
        Dim objPeticion As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.Peticion
        objPeticion.CodigoAgrupacion = New List(Of String)
        objPeticion.CodigoAgrupacion.Add(pCodigoAgrupacao)

        ' invocar logica negocio
        Dim objProxy As New Comunicacion.ProxyAgrupacion
        'Dim lnAccionAgrupacion As New LogicaNegocio.AccionAgrupacion
        Dim objRespuesta As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Respuesta = objProxy.GetAgrupacionesDetail(objPeticion)


        TreeView1.Nodes.Clear()

        For Each objDivisa As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.Divisa In objRespuesta.Agrupaciones(0).Divisas

            Dim objTreeNodeDivisa As New TreeNode(objDivisa.Descripcion, objDivisa.CodigoIso)
            Dim objTreeNodeTipoMedioPago As TreeNode
            Dim objTreeNodeMedioPago As TreeNode

            If objDivisa.TieneEfectivo Then
                'Adiciona o nó efetivo
                objTreeNodeTipoMedioPago = New TreeNode("Efetivo", "Efetivo")
                objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)
            End If

            For Each TipoMedioPago As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.TipoMedioPago In objDivisa.TiposMedioPago
                'Adiciona Nós de Tipo Médio Pago
                objTreeNodeTipoMedioPago = New TreeNode(TipoMedioPago.Descripcion, TipoMedioPago.Codigo)

                For Each MedioPago As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.MedioPago In TipoMedioPago.MediosPago
                    'Adiciona Nós de Médio Pago
                    objTreeNodeMedioPago = New TreeNode(MedioPago.Descripcion, MedioPago.Codigo)
                    objTreeNodeTipoMedioPago.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeTipoMedioPago.ChildNodes, objTreeNodeMedioPago), objTreeNodeMedioPago)
                Next

                objTreeNodeDivisa.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeDivisa.ChildNodes, objTreeNodeTipoMedioPago), objTreeNodeTipoMedioPago)

            Next

            'Adiciona a divisa na Tree
            TreeView1.Nodes.AddAt(IndiceOrdenacao(TreeView1.Nodes, objTreeNodeDivisa), objTreeNodeDivisa)

        Next






    End Sub



    Public Function ObterDados() As DataSet

        Dim objDTPai As New DataTable("Pai")
        Dim objDTFilho As New DataTable("Filho")
        Dim objDTNeto As New DataTable("Neto")


        objDTPai.Columns.Add("CodigoPai")
        objDTPai.Columns.Add("Nome")

        objDTFilho.Columns.Add("CodigoFilho")
        objDTFilho.Columns.Add("Nome")
        objDTFilho.Columns.Add("CodigoPai")

        objDTPai.Rows.Add(objDTPai.NewRow)
        objDTPai.Rows(objDTPai.Rows.Count - 1)("CodigoPai") = 1
        objDTPai.Rows(objDTPai.Rows.Count - 1)("Nome") = "Produto 1"

        objDTFilho.Rows.Add(objDTFilho.NewRow)
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("CodigoPai") = 1
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("CodigoFilho") = 1
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("Nome") = "Sub Produto 1"

        objDTFilho.Rows.Add(objDTFilho.NewRow)
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("CodigoPai") = 1
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("CodigoFilho") = 2
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("Nome") = "Sub Produto 2"

        objDTFilho.Rows.Add(objDTFilho.NewRow)
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("CodigoPai") = 1
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("CodigoFilho") = 3
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("Nome") = "Sub Produto 3"




        objDTPai.Rows.Add(objDTPai.NewRow)
        objDTPai.Rows(objDTPai.Rows.Count - 1)("CodigoPai") = 2
        objDTPai.Rows(objDTPai.Rows.Count - 1)("Nome") = "Produto 2"

        objDTFilho.Rows.Add(objDTFilho.NewRow)
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("CodigoPai") = 2
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("CodigoFilho") = 2
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("Nome") = "Sub Produto A"

        objDTFilho.Rows.Add(objDTFilho.NewRow)
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("CodigoPai") = 2
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("CodigoFilho") = 3
        objDTFilho.Rows(objDTFilho.Rows.Count - 1)("Nome") = "Sub Produto B"


        Dim objDs As New DataSet

        objDs.Tables.Add(objDTPai)
        objDs.Tables.Add(objDTFilho)

        objDs.Relations.Add("pai_filho", objDTPai.Columns("CodigoPai"), objDTFilho.Columns("CodigoPai"))


        Return objDs


    End Function



    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'TreeView2.Nodes.Remove(TreeView2.SelectedNode)

        TreeView2.Nodes.Add(getChilds(TreeView1.SelectedNode))

    End Sub

    Protected Sub Button4_Click(sender As Object, e As System.EventArgs) Handles Button4.Click

        'InsereNaArvore(TreeView2, MontaArvoreSelecionada(TreeView1.SelectedNode))

        InsereNaArvoreDinamica(TreeView2.Nodes, MontaArvoreSelecionada(TreeView1.SelectedNode))


        'TreeView2.Nodes.Add(MontaArvoreSelecionada(TreeView1.SelectedNode))

        'TreeView2.Nodes(0).ChildNodes.Add(New TreeNode("teste"))
        'TreeView2.Nodes(0).ChildNodes(1).ChildNodes.Add(New TreeNode("filho de teste"))

    End Sub

    Protected Sub Button5_Click(sender As Object, e As System.EventArgs) Handles Button5.Click
        Try
            RemoveNode(TreeView2)
        Catch ex As Exception
            'Response.Write("Selecione um registro antes de excluir")
        End Try

    End Sub



#Region "Arvore Binária"
    ''' <summary>
    ''' Copia o nó
    ''' </summary>
    ''' <param name="objNode">Nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyNode(objNode As TreeNode) As TreeNode

        Dim TempNode As New TreeNode
        TempNode.Text = objNode.Text
        TempNode.Value = objNode.Value
        TempNode.Selected = objNode.Selected
        TempNode.Expanded = objNode.Expanded
        TempNode.ImageUrl = objNode.ImageUrl
        TempNode.ToolTip = objNode.ToolTip

        Return TempNode
    End Function

    ''' <summary>
    ''' Retorna os filhos de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getChilds(objTreeNode As TreeNode) As TreeNode
        Dim objTreeNodeRetorno As TreeNode = CopyNode(objTreeNode)

        If objTreeNode.ChildNodes.Count > 0 Then
            Dim objFilhoRetorno As TreeNode
            For Each objFilho As TreeNode In objTreeNode.ChildNodes
                objFilhoRetorno = getChilds(objFilho)
                objTreeNodeRetorno.ChildNodes.Add(objFilhoRetorno)
            Next
        End If

        Return objTreeNodeRetorno

    End Function

    ''' <summary>
    ''' Retorna os páis do nó de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getParent(ByRef objTreeNode As TreeNode)

        Dim objTreeNodeCorrente As TreeNode = CopyNode(objTreeNode)
        If objTreeNode.Parent IsNot Nothing Then
            Dim objPai As TreeNode = getParent(objTreeNode.Parent)
            objPai.ChildNodes.Add(objTreeNodeCorrente)
            Return objPai.ChildNodes(0)
        Else
            Return objTreeNodeCorrente
        End If

    End Function
    ''' <summary>
    ''' Retorna o nó selecionado de forma hierárquica
    ''' </summary>
    ''' <param name="pObjSelecionado">Objeto nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaArvoreSelecionada(pObjSelecionado As TreeNode) As TreeNode

        'Se for o nível de raiz,inclui os filhos
        If pObjSelecionado.Depth = 0 Then

            Dim objNoFilhos As TreeNode = getChilds(pObjSelecionado)

            Return objNoFilhos
        Else

            'Dado um objeto nó selecionado, retorna os pais e filhos a serem inseridos na coleção
            Dim objNoSelecionado As TreeNode = getParent(pObjSelecionado)
            Dim objNoFilhos As TreeNode = getChilds(pObjSelecionado)

            'Adiciona os filhos
            If objNoSelecionado IsNot Nothing Then
                Dim objTreeNodeChildCol As TreeNodeCollection = objNoFilhos.ChildNodes
                While objTreeNodeChildCol.Count > 0
                    objNoSelecionado.ChildNodes.Add(objNoFilhos.ChildNodes(0))
                End While
            End If

            'Suspende para o nível Root
            Dim objGetFather As TreeNode = objNoSelecionado
            While objGetFather.Parent IsNot Nothing
                objGetFather = objGetFather.Parent
            End While

            'Retorna o nó selecionado de forma hierárquica
            Return objGetFather
        End If

    End Function
    ''' <summary>
    ''' Remode o nó selecionado da Treeview informada
    ''' </summary>
    ''' <param name="pObjTreeView">Treevie a ser retirado o nó</param>
    ''' <remarks></remarks>
    Public Sub RemoveNode(ByRef pObjTreeView As TreeView)


        Dim objPai As TreeNode = pObjTreeView.SelectedNode.Parent
        Dim objDelete As TreeNode = pObjTreeView.SelectedNode

        While objPai IsNot Nothing
            objPai.ChildNodes.Remove(objDelete)

            If objPai.ChildNodes.Count = 0 Then
                objDelete = objPai
                objPai = objPai.Parent
            Else
                Exit While
            End If
        End While

        If objDelete IsNot Nothing Then
            pObjTreeView.Nodes.Remove(objDelete)
        End If

    End Sub
    ''' <summary>
    ''' Posiciona o nó selecionado na árvore de destino
    ''' </summary>
    ''' <param name="pObjTreeView">Coleção de nós a ser verificada</param>
    ''' <param name="pObjSelecionado">Objeto selecionado(Hierárquico)</param>
    ''' <remarks></remarks>
    Public Sub InsereNaArvoreDinamica(pObjTreeView As TreeNodeCollection, pObjSelecionado As TreeNode)


        Dim objExiste As TreeNode = pObjSelecionado
        Dim ObjColCorrente As TreeNodeCollection = pObjTreeView

        'Caso não exista nenhum nó na árvore adiciona o primeiro
        If ObjColCorrente.Count = 0 Then
            ObjColCorrente.Add(objExiste)
            Exit Sub
        End If

        While objExiste IsNot Nothing

            Dim addNo As Boolean = True
            'Verifica na árvore de destino se o objeto selecionado existe
            For Each pObjSelecao As TreeNode In ObjColCorrente
                If pObjSelecao.Text = objExiste.Text Then
                    'Se existe filho então continua o processamento
                    If pObjSelecao.ChildNodes.Count > 0 Then
                        'Se selecionou um nó pai inclui novamente os filhos a partir da seleção efetuada
                        If objExiste.ChildNodes.Count > 0 AndAlso objExiste.Selected Then
                            pObjSelecao.ChildNodes.Clear()
                            Dim objNoSelecionado As TreeNode = pObjSelecao
                            Dim objNoFilhos As TreeNode = getChilds(objExiste)
                            If objNoSelecionado IsNot Nothing Then
                                Dim objTreeNodeChildCol As TreeNodeCollection = objNoFilhos.ChildNodes
                                While objTreeNodeChildCol.Count > 0
                                    objNoSelecionado.ChildNodes.Add(objNoFilhos.ChildNodes(0))
                                End While
                            End If
                            Exit Sub
                        End If
                        'Passa o próximo filho do objeto selecionado para ser verificado
                        objExiste = objExiste.ChildNodes(0)
                        'Passa a próxima coleção da árvore de destino para ser verificada
                        ObjColCorrente = pObjSelecao.ChildNodes
                        addNo = False
                        Exit For
                    Else
                        Exit Sub
                    End If
                End If

            Next

            'Adiciona na árvore de forma ordenada
            If addNo Then
                ObjColCorrente.AddAt(IndiceOrdenacao(ObjColCorrente, objExiste), objExiste)
                Exit While
            End If

        End While


    End Sub

    ''' <summary>
    ''' Retorna o índice antes de inserir o nó na coleção passada
    ''' </summary>
    ''' <param name="TreeNodeCol">Coleção a ser verificada a posição</param>
    ''' <param name="treenode">Nó para inclusão</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IndiceOrdenacao(TreeNodeCol As TreeNodeCollection, treenode As TreeNode) As Integer
        Dim treeNodeColTemp As New TreeNodeCollection

        For Each obj As TreeNode In TreeNodeCol
            treeNodeColTemp.Add(CopyNode(obj))
        Next

        If treeNodeColTemp.Count > 0 Then
            treeNodeColTemp.Add(CopyNode(treenode))

            Dim retorno = From objTree In treeNodeColTemp Order By objTree.Text Ascending

            Dim i As Integer = 0
            For Each objRetorno As Object In retorno
                If objRetorno.text = treenode.Text Then
                    Return i
                End If
                i += 1
            Next
        End If

        Return 0
    End Function

#End Region



    Public Sub InsereNaArvore(pObjTreeView As TreeView, pobjSelecionado As TreeNode)

        Dim objRealSelecionado As TreeNode = TreeView1.SelectedNode

        If pObjTreeView.Nodes.Count > 0 Then

            'Verifica Raiz
            Dim addNo As Boolean = True
            Dim addNoFilho As Boolean = True
            Dim addNoNeto As Boolean = True

            Dim root As TreeNode = Nothing
            Dim rootFilho As TreeNode = Nothing

            'Verifica na Raiz
            For Each objNo As TreeNode In pObjTreeView.Nodes
                If (objNo.Text.Equals(pobjSelecionado.Text) AndAlso objNo.Value.Equals(pobjSelecionado.Value)) Then
                    addNo = False
                    root = objNo
                    'Verifica os filhos
                    For Each objNoFilho As TreeNode In objNo.ChildNodes
                        If (objNoFilho.Text.Equals(pobjSelecionado.ChildNodes(0).Text) AndAlso objNoFilho.Value.Equals(pobjSelecionado.ChildNodes(0).Value)) Then
                            addNoFilho = False
                            rootFilho = objNoFilho
                            'Verifica os Netos
                            For Each objNoNeto As TreeNode In objNoFilho.ChildNodes
                                If (objNoNeto.Text.Equals(pobjSelecionado.ChildNodes(0).ChildNodes(0).Text) AndAlso objNoNeto.Value.Equals(pobjSelecionado.ChildNodes(0).ChildNodes(0).Value)) Then
                                    addNoNeto = False
                                    Exit For
                                End If
                            Next
                            Exit For
                        End If
                    Next
                    Exit For
                End If
            Next

            If addNo Then
                pObjTreeView.Nodes. _
                AddAt(IndiceOrdenacao(pObjTreeView.Nodes, pobjSelecionado), pobjSelecionado)
            ElseIf addNoFilho Then
                pObjTreeView.Nodes(pObjTreeView.Nodes.IndexOf(root)).ChildNodes _
                .AddAt(IndiceOrdenacao(pObjTreeView.Nodes(pObjTreeView.Nodes.IndexOf(root)).ChildNodes, pobjSelecionado.ChildNodes(0)), pobjSelecionado.ChildNodes(0))

            ElseIf addNoNeto Then
                pObjTreeView.Nodes(pObjTreeView.Nodes.IndexOf(root)).ChildNodes(pObjTreeView.Nodes(pObjTreeView.Nodes.IndexOf(root)).ChildNodes.IndexOf(rootFilho)).ChildNodes _
                .AddAt(IndiceOrdenacao(pObjTreeView.Nodes(pObjTreeView.Nodes.IndexOf(root)).ChildNodes(pObjTreeView.Nodes(pObjTreeView.Nodes.IndexOf(root)).ChildNodes.IndexOf(rootFilho)).ChildNodes, pobjSelecionado.ChildNodes(0).ChildNodes(0)), pobjSelecionado.ChildNodes(0).ChildNodes(0))
            End If

        Else
            pObjTreeView.Nodes.Add(pobjSelecionado)
        End If


    End Sub



    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            RemoveNode(TreeView2)
        Catch ex As Exception
            'Response.Write("Selecione um registro antes de excluir!")
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            InsereNaArvoreDinamica(TreeView2.Nodes, MontaArvoreSelecionada(TreeView1.SelectedNode))
        Catch ex As Exception
            'Response.Write("Selecione um registro antes de incluir!")
        End Try

    End Sub
End Class