using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using MessageBox = System.Windows.MessageBox;

namespace ChatWheel
{
    public class Settings
    {
        public int PhrasesAmount { get; set; } = 5;
        public int HotKey { get; set; } = (int) Keys.Oemtilde;
        public Phrase[] Phrases { get; set; } =
        {
            new Phrase("First", "First"),
            new Phrase("Second", "Second"),
            new Phrase("Third", "Third"),
            new Phrase("Fourth", "Fourth"),
            new Phrase("Fifth", "Fifth"),
            new Phrase("Sixth", "Sixth"),
            new Phrase("Seventh", "Seventh"),
            new Phrase("Eighth", "Eighth")
        };

        public static Settings Deserialize()
        {
            if (!File.Exists("./heroeswheel.xml")) return null;
            try
            {
                var xmlSerializer = new XmlSerializer(typeof (Settings));
                using (var fs = File.OpenRead("./heroeswheel.xml"))
                {
                    return (Settings) xmlSerializer.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error when saving settings.\n" + e.Message,
                    "Serialization error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }

        public static void Serialize(Settings settings)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof (Settings));
                using (var fs = File.OpenWrite("./heroeswheel.xml"))
                {
                    xmlSerializer.Serialize(fs, settings);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error when saving settings.\n" + e.Message,
                    "Serialization error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public class Phrase
        {
            // ReSharper disable once UnusedMember.Global
            // Required for serialization
            public Phrase()
            {
            }

            public Phrase(string shortPhrase, string fullPhrase)
            {
                ShortPhrase = shortPhrase;
                FullPhrase = fullPhrase;
            }

            public string FullPhrase { get; set; }
            public string ShortPhrase { get; set; }
        }
    }
}