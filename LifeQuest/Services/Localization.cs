using System.ComponentModel;

namespace LifeQuest.Services;

public sealed class I18n : INotifyPropertyChanged
{
    public static I18n Instance { get; } = new();

    private string _lang = "uk";
    public string Lang => _lang;

    public event PropertyChangedEventHandler? PropertyChanged;

    private static readonly Dictionary<string, Dictionary<string, string>> Dict = new()
    {
        ["en"] = new()
        {
            ["tagline"] = "Turn your routine into an adventure",
            ["appName"] = "LifeQuest",
            ["login"] = "Login", ["register"] = "Register", ["signIn"] = "Sign in", ["createHero"] = "Create hero",
            ["username"] = "Username", ["password"] = "Password", ["repeatPassword"] = "Repeat password",
            ["quests"] = "Quests", ["stats"] = "Stats", ["leaderboard"] = "Leaderboard", ["settings"] = "Settings",
            ["admin"] = "Review Queue", ["dev"] = "Dev Mode", ["logout"] = "Log out",
            ["questBoard"] = "Quest Board", ["questBoardSub"] = "Complete quests, submit proof, earn XP",
            ["newQuest"] = "New Quest", ["all"] = "All", ["active"] = "Active", ["pending"] = "In review", ["done"] = "Completed",
            ["noQuests"] = "No quests yet. Create your first and begin the journey!",
            ["dailyLeft"] = "Quests left today", ["dailyLimitReached"] = "Daily limit reached (10 quests/day)",
            ["title"] = "Title", ["description"] = "Description", ["difficulty"] = "Difficulty",
            ["easy"] = "Easy", ["medium"] = "Medium", ["hard"] = "Hard",
            ["titlePh"] = "e.g. Do morning workout", ["descPh"] = "Optional details",
            ["cancel"] = "Cancel", ["create"] = "Create", ["save"] = "Save",
            ["complete"] = "Complete", ["delete"] = "Delete",
            ["reward"] = "Reward", ["xpGained"] = "XP gained",
            ["proofTitle"] = "Quest Proof", ["proofFull"] = "Attach a photo or video and earn",
            ["proofWarn"] = "Without proof, XP is cut to a third", ["xp"] = "XP",
            ["pickFile"] = "Choose a file…", ["submitProof"] = "Submit proof", ["skipProof"] = "Skip (×⅓ XP)",
            ["proofSent"] = "Proof submitted for review",
            ["questAdded"] = "Quest added!", ["questDeleted"] = "Quest deleted",
            ["statLevel"] = "Level", ["statRank"] = "Rank", ["statTotalXp"] = "Total XP earned",
            ["statQuests"] = "Quests completed", ["statProofs"] = "Proofs approved", ["statPrestige"] = "Prestige",
            ["statMult"] = "XP Multiplier", ["statToNext"] = "To next level", ["max"] = "MAX",
            ["prestigeBtn"] = "REBIRTH (Prestige) — reset level, +0.25 multiplier forever",
            ["prestigeDone"] = "Rebirth complete! Multiplier is now",
            ["reviewTitle"] = "Review Queue", ["reviewSub"] = "Approve players' proofs to grant full XP",
            ["queueEmpty"] = "Queue is empty — all proofs reviewed!",
            ["player"] = "Player", ["approve"] = "Approve", ["reject"] = "Reject",
            ["approved"] = "Proof approved", ["rejected"] = "Proof rejected",
            ["devTitle"] = "Developer Mode", ["devUser"] = "Player", ["resetProfile"] = "Reset profile",
            ["grantXp"] = "Grant XP", ["grantLevel"] = "Grant levels", ["setLevel"] = "Set level",
            ["lvlUp"] = "LEVEL UP!", ["newRank"] = "New Rank!",
            ["settingsTitle"] = "Settings", ["language"] = "Language", ["avatar"] = "Avatar",
            ["theme"] = "Theme", ["themeLight"] = "Light", ["themeDark"] = "Dark",
            ["changeAvatar"] = "Change avatar", ["avatarHint"] = "PNG, JPG or GIF",
            ["avatarUpdated"] = "Avatar updated", ["langEn"] = "English", ["langUk"] = "Ukrainian",
            ["account"] = "Account", ["devBadge"] = "DEV", ["loggedAs"] = "Logged in as",
            ["database"] = "Database", ["connString"] = "Connection string", ["testConn"] = "Test connection",
            ["connOk"] = "Connection successful", ["connFail"] = "Connection failed",
            ["passwordsMismatch"] = "Passwords do not match",
            ["usernameShort"] = "Username too short (min 2)", ["passwordShort"] = "Password too short (min 4)",
            ["userTaken"] = "Username already taken", ["badCredentials"] = "Invalid username or password",
            ["errorTitle"] = "Error", ["confirmDelete"] = "Delete this quest?",
            ["welcome"] = "Welcome"
        },
        ["uk"] = new()
        {
            ["tagline"] = "Перетвори рутину на пригоду",
            ["appName"] = "LifeQuest",
            ["login"] = "Вхід", ["register"] = "Реєстрація", ["signIn"] = "Увійти", ["createHero"] = "Створити героя",
            ["username"] = "Нікнейм", ["password"] = "Пароль", ["repeatPassword"] = "Повтори пароль",
            ["quests"] = "Квести", ["stats"] = "Статистика", ["leaderboard"] = "Таблиця лідерів", ["settings"] = "Налаштування",
            ["admin"] = "Перевірка", ["dev"] = "Реж. розробника", ["logout"] = "Вийти",
            ["questBoard"] = "Дошка квестів", ["questBoardSub"] = "Виконуй квести, надсилай пруф, отримуй XP",
            ["newQuest"] = "Новий квест", ["all"] = "Усі", ["active"] = "Активні", ["pending"] = "На перевірці", ["done"] = "Завершені",
            ["noQuests"] = "Поки немає квестів. Створи перший і вирушай у подорож!",
            ["dailyLeft"] = "Залишилось квестів сьогодні", ["dailyLimitReached"] = "Денний ліміт досягнуто (10 квестів/день)",
            ["title"] = "Назва", ["description"] = "Опис", ["difficulty"] = "Складність",
            ["easy"] = "Легкий", ["medium"] = "Середній", ["hard"] = "Складний",
            ["titlePh"] = "напр. Зробити зарядку", ["descPh"] = "Деталі (необов'язково)",
            ["cancel"] = "Скасувати", ["create"] = "Створити", ["save"] = "Зберегти",
            ["complete"] = "Виконати", ["delete"] = "Видалити",
            ["reward"] = "Нагорода", ["xpGained"] = "XP отримано",
            ["proofTitle"] = "Підтвердження квесту", ["proofFull"] = "Прикріпи фото або відео й отримай",
            ["proofWarn"] = "Без пруфу XP ріжеться втричі", ["xp"] = "XP",
            ["pickFile"] = "Вибрати файл…", ["submitProof"] = "Надіслати пруф", ["skipProof"] = "Пропустити (×⅓ XP)",
            ["proofSent"] = "Пруф надіслано на перевірку",
            ["questAdded"] = "Квест додано!", ["questDeleted"] = "Квест видалено",
            ["statLevel"] = "Рівень", ["statRank"] = "Ранг", ["statTotalXp"] = "Усього XP зароблено",
            ["statQuests"] = "Квестів виконано", ["statProofs"] = "Пруфів прийнято", ["statPrestige"] = "Престиж",
            ["statMult"] = "Множник XP", ["statToNext"] = "До наступного рівня", ["max"] = "МАКС",
            ["prestigeBtn"] = "ПЕРЕРОДЖЕННЯ (Prestige) — скинути рівень, +0.25 до множника назавжди",
            ["prestigeDone"] = "Переродження завершено! Множник тепер",
            ["reviewTitle"] = "Черга перевірки", ["reviewSub"] = "Підтверджуй пруфи гравців, щоб нарахувати повний XP",
            ["queueEmpty"] = "Черга порожня — усі пруфи перевірені!",
            ["player"] = "Гравець", ["approve"] = "Підтвердити", ["reject"] = "Відхилити",
            ["approved"] = "Пруф підтверджено", ["rejected"] = "Пруф відхилено",
            ["devTitle"] = "Режим розробника", ["devUser"] = "Гравець", ["resetProfile"] = "Скинути профіль",
            ["grantXp"] = "Видати XP", ["grantLevel"] = "Видати рівні", ["setLevel"] = "Встановити рівень",
            ["lvlUp"] = "НОВИЙ РІВЕНЬ!", ["newRank"] = "Новий ранг!",
            ["settingsTitle"] = "Налаштування", ["language"] = "Мова", ["avatar"] = "Аватар",
            ["theme"] = "Тема", ["themeLight"] = "Світла", ["themeDark"] = "Темна",
            ["changeAvatar"] = "Змінити аватар", ["avatarHint"] = "PNG, JPG або GIF",
            ["avatarUpdated"] = "Аватар оновлено", ["langEn"] = "English", ["langUk"] = "Українська",
            ["account"] = "Акаунт", ["devBadge"] = "DEV", ["loggedAs"] = "Ви увійшли як",
            ["database"] = "База даних", ["connString"] = "Рядок підключення", ["testConn"] = "Перевірити підключення",
            ["connOk"] = "Підключення успішне", ["connFail"] = "Помилка підключення",
            ["passwordsMismatch"] = "Паролі не збігаються",
            ["usernameShort"] = "Нікнейм закороткий (мін. 2)", ["passwordShort"] = "Пароль закороткий (мін. 4)",
            ["userTaken"] = "Такий нікнейм вже зайнятий", ["badCredentials"] = "Невірний нікнейм або пароль",
            ["errorTitle"] = "Помилка", ["confirmDelete"] = "Видалити цей квест?",
            ["welcome"] = "Вітаємо"
        }
    };

    public string this[string key] => T(key);

    public string T(string key)
    {
        if (Dict.TryGetValue(_lang, out var d) && d.TryGetValue(key, out var v)) return v;
        if (Dict["en"].TryGetValue(key, out var e)) return e;
        return key;
    }

    public void SetLang(string lang)
    {
        if (!Dict.ContainsKey(lang) || lang == _lang) return;
        _lang = lang;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Lang)));
        LangChanged?.Invoke();
    }

    public event Action? LangChanged;
}
