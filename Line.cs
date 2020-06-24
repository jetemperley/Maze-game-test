using System;

namespace test {
    public class Line {

        // line elements
        // m = gradient, b = y intercept, a = x intercept
        public float m, b, a, upperX, lowerX, upperY, lowerY;
        public float x1, y1, x2, y2;
        public bool hori = false, vert = false;

        // generate line by points
        public Line (float x1_, float y1_, float x2_, float y2_) {

            x1 = x1_;
            y1 = y1_;
            x2 = x2_;
            y2 = y2_;

            if (x1 > x2) {
                upperX = x1;
                lowerX = x2;
            } else {
                upperX = x2;
                lowerX = x1;
            }
            if (y1 > y2) {
                upperY = y1;
                lowerY = y2;
            } else {
                upperY = y2;
                lowerY = y1;
            }

            if (x1_ == x2_ && y1_ == y2_) {

            } else if (y1_ == y2_) {
                m = 0.001F;
                b = y1_;
                hori = true;

            } else if (x1_ == x2_) {
                m = 999;
                a = x1_;
                vert = true;

            } else {
                m = (y1_ - y2_) / (x1_ - x2_);
                b = y1_ - m * x1_;
                a = -b / m;
                
            }

        }

        public float[] checkCollision (Line l1) {
            return Line.checkCollision (this, l1);
        }

        public static float[] checkCollision (Line line1, Line line2) {

            float[] col = new float[2];
            if (line1.vert && line2.hori){
                col[0] = line1.x1;
                col[1] = line2.y1;
            } else if (line1.hori && line2.vert){
                col[1] = line1.y1;
                col[0] = line2.x1;
            } else if (line1.m - line2.m != 0) {
                
                col[0] = (line2.b - line1.b) / (line1.m - line2.m);
                col[1] = col[0] * line2.m + line2.b;

            } else {
                col = null;
            }

            if (col != null){
                if ((col[0] > line2.upperX || col[0] < line2.lowerX) || (col[1] > line2.upperY || col[1] < line2.lowerY)) {
                    col = null;

                } else if ((col[0] > line1.upperX || col[0] < line1.lowerX) || (col[1] > line1.upperY || col[1] < line1.lowerY)) {
                    col = null;
                }
            }

            return col;
        }

    }
}