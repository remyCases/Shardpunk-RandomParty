// Copyright (C) 2024 Rémy Cases
// See LICENSE file for extended copyright information.
// This file is part of the Speedshard repository from https://github.com/remyCases/Shardpunk-RandomParty.

using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.GameUI;
using Assets.Scripts.GameUI.MainMenu;
using Assets.Scripts.Localisation;
using Assets.Scripts.Logic.Tactical;
using BepInEx;
using HarmonyLib;
using HarmonyLib.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace Random;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        // Plugin startup logic
        HarmonyFileLog.Enabled = true;
        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        Harmony harmony = new(PluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
    }
}

[HarmonyPatch]
public static class RandomPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PartyCreationModalDisplayBehaviour), "Awake")]
    static bool PartyCreationModalDisplayBehaviourAwake(PartyCreationModalDisplayBehaviour __instance)
    {
        // find the element to copy
        // maybe a better button should be used, but it works
        GameObject buttonPrefab = GameObject.Find("GoreRoot");
        // new button root !
        GameObject randomButton = UnityEngine.Object.Instantiate(buttonPrefab, __instance.transform.GetChild(1).GetChild(0));
        randomButton.name = "RandomButtonRoot";
        randomButton.transform.SetSiblingIndex(1);

        // remove the gore component
        PartyCreationGoreToggleDisplayBehavior goreScript = randomButton.GetComponent<PartyCreationGoreToggleDisplayBehavior>();
        UnityEngine.Object.Destroy(goreScript);
        // remove the header text
        UnityEngine.Object.DestroyImmediate(randomButton.transform.GetChild(1).gameObject);

        // add the new component
        RandomDisplayBehavior randomDisplay = randomButton.AddComponent<RandomDisplayBehavior>();
        randomDisplay.enabled = true;

        // change the name of the button
        Transform button = randomButton.transform.GetChild(0);
        button.name = "RandomButton";

        return true;
    }
}

public class RandomDisplayBehavior : GameEventBehaviourBase
{
    public void Awake()
    {
        this.ToggleButton = this.transform.GetComponentInChildren<GameButtonDisplayBehaviour>();
        this.ToggleButton.OnClick.AddListener(new UnityAction(this.ApplyRandom));
        this.UpdateDisplay();
    }

    protected override void OnGameEvent(object sender, EventArgs e)
    {
        if (e is PartySelectionUIStateChangedArgs)
        {
            this.UpdateDisplay();
        }
    }

    protected override void Start()
    {
        base.Start();
        this.UpdateDisplay();
    }

    private void ApplyRandom()
    {
        // all available elements
        Game instance = Game.Instance;
        PartySelectionUIState partySelectionState = instance.MainMenuUIState.PartySelectionState;
        PartySelectionCharacterItem[] allChars = AvailableCharactersProvider.Instance.GetAvailableCharacters();
        PartySelectionCharacterItem[] allBots = AvailableCharactersProvider.Instance.GetAvailableBotCharacters();

        IList<PartySelectionCharacterItem> availableChars = allChars.Where(x => instance.ActiveProfile.Unlocks.IsCharacterTypeUnlocked(x.Character.Type)).ToList();
        IList<PartySelectionCharacterItem> availableBots = allBots.Where(x => instance.ActiveProfile.Unlocks.IsCharacterTypeUnlocked(x.Character.Type)).ToList();
        // clear party
        partySelectionState.Party.ClearAll();

        // add randoms to party
        foreach(Character c in RandomUtils.SelectionSamplingTechnique(availableChars, partySelectionState.MaxCharactersInParty).Select(x => x.Character))
        {
            PartySelection.AddOrRemoveCharacterToParty(c);
        }
        partySelectionState.PartyBotSlot = RandomUtils.SelectionSamplingTechnique(availableBots, 1).First().Character;
        this.UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        this.ToggleButton.Mode = ButtonColorMode.MinorBlue;
        this.ToggleButton.Text.text = GameLoc.Instance.Get("gameplaySettings_randomize");
    }

    public GameButtonDisplayBehaviour ToggleButton;
}

public static class RandomUtils
{
    // from Knuth Donald, The Art Of Computer Programming, Volume 2, Third Edition
    // 3.4.2 Random Sampling and Shuffling, p142
    // Algorithm S
    public static IEnumerable<T> SelectionSamplingTechnique<T>(this IList<T> list, int n)
    {
        System.Random rand = new();
        // number of elements dealt with
        int tt = 0;
        // number of elements selected by the algorithm
        int m = 0;
        int N = list.Count;
        // firewall if we want more elements than the size of the list
        int nn = Math.Min(n, N);

        double u;
        
        while(m < nn)
        {
            u = rand.NextDouble();

            if((N - tt)*u >= nn - m) 
            {
                // element not selected
                tt++;
            }
            else
            {
                // element selected
                yield return list[tt];
                tt++;
                m++;
            }
        }
    }
}
