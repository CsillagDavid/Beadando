﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WtForm
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
        Me.LblFelhasznalok = New System.Windows.Forms.Label()
        Me.LtbFelhasznalok = New System.Windows.Forms.ListBox()
        Me.DgvTabla = New System.Windows.Forms.DataGridView()
        Me.BtnFelhasznalok = New System.Windows.Forms.Button()
        Me.BtnMunkaidoleker = New System.Windows.Forms.Button()
        Me.BtnMentes = New System.Windows.Forms.Button()
        Me.BtnTorles = New System.Windows.Forms.Button()
        Me.BtnMunkaidoossz = New System.Windows.Forms.Button()
        Me.TxtMunkaidoOsszes = New System.Windows.Forms.TextBox()
        Me.LblMunkaidoOsszes = New System.Windows.Forms.Label()
        Me.CmbEv = New System.Windows.Forms.ComboBox()
        Me.CmbHonap = New System.Windows.Forms.ComboBox()
        Me.LblEv = New System.Windows.Forms.Label()
        Me.LvlHonap = New System.Windows.Forms.Label()
        Me.DgvUj = New System.Windows.Forms.DataGridView()
        Me.LblOra = New System.Windows.Forms.Label()
        Me.TstButton = New System.Windows.Forms.Button()
        Me.BtnUnnep = New System.Windows.Forms.Button()
        Me.LblUnnep = New System.Windows.Forms.Label()
        Me.LblJogkor = New System.Windows.Forms.Label()
        Me.BtnJogkor = New System.Windows.Forms.Button()
        CType(Me.DgvTabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvUj, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblFelhasznalok
        '
        Me.LblFelhasznalok.AutoSize = True
        Me.LblFelhasznalok.Location = New System.Drawing.Point(6, 159)
        Me.LblFelhasznalok.Name = "LblFelhasznalok"
        Me.LblFelhasznalok.Size = New System.Drawing.Size(72, 13)
        Me.LblFelhasznalok.TabIndex = 0
        Me.LblFelhasznalok.Text = "Felhasználók:"
        '
        'LtbFelhasznalok
        '
        Me.LtbFelhasznalok.FormattingEnabled = True
        Me.LtbFelhasznalok.Location = New System.Drawing.Point(9, 175)
        Me.LtbFelhasznalok.Name = "LtbFelhasznalok"
        Me.LtbFelhasznalok.Size = New System.Drawing.Size(153, 160)
        Me.LtbFelhasznalok.TabIndex = 1
        '
        'DgvTabla
        '
        Me.DgvTabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvTabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DgvTabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvTabla.Location = New System.Drawing.Point(171, 9)
        Me.DgvTabla.Name = "DgvTabla"
        Me.DgvTabla.Size = New System.Drawing.Size(780, 539)
        Me.DgvTabla.TabIndex = 4
        '
        'BtnFelhasznalok
        '
        Me.BtnFelhasznalok.Location = New System.Drawing.Point(9, 341)
        Me.BtnFelhasznalok.Name = "BtnFelhasznalok"
        Me.BtnFelhasznalok.Size = New System.Drawing.Size(153, 23)
        Me.BtnFelhasznalok.TabIndex = 5
        Me.BtnFelhasznalok.Text = "Felhasználók adatai"
        Me.BtnFelhasznalok.UseVisualStyleBackColor = True
        '
        'BtnMunkaidoleker
        '
        Me.BtnMunkaidoleker.Location = New System.Drawing.Point(9, 52)
        Me.BtnMunkaidoleker.Name = "BtnMunkaidoleker"
        Me.BtnMunkaidoleker.Size = New System.Drawing.Size(153, 46)
        Me.BtnMunkaidoleker.TabIndex = 6
        Me.BtnMunkaidoleker.Text = "Munkaidő lekérdezése"
        Me.BtnMunkaidoleker.UseVisualStyleBackColor = True
        '
        'BtnMentes
        '
        Me.BtnMentes.Enabled = False
        Me.BtnMentes.Location = New System.Drawing.Point(9, 525)
        Me.BtnMentes.Name = "BtnMentes"
        Me.BtnMentes.Size = New System.Drawing.Size(75, 23)
        Me.BtnMentes.TabIndex = 7
        Me.BtnMentes.Text = "Mentés"
        Me.BtnMentes.UseVisualStyleBackColor = True
        '
        'BtnTorles
        '
        Me.BtnTorles.Enabled = False
        Me.BtnTorles.Location = New System.Drawing.Point(90, 525)
        Me.BtnTorles.Name = "BtnTorles"
        Me.BtnTorles.Size = New System.Drawing.Size(75, 23)
        Me.BtnTorles.TabIndex = 8
        Me.BtnTorles.Text = "Törlés"
        Me.BtnTorles.UseVisualStyleBackColor = True
        '
        'BtnMunkaidoossz
        '
        Me.BtnMunkaidoossz.Location = New System.Drawing.Point(9, 104)
        Me.BtnMunkaidoossz.Name = "BtnMunkaidoossz"
        Me.BtnMunkaidoossz.Size = New System.Drawing.Size(153, 23)
        Me.BtnMunkaidoossz.TabIndex = 9
        Me.BtnMunkaidoossz.Text = "Munkaidő összesítése"
        Me.BtnMunkaidoossz.UseVisualStyleBackColor = True
        '
        'TxtMunkaidoOsszes
        '
        Me.TxtMunkaidoOsszes.Location = New System.Drawing.Point(887, 551)
        Me.TxtMunkaidoOsszes.Name = "TxtMunkaidoOsszes"
        Me.TxtMunkaidoOsszes.ReadOnly = True
        Me.TxtMunkaidoOsszes.Size = New System.Drawing.Size(36, 20)
        Me.TxtMunkaidoOsszes.TabIndex = 10
        '
        'LblMunkaidoOsszes
        '
        Me.LblMunkaidoOsszes.AutoSize = True
        Me.LblMunkaidoOsszes.Location = New System.Drawing.Point(788, 556)
        Me.LblMunkaidoOsszes.Name = "LblMunkaidoOsszes"
        Me.LblMunkaidoOsszes.Size = New System.Drawing.Size(93, 13)
        Me.LblMunkaidoOsszes.TabIndex = 11
        Me.LblMunkaidoOsszes.Text = "Összes munkaidő:"
        '
        'CmbEv
        '
        Me.CmbEv.FormattingEnabled = True
        Me.CmbEv.Location = New System.Drawing.Point(9, 25)
        Me.CmbEv.Name = "CmbEv"
        Me.CmbEv.Size = New System.Drawing.Size(75, 21)
        Me.CmbEv.TabIndex = 13
        '
        'CmbHonap
        '
        Me.CmbHonap.FormattingEnabled = True
        Me.CmbHonap.Location = New System.Drawing.Point(90, 25)
        Me.CmbHonap.Name = "CmbHonap"
        Me.CmbHonap.Size = New System.Drawing.Size(72, 21)
        Me.CmbHonap.TabIndex = 14
        '
        'LblEv
        '
        Me.LblEv.AutoSize = True
        Me.LblEv.Location = New System.Drawing.Point(6, 9)
        Me.LblEv.Name = "LblEv"
        Me.LblEv.Size = New System.Drawing.Size(23, 13)
        Me.LblEv.TabIndex = 15
        Me.LblEv.Text = "Év:"
        '
        'LvlHonap
        '
        Me.LvlHonap.AutoSize = True
        Me.LvlHonap.Location = New System.Drawing.Point(87, 9)
        Me.LvlHonap.Name = "LvlHonap"
        Me.LvlHonap.Size = New System.Drawing.Size(42, 13)
        Me.LvlHonap.TabIndex = 16
        Me.LvlHonap.Text = "Hónap:"
        '
        'DgvUj
        '
        Me.DgvUj.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvUj.Enabled = False
        Me.DgvUj.Location = New System.Drawing.Point(9, 561)
        Me.DgvUj.Name = "DgvUj"
        Me.DgvUj.Size = New System.Drawing.Size(10, 10)
        Me.DgvUj.TabIndex = 17
        Me.DgvUj.Visible = False
        '
        'LblOra
        '
        Me.LblOra.AutoSize = True
        Me.LblOra.Location = New System.Drawing.Point(929, 555)
        Me.LblOra.Name = "LblOra"
        Me.LblOra.Size = New System.Drawing.Size(22, 13)
        Me.LblOra.TabIndex = 18
        Me.LblOra.Text = "óra"
        '
        'TstButton
        '
        Me.TstButton.Location = New System.Drawing.Point(197, 554)
        Me.TstButton.Name = "TstButton"
        Me.TstButton.Size = New System.Drawing.Size(75, 23)
        Me.TstButton.TabIndex = 19
        Me.TstButton.Text = "teszt"
        Me.TstButton.UseVisualStyleBackColor = True
        '
        'BtnUnnep
        '
        Me.BtnUnnep.Enabled = False
        Me.BtnUnnep.Location = New System.Drawing.Point(9, 414)
        Me.BtnUnnep.Name = "BtnUnnep"
        Me.BtnUnnep.Size = New System.Drawing.Size(75, 23)
        Me.BtnUnnep.TabIndex = 20
        Me.BtnUnnep.Text = "Szerkesztés"
        Me.BtnUnnep.UseVisualStyleBackColor = True
        Me.BtnUnnep.Visible = False
        '
        'LblUnnep
        '
        Me.LblUnnep.AutoSize = True
        Me.LblUnnep.Enabled = False
        Me.LblUnnep.Location = New System.Drawing.Point(6, 398)
        Me.LblUnnep.Name = "LblUnnep"
        Me.LblUnnep.Size = New System.Drawing.Size(136, 13)
        Me.LblUnnep.TabIndex = 21
        Me.LblUnnep.Text = "Ünnepnapok szerkesztése:"
        Me.LblUnnep.Visible = False
        '
        'LblJogkor
        '
        Me.LblJogkor.AutoSize = True
        Me.LblJogkor.Enabled = False
        Me.LblJogkor.Location = New System.Drawing.Point(9, 444)
        Me.LblJogkor.Name = "LblJogkor"
        Me.LblJogkor.Size = New System.Drawing.Size(54, 13)
        Me.LblJogkor.TabIndex = 22
        Me.LblJogkor.Text = "Jogkörök:"
        Me.LblJogkor.Visible = False
        '
        'BtnJogkor
        '
        Me.BtnJogkor.Enabled = False
        Me.BtnJogkor.Location = New System.Drawing.Point(9, 460)
        Me.BtnJogkor.Name = "BtnJogkor"
        Me.BtnJogkor.Size = New System.Drawing.Size(75, 23)
        Me.BtnJogkor.TabIndex = 23
        Me.BtnJogkor.Text = "Szerkesztés"
        Me.BtnJogkor.UseVisualStyleBackColor = True
        Me.BtnJogkor.Visible = False
        '
        'WtForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(959, 578)
        Me.Controls.Add(Me.BtnJogkor)
        Me.Controls.Add(Me.LblJogkor)
        Me.Controls.Add(Me.LblUnnep)
        Me.Controls.Add(Me.BtnUnnep)
        Me.Controls.Add(Me.TstButton)
        Me.Controls.Add(Me.LblOra)
        Me.Controls.Add(Me.DgvTabla)
        Me.Controls.Add(Me.LvlHonap)
        Me.Controls.Add(Me.LblEv)
        Me.Controls.Add(Me.CmbHonap)
        Me.Controls.Add(Me.CmbEv)
        Me.Controls.Add(Me.LblMunkaidoOsszes)
        Me.Controls.Add(Me.TxtMunkaidoOsszes)
        Me.Controls.Add(Me.BtnMunkaidoossz)
        Me.Controls.Add(Me.BtnTorles)
        Me.Controls.Add(Me.BtnMentes)
        Me.Controls.Add(Me.BtnMunkaidoleker)
        Me.Controls.Add(Me.BtnFelhasznalok)
        Me.Controls.Add(Me.DgvUj)
        Me.Controls.Add(Me.LtbFelhasznalok)
        Me.Controls.Add(Me.LblFelhasznalok)
        Me.Name = "WtForm"
        Me.Text = "Munkaidő nyilvántartó"
        CType(Me.DgvTabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvUj, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblFelhasznalok As Label
    Friend WithEvents LtbFelhasznalok As ListBox
    Friend WithEvents DgvTabla As DataGridView
    Friend WithEvents BtnFelhasznalok As Button
    Friend WithEvents BtnMunkaidoleker As Button
    Friend WithEvents BtnMentes As Button
    Friend WithEvents BtnTorles As Button
    Friend WithEvents BtnMunkaidoossz As Button
    Friend WithEvents TxtMunkaidoOsszes As TextBox
    Friend WithEvents LblMunkaidoOsszes As Label
    Friend WithEvents CmbEv As ComboBox
    Friend WithEvents CmbHonap As ComboBox
    Friend WithEvents LblEv As Label
    Friend WithEvents LvlHonap As Label
    Friend WithEvents DgvUj As DataGridView
    Friend WithEvents LblOra As Label
    Friend WithEvents TstButton As Button
    Friend WithEvents BtnUnnep As Button
    Friend WithEvents LblUnnep As Label
    Friend WithEvents LblJogkor As Label
    Friend WithEvents BtnJogkor As Button
End Class
