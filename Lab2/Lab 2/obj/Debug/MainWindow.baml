   M S B A M L   `   `   `  ����@  <Lab 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab2Lab2  5   	       Lab2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Lab2     5   	   -   5          �Name$  Main_window��5   	    � Title$ 	First app��5   	    � Icon$# /завантаження.jpg��6    � 
ResizeMode$ NoResize=�64    � SizeToContent$ Manual=�6J    � IsSharedSizeScope .   � WindowStartupLocation$ CenterScreen=�6�   $	��994��6�   $	��550��6�   .��5	      � -   5         �Name$ MainForm��5	       J� RenderTransformOrigin$ 0.505,0.486&�6   4  .System.Windows.Automation.AutomationProperties	   Name$	 Hello��6A   $��	-5,-1,0,0q�6c   $	��536��6v   
 � RowDefinitions
 5
   
   �� 5      $	��434 �6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ Black��6    � Offset$ 1Q�6-   � 5      $ 	#FFF06464��6   5      .+�5   
   �� -   5         ��Name$ 	MyLabel_1��5      $H��B                        
                              Main window��6#   $��Center=�6t   $��0,39,0,0q�6�   $	��Top=�6�   $��75��6�   $	��580��6�    � Cursor$ Arrow��6�   $ 
0.82,0.996&�6�   $��24(�6  $��Aharoni)�6  $��Normal$�6(  $��Normal"�6;  ��5      
� 5      .��5      � $ Black��6#   $ 1Q�61   � 5      $ 	#FF0EF100��6#   $ 0Q�65   5      z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 0Q�6&   T� 5      5       5"      �� 5#   
   -   5         ��Name$ ExitButtnon��5#      $
��Exit��6&   $��559,406,89.2,-41.6q�65   $��22(�6Q   ���	#FF3F1DE8$��Algerian)�6v   $ -0.638,-1.306&�6�   $
 Hand��6�   $	��170��6�   $��57��6�   6�   �� 5$   
   -   5        $ 
ButtonToW2��5$      $��To window 2��6%   $��89,247,562,127q�6;   ���	#FF29DFB4$��Algerian)�6j   $��18(�6�   $��60��6�   $
 Hand��6�   6�   �� 5%   
   -   5        $ 
ButtonToW1��5%      $��To window 1��6%   $��89,161,562,193q�6;   ���	#FF29DFB4$��Algerian)�6j   $��18(�6�   $
 Hand��6�   $��60��6�   6�   �� 5&   
   -   5        $ 
ButtonToW3��5&      $��To window 3��6%   $��89,327,562,47q�6;   ���	#FF29DFB4$��Algerian)�6i   $��18(�6   $
 Hand��6�   $��60��6�   6�   �� 5'   
   -   5        $ 
ButtonToW4��5'      $��To window 4��6%   $��89,412,562,-38q�6;   ���	#FF29DFB4$��Algerian)�6j   $��18(�6�   $
 Hand��6�   $��60��6�   6�   5)      5*      