<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.LblInput = New System.Windows.Forms.Label()
        Me.LblOutput = New System.Windows.Forms.Label()
        Me.TxtInput = New System.Windows.Forms.TextBox()
        Me.TxtOutput = New System.Windows.Forms.TextBox()
        Me.BtnDec = New System.Windows.Forms.Button()
        Me.BtnEnc = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LblInput
        '
        Me.LblInput.AutoSize = True
        Me.LblInput.Location = New System.Drawing.Point(12, 66)
        Me.LblInput.Name = "LblInput"
        Me.LblInput.Size = New System.Drawing.Size(31, 13)
        Me.LblInput.TabIndex = 0
        Me.LblInput.Text = "Input"
        '
        'LblOutput
        '
        Me.LblOutput.AutoSize = True
        Me.LblOutput.Location = New System.Drawing.Point(12, 96)
        Me.LblOutput.Name = "LblOutput"
        Me.LblOutput.Size = New System.Drawing.Size(39, 13)
        Me.LblOutput.TabIndex = 1
        Me.LblOutput.Text = "Output"
        '
        'TxtInput
        '
        Me.TxtInput.Location = New System.Drawing.Point(66, 66)
        Me.TxtInput.Name = "TxtInput"
        Me.TxtInput.Size = New System.Drawing.Size(294, 20)
        Me.TxtInput.TabIndex = 2
        '
        'TxtOutput
        '
        Me.TxtOutput.Location = New System.Drawing.Point(66, 96)
        Me.TxtOutput.Name = "TxtOutput"
        Me.TxtOutput.Size = New System.Drawing.Size(294, 20)
        Me.TxtOutput.TabIndex = 3
        '
        'BtnDec
        '
        Me.BtnDec.Location = New System.Drawing.Point(147, 144)
        Me.BtnDec.Name = "BtnDec"
        Me.BtnDec.Size = New System.Drawing.Size(75, 23)
        Me.BtnDec.TabIndex = 4
        Me.BtnDec.Text = "Decrypt"
        Me.BtnDec.UseVisualStyleBackColor = True
        '
        'BtnEnc
        '
        Me.BtnEnc.Location = New System.Drawing.Point(66, 144)
        Me.BtnEnc.Name = "BtnEnc"
        Me.BtnEnc.Size = New System.Drawing.Size(75, 23)
        Me.BtnEnc.TabIndex = 5
        Me.BtnEnc.Text = "Encrypt"
        Me.BtnEnc.UseVisualStyleBackColor = True
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(228, 144)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(75, 23)
        Me.BtnExit.TabIndex = 6
        Me.BtnExit.Text = "Exit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(61, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(262, 31)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Tool Encrypt Dan Decrypt"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(424, 201)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnEnc)
        Me.Controls.Add(Me.BtnDec)
        Me.Controls.Add(Me.TxtOutput)
        Me.Controls.Add(Me.TxtInput)
        Me.Controls.Add(Me.LblOutput)
        Me.Controls.Add(Me.LblInput)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblInput As Label
    Friend WithEvents LblOutput As Label
    Friend WithEvents TxtInput As TextBox
    Friend WithEvents TxtOutput As TextBox
    Friend WithEvents BtnDec As Button
    Friend WithEvents BtnEnc As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Label3 As Label
End Class
