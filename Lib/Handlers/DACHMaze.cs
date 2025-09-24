using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDACHMaze
{
    [SouvenirQuestion("Which region did you depart from in {0}?", OneColumn4Answers, "Burgenland, A", "Carinthia, A", "Lower Austria, A", "North Tyrol, A", "Upper Austria, A", "East Tyrol, A", "Salzburg, A", "Styria, A", "Vorarlberg, A", "Vienna, A", "Aargau, CH", "Appenzell Inner Rhodes, CH", "Appenzell Outer Rhodes, CH", "Basel Country, CH", "Bern, CH", "Basel City, CH", "Fribourg, CH", "Geneva, CH", "Glarus, CH", "Grisons, CH", "Jura, CH", "Luzern, CH", "Nidwalden, CH", "Neuchâtel, CH", "Obwalden, CH", "Schaffhausen, CH", "St. Gallen, CH", "Solothurn, CH", "Schwyz, CH", "Thurgau, CH", "Ticino, CH", "Uri, CH", "Vaud, CH", "Valais, CH", "Zug, CH", "Zürich, CH", "Brandenburg, D", "Berlin, D", "Baden-Württemberg, D", "Bavaria, D", "Bremen, D", "Hesse, D", "Hamburg, D", "Mecklenburg-Vorpommern, D", "Lower Saxony, D", "North Rhine-Westphalia, D", "Rhineland-Palatinate, D", "Schleswig-Holstein, D", "Saarland, D", "Saxony, D", "Saxony-Anhalt, D", "Thuringia, D", "Liechtenstein", TranslateAnswers = true)]
    Origin
}

public partial class SouvenirModule
{
    [SouvenirHandler("DACH", "DACH Maze", typeof(SDACHMaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessDACHMaze(ModuleData module) => processWorldMaze(module, "DACHMaze", SDACHMaze.Origin);
}