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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Model1 Player;
        Mines min;
        Trees tree;
        Bush bushes;
        Game game = new Game();
        Random rand;
        Environment Envi = new Environment();
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            this.Size = new Size(1500,800);
            // Рисуем игрока
            Player = new Model1();
            // Отображаем все
            label1.Text = Convert.ToString(Player.life); // Отображаем жизни
            pictureBoxMain.Controls.Add(Player.Player);
            // Окружение
            min = new Mines();
            tree = new Trees();
            bushes = new Bush();
            // Минируем поле
            min.Mining(this,pictureBoxMain,Envi);
            // Размещаем деревья
            tree.Landing(this,pictureBoxMain, Envi);
            // Камни
            bushes.Resp(this, pictureBoxMain, Envi);
            Game_time.Start();
            Bombs.Start();
            Respawn_enemies.Start();
        }

        private void pictureBoxMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Player.Moving(Player, game, e, this, run_time, min);
        }


        private void Game_Tick(object sender, EventArgs e)
        {
            if (Player.Invulnerability == false)
            {
                min.Mine_explosion(Player, game, label1, Animation_Invulnerability, Invulnerability_tim, Game_time);
            }
        }

        private void Invulnerability_Tick(object sender, EventArgs e)
        {
            Player.Invulnerability_Player(Animation_Invulnerability, Invulnerability_tim);
        }

        private void Demining_Tick(object sender, EventArgs e)
        {
            string temp = Player.Position;
            Player.Player.Image = Image.FromFile(@"Blue/Model1.png");
            Player.Position = "Right";
            min.Turn(temp, Player);
            Player.Rideability = true;
            min.Mins[min.size].Dispose();
            min.Mins.Remove(min.Mins[min.size]);
            Demining.Stop();
        }

        private void PictureBoxMain_Click(object sender, EventArgs e)
        {

        }
    }
}
