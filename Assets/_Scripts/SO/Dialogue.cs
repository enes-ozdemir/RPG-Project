using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Scripts.SO
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Game/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public List<string> dialogs = new List<string>();
        
        public string GetRandomDialog()
        {
            return dialogs[Random.Range(0, dialogs.Count)];
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Dialogue))]
    public class DialogueEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            Dialogue dialogue = (Dialogue)target;
            if (GUILayout.Button("Generate Random NPC Dialogs"))
            {
                GenerateRandomNPCDialogs(dialogue);
            }
        }

        private void GenerateRandomNPCDialogs(Dialogue dialogue)
        {
            dialogue.dialogs.Clear();
            dialogue.dialogs.Add("Merhaba, maceracı dostum!");
            dialogue.dialogs.Add("Bu yolculukta sana yardımcı olabilirim.");
            dialogue.dialogs.Add("Canavarlarla savaşırken dikkatli ol!");
            dialogue.dialogs.Add("Eşyalarım gerçekten özel, bir göz atmak ister misin?");
            dialogue.dialogs.Add("Macera başlamadan önce biraz dinlenmek iyi olur.");
            dialogue.dialogs.Add("Hayat bir savaş gibidir, her düşüş yeni bir başlangıçtır.");
            dialogue.dialogs.Add("Hedeflerine odaklan ve asla vazgeçme!");
            dialogue.dialogs.Add("Güçlü olmak için sürekli eğitim yapmalısın.");
            dialogue.dialogs.Add("Arkadaşlarınla birlikte daha güçlü olabilirsin.");
            dialogue.dialogs.Add("Maceraya atılmadan önce biraz yiyecek almalısın.");

            EditorUtility.SetDirty(dialogue);
            AssetDatabase.SaveAssets();
        }
    }
#endif
}