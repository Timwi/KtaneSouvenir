using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGhostMovement
{
    [SouvenirQuestion("Where was {1} in {0}?", ThreeColumns6Answers, "A1", "B1", "C1", "D1", "E1", "F1", "G1", "H1", "I1", "J1", "K1", "L1", "O1", "P1", "Q1", "R1", "S1", "T1", "U1", "V1", "W1", "X1", "Y1", "Z1", "A2", "F2", "L2", "O2", "U2", "Z2", "A3", "F3", "L3", "O3", "U3", "Z3", "A4", "F4", "L4", "O4", "U4", "Z4", "A5", "B5", "C5", "D5", "E5", "F5", "G5", "H5", "I5", "J5", "K5", "L5", "M5", "N5", "O5", "P5", "Q5", "R5", "S5", "T5", "U5", "V5", "W5", "X5", "Y5", "Z5", "A6", "F6", "I6", "R6", "U6", "Z6", "A7", "F7", "I7", "R7", "U7", "Z7", "A8", "B8", "C8", "D8", "E8", "F8", "I8", "J8", "K8", "L8", "O8", "P8", "Q8", "R8", "U8", "V8", "W8", "X8", "Y8", "Z8", "F9", "L9", "O9", "U9", "F10", "L10", "O10", "U10", "F11", "I11", "J11", "K11", "L11", "M11", "N11", "O11", "P11", "Q11", "R11", "U11", "F12", "I12", "R12", "U12", "F13", "I13", "R13", "U13", "4514", "A14", "B14", "C14", "D14", "E14", "F14", "G14", "H14", "I14", "R14", "S14", "T14", "U14", "V14", "W14", "X14", "Y14", "Z14", "a14", "F15", "I15", "R15", "U15", "F16", "I16", "R16", "U16", "F17", "I17", "J17", "K17", "L17", "M17", "N17", "O17", "P17", "Q17", "R17", "U17", "F18", "I18", "R18", "U18", "F19", "I19", "R19", "U19", "A20", "B20", "C20", "D20", "E20", "F20", "G20", "H20", "I20", "J20", "K20", "L20", "O20", "P20", "Q20", "R20", "S20", "T20", "U20", "V20", "W20", "X20", "Y20", "Z20", "A21", "F21", "L21", "O21", "U21", "Z21", "A22", "F22", "L22", "O22", "U22", "Z22", "A23", "B23", "C23", "F23", "G23", "H23", "I23", "J23", "K23", "L23", "M23", "N23", "O23", "P23", "Q23", "R23", "S23", "T23", "U23", "X23", "Y23", "Z23", "C24", "F24", "I24", "R24", "U24", "X24", "C25", "F25", "I25", "R25", "U25", "X25", "A26", "B26", "C26", "D26", "E26", "F26", "I26", "J26", "K26", "L26", "O26", "P26", "Q26", "R26", "U26", "V26", "W26", "X26", "Y26", "Z26", "A27", "L27", "O27", "Z27", "A28", "L28", "O28", "Z28", "A29", "B29", "C29", "D29", "E29", "F29", "G29", "H29", "I29", "J29", "K29", "L29", "M29", "N29", "O29", "P29", "Q29", "R29", "S29", "T29", "U29", "V29", "W29", "X29", "Y29", "Z29", Arguments = ["Inky", "Blinky", "Pinky", "Clyde", "Pac-Man"], ArgumentGroupSize = 1)]
    Position
}

public partial class SouvenirModule
{
    [SouvenirHandler("ghostMovement", "Ghost Movement", typeof(SGhostMovement), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessGhostMovement(ModuleData module)
    {
        var comp = GetComponent(module, "ghostMovementScript");
        yield return WaitForSolve;

        var screens = GetArrayField<TextMesh>(comp, "Screens", isPublic: true).Get(expectedLength: 5);
        foreach (var s in screens)
            s.text = "";

        var validPositions = new[] { 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 57, 62, 68, 71, 77, 82, 85, 90, 96, 99, 105, 110, 113, 118, 124, 127, 133, 138, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 169, 174, 177, 186, 189, 194, 197, 202, 205, 214, 217, 222, 225, 226, 227, 228, 229, 230, 233, 234, 235, 236, 239, 240, 241, 242, 245, 246, 247, 248, 249, 250, 258, 264, 267, 273, 286, 292, 295, 301, 314, 317, 318, 319, 320, 321, 322, 323, 324, 325, 326, 329, 342, 345, 354, 357, 370, 373, 382, 385, 392, 393, 394, 395, 396, 397, 398, 399, 400, 401, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 426, 429, 438, 441, 454, 457, 466, 469, 482, 485, 486, 487, 488, 489, 490, 491, 492, 493, 494, 497, 510, 513, 522, 525, 538, 541, 550, 553, 561, 562, 563, 564, 565, 566, 567, 568, 569, 570, 571, 572, 575, 576, 577, 578, 579, 580, 581, 582, 583, 584, 585, 586, 589, 594, 600, 603, 609, 614, 617, 622, 628, 631, 637, 642, 645, 646, 647, 650, 651, 652, 653, 654, 655, 656, 657, 658, 659, 660, 661, 662, 663, 664, 665, 668, 669, 670, 675, 678, 681, 690, 693, 696, 703, 706, 709, 718, 721, 724, 729, 730, 731, 732, 733, 734, 737, 738, 739, 740, 743, 744, 745, 746, 749, 750, 751, 752, 753, 754, 757, 768, 771, 782, 785, 796, 799, 810, 813, 814, 815, 816, 817, 818, 819, 820, 821, 822, 823, 824, 825, 826, 827, 828, 829, 830, 831, 832, 833, 834, 835, 836, 837, 838 };
        var nameFunction = GetMethod<string>(comp, "LocationName", 1);
        var combos = new (string name, string id)[] { ("Pac-Man", "pacman"), ("Blinky", "blinky"), ("Inky", "inky"), ("Pinky", "pinky"), ("Clyde", "clyde") }
            .Select(c => (c.name, position: nameFunction.Invoke(GetField<int>(comp, $"{c.id}Pos").Get(v => !validPositions.Contains(v) ? "not a valid position" : null))))
            .ToArray();
        var shownPositions = combos.Select(c => c.position).ToArray();
        addQuestions(module, combos.Select(c => makeQuestion(Question.GhostMovementPosition, module, formatArgs: new[] { c.name }, correctAnswers: new[] { c.position }, preferredWrongAnswers: shownPositions)));
    }
}