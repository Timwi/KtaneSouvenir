using RT.CommandLine;

namespace SouvenirPostBuildTool;

internal class CommandLineOptions
{
    [IsMandatory, IsPositional, Documentation("Specifies the full path and filename of SouvenirLib.dll.")]
    public string AssemblyPath = null;

    [IsMandatory, IsPositional, Documentation("Specifies the path to the KTANE files.")]
    public string GameFolder = null;

    [Option("-c", "--contributors"), Documentation("Specifies the path and filename to the CONTRIBUTORS.md file to be updated.")]
    public string ContributorsFile = null;

    [Option("-t", "--translations"), Documentation("If specified, the translation files in this folder are updated.")]
    public string TranslationsFolder = null;

    [Option("-d", "--data"), Documentation("Specifies the path and filename to the data.json file to be updated.")]
    public string DataFile = null;
}
