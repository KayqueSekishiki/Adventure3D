using System.Linq;
using UnityEditor;

//[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Player fsm = (Player)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (fsm.stateMachine == null) return;

        if (fsm.stateMachine.CurrentState != null)
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.CurrentState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Avaiable States");

        if (showFoldout)
        {
            if (fsm.stateMachine.DictionaryState != null)
            {
                var keys = fsm.stateMachine.DictionaryState.Keys.ToArray();
                var vals = fsm.stateMachine.DictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
                }
            }
        }
    }
}
