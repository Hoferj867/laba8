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
        Finish finish;
        Model1 Player;
        Trees tree;
        Bush bushes;
        Game game = new Game();
        Enemies enemies = new Enemies();
        Random rand;
        Environment Envi = new Environment();
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            this.Size = new Size(1500,800);
            // Ресуем финиш
            finish = new Finish(this);
            // Рисуем игрока
            Player = new Model1();
            // Отображаем все
            label1.Text = Convert.ToString(Player.life); // Отображаем жизни
            pictureBoxMain.Controls.Add(Player.Player);
            pictureBoxMain.Controls.Add(finish.finish);
            // Окружение
            tree = new Trees();
            bushes = new Bush();
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
            Player.Moving(Player, game, e, this, run_time);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) { Player.FireFlag = false; }
        }

        private void run_Tick(object sender, EventArgs e)
        {
            game.playerRun.Stop();
            game.song = true;
        }

        private void Game_Tick(object sender, EventArgs e)
        {
           
            game.Destruction_tree(tree);
            enemies.Enemy_intelligence(Player, this, game, pictureBoxMain);
            game.Projectile_drop(Player, enemies, game, label1, Animation_Invulnerability, Invulnerability_tim, Game_time);

            if (Player.Invulnerability == false)
            {
                
                if (game.Crossing(Player.Player, finish.finish))
                {
                    game.Stop_timers(Animation_Invulnerability, Invulnerability_tim, Game_time);
                    Player = null;
                    MessageBox.Show("You Win!");
                }
            }
        }

        private void Invulnerability_Tick(object sender, EventArgs e)
        {
            Player.Invulnerability_Player(Animation_Invulnerability, Invulnerability_tim);
        }
        private void Animation_Invulnerability_Tick(object sender, EventArgs e)
        {
            Player.Animation_Invulnerability();
        }

        

        private void Respawn_enemies_Tick(object sender, EventArgs e)
        {
            if (enemies.Enemies_mass.LongCount() > 2)
            {

            }
            else
            {
                enemies.new_Enemies(pictureBoxMain, this);
            }
        }

        

        private void PictureBoxMain_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e, object obj)
        {
           
                
        }
    }
}
