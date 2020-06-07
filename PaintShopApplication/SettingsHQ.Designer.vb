<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsHQ
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingsHQ))
        Me.butDone = New System.Windows.Forms.Button()
        Me.grpSettings = New System.Windows.Forms.GroupBox()
        Me.lbCurrency = New System.Windows.Forms.Label()
        Me.cmbCurrency = New System.Windows.Forms.ComboBox()
        Me.lbUnit = New System.Windows.Forms.Label()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.grpSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'butDone
        '
        Me.butDone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butDone.Location = New System.Drawing.Point(276, 141)
        Me.butDone.Name = "butDone"
        Me.butDone.Size = New System.Drawing.Size(64, 23)
        Me.butDone.TabIndex = 4
        Me.butDone.Text = "Done"
        Me.butDone.UseVisualStyleBackColor = True
        '
        'grpSettings
        '
        Me.grpSettings.Controls.Add(Me.lbCurrency)
        Me.grpSettings.Controls.Add(Me.cmbCurrency)
        Me.grpSettings.Controls.Add(Me.lbUnit)
        Me.grpSettings.Controls.Add(Me.cmbUnit)
        Me.grpSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpSettings.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpSettings.Location = New System.Drawing.Point(14, 21)
        Me.grpSettings.Name = "grpSettings"
        Me.grpSettings.Size = New System.Drawing.Size(326, 111)
        Me.grpSettings.TabIndex = 55
        Me.grpSettings.TabStop = False
        Me.grpSettings.Text = "Settings"
        '
        'lbCurrency
        '
        Me.lbCurrency.AutoSize = True
        Me.lbCurrency.Location = New System.Drawing.Point(27, 68)
        Me.lbCurrency.Name = "lbCurrency"
        Me.lbCurrency.Size = New System.Drawing.Size(61, 13)
        Me.lbCurrency.TabIndex = 3
        Me.lbCurrency.Text = "Currency:"
        '
        'cmbCurrency
        '
        Me.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurrency.FormattingEnabled = True
        Me.cmbCurrency.Location = New System.Drawing.Point(99, 64)
        Me.cmbCurrency.Name = "cmbCurrency"
        Me.cmbCurrency.Size = New System.Drawing.Size(221, 21)
        Me.cmbCurrency.TabIndex = 2
        '
        'lbUnit
        '
        Me.lbUnit.AutoSize = True
        Me.lbUnit.Location = New System.Drawing.Point(27, 41)
        Me.lbUnit.Name = "lbUnit"
        Me.lbUnit.Size = New System.Drawing.Size(34, 13)
        Me.lbUnit.TabIndex = 1
        Me.lbUnit.Text = "Unit:"
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Location = New System.Drawing.Point(99, 37)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(221, 21)
        Me.cmbUnit.TabIndex = 0
        '
        'SettingsHQ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(352, 176)
        Me.Controls.Add(Me.grpSettings)
        Me.Controls.Add(Me.butDone)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SettingsHQ"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.grpSettings.ResumeLayout(False)
        Me.grpSettings.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents butDone As System.Windows.Forms.Button
    Friend WithEvents grpSettings As System.Windows.Forms.GroupBox
    Friend WithEvents lbCurrency As System.Windows.Forms.Label
    Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents lbUnit As System.Windows.Forms.Label
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
End Class
