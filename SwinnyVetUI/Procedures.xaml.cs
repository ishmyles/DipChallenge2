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
using NLog;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace SwinnyVetUI
{
    /// <summary>
    /// Interaction logic for Procedures.xaml
    /// </summary>
    public partial class Procedures : Page
    {
        bool postError;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public Procedures()
        {
            InitializeComponent();
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

        private async void addProcedureBTN_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(procedureIDTB.Text, out int result) == false)
                {
                    throw new ValidationFailureException(procedureIDTB.Text);
                }
                if (procedureIDTB.Text == "" || procedureDescTB.Text == "" || procedurePriceTB.Text == "")
                {
                    throw new ValidationFailureException();
                }
                using (var httpClient = new HttpClient())
                {
                    string resourceAddress = "http://swinnyvetapi101571963.azurewebsites.net/api/Procedures/";

                    Procedure p = new Procedure { ProcedureID = procedureIDTB.Text, ProcedureDesc = procedureDescTB.Text, ProcedurePrice = procedurePriceTB.Text };
                    string postBody = JsonConvert.SerializeObject(p);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await httpClient.PostAsync(resourceAddress, new StringContent(postBody, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode == false)
                    {
                        postError = false;
                        throw new ValidationFailureException();
                    }
                }
                procedureIDTB.Clear();
                procedureDescTB.Clear();
                procedurePriceTB.Clear();
            }
            catch (ValidationFailureException)
            {
                if (postError == false)
                {
                    logger.Debug("Validation Failure Exception: Incorrect Price Input (" + procedurePriceTB.Text + ")");
                    MessageBox.Show("Incorrect Price Value");
                    procedureIDTB.Clear();
                    procedureDescTB.Clear();
                    procedurePriceTB.Clear();
                }
                else if (int.TryParse(procedureIDTB.Text, out int result) == false)
                {
                    logger.Debug("Validation Failure Exception : Incorrect Input (" + procedureIDTB.Text + ")");
                    MessageBox.Show("Please enter valid Owner ID input");
                    procedureIDTB.Clear();
                    procedureDescTB.Clear();
                    procedurePriceTB.Clear();
                }
                else
                {
                    logger.Debug("Validation Failure Exception");
                    MessageBox.Show("Please fill in all fields");
                    procedureIDTB.Clear();
                    procedureDescTB.Clear();
                    procedurePriceTB.Clear();
                }
            }
            catch (Exception)
            {
                logger.Fatal("Fatal Error Exception");
            }
        }
    }
}
