

namespace test{

    public interface IGameState{

        void draw(GraphicsContext gc);
        void update();
        void keyPressed(int code);
        void keyTyped(int code);
        void keyReleased(int code);

    }
}