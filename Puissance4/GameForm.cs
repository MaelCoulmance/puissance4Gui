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

using Puissance4.Utils;

namespace Puissance4
{
    public partial class GameForm : Form
    {
        private Grille m_grille;
        private bool m_P1Playing;

        private string m_nameP1;
        private string m_nameP2;

        private bool m_GameOver;

        public GameForm(string nameP1, string nameP2)
        {
            InitializeComponent();

            m_grille = new(6, 7, 36, 32, 100, 100);
            m_P1Playing = (new Random()).Next(100) % 2 == 0;
            m_nameP1 = nameP1;
            m_nameP2 = nameP2;
            m_GameOver = false;

            InitializePictures();
            InitializeButtons();
            UpdateLabel();
        }


        private void InitializePictures()
        {
            m_column1.Location = m_grille.GetColumn(0).Location;
            m_column1.Size = m_grille.GetColumn(0).Size;
            m_column1.Image = m_grille.GetColumn(0).Image;

            m_column2.Location = m_grille.GetColumn(1).Location;
            m_column2.Size = m_grille.GetColumn(1).Size;
            m_column2.Image = m_grille.GetColumn(1).Image;

            m_column3.Location = m_grille.GetColumn(2).Location;
            m_column3.Size = m_grille.GetColumn(2).Size;
            m_column3.Image = m_grille.GetColumn(2).Image;

            m_column4.Location = m_grille.GetColumn(3).Location;
            m_column4.Size = m_grille.GetColumn(3).Size;
            m_column4.Image = m_grille.GetColumn(3).Image;

            m_column5.Location = m_grille.GetColumn(4).Location;
            m_column5.Size = m_grille.GetColumn(4).Size;
            m_column5.Image = m_grille.GetColumn(4).Image;

            m_column6.Location = m_grille.GetColumn(5).Location;
            m_column6.Size = m_grille.GetColumn(5).Size;
            m_column6.Image = m_grille.GetColumn(5).Image;

            m_column7.Location = m_grille.GetColumn(6).Location;
            m_column7.Size = m_grille.GetColumn(6).Size;
            m_column7.Image = m_grille.GetColumn(6).Image;
        }

        private void InitializeButtons()
        {
            m_column1.Click += ButtonClicked;
            m_column2.Click += ButtonClicked;
            m_column3.Click += ButtonClicked;
            m_column4.Click += ButtonClicked;
            m_column5.Click += ButtonClicked;
            m_column6.Click += ButtonClicked;
            m_column7.Click += ButtonClicked;
        }


        private void ButtonClicked(object? sender, EventArgs e)
        {
            if (!m_GameOver)
            {
                if (sender is PictureBox and not null)
                {
                    var button = sender as PictureBox;
                    int id = m_grille.GetImages().IndexOf(button!.Image);

                    Content c = m_P1Playing ? Content.Yellow : Content.Red;

                    bool ok = m_grille.GetColumn(id).Click(c);
                    button.Image = m_grille.GetColumn(id).Image;

                    if (ok)
                    {
                        m_P1Playing = !m_P1Playing;
                        UpdateLabel();
                    }

                    if ((c = m_grille.IsWin()) != Content.Empty)
                    {
                        m_GameOver = true;
                        UpdateLabelWin(c);
                    }
                    else if (m_grille.Full())
                    {
                        m_GameOver = true;
                        UpdateLabelLoose();
                    }
                }


            }
        }

        private void UpdateLabel()
        {
            StringBuilder sb = new StringBuilder("C'est au tour de ");
            sb.Append(m_P1Playing ? m_nameP1 : m_nameP2);
            sb.Append(" de jouer");

            m_label.Text = sb.ToString();
        }

        private void UpdateLabelWin(Content c)
        {
            string name = c == Content.Yellow ? m_nameP1 : m_nameP2;

            m_label.Text = $"{name} remporte la partie";
        }

        private void UpdateLabelLoose()
        {
            m_label.Text = "La grille est pleine...";
        }
    }
}
