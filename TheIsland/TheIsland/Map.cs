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
        MapData[,] Data;
        Random Rand = new Random();
        Perlin Noise;

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

        public MapData GetMapDataAt(int x, int y)
        {
            return Data[x, y];
        }

        void Generate(object o)
        {
            Generated = false;

            Data = new MapData[Width, Height];
            Noise = new Perlin();

            for(int y = 0; y < Height; ++y)
            {
                for(int x = 0; x < Width; ++x )
                {
                    UpdateGenerationProgress(x, y);

                    float height = GenerateHeight(x, y);

                    Data[x, y] = new MapData(height);
                }
            }

            GenerationProgress = 1.0f;
            Generated = true;
        }

        private void UpdateGenerationProgress(int x, int y)
        {
            int index = x + (y * Width);
            GenerationProgress = (float)index / (float)(Width * Height);
        }

        private float GenerateHeight(int x, int y)
        {
            float scale = 2.0f;
            return Noise.OctaveNoise(scale * new Microsoft.Xna.Framework.Vector3((float)x / (float)Width, (float)y / (float)Height, 0), 8);
        }
    }
}
