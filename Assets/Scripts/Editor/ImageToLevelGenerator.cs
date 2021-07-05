using System.IO;
using System.Linq;
using Level3;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace Editor
{
    [EditorTool("Generate new level from an image")]
    public class ImageToLevelGenerator : EditorWindow
    {
        private FoodMatrix _matrix = new FoodMatrix();

        private const string Banana = "Banana";
        private const string Cherry = "Cherry";
        private const string Watermelon = "Watermelon";

        // Preview game area textures.
        private Texture2D[] _textures = new Texture2D[50];
        
        private Texture2D _image;

        [MenuItem("Tools/Image to Level Generator")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ImageToLevelGenerator));
        }

        private void OnGUI()
        {
            GUILayout.Label("Generate New Level from an Image", EditorStyles.boldLabel);
            
            GUILayout.Label("Please enter your image. Then if you want to preview" +
                            "the matrix, please click Preview button.\nClick Save Json File button to save new matrix json file.");
            GUILayout.Label(
                "Note: To show your custom matrix on game, you should change MatrixType on 'FoodMatrixController' to 'Image Generator' in hierarchy", EditorStyles.helpBox);
            
            _image = EditorGUILayout.ObjectField("Image", _image, typeof(Texture2D), false) as Texture2D;
            if (GUILayout.Button("Preview Level"))
            {
                PreviewLevel();
            }

            #region Texture labels

            GUILayout.BeginHorizontal();
            GUILayout.Label(_textures[0], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[1], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[2], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[3], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[4], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label(_textures[5], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[6], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[7], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[8], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[9], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label(_textures[10], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[11], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[12], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[13], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[14], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label(_textures[15], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[16], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[17], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[18], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[19], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label(_textures[20], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[21], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[22], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[23], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[24], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label(_textures[25], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[26], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[27], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[28], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label(_textures[29], GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.EndHorizontal();

            #endregion
            
            if (GUILayout.Button("Save Json File"))
            {
#if UNITY_EDITOR
                SaveJsonData(JsonUtility.ToJson(_matrix), "imageGeneratorMatrix.json");
                Debug.Log("Saving completed. Please press 'ALT + TAB' combination on Unity.");
#endif
            }
        }

        /// <summary>
        /// Preview level according to closest colors.
        /// </summary>
        private void PreviewLevel()
        {
            _matrix.matrix = new int[30];

            int heightIndex = _image.height / 30;
            int widthIndex = _image.width / 30;
            
            for (int multiplier = 0; multiplier < 30; multiplier++)
            {
                var pixelColor = _image.GetPixel(heightIndex * multiplier, widthIndex * multiplier);
                
                // Calculate minimum distance to get closest color
                (float, int)[] distances = new (float, int)[3];
                distances[0] = (Vector4.Distance(pixelColor, new Vector4(255, 0, 0, 1)), 0);
                distances[1] = (Vector4.Distance(pixelColor, new Vector4(255, 242, 0, 1)), 1);
                distances[2] = (Vector4.Distance(pixelColor,new Vector4(0, 255, 0, 1)), 2);
                
                var value = distances.Min().Item2;
                
                // Assign correct value for each unit of matrix.
                _matrix.matrix[multiplier] = value;
                
                // Assign texture according to value.
                _textures[multiplier] = GetPreviewTexture(value);
            }
        }
        
        /// <summary>
        /// Get preview texture according to food value.
        /// </summary>
        private Texture2D GetPreviewTexture(int value)
        {
            Texture2D result = null;
            switch (value)
            {
                case 0:
                    result = AssetPreview.GetAssetPreview(Resources.Load(Cherry));
                    break;
                case 1:
                    result = AssetPreview.GetAssetPreview(Resources.Load(Banana));
                    break;
                case 2:
                    result = AssetPreview.GetAssetPreview(Resources.Load(Watermelon));
                    break;
            }

            return result;
        }
        
        /// <summary>
        /// Save json data to resources.
        /// </summary>
        public static void SaveJsonData(string data, string fileName)
        {
            FileStream fStream = new FileStream($"Assets/Resources/{fileName}", FileMode.Create,
                FileAccess.Write,
                FileShare.None);
            StreamWriter writer = new StreamWriter(fStream);
            writer.Write(data);

            writer.Close();
            fStream.Close();
        }
        
    }
}
