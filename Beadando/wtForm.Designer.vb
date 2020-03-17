<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wtForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblFelhasznalok = New System.Windows.Forms.Label()
        Me.ltbFelhasznalok = New System.Windows.Forms.ListBox()
        Me.dgvFelhasznalok = New System.Windows.Forms.DataGridView()
        Me.btnFelhasznalok = New System.Windows.Forms.Button()
        Me.btnMunkaidő = New System.Windows.Forms.Button()
        CType(Me.dgvFelhasznalok, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFelhasznalok
        '
        Me.lblFelhasznalok.AutoSize = True
        Me.lblFelhasznalok.Location = New System.Drawing.Point(12, 9)
        Me.lblFelhasznalok.Name = "lblFelhasznalok"
        Me.lblFelhasznalok.Size = New System.Drawing.Size(72, 13)
        Me.lblFelhasznalok.TabIndex = 0
        Me.lblFelhasznalok.Text = "Felhasználók:"
        '
        'ltbFelhasznalok
        '
        Me.ltbFelhasznalok.FormattingEnabled = True
        Me.ltbFelhasznalok.Location = New System.Drawing.Point(15, 35)
        Me.ltbFelhasznalok.Name = "ltbFelhasznalok"
        Me.ltbFelhasznalok.Size = New System.Drawing.Size(156, 160)
        Me.ltbFelhasznalok.TabIndex = 1
        '
        'dgvFelhasznalok
        '
        Me.dgvFelhasznalok.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvFelhasznalok.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvFelhasznalok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFelhasznalok.Location = New System.Drawing.Point(186, 9)
        Me.dgvFelhasznalok.Name = "dgvFelhasznalok"
        Me.dgvFelhasznalok.Size = New System.Drawing.Size(765, 517)
        Me.dgvFelhasznalok.TabIndex = 4
        '
        'btnFelhasznalok
        '
        Me.btnFelhasznalok.Location = New System.Drawing.Point(15, 286)
        Me.btnFelhasznalok.Name = "btnFelhasznalok"
        Me.btnFelhasznalok.Size = New System.Drawing.Size(85, 23)
        Me.btnFelhasznalok.TabIndex = 5
        Me.btnFelhasznalok.Text = "Felhasználók"
        Me.btnFelhasznalok.UseVisualStyleBackColor = True
        '
        'btnMunkaidő
        '
        Me.btnMunkaidő.Location = New System.Drawing.Point(15, 211)
        Me.btnMunkaidő.Name = "btnMunkaidő"
        Me.btnMunkaidő.Size = New System.Drawing.Size(75, 46)
        Me.btnMunkaidő.TabIndex = 6
        Me.btnMunkaidő.Text = "Munkaidő lekérdezése"
        Me.btnMunkaidő.UseVisualStyleBackColor = True
        '
        'wtForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(954, 538)
        Me.Controls.Add(Me.btnMunkaidő)
        Me.Controls.Add(Me.btnFelhasznalok)
        Me.Controls.Add(Me.dgvFelhasznalok)
        Me.Controls.Add(Me.ltbFelhasznalok)
        Me.Controls.Add(Me.lblFelhasznalok)
        Me.Name = "wtForm"
        Me.Text = "Munkaidő nyilvántartó"
        CType(Me.dgvFelhasznalok, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblFelhasznalok As Label
    Friend WithEvents ltbFelhasznalok As ListBox
    Friend WithEvents dgvFelhasznalok As DataGridView
    Friend WithEvents btnFelhasznalok As Button
    Friend WithEvents btnMunkaidő As Button
End Class
