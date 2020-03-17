Public Class Form1
    Dim Discos As New Dictionary(Of Integer, Disco)
    Dim Pinos As New Dictionary(Of Integer, Pino)
    Dim Movimentos As Integer = 0
    Dim Wait As Double = 0.5
    Dim resolver As Boolean = True
    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        resolver = False
        Application.ExitThread()
        Application.Exit()

    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        NovoJogo(3, 3)
    End Sub

    Sub NovoJogo(ByVal nDiscos As Integer, ByVal nPilares As Integer)
        Discos.Clear()
        Pinos.Clear()
        Dim Distancia As Integer = Me.Width / nPilares
        Dim AlturaPin As Integer = Me.Height / nDiscos
        Dim DiscH As Integer = 50
        Dim DiscW As Integer = Distancia
        Dim MDisc = DiscW
        Dim PinoW As Integer = 50
        Dim PinoH As Integer = AlturaPin

        For i = nDiscos To 1 Step -1
            Dim D As New Disco(Me, i, New Size(DiscW, DiscH), Nothing)
            Discos.Add(i, D)
            Me.Controls.Add(D.Disco)
            DiscW *= 0.85
            PinoH += DiscH
        Next
        If DiscW < 50 Then PinoW = DiscW
        Dim PinoX As Integer = MDisc / 2 - (PinoW - 10)

        For i = 1 To nPilares
            Dim P As New Pino(Me, i, New Point(PinoX, Me.Height - (Panel1.Height + PinoH)), New Size(PinoW, PinoH))
            Pinos.Add(i, P)
            Me.Controls.Add(P.Pilar)
            PinoX += MDisc '+ (DiscW - 40) / 2
        Next
        For i = nDiscos To 1 Step -1
            Discos(i).MovePara(Pinos(1))
        Next
    End Sub

    Sub Delay(ByVal dblSecs As Double)
        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
        Do Until Now > dblWaitTil
            Application.DoEvents()
        Loop
    End Sub

    Sub Hanoi3(ByVal Disco As Integer, ByVal De As Integer, ByVal Por As Integer, ByVal Para As Integer)
        If Disco = 0 Or resolver = False Then Exit Sub
        Hanoi3(Disco - 1, De, Para, Por)
        If resolver = False Then Exit Sub
        Discos(Disco).MovePara(Pinos(Para))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        Hanoi3(Disco - 1, Por, De, Para)
    End Sub

    Sub Hanoi4(ByVal Disco As Integer, ByVal de As Integer, ByVal por1 As Integer, ByVal por2 As Integer, ByVal para As Integer)
        If resolver = False Then Exit Sub
        If Disco = 1 Then
            If resolver = False Then Exit Sub
            Pinos(de).Discos.Peek.MovePara(Pinos(para))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
        ElseIf Disco = 2 Then
            If resolver = False Then Exit Sub
            Pinos(de).Discos.Peek.MovePara(Pinos(por1))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
            If resolver = False Then Exit Sub
            Pinos(de).Discos.Peek.MovePara(Pinos(para))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
            If resolver = False Then Exit Sub
            Pinos(por1).Discos.Peek.MovePara(Pinos(para))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
        Else
            If resolver = False Then Exit Sub
            Hanoi4(Disco - 2, de, por2, para, por1)
            'cout << source << " --> " << intermed2 << endl;
            If resolver = False Then Exit Sub
            Pinos(de).Discos.Peek.MovePara(Pinos(por2))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
            'cout << source << " --> " << dest << endl;
            If resolver = False Then Exit Sub
            Pinos(de).Discos.Peek.MovePara(Pinos(para))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            'cout << intermed2 << " --> " << dest << endl;
            Delay(Wait)
            If resolver = False Then Exit Sub
            Pinos(por2).Discos.Peek.MovePara(Pinos(para))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
            If resolver = False Then Exit Sub
            Hanoi4(Disco - 2, por1, de, por2, para)
        End If
    End Sub

    Sub Hanoi5(ByVal n As Integer, ByVal de As Integer, ByVal para As Integer, ByVal por1 As Integer, ByVal por2 As Integer, ByVal por3 As Integer)
        If (n = 0) Or resolver = False Then Exit Sub
        If (n = 1) Then
            If resolver = False Then Exit Sub
            Discos(n).MovePara(Pinos(para))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
            Exit Sub
        End If

        Hanoi5(n - 2, de, por1, por2, por3, para)
        If resolver = False Then Exit Sub
        Discos(n - 1).MovePara(Pinos(por3))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        If resolver = False Then Exit Sub
        Discos(n).MovePara(Pinos(para))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        If resolver = False Then Exit Sub
        Discos(n - 1).MovePara(Pinos(para))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        If resolver = False Then Exit Sub
        Hanoi5(n - 2, por1, para, de, por2, por3)

    End Sub

    Sub HanoiN(ByVal n As Integer, ByVal de As Integer, ByVal para As Integer, ByVal aux As List(Of Integer))
        If (n = 0) Or resolver = False Then Exit Sub

        If (n = 1) Then
            If resolver = False Then Exit Sub
            Discos(n).MovePara(Pinos(para))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
            Exit Sub
        End If
        If resolver = False Then Exit Sub
        Dim aux2 As List(Of Integer) = aux.ToList
        aux2.RemoveAt(0)
        HanoiN(n - 2, de, aux(0), aux2)

        Discos(n - 1).MovePara(Pinos(aux.Last))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        If resolver = False Then Exit Sub

        Discos(n).MovePara(Pinos(para))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        If resolver = False Then Exit Sub

        Discos(n - 1).MovePara(Pinos(para))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        If resolver = False Then Exit Sub
        Dim Aux3 As List(Of Integer) = aux.ToList
        Aux3(0) = de
        HanoiN(n - 2, aux(0), para, Aux3)

    End Sub

    Sub HanoiIguais()
        For i = 1 To Discos.Count - 1
            If resolver = False Then Exit Sub
            Discos(i).MovePara(Pinos(Discos.Count - i + 1))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
        Next
        If resolver = False Then Exit Sub
        Discos(1).MovePara(Pinos(Pinos.Count - 1))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        If resolver = False Then Exit Sub
        Discos(Discos.Count).MovePara(Pinos(Pinos.Count))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        If resolver = False Then Exit Sub
        Discos(Discos.Count - 1).MovePara(Pinos(Pinos.Count))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        If resolver = False Then Exit Sub
        Discos(1).MovePara(Pinos(1))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        If resolver = False Then Exit Sub
        For i = Discos.Count - 2 To 1 Step -1
            If resolver = False Then Exit Sub
            Discos(i).MovePara(Pinos(Pinos.Count))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
        Next
    End Sub

    Sub HanoiEasy()
        For i = 1 To Discos.Count - 1
            If resolver = False Then Exit Sub
            'printf("\t[%d] Mova o disco %d para o pilar %d.\n", ++mv, i, i+1);
            Discos(i).MovePara(Pinos(i + 1))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
        Next
        'printf("\t[%d] Mova o disco %d para o pilar %d.\n", ++mv, m, n);
        If resolver = False Then Exit Sub
        Discos(Discos.Count).MovePara(Pinos(Pinos.Count))
        Movimentos += 1
        LabelMovs.Text = Movimentos
        Delay(Wait)
        For i = Discos.Count - 1 To 1 Step -1
            If resolver = False Then Exit Sub
            Discos(i).MovePara(Pinos(Pinos.Count))
            Movimentos += 1
            LabelMovs.Text = Movimentos
            Delay(Wait)
            'printf("\t[%d] Mova o disco %d para o pilar %d.\n", ++mv,i, n);
        Next
    End Sub

    Private Sub NovoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NovoToolStripMenuItem.Click
        Dim pd As New PinDis
        If pd.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.Controls.Clear()
            Me.Controls.Add(Me.MenuStrip1)
            Me.Controls.Add(Me.Panel1)
            Me.Refresh()
            NovoJogo(pd.NumericUpDown2.Value, pd.NumericUpDown1.Value)
        End If
    End Sub

    Private Sub IniciarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IniciarToolStripMenuItem.Click
        Try
            Wait = Double.Parse(InputBox("Atraso entre movimentos (em segundos)", "Delay", "0,5"))
        Catch
            Exit Sub
        End Try
        resolver = True
        Movimentos = 0
        LabelMovs.Text = "0"
        Delay(Wait)
        If Pinos.Count = 3 Then
            Hanoi3(Discos.Count, 1, 2, 3)
        ElseIf Pinos.Count = 4 Then
            Hanoi4(Discos.Count, 1, 2, 3, 4)
        ElseIf Pinos.Count = Discos.Count Then
            HanoiIguais()
        ElseIf Pinos.Count > Discos.Count Then
            HanoiEasy()
        ElseIf Pinos.Count = 5 Then
            Hanoi5(Discos.Count, 1, 5, 2, 3, 4)
        Else
            Dim aux As New List(Of Integer)
            For i = 2 To Pinos.Count - 1
                aux.Add(i)
            Next
            HanoiN(Discos.Count, 1, Pinos.Count, aux)
        End If
    End Sub

    Private Sub PararToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PararToolStripMenuItem.Click
        resolver = False

    End Sub

    Private Sub ReiniciarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReiniciarToolStripMenuItem.Click
        Me.Controls.Clear()
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Refresh()
        NovoJogo(Discos.Count, Pinos.Count)
    End Sub

    Private Sub SairToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SairToolStripMenuItem.Click
        On Error Resume Next

        resolver = False
        Application.ExitThread()
        Application.Exit()

    End Sub

    Private Sub SobreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SobreToolStripMenuItem.Click
        Sobre.ShowDialog()

    End Sub
End Class

Class Pino
    Private WithEvents mPilarIMG As New PictureBox With {.Image = My.Resources.Pilar, .SizeMode = PictureBoxSizeMode.StretchImage}
    Private mNumero As Integer
    Public Discos As New Stack(Of Disco)
    Public Sub New(ByVal Parent As Control, ByVal Numero As Integer, ByVal Location As Point, ByVal Size As Size)
        Me.Numero = Numero
        Me.Location = Location
        Me.Size = Size
    End Sub
    ReadOnly Property Pilar() As Control
        Get
            Return mPilarIMG
        End Get
    End Property

    ReadOnly Property Topo() As Point
        Get
            If Me.Discos.Count > 0 Then
                Return New Point(mPilarIMG.Location.X, (mPilarIMG.Location.Y + Me.Size.Height) - (80 + Me.Discos.Count * 50))
            Else
                Return New Point(Me.Location.X, Me.Location.Y + Me.Size.Height - 80)
            End If
        End Get
    End Property
    Property Numero() As Integer
        Get
            Return mNumero
        End Get
        Private Set(ByVal value As Integer)
            mNumero = value
        End Set
    End Property
    Property Size() As Size
        Get
            Return mPilarIMG.Size
        End Get
        Protected Set(ByVal value As Size)
            mPilarIMG.Size = value
            mPilarIMG.Update()
        End Set
    End Property

    Property Location() As Point
        Get
            Return mPilarIMG.Location
        End Get
        Protected Set(ByVal value As Point)
            mPilarIMG.Location = value
            mPilarIMG.Update()
        End Set
    End Property

End Class

Class Disco
    Private mNumero As Integer
    Private mPilar As Pino
    Private WithEvents mDiscIMG As New PictureBox With {.SizeMode = PictureBoxSizeMode.StretchImage, .Image = My.Resources.Disco, .BackColor = Color.Transparent}
    Public Sub New(ByVal Parent As Control, ByVal Numero As Integer, ByVal Size As Size, ByVal Pilar As Pino)
        Me.Numero = Numero
        Me.Size = Size
        Me.Pilar = Pilar
    End Sub
    ReadOnly Property Disco() As Control
        Get
            Return mDiscIMG
        End Get
    End Property
    Property Numero() As Integer
        Get
            Return mNumero
        End Get
        Private Set(ByVal value As Integer)
            mNumero = value
        End Set
    End Property
    Property Size() As Size
        Get
            Return mDiscIMG.Size
        End Get
        Protected Set(ByVal value As Size)
            mDiscIMG.Size = value
            mDiscIMG.Update()
        End Set
    End Property
    Property Pilar() As Pino
        Get
            Return mPilar
        End Get
        Protected Set(ByVal value As Pino)
            mPilar = value
            If value IsNot Nothing Then
                Me.Location = value.Topo
            End If
        End Set
    End Property
    Property Location() As Point
        Get
            Return mDiscIMG.Location
        End Get
        Protected Set(ByVal value As Point)
            mDiscIMG.Location = New Point(20 + value.X - Me.Size.Width / 2, value.Y) 'value
            mDiscIMG.Update()
        End Set
    End Property
    Sub MovePara(ByVal Pilar As Pino)
        On Error Resume Next
        Me.Pilar.Discos.Pop()
        Me.Pilar = Pilar
        Pilar.Discos.Push(Me)
    End Sub

End Class
