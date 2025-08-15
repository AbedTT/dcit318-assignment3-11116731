using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthcareSystem
{
    public class HealthSystemApp
    {
        private readonly Repository<Patient> _patientRepo = new Repository<Patient>();
        private readonly Repository<Prescription> _prescriptionRepo = new Repository<Prescription>();
        private readonly Dictionary<int, List<Prescription>> _prescriptionMap = new Dictionary<int, List<Prescription>>();

        public void Run()
        {
            Console.WriteLine("Welcome to the Erudite Healthcare Solutions System!");
            SeedData();
            BuildPrescriptionMap();

            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PrintAllPatients();
                        break;
                    case "2":
                        GetPrescriptionsForPatient();
                        break;
                    case "3":
                        AddNewPatient();
                        break;
                    case "4":
                        AddNewPrescription();
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("We appreciate you making use of our system. Stay healthy, byee!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. View all patients");
            Console.WriteLine("2. View prescriptions for a patient");
            Console.WriteLine("3. Add a new patient");
            Console.WriteLine("4. Add a new prescription");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
        }

        public void SeedData()
        {
            // Add initial Patient objects
            _patientRepo.Add(new Patient(101, "Aisha Alima", 34, "Female", "Malaria"));
            _patientRepo.Add(new Patient(102, "Nii Mensah", 52, "Male", "Diabetes"));
            _patientRepo.Add(new Patient(103, "Grace Amoah", 28, "Female", "Typhoid"));

            // Add initial Prescription objects
            _prescriptionRepo.Add(new Prescription(1, 101, "Lufart", DateTime.Now.AddDays(-10)));
            _prescriptionRepo.Add(new Prescription(2, 102, "Metformin", DateTime.Now.AddDays(-5)));
            _prescriptionRepo.Add(new Prescription(3, 101, "Paracetamol", DateTime.Now.AddDays(-2)));
            _prescriptionRepo.Add(new Prescription(4, 103, "Ciprofloxacin", DateTime.Now.AddDays(-15)));
            _prescriptionRepo.Add(new Prescription(5, 102, "Lipitor", DateTime.Now.AddDays(-7)));
        }

        public void BuildPrescriptionMap()
        {
            List<Prescription> allPrescriptions = _prescriptionRepo.GetAll();
            var groupedPrescriptions = allPrescriptions.GroupBy(p => p.PatientId);

            _prescriptionMap.Clear();
            foreach (var group in groupedPrescriptions)
            {
                _prescriptionMap[group.Key] = group.ToList();
            }
        }

        private void PrintAllPatients()
        {
            Console.WriteLine("\n--- All Patients ---");
            foreach (var patient in _patientRepo.GetAll())
            {
                Console.WriteLine(
                    $"ID: {patient.Id}, " +
                    $"Name: {patient.Name}, " +
                    $"Age: {patient.Age}, " +
                    $"Gender: {patient.Gender}, " +
                    $"Illness: {patient.Illness}"
                    );
            }
        }

        private void GetPrescriptionsForPatient()
        {
            Console.Write("Enter Patient ID: ");
            if (int.TryParse(Console.ReadLine(), out int patientId))
            {
                PrintPrescriptionsForPatient(patientId);
            }
            else
            {
                Console.WriteLine("Invalid Patient ID. Please enter a number.");
            }
        }

        private void PrintPrescriptionsForPatient(int patientId)
        {
            Console.WriteLine($"\n--- Prescriptions for Patient ID: {patientId} ---");
            if (_prescriptionMap.TryGetValue(patientId, out List<Prescription>? prescriptions) && prescriptions != null)
            {
                foreach (var prescription in prescriptions)
                {
                    Console.WriteLine($"  Prescription ID: {prescription.Id}, Medication: {prescription.MedicationName}, Date Issued: {prescription.DateIssued.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine("  No prescriptions found for this patient.");
            }
        }

        private void AddNewPatient()
        {
            Console.WriteLine("\n--- Add New Patient ---");
            Console.Write("Enter patient name: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter patient age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Invalid age. Patient not added.");
                return;
            }
            Console.Write("Enter patient gender: ");
            string gender = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter patient condition/illness: ");
            string illness = Console.ReadLine() ?? string.Empty;

            int newId = _patientRepo.GetAll().Count > 0 ? _patientRepo.GetAll().Max(p => p.Id) + 1 : 101;
            Patient newPatient = new Patient(newId, name, age, gender, illness);
            _patientRepo.Add(newPatient);
            Console.WriteLine($"Patient '{newPatient.Name}' with ID {newPatient.Id} added successfully!");
        }

        private void AddNewPrescription()
        {
            Console.WriteLine("\n--- Add New Prescription ---");
            Console.Write("Enter patient ID for prescription: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("Invalid Patient ID. Prescription not added.");
                return;
            }

            // Check if patient exists
            if (_patientRepo.GetById(p => p.Id == patientId) == null)
            {
                Console.WriteLine("Patient with this ID does not exist. Prescription not added.");
                return;
            }

            Console.Write("Enter medication name: ");
            string medicationName = Console.ReadLine() ?? string.Empty;

            int newId = _prescriptionRepo.GetAll().Count > 0 ? _prescriptionRepo.GetAll().Max(p => p.Id) + 1 : 1;
            Prescription newPrescription = new Prescription(newId, patientId, medicationName, DateTime.Now);
            _prescriptionRepo.Add(newPrescription);

            // Rebuild map to include new prescription
            BuildPrescriptionMap();

            Console.WriteLine($"Prescription for '{medicationName}' added successfully for Patient ID {patientId}!");
        }
    }
}