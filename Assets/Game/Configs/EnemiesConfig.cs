using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace VNEagleEngine
{
    [CreateAssetMenu(fileName = "Enemies Data", menuName = "Configs/Enemies Config", order = 1)]
    public class EnemiesConfig : ScriptableObject
    {
        public List<EnemyData> enemies;

        public EnemyData GetEnemyData(EnemyName enemyName)
        {
            return enemies.FirstOrDefault((enemy) => enemy.enemyName == enemyName);
        }

        public void UpdateEnemiesId()
        {
            foreach (EnemyData enemyData in enemies)
            {
                enemyData.UpdateId();
            }
        }
    }

    // [System.Serializable]
    // public class EnemyDataDictionary : SerializableDictionary<string, EnemyData>{ }

    [System.Serializable]
    public class EnemyData
    {
        public string enemyId = "";
        public EnemyName enemyName;
        public EnemyType enemyType;

        public void UpdateId()
        {
            enemyId = string.Format("{0}_{1}", enemyType, enemyName);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(EnemiesConfig))]
    public class CustomEditorEnemiesConfig : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EnemiesConfig enemiesConfig = target as EnemiesConfig;

            if (GUILayout.Button("Load Enemies Id"))
            {
                enemiesConfig.UpdateEnemiesId();
            }
        }
    }
#endif

    public enum EnemyType
    {
        Dragon,
        Human,
        Elf
    }

    public enum EnemyName
    {
        Circle,
        Square,
        DarkNight
    }
}
