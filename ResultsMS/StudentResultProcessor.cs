// StudentResultProcessor.cs
using System;
using System.Collections.Generic;
using System.IO;

namespace GradingSystem
{
    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            var students = new List<Student>();
            if (!File.Exists(inputFilePath))
            {
                throw new FileNotFoundException("The specified input file was not found.", inputFilePath);
            }

            try
            {
                using (var reader = new StreamReader(inputFilePath))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = line.Split(',');

                        if (fields.Length < 3)
                        {
                            throw new MissingFieldException($"Line '{line}' is missing required fields.");
                        }

                        int id;
                        if (!int.TryParse(fields[0].Trim(), out id))
                        {
                            throw new InvalidScoreFormatException($"Invalid ID format in line: '{line}'.");
                        }

                        string fullName = fields[1].Trim();

                        int score;
                        if (!int.TryParse(fields[2].Trim(), out score))
                        {
                            throw new InvalidScoreFormatException($"Invalid score format in line: '{line}'.");
                        }

                        students.Add(new Student(id, fullName, score));
                    }
                }
            }
            catch (Exception)
            {
                // Re-throw specific exceptions 
                throw;
            }

            return students;
        }

        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("--- Student Grade Report ---");
                writer.WriteLine("-----------------------------");

                foreach (var student in students)
                {
                    string reportLine = $"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}";
                    writer.WriteLine(reportLine);
                }

                writer.WriteLine("-----------------------------");
                writer.WriteLine("Report generated successfully.");
            }
        }
    }
}