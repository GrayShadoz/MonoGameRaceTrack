using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoTube.Sprites;
using MonoTube.Models;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace MonoTube.Models
{
    class ScoreManager
    {
        private static string fileName = "score.xml";

        public List<Score> HighScores { get; private set; }

        public List<Score> Scores { get; private set; }

        public ScoreManager()
            : this(new List<Score>())
        {

        }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;
            updateHighScores();
        }

        public void Add(Score score)
        {
            Scores.Add(score);
            Scores = Scores.OrderByDescending(c => c.Value).ToList();
            updateHighScores();
        }

        public static ScoreManager Load()
        {
            if (!File.Exists(fileName))
                return new ScoreManager();

            using (var reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                var serializer = new XmlSerializer(typeof(List<Score>));

                var scores = (List<Score>)serializer.Deserialize(reader);

                return new ScoreManager(scores);
            }
        }

        public void updateHighScores()
        {
            HighScores = Scores.Take(10).ToList();
        }

        public static void Save(ScoreManager scoreManager)
        {
            using (var reader = new StreamWriter(new FileStream(fileName, FileMode.Create)))
            {
                var serializer = new XmlSerializer(typeof(List<Score>));

                serializer.Serialize(reader, scoreManager.Scores);
            }
        }
    }
}
