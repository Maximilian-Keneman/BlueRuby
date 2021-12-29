using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueRuby
{
    public static class Default
    {
        public static Localization Localization => new Localization("Russian", new Dictionary<string, string>()
        {
            { "MainForm", "Синий рубин_Быстрая игра_Начать игру_Настройки_Выход" },
            { "StartForm", "Режим игры_Добавить игрока_Составление колоды_Составление колоды энергии_Сохранить параметры старта_Удалить параметры старта_Загрузить_Старт" },
            { "Gamemodes", "Игрок против Игрока_Команда против Команды" },
            { "NullPlayersError", "Нельзя играть без игроков." },
            { "UnevenCountError", "Необходимо чётное число игроков." },
            { "PlayerVSPlayerError", "При игре \"Игрок против Игрока\" допустимо только 2 игрока." },
            { "EmptyNameError", "Пустое имя не приниматеся." },
            { "RepeatNamesError", "Имя \"{}\" используется {} раз." },
            { "InvalidCharError", "Имя не может содержать символ {}." },
            { "Teams", "Случайная_Свет_Тьма" },
            { "LightTeamCountError", "В команде Света слишком много участников. Лишних {}." },
            { "DarkTeamCountError", "В команде Тьмы слишком много участников. Лишних {}." },
            { "Cards", "5 золота_10 золота_Заклинание Магический огонь_Щит Тайгарол_Дракон Хирагир_Колдун" },
            { "LiveCompilation", "Стартовое кол-во энергии_Кол-во энергии в колоде" },
            { "SaveLoadForm", "Сохранение_Загрузка_Сохранить_Загрузить_Новое сохранение_Имя_Дата_Отмена" },
            { "LoadSaveQuestion", "Вы хотите сохранить игру перед загрузкой?" },
            { "ExitSaveQuestion", "Вы хотите сохранить игру перед выходом?" },
            { "GameMenu", "Меню_Сохранить_Загрузить_Выход" },
            { "DiceForm", "Кубик_Бросить кубик" },
            { "UseDragon", "Использовать Дракона" },
            { "UseMagicFire", "Использовать Магический Огонь" },
            { "Interactives", "Башня магии_Крепость_Подземелье_Корабль" },
            { "NoMoney", "У Вас не хватает золота для покупки." },
            { "NoSmallMoney", "У Вас не хватает мелочи." },
            { "MagicTowerQuestion", "Вы хотите купить дополнительную энергию за 10 золота?" },
            { "FortressQuestion", "Вы хотите приобрести карту?" },
            { "CatacombQuestion", "Вы хотите войти в подземелье?" },
            { "CatacombMoneyQuestion", "Вы хотите заплатить 20 золота и сразу выйти из подземелья?" },
            { "ShipQuestion", "Вы хотите воспользоваться услугами корабля за 15 золота?" },
            { "DeckLose", "В колоде не осталось карт." },
            { "MagicTowerNoDoubleEnter", "Нельзя посещать одну и ту же башню магии дважды подряд. Идите в другую башню." },
            { "FortressNoDoubleEnter", "Нельзя посещать одну и ту же крепость дважды подряд. Идите в другую крепость." },
            { "NoDoubleWizard", "Нельзя иметь больше одного колдуна на игрока, а других карт нет." },
            { "PlayerAttack", "Атака" },
            { "PlayerCanAttack", "Вы можете атаковать {}. Вы хотите вступить в бой?" },
            { "PlayerLoseTurn", "{} пропускает ход." },
            { "TeamWin", "Команда {} победила!" }
        });

        public static Deckmode Deckmode => new Deckmode("Standart",
                                                        new Dictionary<Type, int>()
                                                        {
                                                            { typeof(DragonKhiragir), 3 },
                                                            { typeof(WizardCard), 2 },
                                                            { typeof(SpellMagicFire), 2 },
                                                            { typeof(ShieldTaygarol), 3 },
                                                            { typeof(TenGoldCard), 6 },
                                                            { typeof(FiveGoldCard), 12 }
                                                        }, 2, 18);

        public static Settings.QuickStartParams StartParams =>
            new Settings.QuickStartParams(
                new string[]
                {
                    "Гендальф",
                    "Бело"
                },
                new string[]
                {
                    "Вурдалак",
                    "Саурон",
                    "Черно"
                },
                2, Deckmode, true);
    }
}