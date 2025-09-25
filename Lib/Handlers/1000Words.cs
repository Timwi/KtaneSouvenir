using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S1000Words
{
    [SouvenirQuestion("What was the {1} word shown in {0}?", ThreeColumns6Answers, ExampleAnswers = ["Baken", "Ghost", "Tolts", "Oyers", "Sweel", "Rangy", "Noses", "Chapt", "Phuts", "Pingo", "Hylas", "Podia", "Vizor"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Words
}

public partial class SouvenirModule
{
    [SouvenirHandler("OneThousandWords", "1000 Words", typeof(S1000Words), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> Process1000Words(ModuleData module)
    {
        var comp = GetComponent(module, "ThousandWordsScript");
        var wordsWritten = new List<string>();
        var phrases = new[] {
            "OYERS", "SWEEL", "RANGY", "NOSES", "CHAPT", "PHUTS", "PINGO", "HYLAS", "PODIA", "VIZOR", "METES", "GULCH", "KHETS", "LUMME", "SKEPS", "YABBY", "ROWAN", "SIRIH", "AINGA", "TAXER",
            "TEELS", "YCOND", "BACHS", "DHUTI", "VAUNT", "GLOST", "BELON", "CENTS", "MUSIT", "PRIEF", "JERID", "EVERY", "PUERS", "DUDES", "FANGO", "TAPET", "LOUTS", "PROSS", "LEMON", "BLADS",
            "COWAN", "RIEVE", "IDEAS", "ANOMY", "OPINE", "INERT", "PREES", "BLEED", "BIDED", "LESBO", "COLLS", "FRAUD", "VISON", "WAKER", "MUMUS", "JUCOS", "DIOLS", "REIGN", "ERUPT", "EBONS",
            "LUACH", "CONTO", "ALEWS", "FACIA", "SPINS", "IMSHY", "CURNS", "LINNS", "DOING", "LIENS", "SEELY", "JIBES", "DIMLY", "UNPEN", "MOCHA", "MINED", "SWORD", "MATTS", "KALIS", "WHIRR",
            "MAROR", "SAGES", "DONNA", "PUNGS", "INANE", "STONN", "WEKAS", "OLLIE", "EARST", "BEGET", "QUAKE", "SCURS", "AULAS", "BOSOM", "CUPID", "PETTI", "DOMAL", "TAUTS", "LOHAN", "KOELS",
            "FIARS", "SANTS", "LUSER", "HONED", "COCCO", "MANED", "PAPES", "FLEME", "SNAFU", "DROVE", "PEWIT", "RAWIN", "BAMBI", "TETRA", "GIRLS", "DOWAR", "REAPS", "BELCH", "DAMES", "ZINGY",
            "SOLVE", "QUITS", "BEAUS", "RAREE", "FENIS", "SKEET", "SCULP", "TIFTS", "LAXER", "BUNDH", "KAVAS", "SEPIA", "RIBES", "CYNIC", "PROWL", "THEES", "CLADE", "GHEST", "RACHE", "MUSET",
            "NUDES", "VAIRE", "ZURFS", "ROTOR", "WHOSE", "TRAYS", "BUNTS", "GROKS", "WUSSY", "MIXUP", "SURED", "KOORI", "ROKED", "SLOVE", "CRAMP", "HIDED", "AGAZE", "AURAS", "GLOBS", "KEDGE",
            "PONES", "BLITZ", "DARKY", "BONNY", "INORB", "PARES", "VENTS", "GRASP", "CRAZE", "TROPE", "DUOMO", "QUAYS", "EBBET", "FOIST", "TAKHI", "UPPER", "SHIRE", "RAMIS", "ROWME", "SEDES",
            "ROOTY", "PANED", "NACRE", "FRONT", "SPALD", "ADOWN", "EBBED", "BUSED", "COXED", "WHAPS", "WAGED", "SEELD", "SCALD", "STICH", "LASER", "PECAN", "KEEFS", "PLUCK", "BOZOS", "APPAL",
            "FADER", "SISAL", "CRAWS", "SORTS", "WAXES", "KAGUS", "MICHE", "SENGI", "EXEAT", "MAULS", "MASSA", "MASTY", "FIEFS", "AHEAD", "RAIAS", "ESKAR", "SHALL", "DONNE", "JODEL", "BOWER",
            "MERIL", "VIRID", "JIRDS", "MOLLA", "REWET", "HAFIZ", "ZANZE", "AROMA", "STUCK", "BAHUT", "DRIED", "GIVEN", "PSHAW", "BAUDS", "WRYLY", "BAHTS", "NOOPS", "TINGE", "STOTS", "CAUSA",
            "STILT", "GIBUS", "CLYPE", "CEAZE", "WOVEN", "BLUES", "MIAOW", "SWABS", "REDIA", "TABES", "QUANT", "USUAL", "TINTS", "CREME", "ABOMA", "ACTIN", "JEDIS", "EMMEW", "JEBEL", "RIPES",
            "BROOL", "JEWEL", "EMEND", "FLUKY", "LYASE", "FOILS", "BROKE", "CETES", "BUSES", "PATIN", "CREEP", "LOUSE", "REARS", "LUNES", "SCOUG", "VARDY", "LENIS", "RHINE", "GASTS", "APAYD",
            "BANGS", "DANKS", "ABORE", "BEDEW", "MICOS", "ANNAL", "SUNNA", "REGUR", "SPUMY", "TANTI", "CRUST", "GOLPS", "SLUMS", "BIRTH", "GARBE", "MONAD", "MOXIE", "CRAVE", "MEARE", "PETRE",
            "AMEER", "HEIDS", "RUGGY", "SPOOL", "SOOTS", "TUPIK", "NUDER", "COVER", "MORNE", "FONDA", "CHELP", "BITES", "SKYED", "TEXTS", "NOVAE", "GENUA", "WEEST", "MORNS", "LINEN", "BLAST",
            "BOWES", "CHEKA", "THOFT", "PORTY", "SUMAC", "GREET", "WHEYS", "WARKS", "UNWED", "SUMMA", "CHIRU", "HEXED", "QUERN", "SABOT", "SPITS", "BOYAU", "SLUTS", "YIRTH", "ZAXES", "KAIES",
            "PORAE", "ANTRA", "GHOST", "SOUMS", "MARRY", "PLESH", "ROYAL", "RUSSE", "FAIRS", "TRUGS", "LEGGE", "LIMAS", "LAZAR", "CHAIN", "DIVED", "BLAME", "AARTI", "BUCHU", "TRIOS", "RATAL",
            "MUDRA", "SYRAH", "FLUOR", "EWHOW", "SATES", "OPENS", "NICKS", "MENDS", "NOYAU", "GREAT", "COINS", "DURZI", "CESTA", "IMAGE", "GENUS", "GOWKS", "PIKED", "KARKS", "NUDGE", "YOGIC",
            "GREWS", "RONTS", "TOYOS", "LURES", "SKITS", "KOLAS", "GOOPS", "WAZIR", "BARDE", "SPATE", "ZINKY", "DRAPE", "INCOG", "SLACK", "LYSED", "FETCH", "NOWTS", "STASH", "NIEVE", "MURES",
            "PECKE", "RONES", "EARTH", "EVITE", "EXEME", "KNUTS", "ENDER", "PSOAE", "MEZZO", "COOPT", "PEEKS", "MAMBO", "RANCH", "MUSCA", "SPICE", "ALARM", "SANED", "PEEOY", "BEACH", "BARDY",
            "SKEIN", "ALIBI", "SIFTS", "UMBEL", "WOLFS", "SKIMP", "MARGS", "ERVEN", "STRUM", "DEFIS", "WEIRS", "RIPED", "SOUCE", "DENET", "GREBE", "UNMEW", "CANDY", "SADHU", "DEISM", "METHS",
            "SCRIP", "VIGIA", "INGOT", "SLADE", "EALES", "NAPOO", "PIETY", "SCOOT", "RECCO", "CRAME", "SHIPS", "YODHS", "FANON", "FELLY", "CHILL", "MIGHT", "GONNA", "ICTIC", "GOOFY", "HAPPY",
            "DECOY", "PROYN", "SMITS", "GAMBO", "SHTUM", "RIDGY", "DWELT", "TUFTY", "POOHS", "AZIDE", "BEFOG", "FOUND", "ARTEL", "MOMMA", "NIFES", "BIGHA", "KINGS", "WAURS", "BONKS", "APSES",
            "KENOS", "TOMMY", "FRITZ", "MINKS", "BIOTA", "PLATY", "IDENT", "AREFY", "SLATY", "DORKS", "AVERS", "BOCHE", "PARVE", "JEEPS", "STYES", "BEIGY", "HAHAS", "HAMAL", "TEIID", "ETHER",
            "BEVVY", "IMPIS", "BINDI", "VIGIL", "JIGOT", "MYOMA", "THEMA", "GROSS", "LUCES", "POTIN", "OUIJA", "SNOEP", "VITAE", "LEPID", "STARR", "SYENS", "PAILS", "DESSE", "SPREW", "HUHUS",
            "JUTTY", "OCTET", "NIFFY", "NICKY", "ROBOT", "LEVER", "GIGHE", "JOLES", "KUSSO", "ORANT", "VISTO", "STIVE", "BOOTS", "LISKS", "RICHT", "CATER", "VROWS", "CLEEP", "ADAPT", "BITOU",
            "ZEBRA", "KAURY", "SMAAK", "LOIRS", "SEGOS", "FIRMS", "CAUMS", "ANTAE", "DUPLE", "READY", "DOTAL", "MOMUS", "MORPH", "WHENS", "HYLIC", "WATER", "NAKED", "KHUDS", "TWEET", "WOXEN",
            "KNEES", "ALODS", "MULLA", "COKED", "CODEC", "NICOL", "MACHO", "SHEET", "DRAYS", "SNAKY", "LASES", "WOOTZ", "DISCI", "JUREL", "SMELT", "KIKOI", "OSHAC", "SHUSH", "POORT", "SWALY",
            "LAXES", "YESKS", "READD", "PAVIS", "LENTI", "CYMAS", "TARTS", "CONNE", "GAPED", "IDIOT", "ARGLE", "DAZER", "LINGO", "ANVIL", "AHINT", "MARRI", "BURSE", "FILOS", "WISPS", "BOATS",
            "BAJUS", "BOOFY", "OPERA", "PLOYS", "AWEEL", "COONS", "ZEALS", "HALER", "VARVE", "BELLS", "VINED", "CYMOL", "DHOLL", "KNOUT", "EMBAY", "RITTS", "VEINS", "SKIVY", "FAERY", "CLEPT",
            "BESOT", "LUMEN", "BEARD", "BLITE", "DEBIT", "NONES", "AIMED", "WACKY", "WASES", "FRONS", "HUIAS", "TAUPE", "SLOGS", "STUPE", "NETOP", "ARABA", "HOOKS", "AXILE", "PORES", "TEASE",
            "BANAL", "HERBS", "ALMES", "GHAZI", "ARENE", "PARKI", "PUZEL", "SNARF", "LEECH", "TWIER", "DRICE", "RAWLY", "EMAIL", "PRINK", "EASED", "MACHE", "WISTS", "BITOS", "ELPEE", "NOULS",
            "PIGMY", "AFORE", "PRATY", "MILDS", "FILUM", "ACTED", "HEFTE", "SIALS", "JAGGY", "SCATS", "YMPES", "MAUND", "PIPIT", "LAPSE", "HAYER", "MOPPY", "CAMAN", "RIMAE", "FIFTH", "NEESE",
            "STURE", "KYNDE", "JAMES", "BEIGE", "CAIDS", "SEARS", "BIMAS", "ODALS", "DENTS", "INDIE", "SOLOS", "BRING", "SAROD", "NAUCH", "KINOS", "CHEER", "FORKY", "ADSUM", "ABORD", "NANAS",
            "TELIA", "KILOS", "ARHAT", "WHISK", "LOUED", "GAMED", "LINDY", "GAZOO", "OPAHS", "VALES", "NAZIR", "RENIN", "HOWKS", "GUNNY", "FELON", "RAUNS", "TUXES", "LAWKS", "CARKS", "THEIC",
            "SWARM", "NONIS", "PYXIS", "WADER", "LOSER", "SINKS", "PAVER", "MENSH", "HAZEL", "JUROR", "MUCIC", "HUMID", "CATCH", "TAELS", "MOYLS", "AMENT", "HUCKS", "PIXES", "DRUPE", "STIMY",
            "HEATH", "HOKUM", "HEARD", "BLART", "ANILS", "TROCK", "SHALY", "SEWEN", "REALS", "SLOES", "CHURL", "PLONK", "SNODS", "ONSET", "ACHES", "SAPOR", "ASPER", "BURRS", "THANE", "SIDHA",
            "SAUTE", "JESUS", "TOFUS", "LYSOL", "KHETH", "ORATE", "REIRD", "FAVAS", "SMEEK", "FARSE", "CULLY", "MAURI", "REBUS", "CHILD", "ROUTS", "QUIFF", "WOOER", "VISTA", "PIEND", "PARTS",
            "FAIRY", "KNURS", "STAMP", "CHIMB", "AUDIO", "JOKED", "FEUAR", "SMUGS", "MOTES", "BLUME", "CASED", "LEMED", "BROTH", "KICKS", "STOTT", "JOTUN", "EUROS", "MINCE", "LUBRA", "SOUTH",
            "HAZAN", "SHAKO", "CIMEX", "SCAMS", "FJORD", "PILEI", "RELIC", "BUNKO", "SIXER", "WISER", "LARKY", "ATAXY", "LINGA", "SHOLA", "PLUMY", "UHLAN", "DAINE", "SCOOP", "PAGLE", "JUMPS",
            "LOOTS", "CRUVE", "ELOPE", "FOIDS", "LOCOS", "ABBED", "IDOLA", "FECAL", "ZOBUS", "WAMUS", "SORES", "OZONE", "BORTS", "LIMBA", "EASER", "TICCA", "RHONE", "KNIFE", "KEREL", "LUTER",
            "FANGA", "KAILS", "UNDEE", "PUKED", "QUOTH", "BESEE", "WHOPS", "SCOWS", "TALCY", "POLTS", "KERMA", "SAYED", "FROWS", "RIPEN", "VOLTI", "COSED", "WAMES", "IRIDS", "FRITT", "STULM",
            "CUING", "STEEN", "BRAVA", "PUPIL", "SHILL", "GALOP", "AUGER", "SHORT", "ALANS", "WEXED", "THURL", "ARUMS", "WILJA", "LEISH", "LEGGY", "GULFY", "FATES", "BILGE", "NIZAM", "COPER",
            "MINGS", "DIKES", "POTTY", "RETRY", "LOOFS", "VELLS", "PARRY", "BERME", "YOKEL", "CARED", "SETAE", "ESTOC", "OSMOL", "ALERT", "MOOED", "AIDER", "COARB", "SHOED", "PATUS", "GAGES",
            "DINGE", "OBIIT", "APERS", "SENDS", "GENTY", "PROST", "FRYER", "CURIA", "KURRE", "BIPED", "DOCHT", "BONUS", "VAUCH", "AZOTE", "XENON", "MEINT", "FOALS", "YELMS", "KOKUM", "HERYE",
            "AXLES", "SPRAY", "DOOBS", "GAVOT", "SPRUE", "BUNJE", "FLOWN", "PIANS", "RATTY", "LUNKS", "VARUS", "SUNNY", "DRUBS", "MINAS", "HYKES", "WAUKS", "KOMBU", "PEANS", "STYLI", "REMIT",
            "WINZE", "MINDS", "LURER", "TRAWL", "MILER", "MITIS", "DRYLY", "JUKES", "KOLOS", "BOYGS", "RATED", "PINKS", "DANIO", "CEDIS", "EYASS", "DONGS", "UGALI", "HANDY", "SHOYU", "CONKS",
            "HARMS", "SWOPS", "STIPE", "LUSTY", "GODLY", "DACES", "TOLTS", "HINNY", "TUTTI", "JOINT", "TEENE", "REGGO", "GUSLI", "CAVER", "BASHO", "EXITS", "ARUHE", "DOVED", "EVHOE", "HOWSO",
            "DONEE", "MONAL", "KINAS", "FIRTH", "PRANA", "TOFFS", "SOBAS", "TROTH", "SCHMO", "YAWEY", "FRANK", "HOLDS", "PAMPA", "INFER", "BIERS", "GAYER", "HULKY", "RUTTY", "PAGED", "PURED",
            "CHOMP", "DITAL", "SERFS", "ARDRI", "APIAN", "GANEV", "HAIKA", "MORRA", "BASKS", "BUFFS", "EXING", "ABOUT", "CRAYS", "PLEAS", "STAYS", "SPIAL", "SPEIR", "CALIF", "DREYS", "BIGGS",
            "VILDE", "HALOS", "FABLE", "DISAS", "DESEX", "LOWAN", "NOSED", "SAIMS", "EXPAT", "UMPED", "OULKS", "CONCH", "CHANT", "TACOS", "GOBBY", "OVATE", "CLANG", "WRING", "EMITS", "DIALS",
            "NAIAD", "SHLUB", "SWANS", "TITAN", "WAGER", "ATOCS", "POULP", "DAGGY", "POONS", "GRAMP", "CONIN", "MOHUR", "COALY", "DITTO", "AYAHS", "ADEEM", "STOAT", "LATCH", "FAWNS", "XYLIC",
            "SODAS", "FRAGS", "GANGS", "WITHS", "YIPPY", "BIRLE", "SPAZZ", "CLASP", "TWANG", "DEADS", "PRIGS", "CADGE", "ICHOR", "BARNS", "CYCAD", "SALOL", "RAZED", "LAVAS", "KAYAK", "CURES",
            "STOMP", "NATIS", "KORAS", "CHESS", "CIVIL", "SACKS", "CUSKS", "GEMMY", "LIART", "SHINE", "YARKS", "TAWAI", "TACTS", "HOPES", "INRUN", "CIVES", "DRATS", "FUCUS", "APART", "HENNA",
            "REAME", "SCUTS", "BALES", "TENTY", "DEARS", "UNPEG", "BELAH", "DICTA", "BETES", "HOGHS", "GAULT", "REPEG", "CADEE", "GRISY", "WHIRL", "SUTOR", "DINER", "UREDO", "PILCH", "VASTS",
            "MALAR", "ROJAK", "HILUM", "CRACK", "LAIDS", "FRAYS", "ROATE", "GARBS", "QUOIT", "LOGGY", "TRINE", "VIBEY", "STAIN", "TSKED", "VIEWS", "CYBER", "HALVE", "TANNA", "OCHER", "PHOTS",
            "AMATE", "INKED", "YORPS", "AQUAE", "CREPE", "ACNES", "NOTAL", "SCROW", "MINTY", "GEOID", "DURGY", "BREED", "PEATY", "KERRY", "LAMAS", "FAVOR", "PINNA", "LOVEY", "SMOWT", "NUKED",
            "CREWE", "GLOBY", "GEITS", "VADES", "ROULE", "SHEAR", "MOMES", "ENJOY", "HEDGY", "GONIA", "ALBAS", "ZOOKS", "FUZES", "PREYS", "HERLS", "HAARS", "TETHS", "SHOOK", "LEAST", "LUCKY",
            "FEOFF", "LITHE", "COWRY", "FRUST", "THIOL", "DOWRY", "PIANO", "EPOXY", "SLOTH", "SCOUT", "ANENT", "RHINO", "OOBIT", "SPEED", "PIPES", "SOLID", "POKEY", "CHAWK", "MACES", "AWAKE",
            "FAULT", "FARCY", "COMBI", "REEVE", "EATEN", "RUDDY", "GRABS", "CLANK", "TRAIL", "KITTY", "PALPI", "THILK", "KETAS", "VODUN", "CORAM", "TEWED", "SAVOR", "SULLY", "LABEL", "ANODE",
            "MOIRE", "FAYED", "RUBUS", "KESAR", "PANTY", "VIRUS", "RATAN", "FUGGY", "CYANO", "SCOWL", "MILTS", "ACRES", "BAWLS", "KLICK", "ALPHA", "SHOES", "IRONE", "CREES", "JOYED", "LUNAR",
            "SKATT", "BESTS", "YRNEH", "YARTO", "SESEY", "DONSY", "LEONE", "THORO", "ANCHO", "SLOPY", "GEYER", "SLAVE", "BHUTS", "LOBES", "SESSA", "OMBER", "ONIUM", "DERES", "GAIRS", "GNAWS",
            "MILKY", "SPAZA", "KERKY", "SNATH", "TENDS", "MANIS", "DALES", "KENCH", "DRAWS", "PEPOS", "HARDY", "ALOIN", "SNEAD", "TAILS", "SYNCH", "JAGAS", "STEED", "UMBLE", "BRIKS", "IDLES",
            "SKIFF", "MYNAH", "DHOLE", "LARUM", "HOWRE", "MIAOU", "WEFTE", "HERTZ", "WIMPY", "RETES", "OHING", "TYNED", "PEENS", "DARGA", "THUNK", "BOOZY", "ELAND", "TONUS", "JAAPS", "VIVID",
            "MAXES", "CROCI", "YEWEN", "CHIRM", "RASTA", "BUTUT", "GIRTH", "DIDOS", "ANKUS", "REIFY", "ATUAS", "BOORD", "ERSES", "GRAZE", "SPITZ", "SKEOS", "EXINE", "TOTAL", "SCOFF", "CHIDE",
            "FOUER", "SIBYL", "GRAAL", "SKENE", "RATIO", "AIDOS", "BLIMP", "GESSO", "CHEST", "BLAMS", "ALGIN", "FLANS", "VINCA", "HOYLE", "WAFER", "PRAHU", "SORTA", "BREES", "DAWAH", "CASAS",
            "MAIKO", "BROND", "NAANS", "BRISE", "LOCUM", "AXONE", "CLOFF", "PISES", "NEEZE", "PRIOR", "CROWS", "RORTS", "GRUFE", "TENNY", "NOMAS", "TOUCH", "ORVAL", "MYTHI", "BUDDY", "DUROS",
            "LURVE", "DUNAM", "WEAVE", "REENS", "SOYAS", "SNUSH", "STIRS", "MIFFS", "CUTIS", "DOABS", "PAYEE", "HOWFF", "GRIPT", "YUKOS", "RAVED", "MATLO", "NAIFS", "SAPPY", "QORMA", "BLEAT",
            "FEMAL", "BANED", "BANDA", "RIVER", "SKEWS", "LOUND", "PUNJI", "SAILS", "HAITH", "SELLA", "DRUNK", "RANKE", "AWFUL", "ADIOS", "BEDYE", "WRICK", "ORFES", "FIFTY", "PEATS", "ULCER",
            "CRARE", "RESAW", "AUGHT", "AMNIC", "RIGID", "MILPA", "SAMAN", "SAYER", "SALMI", "BUIKS", "LITRE", "RUDES", "VIAND", "BIRSY", "GILET", "RATHE", "GUESS", "RANGE", "BAKEN", "JAUKS",
            "RURUS", "TICES", "ILIAC", "NIPAS", "MULSE", "UNZIP", "NOVAS", "THARM", "DIPPY", "TOOLS", "GNATS", "WOOSE", "JAGGS", "PAWED", "FAUGH", "UNDER", "SORAL", "ACYLS", "LIANE", "SAVEY",
            "PULER", "SORGO", "BUAZE", "BOUGE", "MANIC", "INPUT", "FAROS", "STUNT", "YLEMS", "HESPS", "CAUDA", "RAIDS", "HAROS", "SAKIS", "VALID", "GARBO", "WHIFF", "SUETS", "ETNAS", "WIGHT",
            "LOWLY", "SALTY", "LANKY", "LIBRI", "UNWET", "CREPY", "MURLY", "GUARS", "SWATS", "SHADS", "YENTA", "DAISY", "BRAKY", "ALUMS", "SEKOS", "GHYLL", "MOGGY", "STOWP", "MEATH", "MANGS",
            "AFROS", "LESTS", "NEAPS", "GRIPY", "URSAE", "REDES", "KRAUT", "GOPIK", "ROLES", "PLOTS", "JINNE", "IMIDO", "CUTER", "VOLVE", "REALM", "AMPUL", "RUANA", "FIATS", "TUANS", "VEILS",
            "ELITE", "OXIME", "WHISS", "SPEUG", "PECHS", "NARIC", "RUDIE", "KUTIS", "DUKED", "SYTHE", "ENOKI", "YEAST", "ALIKE", "MONTE", "TASSE", "COBBY", "IDEAL", "LEGIT", "THRIP", "KUTCH",
            "GLADE", "GRENZ", "SWEIR", "HOARS", "MODUS", "SCAMP", "OATER", "BALKS", "DOGMA", "SINED", "COVET", "RAPER", "DEELY", "MUSHY", "SWEET", "JAUNT", "RUBAI", "SIEVE", "LOBOS", "PYATS",
            "MEZZE", "KANJI", "FAVER", "SNUBS", "CRIES", "NGANA", "COGUE", "SIZAR", "BLOKE", "CENTU", "ADOPT", "EPHAS", "NABLA", "CLAPS", "ALIEN", "SOUPS", "LURCH", "TIRES", "TASAR", "HANGS",
            "BULSE", "FAGOT", "WHORT", "BARMY", "SPELK", "FOYER", "SKYFS", "FURRS", "JAGRA", "LEAZE", "SALAD", "MORTS", "ZONES", "UMIAC", "DATES", "USURY", "CLACK", "ROVES", "MISER", "MORRO",
            "REDDS", "CREDS", "TIARS", "DRIVE", "TABER", "HOMAS", "TOLES", "SOUCT", "PELFS", "GELID", "EMBUS", "VIMEN", "EMCEE", "RESTO", "BRACH", "SNEER", "PAUSE", "JOWLY", "PSYCH", "SHULN",
            "TEAKS", "TUBAE", "FRETS", "SLUFF", "FINER", "GOLDY", "YORKS", "PYOID", "DOWDS", "BORTZ", "GRAPH", "ALLOT", "TRULL", "FISHY", "KATIS", "BORAL", "VICED", "DOMES", "STENT", "SATAY",
            "NEEMB", "KIBLA", "WARBY", "SHOPS", "REBIT", "LEIRS", "FARTS", "FRIZZ", "DOLMA", "ALECS", "JOMON", "IGGED", "FAZES", "MUDIR", "ROUPY", "LEGES", "PUNNY", "OMLAH", "GUANO", "SIDES",
            "BOYAR", "BOARS", "BARCA", "DOLCE", "TIERS", "HELVE", "INNED", "HALTS", "ETYMA", "CURER", "CAWKS", "HEWED", "GROWL", "BONZE", "CARGO", "WAIFS", "VANES", "COMAE", "LAKHS", "KRAIT",
            "ICHES", "OUGHT", "REPLA", "SHOOL", "COBZA", "RUMPS", "TUTEE", "MERCS", "DEGUM", "BOTCH", "OLEIC", "CURDS", "YERBA", "URVAS", "PREVE", "FEESE", "CLOYS", "BINES", "MERDE", "LADLE",
            "WRYER", "BASED", "GINGE", "CLANS", "BRAZE", "WOOSH", "SNARL", "CRICK", "DUANS", "SHANK", "WADTS", "ALGID", "SOOTE", "PECKS", "GRUMP", "CAFFS", "CUTIE", "SNOTS", "WRITE", "TEMPO",
            "TUBBY", "FYCES", "MOITS", "ACTON", "DROOP", "RUGBY", "APTLY", "HYPER", "CAVAS", "CAAED", "CHAFT", "RAFFS", "AMAZE", "SWEES", "LAIKS", "ASSES", "MODGE", "JABOT", "THERM", "PROLE",
            "COALA", "FRORE", "TUMMY", "GLAUM", "TACKY", "PALLS", "CLEAN", "JEHUS", "MEANS", "KNAGS", "HALED", "LULUS", "BEADY", "RONDE", "CLOUD", "PUTTS", "BRAYS", "BLING", "WORKS", "ERGOT",
            "SLUNG", "BUDOS", "BAYED", "AHULL", "PALAS", "DELES", "SLYPE", "TAVER", "FRENA", "POSEY", "PROSY", "BORMS", "POUKS", "LOWES", "SAVED", "MANSE", "TAULD", "SOLON", "DEBUD", "BUOYS",
            "POWRE", "GOFER", "FOUNT", "IOTAS", "SCENT", "ANTES", "NADIR", "MORES", "FORAM", "ARRAH", "SPIEL", "FAQIR", "OWNED", "KANAE", "CHAYA", "CHIRR", "WIFES", "DURED", "HAGGS", "JIVED",
            "STUNG", "BISON", "TERRY", "TRANS", "WHEFT", "LYTED", "NUTSY", "GENOA", "BERRY", "SEANS", "TATHS", "REDON", "QADIS", "DITAS", "GASPS", "HUFFY", "WIZES", "AWNED", "PARGE", "STUNK",
            "GIPPO", "BOKED", "LEBEN", "FUGLY", "BEAUX", "RATCH", "REDLY", "NAGOR", "RIMES", "PYINS", "JUNKS", "HURRY", "REDOX", "BLAIN", "SECCO", "LEEPS", "GRYPT", "NUFFS", "DRAFT", "JADES",
            "MITES", "STERN", "KAMIS", "LYNES", "TYRES", "CAPUT", "DOILY", "WHITE", "BEVEL", "AHURU", "LUDOS", "FILMS", "NODUS", "CLAVI", "BOLIX", "BOGGY", "OUTER", "KYPES", "URALI", "FLOCK",
            "NOIRS", "POSIT", "CALLS", "TRIST", "DRIBS", "KIBBE", "CHADO", "TEERS", "EQUIP", "BUFFY", "FELLS", "KERNS", "SWEEP", "PLUNK", "WHALE", "COOLY", "BARRO", "URINE", "AVENS", "NERTZ",
            "SYPES", "VIBES", "FEATS", "OCHES", "NYAFF", "FRACT", "WIGAN", "ESSAY", "SDAYN", "VOILA", "PEDRO", "OATHS", "STOPS", "CLEVE", "ENOWS", "WYLES", "APAID", "PELON", "CARDS", "BURQA",
            "SIMAR", "STUFF", "VENIN", "DINES", "GEYAN", "PIGHT", "FROTH", "INFRA", "FAINS", "PUJAS", "DECKO", "MILLS", "DUPED", "GUNDY", "LIFES", "GURRY", "GUCKY", "SHULE", "SOLDO", "KABAR",
            "CLAWS", "RALES", "SOWCE", "VEXED", "CAUKS", "SKANK", "QUIDS", "WEARS", "OPTIC", "DASHY", "NIPPY", "GREGE", "GAUMS", "STRIG", "SEATS", "NEMPT", "SPERM", "METRE", "AWASH", "OGGIN",
            "SUCKY", "DAMPY", "HOWES", "EXAMS", "CEDER", "TOYON", "AMBAN", "FLAYS", "ALMUG", "BETHS", "BOVID", "YOKES", "DOODY", "BRANT", "QUEER", "QUINE", "HANTS", "PSALM", "VOCES", "THYMY",
            "FINAL", "RAPHE", "POULT", "TAPAS", "BUXOM", "REDAN", "GLEBE", "PRYSE", "AUMIL", "MACHI", "OMENS", "KIERS", "LENSE", "DEVIL", "CANON", "TAWED", "MAZUT", "SQUAD", "BEAST", "STEIL",
            "BREVE", "LUAUS", "RATER", "LIONS", "PRUNT", "KANGA", "FINCA", "BOING", "ALMUD", "CAPOS", "FOGIE", "STRAW", "PORNO", "DUMBO", "DIBBS", "SICKS", "TARRY", "KREEP", "KYBOS", "SORNS",
            "EXCEL", "BYRES", "THONG", "WOOFS", "SEROW", "FORBS", "JUNTA", "SIEUR", "HEJAB", "DYKED", "VINTS", "KAIAK", "LAPIS", "GYNIE", "EPHOD", "GYPPY", "CUVEE", "AGREE", "SKEGS", "HEEDS" };

        yield return WaitForActivate;

        var yesAndNo = GetArrayField<KMSelectable>(comp, "Buttons", isPublic: true).Get(expectedLength: 2);
        var indexNumber = GetField<int>(comp, "WordIndex");
        var stageNumber = GetField<int>(comp, "Stage");

        for (var i = 0; i < yesAndNo.Length; i++)   // Safe to use ‘for’ loop as long as the loop variable is not captured by the lambda
        {
            var oldInteract = yesAndNo[i].OnInteract;
            yesAndNo[i].OnInteract = delegate
            {
                wordsWritten.Add(phrases[indexNumber.Get()]);
                var result = oldInteract();
                if (stageNumber.Get() == 5 && module.Unsolved)
                    wordsWritten.Clear();
                return result;
            };
        }

        yield return WaitForSolve;

        if (wordsWritten.Count != 5)
            throw new AbandonModuleException("Unable to gather all 5 words.");

        var wordsWrittenArr = wordsWritten.ToArray();
        yield return question(S1000Words.Words, args: [Ordinal(1)]).Answers(wordsWritten[0], all: phrases, preferredWrong: wordsWrittenArr);
        yield return question(S1000Words.Words, args: [Ordinal(2)]).Answers(wordsWritten[1], all: phrases, preferredWrong: wordsWrittenArr);
        yield return question(S1000Words.Words, args: [Ordinal(3)]).Answers(wordsWritten[2], all: phrases, preferredWrong: wordsWrittenArr);
        yield return question(S1000Words.Words, args: [Ordinal(4)]).Answers(wordsWritten[3], all: phrases, preferredWrong: wordsWrittenArr);
        yield return question(S1000Words.Words, args: [Ordinal(5)]).Answers(wordsWritten[4], all: phrases, preferredWrong: wordsWrittenArr);
    }
}
