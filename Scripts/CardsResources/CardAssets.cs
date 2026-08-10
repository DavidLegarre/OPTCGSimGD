using System.Globalization;

public class CardAssets
{
    private string RootPath { get; init; } = "res://Assets/Cards";
    public string FindCard(string cardId)
    {
        var (set, num) = SplitId(cardId);   // set/num unused
        return $"{RootPath}/{set}/{cardId}.png";
    }

    public static (string Set, string Number) SplitId(string id)
    {
        var parts = id.Split('-');
        return (parts[0], parts[1]);
    }

    public string GetCardBack(string cardType)
    {
        return $"{RootPath}/cardback.jpg";
    }
}