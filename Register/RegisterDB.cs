using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Register
{
    namespace UniversityDatabaseApp
    {
        public class Teacher
        {
            [JsonPropertyName("email")]
            public string Email { get; set; } = string.Empty;

            [JsonPropertyName("last_name")]
            public string LastName { get; set; } = string.Empty;

            [JsonPropertyName("first_name")]
            public string FirstName { get; set; } = string.Empty;

            [JsonPropertyName("password")]
            public string Password { get; set; } = string.Empty;

            [JsonIgnore]
            public List<Group> CuratedGroups { get; set; } = new();

            [JsonIgnore]
            public List<GroupSubject> GroupSubjects { get; set; } = new();
        }

        public class Subject
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; } = string.Empty;

            [JsonIgnore]
            public List<GroupSubject> GroupSubjects { get; set; } = new();

            [JsonIgnore]
            public List<Grade> Grades { get; set; } = new();
        }

        public class Group
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("curator_email")]
            public string CuratorEmail { get; set; } = string.Empty;

            [JsonIgnore]
            public Teacher? Curator { get; set; }

            [JsonIgnore]
            public List<Student> Students { get; set; } = new();

            [JsonIgnore]
            public List<GroupSubject> GroupSubjects { get; set; } = new();

            [JsonIgnore]
            public List<Grade> Grades { get; set; } = new();
        }

        public class Student
        {
            [JsonPropertyName("email")]
            public string Email { get; set; } = string.Empty;

            [JsonPropertyName("last_name")]
            public string LastName { get; set; } = string.Empty;

            [JsonPropertyName("first_name")]
            public string FirstName { get; set; } = string.Empty;

            [JsonPropertyName("group_id")]
            public int GroupId { get; set; }

            [JsonPropertyName("password")]
            public string Password { get; set; } = string.Empty;

            [JsonIgnore]
            public Group? Group { get; set; }

            [JsonIgnore]
            public List<Grade> Grades { get; set; } = new();
        }

        public class GroupSubject
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("group_id")]
            public int GroupId { get; set; }

            [JsonPropertyName("subject_id")]
            public int SubjectId { get; set; }

            [JsonPropertyName("teacher_email")]
            public string TeacherEmail { get; set; } = string.Empty;

            [JsonIgnore]
            public Group? Group { get; set; }

            [JsonIgnore]
            public Subject? Subject { get; set; }

            [JsonIgnore]
            public Teacher? Teacher { get; set; }
        }

        public class Grade
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("group_id")]
            public int GroupId { get; set; }

            [JsonPropertyName("student_email")]
            public string StudentEmail { get; set; } = string.Empty;

            [JsonPropertyName("subject_id")]
            public int SubjectId { get; set; }

            [JsonPropertyName("value")]
            public double Value { get; set; }

            [JsonIgnore]
            public Group? Group { get; set; }

            [JsonIgnore]
            public Student? Student { get; set; }

            [JsonIgnore]
            public Subject? Subject { get; set; }
        }

        public class RegisterDB
        {
            [JsonPropertyName("teachers")]
            public List<Teacher> Teachers { get; set; } = new();

            [JsonPropertyName("subjects")]
            public List<Subject> Subjects { get; set; } = new();

            [JsonPropertyName("groups")]
            public List<Group> Groups { get; set; } = new();

            [JsonPropertyName("students")]
            public List<Student> Students { get; set; } = new();

            [JsonPropertyName("group_subjects")]
            public List<GroupSubject> GroupSubjects { get; set; } = new();

            [JsonPropertyName("grades")]
            public List<Grade> Grades { get; set; } = new();

            public void BuildRelationships()
            {
                foreach (var t in Teachers)
                {
                    t.CuratedGroups.Clear();
                    t.GroupSubjects.Clear();
                }

                foreach (var s in Subjects)
                {
                    s.GroupSubjects.Clear();
                    s.Grades.Clear();
                }

                foreach (var g in Groups)
                {
                    g.Curator = null;
                    g.Students.Clear();
                    g.GroupSubjects.Clear();
                    g.Grades.Clear();
                }

                foreach (var st in Students)
                {
                    st.Group = null;
                    st.Grades.Clear();
                }

                var teacherMap = Teachers
                    .Where(t => !string.IsNullOrEmpty(t.Email))
                    .DistinctBy(t => t.Email, StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(t => t.Email, StringComparer.OrdinalIgnoreCase);

                var subjectMap = Subjects
                    .DistinctBy(s => s.Id)
                    .ToDictionary(s => s.Id);

                var groupMap = Groups
                    .DistinctBy(g => g.Id)
                    .ToDictionary(g => g.Id);

                var studentMap = Students
                    .Where(s => !string.IsNullOrEmpty(s.Email))
                    .DistinctBy(s => s.Email, StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(s => s.Email, StringComparer.OrdinalIgnoreCase);

                foreach (var group in Groups)
                {
                    if (teacherMap.TryGetValue(group.CuratorEmail, out var teacher))
                    {
                        group.Curator = teacher;
                        teacher.CuratedGroups.Add(group);
                    }
                }

                foreach (var student in Students)
                {
                    if (groupMap.TryGetValue(student.GroupId, out var group))
                    {
                        student.Group = group;
                        group.Students.Add(student);
                    }
                }

                foreach (var gs in GroupSubjects)
                {
                    if (groupMap.TryGetValue(gs.GroupId, out var group))
                    {
                        gs.Group = group;
                        group.GroupSubjects.Add(gs);
                    }

                    if (subjectMap.TryGetValue(gs.SubjectId, out var subject))
                    {
                        gs.Subject = subject;
                        subject.GroupSubjects.Add(gs);
                    }

                    if (teacherMap.TryGetValue(gs.TeacherEmail, out var teacher))
                    {
                        gs.Teacher = teacher;
                        teacher.GroupSubjects.Add(gs);
                    }
                }

                foreach (var grade in Grades)
                {
                    if (groupMap.TryGetValue(grade.GroupId, out var group))
                    {
                        grade.Group = group;
                        group.Grades.Add(grade);
                    }

                    if (studentMap.TryGetValue(grade.StudentEmail, out var student))
                    {
                        grade.Student = student;
                        student.Grades.Add(grade);
                    }

                    if (subjectMap.TryGetValue(grade.SubjectId, out var subject))
                    {
                        grade.Subject = subject;
                        subject.Grades.Add(grade);
                    }
                }
            }
        }

        public static class DbJsonContext
        {
            private static readonly JsonSerializerOptions Options = new()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            public static string Serialize(RegisterDB db)
            {
                return JsonSerializer.Serialize(db, Options);
            }

            public static RegisterDB Deserialize(string json)
            {
                var db = JsonSerializer.Deserialize<RegisterDB>(json, Options) ?? new RegisterDB();
                db.BuildRelationships();
                return db;
            }

            public static void SaveToFile(RegisterDB db, string filePath)
            {
                File.WriteAllText(filePath, Serialize(db));
            }

            public static RegisterDB LoadFromFile(string filePath)
            {
                if (!File.Exists(filePath))
                    return new RegisterDB();

                return Deserialize(File.ReadAllText(filePath));
            }
        }
    }
}