using System;
using AXNAEngine.com.axna;
using AXNAEngine.com.axna.worlds;
using AXNAEngine.com.testgame;
using AXNAEngine.com.testgame.tilemaps;

namespace AXNAEngine
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Engine game = new Engine(800, 600);

//            AXNA.WorldManager.ActivateWorld(new TileMapIsometricWorld_ZigZag());
//            AXNA.WorldManager.ActivateWorld(new TileMapIsometricWorld_Diamond());
            AXNA.WorldManager.ActivateWorld(new TiledMapRenderTargetWorld());

            game.Run();
        }
    }
#endif
}

