using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Audio
{
    internal class AudioController : IDisposable
    {
        private readonly List<SoundEffectInstance> _activeSoundEffectInstances;

        private float _previousSongVolume;

        private float _previousSoundEffectVolume;
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
