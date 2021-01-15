/// <summary>
/// Contenedor de las llaves para traducir dependiendo del <see cref="Idiom"/>
/// </summary>
public enum TKey
{
    No = -1, // en caso de que tengamos que declarar que no hay

    // IDIOM
    _IDIOM,
#region Buttons
    // BUTTONS
    _0_PLAY,
    _1_INTRODUCTION,
    _2_ACHIEVEMENTS,
    _3_OPTIONS,
    _4_EXIT,
    _5_BACK,
    _6_LANGUAGE,
    _7_TEXTSPEED,
    _8_MUSIC,
    _9_SOUND,
    _10_CONTROLS,
    _11_TUTORIAL,
    _12_MANUAL,
    _13_CREDITS,
    #endregion
    #region Achievements
    //Achievements
    // P1 TODO
    ACHIEVE_KILLS_ROBOT,
    ACHIEVE_KILLS_BOSS,
    ACHIEVE_WAVES_ENEMIES,
    ACHIEVE_OBJECTS_COLLECTED,
    ACHIEVE_HEALS_GAME,
    // P2
    ACHIEVE_TIME_DEATHLIMIT,
    ACHIEVE_METTERS_GAME,
    ACHIEVE_CREATIONS_GAME,
    ACHIEVE_ROBOTS_ALIVE,
    ACHIEVE_ESPECIAL_READ,

#endregion
#region Messages, del menú
    //Messages (Mensajes del cyborg...)
    MSG_CYBORG_WHERE,
    MSG_CYBORG_UNKNOW,
#endregion
#region Messages, de Opciones
    //Messages Opt (Mensajes de las opciones
    MSG_OPT_LANGUAGE,
    MSG_OPT_TEXTSPEED,
    MSG_OPT_MUSIC,
    MSG_OPT_SOUND,
    MSG_OPT_CONTROLS,
    MSG_OPT_BACK,

    // MSG_OPT_TEXTSPEED
    MSG_OPT_TEXTSPEED_INSTANT,
    MSG_OPT_TEXTSPEED_FAST,
    MSG_OPT_TEXTSPEED_NORMAL,
    MSG_OPT_TEXTSPEED_SLOW,

    // MSG_OPT_MUSIC
    MSG_OPT_MUSIC_NO,
    MSG_OPT_MUSIC_LOW,
    MSG_OPT_MUSIC_NORMAL,
    MSG_OPT_MUSIC_HIGH,

    // MSG_OPT_SOUND
    MSG_OPT_SOUND_NO,
    MSG_OPT_SOUND_YES,

    // MSG_OPT_CONTROLS
    MSG_OPT_CONTROLS_NORMAL,
    MSG_OPT_CONTROLS_INVERT,
#endregion
#region Messages, de Init
    MSG_INIT_HISTORY,
    MSG_INIT_PRESS_ANY,
    #endregion
    #region IntroductionScene
    // Tutorial PAGES
    PAGE_TUTORIAL_1,
    PAGE_TUTORIAL_2,
    PAGE_TUTORIAL_3,
    // Manual PAGES
    PAGE_MANUAL_1,
    PAGE_MANUAL_2,
    PAGE_MANUAL_3,
    // Credits PAGES
    PAGE_CREDITS_TITLE,
    PAGE_CREDITS_1,
    PAGE_CREDITS_2,
    PAGE_CREDITS_YOU
    #endregion
}