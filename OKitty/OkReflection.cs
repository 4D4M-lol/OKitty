// Imports

using System.Reflection;

namespace OKitty;

// OkReflection

public static class OkReflection
{
    // Functions
    
    public static object? GetAttribute(object obj, string name)
    {
        if (obj is null || string.IsNullOrWhiteSpace(name))
            return null;

        Type type = obj.GetType();
        PropertyInfo? prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        
        if (prop != null)
            return prop.GetValue(obj);

        FieldInfo? field = type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        
        if (field != null)
            return field.GetValue(obj);

        MethodInfo? method = type.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        
        if (method != null)
            return method;

        return null;
    }

    public static bool SetAttribute(object obj, string name, object? value)
    {
        if (obj is null || string.IsNullOrWhiteSpace(name))
            return false;

        Type type = obj.GetType();
        PropertyInfo? prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        
        if (prop != null && prop.CanWrite)
        {
            prop.SetValue(obj, value);
            
            return true;
        }

        FieldInfo? field = type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        
        if (field != null)
        {
            field.SetValue(obj, value);
            
            return true;
        }

        return false;
    }

    public static bool HasAttribute(object obj, string name)
    {
        if (obj is null || string.IsNullOrWhiteSpace(name))
            return false;

        Type type = obj.GetType();
        
        return type.GetMember(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Any();
    }

    public static string[] Dir(object obj)
    {
        if (obj == null)
            return Array.Empty<string>();

        Type type = obj.GetType();
        
        return type
            .GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Select((MemberInfo member) => member.Name)
            .Distinct()
            .OrderBy((string name) => name)
            .ToArray();
    }

    public static Type TypeOf(object obj)
    {
        return obj?.GetType() ?? typeof(object);
    }

    public static object? Call(object obj, string name, params object[] args)
    {
        if (obj == null || string.IsNullOrWhiteSpace(name))
            return null;

        MethodInfo? method = obj.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        
        if (method == null)
            return null;

        return method.Invoke(obj, args);
    }
}