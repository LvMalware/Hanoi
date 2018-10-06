Imports System.Windows.Forms

Public Class PinDis

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        If NumericUpDown1.Value > 4 Then
            NumericUpDown2.Maximum = 10 'NumericUpDown1.Value
        ElseIf NumericUpDown1.Value = 3 Then
            NumericUpDown2.Maximum = 10
        ElseIf NumericUpDown1.Value = 4 Then
            NumericUpDown2.Maximum = 10
        End If
    End Sub
End Class
