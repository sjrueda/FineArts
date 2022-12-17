using FineArts.Bll;
using FineArts.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace FineArts.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Action
        {
            None = 0,
            Add = 1,
            Remove = 2,
            Edit = 3
        }
        
        Action action = Action.None;
        List<Student> editListStudents = new List<Student>();
        Student[] originalListStudents;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TeacherService Service = new TeacherService();
            TeachersList.DataContext = Service.GetTeachers();
        }

        private void TeachersList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TeacherService Service = new TeacherService();
            Teacher? SelectedTeacher = TeachersList.SelectedItem as Teacher;
            StudentsLists.DataContext = Service.GetStudentsByTeacher(SelectedTeacher!.Id);
            int numberStudent = (StudentsLists.DataContext as List<Student>).Count;
            originalListStudents = new Student[numberStudent];
            (StudentsLists.DataContext as List<Student>).CopyTo(originalListStudents);
        }

        private void StudentsLists_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Enter:
                    string message = string.Empty;
                    if (action == Action.Add)
                    {
                        message = "Debes guardar el estudiante agregado.";
                    }else if (action == Action.Remove)
                    {
                        message = "Debes guardar el estudiante eliminado.";
                    }
                    if (!string.IsNullOrEmpty(message))
                    {
                        var result = MessageBox.Show(message, "Confirmar editar", MessageBoxButton.OKCancel);
                    }
                    Student? SelectedStudent = StudentsLists.SelectedItem as Student;
                    StudentForm studentForm = new StudentForm();
                    studentForm.Title = "Editar la información de usuario";
                    studentForm.FirstName.Text = SelectedStudent!.FirstName;
                    studentForm.LastName.Text = SelectedStudent!.LastName;
                    studentForm.DateOfBirth.Text = SelectedStudent!.DateOfBirth.ToString("dd-MM-yyyy");
                    if (studentForm.ShowDialog().Value)
                    {
                        SelectedStudent.FirstName = studentForm.FirstName.Text;
                        SelectedStudent.LastName = studentForm.LastName.Text;
                        SelectedStudent.DateOfBirth = DateTime.ParseExact(studentForm.DateOfBirth.Text,
                            "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        StudentsLists.Items.Refresh();
                        SaveChanges.IsEnabled = true;
                        action = Action.Edit;
                        editListStudents.Add(SelectedStudent);
                    }
                    
                    break;
                case System.Windows.Input.Key.Insert:
                    studentForm = new StudentForm();
                    Teacher selectedTeacher = TeachersList.SelectedItem as Teacher;
                    studentForm.Title = $"Nuevo estudiante para la clase {selectedTeacher.Class}";
                    if (studentForm.ShowDialog().Value)
                    {
                        Student NewStudent = new Student();
                        NewStudent.TeacherId = selectedTeacher.Id;
                        NewStudent.FirstName = studentForm.FirstName.Text;
                        NewStudent.LastName = studentForm.LastName.Text;
                        NewStudent.DateOfBirth = DateTime.ParseExact(studentForm.DateOfBirth.Text,
                            "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        List<Student> Students = StudentsLists.ItemsSource as List<Student>;
                        Students.Add(NewStudent);
                        StudentsLists.Items.Refresh();
                        SaveChanges.IsEnabled = true;
                        action = Action.Add;
                    }
                    break;
                case System.Windows.Input.Key.Delete:
                    Student SelectedStudentDelete = StudentsLists.SelectedItem as Student;
                    string NombreCompletoStudent = $"{SelectedStudentDelete.FirstName} " +
                        $"{SelectedStudentDelete.LastName}";
                    if (MessageBoxResult.Yes == MessageBox.Show($"¿Está seguro que desea eliminar el estudiante? " +
                        $"{NombreCompletoStudent}",
                        "Eliminar Estudiante", MessageBoxButton.YesNo))
                    {
                        List<Student> Students = StudentsLists.ItemsSource as List<Student>;
                        Students.Remove(SelectedStudentDelete);
                        StudentsLists.Items.Refresh();
                        SaveChanges.IsEnabled = true;
                        action = Action.Remove;
                    }

                    break;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            switch (action)
            {
                case Action.Add:
                    foreach (Student Item in StudentsLists.ItemsSource)
                    {
                        if (Item.Id == 0)
                        {
                            bool result;
                            StudentService service = new StudentService();
                            result = service.AddStudent(Item);
                            string message = result ?
                            $"El estudiante {Item.FirstName} {Item.LastName} Se guardó exitosamente" :
                            $"No se pudo guardar el estudiante {Item.FirstName} {Item.LastName}";

                            MessageBox.Show(message, "Guardar Estudiante", MessageBoxButton.OK);
                            //StudentsLists.Items.Refresh();
                        }
                    }
                    TeachersList_SelectionChanged(null, null);
                    SaveChanges.IsEnabled = false;
                    action = Action.None;
                    break;

                case Action.Remove:
                    List<Student> students = StudentsLists.ItemsSource as List<Student>;
                    foreach (Student item in originalListStudents)
                    {
                        if (students.Exists(s => s.Id == item.Id) == false)
                        {
                            StudentService service = new StudentService();
                            service.DeleteStudent(item);
                        }
                    }
                    SaveChanges.IsEnabled = false;
                    action = Action.None;
                    originalListStudents = new Student[students.Count];
                    students.CopyTo(originalListStudents);
                    break;

                case Action.Edit:
                    foreach (Student item in editListStudents)
                    {
                        StudentService service = new StudentService();
                        service.EditStudent(item);
                    }
                    SaveChanges.IsEnabled = false;
                    action = Action.None;
                    editListStudents.Clear();
                    break;
            }

        }
    }
}
