using System.IO;
using System.Text.RegularExpressions;

namespace CohabitStoreApp
{
    class Program
    {
        private static string _buildConfig;
        private static string _releaseAppName;
        private static string _debugAppName;
        private static string _releaseBgColor;
        private static string _projDir;
        private const string StoreassociationXmlFileName = "Package.StoreAssociation.xml";
        private const string AppxmanifestFileName = "Package.appxmanifest";
        private const string LocaldevPostFix = "LOCALDEV";

        static void Main(string[] args)
        {
            _buildConfig = args[0];
            _releaseAppName = args[1];
            _projDir = args[2];
            _releaseBgColor = args[3];

            _debugAppName = _releaseAppName + LocaldevPostFix;


            var isCurrentlySetToLocalDev = CheckIfFilesAlreadySetToLocalDevVersion();

            if (_buildConfig.ToUpperInvariant() == "DEBUG" && !isCurrentlySetToLocalDev)
            {
                ReplaceFirstOccurenceInFile(AppxmanifestFileName, _releaseAppName, _debugAppName);
                ReplaceFirstOccurenceInFile(StoreassociationXmlFileName, _releaseAppName, _debugAppName);

                ReplaceAppBackgroundColor(_releaseBgColor, "FF0000");
            }
            else if (_buildConfig.ToUpperInvariant() != "DEBUG" && isCurrentlySetToLocalDev) // release
            {
                ReplaceFirstOccurenceInFile(AppxmanifestFileName, _debugAppName, _releaseAppName);
                ReplaceFirstOccurenceInFile(StoreassociationXmlFileName, _debugAppName, _releaseAppName);

                ReplaceAppBackgroundColor("FF0000", _releaseBgColor);
            }
        }

        private static void ReplaceAppBackgroundColor(string orighex, string newhex)
        {
            string pattern = "BackgroundColor=\"#" + orighex + "\"";
            string replacement = "BackgroundColor=\"#" + newhex + "\"";

            var filePath = Path.Combine(_projDir, AppxmanifestFileName);

            var orig = File.ReadAllText(filePath);

            var result = orig.Replace(pattern, replacement);

            File.WriteAllText(filePath, result);
        }


        private static bool CheckIfFilesAlreadySetToLocalDevVersion()
        {
            var filePath = Path.Combine(_projDir, AppxmanifestFileName);

            var orig = File.ReadAllText(filePath);

            var filesAlreadySetToDebug = orig.Contains(LocaldevPostFix);
            return filesAlreadySetToDebug;
        }

        private static void ReplaceFirstOccurenceInFile(string filePath, string replaceThis, string withThis)
        {
            filePath = Path.Combine(_projDir, filePath);

            var orig = File.ReadAllText(filePath);

            var pattern = replaceThis;

            var replacement = withThis;

            var rgx = new Regex(pattern);

            var result = rgx.Replace(orig, replacement, 1);

            File.WriteAllText(filePath, result);
        }
    }
}
