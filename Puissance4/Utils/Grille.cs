// Copyright (C) 2022, Maël Coulmance

namespace Puissance4.Utils
{
    public class Grille
    {
        private List<Column> m_content;

        public Grille(int lines, int columns, int x0, int y0, int caseWidth, int caseHeight)
        {
            m_content = new();

            for (int i = 0, offset = 0; i < columns; i++, offset += caseWidth)
            {
                m_content.Add(new(lines, x0 + offset, y0, caseWidth, caseHeight));
            }
        }

        public Column GetColumn(int index)
        {
            if (index < 0 || index >= m_content.Count)
                throw new IndexOutOfRangeException("Given index is out of range");

            return m_content[index];
        }

        public List<Image> GetImages()
        {
            List<Image> res = new();

            foreach (Column c in m_content)
            {
                res.Add(c.Image);
            }

            return res;
        }

        public bool Full()
        {
            foreach (Column c in m_content)
            {
                if (!c.Full)
                    return false;
            }

            return true;
        }

        public Content IsWin()
        {
            Content c;

            for (int i = 0; i < m_content.Count; i++)
            {
                if ((c = IsColumnFull(i)) != Content.Empty)
                    return c;
            }

            for (int i = 0; i < m_content[0].Count; i++)
            {
                if ((c = IsLineFull(i)) != Content.Empty)
                    return c;
            }

            for (int i = 0; i < m_content[0].Count; i++)
            {
                for (int j = 0; j < m_content.Count; j++)
                {
                    if ((c = IsDiagonalFull(i, j)) != Content.Empty)
                        return c;
                }
            }

            return Content.Empty;
        }



        private Content IsLineFull(int idLine)
        {
            int accY = 0;
            int accR = 0;

            for (int i = 0; i < m_content.Count; i++)
            {
                if (m_content[i].GetCase(idLine).Content == Content.Yellow)
                {
                    accY++;
                    accR = 0;
                }
                else if (m_content[i].GetCase(idLine).Content == Content.Red)
                {
                    accY = 0;
                    accR++;
                }
                else
                {
                    accY = 0;
                    accR = 0;
                }

                if (accR == 4)
                    return Content.Red;
                else if (accY == 4)
                    return Content.Yellow;
            }

            return Content.Empty;
        }

        private Content IsColumnFull(int idColumn)
        {
            int accY = 0;
            int accR = 0;

            for (int i = 0; i < m_content[idColumn].Count; i++)
            {
                if (m_content[idColumn].GetCase(i).Content == Content.Yellow)
                {
                    accY++;
                    accR = 0;
                }
                else if (m_content[idColumn].GetCase(i).Content == Content.Red)
                {
                    accY = 0;
                    accR++;
                }
                else
                {
                    accY = 0;
                    accR = 0;
                }

                if (accY == 4)
                    return Content.Yellow;
                else if (accR == 4)
                    return Content.Red;
            }

            return Content.Empty;
        }

        private Content IsDiagonalFull(int idLine, int idColumn)
        {
            if (idLine + 3 >= m_content[idColumn].Count)
                return Content.Empty;

            if (idColumn - 3 >= 0)
            {
                Content c = m_content[idColumn].GetCase(idLine).Content;

                if (c == m_content[idColumn-1].GetCase(idLine+1).Content
                    && c == m_content[idColumn-2].GetCase(idLine+2).Content
                    && c == m_content[idColumn-3].GetCase(idLine+3).Content)
                {
                    return c;
                }
            }

            if (idColumn + 3 < m_content[idColumn].Count)
            {
                Content c = m_content[idColumn].GetCase(idLine).Content;

                if (c == m_content[idColumn + 1].GetCase(idLine + 1).Content
                    && c == m_content[idColumn + 2].GetCase(idLine + 2).Content
                    && c == m_content[idColumn + 3].GetCase(idLine + 3).Content)
                {
                    return c;
                }
            }

            return Content.Empty;
        }
    }
}
