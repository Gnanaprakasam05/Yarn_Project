using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using yarn_brokerage.Models;

namespace yarn_brokerage.ViewModels
{
    public class PerformanceViewModel : BaseViewModel
    {
        public ObservableCollection<TeamGroupPerformance> TeamGroupPerformance { get; set; }
        public ObservableCollection<TeamPerformance> TeamPerformance { get; set; }
        public PerformanceViewModel()
        {
            TeamGroupPerformance = new ObservableCollection<TeamGroupPerformance>();
            TeamPerformance = new ObservableCollection<TeamPerformance>();

        }

        public async Task<ObservableCollection<TeamGroupPerformance>> ExecuteLoadTeamGroupDataCommand()
        {

            IsBusy = true;


            try
            {

                TeamGroupPerformance.Clear();

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    using (var cl = new HttpClient())
                    {

                        HttpContent formcontent = null;

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"].ToString() + "api/team_group_performance_data_for_app", formcontent);

                        var response = await request.Content.ReadAsStringAsync();

                        var teams = JsonConvert.DeserializeObject<ObservableCollection<TeamGroupPerformance>>(response);


                        foreach (var item in teams)
                        {
                            int bags = ((int.Parse(item.ConfirmedBagsPercentage) + int.Parse(item.DispatchedBagsPercentage)) / 2);
                            int com = (((int.Parse(item.ConfirmedCommissionPercentage) + int.Parse(item.DispatchedCommissionPercentage) + int.Parse(item.ActualCommissionPercentage)) / 3));

                            item.ConfirmedBagsPercentage = (bags == 0) ? "" : bags.ToString() + "%";
                            item.ConfirmedCommissionPercentage = (com == 0) ? "" : com.ToString() + "%";
                           
                            TeamGroupPerformance.Add(item);
                        }

                        //var index_value = TeamGroupPerformance.IndexOf(TeamGroupPerformance.Where(x =>( int.Parse(x.ActualCommissionPercentage) == 0).FirstOrDefault());
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            return TeamGroupPerformance;
        }


        public async Task ExecuteLoadTeamPerformanceDataCommand(int TeamGroupid)
        {

            IsBusy = true;


            try
            {

                TeamPerformance.Clear();

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    using (var cl = new HttpClient())
                    {

                        HttpContent formcontent = null;

                        formcontent = new FormUrlEncodedContent(new[]
                       {
                                new KeyValuePair<string,string>("team_group_id",TeamGroupid.ToString()),

                            });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"].ToString() + "api/team_performance_data_for_app", formcontent);

                        var response = await request.Content.ReadAsStringAsync();

                        var teams = JsonConvert.DeserializeObject<ObservableCollection<TeamPerformance>>(response);


                        foreach (var item in teams)
                        {

                            int bags = ((int.Parse(item.ConfirmedBagsPercentage) + int.Parse(item.DispatchedBagsPercentage)) / 2);
                            int com = (( (int.Parse(item.ConfirmedCommissionPercentage) + int.Parse(item.DispatchedCommissionPercentage) + int.Parse(item.ActualCommissionPercentage)) / 3));

                            item.ConfirmedBagsPercentage = ( bags == 0 ) ? "" : bags.ToString() + "%";
                            item.ConfirmedCommissionPercentage = (com == 0) ? "" : com.ToString() + "%";

                            TeamPerformance.Add(item);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }

        public async Task<Performance> ExecuteTeamPerformanceDataCommand(int teamid)
        {
            Performance responce = new Performance();

            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    using (var cl = new HttpClient())
                    {

                        HttpContent formcontent = null;


                        formcontent = new FormUrlEncodedContent(new[]
                        {
                                        new KeyValuePair<string,string>("team_id",teamid.ToString()),
                            });


                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"].ToString() + "api/whatsapp_data_from_app", formcontent);

                        var response = await request.Content.ReadAsStringAsync();

                        Performance res = Newtonsoft.Json.JsonConvert.DeserializeObject<Performance>(response);

                        Title = res.ConfirmedBags.TeamGroupName;
                        responce = res;
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            return responce;
        }

        public async Task<Performance> ExecuteTeamPerformanceCommand(int teamid)
        {
            Performance responce = new Performance();

            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    using (var cl = new HttpClient())
                    {

                        HttpContent formcontent = null;


                        formcontent = new FormUrlEncodedContent(new[]
                        {
                                        new KeyValuePair<string,string>("team_group_id",teamid.ToString()),
                            });


                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"].ToString() + "api/whatsapp_data_from_app", formcontent);

                        var response = await request.Content.ReadAsStringAsync();

                        Performance res = Newtonsoft.Json.JsonConvert.DeserializeObject<Performance>(response);

                        Title = res.ConfirmedBags.TeamGroupName;
                        responce = res;
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            return responce;
        }

    }
}
