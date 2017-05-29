using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LowRezJam
{
    public class TileSheet
    {
        public Texture2D TileSheetPicture { get; set; }
        public Point TileSize { get; set; }
        public int Tiles { get; set; }

        public TileSheet(Texture2D TileSheetPicture, int TileSize)
        {
            this.TileSheetPicture = TileSheetPicture;
			this.TileSize = new Point(TileSize, TileSize);

            Tiles = (TileSheetPicture.Width / TileSize) * (TileSheetPicture.Height / TileSize);
        }

		public TileSheet(Texture2D TileSheetPicture, Point TileSize)
		{
			this.TileSheetPicture = TileSheetPicture;
			this.TileSize = TileSize;

			Tiles = (TileSheetPicture.Width / TileSize.X) * (TileSheetPicture.Height / TileSize.Y);
		}

        public void DrawTile(SpriteBatch spriteBatch, int Tile, Rectangle TileRectangle)
        {
			if (Tile != -1)
            	spriteBatch.Draw(TileSheetPicture, TileRectangle, new Rectangle(IndexID(Tile).X * TileSize.X, IndexID(Tile).Y * TileSize.Y, TileSize.X, TileSize.Y), Color.White);
        }

        public void DrawTile(SpriteBatch spriteBatch, int Tile, Rectangle TileRectangle, Color SpriteColor)
        {
			if (Tile != -1)
            	spriteBatch.Draw(TileSheetPicture, TileRectangle, new Rectangle(IndexID(Tile).X * TileSize.X, IndexID(Tile).Y * TileSize.Y, TileSize.X, TileSize.Y), SpriteColor);
        }

        public Point IndexID(int Tile)
        {
            Point TileAmount = new Point(TileSheetPicture.Width/TileSize.X,TileSheetPicture.Height/TileSize.Y);
            int X = Tile;
            int Y = 0;

            while (TileAmount.X <= X)
            { 
                X -= TileAmount.X;
                Y++;
            }

            return new Point(X,Y);
            //ID = Num in row + (Num of rows * length of row)
            //array[x] [y]: 
                //x = id - row length until id is smaller than row length, y = only the integer of id/row length
        }
    }
}
