using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StardewWeatherForecast
{
    public class Mod : ModSystem
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Game1.IsPlayerFree || Game1.IsMultiplayer)
                return;

            var font = Game1.activeClickableMenu?.spriteFont ?? Game1.spriteFont;
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Weather Forecast:");

            for (int i = 1; i <= 3; i++)
            {
                int day = Game1.Date.Day + i;
                int month = Game1.Date.Month;
                if (day > 28)
                {
                    day -= 28;
                    month++;
                    if (month > 12) month = 1;
                }
                int weather = Game1.weatherData[day, month];
                string weatherName = GetWeatherName(weather);
                sb.AppendLine($"  {month}/{day}: {weatherName}");
            }

            var text = sb.ToString();
            var size = font.MeasureString(text);
            var pos = new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width - size.X - 20f, 20f);

            Game1.drawWorldBackground(0);
            SpriteBatch sbatch = Game1.spriteBatch;
            sbatch.Begin();
            sbatch.DrawString(font, text, pos, Color.White);
            sbatch.End();
        }

        private string GetWeatherName(int weather)
        {
            switch (weather)
            {
                case 0: return "Sunny";
                case 1: return "Cloudy";
                case 2: return "Rainy";
                case 3: return "Snowy";
                case 4: return "Stormy";
                case 5: return "Windy";
                default: return "Unknown";
            }
        }
    }
}
