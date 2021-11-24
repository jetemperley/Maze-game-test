using System;
using System.Collections.Generic;
using System.Drawing;

namespace test {
    public class World1 : IGameState {

        public List<WorldObject> movers;
        public IControlable inputFocus = null;
        public int GRID_NUMBER = 15, WALL_SIZE_T = 3, wallWidth;
        public List<WorldObject>[, ] partitions;
        GraphicsContext gc;

        public World1 () {
            gc = GraphicsContext.gc;
            wallWidth = gc.MINOR_TILES_N * WALL_SIZE_T;

            movers = new List<WorldObject> ();
            Player player = new Player (wallWidth, wallWidth);
            inputFocus = player;
            add (player);
            partitions = new List<WorldObject>[GRID_NUMBER * WALL_SIZE_T / 3, GRID_NUMBER * WALL_SIZE_T / 3];

            for (int y = 0; y < partitions.GetLength (0); y++) {
                for (int x = 0; x < partitions.GetLength (1); x++) {
                    partitions[y, x] = new List<WorldObject> ();
                }
            }

            int[, ] maze = MazeGen.gen (GRID_NUMBER, GRID_NUMBER);

            for (int y = 0; y < maze.GetLength (0); y++) {
                for (int x = 0; x < maze.GetLength (1); x++) {
                    if (maze[y, x] == 0) {
                        WorldObject wo = new WorldObject (
                            x * wallWidth, y * wallWidth,
                            wallWidth, wallWidth
                        );
                        wo.terrain = true;
                        add (wo);

                    }
                }
            }

        }

        public void add (WorldObject wo) {

            Console.WriteLine (" y " + (wo.y / (3 * gc.MINOR_TILES_N)) + " x " + (wo.x / (3 * gc.MINOR_TILES_N)));
        
            if (wo.terrain == false){
                movers.Add(wo);
            } else {
                partitions[wo.y / (3 * gc.MINOR_TILES_N), wo.x / (3 * gc.MINOR_TILES_N)].Add (wo);
            }
        }

        public void keyPressed (int code) {
            if (inputFocus != null) {
                inputFocus.keyPressed (code);
            }
        }
        public void keyTyped (int code) {
            if (inputFocus != null) {
                inputFocus.keyTyped (code);
            }
        }
        public void keyReleased (int code) {
            if (inputFocus != null) {
                inputFocus.keyReleased (code);

            }
        }

        public void update () {
            moversCollisionsV1 ();

            foreach (WorldObject moo in movers) {
                moo.update ();
            }
        }

        public void moversCollisionsV1 () {
            bool still = false;

            while (!still) {
                still = true;
                foreach (WorldObject moo in movers) {

                    int yi = (moo.y / (3 * gc.MINOR_TILES_N));
                    int xi = (moo.x / (3 * gc.MINOR_TILES_N));

                    if (moo.dx != 0) {
                        still = false;

                        for (int y = -1; y < 2; y++) {
                            for (int x = -1; x < 2; x++) {

                                if (xi + x >= 0 && yi + y >= 0 && xi + x < partitions.GetLength (1) && yi + y < partitions.GetLength (0)) {
                                    Collider.collideXStep (moo, partitions[yi + y, xi + x]);
                                }
                            }
                        }
                    }

                    if (moo.dy != 0) {
                        still = false;
                        for (int y = -1; y < 2; y++) {
                            for (int x = -1; x < 2; x++) {

                                if (xi + x >= 0 && yi + y >= 0 && xi + x < partitions.GetLength (1) && yi + y < partitions.GetLength (0)) {
                                    Collider.collideYStep (moo, partitions[yi + y, xi + x]);
                                }
                            }
                        }
                    }

                    if (moo.dx != 0 && moo.dy != 0) {


                        for (int y = -1; y < 2; y++) {
                            for (int x = -1; x < 2; x++) {

                                if (xi + x >= 0 && yi + y >= 0 && xi + x < partitions.GetLength (1) && yi + y < partitions.GetLength (0)) {
                                    Collider.collideXYStep (moo, partitions[yi + y, xi + x]);
                                }
                            }
                        }
                    }

                    moo.update();
                }

            }
        }

        public void draw (GraphicsContext gc) {

            
            gc.centerAround ((WorldObject) inputFocus);

            for (int x = 0; x < gc.xTiles; x++) {
                // for (int xm = 1; xm < gc.MINOR_TILES_N; xm++){
                //     gc.g.DrawLine(gc.assets.lgraypen, gc.tileSize*x + xm*gc.minorSize, 0, gc.tileSize*x + xm*gc.minorSize, gc.screenHeight);
                // }
                gc.g.DrawLine (gc.assets.graypen, gc.tileSize * x - gc.x % gc.tileSize, 0, gc.tileSize * x - gc.x % gc.tileSize, gc.screenHeight);
            }

            for (int y = 0; y < gc.yTiles; y++) {
                // for (int ym = 0; ym < gc.MINOR_TILES_N; ym++){
                //     gc.g.DrawLine(gc.assets.lgraypen, 0, gc.tileSize*y + ym*gc.minorSize, gc.screenWidth, gc.tileSize*y + ym*gc.minorSize);    
                // }
                gc.g.DrawLine (gc.assets.graypen, 0, gc.tileSize * y - gc.y % gc.tileSize, gc.screenWidth, gc.tileSize * y - gc.y % gc.tileSize);
            }
            int sx = (int) (gc.x / (gc.tileSize * 3));
            int sy = (int) (gc.y / (gc.tileSize * 3));
            for (int y = sy - 1; y < gc.yTiles / 3 + sy + 2; y++) {
                for (int x = sx - 1; x < gc.xTiles / 3 + sx + 2; x++) {
                    if (x >= 0 && y >= 0 && x < partitions.GetLength (1) && y < partitions.GetLength (0)) {
                        foreach (WorldObject wo in partitions[y, x]) {
                            wo.draw (gc);
                        }
                    }
                }
            }
            foreach (WorldObject moo in movers) {
                moo.draw (gc);
            }

        }

    }
}