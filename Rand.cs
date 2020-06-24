using System;

namespace test{

    public static class Rand{
        private static Random r = null;

        public static int random(int lower, int upper){
            if (r == null){
                r = new Random();
            }
            return r.Next(lower, upper); 
        }

    }
}