using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Bombs
{
    class Jugador
    {
        // Animacion representando al jugador
        public Texture2D PlayerTexture;

        // Posicion del jugador
        public Vector2 Position;

        // Estado del jugador
        public bool Active;

        // Cantidad de vida del jugador
        public int Health;

        // Obtener el ancho del jugador
        public int Width
        {
            get { return PlayerTexture.Width; }
        }

        // Obtener el alto del jugador
        public int Height
        {
            get { return PlayerTexture.Height; }
        }
        public void Initialize(Texture2D texture, Vector2 position)
        {
            PlayerTexture = texture;
            // Asignar la posicion del jugador
            Position = position;
            // Activar el jugador
            Active = true;
            // Inicializar la vida del jugador
            Health = 3;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, null,
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
