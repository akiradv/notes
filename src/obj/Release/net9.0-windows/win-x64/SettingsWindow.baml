   M S B A M L   `   `   `  ���� @  <Notes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:NotesNotes      Notes.SettingsWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:Notes       � Title$  Configurações��$	��380��$	��300�� � WindowStartupLocation$ CenterOwner=�
������  � WindowStyle$
 None=� � AllowsTransparency$
 True�� � 
ResizeMode$ NoResize=�.����  �� CornerRadius$ 12��$��1q�   iPodBorderBrush#��[  $��5q�  iPodBackgroundBrush#��[ ._��  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� -     iPodHeaderBrush#��[ $��0�� �� Padding$ 	12,8,12,8q�$ 	12,12,0,0��  SoftShadowEffect J� Effect# [ ._�� 	 � ColumnDefinitions	 �� $��* ��� $
��Auto �.+��� $��Configurações��$��16(�$��Segoe UI, Arial)�  iPodTextBrush#��[ $��Medium"�$��0��$��Center=�$��Center=��� -   $��1��  CloseButtonStyle#��[ $��0,-1,0,0q�
 � Cursor$

 Hand���� $��1��$
��Auto=�$��Disabled=�.���� $��10,10,10,10q�#��[ ._��� $��5q�.+��� $��0,0,0,1q�  iPodLightGrayBrush#��[ $ 0,0,0,15q�$��0,0,0,15q�._��� .+��� $
��Tema��$��SemiBold"�$��14(�#��[ $��0,0,0,12q���  �� Orientation$ 
Horizontal=�$��Center=�.+�	� -    	�Name$ LightThemeRadio��$��Claro�� 	� 	GroupName$ Theme��$��0,0,35,0q� d� 	IsChecked$
 TrueY�$��13(�  iPodRadioButtonStyle#��[ #��[ 	� -   $ DarkThemeRadio��$��Escuro��$ Theme��#��[ $��13(�#��[ �� $��0,0,0,1q�#��[ $ 0,0,0,15q�$��0,0,0,10q�._��� .+��� $ ��Tamanho da Fonte das Notas��$��SemiBold"�$��14(�#��[ $��0,0,0,12q�� $��0,0,0,8q�	 �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��A��$��0��$��12(�#��[ $��Center=�$��0,0,5,0q��� -    ��Name$ FontSizeSlider��$��1�� � Minimum$ 10Q� � Maximum$ 24Q� � Value$ 16Q� �� IsSnapToTickEnabled$
 True�� �� TickFrequency$ 2Q�  iPodSliderStyle#��[ �� $��A��$��2��$��20(�#��[ $��Center=�$��5,0,0,0q��� $ 
Horizontal=�$��Center=�.+��� $��Tamanho atual: ��$��13(�#��[ �� -    ��Name$ FontSizeText��$��16��$��13(� 	 iPodAccentBrush#��[	 $��SemiBold"��� $	�� pt��$��13(�#��[ �� $��(O tamanho padrão é 16)��$��11(�#��[ $��0,3,0,10q�$��Center=�$��Italic$��� $��0,0,0,0q�$��1q�#��[ $ 5��$ 10q�._��� .+��� $��Visualização:��$��12(�#��[ $��0,0,0,5q��� -   $ 
SampleText��$k��eExemplo de texto usando este tamanho de fonte. Ajuste o tamanho conforme sua preferência de leitura.��$
��Wrap=�$��16(�#��[ �� $:��4* O tamanho da fonte afeta apenas o texto das notas.��$��11(�#��[ $��0,5,0,0q�$��Italic$��� $��2��#��[ $ 	0,0,12,12��._��� $ 
Horizontal=�$��Center=�$��0,8,0,8q�.+��� -    ��Name$ ApplyButton��$��Aplicar�� 
 iPodButtonStyle#��[
 $	��120��$��32��$	��5,0q�$��SemiBold"�#��[ �� -	   $ CancelButton��$��Cancelar��#��[
 $	��120��$��32��$	��5,0q�$��SemiBold"�#��[ 