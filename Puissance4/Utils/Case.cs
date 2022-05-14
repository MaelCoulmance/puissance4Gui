// Copyright (C) 2022, Maël Coulmance

#nullable enable

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Puissance4.Utils
{
    public class Case
    {
        public readonly Point p1;   // up-left corner
        public readonly Point p2;   // up-right corner
        public readonly Point p3;   // down-left corner
        public readonly Point p4;   // down-right corner

        private Image? m_image;


        public Case(int x1, int y1, int width, int height)
        {
            p1 = new(x1, y1);
            p2 = new(x1 + width, y1);
            p3 = new(x1, y1 + height);
            p4 = new(x1 + width, y1 + height);

            Content = Content.Empty;
            m_image = null;
        }

        public Point Location => p1;
        public Size Size => new(Width, Height);
        public int Width => p2.X - p1.X;
        public int Height => p3.Y - p1.Y;

        
        public Content Content { get; private set; }

        public Image Image => DrawImage();



        public void Click(Content c)
        {
            if (Content == Content.Empty && c != Content.Empty)
            {
                Content = c;
                m_image = null;
            }
        }


        private Image DrawImage()
        {
            if (m_image is null)
            {
                Bitmap bmp = new(Width, Height);
                Graphics g = Graphics.FromImage(bmp);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.High;

                g.FillRectangle(Brushes.White, 0, 0, Width - 1, Height - 1);
                g.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);

                Rectangle rec = new((Width / 8), (Height / 8), 3 * (Height / 4), 3 * (Width / 4));

                if (Content == Content.Red)
                    g.FillEllipse(Brushes.Red, rec);
                else if (Content == Content.Yellow)
                    g.FillEllipse(Brushes.Yellow, rec);

                g.DrawEllipse(Pens.Black, rec);

                m_image = Image.FromHbitmap(bmp.GetHbitmap());
            }

            return m_image;
        }
    }
}
