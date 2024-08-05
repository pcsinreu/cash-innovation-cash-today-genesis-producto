
Public Class TreeViewHelper

    ''' <summary>
    ''' Fixes the ASP.NET 2.0 TreeView's inability to select a node (and have the style
    ''' properly applied at the client) without PostBack.
    ''' </summary>
    ''' <param name="tv">The TreeView object that is being fixed</param>
    ''' <param name="SelectedClassStyleID">The ID emitted by ASP.NET for the TreeView's SelectedClass (e.g., \"TreeView1_3\"). View Source for the page with the TreeView control and look at the top for the classes emitted in the style block.</param>
    ''' <param name="SelectedHyperLinkStyleID">The ID emitted by ASP.NET for the TreeView's SelectedHyperLinkClass (leave blank if none). View Source for the page with the TreeView control and look at the top for the classes emitted in the style block.</param>
    ''' <remarks></remarks>
    Public Shared Sub ClientSelectFix(plh As PlaceHolder, tv As TreeView, SelectedClassStyleID As String, SelectedHyperLinkStyleID As String)

        ' Make a couple of modifications to the TreeView in preparation
        tv.Target = "_self"
        DisableAutoPostBack(tv.Nodes)

        ' Now build work-around for the ASP.NET 2.0 TreeView's JavaScript library
        Dim TreeViewID As String = tv.ClientID

        ' The SelectedClassStyleID and SelectedHyperLinkStyleID may change depending on the styles
        ' supplied for the TreeView, and may change as styles are modified. Look at the <style>
        ' block at the top of the generated HTML page to determine the names given to these classes.
        ' Leave blank to emit a blank classname.

        Dim SelectedClass As String = String.Empty

        If SelectedClassStyleID <> String.Empty Then
            SelectedClass = TreeViewID + "_" + SelectedClassStyleID
        End If

        Dim SelectedHyperLinkClass As String = String.Empty

        If (SelectedHyperLinkStyleID <> String.Empty) Then
            SelectedHyperLinkClass = TreeViewID + "_" + SelectedHyperLinkStyleID
        End If

        Dim TreeViewClientSelectFix As New StringBuilder  ' The string within which to build the JavaScript fix

        With TreeViewClientSelectFix
            .AppendLine("<script>")
            .AppendLine("// These two elements are not emitted into the " + TreeViewID + "_Data")
            .AppendLine("// structure if a HoverNodeStyle exists, preventing client-side node")
            .AppendLine("// selection from working.")
            .AppendLine("" + TreeViewID + "_Data.selectedClass = '" + SelectedClass + "';")
            .AppendLine("" + TreeViewID + "_Data.selectedHyperLinkClass = '" + SelectedHyperLinkClass + "';")

            .AppendLine("// This is a duplicate of the Microsoft TreeView JavaScript library")
            .AppendLine("// function, but appears later in the page and overrides the library")
            .AppendLine("// version. This one does not Unhover a node if it has the 'selected'")
            .AppendLine("// class applied to it.")
            .AppendLine("function TreeView_UnhoverNode(node)")
            .AppendLine("{")
            .AppendLine("  if (node.className.indexOf('" + SelectedClass + "') == -1)")
            .AppendLine("  {")
            .AppendLine("    WebForm_RemoveClassName(node, node.hoverClass)")
            .AppendLine("    If (__nonMSDOMBrowser) Then")
            .AppendLine("    {")
            .AppendLine("        node = node.childNodes[node.childNodes.length - 1];")
            .AppendLine("    }")
            .AppendLine("    Else")
            .AppendLine("    {")
            .AppendLine("        node = node.children[node.children.length - 1];")
            .AppendLine("    }")
            .AppendLine("    WebForm_RemoveClassName(node, node.hoverHyperLinkClass);")
            .AppendLine("  }")
            .AppendLine("}")

            .AppendLine("// This is a duplicate of the Microsoft TreeView JavaScript library")
            .AppendLine("// function, but appears later in the page and overrides the library")
            .AppendLine("// version. This one takes care to remove the hoverClass from the")
            .AppendLine("// de-selected node.  Contributed by Pushpendra.")
            .AppendLine("function TreeView_SelectNode(data, node, nodeId)")
            .AppendLine("{")
            .AppendLine("   if ((typeof(data.selectedClass) != 'undefined') && (data.selectedClass != null))")
            .AppendLine("   {")
            .AppendLine("      var id = data.selectedNodeID.value;")
            .AppendLine("      If (id.length > 0) Then")
            .AppendLine("      {")
            .AppendLine("         var selectedNode = document.getElementById(id);")
            .AppendLine("         if ((typeof(selectedNode) != 'undefined') && (selectedNode != null))")
            .AppendLine("         {")
            .AppendLine("            WebForm_RemoveClassName(selectedNode, data.selectedHyperLinkClass);")
            .AppendLine("            selectedNode = WebForm_GetParentByTagName(selectedNode, 'TD');")
            .AppendLine("            WebForm_RemoveClassName(selectedNode, data.selectedClass);")
            .AppendLine("            //removing the extra class from the node's parent Element")
            .AppendLine("            WebForm_RemoveClassName(selectedNode, data.hoverClass);")
            .AppendLine("          }")
            .AppendLine("      }")
            .AppendLine("      WebForm_AppendToClassName(node, data.selectedHyperLinkClass);")
            .AppendLine("      node = WebForm_GetParentByTagName(node, 'TD');")
            .AppendLine("      WebForm_AppendToClassName(node, Data.selectedClass)")
            .AppendLine("   }")
            .AppendLine("   data.selectedNodeID.value = nodeId;")
            .AppendLine("}")
            .AppendLine("</script>")
        End With

        plh.Controls.Clear()
        plh.Controls.Add(New LiteralControl(TreeViewClientSelectFix.ToString()))

    End Sub

    Public Shared Sub DisableAutoPostBack(tnc As TreeNodeCollection)

        ' Loop over every node in the passed collection
        For Each tn As TreeNode In tnc

            ' Set the node's NavigateUrl (which equates to A HREF) to javascript:void(0);,
            ' effectively intercepting the click event and disabling PostBack.
            tn.NavigateUrl = "javascript:void(0);"

            ' Set the node's SelectAction to SelectExpand to enable client-side
            ' expansion/collapsing of parent nodes
            tn.SelectAction = TreeNodeSelectAction.SelectExpand

            ' If this node has children, recurse over them as well before returning
            If (tn.ChildNodes.Count > 0) Then
                DisableAutoPostBack(tn.ChildNodes)
            End If

        Next

    End Sub

End Class



