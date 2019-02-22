﻿using CompilerDevelopment.Entities;
using CompilerDevelopment.Graphics;
using CompilerDevelopment.RecursiveDescent;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompilerDevelopment.GUI
{
    /// <summary>
    /// Логика взаимодействия для ParsePage.xaml
    /// </summary>
    public partial class ParsePage : UserControl
    {
        public ParsePage()
        {
            InitializeComponent();
            //string path = @"D:\ASM\testLabCompiler1.txt";
            //using (StreamReader sr = new StreamReader(path))
            //{
            //    TextCode.Text = sr.ReadToEnd();
            //}
            if (TextCode.Text == "")
            {

                parse.IsEnabled = false;
            }
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
            //SyntaxAnalyzer.errors.Clear();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TextCode.Text = "";
            ListError.Text = "";
            RecursiveDescent.SyntaxAnalyzer.errors.Clear();
            // parse.IsEnabled = false;

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    TextCode.Text = File.ReadAllText(openFileDialog.FileName);
                }
            }
        }

        private void Compiler_Click(object sender, RoutedEventArgs e)
        {
            ListError.Text = "";
            MPA.TableOfAnalyzer.fullStack.Clear();
            MPA.TableOfAnalyzer.tableOfAnalysis.Clear();
            SourceTableOfTokens.SourceListOfTokens.Clear();
            TableOfConstants.ConstantListOfTokens.Clear();
            TableOfIdentifiers.IdentifierListOfTokens.Clear();
            string path = @"D:\ASM\testLabCompiler1.txt";
            string code = TextCode.Text;
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(code);
            }
            if (code != "")
            {
                try
                {
                    SourceTableOfTokens.StartParse(path);
                    ListError.Text += "\n Программа успешно прошла лексический анализ";
                    // TableOfAnalyzer table1 = new TableOfAnalyzer();

                    switch (Settings.SettingMethod)
                    {
                        case "Recursion":
                            SyntaxAnalyzer.Analyze(0);

                            if (SyntaxAnalyzer.errors.Count > 0)
                            {
                                for (int i = 0; i < SyntaxAnalyzer.errors.Count; i++)
                                {
                                    ListError.Text += "\n" + SyntaxAnalyzer.errors[i];
                                }
                                SyntaxAnalyzer.errors.Clear();
                            }
                            else
                            {
                                ListError.Text += "\n Программа успешно прошла синтаксический анализ";
                            }
                            break;
                        case "MPA":
                            MPA.TableOfAnalyzer.tableOfAnalysis.Clear();
                            string error = MPA.TableOfAnalyzer.PushInTable(9);
                            ListError.Text += "\n";
                            ListError.Text += error;
                            break;
                    }

                    //SyntaxAnalyzer.Analyze(0);

                    //if (SyntaxAnalyzer.errors.Count > 0)
                    //{
                    //    for (int i = 0; i < SyntaxAnalyzer.errors.Count; i++)
                    //    {
                    //        ListError.Text += "\n" + SyntaxAnalyzer.errors[i];
                    //    }
                    //    SyntaxAnalyzer.errors.Clear();
                    //}
                    //else
                    //{
                    //    ListError.Text += "\n Программа успешно прошла синтаксический анализ";
                    //}
                }
                catch (Exception ex)
                {
                    //TextCode.Text = ex.Message;
                    ListError.Text = ex.Message;
                }
            }
            else
            {
                TextCode.Text = "Введите программу!";
            }
            //if (SourceTableOfTokens.StartParse(path))
            //{
            //    TextCode.Text = "Программа успешно прошла лексический анализ";
            //}
            //else
            //{
            //    TextCode.Text = "Ошибка";
            //}
        }


    }
}