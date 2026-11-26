using System.Text.Json;

namespace DNSLab.Web.Extensions;

public static class JsonExtensions
{
    public static List<JsonDifference> CompareJson(
        this string? oldJson,
        string? newJson)
    {
        var differences = new List<JsonDifference>();

        bool oldIsNull = string.IsNullOrWhiteSpace(oldJson) ||
                         oldJson.Equals("NULL", StringComparison.OrdinalIgnoreCase);

        bool newIsNull = string.IsNullOrWhiteSpace(newJson) ||
                         newJson.Equals("NULL", StringComparison.OrdinalIgnoreCase);

        if (oldIsNull && newIsNull)
            return differences;

        if (oldIsNull)
        {
            differences.Add(new JsonDifference
            {
                Path = string.Empty,
                Operation = "Added",
                NewValue = newJson
            });

            return differences;
        }

        if (newIsNull)
        {
            differences.Add(new JsonDifference
            {
                Path = string.Empty,
                Operation = "Removed",
                OldValue = oldJson
            });

            return differences;
        }

        using var oldDoc = JsonDocument.Parse(oldJson!);
        using var newDoc = JsonDocument.Parse(newJson!);

        CompareElements(
            oldDoc.RootElement,
            newDoc.RootElement,
            string.Empty,
            differences);

        return differences;
    }

    private static void CompareElements(
        JsonElement oldElement,
        JsonElement newElement,
        string path,
        List<JsonDifference> differences)
    {
        if (oldElement.ValueKind != newElement.ValueKind)
        {
            Add(path, oldElement, newElement, differences);
            return;
        }

        switch (oldElement.ValueKind)
        {
            case JsonValueKind.Object:
                {
                    var oldProperties = oldElement.EnumerateObject()
                        .ToDictionary(x => x.Name, x => x.Value);

                    var newProperties = newElement.EnumerateObject()
                        .ToDictionary(x => x.Name, x => x.Value);

                    foreach (var property in oldProperties)
                    {
                        var propertyPath = string.IsNullOrEmpty(path)
                            ? property.Key
                            : $"{path}.{property.Key}";

                        if (!newProperties.TryGetValue(property.Key, out var newValue))
                        {
                            differences.Add(new JsonDifference
                            {
                                Path = propertyPath,
                                Operation = "Removed",
                                OldValue = property.Value.GetRawText()
                            });
                        }
                        else
                        {
                            CompareElements(
                                property.Value,
                                newValue,
                                propertyPath,
                                differences);
                        }
                    }

                    foreach (var property in newProperties)
                    {
                        if (!oldProperties.ContainsKey(property.Key))
                        {
                            var propertyPath = string.IsNullOrEmpty(path)
                                ? property.Key
                                : $"{path}.{property.Key}";

                            differences.Add(new JsonDifference
                            {
                                Path = propertyPath,
                                Operation = "Added",
                                NewValue = property.Value.GetRawText()
                            });
                        }
                    }

                    break;
                }

            case JsonValueKind.Array:
                {
                    int oldLength = oldElement.GetArrayLength();
                    int newLength = newElement.GetArrayLength();

                    for (int i = 0; i < Math.Max(oldLength, newLength); i++)
                    {
                        if (i >= oldLength)
                        {
                            differences.Add(new JsonDifference
                            {
                                Path = $"{path}[{i}]",
                                Operation = "Added",
                                NewValue = newElement[i].GetRawText()
                            });
                        }
                        else if (i >= newLength)
                        {
                            differences.Add(new JsonDifference
                            {
                                Path = $"{path}[{i}]",
                                Operation = "Removed",
                                OldValue = oldElement[i].GetRawText()
                            });
                        }
                        else
                        {
                            CompareElements(
                                oldElement[i],
                                newElement[i],
                                $"{path}[{i}]",
                                differences);
                        }
                    }

                    break;
                }

            default:
                {
                    if (oldElement.GetRawText() != newElement.GetRawText())
                    {
                        Add(path, oldElement, newElement, differences);
                    }

                    break;
                }
        }
    }

    private static void Add(
        string path,
        JsonElement oldElement,
        JsonElement newElement,
        List<JsonDifference> differences)
    {
        differences.Add(new JsonDifference
        {
            Path = path,
            Operation = "Changed",
            OldValue = oldElement.GetRawText(),
            NewValue = newElement.GetRawText()
        });
    }
}

public class JsonDifference
{
    public string Path { get; set; } = null!;
    public string Operation { get; set; } = null!;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
}