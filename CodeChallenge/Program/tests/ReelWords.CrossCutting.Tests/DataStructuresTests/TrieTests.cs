using ReelWords.CrossCutting.DataStructures;

namespace ReelWords.CrossCutting.Tests.DataStructuresTests;

public class TrieTests
{
    [Fact]
    public void When_InsertNewValueAndSearchIt_Should_ReturnTrue()
    {
        var trie = GetTrie();
        const string value = "abc";
        trie.Insert(value);

        Assert.True(trie.Search(value));
    }

    [Fact]
    public void When_InsertNullOrEmpty_Should_NotRaiseErrorAndSearchReturnFalse()
    {
        var trie = GetTrie();
        trie.Insert(null);

        Assert.False(trie.Search(null));
    }

    [Fact]
    public void When_SearchNotExistingNode_ShouldReturnFalse()
    {
        var trie = GetTrie();
        const string value = "abc";
        trie.Insert(value);

        Assert.False(trie.Search("not-existing-key"));
    }

    [Fact]
    public void When_SearchIntermediateNodeValue_Should_ReturnFalse()
    {
        var trie = GetTrie();
        const string value = "abc";
        trie.Insert(value);

        Assert.False(trie.Search("ab"));
    }

    [Fact]
    public void Should_InsertSeveralTimesSameValue()
    {
        var trie = GetTrie();
        const string value = "abc";
        trie.Insert(value);
        trie.Insert(value);
        trie.Insert(value);

        Assert.True(trie.Search(value));
    }

    [Fact]
    public void Should_InsertAndSearchCorrectlySeveralValues()
    {
        var trie = GetTrie();
        const string value1 = "abc";
        const string value2 = "abcde";
        trie.Insert(value1);
        trie.Insert(value2);

        Assert.True(trie.Search(value1));
        Assert.True(trie.Search(value2));
    }

    [Fact]
    public void When_AddingValueInsideAlreadyExistingPath_Should_InsertAndSearchCorrectly()
    {
        var trie = GetTrie();
        const string fullValue = "abcde";
        const string newValue = "abc";
        trie.Insert(fullValue);
        trie.Insert(newValue);

        Assert.True(trie.Search(fullValue));
        Assert.True(trie.Search(newValue));
        Assert.False(trie.Search("abcd"));
    }

    [Fact]
    public void When_AddingValueExtendingAlreadyExistingPath_Should_InsertAndSearchCorrectly()
    {
        var trie = GetTrie();
        const string oldSmallValue = "abc";
        const string newValue = "abcde";
        trie.Insert(oldSmallValue);
        trie.Insert(newValue);

        Assert.True(trie.Search(oldSmallValue));
        Assert.True(trie.Search(newValue));
        Assert.False(trie.Search("abcd"));
    }

    [Fact]
    public void When_Deleting_Should_RemoveFromItemFromResults()
    {
        var trie = GetTrie();
        const string value = "abcd";
        trie.Insert(value);

        Assert.True(trie.Search(value));

        trie.Delete(value);
        Assert.False(trie.Search(value));
    }

    [Fact]
    public void When_DeletingLongest_Should_KeepOtherExistingValues()
    {
        var trie = GetTrie();
        const string value1 = "abcd";
        const string value2 = "abcde";
        trie.Insert(value1);
        trie.Insert(value2);

        Assert.True(trie.Search(value1));
        Assert.True(trie.Search(value2));

        trie.Delete(value2);
        Assert.False(trie.Search(value2));
        Assert.True(trie.Search(value1));
    }

    [Fact]
    public void When_DeletingShortest_Should_KeepOtherExistingValues()
    {
        var trie = GetTrie();
        const string value1 = "abcd";
        const string value2 = "abcde";
        trie.Insert(value1);
        trie.Insert(value2);

        Assert.True(trie.Search(value1));
        Assert.True(trie.Search(value2));

        trie.Delete(value1);
        Assert.False(trie.Search(value1));
        Assert.True(trie.Search(value2));
    }

    private Trie GetTrie() => new();
}