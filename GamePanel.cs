using System;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace test {
    public class GamePanel : Panel {

        // the bool states correspond to the generic c# keyvalues indicated by the keyIndex.
        // that is keyIndex[0] = 37, therefor pressed[0] corresponds to e.KeyValue 37
        bool[] pressed, typed, released;
        int[] keyIndex;

        System.Timers.Timer timer;
        IGameState[] state;
        int currentState = 0;
        GraphicsContext gc;
        readonly int SX = 500, SY = 500, FRAME_TIME = 25;
        bool play = true;

        public GamePanel () {

            gc = new GraphicsContext (SX, SY, 30);
            
            this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //Setup events that listens on keypress
            this.KeyDown += keyDown;
            this.KeyUp += keyUp;
            //this.BackColor = Color.FromArgb (255, 175, 175, 175);
            this.Size = new Size(SX, SY);
            
            timer = new System.Timers.Timer (FRAME_TIME);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += frame;

            pressed = new bool[5];
            typed = new bool[5];
            released = new bool[5];
            keyIndex = new int[5] {37, 38, 39, 40, 16};

            for (int i = 0; i < pressed.Length; i++) {
                pressed[i] = false;
                typed[i] = false;
                released[i] = false;
            }

            state = new IGameState[2];
            state[0] = new World1 ();
        }

        private void frame (Object source, ElapsedEventArgs e) {
            
            if (play) {
                
                
                
                for (int i = 0; i < pressed.Length; i++) {
                    if (pressed[i]) {
                        Console.WriteLine("press");
                        state[currentState].keyPressed (keyIndex[i]);
                    }

                    if (released[i]) {
                        state[currentState].keyReleased (keyIndex[i]);
                        released[i] = false;
                        
                    }
                    
                }
                state[currentState].update ();
                this.Invalidate ();
            }
        }

        protected override void OnPaint (PaintEventArgs e) {
            // Console.WriteLine("paint");

            base.OnPaint (e);
            gc.g.Clear(Color.DarkGray);
            state[currentState].draw (gc);
            e.Graphics.DrawImage(gc.bitmap, 0, 0, gc.screenWidth, gc.screenHeight);
        }

        private void keyUp (object sender, KeyEventArgs e) {
            // Console.WriteLine ($"{e.KeyValue} released");

            for (int i = 0; i < pressed.Length; i++) {
                if (e.KeyValue == keyIndex[i]){
                    released[i] = true;
                    pressed[i] = false;
                }
            }
        }

        // Handle the KeyPress event to print the type of character entered into the control.
        private void keyDown (object sender, KeyEventArgs e) {
            // Console.WriteLine ($"{e.KeyValue} pressed");

            for (int i = 0; i < pressed.Length; i++) {
                if (e.KeyValue == keyIndex[i]){
                    pressed[i] = true;
                    
                }
            }

            // Console.WriteLine(e.KeyValue);

            if (e.KeyValue == 32) {
                play = !play;
            }

        }
    }
}