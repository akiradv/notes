using System;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;

namespace Notes
{
    public partial class SaveNoteWindow : Window
    {
        public string NoteName { get; private set; } = string.Empty;
        public bool SaveConfirmed { get; private set; }

        public SaveNoteWindow(string currentName = "")
        {
            try
            {
                InitializeComponent();
                
                if (NoteNameTextBox != null)
                {
                    NoteNameTextBox.Text = currentName;
                    // Selecionar o texto ao abrir para facilitar a edição
                    NoteNameTextBox.SelectAll();
                    NoteNameTextBox.Focus();
                }
                SaveConfirmed = false;

                // Adicionar handler para a tecla Enter
                if (NoteNameTextBox != null)
                {
                    NoteNameTextBox.KeyDown += (s, e) =>
                    {
                        if (e.Key == Key.Enter)
                        {
                            e.Handled = true; // Prevenir o som de erro
                            ValidateAndSave();
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao inicializar SaveNoteWindow: {ex}");
                MessageWindow.Show(this, 
                    $"Erro ao abrir janela de salvamento: {ex.Message}", 
                    "Erro", MessageBoxButton.OK);
                DialogResult = false;
                Close();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                try
                {
                    DragMove();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Erro ao mover janela: {ex}");
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SaveConfirmed = false;
            DialogResult = false;
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateAndSave();
        }

        private void ValidateAndSave()
        {
            try
            {
                if (NoteNameTextBox == null)
                {
                    Debug.WriteLine("NoteNameTextBox é null");
                    return;
                }

                string nomeDaNota = NoteNameTextBox.Text?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(nomeDaNota))
                {
                    MessageWindow.Show(this, 
                        "Por favor, insira um nome para a nota.", 
                        "Nome Necessário", MessageBoxButton.OK);
                    NoteNameTextBox.Focus();
                    return;
                }

                // Validar caracteres inválidos para nome de arquivo
                string caracteresInvalidos = new string(System.IO.Path.GetInvalidFileNameChars());
                if (nomeDaNota.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
                {
                    MessageWindow.Show(this, 
                        $"O nome da nota não pode conter os seguintes caracteres:\n{caracteresInvalidos}", 
                        "Caracteres Inválidos", MessageBoxButton.OK);
                    NoteNameTextBox.Focus();
                    return;
                }

                NoteName = nomeDaNota;
                SaveConfirmed = true;
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao salvar nota: {ex}");
                MessageWindow.Show(this, 
                    $"Erro ao salvar: {ex.Message}", 
                    "Erro", MessageBoxButton.OK);
            }
        }
    }
} 