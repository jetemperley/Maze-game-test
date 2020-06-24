using System.Drawing;

namespace test{

    public class AssetManager{

        private static AssetManager assets = null;
        public Pen blackpen, graypen, lgraypen;
        private AssetManager(){
            blackpen = new Pen(Brushes.Black);
            graypen = new Pen(Brushes.Gray);
            lgraypen = new Pen(Brushes.LightGray);
        }

        public static AssetManager getAssetManager(){
            if (assets == null){
                assets = new AssetManager();
                return assets;
            }
            return assets;
        }
    }
}