   M S B A M L   `   `   `  ���� @  <Notes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:NotesNotes      Notes.SaveNoteWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:Notes       � Title$  Salvar Nota��$	��180��$	��260�� � WindowStartupLocation$ CenterOwner=�
������  � WindowStyle$
 None=� � AllowsTransparency$
 True�� � 
ResizeMode$ NoResize=�.����  �� CornerRadius$ 12��$��1q�   iPodBorderBrush#��[  $��5q�  iPodBackgroundBrush#��[ ._��  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� -     iPodHeaderBrush#��[ $��0�� �� Padding$
 12,8q�$ 	12,12,0,0��._�� .+��� $��Salvar Nota��$��13(�$��Segoe UI, Arial)�  iPodTextBrush#��[ $��Regular"�$��Center=�$��Center=��� $��1��$��	15,5,15,5q�.+��� $��Nome da Nota:��$��0,0,0,5q�$��12(�#��[ �� -    ��Name$ NoteNameTextBox��$��28��  iPodTextBoxStyle#��[ $��1q�  iPodLightGrayBrush#��[ $	��8,4q�$��13(�	 �� TextWrapping$	 NoWrap=�
 �� AcceptsReturn$
 False��$��Center=��� $��2��#��[ $ 	0,0,12,12��._���  �� Orientation$ 
Horizontal=�$��Center=�$��0,5,0,10q�.+��� -    ��Name$ CancelButton��$��Cancelar��  iPodButtonStyle#��[ $��80��$��28��$	��4,0q��� -   $ 
SaveButton��$��Salvar��#��[ $��80��$��28��$	��4,0q�