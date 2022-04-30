using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Lab2
{
    class CreateWindow1
    {
        private static CommonMethodsForCreatingWindows commonMethods = new CommonMethodsForCreatingWindows();
        private static Window mainWindow;
        private static Grid myGrid = new Grid();

        // Elements of pannel:
        private static Button Button1;
        private static Label Label1;
        private static TextBox TextBox1;
        private static TextBox TextBox2;
        private static Label Label2;
        private static Button Button2;
        private static Button Button3;
        private static TextBox TextBox3;
        private static Button Button4;
        private static Window win1 = new Window();

        public CreateWindow1(Window myMainWindow)
        {
            mainWindow = myMainWindow;
            win1.Name = "Window1";
            win1.Title = "Window1";
            win1.ResizeMode = ResizeMode.NoResize;
            win1.Height = 450; win1.Width = 800;
            win1.HorizontalAlignment = HorizontalAlignment.Center; win1.VerticalAlignment = VerticalAlignment.Center;
            win1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            commonMethods.SetColor(myGrid, "#110606");

            // Button1 (exit)
            Button1 = CreateButton(width: 127,
                                   height: 69.04,
                                   content: "Exit",
                                   fontSize: 24,
                                   horizontalAlignment: "right",
                                   verticalAlignment: "bot",
                                   color: "#FF1FE513",
                                   right: 23,
                                   bot: 25,
                                   fontFamily: "Algerian");
            Button1.Click += ExitButton;

            // Label1 (input data)
            Label1 = CreateLabel(content: "Введіть через кому номер залікової книжки, ПІП та опис студента",
                                 height: 46,
                                 width: 589,
                                 horizontalAlignment: "left",
                                 verticalAlignment: "top",
                                 left: 27,
                                 top: 25,
                                 fontSize: 16,
                                 fontFamily: "Bodoni MT Black",
                                 color: "#FF22D960");

            // TextBox1 (input data)
            TextBox1 = CreateTextBox(height: 83,
                                     width: 589,
                                     horizontalAlignment: "left",
                                     verticalAlignment: "top",
                                     left: 27,
                                     top: 105,
                                     fontSize: 16,
                                     color: "#FFFCA0A0");
            TextBox1.MouseDoubleClick += DoubleClick_Clear_TB1;

            // TextBox2 (output file data)
            TextBox2 = CreateTextBox(height: 83,
                                     width: 589,
                                     horizontalAlignment: "left",
                                     verticalAlignment: "top",
                                     left: 27,
                                     top: 304,
                                     fontSize: 16,
                                     color: "#FFFCA0A0");
            TextBox2.MouseDoubleClick += DoubleClick_Clear_TB2;

            // Label2 (output file data)
            Label2 = CreateLabel(content: "Вміст файлу нижче",
                                 height: 46,
                                 width: 589,
                                 horizontalAlignment: "left",
                                 verticalAlignment: "top",
                                 left: 27,
                                 top: 223,
                                 fontSize: 16,
                                 fontFamily: "Bodoni MT Black",
                                 color: "#FF22D960");

            // Button2 (read data)
            Button2 = CreateButton(width: 103,
                                   height: 53,
                                   content: "Read",
                                   fontSize: 18,
                                   horizontalAlignment: "right",
                                   verticalAlignment: "top",
                                   color: "#FFF706F9",
                                   right: 30,
                                   top: 22,
                                   fontFamily: "Algerian");
            Button2.Click += ReadButton;

            // Button3 (clear data)
            Button3 = CreateButton(width: 103,
                                   height: 53,
                                   content: "Clear",
                                   fontSize: 18,
                                   horizontalAlignment: "right",
                                   verticalAlignment: "top",
                                   color: "#FFF706F9",
                                   right: 30,
                                   top: 95,
                                   fontFamily: "Algerian");
            Button3.Click += ClearButton;

            // TextBox3 (delete student)
            TextBox3 = CreateTextBox(height: 60,
                                     width: 141,
                                     text: "Введіть номер\nзалікової книжки\nстудента",
                                     horizontalAlignment: "right",
                                     verticalAlignment: "top",
                                     right: 14,
                                     top: 240,
                                     fontSize: 14,
                                     color: "#FFFCA0A0",
                                     fontFamily: "Arial",
                                     fontStyle: "italic");
            
            TextBox3.MouseDoubleClick += DoubleClick_Clear_TB3;
            TextBox3.HorizontalContentAlignment = HorizontalAlignment.Center;
            TextBox3.VerticalContentAlignment = VerticalAlignment.Center;

            // Button4 (delete student)
            Button4 = CreateButton(width: 141,
                                   height: 53,
                                   content: "Delete student",
                                   fontSize: 18,
                                   horizontalAlignment: "right",
                                   verticalAlignment: "top",
                                   color: "#FFF706F9",
                                   right: 14,
                                   top: 171,
                                   fontFamily: "Algerian");
            Button4.Click += DeleteStudent;

            myGrid.Children.Add(Button1);
            myGrid.Children.Add(Label1);
            myGrid.Children.Add(TextBox1);
            myGrid.Children.Add(TextBox2);
            myGrid.Children.Add(Label2);
            myGrid.Children.Add(Button2);
            myGrid.Children.Add(Button3);
            myGrid.Children.Add(TextBox3);
            myGrid.Children.Add(Button4);

            win1.Content = myGrid;
            win1.Show();
        }
        private TextBox CreateTextBox(double width = 50,
                                  double height = 100,
                                  string text = "",
                                  double fontSize = 14,
                                  string horizontalAlignment = "center",
                                  string verticalAlignment = "center",
                                  string color = "#FFFFFF",
                                  double left = 0,
                                  double right = 0,
                                  double top = 0,
                                  double bot = 0,
                                  string cursor = "arrow",
                                  string fontFamily = "Times new roman",
                                  string fontStyle = "normal")
        {
            TextBox myTextBox = new TextBox();
            myTextBox.Width = width; myTextBox.Height = height;
            myTextBox.Text = text;
            myTextBox.FontSize = fontSize;

            // horizontal alignment
            if (horizontalAlignment == "left")
            {
                myTextBox.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else if (horizontalAlignment == "right")
            {
                myTextBox.HorizontalAlignment = HorizontalAlignment.Right;
            }
            else if (horizontalAlignment == "center")
            {
                myTextBox.HorizontalAlignment = HorizontalAlignment.Center;
            }

            // vertical alignment
            if (verticalAlignment == "top")
            {
                myTextBox.VerticalAlignment = VerticalAlignment.Top;
            }
            else if (verticalAlignment == "bot")
            {
                myTextBox.VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (verticalAlignment == "center")
            {
                myTextBox.VerticalAlignment = VerticalAlignment.Center;
            }

            commonMethods.SetColor(myTextBox, color);
            commonMethods.SetMargin(myTextBox, left, right, top, bot);

            if (cursor == "arrow")
            {
                myTextBox.Cursor = Cursors.Arrow;
            }
            else if (cursor == "hand")
            {
                myTextBox.Cursor = Cursors.Hand;
            }
            myTextBox.FontFamily = new FontFamily(fontFamily);
            if (fontStyle == "italic")
            {
                myTextBox.FontStyle = FontStyles.Italic;
            }
            else if (fontStyle == "normal")
            {
                myTextBox.FontStyle = FontStyles.Normal;
            }
            return myTextBox;
        }
        private Label CreateLabel(double width = 50,
                                          double height = 100,
                                          string content = "Label",
                                          double fontSize = 14,
                                          string horizontalAlignment = "center",
                                          string verticalAlignment = "center",
                                          string color = "#FFFFFF",
                                          double left = 0,
                                          double right = 0,
                                          double top = 0,
                                          double bot = 0,
                                          string fontStyle = "normal",
                                          string cursor = "arrow",
                                          string fontFamily = "Bodoni MT Black")
        {
            Label myLabel= new Label();
            myLabel.Width = width; myLabel.Height = height;
            myLabel.Content = content;
            myLabel.FontSize = fontSize;

            // horizontal alignment
            if (horizontalAlignment == "left")
            {
                myLabel.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else if (horizontalAlignment == "right")
            {
                myLabel.HorizontalAlignment = HorizontalAlignment.Right;
            }
            else if (horizontalAlignment == "center")
            {
                myLabel.HorizontalAlignment = HorizontalAlignment.Center;
            }

            // vertical alignment
            if (verticalAlignment == "top")
            {
                myLabel.VerticalAlignment = VerticalAlignment.Top;
            }
            else if (verticalAlignment == "bot")
            {
                myLabel.VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (verticalAlignment == "center")
            {
                myLabel.VerticalAlignment = VerticalAlignment.Center;
            }

            myLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            myLabel.VerticalContentAlignment = VerticalAlignment.Center;
            commonMethods.SetColor(myLabel, color);
            commonMethods.SetMargin(myLabel, left, right, top, bot);
            if (cursor == "arrow")
            {
                myLabel.Cursor = Cursors.Arrow;
            }
            else if (cursor == "hand")
            {
                myLabel.Cursor = Cursors.Hand;
            }

            if (fontStyle == "normal")
            {
                myLabel.FontStyle = FontStyles.Normal;
            }
            else if (fontStyle == "italic")
            {
                myLabel.FontStyle = FontStyles.Italic;
            }
            else if (fontStyle == "Bodoni MT Black")
            {
                myLabel.FontFamily = new FontFamily("Bodoni MT Black");
            }
            else if (fontStyle == "bold")
            {
                myLabel.FontFamily = new FontFamily("Bold");
            }

            myLabel.FontFamily = new FontFamily(fontFamily);
            return myLabel;
        }
        private Button CreateButton(double width = 50,
                                  double height = 100,
                                  string content = "Button",
                                  double fontSize = 14,
                                  string horizontalAlignment = "center",
                                  string verticalAlignment = "center",
                                  string horizontalContentAlignment = "center",
                                  string verticalContentAlignment = "center",
                                  string color = "#FFFFFF",
                                  double left = 0,
                                  double right = 0,
                                  double top = 0,
                                  double bot = 0,
                                  string cursor = "hand",
                                  string fontFamily = "Bodoni MT Black")
        {
            Button myButton = new Button();
            myButton.Width = width; myButton.Height = height;
            myButton.Content = content;
            myButton.FontSize = fontSize;

            // horizontal alignment
            if (horizontalAlignment == "left")
            {
                myButton.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else if (horizontalAlignment == "right")
            {
                myButton.HorizontalAlignment = HorizontalAlignment.Right;
            }
            else if (horizontalAlignment == "center")
            {
                myButton.HorizontalAlignment = HorizontalAlignment.Center;
            }

            // vertical alignment
            if (verticalAlignment == "top")
            {
                myButton.VerticalAlignment = VerticalAlignment.Top;
            }
            else if (verticalAlignment == "bot")
            {
                myButton.VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (verticalAlignment == "center")
            {
                myButton.VerticalAlignment = VerticalAlignment.Center;
            }

            // horizontal content alignment
            if (horizontalContentAlignment == "left")
            {
                myButton.HorizontalContentAlignment = HorizontalAlignment.Left;
            }
            else if (verticalContentAlignment == "right")
            {
                myButton.HorizontalContentAlignment = HorizontalAlignment.Right;
            }
            else if (verticalContentAlignment == "center")
            {
                myButton.HorizontalContentAlignment = HorizontalAlignment.Center;
            }

            // vertical content alignment
            if (verticalContentAlignment == "top")
            {
                myButton.VerticalContentAlignment = VerticalAlignment.Top;
            }
            else if (verticalContentAlignment == "bot")
            {
                myButton.VerticalContentAlignment = VerticalAlignment.Bottom;
            }
            else if (verticalContentAlignment == "center")
            {
                myButton.VerticalContentAlignment = VerticalAlignment.Center;
            }

            commonMethods.SetColor(myButton, color);
            commonMethods.SetMargin(myButton, left, right, top, bot);

            if (cursor == "arrow")
            {
                myButton.Cursor = Cursors.Arrow;
            }
            else if (cursor == "hand")
            {
                myButton.Cursor = Cursors.Hand;
            }
            myButton.FontFamily = new FontFamily(fontFamily);
            return myButton;
        }
        public void ExitButton(object sender, RoutedEventArgs e)
        {
            win1.Hide();
            mainWindow.Show();
        }
        private void ShowFileContent()
        {
            StreamReader reader = new StreamReader("info.txt");
            string fileText = reader.ReadToEnd();
            reader.Close();
            TextBox2.Text = fileText;
        }
        private void ReadButton(object sender, RoutedEventArgs e)
        {
            StreamWriter writer = new StreamWriter("info.txt", true);
            string text = TextBox1.Text;
            writer.WriteLine(text);
            writer.Close();
            TextBox1.Text = "";
            ShowFileContent();
        }
        private void ClearButton(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriterToClear = new StreamWriter("info.txt", false);
            streamWriterToClear.Write("");
            streamWriterToClear.Close();
            ShowFileContent();
        }
        private void DoubleClick_Clear_TB1(object sender, RoutedEventArgs e)
        {
            TextBox1.Text = "";
        }
        private void DoubleClick_Clear_TB2(object sender, RoutedEventArgs e)
        {
            TextBox2.Text = "";
        }
        private void DoubleClick_Clear_TB3(object sender, RoutedEventArgs e)
        {
            TextBox3.Text = "";
        }
        private void DeleteStudent(object sender, RoutedEventArgs e)
        {
            string ID = TextBox3.Text;
            string resText = "";
            StreamReader read = new StreamReader("info.txt");
            while (!read.EndOfStream)
            {
                string line = read.ReadLine();
                string[] currentIDArr = line.Split(',');
                string currentID = currentIDArr[0];
                if (currentID != ID)
                {
                    resText += $"{line}\n";
                }
            }
            read.Close();
            StreamWriter write = new StreamWriter("info.txt");
            write.Write(resText);
            write.Close();
            ShowFileContent();
            TextBox3.Text = "";
        }
        public void Show()
        {
            win1.Show();
        }
    }
}