   M S B A M L   `   `   `  ����A  =Pract3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Pract3Pract3  5   	       Pract3.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Pract3     5   	     � Title$  
MainWindow��5   	   $	��300��6   $	��800��6)    � 
ResizeMode$ NoResize=�65    � WindowStartupLocation$ CenterScreen=�6K   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      $
��1.5* �6   �� 5      .+�5   
   �� $��0��6   $��3��6   $��20(�61   $��Center=�6?   $��Center=�6\   $����Розмежування повноважень користувачів на основі парольної ідентифікації��6w   $��Comic Sans MS)�6�   �� 5   
   -   5         ��Name$ AdminButton��5      $��16(�6   $��1��64   $��0��6A   $��30q�6Q   $-��'Режим адміністратора��6]    � Cursor$
 Hand��6|   $��Comic Sans MS)�6�   6�   �� 5   
   -   5        $ 
UserButton��5      $��16(�6   $��1��63   $��1��6@   $��30q�6P   $'��!Режим користувача��6\   $
 Hand��6x   $��Comic Sans MS)�6�   6�   �� 5   
   -   5        $ DeveloperInfoButton��5      $��16(�6   $��1��6<   $��2��6I   $��30q�6Y   $!��Про розробника��6e   $
 Hand��6~   $��Comic Sans MS)�6�   6�   �� 5   
   -   5        $ 
ExitButton��5      $��16(�6   $��2��63   $��1��6@   $��20q�6P   $#��Закрити додаток��6\   $
 Hand��6v   $��Comic Sans MS)�6�   5      5      