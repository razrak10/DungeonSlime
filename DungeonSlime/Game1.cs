using DungeonSlime.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using MonoGameGum;
using MonoGameLibrary;

namespace DungeonSlime;

public class Game1 : Core
{
    // The background theme song
    private Song _themeSong;

    private GumService GumUI => GumService.Default;

    public Game1() : base("Dungeon Slime", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        base.Initialize();

        // Start playing the background music
        Audio.PlaySong(_themeSong);

        // Start the game with the title scene
        ChangeScene(new TitleScene());
    }


    protected override void LoadContent()
    {
        // Load the background theme music
        _themeSong = Content.Load<Song>("audio/theme");
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        GumUI.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        GumUI.Draw();
    }
}
