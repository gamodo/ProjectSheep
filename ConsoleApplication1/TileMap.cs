using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSheep{
    class TileMap : Drawable
    {

        public bool load(string tileset, SFML.Window.Vector2u tileSize, int[] tiles, uint width, uint height) {
            m_vertices.PrimitiveType = SFML.Graphics.PrimitiveType.Quads;
            m_vertices.Resize(width * height * 4);
            for (uint i = 0; i < width; ++i) {
                for (uint j = 0; j < height; ++j) {
                    int tileNumber = tiles[i + j * width];

                    long tu = tileNumber % (m_tileset.Size.X / tileSize.X);
                    long tv = tileNumber / (m_tileset.Size.X / tileSize.X);

                    uint index = (i + j * width) * 4;

                    m_vertices[index + 0] = new Vertex(new Vector2f(i * tileSize.X, j * tileSize.Y), new Vector2f(tu * tileSize.X, tv * tileSize.Y));
                    m_vertices[index + 1] = new Vertex(new Vector2f((i + 1) * tileSize.X, j * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y));
                    m_vertices[index + 2] = new Vertex(new Vector2f((i + 1) * tileSize.X, (j + 1) * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y));
                    m_vertices[index + 3] = new Vertex(new Vector2f(i * tileSize.X, (j + 1) * tileSize.Y), new Vector2f(tu * tileSize.X, (tv + 1) * tileSize.Y));
                }
            }

            return true;
        }

        void Drawable.Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states) {
            // apply the transform
            states.Transform *= Transform.Identity;

            // apply the tileset texture
            states.Texture = m_tileset;

            // draw the vertex array
            target.Draw(m_vertices, states);
        }

        public void Draw(RenderWindow win)
        {
        }

        public bool IsWalkable(GameObject gobj) {
            return true;
        }

        public SFML.Graphics.Texture m_tileset = new SFML.Graphics.Texture("Bilder/map.bmp");
        private SFML.Graphics.VertexArray m_vertices = new SFML.Graphics.VertexArray();

    }
}
