
using System.Drawing;
using System;

namespace test{

    public class GraphicsContext{

        public Graphics g = null;
        public AssetManager assets;

        public int screenWidth, screenHeight;
        // tileD&height of one tile in pixels
        public int MINOR_TILES_N = 6;
        public float minorSize, tileSize;
        // number of tiles visible on screen
        public int xTiles, yTiles;
        // the position of the screens top left corner
        public int x = 0, y = 0;
        // uiSize is a utility value holding 1/uin of the screen height, for UI purposes
        public int uiSize, uiN = 10;
        public static GraphicsContext gc = null;
        public Bitmap bitmap;
        

        public GraphicsContext(int screenX, int screenY, int tileD){
            assets = AssetManager.getAssetManager ();

            screenWidth = screenX;
            screenHeight = screenY;

            tileSize = tileD;

            xTiles = (int)(screenWidth / tileD) + 1;
            //System.out.println("xTiles = " + xTiles);
            yTiles = (int)(screenHeight / tileD) + 1;
            //System.out.println("yTiles = " + yTiles);

            uiSize = screenHeight / uiN;
            Console.WriteLine($"tilesize {tileSize}");
            minorSize = tileSize/MINOR_TILES_N;
            gc = this;

            bitmap = new Bitmap(
                screenX, 
                screenY, 
                System.Drawing.Imaging.PixelFormat.Format24bppRgb
            );
            g = Graphics.FromImage(bitmap);
        }

        public void drawObject(WorldObject wo){
            
            //g.FillRectangle(Brushes.DarkSlateGray, wo.x*minorSize - x, wo.y*minorSize -y, wo.xs*minorSize, wo.ys*minorSize);
            g.DrawRectangle(assets.blackpen, wo.x*minorSize - x, wo.y*minorSize -y, wo.xs*minorSize, wo.ys*minorSize);
        }

        public void centerAround(WorldObject obj){
            x = (int)(obj.x*minorSize - screenWidth/2);
            y = (int)(obj.y*minorSize - screenWidth/2);
        }

        public void orientateBorderAround (WorldObject obj, int border) {
            if (obj.x * tileSize < x + border * tileSize) {
                x = (int)(obj.x * tileSize * minorSize - border * tileSize);
            }

            if (obj.x * tileSize > x - (border + 2) * tileSize + xTiles * tileSize) {
                x = (int)((obj.x * tileSize*minorSize) + (border + 2) * tileSize - (xTiles * tileSize));
            }

            if (obj.y * tileSize  < y + border * tileSize) {
                y = (int)(obj.y * tileSize *minorSize - border * tileSize);
            }

            if (obj.y * tileSize  > y + yTiles * tileSize - (border + 2) * tileSize) {
                y = (int)((obj.y * tileSize * minorSize) + (border + 2) * tileSize - (yTiles * tileSize));
            }

        }



    }
}