using UnityEditor;
using UnityEditor.SceneManagement;

namespace Assets._Project.Develop.Editor
{
    //L4 - Стартовая подготовка. Запускаем EntryPoint с любой сцены 
    //ссылка на документацию docs.unity3d.com/2023.2/Documentation/Manual/RunningEditorCodeOnLaunch.html
    [InitializeOnLoad]
    public static class EntryPointSceneAutoLoader
    {
        static EntryPointSceneAutoLoader()
        {
            if (EditorBuildSettings.scenes.Length == 0)
                return;

            EditorSceneManager.playModeStartScene = AssetDatabase
                .LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
        }
    }
}
