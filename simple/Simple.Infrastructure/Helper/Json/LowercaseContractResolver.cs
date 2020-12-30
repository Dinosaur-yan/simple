using Newtonsoft.Json.Serialization;

public class LowercaseContractResolver : DefaultContractResolver
{
    protected override string ResolvePropertyName(string propertyName)
    {
        return propertyName.ToLower();
    }

    protected override string ResolveDictionaryKey(string dictionaryKey)
    {
        return dictionaryKey.ToLower();
    }
}
