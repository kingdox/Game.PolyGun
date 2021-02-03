#region Imports
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavLib;
using Environment; 
#endregion
/// <summary>
/// Encargado del manejo de pintado con respecto al HUD de la GameScene
/// TODO solo si hay tiempo, hacer que esto reactivo
/// </summary>
public class UIManager : MonoX
{
    #region Variables
    private static UIManager _;
    //Revisar cuando puede disparar:
    [Header("UI Settings")]
    [Range(0, 1)]
    public float disabledAlpha = 0.25f;
    [Space]
    [Header("Waves AREA")]
    public Text text_waves;
    private int lastWave;
    private int lastColorIndex;
    [Space]
    [Header("Weapon AREA")]
    public Shot shot_player;
    public Image img_weaponBG;
    public Image img_weapon;
    public Text text_weapon;
    [Space]
    [Header("Info AREA")]
    public PlayerController player;

    public MultiColor multicolor_playerLifeBG;
    public Text text_playerLife;
    [Space]
    //objetos ordenados igual que el enum, representan los objetos de juego
    // que pueden ser equipables
    public Equipment equipment_player;
    public Sprite[] sprite_items;
    public Image[] img_slots;
    [Header("Buff AREA")]
    public Color[] buffHUDcolors;
    private BuffHUD[] buffsHUD;
    [Header("Showed Keys")]
    public Text[] showedKeys = new Text[4];// C,V,B y ESPACIO
    private TKey[] slotKeys = { TKey.SLOT_1_DEFAULT, TKey.SLOT_2_DEFAULT, TKey.SLOT_3_DEFAULT, TKey.SLOT_1_ALT, TKey.SLOT_2_ALT, TKey.SLOT_3_ALT };


    #endregion
    #region Events
    private void Awake(){
        if (_ == null)
        {
            _ = this;
        }
        else if (_ != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Text[] texts = {text_weapon, text_waves, text_playerLife };
        foreach (Text txt in texts){
            StartCoroutine(SetResize(txt));
        }

        GetChilds(out buffsHUD, player.buffList);
    }
    private void Update()
    {
        Refresher();
    }
    #endregion
    #region MEthods

    /// <summary>
    /// refresca la pantalla
    /// </summary>
    public void Refresher() {

        //cosas que se refrescan en partida
        if (GameManager.IsOnGame()) {
            Refresh_weaponArea(shot_player.timeCount_bullet);
            Refresh_waves(EnemyManager.waveActual);


            float life = player.character.timeLife > 0 ? player.character.timeLife : 0;
            Refresh_playerLife(life);
            Refresh_playerEquipment(equipment_player.GetSlots());
            Refresh_playerBuff(buffsHUD, buffHUDcolors);
            Refresh_KeysShowed();
        }
    }

    private void Refresh_KeysShowed()
    {
        SavedData saved = DataPass.GetSavedData();

        //int length = saved.idiom.Equals(0) ? 3 : 6;
        //int[] length = { 3, 6 };
        //default
        //for (int i = saved.idiom * 3; i < length[saved.idiom]; i++)
        //{

        //    showedKeys[i].text = Data.Translated(saved.idiom).Value(slotKeys[i]);
        //}
        if (saved.idiom.Equals(0))
        {
            for (int i = 0; i < 3; i++)
            {
             showedKeys[i].text = Data.Translated(saved.idiom).Value(slotKeys[i]);
            }

        }
        else
        {
            for (int i = 3; i < 6; i++)
            {   
             showedKeys[i - 3].text = Data.Translated(saved.idiom).Value(slotKeys[i]);
            }
        }

        showedKeys[3].text = Data.Translated(saved.idiom).Value(TKey.ATTACK);


    }

    /// <summary>
    /// Actualiza toda la parte de Armas del HUD, permite hasta 2 redpmdeps
    /// </summary>
    private void Refresh_weaponArea(float time){
        bool c = time.Equals(0);
        text_weapon.text = c ? "": $"{Math.Round(time, 2)} s";
        XavHelpTo.Set.ColorParam(ref img_weapon, ColorType.a, c ? 1 : disabledAlpha);
        XavHelpTo.Set.ColorParam(ref img_weaponBG, ColorType.a, c ? .3f : disabledAlpha);
        
    }

    /// <summary>
    /// Refresca la oleada, cambia el color cuando entras en una nueva oleada
    /// </summary>
    private void Refresh_waves(int wave) {

        if (!lastWave.Equals(wave)){
            string[] colors = { "green", "red", "magenta", "white", "yellow","blue"};
            lastWave = wave;
            lastColorIndex = XavHelpTo.Know.DifferentIndex(colors.Length, lastColorIndex);
            //Pinta el numero con el nuevo texto
            text_waves.text = XavHelpTo.Look.ColorPrint(wave.ToString(),colors[lastColorIndex]);
        }
    }

    /// <summary>
    /// Refrescamos la vida del jugador en la pantalla
    /// <para>Si el tiempo de vida del player es menor que 10 hacemos efectos</para>
    /// </summary>
    private void Refresh_playerLife(float timeLife){

        if (timeLife < 10f){
            timeLife = (float)Math.Round(timeLife, 3);
            if (timeLife == 0)
            {
                text_playerLife.text = "...";
                multicolor_playerLifeBG.isPlaying = false;
            }
            else
            {
                text_playerLife.text = XavHelpTo.Look.ColorPrint($"{timeLife} s");
                multicolor_playerLifeBG.isPlaying = true;
            }

        }else{
            text_playerLife.text = $"{Math.Round(timeLife, 3)} s";
            multicolor_playerLifeBG.isPlaying = false;
        }

    }


    /// <summary>
    /// Refresca la información de qué objetos posee el player actualmente
    /// </summary>
    private void Refresh_playerEquipment(ItemContent[] equipItems){
        for (int x = 0; x < img_slots.Length; x++){

            if (!equipItems[x].Equals(ItemContent.NO)){

                int val = (int)equipItems[x];
                if (val >= 3)
                {
                    img_slots[x].sprite = sprite_items[3];
                    img_slots[x].color = buffHUDcolors[val - 3];
                    XavHelpTo.Set.ColorParam(ref img_slots[x], ColorType.a, 1);
                }
                else
                {
                    //lo normal
                    img_slots[x].color = Color.white;
                    img_slots[x].sprite = sprite_items[val];
                    XavHelpTo.Set.ColorParam(ref img_slots[x], ColorType.a, 1);
                }
            }
            else{
                img_slots[x].sprite = null;
                img_slots[x].color = Color.white;
                XavHelpTo.Set.ColorParam(ref img_slots[x], ColorType.a, disabledAlpha);
            }


        }
    }


    /// <summary>
    /// Refresca al estado actual de los buffs del player
    /// </summary>
    private void Refresh_playerBuff(BuffHUD[] buffsHUD, Color[] colors)
    {
        //revisan si estan runing o no,
        for (int x = 0; x < buffsHUD.Length; x++)
        {
            if (buffsHUD[x].IsRunning())
            {
                buffsHUD[x].Draw(colors[x]);
            }
            else
            {
                buffsHUD[x].Clear(disabledAlpha);
            }
        }
    }


    /// <summary>
    /// Activamos el BestFit por un tiempo, obtenemos el tamaño de la fuente
    /// y con eso desactivamos la fuente, la fuente inicial representa el tope.
    /// <para>Usar esto premite ajustar la fuente al tamaño proporcieonado
    /// sin actualizar constantemente...
    /// </para>
    /// </summary>
    private IEnumerator SetResize(Text txt)
    {
        txt.resizeTextForBestFit = true;
        txt.resizeTextMinSize = 1;
        txt.resizeTextMaxSize = txt.fontSize;
        yield return new WaitForSeconds(0.1f);
        txt.fontSize = txt.cachedTextGenerator.fontSizeUsedForBestFit;
        txt.resizeTextForBestFit = false;
    }

    #endregion
}