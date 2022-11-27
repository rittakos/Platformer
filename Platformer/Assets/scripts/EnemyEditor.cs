using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;


[CustomEditor(typeof(EnemyMovement))]
[CanEditMultipleObjects]
public class EnemyEditor : Editor
{
    SerializedProperty type;
    SerializedProperty speed;
    SerializedProperty min;
    SerializedProperty max;
    SerializedProperty player;
    SerializedProperty distance;

    void OnEnable()
    {
        type = serializedObject.FindProperty("monsterType");
        speed = serializedObject.FindProperty("speed");
        min = serializedObject.FindProperty("min");
        max = serializedObject.FindProperty("max");
        player = serializedObject.FindProperty("player");
        distance = serializedObject.FindProperty("distance");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(type);
        if(type.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField(speed);
            EditorGUILayout.PropertyField(min);
            EditorGUILayout.PropertyField(max);
            
        }

        if (type.enumValueIndex == 2)
        {
            EditorGUILayout.PropertyField(player);
            EditorGUILayout.PropertyField(speed);
            EditorGUILayout.PropertyField(distance);
        }
        serializedObject.ApplyModifiedProperties();
    }
}

