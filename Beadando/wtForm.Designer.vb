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
        Me.cmbEv = New System.Windows.Forms.ComboBox()
        Me.cmbHonap = New System.Windows.Forms.ComboBox()
        Me.lblEv = New System.Windows.Forms.Label()
        Me.lvlHonap = New System.Windows.Forms.Label()
        Me.dgvUj = New System.Windows.Forms.DataGridView()
        Me.lblOra = New System.Windows.Forms.Label()
        Me.tstButton = New System.Windows.Forms.Button()
        CType(Me.dgvTabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvUj, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFelhasznalok
        '
        Me.lblFelhasznalok.AutoSize = True
        Me.lblFelhasznalok.Location = New System.Drawing.Point(12, 152)
        Me.lblFelhasznalok.Name = "lblFelhasznalok"
        Me.lblFelhasznalok.Size = New System.Drawing.Size(72, 13)
        Me.lblFelhasznalok.TabIndex = 0
        Me.lblFelhasznalok.Text = "Felhasználók:"
        '
        'ltbFelhasznalok
        '
        Me.ltbFelhasznalok.FormattingEnabled = True
        Me.ltbFelhasznalok.Location = New System.Drawing.Point(15, 168)
        Me.ltbFelhasznalok.Name = "ltbFelhasznalok"
        Me.ltbFelhasznalok.Size = New System.Drawing.Size(153, 160)
        Me.ltbFelhasznalok.TabIndex = 1
        '
        'dgvTabla
        '
        Me.dgvTabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvTabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTabla.Location = New System.Drawing.Point(186, 9)
        Me.dgvTabla.Name = "dgvTabla"
        Me.dgvTabla.Size = New System.Drawing.Size(765, 539)
        Me.dgvTabla.TabIndex = 4
        '
        'btnFelhasznalok
        '
        Me.btnFelhasznalok.Location = New System.Drawing.Point(15, 334)
        Me.btnFelhasznalok.Name = "btnFelhasznalok"
        Me.btnFelhasznalok.Size = New System.Drawing.Size(153, 23)
        Me.btnFelhasznalok.TabIndex = 5
        Me.btnFelhasznalok.Text = "Felhasználók adatai"
        Me.btnFelhasznalok.UseVisualStyleBackColor = True
        '
        'btnMunkaidoleker
        '
        Me.btnMunkaidoleker.Location = New System.Drawing.Point(15, 90)
        Me.btnMunkaidoleker.Name = "btnMunkaidoleker"
        Me.btnMunkaidoleker.Size = New System.Drawing.Size(153, 46)
        Me.btnMunkaidoleker.TabIndex = 6
        Me.btnMunkaidoleker.Text = "Munkaidő lekérdezése"
        Me.btnMunkaidoleker.UseVisualStyleBackColor = True
        '
        'btnMentes
        '
        Me.btnMentes.Location = New System.Drawing.Point(9, 554)
        Me.btnMentes.Name = "btnMentes"
        Me.btnMentes.Size = New System.Drawing.Size(75, 23)
        Me.btnMentes.TabIndex = 7
        Me.btnMentes.Text = "Mentés"
        Me.btnMentes.UseVisualStyleBackColor = True
        '
        'btnTorles
        '
        Me.btnTorles.Location = New System.Drawing.Point(90, 554)
        Me.btnTorles.Name = "btnTorles"
        Me.btnTorles.Size = New System.Drawing.Size(75, 23)
        Me.btnTorles.TabIndex = 8
        Me.btnTorles.Text = "Törlés"
        Me.btnTorles.UseVisualStyleBackColor = True
        '
        'btnMunkaidoossz
        '
        Me.btnMunkaidoossz.Location = New System.Drawing.Point(186, 554)
        Me.btnMunkaidoossz.Name = "btnMunkaidoossz"
        Me.btnMunkaidoossz.Size = New System.Drawing.Size(153, 23)
        Me.btnMunkaidoossz.TabIndex = 9
        Me.btnMunkaidoossz.Text = "Munkaidő összesítése"
        Me.btnMunkaidoossz.UseVisualStyleBackColor = True
        '
        'txtMunkaidoOsszes
        '
        Me.txtMunkaidoOsszes.Location = New System.Drawing.Point(878, 556)
        Me.txtMunkaidoOsszes.Name = "txtMunkaidoOsszes"
        Me.txtMunkaidoOsszes.ReadOnly = True
        Me.txtMunkaidoOsszes.Size = New System.Drawing.Size(36, 20)
        Me.txtMunkaidoOsszes.TabIndex = 10
        '
        'lblMunkaidoOsszes
        '
        Me.lblMunkaidoOsszes.AutoSize = True
        Me.lblMunkaidoOsszes.Location = New System.Drawing.Point(779, 559)
        Me.lblMunkaidoOsszes.Name = "lblMunkaidoOsszes"
        Me.lblMunkaidoOsszes.Size = New System.Drawing.Size(93, 13)
        Me.lblMunkaidoOsszes.TabIndex = 11
        Me.lblMunkaidoOsszes.Text = "Összes munkaidő:"
        '
        'cmbEv
        '
        Me.cmbEv.FormattingEnabled = True
        Me.cmbEv.Location = New System.Drawing.Point(15, 63)
        Me.cmbEv.Name = "cmbEv"
        Me.cmbEv.Size = New System.Drawing.Size(75, 21)
        Me.cmbEv.TabIndex = 13
        '
        'cmbHonap
        '
        Me.cmbHonap.FormattingEnabled = True
        Me.cmbHonap.Location = New System.Drawing.Point(96, 63)
        Me.cmbHonap.Name = "cmbHonap"
        Me.cmbHonap.Size = New System.Drawing.Size(72, 21)
        Me.cmbHonap.TabIndex = 14
        '
        'lblEv
        '
        Me.lblEv.AutoSize = True
        Me.lblEv.Location = New System.Drawing.Point(12, 47)
        Me.lblEv.Name = "lblEv"
        Me.lblEv.Size = New System.Drawing.Size(23, 13)
        Me.lblEv.TabIndex = 15
        Me.lblEv.Text = "Év:"
        '
        'lvlHonap
        '
        Me.lvlHonap.AutoSize = True
        Me.lvlHonap.Location = New System.Drawing.Point(93, 47)
        Me.lvlHonap.Name = "lvlHonap"
        Me.lvlHonap.Size = New System.Drawing.Size(42, 13)
        Me.lvlHonap.TabIndex = 16
        Me.lvlHonap.Text = "Hónap:"
        '
        'dgvUj
        '
        Me.dgvUj.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUj.Enabled = False
        Me.dgvUj.Location = New System.Drawing.Point(941, 567)
        Me.dgvUj.Name = "dgvUj"
        Me.dgvUj.Size = New System.Drawing.Size(10, 10)
        Me.dgvUj.TabIndex = 17
        Me.dgvUj.Visible = False
        '
        'lblOra
        '
        Me.lblOra.AutoSize = True
        Me.lblOra.Location = New System.Drawing.Point(920, 559)
        Me.lblOra.Name = "lblOra"
        Me.lblOra.Size = New System.Drawing.Size(22, 13)
        Me.lblOra.TabIndex = 18
        Me.lblOra.Text = "óra"
        '
        'tstButton
        '
        Me.tstButton.Location = New System.Drawing.Point(15, 12)
        Me.tstButton.Name = "tstButton"
        Me.tstButton.Size = New System.Drawing.Size(75, 23)
        Me.tstButton.TabIndex = 19
        Me.tstButton.Text = "teszt"
        Me.tstButton.UseVisualStyleBackColor = True
        '
        'wtForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(954, 585)
        Me.Controls.Add(Me.tstButton)
        Me.Controls.Add(Me.lblOra)
        Me.Controls.Add(Me.dgvUj)
        Me.Controls.Add(Me.lvlHonap)
        Me.Controls.Add(Me.lblEv)
        Me.Controls.Add(Me.cmbHonap)
        Me.Controls.Add(Me.cmbEv)
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
        CType(Me.dgvUj, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents cmbEv As ComboBox
    Friend WithEvents cmbHonap As ComboBox
    Friend WithEvents lblEv As Label
    Friend WithEvents lvlHonap As Label
    Friend WithEvents dgvUj As DataGridView
    Friend WithEvents lblOra As Label
    Friend WithEvents tstButton As Button
End Class
