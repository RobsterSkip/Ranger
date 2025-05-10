using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Items : ScriptableObject
{
    [SerializeField]
    private string _id;
    public string _ID { get { return _id; } }
    [SerializeField]
    private string _itemName;
    public Sprite _itemIcon;

    [SerializeField] private GameObject _itemPrefab;
    public GameObject ItemPrefab => _itemPrefab;
    [SerializeField] private string _journalEntry;
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        _id = AssetDatabase.AssetPathToGUID(path);
    }
}
