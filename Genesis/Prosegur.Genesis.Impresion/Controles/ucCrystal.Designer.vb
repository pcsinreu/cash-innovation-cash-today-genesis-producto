<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucCrystal
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.crvRelatorios = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'crvRelatorios
        '
        Me.crvRelatorios.ActiveViewIndex = -1
        Me.crvRelatorios.AutoScroll = True
        Me.crvRelatorios.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.crvRelatorios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvRelatorios.DisplayBackgroundEdge = False
        Me.crvRelatorios.DisplayStatusBar = False
        Me.crvRelatorios.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvRelatorios.EnableDrillDown = False
        Me.crvRelatorios.EnableToolTips = False
        Me.crvRelatorios.Location = New System.Drawing.Point(0, 0)
        Me.crvRelatorios.Name = "crvRelatorios"
        Me.crvRelatorios.SelectionFormula = ""
        Me.crvRelatorios.ShowCloseButton = False
        Me.crvRelatorios.ShowGotoPageButton = False
        Me.crvRelatorios.ShowGroupTreeButton = False
        Me.crvRelatorios.ShowRefreshButton = False
        Me.crvRelatorios.ShowTextSearchButton = False
        Me.crvRelatorios.Size = New System.Drawing.Size(800, 600)
        Me.crvRelatorios.TabIndex = 0
        Me.crvRelatorios.ViewTimeSelectionFormula = ""
        '
        'ucCrystal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.crvRelatorios)
        Me.Name = "ucCrystal"
        Me.Size = New System.Drawing.Size(800, 600)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crvRelatorios As CrystalDecisions.Windows.Forms.CrystalReportViewer

End Class
