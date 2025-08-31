using UnityEngine;
using UnityEditor;
using System.IO;

public class MaterialBaseMapFixer : EditorWindow
{
    [MenuItem("Planeshift Tools/Fix URP Missing BaseMaps")]
    static void FixBaseMaps()
    {
        // Find all materials in the project
        string[] materialGuids = AssetDatabase.FindAssets("t:Material");

        int fixedCount = 0;
        foreach (string guid in materialGuids)
        {
            string matPath = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);

            if (mat == null) continue;

            // Skip materials that already have a BaseMap
            if (mat.HasProperty("_BaseMap") && mat.GetTexture("_BaseMap") != null)
                continue;

            // Look for texture with same name as material
            string matName = Path.GetFileNameWithoutExtension(matPath);
            string[] texGuids = AssetDatabase.FindAssets(matName + " t:Texture");

            if (texGuids.Length > 0)
            {
                string texPath = AssetDatabase.GUIDToAssetPath(texGuids[0]);
                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(texPath);

                if (tex != null && mat.HasProperty("_BaseMap"))
                {
                    mat.SetTexture("_BaseMap", tex);
                    EditorUtility.SetDirty(mat);
                    fixedCount++;
                    Debug.Log($"Assigned {tex.name} to {mat.name}");
                }
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"✅ Fixed BaseMaps for {fixedCount} materials.");
    }
}
