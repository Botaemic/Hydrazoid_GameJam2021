using Hydrazoid.Extensions;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hydrazoid.SceneManagement
{
    [CustomEditor(typeof(GameSceneSO), true)]
    public class GameSceneEditor : ExtendedEditor
    {
		private SerializedProperty _name;
		private SerializedProperty _description;
		private SerializedProperty _sceneName;
		private SerializedProperty _sprite;
		private string[] _sceneList;

		private const string _noScenesWarning = "There are no scenes set for this level yet! Add a new scene with the dropdown below";
		private GUIStyle _headerLabelStyle;

		private void OnEnable()
		{
			_name = serializedObject.FindProperty("_name");
			_description = serializedObject.FindProperty("_description");
			_sceneName = serializedObject.FindProperty("_sceneName");
			_sprite = serializedObject.FindProperty("_sprite");

			PopulateScenePicker();
			InitializeGuiStyles();
		}

		public override void OnInspectorGUI()
		{
			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.LabelField("Scene information", _headerLabelStyle);
			EditorGUILayout.Space();
			DrawProperty(_name);
			DrawScenePicker();
			DrawProperty(_description);
			DrawProperty(_sprite);
			serializedObject.ApplyModifiedProperties();
		}

		private void DrawScenePicker()
		{
			string sceneName = _sceneName.stringValue;
			EditorGUI.BeginChangeCheck();
			int selectedScene = _sceneList.ToList().IndexOf(sceneName);

			if (selectedScene < 0)
			{
				EditorGUILayout.HelpBox(_noScenesWarning, MessageType.Warning);
			}

			selectedScene = EditorGUILayout.Popup("Scene", selectedScene, _sceneList);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Changed selected scene");
				_sceneName.stringValue = _sceneList[selectedScene];
				MarkAllDirty();
			}
		}

		private void InitializeGuiStyles()
		{
			_headerLabelStyle = new GUIStyle(EditorStyles.largeLabel)
			{
				fontStyle = FontStyle.Bold,
				fontSize = 18,
				fixedHeight = 70.0f
			};
		}

		private void PopulateScenePicker()
		{
			var sceneCount = SceneManager.sceneCountInBuildSettings;
			_sceneList = new string[sceneCount];
			for (int i = 0; i < sceneCount; i++)
			{
				_sceneList[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
			}
		}

		private void MarkAllDirty()
		{
			EditorUtility.SetDirty(target);
			EditorSceneManager.MarkAllScenesDirty();
		}
	}
}