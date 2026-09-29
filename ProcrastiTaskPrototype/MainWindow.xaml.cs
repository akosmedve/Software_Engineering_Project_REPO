using System;
using System.IO;
using System.Windows;
//using ProcrastiTaskPrototype.Application;
//.NET5 ????
using ProcrastiTaskPrototype.Persistence;
//.NET5 ????
using TaskApplication = ProcrastiTaskPrototype.Application.TaskManager;
using ProcrastiTaskPrototype.Domain;
using System.Linq;

namespace ProcrastiTaskPrototype
{
    public partial class MainWindow : Window
    {
        private readonly TaskApplication taskManager;
        private ProcrastiTaskPrototype.Domain.Task selectedTask;

        //initcomponenet!!!!!!!
        public MainWindow()
        {
            InitializeComponent();

            string filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "tasks.json");

            ITaskRepository repository =
                new JsonTaskRepository(filePath);

            taskManager = new TaskApplication(repository);
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(ProcrastiTaskPrototype.Domain.TaskStatus));
            PriorityComboBox.ItemsSource = Enum.GetValues(typeof(ProcrastiTaskPrototype.Domain.TaskPriority));
            RefreshTaskList();
        }
        /*private void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StatusComboBox.SelectedItem == null)
                {
                    StatusComboBox.SelectedItem = TaskStatus.tsToDo
                }

                if (PriorityComboBox.SelectedItem = null)
                {
                    PriorityComboBox.SelectedItem = TaskPriority.tpMedium;
                }

                TaskStatus status =
                    (TaskStatus)StatusComboBox.SelectedItem;

                TaskPriority priority =
                    (TaskPriority)PriorityComboBox.SelectedItem;

                ProcrastiTaskPrototype.Domain.Task task =
                    taskManager.CreateTask(
                        TitleTextBox.Text,
                        DescriptionTextBox.Text,
                        status,
                        priority);

                task.DevContext.ConNextStep =
                    NextStepTextBox.Text;

                task.DevContext.ConSourceFile =
                    SourceFileTextBox.Text;

                task.DevContext.ConComponent =
                    ComponentTextBox.Text;

                task.DevContext.ConGitBranch =
                    GitBranchTextBox.Text;

                task.DevContext.ConTechnicalNotes =
                    TechnicalNotesTextBox.Text;

                MessageBox.Show("Task created.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }*/

        //Task button
        private void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTaskFields();

            StatusComboBox.SelectedItem = TaskStatus.tsToDo;
            PriorityComboBox.SelectedItem = TaskPriority.tpMedium;

            selectedTask = null;

            TitleTextBox.Focus();
        }


        /*private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            taskManager.SaveTasks();
            MessageBox.Show("Tasks saved.");
        }*/
        
        //save button
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedTask == null)
                {
                    if (StatusComboBox.SelectedItem == null)
                    {
                        StatusComboBox.SelectedItem = TaskStatus.tsToDo;
                    }

                    if (PriorityComboBox.SelectedItem == null)
                    {
                        PriorityComboBox.SelectedItem = TaskPriority.tpMedium;
                    }

                    TaskStatus status =
                        (TaskStatus)StatusComboBox.SelectedItem;

                    TaskPriority priority =
                        (TaskPriority)PriorityComboBox.SelectedItem;

                    selectedTask = taskManager.CreateTask(
                        TitleTextBox.Text,
                        DescriptionTextBox.Text,
                        status,
                        priority);
                }
                else
                {
                    selectedTask.TaskTitle = TitleTextBox.Text;
                    selectedTask.TaskDescription = DescriptionTextBox.Text;

                    selectedTask.TaskStatus =
                        (TaskStatus)StatusComboBox.SelectedItem;

                    selectedTask.TaskPriority =
                        (TaskPriority)PriorityComboBox.SelectedItem;
                }

                selectedTask.DevContext.ConCurrentDevelopmentState =
                    CurrentStateTextBox.Text;

                selectedTask.DevContext.ConNextStep =
                    NextStepTextBox.Text;

                selectedTask.DevContext.ConSourceFile =
                    SourceFileTextBox.Text;

                selectedTask.DevContext.ConComponent =
                    ComponentTextBox.Text;

                selectedTask.DevContext.ConGitBranch =
                    GitBranchTextBox.Text;

                selectedTask.DevContext.ConTechnicalNotes =
                    TechnicalNotesTextBox.Text;

                taskManager.SaveTasks();

                RefreshTaskList();

                MessageBox.Show("Task saved.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        //Delete button
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTask == null)
            {
                MessageBox.Show("Please select a task to delete.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete this task?",
                "Delete Task",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            taskManager.DeleteTask(selectedTask);

            taskManager.SaveTasks();

            selectedTask = null;

            ClearTaskFields();

            RefreshTaskList();

            MessageBox.Show("Task deleted.");
        }

        private void RefreshTaskList()
        {   
            //DEBUG for task visibility
            /*MessageBox.Show(
            "Number of tasks: " + taskManager.GetTasks().Count);*/

            TaskList.ItemsSource = null;
            TaskList.ItemsSource = taskManager.GetTasks();
        }

        private void ClearTaskFields()
        {
            TitleTextBox.Clear();
            DescriptionTextBox.Clear();

            CurrentStateTextBox.Clear();
            NextStepTextBox.Clear();
            SourceFileTextBox.Clear();
            ComponentTextBox.Clear();
            GitBranchTextBox.Clear();
            TechnicalNotesTextBox.Clear();
        }

        private void TaskList_SelectionChanged(
    object sender,
    System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedTask = TaskList.SelectedItem as ProcrastiTaskPrototype.Domain.Task;

            if (selectedTask == null)
            {
                return;
            }

            TitleTextBox.Text = selectedTask.TaskTitle;
            DescriptionTextBox.Text = selectedTask.TaskDescription;

            StatusComboBox.SelectedItem = selectedTask.TaskStatus;
            PriorityComboBox.SelectedItem = selectedTask.TaskPriority;

            if (selectedTask.DevContext != null)
            {
                CurrentStateTextBox.Text =
                    selectedTask.DevContext.ConCurrentDevelopmentState;

                NextStepTextBox.Text =
                    selectedTask.DevContext.ConNextStep;

                SourceFileTextBox.Text =
                    selectedTask.DevContext.ConSourceFile;

                ComponentTextBox.Text =
                    selectedTask.DevContext.ConComponent;

                GitBranchTextBox.Text =
                    selectedTask.DevContext.ConGitBranch;

                TechnicalNotesTextBox.Text =
                    selectedTask.DevContext.ConTechnicalNotes;
            }
        }
    }

}