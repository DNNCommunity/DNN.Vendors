using System;

namespace Extensions
{
    public static class VersionExtensions
    {
        public static string DnnMajorMinorPatch(this Version version)
        {
            {
                return $"{version.Major.ToString().PadLeft(2, '0')}.{version.Minor.ToString().PadLeft(2, '0')}.{version.Build.ToString().PadLeft(2, '0')}";
            }
        }
    }
}