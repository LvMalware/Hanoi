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
        Wait = Double.Parse(InputBox("Atraso entre movimentos (em segundos)", "Delay", "0.5"))
        resolver = True
        Movimentos = 0
        Hanoi3(Discos.Count, 1, 2, 3)
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
End Class

Class Pino
    Private WithEvents mPilarIMG As New PictureBox With {.Image = My.Resources.Pilar, .SizeMode = PictureBoxSizeMode.StretchImage}
    Private mNumero As Integer
    Public Discos As New Queue(Of Disco)
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
            mDiscIMG.Location = New Point(30 + value.X - Me.Size.Width / 2, value.Y) 'value
            mDiscIMG.Update()
        End Set
    End Property
    Sub MovePara(ByVal Pilar As Pino)
        On Error Resume Next
        Me.Pilar.Discos.Dequeue()
        Me.Pilar = Pilar
        Pilar.Discos.Enqueue(Me)
    End Sub

End Class
