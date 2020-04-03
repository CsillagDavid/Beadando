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
        Me.dgvTabla = New System.Windows.Forms.DataGridView()
        Me.btnFelhasznalok = New System.Windows.Forms.Button()
        Me.btnMunkaidoleker = New System.Windows.Forms.Button()
        Me.btnMentes = New System.Windows.Forms.Button()
        Me.btnTorles = New System.Windows.Forms.Button()
        Me.btnMunkaidoossz = New System.Windows.Forms.Button()
        Me.txtMunkaidoOsszes = New System.Windows.Forms.TextBox()
        Me.lblMunkaidoOsszes = New System.Windows.Forms.Label()
        CType(Me.dgvTabla, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFelhasznalok
        '
        Me.lblFelhasznalok.AutoSize = True
        Me.lblFelhasznalok.Location = New System.Drawing.Point(12, 67)
        Me.lblFelhasznalok.Name = "lblFelhasznalok"
        Me.lblFelhasznalok.Size = New System.Drawing.Size(72, 13)
        Me.lblFelhasznalok.TabIndex = 0
        Me.lblFelhasznalok.Text = "Felhasználók:"
        '
        'ltbFelhasznalok
        '
        Me.ltbFelhasznalok.FormattingEnabled = True
        Me.ltbFelhasznalok.Location = New System.Drawing.Point(15, 83)
        Me.ltbFelhasznalok.Name = "ltbFelhasznalok"
        Me.ltbFelhasznalok.Size = New System.Drawing.Size(156, 160)
        Me.ltbFelhasznalok.TabIndex = 1
        '
        'dgvTabla
        '
        Me.dgvTabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvTabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTabla.Location = New System.Drawing.Point(186, 9)
        Me.dgvTabla.Name = "dgvTabla"
        Me.dgvTabla.Size = New System.Drawing.Size(765, 526)
        Me.dgvTabla.TabIndex = 4
        '
        'btnFelhasznalok
        '
        Me.btnFelhasznalok.Location = New System.Drawing.Point(15, 301)
        Me.btnFelhasznalok.Name = "btnFelhasznalok"
        Me.btnFelhasznalok.Size = New System.Drawing.Size(156, 23)
        Me.btnFelhasznalok.TabIndex = 5
        Me.btnFelhasznalok.Text = "Felhasználók adatai"
        Me.btnFelhasznalok.UseVisualStyleBackColor = True
        '
        'btnMunkaidoleker
        '
        Me.btnMunkaidoleker.Location = New System.Drawing.Point(15, 249)
        Me.btnMunkaidoleker.Name = "btnMunkaidoleker"
        Me.btnMunkaidoleker.Size = New System.Drawing.Size(156, 46)
        Me.btnMunkaidoleker.TabIndex = 6
        Me.btnMunkaidoleker.Text = "Munkaidő lekérdezése"
        Me.btnMunkaidoleker.UseVisualStyleBackColor = True
        '
        'btnMentes
        '
        Me.btnMentes.Location = New System.Drawing.Point(12, 12)
        Me.btnMentes.Name = "btnMentes"
        Me.btnMentes.Size = New System.Drawing.Size(75, 23)
        Me.btnMentes.TabIndex = 7
        Me.btnMentes.Text = "Mentés"
        Me.btnMentes.UseVisualStyleBackColor = True
        '
        'btnTorles
        '
        Me.btnTorles.Location = New System.Drawing.Point(93, 12)
        Me.btnTorles.Name = "btnTorles"
        Me.btnTorles.Size = New System.Drawing.Size(75, 23)
        Me.btnTorles.TabIndex = 8
        Me.btnTorles.Text = "Törlés"
        Me.btnTorles.UseVisualStyleBackColor = True
        '
        'btnMunkaidoossz
        '
        Me.btnMunkaidoossz.Location = New System.Drawing.Point(12, 41)
        Me.btnMunkaidoossz.Name = "btnMunkaidoossz"
        Me.btnMunkaidoossz.Size = New System.Drawing.Size(156, 23)
        Me.btnMunkaidoossz.TabIndex = 9
        Me.btnMunkaidoossz.Text = "Munkaidő összesítése"
        Me.btnMunkaidoossz.UseVisualStyleBackColor = True
        '
        'txtMunkaidoOsszes
        '
        Me.txtMunkaidoOsszes.Location = New System.Drawing.Point(270, 541)
        Me.txtMunkaidoOsszes.Name = "txtMunkaidoOsszes"
        Me.txtMunkaidoOsszes.ReadOnly = True
        Me.txtMunkaidoOsszes.Size = New System.Drawing.Size(166, 20)
        Me.txtMunkaidoOsszes.TabIndex = 10
        '
        'lblMunkaidoOsszes
        '
        Me.lblMunkaidoOsszes.AutoSize = True
        Me.lblMunkaidoOsszes.Location = New System.Drawing.Point(183, 544)
        Me.lblMunkaidoOsszes.Name = "lblMunkaidoOsszes"
        Me.lblMunkaidoOsszes.Size = New System.Drawing.Size(81, 13)
        Me.lblMunkaidoOsszes.TabIndex = 11
        Me.lblMunkaidoOsszes.Text = "Havi munkaidő:"
        '
        'wtForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(954, 585)
        Me.Controls.Add(Me.lblMunkaidoOsszes)
        Me.Controls.Add(Me.txtMunkaidoOsszes)
        Me.Controls.Add(Me.btnMunkaidoossz)
        Me.Controls.Add(Me.btnTorles)
        Me.Controls.Add(Me.btnMentes)
        Me.Controls.Add(Me.btnMunkaidoleker)
        Me.Controls.Add(Me.btnFelhasznalok)
        Me.Controls.Add(Me.dgvTabla)
        Me.Controls.Add(Me.ltbFelhasznalok)
        Me.Controls.Add(Me.lblFelhasznalok)
        Me.Name = "wtForm"
        Me.Text = "Munkaidő nyilvántartó"
        CType(Me.dgvTabla, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblFelhasznalok As Label
    Friend WithEvents ltbFelhasznalok As ListBox
    Friend WithEvents dgvTabla As DataGridView
    Friend WithEvents btnFelhasznalok As Button
    Friend WithEvents btnMunkaidoleker As Button
    Friend WithEvents btnMentes As Button
    Friend WithEvents btnTorles As Button
    Friend WithEvents btnMunkaidoossz As Button
    Friend WithEvents txtMunkaidoOsszes As TextBox
    Friend WithEvents lblMunkaidoOsszes As Label
End Class
