using System;
using System.Collections.Generic;

namespace Souvenir;

public class Translation_de : TranslationBase<Translation_de.TranslationInfo_de>
{
    public sealed class TranslationInfo_de : TranslationInfo
    {
        public Gender Gender = Gender.Neuter;
        public string ModuleNameDative;
        public string ModuleNameWithThe;
    }

    public enum Gender
    {
        Masculine,
        Feminine,
        Neuter,
        Plural
    }

    public override string FormatModuleName(SouvenirHandlerAttribute handler, bool addSolveCount, int numSolved) => addSolveCount
        ? (_translations.Get(handler.EnumType)?.Gender ?? Gender.Neuter) switch
        {
            Gender.Feminine => $"der als {ordinal(numSolved)}e gelösten {_translations.Get(handler.EnumType)?.ModuleNameDative ?? _translations.Get(handler.EnumType)?.ModuleName ?? handler.ModuleName}",
            Gender.Masculine => $"dem als {ordinal(numSolved)}en gelösten {_translations.Get(handler.EnumType)?.ModuleNameDative ?? _translations.Get(handler.EnumType)?.ModuleName ?? handler.ModuleName}",
            Gender.Neuter => $"dem als {ordinal(numSolved)}es gelösten {_translations.Get(handler.EnumType)?.ModuleNameDative ?? _translations.Get(handler.EnumType)?.ModuleName ?? handler.ModuleName}",
            _ => /* Plural */ $"den als {ordinal(numSolved)}e gelösten {_translations.Get(handler.EnumType)?.ModuleNameDative ?? _translations.Get(handler.EnumType)?.ModuleName ?? handler.ModuleName}",
        }
        : _translations.Get(handler.EnumType)?.ModuleNameWithThe ?? _translations.Get(handler.EnumType)?.ModuleName ?? handler.ModuleNameWithThe;

    public override string Ordinal(int number) => ordinal(number);
    private string ordinal(int num) => num < 0 ? $"({num})t" : num switch
    {
        1 => "erst",
        2 => "zweit",
        3 => "dritt",
        4 => "viert",
        5 => "fünft",
        6 => "sechst",
        7 => "siebt",
        8 => "acht",
        9 => "neunt",
        10 => "zehnt",
        11 => "elft",
        12 => "zwölft",
        _ => $"{num}t"
    };

    protected override Dictionary<Type, TranslationInfo_de> _translations => new()
    {
        #region Translatable strings
        [typeof(SAbyss)] = new()
        {
            Questions = new()
            {
                [SAbyss.Seed] = new()
                {
                    Question = "Welcher Buchstabe wurde bei {0} als {1}es angezeigt?"
                }
            }
        },

        [typeof(SPentabutton)] = new()
        {
            Gender = Gender.Masculine,
            ModuleName = "Pentaknopf",
            Questions = new()
            {
                [SPentabutton.BaseColor] = new()
                {
                    Question = "Was war bei {0} die Basisfarbe?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                        ["White"] = "Weiß",
                    },
                }
            },
            Discriminators = new()
            {
                [SPentabutton.Label] = new()
                {
                    Discriminator = "dem Pentaknopf mit der Aufschrift “{0}”"
                }
            }
        }

        #endregion
    };

    public override string[] IntroTexts => Ut.NewArray(
        "Was geht ab, Alter?",
        "Ey, du kommst hier net rein.", // Kaya Yanar
        "Was guckst du?!",  // Kaya Yanar
        "Joa... also hallo erstmal...",  // Rüdiger Hoffmann
        "Meine Herren, das geht alles von Ihrer Zeit ab.",   // Piet Klocke
        "Die Bombe ist dem Entschärfer sein Tod.",   // „Der Dativ ist dem Genitiv sein Tod“, Bastian Sick
        "Was nicht explodiert, wird explodierend gemacht.", // „Was nicht passt, wird passend gemacht“ (Film, 2002)
        "Das merkwürdige Verhalten explosionsreifer Bomben zur Detonationszeit", // „Das merkwürdige Verhalten geschlechtsreifer Großstädter zur Paarungszeit“ (Film, 1998)
        "Die fetten Entschärfungen sind vorbei."   // „Die fetten Jahre sind vorbei“ (Film, 2004)
    );
}
