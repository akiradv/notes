   M S B A M L   `   `   `  ���� @  <Notes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:NotesNotes      Notes.MainWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:Notes       � Title$  iNotes��$	��360��$	��280��
������  � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanResizeWithGrip=� � WindowStyle$
 None=� � AllowsTransparency$
 True��$	��260��$	��320�� � 	Resources 3  -System.Windows.Media.Effects.DropShadowEffect   SoftShadowEffect   
BlurRadius   ShadowDepth   Opacity	   	Direction
   Color  iPodButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� Effect �� Triggers P� Property P� Value  CloseButtonStyle �� Triggers  iPodCheckBoxStyle��%y  &	        &	 8     7   &	 �    &	 �      $ 10Q�$ 1Q�$	 0.2Q�$		 315Q�$
 #000000���� " ��.���  � �� �#F5F5F5��  � �� �#007AFF��  � ��$ Segoe UI, Arial)���  � ��$ Normal"���  � ��$
 10,5q���  � ��$ 5q���  � �� �� " ��.R��� #��z��$��0q�$ 10��8   ._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � �� �#E8E8E8P�  � ��$
 True��. ���  � �� �#D8D8D8�� " ��.���  � �� �� " ��.R��� $��20��$��10�����#007AFF$ 1��   $ 1Q�$	 0.5Q�$	 0.1Q�$		 315Q�$
 #000000�� P�  �@J�IsMouseOver$
 True��. ���  �@J�Opacity$	 0.9Q�P�  � ��$
 True��. ���  �@J�Opacity$	 0.7Q��� " ��.���  � �� �� " ��.R��� $��14��$��14��$��1q����#999999$ 2��
�������   $ 10Q�$ 1Q�$	 0.2Q�$		 315Q�$
 #000000��.���� $ 12��$��1q�  iPodBorderBrush#��[ $��5q�  iPodBackgroundBrush#��[ ._��  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� -     iPodHeaderBrush#��[ $��0�� �� Padding$ 	12,8,12,8q�$ 	12,12,0,0��# [  ._��  � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� -    ��Name$ SettingsButton��$��0��  ThemeToggleButtonStyle#��[ $��0,0,5,0q� � ToolTip$5 /Abrir configurações (tema e tamanho da fonte)�� � Cursor$
 Hand��.��� .+�F�   ThemeIconStyle#��[  	 iPodAccentBrush#��[	 ����� � ��� � ��  � ��g5 �g5 � �  �� ��g5 �g5 ����  � ��g5 �g5 � � ��� ��g5 �g5 �pz(�� Ӱ)� ��)$� ��) � ���)�� �)�� pz(�ا Ѡ�H��� �ВK���  VL� `�J�5� ���+�iP � (*�M p	& �K �"�M ����(\�0@�P\ �@�� `]V ��� @-Q ��� �\M Ѡ@�  �$ �`�� �C! p� �� ��� �� р�� �� ��Ŕ �� ��� �C! `��  �$ �P� �\M � �� @-Q �uy `]V ��q �P\ ���K �M Ӑ,H  �K �D �M  9B �iP Ѡ�# �5� �и! � 0�" ��� `�% ��� ѐ�E �ا �PE ��  �D ��  �D  � � �D $� PE � ��E �� �`�% p<� �0�" `�� и! �� ��# p � � 9B ���D p'!�,H �_"��K p'!р�q  ���uy �� �� �P� P� �`�� �HIӠ�� `�L�Ŕ ��O��� ��Oр�� ��O�p� ��O`�� `�L�@� �HI���� P� ���� ��@�� �����(\�0@ ���"p'!�p	&�_" (*p'!��+��`�Jp � � VL�� ВK`�� ��Hp<� �pz(�� 	A� $��22��$��22��#��[	 $	��0.5��
������    $ 5Q�$ 0Q�$	 0.2Q�$
 #007AFF���� $��iNotes��$��16(�$��Segoe UI, Arial)� 
 iPodTextBrush#��[
 $��Medium"�$��1��$��Center=�$��Center=� �� TextAlignment$ Center=��� -   $��2��#��[ $��0,-1,0,0q�$
 Hand���� $��1��$��	10,6,10,6q�$ 8��#��[ $	��1.5q�  iPodLightGrayBrush#��[ # [  ._��� -   $��NotesTextBox��  iPodTextBoxStyle#��[ $��12q�$��1q��� $��2��#��[ $ 	0,0,12,12��._��  �� $
��Auto ��� $
��Auto �.+��� $��0�� �� Orientation$ 
Horizontal=�$��Center=�$��0,8,0,2q�.+��� -   $��	NewButton��$
��Novo��#��[ $��70��$��28��$	��4,0q�#��[
 �� -   $��
SaveButton��$��Salvar��#��[ $��70��$��28��$	��4,0q�#��[
 �� -   $��
ListButton��$��Lista��#��[ $��70��$��28��$	��4,0q�#��[
 �� $��1��$ 
Horizontal=�$��Center=�$��0,2,0,8q�.+��� -   $��ExportButton��$��Exportar��#��[ $��85��$��28��$	��4,0q�#��[
 �� -	   $��ImportButton��$��Importar��#��[ $��85��$��28��$	��4,0q�#��[
 