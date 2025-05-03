using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Notes
{
    /// <summary>
    /// Janela de configurações do iNotes que permite ao usuário ajustar o tamanho da fonte das notas
    /// e escolher entre os temas claro e escuro.
    /// </summary>
    public partial class SettingsWindow : Window
    {
        /// <summary>
        /// Obtém o tamanho da fonte selecionado pelo usuário.
        /// Este valor afeta apenas a área de texto das notas, não os elementos de interface.
        /// </summary>
        public double SelectedFontSize { get; private set; }
        
        /// <summary>
        /// Obtém o valor que indica se o tema escuro está selecionado.
        /// </summary>
        public bool IsDarkTheme { get; private set; }
        
        /// <summary>
        /// Obtém o valor que indica se as configurações foram aplicadas (botão Aplicar).
        /// </summary>
        public bool SettingsApplied { get; private set; } = false;
        
        /// <summary>
        /// Inicializa uma nova instância da janela de configurações.
        /// </summary>
        /// <param name="currentFontSize">O tamanho atual da fonte das notas.</param>
        /// <param name="isDarkTheme">Indica se o tema escuro está ativado atualmente.</param>
        public SettingsWindow(double currentFontSize, bool isDarkTheme)
        {
            InitializeComponent();
            
            // Inicializar valores com as configurações atuais
            SelectedFontSize = currentFontSize;
            IsDarkTheme = isDarkTheme;
            
            // Configurar controles com valores iniciais
            FontSizeSlider.Value = currentFontSize;
            FontSizeText.Text = currentFontSize.ToString("0.0");
            SampleText.FontSize = currentFontSize;
            
            // Configurar tema
            if (isDarkTheme)
                DarkThemeRadio.IsChecked = true;
            else
                LightThemeRadio.IsChecked = true;
                
            // Adicionar evento de carregamento para animação
            this.Loaded += SettingsWindow_Loaded;
        }
        
        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Adicionar efeito de entrada suave
            this.Opacity = 0;
            DoubleAnimation fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(250),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            this.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }
        
        // Evento para atualizar o texto de exemplo quando o slider é movido
        private void FontSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SampleText == null || FontSizeText == null) return;
            
            double newSize = Math.Round(e.NewValue, 1);
            SelectedFontSize = newSize;
            
            // Atualizar o texto do valor
            FontSizeText.Text = newSize.ToString("0.0");
            
            // Animar a mudança de tamanho do texto de exemplo
            DoubleAnimation animation = new DoubleAnimation
            {
                From = SampleText.FontSize,
                To = newSize,
                Duration = TimeSpan.FromMilliseconds(100)
            };
            
            SampleText.BeginAnimation(TextBlock.FontSizeProperty, animation);
        }
        
        // Evento para atualizar o tema selecionado
        private void ThemeRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == DarkThemeRadio)
            {
                IsDarkTheme = true;
            }
            else if (sender == LightThemeRadio)
            {
                IsDarkTheme = false;
            }
        }
        
        // Método para permitir arrastar a janela
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        
        // Botão Fechar
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsApplied = false;
            
            // Animar fechamento
            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            
            fadeOut.Completed += (s, args) => Close();
            this.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }
        
        // Botão Aplicar
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsApplied = true;
            
            // Animar fechamento
            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            
            fadeOut.Completed += (s, args) => Close();
            this.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }
        
        // Botão Cancelar
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsApplied = false;
            
            // Animar fechamento
            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            
            fadeOut.Completed += (s, args) => Close();
            this.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }
    }
}