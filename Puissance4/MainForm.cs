// Copyright (C) 2022, Maël Coulmance

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Puissance4
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Puissance4.Resource.wallpaper.jpg");

            if (stream is not null)
                pictureBox1.Image = Image.FromStream(stream);
            else
                throw new FileNotFoundException("Cannot load file wallpaper.jpg");


            player1TextBox.PlaceholderText = "Joueur 1";
            player2TextBox.PlaceholderText = "Joueur 2";

            ActiveControl = startButton;

            KeyDown += MainForm_KeyDown;
        }

        private void MainForm_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (player1TextBox.Focused)
                        ActiveControl = player2TextBox;
                    break;

                case Keys.Up:
                    if (player2TextBox.Focused)
                        ActiveControl = player1TextBox;
                    break;

                case Keys.Enter:
                    startButton.PerformClick();
                    break;

                default:
                    break;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            string nameP1 = player1TextBox.Text;
            string nameP2 = player2TextBox.Text;

            if (nameP1 != "" && nameP2 != "")
            {
                if (nameP1 == nameP2)
                {
                    DialogResult res = MessageBox.Show("Attention: Le joueur 1 et le joueur 2 ont le même nom, cela pourrais"
                        + " porter à confusion lors de la partie.\n"
                        + "Souhaitez vous tout de même commencer la partie ?",
                        "Avertissement",
                        MessageBoxButtons.YesNo);

                    if (res == DialogResult.No)
                        return;
                }

                GameForm game = new(nameP1, nameP2);
                game.Disposed += Game_Disposed;
                Visible = false;
                game.Show();
            }
            else
            {
                MessageBox.Show("Vous devez entrer un nom pour les deux joueurs !", "Erreur: Noms manquants", MessageBoxButtons.OK);
            }
        }

        private void Game_Disposed(object? sender, EventArgs e)
        {
            Visible = true;
        }
    }
}
