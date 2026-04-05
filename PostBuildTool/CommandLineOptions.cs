using RT.CommandLine;

namespace SouvenirPostBuildTool;

internal class CommandLineOptions
{
    [IsMandatory, IsPositional, Documentation("Specifies the full path and filename of SouvenirLib.dll.")]
    public string AssemblyPath = null;

    [IsMandatory, IsPositional, Documentation("Specifies the full path of the source folder.")]
    public string SourceFolder = null;

    [IsMandatory, IsPositional, Documentation("Specifies the path to the KTANE files.")]
    public string GameFolder = null;

    [Option("-c", "--contributors"), Documentation("Specifies the path and filename to the CONTRIBUTORS.md file to be updated.")]
    public string ContributorsFile = null;

    [Option("-t", "--translations"), Documentation("If specified, the translation files in this folder are updated.")]
    public bool DoTranslations = false;

    [Option("-d", "--data"), Documentation("Specifies the path and filename to the data.json file to be updated.")]
    public string DataFile = null;

    [Option("-j", "--js"), Documentation("If specified, generates the SouvenirData.js file (and its translations) used by the Souvenir manual.")]
    public string JsFile = null;

    [Option("-f", "--find-discriminators"), Documentation("If specified, the tool will list modules that do not have a discriminator and are not marked with the [NoDiscriminator] attribute.")]
    public bool FindDiscriminators = false;
}
