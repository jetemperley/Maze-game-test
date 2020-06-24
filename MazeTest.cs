
using System.Windows.Forms;


namespace test {
    public class MazeTest{

        static void Main () {

            Application.EnableVisualStyles ();
            Application.SetCompatibleTextRenderingDefault (false);
            Form form = new Form ();
            GamePanel panel = new GamePanel();
            form.Controls.Add(panel);
            form.ActiveControl = panel;
            form.AutoSize = true;
            Application.Run (form);

        }

    }

}