using System;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Engine.SceneSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace Key_Quest.Engine.LevelEditor;

public class TilemapManager
{
    TmxMap map;
    Texture2D tileset;

    int tileWidth;
    int tileHeight;
    int tilesetTilesWide;
    int tilesetTilesHigh;

    public TilemapManager(TmxMap map, Texture2D tileset)
    {
        this.map = map;
        this.tileset = tileset;
        tileWidth = map.Tilesets[0].TileWidth;
        tileHeight = map.Tilesets[0].TileHeight;
        tilesetTilesWide = tileset.Width / tileWidth;
    }

    public void Draw(Vector2 startPosition)
    {
        foreach (var layer in map.Layers)
        {
            for (int i = 0; i < layer.Tiles.Count; i++)
            {
                int gid = layer.Tiles[i].Gid;

                if (gid != 0)
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = startPosition.X + (i % map.Width) * tileWidth * Config.GameScale; // Adjusted for scaling and starting position
                    float y = startPosition.Y + (float)Math.Floor(i / (double)map.Width) * tileHeight * Config.GameScale; // Adjusted for scaling and starting position

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                    // Adjusted position and size with scaling
                    Config.Batch.Draw(tileset, 
                        new Rectangle((int)x - (int)Config.CameraX, (int)y - (int)Config.CameraY, (int)(tileWidth * Config.GameScale), (int)(tileHeight * Config.GameScale)), 
                        tilesetRec, 
                        Color.White);
                }
            }
        }
    }
    
    public void CreateColliders(Vector2 startPosition)
    {
        // Assuming "Collision" is the name of the collision layer in your Tiled map

        foreach (var obj in map.ObjectGroups["Collision"].Objects)
        {
            // The object's position and size in pixels
            double x = startPosition.X + obj.X * Config.GameScale; // Adjusted for scaling
            double y = startPosition.Y + obj.Y * Config.GameScale; // Adjusted for scaling
            double width = obj.Width * Config.GameScale; // Adjusted for scaling
            double height = obj.Height * Config.GameScale; // Adjusted for scaling

            GameObject collider = new GameObject();
            collider.Transform.Position.X = (float)x;
            collider.Transform.Position.Y = (float)y;
            collider.AddComponent(new BoxCollider(new Vector2((float)width, (float)height)));
            SceneManager.CurrentScene.AddGameObject(collider);
        }
    }
}