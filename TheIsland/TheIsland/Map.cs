using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheIsland
{
    public class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float GenerationProgress { get; private set; }
        public bool Generated { get; private set; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            GenerationProgress = 0;
            Generated = false;
        }

        public void BeginGeneration()
        {
            ThreadPool.QueueUserWorkItem(Generate, null);
        }

        void Generate(object o)
        {
            Generated = false;

            for(int y = 0; y < Height; ++y)
            {
                for(int x = 0; x < Width; ++x )
                {
                    int index = x + (y * Width);
                    GenerationProgress = (float)index / (float)(Width * Height);
                }
            }

            GenerationProgress = 1.0f;
            Generated = true;
        }
    }
}
