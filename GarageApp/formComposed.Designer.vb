<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formComposed
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formComposed))
        Me.butExit = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grp3 = New System.Windows.Forms.GroupBox()
        Me.Button3A = New System.Windows.Forms.Button()
        Me.Button3B = New System.Windows.Forms.Button()
        Me.grp2 = New System.Windows.Forms.GroupBox()
        Me.Button2A = New System.Windows.Forms.Button()
        Me.Button2B = New System.Windows.Forms.Button()
        Me.grp1 = New System.Windows.Forms.GroupBox()
        Me.Button1B = New System.Windows.Forms.Button()
        Me.Button1A = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        Me.grp3.SuspendLayout()
        Me.grp2.SuspendLayout()
        Me.grp1.SuspendLayout()
        Me.SuspendLayout()
        '
        'butExit
        '
        Me.butExit.Location = New System.Drawing.Point(18, 214)
        Me.butExit.Name = "butExit"
        Me.butExit.Size = New System.Drawing.Size(136, 23)
        Me.butExit.TabIndex = 2
        Me.butExit.Text = "Exit"
        Me.butExit.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grp3)
        Me.GroupBox2.Controls.Add(Me.grp2)
        Me.GroupBox2.Controls.Add(Me.grp1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(312, 196)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        '
        'grp3
        '
        Me.grp3.Controls.Add(Me.Button3A)
        Me.grp3.Controls.Add(Me.Button3B)
        Me.grp3.Location = New System.Drawing.Point(6, 132)
        Me.grp3.Name = "grp3"
        Me.grp3.Size = New System.Drawing.Size(298, 55)
        Me.grp3.TabIndex = 11
        Me.grp3.TabStop = False
        Me.grp3.Text = "Z"
        '
        'Button3A
        '
        Me.Button3A.Location = New System.Drawing.Point(10, 19)
        Me.Button3A.Name = "Button3A"
        Me.Button3A.Size = New System.Drawing.Size(136, 23)
        Me.Button3A.TabIndex = 5
        Me.Button3A.Text = "Button3A"
        Me.Button3A.UseVisualStyleBackColor = True
        '
        'Button3B
        '
        Me.Button3B.Location = New System.Drawing.Point(152, 19)
        Me.Button3B.Name = "Button3B"
        Me.Button3B.Size = New System.Drawing.Size(136, 23)
        Me.Button3B.TabIndex = 8
        Me.Button3B.Text = "Button3B"
        Me.Button3B.UseVisualStyleBackColor = True
        '
        'grp2
        '
        Me.grp2.Controls.Add(Me.Button2A)
        Me.grp2.Controls.Add(Me.Button2B)
        Me.grp2.Location = New System.Drawing.Point(6, 75)
        Me.grp2.Name = "grp2"
        Me.grp2.Size = New System.Drawing.Size(298, 55)
        Me.grp2.TabIndex = 10
        Me.grp2.TabStop = False
        Me.grp2.Text = "Y"
        '
        'Button2A
        '
        Me.Button2A.Location = New System.Drawing.Point(10, 19)
        Me.Button2A.Name = "Button2A"
        Me.Button2A.Size = New System.Drawing.Size(136, 23)
        Me.Button2A.TabIndex = 1
        Me.Button2A.Text = "Button2A"
        Me.Button2A.UseVisualStyleBackColor = True
        '
        'Button2B
        '
        Me.Button2B.Location = New System.Drawing.Point(152, 19)
        Me.Button2B.Name = "Button2B"
        Me.Button2B.Size = New System.Drawing.Size(136, 23)
        Me.Button2B.TabIndex = 7
        Me.Button2B.Text = "Button2B"
        Me.Button2B.UseVisualStyleBackColor = True
        '
        'grp1
        '
        Me.grp1.Controls.Add(Me.Button1B)
        Me.grp1.Controls.Add(Me.Button1A)
        Me.grp1.Location = New System.Drawing.Point(6, 19)
        Me.grp1.Name = "grp1"
        Me.grp1.Size = New System.Drawing.Size(298, 55)
        Me.grp1.TabIndex = 9
        Me.grp1.TabStop = False
        Me.grp1.Text = "X"
        '
        'Button1B
        '
        Me.Button1B.Location = New System.Drawing.Point(152, 19)
        Me.Button1B.Name = "Button1B"
        Me.Button1B.Size = New System.Drawing.Size(136, 23)
        Me.Button1B.TabIndex = 6
        Me.Button1B.Text = "Button1B"
        Me.Button1B.UseVisualStyleBackColor = True
        '
        'Button1A
        '
        Me.Button1A.Location = New System.Drawing.Point(10, 19)
        Me.Button1A.Name = "Button1A"
        Me.Button1A.Size = New System.Drawing.Size(136, 23)
        Me.Button1A.TabIndex = 0
        Me.Button1A.Text = "Button1A"
        Me.Button1A.UseVisualStyleBackColor = True
        '
        'formComposed
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 239)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.butExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formComposed"
        Me.GroupBox2.ResumeLayout(False)
        Me.grp3.ResumeLayout(False)
        Me.grp2.ResumeLayout(False)
        Me.grp1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents butExit As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grp3 As System.Windows.Forms.GroupBox
    Friend WithEvents Button3A As System.Windows.Forms.Button
    Friend WithEvents Button3B As System.Windows.Forms.Button
    Friend WithEvents grp2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2A As System.Windows.Forms.Button
    Friend WithEvents Button2B As System.Windows.Forms.Button
    Friend WithEvents grp1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1B As System.Windows.Forms.Button
    Friend WithEvents Button1A As System.Windows.Forms.Button
End Class
