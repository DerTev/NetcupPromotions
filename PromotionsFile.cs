using System.Text.Json;

namespace NetcupPromotions;

public class PromotionsFile
{
    private readonly string _path;
    
    public List<String>? RegisteredPromotions
    {
        get => File.Exists(_path) ? 
            JsonSerializer.Deserialize<List<String>>(File.ReadAllText(_path)) 
            : null;
        set => File.WriteAllText(_path, JsonSerializer.Serialize(value));
    }

    public PromotionsFile(string path)
    {
        _path = path;
        if (!File.Exists(path)) RegisteredPromotions = new List<string>();
    }
}