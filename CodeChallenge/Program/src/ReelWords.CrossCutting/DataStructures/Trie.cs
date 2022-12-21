using System.Collections.Generic;

namespace ReelWords.CrossCutting.DataStructures;

public class Trie
{
    private readonly char? _key;
    private readonly IDictionary<char, Trie> _children;
    private readonly Trie _parent;
    private bool _isMatch;

    public Trie()
    {
        _key = null;
        _children = new Dictionary<char, Trie>();
        _isMatch = false;
        _parent = null;
    }

    private Trie(char key, Trie parent) : this()
    {
        _key = key;
        _parent = parent;
    }

    public bool Search(string searchValue) => GetNode(this, searchValue)?._isMatch == true;

    public void Insert(string newValue)
    {
        if (string.IsNullOrEmpty(newValue)) return;
        int currentIndex = 0;
        var current = this;

        while (currentIndex < newValue.Length)
        {
            var currentCharacter = newValue[currentIndex];
            if (!current._children.TryGetValue(currentCharacter, out var child))
            {
                child = GetChildNode(currentCharacter, current);
                current._children.Add(currentCharacter, child);
            }
            current = child;
            currentIndex++;
        }

        current.SetMatch();
    }

    public void Delete(string removeValue)
    {
        var trie = GetNode(this, removeValue);
        if (trie == null) return;
        trie.RemoveMatch();
        RemoveEmptyNodes(trie);
    }

    private bool IsLeaf => _children.Count == 0;

    private void SetMatch() => _isMatch = true;

    private void RemoveMatch() => _isMatch = false;

    private static Trie GetChildNode(char key, Trie parent) => new (key, parent);

    private static Trie GetNode(Trie root, string value)
    {
        if (string.IsNullOrEmpty(value)) return null;

        var currentIndex = 0;
        var current = root;

        while (currentIndex < value.Length)
        {
            var currentCharacter = value[currentIndex];
            if (!current._children.TryGetValue(currentCharacter, out var child))
            {
                return null;
            }

            current = child;
            currentIndex++;
        }

        return current;
    }

    private static void RemoveEmptyNodes(Trie trie)
    {
        var current = trie;
        var parent = current._parent;

        while (parent != null)
        {
            if (!current.IsLeaf || current._isMatch) break;
            parent._children.Remove(current._key.Value);
            current = parent;
            parent = current._parent;
        }
    }
}