public class CardAssets
{
    private string RootPath { get; init; } = "res://Assets/Cards";
    public string FindCard(string cardId)
    {
        var set = cardId.Split('-')[0];
        return $"{RootPath}/{set}/{cardId}.png";
    }

    public string GetCardBack()
    {
        return $"{RootPath}/cardback.jpg";
    }
}