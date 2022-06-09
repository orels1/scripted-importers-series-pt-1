using System.IO;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

public class JsonPrimitive
{
  public string PrimitiveName;
  public Vector3 Position;
  public Vector3 Rotation;
  public Vector3 Scale;
}

[ScriptedImporter(1, "jsonprimitive")]
public class JsonPrimitiveImporter : ScriptedImporter
{
  public override void OnImportAsset(AssetImportContext ctx) {
    var info = JsonUtility.FromJson<JsonPrimitive>(File.ReadAllText(ctx.assetPath));
    var primitiveType = PrimitiveType.Cube;
    switch (info.PrimitiveName.ToLower())
    {
      case "cube":
        primitiveType = PrimitiveType.Cube;
        break;
      case "sphere":
        primitiveType = PrimitiveType.Sphere;
        break;
      case "cylinder":
        primitiveType = PrimitiveType.Cylinder;
        break;
      case "capsule":
        primitiveType = PrimitiveType.Capsule;
        break;
    }

    var primitive = GameObject.CreatePrimitive(primitiveType);
    var rotation = Quaternion.Euler(info.Rotation);
    primitive.transform.SetPositionAndRotation(info.Position, rotation);
    primitive.transform.localScale = info.Scale;
    ctx.AddObjectToAsset("Object", primitive);
    ctx.SetMainObject(primitive);
  }
}
