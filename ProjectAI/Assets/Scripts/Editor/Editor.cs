using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CSVReader))]
public class CSVBtn : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CSVReader csvReader = (CSVReader)target;

        if (GUILayout.Button("Read CSV"))
        {
            csvReader.AmmoReadCSV();
            csvReader.PlayerReadCSV();
            csvReader.MonsterReadCSV();
            csvReader.PriceReadCSV();
        }
    }
}
