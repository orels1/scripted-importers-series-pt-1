using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.AssetImporters;
using UnityEditor.iOS;

[ScriptedImporter(1, "shadertotex")]
public class ShaderToTexture : ScriptedImporter
{
  public override void OnImportAsset(AssetImportContext ctx) {
    var code = File.ReadAllText(ctx.assetPath);
    // using the Resources folder for easy loading
    var template = Resources.Load<TextAsset>("ShaderToTex");
    var shaderCode = template.text;
    shaderCode = shaderCode.Replace("#CODE#", code);
    var shader = ShaderUtil.CreateShaderAsset(ctx, shaderCode, true);
    shader.name = "Shader";
    var finalShaderSource = new TextAsset(shaderCode);
    finalShaderSource.name = "Shader Source";
    ctx.AddObjectToAsset("Shader", shader);
    ctx.AddObjectToAsset("Shader Source", finalShaderSource);
    var buffer = new RenderTexture(2048, 2048, 24, RenderTextureFormat.ARGB32);
    var source = new Texture2D(2048, 2048, TextureFormat.ARGB32, false, false);
    var target = new Texture2D(2048, 2048, TextureFormat.ARGB32, false, false);
    var blitter = new Material(shader);
    Graphics.Blit(source, buffer, blitter);
    RenderTexture.active = buffer;
    target.ReadPixels(new Rect(0, 0, 2048, 2048), 0, 0);
    target.Apply();
    RenderTexture.active = null;
    DestroyImmediate(source);
    DestroyImmediate(buffer);
    DestroyImmediate(blitter);
    ctx.AddObjectToAsset("Texture", target);
    ctx.SetMainObject(target);
  }
}
