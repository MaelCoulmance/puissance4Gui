// Copyright (C) 2022, Maël Coulmance

#nullable enable

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Puissance4.Utils
{
    public class Column
    {
        private List<Case> m_content;
        private Image? m_image;
        private int m_nextLine;

        public Column(int size, int x0, int y0, int caseWidth, int caseHeight)
        {
            m_content = new List<Case>();

            for (int i = 0, offset = 0; i < size; i++, offset += caseHeight)
            {
                m_content.Add(new(x0 + offset, y0, caseWidth, caseHeight));
            }

            m_image = null;
            m_nextLine = size-1;
        }

        public int Count => m_content.Count;
        public int Width => m_content[0].Width;
        public int Height => m_content[0].Height * m_content.Count;

        public Point Location => m_content[0].Location;
        public Size Size => new(Width, Height);

        public Image Image => DrawImage();

        public bool Full => m_nextLine < 0;



        public bool Click(Content c)
        {
            if (m_nextLine >= 0)
            {
                m_content[m_nextLine--].Click(c);
                m_image = null;
                return true;
            }

            return false;
        }

        public Case GetCase(int index)
        {
            if (index < 0 || index >= m_content.Count)
                throw new ArgumentOutOfRangeException("Given index is out of range");

            return m_content[index];
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

                int offset = 0;
                foreach (Case c in m_content)
                {
                    g.DrawImage(c.Image, 0, offset);
                    offset += c.Height;
                }

                m_image = Image.FromHbitmap(bmp.GetHbitmap());
            }

            return m_image;
        }
        
    }
}
