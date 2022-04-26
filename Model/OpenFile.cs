using System;
using QuizMVVM.ViewModel;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace QuizMVVM.Model
{
    static public class OpenFile
    {
        public static object OpenJsonEncrypted()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            object? quizClass = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON (*.json)|*.json|Wszystkie pliki|*.*";
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    string fileName = openFileDialog.FileName;
                    string jsonStringDecrypted = Decode.Decoding(fileName, 3);
                    quizClass = JsonSerializer.Deserialize<QuizClass>(jsonStringDecrypted, options);
                }
                catch
                {
                    MessageBox.Show("Nie można otworzyć pliku", "Błąd odczytu", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return quizClass;
        }
    }
}
