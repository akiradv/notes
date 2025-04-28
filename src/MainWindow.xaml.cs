using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace Notes;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string notesFolder = string.Empty;
    private string currentFileName = string.Empty;
    private List<string> notesList = new List<string>();

    public MainWindow()
    {
        try
        {
            InitializeComponent();
            
            // Inicializar o armazenamento
            notesFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "iPodNotes");
            if (!Directory.Exists(notesFolder))
            {
                Directory.CreateDirectory(notesFolder);
            }
            
            notesList = new List<string>();
            UpdateNotesList();
            
            // Adicionar manipuladores de eventos
            NewButton.Click += NewButton_Click;
            SaveButton.Click += SaveButton_Click;
            ListButton.Click += ListButton_Click;
            
            // Adicionar animação de carregamento
            this.Loaded += MainWindow_Loaded;
            
            // Iniciar com uma nota em branco
            NewNote();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro na inicialização do aplicativo: {ex.Message}\n\nDetalhes: {ex.StackTrace}", 
                "Erro Crítico", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // Versão simplificada para evitar problemas
            this.Opacity = 1.0;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao carregar a janela: {ex.Message}", 
                "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void NewButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyButtonClickEffect(NewButton);
        NewNote();
    }
    
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyButtonClickEffect(SaveButton);
        SaveNote();
    }
    
    private void ListButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyButtonClickEffect(ListButton);
        ShowNotesList();
    }
    
    private void ApplyButtonClickEffect(Button button)
    {
        // Versão simplificada que não faz animações complexas
        try
        {
            // Apenas chamar o método sem efeitos visuais
        }
        catch
        {
            // Silenciosamente ignora erros de efeitos
        }
    }
    
    private void NewNote()
    {
        // Animação de transição
        AnimateTextBoxTransition(() =>
        {
            NotesTextBox.Clear();
            currentFileName = $"Nota_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            Title = "iPod Notes - Nova Nota";
        });
    }
    
    private void SaveNote()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(NotesTextBox.Text))
            {
                MessageBox.Show("Não é possível salvar uma nota vazia.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            string filePath = System.IO.Path.Combine(notesFolder, currentFileName);
            File.WriteAllText(filePath, NotesTextBox.Text);
            
            UpdateNotesList();
            
            MessageBox.Show("Nota salva com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            
            // Atualizar título com o nome do arquivo sem extensão
            Title = $"iPod Notes - {System.IO.Path.GetFileNameWithoutExtension(currentFileName)}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao salvar a nota: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void UpdateNotesList()
    {
        try
        {
            string[] files = Directory.GetFiles(notesFolder, "*.txt");
            notesList = files.Select(f => System.IO.Path.GetFileName(f) ?? string.Empty)
                             .Where(f => !string.IsNullOrEmpty(f))
                             .ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao atualizar lista de notas: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void ShowNotesList()
    {
        UpdateNotesList();
        
        if (notesList.Count == 0)
        {
            MessageBox.Show("Nenhuma nota encontrada.", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        try
        {
            // Criar uma janela estilizada para mostrar a lista de notas
            var notesListWindow = new Window
            {
                Title = "Notas Salvas",
                Width = 260,
                Height = 320,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this,
                Background = Brushes.Transparent,
                ResizeMode = ResizeMode.NoResize
            };
            
            // Criar o conteúdo principal
            var mainBorder = new Border
            {
                CornerRadius = new CornerRadius(12),
                Background = Brushes.White,
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Color.FromRgb(204, 204, 204))
            };
            
            // Aplicar efeito de sombra
            mainBorder.Effect = new DropShadowEffect
            {
                BlurRadius = 10,
                ShadowDepth = 1,
                Opacity = 0.2,
                Direction = 315,
                Color = Colors.Black
            };
            
            // Criar grid principal
            var mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            
            // Cabeçalho
            var headerBorder = new Border
            {
                Background = (SolidColorBrush)Application.Current.Resources["iPodHeaderBrush"],
                Padding = new Thickness(12, 10, 12, 10),
                CornerRadius = new CornerRadius(12, 12, 0, 0)
            };
            
            var headerTextBlock = new TextBlock
            {
                Text = "Notas Salvas",
                FontSize = 16,
                FontFamily = new FontFamily("Segoe UI, Arial"),
                Foreground = (SolidColorBrush)Application.Current.Resources["iPodTextBrush"],
                HorizontalAlignment = HorizontalAlignment.Center
            };
            
            headerBorder.Child = headerTextBlock;
            Grid.SetRow(headerBorder, 0);
            mainGrid.Children.Add(headerBorder);
            
            // Lista
            var listBox = new ListBox
            {
                Margin = new Thickness(8),
                BorderThickness = new Thickness(0),
                FontFamily = new FontFamily("Segoe UI, Arial"),
                FontSize = 14
            };
            
            foreach (var note in notesList)
            {
                listBox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(note));
            }
            
            listBox.SelectionChanged += (s, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    int index = listBox.SelectedIndex;
                    if (index >= 0 && index < notesList.Count)
                    {
                        string selectedNote = notesList[index];
                        LoadNote(selectedNote);
                        notesListWindow.Close();
                    }
                }
            };
            
            Grid.SetRow(listBox, 1);
            mainGrid.Children.Add(listBox);
            
            // Botão Fechar
            var closeButton = new Button
            {
                Content = "Fechar",
                Margin = new Thickness(0, 8, 0, 12),
                Width = 120,
                Style = (Style)Application.Current.Resources["iPodButtonStyle"]
            };
            
            closeButton.Click += (s, e) => notesListWindow.Close();
            
            var buttonPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center
            };
            buttonPanel.Children.Add(closeButton);
            
            Grid.SetRow(buttonPanel, 2);
            mainGrid.Children.Add(buttonPanel);
            
            mainBorder.Child = mainGrid;
            notesListWindow.Content = mainBorder;
            
            notesListWindow.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao mostrar a lista de notas: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void LoadNote(string fileName)
    {
        try
        {
            string filePath = System.IO.Path.Combine(notesFolder, fileName);
            string noteContent = File.ReadAllText(filePath);
            
            // Animação de transição
            AnimateTextBoxTransition(() =>
            {
                NotesTextBox.Text = noteContent;
                currentFileName = fileName;
                
                // Atualizar título
                Title = $"iPod Notes - {System.IO.Path.GetFileNameWithoutExtension(fileName)}";
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao carregar a nota: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void AnimateTextBoxTransition(Action afterTransition)
    {
        try
        {
            // Executar a ação diretamente, sem animação complexa
            afterTransition?.Invoke();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro durante a transição: {ex.Message}", 
                "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}