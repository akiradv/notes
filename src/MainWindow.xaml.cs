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
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Windows.Resources;
using System.Reflection;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string notesFolder = string.Empty;
        private string currentFileName = string.Empty;
        private List<string> notesList = new List<string>();
        private bool isDarkTheme = false;
        private string settingsFilePath = string.Empty;
        private double currentFontSize = 16.0; // Tamanho padrão da fonte
        private const double DEFAULT_FONT_SIZE = 16.0;
        private const double MIN_FONT_SIZE = 10.0;
        private const double MAX_FONT_SIZE = 24.0;
        private const double FONT_SIZE_STEP = 2.0;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                
                // Inicializar o armazenamento
                notesFolder = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                    "iNotes"
                );

                if (!Directory.Exists(notesFolder))
                {
                    Directory.CreateDirectory(notesFolder);
                }
                
                // Caminho para o arquivo de configurações
                settingsFilePath = System.IO.Path.Combine(notesFolder, "settings.ini");
                
                notesList = new List<string>();
                UpdateNotesList();
                
                // Adicionar manipuladores de eventos
                if (NewButton != null) NewButton.Click += NewButton_Click;
                if (SaveButton != null) SaveButton.Click += SaveButton_Click;
                if (ListButton != null) ListButton.Click += ListButton_Click;
                
                // Adicionar animação de carregamento
                this.Loaded += MainWindow_Loaded;
                
                // Iniciar com uma nota em branco
                NewNote();

                // Carregar preferências do usuário
                LoadUserPreferences();
            }
            catch (Exception ex)
            {
                MessageWindow.Show(this, 
                    $"Erro na inicialização do aplicativo: {ex.Message}", 
                    "Erro Crítico", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Carrega todas as preferências do usuário
        /// </summary>
        private void LoadUserPreferences()
        {
            LoadThemePreference();
            LoadFontSizePreference();
        }

        /// <summary>
        /// Carrega a preferência de tamanho de fonte do usuário
        /// </summary>
        private void LoadFontSizePreference()
        {
            try
            {
                if (File.Exists(settingsFilePath))
                {
                    var settings = File.ReadAllLines(settingsFilePath);
                    foreach (var line in settings)
                    {
                        if (line.StartsWith("FontSize="))
                        {
                            string value = line.Substring("FontSize=".Length);
                            if (double.TryParse(value, out double fontSize))
                            {
                                // Garantir que o valor está dentro dos limites
                                currentFontSize = Math.Clamp(fontSize, MIN_FONT_SIZE, MAX_FONT_SIZE);
                                NotesTextBox.FontSize = currentFontSize;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao carregar preferência de tamanho de fonte: {ex.Message}");
                // Em caso de erro, apenas use o tamanho padrão
                currentFontSize = DEFAULT_FONT_SIZE;
                NotesTextBox.FontSize = currentFontSize;
            }
        }

        /// <summary>
        /// Salva a preferência de tamanho de fonte do usuário
        /// </summary>
        private void SaveFontSizePreference()
        {
            try
            {
                bool fontSizeUpdated = false;
                List<string> lines = new List<string>();
                
                if (File.Exists(settingsFilePath))
                {
                    lines = File.ReadAllLines(settingsFilePath).ToList();
                    
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].StartsWith("FontSize="))
                        {
                            lines[i] = $"FontSize={currentFontSize}";
                            fontSizeUpdated = true;
                            break;
                        }
                    }
                }
                
                if (!fontSizeUpdated)
                {
                    lines.Add($"FontSize={currentFontSize}");
                }
                
                File.WriteAllLines(settingsFilePath, lines);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao salvar preferência de tamanho de fonte: {ex.Message}");
            }
        }

        /// <summary>
        /// Anima a mudança do tamanho da fonte
        /// </summary>
        private void AnimateFontSizeChange(double targetFontSize)
        {
            try
            {
                DoubleAnimation fontSizeAnimation = new DoubleAnimation
                {
                    From = NotesTextBox.FontSize,
                    To = targetFontSize,
                    Duration = TimeSpan.FromMilliseconds(200),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };
                
                NotesTextBox.BeginAnimation(TextBox.FontSizeProperty, fontSizeAnimation);
            }
            catch
            {
                // Em caso de falha na animação, aplique diretamente
                NotesTextBox.FontSize = targetFontSize;
            }
        }

        private void LoadTheme(bool isDark)
        {
            isDarkTheme = isDark;
            
            // Aplicar uma animação suave antes de mudar o tema
            var preTransitionAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.85,
                Duration = TimeSpan.FromMilliseconds(150)
            };

            preTransitionAnimation.Completed += (s, e) =>
            {
                // Atualizar o ícone do botão de tema
                if (this.FindName("ThemeIcon") is System.Windows.Shapes.Path themeIcon && themeIcon != null)
                {
                    if (isDark)
                    {
                        // Ícone de sol para o modo escuro
                        themeIcon.Data = Geometry.Parse("M12,7A5,5 0 0,1 17,12A5,5 0 0,1 12,17A5,5 0 0,1 7,12A5,5 0 0,1 12,7M12,9A3,3 0 0,0 9,12A3,3 0 0,0 12,15A3,3 0 0,0 15,12A3,3 0 0,0 12,9M12,2L14.39,5.42C13.65,5.15 12.84,5 12,5C11.16,5 10.35,5.15 9.61,5.42L12,2M3.34,7L7.5,6.65C6.9,7.16 6.36,7.78 5.94,8.5C5.5,9.24 5.25,10 5.11,10.79L3.34,7M3.36,17L5.12,13.23C5.26,14 5.53,14.78 5.95,15.5C6.37,16.24 6.91,16.86 7.5,17.37L3.36,17M20.65,7L18.88,10.79C18.74,10 18.47,9.23 18.05,8.5C17.63,7.78 17.1,7.15 16.5,6.64L20.65,7M20.64,17L16.5,17.36C17.09,16.85 17.62,16.22 18.04,15.5C18.46,14.77 18.73,14 18.87,13.21L20.64,17M12,22L9.59,18.56C10.33,18.83 11.14,19 12,19C12.82,19 13.63,18.83 14.37,18.56L12,22Z");
                        themeIcon.ToolTip = "Mudar para tema claro";
                    }
                    else
                    {
                        // Ícone de lua para o modo claro
                        themeIcon.Data = Geometry.Parse("M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2M12,4A8,8 0 0,1 20,12A8,8 0 0,1 12,20V4Z");
                        themeIcon.ToolTip = "Mudar para tema escuro";
                    }
                }
                
                // Atualizar as cores do tema
                var resources = Application.Current.Resources;
                string prefix = isDark ? "DarkTheme." : "LightTheme.";
                
                try
                {
                    // Lista de chaves de recursos a serem atualizados
                    string[] resourceKeys = new string[] { 
                        "iPodBackgroundBrush", "iPodHeaderBrush", "iPodAccentBrush", 
                        "iPodTextBrush", "iPodLightGrayBrush", "iPodBorderBrush", "iPodShadowBrush" 
                    };
                    
                    // Atualizar cada recurso com a versão do tema atual
                    foreach (string key in resourceKeys)
                    {
                        string sourceKey = prefix + key;
                        if (resources.Contains(key) && resources.Contains(sourceKey))
                        {
                            resources[key] = resources[sourceKey];
                        }
                    }
                    
                    // Forçar atualização visual de elementos específicos
                    // 1. Encontrar os elementos principais do MainWindow e atualizar suas cores
                    if (this.FindName("NotesTextBox") is TextBox notesTextBox)
                    {
                        notesTextBox.Background = (SolidColorBrush)resources["iPodBackgroundBrush"];
                        notesTextBox.Foreground = (SolidColorBrush)resources["iPodTextBrush"];
                    }
                    
                    // 2. Encontrar e atualizar o background principal da interface
                    if (this.Content is Border mainBorder)
                    {
                        mainBorder.Background = (SolidColorBrush)resources["iPodBackgroundBrush"];
                        mainBorder.BorderBrush = (SolidColorBrush)resources["iPodBorderBrush"];
                        
                        // Se a borda principal contém um Grid, atualize seus filhos
                        if (mainBorder.Child is Grid mainGrid)
                        {
                            // Atualizar o cabeçalho
                            if (mainGrid.Children.Count > 0 && mainGrid.Children[0] is Border headerBorder)
                            {
                                headerBorder.Background = (SolidColorBrush)resources["iPodHeaderBrush"];
                            }
                            
                            // Atualizar a área de texto
                            if (mainGrid.Children.Count > 1 && mainGrid.Children[1] is Border textAreaBorder)
                            {
                                textAreaBorder.Background = (SolidColorBrush)resources["iPodBackgroundBrush"];
                                textAreaBorder.BorderBrush = (SolidColorBrush)resources["iPodLightGrayBrush"];
                            }
                            
                            // Atualizar a barra inferior
                            if (mainGrid.Children.Count > 2 && mainGrid.Children[2] is Border bottomBorder)
                            {
                                bottomBorder.Background = (SolidColorBrush)resources["iPodHeaderBrush"];
                            }
                            
                            // Atualizar explicitamente o título "iNotes"
                            if (mainGrid.Children.Count > 0 && mainGrid.Children[0] is Border titleBorder)
                            {
                                if (titleBorder.Child is Grid titleGrid)
                                {
                                    foreach (var child in titleGrid.Children)
                                    {
                                        if (child is TextBlock titleTextBlock && titleTextBlock.Text == "iNotes")
                                        {
                                            titleTextBlock.Foreground = (SolidColorBrush)resources["iPodTextBrush"];
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    // Aplicar uma animação suave de retorno
                    var afterTransitionAnimation = new DoubleAnimation
                    {
                        From = 0.85,
                        To = 1.0,
                        Duration = TimeSpan.FromMilliseconds(200),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                    };
                    
                    this.BeginAnimation(UIElement.OpacityProperty, afterTransitionAnimation);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao alterar o tema: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                    // Garantir que retornamos a opacidade normal mesmo em caso de erro
                    this.Opacity = 1.0;
                }
            };
            
            // Iniciar a animação de transição
            this.BeginAnimation(UIElement.OpacityProperty, preTransitionAnimation);
        }

        private void AnimateThemeTransition()
        {
            try
            {
                // Uma animação de fade para suavizar a transição
                var fadeOutAnimation = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.9,
                    Duration = TimeSpan.FromMilliseconds(150)
                };
                
                var fadeInAnimation = new DoubleAnimation
                {
                    From = 0.9,
                    To = 1.0,
                    Duration = TimeSpan.FromMilliseconds(200),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };
                
                fadeOutAnimation.Completed += (s, e) =>
                {
                    // Iniciar o fade in após o fade out
                    this.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
                };
                
                // Iniciar a animação de fade out
                this.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
            }
            catch
            {
                // Ignorar erros de animação
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Opacity = 1.0;
            }
            catch (Exception ex)
            {
                MessageWindow.Show(this, 
                    $"Erro ao carregar a janela: {ex.Message}", 
                    "Erro", MessageBoxButton.OK);
            }
        }
        
        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewButton != null) ApplyButtonClickEffect(NewButton);
            NewNote();
        }
        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SaveButton != null) ApplyButtonClickEffect(SaveButton);
            SaveNote();
        }
        
        private void ListButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListButton != null) ApplyButtonClickEffect(ListButton);
            ShowNotesList();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExportButton != null) ApplyButtonClickEffect(ExportButton);
            ExportNote();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImportButton != null) ApplyButtonClickEffect(ImportButton);
            ImportNote();
        }
        
        private void ApplyButtonClickEffect(Button button)
        {
            try
            {
                var animation = new ColorAnimation
                {
                    To = ((SolidColorBrush)Application.Current.Resources["iPodLightGrayBrush"]).Color,
                    Duration = TimeSpan.FromMilliseconds(100),
                    AutoReverse = true
                };

                button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            }
            catch
            {
                // Silenciosamente ignora erros de efeitos
            }
        }
        
        private void NewNote()
        {
            AnimateTextBoxTransition(() =>
            {
                if (NotesTextBox != null)
                {
                    NotesTextBox.Clear();
                    currentFileName = $"Nota_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                    Title = "iNotes - Nova Nota";
                }
            });
        }
        
        private void SaveNote()
        {
            try
            {
                // Verificar se o TextBox existe e tem conteúdo
                if (NotesTextBox == null)
                {
                    MessageWindow.Show(this, 
                        "Erro interno: Campo de texto não encontrado.", 
                        "Erro", MessageBoxButton.OK);
                    return;
                }

                string conteudoNota = NotesTextBox.Text?.Trim() ?? string.Empty;
                
                if (string.IsNullOrWhiteSpace(conteudoNota))
                {
                    MessageWindow.Show(this, 
                        "A nota está vazia!\nPor favor, escreva algo antes de salvar.", 
                        "Nota Vazia", MessageBoxButton.OK);
                    if (NotesTextBox != null)
                    {
                        NotesTextBox.Focus();
                    }
                    return;
                }
                
                try
                {
                    var saveDialog = new SaveNoteWindow(System.IO.Path.GetFileNameWithoutExtension(currentFileName));
                    saveDialog.Owner = this;
                    saveDialog.ShowDialog();

                    if (saveDialog.SaveConfirmed && !string.IsNullOrWhiteSpace(saveDialog.NoteName))
                    {
                        string newFileName = $"{saveDialog.NoteName}.txt";
                        if (!newFileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            newFileName += ".txt";
                        }

                        string filePath = System.IO.Path.Combine(notesFolder, newFileName);
                        
                        // Se já existe um arquivo com este nome, perguntar se quer sobrescrever
                        if (File.Exists(filePath) && newFileName != currentFileName)
                        {
                            var result = MessageWindow.Show(this, 
                                "Já existe uma nota com este nome. Deseja sobrescrever?",
                                "Nota Existente", MessageBoxButton.YesNo);
                            
                            if (result != MessageBoxResult.Yes)
                            {
                                return;
                            }
                        }

                        try
                        {
                            // Salvar o conteúdo da nota
                            File.WriteAllText(filePath, conteudoNota);
                            currentFileName = newFileName;
                            
                            UpdateNotesList();
                            
                            // Atualizar título com o nome do arquivo sem extensão
                            Title = $"iNotes - {System.IO.Path.GetFileNameWithoutExtension(currentFileName)}";

                            // Mostrar mensagem de sucesso
                            MessageWindow.Show(this, 
                                "Nota salva com sucesso!", 
                                "Sucesso", MessageBoxButton.OK);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Erro ao salvar arquivo: {ex}");
                            MessageWindow.Show(this, 
                                $"Erro ao salvar o arquivo: {ex.Message}", 
                                "Erro ao Salvar", MessageBoxButton.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Erro ao mostrar diálogo de salvamento: {ex}");
                    MessageWindow.Show(this, 
                        $"Erro ao abrir janela de salvamento: {ex.Message}", 
                        "Erro", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro geral ao salvar nota: {ex}");
                MessageWindow.Show(this, 
                    $"Erro ao salvar a nota: {ex.Message}", 
                    "Erro", MessageBoxButton.OK);
            }
        }

        private void ExportNote()
        {
            try
            {
                // Verificar se existe conteúdo para exportar
                if (NotesTextBox == null || string.IsNullOrWhiteSpace(NotesTextBox.Text))
                {
                    MessageWindow.Show(this, 
                        "Não há conteúdo para exportar. Por favor, escreva ou abra uma nota primeiro.", 
                        "Exportação", MessageBoxButton.OK);
                    return;
                }

                // Criar diálogo de salvamento
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Arquivo de Texto (.txt)|*.txt|Documento PDF (.pdf)|*.pdf|Todos os arquivos|*.*",
                    Title = "Exportar Nota",
                    FileName = System.IO.Path.GetFileNameWithoutExtension(currentFileName)
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string extension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                    string conteudo = NotesTextBox.Text;

                    if (extension == ".pdf")
                    {
                        ExportToPdf(conteudo, saveFileDialog.FileName);
                    }
                    else
                    {
                        // Exportar como texto
                        File.WriteAllText(saveFileDialog.FileName, conteudo);
                        MessageWindow.Show(this, 
                            "Nota exportada com sucesso!", 
                            "Exportação", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageWindow.Show(this, 
                    $"Erro ao exportar nota: {ex.Message}", 
                    "Erro", MessageBoxButton.OK);
            }
        }

        private void ExportToPdf(string content, string filePath)
        {
            try
            {
                // Configurar codificação para UTF-8
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                // Criar um novo documento PDF
                PdfDocument pdfDocument = new PdfDocument();
                pdfDocument.Info.Title = System.IO.Path.GetFileNameWithoutExtension(currentFileName);
                pdfDocument.Info.Author = "iNotes";
                pdfDocument.Info.Subject = "Nota exportada do iNotes";
                pdfDocument.Info.CreationDate = DateTime.Now;
                
                // Adicionar uma página
                PdfPage page = pdfDocument.AddPage();
                
                // Configurar o tamanho da página
                page.Size = PdfSharp.PageSize.A4;
                
                // Criar um objeto de desenho
                XGraphics gfx = XGraphics.FromPdfPage(page);
                
                // Configurar a fonte
                XFont titleFont = new XFont("Arial", 16, XFontStyle.Bold);
                XFont normalFont = new XFont("Arial", 12);
                XFont footerFont = new XFont("Arial", 8);
                
                // Desenhar o título
                string title = System.IO.Path.GetFileNameWithoutExtension(currentFileName);
                gfx.DrawString(title, titleFont, XBrushes.Black, new XRect(30, 30, page.Width, 50), XStringFormats.TopLeft);
                
                // Desenhar uma linha separadora
                gfx.DrawLine(new XPen(XColors.DarkGray, 1), 30, 60, page.Width - 30, 60);
                
                // Desenhar o conteúdo - dividir em linhas e desenhar cada uma
                string[] lines = content.Split('\n');
                double yPosition = 70;
                double lineHeight = 14; // Altura fixa da linha
                
                foreach (string line in lines)
                {
                    gfx.DrawString(line, normalFont, XBrushes.Black, new XRect(30, yPosition, page.Width - 60, lineHeight), XStringFormats.TopLeft);
                    yPosition += lineHeight * 1.2; // Espaçamento entre linhas
                    
                    // Se chegou ao fim da página, adicionar nova página
                    if (yPosition > page.Height - 50)
                    {
                        page = pdfDocument.AddPage();
                        page.Size = PdfSharp.PageSize.A4;
                        gfx = XGraphics.FromPdfPage(page);
                        yPosition = 30;
                    }
                }
                
                // Desenhar o rodapé com data e hora
                string footer = $"Exportado em {DateTime.Now:dd/MM/yyyy HH:mm}";
                gfx.DrawString(footer, footerFont, XBrushes.Gray, 
                    new XRect(30, page.Height - 30, page.Width - 60, 20), XStringFormats.TopRight);
                
                // Salvar o documento
                pdfDocument.Save(filePath);
                
                MessageWindow.Show(this, 
                    "Nota exportada como PDF com sucesso!", 
                    "Exportação PDF", MessageBoxButton.OK);
                
                // Perguntar se deseja abrir o PDF
                var result = MessageWindow.Show(this, 
                    "Deseja abrir o PDF agora?", 
                    "Abrir PDF", MessageBoxButton.YesNo);
                
                if (result == MessageBoxResult.Yes)
                {
                    // Abrir o PDF com o visualizador padrão
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao criar PDF: {ex.Message}", ex);
            }
        }

        private void ImportNote()
        {
            try
            {
                // Criar diálogo de abertura
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Arquivos de Texto (.txt)|*.txt|Todos os arquivos|*.*",
                    Title = "Importar Nota"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    string conteudo = File.ReadAllText(openFileDialog.FileName);
                    
                    // Perguntar se deseja substituir o conteúdo atual
                    if (!string.IsNullOrWhiteSpace(NotesTextBox.Text) && NotesTextBox.Text.Trim().Length > 0)
                    {
                        var result = MessageWindow.Show(this, 
                            "Deseja substituir o conteúdo atual da nota?", 
                            "Importação", MessageBoxButton.YesNo);
                        
                        if (result != MessageBoxResult.Yes)
                        {
                            // Concatenar o conteúdo importado
                            NotesTextBox.Text += Environment.NewLine + Environment.NewLine + conteudo;
                            return;
                        }
                    }

                    // Substituir conteúdo
                    NotesTextBox.Text = conteudo;
                    
                    // Sugerir um nome baseado no arquivo importado
                    currentFileName = System.IO.Path.GetFileName(openFileDialog.FileName);
                    if (!currentFileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        currentFileName += ".txt";
                    }
                    
                    // Atualizar título
                    Title = $"iNotes - {System.IO.Path.GetFileNameWithoutExtension(currentFileName)}";
                    
                    MessageWindow.Show(this, 
                        "Nota importada com sucesso!", 
                        "Importação", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageWindow.Show(this, 
                    $"Erro ao importar nota: {ex.Message}", 
                    "Erro", MessageBoxButton.OK);
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
                MessageWindow.Show(this, 
                    $"Erro ao atualizar lista de notas: {ex.Message}", 
                    "Erro", MessageBoxButton.OK);
            }
        }
        
        private void ShowNotesList()
        {
            try
            {
                // Atualizar a lista de notas
                UpdateNotesList();
                
                // Criar a janela de lista de notas
                var listWindow = new Window
                {
                    Title = "Notas",
                    Width = 250,
                    Height = 330,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = this,
                    Background = Brushes.Transparent,
                    WindowStyle = WindowStyle.None,
                    AllowsTransparency = true,
                    ResizeMode = ResizeMode.NoResize
                };
                
                // Criar borda arredondada
                var mainBorder = new Border
                {
                    CornerRadius = new CornerRadius(12),
                    BorderThickness = new Thickness(1),
                    BorderBrush = (SolidColorBrush)Application.Current.Resources["iPodBorderBrush"],
                    Margin = new Thickness(5),
                    Background = (SolidColorBrush)Application.Current.Resources["iPodBackgroundBrush"]
                };
                
                // Grid principal
                var mainGrid = new Grid();
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                
                // Cabeçalho
                var headerBorder = new Border
                {
                    Background = (SolidColorBrush)Application.Current.Resources["iPodHeaderBrush"],
                    Padding = new Thickness(12, 8, 12, 8),
                    CornerRadius = new CornerRadius(12, 12, 0, 0)
                };
                
                var headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                
                var titleTextBlock = new TextBlock
                {
                    Text = "Minhas Notas",
                    FontSize = 13,
                    FontFamily = new FontFamily("Segoe UI, Arial"),
                    Foreground = (SolidColorBrush)Application.Current.Resources["iPodTextBrush"],
                    FontWeight = FontWeights.Regular,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                
                var closeButton = new Button
                {
                    Style = (Style)Application.Current.Resources["CloseButtonStyle"],
                    Cursor = Cursors.Hand,
                    Margin = new Thickness(0, -1, 0, 0)
                };
                closeButton.Click += (s, e) => listWindow.Close();
                
                headerGrid.Children.Add(titleTextBlock);
                headerGrid.Children.Add(closeButton);
                Grid.SetColumn(titleTextBlock, 0);
                Grid.SetColumn(closeButton, 1);
                
                headerBorder.Child = headerGrid;
                
                // ListBox para as notas - Definir antes de usar
                var notesListBox = new ListBox
                {
                    Style = (Style)Application.Current.Resources["iPodListBoxStyle"],
                    Margin = new Thickness(0),
                    BorderThickness = new Thickness(0),
                    ItemContainerStyle = (Style)Application.Current.Resources["iPodListBoxItemStyle"]
                };
                
                // Área de pesquisa
                var searchBorder = new Border
                {
                    Background = (SolidColorBrush)Application.Current.Resources["iPodBackgroundBrush"],
                    Padding = new Thickness(10, 5, 10, 5),
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    BorderBrush = (SolidColorBrush)Application.Current.Resources["iPodLightGrayBrush"]
                };
                
                var searchBox = new TextBox
                {
                    Style = (Style)Application.Current.Resources["iPodTextBoxStyle"],
                    BorderThickness = new Thickness(1),
                    BorderBrush = (SolidColorBrush)Application.Current.Resources["iPodLightGrayBrush"],
                    Padding = new Thickness(8, 4, 8, 4),
                    FontSize = 12,
                    TextWrapping = TextWrapping.NoWrap,
                    AcceptsReturn = false,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Height = 24
                };
                
                // Adicionar texto de placeholder usando um TextBlock transparente
                var searchGrid = new Grid();
                
                // Criar uma cor de placeholder adequada para ambos os temas
                var placeholderBrush = new SolidColorBrush(
                    isDarkTheme ? 
                    Color.FromArgb(150, 200, 200, 200) : 
                    Color.FromArgb(128, 128, 128, 128)
                );
                
                var searchPlaceholder = new TextBlock
                {
                    Text = "Pesquisar notas...",
                    Foreground = placeholderBrush,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0),
                    IsHitTestVisible = false
                };
                
                searchGrid.Children.Add(searchBox);
                searchGrid.Children.Add(searchPlaceholder);
                
                // Esconder o placeholder quando o texto for digitado
                searchBox.TextChanged += (s, e) => 
                {
                    searchPlaceholder.Visibility = string.IsNullOrEmpty(searchBox.Text) 
                        ? Visibility.Visible 
                        : Visibility.Collapsed;
                    
                    FilterNotes(notesListBox, searchBox.Text);
                };
                
                searchBorder.Child = searchGrid;
                
                // Adicionar notas ao ListBox
                foreach (var noteName in notesList)
                {
                    string displayName = System.IO.Path.GetFileNameWithoutExtension(noteName);
                    var noteItem = new ListBoxItem
                    {
                        Content = displayName,
                        Tag = noteName,
                        Style = (Style)Application.Current.Resources["iPodListBoxItemStyle"]
                    };
                    notesListBox.Items.Add(noteItem);
                }
                
                // Configurar evento de clique no item da lista
                notesListBox.SelectionChanged += (s, e) =>
                {
                    if (notesListBox.SelectedItem != null)
                    {
                        var selectedNote = ((ListBoxItem)notesListBox.SelectedItem).Tag.ToString();
                        LoadNote(selectedNote);
                        listWindow.Close();
                    }
                };
                
                // Botões
                var buttonsBorder = new Border
                {
                    Background = (SolidColorBrush)Application.Current.Resources["iPodHeaderBrush"],
                    CornerRadius = new CornerRadius(0, 0, 12, 12),
                    Padding = new Thickness(0, 8, 0, 8)
                };
                
                var buttonsPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                
                var newNoteButton = new Button
                {
                    Content = "Nova Nota",
                    Style = (Style)Application.Current.Resources["iPodButtonStyle"],
                    Width = 100,
                    Height = 28,
                    Margin = new Thickness(4, 0, 4, 0),
                    Foreground = (SolidColorBrush)Application.Current.Resources["iPodTextBrush"]
                };
                
                var cancelButton = new Button
                {
                    Content = "Cancelar",
                    Style = (Style)Application.Current.Resources["iPodButtonStyle"],
                    Width = 100,
                    Height = 28,
                    Margin = new Thickness(4, 0, 4, 0),
                    Foreground = (SolidColorBrush)Application.Current.Resources["iPodTextBrush"]
                };
                
                newNoteButton.Click += (s, e) =>
                {
                    NewNote();
                    listWindow.Close();
                };
                
                cancelButton.Click += (s, e) => listWindow.Close();
                
                buttonsPanel.Children.Add(newNoteButton);
                buttonsPanel.Children.Add(cancelButton);
                buttonsBorder.Child = buttonsPanel;
                
                // Montar a janela
                mainGrid.Children.Add(headerBorder);
                mainGrid.Children.Add(searchBorder);
                mainGrid.Children.Add(notesListBox);
                mainGrid.Children.Add(buttonsBorder);
                
                Grid.SetRow(headerBorder, 0);
                Grid.SetRow(searchBorder, 1);
                Grid.SetRow(notesListBox, 2);
                Grid.SetRow(buttonsBorder, 3);
                
                mainBorder.Child = mainGrid;
                listWindow.Content = mainBorder;
                
                // Habilitar arrastar a janela
                headerBorder.MouseLeftButtonDown += (s, e) => listWindow.DragMove();
                
                // Aplicar efeito de sombra
                listWindow.Effect = new DropShadowEffect
                {
                    BlurRadius = 10,
                    ShadowDepth = 1,
                    Opacity = 0.2,
                    Direction = 315,
                    Color = Colors.Black
                };
                
                // Mostrar a janela
                listWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageWindow.Show(this, 
                    $"Erro ao mostrar lista de notas: {ex.Message}", 
                    "Erro", MessageBoxButton.OK);
            }
        }
        
        /// <summary>
        /// Filtra as notas com base no termo de pesquisa
        /// </summary>
        private void FilterNotes(ListBox notesListBox, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // Se não houver termo de pesquisa, mostrar todas as notas
                notesListBox.Items.Clear();
                foreach (var noteName in notesList)
                {
                    string displayName = System.IO.Path.GetFileNameWithoutExtension(noteName);
                    var noteItem = new ListBoxItem
                    {
                        Content = displayName,
                        Tag = noteName,
                        Style = (Style)Application.Current.Resources["iPodListBoxItemStyle"]
                    };
                    notesListBox.Items.Add(noteItem);
                }
            }
            else
            {
                // Filtrar notas pelo termo de pesquisa
                searchTerm = searchTerm.ToLower();
                notesListBox.Items.Clear();
                
                foreach (var noteName in notesList)
                {
                    var fileName = System.IO.Path.GetFileNameWithoutExtension(noteName).ToLower();
                    
                    // Verifique se o nome contém o termo de pesquisa
                    if (fileName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        string displayName = System.IO.Path.GetFileNameWithoutExtension(noteName);
                        var noteItem = new ListBoxItem
                        {
                            Content = displayName,
                            Tag = noteName,
                            Style = (Style)Application.Current.Resources["iPodListBoxItemStyle"]
                        };
                        notesListBox.Items.Add(noteItem);
                    }
                    else
                    {
                        // Também verificar o conteúdo da nota
                        try
                        {
                            string filePath = System.IO.Path.Combine(notesFolder, noteName);
                            string content = File.ReadAllText(filePath).ToLower();
                            
                            if (content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                            {
                                string displayName = System.IO.Path.GetFileNameWithoutExtension(noteName);
                                var noteItem = new ListBoxItem
                                {
                                    Content = displayName,
                                    Tag = noteName,
                                    Style = (Style)Application.Current.Resources["iPodListBoxItemStyle"]
                                };
                                notesListBox.Items.Add(noteItem);
                            }
                        }
                        catch
                        {
                            // Ignorar erros de leitura do arquivo
                        }
                    }
                }
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
                    Title = $"iNotes - {System.IO.Path.GetFileNameWithoutExtension(fileName)}";
                });
            }
            catch (Exception ex)
            {
                MessageWindow.Show(this, 
                    $"Erro ao carregar a nota: {ex.Message}", 
                    "Erro", MessageBoxButton.OK);
            }
        }

        private void AnimateTextBoxTransition(Action afterTransition)
        {
            try
            {
                // Parte 1: Animação de saída (efeito de virar para a esquerda)
                TransformGroup transformGroup = new TransformGroup();
                ScaleTransform scaleTransform = new ScaleTransform(1, 1);
                SkewTransform skewTransform = new SkewTransform(0, 0);
                
                transformGroup.Children.Add(scaleTransform);
                transformGroup.Children.Add(skewTransform);
                
                NotesTextBox.RenderTransform = transformGroup;
                NotesTextBox.RenderTransformOrigin = new Point(0, 0.5);
                
                // Sequência de animações para criar efeito de "virar página"
                DoubleAnimation scaleXAnimation = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.5,
                    Duration = TimeSpan.FromMilliseconds(150),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                };
                
                DoubleAnimation skewAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = -15,
                    Duration = TimeSpan.FromMilliseconds(150),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                };
                
                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.2,
                    Duration = TimeSpan.FromMilliseconds(150),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                };
                
                // Quando a animação de saída terminar, execute a ação e inicie a animação de entrada
                opacityAnimation.Completed += (s, e) =>
                {
                    // Executar a ação (por exemplo, carregar nova nota)
                    afterTransition?.Invoke();
                    
                    // Parte 2: Animação de entrada (efeito de virar da direita)
                    scaleTransform.ScaleX = 0.5;
                    skewTransform.AngleX = 15; // Ângulo oposto para entrada
                    NotesTextBox.RenderTransformOrigin = new Point(1, 0.5); // Origem na direita
                    NotesTextBox.Opacity = 0.2;
                    
                    // Animações de entrada
                    DoubleAnimation scaleXAnimationIn = new DoubleAnimation
                    {
                        From = 0.5,
                        To = 1.0,
                        Duration = TimeSpan.FromMilliseconds(200),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                    };
                    
                    DoubleAnimation skewAnimationIn = new DoubleAnimation
                    {
                        From = 15,
                        To = 0,
                        Duration = TimeSpan.FromMilliseconds(200),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                    };
                    
                    DoubleAnimation opacityAnimationIn = new DoubleAnimation
                    {
                        From = 0.2,
                        To = 1.0,
                        Duration = TimeSpan.FromMilliseconds(200),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                    };
                    
                    // Iniciar animações de entrada
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimationIn);
                    skewTransform.BeginAnimation(SkewTransform.AngleXProperty, skewAnimationIn);
                    NotesTextBox.BeginAnimation(UIElement.OpacityProperty, opacityAnimationIn);
                };
                
                // Iniciar animações de saída
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
                skewTransform.BeginAnimation(SkewTransform.AngleXProperty, skewAnimation);
                NotesTextBox.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            }
            catch
            {
                // Em caso de falha na animação, apenas execute a ação
                afterTransition?.Invoke();
            }
        }

        // Método para permitir arrastar a janela
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Permitir arrastar a janela quando o usuário clicar no cabeçalho
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        // Método para fechar a janela
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadThemePreference()
        {
            try
            {
                // Verificar se o arquivo de configurações existe
                if (File.Exists(settingsFilePath))
                {
                    // Ler as configurações
                    string[] settings = File.ReadAllLines(settingsFilePath);
                    foreach (string line in settings)
                    {
                        if (line.StartsWith("DarkTheme="))
                        {
                            bool.TryParse(line.Substring("DarkTheme=".Length), out isDarkTheme);
                            break;
                        }
                    }
                }
                
                // Aplicar o tema baseado na preferência
                LoadTheme(isDarkTheme);
            }
            catch
            {
                // Em caso de erro, carrega o tema padrão (claro)
                LoadTheme(false);
            }
        }

        private void SaveThemePreference()
        {
            try
            {
                // Salvar a preferência de tema
                File.WriteAllText(settingsFilePath, $"DarkTheme={isDarkTheme}");
            }
            catch
            {
                // Ignorar erros ao salvar preferências
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Aplicar efeito ao botão
            if (SettingsButton != null) ApplyButtonClickEffect(SettingsButton);

            try
            {
                // Animar transição para a janela de configurações
                // Primeiro, aplicamos um efeito de desbotamento à janela principal
                DoubleAnimation fadeAnimation = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.8,
                    Duration = TimeSpan.FromMilliseconds(150),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };
                
                fadeAnimation.Completed += (s, args) =>
                {
                    // Criar janela de configurações com os valores atuais
                    var settingsWindow = new SettingsWindow(currentFontSize, isDarkTheme);
                    settingsWindow.Owner = this;
                    
                    // Manipulador para quando a janela de configurações fechar
                    settingsWindow.Closed += (s2, args2) =>
                    {
                        // Animar a janela principal retornando à opacidade normal
                        DoubleAnimation fadeBackAnimation = new DoubleAnimation
                        {
                            From = 0.8,
                            To = 1.0,
                            Duration = TimeSpan.FromMilliseconds(200),
                            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                        };
                        
                        this.BeginAnimation(UIElement.OpacityProperty, fadeBackAnimation);
                        
                        // Verificar se as configurações foram aplicadas
                        if (settingsWindow.SettingsApplied)
                        {
                            // Atualizar tamanho da fonte se mudou
                            if (Math.Abs(currentFontSize - settingsWindow.SelectedFontSize) > 0.1)
                            {
                                currentFontSize = settingsWindow.SelectedFontSize;
                                AnimateFontSizeChange(currentFontSize);
                                SaveFontSizePreference();
                            }
                            
                            // Atualizar tema se mudou
                            if (isDarkTheme != settingsWindow.IsDarkTheme)
                            {
                                LoadTheme(settingsWindow.IsDarkTheme);
                                SaveThemePreference();
                            }
                            
                            // Mostrar mensagem de confirmação
                            MessageWindow.Show(this, "Configurações aplicadas com sucesso!", 
                                "Configurações", MessageBoxButton.OK);
                        }
                    };
                    
                    // Mostrar janela de configurações
                    settingsWindow.ShowDialog();
                };
                
                // Iniciar animação de desvanecimento
                this.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
            }
            catch (Exception ex)
            {
                // Garantir que a opacidade volte ao normal em caso de erro
                this.Opacity = 1.0;
                
                MessageWindow.Show(this, 
                    $"Erro ao abrir configurações: {ex.Message}", 
                    "Erro", MessageBoxButton.OK);
            }
        }
    }
}