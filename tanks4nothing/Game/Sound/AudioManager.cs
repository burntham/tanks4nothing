﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using tanks4nothing.Game.Sound;
#endregion

namespace tanks4nothing
{
    /// <summary>
    /// Component that manages audio playback for all sounds.
    /// </summary>
    public class AudioManager : GameComponent
    {
        #region Volume settings
        public float
            masterVolume = 1.0f,
            musicVolume = 1.0f,

            //Weapons volumes
            pistol = 0.1f,
            sniper = 0.5f,
            shottie = 0.6f,
            assault = 0.3f,
            sword = 0.3f,

            //entity volumes
            alienDeath1 = 0.8f,
            alienRange = 0.4f;
        #endregion

        #region Fields
        bool inGame = false;

        //Game Sounds
        Dictionary<string, SoundEffectInstanceManager> gameSounds;

        //Menu Sounds
        Dictionary<string, SoundEffectInstanceManager> menuSoundS;
        SoundEffectInstanceManager menuMusic;        
        #endregion

        #region Initialization
        public AudioManager(Microsoft.Xna.Framework.Game game)
            : base(game)
        {

            menuSoundS = new Dictionary<string, SoundEffectInstanceManager>();
            gameSounds = new Dictionary<string, SoundEffectInstanceManager>();

            game.Components.Add(this);
        }

        #endregion


        /// <summary>
        /// Play a new instance of a sound effect
        /// </summary>
        /// <param name="effectName eg. pistol"></param>
        /// <param name="volume"></param>
        public void PlayGameSound(string effectName)
        {
            gameSounds[effectName].playSound();
        }

        public int Music
        {
            set
            {
                if (value == 0)
                    musicVolume = 1.0f;
                else
                    musicVolume = 0.0f;

                menuMusic.resetVolume();                
            }
            get
            {
                if (musicVolume == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }


        public int Volume
        {
            set {
                if (value == 0)
                    masterVolume = 0.1f;
                else if (value == 1)
                    masterVolume = 0.5f;
                else
                    masterVolume = 1.0f;

                menuMusic.resetVolume();
                }
            get
            {
                if (masterVolume == 0.1f)
                    return 0;
                else if (masterVolume == 0.5f)
                    return 1;
                else
                    return 2;
            }
        }

        /// <summary>
        /// Load the Game sounds effects and music (Excludes menu stuff)
        /// </summary>
        /// <param name="SEffect">The Sound effect</param>
        /// <param name="effectName">Name of the effect (so that it can be referenced/played later)</param>
        /// <param name="instances"> How many instances do you want playing at a time</param>
        /// <param name="volume"></param>
        /// <param name="isMusic">Is this game sound, a music clip?</param>
        public void LoadGameSound(SoundEffect SEffect, string effectName, int instances, float volume, bool isMusic)
        {
            if (!gameSounds.ContainsKey(effectName))
                gameSounds.Add(effectName, new SoundEffectInstanceManager(SEffect, instances, volume, isMusic));
        }

        public void SwitchToGame()
        {
            menuMusic.Pause();
            foreach (SoundEffectInstanceManager sounds in gameSounds.Values)
                sounds.Resume();
        }

        public void SwitchToMenu()
        {
            foreach (SoundEffectInstanceManager sounds in gameSounds.Values)
                sounds.Pause();
            menuMusic.Resume();
        }


        #region Loading Methodes

        public void LoadMenuSound(SoundEffect SEffect, string effectName, int instances, float volume)
        {
            menuSoundS.Add(effectName, new SoundEffectInstanceManager(SEffect, instances, volume, false));
        }

        public void LoadMenuMusic(string path)
        {
            menuMusic = new SoundEffectInstanceManager(Game.Content.Load<SoundEffect>(path), 1, 1.0f, true);
        }

        public void PlayMenuMusic()
        {
            menuMusic.playSound();
        }

        #endregion

    }

}