using System;
using System.Collections.Generic;
using static Souvenir.Translation_ru.Conjugation;

namespace Souvenir;

public class Translation_ru : TranslationBase<Translation_ru.TranslationInfo_ru>
{
    public sealed class TranslationInfo_ru : TranslationInfo
    {
        public Conjugation Conjugation = в_PrepositiveMascNeuter;
    }

    public enum Conjugation
    {
        // The preposition в is automatically added in front of the module name, so omit it from the question text
        в_PrepositiveMascNeuter,
        в_PrepositiveFeminine,
        в_PrepositivePlural,

        // The preposition во is automatically added in front of the module name, so omit it from the question text
        во_PrepositiveMascNeuter,
        во_PrepositiveFeminine,
        во_PrepositivePlural,

        // No preposition is automatically added in front of the module name, so include it in the question text
        PrepositiveMascNeuter,
        PrepositiveFeminine,
        PrepositivePlural,
        NominativeMasculine,
        NominativeNeuter,
        NominativeFeminine,
        NominativePlural,
        GenitiveMascNeuter,
        GenitiveFeminine,
        GenitivePlural,
        AccusativeMascNeuter,
        AccusativeFeminine,
        AccusativePlural,
        InstrumentalMascNeuter,
        InstrumentalFeminine,
        InstrumentalPlural,
        DativeMascNeuter,
        DativeFeminine,
        DativePlural,
    }

    public override string FormatModuleName(SouvenirHandlerAttribute handler, bool addSolveCount, int numSolved) =>
        _translations.Get(handler.EnumType) is not TranslationInfo_ru tr ? base.FormatModuleName(handler, addSolveCount, numSolved) :
        addSolveCount ? tr.Conjugation switch
        {
            NominativeMasculine => $"{Ordinal(numSolved)}-й решённый {tr.ModuleName}",
            NominativeFeminine => $"{Ordinal(numSolved)}-я решённая {tr.ModuleName}",
            NominativeNeuter => $"{Ordinal(numSolved)}-е решённое {tr.ModuleName}",
            NominativePlural => $"{Ordinal(numSolved)}-е решённые {tr.ModuleName}",
            GenitiveMascNeuter => $"{Ordinal(numSolved)}-го решённого {tr.ModuleName}",
            GenitiveFeminine => $"{Ordinal(numSolved)}-й решённой {tr.ModuleName}",
            GenitivePlural => $"{Ordinal(numSolved)}-х решённых {tr.ModuleName}",
            PrepositiveMascNeuter => $"{Ordinal(numSolved)}-м решённом {tr.ModuleName}",
            PrepositiveFeminine => $"{Ordinal(numSolved)}-й решённой {tr.ModuleName}",
            PrepositivePlural => $"{Ordinal(numSolved)}-х решённых {tr.ModuleName}",
            InstrumentalMascNeuter => $"{Ordinal(numSolved)}-м решённым {tr.ModuleName}",
            InstrumentalFeminine => $"{Ordinal(numSolved)}-й решённой {tr.ModuleName}",
            InstrumentalPlural => $"{Ordinal(numSolved)}-ми решёнными {tr.ModuleName}",
            DativeMascNeuter => $"{Ordinal(numSolved)}-му решённому {tr.ModuleName}",
            DativeFeminine => $"{Ordinal(numSolved)}-й решённой {tr.ModuleName}",
            DativePlural => $"{Ordinal(numSolved)}-м решённым {tr.ModuleName}",
            в_PrepositiveMascNeuter or во_PrepositiveMascNeuter => $"{(numSolved == 2 ? "во" : "в")} {Ordinal(numSolved)}-м решённом {tr.ModuleName}",
            в_PrepositiveFeminine or во_PrepositiveFeminine => $"{(numSolved == 2 ? "во" : "в")} {Ordinal(numSolved)}-й решённой {tr.ModuleName}",
            в_PrepositivePlural or во_PrepositivePlural => $"{(numSolved == 2 ? "во" : "в")} {Ordinal(numSolved)}-х решённых {tr.ModuleName}",
            _ => throw new InvalidOperationException($"Unknown conjugation: {tr.Conjugation}")
        } :
        tr.Conjugation switch
        {
            в_PrepositiveMascNeuter or в_PrepositiveFeminine or в_PrepositivePlural => $"в {tr.ModuleName}",
            во_PrepositiveMascNeuter or во_PrepositiveFeminine or во_PrepositivePlural => $"во {tr.ModuleName}",
            _ => tr.ModuleName,
        };

    public override string Ordinal(int number) => number.ToString();

    protected override Dictionary<Type, TranslationInfo_ru> _translations => new()
    {
        #region Translatable strings
        #endregion
    };

    public override string[] IntroTexts => Ut.NewArray(
        // Russian translations of English-language quotes
        "Я вижу мёртвых сапёров.",     // “I see dead people.” (Sixth Sense)
        "Добро пожаловать... в настоящую бомбу.",     // “Welcome... to the real world.” (The Matrix)
        "Я собираюсь сделать бомбу, которую он не сможет обезвредить.",   // “I’m gonna make him an offer he can’t refuse.” (The Godfather)
        "Луис, я думаю, это начало прекрасного взрыва.",   // “Louis, I think this is the beginning of a beautiful friendship.” (Casablanca)
        "Эй. Я мог бы обезвредить эту бомбу ровно за десять секунд.",   // “Hey. I could clear the sky in ten seconds flat.” (MLP:FiM, Friendship is Magic - Part 1)
        "Да пребудет с тобой бомба.",    // “May the Force be with you.” (Star Wars IV: A New Hope)
        "Люблю запах взрывов по утрам.",   // “I love the smell of napalm in the morning.” (Apocalypse Now)
        "Алло? Да, я сейчас обезвреживаю бомбу.",    // “E.T. phone home.” (E.T. the Extra-Terrestrial)
        "Бомб. Джеймс Бомб.",    // “Bond. James Bond.” (Dr. No / James Bond series)
        "Бомба тебе не по зубам!",   // “You can’t handle the truth!” (A Few Good Men)
        "Тебе понадо- бится бомба побольше.", // “You’re gonna need a bigger boat.” (Jaws)
        "Бомбы – это как коробка шоколадных конфет. Никогда не знаешь, что попадётся.",    // “My mom always said life was like a box of chocolates. You never know what you’re gonna get.” (Forrest Gump)
        "Хьюстон, у нас бомба.",   // “Houston, we have a problem.” (Apollo 13)
        "Элементарно, мой дорогой эксперт.",  // “Elementary, my dear Watson.” (Sherlock Holmes) (misquote)
        "Забудь об этом, Джейк, это КТАНЕ.",     // “Forget it, Jake, it’s Chinatown.” (Chinatown)
        "Я всегда полагался на компетент- ность экспертов.",    // “I’ve always depended on the kindness of strangers.” (A Streetcar Named Desire)
        "Бомба. Взорванная, а не обезвреженная.",   // “A martini. Shaken, not stirred.” (Diamonds Are Forever (novel) / James Bond)
        "Ябба- дабба- бум!",    // “Yabba dabba doo!” (Flintstones)
        "Эта бомба взорвётся через пять секунд.",    // “This tape will self-destruct in five seconds.” (Mission: Impossible)
        "Обезвре- живание бесполезно.",  // “Resistance is futile.” (Star Trek: The Next Generation)
        "Это твой окончатель- ный ответ?",   // direct quote (Who Wants to be a Millionaire?)
        "Лучший друг бомбы – это её сапёр.", // “A man’s best friend is his dog.” (attorney George Graham Vest, 1870 Warrensburg)
        "Держи своих экспертов близко, но свою бомбу – ещё ближе.",   // “Keep your friends close and your enemies closer.” (The Prince / Machiavelli)
        "Пристегните ремни безопасности. Это будет взрывная ночь.",   // “Fasten your seat belts, it’s going to be a bumpy night.” (All About Eve)
        "Ты сапёр, Гарри.", // “You’re a wizard, Harry.” (Harry Potter and the Philosopher’s Stone)
        "Либо ты умираешь сапёром, либо живёшь до тех пор, пока не становишься экспертом.", // “Well, I guess you either die a hero or you live long enough to see yourself become the villain.” (The Dark Knight)
        "Это не обезврежи- вание. Это взрыв... со стилем.",    // “This isn’t flying. This is falling... with style.” (Toy Story)
        "Вы что, перепутали провода?", // “Have you got your lions crossed?” (The Lion King)
        "Не перепутай провода.",   // “Don’t cross the streams.” (Ghostbusters)
        "Хотите услышать самый мощный и громкий взрыв в мире?", // “Wanna hear the most annoying sound in the world?” (Dumb & Dumber)
        "Руковод- ства? Там, куда мы идём, они нам не нужны.",   // “Roads? Where we’re going, we don’t need roads.” (Back to the Future)
        "Первое правило обезвреживания заключается в том, что вы продолжаете говорить об обезвреживании.",    // “The first rule of Fight Club is, you don’t talk about Fight Club.” (Fight Club)
        "Мы обезвреживаем бомбы.",  // “We rob banks.” (Bonnie and Clyde)
        "Кто-то подложил нам бомбу.",  // direct quote (Zero Wing)
        "Люк, я твой эксперт.", // “Luke, I am your father.” (Star Wars V: The Empire Strikes Back) (misquote)
        "Она должна быть примерно на 20% более взрывоопасной.", // “It needs to be about 20 percent cooler.” (MLP:FiM, Suited for Success)
        "То же самое, что мы делаем каждый вечер, эксперт. Попробуем обезвредить бомбу!", // “The same thing we do every night, Pinky. Try to take over the world!” (Pinky and the Brain)
        "Кто-нибудь заказывал жареного сапёра?", // “Anybody order fried sauerkraut?” (Once Upon a Time in Hollywood)
        "У меня есть несколько сапёров, которых нужно разнести в пух и прах!", // “I’ve got some children I need to make into corpses!” (Gravity Falls, Weirdmageddon 3: Take Back The Falls)
        "Я – неизбеж- ность.", // direct quote (Avengers: Endgame)
        "Бойтесь, бегите! Бомбы все равно взорвутся.", // “Dread it, run from it, destiny still arrives.” (Avengers: Infinity War)
        "Это прекрасная вещь – детонация бомб.", // “It’s a beautiful thing, the destruction of words.” (1984)
        "Кто-то считает себя слишком умным для меня. Они все так думают поначалу.", // Someone thinks they’re too clever for us. They all think that at first. (Invincible)

        // Specific to Russian culture
        "Хочешь, я взорву все бомбы, что мешают спать?",  // Хочешь я взорву все звёзды, что мешают спать? — Song, Земфира - Хочешь?
        "И у бомбы нашей села батарейка.",  // И у любви нашей села батарейка. — Song, Жуки - Батарейка
        "Какая гадость, какая гадость эта ваша бомба...",  // Какая гадость, какая гадость эта ваша заливная рыба... — Movie "Ирония судьбы, или С лёгким паром!"
        "Научиться бы не взрываться по пустякам.",  // Научиться бы не париться по пустякам. — Song, Градусы - Научиться бы не париться
        "Астрологи объявили неделю бомб. Количество взрывов увеличилось вдвое.",  // Астрологи объявили неделю Глицина. Количество Y увеличилось вдвое. — Game, Heroes of Might and Magic III
        "Охлади своё взрывание.",  // Охлади своё трахание. — Game, bootleg GTA translation
        "Он взорвётся скоро, надо только ждать.",  // Он наступит скоро, надо только ждать. — Song, Егор Летов - Всё идет по плану
        "Мой серийный номер – на рукаве.",  // Мой порядковый номер - на рукаве. — Song, Кино - Группа крови
        "Экспертов нет, но вы держитесь, всего доброго.",  // Денег нет, но вы держитесь, всего доброго. — Dmitry Medvedev's infamous speech
        "Ух, бомба-то какая! Лепота!",  // Ух, красота-то какая! Лепота! — Movie "Иван Васильевич меняет профессию"
        "Семь раз отмерь, один отрежь.",  // Семь раз отмерь, один отрежь. — Russian proverb
        "Надо, сапёр, надо!",  // Надо, Федя, надо! — Movie "Операция Ы"
        "Может быть, тебе дать ещё ключ от квартиры, где руководства лежат?",  // Может быть, тебе дать ещё ключ от квартиры, где деньги лежат? — Movie "12 стульев"
        "Всё... Взрыва не будет... Электричество кончилось.",  // Всё... Кина не будет... Электричество кончилось. — Movie "Брилиантовая рука"
        "Сапёр ошибается только один раз.",  // Сапёр ошибается только один раз. — Common saying/Joke
        "Я надеюсь, что я не пострадаю.",  // Я надеюсь, что я не пострадаю. — Internet meme
        "Ты... не взрывайся, если что!...",  // Ты... заходи, если что!... — Movie "Жил был пёс"
        "Решение видишь? Вот и я не вижу. А оно есть.",  // Суслика видишь? Вот и я не вижу. А он есть. — Movie "ДМБ"
        "Шах и мат, эксперты!",  // Шах и мат, атеисты! — Internet meme
        "Хороший модуль. Решать я его, конечно, не буду.",  // Сильное заявление. Проверять его я, конечно, не буду. — Show, Необъяснимо, но факт
        "Ты кнопку нажал, должен был 6 секунд держать! Почему так мало?",  // Ты на пенёк сел, должен был косарь отдать! Почему так мало? — Internet meme
        "Слово «бомба» и слово «смерть» для вас означают одно и то же.",  // Слово "Ром" и слово "Смерть" для вас означают одно и то же. — Movie, Остров сокровищ
        "Неправильно, попробуй ещё раз.",  // Неправильно, попробуй ещё раз. — Internet meme
        "Сапёр хороший, эксперты плохие.",  // Царь хороший, бояре плохие. — Political catchphrase
        "Стар- туем!",  // Стартуем! — Internet meme
        "Неправильно ты, дядя Фёдор, бомбу обезвреживаешь.",  // Неправильно ты, дядя Фёдор, бутерброд ешь. — Cartoon "Простоквашино"
        "Я почему вредный был? Потому что у меня инструкций не было!",  // Я почему вредный был? Потому что у меня велосипеда не было! — Cartoon "Простоквашино"
        "У меня есть мысль, и я её думаю.",  // У меня есть мысль, и я её думаю. — Cartoon, 38 попугаев
        "Ну, бомба! Ну, погоди!",  // Ну, заяц! Ну, погоди! — Cartoon "Ну, погоди!"
        "Чудо враждебной техники!",  // Чудо враждебной техники! — Movie "Тайна третьей планеты"
        "Нельзя просто так взять, и обезвредить бомбу.",  // Нельзя просто так взять и войти в Мордор. — Meme from "The Lord of the Rings"
        "Я в своём обезврежи- вании настолько преисполнился.",  // Я в своём познании настолько преисполнился. — Internet meme
        "Бомба замини- рована.",  // Тапок заминирован — Internet meme
        "Это бомба, братан.",  // Это фиаско, братан. — Internet meme
        "Выпьем за бомбу!",  // Выпьем за любовь! — Toast phrase/Song Игорь Николаев - Выпьем за любовь
        "Укуси меня бомба!",  // Укуси меня пчела! — Cartoon "Смешарики"
        "Взорвать нельзя обезвредить.",  // Казнить нельзя помиловать. — Famous grammatical exercise
        "Египет- ская бомба!",  // Египетская сила! — Sitcom, Воронины
        "И мы взорваны!",  // И мы счастливы! — Sitcom, Счастливы вместе
        "Всё поймать стремится. Бомбу!",  // Всё поймать стремится молнию! — Song, КиШ - Дурак и молния
        "Что нас ждёт, бомба хранит молчанье.",  // Что нас ждёт, море хранит молчанье. — Song, Ария - Штиль
        "Сапёр ли я дрожащий или право имею...",  // Тварь ли я дрожащая или право имею... — Novel, Преступление и наказание (Фёдор Достоевский)
        "Порядок у модулей в киоске был взят.",  // Порядок у карт в киоске был взят — Internet meme
        "Короче, Сапёр. Я всё обезвредил, и в благородство играть не буду.",  // Короче, Меченый, я тебя спас и в благородство играть не буду. — Game, Stalker reference
        "Вы допустили потерю дорогостоящего модуля!",  // Вы допустили потерю дорогостоящего обмундирования! — Game, Fallout 2
        "Не брат ты мне, сапёр.",  // Не брат ты мне, гнида черножопая. — Movie, Брат
        "А жаренных проводов не хочешь?",  // А жаренных гвоздей не хочешь? — Cartoon, TMNT
        "Невежда обезвреживает бомбы руками, а мастер – силой своего духа.",  // Невежда передвигает предметы руками, а мастер - силой своего духа. — Game, Gothic, telekinesis reference
        "Незабудка – твой любимый цветок.",  // Незабудка – твой любимый цветок. — Pop song, Тима Белорусских - Незабудка
        "Эксперт, у нас босс. Возможно Сувенир. По коням!",  // Андрюх, у нас труп. Возможно криминал. По коням! — Series, Улицы разбитых фонарей
        "Бомба слабее торпеды и ракеты, но в цирке бомба не выступает."  // Волк слабее льва и тигра, но в цирке Волк не выступает. — Internet meme
    );
}
