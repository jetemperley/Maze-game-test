using System;

namespace test{

    public class WorldObject{
        
        public int xs, ys, x = 0, y = 0;
        public int dx = 0, dy = 0;
        public bool terrain = false;

        public WorldObject():this(0, 0, GraphicsContext.gc.MINOR_TILES_N, GraphicsContext.gc.MINOR_TILES_N){
            
        }

        public void update(){
            //Console.WriteLine($"dx {dx} sign {Math.Sign(dx)}");
            //Console.WriteLine($"dx {dx}");
            move();
            
        }

        public WorldObject(int x1, int y1, int xs1, int ys1){
            x = x1;
            y = y1;
            xs = xs1;
            ys = ys1;
        }

        public WorldObject(int x1, int y1):this(x1, y1, GraphicsContext.gc.MINOR_TILES_N, GraphicsContext.gc.MINOR_TILES_N){
            
        }

        
        public void draw(GraphicsContext gc){
            //Console.WriteLine($"x = {x} y = {y}");
            gc.drawObject(this);
            
        }

        public int getEastXStep(){
            return x+xs + Math.Sign(dx);
        }

        public int getNorthYStep(){
            return y+ Math.Sign(dy);
        }

        public int getSouthYStep(){
            return y + ys+ Math.Sign(dy);
        }

        public int getWestXStep(){
            return x+ Math.Sign(dx);
        }
        
        public void move(){
            x += Math.Sign(dx);
            dx -= Math.Sign(dx);
            y += Math.Sign(dy);
            dy -= Math.Sign(dy);
        }

        public int getEastX(){
            return x + xs;
        }

        public int getWestX(){
            return x;
        }
        public int getNorthY(){
            return y;
        }
        public int getSouthY(){
            return y + ys;
        }
        
        public void collideX(WorldObject hit){
            dx = 0;
            onCollision(hit);
        }

        public void collideY(WorldObject hit){
            dy = 0;
            onCollision(hit);
        }
        public void collideXY(WorldObject hit){
            dx = 0;
            dy = 0;
            onCollision(hit);
        }
        
        public void onCollision(WorldObject hit){

        }
        public void interaction(){

        }
    }
}