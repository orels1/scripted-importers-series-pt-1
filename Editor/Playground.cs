using System.IO;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace {
  public class Playground : EditorWindow {
    [MenuItem("Tools/Playground")]
    private static void ShowWindow() {
      var window = GetWindow<Playground>();
      window.titleContent = new GUIContent("Playground");
      window.Show();
    }

    private void OnGUI() {
      if (GUILayout.Button("Create file")) {
        var file = new JsonPrimitive {
          Position = new Vector3(0, 1, 2),
          Rotation = new Vector3(3, 4, 5),
          Scale = new Vector3(6, 7, 8),
          PrimitiveName = "cube"
        };
        var path = Application.dataPath + "/test.json";
        var jsonString = JsonUtility.ToJson(file, true);
        File.WriteAllText(path, jsonString);
      }
    }
  }
}
