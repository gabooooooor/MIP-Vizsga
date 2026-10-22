using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIPVizsga
{
    public partial class Form1 : Form
    {
        // Database repository
        private IDeliveryRepository repository;

        // Background worker thread
        private Thread workerThread;
        private bool stopRequested = false;

        // Lock for thread safety
        private object lockObject = new object();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize the database
            repository = new SqliteDeliveryRepository();
            repository.Initialize();

            // Load and display all deliveries
            RefreshDataGrid();
            UpdateStatusCounts();
        }

        private void RefreshDataGrid()
        {
            // Get all deliveries from database
            List<Delivery> deliveries = repository.LoadAll();

            // Clear the grid
            dataGridViewDeliveries.DataSource = null;

            // Create a list to bind to the grid
            var displayList = new List<dynamic>();
            foreach (var delivery in deliveries)
            {
                displayList.Add(new
                {
                    Id = delivery.Id,
                    Recipient = delivery.Recipient,
                    Priority = delivery.Priority,
                    Status = delivery.Status
                });
            }

            // Bind to grid
            dataGridViewDeliveries.DataSource = displayList;
        }

        private void UpdateStatusCounts()
        {
            // Get all deliveries
            List<Delivery> deliveries = repository.LoadAll();

            // Count each status using LINQ
            int pending = deliveries.Where(d => d.Status == "Pending").Count();
            int processing = deliveries.Where(d => d.Status == "Processing").Count();
            int completed = deliveries.Where(d => d.Status == "Completed").Count();

            // Update label
            labelCounts.Text = $"Status: Pending={pending}, Processing={processing}, Completed={completed}";
        }

        private void ButtonAddDelivery_Click(object sender, EventArgs e)
        {
            // Get recipient from textbox
            string recipient = textBoxRecipient.Text.Trim();

            // Validate recipient is not empty
            if (string.IsNullOrEmpty(recipient))
            {
                MessageBox.Show("Please enter a recipient name");
                return;
            }

            // Get priority from dropdown
            int priority = int.Parse(comboBoxPriority.SelectedItem.ToString());

            // Create delivery of correct type
            Delivery delivery;
            if (priority == 3)
            {
                delivery = new ExpressDelivery();
            }
            else
            {
                delivery = new RegularDelivery();
            }

            // Set properties
            delivery.Recipient = recipient;
            delivery.Priority = priority;
            delivery.Status = "Pending";

            // Insert into database
            repository.Insert(delivery);

            TraceLogger.LogDeliveryAdded(delivery.Id);

            // Refresh UI
            textBoxRecipient.Text = "";
            RefreshDataGrid();
            UpdateStatusCounts();

            MessageBox.Show("Delivery added!");
        }

        private void ButtonGenerateDataset_Click(object sender, EventArgs e)
        {
            // Generate 110 test deliveries
            List<Delivery> deliveries = DatasetGenerator.GenerateDataset();

            // Insert all into database
            foreach (var delivery in deliveries)
            {
                repository.Insert(delivery);
            }

            // Refresh UI
            RefreshDataGrid();
            UpdateStatusCounts();

            MessageBox.Show("Dataset generated with " + deliveries.Count + " deliveries!");
        }

        private void ButtonStartProcessing_Click(object sender, EventArgs e)
        {
            // Prevent starting if already running
            if (workerThread != null && workerThread.IsAlive)
            {
                MessageBox.Show("Processing already running!");
                return;
            }

            // Reset stop flag
            stopRequested = false;

            // Create and start worker thread
            workerThread = new System.Threading.Thread(ProcessDeliveries);
            workerThread.Start();

            // Update UI
            buttonStartProcessing.Enabled = false;
            buttonStopProcessing.Enabled = true;
        }

        private void ButtonStopProcessing_Click(object sender, EventArgs e)
        {
            // Signal thread to stop
            stopRequested = true;

            // Update UI
            buttonStopProcessing.Enabled = false;
            buttonStartProcessing.Enabled = true;
        }

        private void ProcessDeliveries()
        {
            DeliveryScheduler scheduler = new DeliveryScheduler();

            while (!stopRequested)
            {
                // Get next pending delivery using scheduler
                lock (lockObject)
                {
                    List<Delivery> allDeliveries = repository.LoadAll();

                    // Use scheduler to get next delivery based on priority rules
                    Delivery nextDelivery = scheduler.GetNextDelivery(allDeliveries);

                    if (nextDelivery == null)
                    {
                        // No deliveries left
                        break;
                    }

                    // Update status to Processing
                    repository.UpdateStatus(nextDelivery.Id, "Processing");

                    // Sleep for the processing time (polymorphic call)
                    System.Threading.Thread.Sleep(nextDelivery.GetProcessingDelayMs());

                    // Update status to Completed
                    repository.UpdateStatus(nextDelivery.Id, "Completed");

                    TraceLogger.LogDeliveryCompleted(nextDelivery.Id);
                }

                // Refresh UI on main thread
                this.Invoke(new Action(() =>
                {
                    RefreshDataGrid();
                    UpdateStatusCounts();
                }));
            }

            // Finished - update UI
            this.Invoke(new Action(() =>
            {
                buttonStopProcessing.Enabled = false;
                buttonStartProcessing.Enabled = true;
            }));
        }
    }
}
