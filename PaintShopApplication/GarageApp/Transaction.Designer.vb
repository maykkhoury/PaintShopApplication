<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class transation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(transation))
        Me.grpTransaction = New System.Windows.Forms.GroupBox()
        Me.dpTransactionDate = New System.Windows.Forms.DateTimePicker()
        Me.lbIdCarForm = New System.Windows.Forms.Label()
        Me.lbTransactionDate = New System.Windows.Forms.Label()
        Me.lsvTransaction = New System.Windows.Forms.ListView()
        Me.cidTransaction = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cFormulaName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cTransactionDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cTransactionAmount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cDiscount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cTransactionCurrency = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cClientName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.butSearchTransaction = New System.Windows.Forms.Button()
        Me.grpTransaction.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpTransaction
        '
        Me.grpTransaction.BackColor = System.Drawing.Color.Transparent
        Me.grpTransaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.grpTransaction.Controls.Add(Me.dpTransactionDate)
        Me.grpTransaction.Controls.Add(Me.lbIdCarForm)
        Me.grpTransaction.Controls.Add(Me.lbTransactionDate)
        Me.grpTransaction.Controls.Add(Me.lsvTransaction)
        Me.grpTransaction.Controls.Add(Me.butSearchTransaction)
        Me.grpTransaction.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTransaction.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.grpTransaction.Location = New System.Drawing.Point(12, 12)
        Me.grpTransaction.Name = "grpTransaction"
        Me.grpTransaction.Size = New System.Drawing.Size(722, 296)
        Me.grpTransaction.TabIndex = 66
        Me.grpTransaction.TabStop = False
        '
        'dpTransactionDate
        '
        Me.dpTransactionDate.Location = New System.Drawing.Point(106, 25)
        Me.dpTransactionDate.Name = "dpTransactionDate"
        Me.dpTransactionDate.Size = New System.Drawing.Size(317, 23)
        Me.dpTransactionDate.TabIndex = 77
        Me.dpTransactionDate.Value = New Date(2012, 4, 9, 11, 27, 41, 0)
        '
        'lbIdCarForm
        '
        Me.lbIdCarForm.AutoSize = True
        Me.lbIdCarForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbIdCarForm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbIdCarForm.Location = New System.Drawing.Point(16, 55)
        Me.lbIdCarForm.Name = "lbIdCarForm"
        Me.lbIdCarForm.Size = New System.Drawing.Size(20, 15)
        Me.lbIdCarForm.TabIndex = 76
        Me.lbIdCarForm.Text = "-1"
        Me.lbIdCarForm.Visible = False
        '
        'lbTransactionDate
        '
        Me.lbTransactionDate.AutoSize = True
        Me.lbTransactionDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTransactionDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbTransactionDate.Location = New System.Drawing.Point(16, 25)
        Me.lbTransactionDate.Name = "lbTransactionDate"
        Me.lbTransactionDate.Size = New System.Drawing.Size(41, 15)
        Me.lbTransactionDate.TabIndex = 68
        Me.lbTransactionDate.Text = "Date:"
        '
        'lsvTransaction
        '
        Me.lsvTransaction.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lsvTransaction.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cidTransaction, Me.cFormulaName, Me.cTransactionDate, Me.cTransactionAmount, Me.cDiscount, Me.cTransactionCurrency, Me.cClientName})
        Me.lsvTransaction.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lsvTransaction.FullRowSelect = True
        Me.lsvTransaction.GridLines = True
        Me.lsvTransaction.HideSelection = False
        Me.lsvTransaction.Location = New System.Drawing.Point(9, 90)
        Me.lsvTransaction.MultiSelect = False
        Me.lsvTransaction.Name = "lsvTransaction"
        Me.lsvTransaction.Size = New System.Drawing.Size(706, 200)
        Me.lsvTransaction.TabIndex = 55
        Me.lsvTransaction.UseCompatibleStateImageBehavior = False
        Me.lsvTransaction.View = System.Windows.Forms.View.Details
        '
        'cidTransaction
        '
        Me.cidTransaction.Width = 0
        '
        'cFormulaName
        '
        Me.cFormulaName.Text = "Formula"
        Me.cFormulaName.Width = 124
        '
        'cTransactionDate
        '
        Me.cTransactionDate.Text = "Date"
        Me.cTransactionDate.Width = 101
        '
        'cTransactionAmount
        '
        Me.cTransactionAmount.Text = "Amount"
        Me.cTransactionAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.cTransactionAmount.Width = 108
        '
        'cDiscount
        '
        Me.cDiscount.Text = "Discount(%)"
        Me.cDiscount.Width = 123
        '
        'cTransactionCurrency
        '
        Me.cTransactionCurrency.Text = "Currency"
        Me.cTransactionCurrency.Width = 83
        '
        'cClientName
        '
        Me.cClientName.Text = "Cient"
        Me.cClientName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.cClientName.Width = 125
        '
        'butSearchTransaction
        '
        Me.butSearchTransaction.BackColor = System.Drawing.SystemColors.Control
        Me.butSearchTransaction.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butSearchTransaction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butSearchTransaction.Location = New System.Drawing.Point(467, 22)
        Me.butSearchTransaction.Name = "butSearchTransaction"
        Me.butSearchTransaction.Size = New System.Drawing.Size(98, 62)
        Me.butSearchTransaction.TabIndex = 9
        Me.butSearchTransaction.Text = "Find"
        Me.butSearchTransaction.UseVisualStyleBackColor = False
        '
        'transation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 320)
        Me.Controls.Add(Me.grpTransaction)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "transation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transactions History"
        Me.grpTransaction.ResumeLayout(False)
        Me.grpTransaction.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents fileDialogCar As System.Windows.Forms.OpenFileDialog
    Friend WithEvents grpTransaction As System.Windows.Forms.GroupBox
    Friend WithEvents lbIdCarForm As System.Windows.Forms.Label
    Friend WithEvents lbTransactionDate As System.Windows.Forms.Label
    Friend WithEvents lsvTransaction As System.Windows.Forms.ListView
    Friend WithEvents cidTransaction As System.Windows.Forms.ColumnHeader
    Friend WithEvents cTransactionDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents butSearchTransaction As System.Windows.Forms.Button
    Friend WithEvents cTransactionAmount As System.Windows.Forms.ColumnHeader
    Friend WithEvents cDiscount As System.Windows.Forms.ColumnHeader
    Friend WithEvents cTransactionCurrency As System.Windows.Forms.ColumnHeader
    Friend WithEvents cFormulaName As System.Windows.Forms.ColumnHeader
    Friend WithEvents dpTransactionDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cClientName As System.Windows.Forms.ColumnHeader
End Class
