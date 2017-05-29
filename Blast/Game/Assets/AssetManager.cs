using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace LowRezJam
{
	public static class AssetManager
	{
        public static Dictionary<string, Texture2D> Sprites { get; set; }
		public static Dictionary<string, SoundEffect> Sounds { get; set; }
		private static Random Rand { get; set; }

        public static void LoadContent(ContentManager Content)
		{
			Rand = new Random();

            Sprites = new Dictionary<string, Texture2D>();
            foreach (var FName in Directory.GetFiles("Content/Sprites"))
            {
                if (FName.Contains(".xnb") || FName.Contains(".ase")) continue;

                var FixedFName = FName.Replace(".png", "").Replace(".jpg","").Replace(".gif", "");
                FixedFName = FixedFName.Replace("Content/", "");

                Sprites.Add(
					FixedFName.Replace("Sprites\\", "").Replace("Sprites/", ""),
                    Content.Load<Texture2D>(FixedFName));
            }

			Sounds = new Dictionary<string, SoundEffect>();
			foreach (var FName in Directory.GetFiles("Content/Sounds"))
			{
				if (!FName.Contains(".xnb")) continue;

				var FixedFName = FName.Replace(".xnb", "");
				FixedFName = FixedFName.Replace("Content/", "");

				Sounds.Add(
					FixedFName.Replace("Sounds\\", "").Replace("Sounds/", ""),
					Content.Load<SoundEffect>(FixedFName));
			}
		}

		public static void PlaySound(string Sound, float Volume = 1, float Pitch = 0)
		{
			Sounds[Sound].Play(Volume, Pitch, 0);
		}

		public static void PlaySoundRandomPitch(string Sound, float Volume = 1, float PitchMax = 1, float PitchMin = -1)
		{
			float Pitch = (float)Rand.NextDouble() * (PitchMax - PitchMin) + PitchMin;
			Sounds[Sound].Play(Volume, Pitch, 0);
		}
	}
}

