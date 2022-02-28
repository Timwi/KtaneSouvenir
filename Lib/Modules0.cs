using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using Souvenir.Reflection;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> Process1000Words(KMBombModule module)
    {
        var comp = GetComponent(module, "ThousandWordsScript");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");
        var wordsWritten = new List<string>();
        var phrases = new[] { "OYERS", "SWEEL", "RANGY", "NOSES", "CHAPT", "PHUTS", "PINGO", "HYLAS", "PODIA", "VIZOR", "METES", "GULCH", "KHETS", "LUMME", "SKEPS", "YABBY", "ROWAN", "SIRIH", "AINGA", "TAXER", "TEELS", "YCOND", "BACHS", "DHUTI", "VAUNT", "GLOST", "BELON", "CENTS", "MUSIT", "PRIEF", "JERID", "EVERY", "PUERS", "DUDES", "FANGO", "TAPET", "LOUTS", "PROSS", "LEMON", "BLADS", "COWAN", "RIEVE", "IDEAS", "ANOMY", "OPINE", "INERT", "PREES", "BLEED", "BIDED", "LESBO", "COLLS", "FRAUD", "VISON", "WAKER", "MUMUS", "JUCOS", "DIOLS", "REIGN", "ERUPT", "EBONS", "LUACH", "CONTO", "ALEWS", "FACIA", "SPINS", "IMSHY", "CURNS", "LINNS", "DOING", "LIENS", "SEELY", "JIBES", "DIMLY", "UNPEN", "MOCHA", "MINED", "SWORD", "MATTS", "KALIS", "WHIRR", "MAROR", "SAGES", "DONNA", "PUNGS", "INANE", "STONN", "WEKAS", "OLLIE", "EARST", "BEGET", "QUAKE", "SCURS", "AULAS", "BOSOM", "CUPID", "PETTI", "DOMAL", "TAUTS", "LOHAN", "KOELS", "FIARS", "SANTS", "LUSER", "HONED", "COCCO", "MANED", "PAPES", "FLEME", "SNAFU", "DROVE", "PEWIT", "RAWIN", "BAMBI", "TETRA", "GIRLS", "DOWAR", "REAPS", "BELCH", "DAMES", "ZINGY", "SOLVE", "QUITS", "BEAUS", "RAREE", "FENIS", "SKEET", "SCULP", "TIFTS", "LAXER", "BUNDH", "KAVAS", "SEPIA", "RIBES", "CYNIC", "PROWL", "THEES", "CLADE", "GHEST", "RACHE", "MUSET", "NUDES", "VAIRE", "ZURFS", "ROTOR", "WHOSE", "TRAYS", "BUNTS", "GROKS", "WUSSY", "MIXUP", "SURED", "KOORI", "ROKED", "SLOVE", "CRAMP", "HIDED", "AGAZE", "AURAS", "GLOBS", "KEDGE", "PONES", "BLITZ", "DARKY", "BONNY", "INORB", "PARES", "VENTS", "GRASP", "CRAZE", "TROPE", "DUOMO", "QUAYS", "EBBET", "FOIST", "TAKHI", "UPPER", "SHIRE", "RAMIS", "ROWME", "SEDES", "ROOTY", "PANED", "NACRE", "FRONT", "SPALD", "ADOWN", "EBBED", "BUSED", "COXED", "WHAPS", "WAGED", "SEELD", "SCALD", "STICH", "LASER", "PECAN", "KEEFS", "PLUCK", "BOZOS", "APPAL", "FADER", "SISAL", "CRAWS", "SORTS", "WAXES", "KAGUS", "MICHE", "SENGI", "EXEAT", "MAULS", "MASSA", "MASTY", "FIEFS", "AHEAD", "RAIAS", "ESKAR", "SHALL", "DONNE", "JODEL", "BOWER", "MERIL", "VIRID", "JIRDS", "MOLLA", "REWET", "HAFIZ", "ZANZE", "AROMA", "STUCK", "BAHUT", "DRIED", "GIVEN", "PSHAW", "BAUDS", "WRYLY", "BAHTS", "NOOPS", "TINGE", "STOTS", "CAUSA", "STILT", "GIBUS", "CLYPE", "CEAZE", "WOVEN", "BLUES", "MIAOW", "SWABS", "REDIA", "TABES", "QUANT", "USUAL", "TINTS", "CREME", "ABOMA", "ACTIN", "JEDIS", "EMMEW", "JEBEL", "RIPES", "BROOL", "JEWEL", "EMEND", "FLUKY", "LYASE", "FOILS", "BROKE", "CETES", "BUSES", "PATIN", "CREEP", "LOUSE", "REARS", "LUNES", "SCOUG", "VARDY", "LENIS", "RHINE", "GASTS", "APAYD", "BANGS", "DANKS", "ABORE", "BEDEW", "MICOS", "ANNAL", "SUNNA", "REGUR", "SPUMY", "TANTI", "CRUST", "GOLPS", "SLUMS", "BIRTH", "GARBE", "MONAD", "MOXIE", "CRAVE", "MEARE", "PETRE", "AMEER", "HEIDS", "RUGGY", "SPOOL", "SOOTS", "TUPIK", "NUDER", "COVER", "MORNE", "FONDA", "CHELP", "BITES", "SKYED", "TEXTS", "NOVAE", "GENUA", "WEEST", "MORNS", "LINEN", "BLAST", "BOWES", "CHEKA", "THOFT", "PORTY", "SUMAC", "GREET", "WHEYS", "WARKS", "UNWED", "SUMMA", "CHIRU", "HEXED", "QUERN", "SABOT", "SPITS", "BOYAU", "SLUTS", "YIRTH", "ZAXES", "KAIES", "PORAE", "ANTRA", "GHOST", "SOUMS", "MARRY", "PLESH", "ROYAL", "RUSSE", "FAIRS", "TRUGS", "LEGGE", "LIMAS", "LAZAR", "CHAIN", "DIVED", "BLAME", "AARTI", "BUCHU", "TRIOS", "RATAL", "MUDRA", "SYRAH", "FLUOR", "EWHOW", "SATES", "OPENS", "NICKS", "MENDS", "NOYAU", "GREAT", "COINS", "DURZI", "CESTA", "IMAGE", "GENUS", "GOWKS", "PIKED", "KARKS", "NUDGE", "YOGIC", "GREWS", "RONTS", "TOYOS", "LURES", "SKITS", "KOLAS", "GOOPS", "WAZIR", "BARDE", "SPATE", "ZINKY", "DRAPE", "INCOG", "SLACK", "LYSED", "FETCH", "NOWTS", "STASH", "NIEVE", "MURES", "PECKE", "RONES", "EARTH", "EVITE", "EXEME", "KNUTS", "ENDER", "PSOAE", "MEZZO", "COOPT", "PEEKS", "MAMBO", "RANCH", "MUSCA", "SPICE", "ALARM", "SANED", "PEEOY", "BEACH", "BARDY", "SKEIN", "ALIBI", "SIFTS", "UMBEL", "WOLFS", "SKIMP", "MARGS", "ERVEN", "STRUM", "DEFIS", "WEIRS", "RIPED", "SOUCE", "DENET", "GREBE", "UNMEW", "CANDY", "SADHU", "DEISM", "METHS", "SCRIP", "VIGIA", "INGOT", "SLADE", "EALES", "NAPOO", "PIETY", "SCOOT", "RECCO", "CRAME", "SHIPS", "YODHS", "FANON", "FELLY", "CHILL", "MIGHT", "GONNA", "ICTIC", "GOOFY", "HAPPY", "DECOY", "PROYN", "SMITS", "GAMBO", "SHTUM", "RIDGY", "DWELT", "TUFTY", "POOHS", "AZIDE", "BEFOG", "FOUND", "ARTEL", "MOMMA", "NIFES", "BIGHA", "KINGS", "WAURS", "BONKS", "APSES", "KENOS", "TOMMY", "FRITZ", "MINKS", "BIOTA", "PLATY", "IDENT", "AREFY", "SLATY", "DORKS", "AVERS", "BOCHE", "PARVE", "JEEPS", "STYES", "BEIGY", "HAHAS", "HAMAL", "TEIID", "ETHER", "BEVVY", "IMPIS", "BINDI", "VIGIL", "JIGOT", "MYOMA", "THEMA", "GROSS", "LUCES", "POTIN", "OUIJA", "SNOEP", "VITAE", "LEPID", "STARR", "SYENS", "PAILS", "DESSE", "SPREW", "HUHUS", "JUTTY", "OCTET", "NIFFY", "NICKY", "ROBOT", "LEVER", "GIGHE", "JOLES", "KUSSO", "ORANT", "VISTO", "STIVE", "BOOTS", "LISKS", "RICHT", "CATER", "VROWS", "CLEEP", "ADAPT", "BITOU", "ZEBRA", "KAURY", "SMAAK", "LOIRS", "SEGOS", "FIRMS", "CAUMS", "ANTAE", "DUPLE", "READY", "DOTAL", "MOMUS", "MORPH", "WHENS", "HYLIC", "WATER", "NAKED", "KHUDS", "TWEET", "WOXEN", "KNEES", "ALODS", "MULLA", "COKED", "CODEC", "NICOL", "MACHO", "SHEET", "DRAYS", "SNAKY", "LASES", "WOOTZ", "DISCI", "JUREL", "SMELT", "KIKOI", "OSHAC", "SHUSH", "POORT", "SWALY", "LAXES", "YESKS", "READD", "PAVIS", "LENTI", "CYMAS", "TARTS", "CONNE", "GAPED", "IDIOT", "ARGLE", "DAZER", "LINGO", "ANVIL", "AHINT", "MARRI", "BURSE", "FILOS", "WISPS", "BOATS", "BAJUS", "BOOFY", "OPERA", "PLOYS", "AWEEL", "COONS", "ZEALS", "HALER", "VARVE", "BELLS", "VINED", "CYMOL", "DHOLL", "KNOUT", "EMBAY", "RITTS", "VEINS", "SKIVY", "FAERY", "CLEPT", "BESOT", "LUMEN", "BEARD", "BLITE", "DEBIT", "NONES", "AIMED", "WACKY", "WASES", "FRONS", "HUIAS", "TAUPE", "SLOGS", "STUPE", "NETOP", "ARABA", "HOOKS", "AXILE", "PORES", "TEASE", "BANAL", "HERBS", "ALMES", "GHAZI", "ARENE", "PARKI", "PUZEL", "SNARF", "LEECH", "TWIER", "DRICE", "RAWLY", "EMAIL", "PRINK", "EASED", "MACHE", "WISTS", "BITOS", "ELPEE", "NOULS", "PIGMY", "AFORE", "PRATY", "MILDS", "FILUM", "ACTED", "HEFTE", "SIALS", "JAGGY", "SCATS", "YMPES", "MAUND", "PIPIT", "LAPSE", "HAYER", "MOPPY", "CAMAN", "RIMAE", "FIFTH", "NEESE", "STURE", "KYNDE", "JAMES", "BEIGE", "CAIDS", "SEARS", "BIMAS", "ODALS", "DENTS", "INDIE", "SOLOS", "BRING", "SAROD", "NAUCH", "KINOS", "CHEER", "FORKY", "ADSUM", "ABORD", "NANAS", "TELIA", "KILOS", "ARHAT", "WHISK", "LOUED", "GAMED", "LINDY", "GAZOO", "OPAHS", "VALES", "NAZIR", "RENIN", "HOWKS", "GUNNY", "FELON", "RAUNS", "TUXES", "LAWKS", "CARKS", "THEIC", "SWARM", "NONIS", "PYXIS", "WADER", "LOSER", "SINKS", "PAVER", "MENSH", "HAZEL", "JUROR", "MUCIC", "HUMID", "CATCH", "TAELS", "MOYLS", "AMENT", "HUCKS", "PIXES", "DRUPE", "STIMY", "HEATH", "HOKUM", "HEARD", "BLART", "ANILS", "TROCK", "SHALY", "SEWEN", "REALS", "SLOES", "CHURL", "PLONK", "SNODS", "ONSET", "ACHES", "SAPOR", "ASPER", "BURRS", "THANE", "SIDHA", "SAUTE", "JESUS", "TOFUS", "LYSOL", "KHETH", "ORATE", "REIRD", "FAVAS", "SMEEK", "FARSE", "CULLY", "MAURI", "REBUS", "CHILD", "ROUTS", "QUIFF", "WOOER", "VISTA", "PIEND", "PARTS", "FAIRY", "KNURS", "STAMP", "CHIMB", "AUDIO", "JOKED", "FEUAR", "SMUGS", "MOTES", "BLUME", "CASED", "LEMED", "BROTH", "KICKS", "STOTT", "JOTUN", "EUROS", "MINCE", "LUBRA", "SOUTH", "HAZAN", "SHAKO", "CIMEX", "SCAMS", "FJORD", "PILEI", "RELIC", "BUNKO", "SIXER", "WISER", "LARKY", "ATAXY", "LINGA", "SHOLA", "PLUMY", "UHLAN", "DAINE", "SCOOP", "PAGLE", "JUMPS", "LOOTS", "CRUVE", "ELOPE", "FOIDS", "LOCOS", "ABBED", "IDOLA", "FECAL", "ZOBUS", "WAMUS", "SORES", "OZONE", "BORTS", "LIMBA", "EASER", "TICCA", "RHONE", "KNIFE", "KEREL", "LUTER", "FANGA", "KAILS", "UNDEE", "PUKED", "QUOTH", "BESEE", "WHOPS", "SCOWS", "TALCY", "POLTS", "KERMA", "SAYED", "FROWS", "RIPEN", "VOLTI", "COSED", "WAMES", "IRIDS", "FRITT", "STULM", "CUING", "STEEN", "BRAVA", "PUPIL", "SHILL", "GALOP", "AUGER", "SHORT", "ALANS", "WEXED", "THURL", "ARUMS", "WILJA", "LEISH", "LEGGY", "GULFY", "FATES", "BILGE", "NIZAM", "COPER", "MINGS", "DIKES", "POTTY", "RETRY", "LOOFS", "VELLS", "PARRY", "BERME", "YOKEL", "CARED", "SETAE", "ESTOC", "OSMOL", "ALERT", "MOOED", "AIDER", "COARB", "SHOED", "PATUS", "GAGES", "DINGE", "OBIIT", "APERS", "SENDS", "GENTY", "PROST", "FRYER", "CURIA", "KURRE", "BIPED", "DOCHT", "BONUS", "VAUCH", "AZOTE", "XENON", "MEINT", "FOALS", "YELMS", "KOKUM", "HERYE", "AXLES", "SPRAY", "DOOBS", "GAVOT", "SPRUE", "BUNJE", "FLOWN", "PIANS", "RATTY", "LUNKS", "VARUS", "SUNNY", "DRUBS", "MINAS", "HYKES", "WAUKS", "KOMBU", "PEANS", "STYLI", "REMIT", "WINZE", "MINDS", "LURER", "TRAWL", "MILER", "MITIS", "DRYLY", "JUKES", "KOLOS", "BOYGS", "RATED", "PINKS", "DANIO", "CEDIS", "EYASS", "DONGS", "UGALI", "HANDY", "SHOYU", "CONKS", "HARMS", "SWOPS", "STIPE", "LUSTY", "GODLY", "DACES", "TOLTS", "HINNY", "TUTTI", "JOINT", "TEENE", "REGGO", "GUSLI", "CAVER", "BASHO", "EXITS", "ARUHE", "DOVED", "EVHOE", "HOWSO", "DONEE", "MONAL", "KINAS", "FIRTH", "PRANA", "TOFFS", "SOBAS", "TROTH", "SCHMO", "YAWEY", "FRANK", "HOLDS", "PAMPA", "INFER", "BIERS", "GAYER", "HULKY", "RUTTY", "PAGED", "PURED", "CHOMP", "DITAL", "SERFS", "ARDRI", "APIAN", "GANEV", "HAIKA", "MORRA", "BASKS", "BUFFS", "EXING", "ABOUT", "CRAYS", "PLEAS", "STAYS", "SPIAL", "SPEIR", "CALIF", "DREYS", "BIGGS", "VILDE", "HALOS", "FABLE", "DISAS", "DESEX", "LOWAN", "NOSED", "SAIMS", "EXPAT", "UMPED", "OULKS", "CONCH", "CHANT", "TACOS", "GOBBY", "OVATE", "CLANG", "WRING", "EMITS", "DIALS", "NAIAD", "SHLUB", "SWANS", "TITAN", "WAGER", "ATOCS", "POULP", "DAGGY", "POONS", "GRAMP", "CONIN", "MOHUR", "COALY", "DITTO", "AYAHS", "ADEEM", "STOAT", "LATCH", "FAWNS", "XYLIC", "SODAS", "FRAGS", "GANGS", "WITHS", "YIPPY", "BIRLE", "SPAZZ", "CLASP", "TWANG", "DEADS", "PRIGS", "CADGE", "ICHOR", "BARNS", "CYCAD", "SALOL", "RAZED", "LAVAS", "KAYAK", "CURES", "STOMP", "NATIS", "KORAS", "CHESS", "CIVIL", "SACKS", "CUSKS", "GEMMY", "LIART", "SHINE", "YARKS", "TAWAI", "TACTS", "HOPES", "INRUN", "CIVES", "DRATS", "FUCUS", "APART", "HENNA", "REAME", "SCUTS", "BALES", "TENTY", "DEARS", "UNPEG", "BELAH", "DICTA", "BETES", "HOGHS", "GAULT", "REPEG", "CADEE", "GRISY", "WHIRL", "SUTOR", "DINER", "UREDO", "PILCH", "VASTS", "MALAR", "ROJAK", "HILUM", "CRACK", "LAIDS", "FRAYS", "ROATE", "GARBS", "QUOIT", "LOGGY", "TRINE", "VIBEY", "STAIN", "TSKED", "VIEWS", "CYBER", "HALVE", "TANNA", "OCHER", "PHOTS", "AMATE", "INKED", "YORPS", "AQUAE", "CREPE", "ACNES", "NOTAL", "SCROW", "MINTY", "GEOID", "DURGY", "BREED", "PEATY", "KERRY", "LAMAS", "FAVOR", "PINNA", "LOVEY", "SMOWT", "NUKED", "CREWE", "GLOBY", "GEITS", "VADES", "ROULE", "SHEAR", "MOMES", "ENJOY", "HEDGY", "GONIA", "ALBAS", "ZOOKS", "FUZES", "PREYS", "HERLS", "HAARS", "TETHS", "SHOOK", "LEAST", "LUCKY", "FEOFF", "LITHE", "COWRY", "FRUST", "THIOL", "DOWRY", "PIANO", "EPOXY", "SLOTH", "SCOUT", "ANENT", "RHINO", "OOBIT", "SPEED", "PIPES", "SOLID", "POKEY", "CHAWK", "MACES", "AWAKE", "FAULT", "FARCY", "COMBI", "REEVE", "EATEN", "RUDDY", "GRABS", "CLANK", "TRAIL", "KITTY", "PALPI", "THILK", "KETAS", "VODUN", "CORAM", "TEWED", "SAVOR", "SULLY", "LABEL", "ANODE", "MOIRE", "FAYED", "RUBUS", "KESAR", "PANTY", "VIRUS", "RATAN", "FUGGY", "CYANO", "SCOWL", "MILTS", "ACRES", "BAWLS", "KLICK", "ALPHA", "SHOES", "IRONE", "CREES", "JOYED", "LUNAR", "SKATT", "BESTS", "YRNEH", "YARTO", "SESEY", "DONSY", "LEONE", "THORO", "ANCHO", "SLOPY", "GEYER", "SLAVE", "BHUTS", "LOBES", "SESSA", "OMBER", "ONIUM", "DERES", "GAIRS", "GNAWS", "MILKY", "SPAZA", "KERKY", "SNATH", "TENDS", "MANIS", "DALES", "KENCH", "DRAWS", "PEPOS", "HARDY", "ALOIN", "SNEAD", "TAILS", "SYNCH", "JAGAS", "STEED", "UMBLE", "BRIKS", "IDLES", "SKIFF", "MYNAH", "DHOLE", "LARUM", "HOWRE", "MIAOU", "WEFTE", "HERTZ", "WIMPY", "RETES", "OHING", "TYNED", "PEENS", "DARGA", "THUNK", "BOOZY", "ELAND", "TONUS", "JAAPS", "VIVID", "MAXES", "CROCI", "YEWEN", "CHIRM", "RASTA", "BUTUT", "GIRTH", "DIDOS", "ANKUS", "REIFY", "ATUAS", "BOORD", "ERSES", "GRAZE", "SPITZ", "SKEOS", "EXINE", "TOTAL", "SCOFF", "CHIDE", "FOUER", "SIBYL", "GRAAL", "SKENE", "RATIO", "AIDOS", "BLIMP", "GESSO", "CHEST", "BLAMS", "ALGIN", "FLANS", "VINCA", "HOYLE", "WAFER", "PRAHU", "SORTA", "BREES", "DAWAH", "CASAS", "MAIKO", "BROND", "NAANS", "BRISE", "LOCUM", "AXONE", "CLOFF", "PISES", "NEEZE", "PRIOR", "CROWS", "RORTS", "GRUFE", "TENNY", "NOMAS", "TOUCH", "ORVAL", "MYTHI", "BUDDY", "DUROS", "LURVE", "DUNAM", "WEAVE", "REENS", "SOYAS", "SNUSH", "STIRS", "MIFFS", "CUTIS", "DOABS", "PAYEE", "HOWFF", "GRIPT", "YUKOS", "RAVED", "MATLO", "NAIFS", "SAPPY", "QORMA", "BLEAT", "FEMAL", "BANED", "BANDA", "RIVER", "SKEWS", "LOUND", "PUNJI", "SAILS", "HAITH", "SELLA", "DRUNK", "RANKE", "AWFUL", "ADIOS", "BEDYE", "WRICK", "ORFES", "FIFTY", "PEATS", "ULCER", "CRARE", "RESAW", "AUGHT", "AMNIC", "RIGID", "MILPA", "SAMAN", "SAYER", "SALMI", "BUIKS", "LITRE", "RUDES", "VIAND", "BIRSY", "GILET", "RATHE", "GUESS", "RANGE", "BAKEN", "JAUKS", "RURUS", "TICES", "ILIAC", "NIPAS", "MULSE", "UNZIP", "NOVAS", "THARM", "DIPPY", "TOOLS", "GNATS", "WOOSE", "JAGGS", "PAWED", "FAUGH", "UNDER", "SORAL", "ACYLS", "LIANE", "SAVEY", "PULER", "SORGO", "BUAZE", "BOUGE", "MANIC", "INPUT", "FAROS", "STUNT", "YLEMS", "HESPS", "CAUDA", "RAIDS", "HAROS", "SAKIS", "VALID", "GARBO", "WHIFF", "SUETS", "ETNAS", "WIGHT", "LOWLY", "SALTY", "LANKY", "LIBRI", "UNWET", "CREPY", "MURLY", "GUARS", "SWATS", "SHADS", "YENTA", "DAISY", "BRAKY", "ALUMS", "SEKOS", "GHYLL", "MOGGY", "STOWP", "MEATH", "MANGS", "AFROS", "LESTS", "NEAPS", "GRIPY", "URSAE", "REDES", "KRAUT", "GOPIK", "ROLES", "PLOTS", "JINNE", "IMIDO", "CUTER", "VOLVE", "REALM", "AMPUL", "RUANA", "FIATS", "TUANS", "VEILS", "ELITE", "OXIME", "WHISS", "SPEUG", "PECHS", "NARIC", "RUDIE", "KUTIS", "DUKED", "SYTHE", "ENOKI", "YEAST", "ALIKE", "MONTE", "TASSE", "COBBY", "IDEAL", "LEGIT", "THRIP", "KUTCH", "GLADE", "GRENZ", "SWEIR", "HOARS", "MODUS", "SCAMP", "OATER", "BALKS", "DOGMA", "SINED", "COVET", "RAPER", "DEELY", "MUSHY", "SWEET", "JAUNT", "RUBAI", "SIEVE", "LOBOS", "PYATS", "MEZZE", "KANJI", "FAVER", "SNUBS", "CRIES", "NGANA", "COGUE", "SIZAR", "BLOKE", "CENTU", "ADOPT", "EPHAS", "NABLA", "CLAPS", "ALIEN", "SOUPS", "LURCH", "TIRES", "TASAR", "HANGS", "BULSE", "FAGOT", "WHORT", "BARMY", "SPELK", "FOYER", "SKYFS", "FURRS", "JAGRA", "LEAZE", "SALAD", "MORTS", "ZONES", "UMIAC", "DATES", "USURY", "CLACK", "ROVES", "MISER", "MORRO", "REDDS", "CREDS", "TIARS", "DRIVE", "TABER", "HOMAS", "TOLES", "SOUCT", "PELFS", "GELID", "EMBUS", "VIMEN", "EMCEE", "RESTO", "BRACH", "SNEER", "PAUSE", "JOWLY", "PSYCH", "SHULN", "TEAKS", "TUBAE", "FRETS", "SLUFF", "FINER", "GOLDY", "YORKS", "PYOID", "DOWDS", "BORTZ", "GRAPH", "ALLOT", "TRULL", "FISHY", "KATIS", "BORAL", "VICED", "DOMES", "STENT", "SATAY", "NEEMB", "KIBLA", "WARBY", "SHOPS", "REBIT", "LEIRS", "FARTS", "FRIZZ", "DOLMA", "ALECS", "JOMON", "IGGED", "FAZES", "MUDIR", "ROUPY", "LEGES", "PUNNY", "OMLAH", "GUANO", "SIDES", "BOYAR", "BOARS", "BARCA", "DOLCE", "TIERS", "HELVE", "INNED", "HALTS", "ETYMA", "CURER", "CAWKS", "HEWED", "GROWL", "BONZE", "CARGO", "WAIFS", "VANES", "COMAE", "LAKHS", "KRAIT", "ICHES", "OUGHT", "REPLA", "SHOOL", "COBZA", "RUMPS", "TUTEE", "MERCS", "DEGUM", "BOTCH", "OLEIC", "CURDS", "YERBA", "URVAS", "PREVE", "FEESE", "CLOYS", "BINES", "MERDE", "LADLE", "WRYER", "BASED", "GINGE", "CLANS", "BRAZE", "WOOSH", "SNARL", "CRICK", "DUANS", "SHANK", "WADTS", "ALGID", "SOOTE", "PECKS", "GRUMP", "CAFFS", "CUTIE", "SNOTS", "WRITE", "TEMPO", "TUBBY", "FYCES", "MOITS", "ACTON", "DROOP", "RUGBY", "APTLY", "HYPER", "CAVAS", "CAAED", "CHAFT", "RAFFS", "AMAZE", "SWEES", "LAIKS", "ASSES", "MODGE", "JABOT", "THERM", "PROLE", "COALA", "FRORE", "TUMMY", "GLAUM", "TACKY", "PALLS", "CLEAN", "JEHUS", "MEANS", "KNAGS", "HALED", "LULUS", "BEADY", "RONDE", "CLOUD", "PUTTS", "BRAYS", "BLING", "WORKS", "ERGOT", "SLUNG", "BUDOS", "BAYED", "AHULL", "PALAS", "DELES", "SLYPE", "TAVER", "FRENA", "POSEY", "PROSY", "BORMS", "POUKS", "LOWES", "SAVED", "MANSE", "TAULD", "SOLON", "DEBUD", "BUOYS", "POWRE", "GOFER", "FOUNT", "IOTAS", "SCENT", "ANTES", "NADIR", "MORES", "FORAM", "ARRAH", "SPIEL", "FAQIR", "OWNED", "KANAE", "CHAYA", "CHIRR", "WIFES", "DURED", "HAGGS", "JIVED", "STUNG", "BISON", "TERRY", "TRANS", "WHEFT", "LYTED", "NUTSY", "GENOA", "BERRY", "SEANS", "TATHS", "REDON", "QADIS", "DITAS", "GASPS", "HUFFY", "WIZES", "AWNED", "PARGE", "STUNK", "GIPPO", "BOKED", "LEBEN", "FUGLY", "BEAUX", "RATCH", "REDLY", "NAGOR", "RIMES", "PYINS", "JUNKS", "HURRY", "REDOX", "BLAIN", "SECCO", "LEEPS", "GRYPT", "NUFFS", "DRAFT", "JADES", "MITES", "STERN", "KAMIS", "LYNES", "TYRES", "CAPUT", "DOILY", "WHITE", "BEVEL", "AHURU", "LUDOS", "FILMS", "NODUS", "CLAVI", "BOLIX", "BOGGY", "OUTER", "KYPES", "URALI", "FLOCK", "NOIRS", "POSIT", "CALLS", "TRIST", "DRIBS", "KIBBE", "CHADO", "TEERS", "EQUIP", "BUFFY", "FELLS", "KERNS", "SWEEP", "PLUNK", "WHALE", "COOLY", "BARRO", "URINE", "AVENS", "NERTZ", "SYPES", "VIBES", "FEATS", "OCHES", "NYAFF", "FRACT", "WIGAN", "ESSAY", "SDAYN", "VOILA", "PEDRO", "OATHS", "STOPS", "CLEVE", "ENOWS", "WYLES", "APAID", "PELON", "CARDS", "BURQA", "SIMAR", "STUFF", "VENIN", "DINES", "GEYAN", "PIGHT", "FROTH", "INFRA", "FAINS", "PUJAS", "DECKO", "MILLS", "DUPED", "GUNDY", "LIFES", "GURRY", "GUCKY", "SHULE", "SOLDO", "KABAR", "CLAWS", "RALES", "SOWCE", "VEXED", "CAUKS", "SKANK", "QUIDS", "WEARS", "OPTIC", "DASHY", "NIPPY", "GREGE", "GAUMS", "STRIG", "SEATS", "NEMPT", "SPERM", "METRE", "AWASH", "OGGIN", "SUCKY", "DAMPY", "HOWES", "EXAMS", "CEDER", "TOYON", "AMBAN", "FLAYS", "ALMUG", "BETHS", "BOVID", "YOKES", "DOODY", "BRANT", "QUEER", "QUINE", "HANTS", "PSALM", "VOCES", "THYMY", "FINAL", "RAPHE", "POULT", "TAPAS", "BUXOM", "REDAN", "GLEBE", "PRYSE", "AUMIL", "MACHI", "OMENS", "KIERS", "LENSE", "DEVIL", "CANON", "TAWED", "MAZUT", "SQUAD", "BEAST", "STEIL", "BREVE", "LUAUS", "RATER", "LIONS", "PRUNT", "KANGA", "FINCA", "BOING", "ALMUD", "CAPOS", "FOGIE", "STRAW", "PORNO", "DUMBO", "DIBBS", "SICKS", "TARRY", "KREEP", "KYBOS", "SORNS", "EXCEL", "BYRES", "THONG", "WOOFS", "SEROW", "FORBS", "JUNTA", "SIEUR", "HEJAB", "DYKED", "VINTS", "KAIAK", "LAPIS", "GYNIE", "EPHOD", "GYPPY", "CUVEE", "AGREE", "SKEGS", "HEEDS" };

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var yesAndNo = GetArrayField<KMSelectable>(comp, "Buttons", isPublic: true).Get(expectedLength: 2);
        var indexNumber = GetField<int>(comp, "WordIndex");
        var stageNumber = GetField<int>(comp, "Stage");

        foreach (var i in Enumerable.Range(0, yesAndNo.Length))    // Do not use ‘for’ loop as the loop variable is captured by a lambda
        {
            var oldInteract = yesAndNo[i].OnInteract;
            yesAndNo[i].OnInteract = delegate
            {
                wordsWritten.Add(phrases[indexNumber.Get()]);
                var result = oldInteract();
                if (stageNumber.Get() == 5 && !fldSolved.Get())
                    wordsWritten = new List<string>();
                return result;
            };
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_1000Words);

        if (wordsWritten.Count() != 5)
            throw new AbandonModuleException("Unable to gather all 5 words in 1000 Words.");

        addQuestions(module,
            makeQuestion(Question._1000WordsWords, _1000Words, formatArgs: new[] { ordinal(1) }, correctAnswers: new[] { wordsWritten[0] }, preferredWrongAnswers: phrases),
            makeQuestion(Question._1000WordsWords, _1000Words, formatArgs: new[] { ordinal(2) }, correctAnswers: new[] { wordsWritten[1] }, preferredWrongAnswers: phrases),
            makeQuestion(Question._1000WordsWords, _1000Words, formatArgs: new[] { ordinal(3) }, correctAnswers: new[] { wordsWritten[2] }, preferredWrongAnswers: phrases),
            makeQuestion(Question._1000WordsWords, _1000Words, formatArgs: new[] { ordinal(4) }, correctAnswers: new[] { wordsWritten[3] }, preferredWrongAnswers: phrases),
            makeQuestion(Question._1000WordsWords, _1000Words, formatArgs: new[] { ordinal(5) }, correctAnswers: new[] { wordsWritten[4] }, preferredWrongAnswers: phrases));
    }

    private IEnumerable<object> Process100LevelsOfDefusal(KMBombModule module)
    {
        var comp = GetComponent(module, "OneHundredLevelsOfDefusal");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_100LevelsOfDefusal);

        var display = GetArrayField<char>(comp, "displayedLetters").Get(expectedLength: 12);

        addQuestions(module, display.Select((ans, i) =>
            makeQuestion(Question._100LevelsOfDefusalLetters, _100LevelsOfDefusal, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { ans.ToString() })));
    }

    private IEnumerable<object> Process1DChess(KMBombModule module)
    {
        var comp = GetComponent(module, "OneDimensionalChessScript");
        var fldSolved = GetProperty<bool>(comp, "IsSolved", isPublic: true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_1DChess);

        var moves = GetListField<string>(comp, "souvenirPositions").Get();
        addQuestions(module, moves.Select((move, ix) =>
            makeQuestion(Question._1DChessMoves, _1DChess,
                formatArgs: new[] { new[] { "your first move", "Rustmate’s first move", "your second move", "Rustmate’s second move", "your third move", "Rustmate’s third move", "your fourth move", "Rustmate’s fourth move", "your fifth move", "Rustmate’s fifth move", "your sixth move", "Rustmate’s sixth move", "your seventh move", "Rustmate’s seventh move", "your eighth move", "Rustmate’s eighth move" }[ix] },
                correctAnswers: new[] { move })));
    }

    private IEnumerable<object> Process3DMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "ThreeDMazeModule");
        var fldIsComplete = GetField<bool>(comp, "isComplete");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var map = GetField<object>(comp, "map").Get();
        var mapData = GetField<Array>(map, "mapData").Get(arr => arr.GetLength(0) != 8 || arr.GetLength(1) != 8 ? string.Format("size {0},{1}, expected 8,8", arr.GetLength(0), arr.GetLength(1)) : null);

        IntFieldInfo fldBearing = null;
        try { fldBearing = GetIntField(map, "end_dir"); }
        catch (AbandonModuleException)
        {
            Debug.LogFormat("[Souvenir #{0}] You are running an old version of the 3D Maze module.");
            Debug.LogFormat("[Souvenir #{0}] Please UNSUBSCRIBE here: https://steamcommunity.com/workshop/filedetails/?id=752338147");
            Debug.LogFormat("[Souvenir #{0}] Please SUBSCRIBE here: https://steamcommunity.com/workshop/filedetails/?id=2164996443");
            _legitimatelyNoQuestions.Add(module);
            _showWarning = true;
            yield break;
        }

        int bearing = fldBearing.Get(min: 0, max: 3);
        var fldLabel = GetField<char>(mapData.GetValue(0, 0), "label", isPublic: true);
        var chars = new HashSet<char>();
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
            {
                var ch = fldLabel.GetFrom(mapData.GetValue(i, j));
                if ("ABCDH".Contains(ch))
                    chars.Add(ch);
            }
        var correctMarkings = chars.OrderBy(c => c).JoinString();

        while (!fldIsComplete.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_3DMaze);
        addQuestions(module,
            makeQuestion(Question._3DMazeMarkings, _3DMaze, correctAnswers: new[] { correctMarkings }),
            makeQuestion(Question._3DMazeBearing, _3DMaze, correctAnswers: new[] { new[] { "North", "East", "South", "West" }[bearing] }));
    }

    private IEnumerable<object> Process3DTapCode(KMBombModule module)
    {
        var comp = GetComponent(module, "ThreeDTapCodeScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_3DTapCode);

        var uncapitalizedWord = GetField<string>(comp, "_chosenWord").Get();
        var word = uncapitalizedWord[0] + uncapitalizedWord.Substring(1).ToLowerInvariant();
        var allWords = GetArrayField<string>(comp, "_chosenWordList").Get(expectedLength: 125).Select(x => x[0] + x.Substring(1).ToLowerInvariant()).ToArray();
        addQuestion(module, Question._3DTapCodeWord, correctAnswers: new[] { word }, preferredWrongAnswers: allWords);
    }

    private IEnumerable<object> Process3DTunnels(KMBombModule module)
    {
        var comp = GetComponent(module, "ThreeDTunnels");
        var fldSolved = GetField<bool>(comp, "_solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_3DTunnels);

        var symbols = GetStaticField<string>(comp.GetType(), "_symbols").Get();
        var targetNodeNames = GetListField<int>(comp, "_targetNodes")
            .Get(tns => tns.Any(tn => tn < 0 || tn >= symbols.Length) ? "invalid symbols" : null)
            .Select(tn => symbols[tn].ToString())
            .ToArray();
        addQuestions(module, targetNodeNames.Select((tn, ix) => makeQuestion(Question._3DTunnelsTargetNode, _3DTunnels, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { tn }, preferredWrongAnswers: targetNodeNames)));
    }

    private IEnumerable<object> Process3LEDs(KMBombModule module)
    {
        var comp = GetComponent(module, "ThreeLEDsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_3LEDs);

        var initialStates = GetArrayField<bool>(comp, "initialStates").Get(expectedLength: 3);
        addQuestion(module, Question._3LEDsInitialState, correctAnswers: new[] { initialStates.Select(s => s ? "on" : "off").JoinString("/") });
    }

    private IEnumerable<object> Process7(KMBombModule module)
    {
        var comp = GetComponent(module, "SevenHandler");
        var isSolved = false;
        module.OnPass += delegate { isSolved = true; return false; };

        while (!isSolved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_7);

        var allDisplayedValues = GetListField<int[]>(comp, "displayedValues")
            .Get(stg => stg.Any(a => a.Length != 3) ? "at least 1 stage’s array does not have exactly a length of 3" : null);

        // Check if all of the stages have exactly 3 sets of values.
        var allIdxDisplayedOperators = GetListField<int>(comp, "idxOperations").Get(
            idx => !idx.Skip(1).All(a => a >= 0 && a <= 3) ? "After stage 0, at least 1 stage does not have a valid index between 0 and 3 inclusive" : // Check after stage 0 if all indexes are within 0-3 inclusive
            !(idx.First() == -1) ? "Stage 0 does not have an index of -1." : // Then check if stage 0 has an idx of -1.
            null);

        var allQuestions = new List<QandA>();

        var colorReference = new[] { "red", "green", "blue", "white" };

        for (var x = 0; x < allDisplayedValues.Count; x++)
        {
            if (x == 0) // Stage 0 is denoted as the initial stage on this module.
            {
                for (int y = 0; y < 3; y++)
                    allQuestions.Add(makeQuestion(Question._7InitialValues, _7, formatArgs: new[] { colorReference[y] }, correctAnswers: new[] { allDisplayedValues[x][y].ToString() }));
            }
            else
                allQuestions.Add(makeQuestion(Question._7LedColors, _7, formatArgs: new[] { x.ToString() }, correctAnswers: new[] { colorReference[allIdxDisplayedOperators[x]] }, preferredWrongAnswers: colorReference));
        }

        addQuestions(module, allQuestions.ToArray());
    }

    private IEnumerable<object> Process9Ball(KMBombModule module)
    {
        var comp = GetComponent(module, "NineBallScript");
        var potted = GetField<bool[]>(comp, "Potted");

        while (potted.Get().Count(x => x) != 9)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_9Ball);

        var balls = GetArrayField<int>(comp, "RndBallNums").Get(expectedLength: 7);

        addQuestions(module,
            makeQuestion(Question._9BallLetters, _9Ball, formatArgs: new[] { "A" }, correctAnswers: new[] { (balls[0] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, _9Ball, formatArgs: new[] { "B" }, correctAnswers: new[] { (balls[1] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, _9Ball, formatArgs: new[] { "C" }, correctAnswers: new[] { (balls[2] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, _9Ball, formatArgs: new[] { "D" }, correctAnswers: new[] { (balls[3] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, _9Ball, formatArgs: new[] { "E" }, correctAnswers: new[] { (balls[4] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, _9Ball, formatArgs: new[] { "F" }, correctAnswers: new[] { (balls[5] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, _9Ball, formatArgs: new[] { "G" }, correctAnswers: new[] { (balls[6] + 1).ToString() }),
            makeQuestion(Question._9BallNumbers, _9Ball, formatArgs: new[] { "2" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 1)] }),
            makeQuestion(Question._9BallNumbers, _9Ball, formatArgs: new[] { "3" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 2)] }),
            makeQuestion(Question._9BallNumbers, _9Ball, formatArgs: new[] { "4" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 3)] }),
            makeQuestion(Question._9BallNumbers, _9Ball, formatArgs: new[] { "5" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 4)] }),
            makeQuestion(Question._9BallNumbers, _9Ball, formatArgs: new[] { "6" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 5)] }),
            makeQuestion(Question._9BallNumbers, _9Ball, formatArgs: new[] { "7" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 6)] }),
            makeQuestion(Question._9BallNumbers, _9Ball, formatArgs: new[] { "8" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 7)] }));
    }
}