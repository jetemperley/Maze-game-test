using System;
using System.Collections.Generic;

namespace test {

    public static class Collider {

        public static void collideXYStep (WorldObject stepper, WorldObject stator) {

            if (
                stepper.getEastXStep () > stator.getWestX () &&
                stepper.getWestXStep () < stator.getEastX () &&
                stepper.getNorthYStep () < stator.getSouthY () &&
                stepper.getSouthYStep () > stator.getNorthY ()) {

                stepper.collideXY(stator);

            }
        }

        public static void collideXYStep (WorldObject stepper, List<WorldObject> stators) {
            foreach (WorldObject stator in stators) {
                collideXYStep (stepper, stator);
            }
        }

        public static void collideXStep (WorldObject stepper, WorldObject stator) {
            if (
                stepper.getNorthY () < stator.getSouthY () &&
                stepper.getSouthY () > stator.getNorthY () &&
                stepper.getEastXStep () > stator.getWestX () &&
                stepper.getWestXStep () < stator.getEastX ()) {

                stepper.collideX(stator);
            }
        }

        public static void collideXStep (WorldObject stepper, List<WorldObject> stators) {
            foreach (WorldObject stator in stators) {
                collideXStep (stepper, stator);
            }
        }

        public static void collideYStep (WorldObject stepper, WorldObject stator) {
            if (
                stepper.getNorthYStep () < stator.getSouthY () &&
                stepper.getSouthYStep () > stator.getNorthY () &&
                stepper.getEastX () > stator.getWestX () &&
                stepper.getWestX () < stator.getEastX ()) {

                stepper.collideY(stator);
            }

        }

        public static void collideYStep (WorldObject stepper, List<WorldObject> stators) {
            foreach (WorldObject stator in stators) {
                collideYStep (stepper, stator);
            }
        }

        //  public static void collide (List<WorldObject> objs) {
        //     foreach (WorldObject obj in objs) {
        //         if (!obj.Equals (wo)) {
        //             Collider.collideXStep (wo, obj);
        //         }
        //     }
        //     foreach (WorldObject obj in objs) {
        //         if (!obj.Equals (wo)) {
        //             Collider.collideYStep (wo, obj);
        //         }
        //     }
        //     foreach (WorldObject obj in objs) {
        //         if (!obj.Equals (wo)) {
        //             Collider.collideXYStep (wo, obj);
        //         }
        //     }

        // }

        // public static void collide (List<WorldObject> objs) {
        //     // Console.WriteLine(objs.Count);
        //     for (int i = 0; i < objs.Count - 1; i++) {
        //         for (int j = i + 1; j < objs.Count; j++) {
        //             Collider.collideXStep (objs[i], objs[j]);
        //         }
        //     }
        //     for (int i = 0; i < objs.Count - 1; i++) {
        //         for (int j = i + 1; j < objs.Count; j++) {
        //             Collider.collideYStep (objs[i], objs[j]);
        //         }
        //     }
        //     for (int i = 0; i < objs.Count - 1; i++) {
        //         for (int j = i + 1; j < objs.Count; j++) {
        //             Collider.collideXYStep (objs[i], objs[j]);
        //         }
        //     }
        // }

        public static void collide (WorldObject moo, List<WorldObject> wool) {
            
                foreach (WorldObject woo in wool) {
                    collideXStep (moo, woo);
                }

            foreach (WorldObject woo in wool) {
                collideYStep (moo, woo);
            }

            foreach (WorldObject woo in wool) {
                collideXYStep (moo, woo);
            }
        }

    }

}
