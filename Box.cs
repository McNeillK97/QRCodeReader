using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR_Scanner
{
    class Box
    {
        private int id;
        private float length;
        private float width;
        private float height;
        private float weight;

        public Box()
        {
            id = 0;
            length = 0;
            width = 0;
            height = 0;
            weight = 0;
        }

        public Box(int i, float len, float wid, float hei, float wei)
        {
            id = i;
            length = len;
            width = wid;
            height = hei;
            weight = wei;
        }

        public int getID()
        {
            return id;
        }

        public float getLength()
        {
            return length;
        }

        public float getWidth()
        {
            return width;
        }

        public float getHeight()
        {
            return height;
        }

        public float getWeight()
        {
            return weight;
        }
    }
}
