using System;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

[ScriptedImporter(1, "fancytext")]
public class FancyTextImporter : ScriptedImporter
{
  public override void OnImportAsset(AssetImportContext ctx)
  {
    var text = File.ReadAllText(ctx.assetPath);
    var asset = new TextAsset(text);
    ctx.AddObjectToAsset("Text", asset);
    ctx.SetMainObject(asset);
  }
}
