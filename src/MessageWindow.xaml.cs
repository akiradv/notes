using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notes
{
    public partial class MessageWindow : Window
    {
        public MessageBoxResult Result { get; private set; }

        public MessageWindow(string message, string title, MessageBoxButton buttons)
        {
            try
            {
                InitializeComponent();
                
                if (MessageText != null && TitleText != null)
                {
                    MessageText.Text = message;
                    TitleText.Text = title;
                    Result = MessageBoxResult.None;

                    // Adicionar botões baseado no tipo
                    switch (buttons)
                    {
                        case MessageBoxButton.OK:
                            AddButton("OK", MessageBoxResult.OK);
                            break;
                        case MessageBoxButton.OKCancel:
                            AddButton("Cancelar", MessageBoxResult.Cancel);
                            AddButton("OK", MessageBoxResult.OK);
                            break;
                        case MessageBoxButton.YesNo:
                            AddButton("Não", MessageBoxResult.No);
                            AddButton("Sim", MessageBoxResult.Yes);
                            break;
                        case MessageBoxButton.YesNoCancel:
                            AddButton("Cancelar", MessageBoxResult.Cancel);
                            AddButton("Não", MessageBoxResult.No);
                            AddButton("Sim", MessageBoxResult.Yes);
                            break;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Componentes da janela não foram inicializados corretamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar MessageWindow: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void AddButton(string content, MessageBoxResult result)
        {
            try
            {
                if (ButtonsPanel == null)
                {
                    throw new InvalidOperationException("Painel de botões não inicializado.");
                }

                var button = new Button
                {
                    Content = content,
                    Width = 80,
                    Height = 28,
                    Margin = new Thickness(4, 0, 4, 0),
                    Style = (Style)Application.Current.Resources["iPodButtonStyle"]
                };

                button.Click += (s, e) =>
                {
                    Result = result;
                    Close();
                };

                ButtonsPanel.Children.Add(button);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar botão: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    DragMove();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao mover janela: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            Close();
        }

        public static MessageBoxResult Show(Window owner, string message, string title, MessageBoxButton buttons)
        {
            try
            {
                var window = new MessageWindow(message, title, buttons)
                {
                    Owner = owner
                };
                window.ShowDialog();
                return window.Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao mostrar MessageWindow: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return MessageBoxResult.Cancel;
            }
        }
    }
} 