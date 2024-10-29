using System;
using System.Windows;
using System.Windows.Input;

namespace ChatWheel
{
    /// <summary>
    ///     Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        private readonly ChatOverlay co;
        private readonly Settings settings;
        private bool isWaitingForHotkey;

        public ConfigWindow()
        {
            settings = Settings.Deserialize() ?? new Settings();

            InitializeComponent();
            co = new ChatOverlay(settings);
            SliderQuantity.Value = settings.PhrasesAmount;
            BtnHotkey.Content = (Key) settings.HotKey;

            PhrasesList.ItemsSource = settings.Phrases;
            co.UpdateChatWheel();
            co.Show();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(co == null)
                return;
            
            settings.PhrasesAmount = (int) e.NewValue;
            co.UpdateChatWheel();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isWaitingForHotkey)
                return;
            
            settings.HotKey = (int) e.Key;
            BtnHotkey.Content = e.Key;
            isWaitingForHotkey = false;
        }

        private void BtnNewHotkey_Click(object sender, RoutedEventArgs e)
        {
            isWaitingForHotkey = true;
            BtnHotkey.Content = "Press a key";
        }

        protected override void OnClosed(EventArgs e)
        {
            co.Close();
            Settings.Serialize(settings);
            base.OnClosed(e);
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            Settings.Serialize(settings);
        }

        private void PhraseBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            co.UpdateChatWheel();
        }
    }
}