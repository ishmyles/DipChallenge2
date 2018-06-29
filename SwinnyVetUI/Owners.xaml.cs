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
using System.Net.Http.Headers;

namespace SwinnyVetUI
{
    /// <summary>
    /// Interaction logic for Owners.xaml
    /// </summary>
    public partial class Owners : Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public Owners()
        {
            InitializeComponent();
        }

        private async void addOwnerBTN_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(ownerIDTB.Text, out int result) == false)
                {
                    throw new ValidationFailureException(ownerIDTB.Text);
                }
                if (ownerIDTB.Text == "" || firstnameTB.Text == "" || lastnameTB.Text == "" || phoneNoTB.Text == "")
                {
                    throw new ValidationFailureException();
                }
                using (var httpClient = new HttpClient())
                {
                    string resourceAddress = "http://swinnyvetapi101571963.azurewebsites.net/api/Owners/";

                    Owner o = new Owner { OwnerID = ownerIDTB.Text, GivenName = firstnameTB.Text, Surname = lastnameTB.Text, PhoneNo = phoneNoTB.Text };
                    string postBody = JsonConvert.SerializeObject(o);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await httpClient.PostAsync(resourceAddress, new StringContent(postBody, Encoding.UTF8, "application/json"));
                }
                ownerIDTB.Clear();
                firstnameTB.Clear();
                lastnameTB.Clear();
                phoneNoTB.Clear();
            }
            catch (ValidationFailureException)
            {
                if (int.TryParse(ownerIDTB.Text, out int result) == false)
                {
                    logger.Debug("Validation Failure Exception : Incorrect Input (" + ownerIDTB.Text + ")");
                    MessageBox.Show("Please enter valid Owner ID input");
                    ownerIDTB.Clear();
                    firstnameTB.Clear();
                    lastnameTB.Clear();
                    phoneNoTB.Clear();
                }
                else
                {
                    logger.Debug("Validation Failure Exception");
                    MessageBox.Show("Please fill in all fields");
                    ownerIDTB.Clear();
                    firstnameTB.Clear();
                    lastnameTB.Clear();
                    phoneNoTB.Clear();
                }
            }
            catch (Exception)
            {
                logger.Fatal("Fatal Error Exception");
            }
        }

        private async void editOwnerBTN_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(ownerIDTB.Text, out int result) == false)
                {
                    throw new ValidationFailureException();
                }
                if (ownerIDTB.Text == "" || firstnameTB.Text == "" || lastnameTB.Text == "" || phoneNoTB.Text == "")
                {
                    throw new ValidationFailureException();
                }
                using (var client = new HttpClient())
                {
                var updatedClient = new Owner()
                {
                    OwnerID = ownerIDTB.Text,
                    GivenName = firstnameTB.Text,
                    Surname = lastnameTB.Text,
                    PhoneNo = phoneNoTB.Text
                };

                var updatedData = JsonConvert.SerializeObject(updatedClient);
                var stringContent = new StringContent(updatedData, UnicodeEncoding.UTF8, "application/json");
                client.BaseAddress = new Uri("http://swinnyvetapi101571963.azurewebsites.net/api/Owners/");
                HttpResponseMessage response = await client.PutAsync(ownerIDTB.Text, stringContent);
            }
            ownerIDTB.Clear();
            firstnameTB.Clear();
            lastnameTB.Clear();
            phoneNoTB.Clear();
            }
            catch (ValidationFailureException)
            {
                if (int.TryParse(ownerIDTB.Text, out int result) == false)
                {
                    logger.Debug("Validation Failure Exception : Incorrect Input (" + ownerIDTB.Text + ")");
                    MessageBox.Show("Please enter valid Owner ID input");
                    ownerIDTB.Clear();
                    firstnameTB.Clear();
                    lastnameTB.Clear();
                    phoneNoTB.Clear();
                }
                else
                {
                    logger.Debug("Validation Failure Exception");
                    MessageBox.Show("Please fill in all fields");
                    ownerIDTB.Clear();
                    firstnameTB.Clear();
                    lastnameTB.Clear();
                    phoneNoTB.Clear();
                }
            }
            catch (Exception)
            {
                logger.Fatal("Fatal Error Exception");
            }
        }

        private async void deleteOwnerBTN_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(ownerIDTB.Text, out int result) == false)
                {
                    throw new ValidationFailureException(ownerIDTB.Text);
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://swinnyvetapi101571963.azurewebsites.net/api/Owners/");
                    HttpResponseMessage response = await client.DeleteAsync(ownerIDTB.Text);
                }
                ownerIDTB.Clear();
            }
            catch (ValidationFailureException)
            {
                logger.Debug("Validation Failure Exception : Incorrect Input (" + ownerIDTB.Text + ")");
                MessageBox.Show("Please enter valid Owner ID");
                ownerIDTB.Clear();
                firstnameTB.Clear();
                lastnameTB.Clear();
                phoneNoTB.Clear();
            }
            catch (Exception)
            {
                logger.Fatal("Fatal Error Exception");
            }

        }

        private async void viewOwnersBTN_click(object sender, RoutedEventArgs e)
        {
            ViewAllOwnersGrid.Visibility = Visibility.Visible;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://swinnyvetapi101571963.azurewebsites.net/api/");
                HttpResponseMessage response = await client.GetAsync("Owners");
                var content = await response.Content.ReadAsStringAsync();
                var owners = JsonConvert.DeserializeObject<List<Owner>>(content);
                OwnersLV.ItemsSource = owners;
            }
        }

        private void treatmentsBTN_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProcedureTreatment());
        }

        private void ownersBTN_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Owners());
        }

        private void ownersBackBTN_click(object sender, RoutedEventArgs e)
        {
            ViewAllOwnersGrid.Visibility = Visibility.Hidden;
        }

        private void proceduresBTN_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Procedures());
        }
    }
}
