/// <summary>
/// Contenedor de las llaves para traducir dependiendo del <see cref="Idiom"/>
/// </summary>
public enum TKey
{
    No = -1, // en caso de que tengamos que declarar que no hay

    // IDIOM
    _IDIOM,

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


    //Messages (Mensajes del cyborg...)
    MSG_CYBORG_WHERE,
    MSG_CYBORG_UNKNOW,

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
    MSG_OPT_CONTROLS_INVERT
}