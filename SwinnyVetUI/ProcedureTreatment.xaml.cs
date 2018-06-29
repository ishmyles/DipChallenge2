using System;
using System.Collections.Generic;
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
using SwinnyVetUI.Classes;
using System.Net.Http;
using Newtonsoft.Json;

namespace SwinnyVetUI
{
    /// <summary>
    /// Interaction logic for ProcedureTreatment.xaml
    /// </summary>
    public partial class ProcedureTreatment : Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ProcedureTreatment()
        {
            InitializeComponent();
        }

        private async void searchTreatmentsBTN_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(int.TryParse(procedureNoTB.Text, out int result) == false)
                {
                    throw new ValidationFailureException(procedureNoTB.Text);
                }

                TreatmentsGrid.Visibility = Visibility.Visible;

                using (var client = new HttpClient())
                {
                    List<Treatment> sTreatment = new List<Treatment>();
                    client.BaseAddress = new Uri("http://swinnyvetapi101571963.azurewebsites.net/api/");
                    HttpResponseMessage response = await client.GetAsync("Treatments");
                    var content = await response.Content.ReadAsStringAsync();
                    var treatments = JsonConvert.DeserializeObject<List<Treatment>>(content);

                    for (int i = 0; i < treatments.Count; i++)
                    {
                        if (treatments[i].ProcedureID == procedureNoTB.Text.ToString())
                        {
                            sTreatment.Add(new Treatment()
                            {
                                PetName = treatments[i].PetName,
                                OwnerID = treatments[i].OwnerID,
                                ProcedureID = treatments[i].ProcedureID,
                                TreatmentDate = treatments[i].TreatmentDate,
                                TreatmentNotes = treatments[i].TreatmentNotes,
                                TreatmentPrice = treatments[i].TreatmentPrice,
                            });
                        }
                    }
                    TreatmentsLV.ItemsSource = sTreatment;
                }
                procedureNoTB.Clear();
            }
            catch(ValidationFailureException)
            {
                logger.Debug("Validation Failure Exception : Incorrect Input (" + procedureNoTB.Text + ")");
                MessageBox.Show("Please enter a valid Procedure ID");
                procedureNoTB.Clear();
            }
            catch(Exception)
            {
                logger.Fatal("Fatal Error Exception");
            }
        }

        private async void viewProcedureBTN_click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://swinnyvetapi101571963.azurewebsites.net/api/");
                HttpResponseMessage response = await client.GetAsync("Procedures");
                var content = await response.Content.ReadAsStringAsync();
                var procedures = JsonConvert.DeserializeObject<List<Procedure>>(content);
                ProcedureLV.ItemsSource = procedures;
            }
        }

        private void backBTN_click(object sender, RoutedEventArgs e)
        {
            TreatmentsGrid.Visibility = Visibility.Hidden;
        }

        private void treatmentsBTN_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProcedureTreatment());
        }

        private void ownersBTN_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Owners());
        }

        private void proceduresBTN_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Procedures());
        }
    }
}
