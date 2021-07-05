using System.IO;
using Level3;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace Editor
{
    [EditorTool("CreateNewLevel")]
    public class LevelEditor : EditorWindow
    {
        private string _matrixJson;

        private const string Banana = "Banana";
        private const string Cherry = "Cherry";
        private const string Watermelon = "Watermelon";

        private Texture2D[] _textures = new Texture2D[50];

        [MenuItem("Tools/Level Editor")]
        public static void ShowWindow()
        {
            GetWindow(typeof(LevelEditor));
        }
        
        private void OnGUI()
        {
            GUILayout.Label("Create New Level", EditorStyles.boldLabel);
            
            GUILayout.Label("Please enter your custom 6x5 matrix as json file. Then if you want to preview" +
                            "the matrix, please click Preview button.\nClick Save Json File button to save new matrix json file.");
            GUILayout.Label(
                "Note: To show your custom matrix on game, you should change MatrixType on 'FoodMatrixController' to 'Level Editor' in hierarchy", EditorStyles.helpBox);

            _matrixJson = EditorGUILayout.TextField("Matrix Json", _matrixJson);

            if (GUILayout.Button("Preview Level"))
            {
                PreviewLevel();
            }

            #region Texture Labels

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
                SaveJsonData(_matrixJson, "levelEditorMatrix.json");
                Debug.Log("Saving completed. Please press 'ALT + TAB' combination on Unity.");
#endif
            }

        }
        
        /// <summary>
        /// Preview level given matrix.
        /// </summary>
        public void PreviewLevel()
        {
            FoodMatrix matrix = JsonUtility.FromJson<FoodMatrix>(_matrixJson);

            for (int ind = 0; ind < matrix.matrix.Length; ind++)
            {
                _textures[ind] = GetPreviewTexture(matrix.matrix[ind]);
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
