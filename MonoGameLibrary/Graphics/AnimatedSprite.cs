using Microsoft.Xna.Framework;
using System;

namespace MonoGameLibrary.Graphics
{
    public class AnimatedSprite : Sprite
    {
        private int _currentFrame;
        private TimeSpan _elapsed;
        private Animation _animation;

        /// <summary>
        /// Gets or Sets the animation for this animated sprite.
        /// </summary>
        public Animation Animation
        {
            get => _animation;
            set
            {
                _animation = value;
                Region = _animation.Frames[0];
            }
        }

        /// <summary>
        /// Creates a new animated sprite.
        /// </summary>
        public AnimatedSprite() { }

        /// <summary>
        /// Creates a new animated sprite with the specified frames and delay.
        /// </summary>
        /// <param name="animation">The animation for this animated sprite.</param>
        public AnimatedSprite(Animation animation)
        {
            Animation = animation;
        }

        /// <summary>
        /// Updates the current frame of the animation based on the elapsed game time.
        /// </summary>
        /// <remarks>This method advances the animation frame if the elapsed time exceeds the frame delay.
        /// It loops back to the first frame after reaching the last frame.</remarks>
        /// <param name="gameTime">The current game time, which provides the elapsed time since the last update.</param>
        public void Update(GameTime gameTime)
        {
            _elapsed += gameTime.ElapsedGameTime;

            if (_elapsed >= _animation.Delay)
            {
                _elapsed -= _animation.Delay;
                _currentFrame++;

                if (_currentFrame >= _animation.Frames.Count)
                {
                    _currentFrame = 0;
                }

                Region = _animation.Frames[_currentFrame];
            }
        }
    }
}
