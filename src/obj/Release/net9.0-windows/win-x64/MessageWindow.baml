   M S B A M L   `   `   `  ���� @  <Notes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:NotesNotes      Notes.MessageWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:Notes       � Title$  Mensagem��$	��180��$	��300�� � WindowStartupLocation$ CenterOwner=�
������  � WindowStyle$
 None=� � AllowsTransparency$
 True�� � 
ResizeMode$ NoResize=�.����  �� CornerRadius$ 12��$��1q�   iPodBorderBrush#��[  $��5q�  iPodBackgroundBrush#��[ ._��  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� -     iPodHeaderBrush#��[ $��0�� �� Padding$ 	12,8,12,8q�$ 	12,12,0,0��._��  � ColumnDefinitions �� $��* ��� $
��Auto �.+��� -   	 ��Name$	 	TitleText��$��Mensagem��$��13(�$��Segoe UI, Arial)�  iPodTextBrush#��[ $��Regular"�$��0��$��Center=�$��Center=��� -   $��1��  CloseButtonStyle#��[ $��0,-1,0,0q�
 � Cursor$

 Hand���� -   $	 MessageText��$��1��$��20,15q�$
��Wrap=�$��13(�#��[ $��Center=� �� TextAlignment$ Center=��� $��2��#��[ $ 	0,0,12,12��._��� -    ��Name$ ButtonsPanel�� �� Orientation$ 
Horizontal=�$��Center=�$��0,8,0,12q�