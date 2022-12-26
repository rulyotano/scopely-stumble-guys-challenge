using ReelWords.Domain.Entities.Game;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Collections.Generic;

namespace ReelWords.Game;

public class ValidWordsProvider : IValidWordsProvider
{
    public const string ValidWordsPathKey = "ValidWordsPath";

    public async Task<ValidWords> GetValidWords()
    {
        var validWordsPath = ConfigurationManager.AppSettings.Get(ValidWordsPathKey);
        var words = new List<string>();
        using (var streamReader = new StreamReader(validWordsPath))
        {
            var word = await streamReader.ReadLineAsync();
            while (word != null)
            {
                words.Add(word);
                word = await streamReader.ReadLineAsync();
            }
        }

        return ValidWords.CreateValidWords(words);
    }
}
