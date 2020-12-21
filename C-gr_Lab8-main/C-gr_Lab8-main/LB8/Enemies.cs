using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace LB8
{
    class Enemies
    {
       
        public List<PictureBox> Enemies_mass = new List<PictureBox>();
        public List<string> Enemies_mass_Position = new List<string>();
        public int Cooldown = 30; // Скорость зарядки пушки (3/10 секунды)
        public bool FireFlag; // Флаг огня 
        public int EnemiesSpeed = 3; // Скорость танка
        public int life = 1; // Кол-во жизней
        int a = 1;
        public bool Rideability = true;
        Random rand = new Random(); 
        public Enemies()
        {
            
        }

        public void new_Enemies(PictureBox Main, Form1 forma)
        {
            PictureBox Enemies = new PictureBox();
            FireFlag = false;
            Enemies.Image = Image.FromFile(@"Red/3.png");
            Enemies.BackColor = Color.Transparent;
            Enemies.Location = new Point(forma.Width - 600, 400);
            Enemies.Size = new Size(Enemies.Image.Width, Enemies.Image.Height);
            string poz = "Left";
            Main.Controls.Add(Enemies);
            Enemies_mass.Add(Enemies);
            Enemies_mass_Position.Add(poz);
        }

        public void Enemy_intelligence(Model1 Player, Form1 forma, Game game, PictureBox pictureBoxMain)
        {
            for (int  i = 0; i < Enemies_mass.LongCount();i++)
            {
                if (rand.Next(20) == 1)
                {
                    a = rand.Next(4);
                }
                if (Rideability == true)
                {
                    if (a == 0)
                    {
                        if (Enemies_mass_Position[i] != "Right") { Turn("Right", Enemies_mass[i]); Enemies_mass_Position[i] = "Right"; } else { }
                        if (Enemies_mass[i].Left + Enemies_mass[i].Width + EnemiesSpeed < forma.Width)
                        { Enemies_mass[i].Left = Enemies_mass[i].Left + EnemiesSpeed; }
                    }
                    if (a == 1)
                    {
                        if (Enemies_mass_Position[i] != "Left") { Turn("Left", Enemies_mass[i]); Enemies_mass_Position[i] = "Left"; }
                        if (Enemies_mass[i].Left >= EnemiesSpeed) { Enemies_mass[i].Left = Enemies_mass[i].Left - EnemiesSpeed; }
                    }
                    if (a == 2)
                    {
                        if (Enemies_mass_Position[i] != "Up") { Turn("Up", Enemies_mass[i]); Enemies_mass_Position[i] = "Up"; }
                        if (Enemies_mass[i].Top >= 0) { Enemies_mass[i].Top = Enemies_mass[i].Top - EnemiesSpeed; }
                    }
                    if (a == 3)
                    {
                        if (Enemies_mass_Position[i] != "Down") { Turn("Down", Enemies_mass[i]); Enemies_mass_Position[i] = "Down"; }
                        if (Enemies_mass[i].Bottom <= forma.Height) { Enemies_mass[i].Top = Enemies_mass[i].Top + EnemiesSpeed; }
                    }
                }
            }
                for (int k = 0; k < Enemies_mass.LongCount(); k++)
                {
                    Point lokBullets = new Point(0,0);
                    if (Enemies_mass_Position[k] == "Right") { lokBullets = new Point(Enemies_mass[k].Location.X + Enemies_mass[k].Image.Width, Enemies_mass[k].Location.Y + Enemies_mass[k].Image.Height / 2); };
                    if (Enemies_mass_Position[k] == "Left") { lokBullets = new Point(Enemies_mass[k].Location.X, Enemies_mass[k].Location.Y + Enemies_mass[k].Image.Height / 2);};
                    if (Enemies_mass_Position[k] == "Up") { lokBullets = new Point(Enemies_mass[k].Location.X + Enemies_mass[k].Image.Width / 2, Enemies_mass[k].Location.Y); };
                    if (Enemies_mass_Position[k] == "Down") { lokBullets = new Point(Enemies_mass[k].Location.X + Enemies_mass[k].Image.Width / 2, Enemies_mass[k].Location.Y + Enemies_mass[k].Image.Height);};
                    for (int i = Player.Player.Location.Y; i < Player.Player.Location.Y + Player.Player.Image.Height; i++)
                    {
                        for (int j = Player.Player.Location.X; j < Player.Player.Location.X + Player.Player.Image.Width; j++)
                        {
                            if (i == lokBullets.Y || j == lokBullets.X)
                            {
                                if (Enemies_mass_Position[k] == "Right") { if (j > lokBullets.X) { FireFlag = true;  } };
                                if (Enemies_mass_Position[k] == "Left") { if (j < lokBullets.X) { FireFlag = true;} };
                                if (Enemies_mass_Position[k] == "Up") { if (i < lokBullets.Y) { FireFlag = true;} };
                                if (Enemies_mass_Position[k] == "Down") { if (i > lokBullets.Y) { FireFlag = true;} };
                            FireFlag = false;
                            }
                        }
                    }
                }
            
        }
        public void Bias(Enemies Player, int h)
        {
            for (int i = 0; i < Enemies_mass.LongCount(); i++)
            {
                Player.Rideability = false;
                if (Enemies_mass_Position[i] == "Right") { Player.Enemies_mass[h].Left = Player.Enemies_mass[h].Left - Player.EnemiesSpeed * 2; };
                if (Enemies_mass_Position[i] == "Left") { Player.Enemies_mass[h].Left = Player.Enemies_mass[h].Left + Player.EnemiesSpeed * 2; };
                if (Enemies_mass_Position[i] == "Up") { Player.Enemies_mass[h].Top = Player.Enemies_mass[h].Top + Player.EnemiesSpeed * 2; };
                if (Enemies_mass_Position[i] == "Down") { Player.Enemies_mass[h].Top = Player.Enemies_mass[h].Top - Player.EnemiesSpeed * 2; };
            }
        }
        public void Turn(string nowPosition, PictureBox Player)
        {
            for (int i = 0; i < Enemies_mass.LongCount(); i++)
            {
                if (nowPosition == "Right" || nowPosition == "Left" && Enemies_mass_Position[i] == "Up" || Enemies_mass_Position[i] == "Down")
                {
                    int temp = Player.Width;
                    Player.Width = Player.Height;
                    Player.Height = temp;
                }
                if (nowPosition == "Up" || nowPosition == "Down" && Enemies_mass_Position[i] == "Right" || Enemies_mass_Position[i] == "Left")
                {
                    int temp = Player.Height;
                    Player.Height = Player.Width;
                    Player.Width = temp;
                }
                if (nowPosition == "Right")
                {
                    if (Enemies_mass_Position[i] == "Left") { Player.Image.RotateFlip(RotateFlipType.Rotate180FlipY); }
                    if (Enemies_mass_Position[i] == "Up") { Player.Image.RotateFlip(RotateFlipType.Rotate270FlipX); }
                    if (Enemies_mass_Position[i] == "Down") { Player.Image.RotateFlip(RotateFlipType.Rotate90FlipX); }
                    Enemies_mass_Position[i] = "Right";
                }
                if (nowPosition == "Left")
                {
                    if (Enemies_mass_Position[i] == "Right") { Player.Image.RotateFlip(RotateFlipType.Rotate180FlipY); }
                    if (Enemies_mass_Position[i] == "Up") { Player.Image.RotateFlip(RotateFlipType.Rotate270FlipY); }
                    if (Enemies_mass_Position[i] == "Down") { Player.Image.RotateFlip(RotateFlipType.Rotate90FlipY); }
                    Enemies_mass_Position[i] = "Left";
                }
                if (nowPosition == "Up")
                {
                    if (Enemies_mass_Position[i] == "Left") { Player.Image.RotateFlip(RotateFlipType.Rotate90FlipX); }
                    if (Enemies_mass_Position[i] == "Right") { Player.Image.RotateFlip(RotateFlipType.Rotate270FlipX); }
                    if (Enemies_mass_Position[i] == "Down") { Player.Image.RotateFlip(RotateFlipType.Rotate180FlipX); }
                    Enemies_mass_Position[i] = "Up";
                }
                if (nowPosition == "Down")
                {
                    if (Enemies_mass_Position[i] == "Right") { Player.Image.RotateFlip(RotateFlipType.Rotate90FlipX); }
                    if (Enemies_mass_Position[i] == "Up") { Player.Image.RotateFlip(RotateFlipType.Rotate180FlipX); }
                    if (Enemies_mass_Position[i] == "Left") { Player.Image.RotateFlip(RotateFlipType.Rotate270FlipX); }
                    Enemies_mass_Position[i] = "Down";
                }
            }
        }
        
    }
}
