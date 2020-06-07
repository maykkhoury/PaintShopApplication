<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class importPrices
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(importPrices))
        Me.butBrowse = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.butUpdate = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtExcelFile = New garageApp.MyTextBox()
        Me.fileDialogPrices = New System.Windows.Forms.OpenFileDialog()
        Me.lbLoading = New System.Windows.Forms.Label()
        Me.prgBarImport = New System.Windows.Forms.ProgressBar()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'butBrowse
        '
        Me.butBrowse.BackColor = System.Drawing.SystemColors.Control
        Me.butBrowse.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butBrowse.Location = New System.Drawing.Point(322, 15)
        Me.butBrowse.Name = "butBrowse"
        Me.butBrowse.Size = New System.Drawing.Size(37, 26)
        Me.butBrowse.TabIndex = 88
        Me.butBrowse.Text = "--"
        Me.butBrowse.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(13, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 15)
        Me.Label10.TabIndex = 87
        Me.Label10.Text = "File Path:"
        '
        'butUpdate
        '
        Me.butUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.butUpdate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butUpdate.Location = New System.Drawing.Point(6, 53)
        Me.butUpdate.Name = "butUpdate"
        Me.butUpdate.Size = New System.Drawing.Size(362, 57)
        Me.butUpdate.TabIndex = 85
        Me.butUpdate.Text = "Update Prices"
        Me.butUpdate.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtExcelFile)
        Me.GroupBox2.Controls.Add(Me.butBrowse)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.butUpdate)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(374, 117)
        Me.GroupBox2.TabIndex = 89
        Me.GroupBox2.TabStop = False
        '
        'txtExcelFile
        '
        Me.txtExcelFile.Location = New System.Drawing.Point(87, 18)
        Me.txtExcelFile.Name = "txtExcelFile"
        Me.txtExcelFile.ReadOnly = True
        Me.txtExcelFile.Size = New System.Drawing.Size(229, 20)
        Me.txtExcelFile.TabIndex = 90
        '
        'fileDialogPrices
        '
        Me.fileDialogPrices.FileName = " "
        '
        'lbLoading
        '
        Me.lbLoading.AutoSize = True
        Me.lbLoading.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLoading.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbLoading.Location = New System.Drawing.Point(0, 140)
        Me.lbLoading.Name = "lbLoading"
        Me.lbLoading.Size = New System.Drawing.Size(217, 15)
        Me.lbLoading.TabIndex = 90
        Me.lbLoading.Text = "Import in progress, please wait..."
        Me.lbLoading.Visible = False
        '
        'prgBarImport
        '
        Me.prgBarImport.Location = New System.Drawing.Point(224, 128)
        Me.prgBarImport.Name = "prgBarImport"
        Me.prgBarImport.Size = New System.Drawing.Size(156, 23)
        Me.prgBarImport.TabIndex = 91
        Me.prgBarImport.Visible = False
        '
        'importPrices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(402, 155)
        Me.Controls.Add(Me.prgBarImport)
        Me.Controls.Add(Me.lbLoading)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "importPrices"
        Me.Text = "importPrices"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents butBrowse As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents butUpdate As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents fileDialogPrices As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtExcelFile As garageApp.MyTextBox
    Friend WithEvents lbLoading As System.Windows.Forms.Label
    Friend WithEvents prgBarImport As System.Windows.Forms.ProgressBar
End Class
