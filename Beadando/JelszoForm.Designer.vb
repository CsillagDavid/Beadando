<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JelszoForm
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
        Me.TxtJelenlegi = New System.Windows.Forms.TextBox()
        Me.TxtUj = New System.Windows.Forms.TextBox()
        Me.TxtMegerosites = New System.Windows.Forms.TextBox()
        Me.LblJelenlegi = New System.Windows.Forms.Label()
        Me.LblUj = New System.Windows.Forms.Label()
        Me.LblMegerosites = New System.Windows.Forms.Label()
        Me.BtnCsere = New System.Windows.Forms.Button()
        Me.BtnVissza = New System.Windows.Forms.Button()
        Me.LblEmail = New System.Windows.Forms.Label()
        Me.TxtEmail = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TxtJelenlegi
        '
        Me.TxtJelenlegi.Location = New System.Drawing.Point(142, 45)
        Me.TxtJelenlegi.Name = "TxtJelenlegi"
        Me.TxtJelenlegi.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtJelenlegi.Size = New System.Drawing.Size(178, 20)
        Me.TxtJelenlegi.TabIndex = 0
        '
        'TxtUj
        '
        Me.TxtUj.Location = New System.Drawing.Point(142, 71)
        Me.TxtUj.Name = "TxtUj"
        Me.TxtUj.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtUj.Size = New System.Drawing.Size(178, 20)
        Me.TxtUj.TabIndex = 1
        '
        'TxtMegerosites
        '
        Me.TxtMegerosites.Location = New System.Drawing.Point(142, 97)
        Me.TxtMegerosites.Name = "TxtMegerosites"
        Me.TxtMegerosites.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtMegerosites.Size = New System.Drawing.Size(178, 20)
        Me.TxtMegerosites.TabIndex = 2
        '
        'LblJelenlegi
        '
        Me.LblJelenlegi.AutoSize = True
        Me.LblJelenlegi.Location = New System.Drawing.Point(12, 48)
        Me.LblJelenlegi.Name = "LblJelenlegi"
        Me.LblJelenlegi.Size = New System.Drawing.Size(80, 13)
        Me.LblJelenlegi.TabIndex = 3
        Me.LblJelenlegi.Text = "Jelenlegi jelszó:"
        '
        'LblUj
        '
        Me.LblUj.AutoSize = True
        Me.LblUj.Location = New System.Drawing.Point(12, 74)
        Me.LblUj.Name = "LblUj"
        Me.LblUj.Size = New System.Drawing.Size(49, 13)
        Me.LblUj.TabIndex = 4
        Me.LblUj.Text = "Új jelszó:"
        '
        'LblMegerosites
        '
        Me.LblMegerosites.AutoSize = True
        Me.LblMegerosites.Location = New System.Drawing.Point(12, 100)
        Me.LblMegerosites.Name = "LblMegerosites"
        Me.LblMegerosites.Size = New System.Drawing.Size(106, 13)
        Me.LblMegerosites.TabIndex = 5
        Me.LblMegerosites.Text = "Jelszó megerősítése:"
        '
        'BtnCsere
        '
        Me.BtnCsere.Location = New System.Drawing.Point(181, 123)
        Me.BtnCsere.Name = "BtnCsere"
        Me.BtnCsere.Size = New System.Drawing.Size(139, 23)
        Me.BtnCsere.TabIndex = 6
        Me.BtnCsere.Text = "Jelszó megváltoztatása"
        Me.BtnCsere.UseVisualStyleBackColor = True
        '
        'BtnVissza
        '
        Me.BtnVissza.Location = New System.Drawing.Point(15, 123)
        Me.BtnVissza.Name = "BtnVissza"
        Me.BtnVissza.Size = New System.Drawing.Size(75, 23)
        Me.BtnVissza.TabIndex = 7
        Me.BtnVissza.Text = "Vissza"
        Me.BtnVissza.UseVisualStyleBackColor = True
        '
        'LblEmail
        '
        Me.LblEmail.AutoSize = True
        Me.LblEmail.Location = New System.Drawing.Point(12, 12)
        Me.LblEmail.Name = "LblEmail"
        Me.LblEmail.Size = New System.Drawing.Size(59, 13)
        Me.LblEmail.TabIndex = 8
        Me.LblEmail.Text = "E-mail cím:"
        '
        'TxtEmail
        '
        Me.TxtEmail.Location = New System.Drawing.Point(142, 9)
        Me.TxtEmail.Name = "TxtEmail"
        Me.TxtEmail.Size = New System.Drawing.Size(178, 20)
        Me.TxtEmail.TabIndex = 9
        '
        'JelszoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(330, 153)
        Me.Controls.Add(Me.TxtEmail)
        Me.Controls.Add(Me.LblEmail)
        Me.Controls.Add(Me.BtnVissza)
        Me.Controls.Add(Me.BtnCsere)
        Me.Controls.Add(Me.LblMegerosites)
        Me.Controls.Add(Me.LblUj)
        Me.Controls.Add(Me.LblJelenlegi)
        Me.Controls.Add(Me.TxtMegerosites)
        Me.Controls.Add(Me.TxtUj)
        Me.Controls.Add(Me.TxtJelenlegi)
        Me.Name = "JelszoForm"
        Me.Text = "Jelszo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtJelenlegi As TextBox
    Friend WithEvents TxtUj As TextBox
    Friend WithEvents TxtMegerosites As TextBox
    Friend WithEvents LblJelenlegi As Label
    Friend WithEvents LblUj As Label
    Friend WithEvents LblMegerosites As Label
    Friend WithEvents BtnCsere As Button
    Friend WithEvents BtnVissza As Button
    Friend WithEvents LblEmail As Label
    Friend WithEvents TxtEmail As TextBox
End Class
