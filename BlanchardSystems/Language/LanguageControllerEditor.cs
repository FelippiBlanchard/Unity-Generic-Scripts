using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace BlanchardSystems.Language
{
    [CustomEditor(typeof(LanguageTextController))]

    [CanEditMultipleObjects]

    public class LanguageControllerEditor : Editor
    {

        public override void OnInspectorGUI()
        {

            var languageTexts = serializedObject.FindProperty("languageTexts");
            int sizeEnum = System.Enum.GetNames(typeof(LanguageText.Language)).Length;
            languageTexts.arraySize = sizeEnum;
            //EditorGUILayout.PropertyField(languageTexts);
            for (int i = 0; i < sizeEnum; i++)
            {
                var property = languageTexts.GetArrayElementAtIndex(i);
                var language = property.FindPropertyRelative("language");
                language.intValue = i;

                EditorGUILayout.BeginHorizontal();

                property.FindPropertyRelative("text").stringValue = EditorGUILayout.TextField(((LanguageText.Language)i).ToString(), property.FindPropertyRelative("text").stringValue);

                if (GUILayout.Button("Preview " + (LanguageText.Language)i))
                {
                    var textBox = Selection.activeGameObject.GetComponent<TextMeshProUGUI>();
                    textBox.text = property.FindPropertyRelative("text").stringValue;
                    EditorUtility.SetDirty(target);
                }
                EditorGUILayout.EndHorizontal();
            }


            serializedObject.ApplyModifiedProperties();


        }
        private string TextField(string label, string text)
        {
            float originalLabelValue = EditorGUIUtility.labelWidth;
            //float originalFieldValue = EditorGUIUtility.fieldWidth;
            EditorGUIUtility.labelWidth = 80;
            string finalText = EditorGUILayout.TextField(label, text);
            EditorGUIUtility.labelWidth = originalLabelValue;

            return finalText;
        }
    }

}