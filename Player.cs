using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace test {

    public class Player : WorldObject, IControlable {

        int speed = 1;

        public Player (int x, int y) : base (x, y) {

        }

        public void keyPressed (int code) {
            //Console.WriteLine($"adding {GraphicsContext.gc.minorSize}");
            switch (code) {
                case 37:
                    
                    if (dx == 0) {
                        dx -= speed;
                    }
                    break;

                case 38:
                    
                    if (dy == 0) {
                        dy -= speed;
                    }
                    break;

                case 39:
                    
                    if (dx == 0) {
                        dx += speed;
                    }
                    break;

                case 40:
                    
                    if (dy == 0) {
                        dy += speed;
                    }
                    break;

                case 16:
                    
                    break;
            }
        }

        public void keyReleased (int code) {
            // Console.WriteLine($"di = 0, code {code}");
            switch (code) {
                case 37:
                    //dx = 0; 
                    break;

                case 38:
                    //dy = 0;
                    break;

                case 39:
                    //dx = 0;
                    break;

                case 40:
                    //dy = 0;
                    break;

                case 16:
                    
                    break;
            }
        }

        public void keyTyped (int code) { }

        
    }

}