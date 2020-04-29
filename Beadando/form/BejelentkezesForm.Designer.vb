<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726")>
Partial Class Login
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
    Friend WithEvents PcbBejelentkezes As System.Windows.Forms.PictureBox
    Friend WithEvents LblFelhasznalonev As System.Windows.Forms.Label
    Friend WithEvents LblJelszo As System.Windows.Forms.Label
    Friend WithEvents TxtFelhasznalonev As System.Windows.Forms.TextBox
    Friend WithEvents TxtJelszo As System.Windows.Forms.TextBox
    Friend WithEvents BtnBejelentkez As System.Windows.Forms.Button
    Friend WithEvents BtnKilep As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.PcbBejelentkezes = New System.Windows.Forms.PictureBox()
        Me.LblFelhasznalonev = New System.Windows.Forms.Label()
        Me.LblJelszo = New System.Windows.Forms.Label()
        Me.TxtFelhasznalonev = New System.Windows.Forms.TextBox()
        Me.TxtJelszo = New System.Windows.Forms.TextBox()
        Me.BtnBejelentkez = New System.Windows.Forms.Button()
        Me.BtnKilep = New System.Windows.Forms.Button()
        Me.BtnJelszocsere = New System.Windows.Forms.Button()
        CType(Me.PcbBejelentkezes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PcbBejelentkezes
        '
        Me.PcbBejelentkezes.Image = CType(resources.GetObject("PcbBejelentkezes.Image"), System.Drawing.Image)
        Me.PcbBejelentkezes.Location = New System.Drawing.Point(0, 0)
        Me.PcbBejelentkezes.Name = "PcbBejelentkezes"
        Me.PcbBejelentkezes.Size = New System.Drawing.Size(165, 193)
        Me.PcbBejelentkezes.TabIndex = 0
        Me.PcbBejelentkezes.TabStop = False
        '
        'LblFelhasznalonev
        '
        Me.LblFelhasznalonev.Location = New System.Drawing.Point(172, 24)
        Me.LblFelhasznalonev.Name = "LblFelhasznalonev"
        Me.LblFelhasznalonev.Size = New System.Drawing.Size(220, 23)
        Me.LblFelhasznalonev.TabIndex = 0
        Me.LblFelhasznalonev.Text = "Felhasználó név"
        Me.LblFelhasznalonev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblJelszo
        '
        Me.LblJelszo.Location = New System.Drawing.Point(172, 81)
        Me.LblJelszo.Name = "LblJelszo"
        Me.LblJelszo.Size = New System.Drawing.Size(220, 23)
        Me.LblJelszo.TabIndex = 2
        Me.LblJelszo.Text = "Jelszó"
        Me.LblJelszo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtFelhasznalonev
        '
        Me.TxtFelhasznalonev.Location = New System.Drawing.Point(174, 44)
        Me.TxtFelhasznalonev.Name = "TxtFelhasznalonev"
        Me.TxtFelhasznalonev.Size = New System.Drawing.Size(220, 20)
        Me.TxtFelhasznalonev.TabIndex = 1
        '
        'TxtJelszo
        '
        Me.TxtJelszo.Location = New System.Drawing.Point(174, 101)
        Me.TxtJelszo.Name = "TxtJelszo"
        Me.TxtJelszo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtJelszo.Size = New System.Drawing.Size(220, 20)
        Me.TxtJelszo.TabIndex = 3
        '
        'BtnBejelentkez
        '
        Me.BtnBejelentkez.Location = New System.Drawing.Point(175, 136)
        Me.BtnBejelentkez.Name = "BtnBejelentkez"
        Me.BtnBejelentkez.Size = New System.Drawing.Size(94, 23)
        Me.BtnBejelentkez.TabIndex = 4
        Me.BtnBejelentkez.Text = "&Bejelentkezés"
        '
        'BtnKilep
        '
        Me.BtnKilep.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnKilep.Location = New System.Drawing.Point(300, 136)
        Me.BtnKilep.Name = "BtnKilep"
        Me.BtnKilep.Size = New System.Drawing.Size(94, 23)
        Me.BtnKilep.TabIndex = 5
        Me.BtnKilep.Text = "&Kilépés"
        '
        'BtnJelszocsere
        '
        Me.BtnJelszocsere.Location = New System.Drawing.Point(175, 165)
        Me.BtnJelszocsere.Name = "BtnJelszocsere"
        Me.BtnJelszocsere.Size = New System.Drawing.Size(217, 23)
        Me.BtnJelszocsere.TabIndex = 6
        Me.BtnJelszocsere.Text = "Jelszó megváltoztatása"
        Me.BtnJelszocsere.UseVisualStyleBackColor = True
        '
        'Login
        '
        Me.AcceptButton = Me.BtnBejelentkez
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnKilep
        Me.ClientSize = New System.Drawing.Size(401, 192)
        Me.Controls.Add(Me.BtnJelszocsere)
        Me.Controls.Add(Me.BtnKilep)
        Me.Controls.Add(Me.BtnBejelentkez)
        Me.Controls.Add(Me.TxtJelszo)
        Me.Controls.Add(Me.TxtFelhasznalonev)
        Me.Controls.Add(Me.LblJelszo)
        Me.Controls.Add(Me.LblFelhasznalonev)
        Me.Controls.Add(Me.PcbBejelentkezes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Login"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Bejelentkezés"
        CType(Me.PcbBejelentkezes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnJelszocsere As Button
End Class
