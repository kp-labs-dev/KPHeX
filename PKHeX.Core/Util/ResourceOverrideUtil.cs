using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PKHeX.Core;

public sealed class ResourceOverrideUtil
{
    public const string ENV_RESOURCE_OVERRIDE_PREFIX = "PKHEX_RESOURCE_OVERRIDE";
    /// <summary>
    /// Fetches a string resource from this assembly.
    /// </summary>
    public static bool TryGetOverrideStream(string name, [NotNullWhen(true)] out Stream? result)
    {
        result = null;
        if (!TryGetOverridePath(name, out string? path))
            return false;

        if (!File.Exists(path))
            return false;

        result = new FileStream(path, FileMode.Open);
        return true;
    }

    public static string GetEnvVarName(string resourceName)
    {
        return $"{ENV_RESOURCE_OVERRIDE_PREFIX}_{resourceName}";
    }

    static bool TryGetOverridePath(string name, [NotNullWhen(true)] out string? result)
    {
        result = Environment.GetEnvironmentVariable(GetEnvVarName(name));
        return result != null;
    }
}
