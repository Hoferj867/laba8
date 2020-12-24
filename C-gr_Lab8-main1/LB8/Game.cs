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
    class Game
    {
        public bool Crossing(PictureBox first, PictureBox Second) // Пересечение объектов
        {
            Rectangle first_z = first.DisplayRectangle;
            Rectangle Second_z = Second.DisplayRectangle;
            first_z.Location = first.Location;
            Second_z.Location = Second.Location;
            if (first_z.IntersectsWith(Second_z))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Crossing_10px(PictureBox first, PictureBox Second) // Пересечение объектов
        {
            Rectangle first_z = first.DisplayRectangle;
            Rectangle Second_z = Second.DisplayRectangle;
            first_z.Location = new Point(first.Location.X - 20, first.Location.Y - 20);
            first_z.Size = new Size(first.Size.Width + 40, first.Size.Height + 40);
            Second_z.Location = Second.Location;
            if (first_z.IntersectsWith(Second_z))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Let (Model1 Player, Trees tree, Bush rock ) // Препятствие
        {
            if (Player.Rideability == true)
            {
                for (int i = 0; i < tree.Trees_mass.Length; i++)
                {
                    if (Crossing(Player.Player, tree.Trees_mass[i]) && tree.Let[i] != true)
                    { Bias(Player); Player.Rideability = false; }
                    else { Player.Rideability = true; }
                }
                for (int i = 0; i < rock.Bush_arr.Length; i++)
                {
                    if (Crossing(Player.Player, rock.Bush_arr[i]))
                    { Bias(Player); }
                    else { Player.Rideability = true; }
                }
            }
        }
        public void Bias(Model1 Player)
        {
            Player.Rideability = false;
            if (Player.Position == "Right") { Player.Player.Left = Player.Player.Left - Player.PlayerSpeed * 2; };
            if (Player.Position == "Left") { Player.Player.Left = Player.Player.Left + Player.PlayerSpeed * 2; };
            if (Player.Position == "Up") { Player.Player.Top = Player.Player.Top + Player.PlayerSpeed * 2; };
            if (Player.Position == "Down") { Player.Player.Top = Player.Player.Top - Player.PlayerSpeed * 2; };
        }
    }
}
