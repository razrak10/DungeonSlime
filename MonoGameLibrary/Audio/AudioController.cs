using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoGameLibrary.Audio
{
    /// <summary>
    /// Provides functionality to control audio playback, including songs and sound effects, with support for muting,
    /// pausing, and resuming audio.
    /// </summary>
    /// <remarks>The <see cref="AudioController"/> class manages audio playback for both songs and sound
    /// effects. It allows for global volume control, muting, and toggling mute states. It also tracks active sound
    /// effect instances to manage their lifecycle, including pausing, resuming, and disposing of them when they are no
    /// longer needed. The class implements <see cref="IDisposable"/> to ensure resources are released
    /// properly.</remarks>
    public class AudioController : IDisposable
    {
        // Tracks sound effect instances created so they can be paused, unpaused, and/or disposed.
        private readonly List<SoundEffectInstance> _activeSoundEffectInstances;

        // Tracks the volume for song playback when muting and unmuting.
        private float _previousSongVolume;

        // Tracks the volume for sound effect playback when muting and unmuting.
        private float _previousSoundEffectVolume;

        /// <summary>
        /// Gets a value that indicates if audio is muted.
        /// </summary>
        public bool IsMuted { get; private set; }

        /// <summary>
        /// Gets or Sets the global volume of songs.
        /// </summary>
        /// <remarks>
        /// If IsMuted is true, the getter will always return back 0.0f and the
        /// setter will ignore setting the volume.
        /// </remarks>
        public float SongVolume
        {
            get
            {
                if (IsMuted)
                {
                    return 0.0f;
                }

                return MediaPlayer.Volume;
            }
            set
            {
                if (IsMuted)
                {
                    return;
                }

                MediaPlayer.Volume = Math.Clamp(value, 0.0f, 1.0f);
            }
        }

        /// <summary>
        /// Gets or Sets the global volume of sound effects.
        /// </summary>
        /// <remarks>
        /// If IsMuted is true, the getter will always return back 0.0f and the
        /// setter will ignore setting the volume.
        /// </remarks>
        public float SoundEffectVolume
        {
            get
            {
                if (IsMuted)
                {
                    return 0.0f;
                }

                return SoundEffect.MasterVolume;
            }
            set
            {
                if (IsMuted)
                {
                    return;
                }

                SoundEffect.MasterVolume = Math.Clamp(value, 0.0f, 1.0f);
            }
        }

        /// <summary>
        /// Gets a value that indicates if this audio controller has been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Creates a new audio controller instance.
        /// </summary>
        public AudioController()
        {
            _activeSoundEffectInstances = new List<SoundEffectInstance>();
        }

        // Finalizer called when object is collected by the garbage collector.
        ~AudioController() => Dispose(false);

        /// <summary>
        /// Updates this audio controller.
        /// </summary>
        public void Update()
        {
            for (int i = _activeSoundEffectInstances.Count - 1; i >= 0; i--)
            {
                SoundEffectInstance instance = _activeSoundEffectInstances[i];

                if (instance.State == SoundState.Stopped)
                {
                    if (!instance.IsDisposed)
                    {
                        instance.Dispose();
                    }
                    _activeSoundEffectInstances.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Plays the specified sound effect with default volume, pitch, and pan settings.
        /// </summary>
        /// <param name="soundEffect">The <see cref="SoundEffect"/> to be played. Cannot be null.</param>
        /// <returns>A <see cref="SoundEffectInstance"/> representing the instance of the sound effect being played.</returns>
        public SoundEffectInstance PlaySoundEffect(SoundEffect soundEffect)
        {
            return PlaySoundEffect(soundEffect, 1.0f, 1.0f, 0.0f, false);
        }

        /// <summary>
        /// Plays a sound effect with specified audio properties and returns the instance of the sound effect being
        /// played.
        /// </summary>
        /// <remarks>The returned <see cref="SoundEffectInstance"/> can be used to control playback of the
        /// sound effect, such as pausing or stopping it.</remarks>
        /// <param name="soundEffect">The sound effect to be played. Cannot be null.</param>
        /// <param name="volume">The volume level of the sound effect. Must be between 0.0f (silent) and 1.0f (full volume).</param>
        /// <param name="pitch">The pitch adjustment of the sound effect. Must be between -1.0f (down one octave) and 1.0f (up one octave).</param>
        /// <param name="pan">The panning of the sound effect. Must be between -1.0f (full left) and 1.0f (full right).</param>
        /// <param name="isLooped">A value indicating whether the sound effect should loop continuously.</param>
        /// <returns>An instance of <see cref="SoundEffectInstance"/> representing the sound effect being played.</returns>
        public SoundEffectInstance PlaySoundEffect(
            SoundEffect soundEffect,
            float volume,
            float pitch,
            float pan,
            bool isLooped)
        {
            // Create an instance fromm the sound effect given
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();

            // Apply the volume, pitch, pan and loop values specified
            soundEffectInstance.Volume = volume;
            soundEffectInstance.Pitch = pitch;
            soundEffectInstance.Pan = pan;
            soundEffectInstance.IsLooped = isLooped;

            // Tell instance to play
            soundEffectInstance.Play();

            // Add it to the active instances for tracking
            _activeSoundEffectInstances.Add(soundEffectInstance);

            return soundEffectInstance;
        }

        /// <summary>
        /// Plays the specified song using the media player.
        /// </summary>
        /// <remarks>If the media player is already playing a song, it will be stopped before the new song
        /// is played.</remarks>
        /// <param name="song">The song to be played. Cannot be null.</param>
        /// <param name="isRepeating">A value indicating whether the song should repeat after finishing. The default is <see langword="true"/>.</param>
        public void PlaySong(Song song, bool isRepeating = true)
        {
            // Check if media player is already playing. If yes, stop it.
            // Not doing so can cause crashes on some platforms
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }

            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = isRepeating;
        }

        /// <summary>
        /// Pauses the currently playing audio, including both music and sound effects.
        /// </summary>
        /// <remarks>This method pauses all active audio streams. It affects both the music being played
        /// through the media player and any sound effects that are currently active.</remarks>
        /// <returns></returns>
        public async Task PauseAudio()
        {
            // Pause active songs playing
            MediaPlayer.Pause();

            // Pause active sound effects
            foreach(SoundEffectInstance soundEffectInstance in _activeSoundEffectInstances)
            {
                soundEffectInstance.Pause();
            }
        }

        /// <summary>
        /// Resumes playback of the audio that was previously paused.
        /// </summary>
        /// <remarks>This method resumes the media player and all active sound effect instances. It should
        /// be called after audio has been paused to continue playback.</remarks>
        public void ResumeAudio()
        {
            // Resume paused music
            MediaPlayer.Resume();

            // Resume active sound effects
            foreach (SoundEffectInstance soundEffectInstance in _activeSoundEffectInstances)
            {
                soundEffectInstance.Resume();
            }
        }


        /// <summary>
        /// Mutes all audio by setting the media player and sound effect volumes to zero.
        /// </summary>
        /// <remarks>This method stores the current volume levels before muting, allowing them to be
        /// restored later. It sets the <see cref="IsMuted"/> property to <see langword="true"/>.</remarks>
        public void MuteAudio()
        {
            // Store the volume so they can be restored during ResumeAudio
            _previousSongVolume = MediaPlayer.Volume;
            _previousSoundEffectVolume = SoundEffect.MasterVolume;

            // Set all volumes to 0
            MediaPlayer.Volume = 0.0f;
            SoundEffect.MasterVolume = 0.0f;

            IsMuted = true;
        }

        /// <summary>
        /// Restores the audio volume to its previous levels, effectively unmuting the audio.
        /// </summary>
        /// <remarks>This method sets the media player and sound effect volumes to their values before
        /// muting. It also updates the mute state to indicate that audio is no longer muted.</remarks>
        public void UnMuteAudio()
        {
            // Store the volume so they can be restored during ResumeAudio
            MediaPlayer.Volume = _previousSongVolume;
            SoundEffect.MasterVolume = _previousSoundEffectVolume;

            IsMuted = false;
        }

        /// <summary>
        /// Toggles the mute state of the audio.
        /// </summary>
        /// <remarks>If the audio is currently muted, this method will unmute it.  Conversely, if the
        /// audio is not muted, it will mute the audio.</remarks>
        public void ToggleMute()
        {
            if (IsMuted)
            {
                UnMuteAudio();
            }
            else
            {
                MuteAudio();
            }
        }

        /// <summary>
        /// Releases all resources used by the current instance of the class.
        /// </summary>
        /// <remarks>This method should be called when the instance is no longer needed to free up
        /// resources. After calling this method, the instance is in an unusable state and should not be used.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the resources used by the object.
        /// </summary>
        /// <remarks>This method is called by the public <c>Dispose</c> method and the finalizer. When
        /// <paramref name="disposing"/> is <see langword="true"/>, it releases all resources held by any managed
        /// objects that this object references. This method should be overridden by derived classes to release
        /// additional resources.</remarks>
        /// <param name="disposing">A boolean value indicating whether to release both managed and unmanaged resources (<see langword="true"/>)
        /// or only unmanaged resources (<see langword="false"/>).</param>
        protected void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (SoundEffectInstance soundEffectInstance in _activeSoundEffectInstances)
                {
                    soundEffectInstance.Dispose();
                }
                _activeSoundEffectInstances.Clear();
            }

            IsDisposed = true;
        }
    }
}
