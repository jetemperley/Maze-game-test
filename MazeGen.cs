using System.Collections.Generic;
using System;

namespace test {

    public static class MazeGen {

        public static int[,] gen (int x, int y) {
            if (x < 5 || y < 5) {
                return null;
            }
            int[,] grid = new int[y, x];

            x = x / 2;
            x += x % 2 - 1;
            y = y / 2;
            y += y % 2 - 1;

            Pather p = new Pather (grid, x, y);
            Pather q = new Pather (grid, x, y);

            while (!(p.complete && p.shootsComplete && q.complete && q.shootsComplete)) {
                p.step ();
                q.step ();
            }

            return grid;

        }

        

    }

    public class Pather {

            public List<int> xstack, ystack;
            public List<Pather> shoots;
            public int xPos, yPos;
            public bool complete = false, shootsComplete = true;
            public int[,] grid;

            public Pather (int[,] g, int x, int y) {
                xPos = x;
                yPos = y;
                grid = g;
                xstack = new List<int> ();
                ystack = new List<int> ();
                shoots = new List<Pather> ();
            }

            public void step () {

                if (!complete) {
                    bool wallFlag = false;

                    if (!isDead ()) {
                        int d = Rand.random (0, 4);
                        if (d == 0) { // left
                            wallFlag = isWall (xPos - 2, yPos);
                            if (wallFlag) {
                                grid[yPos,xPos - 1] = 1;
                                grid[yPos,xPos - 2] = 1;
                                randShoot ();
                                xstack.Add (xPos);
                                ystack.Add (yPos);
                                xPos -= 2;
                            }
                        } else if (d == 1) { // up
                            wallFlag = isWall (xPos, yPos - 2);
                            if (wallFlag) {
                                grid[yPos - 1,xPos] = 1;
                                grid[yPos - 2,xPos] = 1;
                                randShoot ();
                                xstack.Add (xPos);
                                ystack.Add (yPos);
                                yPos -= 2;
                            }
                        } else if (d == 2) { // right
                            wallFlag = isWall (xPos + 2, yPos);
                            if (wallFlag) {
                                grid[yPos,xPos + 1] = 1;
                                grid[yPos,xPos + 2] = 1;
                                randShoot ();
                                xstack.Add (xPos);
                                ystack.Add (yPos);
                                xPos += 2;
                            }
                        } else if (d == 3) { // d == 3 // down
                            wallFlag = isWall (xPos, yPos + 2);
                            if (wallFlag) {
                                grid[yPos + 1,xPos] = 1;
                                grid[yPos + 2,xPos] = 1;
                                randShoot ();
                                xstack.Add (xPos);
                                ystack.Add (yPos);
                                yPos += 2;
                            }
                        }
                    } else if (xstack.Count == 0) {
                        complete = true;
                    } else {
                        grid[yPos,xPos] = 1;
                        xPos = xstack [xstack.Count - 1];
                        yPos = ystack [ystack.Count - 1];
                        xstack.RemoveAt (xstack.Count - 1);
                        ystack.RemoveAt (ystack.Count - 1);
                    }
                }

                if (shoots.Count != 0) {
                    shootsComplete = false;
                    for (int i = 0; i < shoots.Count; i++) {
                        if (shoots [i].complete && shoots [i].shootsComplete) {
                            shoots.RemoveAt (i);
                            i--;
                        } else {
                            shoots [i].step ();
                        }
                    }
                } else {
                    shootsComplete = true;
                }
            }
            public int dinc (int d) {
                if (d < 3) {
                    d++;
                    return d;
                } else {
                    return 0;
                }
            }

            public bool isDead () {
                if (
                    isWall (xPos - 2, yPos) ||
                    isWall (xPos + 2, yPos) ||
                    isWall (xPos, yPos - 2) ||
                    isWall (xPos, yPos + 2)
                ) {
                    return false;
                }
                return true;
            }
            public void generateShoots () {
                while (!isDead ()) {
                    Pather p = new Pather (grid, xPos, yPos);
                    p.step ();
                    shoots.Add (p);
                }
            }

            public void randShoot () {
                int i = Rand.random (0, 100);
                if (!isDead () && i < 40) {
                    Pather p = new Pather (grid, xPos, yPos);
                    p.step ();
                    shoots.Add (p);
                }
            }

            public void generateShoot () {
                if (!isDead ()) {
                    Pather p = new Pather (grid, xPos, yPos);
                    p.step ();
                    shoots.Add (p);
                }
            }

            public bool isWall (int x, int y) {
                if (x < 0 || x >= grid.GetLength(1) || y < 0 || y >= grid.GetLength(0)) {
                    return false;
                } else if (grid[y,x] == 0) {
                    return true;
                }
                return false;
            }

        }

}