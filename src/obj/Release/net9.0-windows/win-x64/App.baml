   M S B A M L   `   `   `  ���� @  <Notes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:NotesNotes      	Notes.App   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     %localclr-namespace:Notes       �� 	Resources   �� MergedDictionaries   Key �� Color3  -System.Windows.Media.Effects.DropShadowEffect  SoftShadowEffect   
BlurRadius   ShadowDepth   Opacity   	Direction   Color  iPodBackgroundBrush  iPodHeaderBrush  iPodAccentBrush  iPodTextBrush  iPodLightGrayBrush  iPodBorderBrush  iPodShadowBrush 	 iPodButtonStyle �� 
TargetType	 �� Property
 �� Value �� ScaleX �� ScaleY �� 
TargetType �� CornerRadius ��Name J� Opacity J� Effect �� Triggers P� Property P� Value �� 
TargetName �� Triggers :� RoutedEvent g� Duration	 W� To �� TargetProperty 
 iPodListBoxStyle � 	Resources  VerticalScrollBar ]�Name ]� Thumb �� RadiusX �� RadiusY  J� SnapsToDevicePixels  iPodListBoxItemStyle! � ColumnDefinitions  CloseButtonStyle  ThemeToggleButtonStyle" A�Name  iPodTextBoxStyle# ��Name"$ �� VerticalScrollBarVisibility  ThemeIconStyle  iPodCheckBoxStyle  iPodRadioButtonStyle  iPodSliderStyle% � RowDefinitions& ]� DecreaseRepeatButton' ]� IncreaseRepeatButton��  �� �� "LightTheme.iPodBackgroundBrush  $ #FFFFFF���� LightTheme.iPodHeaderBrush  $ #F5F5F5���� LightTheme.iPodAccentBrush  $ #007AFF���� LightTheme.iPodTextBrush  $ #333333���� !LightTheme.iPodLightGrayBrush  $ #E8E8E8���� LightTheme.iPodBorderBrush  $ #CCCCCC���� LightTheme.iPodShadowBrush  $ 	#20000000���� �� !DarkTheme.iPodBackgroundBrush  $ #121212���� DarkTheme.iPodHeaderBrush  $ #1E1E1E���� DarkTheme.iPodAccentBrush  $ #64B5F6���� DarkTheme.iPodTextBrush  $ #FFFFFF����  DarkTheme.iPodLightGrayBrush  $ #333333���� DarkTheme.iPodBorderBrush  $ #424242���� DarkTheme.iPodShadowBrush  $ 	#40000000��%  &	       &	 8     &	 K     &	 ^     &	 q     &	 �     &	 �     &	 �     &		 �     7  7  7  7  7  7  7  &	
 a    7  7  7  7  &	     7  7  7  7  7  7  &	 6
    7  &	 F    7  7  7  7  &	 �    7  7  &	 y    &	 	    &	 u    7  7  7  7  &	 �    7  7  7  7  7  7  7  7    $ 10Q�$ 1Q�$	 0.2Q�$	 315Q�$ #000000���� $ #FFFFFF���� $ #F5F5F5���� $ #007AFF���� $ #333333���� $ #E8E8E8���� $ #CCCCCC���� $ 	#20000000���� " ��.��� 	 � ��8
   �� 	 � ��8
  �� 	 � ��$
 Segoe UI, Arial)��� 	 � ��$
 SemiBold"��� 	 � ��$
 13(��� 	 � ��$

 12,6q��� 	 � ��$
 5q��� 	 �@�Cursor$

 Hand���� 	 �@J�RenderTransformOrigin$
 0.5,0.5&��� 	 � z�
 �� $	 1.0Q�$	 1.0Q��� 	 � ��
 �� " ��.R�� .+��� 8�� $ 14��$��0,0,0,2q��� $ ButtonBorder��#��z��$��1q�8�� $	 0.9Q�$ 14��   $ 3Q�$ 0Q�$	 0.1Q�$ #0A84FF��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ��� $ ButtonBorder��8
  	 � ���� $ ButtonBorder��	 � ��
 �#0A84FF�� $ ButtonBorder��	 �@J�Effect
   $ 6Q�$ 0Q�$	 0.3Q�$ #0A84FF��P�  � ��$
 True��. ��� $ ButtonBorder��8
  	 � ���� $ ButtonBorder��	 � ��$
 0,1,0,-1q��� 8
  	 � �� :� $ Mouse.MouseEnter��.[��� .���� .��W� $ 0:0:0.2D� 1.03$ RenderTransform.ScaleX�W� $ 0:0:0.2D� 1.03$ RenderTransform.ScaleY�:� $ Mouse.MouseLeave��.[��� .���� .��W� $ 0:0:0.2D� 1.0$ RenderTransform.ScaleX�W� $ 0:0:0.2D� 1.0$ RenderTransform.ScaleY��� " ��.��� 	 � ��8
   �� 	 � ��$
 1q��� 	 � ��8
  �� 	 � ��$
 0q��� 	 � ��$
 Segoe UI, Arial)��� 	 � ��$
 12(��� 	 � ��
 �� " ��.R��� #��z��#��z��#��z��$ 4��   $ 1Q�$ 0Q�$	 0.1Q�$ #000000��._��� #��z��$|�False��
������  ���� '��       " ��.��� 	 � ��

 ���� �� 	 � ��

 ���� �� 	 � ��8
   P�  �@��Orientation$ Vertical=�. ��� 	 � ��$
 8���� 	 � ��#
 �  �� VerticalScrollBar  " ��.R�� .+��� #��z��#��z��$��0q�]� $ 
PART_Track��$
��True�� n� ���� " n�.R��� $��6��8�� $ 3��$ 3��$	 0.6Q�.���� #  z  �� " ��.��� 	 � ��$

 10,8q��� 	 � ��$
 0,0,0,1q��� 	 � ��8
   �� 	 � ��8
  �� 	 � ��
 �� " ��.R��� #��z��#��z��#��z��._�� ! �� $��* ��� $
��Auto �.+��� $��0��#��z��8�� $��8,0,0,0q�$��Center=�$��CharacterEllipsis=�F� $��1��"����        � 	=  	= �     z 	8�� $��2��$��Center=�$��5,0,10,0q�   $ 1Q�$	 0.5Q�$	 0.2Q�$ #FFFFFF�� P�  �@��
IsSelected$
 True��. ��� 8
  	 � ��P�  �@J�IsMouseOver$
 True��. ��� 8
  	 � ���� 	 �@�Cursor$

 Hand���� " ��.��� 	 � ��
 �� " ��.R��� $��20��$��10��8��  $ 1��   $ 1Q�$	 0.5Q�$	 0.1Q�$	 315Q�$ #000000�� P�  �@J�IsMouseOver$
 True��. ��� 	 �@J�Opacity$	
 0.9Q�P�  � ��$
 True��. ��� 	 �@J�Opacity$	
 0.7Q��� " ��.��� 	 � ��$
 30���� 	 � ��$
 30���� 	 � ��$
 5q��� 	 � ��
 �� " ��.R�� .+�A� $" ButtonBackground��8��  8�� $	��1.5��   $ 5Q�$ 0Q�$	 0.1Q�$ #0A84FF���� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ��� $ ButtonBackground��8
  	 � ��P�  � ��$
 True��. ��� $ ButtonBackground��8
  	 � ���� " ��.��� 	 � ��8
   �� 	 � ��8
  �� 	 � ��$
 0q��� 	 � ��$
 8q��� 	 � ��$
 Segoe UI, Arial)��� 	 � ��$
 14(��� 	 �@��TextWrapping$

 Wrap=��� 	 �@��AcceptsReturn$

 True���� #	 �@��VerticalScrollBarVisibility$

 Auto=��� 	 �@�FocusVisualStyle
 Z� �� 	 � ��
 �� " ��.R��� #��z��#��z��#��z��$ 8��._��� $# PART_ContentHost��#��z��#��z$ �� " F�.��� 	 �@��Stretch$
 Uniform=��� 	 � ��$
 16���� 	 � ��$
 16���� 	 � ��$
 Center=��� 	 � ��$
 Center=��� " ��.��� 	 � ��
 �� " ��.R��� $��14��$��14��$��1q����#999999$ 2��
��������� " 	�.��� 	 � ��
 �� " 	�.R�� ! �� $
��Auto ��� $��* �.+��� $ RadioButtonBorder��$��14��$��14��$��1q�8��  $ 7��
������ $��0,0,5,0q�._�A� $" RadioButtonDot��$��8��$��8��8�� $y�	Collapsed=��� $��1��$��Center=�$
��Left=�$
��True�� P�  �@d�	IsChecked$
 TrueY�. ��� $ RadioButtonDot��	 � y�$
 Visible=�P�  �@J�IsMouseOver$
 True��. ��� $ RadioButtonBorder��8
  	 � ���� 	 � ��8
  �� 	 � ��$
 0,0,0,5q��� 	 �@�Cursor$

 Hand���� " ��.��� 	 � ��8
   �� 	 � ��8
  �� 	 � ��8
  �� 	 � ��
 �� " ��.R�� % �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+��� $ TrackBackground��$��1��$��4��$	��5,0q�8�� $ 2��$��Center=�]� $ 
PART_Track��$��1��& �� $��Slider.DecreaseLarge��8�� ���� " ��.R��� $��4��$	��5,0q�8�� $ 2��$��Center=�' �� $��Slider.IncreaseLarge������ " ��.R��� 
������  n� ���� " n�.R�A� $��16��$��16��8�� 8�� $��1��   $ 3Q�$ 1Q�$	 0.3Q�$ #000000��