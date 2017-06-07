using System.Collections.Generic;
using PulseEngine.Objects.Sprite;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PulseEngine.Component.Collision;
using Microsoft.Xna.Framework;

namespace PulseEngine.Display.World
{
    public class TileMap
    {
        public List<Entity> Tiles =
            new List<Entity>();

        public List<string> TileName =
            new List<string>();


        //Properties 
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public int Offset_X { get; set; }
        public int Offset_Y { get; set; }
        public float ZPlane { get; set; }

        public TileMap()
        {
            ZPlane = 0.05f;
            TileName.Add("_Blank_");
        }

        public void Load(ContentManager content, 
           int [,] map, 
           int tileWidth,
           int tileHeight, float scale)
        {
            MapHeight = map.GetLength(0); //Get first range - Rows
            MapWidth = map.GetLength(1); //Get Second range - Columns

            TileWidth = tileWidth;
            TileHeight = tileHeight;

            //Load Map and Tiles into Memory
            for(int y = 0; y < MapHeight; y++)
                for(int x = 0; x < MapWidth; x++)
                {
                    Tiles.Add(LoadTiles(content, map[y, x], scale));
                }
        }

        Entity LoadTiles(ContentManager content, int index, float scale)
        {
        BoundingRectangle _boundingBox =
            new BoundingRectangle();

        Entity _tile;
            _tile = new Entity()
            {
                Scale = scale
            };
            _boundingBox.AttachTo(_tile);
            _tile.AddComponent(_boundingBox);
            _tile.ZPlane = this.ZPlane;

            if (index > 0 && index<TileName.Count)
            {
                _tile.AssetName = TileName[index];
                _tile.Load(content);
                _tile.Width = TileWidth;
                _tile.Height = TileHeight;
            }
            else
            {
                _tile.AssetName = TileName[index];
                _tile.Width = TileWidth;
                _tile.Height = TileHeight;
            }
            
            return _tile;
        }

        public void Update(GameTime gameTime)
        {
            foreach(Entity e in Tiles)
            {
                e.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int _index = 0;

            if(Tiles.Count>0)
            foreach(Entity t in Tiles)
            {
                int x = _index % MapWidth;
                int y = _index / MapWidth;

                t.X = x * TileWidth + Offset_X;
                t.Y = y * TileHeight + Offset_Y;

                    if (t.AssetName != null)
                    {
                        if (!t.AssetName.Contains("_Blank_"))
                        {
                            t.Draw(spriteBatch);
                        }_index++;   
                    }

                   // _index++;
                }

        }
    }
}
